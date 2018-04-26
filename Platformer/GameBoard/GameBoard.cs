using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;

namespace Platformer
{
    class GameBoard
    {
        #region Member variables
        List<Platform> myPlatforms;
        List<Enemy> myEnemies;
        List<PowerUpBall> myPowerUpBalls;
        List<Projectile> myProjectiles;
        LifeHeart myLifeHeart;
        Background myBackground;

        float myMapHeight;
        int myCurrentLevel;
        const int NumberOfLevels = 4;
        #endregion

        #region Properties
        static public int PlaythroughTime
        {
            get;
            private set;
        }

        public Player Player
        {
            get;
            private set;
        }
        #endregion

        #region Constructors
        public GameBoard(SpriteFont aFont)
        {
            InitializeMemberVariables(aFont);
        }
        #endregion

        #region Events
        public event EventHandler GameOver;

        public event EventHandler WonGame;
        #endregion

        #region Public methods
        public void Reset()
        {
            ClearLevel();
            InitializeLevel();
        }

        public void Update(GameTime aGameTime)
        {
            UpdatePlaythroughTime(aGameTime);
            myBackground.Update(Player);
            UpdateCharacters(aGameTime);
            UpdatePlatforms(aGameTime);
            myLifeHeart.Update(Player);

            HandleCollision();
        }

        public void Draw(SpriteBatch aSpriteBatch)
        {
            myBackground.Draw(aSpriteBatch);
            DrawCharacters(aSpriteBatch);
            DrawPlatforms(aSpriteBatch);
            myLifeHeart.Draw(aSpriteBatch);
        }
        #endregion

        #region Private methods

        private void HandleCollision()
        {
            HandlePlatformCollision();
            HandleEnemyCollision();
            HandleProjectileCollision();
            HandlePowerUpBallCollision();
        }

        private void HandlePowerUpBallCollision()
        {
            foreach (PowerUpBall powerUpBall in myPowerUpBalls)
            {
                powerUpBall.Collision(Player);
            }
        }

        private void HandleProjectileCollision()
        {
            foreach (Projectile projectile in myProjectiles)
            {
                if (projectile is PlayerProjectile)
                {
                    foreach (Enemy enemy in myEnemies)
                    {
                        if (enemy.Hitbox.Intersects(projectile.Hitbox))
                        {
                            enemy.TakeDamage();
                            projectile.TakeDamage();
                        }
                    }
                }
                else
                {
                    if (Player.Hitbox.Intersects(projectile.Hitbox))
                    {
                        Player.TakeDamage();
                        projectile.TakeDamage();
                    }
                }
            }
        }

        private void HandleEnemyCollision()
        {
            foreach (Enemy enemy in myEnemies)
            {
                if (PlayerCollidesWithEnemy(enemy))
                {
                    Player.Collision(enemy);
                }
            }
        }

        private void HandlePlatformCollision()
        {
            foreach (Platform platform in myPlatforms)
            {
                if (PlayerCollidesWithPlatform(platform))
                {
                    platform.Collision(Player);
                }

                foreach (Enemy enemy in myEnemies)
                {
                    if (enemy.Hitbox.Intersects(platform.Hitbox))
                    {
                        enemy.Collision(platform);
                        platform.Collision(enemy);
                    }
                }

                foreach (PowerUpBall powerUpBall in myPowerUpBalls)
                {
                    if (powerUpBall.Hitbox.Intersects(platform.Hitbox))
                    {
                        platform.Collision(powerUpBall);
                    }
                }

                foreach (Projectile projectile in myProjectiles)
                {
                    if (!(projectile is TurretProjectile) && projectile.Hitbox.Intersects(platform.Hitbox))
                    {
                        projectile.TakeDamage();
                    }
                }
            }
        }

        private List<string> ReadFromFile()
        {
            List<string> strings = new List<string>();
            using (StreamReader myStreamReader = new StreamReader("../../../Content/Levels/" + myCurrentLevel + ".lvl"))
            {
                while (!myStreamReader.EndOfStream)
                {
                    strings.Add(myStreamReader.ReadLine());
                }
            }
            return strings;
        }

        private void InstructPlayer(List<string> aVariableString)
        {
            float positionX = float.Parse(aVariableString[0]);
            float positionY = float.Parse(aVariableString[1]);

            Player.Position = new Vector2(positionX, positionY);
            Player.ShootProjectile += ShootProjectile;
            Player.TookDamage += Reset;
        }

        private void CreateBlock(List<string> aVariableString)
        {
            float positionX = float.Parse(aVariableString[0]);
            float positionY = float.Parse(aVariableString[1]);
            int width = int.Parse(aVariableString[2]);
            int height = int.Parse(aVariableString[3]);
            myPlatforms.Add(new Block(new Vector2(positionX, positionY), width, height));
        }

        private void CreateSpike(List<string> aVariableString)
        {
            float positionX = float.Parse(aVariableString[0]);
            float positionY = float.Parse(aVariableString[1]);
            myPlatforms.Add(new Spike(new Vector2(positionX, positionY)));
        }

        private void CreateWall(List<string> aVariableString)
        {
            float positionX = float.Parse(aVariableString[0]);
            float positionY = float.Parse(aVariableString[1]);
            int width = int.Parse(aVariableString[2]);
            int height = int.Parse(aVariableString[3]);
            myPlatforms.Add(new Wall(new Vector2(positionX, positionY), width, height));
        }

        private void CreateRope(List<string> aVariableString)
        {
            float positionX = float.Parse(aVariableString[0]);
            float positionY = float.Parse(aVariableString[1]);
            int width = int.Parse(aVariableString[2]);
            int height = int.Parse(aVariableString[3]);
            myPlatforms.Add(new Rope(new Vector2(positionX, positionY), width, height));
        }

        private void CreateTrapDoor(List<string> aVariableString)
        {
            float positionX = float.Parse(aVariableString[0]);
            float positionY = float.Parse(aVariableString[1]);
            int width = int.Parse(aVariableString[2]);
            int height = int.Parse(aVariableString[3]);
            myPlatforms.Add(new TrapDoor(new Vector2(positionX, positionY), width, height));
        }

        private void CreateTrampoline(List<string> aVariableString)
        {
            float positionX = float.Parse(aVariableString[0]);
            float positionY = float.Parse(aVariableString[1]);
            int width = int.Parse(aVariableString[2]);
            int height = int.Parse(aVariableString[3]);
            myPlatforms.Add(new Trampoline(new Vector2(positionX, positionY), width, height));
        }

        private void CreateMovingPlatformVertical(List<string> aVariableString)
        {
            float positionX = float.Parse(aVariableString[0]);
            float positionY = float.Parse(aVariableString[1]);
            int width = int.Parse(aVariableString[2]);
            int height = int.Parse(aVariableString[3]);
            myPlatforms.Add(new MovingPlatform(MovingPlatformType.Vertical, new Vector2(positionX, positionY), width, height));
        }

        private void CreateMovingPlatformHorizontal(List<string> aVariableString)
        {
            float positionX = float.Parse(aVariableString[0]);
            float positionY = float.Parse(aVariableString[1]);
            int width = int.Parse(aVariableString[2]);
            int height = int.Parse(aVariableString[3]);
            myPlatforms.Add(new MovingPlatform(MovingPlatformType.Horizontal, new Vector2(positionX, positionY), width, height));
        }

        private void CreatePowerUpContainer(List<string> aVariableString)
        {
            float positionX = float.Parse(aVariableString[0]);
            float positionY = float.Parse(aVariableString[1]);
            int powerUp = int.Parse(aVariableString[2]);
            PowerUpType powerUpType = PowerUpType.None;

            switch (powerUp)
            {
                case 0:
                    powerUpType = PowerUpType.Violet;
                    break;
                case 1:
                    powerUpType = PowerUpType.Blue;
                    break;
                case 2:
                    powerUpType = PowerUpType.Orange;
                    break;
                case 3:
                    powerUpType = PowerUpType.Green;
                    break;
                case 4:
                    powerUpType = PowerUpType.None;
                    break;
            }

            PowerUpContainer powerUpContainer = new PowerUpContainer(new Vector2(positionX, positionY), powerUpType);
            powerUpContainer.SpawnPowerUpBall += SpawnPowerUpBall;
            myPlatforms.Add(powerUpContainer);
        }

        private void CreateTurret(List<string> aVariableString)
        {
            float positionX = float.Parse(aVariableString[0]);
            float positionY = float.Parse(aVariableString[1]);
            int readDirection = int.Parse(aVariableString[2]);

            Direction direction = readDirection == 1 ? Direction.Left : Direction.Right;

            Turret turret = new Turret(new Vector2(positionX, positionY), direction, Player);
            turret.ShootProjectile += ShootProjectile;
            myPlatforms.Add(turret);
        }

        private void CreateEnemy(List<string> aVariableString)
        {
            float positionX = float.Parse(aVariableString[0]);
            float positionY = float.Parse(aVariableString[1]);
            myEnemies.Add(new Enemy(new Vector2(positionX, positionY)));
        }

        private void CreateJumpingEnemy(List<string> aVariableString)
        {
            float positionX = float.Parse(aVariableString[0]);
            float positionY = float.Parse(aVariableString[1]);
            myEnemies.Add(new JumpingEnemy(new Vector2(positionX, positionY)));
        }

        private void CreateShootingEnemy(List<string> aVariableString)
        {
            float positionX = float.Parse(aVariableString[0]);
            float positionY = float.Parse(aVariableString[1]);

            ShootingEnemy shootingEnemy = new ShootingEnemy(new Vector2(positionX, positionY));
            shootingEnemy.ShootProjectile += ShootProjectile;
            myEnemies.Add(shootingEnemy);
        }

        private void CreateGoal(List<string> aVariableString)
        {
            float positionX = float.Parse(aVariableString[0]);
            float positionY = float.Parse(aVariableString[1]);
            int width = int.Parse(aVariableString[2]);
            int height = int.Parse(aVariableString[3]);

            Goal goal = new Goal(new Vector2(positionX, positionY), width, height);
            goal.GoalReached += LevelObjectiveCompleted;
            myPlatforms.Add(goal);
        }

        private void InstructCamera(List<string> aVariableString)
        {
            float positionX = float.Parse(aVariableString[0]);
            float positionY = float.Parse(aVariableString[1]);

            Camera.Initlaize(Player, new Rectangle(0, 0, (int)positionX, (int)positionY));
            myMapHeight = positionY;
        }

        private void InstructionsFromFile(int aRowIndex, List<string> aVariableString)
        {
            switch (aRowIndex)
            {
                case 0: InstructPlayer(aVariableString); break;
                case 1: CreateBlock(aVariableString); break;
                case 2: CreateSpike(aVariableString); break;
                case 3: CreateWall(aVariableString); break;
                case 4: CreateRope(aVariableString); break;
                case 5: CreateTrapDoor(aVariableString); break;
                case 6: CreateTrampoline(aVariableString); break;
                case 7: CreateMovingPlatformVertical(aVariableString); break;
                case 8: CreateMovingPlatformHorizontal(aVariableString); break;
                case 9: CreatePowerUpContainer(aVariableString); break;
                case 10: CreateTurret(aVariableString); break;
                case 11: CreateEnemy(aVariableString); break;
                case 12: CreateJumpingEnemy(aVariableString); break;
                case 13: CreateShootingEnemy(aVariableString); break;
                case 14: CreateGoal(aVariableString); break;
                case 15: InstructCamera(aVariableString); break;
            }
        }

        private void InitializeLevel()
        {
            List<string> strings = ReadFromFile();

            for (int i = 0; i < strings.Count; i++)
            {
                List<string> objectString = new List<string>(strings[i].Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries));

                foreach (string variableText in objectString)
                {
                    List<string> variableString = new List<string>(variableText.Split(','));
                    InstructionsFromFile(i, variableString);
                }
            }
        }

        private bool PlayerCollidesWithPlatform(Platform aPlatform)
        {
            return (Player.Hitbox.Intersects(aPlatform.Hitbox));
        }

        private bool PlayerCollidesWithEnemy(Enemy aEnemy)
        {
            return (Player.Hitbox.Intersects(aEnemy.Hitbox));
        }

        private void ChangeLevel()
        {
            myCurrentLevel++;
            ClearLevel();
            InitializeLevel();
        }

        private void ClearLevel()
        {
            myPlatforms = new List<Platform>();
            myEnemies = new List<Enemy>();
            myPowerUpBalls = new List<PowerUpBall>();
            myProjectiles = new List<Projectile>();
            myBackground = new Background();
        }

        private void ApplyGravity(GameTime aGameTime, Character aCharacter)
        {
            PhysicsManager.Gravity(aGameTime, aCharacter);
        }

        private void LevelObjectiveCompleted(object aSender, EventArgs aEventArg)
        {
            if (myCurrentLevel < NumberOfLevels)
            {
                ChangeLevel();
            }
            else
            {
                WonGame(this, EventArgs.Empty);
            }
        }

        private void SpawnPowerUpBall(object aSender, EventArgs aEventArg)
        {
            myPowerUpBalls.Add(new PowerUpBall((aSender as PowerUpContainer).PowerUpType, (aSender as PowerUpContainer), Player));
        }

        private void ShootProjectile(object aSender, EventArgs aEventArg)
        {
            if (aSender is Player)
            {
                myProjectiles.Add(new PlayerProjectile(aSender as Player));
            }
            else if (aSender is Turret)
            {
                myProjectiles.Add(new TurretProjectile(aSender as Turret));
            }
            else if (aSender is ShootingEnemy)
            {
                myProjectiles.Add(new EnemyProjectile(aSender as ShootingEnemy));
            }
            SoundEffectManager.PlayShootSound();
        }

        private void Reset(object aSender, EventArgs aEventArg)
        {
            Reset();
        }

        private void InitializeMemberVariables(SpriteFont aFont)
        {
            PlaythroughTime = 0;
            myCurrentLevel = 1;

            Player = new Player(Vector2.Zero);
            myLifeHeart = new LifeHeart(aFont);

            Reset();
        }

        private void UpdatePlaythroughTime(GameTime aGameTime)
        {
            PlaythroughTime += aGameTime.ElapsedGameTime.Milliseconds;
        }

        private void ApplyGravityToAllCharacters(GameTime aGameTime)
        {
            ApplyGravity(aGameTime, Player);
            foreach (Enemy enemy in myEnemies)
            {
                ApplyGravity(aGameTime, enemy);
            }
            foreach (PowerUpBall powerUpBall in myPowerUpBalls)
            {
                ApplyGravity(aGameTime, powerUpBall);
            }
        }

        private void UpdateEnemies(GameTime aGameTime)
        {
            for (int i = myEnemies.Count - 1; i >= 0; i--)
            {
                if (myEnemies[i].IsDead == true)
                {
                    myEnemies.RemoveAt(i);
                }
                else
                {
                    myEnemies[i].Update(aGameTime);
                }
            }
        }

        private void UpdatePowerUpBalls(GameTime aGameTime)
        {
            for (int i = myPowerUpBalls.Count - 1; i >= 0; i--)
            {
                if (myPowerUpBalls[i].IsEaten == true)
                {
                    myPowerUpBalls.RemoveAt(i);
                }
                else
                {
                    myPowerUpBalls[i].Update(aGameTime);
                }
            }
        }

        private void UpdateProjectiles(GameTime aGameTime)
        {
            for (int i = myProjectiles.Count - 1; i >= 0; i--)
            {
                if (myProjectiles[i].HasCollided == true)
                {
                    myProjectiles.RemoveAt(i);
                }
                else
                {
                    myProjectiles[i].Update(aGameTime);
                }
            }
        }

        private void UpdateCharacters(GameTime aGameTime)
        {
            ApplyGravityToAllCharacters(aGameTime);

            UpdatePlayer(aGameTime);
            UpdateEnemies(aGameTime);
            UpdatePowerUpBalls(aGameTime);
            UpdateProjectiles(aGameTime);

        }

        private void UpdatePlayer(GameTime aGameTime)
        {
            Player.Update(aGameTime);
            PlayerFallDamage();
        }

        private void PlayerFallDamage()
        {
            if (Player.Position.Y > myMapHeight)
            {
                Player.TakeDamage();
            }
        }

        private void UpdatePlatforms(GameTime aGameTime)
        {
            foreach (Platform platform in myPlatforms)
            {
                platform.Update(aGameTime);
            }
        }

        private void DrawPlatforms(SpriteBatch aSpriteBatch)
        {
            foreach (Platform platform in myPlatforms)
            {
                platform.Draw(aSpriteBatch);
            }
        }

        private void DrawEnemies(SpriteBatch aSpriteBatch)
        {
            foreach (Enemy enemy in myEnemies)
            {
                enemy.Draw(aSpriteBatch);
            }
        }

        private void DrawPowerUpBalls(SpriteBatch aSpriteBatch)
        {
            foreach (PowerUpBall powerupball in myPowerUpBalls)
            {
                powerupball.Draw(aSpriteBatch);
            }
        }

        private void DrawProjectiles(SpriteBatch aSpriteBatch)
        {
            foreach (Projectile projectile in myProjectiles)
            {
                projectile.Draw(aSpriteBatch);
            }
        }

        private void DrawCharacters(SpriteBatch aSpriteBatch)
        {
            Player.Draw(aSpriteBatch);
            DrawEnemies(aSpriteBatch);
            DrawPowerUpBalls(aSpriteBatch);
            DrawProjectiles(aSpriteBatch);
        }
        #endregion
    }
}

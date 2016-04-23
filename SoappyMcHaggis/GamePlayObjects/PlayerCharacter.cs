using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using SoappyMcHaggis;

namespace SoappyMcHaggis
{
    public class PlayerCharacter : GameObject
    {
        private List<Weapon> m_weapon;
        private int maxWeapons = 2;
        private int currentWeapon = 1;
        private float equipDelay = 1.0f;
        private float timeToEquip = 0f;
        private Texture2D heart;

        private Level level;
        public Level Level
        {
            get { return level; }
        }

        private MovementState state;
        public MovementState State
        {
            get { return state; }
            set { state = value; }
        }
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        private float speed;
        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        private float jumpHeight;
        public float JumpHeight
        {
            get { return jumpHeight; }
            set { jumpHeight = value; }
        }

        private float jumpSpeed;
        public float JumpSpeed
        {
            get { return jumpSpeed; }
            set { jumpSpeed = value; }
        }

        private float weight;
        public float Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        public enum CharState
        {
            Standing,
            Walking,
            Jumping,
            Falling,
            Ducking
        }

        public enum Direction
        {
            None,
            Left,
            Right,
            Up,
            Down
        }

        private float invulnerabilityDelay = 1f;
        private float timeToInvulnerable = 0f;

        private Vector2 firingDirection = new Vector2(1, 0);

        public int health;

        public Camera TheCamera;

        private Direction currentDirection;
        public Direction CurrentDirection
        {
            get { return currentDirection; }
            set
            {
                currentDirection = value;

                switch (currentDirection)
                {
                    case Direction.Left:
                        firingDirection = new Vector2(-1, 0);
                        flipImage = true;
                        break;
                    case Direction.Right:
                        firingDirection = new Vector2(1, 0);
                        flipImage = false;
                        break;
                }
            }
        }

        public CharState CurrentState;

        private FabulousAdventure adv;

        public float CurrentJumpHeight = 0;

        public PlayerCharacter(Camera theCamera, Vector2 startPosition, Level currentLevel, float initialSpeed,
            float initialJumpSpeed, float initialJumpHeight, float initialFallSpeed, FabulousAdventure adv, int health)
        {
            m_weapon = new List<Weapon>(maxWeapons);
            AddWeapon(new Fireballer(this));
            AddWeapon(new Vortexer(this));
            this.health = health;
            this.adv = adv;
            this.TheCamera = theCamera;
            this.position = startPosition;
            level = currentLevel;
            speed = initialSpeed;
            jumpSpeed = initialJumpSpeed;
            jumpHeight = initialJumpHeight;
            weight = initialFallSpeed;

            //If the character start position is above the ground, they'll fall to the ground
            CurrentState = CharState.Falling;
            CollisionArea = new Rectangle((int)startPosition.X, (int)startPosition.Y, 50, 50);
        }

        public override void Update(float elapsedTime)
        {
            UpdateTimers(elapsedTime);
            if (timeToInvulnerable == 0f)
            {
                EnemyManager.Collide(this);
                timeToInvulnerable = invulnerabilityDelay;
            }
            UpdateLossConditions();
            UpdateKeyboard();
            UpdateCharacter(elapsedTime);
            TheCamera.Reset(Position);
            if (m_weapon != null)
            {
                if (m_weapon[currentWeapon] != null)
                {
                    m_weapon[currentWeapon].Update(elapsedTime);
                }
            }
        }

        private void UpdateTimers(float elapsedTime)
        {
            if (timeToInvulnerable > 0f)
            {
                timeToInvulnerable = Math.Max(timeToInvulnerable - elapsedTime, 0f);
            }
            if (timeToEquip > 0f)
            {
                timeToEquip = Math.Max(timeToEquip - elapsedTime, 0f);
            }
        }

        public void UpdateKeyboard()
        {
            KeyboardState keyboard = Keyboard.GetState();

            if (CurrentState == CharState.Falling)
            {
                CurrentState = CharState.Falling;
                if (keyboard.IsKeyDown(Keys.Left))
                {
                    CurrentDirection = Direction.Left;
                }
                else if (keyboard.IsKeyDown(Keys.Right))
                {
                    CurrentDirection = Direction.Right;
                }
                else
                    CurrentDirection = Direction.None;
            }
            else if (CurrentState == CharState.Jumping || keyboard.IsKeyDown(Keys.Up))
            {
                CurrentState = CharState.Jumping;
                if (keyboard.IsKeyDown(Keys.Left))
                {
                    CurrentDirection = Direction.Left;
                }
                else if (keyboard.IsKeyDown(Keys.Right))
                {
                    CurrentDirection = Direction.Right;
                }
                else
                    CurrentDirection = Direction.None;
            }
            else if (!(CurrentState == CharState.Falling) && !(CurrentState == CharState.Jumping))
            {
                if (keyboard.IsKeyDown(Keys.Left))
                {
                    CurrentState = CharState.Walking;
                    CurrentDirection = Direction.Left;
                }
                else if (keyboard.IsKeyDown(Keys.Right))
                {
                    CurrentState = CharState.Walking;
                    CurrentDirection = Direction.Right;
                }
                else if (keyboard.IsKeyDown(Keys.Space))
                    level.EnterDoor(this);
                else if (keyboard.IsKeyDown(Keys.LeftControl))
                {
                    if (m_weapon[currentWeapon] != null)
                        m_weapon[currentWeapon].Attack(firingDirection);
                }
                else if (keyboard.IsKeyDown(Keys.X))
                {
                    // Can change weapon
                    if (timeToEquip == 0f)
                    {
                        timeToEquip = equipDelay;
                        if (currentWeapon == 0)
                        {
                            currentWeapon = 1;
                        }
                        else if (currentWeapon == 1)
                        {
                            currentWeapon = 0;
                        }
                    }
                }
                else
                {
                    CurrentState = CharState.Standing;
                }
            }
        }

        public void AddWeapon(Weapon weapon)
        {
            m_weapon.Add(weapon);
            if (m_weapon.Count > maxWeapons)
            {
                m_weapon.RemoveAt(0);
            }
        }

        public void UpdateCharacter(float elapsedTime)
        {
            switch (CurrentState)
            {
                case CharState.Walking:
                    switch (CurrentDirection)
                    {
                        case Direction.Left:
                            state = MovingLeft.getInstance();
                            state.UpdatePosition(this, elapsedTime);
                            break;
                        case Direction.Right:
                            state = MovingRight.getInstance();
                            state.UpdatePosition(this, elapsedTime);
                            break;
                    }
                    break;
                case CharState.Falling:
                    state = Falling.getInstance();
                    state.UpdatePosition(this, elapsedTime);
                    break;
                case CharState.Jumping:
                    state = Jumping.getInstance();
                    state.UpdatePosition(this, elapsedTime);
                    break;
            }
        }

        public void UpdateLossConditions()
        {
            if (CollisionArea.Bottom > level.MapBounds.Bottom)
            {
                adv.currentGameState = FabulousAdventure.GameState.GameOverLose;
                //Sound.Stop(level.BGMCue);
                //Sound.Play("Laughing");
            }
            if (health <= 0)
            {
                adv.currentGameState = FabulousAdventure.GameState.GameOverLose;
                //Sound.Stop(level.BGMCue);
                //Sound.Play("Laughing");
            }
        }

        public override void Draw(SpriteBatch theSpriteBatch, Camera theCamera)
        {
            /*
            String x = "X Position: " + Position.X + " - Y Position: " + Position.Y;
            x += "X Collision: " + CollisionArea.X + " - Y COllision: " + CollisionArea.Y;
            theSpriteBatch.DrawString(arial, x, new Vector2(0, 50), Color.Red);
            */
            String x = "" + this.GetType();
            theSpriteBatch.DrawString(arial, x, new Vector2(0, 50), Color.Red);
            for (int i = 0; i < health; i++)
            {
                Vector2 drawPosition = new Vector2((i + 1) * 50, 0);
                theSpriteBatch.Draw(heart, drawPosition, Color.White);
            }
            if (m_weapon != null)
            {
                if (m_weapon[currentWeapon] != null)
                {
                    m_weapon[currentWeapon].Draw(theSpriteBatch);
                }
            }
            base.Draw(theSpriteBatch, theCamera);
        }

        public void LoadContent(ContentManager theContentManager)
        {
            arial = theContentManager.Load<SpriteFont>(@"Content/Arial");
            if (theContentManager == null)
            {
                throw new ArgumentNullException("contentManager");
            }

            // load the texture
            texture = theContentManager.Load<Texture2D>("Content/Textures/Characters/character") as Texture2D;
            heart = theContentManager.Load<Texture2D>("Content/Textures/heart") as Texture2D;
        }
    }
}

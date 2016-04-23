using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SoappyMcHaggis
{
    abstract public class Projectile
    {
        protected PlayerCharacter owner;
        public PlayerCharacter Owner
        {
            get { return owner; }
        }

        protected int damage;
        public int Damage
        {
            get { return damage; }
        }
        protected Vector2 velocity;
        protected Vector2 position;
        protected Vector2 startPosition;
        private Rectangle collisionArea;
        public Rectangle CollisionArea
        {
            get { return collisionArea; }
        }
        protected bool active;

        //Distance to travel before expiring
        protected float distance;


        protected Projectile(PlayerCharacter owner, Vector2 direction)
        {
            if (!(owner == null))
            {
                this.owner = owner;
                this.velocity = direction;
                this.position = owner.Position;
                this.startPosition = owner.Position;
                this.active = true;
                this.collisionArea = new Rectangle((int)position.X, (int)position.Y, 50, 50);
            }
        }

        public void Update(float elapsedTime)
        {
            if (active)
            {
                this.position += this.velocity * elapsedTime;
                collisionArea.X = (int)position.X;
                collisionArea.Y = (int)position.Y;

                if (EnemyManager.Collide(this) || owner.Level.Collide(CollisionArea))
                    active = false;
            }
        }

        public abstract void Draw(SpriteBatch theSpriteBatch, Camera theCamera);
    }
}

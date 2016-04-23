using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SoappyMcHaggis
{
    public abstract class Enemy : GameObject
    {
        protected int m_health;
        protected bool m_isDead;

        public override void Draw(SpriteBatch theSpriteBatch, Camera theCamera)
        {
            if (!m_isDead)
            {
                base.Draw(theSpriteBatch, theCamera);
            }
        }

        public virtual bool Collide(PlayerCharacter playerCharacter)
        {
            bool hasCollided = false;
            if (!m_isDead)
            {
                if (base.Collide(playerCharacter.CollisionArea))
                {
                    playerCharacter.health--;
                    hasCollided = true;
                }
            }
            return hasCollided;
        }

        public virtual bool Collide(Projectile projectile)
        {
            bool hasCollided = false;
            if (!m_isDead)
            {
                if (base.Collide(projectile.CollisionArea))
                {
                    m_health -= projectile.Damage;
                    if (m_health <= 0)
                        m_isDead = true;
                    hasCollided = true;
                }
            }
            return hasCollided;
        }

        public bool IsDead()
        {
            return m_isDead;
        }

        public void LoadContent(ContentManager theContentManager, String textureName)
        {
            if (theContentManager == null)
            {
                throw new ArgumentNullException("contentManager");
            }

            // load the texture
            texture = theContentManager.Load<Texture2D>("Content/Textures/Enemies/" + textureName) as Texture2D;
        }
    }
}

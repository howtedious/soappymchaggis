using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace SoappMcHaggis
{
    abstract public class GameObject
    {
        protected SpriteFont arial;
        protected Vector2 position;
        protected Texture2D texture;
        public Rectangle CollisionArea;
        protected float rotation;
        protected bool visible;
        protected bool flipImage = false;

        public virtual void Draw(SpriteBatch theSpriteBatch, Camera theCamera)
        {
            if (!flipImage)
            {
                theSpriteBatch.Draw(texture, theCamera.getRenderPosition(position), Color.White);
            }
            else
            {
                theSpriteBatch.Draw(texture, theCamera.getRenderPosition(position), null, Color.White, rotation, Vector2.Zero, Vector2.One, SpriteEffects.FlipHorizontally, 0);
            }
        }

        public virtual bool Collide(Rectangle collideArea)
        {
            return collideArea.Intersects(CollisionArea);
        }

        public virtual void Update(float elapsedTime)
        {

        }
    }
}

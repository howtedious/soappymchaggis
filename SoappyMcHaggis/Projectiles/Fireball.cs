using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SoappyMcHaggis
{

    public class Fireball : Projectile
    {
        protected static Texture2D texture;

        public Fireball(PlayerCharacter owner, Vector2 direction)
            : base(owner, direction)
        {
            this.damage = 1;
            this.velocity = new Vector2(200, 0);
            this.velocity = this.velocity * direction;
        }

        public static void LoadContent(ContentManager theContentManager)
        {
            if (theContentManager == null)
            {
                throw new ArgumentNullException("contentManager");
            }

            // load the texture
            texture = theContentManager.Load<Texture2D>("Content/Textures/Projectiles/fireball");
        }

        public override void Draw(SpriteBatch theSpriteBatch, Camera theCamera)
        {
            {
                if (active)
                    if (velocity.X > 0)
                    {
                        theSpriteBatch.Draw(texture, theCamera.getRenderPosition(position), Color.White);
                    }
                    else
                    {
                        theSpriteBatch.Draw(texture, theCamera.getRenderPosition(position), null, Color.White, 0f, Vector2.Zero, Vector2.One, SpriteEffects.FlipHorizontally, 0);
                    }
            }
        }
    }
}

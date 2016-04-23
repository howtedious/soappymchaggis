using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SoappyMcHaggis
{

    public class Vortex : Projectile
    {
        protected static Texture2D texture;

        public Vortex(PlayerCharacter owner, Vector2 direction)
            : base(owner, direction)
        {
            this.damage = 10;
            this.velocity = new Vector2(150, 0);
            this.velocity = this.velocity * direction;

        }

        public static void LoadContent(ContentManager theContentManager)
        {
            if (theContentManager == null)
            {
                throw new ArgumentNullException("contentManager");
            }

            // load the texture
            texture = theContentManager.Load<Texture2D>("Content/Textures/Projectiles/vortex");
        }

        public override void Draw(SpriteBatch theSpriteBatch, Camera theCamera)
        {
            {
                if (active)
                    theSpriteBatch.Draw(texture, theCamera.getRenderPosition(position), Color.White);
            }
        }
    }
}

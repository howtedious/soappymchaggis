using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SoappyMcHaggis
{
    public class Vortexer : Weapon
    {
        private static Texture2D texture;

        public Vortexer(PlayerCharacter owner)
            : base(owner)
        {
            attackDelay = 2.0f;
            this.projectileWeapon = true;
        }

        protected override void CreateProjectile(Vector2 direction)
        {
            Vortex vortex = new Vortex(owner, direction);
            ProjectileManager.AddProjectile(vortex);
        }

        public override void Draw(SpriteBatch theSpriteBatch)
        {
            theSpriteBatch.Draw(texture, new Vector2(0, 0), Color.White);

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
    }
}
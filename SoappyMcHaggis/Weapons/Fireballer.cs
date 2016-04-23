using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SoappMcHaggis
{
    public class Fireballer : Weapon
    {
        private static Texture2D texture;

        public Fireballer(PlayerCharacter owner)
            : base(owner)
        {
            attackDelay = 0.5f;
            this.projectileWeapon = true;
        }

        protected override void CreateProjectile(Vector2 direction)
        {
            Fireball fireball = new Fireball(owner, direction);
            ProjectileManager.AddProjectile(fireball);
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
            texture = theContentManager.Load<Texture2D>("Content/Textures/Projectiles/fireball");
        }
    }
}

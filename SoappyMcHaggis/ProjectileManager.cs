using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace SoappyMcHaggis
{
    public static class ProjectileManager
    {
        private static List<Projectile> projectiles = new List<Projectile>();


        public static void AddProjectile(Projectile projectile)
        {
            projectiles.Add(projectile);
        }

        public static void Update(float elapsedTime)
        {
            foreach (Projectile projectile in projectiles)
                projectile.Update(elapsedTime);
        }

        public static void Draw(SpriteBatch theSpriteBatch, Camera theCamera)
        {
            foreach (Projectile projectile in projectiles)
                projectile.Draw(theSpriteBatch, theCamera);
        }

        public static void Clear()
        {
            projectiles.Clear();
        }
    }
}

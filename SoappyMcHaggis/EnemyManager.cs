using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace SoappyMcHaggis
{
    public static class EnemyManager
    {
        private static List<Enemy> m_Enemies = new List<Enemy>();


        public static void addEnemy(Enemy enemy)
        {
            m_Enemies.Add(enemy);
        }

        public static void Update(float elapsedTime)
        {
            foreach (Enemy enemy in m_Enemies)
            {
                enemy.Update(elapsedTime);
            }
        }

        public static bool AllDead()
        {
            bool allDead = true;
            foreach (Enemy enemy in m_Enemies)
            {
                if (!enemy.IsDead())
                {
                    allDead = false;
                }
            }
            return allDead;
        }

        public static void Clear()
        {
            m_Enemies.Clear();
        }

        public static bool Collide(PlayerCharacter playerCharacter)
        {
            bool hasCollided = false;
            foreach (Enemy enemy in m_Enemies)
            {
                if (enemy.Collide(playerCharacter))
                {
                    hasCollided = true;
                    break;
                }
            }
            return hasCollided;
        }

        public static bool Collide(Projectile projectile)
        {
            bool hasCollided = false;
            foreach (Enemy enemy in m_Enemies)
            {
                if (enemy.Collide(projectile))
                {
                    hasCollided = true;
                    break;
                }
            }
            return hasCollided;
        }

        public static void Draw(SpriteBatch theSpriteBatch, Camera theCamera)
        {
            foreach (Enemy enemy in m_Enemies)
            {
                enemy.Draw(theSpriteBatch, theCamera);
            }
        }

    }
}

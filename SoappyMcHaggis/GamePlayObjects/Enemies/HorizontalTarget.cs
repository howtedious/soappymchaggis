using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SoappMcHaggis
{
    public class HorizontalTarget : Enemy
    {
        private Vector2 direction;

        public HorizontalTarget(int health, Vector2 startPosition)
        {
            direction = new Vector2(-1, 0);
            m_health = health;
            position.X = startPosition.X * 50;
            position.Y = startPosition.Y * 50;
            m_isDead = false;
            CollisionArea = new Rectangle((int)position.X, (int)position.Y, 50, 50);
        }

        private float moveTime = 0f;
        private float reverseTime = 0f;
        public override void Update(float elapsedTime)
        {
            moveTime += elapsedTime;
            reverseTime += elapsedTime;
            
            if (!m_isDead)
            {
                if (moveTime > 0.01f)
                {
                    if (reverseTime < 2f)
                    {
                        position = position + direction;
                        CollisionArea.X += (int)direction.X;
                        moveTime = 0f;
                    } else{
                        Vector2.Negate(ref direction, out direction);
                        reverseTime = 0;
                    }
                }
            }
        }
    }
}

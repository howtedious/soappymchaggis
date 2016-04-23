using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SoappMcHaggis
{
    abstract public class Weapon
    {
        protected PlayerCharacter owner = null;

        protected float timeToNextAttack = 0f;

        protected float attackDelay = 0;

        protected bool projectileWeapon = false;

        protected Weapon(PlayerCharacter owner)
        {
            if (owner != null)
                this.owner = owner;
        }

        public virtual void Update(float elapsedTime)
        {

            if (timeToNextAttack > 0f)
            {
                timeToNextAttack = Math.Max(timeToNextAttack - elapsedTime, 0f);
            }
        }

        public virtual void Attack(Vector2 direction)
        {
            //Can't attack yet.
            if (timeToNextAttack > 0f)
            {
                return;
            }

            timeToNextAttack = attackDelay;

            if (projectileWeapon)
                CreateProjectile(direction);
        }

        protected abstract void CreateProjectile(Vector2 direction);

        public abstract void Draw(SpriteBatch theSpriteBatch);

    }
}

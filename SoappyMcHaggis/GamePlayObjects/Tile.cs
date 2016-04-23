using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SoappMcHaggis
{
    public class Tile : GameObject
    {
        public int TileType;
        public Boolean Walkable;

        public Tile(int tileType, Boolean initialVisibility, Boolean walkable, Vector2 startPosition)
        {
            TileType = tileType;
            this.visible = initialVisibility;
            Walkable = walkable;
            this.position = startPosition;
            this.position.X = 50 * startPosition.X;
            this.position.Y = 50 * startPosition.Y;
            CollisionArea = new Rectangle((int)position.X, (int)position.Y, 50, 50);
        }

        public override void Draw(SpriteBatch theSpriteBatch, Camera theCamera)
        {
            if (visible)
                base.Draw(theSpriteBatch, theCamera);
        }

        public void LoadContent(ContentManager theContentManager, String textureName)
        {
            if (theContentManager == null)
            {
                throw new ArgumentNullException("contentManager");
            }

            // load the texture
            texture = theContentManager.Load<Texture2D>("Content/Textures/Tiles/" + textureName);
        }


        public override bool Collide(Rectangle collideArea)
        {
            if (!Walkable)
                return (base.Collide(collideArea));
            else
                return false;
        }
    }
}

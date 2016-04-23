using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace SoappyMcHaggis
{
    public class Door : GameObject
    {
        public int DoorNumber;
        public int LinkedDoor;

        public Door(int doorNumber, Boolean initialVisibility, Vector2 startPosition, int linkedDoor)
        {
            DoorNumber = doorNumber;
            this.visible = initialVisibility;
            LinkedDoor = linkedDoor;
            position.X = 50 * startPosition.X;
            position.Y = 50 * startPosition.Y;
            CollisionArea = new Rectangle((int)position.X, (int)position.Y, 50, 50);
        }

        public override void Draw(SpriteBatch theSpriteBatch, Camera theCamera)
        {
            base.Draw(theSpriteBatch, theCamera);
            /*String doorInfo = "No: " + DoorNumber + "Linked: " + LinkedDoor;
            theSpriteBatch.DrawString(arial, doorInfo, theCamera.getRenderPosition(position), Color.Red);*/
        }

        public bool Enter(PlayerCharacter entering, List<Door> doors)
        {
            Door linkDoor = null;
            bool doorEntered = false;
            if (base.Collide(entering.CollisionArea))
            {
                foreach (Door otherDoor in doors)
                    if (this.LinkedDoor == otherDoor.DoorNumber)
                        linkDoor = otherDoor;
                if (linkDoor != null)
                {
                    entering.Position = linkDoor.position;
                    entering.CollisionArea.X = (int)linkDoor.position.X;
                    entering.CollisionArea.Y = (int)linkDoor.position.Y;
                    doorEntered = true;
                }
            }
            return doorEntered;
        }

        public void LoadContent(ContentManager theContentManager, String textureName)
        {
            arial = theContentManager.Load<SpriteFont>(@"Content/Arial");
            if (theContentManager == null)
            {
                throw new ArgumentNullException("contentManager");
            }

            // load the texture
            texture = theContentManager.Load<Texture2D>("Content/Textures/Tiles/" + textureName);
        }

    }
}

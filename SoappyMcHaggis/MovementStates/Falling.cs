using Microsoft.Xna.Framework;

namespace SoappyMcHaggis
{
    public class Falling : MovementState
    {

        private static Falling falling = new Falling();

        public Falling()
        {
        }

        public static Falling getInstance()
        {
            return falling;
        }

        public void UpdatePosition(PlayerCharacter movingCharacter, float elapsedTime)
        {
            Vector2 newPosition = new Vector2(movingCharacter.Position.X, movingCharacter.Position.Y + movingCharacter.Weight);
            Rectangle collisionArea = movingCharacter.CollisionArea;
            collisionArea.Offset(0, (int)movingCharacter.Weight);


            if (!movingCharacter.Level.Collide(collisionArea))
            {
                movingCharacter.Position = newPosition;
                movingCharacter.CollisionArea.Offset(0, (int)movingCharacter.Weight);

                if (movingCharacter.CurrentDirection == PlayerCharacter.Direction.Right)
                {
                    collisionArea.Offset(5, 0);
                    if (!movingCharacter.Level.Collide(collisionArea) && movingCharacter.CollisionArea.Right < movingCharacter.Level.MapBounds.Right)
                    {
                        movingCharacter.Position = new Vector2(movingCharacter.Position.X + 5, movingCharacter.Position.Y);
                        movingCharacter.CollisionArea.Offset(5, 0);
                    }
                }
                else if (movingCharacter.CurrentDirection == PlayerCharacter.Direction.Left)
                {
                    collisionArea.Offset(-5, 0);
                    if (!movingCharacter.Level.Collide(collisionArea) && !(movingCharacter.CollisionArea.Left - 5 < 0))
                    {
                        movingCharacter.Position = new Vector2(movingCharacter.Position.X - 5, movingCharacter.Position.Y);
                        movingCharacter.CollisionArea.Offset(-5, 0);
                    }
                }
            }
            else
                movingCharacter.CurrentState = PlayerCharacter.CharState.Standing;
        }

        public string getState()
        {
            return "Falling";
        }
    }
}
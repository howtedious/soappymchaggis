using Microsoft.Xna.Framework;

namespace SoappyMcHaggis
{
    public class MovingRight : MovementState
    {
        private static MovingRight movingRight = new MovingRight();

        private MovingRight()
        {
        }

        public static MovingRight getInstance()
        {
            return movingRight;
        }

        public void UpdatePosition(PlayerCharacter movingCharacter, float elapsedTime)
        {
            Vector2 newPosition = new Vector2(movingCharacter.Position.X + movingCharacter.Speed, movingCharacter.Position.Y);

            Rectangle collideArea = movingCharacter.CollisionArea;
            collideArea.Offset((int)movingCharacter.Speed, 0);


            if (!(collideArea.Right > movingCharacter.Level.MapBounds.Right) && !movingCharacter.Level.Collide(collideArea))
            {
                movingCharacter.Position = newPosition;
                movingCharacter.CollisionArea.Offset((int)movingCharacter.Speed, 0);
            }

            collideArea.Offset(0, (int)movingCharacter.Weight);
            //Check if there is a tile below you
            if (!movingCharacter.Level.Collide(collideArea))
            {
                movingCharacter.CurrentState = PlayerCharacter.CharState.Falling;
            }

        }

        public string getState()
        {
            return "MovingRight";
        }
    }
}

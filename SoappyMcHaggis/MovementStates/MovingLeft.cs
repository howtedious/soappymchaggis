using Microsoft.Xna.Framework;

namespace SoappMcHaggis
{
    public class MovingLeft : MovementState
    {
        private static MovingLeft movingLeft = new MovingLeft();

        private MovingLeft()
        {
        }

        public static MovingLeft getInstance()
        {
            return movingLeft;
        }

        public void UpdatePosition(PlayerCharacter movingCharacter, float elapsedTime)
        {
            Vector2 newPosition = new Vector2(movingCharacter.Position.X - movingCharacter.Speed, movingCharacter.Position.Y);

            Rectangle collideArea = movingCharacter.CollisionArea;
            collideArea.X -= (int)movingCharacter.Speed;

            if (!(collideArea.Left < 0) && !movingCharacter.Level.Collide(collideArea))
            {
                movingCharacter.Position = newPosition;
                movingCharacter.CollisionArea.Offset(-(int)movingCharacter.Speed, 0);
            }

            //Check if there is a tile below you
            collideArea.Offset(0, (int)movingCharacter.Weight);
            if(!movingCharacter.Level.Collide(collideArea)){
                movingCharacter.CurrentState = PlayerCharacter.CharState.Falling;
            }
        }

        public string getState()
        {
            return "MovingLeft";
        }
    }
}
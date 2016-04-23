using Microsoft.Xna.Framework;

namespace SoappyMcHaggis
{
    public class Jumping : MovementState
    {

        private static Jumping jumping = new Jumping();

        public Jumping()
        {
        }

        public static Jumping getInstance()
        {
            return jumping;
        }

        public void UpdatePosition(PlayerCharacter movingCharacter, float elapsedTime)
        {
            movingCharacter.CurrentJumpHeight += movingCharacter.JumpSpeed;

            Rectangle collisionArea = movingCharacter.CollisionArea;
            if (!(movingCharacter.CurrentJumpHeight > movingCharacter.JumpHeight))
            {

                collisionArea = movingCharacter.CollisionArea;
                collisionArea.Offset(0, -5);

                if (!movingCharacter.Level.Collide(collisionArea) && !(collisionArea.Y < 0))
                {
                    movingCharacter.Position = new Vector2(movingCharacter.Position.X, movingCharacter.Position.Y - movingCharacter.JumpSpeed);
                    movingCharacter.CollisionArea.Offset(0, -(int)movingCharacter.JumpSpeed);

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

            }
            else
            {
                movingCharacter.CurrentJumpHeight = 0;
                movingCharacter.CurrentState = PlayerCharacter.CharState.Standing;
                if (!movingCharacter.Level.Collide(collisionArea))
                    movingCharacter.CurrentState = PlayerCharacter.CharState.Falling;
            }
        }

        public string getState()
        {
            return "Jumping";
        }

    }
}
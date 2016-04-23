using Microsoft.Xna.Framework;

namespace SoappyMcHaggis
{
    public class Camera
    {
        private Vector2 position;

        public Camera()
        {
            position = new Vector2();
        }

        private Rectangle mapSize;

        public Camera(Vector2 initialPosition, Rectangle mapSize)
        {
            this.mapSize = mapSize;
            position = initialPosition;
            //Reset the camera to a legal position
            Move(new Vector2(0, 0));
        }

        public void Reset(Vector2 newPosition)
        {
            position = newPosition;
            position.X -= 400;
            position.Y -= 300;
            Move(new Vector2(0, 0));
        }

        public void Move(Vector2 moveVector)
        {
            position += moveVector;
            if (position.X < mapSize.Left)
            {
                position.X = 0;
            }
            if (position.X + 800 > mapSize.Right)
            {
                position.X = mapSize.Right - 800;
            }
            if (position.Y < 0)
            {
                position.Y = 0;
            }
            if (position.Y + 600 > mapSize.Bottom)
            {
                position.Y = mapSize.Bottom - 600;
            }
        }

        public Vector2 getRenderPosition(Vector2 spirtePosition)
        {
            return (spirtePosition - position);
        }
    }
}

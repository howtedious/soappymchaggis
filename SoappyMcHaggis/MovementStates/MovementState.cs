namespace SoappyMcHaggis
{
    public interface MovementState
    {
        string getState();
        void UpdatePosition(PlayerCharacter movingCharacter, float elapsedTime);
    }
}

using UnityEngine;

public class LevelEnd : MonoBehaviour, IInteract
{
    [SerializeField]
    public GameEvent LevelEndReached;

    public void EndLevel()
    {
        LevelEndReached.Raise();
    }

    public void Interact()
    {
        EndLevel();
    }
}

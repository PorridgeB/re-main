using UnityEngine;

public class LevelEnd : MonoBehaviour, IInteract
{
    [SerializeField]
    public GameEvent LevelEndReached;

    public void EndLevel()
    {
        Debug.Log("ending level");
        LevelEndReached.Raise();
    }

    public void Interact()
    {
        EndLevel();
    }
}

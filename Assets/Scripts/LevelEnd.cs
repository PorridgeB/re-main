using UnityEngine;

public class LevelEnd : MonoBehaviour, IInteract
{
    [SerializeField]
    public GameEvent LevelEndReached;
    [SerializeField]
    private AudioClip onInteract;

    public void EndLevel()
    {
        Debug.Log("ending level");
        LevelEndReached.Raise();
    }

    public void Interact()
    {
        SoundManager.PlaySound(onInteract, 0.6f);
        EndLevel();
    }
}

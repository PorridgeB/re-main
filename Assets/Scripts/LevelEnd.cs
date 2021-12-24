using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    [SerializeField]
    public GameEvent LevelEndReached;

    public void EndLevel()
    {
        LevelEndReached.Raise();
    }
}

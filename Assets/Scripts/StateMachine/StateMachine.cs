using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public delegate void Changed();
    public static event Changed StateChanged;

    [SerializeField]
    private State state;

    // Start is called before the first frame update
    void Start()
    {
        foreach (State s in GetComponents<State>())
        {
            s.SetStateMachine(this);
        }
        state.Enter(new List<string>());
    }

    // Update is called once per frame
    void Update()
    {
        state.Process();
    }

    private void FixedUpdate()
    {
        state.PhysicsProcess();
    }

    public void ChangeTo(string TargetStateName, List<string> message)
    {
        //This might need to be conditional later on if certain states need to "reenter" each time
        if (state.name == TargetStateName) return;

        foreach (State s in GetComponents<State>())
        {
            if (s.name == TargetStateName)
            {
                state.Exit();
                state = s;
                state.Enter(message);

                StateChanged?.Invoke();
                return;
            }
        }
        Debug.Log("Cannot change state to " + TargetStateName + " since it does not exist!");
    }
}

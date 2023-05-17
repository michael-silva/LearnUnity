using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterState
{
    public virtual CharacterState Execute()
    {
        return this;
    }
}

public class JumpingState : CharacterState
{
}

public class IdleState : CharacterState
{
    public override CharacterState Execute()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            return new JumpingState();

        return this;
    }
}

public class Character : MonoBehaviour
{
    private CharacterState state;

    // Start is called before the first frame update
    void Start()
    {
        state = new IdleState();
    }

    // Update is called once per frame
    void Update()
    {
        state = state.Execute();
    }
}

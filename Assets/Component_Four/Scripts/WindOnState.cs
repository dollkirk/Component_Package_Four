using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindOnState : WindState
{
    public override void EnterState(WindStateController wind)
    {
        //This is changing the wind direction everytime it enters the "WindOnState".
        wind.windDirection *= -1;
    }

    public override void UpdateState(WindStateController wind)
    {
        //Applying the force in "UpdateState()" as it needs to constantly apply it to push the objects in the scene.
        wind.ApplyForce();
    }

    public void Destroyed(WindState newState, WindStateController state)
    {
        state.TransitionToState(newState);
    }
}
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class WindStateController : MonoBehaviour
{
    //Wind timer variables
    [Header("Timer")]
    [Range(0.01f, 60)]
    public float givenTimeCountDown;
    private float timeCountDown;
    public TextMeshProUGUI CountText;

    //Moving objects by wind variables
    [Header("Wind")]
    public ObjectsAffectedByWind[] objsAffectedByWind;
    public float windSpeed = 5f;
    public int windDirection;

    //Changing state variables
    private WindState currentState;
    public readonly WindOffState offState = new WindOffState();
    public readonly WindOnState onState = new WindOnState();

    void Start()
    {
        //At the start, the state needs to be "WindOffState" as it has yet to be activated.
        TransitionToState(offState);

        //Wind direction is set to 1 at the start.
        windDirection = 1;
    }

    void Update()
    {

        currentState.UpdateState(this);

        //Countdown when the state is "WindOnState".
        if (timeCountDown > 0)
        {
            timeCountDown -= Time.deltaTime;
        }

        //Displays countdown with TMPro UI.
        CountText.text = string.Format("{0:n0}", timeCountDown);

        //Checks if countdown hits 0.
        if (timeCountDown < 0)
        {
            //Checks if the current state is the "windOnState".
            if (currentState == onState)
            {
                //The "WindOnState" is stopped and swapped to the "WindOffState".
                onState.Destroyed(offState, this);

                //This State change is updated.
                offState.UpdateState(this);
            }

            //The "WindOffState" is stopped and swapped to the "WindOnState".
            offState.Destroyed(onState, this);

            //This State change is updated. This creates the countdown cycle by iterating this part again.
            onState.UpdateState(this);

            //Resets the countdown back to "givenTimeCountDown" as it cycles.
            timeCountDown = givenTimeCountDown;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Checking if the player object triggers the wind activator collider.
        if (other.gameObject.CompareTag("Player"))
        {
            //Checking if the current state is the "WindOffState".
            if (currentState == offState)
            {
                //Setting the countdown to "givenTimeCountDown" as this is the first start cycle.
                timeCountDown = givenTimeCountDown;

                //The "WindOffState" is stopped and swapped to the "WindOnState".
                offState.Destroyed(onState, this);

                //This State change is updated.
                onState.UpdateState(this);
            }
        }
    }

    public void ApplyForce()
    {
        //Applies wind force to each objected that has the script "ObjectsAffectedByWind" attached.
        foreach (var item in objsAffectedByWind)
        {
            item.rb.AddForce(windDirection * windSpeed, 0, 0, ForceMode.Force);
        }
    }

    //Function that transitions the states.
    public void TransitionToState(WindState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
}

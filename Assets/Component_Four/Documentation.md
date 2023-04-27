# Component Package Four - Wind Cycle

## Behaviours
WindStateController

WindState

WindOnState

WindOffState

ObjectsAffectedByWind

## WindStateController
This script is the main controller for the wind cycle. 

It takes an OntriggerEnter collider input and checks if it is the player object that triggered it. If it is the player object, it changes the wind state to on and starts a countdown that displays by TMPro in the game UI. the controller cycles this change of states everytime the countdown runs out so that it is an infinite loop of wind changing directions.

The variables that can be edited for desired outcome are "givenTimeCountDown" (the desired amount of time you want for the wind to change direction), "countText" (the desired TMPro UI object in the scene), "objsAffectedByWind" (every object that has a rigidbody and the script "ObjectsAffectedByWind" attached), "windSpeed" (the force you want the wind to push the objects affected by wind) and "windDirection" (the positive or negative number to determine left or right direction).


## WindState
This script is the State Machine brain. it is an abstract class and contains the functions "EnterState()" and "UpdateState()".


## WindOnState
This script is for when the wind is on. 

When it enters this state, it multiplies the wind direction by -1 to change its direction everytime it enters this state. During this state, it applies the force by referencing the 
"ApplyForce()" function that is in the "WindController" script.


## WindOffState
This script is for when the wind is off. 

When it enters this state, it does nothing as the wind should be off.


## ObjectsAffectedByWind
This script is to reference every object that is affected by the wind and that has a Rigidbody component.

Any object in the scene that has this script attached is going to be moved by the wind as long as it has a Rigidbody component.


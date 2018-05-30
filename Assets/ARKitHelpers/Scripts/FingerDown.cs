
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


// Creating an even for our screen
// down moving x y
[System.Serializable]
public class ScreenEvent : UnityEvent<float, float, float, float>{}

public class FingerDown : MonoBehaviour {


  public ScreenEvent OnDown;
  public ScreenEvent OnUp;
  public ScreenEvent OnMove;
  public ScreenEvent OnHeld;
  public ScreenEvent WhileDown;

  public float movingDeadZone;

  public float yPos;
  public float xPos;
  public float down;
  public float oDown;
  public float moving;

  private Vector2 oPos;
  private Vector2 p;


  // When the program starts, create our events
  void Awake () {
    if (OnDown == null) OnDown = new ScreenEvent();
    if (OnUp == null) OnUp = new ScreenEvent();
    if (OnMove == null) OnMove = new ScreenEvent();
    if (OnHeld == null) OnHeld = new ScreenEvent();
    if (WhileDown == null) WhileDown = new ScreenEvent();
  }
  
  // Update is called once per frame
  void Update () {


    // This is called if we are in the editor 
    #if UNITY_EDITOR  
      if (Input.GetMouseButton (0)) {
       down = 1;
       p  =  Input.mousePosition;
      }else{
        down = 0;
        moving = 0;
        oPos = p;
      }

    // This is called if we are on the phone
    #else
      if (Input.touchCount > 0 ){
        down = 1;
        p  =  Input.GetTouch(0).position;
      }else{
        down = 0;
        moving = 0;
        oPos = p;
      }
    #endif


    // If our finger or mouse is down
    if( down == 1){
      
      // check to see how much we have moved since last frame
      // if its bigger than our dead zone, let it know that we are moving
      if( (p-oPos).magnitude > movingDeadZone ){
        moving = 1;
      }


      // If we ARE moving, save the position of our finger on the screen
      if( moving == 1){
        yPos = p.y / Screen.height;
        xPos = p.x / Screen.width;
        oPos = p;

        OnMove.Invoke(down,moving,xPos,yPos);
      }else{

        OnHeld.Invoke(down,moving,xPos,yPos);
      }

      WhileDown.Invoke(down,moving,xPos,yPos);

    }

    // If we just pressed down, call on down
    if( oDown == 0 && down == 1 ){
        OnDown.Invoke(down,moving,xPos,yPos);
    }

    // If we just released, call on up
    if( oDown == 1 && down == 0 ){
        OnUp.Invoke(down,moving,xPos,yPos);
    }

    oDown = down;

  }



}
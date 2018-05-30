
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;



// down moving x y
[System.Serializable]
public class ScreenEvent : UnityEvent<float, float, float, float>{}

public class FingerDown : MonoBehaviour {


  public ScreenEvent OnDown;
  public ScreenEvent OnUp;


  public float yPos;
  public float xPos;
  public float down;
  public float oDown;
  public float moving;

  private Vector2 oPos;
  private Vector2 p;


  // Use this for initialization
  void Awake () {
    if (OnDown == null) OnDown = new ScreenEvent();
    if (OnUp == null) OnUp = new ScreenEvent();
  }
  
  // Update is called once per frame
  void Update () {

    #if UNITY_EDITOR  
      if (Input.GetMouseButton (0)) {
       down = 1;
       p  =  Input.mousePosition;///Input.GetTouch(0).position;
      }else{
        down = 0;
        moving = 0;
        oPos = p;
      }
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

    if( down == 1){
      
      // We have moved more than 20 pixels!
      if( (p-oPos).magnitude > 20 ){
        print( "ya moving");
        moving = 1;
      }

      if( moving == 1){
        yPos = p.y / Screen.height;
        xPos = p.x / Screen.width;
        oPos = p;
      }

    }

    if( oDown == 0 && down == 1 ){
        OnDown.Invoke(down,moving,xPos,yPos);
    }

    if( oDown == 1 && down == 0 ){
        OnUp.Invoke(down,moving,xPos,yPos);
    }

    oDown = down;

  }



}
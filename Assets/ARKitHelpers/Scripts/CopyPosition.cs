using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPosition : MonoBehaviour {

  public GameObject toCopy;
  public FingerDown fingerDown;
  
  public float forwardOffset;


	// Listen for finger down
	void Start () {
		fingerDown.OnDown.AddListener(OnDown);
	}
	


  // Every time we press down, set out objects position with whatever
  // forward offset we choose
  void OnDown( float down, float moving, float xPos , float yPos ){
    transform.position = toCopy.transform.position + toCopy.transform.forward * forwardOffset;
  }

}

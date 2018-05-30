using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPosition : MonoBehaviour {

  public GameObject toCopy;
  public FingerDown fingerDown;


  public float forwardOffset;


	// Use this for initialization
	void Start () {
		fingerDown.OnDown.AddListener(OnDown);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  void OnDown( float down, float moving, float xPos , float yPos ){

    print( "hello");
    transform.position = toCopy.transform.position + toCopy.transform.forward * forwardOffset;


  }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPlacer : MonoBehaviour {


  public Transform toCopy;
  public SphereBuffer buffer;
  public FingerDown  down;

	// Use this for initialization
	void Start () {
		

    down.OnDown.AddListener(Down);
	}
	

  void Down( float down, float moving, float x, float y ){
    print("hmm");
    buffer.SetSphere( toCopy.position + toCopy.forward * .1f );
  }


	// Update is called once per frame
	void Update () {
		
	}
}

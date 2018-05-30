using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchPlacer : MonoBehaviour {

  public Transform toCopy;
  public SphereBuffer buffer;
  public FingerDown  down;

	// Using our sphere buffer to place new spheres!
	void Start () {
    down.OnDown.AddListener(Down);
	}
	

  void Down( float down, float moving, float x, float y ){
    buffer.SetSphere( toCopy.position + toCopy.forward * .1f );
  }

}

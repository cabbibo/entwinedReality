using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectToClosest : MonoBehaviour {

  private LineRenderer lr;

  public Vector3[] closestPoints;


	// Use this for initialization
	void Start () {

    lr = GetComponent<LineRenderer>();
    closestPoints = new Vector3[lr.positionCount];

	}
	
	// Update is called once per frame
	void Update () {
		
	}

  public void SetClosest( Vector3[] points ){

    Vector3 closest = points[0];

    float closestDistance = 0;

    // will need 2 points to represent every line
    for( int i = 0; i < (closestPoints.Length/2); i++){

      Vector3 p1;
      Vector3 p2;
      float minDistance = 100000;

      // Check the distance to each point
      for( int j = 0; j < points.Length; j++ ){

        p1 = points[j];
        p2 = transform.position;

        float d = (p1-p2).magnitude;

        // If its smalled than the current smallest distance, 
        // but greater than the biggest distance, set our position
        if( d < minDistance && d > closestDistance ){
          minDistance = d;
          closest = points[j];
        }

      }

      // Set our new closest distance, for next iteration
      closestDistance = minDistance;

      // Set the positions for the line renderer
      closestPoints[i * 2 + 0] = closest;
      closestPoints[i * 2 + 1] = transform.position;

    }

    lr.SetPositions( closestPoints );


  }


}

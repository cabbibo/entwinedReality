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

    print( points.Length );


    Vector3 closest = points[0];

    float closestDistance = 0;



    for( int i = 0; i < (closestPoints.Length/2); i++){

      Vector3 p1;
      Vector3 p2;
      float minDistance = 100000;

      for( int j = 0; j < points.Length; j++ ){

        p1 = points[j];
        p2 = transform.position;//closestPoints[i*2+0];

        float d = (p1-p2).magnitude;

        if( d < minDistance && d > closestDistance ){

          print(d);
          print(points[j]);
          minDistance = d;

          closest = points[j];
        }

      }


      closestDistance = minDistance;
      closestPoints[i * 2 + 0] = closest;
      closestPoints[i * 2 + 1] = transform.position;

    }


    //for( int i = 0; i < lr.positionCount; i++ ){
      lr.SetPositions( closestPoints );
    //}

  }


}

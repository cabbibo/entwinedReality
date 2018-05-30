using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.iOS;


// numNewPoints, currentTotal,fullSize
[System.Serializable]
public class NewPointsEvent: UnityEvent<Vector3[],int, int, int>{}

public class StablePointCloud : MonoBehaviour {




  // The minimum distance between points.
  // The higher this value is , the more 'sparse' the cloud will be
  public float minDistance;

  // The max number of points to show! 
  // Once you place this many points, 
  //it will start eating up your old points.
  public int maxPointsToShow;


  // values to keep track of how many points we have placed 
  // and the current id to place
  private int pointsShown = 0;
  private int currentPointID = 0;


  // Our event that we can grab
  public NewPointsEvent OnNewPoints;



  private Vector3[] fullPoints;
  private Vector3[] newPoints;

  private bool frameUpdated = false;


  // Making it so we can return our private
  // variable publically
  public Vector3[] GetFull(){
    return fullPoints;
  }

  // Use this for initialization
  void Start () {

     // Add our events
      UnityARSessionNativeInterface.ARFrameUpdatedEvent += ARFrameUpdated;
      frameUpdated = false;

      fullPoints = new Vector3[maxPointsToShow];

      for( int i = 0; i < fullPoints.Length; i++ ){
        fullPoints[i] = new Vector3( 100000 ,0 ,0);
      }

      // Creating a 'OnNewPoint' events that other objects can use
     if (OnNewPoints == null) OnNewPoints = new NewPointsEvent();
  }
  

    // This is called when we get new data from ARKit
    public void ARFrameUpdated(UnityARCamera camera){

        // Getting that point cloud data into our world
        newPoints = camera.pointCloudData;

        // We now know we need to update our info
        frameUpdated = true;

    }

    // Update is called once per frame
    void Update () {


      // If we have new data
      if (frameUpdated) {

        // And that new data isn't null or 0 points
        if (newPoints != null && newPoints.Length > 0) {


          // Getting the full number of particles ( we can't add more than our max every frame! )
          int numParticles = Mathf.Min (newPoints.Length, maxPointsToShow);
          int index = 0;

          float minLength;

          Vector3 p1;
          Vector3 p2;
          Vector3 dif;

          Vector3 finalPoint = new Vector3(100000,0,0);

          int totalThisFrame = 0;

          // For every new point, check the distance
          // to all of our current points
          // IF it is far enough from the rest of the points
          // Add it to our array, and add it to the number of
          // new points we have added THIS Frame
          for( int i = 0; i < newPoints.Length; i++ ){

            minLength = 100000;

            p1 = newPoints[i];

            finalPoint = p1;

            for( int j = 0; j < pointsShown; j++){
              
              p2 = fullPoints[j];
              dif = p1 - p2;

              if( dif.magnitude < minLength ){
                minLength = dif.magnitude;
                finalPoint = p1;
              }

            }

            if( minLength > minDistance ){

               fullPoints[currentPointID] = finalPoint;

              currentPointID ++;
              totalThisFrame ++;
              pointsShown ++;
             
              if( pointsShown >= maxPointsToShow ){ pointsShown = maxPointsToShow; }
              
              currentPointID %= maxPointsToShow;

            }

          }

          // If we have added any new points, FIRE!
          if( totalThisFrame > 0 ){
            OnNewPoints.Invoke(fullPoints,totalThisFrame , currentPointID , maxPointsToShow);
          }
      
        } 

        // We have updated the frame and don't need to fire this
        // until we get more new data
        frameUpdated = false;
      }
  }

}






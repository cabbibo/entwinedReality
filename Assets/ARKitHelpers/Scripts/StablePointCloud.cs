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

  public Vector3[] GetFull(){
    return fullPoints;
  }

  // Use this for initialization
  void Start () {
        UnityARSessionNativeInterface.ARFrameUpdatedEvent += ARFrameUpdated;
        frameUpdated = false;

        fullPoints = new Vector3[maxPointsToShow];

        for( int i = 0; i < fullPoints.Length; i++ ){
          fullPoints[i] = new Vector3( 100000 ,0 ,0);
        }
     if (OnNewPoints == null) OnNewPoints = new NewPointsEvent();
  }
  
    public void ARFrameUpdated(UnityARCamera camera){

        newPoints = camera.pointCloudData;
        frameUpdated = true;


    }

    // Update is called once per frame
    void Update () {

      if (frameUpdated) {

          if (newPoints != null && newPoints.Length > 0) {



              int numParticles = Mathf.Min (newPoints.Length, maxPointsToShow);
              int index = 0;

              float minLength;

              Vector3 p1;
              Vector3 p2;
              Vector3 dif;

                Vector3 finalPoint = new Vector3(100000,0,0);

              int totalThisFrame = 0;
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

              if( totalThisFrame > 0 ){
                OnNewPoints.Invoke(fullPoints,totalThisFrame , currentPointID , maxPointsToShow);
              }
        
          } 
          frameUpdated = false;
      }
  }

}






using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetParticlesFromPointCloud : MonoBehaviour {

  public StablePointCloud pointCloud;
  public float particleSize;

  private ParticleSystem ParticleSystem;

  private ParticleSystem.Particle [] particles;

	// Use this for initialization
	void Start () {

    ParticleSystem = GetComponent<ParticleSystem>();

    particles = new ParticleSystem.Particle[pointCloud.maxPointsToShow];

    pointCloud.OnNewPoints.AddListener( NewPoints );
		
	}


  // When we have new points, go through and set every positions and color of all of the particles in the particle system
  void NewPoints( Vector3[] fullPoints, int numNew , int currentID,  int total){

    for( int i = 0; i < numNew; i++ ){
      
      int fID = currentID-i;
      if( fID < 0){ fID += total; }

      particles[fID].position = fullPoints[fID];
      particles[fID].startColor = Color.HSVToRGB((float)(fID)/ total, 1, 1);
      particles[fID].startSize = particleSize;
    
    }

    ParticleSystem.SetParticles (particles, total);
  }

}

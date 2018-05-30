using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class LineRendererStealCameraTextures : MonoBehaviour {

  public UnityARVideo video;
  private Material mat;
	void OnEnable() {
    mat = GetComponent<LineRenderer>().material;
    if( video == null ){ video = Camera.main.GetComponent<UnityARVideo>();}
  }
  
  // Update is called once per frame
  void Update () {

    // Get the camera data from the unity AR video
    mat = GetComponent<LineRenderer>().material;
    mat.SetTexture( "_vidTexY" , video.m_ClearMaterial.GetTexture( "_textureY" ) );
    mat.SetTexture( "_vidTexCBCR" , video.m_ClearMaterial.GetTexture( "_textureCbCr" ));

  }


}

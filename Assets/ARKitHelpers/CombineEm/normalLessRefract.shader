Shader "Custom/normalLessRefract" {

  Properties {

    _RefractDist( "RefractRayDistance",float) = .001
    _RefractAmount( "RefractAmount",float) = .8

  }

  SubShader {


    Cull Back

    Pass {

      CGPROGRAM

      #pragma vertex vert
      #pragma fragment frag

      #include "UnityCG.cginc"


      struct VertexIn{
         float4 position  : POSITION; 
         float4 texcoord  : TEXCOORD0; 
				 float4 color : COLOR;
      };


      struct VertexOut {
          float4 pos      : POSITION; 
          float4 uv       : TEXCOORD0; 
          float3 mPos     : TEXCOORD1;
				 	float4 color : COLOR;
      };

      uniform sampler2D _vidTexY;
      uniform sampler2D _vidTexCBCR;
      uniform float _RefractDist;
      uniform float _RefractAmount;


      
      VertexOut vert(VertexIn v) {
        
        VertexOut o;

        o.uv = v.texcoord;
       	o.color = v.color;
  
        // Getting the position for actual position
        o.pos = UnityObjectToClipPos(  v.position );
     
        //Gets the positino of the vert in world space
        float4 mPos = mul( unity_ObjectToWorld , float4( v.position.xyz , 1) );
        o.mPos = mPos;


        return o;

      }

      // Fragment Shader
      fixed4 frag(VertexOut v) : COLOR {

        // Gets ray going from Camera to vert
        float3 dir = normalize((_WorldSpaceCameraPos - v.mPos));

        float3 fNor =normalize( cross(ddx( v.mPos ), ddy( v.mPos)));

        float3 l = normalize( cross(fNor , float3(0,1,0)));
        float3 r = normalize(cross( l , fNor ));

				float2 dUV = length( v.uv- float2(.5,.5));

				float3 fVec = l * dUV.x + l * dUV.y;
				fNor -= fVec * fVec * fVec * 10;
				fNor = normalize( fNor);
				if( length(dUV) > .5){ discard; }


        // Here is where refraction happens!
        float3 refracted = normalize(refract( dir , fNor , _RefractAmount ));
        
        //We take the refracted ray, and step it forward a tiny bit 
        //This is the point that we will use to sample from video texture
        float3 newPos = v.mPos + refracted * _RefractDist;


        float4 mp = mul( UNITY_MATRIX_VP , float4( newPos, 1. ) );

        // Getting our screen position
        float4 sp = ComputeScreenPos( mp );
     



        // Our color starts off at zero,   
        float3 col = float3( 0.0 , 0.0 , 0.0 );

        // Getting The color from the ARCamera textures!
        float2 texcoord = float2(1,1)-  sp.yx/sp.w;//ComputeScreenPos( v.screenPos ); //v.uv;

        //clamping for when you get to the edge of the screen. 
        // This is really where you can see how the process is quite fake...
        float y = tex2D(_vidTexY, clamp( texcoord , float2(0.001,0.001) , float2(.9999,.9999))).r;
        float4 ycbcr = float4(y, tex2D(_vidTexCBCR, texcoord).rg, 1.0);

        const float4x4 ycbcrToRGBTransform = float4x4(
            float4(1.0, +0.0000, +1.4020, -0.7010),
            float4(1.0, -0.3441, -0.7141, +0.5291),
            float4(1.0, +1.7720, +0.0000, -0.8860),
            float4(0.0, +0.0000, +0.0000, +1.0000)
          );

        col= mul(ycbcrToRGBTransform, ycbcr).xyz;


       // col = float3(1,1,1);
        fixed4 color;
        color = fixed4( col , 1. );
        return color;
      }

      ENDCG
    }
  }
  FallBack "Diffuse"
}
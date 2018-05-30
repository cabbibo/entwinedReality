# Entwined Reality
This repo is a set of code created for the iX Symposium at S.A.T. ( 2018 ) 

In this code we will be examing the parts of the iOS ARKit data that is NOT flat plane information.

The 3 types of data we will be looking at are:
- Position Data
- Camera Data
- Point Cloud Data


All of the code is inside the 'ARKitHelpers' and you will have to make sure you have downloaded the Unity ARKit Plugin into the same project.


## Position Data
The scene in this section will be looking at a simple way to use the data from the phone's position in space to create something interesting.
- It is as simple as placing a trail renderer on a game object that is attached to the camera

## Camera Data
This scene in this section will be looking at a simple way to use the camera data to create interesting effects. For this section we will be writing a 'refractive' shader, and placing a refractive sphere in the scene. 

## Point Cloud Data
This scene in this section will be looking at interesting ways of using the point cloud data received from ARKit. The code in this section is more complex ( and less efficient ) as it will be creating a *stable* pointcloud out of the ARKit Point cloud.


## COMBINED
Lastly we have a scene that tries to combine all these different types of data into a art creation tool

### Contact:
Isaac Cohen
@cabbibo
hello@cabbi.bo

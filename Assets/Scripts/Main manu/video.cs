using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;



public class video : MonoBehaviour
{
    public VideoPlayer VP;

    public VideoSource clip1;
    public VideoSource clip2; 
    
    bool canviat = false;   

    void Start(){
        VP.loopPointReached += EndReached;
    }

     void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        if(!canviat){
            VP.source=clip2;
            canviat =true;
        }
        VP.playbackSpeed = VP.playbackSpeed / 10.0F;
    }
}

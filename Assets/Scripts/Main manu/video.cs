using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;



public class video : MonoBehaviour
{
    private VideoPlayer VP;

    public VideoClip clip2; 
    
    bool canviat = false;   

    void Awake(){
        VP = GetComponent<VideoPlayer> ();
    }

    void ChangeLoopClip(){
        VP.clip = clip2;
    }

    void Start(){
        VP.loopPointReached += EndReached;
    }

     void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        if(!canviat && VP.isPrepared){
            ChangeLoopClip();
            canviat =true;
            VP.isLooping = true;
        }
    }
}

using System.Collections.Generic;
using GoogleARCore;
using GoogleARCore.Examples.Common;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerFollow : MonoBehaviour
{
    public GameObject firstPersonCamera;
    private Quaternion originalRotation;
    private Quaternion maxRotation;
    
    // // new
   Animator penguinAnimator;

    // variables for following camera
    private Vector3 penguinOffset = new Vector3(0, -1f, 1f);
    public float SmoothFactor = 0.5f;
    public float speed = 10f;

    void Start()
    {
        // // new
        penguinAnimator = GetComponent <Animator> ();
        penguinAnimator.SetBool("IsSwimming",true);
        // originalRotation = transform.rotation; //we save the original rotation to interpolate rotation correctly
        // maxRotation = Quaternion.identity; //make new rotation object
        // maxRotation.eulerAngles = new Vector3(180f, 0f, 0f); //set new rotation object's rotation to 180
    }

    void Update()
    {
        Vector3 newPos = firstPersonCamera.transform.position + penguinOffset;
        // transform.position = Vector3.MoveTowards(transform.position, newPos, Time.deltaTime * speed);
        transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);
        // penguinAnimations.Move(newPos.z, newPos.x);
        transform.position += transform.up * Mathf.Sin (Time.time * 3f) * 0.1f;
        
    }

    // // simple value mapping function 
    // private float map(float value, float start1, float stop1, float start2, float stop2)
    // {
    //     //map the value
    //     float res = start2 + (stop2 - start2) * ((value - start1) / (stop1 - start1));

    //     //below we clamp the return value to be within the new start and stop
    //     //start2 isn't necessarily less than stop2 
    //     float new_min = Mathf.Min(start2, stop2);
    //     float new_max = Mathf.Max(start2, stop2);
    //     res = Mathf.Max(new_min, res);
    //     res = Mathf.Min(new_max, res);
    //     return res;
    // }
    
    
    
    


    
    



    
        
    
}

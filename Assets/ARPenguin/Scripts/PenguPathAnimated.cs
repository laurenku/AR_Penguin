using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguPathAnimated : MonoBehaviour
{
    Animator animator;

    Vector3[] mainPath;
    int numPathPoints;
    int nextPathPos;
    public GameObject mainCamera;
    public float penguSpeed;
    public float smoothFactor = 1f;
    bool looking = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        createPath();
        transform.LookAt(mainPath[nextPathPos], Vector3.up);
        animator.SetFloat("Forward", 0.1f);
    }

    void Update()
    {	
    	if(Vector3.Distance(this.transform.position, mainCamera.transform.position) > 5){
            animator.SetFloat("Forward", 0.1f);
    		createPath();
   		}
        if(nearPosition()){
            animator.SetFloat("Forward", 0.1f);
            looking = false;

        	if(nextPathPos >= numPathPoints - 1){
        		createPath();
                
                transform.LookAt(mainPath[nextPathPos], Vector3.up);
        	}else{
        		nextPathPos++;
                
                // nice pengu turn
                float angleBetween = 0.0f;
                Vector3 targetDir = mainPath[nextPathPos] - this.transform.position;
                angleBetween = Vector3.SignedAngle(transform.forward, targetDir, Vector3.up);
                if (angleBetween < -5f)
                {
                    float left = map(angleBetween, -180f, 0f, 0f, 0.5f);
                    animator.SetFloat("Forward", 0f);
                    animator.SetFloat("Turn", left);
                }
                else if (angleBetween > 5f)
                {
                    float right = map(angleBetween, 0f, 180f, 0.5f, 1f);
                    animator.SetFloat("Forward", 0f);
                    animator.SetFloat("Turn", right);
                }

                
                transform.LookAt(mainPath[nextPathPos], Vector3.up);
                
        	}
        }else{
            // if pengu is turned towards the destination already, don't turn
            if (!looking)
            {
                animator.SetFloat("Forward", 0.1f);
                looking = true;
            }
        	
        	this.transform.position = Vector3.MoveTowards(this.transform.position, mainPath[nextPathPos], penguSpeed);
            // this.transform.position = Vector3.Lerp(this.transform.position, mainPath[nextPathPos], smoothFactor);
        }
    }

    public void createPath() {
        numPathPoints = Random.Range(30, 35);
        generateMainPath(numPathPoints);
        nextPathPos = 0;
    }

    public bool nearPosition() {
        float distFromTarget = Vector3.Distance(this.transform.position, mainPath[nextPathPos]);
    	return distFromTarget <= 0.001f;
    }

    public void generateMainPath(int numPathPoints){

        mainPath = new Vector3[numPathPoints];
        Vector3 cameraPos = mainCamera.transform.position;

        for (int i = 0; i < numPathPoints; i++){
          float r = Random.Range(0.5f, 2f);
          float newX = r * Mathf.Cos(2* Mathf.PI/numPathPoints*i) + cameraPos.x;
          float newZ = r * Mathf.Sin(2* Mathf.PI/numPathPoints*i) + cameraPos.z;
          mainPath[i] = new Vector3(newX, cameraPos.y - 1, newZ);
        }
        
    }

    

    private float map(float value, float start1, float stop1, float start2, float stop2)
    {
        //map the value
        float res = start2 + (stop2 - start2) * ((value - start1) / (stop1 - start1));
        //below we clamp the return value to be within the new start and stop
        //start2 isn't necessarily less than stop2 
        float new_min = Mathf.Min(start2, stop2);
        float new_max = Mathf.Max(start2, stop2);
        res = Mathf.Max(new_min, res);
        res = Mathf.Min(new_max, res);
        return res;
    }
}

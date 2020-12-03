using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pinchToMoveForward : MonoBehaviour
{
	Vector3 touchStart;
    float rotationSpeed = 10f;
    private Vector3 firstpoint; //change type on Vector3
    private Vector3 secondpoint;
    private float xAngle = 0.0f; //angle for axes x for rotation
    private float yAngle = 0.0f;
    private float xAngTemp = 0.0f; //temp variable for angle
    private float yAngTemp = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        xAngle = 0.0f;
        yAngle = 0.0f;
        transform.rotation = Quaternion.Euler(yAngle, xAngle, 0.0f);        
    }

    // Update is called once per frame
    void Update()
    {
    	if(Input.touchCount == 2){
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            zoom(difference*2f);
        }else if(Input.touchCount == 3){
            Touch touchZero = Input.GetTouch(0);
            if(touchZero.phase == TouchPhase.Moved){
                Quaternion rotationY = Quaternion.Euler(0f,- touchZero.deltaPosition.x * rotationSpeed,0f);
                transform.rotation = rotationY * transform.rotation;
            }

        } else if(Input.touchCount == 1 && !GlobalVariableManager.Instance.autoGyroMode){
        	if(Input.GetTouch(0).phase == TouchPhase.Began) {
                firstpoint = Input.GetTouch(0).position;
                xAngTemp = xAngle;
                yAngTemp = yAngle;
            }
            
            //Move finger by screen
            if(Input.GetTouch(0).phase==TouchPhase.Moved) {
                 secondpoint = Input.GetTouch(0).position;
                 //Mainly, about rotate camera. For example, for Screen.width rotate on 180 degree
                 xAngle = xAngTemp + (secondpoint.x - firstpoint.x) * 180.0f / Screen.width;
                 yAngle = yAngTemp - (secondpoint.y - firstpoint.y) *90.0f / Screen.height;
                 //Rotate camera
                 transform.rotation = Quaternion.Euler(-yAngle*0.6f, -xAngle*0.6f, 0.0f);	
            }
        }	
    }


    private void zoom(float increment){
    	transform.position += 	this.transform.forward * increment;

    }
}

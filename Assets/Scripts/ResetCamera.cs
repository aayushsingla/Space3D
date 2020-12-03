using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCamera : MonoBehaviour
{
    public void ResetPosition(){
    	Camera.main.transform.position = new Vector3(0,0,0);
    	GlobalVariableManager.Instance.ShowToast("Camera set to original position",2);
    }
}

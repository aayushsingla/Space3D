using UnityEngine;

public class GyroControl : MonoBehaviour
{
	private bool gyroEnabled;
	private Gyroscope gyro;
	private GameObject cameraContainer;
	private Quaternion rot;
    // Start is called before the first frame update
    private void Start()
    {
    	cameraContainer = new GameObject("Camera Container");
    	cameraContainer.transform.position = transform.position;
    	transform.SetParent(cameraContainer.transform);

    	gyroEnabled = EnableGyro();
        GlobalVariableManager.Instance.autoGyroMode = gyroEnabled;
    }

    // Update is called once per frame
    private void Update(){
       if(gyroEnabled && GlobalVariableManager.Instance.autoGyroMode){
        	transform.localRotation = gyro.attitude * rot;
        }
    }


    private bool EnableGyro(){
    	if(SystemInfo.supportsGyroscope){
    		gyro = Input.gyro;
    		gyro.enabled = true;
    		cameraContainer.transform.rotation = Quaternion.Euler(90f,90f,0f);
    		rot = new Quaternion(0,0,1,0);
    		return true;

    	}

    	return false;
    }
}


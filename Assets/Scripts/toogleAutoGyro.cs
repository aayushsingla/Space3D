using UnityEngine;

public class toogleAutoGyro : MonoBehaviour
{
    // Start is called before the first frame update
   	public void toogle(){
   		GlobalVariableManager.Instance.autoGyroMode = !GlobalVariableManager.Instance.autoGyroMode;
    	if(GlobalVariableManager.Instance.autoGyroMode)
    		GlobalVariableManager.Instance.ShowToast("Gyroscope Enabled",2);
    	else
    		GlobalVariableManager.Instance.ShowToast("Gyroscope Disabled",2);

   	}
}

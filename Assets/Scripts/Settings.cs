using UnityEngine;

public class Settings : MonoBehaviour
{
	public GameObject Panel;
	public GameObject SearchPanel;
	
	public static Settings Instance{get; private set;}

    private void Awake(){
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }
    
    public void OpenPanel(){
    	Debug.Log("OpenPanel");

    	if (Panel != null){
    		bool isActive = Panel.activeSelf;
    		
    		Debug.Log(isActive);

    		Panel.SetActive(!isActive); 
    	}
    }


    public void openSearchPanel(){
    	Debug.Log("openSearchPanel");

		if(!GlobalVariableManager.Instance.search_panel_on){
			OpenPanel();
			SearchPanel.SetActive(true);
			GlobalVariableManager.Instance.search_panel_on = true;
		}
	
    }

    public void closeSearchPanel(){
    	Debug.Log("closeSearchPanel");
	
		if(GlobalVariableManager.Instance.search_panel_on){
			SearchPanel.SetActive(false);
			GlobalVariableManager.Instance.search_panel_on = false;
		}
    }

    		
}	

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickDetection : MonoBehaviour
{
	public Text textBox;
	private bool objectSelected = false;
	private GameObject lastObj;
	private RaycastHit hitInfo;

    public static ClickDetection Instance{get; private set;}

    private void Awake(){
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hitInfo))
            {
                //var rig = hitInfo.collider.GetComponent<Rigidbody>();
                var coll = hitInfo.collider;
                if(coll != null)
                {	

                	if(objectSelected){
                		UnSelect(lastObj);
                	}
                	
                	Select(coll.gameObject);
                	
            	}else{

            		UnSelect(lastObj);
                	

                }
            	
            }else{
            	if(lastObj  !=  null)
        			UnSelect(lastObj);
            }
        }


    }


    public void Select(GameObject obj){
    	obj.AddComponent<Outline>();
    	Debug.Log("Distance" + hitInfo.distance.ToString());
    	var data = obj.GetComponent<ObjectProfile>();
    	if(data!=null){
    		textBox.text = "Id: "+data.id+"\nName: "+ data.name+"\nDistance: "+data.dist+" parsecs\nMag: "+data.mag+"\nAbsMag: "+data.absmag+"\nSpect: "+data.spect+"\nColor Index: "+data.ci;
		}else{
			textBox.text = "";
		}

		objectSelected = true;
		lastObj = obj;


    }
    public void UnSelect(GameObject obj){
    	Destroy(obj.GetComponent<Outline>());
    	textBox.text="";
    	objectSelected = false;
    }
}

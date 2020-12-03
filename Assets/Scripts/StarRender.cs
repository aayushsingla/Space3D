using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using ImportantStuff;

public class StarRender : MonoBehaviour
{
	string m_Path;
    public GameObject star;
    public DataObject database;

	public static StarRender Instance{get; private set;}
	
	private void Awake(){
		if(Instance == null){
			Instance = this;
			if(database == null)
				database = new DataObject();
			DontDestroyOnLoad(gameObject);
		}else{
			Destroy(gameObject);
		}
	}
    
    void Start()
    {	
    	if(!database.IsLoaded()){
    		var filePath = Path.Combine(Application.streamingAssetsPath, "hygdata_v3.csv");
	    	
	    	#if UNITY_ANDROID

		    	var loadingRequest = UnityWebRequest.Get(Path.Combine(Application.streamingAssetsPath, "hygdata_v3.csv"));
		    	loadingRequest.SendWebRequest();
		    	while (!loadingRequest.isDone) {
				     if (loadingRequest.isNetworkError || loadingRequest.isHttpError) {
				     	Debug.Log("Network Error");
				        break;
				     }
			     	Debug.Log("Loading....");

				}
				if (loadingRequest.isNetworkError || loadingRequest.isHttpError) {
				 
				} else {
				   File.WriteAllBytes(Path.Combine(Application.persistentDataPath, "hygdata_v3.csv"), loadingRequest.downloadHandler.data);
			       Debug.Log("Writing....");

				}
				filePath = Path.Combine(Application.persistentDataPath,"hygdata_v3.csv");

		    #endif

	    	using(StreamReader reader = new StreamReader(filePath)){

	        	string content = reader.ReadToEnd();
	        	database.Load(content);
		    }

    	}

	    	Render();
		    
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void Render(){
    	int rows = database.NumRows();
    	List<DataObject.Row> rowList = database.GetRowList();

    	for(int i = 2; i< rows;i++){
    		DataObject.Row row = rowList[i];
    		float dist = float.Parse(row.dist);
            float x = float.Parse(row.x);
            float y = float.Parse(row.y);
  		    float z = float.Parse(row.z);
  		    
  	        GameObject clone = Instantiate(star, new Vector3((x*1e+5f)/dist,(z*1e+5f)/dist,(y*1e+5f)/dist), Quaternion.identity);
  	        clone.name = row.proper;
            ObjectProfile data = clone.AddComponent(typeof(ObjectProfile)) as ObjectProfile;
			data.id = row.id;
  	        data.name = row.proper;
  	        data.dist = row.dist;
  	        data.mag = row.mag;
  	        data.absmag = row.absmag;
  	        data.spect = row.spect;
  	        data.ci = data.ci;
    	}
    }


    private Vector3 get_size(float luminosity){
    	float luminosity_sun = 1f;
	    float diameter_sun_squared = 0.00008645765f; //in AU
	    float diameter = Mathf.Sqrt(luminosity*diameter_sun_squared/luminosity_sun);
	    diameter = diameter * 14960f;

	    return new Vector3(diameter,diameter,diameter);

    }
}

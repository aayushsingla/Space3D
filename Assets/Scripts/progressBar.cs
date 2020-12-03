using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class progressBar : MonoBehaviour
{	private AsyncOperation loadingOperation;
	private float loadProgress;
	private RectTransform rct;
    public Text startText; // used for showing countdown from 3, 2, 1 

    // Start is called before the first frame update
    void Start()
    {
         loadingOperation = SceneManager.LoadSceneAsync("SolarSystem");
         rct = GetComponent<RectTransform>();
    	 rct.sizeDelta = new Vector2(0,rct.sizeDelta.y);

    }

    // Update is called once per frame
    void Update()
    {	
		loadProgress = loadingOperation.progress;
		Debug.Log("progress "+loadProgress);
		rct.sizeDelta = new Vector2(loadProgress * Screen.width,rct.sizeDelta.y);
        startText.text = "Time Spent: "+ Time.timeSinceLevelLoad+ "s";	
    }
}

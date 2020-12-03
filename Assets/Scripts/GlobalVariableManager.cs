using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


public class GlobalVariableManager : MonoBehaviour
{
	public bool autoGyroMode;
	public Text txt;
	public bool search_panel_on;

	public static GlobalVariableManager Instance{get; private set;}


	private void Awake(){
		if(Instance == null){
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}else{
			Destroy(gameObject);
		}
	}

	public void ShowToast(string text, int duration)
	{
    	StartCoroutine(showToastCOR(text, duration));
	}

	private IEnumerator showToastCOR(string text, int duration)
	{
	    Color orginalColor = txt.color;

	    txt.text = text;
	    txt.enabled = true;

	    //Fade in
	    yield return fadeInAndOut(txt, true, 0.5f);

	    //Wait for the duration
	    float counter = 0;
	    while (counter < duration)
	    {
	        counter += Time.deltaTime;
	        yield return null;
	    }

	    //Fade out
	    yield return fadeInAndOut(txt, false, 0.5f);

	    txt.enabled = false;
	    txt.color = orginalColor;
	}

	private IEnumerator fadeInAndOut(Text targetText, bool fadeIn, float duration)
	{
	    //Set Values depending on if fadeIn or fadeOut
	    float a, b;
	    if (fadeIn)
	    {
	        a = 0f;
	        b = 1f;
	    }
	    else
	    {
	        a = 1f;
	        b = 0f;
	    }

	    Color currentColor = Color.clear;
	    float counter = 0f;

	    while (counter < duration)
	    {
	        counter += Time.deltaTime;
	        float alpha = Mathf.Lerp(a, b, counter / duration);

	        targetText.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
	        yield return null;
	    }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Search : MonoBehaviour
{   private Vector3 center;
    private Vector3 search_pos;
    private bool search_on = false;
    public RawImage img;
    public GameObject txt;
    // Start is called before the first frame update



    public void StartSearch(){
        Debug.Log("StartSearch");

        searchFor((txt.GetComponent<InputField>()).text);
        txt.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        if(search_on){
            updateArrow();
        }
    }

    private void searchFor(string query)
    {        
        Debug.Log("SearchFor");
        Debug.Log(query);

        GameObject obj = GameObject.Find(query);
        if(obj == null){
            GlobalVariableManager.Instance.ShowToast("Body not found",3);
            SearchComplete();
            return;
        }
        //initVector = new  Vector3(0,-1f,0);    
        search_pos = obj.transform.position;
        search_pos = new Vector3(search_pos.x, search_pos.y,0f ).normalized;

        Debug.Log(search_pos);

        search_on = true;
        ClickDetection.Instance.Select(obj);


    }

    private void updateArrow(){
        Debug.Log("updateArrow");
        center = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2,Screen.height/2,100f));
        Vector3 init = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2,Screen.height,100f));
        Debug.Log(center.x.ToString()+ " " +center.y.ToString()+""+center.z.ToString());
        Vector3 newVec = search_pos - center;

        Vector3 inView = Camera.main.WorldToViewportPoint(search_pos);
        if(inView.x >0 && inView.x <1 && inView.y >0 && inView.y <1){
            SearchComplete();
        }
        // if(Mathf.Abs(newVec.x)<Screen.width/2){
        //     SearchComplete();
        //     return;
        // }

        var angle = Vector2.SignedAngle(new Vector2((init-center).x,(init-center).y),new Vector2(inView.x,inView.y));

        // Vector3 projection = Vector3.ProjectOnPlane(newVec,new Vector3(center.x-Camera.main.transform.position.x,center.y-Camera.main.transform.position.y,0));
        // var angle = Vector3.SignedAngle(initVector,projection,new Vector3(center.x-Camera.main.transform.position.x,center.y-Camera.main.transform.position.y,0));
        //img.transform.position =  final;
        img.transform.rotation = Quaternion.Euler(0,0,angle);
    }

    private Vector2 DegreeToUnitVector2(float degrees)
    {
        float radians = degrees * Mathf.Deg2Rad; 
        return new Vector2(Mathf.Sin(radians), Mathf.Cos(radians));
    }

    private void SearchComplete(){
        Debug.Log("SearchComplete");

        search_on = false;
        txt.SetActive(true);
        Settings.Instance.closeSearchPanel();
    }

}

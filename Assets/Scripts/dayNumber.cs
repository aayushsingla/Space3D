using UnityEngine;
using System;

public class dayNumber : MonoBehaviour
{
    public static int dayNum;

    void Awake(){

	    int Y = int.Parse(System.DateTime.Now.ToString("yyyy"));
		int M = int.Parse(System.DateTime.Now.ToString("MM"));
		int D = int.Parse(System.DateTime.Now.ToString("dd"));

		dayNum = getDayNum(Y,M,D);
        	
    }


    int getDayNum(int Y,int M,int D){
    	return 367*Y - (7*(Y + ((M+9)/12)))/4 + (275*M)/9 + D - 730530;  	
    }

}

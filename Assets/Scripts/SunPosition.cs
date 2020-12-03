using UnityEngine;
using System;

public class SunPosition : MonoBehaviour
{
	// Start is called before the first frame update
    void Start(){

    	transform.position = getSunCoordiantes(dayNumber.dayNum)*14960f;
    	transform.localPosition = transform.position;
    }

    public static double rev(double x)
	{
        return  x - System.Math.Floor(x/360f)*360.0;
    }

    public static Vector3 getSunCoordiantes(int d){	

		// Refer to http://www.stjarnhimlen.se/comp/tutorial.html#5 for more details
    	double longitude_perihelion = 282.9404 + (4.70935* d)/100000;  
	    double mean_distance_au = 1.000000;
	    double eccentricity = 0.016709 - (1.151* d)/1000000000;
	    double mean_anomaly = rev(356.0470 + 0.9856002585 * d);
	     
	    double obliquity = 23.4393 - 3.563E-7 * d;

	    double mean_longitude = rev(longitude_perihelion + mean_anomaly);
	    double ecentric_anomaly = mean_anomaly + 
	    (180/System.Math.PI) * eccentricity * System.Math.Sin((mean_anomaly * System.Math.PI)/180) 
	    * (1 + eccentricity * System.Math.Cos(mean_anomaly * System.Math.PI));


	    double x = System.Math.Cos((ecentric_anomaly * System.Math.PI)/180) - eccentricity;
	    double z = System.Math.Sin((ecentric_anomaly * System.Math.PI)/180) * System.Math.Sqrt(1 - eccentricity*eccentricity);
	    double y = 0;

	    double r = System.Math.Sqrt(x*x + z*z);
	    double v =  Mathf.Atan2((float)z,(float)x) * Mathf.Rad2Deg;

	    // Debug.Log(r);
	    // Debug.Log(v);

	    double lon = rev(v + longitude_perihelion);
	    // Debug.Log(lon);

	    // 1 au = 10^11 and we assume 10^7 = 1. Therefore, x = x *1.496 *100 and y = y * 1.496*100 for au to m conversion
	    x = r * System.Math.Cos(lon * Mathf.Deg2Rad);
	    z = r * System.Math.Sin(lon * Mathf.Deg2Rad);
	    y = 0;
	    // Debug.Log(x);
	    // Debug.Log(z);

	 
	    return new Vector3((float)x,(float)y,(float)z);
    }
}

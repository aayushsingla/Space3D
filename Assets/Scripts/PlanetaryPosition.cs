using UnityEngine;

public class PlanetaryPosition : MonoBehaviour
{
    
	public float[] longitude_ascillion_data = new float[2];
    public float[] inclination_data = new float[2];
    public float[] arguement_perihelion_data = new float[2];
    public float semi_major_axis;
    public float[] eccentricity_data = new float[2];
    public float[] mean_anomaly_data = new float[2];
    // Start is called before the first frame update

//    public SunPosition sunPosition;

    void Start()
    {


    	int d = dayNumber.dayNum;
    	float N = rev(longitude_ascillion_data[0] + longitude_ascillion_data[1]*d);
        float i = rev(inclination_data[0] + inclination_data[1]*d);
        float w = rev(arguement_perihelion_data[0] + arguement_perihelion_data[1]*d);
        float a = semi_major_axis;
        float e = eccentricity_data[0] + eccentricity_data[1]*d;
        float M = rev(mean_anomaly_data[0] + mean_anomaly_data[1]*d);

	    float E = M + (180f/Mathf.PI) * e * Mathf.Sin((M * Mathf.PI)/180f); 
		float E1 = E - (E - (180f/Mathf.PI) * e * Mathf.Sin((E * Mathf.PI)/180f)-M)/(1-e*Mathf.Cos(E * Mathf.PI/180f)); 	

		while((E1-E) > 0.05){
			E = E1;
			E1 = E - (E - (180f/Mathf.PI) * e * Mathf.Sin((E * Mathf.PI)/180f)-M)/(1-e*Mathf.Cos(E * Mathf.PI/180f));
		}
		E1 = rev(E1);

		float x = a*(Mathf.Cos((E1 * Mathf.PI)/180f) - e);
	    float z = a*Mathf.Sin((E1 * Mathf.PI)/180f) * Mathf.Sqrt(1 - e*e);
	    float y = 0;


	    float r = Mathf.Sqrt(x*x + z*z);
	    float v =  rev(Mathf.Atan2(z,x) * Mathf.Rad2Deg);

	    x = r * ( Mathf.Cos(N* Mathf.Deg2Rad) * Mathf.Cos((v+w)* Mathf.Deg2Rad) - Mathf.Sin(N* Mathf.Deg2Rad) * Mathf.Sin((v+w)* Mathf.Deg2Rad) * Mathf.Cos(i* Mathf.Deg2Rad) );
    	z = r * ( Mathf.Sin(N* Mathf.Deg2Rad) * Mathf.Cos((v+w)* Mathf.Deg2Rad) + Mathf.Cos(N* Mathf.Deg2Rad) * Mathf.Sin((v+w)* Mathf.Deg2Rad) * Mathf.Cos(i* Mathf.Deg2Rad) );
    	y = r * Mathf.Sin((v+w)* Mathf.Deg2Rad) * Mathf.Sin(i * Mathf.Deg2Rad);



    	Vector3 planet= new Vector3(x,y,z); 
        Vector3 sun = SunPosition.getSunCoordiantes(d);
        planet = planet + sun;

    	transform.position = planet*149.6f;


    

        
    }


 	float rev(float x)
	{
        return  x - Mathf.Floor(x/360f)*360f;
    }
 
}

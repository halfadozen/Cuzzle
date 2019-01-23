using UnityEngine;
using System.Collections;

public class AnimatedGradient : MonoBehaviour {

	Material m;
	float colorPosition = 0;
	bool forward = true;
	Color startColor = Color.green;
	Color endColor = Color.red;
	Color startColor2 = Color.blue;
	Color endColor2 = Color.cyan;

	// Use this for initialization
	void Start () {	
		m = gameObject.GetComponent<MeshRenderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
		Color a = Color.Lerp(startColor,endColor,colorPosition);
		Color b = Color.Lerp(endColor,startColor,colorPosition);
		Color a2 = Color.Lerp(startColor2,endColor2,colorPosition);
		Color b2 = Color.Lerp(endColor2,startColor2,colorPosition);

		m.SetColor("_FrontColor1",a);
		m.SetColor("_FrontColor3",b);

		m.SetColor("_LeftColor1",a2);
		m.SetColor("_LeftColor3",b2);

		if(forward){
			colorPosition+=0.02f;
			if(colorPosition>=1) {
				colorPosition=1;
				forward=false;
			}
		} else {
			colorPosition-=0.02f;
			if(colorPosition<=0) {
				colorPosition=0;
				forward=true;
			}
		}
	}
}

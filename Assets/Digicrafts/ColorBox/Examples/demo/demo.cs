using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class demo : MonoBehaviour {


	public static bool RotationEnalbed = true;

	public GameObject _objects;
	public Dropdown _dropdown;

	private string address = "https://www.assetstore.unity3d.com/en/#!/search/page=1/sortby=popularity/query=publisher:20415";


	private bool _mixColor=false;
	private string _world="_UV_SPACE_OBJECT";
	private bool _shadow=false;
	private bool _fade=false;
	private bool _spec=false;
	private bool _outline=false;

	private Shader _shader1;
	private Shader _shader2;

	private Dictionary<string, float> _colorValue;

	void Start(){

		Transform tran = _objects.transform.GetChild(0);
		GameObject obj = tran.gameObject;
		Material mat = obj.GetComponent<MeshRenderer>().material;

		_shader1=Shader.Find("Digicrafts/ColorBox");
		_shader2=Shader.Find("Digicrafts/ColorBox-Outline");
			
		string[] key = new string[]{"_TOP_COLOR","_BOTTOM_COLOR","_LEFT_COLOR","_RIGHT_COLOR","_FRONT_COLOR","_BACK_COLOR"};
		_colorValue = new Dictionary<string, float>();
		foreach(string color in key){			
			_colorValue.Add(color,mat.GetFloat(color));
		}

	}

	public void openURL(){
		Application.OpenURL(address);
	}

	public void toggleRotation(){
		RotationEnalbed=!RotationEnalbed;
	}

	public void toggleMixColor(){
		_mixColor=!_mixColor;
		foreach(MeshRenderer mat in _objects.GetComponentsInChildren<MeshRenderer>()){
			if(_mixColor)
				mat.material.EnableKeyword("_MIXCOLOR_ON");
			else
				mat.material.DisableKeyword("_MIXCOLOR_ON");
		}
	}

	public void toggleUV(){

		string world=_world;

		switch(_dropdown.value){
		case 0:
			world="_UV_SPACE_OBJECT";
			break;
		case 1:
			world="_UV_SPACE_WORLD";
			break;
		case 2:
			world="_UV_SPACE_SCREEN";
			break;
		}
			
		foreach(MeshRenderer mat in _objects.GetComponentsInChildren<MeshRenderer>()){
			mat.material.EnableKeyword(world);
			mat.material.DisableKeyword(_world);
		}

		_world=world;

	}

	public void toggleColor(string color){

		foreach(MeshRenderer mat in _objects.GetComponentsInChildren<MeshRenderer>()){

			float val = mat.material.GetFloat(color);

			if(val==0)
				mat.material.SetFloat(color,_colorValue[color]);
			else
				mat.material.SetFloat(color,0);
		}

	}

	public void toggleShadow(){
		_shadow=!_shadow;
		foreach(MeshRenderer mat in _objects.GetComponentsInChildren<MeshRenderer>()){
			if(_shadow)
				mat.material.EnableKeyword("_SHADOW_ON");
			else
				mat.material.DisableKeyword("_SHADOW_ON");
		}
	
	}

	public void toggleSpec(){
		_spec=!_spec;

		foreach(MeshRenderer mat in _objects.GetComponentsInChildren<MeshRenderer>()){
			if(_spec)
				mat.material.EnableKeyword("_SPECULAR_ON");
			else
				mat.material.DisableKeyword("_SPECULAR_ON");
		}
	}

	public void toggleFade(){
		_fade=!_fade;
		foreach(MeshRenderer mat in _objects.GetComponentsInChildren<MeshRenderer>()){
			if(_fade){
				mat.material.EnableKeyword("_FADE_BOTTOM");
				mat.material.DisableKeyword("_FADE_NONE");
			}else{
				mat.material.EnableKeyword("_FADE_NONE");
				mat.material.DisableKeyword("_FADE_BOTTOM");
			}
		}
	}

	public void toggleOutline(){
		_outline=!_outline;
		foreach(MeshRenderer mat in _objects.GetComponentsInChildren<MeshRenderer>()){
			if(_outline){
				mat.material.shader=_shader2;
			}else{
				mat.material.shader=_shader1;
			}
		}
	}

}

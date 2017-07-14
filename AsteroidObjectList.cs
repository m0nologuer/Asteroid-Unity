using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AsteroidObjectList : MonoBehaviour {

	public AsteroidObject[] children;

	public bool use_randomizer;
	public bool use_color_randomizer;
	public bool use_texture_randomizer;
	public bool use_material_randomizer;

	public float random_material_variance;
	public float random_color_brightness;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

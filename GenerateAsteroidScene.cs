using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[System.Serializable]
class FilterData:object{
	public string filePath;
	public FilterData(Object obj){
		filePath = AssetDatabase.GetAssetPath(obj);
		filePath = filePath.Substring(7, filePath.Length-7);
	}
}

[System.Serializable]
class VectorData:object{
	public float x;
	public float y;
	public float z;
	public VectorData(Vector3 vec){
		x = vec.x;
		y = vec.y;
		z = vec.z;
	}
}


[System.Serializable]
class ConstraintData:object{
	public int obj_id1;
	public int obj_id2;
	public float var1;
	public float var2;
	public float var3;
	public int type;

	public VectorData pos1;
	public VectorData pos2;
	public VectorData hpr1;
	public VectorData hpr2;

	public ConstraintData(AsteroidConstraint constraint, List<AsteroidObject> asteroidObjectList){
		obj_id1 = asteroidObjectList.FindIndex(a => a == constraint.object1);
		obj_id2 = asteroidObjectList.FindIndex(a => a == constraint.object2);

		type = (int)constraint.type;

		var1 = constraint.var1;
		var2 = constraint.var2;
		var3 = constraint.var3;

		pos1 = new VectorData(constraint.object1.transform.position);
		hpr1 = new VectorData(constraint.object1.transform.rotation.eulerAngles);

		if (constraint.object2){
			pos2 = new VectorData(constraint.object2.transform.position);
			hpr2 = new VectorData(constraint.object2.transform.rotation.eulerAngles);
		}

	}
}


[System.Serializable]
class BufferData:object{
	public string name;
	public string shader_name;
	public FilterData[] filters;
	public BufferData(Buffer buf){
		name = buf.name;
		shader_name = AssetDatabase.GetAssetPath(buf.shader);
		shader_name = shader_name.Substring(7, shader_name.Length-7);

		List<FilterData> filterList = new List<FilterData>();
		System.Console.WriteLine(buf.filters);
		foreach (Object obj in buf.filters){
			filterList.Add(new FilterData(obj));
		}
		filters = filterList.ToArray();
	}
}


[System.Serializable]
class ColorData:object{
	public float r;
	public float g;
	public float b;
	public float a;

	public ColorData(Color color){
		r = color.r;
		g = color.g;
		b = color.b;
		a = color.a;
	}
	public ColorData(){
		r = 1;
		g = 1;
		b = 1;
		a = 1;
	}
}


[System.Serializable]
class MaterialData:object{
	public ColorData diffuse;
	public ColorData emission;
	public ColorData specular;
	public float shininess;

	public MaterialData(Material mat){

		if (mat.HasProperty("_Color")){
			diffuse = new ColorData(mat.GetColor("_Color"));
		}
		else{
			diffuse = new ColorData();
		}

		if (mat.HasProperty("_SpecColor")){
			specular = new ColorData(mat.GetColor("_SpecColor"));
		}
		else{
			specular = new ColorData();
		}

		if (mat.HasProperty("_EmissionColor")){
			emission = new ColorData(mat.GetColor("_EmissionColor"));
		}
		else{
			emission = new ColorData();
		}

		if (mat.HasProperty("Metallic")){
			shininess = mat.GetFloat("Metallic");
		}
		else{
			shininess = 0;
		}
	}
}


[System.Serializable]
class ModelData:object{
	public string mesh;
	public string texture;
	public MaterialData material;

	public ModelData(GameObject gameObject){

		Material mat = gameObject.GetComponentsInChildren<MeshRenderer>()[0].material;
		material = new MaterialData(mat);

		mesh = AssetDatabase.GetAssetPath(gameObject.GetComponentsInChildren<MeshFilter>()[0].sharedMesh.GetInstanceID());
		texture = AssetDatabase.GetAssetPath(mat.mainTexture);

		mesh = mesh.Substring(7, mesh.Length-7);
		if (texture.Length > 7) {
			texture = texture.Substring(7, texture.Length-7);
		}
	
	}
}
[System.Serializable]
class PhysicsOptionsData:object{
	public string name;
	public bool rigid;
	public float mass;
	public VectorData position;
	public VectorData velocity;
	public VectorData rotation;
	public VectorData scale;

	public PhysicsOptionsData(AsteroidObject obj){
		name = obj.material.material_name;
		position = new VectorData(obj.gameObject.transform.position);
		rotation = new VectorData(obj.gameObject.transform.rotation.eulerAngles);
		scale = new VectorData(obj.gameObject.transform.lossyScale);
		velocity = new VectorData(obj.velocity);
		rigid = true;
		mass = obj.mass;
	}
}

[System.Serializable]
class ObjectData:object{
	public string name;
	public int model_id;
	public string script;
	public string obj_class;
	public PhysicsOptionsData physics_options;

	public ObjectData(AsteroidObject obj, int id){
		model_id = id;
		name = obj.name;
		script = AssetDatabase.GetAssetPath(obj.python_script);
		script = script.Substring(19, script.Length-22);
		obj_class = obj.python_class;
		physics_options = new PhysicsOptionsData(obj);
	}
}
[System.Serializable]
class SelectedObjectData:object{
	public string name;
	public int object_id;
	public SelectedObjectData(string _name, int _obj){
		name = _name;
		object_id =_obj;
	}
}
[System.Serializable]
class ColorRandomizer:object{
	public bool use_randomizer;
	public float brightness;
	public ColorRandomizer(AsteroidObjectList list){
		use_randomizer = list.use_color_randomizer;
		brightness = list.random_color_brightness;
	}

}
[System.Serializable]
class MaterialRandomizer:object{
	public bool use_randomizer;
	public float variance;
	public MaterialRandomizer(AsteroidObjectList list){
		use_randomizer = list.use_material_randomizer;
		variance = list.random_material_variance;
	}
}
[System.Serializable]
class SpawnRandomizer:object{
	public bool use_randomizer;
	public int number;
	public VectorData plane_position;
	public VectorData plane_normal;
	public VectorData plane_u;
	public float x;
	public float y;
	public SpawnRandomizer(AsteroidObjectList list){
		use_randomizer = list.use_spawn_randomizer;
		number = list.spawn_number;
		plane_position = new VectorData(list.transform.position);
		plane_normal = new VectorData(list.transform.TransformVector(new Vector3(0,1,0)).normalized);
		plane_u = new VectorData(list.transform.TransformVector(new Vector3(1,0,0)).normalized);
		x = list.spawn_x;
		y = list.spawn_y;
	}
}
[System.Serializable]
class TextureRandomizer:object{
	public bool use_randomizer;
	public TextureRandomizer(AsteroidObjectList list){
		use_randomizer = list.use_texture_randomizer;
	}
}
[System.Serializable]
class Randomizer:object{
	public bool use_randomizer;
	public ColorRandomizer color_randomize_options;
	public TextureRandomizer texture_randomize_options;
	public MaterialRandomizer material_randomize_options;
	public SpawnRandomizer spawn_options;

	public Randomizer(AsteroidObjectList list){
		use_randomizer = list.use_randomizer;
		color_randomize_options = new ColorRandomizer(list);
		material_randomize_options = new MaterialRandomizer(list);
		texture_randomize_options = new TextureRandomizer(list);
		spawn_options = new SpawnRandomizer(list);
	}
}
[System.Serializable]
class ObjectListData:object{
	public SelectedObjectData[] children;
	public VectorData position;
	public Randomizer randomizer;

	public ObjectListData(AsteroidObjectList list, SelectedObjectData[] input_children){
		children = input_children;
		position = new VectorData(list.gameObject.transform.position);
		randomizer = new Randomizer(list);
	}
}
[System.Serializable]
class MatieralData:object{
	public string name;
	public float angular_stiffness;
	public float linear_stiffness;
	public float volume_preservation;
	public MatieralData(AsteroidMaterial mat){
		name = mat.material_name;
		angular_stiffness = mat.angular_stiffness;
		linear_stiffness = mat.linear_stiffness;
		volume_preservation = mat.volume_preservation;
	}
}
[System.Serializable]
class EnvData:object{
	public int action_dim;
	public int obs_dim;
	public SelectedObjectData score_keeper;
	public SelectedObjectData action_controller;
	public VectorData camera_p;
	public VectorData camera_hpr;

	public EnvData(int ad, SelectedObjectData sk, SelectedObjectData ac, Camera cam){
		action_dim = ad;
		obs_dim = 10; //dummy variable
		score_keeper = sk;
		action_controller = ac;
		camera_p = new VectorData(cam.transform.position);
		camera_hpr = new VectorData(cam.transform.rotation.eulerAngles);
	}
}


[System.Serializable]
class LightData:object{
	public VectorData direction;
	public ColorData color;

	public LightData(Light light){
		direction = new VectorData(light.transform.TransformVector(new Vector3(0,0,-1)).normalized);
		color = new ColorData(light.color);
	}
}

[System.Serializable]
class AsteroidData:object{
	public BufferData[] buffers;
	public ModelData[] models;
	public MatieralData[] materials;
	public ObjectData[] objects;
	public ObjectListData[] object_lists;
	public EnvData env_config;
	public LightData[] light_data;
	public ConstraintData[] constraint_data;


	public AsteroidData(Buffer[] input_buffers, AsteroidMaterial[] input_materials, 
		AsteroidObjectList[] input_object_lists, AsteroidObject action_controller, AsteroidObject score_keeper,
		int action_dim, Camera cam, Light[] lights, AsteroidConstraint[] constraints){

		//Create buffers
		List<BufferData> bufferList = new List<BufferData>();
		foreach (Buffer buf in input_buffers){
			bufferList.Add(new BufferData(buf));
		}
		buffers = bufferList.ToArray();

		//Materials
		List<MatieralData> matList = new List<MatieralData>();
		foreach (AsteroidMaterial mat in input_materials){
			matList.Add(new MatieralData(mat));
		}
		materials = matList.ToArray();

		//Set up special objects
		SelectedObjectData sk = new SelectedObjectData("",0);
		SelectedObjectData ac = new SelectedObjectData("",0);

		//Create object list
		List<ModelData> modelList = new List<ModelData>();
		List<ObjectData> objectList = new List<ObjectData>();
		List<AsteroidObject> asteroidObjectList = new List<AsteroidObject>();
		List<ObjectListData> objListList = new List<ObjectListData>();

		foreach (AsteroidObjectList asteroid_object_list in input_object_lists){
			//Create objects
			AsteroidObject[] asteroid_objects = asteroid_object_list.GetComponentsInChildren<AsteroidObject>();
			List<SelectedObjectData> selected_objects = new List<SelectedObjectData>();

			//Encode each object, each model, and also create a reference to it
			foreach (AsteroidObject obj in asteroid_objects){
				int obj_id = objectList.Count;
				objectList.Add(new ObjectData(obj, obj_id));
				asteroidObjectList.Add(obj);
				modelList.Add(new ModelData(obj.gameObject));
				selected_objects.Add(new SelectedObjectData(obj.object_name, obj_id));

				if (obj == action_controller){
					ac = new SelectedObjectData(obj.object_name, obj_id);
				}
				if (obj == score_keeper){
					sk = new SelectedObjectData(obj.object_name, obj_id);
				}

			}
			SelectedObjectData[] selected_objects_final = selected_objects.ToArray();

			objListList.Add(new ObjectListData(asteroid_object_list, selected_objects_final));
		}
		object_lists = objListList.ToArray();
		models = modelList.ToArray();
		objects = objectList.ToArray();

		//Create lights
		List<LightData> lightList = new List<LightData>();
		foreach (Light light in lights){
			lightList.Add(new LightData(light));
		}
		light_data = lightList.ToArray();

		//Create constraints
		List<ConstraintData> constraintList = new List<ConstraintData>();
		foreach (AsteroidConstraint constraint in constraints){
			constraintList.Add(new ConstraintData(constraint, asteroidObjectList));
		}
		constraint_data = constraintList.ToArray();

		//Finally, do the config
		env_config = new EnvData(action_dim, sk, ac, cam);
	}
}

public class GenerateAsteroidScene : MonoBehaviour {

	public Buffer[] buffers;
	public AsteroidObjectList[] object_lists;
	public int action_dim;
	public AsteroidObject action_controller;	
	public AsteroidObject score_keeper;	
	public AsteroidMaterial[] materials;
	public Camera camera;
	public Light[] lights;
	public AsteroidConstraint[] constraints;


	// Use this for initialization
	void Start () {
		
		AsteroidData obj = new AsteroidData(buffers, materials, object_lists, 
			action_controller, score_keeper, action_dim, camera, lights, constraints);

		string str = JsonUtility.ToJson(obj);
		string path = "Assets/assets/scene.json";

	    using (FileStream fs = new FileStream(path, FileMode.Create)){
	        using (StreamWriter writer = new StreamWriter(fs)){
	            writer.Write(str);
	       	}
	    }
	    UnityEditor.AssetDatabase.Refresh ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

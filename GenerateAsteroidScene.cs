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
class ModelData:object{
	public string mesh;
	public ModelData(GameObject gameObject){
		mesh = AssetDatabase.GetAssetPath(gameObject.GetComponentsInChildren<MeshFilter>()[0].sharedMesh.GetInstanceID());
		
		mesh = mesh.Substring(7, mesh.Length-7);
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
		script = script.Substring(7, script.Length-7);
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
class DefaultRandomizer:object{
	public bool use_randomizer;
	public DefaultRandomizer(AsteroidObjectList list){
		use_randomizer = list.use_texture_randomizer;
	}
}
[System.Serializable]
class Randomizer:object{
	public bool use_randomizer;
	public ColorRandomizer color_randomize_options;
	public DefaultRandomizer texture_randomize_options;
	public MaterialRandomizer material_randomize_options;
	public DefaultRandomizer spawn_options;

	public Randomizer(AsteroidObjectList list){
		use_randomizer = list.use_randomizer;
		color_randomize_options = new ColorRandomizer(list);
		material_randomize_options = new MaterialRandomizer(list);
		texture_randomize_options = new DefaultRandomizer(list);
		spawn_options = new DefaultRandomizer(list);
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
		name = mat.name;
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
class AsteroidData:object{
	public BufferData[] buffers;
	public ModelData[] models;
	public MatieralData[] materials;
	public ObjectData[] objects;
	public ObjectListData[] object_lists;
	public EnvData env_config;

	public AsteroidData(Buffer[] input_buffers, AsteroidMaterial[] input_materials, 
		AsteroidObjectList[] input_object_lists, AsteroidObject action_controller, AsteroidObject score_keeper,
		int action_dim, Camera cam){

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
		List<ObjectListData> objListList = new List<ObjectListData>();

		foreach (AsteroidObjectList asteroid_object_list in input_object_lists){
			//Create objects
			AsteroidObject[] asteroid_objects = asteroid_object_list.GetComponentsInChildren<AsteroidObject>();
			List<SelectedObjectData> selected_objects = new List<SelectedObjectData>();

			//Encode each object, each model, and also create a reference to it
			foreach (AsteroidObject obj in asteroid_objects){
				int obj_id = objectList.Count;
				objectList.Add(new ObjectData(obj, obj_id));
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

		//Finally, do the config
		env_config = new EnvData(action_dim, ac, sk, cam);
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


	// Use this for initialization
	void Start () {
		
		AsteroidData obj = new AsteroidData(buffers, materials, object_lists, 
			action_controller, score_keeper, action_dim, camera);

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

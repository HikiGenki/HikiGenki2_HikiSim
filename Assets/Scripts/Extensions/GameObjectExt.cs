using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExt{

    public static void SetParent(this GameObject go, GameObject parent){
		go.transform.SetParent(parent.transform);
	}

    public static void SetParent(this GameObject go, MonoBehaviour parent){
        go.transform.SetParent(parent.gameObject.transform);
    }

	public static List<GameObject> GetAllChildrenRecursive(this GameObject go) {
		List<GameObject> objs = new List<GameObject>();
		List<Transform> children = new List<Transform>();

		for(int i = 0; i < go.transform.childCount; i++) {
			Transform child = go.transform.GetChild(i);
			children.Add(child);
			objs.Add(child.gameObject);
        }

		for(int i = 0; i < children.Count; i++) {
			Transform child = children[i];
			List<GameObject> childObjects = child.gameObject.GetAllChildrenRecursive();
			objs.AddRange(childObjects);
        }
		return objs;
    }
	
	public static List<T> GetAllChildrenComponentsRecursive<T>(this GameObject go){
		List<T> childrenComponentList = new List<T>();
		List<Transform> children = new List<Transform>();
		
		for(int i = 0; i < go.transform.childCount; i++){
			Transform child = go.transform.GetChild(i);
			children.Add(child);
			
			if(child.gameObject.TryGetComponent<T>(out T t)) {
				//Debug.Log("Adding " + child.gameObject.name);
				childrenComponentList.Add(t);
            }
		}
		
		for(int i = 0; i < children.Count; i++){
			Transform child = children[i];
			List<T> childComps = child.gameObject.GetAllChildrenComponentsRecursive<T>();
			foreach(T tcomp in childComps){
				childrenComponentList.Add(tcomp);
			}
		}
		
		return childrenComponentList;
	}

	public static T AddInstanceAsChild<T>(this GameObject go, T prefab) where T : MonoBehaviour {
		T instance = GameObject.Instantiate(prefab);
		instance.SetParent(go);
		return instance;
    }
	public static GameObject AddInstanceAsChild(this GameObject go, GameObject prefab) {
		GameObject instance = GameObject.Instantiate(prefab);
		instance.SetParent(go);
		return instance;
    }

	public static void Activate(this GameObject go) {
		go.SetActive(true);
    }
	public static void Deactivate(this GameObject go) {
		go.SetActive(false);
    }

	public static bool IsActive(this GameObject go) {
		return go.activeSelf && go.activeInHierarchy;
	}

	public static void ToggleActive(this GameObject go) {
		go.SetActive(!go.IsActive());
    }

	public static void DestroyAllChildren(this GameObject go, bool immediate = true){
		for(int i = go.transform.childCount-1; i >= 0; i--){
			Transform child = go.transform.GetChild(i);
			if(immediate){
				GameObject.DestroyImmediate(child.gameObject);
			}else{
				GameObject.Destroy(child.gameObject);
			}
		}
	}
	
	public static List<GameObject> GetAllChildren(this GameObject go){
		List<GameObject> list = new List<GameObject>();
		for(int i = 0; i < go.transform.childCount; i++){
			list.Add(go.transform.GetChild(i).gameObject);
		}
		return list;
	}

    public static List<T> GetAllChildrenComponents<T>(this GameObject go){
		List<T> childrenComponentList = new List<T>();
        for(int i = 0; i < go.transform.childCount; i++){
            Transform child = go.transform.GetChild(i);
            T t = child.gameObject.GetComponent<T>();
            if(t != null){
                childrenComponentList.Add(t);
            }
        }

        return childrenComponentList;
	}
	
	public static GameObject GetFirstChild(this GameObject go){
		if(go.GetChildCount() == 0){
			return null;
		}
		return go.transform.GetChild(0).gameObject;
	}

	public static GameObject GetLastChild(this GameObject go){
		if(go.GetChildCount() == 0){
			return null;
		}
		return go.transform.GetChild(go.GetChildCount()-1).gameObject;
	}
	
	public static int GetChildCount(this GameObject go){
		return go.transform.childCount;
	}
}

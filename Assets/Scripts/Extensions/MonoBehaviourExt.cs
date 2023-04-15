using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class MonoBehaviourExt{

	public delegate void ActionDelegate();
	public delegate bool ConditionDelegate();
	public delegate Coroutine EnumeratedDelegate();
	public delegate IEnumerator EnumeratorDelegate();

	/// <summary>
	/// Performs an action after the provided delay
	/// </summary>
	/// <param name="mb"></param>
	/// <param name="delay">Delay before performing action</param>
	/// <param name="action">The action to be executed.</param>
	public static void DoAfter(this MonoBehaviour mb, float delay, ActionDelegate action) {
		mb.StartCoroutine(DelayedActionCoroutine(delay, action));
    }

	public static void DoAfterFrames(this MonoBehaviour mb, int frames, ActionDelegate action) {
		mb.StartCoroutine(DelayedFrameActionCoroutine(frames, action));
    }

	/// <summary>
	/// Performs an action in intervals. Stops if stopCondition is true
	/// </summary>
	/// <param name="mb"></param>
	/// <param name="interval"></param>
	/// <param name="action"></param>
	/// <param name="stopCondition"></param>
	public static void DoEvery(this MonoBehaviour mb, float interval, ActionDelegate action, ConditionDelegate stopCondition = null) {
		mb.StartCoroutine(IntervalActionCoroutine(interval, action, stopCondition));
    }

	public static void DoInOrder(this MonoBehaviour mb, params EnumeratedDelegate[] actions) {
		mb.StartCoroutine(OrderedActionCoroutine(mb, actions));
    }

	public static void DoAsCoroutine(this MonoBehaviour mb, EnumeratorDelegate[] actions) {
		EnumeratedDelegate[] actionOrders = new EnumeratedDelegate[actions.Length];
		for(int i = 0; i < actions.Length; i++) {
			actionOrders[i] = () => {
				return mb.StartCoroutine(actions[i]());
			};
        }

		mb.DoInOrder(actionOrders);
    }
	

	static IEnumerator OrderedActionCoroutine(MonoBehaviour routineStarter, params EnumeratedDelegate[] actions) {
		for(int i = 0; i < actions.Length; i++) {
			Debug.Log("Action " + i);
			yield return actions[i]();
        }
    }

	static IEnumerator DelayedActionCoroutine(float delay, ActionDelegate action) {
		yield return new WaitForSeconds(delay);
		action();
        
    }

	static IEnumerator DelayedFrameActionCoroutine(int frames, ActionDelegate action) {
		for (int i = 0; i < frames; i++) {
			yield return new WaitForEndOfFrame();
		}
		action();
    }


	static IEnumerator IntervalActionCoroutine(float interval, ActionDelegate action, ConditionDelegate stopCondition = null) {
        while (true) {
			action();
			yield return new WaitForSeconds(interval);
			if(stopCondition != null && stopCondition()) {
				break;
            }
        }
    }
	
	public static void DestroyAllChildren(this MonoBehaviour mb, bool immediate = true){
		mb.gameObject.DestroyAllChildren(immediate);
	}
	
	public static bool IsActive(this MonoBehaviour mb){
		return mb.gameObject.activeSelf && mb.gameObject.activeInHierarchy;
	}

	public static void Activate(this MonoBehaviour mb){
		mb.gameObject.SetActive(true);
	}
	
	public static void Deactivate(this MonoBehaviour mb){
		mb.gameObject.SetActive(false);
	}
	
	public static int GetChildCount(this MonoBehaviour mb){
		return mb.gameObject.transform.childCount;
	}
	
	public static GameObject GetFirstChild(this MonoBehaviour mb){
		if(mb.GetChildCount() == 0){
			return null;
		}
		return mb.gameObject.transform.GetChild(0).gameObject;
	}

	public static GameObject GetLastChild(this MonoBehaviour mb){
		if(mb.GetChildCount() == 0){
			return null;
		}
		return mb.gameObject.transform.GetChild(mb.GetChildCount()-1).gameObject;
	}
	
	public static List<T> GetAllChildrenComponentsRecursive<T>(this MonoBehaviour mb){
		return mb.gameObject.GetAllChildrenComponentsRecursive<T>();
	}

	public static List<T> GetAllChildrenComponents<T>(this MonoBehaviour mb){
		return mb.gameObject.GetAllChildrenComponents<T>();
	}
	
	public static bool HasParent(this MonoBehaviour mb){
		return mb.gameObject.transform.parent != null;
	}
	
	public static T GetParentComponent<T>(this MonoBehaviour mb) where T : Component{
		if(!mb.HasParent()){
			return default(T);
		}
		Transform parent = mb.GetParent();
		T t = parent.gameObject.GetComponent<T>();
		return t;
	}
	
	public static Transform GetParent(this MonoBehaviour mb){
		return mb.gameObject.transform.parent;
	}
	

	public static void SetParent(this MonoBehaviour mb, RectTransform parent) {
		mb.gameObject.transform.SetParent(parent, false);
    }

	public static void SetParent(this MonoBehaviour mb, Transform parent){
		mb.gameObject.transform.SetParent(parent);
	}
	public static void SetParent(this MonoBehaviour mb, MonoBehaviour parent){
		mb.gameObject.transform.SetParent(parent.gameObject.transform);
	}
	public static void SetParent(this MonoBehaviour mb, GameObject parent){
		mb.gameObject.transform.SetParent(parent.transform);
	}
	public static void RemoveParent(this MonoBehaviour mb){
		mb.gameObject.transform.SetParent(null);
	}
	
	public static List<GameObject> GetAllChildren(this MonoBehaviour mb){
		return mb.gameObject.GetAllChildren();
	}
	
	public static T GetClosestParentComponent<T>(this MonoBehaviour mb) where T : Component{
		return mb.transform.GetClosestParentComponent<T>();
		/*if(!mb.HasParent()){
			return default(T);
		}
		Transform activeObject = mb.transform;
		
		T t = activeObject.GetComponentInParent<T>();
		


		while(activeObject != null && activeObject.HasParent()){
			T t = activeObject.GetParentComponent<T>();
			if(t == null){
				activeObject = mb.GetParent();
			}else{
				return t;
			}
		}
		
		return default(T);*/
	}

}













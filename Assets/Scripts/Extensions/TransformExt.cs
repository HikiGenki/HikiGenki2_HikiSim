using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExt{
	
	public static void CopyTo(this Transform source, Transform to){
		to.position = source.position.Clone();
		to.localEulerAngles = source.localEulerAngles.Clone();
		to.localPosition = source.localPosition.Clone();
		to.localRotation = source.localRotation.Clone();
		to.localScale = source.localScale.Clone();
		to.rotation = source.rotation.Clone();
	}
	
	public static bool HasParent(this Transform t){
		return t.parent != null;
	}
	
	public static List<GameObject> GetAllChildren(this Transform t){
		return t.gameObject.GetAllChildren();
	}

	public static T GetClosestParentComponent<T>(this Transform transform) where T : Component {
		if (!transform.HasParent()) {
			return default(T);
		}
		Transform activeObject = transform;

		T t = activeObject.GetComponentInParent<T>();

		if(t == null) {
			return activeObject.parent.GetClosestParentComponent<T>();
        } else {
			return t;
        }

	}

	public static T GetParentComponent<T>(this Transform mb) where T : Component{
		if(!mb.HasParent()){
			return default(T);
		}
		Transform parent = mb.parent;
		T t = parent.gameObject.GetComponent<T>();
		return t;
	}

	public static void BringToFront(this Transform t) {
		t.SetAsLastSibling();
    }

	public static void MaximizeRectTransform(this Transform t) {
		RectTransform rt = t.gameObject.GetComponent<RectTransform>();
		if (!rt) {
			rt = t.gameObject.AddComponent<RectTransform>();
		}
		rt.Maximize();
	}
	public static void Maximize(this RectTransform rt) {
		rt.ClearOffsets();
		rt.ClearPosition();

		rt.anchorMin = Vector2.zero;
		rt.anchorMax = Vector2.one;
	}
	public static void ClearOffsets(this RectTransform rt) {
		rt.offsetMin = Vector2.zero;
		rt.offsetMax = Vector2.zero;
    }
	public static void ClearPosition(this RectTransform rt) {
		rt.anchoredPosition = Vector2.zero;
    }
	public static void SetAnchors(this RectTransform rt, float xMin, float xMax, float yMin, float yMax) {
		rt.anchorMin = new Vector2(xMin, yMin);
		rt.anchorMax = new Vector2(xMax, yMax);
    }
	public static void SetAnchorX(this RectTransform rt, float xMin, float xMax) {
		rt.anchorMin = new Vector2(xMin, rt.anchorMin.y);
		rt.anchorMax = new Vector2(xMax, rt.anchorMax.y);
    }
	public static void SetAnchorY(this RectTransform rt, float yMin, float yMax) {
		rt.anchorMin = new Vector2(rt.anchorMin.x, yMin);
		rt.anchorMax = new Vector2(rt.anchorMax.x, yMax);
    }
}
		

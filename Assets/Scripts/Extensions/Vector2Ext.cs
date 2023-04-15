using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector2Ext{
	
	public static Vector2 Clone(this Vector2 vec){
		return new Vector2(vec.x, vec.y);
	}
	
	public static Vector2 Reverse(this Vector2 vec){
		return new Vector2(-vec.x, -vec.y);
	}
	
	public static Vector2 Rotate(this Vector2 vec, float degrees){
		return Quaternion.Euler(0, 0, degrees) * vec;
	}

	public static Vector2Int Rotate(this Vector2Int vec, float degrees) {
		Vector2 v = new Vector2(vec.x, vec.y).Rotate(degrees);
		return new Vector2Int(Mathf.RoundToInt(v.x),Mathf.RoundToInt(v.y));
    }

	public static RaycastHit2D Raycast(this Vector2 origin, Vector2 direction, params Collider2D[] ignoredColliders) {
		RaycastHit2D[] hits = Physics2D.RaycastAll(origin, direction);
		for (int i = 0; i < hits.Length; i++) {
			bool ignore = false;
			for(int j = 0; j < ignoredColliders.Length; j++) {
				if (hits[i].collider == ignoredColliders[j]) {
					ignore = true;
					break;
				}
            }
			if (ignore) {
				continue;
			}
			return hits[i];
		}
		return new RaycastHit2D();
	}
}

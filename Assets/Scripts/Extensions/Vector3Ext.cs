using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector3Ext{
	
	public static Vector3 Clone(this Vector3 vec){
		return new Vector3(vec.x, vec.y, vec.z);
	}
	
	public static Vector3 Reverse(this Vector3 vec){
		return new Vector3(-vec.x, -vec.y, -vec.z);
	}
	
	
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class QuaternionExt{
	
	public static Quaternion Clone(this Quaternion q){
		Quaternion clone = new Quaternion(q.x, q.y, q.z, q.w);
		clone.eulerAngles = q.eulerAngles.Clone();
		return clone;
	}
	
}

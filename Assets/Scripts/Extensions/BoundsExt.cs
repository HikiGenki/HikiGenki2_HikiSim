using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BoundsExt{
    
	public static Bounds Clone(this Bounds bounds){
		Bounds clone = new Bounds(bounds.center.Clone(), bounds.size.Clone());
		clone.extents = bounds.extents.Clone();
		clone.min = bounds.min.Clone();
		clone.max = bounds.max.Clone();
		return clone;
	}

}

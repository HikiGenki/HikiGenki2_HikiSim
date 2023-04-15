using UnityEngine;

public static class SpriteRendererExt{
    
	public static bool SpriteOverlaps(this SpriteRenderer sr, SpriteRenderer overlapper){
		Rect r = sr.GetSpriteRect();
		Rect r2 = overlapper.GetSpriteRect();
		
		return r.Overlaps(r2);
	}
	
	public static Rect GetSpriteRect(this SpriteRenderer sr){
		Rect r = new Rect();
		Sprite s = sr.sprite;
		
		r.x = sr.gameObject.transform.position.x - s.bounds.extents.x;
		r.y = sr.gameObject.transform.position.y - s.bounds.extents.y;
		
		r.width = s.bounds.extents.x * 2;
		r.height = s.bounds.extents.y * 2;
		return r;
	}


}

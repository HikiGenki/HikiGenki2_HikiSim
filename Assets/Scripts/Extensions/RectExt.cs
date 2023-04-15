using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RectExt{
	
	/**
		Direction should be a constant from Vector2, left, right, down, up
	*/
	public static Rect Extend(this Rect rect, Vector2 direction){
		
		if(direction == Vector2.left){
			return new Rect(rect.x-1, rect.y, rect.width + 1, rect.height);
		}else if(direction == Vector2.right){
			return new Rect(rect.x, rect.y, rect.width + 1, rect.height);
		}else if(direction == Vector2.up){
			return new Rect(rect.x, rect.y, rect.width, rect.height + 1);
		}else if(direction == Vector2.down){
			return new Rect(rect.x, rect.y-1, rect.width, rect.height+1);
		}
		
		return rect;
	}

	public static RectInt Move(this RectInt rect, int dx, int dy) {

		return new RectInt(rect.x + dx, rect.y + dy, rect.width, rect.height);
    }

	public static RectInt Extend(this RectInt rect, Vector2 direction) {

		if(direction == Vector2.left) {
			return new RectInt(rect.x - 1, rect.y, rect.width + 1, rect.height);
		} else if(direction == Vector2.right) {
			return new RectInt(rect.x, rect.y, rect.width + 1, rect.height);
		} else if(direction == Vector2.up) {
			return new RectInt(rect.x, rect.y, rect.width, rect.height + 1);
		} else if(direction == Vector2.down) {
			return new RectInt(rect.x, rect.y - 1, rect.width, rect.height + 1);
		}

		return rect;
	}

	public static Rect Grow(this Rect rect) {
		Vector2[] dirs = { Vector2.left, Vector2.right, Vector2.up, Vector2.down };

		Rect result = rect;
		foreach(Vector2 dir in dirs) {
			result = result.Extend(dir);
        }
		return result;
    }

	public static Rect Copy(this Rect rect) {
		return new Rect(rect.x, rect.y, rect.width, rect.height);
    }

	public static Rect Grow(this Rect rect, int times) {
		
		Rect result = rect;
		for(int i = 0; i < times; i++) {
			result = result.Grow();
        }
		return result;
    }

	public static RectInt Grow(this RectInt rect, int times) {

		RectInt result = rect;
		for(int i = 0; i < times; i++) {
			result = result.Grow();
		}
		return result;
	}
	public static RectInt Grow(this RectInt rect) {
		Vector2[] dirs = { Vector2.left, Vector2.right, Vector2.up, Vector2.down };

		RectInt result = rect;
		foreach(Vector2 dir in dirs) {
			result = result.Extend(dir);
		}
		return result;
	}

	public static Rect ShrinkIn(this Rect rect){
		float x = rect.x + 1;
		float y = rect.y + 1;
		float width = rect.width - 2;
		float height = rect.height - 2;
		
		return new Rect(x,y,width,height);
	}

	public static Rect ShrinkBy(this Rect rect, float amount) {
		float x = rect.x + amount;
		float y = rect.y + amount;
		float width = rect.width - (amount * 2);
		float height = rect.height - (amount * 2);
		return new Rect(x, y, width, height);
    }

	public static Rect ShrinkIn(this Rect rect, int times) {
		Rect result = rect;
		for(int i = 0; i < times; i++) {
			result = result.ShrinkIn();
        }
		return result;
    }

	public static bool ContainsInt(this Rect rect, int x, int y) {
		return x >= rect.x && y >= rect.y && x < rect.x + rect.width && y < rect.y + rect.height; 
    }
	public static bool Contains(this RectInt rect, int x, int y) {
		return x >= rect.x && y >= rect.y && x < rect.x + rect.width && y < rect.y + rect.height; 
    }
	
	public static Vector2 RandomPoint(this Rect rect){
		float x = Random.Range(rect.x, rect.x + rect.width-1);
		float y = Random.Range(rect.y, rect.y + rect.height-1);
		return new Vector2(x,y);
	}
	
	public static Vector2Int RandomIntPoint(this Rect rect){
		Vector2 r = rect.RandomPoint();
		return new Vector2Int((int)r.x, (int)r.y);
	}
}
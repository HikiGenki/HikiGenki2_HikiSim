using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class SpriteExt{
	
	public static Texture2D AsTexture(this Sprite sprite){
		if(sprite.texture.isReadable) {
			Texture2D croppedTexture = new Texture2D(sprite.GetWidth(),
													 sprite.GetHeight()
													);
			
			Color[] pixels = sprite.texture.GetPixels((int)sprite.rect.x,
														(int)sprite.rect.y,
														(int)sprite.GetWidth(),
														(int)sprite.GetHeight()
													 );
			croppedTexture.SetPixels(pixels);
			croppedTexture.Apply();
			return croppedTexture;
		}else{
			Debug.LogWarning("Texture is not readable.");
		}
		
		return null;
	}
	
	public static Sprite Fuse(this Sprite s, Sprite s2){
		Texture2D mainTex = new Texture2D(Mathf.Max(s.GetWidth(),s2.GetWidth()), Math.Max(s.GetHeight(), s2.GetHeight()));
		s.DrawToTexture(mainTex, Vector2Int.zero);
		s2.DrawToTexture(mainTex, Vector2Int.zero);
		mainTex.Apply();
		return mainTex.AsSprite();
	}
	
	public static void DrawToTexture(this Sprite s, Texture2D tex){
		s.DrawToTexture(tex, Vector2Int.zero);
	}
	
	public static void DrawToTexture(this Sprite s, Texture2D tex, Vector2Int offset){
		int xOffset = offset.x;
		int yOffset = offset.y;
		for(int x = 0; x < s.GetWidth(); x++){
			for(int y = 0; y < s.GetHeight(); y++){
				Color pixel = s.GetPixel(x,y);
				if(pixel.a > 0.0f){
					tex.SetPixel(x+xOffset,y+yOffset,s.GetPixel(x,y));
				}
			}
		}
	}
	
	public static int GetWidth(this Sprite s){
		return (int)s.rect.width;
	}
	
	public static int GetHeight(this Sprite s){
		return (int)s.rect.height;
	}
	
	public static Color GetPixel(this Sprite s, int x, int y){
		return s.texture.GetPixel((int)s.rect.x + x, (int)s.rect.y + y);
	}

}

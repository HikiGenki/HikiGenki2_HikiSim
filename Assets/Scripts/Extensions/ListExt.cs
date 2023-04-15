using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListExt {

	public static void ResizeDestroy<T>(this List<T> list, int newSize) where T : MonoBehaviour{
		if(newSize > list.Count) {
			throw new System.Exception("ResizeDestroy can only be called with a size equal or smaller than the list. To increase size use list.ResizeInstantiate(newSize, prefab, parent)");
        }else if(newSize == list.Count) {
			return;
        }
		List<T> toDestroy = new List<T>();
		toDestroy.AddRange(list.GetFromEnd(list.Count - newSize));
		list.Resize(newSize);

		foreach(T t in toDestroy) {
			GameObject.Destroy(t.gameObject);
        }
    }

	public static void ResizeInstantiate<T>(this List<T> list, int newSize, T prefab, GameObject parent) where T : MonoBehaviour {
		if(newSize < list.Count) {
			throw new System.Exception("ResizeInstantiate can only be called with a size equal or greater than the list. To decrease size using list.ResizeDestroy(newSize) or list.Resize(newSize)");
        }else if(newSize == list.Count) {
			return;
        }
		int newItemsToAdd = newSize - list.Count;
		for (int i = 0; i < newItemsToAdd; i++) {
			T instance = parent.AddInstanceAsChild(prefab);
			list.Add(instance);
		}
    }


	/**
	 * Returns true if successful activating an object
	 */
	public static bool ActivateNext(this List<GameObject> list) {
		int activeIndex = list.FirstActiveIndex();

		if(activeIndex != -1) {
			list[activeIndex].SetActive(false);

			if(activeIndex < list.Count - 1) {
				activeIndex++;
			} else if(activeIndex == list.Count - 1) {
				activeIndex = 0;
			}

			list[activeIndex].SetActive(true);
			return true;
        }
		return false;
	}

	/**
	 * Returns true if successful actibating an object
	 */
	public static bool ActivatePrevious(this List<GameObject> list) {
		int activeIndex = list.FirstActiveIndex();

		if(activeIndex != -1) {
			list[activeIndex].SetActive(false);

			if(activeIndex == 0) {
				activeIndex = list.Count - 1;
            } else {
				activeIndex--;
            }

			list[activeIndex].SetActive(true);
			return true;
        }
		return false;
    }

	/**
	 * Returns index of first GameObject in the list that is active using activeSelf. If no objects are active -1 is returned
	 */
	public static int FirstActiveIndex(this List<GameObject> list) {
		
		for(int i = 0; i < list.Count; i++) {
			if(list[i].activeSelf) {
				return i;
			}
		}
		return -1;
	}

	public static void DeactivateAll(this List<GameObject> list) {
		list.ForEach((go) => go.SetActive(false));
    }

	public static void ActivateAll(this List<GameObject> list) {
		list.ForEach((go) => go.SetActive(true));
    }

	public static List<T> NonNull<T>(this List<T> list) where T : class {
		List<T> nList = new List<T>(list);
		nList.RemoveAll(n => n == null);
		return nList;
    }

	public static void Resize<T>(this List<T> list, int newCount) {
		if(newCount <= 0) {
			list.Clear();
		} else {
			while(list.Count > newCount) {
				list.RemoveAt(list.Count - 1);
			}
			while(list.Count < newCount) {
				list.Add(default(T));
			}
		}
	}

	public static bool ValidIndex<T>(this List<T> list, int index) {
		return index >= 0 && index < list.Count;
	}

	public static void RemoveFromEnd<T>(this List<T> list, int amount) {
		if(amount > list.Count) {
			list.Clear();
		}
		for(int i = 0; i < amount; i++) {
			list.RemoveAt(list.Count - 1);
		}
	}

	public static List<T> GetFromEnd<T>(this List<T> list, int amount) {
		List<T> res = new List<T>();
		if(list.Count <= amount) {
			res.AddRange(list);
			return res;
		} else {

			for(int i = list.Count - 1, j = 0; i >= 0 && j < amount; i--, j++) {
				res.Add(list[i]);
			}
		}
		res.Reverse();
		return res;
	}

	public static string AsString(this List<string> list, string separator = "") {
		string res = "";
		list.ForEach(s => res += s + separator);
		return res;
    }

	public static List<T> Random<T>(this List<T> list, int amount) {
		List<T> res = new List<T>();
		if(list.Count > 0) {
			for(int i = 0; i < amount; i++) {
				res.Add(list.Random());
			}
		}
		return res;
    }

	/**
	 * Random entry between two indexes; maxIndex is exclusive
	 */
	public static T Random<T>(this List<T> list, int minIndex, int maxIndex) {
		int index = UnityEngine.Random.Range(minIndex, maxIndex);
		return list[index];
		
    }

	
	public static T Random<T>(this List<T> list) {
		if(list.Count == 0) {
			return default(T);
		} else if(list.Count == 1) {
			return list[0];
		}
		return list[UnityEngine.Random.Range(0, list.Count)];

	}

	public static T Last<T>(this List<T> list) {
		if(list.Count < 1) {
			return default(T);
		}
		return list[list.Count - 1];
	}

	public static List<T> Last<T>(this List<T> list, int amount) {
		return list.GetFromEnd(amount);
    }

	public static List<T> Shuffle<T>(this List<T> list) {
		for(int i = 0; i < list.Count; i++) {

			int r = UnityEngine.Random.Range(0, list.Count);

			T value = list[r];
			list[r] = list[0];
			list[0] = value;
		}
		return list;
	}

	public static List<T> GetFirst<T>(this List<T> list, int amount = 1) {
		List<T> l = new List<T>();
		for(int i = 0; i < amount && i < list.Count; i++) {
			l.Add(list[i]);
		}
		return l;
	}

	public static int LastIndex<T>(this List<T> list) {
		return list.Count - 1;
    }

	/**
		Gets middle of List; if no elements returns null; if even number of elements returns lower middle

	*/
	public static T Middle<T>(this List<T> list){
		if(list.Count == 0){
			return default(T);
		}
		return list.Half(true).Last();
	}

	public static List<T> Half<T>(this List<T> list, bool ceil = true) {
		List<T> halfList = new List<T>();
		if(list.Count < 2) {
			halfList.AddRange(list);
        } else {
			float count = list.Count;
			count *= 0.5f;
			int targetCount = (int)count;
            if(ceil) {
				targetCount = Mathf.CeilToInt(count);
            }
			if(targetCount >= list.Count) {
				halfList.AddRange(list);
            } else {
				halfList.AddRange(list.GetFirst(targetCount));
            }
        }
		return halfList;
    }

	public static List<T> Shuffle<T>(this List<T> list, int times) {
		for(int i = 0; i < times; i++) {
			list.Shuffle();
		}
		return list;
	}

	public static List<T> Copy<T>(this List<T> list) {
		return new List<T>(list);
    }

	public static void Swap<T>(this List<T> list, int index1, int index2) {
		T temp = list[index1];
		list[index1] = list[index2];
		list[index2] = temp;
    }
	
	public static List<K> AsComponents<T, K>(this List<T> list) where T : MonoBehaviour where K : MonoBehaviour{
		List<K> components = new List<K>();
		foreach(T mb in list) {
			K k = mb.GetComponent<K>();
            if (k) {
				components.Add(k);
            }
        }
		return components;

    }

}
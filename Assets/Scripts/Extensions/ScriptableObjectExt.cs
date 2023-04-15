using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class ScriptableObjectExt{

#if UNITY_EDITOR
    public static void AddSubAsset(this ScriptableObject asset, ScriptableObject objectToAdd) {
        AssetDatabase.AddObjectToAsset(objectToAdd, asset);
        asset.Dirty();
        AssetDatabase.SaveAssets();
        objectToAdd.Import();
        
    }

    public static void ForceSaveChanges(this ScriptableObject asset) {
        asset.Dirty();
        AssetDatabase.SaveAssetIfDirty(asset);
        asset.Import();
    }

    public static void DeleteAsset(this ScriptableObject asset) {
        AssetDatabase.DeleteAsset(asset.GetPath());
    }


    public static void Import(this ScriptableObject asset) {
        AssetDatabase.ImportAsset(asset.GetPath());
    }

    public static void Dirty(this ScriptableObject asset) {
        EditorUtility.SetDirty(asset);
    }

    public static string GetDirectoryPath(this ScriptableObject asset) {
        string path = asset.GetPath();
        return path.Substring(0, path.LastIndexOf("/")+1);
    }

    public static string GetPath(this ScriptableObject asset) {
        return AssetDatabase.GetAssetPath(asset);
    }

    public static void SaveAsAsset(this ScriptableObject asset, string path) {
        AssetDatabase.CreateAsset(asset, path);
    }

    public static void SaveChanges(this ScriptableObject asset){
        asset.Dirty();
        AssetDatabase.SaveAssets();
        asset.Import();
    }
#endif
}

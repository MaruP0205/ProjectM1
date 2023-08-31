using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.IO;
public static class BYDataTableMaker
{
    [MenuItem("Assets/BY/Create Binary Config(TAB Delimited)",false,1)]
    public static void CreateBinaryFileConfig()
    {
        foreach (UnityEngine.Object obj in Selection.objects)
        {
            TextAsset texFile = (TextAsset)obj;
            string nameFile = Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(texFile));
            ScriptableObject scriptAble = ScriptableObject.CreateInstance(nameFile);
            AssetDatabase.CreateAsset(scriptAble, "Assets/Resources/Config/" + nameFile + ".asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            BYBaseDataTable byBaseData = (BYBaseDataTable)scriptAble;
            byBaseData.CreateData(texFile);
            EditorUtility.SetDirty(byBaseData);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System;

public class CreateQuestEditor : MonoBehaviour
{
    [MenuItem("Assets/Create Quest Behaviours Script")]
    public static void CreateQuestBehaviourScript(MenuCommand cmd)
    {


        ConfigQuest configQuest = Resources.Load("Config/ConfigQuest", typeof(ScriptableObject)) as ConfigQuest;
        foreach (ConfigQuestRecord e in configQuest.GetAllRecords())
        {
            string name = "Quest_" + e.ID.ToString();
            string classParent = "QuestItemControl";

            string copyPath = "Assets/Script/Quest/" + name + ".cs";
            Debug.Log("Creating ClassFire: " + copyPath);


            if (File.Exists(copyPath) == false)
            { // do not overwrite
                using (StreamWriter outfile =
                    new StreamWriter(copyPath))
                {
                    outfile.WriteLine("using UnityEngine;");
                    outfile.WriteLine("using System.Collections;");
                    outfile.WriteLine("");
                    outfile.WriteLine("public class " + name + " : " + classParent + " {");
                    outfile.WriteLine("     public override void Setup(ConfigQuestRecord configQuest)");
                    outfile.WriteLine("     {");
                    outfile.WriteLine("         base.Setup(configQuest);");
                    outfile.WriteLine("     }");
                    outfile.WriteLine("     public override void LogQuest(QuestLogData logData)");
                    outfile.WriteLine("     {");
                    outfile.WriteLine("         base.LogQuest(logData);");
                    outfile.WriteLine("     }");
                    outfile.WriteLine("     public override void CheckQuest()");
                    outfile.WriteLine("     {");
                    outfile.WriteLine("         base.CheckQuest();");
                    outfile.WriteLine("     }");
                    outfile.WriteLine("     void Start()");
                    outfile.WriteLine("     {");
                    outfile.WriteLine("     }");
                    outfile.WriteLine("     void Update()");
                    outfile.WriteLine("     {");
                    outfile.WriteLine("     }");

                    outfile.WriteLine("}");
                }//File written
            }

        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
    [MenuItem("Assets/Create Quest Behaviours Prefab")]
    public static void CreateCardBehaviourPrefab(MenuCommand cmd)
    {
        ConfigQuest configQuest = Resources.Load("Config/ConfigQuest", typeof(ScriptableObject)) as ConfigQuest;
        foreach (ConfigQuestRecord e in configQuest.GetAllRecords())
        {
            string name = "Quest_" + e.ID.ToString();
            Debug.LogError(name);
            string localPath = "Assets/Resources/Quest/" + name + ".prefab";
            if (File.Exists(localPath) == false)
            {
                GameObject obj = new GameObject();
                Type com = Type.GetType(name);
                obj.AddComponent(com);
                // Create the new Prefab.

                PrefabUtility.SaveAsPrefabAsset(obj, localPath);
            }


        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();


    }
}


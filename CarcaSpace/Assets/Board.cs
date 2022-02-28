using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Board : MonoBehaviour
{
    /*
    SerializedObject tagManager = new SerializedObject(AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/TagManager.asset")[0]);
    SerializedProperty tagsProp = tagManager.FindProperty("tags");
 
    // For Unity 5 we need this too
    SerializedProperty layersProp = tagManager.FindProperty("layers");
 
    // Adding a Tag
    string s = "Plateau";
 
    // First check if it is not already present
    bool found = false;
    for(int i = 0;i < tagsProp.arraySize;i++)
    {
        SerializedProperty t = tagsProp.GetArrayElementAtIndex(i);
        if (t.stringValue.Equals(s)) { found = true; break; }
    }
 
    // if not found, add it
    if (!found)
    {
        tagsProp.InsertArrayElementAtIndex(0);
        SerializedProperty n = tagsProp.GetArrayElementAtIndex(0);
        n.stringValue = s;
    }
    */

    //List<GameObject> board = new List<GameObject>();
    // Start is called before the first frame update

    //ajouter le tag "Plateau" a tout les elements de la grid qui deviennent invisibles
    //il faudra creer le tag avant dans project settings

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}

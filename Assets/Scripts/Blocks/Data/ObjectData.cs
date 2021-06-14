using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ObjectData 
{
    public int id;
    public int parent;
    public string name;
    public string model = "";
    public string texture = "";
    public string color = "";
    public Vector3 position;
    public Quaternion rotation;
    public Vector3 scale;
    public int isVisible;
    public bool isInteractable;
    public PhysicsData physics = new PhysicsData();
    public TextData text = new TextData();
    public CodeData code = new CodeData();
    public string tag = "";
}

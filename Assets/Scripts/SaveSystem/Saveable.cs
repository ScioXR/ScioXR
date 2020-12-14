using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saveable : MonoBehaviour
{
    public int id;
    public static int lastUniqueId = 1;
    public string model;
    public bool shouldSave = true;

    public CodeData codeData;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateUniqueId()
    {
        id = lastUniqueId++;
    }

}

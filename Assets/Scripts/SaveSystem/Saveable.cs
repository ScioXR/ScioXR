using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saveable : MonoBehaviour
{
    public int id;
    public static int lastUniqueId = 1;
    public string model;
    public string texture;
    public string color = "FFFFFF";
    public bool isInteractable;

    public bool shouldSave = true;

    public CodeData codeData;

    public void GenerateUniqueId()
    {
        id = lastUniqueId++;
    }

    public void SetTexture(string textureName, Texture2D textureAsset)
    {
        texture = textureName;
        GetComponent<MeshRenderer>().material.mainTexture = textureAsset;
    }

    public void SetColor(Color color)
    {
        this.color = ColorUtility.ToHtmlStringRGBA(color);
        GetComponent<MeshRenderer>().material.color = color;
    }

}

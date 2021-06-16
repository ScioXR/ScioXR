using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Saveable : MonoBehaviour
{
    public static int lastUniqueId = 1;

    public ObjectData data = new ObjectData();

    public bool shouldSave = true;

    private TextMeshPro textModel;

    private static float fontScaleFactor = 33.0f;

    public void GenerateUniqueId()
    {
        data.id = lastUniqueId++;
    }

    public void SetupGraphics()
    {
        Color color = Color.white;
        bool colorParse = ColorUtility.TryParseHtmlString("#" + data.color, out color);
        GetComponent<MeshRenderer>().material.color = color;
        if (data.texture != "")
        {
            StartCoroutine(AssetsLoader.ImportMaterial(data.texture, texture =>
            {
                GetComponent<MeshRenderer>().material.mainTexture = texture;
            }));
        }

        UpdateText();

        if (GetComponent<Rigidbody>())
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.mass = data.physics.mass;
            rb.drag = data.physics.drag;
            rb.angularDrag = data.physics.angularDrag;
        }
    }

    public void UpdateText()
    {
        if (data.text.text != "")
        {
            if (textModel == null)
            {
                //Create text object
                GameObject textObject = new GameObject("Text");
                textObject.transform.parent = gameObject.transform;
                textObject.transform.localPosition = new Vector3(0, 0.1f, 0);
                textObject.transform.localEulerAngles = new Vector3(90, 0, 0);
                textModel = textObject.AddComponent<TextMeshPro>();
                textModel.horizontalAlignment = HorizontalAlignmentOptions.Center;
                textModel.verticalAlignment = VerticalAlignmentOptions.Middle;

                Bounds bounds = gameObject.GetComponent<MeshRenderer>().bounds;
                textObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, bounds.size.y);
                textObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, bounds.size.x);
            }
            textModel.text = data.text.text;
           // textModel.fontSize = data.text.size / fontScaleFactor;
            //auto size text
            textModel.enableAutoSizing = true;
            textModel.fontSizeMin = 0.2f;
            textModel.fontSizeMax = data.text.size / fontScaleFactor;
            Color textColor = Color.white;
            ColorUtility.TryParseHtmlString("#" + data.text.color, out textColor);
            textModel.color = textColor;

        } else
        {
            if (textModel)
            {
                Destroy(textModel.gameObject);
                textModel = null;
            }
        }        
    }

    public void SetTexture(string textureName, Texture2D textureAsset)
    {
        data.texture = textureName;
        GetComponent<MeshRenderer>().material.mainTexture = textureAsset;
    }

    public void ClearTexture()
    {
        data.texture = "";
        GetComponent<MeshRenderer>().material.mainTexture = null;
    }

    public void SetColor(Color color)
    {
        data.color = ColorUtility.ToHtmlStringRGBA(color);
        GetComponent<MeshRenderer>().material.color = color;
    }

    public void SetTag(Text tag)
    {
        data.tag = tag.text;
    }

    public ObjectData GetSavableData()
    {
        data.parent = gameObject.transform.parent ? gameObject.transform.parent.gameObject.GetComponent<Saveable>().data.id : 0;
        data.name = gameObject.name;
        data.position = transform.position;
        data.rotation = transform.rotation;
        data.scale = transform.localScale;
        data.isVisible = gameObject.activeInHierarchy ? 1 : 0;

        return data;
    }
}

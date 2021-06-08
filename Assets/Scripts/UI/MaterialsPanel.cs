using HSVPicker;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MaterialsPanel : XRPanel
{
    public TextMeshProUGUI loadingText;
    public GameObject textureCardPrefab;
    public TextMeshProUGUI pageText;

    public ColorPicker colorPicker;

    public Transform cardStartPosition;

    public float spaceBetweenCardsX = 0.12f;
    public float spaceBetweenCardsY = 0.12f;
    public Vector3 modelOffset;
    // float cardWidth = 0.1f;
    public int texturesInRow = 4;
    public int texturesPerPage = 12;

    List<GameObject> textures = new List<GameObject>();

    bool isListCreated;

    int currentPage = 0;

    public override void Show()
    {
        base.Show();

        if (!isListCreated)
        {
            Color color = Color.white;
            ColorUtility.TryParseHtmlString("#" + EditorManager.instance.selectedObject.GetComponent<Saveable>().data.color, out color);
            colorPicker.CurrentColor = color;

            PopulateMaterialTextures();
            isListCreated = true;
        }
    }

    private void PopulateMaterialTextures()
    {
        StartCoroutine(AssetsLoader.GetTexturesList(result => {
            loadingText.gameObject.SetActive(false);
            for (int i = 0; i < result.Count; i++)
            {
                string textureName = result[i];
                int gridX = i % texturesInRow;
                int gridY = (i % texturesPerPage) / texturesInRow;

                GameObject textureCard = Instantiate(textureCardPrefab, transform);
                textureCard.name = textureName;
                textureCard.GetComponentInChildren<TextMeshProUGUI>().text = textureName;
                textureCard.transform.localPosition = new Vector3(cardStartPosition.transform.localPosition.x + gridX * spaceBetweenCardsX, cardStartPosition.transform.localPosition.y - gridY * spaceBetweenCardsY, cardStartPosition.transform.localPosition.z);
                textures.Add(textureCard);

                //Debug.Log("Card: " + textureName + ", " + gridX + ", " + gridY);

                StartCoroutine(AssetsLoader.ImportMaterial(textureName, importedObject =>
                {
                    Texture2D tex = importedObject;
                    

                    Sprite image = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);

                    textureCard.GetComponent<Image>().sprite = image;

                    textureCard.GetComponent<Button>().onClick.AddListener(delegate { SelectMaterial(textureName, tex); });
                }));
            }
            SetPage(0);
        }));
    }

    private void SelectMaterial(string textureName, Texture2D tex)
    {
        EditorManager.instance.selectedObject.GetComponent<Saveable>().SetTexture(textureName, tex);
    }

    public void SelectColor(Color color)
    {
        EditorManager.instance.selectedObject.GetComponent<Saveable>().SetColor(color);
    }

    public void SetPage(int pageIndex)
    {
        int totalPages = textures.Count / texturesPerPage + 1;
        if (totalPages <= pageIndex)
        {
            pageIndex -= totalPages;
        }
        else if (pageIndex < 0)
        {
            pageIndex += totalPages;
        }

        currentPage = pageIndex;
        for (int i = 0; i < textures.Count; i++)
        {
            bool modelVisible = i >= texturesPerPage * currentPage && i < texturesPerPage * (currentPage + 1);
            textures[i].SetActive(modelVisible);
        }
        pageText.text = "" + (currentPage + 1) + "/" + totalPages;
    }

    public void PrevPage()
    {
        SetPage(currentPage - 1);
    }

    public void NextPage()
    {
        SetPage(currentPage + 1);
    }
}

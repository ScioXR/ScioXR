using HSVPicker;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnvironmentPanel : XRPanel
{
    public Renderer[] rend;
    public Material[] materials;

    public void ChangeEnvironmentColor (int index)
    {
        for (int i = 0; i < rend.Length; i++)
        {
            rend[i].GetComponent<MeshRenderer>().material = materials[index];
        }
    }


    public GameObject environmentCardPrefab;

    public TextMeshProUGUI loadingText;
    public TextMeshProUGUI pageText;

    public ColorPicker colorPicker;

    public Transform cardStartPosition;

    public float spaceBetweenCardsX = 0.12f;
    public float spaceBetweenCardsY = 0.12f;
    public int environmentsInRow = 4;
    public int environmentsPerPage = 12;

    List<GameObject> environments = new List<GameObject>();

    bool isListCreated;

    int currentPage = 0;

    public override void Show()
    {
        base.Show();

        if (!isListCreated)
        {
            /*Color color = Color.white;
            ColorUtility.TryParseHtmlString("#" + EditorManager.instance.selectedObject.GetComponent<Saveable>().color, out color);
            colorPicker.CurrentColor = color;*/

            PopulateEnvironmentCards();
            isListCreated = true;
        }
    }

    private void PopulateEnvironmentCards()
    {
        StartCoroutine(AssetsLoader.GetEnvironmentList(result => {
            loadingText.gameObject.SetActive(false);

            //add default card
            GameObject environmentCard = Instantiate(environmentCardPrefab, transform);
            environmentCard.name = "Default";
            environmentCard.GetComponentInChildren<TextMeshProUGUI>().text = "Default";
            environmentCard.transform.localPosition = new Vector3(cardStartPosition.transform.localPosition.x, cardStartPosition.transform.localPosition.y, cardStartPosition.transform.localPosition.z);
            environmentCard.GetComponent<Button>().onClick.AddListener(delegate { SelectEnvironment(""); });
            environments.Add(environmentCard);

            for (int i = 0; i < result.Count; i++)
            {
                string environmentName = result[i];
                int gridX = (i + 1) % environmentsInRow;
                int gridY = ((i + 1) % environmentsPerPage) / environmentsInRow;

                environmentCard = Instantiate(environmentCardPrefab, transform);
                environmentCard.name = environmentName;
                environmentCard.GetComponentInChildren<TextMeshProUGUI>().text = environmentName;
                environmentCard.transform.localPosition = new Vector3(cardStartPosition.transform.localPosition.x + gridX * spaceBetweenCardsX, cardStartPosition.transform.localPosition.y - gridY * spaceBetweenCardsY, cardStartPosition.transform.localPosition.z);
                environments.Add(environmentCard);

                //Debug.Log("Card: " + textureName + ", " + gridX + ", " + gridY);
                StartCoroutine(AssetsLoader.ImportEnvironmentThumbnail(environmentName, importedObject =>
                {
                    Texture2D tex = importedObject;

                    Sprite image = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);

                    environmentCard.GetComponent<Image>().sprite = image;

                    environmentCard.GetComponent<Button>().onClick.AddListener(delegate { SelectEnvironment(environmentName); });
                }));
            }
            SetPage(0);
        }));
    }

    public void SelectEnvironment(string environmentName)
    {
        ScioXRSceneManager.instance.SetEnvironment(environmentName);
    }

    public void SetPage(int pageIndex)
    {
        int totalPages = environments.Count / environmentsPerPage + 1;
        if (totalPages <= pageIndex)
        {
            pageIndex -= totalPages;
        }
        else if (pageIndex < 0)
        {
            pageIndex += totalPages;
        }

        currentPage = pageIndex;
        for (int i = 0; i < environments.Count; i++)
        {
            bool modelVisible = i >= environmentsPerPage * currentPage && i < environmentsPerPage * (currentPage + 1);
            environments[i].SetActive(modelVisible);
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

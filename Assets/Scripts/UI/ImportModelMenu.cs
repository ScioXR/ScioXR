using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ImportModelMenu : XRPanel
{
    public TextMeshProUGUI loadingText;
    public TextMeshProUGUI pageText;
    public Transform cardStartPosition;
    public GameObject modelCardPrefab;

    bool isListCreated;
    public float modelsMaxSize = 0.02f;
    public float spaceBetweenCardsX = 0.12f;
    public float spaceBetweenCardsY = 0.12f;
    public Vector3 modelOffset;
    // float cardWidth = 0.1f;
    public int modelsInRow = 4;
    public int modelsPerPage = 12;
    int currentPage = 0;
    List<GameObject> models = new List<GameObject>();
    public override void Show()
    {
        base.Show();

        if (!isListCreated)
        {
            CreateModelCards();
        }
    }


    public void CreateModelCards()
    {
        isListCreated = true;
        Vector3 position = cardStartPosition.transform.localPosition;

        StartCoroutine(AssetsLoader.GetModelsList(result => {
            loadingText.gameObject.SetActive(false);
            for (int i = 0; i < result.Count; i++)
            {
                string modelName = result[i];
                int gridX = i % modelsInRow;
                int gridY = (i % modelsPerPage) / modelsInRow;

                GameObject modelCard = Instantiate(modelCardPrefab, transform);
                modelCard.name = modelName;
                modelCard.GetComponentInChildren<TextMeshProUGUI>().text = modelName;
                modelCard.transform.localPosition = new Vector3(cardStartPosition.transform.localPosition.x + gridX * spaceBetweenCardsX, cardStartPosition.transform.localPosition.y - gridY * spaceBetweenCardsY, cardStartPosition.transform.localPosition.z);
                models.Add(modelCard);

                //Debug.Log("Card: " + modelName + ", " + gridX + ", " + gridY);

                StartCoroutine(AssetsLoader.ImportModel(modelName, importedObject =>
                {
                    GameObject model = importedObject;
                    model.transform.parent = modelCard.transform;
                    PrepareModel(model, modelName);

                    ModelSelecter modelSelecter = modelCard.AddComponent<ModelSelecter>();
                    modelSelecter.modelObject = model;

                    modelCard.GetComponent<Button>().onClick.AddListener(modelSelecter.CreateModel);

                    Vector3 size = Vector3.Scale(model.transform.localScale, model.GetComponentInChildren<MeshFilter>().mesh.bounds.size);
                    float modelSize = Math.Max(size.x, Math.Max(size.y, size.z));

                    if (modelSize > modelsMaxSize)
                    {
                        model.transform.localScale /= (modelSize / modelsMaxSize);
                    }

                    model.transform.localPosition = modelOffset;
                }));
            }
            SetPage(0);
        }));
    }

    public void SetPage(int pageIndex)
    {
        int totalPages = models.Count / modelsPerPage + 1;
        if (totalPages <= pageIndex)
        {
            pageIndex -= totalPages;
        } else if (pageIndex < 0)
        {
            pageIndex += totalPages;
        }

        currentPage = pageIndex;
        for (int i = 0; i < models.Count; i++)
        {
            bool modelVisible = i >= modelsPerPage * currentPage && i < modelsPerPage * (currentPage + 1);
            models[i].SetActive(modelVisible);
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

    public void PrepareModel(GameObject loadedModel, string modelName)
    {
        loadedModel.AddComponent<Saveable>();
        loadedModel.GetComponent<Saveable>().model = modelName;
        loadedModel.GetComponent<Saveable>().shouldSave = false;
    }

}

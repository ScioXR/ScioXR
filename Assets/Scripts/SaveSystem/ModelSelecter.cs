using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class ModelSelecter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool hovered;
    public GameObject modelObject;

    private void Update()
    {
        if (hovered)
        {
            modelObject.transform.Rotate(new Vector3(90, 90, 0) * Time.deltaTime * 0.5f);
        }
    }

    public void CreateModel()
    {
        //TODO better to just clone the current
        StartCoroutine(AssetsLoader.ImportModel(modelObject.GetComponent<Saveable>().model, loadedModel =>
        {
            loadedModel.transform.position = new Vector3(modelObject.transform.position.x, modelObject.transform.position.y, modelObject.transform.position.z + 1f);
            loadedModel.transform.localScale = loadedModel.transform.localScale / 10;
            PlatformLoader.instance.platform.SetupEditorObject(loadedModel, modelObject.GetComponent<Saveable>().model);
            loadedModel.AddComponent<Saveable>();
            loadedModel.GetComponent<Saveable>().model = modelObject.GetComponent<Saveable>().model;
            loadedModel.GetComponent<Saveable>().GenerateUniqueId();

            WebXRInteractor[] interactors = FindObjectsOfType<WebXRInteractor>();
            foreach (var interactor in interactors)
            {
                if (interactor.controller.trigger.IsPressed())
                {
                    loadedModel.transform.position = interactor.transform.position;
                    break;
                }
            }

            EditorManager.instance.mainMenu.Toggle();
        }));
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        hovered = true;
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        hovered = false;
    }
}

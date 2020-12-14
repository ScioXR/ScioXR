using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
#if !UNITY_WEBGL
using UnityEngine.XR.Interaction.Toolkit;
#endif

public class ModelSelecter : MonoBehaviour, IPointerClickHandler
{
#if !UNITY_WEBGL
    private XRGrabInteractable grab;
    public void Awake()
    {
        grab = gameObject.AddComponent<XRGrabInteractable>();
        grab.trackPosition = false;
        grab.trackRotation = false;
        grab.onSelectEntered.AddListener(OnModelSelect);
        grab.onSelectExited.AddListener(OnModelSelectExit);
    }

    //create real model in scene when model is selected from UI
    public void OnModelSelect(XRBaseInteractor interactor)
    {
        CreateModel();
    }

    public void OnModelSelectExit(XRBaseInteractor interactor)
    {
       // this.gameObject.SetActive(false);
        this.transform.SetParent(EditorManager.instance.mainMenu.transform);
    
    }
#else
    public void Awake()
    {
        //gameObject.AddComponent<Rigidbody>();
    }
#endif

    private void Update()
    {
        transform.Rotate(new Vector3(90, 90, 0) * Time.deltaTime * 0.5f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        CreateModel();
    }

    public void CreateModel()
    {
        //TODO better to just clone the current
        StartCoroutine(AssetsLoader.ImportModel(this.GetComponent<Saveable>().model, loadedModel =>
        {
            loadedModel.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 1f);
            loadedModel.transform.localScale = loadedModel.transform.localScale / 10;
            PlatformLoader.instance.platform.SetupEditorObject(loadedModel, this.GetComponent<Saveable>().model);
            loadedModel.AddComponent<Saveable>();
            loadedModel.GetComponent<Saveable>().model = this.GetComponent<Saveable>().model;
            loadedModel.GetComponent<Saveable>().GenerateUniqueId();

            this.gameObject.SetActive(true);
            EditorManager.instance.mainMenu.Toggle();
        }));
    }
}

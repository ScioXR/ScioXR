using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizationController : MonoBehaviour {

    public enum LocalizationType
    {
        TEXT,
        AUDIO
    }

    public LocalizationType type;
    public string key;

	// Use this for initialization
	void Start () {
		if (type == LocalizationType.TEXT)
        {
            GetComponent<Text>().text = LocalizationManager.instance.GetLocalizedValue(key);
        } else if (type == LocalizationType.AUDIO)
        {

        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

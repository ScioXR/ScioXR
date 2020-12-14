using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System;

public class EventManager : MonoBehaviour
{
    private Dictionary<string, UnityEvent> eventDictionary;

    private Dictionary<string, Action<object[]>> actionDictionary;

    private static EventManager eventManager;

    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else
                {
                    eventManager.Init();
                }
            }

            return eventManager;
        }
    }

    private void OnDestroy()
    {
        if (eventManager == this)
        {
            eventManager = null;
        }
    }

    void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, UnityEvent>();
            actionDictionary = new Dictionary<string, Action<object[]>>();
        }
    }

    public static void StartListening(string eventName, UnityAction listener)
    {
        UnityEvent thisEvent;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction listener)
    {
        if (eventManager == null) return;
        UnityEvent thisEvent;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName)
    {
        UnityEvent thisEvent;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke();
        }
    }

    public static void StartListening(string eventName, Action<object[]> listener)
    {
        Action<object[]> thisEvent;
        if (!instance.actionDictionary.TryGetValue(eventName, out thisEvent))
        {
            instance.actionDictionary.Add(eventName, listener);
        }
        else
        {
            Debug.LogError("Action already exists.");
        }
    }


    public static void StopListening(string eventName, Action<object[]> listener)
    {
        if (eventManager == null) return;
        Action<object[]> thisEvent;
        if (instance.actionDictionary.TryGetValue(eventName, out thisEvent))
        {
            if (thisEvent == listener)
            {
                instance.actionDictionary.Remove(eventName);
            } else
            {
                Debug.LogError("Action is not link to the event");
            }
        } else
        {
            Debug.LogError("Action does not exist exists.");
        }
    }

    public static void TriggerAction(string eventName, params object[] list)
    {
        Action<object[]> thisEvent;
        if (instance.actionDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(list);
        }
    }
}
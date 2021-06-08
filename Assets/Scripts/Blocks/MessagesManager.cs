using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessagesManager : MonoBehaviour
{
    public MessagesDispatcher[] messages;

    [Serializable]
    public class MessagesDispatcher
    {
        public string name;
        public List<ReceiveEvent> receiveEvents = new List<ReceiveEvent>();
    }

    public static MessagesManager instance;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        if (instance == this)
        {
            instance = null;
        }
    }

    public void InitMessages(string[] messagesString)
    {
        messages = new MessagesDispatcher[messagesString.Length];
        for (int i = 0; i < messagesString.Length; i++)
        {
            messages[i] = new MessagesDispatcher() { name = messagesString[i] };

        }
    }

    public void AddMessageReceiver(ReceiveEvent receiveEvent)
    {
        foreach (var message in messages)
        {
            if (message.name == receiveEvent.messageName)
            {
                message.receiveEvents.Add(receiveEvent);
                break;
            }
        }
    }

    public void TriggerMessage(string messageName)
    {
        foreach (var message in messages)
        {
            if (message.name == messageName)
            {
                foreach (var receiveEvents in message.receiveEvents)
                {
                    receiveEvents.Trigger();
                }
                break;
            }
        }
    }
}

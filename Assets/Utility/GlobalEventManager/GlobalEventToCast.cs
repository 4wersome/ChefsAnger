using System;
using UnityEngine;

[Serializable]
public class GlobalEventToCast 
{

    [SerializeField]
    private GlobalEventIndex eventToCast;
    public GlobalEventIndex EventToCast {
        get { return eventToCast; }
    }
    [SerializeField]
    private GlobalEventArgs message;
    public GlobalEventArgs Message {
        get { return message; }
    }

    public GlobalEventToCast (GlobalEventIndex eventToCast, GlobalEventArgs message) {
        this.eventToCast = eventToCast;
        this.message = message;
    }
}

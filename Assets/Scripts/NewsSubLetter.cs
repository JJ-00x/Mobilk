using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class NewsSubLetter : ScriptableObject
{
    private List<INewsletter> _subscribers = new List<INewsletter>();

    public void SendNewsletter()
    {
        for (int i = 0; i < _subscribers.Count; i++)
        {
            _subscribers[i].Notify();
        }
    }
    public void SubsrcribeForNewsletter(INewsletter newSubscriber)
    {
        _subscribers.Add(newSubscriber);
    }
    
    public void UnsubscribeForNewsletter(INewsletter SubscriberToRemove)
    {
        _subscribers.Remove(SubscriberToRemove);
    }
}

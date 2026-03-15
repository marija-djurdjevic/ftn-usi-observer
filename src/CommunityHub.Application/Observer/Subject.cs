using System;
using System.Collections.Generic;
using System.Text;

namespace CommunityHub.Application.Observer
{
    public class Subject
    {
        private readonly List<IObserver> _observers;

        public Subject()
        {
            _observers = new List<IObserver>();
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (IObserver observer in _observers)
            {
                observer.Update();
            }
        }
    }
}

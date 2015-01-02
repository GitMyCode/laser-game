using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Assets.Scripts.Interfaces
{
    public interface ISubject
    {


        void attach(Assets.Scripts.Interfaces.Observer observer);


        void detach(Assets.Scripts.Interfaces.Observer observer);


       void notifyObservers();

    }
}

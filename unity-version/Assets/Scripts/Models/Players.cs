using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Players : IEnumerable<PlayerBase> {


    private List<PlayerBase> players; 


    public Players()
    {
        players = new List<PlayerBase>();
    }

    public void Awake()
    {
         players = new List<PlayerBase>( GameObject.FindObjectsOfType<PlayerBase>() );
        
    }


    public IEnumerator<PlayerBase> GetEnumerator()
    {
        foreach (PlayerBase p in players)
        {
            yield return p;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

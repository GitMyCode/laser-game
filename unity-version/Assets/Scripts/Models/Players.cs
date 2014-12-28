using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Players : IEnumerable<MyPlayer> {


    private List<MyPlayer> players; 


    public Players()
    {
        players = new List<MyPlayer>();
    }

    public void Awake()
    {
         players = new List<MyPlayer>( GameObject.FindObjectsOfType<MyPlayer>() );
        
    }


    public IEnumerator<MyPlayer> GetEnumerator()
    {
        foreach (MyPlayer p in players)
        {
            yield return p;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

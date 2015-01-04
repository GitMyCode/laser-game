using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Players : IEnumerable<myPlayer> {


    private List<myPlayer> players; 


    public Players()
    {
        players = new List<myPlayer>();
    }

    public void Awake()
    {
         players = new List<myPlayer>( GameObject.FindObjectsOfType<myPlayer>() );
        
    }


    public IEnumerator<myPlayer> GetEnumerator()
    {
        foreach (myPlayer p in players)
        {
            yield return p;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

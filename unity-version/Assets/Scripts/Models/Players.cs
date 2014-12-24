using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Players : IEnumerable<Player> {


    private List<Player> players; 


    public Players()
    {
        players = new List<Player>();
    }

    public void Awake()
    {
         players = new List<Player>( GameObject.FindObjectsOfType<Player>() );
        
    }


    public IEnumerator<Player> GetEnumerator()
    {
        foreach (Player p in players)
        {
            yield return p;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

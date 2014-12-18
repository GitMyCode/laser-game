using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Players : IEnumerable<IPlayer> {


    private List<Player> players; 


    public Players()
    {
        players = new List<Player>();
    }

    public void Awake()
    {
         players = new List<Player>( GameObject.FindObjectsOfType<Player>() );
        
    }


    public IEnumerator<IPlayer> GetEnumerator()
    {
        throw new System.NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new System.NotImplementedException();
    }
}

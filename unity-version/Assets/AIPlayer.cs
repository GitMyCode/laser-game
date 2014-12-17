using UnityEngine;
using System.Collections;

public class AIPlayer : IPlayer {
    private int life;
    private int energy;
    private GameObject zone;
    private GameObject goal;
    private EPlayer player;

    public int blockCheckRate = 20;
    public int blockCounter = 1;

    bool CanShoot = true;

     public AIPlayer(int life, int energy, GameObject zone,GameObject goal)
    {
        this.life = life;
        this.energy = energy;
        this.zone = zone;
        this.goal = goal;
    }

     public void playerTurn()
     {
         if (blockCounter % blockCheckRate == 0)
         {

             foreach (LaserModel lm in GameArbiter.lineModelDictionary.Values)
             {
                 if (lm.owner != (IPlayer)this)
                 {
                     if (zone.transform.collider2D.bounds.Contains(lm.head.transform.position))
                     {
                         GameArbiter.Instance.actionQueue.Enqueue(new Action(lm.head.transform.position,
                                                                    lm.head.transform.position,
                                                                    0.1f, Action.ActionType.DEFENSIVE,
                                                                    this));
                     }

                 }
             }
             blockCounter = 0;
         }
         blockCounter++;


         if (CanShoot)
         {
             launchLine();
             CanShoot = false;
             Coroutiner.Instance.StartCoroutine2(reload());
         }

     }


     void launchLine()
     {
         float randomX = Random.Range(zone.transform.position.x - zone.transform.localScale.x / 2, zone.transform.position.x + zone.transform.localScale.x / 2);
         float randomY = Random.Range((zone.transform.position.y - zone.transform.localScale.y / 2), (zone.transform.position.y));
         Vector3 start = new Vector3(randomX, randomY, 0);
         randomX = Random.Range(zone.transform.position.x - zone.transform.localScale.x / 2, zone.transform.position.x + zone.transform.localScale.x / 2);

         randomY = Random.Range((zone.transform.position.y - zone.transform.localScale.y / 2), (zone.transform.position.y + zone.transform.localScale.y / 2) - (randomY + 11));
         Vector3 end = new Vector3(randomX, randomY, 0);

         Debug.DrawRay(start, end, Color.red, 2, false);
         GameArbiter.Instance.actionQueue.Enqueue(new Action(start, end, Random.Range(0.1f, 0.3f), Action.ActionType.ATTACK, this));
        
     }

     IEnumerator reload()
     {
         yield return new WaitForSeconds(1);
         CanShoot = true;
     }



     public bool isInZone(Vector3 point)
     {
         return zone.transform.collider2D.bounds.Contains(point);
     }

     public int Life
     {
         get
         {
             return life;
         }
         set
         {
             life = value;
         }
     }

     public int Energy
     {
         get
         {
             return energy;
         }
         set
         {
             energy = value;
         }
     }

     public EPlayer Player
     {
         get
         {
             return player;
         }
         set
         {
             this.player = value;
         }
     }




     public GameObject Zone
     {
         get
         {
             return zone;
         }
         set
         {
             zone = value;
         }
     }

     public override string ToString()
     {
         return string.Format("[P2: life={0},\n energy={1}]", life, energy);
     }



     public GameObject Goal
     {
         get
         {
             return goal;
         }
         set
         {
             goal = value;
         }
     }




    
}

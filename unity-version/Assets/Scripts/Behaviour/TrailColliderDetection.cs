using UnityEngine;
using System.Collections;

public class TrailColliderDetection : MonoBehaviour {

 
 public float time = 2.0f;
 public int rate = 10;
 public TrailRenderer trail;
 
 private Vector3[] arv3;
 private int head ;
 private int tail = 0;
 private float sliceTime = ( 1.0f);
 private RaycastHit2D hit ;
 ICollidable collidableHead;
 public void Start () {
     //trail.gameObject.layer = 6;
     
     
     collidableHead = gameObject.GetComponent(typeof(ICollidable)) as ICollidable;
     sliceTime = sliceTime/rate;

     arv3 = new Vector3[(Mathf.RoundToInt(time * rate) + 1)];
     
     for (var i = 0; i < arv3.Length; i++)
         arv3[i] = transform.position;
     head = arv3.Length-1;
     StartCoroutine(CollectData());
 }
 
 public IEnumerator  CollectData()  {
     while (true) {
         if (transform.position != arv3[head]) {
             head = (head + 1) % arv3.Length;
             tail = (tail + 1) % arv3.Length;
             arv3[head] = transform.position;
         }
         yield return new WaitForSeconds(sliceTime);
     }
 }
 
 public void Update() {
     if (Hit())
     {
         if (collidableHead is CollidableBase && ((GameObject)hit.collider.gameObject).GetComponent(typeof(ICollidable)) != null)
         {
             ICollidable a = ((GameObject)hit.collider.gameObject).GetComponent(typeof(ICollidable)) as ICollidable;
             GameArbiter.Instance.collidableQueue.Enqueue(new CollidableEvent(collidableHead, a));
         }
         Debug.Log("I hit: "+hit.collider.name);
     }
    //DebugDraw.DrawSphere(arv3[tail], 2, Color.yellow);
 }
 
 public bool Hit() {
     var i = head;
     var j = (head  - 1);
     if (j < 0) j = arv3.Length - 1;
     var distance = Vector2.Distance(arv3[i], arv3[j]); 
     while (j != head) {
         var direction = arv3[j] - arv3[i];
         //Debug.DrawRay(arv3[i], direction, Color.red,0.1f, false);
         hit = Physics2D.Raycast(arv3[i], direction, distance, 1 << LayerMask.NameToLayer("Line"));
         if (hit.collider != null && transform.gameObject.name != hit.collider.name)
         {

             return true;
         }
         i = i - 1; 
         if (i < 0) i = arv3.Length - 1;
         j = j - 1;
         if (j < 0) j = arv3.Length - 1;
     }
     return false;
 }
	
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Coroutiner : MonoBehaviour {
    // This will be null until after Awake()
    private static Coroutiner instance;

    

    public static Coroutiner Instance
    {
        get { return instance; }
    }

    void Awake() {
        instance = this;
    }
    public Coroutine StartCoroutine2(IEnumerator firstIterationResult) {
        return this.StartCoroutine( firstIterationResult );
    }
}
 
using UnityEngine;
using System.Collections;

public class Ring : MonoBehaviour {

    Collider coll;
    MeshRenderer mr;
    Material ringMat;
    Color color;
    bool is_vanishing = false;
    RingMananger ringManager;

    public delegate void RingComplete();
    public static event RingComplete OnComplete;
    //Ring.RingComplete callback;

    // Use this for initialization
    void Start () {
        coll = GetComponent<Collider>();
        mr = GetComponent<MeshRenderer>();
        ringMat = mr.material;
        ringManager = GetComponent<RingMananger>();
        Debug.Log(OnComplete);
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggedd");
        if(other.tag=="Player" && !is_vanishing)
        {
            Debug.Log("Player hit");
            is_vanishing = true;
            StartCoroutine("VanishRing");
        }
    }


    IEnumerator VanishRing()
    {
        Debug.Log("VAnish");
        color = ringMat.color;
        while (color.a>-0.01f)
        {
            Debug.Log(color);
            color.a -= 0.7f * Time.deltaTime;
            ringMat.color = color;
            yield return null;
        }
        RunCallback();
        Destroy(gameObject);
    }

    void RunCallback()
    {
        Debug.Log("Run callback called");
        
        if (OnComplete != null)
        {
            Debug.Log("oncomplete not called");
            OnComplete();
        }
    }
}

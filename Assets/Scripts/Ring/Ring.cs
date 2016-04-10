using UnityEngine;
using System.Collections;

public class Ring : MonoBehaviour
{

    Collider coll;
    MeshRenderer mr;
    Material ringMat;
    Color color;
    bool is_vanishing = false;
    RingMananger ringManager;

    public AudioSource Audio;

    public delegate void RingComplete();
    public static event RingComplete OnComplete;

    void Start()
    {
        coll = GetComponent<Collider>();
        mr = GetComponent<MeshRenderer>();
        ringMat = mr.material;
        ringManager = GetComponent<RingMananger>();
    }

    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9 && !is_vanishing)
        {
            is_vanishing = true;
            StartCoroutine(VanishRing());
            Player.Instance.Score += 10;
        }
    }

    IEnumerator VanishRing()
    {
        Debug.Log(ringMat);
        color = ringMat.color;

        RunCallback();

        while (color.a > -0.01f)
        {
            color.a -= 0.7f * Time.deltaTime;
            ringMat.color = color;
            yield return null;
        }
        Destroy(gameObject, 2.0f);
    }

    void RunCallback()
    {

        if (OnComplete != null)
        {
            OnComplete();
        }
    }
}

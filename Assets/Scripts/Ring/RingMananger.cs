using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RingMananger : MonoBehaviour {

    public GameObject ring;
    //public List<Vector3> positions;
    public float nextRingDistance;
    public float nextRingRandomXY;

    // number of rings to create, set to -1 for infinite
    public int maxRings;
    public Vector3 initialRingPosition;
    int index = 0;
    Vector3 lastRingPosition;

    void Awake()
    {
        Ring.OnComplete += RingComplete;
    }

    public MusicController MC;
    public EnemySpawner ES;

	// Use this for initialization
	void Start () {

        if(index< maxRings)
        {
            index++;
            CreateRing(initialRingPosition);
            lastRingPosition = initialRingPosition;
        }


        //Debug.Log(positions.Count);
        //if ( positions.Count > 0)
            //CreateRing(positions[0]);
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    // Creates a Ring at position given
    void CreateRing(Vector3 position)
    {
        Debug.Log("Created Ring at " + position);
        Object obj = Instantiate(ring,position,Quaternion.Euler(90f,0f,0f));
        GameObject gb = (GameObject)obj;
        Ring thering = gb.GetComponent<Ring>();
        StartCoroutine(FadeInRing(thering));
    }

    IEnumerator FadeInRing(Ring thering)
    {
        MeshRenderer mr = thering.GetComponent<MeshRenderer>();
        Material mat = mr.material;
        Color color = mat.color;
        float MaxA = color.a;
        color.a = 0;
        mat.color = color;
        while(color.a< MaxA)
        {
            color.a += Time.deltaTime * MaxA / 0.5f;
            mat.color = color;
            yield return null;
        }

        color.a = MaxA;
        mat.color = color;
    }

    Vector3 NextRingPosition()
    {
        lastRingPosition = new Vector3(
            lastRingPosition.x + Random.Range(0, nextRingRandomXY) - nextRingRandomXY / 2,
            lastRingPosition.y + Random.Range(0, nextRingRandomXY) - nextRingRandomXY / 2,
            lastRingPosition.z + nextRingDistance);
        return lastRingPosition;
    }
    
    public void RingComplete()
    {
        MC.TargetVolume = 0.3f;
        ES.gameObject.SetActive(true);
        if (index < maxRings)
        {
            CreateRing(NextRingPosition());
            index++;
        }
    }
}

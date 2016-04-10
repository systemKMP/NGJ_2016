using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RingMananger : MonoBehaviour {

    public GameObject ring;
    public List<Vector3> positions;
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
        Debug.Log("Created Ring at " + positions);
        Instantiate(ring,position,Quaternion.Euler(90f,0f,0f));
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
        Debug.Log("Ring Complete called");

        if (index < maxRings)    //positions.Count)
        {
            //CreateRing(positions[index]);
            CreateRing(NextRingPosition());
            index++;
        }
    }
}

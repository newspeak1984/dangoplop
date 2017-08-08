using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball_Spawn : MonoBehaviour {

    public GameObject ball;
    public GameObject spawnPos;
    public GameObject spawnPos2;
    private int random;
    public int maxBalls;
    public float spawnWait;
    public int count;

	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnWaves());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator SpawnWaves()
    {
        random = (int)(Random.Range(0, 2));
        Debug.Log(random);
        if (random == 0)
        {          
            Instantiate(ball, spawnPos.transform.position, transform.rotation);
        }
        else
        {
            Instantiate(ball, spawnPos2.transform.position, transform.rotation);
        }    
        count++;
        yield return new WaitForSeconds(spawnWait);
        if (count == maxBalls)
        {
            Debug.Log("DONE RECURSION");
        }
        else
        {
            StartCoroutine(SpawnWaves());
        }


    }
}

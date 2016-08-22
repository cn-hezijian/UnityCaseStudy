using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    public float startWait = 1.0f;
    public float spawnWait = 0.5f;
    public float waveWait = 2.0f;

    public GameObject hazard;
    public int hazardCount = 5;
    public Vector3 spawnValue;
    private Vector3 spawnPosition = Vector3.zero;
    private Quaternion spawnRotation;

    IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; ++i)
            {
                spawnPosition.x = Random.Range(-spawnValue.x, spawnValue.x);
                spawnPosition.z = spawnValue.z;
                spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }
    }
	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnWave());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

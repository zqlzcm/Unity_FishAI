using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManager : MonoBehaviour {
    public GameObject fishPrefab;
    public int numOfFlock=10;
    public static Vector3 boundary=new Vector3(5,5,5);
    public static Vector3 goalPos=Vector3.zero;
    public static GameObject[] fishes;

	// Use this for initialization
	void Start () {
        FogSetting();
        Spwan();
	}
	
	// Update is called once per frame
	void Update () {
        if (Random.Range(0, 1000) < 50)
        {
            goalPos = new Vector3(Random.Range(-boundary.x, boundary.x),
               Random.Range(-boundary.y, boundary.y),
               Random.Range(-boundary.z, boundary.z));
        }
	}

    void FogSetting()
    {
        RenderSettings.fogColor = Camera.main.backgroundColor;
        RenderSettings.fogDensity = 0.03f;
        RenderSettings.fog = true;
    }
    void Spwan()
    {
        fishes = new GameObject[numOfFlock];
        for (int i = 0; i < numOfFlock; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-boundary.x,boundary.x),
                Random.Range(-boundary.y, boundary.y),
                Random.Range(-boundary.z, boundary.z));
            GameObject go = Instantiate(fishPrefab, pos, Quaternion.identity);
            fishes[i] = go;
            go.transform.parent = transform;
        }
    }

    



}

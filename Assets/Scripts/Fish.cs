using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour {
    public float speed = 5f;
    float rotationSpeed = 4f;
    Vector3 averageHeading;
    Vector3 averagePosition;
    float neigbourDistance=2f;
    bool turning=false;
	// Use this for initialization
	void Start () {
        speed = Random.Range(2, speed);
	}
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(transform.position, Vector3.zero) >= FishManager.boundary.x)
        {
            turning = true;
        }
        else
        {
            turning = false;

        }

        if (turning)
        {
            Vector3 direction = Vector3.zero - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
            speed = Random.Range(2, speed);
        }else
        if (Random.Range(0, 5) < 1)
        {
            ApplyRules();
        }
        transform.Translate(transform.forward * speed * Time.deltaTime);
	}
    void ApplyRules()
    {
        GameObject[] gos;
        gos = FishManager.fishes;
        Vector3 vcenter = Vector3.zero;
        Vector3 vavoid = Vector3.zero;
        float gSpeed = 0.1f;
        Vector3 goalPos = FishManager.goalPos;
        float dist;
        int groupSize=0;
        foreach (GameObject go in gos)
        {
            if (go != this.gameObject)
            {
                dist = Vector3.Distance(go.transform.position, this.gameObject.transform.position);
                if (dist <= neigbourDistance)
                {
                    vcenter += go.transform.position;
                    groupSize++;
                    if (dist < 1)
                    {
                        vavoid= vavoid+(this.transform.position-go.transform.position);                        
                    }
                    Fish anotherFlock = go.GetComponent<Fish>();
                    gSpeed = gSpeed + anotherFlock.speed;
                }
            }
        }
        if (groupSize>0)
        {
            vcenter = vcenter / groupSize + (goalPos - this.transform.position);
            speed = gSpeed / groupSize;
            //print("GS:"+groupSize);
            Vector3 direction = (vcenter + vavoid) - transform.position;
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
                //print("Rotate");
            }
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        print("1:" + transform.name + ":" + collision.transform.name);
    }
}

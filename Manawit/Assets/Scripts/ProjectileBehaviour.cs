using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour {
    public float speed;
    public Vector3 destination;
	// Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(this.transform.position, destination) < 0.1)
        {
            Destroy(this.gameObject);

        }
        else
        {
            Vector3 direction = Vector3.Normalize(destination - this.transform.position);
            this.transform.position += direction * speed* Time.deltaTime;

        }
	}
}

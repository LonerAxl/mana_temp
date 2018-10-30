using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



public class ElementCube : MonoBehaviour {
    public float speed;
    public GameObject generatingManager;
    public Ball ball;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        Vector3 dest = new Vector3(-3.0f+this.ball.desCol, -1.5f, -3.0f+this.ball.desRow);
        if (Vector3.Distance(this.transform.position, dest) < 0.1)
        {
            this.transform.position = dest;
            this.ball.unlock();
            if (this.ball.playerFlag!=0)
            {
                this.generatingManager.GetComponent<Generating>().playerSwapped(this.gameObject,this.ball.playerFlag);

            }

        }
        else
        {
            Vector3 direction = Vector3.Normalize(dest - this.transform.position);
            this.transform.position += direction * speed* Time.deltaTime;

        }

	}

   

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {
    public GameObject pipePrefab;

    private List<GameObject> selection;

    public Player player;
	// Use this for initialization
	void Start () {
        this.selection = new List<GameObject>(4);

	}
	
	// Update is called once per frame
	void Update () {
        if (player.isSelect && selection.Count==0)
        {
            
            GameObject instance1 = Instantiate(pipePrefab, this.gameObject.transform.position+new Vector3(0.0f,0.0f,1.0f), this.gameObject.transform.rotation);
            GameObject instance2 = Instantiate(pipePrefab, this.gameObject.transform.position+new Vector3(0.0f,0.0f,-1.0f), this.gameObject.transform.rotation);
            GameObject instance3 = Instantiate(pipePrefab, this.gameObject.transform.position+new Vector3(1.0f,0.0f,0.0f), this.gameObject.transform.rotation);
            GameObject instance4 = Instantiate(pipePrefab, this.gameObject.transform.position+new Vector3(-1.0f,0.0f,0.0f), this.gameObject.transform.rotation);
            instance1.GetComponent<Renderer>().material.color = this.gameObject.GetComponent<Renderer>().material.color;
            instance2.GetComponent<Renderer>().material.color = this.gameObject.GetComponent<Renderer>().material.color;
            instance3.GetComponent<Renderer>().material.color = this.gameObject.GetComponent<Renderer>().material.color;
            instance4.GetComponent<Renderer>().material.color = this.gameObject.GetComponent<Renderer>().material.color;
            selection.Add(instance1);
            selection.Add(instance2);
            selection.Add(instance3);
            selection.Add(instance4);
        }

        if(!player.isSelect&&selection.Count!=0)
        {
            GameObject tmp;
            for (int i = 0; i < 4; i++)
            {
                tmp = selection[0];
                selection.Remove(tmp);
                Destroy(tmp);
            }
        }

        this.setPosition(this.player.row, this.player.col);
	}

    public void setColor(int id){
        if (id == 1)
        {
            this.gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
        else if (id == 2)
        {
            this.gameObject.GetComponent<Renderer>().material.color = Color.black;
        }
    }

    private void setPosition(int row, int col){
        Vector3 tmp = new Vector3(-3.5f+col, 1.0f, -3.5f+row);
        this.transform.position = tmp;
    }
}

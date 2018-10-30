using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BattleManager : MonoBehaviour {
    public GameObject p1obj;
    public GameObject p2obj;
    public Player player1;
    public Player player2;
    private bool p1WindFlag;
    private bool p2WindFlag;

    public GameObject lightProjectile;
    public GameObject fireProjectile;
    public GameObject waterProjectile;
    public GameObject windProjectile;
    public GameObject earthProjectile;
    public GameObject darkProjectile;

    public Vector3 p1Pos;
    public Vector3 p2Pos;

	// Use this for initialization
	void Start () {
        this.p1WindFlag = false;
        this.p2WindFlag = false;
        this.player1 = p1obj.GetComponent<PlayerBehaviour>().player;
        this.player2 = p2obj.GetComponent<PlayerBehaviour>().player;
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < 6; i++)
        {
            if (player1.inventory[i] >= 10)
            {
                player1.inventory[i] -= 10;
                switch (i)
                {
                    case 0:
                        player1.hp *= 2;
                        break;
                    case 1:
                        player2.hp -= 6;
                        break;
                    case 2:
                        player1.hp += 6;
                        break;
                    case 3:
                        p1WindFlag = true;
                        break;
                    case 4:
                        player1.hp += 3;
                        player2.hp -= 3;
                        break;
                    case 5:
                        player2.hp = (int)(player2.hp / 2);
                        break;
                }
                attackEffect(1, i);
            }
                
            if (player2.inventory[i] >= 10)
            {
                player2.inventory[i] -= 10;
                switch (i)
                {
                    case 0:
                        player2.hp *= 2;
                        break;
                    case 1:
                        player1.hp -= 6;
                        break;
                    case 2:
                        player2.hp += 6;
                        break;
                    case 3:
                        p2WindFlag = true;
                        break;
                    case 4:
                        player1.hp -= 3;
                        player2.hp += 3;
                        break;
                    case 5:
                        player1.hp = (int)(player1.hp / 2);
                        break;
                }
                attackEffect(2, i);
            }
        }

        if (p1WindFlag)
        {
            for (int i = 0; i < 6; i++)
            {
                player2.inventory[i] = (int)(player2.inventory[i] / 2);
            }
        }

        if (p2WindFlag)
        {
            for (int i = 0; i < 6; i++)
            {
                player1.inventory[i] = (int)(player1.inventory[i] / 2);
            }
        }

        p1WindFlag = false;
        p2WindFlag = false;
	}

    private void attackEffect(int player, int i){
        Vector3 origin;
        Vector3 destination;
        GameObject instance;
        Quaternion rotation;

        if (player == 1)
        {
            origin = p1Pos;
            destination = p2Pos;
            rotation = lightProjectile.transform.rotation;

        }
        else
        {
            origin = p2Pos;
            destination = p1Pos;
            rotation = lightProjectile.transform.rotation * Quaternion.Euler(0.0f,180f,0.0f);
        }
        switch (i)
        {
            case 0:
                instance = Instantiate(lightProjectile, origin, rotation);
                instance.GetComponent<ProjectileBehaviour>().destination = destination;
                break;
            case 1:
                instance = Instantiate(fireProjectile, origin, rotation);
                instance.GetComponent<ProjectileBehaviour>().destination = destination;
                break;
            case 2:
                instance = Instantiate(waterProjectile, origin, rotation);
                instance.GetComponent<ProjectileBehaviour>().destination = destination;
                break;
            case 3:
                instance = Instantiate(windProjectile, origin, rotation);
                instance.GetComponent<ProjectileBehaviour>().destination = destination;
                break;
            case 4:
                instance = Instantiate(earthProjectile, origin, rotation);
                instance.GetComponent<ProjectileBehaviour>().destination = destination;
                break;
            case 5:
                instance = Instantiate(darkProjectile, origin, rotation);
                instance.GetComponent<ProjectileBehaviour>().destination = destination;
                break;
        }

    }
}

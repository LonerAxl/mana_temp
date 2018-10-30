using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour {
    public GameObject p1obj;
    public GameObject p2obj;
    public Player player1;
    public Player player2;
    public GameObject manager;
    private Text[] p1Inventory;
    private Text[] p2Inventory;
    private Text p1HP;
    private Text p2HP;
    private Text timeleft;
    private Text win;
	// Use this for initialization
	void Start () {
        p1Inventory = new Text[]{
            this.gameObject.transform.Find("P1Light").gameObject.GetComponent<Text>(),
            this.gameObject.transform.Find("P1Fire").gameObject.GetComponent<Text>(),
            this.gameObject.transform.Find("P1Water").gameObject.GetComponent<Text>(),
            this.gameObject.transform.Find("P1Wind").gameObject.GetComponent<Text>(),
            this.gameObject.transform.Find("P1Earth").gameObject.GetComponent<Text>(),
            this.gameObject.transform.Find("P1Dark").gameObject.GetComponent<Text>()
        };
        p2Inventory = new Text[]{
            this.gameObject.transform.Find("P2Light").gameObject.GetComponent<Text>(),
            this.gameObject.transform.Find("P2Fire").gameObject.GetComponent<Text>(),
            this.gameObject.transform.Find("P2Water").gameObject.GetComponent<Text>(),
            this.gameObject.transform.Find("P2Wind").gameObject.GetComponent<Text>(),
            this.gameObject.transform.Find("P2Earth").gameObject.GetComponent<Text>(),
            this.gameObject.transform.Find("P2Dark").gameObject.GetComponent<Text>()
        };
        p1HP = this.gameObject.transform.Find("P1HP").gameObject.GetComponent<Text>();
        p2HP = this.gameObject.transform.Find("P2HP").gameObject.GetComponent<Text>();
        timeleft=this.gameObject.transform.Find("Timeleft").gameObject.GetComponent<Text>();
        win=this.gameObject.transform.Find("Win").gameObject.GetComponent<Text>();
        this.player1 = p1obj.GetComponent<PlayerBehaviour>().player;
        this.player2 = p2obj.GetComponent<PlayerBehaviour>().player;
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < 6; i++)
        {
            p1Inventory[i].text = player1.inventory[i].ToString();
            p2Inventory[i].text = player2.inventory[i].ToString();

        }
        p1HP.text = player1.hp.ToString();
        p2HP.text = player2.hp.ToString();
        timeleft.text = ((int)(manager.GetComponent<Generating>().time)).ToString();
        if (player1.hp <= 0)
        {
            win.text = "Player 2 Win!!!";
        }
        else if (player2.hp <= 0)
        {
            win.text = "Player 1 Win!!!";
        }
        else if (manager.GetComponent<Generating>().time<0)
        {
            if (player1.hp == player2.hp)
            {
                win.text = "Draw!!!";
            }
            else if (player1.hp > player2.hp)
            {
                win.text = "Player 1 Win!!!";
            }
            else
            {
                win.text = "Player 2 Win!!!";
            }
        }
	}
}

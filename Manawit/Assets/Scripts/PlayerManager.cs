using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    public Player p1;
    public Player p2;
    public GameObject p1obj;
    public GameObject p2obj;

	// Use this for initialization
	void Start () {
        this.p1 = new Player(1, p1obj);
        this.p2 = new Player(2, p2obj);
        p1obj.GetComponent<PlayerBehaviour>().player = p1;
        p2obj.GetComponent<PlayerBehaviour>().player = p2;

	}
	
	// Update is called once per frame
	void Update () {
        p1.pressUp = Input.GetKeyDown(KeyCode.W);
        p1.pressDown = Input.GetKeyDown(KeyCode.S);
        p1.pressLeft = Input.GetKeyDown(KeyCode.A);
        p1.pressRight = Input.GetKeyDown(KeyCode.D);
        p1.pressSelect = Input.GetKeyDown(KeyCode.Space);
        p1.flag = false;

        p2.pressUp = Input.GetKeyDown(KeyCode.UpArrow);
        p2.pressDown = Input.GetKeyDown(KeyCode.DownArrow);
        p2.pressLeft = Input.GetKeyDown(KeyCode.LeftArrow);
        p2.pressRight = Input.GetKeyDown(KeyCode.RightArrow);
        p2.pressSelect = Input.GetKeyDown(KeyCode.L);

        p2.flag = false;

        Player cur;
        for (int i = 0; i < 2; i++)
        {
            if (i == 0)
            {
                cur = p1;
            }
            else
            {
                cur = p2;
            }

            if(cur.isSelect)
            {
                int tmpRow = 0;
                int tmpCol = 0;
                if (cur.pressUp)
                {
                    tmpRow += 1;
                }else if (cur.pressDown)
                {
                    tmpRow += -1;
                }else if (cur.pressLeft)
                {
                    tmpCol += -1;
                }else if (cur.pressRight)
                {
                    tmpCol += 1;
                }
                if ((cur.pressUp&&cur.row<8)||(cur.pressDown&&cur.row>0)||(cur.pressLeft&&cur.col>0)||(cur.pressRight&&cur.col<8))
                {
                    int desRow = cur.row + tmpRow;
                    int desCol = cur.col + tmpCol;
                    Globals.Swap(Globals.FindCube(cur.row,cur.col),Globals.FindCube(desRow,desCol), cur.id);
                }
                if (cur.pressSelect||cur.pressUp||cur.pressDown||cur.pressLeft||cur.pressRight)
                {
                    cur.isSelect = false;
                    cur.flag = true;
                }

            }
            if(!cur.isSelect)
            {

                if (cur.pressUp&&cur.row<8)
                {
                    cur.row += 1;
                }
                if (cur.pressDown&&cur.row>0)
                {
                    cur.row -= 1;
                }
                if (cur.pressLeft&&cur.col>0)
                {
                    cur.col -= 1;
                }
                if (cur.pressRight&&cur.col<8)
                {
                    cur.col += 1;
                }
                if (cur.pressSelect && !cur.flag)
                {
                    cur.isSelect = true;
                }
            }


        }


	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player {
    
    public bool isSelect;

    public int row;
    public int col;
    public int id;
    public int hp;
    public int[] inventory;
    public bool flag;

    public bool pressUp;
    public bool pressDown;
    public bool pressLeft;
    public bool pressRight;
    public bool pressSelect;

    public Player(int id, GameObject obj){
        this.id = id;
        this.isSelect = false;
        this.row=Mathf.RoundToInt((float)(obj.transform.position.z+3));
        this.col=Mathf.RoundToInt((float)(obj.transform.position.x+3));
        this.hp = 20;
        this.inventory = new int[]{ 0, 0, 0, 0, 0, 0 };
        this.flag = false;
        this.pressUp = false;
        this.pressDown = false;
        this.pressLeft = false;
        this.pressRight = false;
        this.pressSelect = false;

        ActionTest((param)=>{
            obj.GetComponent<PlayerBehaviour>().setColor((int)param);
        },this.id);
    }

    public void getAnElement(Globals.ElementType element){
        this.inventory[(int)element] += 1;
    }

    public void ActionTest(Action<object> taction, object tparam)
    {
        taction(tparam);
    }
}

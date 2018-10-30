using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ball {
    public Globals.ElementType type;
    public int desRow;
    public int desCol;
    public bool isLocked;

    public int playerFlag;

    public Ball(Globals.ElementType type, GameObject obj){
        this.type = type;
        this.desRow=Mathf.RoundToInt((float)(obj.transform.position.z+3));
        this.desCol=Mathf.RoundToInt((float)(obj.transform.position.x+3));
        this.isLocked = false;
        this.playerFlag = 0;
    }

    public void unlock(){
        this.isLocked = false;
    }
}

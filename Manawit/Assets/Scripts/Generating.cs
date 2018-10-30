using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generating : MonoBehaviour {

    public GameObject cubePrefab;
    public GameObject darkPrefab;
    public GameObject lightPrefab;
    public GameObject firePrefab;
    public GameObject waterPrefab;
    public GameObject windPrefab;
    public GameObject earthPrefab;

    private HashSet<GameObject> destroy;
    private bool startFlag;
    public GameObject player1;
    public GameObject player2;
    public float time;
	// Use this for initialization
	void Start () {
        destroy = new HashSet<GameObject>();
        InvokeRepeating("generateBackup", 0, 1);
        startFlag = false;
        time = 180;
	}
	
	// Update is called once per frame
	void Update () {
        bool restart = Input.GetKeyDown(KeyCode.G);
        bool destroy = Input.GetKeyDown(KeyCode.H);
        bool quit = Input.GetKeyDown(KeyCode.J);
        bool test = Input.GetKeyDown(KeyCode.I);
        if (startFlag)
        {
            for (int i = 0; i < 9; i++)
            {
                
                if (Globals.getCol(i).Count < 9)
                {
                    if (Globals.backupCube[i]!=null){
                        GameObject current = Globals.backupCube[i];

                        Globals.backupCube[i] = null;

                        Globals.cubeList.Add(current);

                    }
                }
            }
            time -= Time.deltaTime;
        }
        if (restart){
            DestroyAll();
            startFlag = true;
        }
        if (destroy)
        {
            DestroyAll();
        }
        if (quit) {
            Application.Quit();
        }
        if (this.destroy.Count != 0)
        {
            foreach (GameObject cube in this.destroy)
            {
                Globals.cubeList.Remove(cube);
//                int targetCol = cube.GetComponent<ElementCube>().col;
//                int targetRow = cube.GetComponent<ElementCube>().row;
//                respawn(targetRow,targetCol);
                if(cube.GetComponent<ElementCube>().ball.playerFlag==1){
                    player1.GetComponent<PlayerBehaviour>().player.getAnElement(cube.GetComponent<ElementCube>().ball.type);
                }else if(cube.GetComponent<ElementCube>().ball.playerFlag==2){
                    player2.GetComponent<PlayerBehaviour>().player.getAnElement(cube.GetComponent<ElementCube>().ball.type);
                }
                Destroy(cube);
            }
            this.destroy.Clear();
            foreach (GameObject cube in Globals.cubeList)
            {
                if (!cube.GetComponent<ElementCube>().ball.isLocked)
                {
                    cube.GetComponent<ElementCube>().ball.playerFlag = 0;
                }
            }
        }

        //handlind cube dropping
        for (int i = 0; i < 9; i++)
        {
            List<GameObject> currCol = Globals.getCol(i);
            for(int j=0;j<currCol.Count;j++){

                int gap =currCol[j].GetComponent<ElementCube>().ball.desRow - j;
                if ((!currCol[j].GetComponent<ElementCube>().ball.isLocked) && gap !=0)
                {
                    for (int k = j; k < currCol.Count; k++)
                    {
                        if (!currCol[k].GetComponent<ElementCube>().ball.isLocked)
                        {
                            currCol[k].GetComponent<ElementCube>().ball.desRow += (-gap);
                            currCol[k].GetComponent<ElementCube>().ball.isLocked = true;

                        }

                    }
                }
            }
        }

	}

//    void GenerateAll(){
//        
//        DestroyAll();
//        Transform trans = cubePrefab.transform;
//        for (int i = 0; i < 9; i++)
//        {
//            for (int j = 0; j < 9; j++)
//            {
//                int randomInt = (int)Mathf.Round(Random.Range(0.0f, 5.0f));
//                GameObject instance = Instantiate(cubePrefab, trans.position+new Vector3((float)j,0.0f,-(float)i), trans.rotation);
//                instance.GetComponent<ElementCube>().setDestination(trans.position + new Vector3((float)j, 0.0f, -(float)i));
//                instance.GetComponent<ElementCube>().generatingManager = this.gameObject;
//                instance.GetComponent<ElementCube>().col = j;
//                (instance.GetComponent("ElementCube") as ElementCube).type=(Globals.ElementType)randomInt;
//
//                (instance.GetComponent("ElementCube") as ElementCube).ChangeColor();
//                Globals.cubeList.Add(instance);
//            }
//
//        }
//        this.CheckRepeat();
//       
//    }

    void DestroyAll(){

        int count = Globals.cubeList.Count;
        for (int i = 0; i < count; i++)
        {
            GameObject cur = Globals.cubeList[0];
            Globals.cubeList.RemoveAt(0);
            Destroy(cur);

        }
    }

//    void CheckRepeat(){
//        int RepeatCount = 0;
//        ElementCube current;
//        for (int i = 0; i < 9; i++)
//        {
//            RepeatCount = 0;
//            for (int j = 1; j < 9; j++)
//            {
//                if ((Globals.cubeList[i*9+j].GetComponent("ElementCube") as ElementCube).type == (Globals.cubeList[i*9+j-1].GetComponent("ElementCube") as ElementCube).type)
//                {
//                    RepeatCount += 1;
//                }
//                else
//                {
//                    RepeatCount = 0;
//                }
//                if (RepeatCount == 2)
//                {
//                    current = (Globals.cubeList[i*9+j-1].GetComponent("ElementCube") as ElementCube);
//                    current.type =(Globals.ElementType) ( ((int)current.type+1)%6);
//                    current.ChangeColor();
//                    RepeatCount = 0;
//
//                }
//
//            }
//        }
//
//        for (int j = 0; j < 9; j++)
//        {
//            RepeatCount = 0;
//            for (int i = 1; i < 9; i++)
//            {
//                if ((Globals.cubeList[i*9+j].GetComponent("ElementCube") as ElementCube).type == (Globals.cubeList[(i-1)*9+j].GetComponent("ElementCube") as ElementCube).type)
//                {
//                    RepeatCount += 1;
//                }
//                else
//                {
//                    RepeatCount = 0;
//                }
//                if (RepeatCount == 2)
//                {
//                    current = (Globals.cubeList[(i-1)*9+j].GetComponent("ElementCube") as ElementCube);
//                    current.type =(Globals.ElementType) ( ((int)current.type+1)%6);
//                    current.ChangeColor();
//                    RepeatCount = 0;
//
//                }
//
//            }
//        }
//    }

    public void playerSwapped(GameObject cube,int playerflag){
        HashSet<GameObject> curr = Globals.CheckThree(cube);
        foreach(GameObject c in curr){
            c.GetComponent<ElementCube>().ball.playerFlag = playerflag;
        }
        destroy.UnionWith(curr);
    }

//    private void respawn(int row, int col){
//        
//        Transform trans = cubePrefab.transform;
//        int randomInt = (int)Mathf.Round(Random.Range(0, 6));
//        GameObject instance = Instantiate(cubePrefab, trans.position+new Vector3((float)col,0.0f,3.0f), trans.rotation);
//        instance.GetComponent<ElementCube>().setDestination(trans.position + new Vector3((float)col, 0.0f, 2.0f));
//        instance.GetComponent<ElementCube>().generatingManager = this.gameObject;
//        (instance.GetComponent("ElementCube") as ElementCube).type=(Globals.ElementType)randomInt;
//        GameObject orb;
//        switch (instance.GetComponent<ElementCube>().type)
//        {
//            case Globals.ElementType.Light:
//                orb = Instantiate(lightPrefab);
//                orb.transform.SetParent(instance.transform);
//                break;
//            case Globals.ElementType.Fire:
//                orb = Instantiate(firePrefab);
//                orb.transform.SetParent(instance.transform);
//                break;
//            case Globals.ElementType.Water:
//                orb = Instantiate(waterPrefab);
//                orb.transform.SetParent(instance.transform);
//                break;
//            case Globals.ElementType.Wind:
//                orb = Instantiate(windPrefab);
//                orb.transform.SetParent(instance.transform);
//                break;
//            case Globals.ElementType.Earth:
//                orb = Instantiate(earthPrefab);
//                orb.transform.SetParent(instance.transform);
//                break;
//            case Globals.ElementType.Dark:
//                orb = Instantiate(darkPrefab);
//                orb.transform.SetParent(instance.transform);
//                break;
//        }
//
//
//        Globals.cubeList.Add(instance);
//    }

    private void generateBackup(){
        for (int i = 0; i < 9; i++)
        {
            if (Globals.backupCube[i] == null && Globals.CheckCubeNotExist(9,i))
            {
                int randomInt = (int)Mathf.Round(Random.Range(0.0f, 5.0f));
                GameObject instance = Instantiate(cubePrefab, cubePrefab.transform.position+new Vector3((float)i,0.0f,4.0f), cubePrefab.transform.rotation);
                Ball ball = new Ball((Globals.ElementType) randomInt, instance);
                instance.GetComponent<ElementCube>().ball = ball;
                instance.GetComponent<ElementCube>().generatingManager = this.gameObject;
                GameObject orb;
                switch (ball.type)
                {
                    case Globals.ElementType.Light:
                        orb = Instantiate(lightPrefab, cubePrefab.transform.position+new Vector3((float)i,1.5f,4.0f), cubePrefab.transform.rotation);
                        orb.transform.SetParent(instance.transform);
                        break;
                    case Globals.ElementType.Fire:
                        orb = Instantiate(firePrefab, cubePrefab.transform.position+new Vector3((float)i,1.5f,4.0f), cubePrefab.transform.rotation);
                        orb.transform.SetParent(instance.transform);
                        break;
                    case Globals.ElementType.Water:
                        orb = Instantiate(waterPrefab, cubePrefab.transform.position+new Vector3((float)i,1.5f,4.0f), cubePrefab.transform.rotation);
                        orb.transform.SetParent(instance.transform);
                        break;
                    case Globals.ElementType.Wind:
                        orb = Instantiate(windPrefab, cubePrefab.transform.position+new Vector3((float)i,1.5f,4.0f), cubePrefab.transform.rotation);
                        orb.transform.SetParent(instance.transform);
                        break;
                    case Globals.ElementType.Earth:
                        orb = Instantiate(earthPrefab, cubePrefab.transform.position+new Vector3((float)i,1.5f,4.0f), cubePrefab.transform.rotation);
                        orb.transform.SetParent(instance.transform);
                        break;
                    case Globals.ElementType.Dark:
                        orb = Instantiate(darkPrefab, cubePrefab.transform.position+new Vector3((float)i,1.5f,4.0f), cubePrefab.transform.rotation);
                        orb.transform.SetParent(instance.transform);
                        break;
                }

                Globals.backupCube[i]=instance;
            }
        }
    }


   

}

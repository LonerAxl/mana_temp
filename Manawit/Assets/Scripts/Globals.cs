using UnityEngine;
using System.Collections.Generic;

public static class Globals {
    public enum ElementType{Light,Fire, Water, Wind, Earth, Dark};

    public static List<GameObject> cubeList = new List<GameObject>(81);

    public static GameObject[] backupCube = new GameObject[9];

    public static GameObject FindCube(int row,int col) {
        if (row < 0 || row > 8 || col < 0 || col > 8)
        {
            return null;
        }
        for (int i = 0; i < cubeList.Count; i++) {
            GameObject cur = cubeList[i];

            if (cur.GetComponent<ElementCube>().ball.desCol==col && cur.GetComponent<ElementCube>().ball.desRow==row && !cur.GetComponent<ElementCube>().ball.isLocked) {

                return cubeList[i];
            }
        }
        return null;
    }

    public static bool CheckCubeNotExist(int rowl,int col) {
        if (cubeList.Count == 0)
        {
            return true;
        }

        foreach (GameObject cube in cubeList)
        {
            if (cube.GetComponent<ElementCube>().ball.desCol==col && cube.GetComponent<ElementCube>().ball.desRow>=rowl) {

                return false;
            }
        }
        return true;
    }

    public static void Swap(GameObject cube1, GameObject cube2, int a) {
        if (cube1 == null || cube2 == null || cube1.GetComponent<ElementCube>().ball.isLocked || cube2.GetComponent<ElementCube>().ball.isLocked)
        {
            return;
        }
        int tmpRow = cube1.GetComponent<ElementCube>().ball.desRow;
        int tmpCol = cube1.GetComponent<ElementCube>().ball.desCol;
        cube1.GetComponent<ElementCube>().ball.desRow = cube2.GetComponent<ElementCube>().ball.desRow;
        cube1.GetComponent<ElementCube>().ball.desCol = cube2.GetComponent<ElementCube>().ball.desCol;
        cube2.GetComponent<ElementCube>().ball.desRow = tmpRow;
        cube2.GetComponent<ElementCube>().ball.desCol = tmpCol;
        cube1.GetComponent<ElementCube>().ball.playerFlag = a;
        cube2.GetComponent<ElementCube>().ball.playerFlag = a;
        cube1.GetComponent<ElementCube>().ball.isLocked = true;
        cube2.GetComponent<ElementCube>().ball.isLocked = true;
    }

    public static Vector3 TranformPlayerToCube(Vector3 pos){
        Vector3 result = pos;
        result += new Vector3(0.5f, -1.0f, 0.5f);
        return result;
    }
        
    public static HashSet<GameObject> CheckThree(GameObject cube1){
        HashSet<GameObject> result = new HashSet<GameObject>();
        List<GameObject> row = getRow(cube1.GetComponent<ElementCube>().ball.desRow);
        List<GameObject> col = getCol(cube1.GetComponent<ElementCube>().ball.desCol);


        int curRow = cube1.GetComponent<ElementCube>().ball.desRow;
        int curCol = cube1.GetComponent<ElementCube>().ball.desCol;
        int start=-1,end=-1;

        foreach(GameObject cube in row){
            if (cube.GetComponent<ElementCube>().ball.desCol == curCol)
            {
                start = row.IndexOf(cube);
                end = row.IndexOf(cube);
                break;
            }
        }

        for (int i = start; i > 0; i--)
        {
            if (row.Count <= i || row[i - 1].GetComponent<ElementCube>().ball.desCol != row[i].GetComponent<ElementCube>().ball.desCol - 1 || row[i - 1].GetComponent<ElementCube>().ball.isLocked)
            {
                break;
            }
            else if (row[i - 1].GetComponent<ElementCube>().ball.type == row[i].GetComponent<ElementCube>().ball.type)
            {
                start -= 1;
            }
            else
            {
                break;
            }
        }
        for (int i = end; i < 8; i++)
        {
            if (row.Count<=i+1 ||row[i+1].GetComponent<ElementCube>().ball.desCol!=row[i].GetComponent<ElementCube>().ball.desCol+1 || row[i+1].GetComponent<ElementCube>().ball.isLocked){
                break;
            }else if(row[i+1].GetComponent<ElementCube>().ball.type==row[i].GetComponent<ElementCube>().ball.type){
                end += 1;
            }else
            {
                break;
            }
        }

        if (end - start >= 2)
        {
            for (int i = start; i <= end; i++)
            {
                result.Add(row[i]);
            }
        }



        foreach(GameObject cube in col){
            if (cube.GetComponent<ElementCube>().ball.desRow == curRow)
            {
                start = end = col.IndexOf(cube);
                break;
            }
        }
        for (int i = start; i > 0; i--)
        {
            if (col.Count<=i || col[i-1].GetComponent<ElementCube>().ball.desRow!=col[i].GetComponent<ElementCube>().ball.desRow-1 || col[i-1].GetComponent<ElementCube>().ball.isLocked){
                break;
            }else if(col[i-1].GetComponent<ElementCube>().ball.type==col[i].GetComponent<ElementCube>().ball.type){
                start -= 1;
            }else
            {
                break;
            }
        }
        for (int i = end; i < 8; i++)
        {
            if (col.Count<=i+1 ||col[i+1].GetComponent<ElementCube>().ball.desRow!=col[i].GetComponent<ElementCube>().ball.desRow+1 || col[i+1].GetComponent<ElementCube>().ball.isLocked){
                break;
            }else if(col[i+1].GetComponent<ElementCube>().ball.type==col[i].GetComponent<ElementCube>().ball.type){
                end += 1;
            }else
            {
                break;
            }
        }

        if (end - start >= 2)
        {
            for (int i = start; i <= end; i++)
            {
                result.Add(col[i]);
            }
        }


        return result;
    }




    private static List<GameObject> getRow(int row){
        List<GameObject> result = new List<GameObject>();
        for (int i = 0; i < cubeList.Count; i++) {
            if (cubeList[i].GetComponent<ElementCube>().ball.desRow==row) {
                result.Add(cubeList[i]);
            }
        }
        result.Sort(delegate(GameObject x, GameObject y)
            {
                return x.GetComponent<ElementCube>().ball.desCol.CompareTo(y.GetComponent<ElementCube>().ball.desCol);
            });
        return result;
    }

    public static List<GameObject> getCol(int col){
        List<GameObject> result = new List<GameObject>();
        for (int i = 0; i < cubeList.Count; i++) {
            if (cubeList[i].GetComponent<ElementCube>().ball.desCol==col) {
                result.Add(cubeList[i]);
            }
        }
        result.Sort(delegate(GameObject x, GameObject y)
            {
                return x.GetComponent<ElementCube>().ball.desRow.CompareTo(y.GetComponent<ElementCube>().ball.desRow);
            });
        return result;
    }



    public static int vectorToRow(Vector3 pos){
        return Mathf.RoundToInt((float)(pos.z+3));
    }

    public static int vectorToCol(Vector3 pos){
        return Mathf.RoundToInt((float)(pos.x+3));
    }
}

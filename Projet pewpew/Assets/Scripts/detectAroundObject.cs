using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrays : MonoBehaviour
{
    public GameObject cube;
    GameObject[,] objs = new GameObject[5, 5];

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                Vector3 pos = new Vector3(j * 1.5f, i * 1.5f, 0);
                GameObject go = (GameObject)Instantiate(cube, pos, Quaternion.identity);
                objs[j, i] = go;
            }
        }

        StartCoroutine(detectObjsAround(objs, 2, 2, 2));
    }

    IEnumerator detectObjsAround(GameObject[,] objs, int x, int y, int distance)
    {
        int maxX = x + distance;
        int minX = x - distance;
        int maxY = y + distance;
        int minY = y - distance;

        if (minX < 0)
            minX = 0;

        if (maxX > 4)
            maxX = 4;

        if (minY < 0)
            minY = 0;

        if (maxY > 4)
            maxY = 4;

        for (int i = minX; i <= maxX; i++)
        {
            for (int j = minY; j <= maxY; j++)
            {
                if (x != i || y != j)
                {
                    yield return new WaitForSeconds(0.2f);
                    objs[i, j].transform.position = new Vector3(i, j, 1);
                    objs[i, j].GetComponent<Renderer>().material.color = Color.red;
                }
                else
                {
                    objs[i, j].transform.position = new Vector3(i, j, 1);
                }
            }
        }
    }
}
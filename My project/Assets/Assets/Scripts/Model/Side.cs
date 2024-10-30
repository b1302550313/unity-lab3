using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Side
{
    public GameObject side;
    public int pastorCount, devilCount;
    public Side(Vector3 position)
    {
        side = GameObject.Instantiate(Resources.Load("prefab/Left", typeof(GameObject))) as GameObject;
        side.transform.localScale = new Vector3(8, 4.8f, 2);
        side.transform.position = position;
        pastorCount = devilCount = 0;
    }
}

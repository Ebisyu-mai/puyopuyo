using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextPuyo : MonoBehaviour
{
    public GameObject[] puyo;
    public bool nextGo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (nextGo)
        {
            nextGo = false;
            int rnd = Random.Range(1, 7);
            GameObject puyos = Instantiate(puyo[rnd - 1]) as GameObject;
            //GameObject puyos = Instantiate(puyo[1]) as GameObject;
            puyos.transform.position = new Vector3(0.5f, 5.5f, 0);
        }
    }
}

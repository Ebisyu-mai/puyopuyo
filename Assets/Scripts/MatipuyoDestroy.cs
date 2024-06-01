using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatipuyoDestroy : MonoBehaviour
{
    GameController gameController;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.matipuyoKesou)
        {
            gameController.matipuyoKesou = false;
            Destroy(this.gameObject);
        }
    }
}

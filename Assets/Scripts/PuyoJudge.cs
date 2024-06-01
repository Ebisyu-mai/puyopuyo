using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuyoJudge : MonoBehaviour
{
    GameController gameController;
    int myName;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        myName = int.Parse(this.name);
        //Debug.Log(myName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "blue")
        {
            gameController.puyoNum[myName] = 1;
        }
        if (collision.gameObject.tag == "pink")
        {
            gameController.puyoNum[myName] = 2;
        }
        if (collision.gameObject.tag == "yellow")
        {
            gameController.puyoNum[myName] = 3;
        }

        //青ぷよは１、ピンクぷよは２、黄色ぷよは３、何も入ってない場合は0をリストに挿入
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "blue" || collision.gameObject.tag == "pink" || collision.gameObject.tag == "yellow")
        {
            gameController.puyoNum[myName] = 0;
        }
    }
}

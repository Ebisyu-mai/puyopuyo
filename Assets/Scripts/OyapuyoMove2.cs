using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OyapuyoMove2 : MonoBehaviour
{
    bool wait = true;
    float waitSec = 1;
    public bool move = true;
    public GameObject puyo1;
    public string color1;
    public int puyo1Pos;
    public GameObject puyo2;
    public string color2;
    public int puyo2Pos;

    KopuyoController kopuyo1;
    KopuyoController kopuyo2;

    GameController gameController;

    public bool PosOk;
    public bool RotOk;


    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        kopuyo1 = puyo1.GetComponent<KopuyoController>();
        kopuyo2 = puyo2.GetComponent<KopuyoController>();
    }

    void Update()
    {
        puyo1Pos = kopuyo1.nowPos;
        puyo2Pos = kopuyo2.nowPos;

        if (move)
        {
            if (wait)
            {
                wait = false;
                StartCoroutine("DownSpeed");
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                waitSec = 0.2f;
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                puyoMoveHantei(1);
                if (PosOk) transform.position += new Vector3(1, 0, 0);
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                puyoMoveHantei(-1);
                if (PosOk) transform.position += new Vector3(-1, 0, 0);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                puyoMoveHantei(2);
                if (RotOk)
                {
                    transform.Rotate(new Vector3(0, 0, -90));
                    puyo1.transform.Rotate(new Vector3(0, 0, 90));
                    puyo2.transform.Rotate(new Vector3(0, 0, 90));
                }
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                puyoMoveHantei(-2);
                if (RotOk)
                {
                    transform.Rotate(new Vector3(0, 0, 90));
                    puyo1.transform.Rotate(new Vector3(0, 0, -90));
                    puyo2.transform.Rotate(new Vector3(0, 0, -90));
                }
            }

            if (puyo1Pos > 54 || puyo2Pos > 54 || gameController.puyoNum[puyo1Pos + 6] != 0 || gameController.puyoNum[puyo2Pos + 6] != 0) move = false;
        }
        else
        {
            puyo1.gameObject.tag = color1;
            puyo2.gameObject.tag = color2;
            gameController.puyoFall = true;
            this.gameObject.transform.DetachChildren();
            Destroy(this.gameObject);
        }
    }

    IEnumerator DownSpeed()
    {
        transform.position -= new Vector3(0, 1, 0);
        yield return new WaitForSeconds(waitSec);
        wait = true;
    }

    void puyoMoveHantei(int key)//keyが１なら右、ー１なら左、２ならD、−２ならA
    {
        if ((puyo1Pos % 6 == 0 || puyo2Pos % 6 == 0) && key == 1) PosOk = false;
        else if ((puyo1Pos % 6 == 1 || puyo2Pos % 6 == 1) && key == -1) PosOk = false;
        else if (gameController.puyoNum[puyo1Pos + key] != 0 || gameController.puyoNum[puyo2Pos + key] != 0) PosOk = false;
        else PosOk = true;

        RotOk = true;
        if (puyo1Pos - puyo2Pos == 6)
        {
            if (key == 2)
            {
                if (puyo1Pos % 6 == 1 && puyo2Pos % 6 == 1) RotOk = false;
                if (gameController.puyoNum[puyo1Pos - 1] != 0) RotOk = false;
            }
            else if (key == -2)
            {
                if (puyo2Pos % 6 == 0 && puyo2Pos % 6 == 0) RotOk = false;
                if (gameController.puyoNum[puyo1Pos + 1] != 0) RotOk = false;
            }
        }
        else if (puyo1Pos - puyo2Pos == -6)
        {
            if (key == 2)
            {
                if (puyo1Pos % 6 == 0 && puyo2Pos % 6 == 0) RotOk = false;
                if (gameController.puyoNum[puyo2Pos + 1] != 0) RotOk = false;
            }
            else if (key == -2)
            {
                if (puyo2Pos % 6 == 1 && puyo2Pos % 6 == 1) RotOk = false;
                if (gameController.puyoNum[puyo2Pos - 1] != 0) RotOk = false;
            }
            
            
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KopuyoController : MonoBehaviour
{
    bool wait;
    bool go;

    public int nowPos;
    int num = 0;
    GameController gameController;

    void Start()
    {
        wait = true;
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    void Update()
    {
        if (this.gameObject.transform.parent == null && nowPos < 55)
        {
            if (!go) StartCoroutine("goDown");
            if (gameController.puyoNum[nowPos + 6] == 0 && go && wait)
            {
                wait = false;
                StartCoroutine("DownSpeed");
            }
        }
    }

    IEnumerator DownSpeed()
    {
        if (transform.position.y > -4)
        {
            transform.position -= new Vector3(0, 1, 0);
            yield return new WaitForSeconds(0.05f);
            wait = true;
        }
    }

    IEnumerator goDown()
    {
        yield return new WaitForSeconds(0.1f);
        go = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "hantei" & int.TryParse(other.gameObject.name, out num))
        {
            nowPos = int.Parse(other.gameObject.name);
        }
    }
}

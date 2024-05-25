using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OyapuyoMove : MonoBehaviour
{
    bool wait = true;
    public bool move = true;
    public GameObject puyo1;
    public string color1;
    public GameObject puyo2;
    public string color2;

    public GameObject sita;
    public GameObject migi;
    public GameObject hidari;
    public GameObject ue;
    int muki = 0;

    bool hitLeftWall;
    bool hitRightWall;
    NextPuyo nextPuyo;
    // Start is called before the first frame update
    void Start()
    {
        nextPuyo = GameObject.Find("GameController").GetComponent<NextPuyo>();
        migi.SetActive(false);
        hidari.SetActive(false);
        ue.SetActive(false);
    }

    void Update()
    {
        if (move)
        {
            if (transform.position.y > -4 && wait)
            {
                wait = false;
                StartCoroutine("DownSpeed");
            }

            if (Input.GetKeyDown(KeyCode.RightArrow) && !hitRightWall)
            {
                transform.position += new Vector3(1, 0, 0);
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow) && !hitLeftWall)
            {
                transform.position += new Vector3(-1, 0, 0);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) && transform.position.y > -4)
            {
                transform.position += new Vector3(0, -1, 0);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                if ((muki == 0 && hitRightWall) || (muki == 2 && hitLeftWall))
                {
                    Debug.Log("その方向には回転できないよ");
                }
                else
                {
                    NextMuki(1);
                    transform.Rotate(new Vector3(0, 0, -90));
                    puyo1.transform.Rotate(new Vector3(0, 0, 90));
                    puyo2.transform.Rotate(new Vector3(0, 0, 90));
                    Kaiten(muki);
                }
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                if ((muki == 2 && hitRightWall) || (muki == 0 && hitLeftWall))
                {
                    Debug.Log("その方向には回転できないよ");
                }
                else
                {
                    NextMuki(-1);
                    transform.Rotate(new Vector3(0, 0, 90));
                    puyo1.transform.Rotate(new Vector3(0, 0, -90));
                    puyo2.transform.Rotate(new Vector3(0, 0, -90));
                    Kaiten(muki);
                }
            }
        }
        else
        {
            puyo1.gameObject.tag = color1;
            puyo2.gameObject.tag = color2;
            Destroy(ue.gameObject);
            Destroy(sita.gameObject);
            Destroy(migi.gameObject);
            Destroy(hidari.gameObject);
            nextPuyo.nextGo = true;
            this.gameObject.transform.DetachChildren();
            Destroy(this.gameObject);
        }
    }

    IEnumerator DownSpeed()
    {
        transform.position -= new Vector3 (0, 1, 0);
        yield return new WaitForSeconds(1);
        wait = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "blue" || other.gameObject.tag == "yellow" || other.gameObject.tag == "pink" || other.gameObject.tag == "soko")
        {
            move = false;
        }

        if (other.gameObject.tag == "rightwall")
        {
            hitRightWall = true;
        }

        if (other.gameObject.tag == "leftwall")
        {
            hitLeftWall = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "rightwall")
        {
            hitRightWall = false;
        }

        if (collision.gameObject.tag == "leftwall")
        {
            hitLeftWall = false;
        }
    }

    void Kaiten(int nowRot)
    {
        //Debug.Log(nowRot);
        if (nowRot == 0)
        {
            migi.SetActive(false);
            hidari.SetActive(false);
            ue.SetActive(false);
            sita.SetActive(true);
        }
        else if (nowRot == 2)
        {
            migi.SetActive(false);
            hidari.SetActive(false);
            ue.SetActive(true);
            sita.SetActive(false);
        }
        else if (nowRot == 3)
        {
            migi.SetActive(false);
            hidari.SetActive(true);
            ue.SetActive(false);
            sita.SetActive(false);
        }
        else if (nowRot == 1)
        {
            migi.SetActive(true);
            hidari.SetActive(false);
            ue.SetActive(false);
            sita.SetActive(false);
        }
    }

    void NextMuki(int arrow)
    {
        muki += arrow;

        if (muki == 4)
        {
            muki = 0;
        }
        else if (muki == -1)
        {
            muki = 3;
        }
    }
}

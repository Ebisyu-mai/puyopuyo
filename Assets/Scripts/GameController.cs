using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int[] puyoNum;
    public bool nextGo; //次のぷよを落とす
    public bool puyoHantei; //消えるぷよがないか確認
    public bool puyoKesou;
    public bool puyoFall; //落ちるぷよがないか確認
    public int madaOtitenai = 0;

    public GameObject[] puyo;
    public GameObject[] matiPuyo;
    public int[] destroyPuyo;
    int nextpuyo;
    public bool matipuyoKesou;

    void Start()
    {
        nextGo = true;
        puyoHantei = false;
        puyoFall = false;
        nextpuyo = Random.Range(1, 7);
    }


    void Update()
    {
        if (nextGo)
        {
            nextGo = false;
            GameObject puyos = Instantiate(puyo[nextpuyo - 1]) as GameObject;
            //GameObject puyos = Instantiate(puyo[1]) as GameObject;
            puyos.transform.position = new Vector3(0.5f, 4.5f, 0);
            nextpuyo = Random.Range(1, 7);
            GameObject matiPuyos = Instantiate(matiPuyo[nextpuyo - 1]) as GameObject;
            matiPuyos.transform.position = new Vector3(4.5f, 1.5f, 0);
        }

        if (puyoFall)
        {
            puyoFall = false;
            madaOtitenai = 0;
            for (int i = 1; i < puyoNum.Length - 5; i++)
            {
                if (puyoNum[i] != 0 && puyoNum[i + 6] == 0)
                {
                    puyoFall = true;
                    madaOtitenai++;
                }
            }

            if (madaOtitenai == 0)
            {
                StartCoroutine("tyottoMatsuyo");
            }
        }

        if (puyoHantei)
        {
            puyoHantei = false;
            
        }
    }

    IEnumerator tyottoMatsuyo()
    {
        yield return new WaitForSeconds(1);
        puyoHantei = true;
        matipuyoKesou = true;
    }

    void puyoCheck()
    {
        for (int i = 1; i < puyoNum.Length - 5; i++)
        {
            int num = 0;
            destroyPuyo[num] = i;
            if (puyoNum[i] != 0)
            {
                int n = 1;
                num = 1;




                while (puyoNum[i] == puyoNum[i + n] && puyoNum[i + n] % 6 != 1)
                {
                    n++;
                    destroyPuyo[num] = i + n;
                }

                while (n == 0)
                {
                    if (puyoNum[i] == puyoNum[i + n + 6])
                    {

                    }
                }

                if (puyoNum[i] % 6 != 0 && puyoNum[i] == puyoNum[i + 1])
                {

                }
            }
        }
    }
}

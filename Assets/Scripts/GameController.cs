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
    public List<int> destroyPuyo = new List<int>(); //実際に消すぷよのIDを入れとく
    public List<int> provisionalDestroyPuyo = new List<int>(); //一次的に同じ色のぷよを入れとく
    public int[] finishPuyo; //ぷよがつながってるか確認済みのリスト 0ならまだ、1なら探索済み
    public int destroyPuyoNum = 0;
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
            //GameObject puyos = Instantiate(puyo[0]) as GameObject;
            puyos.transform.position = new Vector3(0.5f, 4.5f, 0);
            nextpuyo = Random.Range(1, 7);
            GameObject matiPuyos = Instantiate(matiPuyo[nextpuyo - 1]) as GameObject;
            matiPuyos.transform.position = new Vector3(4.5f, 1.5f, 0);
        }

        if (puyoFall)
        {
            puyoFall = false;
            madaOtitenai = 0;
            for (int i = 1; i < puyoNum.Length - 6; i++)
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
            for (int i = 1; i < finishPuyo.Length; i++)
            {
                finishPuyo[i] = 0;
            }

            for (int j = 1; j < puyoNum.Length; j++)
            {
                puyoCheck(j, puyoNum[j]);
                if (provisionalDestroyPuyo.Count > 3)
                {
                   destroyPuyo.AddRange(provisionalDestroyPuyo);
                }
                provisionalDestroyPuyo.Clear();
            }

            if (destroyPuyo.Count == 0)
            {
                matipuyoKesou = true;
                nextGo = true;
            }
            else
            {
                puyoKesou = true;
            }
        }

        if (puyoKesou && destroyPuyo.Count == destroyPuyoNum)
        {
            destroyPuyo.Clear();
            puyoKesou = false;
            puyoFall = true;
            destroyPuyoNum = 0;
        }
    }

    IEnumerator tyottoMatsuyo()
    {
        yield return new WaitForSeconds(0.5f);
        puyoHantei = true; 
    }

    void puyoCheck(int num, int color)
    {
        if (finishPuyo[num] == 0)
        {
            finishPuyo[num] = 1;
            if (color != 0)
            {
                provisionalDestroyPuyo.Add(num);
                if (num % 6 != 0 && puyoNum[num + 1] == color) puyoCheck(num + 1, color);
                if (num < 55 && puyoNum[num + 6] == color) puyoCheck(num + 6, color);
                if (num % 6 != 1 && puyoNum[num - 1] == color) puyoCheck(num - 1, color);
                if (num > 6 && puyoNum[num - 6] == color) puyoCheck(num - 6, color);
            }
        }
    }
}

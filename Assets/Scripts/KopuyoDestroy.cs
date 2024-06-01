using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KopuyoDestroy : MonoBehaviour
{
    GameController gameController;
    KopuyoController kopuyoController;
    bool konopuyoKesuyo;
    bool fadeStart;
    int indexNum;
    float fadeSpeed = 0.01f;
    float nowAlpha;
    SpriteRenderer sprite;
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        kopuyoController = GetComponent<KopuyoController>();
        sprite = GetComponent<SpriteRenderer>();
        fadeStart = false;
    }


    void Update()
    {
        if (gameController.puyoKesou)
        {
            for (int i = 0; i < gameController.destroyPuyo.Count; i++)
            {
                if (kopuyoController.nowPos == gameController.destroyPuyo[i])
                {
                    konopuyoKesuyo = true;
                    indexNum = i;
                }
                    
            }
        }

        if (konopuyoKesuyo && !fadeStart)
        {
            fadeStart = true;
            nowAlpha = sprite.color.a;
            StartCoroutine("PuyoSayonara");
        }

    }

    IEnumerator PuyoSayonara()
    {
        for (int j = 0; j < 2; j++)
        {
            for (int i = 0; i < 80; i++)
            {
                nowAlpha -= fadeSpeed;
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, nowAlpha);
                yield return new WaitForSeconds(0.001f);
            }
            for (int i = 0; i < 80; i++)
            {
                nowAlpha += fadeSpeed;
                sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, nowAlpha);
                yield return new WaitForSeconds(0.001f);
            }
        }
        gameController.destroyPuyoNum++;
        Destroy(this.gameObject);

    }
}

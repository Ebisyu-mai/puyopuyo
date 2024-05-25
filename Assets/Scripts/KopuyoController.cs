using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KopuyoController : MonoBehaviour
{
    bool wait;
    bool sita;
    bool go;

    void Start()
    {
        sita = false;
        wait = true;
    }

    void Update()
    {
        //Debug.Log(sita);
        if (this.gameObject.transform.parent == null && !go)
        {
            StartCoroutine("goDown");
        }

        if (!sita && wait && go)
        {
            wait = false;
            StartCoroutine("DownSpeed");
        }

        if (Input.GetKeyDown(KeyCode.B) && this.gameObject.name == "bluepuyo")
        {
            Destroy(this.gameObject);
        }
    }

    IEnumerator DownSpeed()
    {
        if (transform.position.y > -4)
        {
            transform.position -= new Vector3(0, 1, 0);
            yield return new WaitForSeconds(0.1f);
            wait = true;
        }
        
    }

    IEnumerator goDown()
    {
        yield return new WaitForSeconds(0.5f);
        go = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "blue" || other.gameObject.tag == "yellow" || other.gameObject.tag == "pink" || other.gameObject.tag == "soko")
        {
            sita = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "blue" || other.gameObject.tag == "yellow" || other.gameObject.tag == "pink")
        {
            sita = false;
        }
    }
}

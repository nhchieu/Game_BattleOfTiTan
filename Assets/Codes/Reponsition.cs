using System;

using UnityEngine;
using UnityEngine.EventSystems;


public class Reponsition : MonoBehaviour
{
    Collider2D coll;

    private void Awake()
    {
        coll = GetComponent<Collider2D>();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (!collision.CompareTag("Area"))
            return;
        Vector3 playerPos =GameManager.instance.player.transform.position;
        Vector3 myPos=transform.position;
        float diffX = Mathf.Abs(playerPos.x - myPos.x);
        float diffY = Mathf.Abs(playerPos.y - myPos.y);
        Vector3 playerDir = GameManager.instance.player.inputVec;
        float dirX = playerDir.x > 0 ? 1 : -1;
        float dirY = playerDir.y > 0 ? 1 : -1;
        float diffx = Mathf.Abs(dirX);
        float diffy = Mathf.Abs(dirY);
        dirX = dirX > 0 ? 1 : -1;
        dirY = dirY > 0 ? 1 : -1;

        switch (transform.tag)
        {
            case "Ground":
                if (diffX>diffY)
                {
                    transform.Translate(Vector3.right*dirX*112); 
                }
                else if (diffX < diffY)
                {
                    transform.Translate(Vector3.up * dirY* 112);
                }
                break;
            case "Enemy":
                if (coll.enabled)
                {
                    System.Random rand = new System.Random();

                    transform.Translate(playerDir * 45 + new Vector3((float) rand.Next(-3, 4), (float) rand.Next(-3,4), 0f));
                }
                break;
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RePostion : MonoBehaviour
{
    private Collider2D _coll;

    void Awake()
    {
        _coll = GetComponent<Collider2D>();
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area")) { return; }

        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 myPos = transform.position;

        switch(transform.tag)
        {
            case "Background":
                ReBackground(playerPos, myPos);
                break;
            case "Enemy":
                ReEnemy(playerPos, myPos);
                break;
        }
    }
    void ReBackground(Vector3 playerPos, Vector3 myPos)
    {
        float gapX = playerPos.x - myPos.x;
        float gapY = playerPos.y - myPos.y;
        float dirX = gapX < 0 ? -1 : 1;
        float dirY = gapY < 0 ? -1 : 1;

        gapX = Mathf.Abs(gapX);
        gapY = Mathf.Abs(gapY);

        if (gapX > gapY)
        {
            transform.Translate(Vector3.right * dirX * 80);
        }
        else if (gapX < gapY)
        {
            transform.Translate(Vector3.up * dirY * 80);
        }
    }

    void ReEnemy(Vector3 playerPos, Vector3 myPos)
    {
        if (!_coll.enabled) { return; }
        Vector3 dist = playerPos - myPos;
        Vector3 ran = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0f);
        transform.Translate(ran + dist * 2);
    }
}

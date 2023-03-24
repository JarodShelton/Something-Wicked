using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowIndicator : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        GameObject closestPile = GetClosestPile();
        if (closestPile != null)
        {
            if (closestPile.GetComponent<Renderer>().isVisible)
            {
                GetComponentInChildren<SpriteRenderer>().enabled = false;
            }
            else
            {
                GetComponentInChildren<SpriteRenderer>().enabled = true;

                Vector3 targ = closestPile.transform.position;
                targ.z = 0f;

                Vector3 objectPos = transform.position;
                targ.x = targ.x - objectPos.x;
                targ.y = targ.y - objectPos.y;

                float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg - 90;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }
        }

    }

    GameObject GetClosestPile()
    {
        CandyPileController[] Piles = FindObjectsOfType<CandyPileController>();

        if (Piles.Length > 0)
        {
            GameObject nearestPile = Piles[0].gameObject;

            foreach (CandyPileController pile in Piles)
            {
                if (Vector2.Distance(transform.position, pile.gameObject.transform.position) < (Vector2.Distance(transform.position, nearestPile.transform.position)))
                {
                    nearestPile = pile.gameObject;
                }
            }
            return nearestPile;
        }
        else
        {
            return null;
        }

    }
}

using UnityEngine;
using System.Collections;

public class SpriteSorter : MonoBehaviour
{
    void LateUpdate()
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        int sortingOrder = (int)Camera.main.WorldToScreenPoint(spriteRenderer.bounds.min).y * -1;
        spriteRenderer.sortingOrder = sortingOrder;
        Debug.Log(sortingOrder);
    }
}

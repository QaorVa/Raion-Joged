using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Transform Bar;

    // Start is called before the first frame update
    private void Awake()
    {
        Bar = transform.Find("Bar");
    }

    public void SetSize(float sizeNormalized)
    {
        Bar.localScale = new Vector3(sizeNormalized, 1f);
    }

    public void SetColor(Color color)
    {
        Bar.Find("BarSprite").GetComponent<SpriteRenderer>().color = color;
    }
}

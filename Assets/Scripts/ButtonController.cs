using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private SpriteRenderer sr;
    public Sprite defaultImg;
    public Sprite pressedImg;

    public KeyCode keyToPress;

    // Start is called before the first frame update
    void Start()
    {
        // memanggil sprite renderer dari button
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.gameIsPaused){
        // merubah sprite ketika sebuah tombol ditekan
            if (Input.GetKeyDown(keyToPress))
            {
                sr.sprite = pressedImg;
            }

        // merubah sprite ketika sebuah tombol dilepaskan
            if (Input.GetKeyUp(keyToPress))
            {
                sr.sprite = defaultImg;
            }
        }
    }
}

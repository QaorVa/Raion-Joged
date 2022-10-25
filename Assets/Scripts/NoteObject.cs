using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{

    public bool canBePressed;
    public KeyCode keyToPress;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ngilangin panah note pas berhasil dipencet
        if(Input.GetKeyDown(keyToPress))
        {
            if(canBePressed)
            {
                gameObject.SetActive(false);

                GameManager.instance.noteHit();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // ngecek kalo panah udah masuk kotak buat dipencet
        if(other.tag == "Activator")
        {
            canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // ngecek kalo panah udah keluar kotak buat dipencet
        if (other.tag == "Activator" && gameObject.activeSelf)
        {
            canBePressed = false;

            GameManager.instance.noteMissed();
        }
    }
}

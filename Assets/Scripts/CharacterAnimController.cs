using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimController : MonoBehaviour
{
    
    private Animator anim;
    public ButtonController[] button = new ButtonController[4];
    private string[] direction = {"Left", "Down", "Up", "Right"};
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update() {
        if(!GameManager.gameIsPaused){
        // merubah character ketika sebuah tombol ditekan

            for (int i = 0; i < 4; i++) 
            {
                if (Input.GetKeyDown(button[i].keyToPress))
                {
                    anim.SetBool(direction[i], true);
                }
        // merubah character ketika sebuah tombol dilepaskan
                if (Input.GetKeyUp(button[i].keyToPress))
                {
                    anim.SetBool(direction[i], false);
                }
            }

        // merubah sprite ketika sebuah tombol dilepaskan
            
        }
    }
}

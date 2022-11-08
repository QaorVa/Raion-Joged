using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    public float beatTempo;
    public bool hasStarted;


    // Start is called before the first frame update
    void Start()
    {
        // beat tempo per second
        beatTempo = beatTempo / 60f;
    }

    // Update is called once per frame
    void Update()
    {
        // pencet any key buat mulai game
        
        if(!hasStarted)
        {
            /*
            if(Input.anyKeyDown)
            {
                hasStarted = true;
            }
            */
        }
        else // jalanin scroller panah
        {
            //transform.position += new Vector3(0f, beatTempo * Time.deltaTime, 0f);
            //transform.localPosition = Vector3.Lerp(Vector3.up * SongManager.Instance.noteSpawnY, Vector3.up * SongManager.Instance.noteDespawnY, t);
        }
    }
}

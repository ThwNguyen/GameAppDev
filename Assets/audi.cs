using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audi : MonoBehaviour
{
    // Start is called before the first frame update
    private bool mute = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeMute() {
        mute = !mute;
        if (mute) {
            AudioListener.volume = 1;
        } else {

        }
    }
}

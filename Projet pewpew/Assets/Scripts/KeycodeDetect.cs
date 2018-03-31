using System;
using UnityEngine;

public class detect : MonoBehaviour {
    
	void Update ()
    {
		foreach(KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyDown(kcode))
                print("Keycode pressed : " + kcode);
        }
	}

}

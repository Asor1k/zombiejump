using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggercs : MonoBehaviour {

   public Phisics phisics;

	void Start () {
        phisics = FindObjectOfType<Phisics>();
	}

    private void OnMouseDown()
    {
        phisics.ButtonDown();
    }

    private void OnMouseUp()
    {
        phisics.ButtonUp();
    }
}

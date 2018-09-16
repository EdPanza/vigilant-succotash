using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerjump : MonoBehaviour {

    private PlayerController player;

	// Use this for initialization
	void Start () {
        player = gameObject.GetComponentInParent<PlayerController>();
        player.grounded = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionStay2D(Collision2D collision)
    {
        player.grounded = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        player.grounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        player.grounded = false;
    }
}

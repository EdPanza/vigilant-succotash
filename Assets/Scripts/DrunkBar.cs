using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrunkBar : MonoBehaviour {

    private Slider drunkBar;
    private int currentDrunk = 0;

    private void Awake()
    {
        drunkBar = GetComponent<Slider>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        drunkBar.value = currentDrunk;
	}

    public void changeDrunk(int dP)
    {
        currentDrunk += dP;
    }
}

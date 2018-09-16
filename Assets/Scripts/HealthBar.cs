using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    private Slider healthBar;
    private int currentHP = 100;

    private void Awake()
    {
        healthBar = GetComponent<Slider>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = currentHP;
    }

    public void changeHealth(int dP)
    {
        currentHP += dP;
    }
}

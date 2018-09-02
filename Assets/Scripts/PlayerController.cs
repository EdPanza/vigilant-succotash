﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public float runSpeed;
    public float jumpForce;

    private int Surprise;
    private int drunk;
    private int count;
    private Rigidbody2D playerBody;
    private DrunkBar drunk_controller;
    private HealthBar health_controller;
    private const string Drink_Tag = "DrinkTag";
    private const string Enemy_Tag = "EnemyGuard";
    private Text timer;

    Texture2D blk;
    public bool fade;
    public float alph;


    // Use this for initialization
    void Start () {
        blk = new Texture2D(1, 1);
        blk.SetPixel(0, 0, new Color(0, 0, 0, 0));
        blk.Apply();
    }

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), blk);
    }

    private void Awake()
    {
        playerBody = this.gameObject.GetComponent<Rigidbody2D>();
        drunk_controller = GameObject.Find("DrunkBar").GetComponent<DrunkBar>();
        health_controller = GameObject.Find("Canvas/HealthBar").GetComponent<HealthBar>();
        timer = GameObject.Find("Canvas/Timer").GetComponent<Text>();
        this.gameObject.AddComponent<FadeOutCamera>();

        drunk = 0;
        count = 0;
        Surprise = 0;
    }

    // Update is called once per frame
    void Update()
    {


        if (drunk == 0)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                //print("up arrow key is held down");
                playerBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }

            if (Input.GetKey("down"))
            {
                print("down arrow key is held down");
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                //print("left arrow key is held down");
                this.transform.Translate(Vector3.left * runSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                this.transform.Translate(Vector3.right * runSpeed * Time.deltaTime);
            }
        }

        if (drunk == 1)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                print("up arrow key is held down");
            }

            if (Input.GetKey("down"))
            {
                //print("down arrow key is held down");
                playerBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                //print("left arrow key is held down");
                this.transform.Translate(Vector3.right * runSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.RightArrow))
            {
                this.transform.Translate(Vector3.left * runSpeed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case Drink_Tag:
                consumeDrink(collision.gameObject);
                break;
            case Enemy_Tag:
                InstantDeath(collision.gameObject);
                break;
        }
    }

    private void consumeDrink(GameObject drink)
    {
        //print("entro en esta joda");
        GameObject.Destroy(drink);
        count = count + 1;
        drunk_controller.changeDrunk(1);

        if (count == 3){

            timer.text = "10";
            timer.enabled = true;

            Surprise = Random.Range(1,4);
            //Surprise = 2;

            if (Surprise == 1){
                InvokeRepeating("decreaseTime", 1f, 1f);
                drunk = 1;
            }

            if(Surprise == 2){
                //print("Aqui es donde se debe apagar la pantalla");
                this.gameObject.GetComponent<FadeOutCamera>().isDrunk = true;
                InvokeRepeating("decreaseTime", 1f, 1f);
            }
            if (Surprise == 3)
            {
                print("Aqui es donde da super poder");
                drunk_controller.changeDrunk(-3);
                count = 0;
            }
        }
    }


    private void sober() {
        drunk = 0;
        drunk_controller.changeDrunk(-3);
        count = 0;
        this.gameObject.GetComponent<FadeOutCamera>().isDrunk = false;

    }

    private void decreaseTime() {
        Debug.Log("Are you working?" + Surprise);
        int value;
        int.TryParse (timer.text, out value);

        value -= 1;

        timer.text = value.ToString();

        if((value == 0) & (Surprise == 1)) 
        {
            sober();
            CancelInvoke("decreaseTime");
            timer.enabled = false;
        }
        if ((value == 0) & (Surprise == 2))
        {
            sober();
            CancelInvoke("decreaseTime");
            timer.enabled = false;
        }
    }

    private void InstantDeath(GameObject guard){
        //print("ENTRO EN ESTA JODA - " + health_controller);
        health_controller.changeHealth(-100);

    }
}
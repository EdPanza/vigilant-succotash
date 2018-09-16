using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public float runSpeed;
    public float jumpForce;
    public bool grounded;

    private int Surprise;
    private int drunk;
    private int count;
    private Rigidbody2D playerBody;
    private Animator Animator;
    private DrunkBar drunk_controller;
    private HealthBar health_controller;
    private SpriteRenderer Ned;
    private const string Drink_Tag = "DrinkTag";
    private const string Enemy_Tag = "EnemyGuard";
    private const string Kill_Tag = "eche";
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
        Animator = this.gameObject.GetComponent<Animator>();
        drunk_controller = GameObject.Find("DrunkBar").GetComponent<DrunkBar>();
        health_controller = GameObject.Find("Canvas/HealthBar").GetComponent<HealthBar>();
        timer = GameObject.Find("Canvas/Timer").GetComponent<Text>();
        this.gameObject.AddComponent<FadeOutCamera>();
        Ned = this.gameObject.GetComponent<SpriteRenderer>();

        drunk = 0;
        count = 0;
        Surprise = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Animator.SetBool("Moving", false);

        if (drunk == 0)
        {   

            //Tecla de la izquierda
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                //print("left arrow key is held down");
                Animator.SetBool("Moving", true);
                Ned.flipX = true;
                this.transform.Translate(Vector3.left * runSpeed * Time.deltaTime);
            }

            //Tecla de la derecha
            if (Input.GetKey(KeyCode.RightArrow))
            {
                Ned.flipX = false;
                Animator.SetBool("Moving", true);
                this.transform.Translate(Vector3.right * runSpeed * Time.deltaTime);
            }

            //Tecla de arriba
            if(Input.GetKeyDown(KeyCode.UpArrow) && grounded)
            {
                //playerBody.AddForce(new Vector2(0, 10), ForceMode2D.Impulse);
                //playerBody.AddForce(Vector2.up * jumpForce * Time.deltaTime);
                playerBody.AddForce(Vector2.up * jumpForce);
            }

        }

        if (drunk == 1)
        {

            //Tecla de la izquierda
            if (Input.GetKey(KeyCode.RightArrow))
            {
                //print("left arrow key is held down");
                Animator.SetBool("Moving", true);
                Ned.flipX = true;
                this.transform.Translate(Vector3.left * runSpeed * Time.deltaTime);
            }

            //Tecla de la derecha
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                Ned.flipX = false;
                Animator.SetBool("Moving", true);
                this.transform.Translate(Vector3.right * runSpeed * Time.deltaTime);
            }

            //Tecla de arriba
            if (Input.GetKeyDown(KeyCode.DownArrow) && grounded)
            {
                playerBody.AddForce(Vector2.up * jumpForce);
            }

        }

    }


    // Esta ees la funcion para determinar cuando toca un collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case Drink_Tag:
                consumeDrink(collision.gameObject);
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case Kill_Tag:
                DyingEnemy(collision.gameObject);
                break;
            case Enemy_Tag:
                InstantDeath(collision.gameObject);
                break;
        }
    }


    private void DyingEnemy(GameObject enemy)
    {
        Debug.Log("Entro en esta joda");
        GameObject.Destroy(enemy);
    }




    // Funcion para hacer lo aleatorio /////////////////////////////////////


    private void consumeDrink(GameObject drink)
    {
        //print("entro en esta joda");
        GameObject.Destroy(drink);
        count = count + 1;
        drunk_controller.changeDrunk(1);

        if (count == 3)
        {

            //Surprise = Random.Range(1,5);
            Surprise = 1;

            if (Surprise == 1){
                timer.text = "10";
                timer.enabled = true;
                InvokeRepeating("decreaseTime", 1f, 1f);
                drunk = 1;
            }

            if(Surprise == 2){
                timer.text = "10";
                timer.enabled = true;
                this.gameObject.GetComponent<FadeOutCamera>().isDrunk = true;
                InvokeRepeating("decreaseTime", 1f, 1f);
            }
            if (Surprise == 3)
            {
                timer.text = "10";
                timer.enabled = true;
                print("Aqui debe buscar otro trago para mantener la pea");
                InvokeRepeating("decreaseTime", 1f, 1f);

            }

            if (Surprise == 4)
            {
                //Empieza el laberinto
                SceneManager.LoadScene("Dungeon", LoadSceneMode.Single);
            }

            if (Surprise == 5)
            {
                timer.text = "10";
                timer.enabled = true;
                print("Aqui es donde da super poder");
                drunk_controller.changeDrunk(-3);
                count = 0;
            }

        }
        if((count == 4)&&(Surprise ==3))
        {
            sober();
            CancelInvoke("decreaseTime");
            timer.enabled = false;
            
        }
    }


    //Vuelve a la normalidad

    private void sober() {
        drunk = 0;
        drunk_controller.changeDrunk(-3);
        count = 0;
        this.gameObject.GetComponent<FadeOutCamera>().isDrunk = false;

    }

    //Todo demora 10 segundos

    private void decreaseTime() {
        Debug.Log("Are you working?" + " " + Surprise);
        int value;
        int.TryParse (timer.text, out value);

        value -= 1;

        timer.text = value.ToString();


        if ((count == 3) && (Surprise == 3))
        {
            health_controller.changeHealth(-10);
        }

        if ((value == 0) & (Surprise == 1)) 
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

    //Cuando toca el guardia muere de una vez.

    private void InstantDeath(GameObject guard){
        print("ENTRO EN ESTA JODA de muerte");
        health_controller.changeHealth(-100);
    }
}

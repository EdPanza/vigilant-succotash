using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerDungeon : MonoBehaviour {


    public float runSpeed;
    public float jumpForce;

    private Rigidbody2D playerBody;
    private const string Tag = "FinishDungeon";


    void Awake()
    {
        playerBody = this.gameObject.GetComponent<Rigidbody2D>();
    }


    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

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

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            //print("up arrow key is held down");
            playerBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case Tag:
                ReturnToGame();
                break;
        }
    }

    private void ReturnToGame()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
}

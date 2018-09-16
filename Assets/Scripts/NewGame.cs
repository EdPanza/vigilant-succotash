using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour {


    public Button NewGameButton;

    // Use this for initialization
    void Start () {

        Button btn1 = NewGameButton.GetComponent<Button>();


        //Calls the TaskOnClick/TaskWithParameters method when you click the Button
        btn1.onClick.AddListener(TaskOnClick);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void TaskOnClick()
    {
        //Output this to console when the Button is clicked
        //Debug.Log("ECHE ESTA FUNCIONANDO");
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);

    }



}

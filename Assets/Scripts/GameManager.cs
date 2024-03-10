using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int ectsCount;
    public TextMeshProUGUI ectsText;
    public TextMeshProUGUI levelText;
    public bool bothFinished;

    void Start()
    {

    }

    void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name == "level1" || scene.name == "level2" || scene.name == "level3")
        {
            levelText.text = "year " + getYear();
            ectsText.text = "ects: " + ectsCount.ToString();
        }

        if(Input.GetButtonDown("Submit"))
        {
            Debug.Log("OK");
            if(scene.name == "start")
            {
                SceneManager.LoadScene("level1");
            }
            else if(scene.name == "afterLevel1")
            {
                SceneManager.LoadScene("level2");
            }
            else if(scene.name == "afterLevel2")
            {
                SceneManager.LoadScene("level3");
            }
            else
            {

            }
        }
        

        if(bothFinished)
        {
            if(scene.name == "level1")
            {
                SceneManager.LoadScene("afterLevel1");
            }
            else if(scene.name == "level2")
            {
                SceneManager.LoadScene("afterLevel2");
            }
            else
            {
                SceneManager.LoadScene("end");
            }
        }
        if(Input.GetButtonDown("Reload"))
        {
            ReloadScene();
        }
    }

    public void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
    }

    private string getYear()
    {
        string name = SceneManager.GetActiveScene().name;
        string lastCharacter = name.Substring(name.Length-1);
        return lastCharacter;
    }
}

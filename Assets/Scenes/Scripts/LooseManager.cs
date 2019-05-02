using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LooseManager : MonoBehaviour {
    public Button resetButon;
    public Text scoretxt;
    public Text bestScoretxt;
    PlatformGenerator generator;

    private void Start()
    {
        generator = FindObjectOfType<PlatformGenerator>();
    }
    

    internal void Loose()
    {
        resetButon.gameObject.SetActive(true);
        scoretxt.gameObject.SetActive(true);
        bestScoretxt.gameObject.SetActive(true);
        scoretxt.text = generator.score.ToString();
        bestScoretxt.text = "Best score: "+PlayerPrefs.GetInt("maxScore").ToString();
    }
    

    public void ResetGame()
    {
        SceneManager.LoadScene(0);
    }


    void Update () {

		
	}
}

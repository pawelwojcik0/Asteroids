using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject GameOver;

    private int points;
    private int lifes;
    private HUD HUD;

    public int Points
    {
        get { return points; }
        set
        {
            points = value;
            HUD.UpdatePoints(points);
        }
    }

    public int Lifes
    {
        get { return lifes; }
        set
        {
            lifes = value;
            HUD.UpdateLifes(lifes);
        }
    }

    private void Start()
    {
        HUD = FindObjectOfType<HUD>();
        Points = 0;
        Lifes = 5;
    }

    private void Update()
    {
        if(GameOver.activeInHierarchy == true)
        {
            StartCoroutine(Restart());
        }
    }
    IEnumerator Restart()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("SampleScene");
    }
}

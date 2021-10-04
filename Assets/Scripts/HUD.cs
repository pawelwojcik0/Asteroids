using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class HUD : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI Points;
    [SerializeField] private GameObject GameOver;
    [SerializeField] private GameObject lifePrefab;
    [SerializeField] private int lifesAmount;
    [SerializeField] private Vector3 firstLifeInstantiatePosition;
    [SerializeField] private float lifesObjectsDistance;

    private List<Transform> lifesObjects = new List<Transform>();

    private void Start()
    {
        GameOver.SetActive(false);

        for(int i = 0; i < lifesAmount; i++)
        {
            Transform life = Instantiate(lifePrefab, new Vector3(firstLifeInstantiatePosition.x + (i * lifesObjectsDistance), firstLifeInstantiatePosition.y, firstLifeInstantiatePosition.z) , Quaternion.identity).transform;
            life.SetParent(transform);
            lifesObjects.Add(life);
        }
    }

    public void UpdatePoints(int points)
    {
        Points.text = "" + points;
    }

    public void UpdateLifes(int lifes)
    {
        Transform life = lifesObjects.LastOrDefault();
        life.transform.position = new Vector3(100f, 100f, 100f);
        lifesObjects.Remove(life);
    }
}

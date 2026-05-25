using System;
using UnityEngine;
using UnityEngine.Splines;

public class CivCarManager : MonoBehaviour
{
    [SerializeField] private GameObject carPrefab;
    [SerializeField] private SplineContainer[] paths;
    public int CarBaseAmount = 5;
    private int carCount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        paths = FindObjectsByType<SplineContainer>(FindObjectsSortMode.None);

        for(int i = 0; i < paths.Length; i++)
        {
            carCount = CarBaseAmount + paths[i].Spline.Count;

            for(int j = 0; j < carCount; j++)
            {
                GameObject car = Instantiate(carPrefab, transform);
                SplineAnimate follower = car.GetComponent<SplineAnimate>();
                follower.Container = paths[i];
                follower.StartOffset = (float)j / carCount;
                follower.Loop = SplineAnimate.LoopMode.Loop;
                follower.Play();
            }

            carCount = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

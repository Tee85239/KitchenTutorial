using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class PlateVisual : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private Transform counterTopPoint;
    [SerializeField]
    private Transform platePrefab;
    [SerializeField]
    private PlatesCounter platesCounter;

    private List<GameObject> plateVisualList;

    private void Awake()
    {
        plateVisualList = new List<GameObject>();
    }
    private void Start()
    {
        platesCounter.OnPlateSpawn += PlatesCounter_OnPlateSpawn;
        platesCounter.OnPlateRemove += PlatesCounter_OnPlateRemove;
    }

    private void PlatesCounter_OnPlateRemove(object sender, System.EventArgs e)
    {
        GameObject plateGameobject = plateVisualList[plateVisualList.Count - 1];
        plateVisualList.Remove(plateGameobject);
        Destroy(plateGameobject);
    }

    private void PlatesCounter_OnPlateSpawn(object sender, System.EventArgs e)
    {
       Transform plateVisualTransform = Instantiate(platePrefab, counterTopPoint);
        float plateoffSetY = 0.1f;
        plateVisualTransform.localPosition = new Vector3(0, plateoffSetY * plateVisualList.Count, 0);

        plateVisualList.Add(plateVisualTransform.gameObject);
    }
}

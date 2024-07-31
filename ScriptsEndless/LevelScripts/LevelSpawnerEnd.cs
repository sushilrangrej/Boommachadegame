using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelSpawnerEnd : MonoBehaviour
{

    public GameObject[] model;
    [HideInInspector]
    public GameObject[] modelPrefab = new GameObject[4];

    public GameObject temp1;

    public Transform playerTransform;
    private float tilesLength = 0.30f;
    private float spawnY =  1f;
    private int amtOfTilesOnScreen = 45;

    public Material plateMat, baseMat;
    public MeshRenderer playerMesh;

    [SerializeField][Range(0f, 1f)] float lerptime;

    [SerializeField] Color[] myColors;
    public int myColorIndex = 0;
    float t = 0f;


    void Awake()
    {
        ModelSelection();
        playerTransform = GameObject.FindGameObjectWithTag("Ball").transform;
        
        for(int i = 0; i < amtOfTilesOnScreen; i++)
        {
            spawnTile();
        }
    }

    void Update()
    {
        if (-playerTransform.position.y > (spawnY - amtOfTilesOnScreen * tilesLength))
        {
            spawnTile();
        }

        plateMat.color = Color.Lerp(plateMat.color, myColors[myColorIndex], lerptime * Time.deltaTime);

        t = Mathf.Lerp(t, 1f, lerptime * Time.deltaTime);
        if (t > 0.9f)
        {
            t = 0f;
            myColorIndex = Random.Range(0, 18);
            myColorIndex = (myColorIndex >= myColors.Length) ? 0 : myColorIndex;
        }

        baseMat.color = plateMat.color + Color.gray;
        playerMesh.material.color = plateMat.color;
    }

    private void spawnTile()
    {
        if (ScoreManagerEnd.instance.ScoreN <= 300)
        {
            temp1 = Instantiate(modelPrefab[Random.Range(0, 2)]) as GameObject;
        }
        if (ScoreManagerEnd.instance.ScoreN > 300 && ScoreManagerEnd.instance.ScoreN <= 400)
        {
            temp1 = Instantiate(modelPrefab[Random.Range(0, 3)]) as GameObject;
        }
        if (ScoreManagerEnd.instance.ScoreN > 400 && ScoreManagerEnd.instance.ScoreN <= 600)
        {
            temp1 = Instantiate(modelPrefab[Random.Range(0, 2)]) as GameObject;
        }
        if (ScoreManagerEnd.instance.ScoreN > 600 && ScoreManagerEnd.instance.ScoreN <= 700)
        {
            temp1 = Instantiate(modelPrefab[Random.Range(0, 4)]) as GameObject;
        }
        if (ScoreManagerEnd.instance.ScoreN > 700 && ScoreManagerEnd.instance.ScoreN <= 1000)
        {
            temp1 = Instantiate(modelPrefab[Random.Range(0, 2)]) as GameObject;
        }
        if (ScoreManagerEnd.instance.ScoreN > 1000 && ScoreManagerEnd.instance.ScoreN <= 1100)
        {
            temp1 = Instantiate(modelPrefab[Random.Range(0, 4)]) as GameObject;
        }
        if (ScoreManagerEnd.instance.ScoreN > 1100 && ScoreManagerEnd.instance.ScoreN <= 1400)
        {
            temp1 = Instantiate(modelPrefab[Random.Range(0, 2)]) as GameObject;
        }
        if (ScoreManagerEnd.instance.ScoreN > 1400 && ScoreManagerEnd.instance.ScoreN <= 1500)
        {
            temp1 = Instantiate(modelPrefab[Random.Range(0, 3)]) as GameObject;
        }
        if (ScoreManagerEnd.instance.ScoreN > 1500 && ScoreManagerEnd.instance.ScoreN <= 1700)
        {
            temp1 = Instantiate(modelPrefab[Random.Range(0, 2)]) as GameObject;
        }
        if (ScoreManagerEnd.instance.ScoreN > 1700 && ScoreManagerEnd.instance.ScoreN <= 1800)
        {
            temp1 = Instantiate(modelPrefab[Random.Range(0, 4)]) as GameObject;
        }
        if (ScoreManagerEnd.instance.ScoreN > 1800 && ScoreManagerEnd.instance.ScoreN <= 2000)
        {
            temp1 = Instantiate(modelPrefab[Random.Range(0, 2)]) as GameObject;
        }
        if (ScoreManagerEnd.instance.ScoreN > 2000 && ScoreManagerEnd.instance.ScoreN <= 2100)
        {
            temp1 = Instantiate(modelPrefab[Random.Range(0, 4)]) as GameObject;
        }
        if (ScoreManagerEnd.instance.ScoreN > 2100 && ScoreManagerEnd.instance.ScoreN <= 2300)
        {
            temp1 = Instantiate(modelPrefab[Random.Range(0, 2)]) as GameObject;
        }
        if (ScoreManagerEnd.instance.ScoreN > 2300 && ScoreManagerEnd.instance.ScoreN <= 2400)
        {
            temp1 = Instantiate(modelPrefab[Random.Range(0, 4)]) as GameObject;
        }
        if (ScoreManagerEnd.instance.ScoreN > 2400 && ScoreManagerEnd.instance.ScoreN <= 2600)
        {
            temp1 = Instantiate(modelPrefab[Random.Range(0, 2)]) as GameObject;
        }
        if (ScoreManagerEnd.instance.ScoreN > 2600 && ScoreManagerEnd.instance.ScoreN <= 2700)
        {
            temp1 = Instantiate(modelPrefab[Random.Range(0, 4)]) as GameObject;
        }
        if (ScoreManagerEnd.instance.ScoreN > 2700 && ScoreManagerEnd.instance.ScoreN <= 3000)
        {
            temp1 = Instantiate(modelPrefab[Random.Range(0, 2)]) as GameObject;
        }
        if (ScoreManagerEnd.instance.ScoreN > 3000)
        {
            temp1 = Instantiate(modelPrefab[Random.Range(0, 4)]) as GameObject;
        }
        temp1.transform.eulerAngles = new Vector3(0, spawnY * 8, 0);
        //temp1.transform.SetParent(transform);
        temp1.transform.position = Vector3.down * spawnY;
        spawnY += tilesLength;

        temp1.transform.parent = FindObjectOfType<RotatorEnd>().transform; // when we use Rotatore findObjectType becz we are accessing the transform of the rotator in the rotator gameobject
    }

    void ModelSelection()
    {
        int randomModel = Random.Range(0, 5);

        switch (randomModel)
        {
            case 0:
                for (int i = 0; i < 4; i++)
                    modelPrefab[i] = model[i];
                break;
            case 1:
                for (int i = 0; i < 4; i++)
                    modelPrefab[i] = model[i + 4];
                break;
            case 2:
                for (int i = 0; i < 4; i++)
                    modelPrefab[i] = model[i + 8];
                break;
            case 3:
                for (int i = 0; i < 4; i++)
                    modelPrefab[i] = model[i + 12];
                break;
            case 4:
                for (int i = 0; i < 4; i++)
                    modelPrefab[i] = model[i + 16];
                break;
        }
    }
}


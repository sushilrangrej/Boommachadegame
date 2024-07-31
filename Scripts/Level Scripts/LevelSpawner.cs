using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSpawner : MonoBehaviour
{
    public GameObject[] model;
    [HideInInspector]
    public GameObject[] modelPrefab = new GameObject[4];
    public GameObject winPrefab;

    private GameObject temp1, temp2;

    public int level = 1, addOn = 7;
    float i = 0;

    public Material plateMat, baseMat;
    public MeshRenderer playerMesh;

    [SerializeField][Range(0f, 1f)] float lerptime;

    [SerializeField] Color[] myColors;
    public int myColorIndex = 0;
    float t = 0f;
    int len;

    void Awake()
    {
        len = myColors.Length;

        level = PlayerPrefs.GetInt("level",0);

        if (level > 9)
            addOn = 0;

        ModelSelection();
        float random = Random.value;

        for (i = 0; i > -level - addOn; i -= 0.30f)// i -= 0.35f tells you how much space between the model should allocate in the value adjusted here 
        {
            if (level <= 5)
                temp1 = Instantiate(modelPrefab[Random.Range(0, 2)]);
            if (level > 5 && level <= 10)
                temp1 = Instantiate(modelPrefab[Random.Range(0, 3)]);
            if (level > 10 && level <= 20)
                temp1 = Instantiate(modelPrefab[Random.Range(0, 2)]);
            if (level > 20 && level <= 40)
                temp1 = Instantiate(modelPrefab[Random.Range(0, 3)]);
            if (level > 40 && level <= 45)
                temp1 = Instantiate(modelPrefab[Random.Range(0, 4)]);
            if (level > 45 && level <= 55)
                temp1 = Instantiate(modelPrefab[Random.Range(0, 3)]);
            if (level > 55 && level <= 100)
                temp1 = Instantiate(modelPrefab[Random.Range(0, 4)]);
            if (level > 100)
                temp1 = Instantiate(modelPrefab[Random.Range(0, 4)]);

            temp1.transform.position = new Vector3(0, i + 0.8f, 0);// from where the gameobject sholud be instantiated allocated here
            temp1.transform.eulerAngles = new Vector3(0, i * 8, 0);

            if (Mathf.Abs(i) >= level * .3f && Mathf.Abs(i) <= level * .6f)
            {
                temp1.transform.eulerAngles = new Vector3(0, i * 8, 0);
                temp1.transform.eulerAngles += Vector3.up * 180;
            }else if(Mathf.Abs(i) >= level * .8f)
            {
                temp1.transform.eulerAngles = new Vector3(0, i * 8, 0);

                if (random > .75f)
                    temp1.transform.eulerAngles += Vector3.up * 180;
            }

            temp1.transform.parent = FindObjectOfType<Rotator>().transform; // when we use Rotatore findObjectType becz we are accessing the transform of the rotator in the rotator gameobject
        }

        temp2 = Instantiate(winPrefab);
        temp2.transform.position = new Vector3(0, i + 0.9f, 0);// from where the gameobject sholud be instantiated allocated here from that it will allocate win screen
    }

    void Update()
    {
        plateMat.color = Color.Lerp(plateMat.color, myColors[myColorIndex], lerptime * Time.deltaTime);

        t = Mathf.Lerp(t, 1f, lerptime * Time.deltaTime);
        if (t > 0.9f)
        {
            t = 0f;
            myColorIndex = Random.Range(0,18);
            myColorIndex = (myColorIndex >= myColors.Length) ? 0 : myColorIndex;
        }

        baseMat.color = plateMat.color + Color.gray;
        playerMesh.material.color = plateMat.color; 
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

    public void NextLevel()//When it finishes we go to the next level
    {
        PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);
        SceneManager.LoadScene(3);
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private Rigidbody rb;

    public GameObject sphere;

    public Interstitial interstitial;

    public int died;


    private float currentTime;
    public Text timing;

    private bool smash, Invincible;

    private int currentBrokenStacks, totalStacks;//it is for how many we broken the stacks and how many still left

    public GameObject invicnbleObj;
    public Image invincibleFill;
    public GameObject fireEffect, winEffect, splashEffect;

    public enum PlayerState
    {
        Prepare,
        Playing,
        Died,
        Finish
    }

    [HideInInspector]
    public PlayerState playerState = PlayerState.Prepare;

    public AudioClip bounceOfClip, deadClip, winClip, destroyClip, iDestroyClip;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        currentBrokenStacks = 0;
    }

    void Start()
    {
        totalStacks = FindObjectsOfType<StackController>().Length;//Find Objects nor Findobject
    }
    void Update()
    {
        if (playerState == PlayerState.Playing)
        {
            if (Input.GetMouseButtonDown(0))// when ever you pressed it detect GetMouseButtonDown
                smash = true;

            if (Input.GetMouseButtonUp(0))// when ever you release it detect GetMouseButtonUp
                smash = false;
            if (Invincible)
            {
                currentTime -= Time.deltaTime * .35f;
                timing.text = currentTime.ToString();
                if (!fireEffect.activeInHierarchy)
                    fireEffect.SetActive(true);//Enabaling the fire effect when player is Invincible
            }
            else
            {
                if (fireEffect.activeInHierarchy)
                    fireEffect.SetActive(false);

                if (smash)
                {
                    currentTime += Time.deltaTime * .8f;//when we are pressing we want to increse out current time
                    timing.text = currentTime.ToString();
                }
                else
                {
                    currentTime -= Time.deltaTime * .5f;
                }
            }

            if (currentTime >= 0f || invincibleFill.color == new Color(255, 167, 0))
                invicnbleObj.SetActive(true);
            else
                invicnbleObj.SetActive(false);


            if (currentTime >= 1f)//How much time it has to be invincible it is declared here
            {
                currentTime = 1;
                Invincible = true;
                invincibleFill.color = new Color(255, 167, 0);
            }
            else if (currentTime <= 0f)
            {
                currentTime = 0;
                Invincible = false;
                invincibleFill.color = Color.white;
            }

            if (invicnbleObj.activeInHierarchy)
                invincibleFill.fillAmount = currentTime / 1;
        }

        if (playerState == PlayerState.Finish)
        {
            if (Input.GetMouseButtonDown(0))
                FindObjectOfType<LevelSpawner>().NextLevel();
        }
    }

    void FixedUpdate()
    {
        if (playerState == PlayerState.Playing)
        {
            if (Input.GetMouseButton(0)) // When ever you returns whether the given mouse button held down
            {
                smash = true;
                rb.velocity = new Vector3(0, -100 * Time.fixedDeltaTime * 7, 0);//how fast it is going
            }
        }
        if (rb.velocity.y > 5)
        {
            rb.velocity = new Vector3(rb.velocity.x, 5, rb.velocity.z);
        }
    }

    public void IncreseBrokenStacks()
    {
        currentBrokenStacks++;//Increasing the Broken Stacks

        if(!Invincible)
        {
            FindObjectOfType<ScoreManager>().AddScore(1);
            SoundManager.instance.PlaySoundFX(destroyClip, 0.5f);
        }
        else
        {
            FindObjectOfType<ScoreManager>().AddScore(2);
            SoundManager.instance.PlaySoundFX(iDestroyClip, 0.5f);
        }
    }

    void OnCollisionEnter(Collision target)
    {
        if (!smash)//if we are not pressing the button
        {
            rb.velocity = new Vector3(0, 50 * Time.deltaTime * 5, 0);

            SoundManager.instance.PlaySoundFX(bounceOfClip, 0.5f);
        }
        else
        {
            if (Invincible)//fire activated and destroy all objects
            {
                if(target.gameObject.tag == "enemy" || target.gameObject.tag == "plane")
                {
                    target.transform.parent.GetComponent<StackController>().ShatterAllParts();
                }
            }
            else
            {
                if (target.gameObject.tag == "enemy")
                {
                    target.transform.parent.GetComponent<StackController>().ShatterAllParts();//Destroying Gameobject if we use trnasform.parent.gameobject it will destroy whole gameobject we are calling ShatterAllParts function from the StackPartController
                }

                if (target.gameObject.tag == "plane")
                {
                    rb.isKinematic = true;
                    transform.GetChild(0).gameObject.SetActive(false);

                    GameObject frac = Instantiate(sphere, transform.position, transform.rotation);

                    foreach (Rigidbody rb in gameObject.GetComponentsInChildren<Rigidbody>())
                    {
                        Vector3 force = (rb.transform.position - transform.position).normalized;
                        rb.AddForce(force);
                    }
                    Destroy(frac, 2.5f);

                    playerState = PlayerState.Died;

                    ScoreManager.instance.dies += 1;

                    if(ScoreManager.instance.dies == 5)
                    {
                        interstitial.showingthead();
                        ScoreManager.instance.dies = 0; 
                    } 
                    SoundManager.instance.PlaySoundFX(deadClip, 0.5f);
                }
            }
        }

        FindObjectOfType<GameUI>().LevelSliderFill(currentBrokenStacks / (float)totalStacks);

        if (target.gameObject.tag == "Finish" && playerState == PlayerState.Playing)
        {
            playerState = PlayerState.Finish;
            SoundManager.instance.PlaySoundFX(winClip, 0.7f);
            GameObject win = Instantiate(winEffect);
            win.transform.SetParent(Camera.main.transform);
            win.transform.localPosition = Vector3.up * 1.5f;
            win.transform.eulerAngles = Vector3.zero;
        }
    }

    void OnCollisionStay(Collision target)
    {
        if (!smash || target.gameObject.tag == "Finish")//when we press back we have to bounce back
        {
            rb.velocity = new Vector3(0, 50 * Time.deltaTime * 5, 0);
        }
    }
}

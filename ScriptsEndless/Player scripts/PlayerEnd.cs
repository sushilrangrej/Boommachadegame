using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEnd : MonoBehaviour
{
    private Rigidbody rb;

    public GameObject sphere;

    [Header("Admob")]
    public Interstitial interstitialEnd;

    private float currentTime;
    public Text timingEnd;

    private bool smash, invincible;

    public GameObject invincibleObj;
    public Image invincibleFill;
    public GameObject fireEffect;

    public enum PlayerState
    {
        PrepareEnd,
        PlayingEnd,
        DiedEnd,
        FinishEnd
    }

    //[HideInInspector]
    public PlayerState playerState = PlayerState.PrepareEnd;

    public AudioClip bounceOffClip, deadClip, destoryClip, iDestroyClip;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerState == PlayerState.PlayingEnd)
        {
            if (Input.GetMouseButtonDown(0))
                smash = true;

            if (Input.GetMouseButtonUp(0))
                smash = false;

            if (invincible)
            {
                currentTime -= Time.deltaTime * .35f;
                timingEnd.text = currentTime.ToString();
                if(!fireEffect.activeInHierarchy)//if fireeccect not active in hirarchy active it
                    fireEffect.SetActive(true);
            }
            else
            {
                if (fireEffect.activeInHierarchy)//if fireeccect  active in hirarchy de active it
                    fireEffect.SetActive(false);

                if (smash)
                {
                    currentTime += Time.deltaTime * .8f;//when we are pressing we want to increse out current time
                    timingEnd.text = currentTime.ToString();
                }
                else
                {
                    currentTime -= Time.deltaTime * .5f;
                }
            }

            if (currentTime >= 0.1f || invincibleFill.color == new Color(255, 167, 0))//why 0.15 becz when it reaches in the fill at 0.15 it has to be showed in the screen
                invincibleObj.SetActive(true);
            else
                invincibleObj.SetActive(false);

            if (currentTime >= 1)//How much time it has to be invincible it is declared here
            {
                currentTime = 1;
                invincible = true;
                invincibleFill.color = new Color(255,167,0);
            }
            else if (currentTime <= 0)
            {
                currentTime = 0;
                invincible = false;
                invincibleFill.color = Color.white;
            }

            if (invincibleObj.activeInHierarchy)
                invincibleFill.fillAmount = currentTime / 1;
        }
    }

    void FixedUpdate()
    {
        if(playerState == PlayerState.PlayingEnd)
        {
            if (Input.GetMouseButton(0))//when we are holding the mouse buttton how much veclocity has to be Added
            {
                smash = true;
                rb.velocity = new Vector3(0, -100 * Time.fixedDeltaTime * 7, 0);
            }
        }

        if (rb.velocity.y > 5)
            rb.velocity = new Vector3(rb.velocity.x, 5, rb.velocity.z);
    }

    public void IncreaseBrokenStacks()
    {
        if (!invincible)
        {
            //ScoreManagerEnd.instance.AddScore(1);
            FindObjectOfType<ScoreManagerEnd>().AddScore(1);
            SoundManagerEnd.instance.PlaySoundFx(destoryClip, 0.5f);
        }
        else
        {
            //ScoreManagerEnd.instance.AddScore(2);
            FindObjectOfType<ScoreManagerEnd>().AddScore(2);
            SoundManagerEnd.instance.PlaySoundFx(iDestroyClip, 0.5f);
        }
    }

    void OnCollisionEnter(Collision target)
    {
        if (!smash)//If we are not pressing our button
        {
            rb.velocity = new Vector3(0, 50 * Time.deltaTime * 5, 0);

            SoundManagerEnd.instance.PlaySoundFx(bounceOffClip, 0.5f);
        }
        else
        {
            if(invincible)
            {
                if (target.gameObject.tag == "enemy" || target.gameObject.tag == "plane")
                {
                    target.transform.parent.GetComponent<StackControllerEnd>().ShatterAllParts();
                }
            }
            else
            {
                if (target.gameObject.tag == "enemy")
                {
                    target.transform.parent.GetComponent<StackControllerEnd>().ShatterAllParts();
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

                    playerState = PlayerState.DiedEnd;

                    ScoreManagerEnd.instance.dies += 1;
                    if (ScoreManagerEnd.instance.dies == 6)
                    {
                        //Debug.Log("Load Rewarded Video add Ankita");
                        interstitialEnd.showingthead();
                        ScoreManagerEnd.instance.dies = 0;
                    }
                    SoundManagerEnd.instance.PlaySoundFx(deadClip, 0.5f);
                }
            }
        }

        if(target.gameObject.tag == "Finish" && playerState == PlayerState.PlayingEnd)
        {
            playerState = PlayerState.FinishEnd;
        }
    }

    void OnCollisionStay(Collision target)
    {
        if (!smash)
        {
            rb.velocity = new Vector3(0, 50 * Time.deltaTime * 5, 0);
        }
    }
}

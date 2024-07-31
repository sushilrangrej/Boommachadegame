using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIEnd : MonoBehaviour
{
    public GameObject homeUI, inGameUI, gameOverUI,quitGameUI;
    public GameObject allBtn;
    private PlayerEnd player;

    private bool Btns;

    [Header("AdmobEnd")]
    public RewardedEnd rewardedEnd;

    [Header("PreGame")]
    public Button soundBtn;
    public Sprite soundOnSpr, soundOffSpr;

    [Header("GameOver")]

    public Text scoreAdded;
    public Text gameOverBestText;

    void Awake()
    {
        player = FindObjectOfType<PlayerEnd>();
        soundBtn.onClick.AddListener(() => SoundManagerEnd.instance.SoundOnOff());
    }

    public void Settings()
    {
        Btns = !Btns;//it works like toggle
        allBtn.SetActive(Btns);//allBtns setActive conditions decalred in the bracket
    }

    void Update()
    {
        if(player.playerState == PlayerEnd.PlayerState.PrepareEnd)
        {
            if(SoundManagerEnd.instance.sound && soundBtn.GetComponent<Image>().sprite != soundOnSpr)
                soundBtn.GetComponent<Image>().sprite = soundOnSpr;
            else if (!SoundManagerEnd.instance.sound && soundBtn.GetComponent<Image>().sprite != soundOffSpr)
                soundBtn.GetComponent<Image>().sprite = soundOffSpr;
        }

        if(Input.GetMouseButtonDown(0) && !IgnoreUI() && player.playerState == PlayerEnd.PlayerState.PrepareEnd)
        {
            player.playerState = PlayerEnd.PlayerState.PlayingEnd;
            homeUI.SetActive(false);
            inGameUI.SetActive(true);
            gameOverUI.SetActive(false);
            quitGameUI.SetActive(false);
        }

        if (player.playerState == PlayerEnd.PlayerState.DiedEnd)
        {
            homeUI.SetActive(false);
            inGameUI.SetActive(false);
            gameOverUI.SetActive(true);
            quitGameUI.SetActive(false);

            scoreAdded.text = ScoreManagerEnd.instance.ScoreN.ToString();
            gameOverBestText.text = PlayerPrefs.GetInt("HighScoreEnd").ToString();
        }
    }

    private bool IgnoreUI()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);//Simply mouse touch events
        pointerEventData.position = Input.mousePosition;//setting pointerEventdata at our mouse where is our mouse

        List<RaycastResult> raycastResultList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResultList);
        for (int i = 0; i < raycastResultList.Count; i++)
        {
            if (raycastResultList[i].gameObject.GetComponent<IgnoreEnd>() != null)
            {
                raycastResultList.RemoveAt(i);
                i--;
            }
        }
        return raycastResultList.Count > 0;
    }

    public void RestartEnd()
    {
        ScoreManagerEnd.instance.reset = true;
        ScoreManagerEnd.instance.ResetScore();
        SceneManager.LoadScene(4);
    }

    public void ContinueGameEnd()
    {
        //Load rewarded video ad and put this load scene in the rewarded handled function
        rewardedEnd.reward();
        ScoreManagerEnd.instance.reset = false;
    }
}

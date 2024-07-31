using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{

    public GameObject homeUI, inGameUI, finishUI, gameOverUI,quitGameUI;//Sprite images for the gameUI
    public GameObject allbtns;

    private bool btns;

    [Header("Admob")]
    public Rewarded rewardedAds;

    [Header("PreGame")]
    public Button soundBtn;
    public Sprite soundOnSpr, soundOffSpr;

    [Header("Level filler Sprites")]
    public Sprite[] imagesRandom;

    [Header("InGame")]
    public Image levelSlider;
    public Image currentLevetImg;
    public Image nextLevelImg;
    public Text currentLevelText, nextLevelText;

    [Header("Finish")]
    public Text finishLevelText;

    [Header("GameOver")]
    public Text gameOverScoreText;
    public Text gameOverBestText;

    private Material playerMat;
    private Player player;

    // Start is called before the first frame update
    void Awake()
    {
        playerMat = FindObjectOfType<Player>().transform.GetChild(0).GetComponent<MeshRenderer>().material;//Accessing the material of the player Gameobject it is in child so transform.Getchield
        player = FindObjectOfType<Player>();//accessing the Playerscripts

        soundBtn.onClick.AddListener(() => SoundManager.instance.SoundOnOff());
    }

    private void Start()
    {
        currentLevelText.text = FindObjectOfType<LevelSpawner>().level.ToString();
        nextLevelText.text = FindObjectOfType<LevelSpawner>().level + 1 + "";

        int num = UnityEngine.Random.Range(0, imagesRandom.Length);
        levelSlider.sprite = imagesRandom[num];

    }

    void Update()
    {
        if (player.playerState == Player.PlayerState.Prepare)
        {
            if (SoundManager.instance.sound && soundBtn.GetComponent<Image>().sprite != soundOnSpr)
                soundBtn.GetComponent<Image>().sprite = soundOnSpr;
            else if (!SoundManager.instance.sound && soundBtn.GetComponent<Image>().sprite != soundOffSpr)
                soundBtn.GetComponent<Image>().sprite = soundOffSpr;
        }
        if (Input.GetMouseButtonDown(0) && !IgnoreUI() && player.playerState == Player.PlayerState.Prepare)
        {
            player.playerState = Player.PlayerState.Playing;
            homeUI.SetActive(false);
            inGameUI.SetActive(true);
            finishUI.SetActive(false);
            gameOverUI.SetActive(false);
            quitGameUI.SetActive(false);
        }

        if (player.playerState == Player.PlayerState.Finish)
        {
            homeUI.SetActive(false);
            inGameUI.SetActive(false);
            finishUI.SetActive(true);
            gameOverUI.SetActive(false);
            quitGameUI.SetActive(false);

            finishLevelText.text = "Level " + FindObjectOfType<LevelSpawner>().level;
        }

        if (player.playerState == Player.PlayerState.Died)
        {
            homeUI.SetActive(false);
            inGameUI.SetActive(false);
            finishUI.SetActive(false);
            gameOverUI.SetActive(true);
            quitGameUI.SetActive(false);

            gameOverScoreText.text = ScoreManager.instance.score.ToString();
            gameOverBestText.text = PlayerPrefs.GetInt("HighScore").ToString();
        }
    }

    private bool IgnoreUI()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;//setting pointerEventdata at our mouse

        List<RaycastResult> raycastResultList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResultList);
        for (int i = 0; i < raycastResultList.Count; i++)
        {
            if (raycastResultList[i].gameObject.GetComponent<Ignore>() != null)
            {
                raycastResultList.RemoveAt(i);
                i--;
            }
        }
        return raycastResultList.Count > 0;
    }

    public void LevelSliderFill(float fillAmount)
    {
        levelSlider.fillAmount = fillAmount;
    }

    public void Settings()
    {
        btns = !btns;//Toggles for settings buttons
        allbtns.SetActive(btns);
    }

    public void RestartGame()
    {
        ScoreManager.instance.reset = true;
        ScoreManager.instance.ResetScore();
        SceneManager.LoadScene(3);
    }

    public void ContinueGame()
    {
        //Load rewarded video ad and put this load scene in the rewarded handled function
        rewardedAds.reward();
        ScoreManager.instance.reset = false;
    }
}

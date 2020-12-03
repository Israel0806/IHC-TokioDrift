using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using KartGame.KartSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

//using Mirror;

public enum GameState { Play, Won, Lost }

public class GameFlowManager : MonoBehaviour
{
    
    [Header("Show score text ")]
    public TextMeshProUGUI textScore1;
    public TextMeshProUGUI textScore2;
    //public ControlComunication CCS;
    //public DisplayScore DS;
    //public GameObject GOCCS;
    //public GameObject GODS;
    [Header("Parameters")]
    [Tooltip("Duration of the fade-to-black at the end of the game")]
    public float endSceneLoadDelay = 3f;
    [Tooltip("The canvas group of the fade-to-black screen")]
    public CanvasGroup endGameFadeCanvasGroup;

    [Header("Win")]
    [Tooltip("This string has to be the name of the scene you want to load when winning")]
    public string winSceneName = "WinScene";
    [Tooltip("Duration of delay before the fade-to-black, if winning")]
    public float delayBeforeFadeToBlack = 4f;
    [Tooltip("Duration of delay before the win message")]
    public float delayBeforeWinMessage = 2f;
    [Tooltip("Sound played on win")]
    public AudioClip victorySound;

    [Tooltip("Prefab for the win game message")]
    public DisplayMessage winDisplayMessage;

    public PlayableDirector raceCountdownTrigger;

    [Header("Lose")]
    [Tooltip("This string has to be the name of the scene you want to load when losing")]
    public string loseSceneName = "LoseScene";
    [Tooltip("Prefab for the lose game message")]
    public DisplayMessage loseDisplayMessage;

    public Text endGame;

    public GameState gameState { get; private set; }

    public bool autoFindKarts = true;
    public ArcadeKart playerKart;

    public ArcadeKart[] karts;
    
    public int myScore = 0;

    ObjectiveManager m_ObjectiveManager;
    TimeManager m_TimeManager;
    float m_TimeLoadEndGameScene;
    string m_SceneToLoad;
    float elapsedTimeBeforeEndScene = 0;

    [Header("Managing state of game")]
    public int gamePhase;

    [Header("DisplayMessage")]
    public DisplayMessage displayMessage;

    [Header("Track manager")]
    bool isGameReady;
    public TrackController TC;
    public OrbController OC;
    public KartController[] kartsControllers;
    public DisplayScore[] kartsScore;
   
    void Start()
    {
        //initialize the text 
        //textScore1 = GameObject.Find("TextScore1").GetComponent<TextMeshProUGUI>();
        //textScore2 = GameObject.Find("TextScore2").GetComponent<TextMeshProUGUI>();
        gamePhase = 0;
        isGameReady = false;
        
        //if (playerKart == null) return;
        if (autoFindKarts)
        {
            karts = FindObjectsOfType<ArcadeKart>();
            if (karts.Length > 0)
            {
                if (!playerKart) playerKart = karts[0];
            }
            DebugUtility.HandleErrorIfNullFindObject<ArcadeKart, GameFlowManager>(playerKart, this);
        }

        m_ObjectiveManager = FindObjectOfType<ObjectiveManager>();
        DebugUtility.HandleErrorIfNullFindObject<ObjectiveManager, GameFlowManager>(m_ObjectiveManager, this);

        m_TimeManager = FindObjectOfType<TimeManager>();
        DebugUtility.HandleErrorIfNullFindObject<TimeManager, GameFlowManager>(m_TimeManager, this);

        AudioUtility.SetMasterVolume(1);

        winDisplayMessage.gameObject.SetActive(false);
        loseDisplayMessage.gameObject.SetActive(false);

        m_TimeManager.StopRace();
        //run race countdown animation
    }

    public void GameReady()
    {
        //karts = FindObjectsOfType<ArcadeKart>();

        ShowRaceCountdownAnimation();
        StartCoroutine(ShowObjectivesRoutine());

        StartCoroutine(CountdownThenStartRaceRoutine());

        // build tracks and orbs
        kartsControllers = FindObjectsOfType<KartController>();
        //***************************************************************
        //GOCCS.SetActive(true);
        //GODS.SetActive(true);
        //GOCCS.GetComponent<ControlComunication>().enabled = true;
        //GODS.GetComponent<DisplayScore>().enabled = true;
        //***************************************************************
        foreach (KartController kart in kartsControllers)
        {

            TC.SelectTracks(kart.randTrackNumber);
            OC.CreateOrbs(kart.randOrbNumber);   

            break;
            
            /*if (kart.isHost)
            {
                TC.SelectTracks(kart.randOrbNumber);
                OC.CreateOrbs(kart.randTrackNumber);        
                //Score1 =  kart.score;
                break;
            }else{
                //Score2 =  kart.score;
                break;
            }*/
        }
    }

    IEnumerator CountdownThenStartRaceRoutine()
    {
        yield return new WaitForSeconds(3f);
        StartRace();
    }

    void StartRace()
    {
        foreach (ArcadeKart k in karts)
        {
            k.SetCanMove(true);
        }
        m_TimeManager.StartRace();
    }

    void ShowRaceCountdownAnimation()
    {
        raceCountdownTrigger.Play();
    }

    IEnumerator ShowObjectivesRoutine()
    {
        while (m_ObjectiveManager.Objectives.Count == 0)
            yield return null;
        yield return new WaitForSecondsRealtime(0.2f);
        for (int i = 0; i < m_ObjectiveManager.Objectives.Count; i++)
        {
            if (m_ObjectiveManager.Objectives[i].displayMessage) m_ObjectiveManager.Objectives[i].displayMessage.Display();
            yield return new WaitForSecondsRealtime(1f);
        }
    }


    void Update()
    {
        if (gameState != GameState.Play)
        {
            elapsedTimeBeforeEndScene += Time.deltaTime;
            if (elapsedTimeBeforeEndScene >= endSceneLoadDelay)
            {

                float timeRatio = 1 - (m_TimeLoadEndGameScene - Time.time) / endSceneLoadDelay;
                endGameFadeCanvasGroup.alpha = timeRatio;

                float volumeRatio = Mathf.Abs(timeRatio);
                float volume = Mathf.Clamp(1 - volumeRatio, 0, 1);
                AudioUtility.SetMasterVolume(volume);

                // See if it's time to load the end scene (after the delay)
                if (Time.time >= m_TimeLoadEndGameScene)
                {
                    SceneManager.LoadScene(m_SceneToLoad);
                    gameState = GameState.Play;
                }
            }
        }
        else
        {
            karts = FindObjectsOfType<ArcadeKart>();
            if (karts.Length == 1 && !isGameReady)
            {
                isGameReady = true;
                gameState = GameState.Play;
                Invoke("GameReady",1.0f);
            }

            //update the score of both players and send this data 
            // kartsControllers = FindObjectsOfType<KartController>();
            // foreach (ArcadeKart kart in karts)
            // {
            //     if (kart.isLocalPlayer)
            //     {
            //          kart.myScore = myScore;
            //     }
            // }
            
            
            // foreach (ArcadeKart kart in karts)
            // {
            //     if (kart.isHost)
            //     {
            //         kart.scoreOtherPlayer = Score2;
            //     }
            //     else
            //     {
            //         kart.scoreOtherPlayer = Score1;
            //     }
            // }
            //Write in the interface the scores of both playrs
            int k = 0;
            kartsScore = FindObjectsOfType<DisplayScore>();
            foreach (DisplayScore _score in kartsScore)
            {
                if(k == 0 ){
                    textScore1.text = (_score.myScore).ToString();
                    k++;
                }else{
                    textScore2.text = (_score.myScore).ToString();
                }
                
                // if (kart.isHost)
                // {
                //     textScore1.text = (Score1).ToString();
                //     textScore2.text = (Score2).ToString();
                //     break;
                // }
                // else
                // {
                //     textScore1.text = (Score2).ToString();
                //     textScore2.text = (Score1).ToString();
                //     break;
                // }
            }

            
            if (m_ObjectiveManager.AreAllObjectivesCompleted())
                EndGame(true);

            if (m_TimeManager.IsFinite && m_TimeManager.IsOver)
                EndGame(false);
        }
    }

    void EnterCompetitive()
    {
        //gamePhase == 1;
        displayMessage.message = "Compete against the other player to win!";
    }

    void EndGame(bool win)
    {
        if(gamePhase == 0)
        {
            EnterCompetitive();
            return;
        }
        // Shutdown server for phone
        endGame.text = "1";
        //NetworkManager.GetComponent<TurnItOff>().StopServer();


        // unlocks the cursor before leaving the scene, to be able to click buttons
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        m_TimeManager.StopRace();

        // Remember that we need to load the appropriate end scene after a delay
        gameState = win ? GameState.Won : GameState.Lost;
        endGameFadeCanvasGroup.gameObject.SetActive(true);
        if (win)
        {
            m_SceneToLoad = winSceneName;
            m_TimeLoadEndGameScene = Time.time + endSceneLoadDelay + delayBeforeFadeToBlack;

            // play a sound on win
            var audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = victorySound;
            audioSource.playOnAwake = false;
            audioSource.outputAudioMixerGroup = AudioUtility.GetAudioGroup(AudioUtility.AudioGroups.HUDVictory);
            audioSource.PlayScheduled(AudioSettings.dspTime + delayBeforeWinMessage);

            // create a game message
            winDisplayMessage.delayBeforeShowing = delayBeforeWinMessage;
            winDisplayMessage.gameObject.SetActive(true);
        }
        else
        {
            m_SceneToLoad = loseSceneName;
            m_TimeLoadEndGameScene = Time.time + endSceneLoadDelay + delayBeforeFadeToBlack;

            // create a game message
            loseDisplayMessage.delayBeforeShowing = delayBeforeWinMessage;
            loseDisplayMessage.gameObject.SetActive(true);
        }
    }
}

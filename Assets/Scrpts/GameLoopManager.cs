using UnityEngine;
using System.Collections;
using TMPro;

public class GameLoopManager : MonoBehaviour
{
    public static GameLoopManager instance;
    public bool isGameStarted { get; private set; } = false;

    //1 - run   2 - building
    private float phase1Time = 30, phase2Time = 30;
    private float timeToChange = 0;
    private bool isRun = false;

    // Manager Veriables
    private int player1Cash = 0, player2Cash = 0;
    private int winnerReward = 10, loserReward = 5;
    private int roundsToWin = 5;
    private int player1Wins = 0, player2Wins = 0;

    private GameObject b1, b2;


    //references
    [SerializeField] private PlayerMovement p1, p2;
    [SerializeField] private TextMeshProUGUI timeToSwapModes;
    [SerializeField] private Transform p1start, p2start, p1build, p2build;
    [SerializeField] private TextMeshProUGUI p1Points, p2Points, modeText;
    [SerializeField] private Budowanie budowanie;
    [SerializeField] private GameObject BuildCanvas, RunCanvas, bgCanva;
    [SerializeField] private cameraManager camMan1, camMan2;
    [SerializeField] private Camera buildCam;
    [SerializeField] private GameObject pop1, pop2;

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    private void Start()
    {
        BuildCanvas.SetActive(false);
        RunCanvas.SetActive(false);
        bgCanva.SetActive(false);
        pop1.SetActive(false);
        pop2.SetActive(false);
    }


    [ContextMenu("Start")]
    public void StartGame()
    {
        if(!isGameStarted)
        {
            player1Cash = 0;
            player2Cash = 0;
            player1Wins = 0;
            player2Wins = 0;

            p1.transform.position = p1start.transform.position;
            p2.transform.position = p2start.transform.position;

            p1Points.text = "Points: 0\ncash: 0";
            p2Points.text = "Points: 0\ncash: 0";

            StartCoroutine(ChangeMode());
            isGameStarted = true;
        }
    }

    public void PlayerReachEnd(GameObject player)
    {
        bool p1 = player.GetComponent<PlayerMovement>().isPlayerOne;

        if(p1)
        {
            player1Wins++;
            player1Cash += 10;
            player2Cash += 5;          
        }
        else
        {
            player2Wins++;
            player1Cash += 5;
            player2Cash += 10;
        }

        p1Points.text = "Points: " + player1Wins + "\ncash: " + player1Cash;
        p2Points.text = "Points: " + player2Wins + "\ncash: " + player2Cash;
        bool win = CheckForGameWin();

        if(win)
        {
            StartCoroutine(ChangeMode());
        }
    }

    private bool CheckForGameWin()
    {
        if(player1Wins >= roundsToWin)
        {
            GameWinHandler(true);
            StartCoroutine(ChangeMode());
            return false;
        }
        else if(player2Wins >= roundsToWin)
        {
            GameWinHandler(false);
            StartCoroutine(ChangeMode());
            return false;
        }

        return true;
    }

    private void GameWinHandler(bool winPlayer1)
    {
        //wincanva

        //disable players movement
        isGameStarted = false;
    }


    private void Update()
    {
        if (!isGameStarted) return;

        timeToChange -= Time.deltaTime;
        timeToSwapModes.text = ("Time: " + (int)timeToChange);

        if (timeToChange < 0)
        {
            StartCoroutine(ChangeMode());
        }

        if(!isRun)
        {
            if(b1 != null && b2 != null && !b1.GetComponent<ObstacleMovement>().enabled && !b2.GetComponent<ObstacleMovement>().enabled)
            {
                StartCoroutine(ChangeMode());
                b1 = null;
                b2 = null;
            }
        }
    }

    private IEnumerator ChangeMode()
    {
        isRun = !isRun;
        if(b1 != null && b1.GetComponent<ObstacleMovement>().enabled)
        {
            Destroy(b1);
            b1 = null;
        }
        if (b2 != null && b2.GetComponent<ObstacleMovement>().enabled)
        {
            Destroy(b2);
            b2 = null;
        }

        if (isRun)
        {
            camMan1.gameObject.SetActive(true);
            camMan2.gameObject.SetActive(true);
            buildCam.gameObject.SetActive(false);
            pop1.SetActive(false);
            pop2.SetActive(false);
            modeText.text = "RUN!";

            timeToChange = phase1Time;
            //show run canva
            yield return StartCoroutine(ShowCanvas(RunCanvas));
            //hide run canva

            p1.GetComponent<PlayerMovement>().SetMovement(true);
            p2.GetComponent<PlayerMovement>().SetMovement(true);

            camMan1.target = p1.transform;
            camMan2.target = p2.transform;
        }
        else
        {
            camMan1.gameObject.SetActive(false);
            camMan2.gameObject.SetActive(false);
            buildCam.gameObject.SetActive(true);
            pop1.SetActive(true);
            pop2.SetActive(true);
            modeText.text = "BUILD!";

            p1.transform.position = p1start.transform.position;
            p2.transform.position = p2start.transform.position;
            p1.GetComponent<PlayerMovement>().SetMovement(false);
            p2.GetComponent<PlayerMovement>().SetMovement(false);

            timeToChange = phase2Time;
            //show buy canva          
            yield return StartCoroutine(ShowCanvas(BuildCanvas));
            //hide buy canva

            b1 = budowanie.Buduj();
            b2 = budowanie.Buduj();

            b1.GetComponent<ObstacleMovement>().isPlayerOne = true;
            b2.GetComponent<ObstacleMovement>().isPlayerOne = false;

            b1.transform.position = p1build.transform.position;
            b2.transform.position = p2build.transform.position;

            camMan1.target = b1.transform;
            camMan2.target = b2.transform;
        }

        yield return null;
    }


    private IEnumerator ShowCanvas(GameObject Canva)
    {
        bgCanva.SetActive(true);
        bool c = false;
        for(int i = 0; i<5; i++)
        {
            c = !c;
            Canva.SetActive(c);
            yield return new WaitForSeconds(0.3f);
        }
        Canva.SetActive(false);
        bgCanva.SetActive(false);
    }
}

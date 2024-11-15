using UnityEngine;
using System.Collections;
using TMPro;

public class GameLoopManager : MonoBehaviour
{
    public static GameLoopManager instance;
    public bool isGameStarted { get; private set; } = false;

    //1 - run   2 - building
    private float phase1Time = 10, phase2Time = 10;
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

            p1Points.text = "Player 1 points: 0";
            p2Points.text = "Player 2 points: 0";

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
            p1Points.text = "Player 1 points: " + player1Wins;
        }
        else
        {
            player2Wins++;
            p1Points.text = "Player 2 points: " + player2Wins;
        }

        //add cash

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
            if(b1 != null && b2 != null && !b1.GetComponent<ObstacleInfo>().enabled && !b2.GetComponent<ObstacleInfo>().enabled)
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
        if(b1 != null && b1.GetComponent<ObstacleInfo>().enabled)
        {
            Destroy(b1);
            b1 = null;
        }
        if (b2 != null && b2.GetComponent<ObstacleInfo>().enabled)
        {
            Destroy(b2);
            b2 = null;
        }

        if (isRun)
        {
            modeText.text = "RUN!";

            timeToChange = phase1Time;
            //show run canva
            yield return new WaitForSeconds(0.5f);
            //hide run canva

            p1.GetComponent<PlayerMovement>().SetMovement(true);
            p2.GetComponent<PlayerMovement>().SetMovement(true);
        }
        else
        {
            modeText.text = "BUILD!";

            p1.transform.position = p1start.transform.position;
            p2.transform.position = p2start.transform.position;
            p1.GetComponent<PlayerMovement>().SetMovement(false);
            p2.GetComponent<PlayerMovement>().SetMovement(false);

            timeToChange = phase2Time;
            //show buy canva
            yield return new WaitForSeconds(0.5f);
            //hide buy canva

            b1 = budowanie.Buduj();
            b2 = budowanie.Buduj();

            b1.GetComponent<ObstacleMovement>().isPlayerOne = true;
            b2.GetComponent<ObstacleMovement>().isPlayerOne = false;

            b1.transform.position = p1build.transform.position;
            b2.transform.position = p2build.transform.position;
        }

        yield return null;
    }
}

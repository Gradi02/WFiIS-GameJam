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

    //references
    [SerializeField] private PlayerMovement p1, p2;
    [SerializeField] private TextMeshProUGUI timeToSwapModes;


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

            StartCoroutine(ChangeMode());
            isGameStarted = true;
        }
    }

    [ContextMenu("Pwin")]
    public void PlayerReachEnd()
    {
        //add player win
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
        timeToSwapModes.text = ("Time: " + timeToChange);

        if (timeToChange < 0)
        {
            StartCoroutine(ChangeMode());
        }
    }

    private IEnumerator ChangeMode()
    {
        isRun = !isRun;
        if(isRun)
        {
            timeToChange = phase1Time;
            //show run canva
            yield return new WaitForSeconds(0.5f);
            //hide run canva


        }
        else
        {
            timeToChange = phase2Time;
            //show buy canva
            yield return new WaitForSeconds(0.5f);
            //hide buy canva


        }

        yield return null;
    }
}

using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class GameLoopManager : MonoBehaviour
{
    public static GameLoopManager instance;
    public bool isGameStarted { get; private set; } = false;

    //1 - run   2 - building
    private float phase1Time = 30, phase2Time = 30;
    private float timeToChange = 0;
    private bool isRun = false;

    // Manager Veriables
    public int player1Cash = 0, player2Cash = 0;
    private int winnerReward = 10, loserReward = 5;
    private int roundsToWin = 5;
    private int player1Wins = 0, player2Wins = 0;

    private GameObject b1, b2;

    public Canvas ReadyForGameCanvas;
    public TextMeshProUGUI player1ready;
    public TextMeshProUGUI player2ready;
    public TextMeshProUGUI Countdowntxt;
    private bool p1ready = false;
    private bool p2ready = false;
    private bool IsCountdowning = false;
    private int p1SelectedIndex = 0, p2SelectedIndex = 0;
    private GameObject pw1, pw2;

    public Canvas GameInfoCanvas;

    //references
    [SerializeField] private PlayerMovement p1, p2;
    [SerializeField] private TextMeshProUGUI timeToSwapModes;
    [SerializeField] private Transform p1start, p2start, p1build, p2build;
    [SerializeField] private TextMeshProUGUI p1Points, p2Points;
    [SerializeField] private Budowanie budowanie;
    [SerializeField] private GameObject BuildCanvas, RunCanvas, bgCanva;
    [SerializeField] private cameraManager camMan1, camMan2;
    [SerializeField] private Camera buildCam;
    [SerializeField] private GameObject pop1, pop2;
    [SerializeField] private Image[] p1Images, p2Images;
    [SerializeField] private Color disabledColor, defaultColor;
    [SerializeField] private GameObject shop;
    [SerializeField] private GameObject winCanva;
    [SerializeField] private TextMeshProUGUI winText;
    [SerializeField] private Transform[] powerupSlots;
    [SerializeField] private GameObject powerPref;


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
        winCanva.SetActive(false);

        StartCoroutine(Textflash());
        GameInfoCanvas.gameObject.SetActive(false);

        foreach(Image i in p1Images)
        {
            i.color = defaultColor;
            i.GetComponent<ItemInfo>().selected.SetActive(false);
        }

        foreach (Image i in p2Images)
        {
            i.color = defaultColor;
            i.GetComponent<ItemInfo>().selected.SetActive(false);
        }
    }


    private IEnumerator Countdown()
    {
        for (int i = 3; i > 0; i--) {
            yield return new WaitForSeconds(1f);
            Countdowntxt.text = i.ToString();
        }
        yield return new WaitForSeconds(1f);
        ReadyForGameCanvas.gameObject.SetActive(false);
        StartGame();
        GameInfoCanvas.gameObject.SetActive(true);
    }
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

    private IEnumerator Textflash()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.3f);
            if (p1ready == false)
            {
                player1ready.color = new Color(0, 0, 0, 0);
            }
            if (p2ready == false)
            {
                player2ready.color = new Color(0, 0, 0, 0);
            }
            yield return new WaitForSeconds(0.3f);
            if (p1ready == false)
            {
                player1ready.color = Color.white;
            }
            if (p2ready == false)
            {
                player2ready.color = Color.white;
            }
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

        p1Points.text = "Points: " + player1Wins + "/5\ncash: " + player1Cash;
        p2Points.text = "Points: " + player2Wins + "/5\ncash: " + player2Cash;
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
        winCanva.SetActive(true);
        winText.text = winPlayer1 ? "Player 1 WIN" : "Player 2 WIN";

        //disable players movement
        isGameStarted = false;
    }

    public void ResetPlayer(GameObject player)
    {
       
    }
    private bool p1dead = false, p2dead = false;
    public IEnumerator ResetPlr(GameObject player)
    {
        FindFirstObjectByType<AudioManager>().Play("hit");
        if (player.name == "p1")
        {
            if(!p1dead)
            {
                p1dead = true;
                p1.GetComponent<BoxCollider2D>().enabled = false;
                LeanTween.moveLocal(p1.gameObject, p1start.transform.position, 1f).setEase(LeanTweenType.easeInOutSine);
                p1.gameObject.GetComponent<PlayerMovement>().SetMovement(false);
                yield return new WaitForSeconds(1f);
                LeanTween.cancel(p1.gameObject);
                p1.gameObject.GetComponent<PlayerMovement>().SetMovement(true);
                p1.GetComponent<BoxCollider2D>().enabled = true;
                p1dead = false;
            }
        }
        else
        {
            if (!p2dead)
            {
                p2dead = true;
                p2.GetComponent<BoxCollider2D>().enabled = false;
                LeanTween.moveLocal(p2.gameObject, p2start.transform.position, 0.5f).setEase(LeanTweenType.easeInOutSine);
                p2.gameObject.GetComponent<PlayerMovement>().SetMovement(false);
                yield return new WaitForSeconds(1f);
                LeanTween.cancel(p2.gameObject);
                p2.gameObject.GetComponent<PlayerMovement>().SetMovement(true);
                p2.GetComponent<BoxCollider2D>().enabled = true;
                p2dead = false;
            }
        }

    }

    private void Update()
    {
        if (!isGameStarted)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                player1ready.text = "READY!";
                p1ready = true;
                player1ready.color = Color.white;
            }

            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                player2ready.text = "READY!";
                p2ready = true;
                player2ready.color = Color.white;
            }

            if (p1ready && p2ready && !IsCountdowning)
            {
                IsCountdowning = true;
                StartCoroutine(Countdown());
            }

            return;
        }


        timeToChange -= Time.deltaTime;
        timeToSwapModes.text = ("Time: " + (int)timeToChange);

        if (timeToChange < 0)
        {
            player1Cash += 5;
            player2Cash += 5;
            p1Points.text = "Points: " + player1Wins + "/5\ncash: " + player1Cash;
            p2Points.text = "Points: " + player2Wins + "/5\ncash: " + player2Cash;
            StartCoroutine(ChangeMode());
        }

        if(!isRun)
        {
            //Gracz 1 wybieranie itemu
            if (b1 == null)
            {
                if (Input.GetKeyDown(KeyCode.D))
                {
                    p1Images[p1SelectedIndex].GetComponent<ItemInfo>().selected.SetActive(false);
                    p1SelectedIndex++;

                    if (p1SelectedIndex > p1Images.Length - 1)
                        p1SelectedIndex = 0;

                    p1Images[p1SelectedIndex].GetComponent<ItemInfo>().selected.SetActive(true);
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    p1Images[p1SelectedIndex].GetComponent<ItemInfo>().selected.SetActive(false);
                    p1SelectedIndex--;

                    if (p1SelectedIndex < 0)
                        p1SelectedIndex = p1Images.Length - 1;

                    p1Images[p1SelectedIndex].GetComponent<ItemInfo>().selected.SetActive(true);
                }
                if(Input.GetKeyDown(KeyCode.LeftShift) && player1Cash >= p1Images[p1SelectedIndex].GetComponent<ItemInfo>().price)
                {
                    b1 = Instantiate(p1Images[p1SelectedIndex].GetComponent<ItemInfo>().obstaclePrefab, new Vector3(0, 0, 0), Quaternion.identity);
                    b1.GetComponent<ObstacleMovement>().isPlayerOne = true;
                    b1.transform.position = p1build.transform.position;
                    CheckForBlockedItems();
                }
            }

            //Gracz 2 wybieranie itemu
            if (b2 == null)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    p2Images[p2SelectedIndex].GetComponent<ItemInfo>().selected.SetActive(false);
                    p2SelectedIndex++;

                    if (p2SelectedIndex > p2Images.Length - 1)
                        p2SelectedIndex = 0;

                    p2Images[p2SelectedIndex].GetComponent<ItemInfo>().selected.SetActive(true);
                }
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    p2Images[p2SelectedIndex].GetComponent<ItemInfo>().selected.SetActive(false);
                    p2SelectedIndex--;

                    if (p2SelectedIndex < 0)
                        p2SelectedIndex = p2Images.Length - 1;

                    p2Images[p2SelectedIndex].GetComponent<ItemInfo>().selected.SetActive(true);
                }
                if (Input.GetKeyDown(KeyCode.RightShift) && player2Cash >= p2Images[p2SelectedIndex].GetComponent<ItemInfo>().price)
                {
                    b2 = Instantiate(p2Images[p2SelectedIndex].GetComponent<ItemInfo>().obstaclePrefab, new Vector3(0, 0, 0), Quaternion.identity);
                    b2.GetComponent<ObstacleMovement>().isPlayerOne = false;
                    b2.transform.position = p2build.transform.position;
                    CheckForBlockedItems();
                }
            }
        }
    }

    private void CheckForBlockedItems()
    {
        foreach (Image i in p1Images)
        {
            if (i.GetComponent<ItemInfo>().price > player1Cash)
            {
                i.color = disabledColor;
            }
            else
            {
                i.color = defaultColor;
            }
        }

        foreach (Image i in p2Images)
        {
            if (i.GetComponent<ItemInfo>().price > player2Cash)
            {
                i.color = disabledColor;
            }
            else
            {
                i.color = defaultColor;
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
            FindFirstObjectByType<AudioManager>().Stop("buildbg");
            FindFirstObjectByType<AudioManager>().Play("runbg");
            camMan1.gameObject.SetActive(true);
            camMan2.gameObject.SetActive(true);
            buildCam.gameObject.SetActive(false);
            pop1.SetActive(false);
            pop2.SetActive(false);
            shop.SetActive(false);

            Destroy(pw1);
            Destroy(pw2);
            Transform pos = powerupSlots[Random.Range(0, powerupSlots.Length)];
            pw1 = Instantiate(powerPref, transform.position, Quaternion.identity);
            pw2 = Instantiate(powerPref, transform.position, Quaternion.identity);
            pw1.transform.position = pos.position;
            pw2.transform.position = new Vector3(-pos.position.x-2, pos.position.y, pos.position.z);

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
            FindFirstObjectByType<AudioManager>().Stop("runbg");
            FindFirstObjectByType<AudioManager>().Play("buildbg");
            CheckForBlockedItems();

            Destroy(pw1);
            Destroy(pw2);

            camMan1.gameObject.SetActive(false);
            camMan2.gameObject.SetActive(false);
            buildCam.gameObject.SetActive(true);
            pop1.SetActive(true);
            pop2.SetActive(true);
            shop.SetActive(true);
            p1Images[p1SelectedIndex].GetComponent<ItemInfo>().selected.SetActive(true);
            p2Images[p2SelectedIndex].GetComponent<ItemInfo>().selected.SetActive(true);

            p1.transform.position = p1start.transform.position;
            p2.transform.position = p2start.transform.position;
            p1.GetComponent<PlayerMovement>().SetMovement(false);
            p2.GetComponent<PlayerMovement>().SetMovement(false);

            timeToChange = phase2Time;
            //show buy canva          
            yield return StartCoroutine(ShowCanvas(BuildCanvas));
            //hide buy canva
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

    public void BuyB(bool b1t)
    {
        if (b1t)
        {
            b1 = null;
            player1Cash -= p1Images[p1SelectedIndex].GetComponent<ItemInfo>().price;
            p1Points.text = "Points: " + player1Wins + "/5\ncash: " + player1Cash;
            CheckForBlockedItems();
        }
        else
        {
            b2 = null;
            player2Cash -= p2Images[p2SelectedIndex].GetComponent<ItemInfo>().price;
            p2Points.text = "Points: " + player2Wins + "/5\ncash: " + player2Cash;
            CheckForBlockedItems();
        }
    }

    public void AddEffect(bool if1player, Types tp)
    {
        if(if1player)
        {
            switch(tp)
            {
                case Types.table:
                    p2.SetTable();
                    break;
                case Types.invc:
                    p1.SetInv();
                    break;
                case Types.cash:
                    player1Cash += 5;
                    p1Points.text = "Points: " + player1Wins + "/5\ncash: " + player1Cash;
                    p2Points.text = "Points: " + player2Wins + "/5\ncash: " + player2Cash;
                    break;
            }
        }
        else
        {
            switch (tp)
            {
                case Types.table:
                    p1.SetTable();
                    break;
                case Types.invc:
                    p2.SetInv();
                    break;
                case Types.cash:
                    player2Cash += 5;
                    p1Points.text = "Points: " + player1Wins + "/5\ncash: " + player1Cash;
                    p2Points.text = "Points: " + player2Wins + "/5\ncash: " + player2Cash;
                    break;
            }
        }
    }

    public enum Types
    {
        table,
        invc,
        cash
    }
}

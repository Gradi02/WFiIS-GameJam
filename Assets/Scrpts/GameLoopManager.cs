using UnityEngine;

public class GameLoopManager : MonoBehaviour
{
    public static GameLoopManager instance;
    public bool isGameStarted { get; private set; } = false;

    //1 - run   2 - building
    private float phase1Time = 60, phase2Time = 60;


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

    public void StartGame()
    {
        if(!isGameStarted)
        {
            isGameStarted = true;
            //start gry
        }
    }
}

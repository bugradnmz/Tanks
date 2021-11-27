using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static int m_NumRoundsToWin;        
	public float m_StartDelay = 3f;         
    public float m_EndDelay = 3f;
    public CameraControl m_CameraControl;   
    public Text m_MessageText;
	public TankManager[] m_Tanks;
    public GameObject m_TankPrefab;
	public GameObject m_ScoreText;
	public GameObject m_InGameMenu;
	public GameObject m_GamePausedText;
	public GameObject m_LoadingScreen;
	public Slider m_LoadingBar;
	public AudioSource m_WinSound;

	public static Color m_P1Color;
	public static Color m_P2Color;

	public Color m_BrightGreen;
	public Color m_Green;
	public Color m_Yellow;
	public Color m_Orange;
	public Color m_Red;

	private int m_P1Wins;
	private int m_P2Wins;
	private int m_RoundNumber;
	private int m_NumOfESCDown = 2;

    private WaitForSeconds m_StartWait;     
    private WaitForSeconds m_EndWait;       
    private TankManager m_RoundWinner;
    private TankManager m_GameWinner;
	private AsyncOperation m_LoadingAsync;

    private void Start()
	{
		Cursor.visible = false;

		m_P1Wins = 0;
		m_P2Wins = 0;

		m_StartWait = new WaitForSeconds(m_StartDelay);
        m_EndWait = new WaitForSeconds(m_EndDelay);

		SpawnAllTanks();
        SetCameraTargets();
        StartCoroutine(GameLoop());

		m_P1Color = GlobalVar.GlobalVariables.g_Player1Color;
		m_P2Color = GlobalVar.GlobalVariables.g_Player2Color;
		m_NumRoundsToWin = GlobalVar.GlobalVariables.g_NumRoundsToWin;
		
		if (m_NumRoundsToWin == 1)
		{
		m_ScoreText.GetComponent<Text>().text = "<color=#" + ColorUtility.ToHtmlStringRGB(m_P1Color) + ">P1 WINS : " + m_P1Wins + "</color>" + " / " +
												"<color=#" + ColorUtility.ToHtmlStringRGB(m_P2Color) + ">P2 WINS : " + m_P2Wins + "</color>" + " / " +
												"<color=#" + ColorUtility.ToHtmlStringRGB(m_BrightGreen) + ">ROUNDS TO WIN : " + m_NumRoundsToWin + "</color>";
		}
		else if (m_NumRoundsToWin == 3)
		{
			m_ScoreText.GetComponent<Text>().text = "<color=#" + ColorUtility.ToHtmlStringRGB(m_P1Color) + ">P1 WINS : " + m_P1Wins + "</color>" + " / " +
													"<color=#" + ColorUtility.ToHtmlStringRGB(m_P2Color) + ">P2 WINS : " + m_P2Wins + "</color>" + " / " +
													"<color=#" + ColorUtility.ToHtmlStringRGB(m_Green) + ">ROUNDS TO WIN : " + m_NumRoundsToWin + "</color>";
		}
		else if (m_NumRoundsToWin == 5)
		{
			m_ScoreText.GetComponent<Text>().text = "<color=#" + ColorUtility.ToHtmlStringRGB(m_P1Color) + ">P1 WINS : " + m_P1Wins + "</color>" + " / " +
													"<color=#" + ColorUtility.ToHtmlStringRGB(m_P2Color) + ">P2 WINS : " + m_P2Wins + "</color>" + " / " +
													"<color=#" + ColorUtility.ToHtmlStringRGB(m_Yellow) + ">ROUNDS TO WIN : " + m_NumRoundsToWin + "</color>";
		}
		else if (m_NumRoundsToWin == 7)
		{
			m_ScoreText.GetComponent<Text>().text = "<color=#" + ColorUtility.ToHtmlStringRGB(m_P1Color) + ">P1 WINS : " + m_P1Wins + "</color>" + " / " +
													"<color=#" + ColorUtility.ToHtmlStringRGB(m_P2Color) + ">P2 WINS : " + m_P2Wins + "</color>" + " / " +
													"<color=#" + ColorUtility.ToHtmlStringRGB(m_Orange) + ">ROUNDS TO WIN : " + m_NumRoundsToWin + "</color>";
		}
		else if (m_NumRoundsToWin == 9)
		{
			m_ScoreText.GetComponent<Text>().text = "<color=#" + ColorUtility.ToHtmlStringRGB(m_P1Color) + ">P1 WINS : " + m_P1Wins + "</color>" + " / " +
													"<color=#" + ColorUtility.ToHtmlStringRGB(m_P2Color) + ">P2 WINS : " + m_P2Wins + "</color>" + " / " +
													"<color=#" + ColorUtility.ToHtmlStringRGB(m_Red) + ">ROUNDS TO WIN : " + m_NumRoundsToWin + "</color>";
		}
    }

	private void Update()
	{
		if (m_GameWinner == null)
		{
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				m_NumOfESCDown++;
				if (m_NumOfESCDown % 2 == 1)
				{
					m_InGameMenu.SetActive(true);
					Cursor.visible = true;
					DisableTankControl();
					AudioListener.pause = true;
				}
				else if (m_NumOfESCDown % 2 == 0 && m_NumOfESCDown >=4)
				{
					m_InGameMenu.SetActive(false);
					Cursor.visible = false;
					EnableTankControl();
					AudioListener.pause = false;
				}
			}
		}

		if (m_NumRoundsToWin == 1)
		{
			m_ScoreText.GetComponent<Text>().text = "<color=#" + ColorUtility.ToHtmlStringRGB(m_P1Color) + ">P1 WINS : " + m_P1Wins + "</color>" + " / " +
				"<color=#" + ColorUtility.ToHtmlStringRGB(m_P2Color) + ">P2 WINS : " + m_P2Wins + "</color>" + " / " +
				"<color=#" + ColorUtility.ToHtmlStringRGB(m_BrightGreen) + ">ROUNDS TO WIN : " + m_NumRoundsToWin + "</color>";
		}
		else if (m_NumRoundsToWin == 3)
		{
			m_ScoreText.GetComponent<Text>().text = "<color=#" + ColorUtility.ToHtmlStringRGB(m_P1Color) + ">P1 WINS : " + m_P1Wins + "</color>" + " / " +
				"<color=#" + ColorUtility.ToHtmlStringRGB(m_P2Color) + ">P2 WINS : " + m_P2Wins + "</color>" + " / " +
				"<color=#" + ColorUtility.ToHtmlStringRGB(m_Green) + ">ROUNDS TO WIN : " + m_NumRoundsToWin + "</color>";
		}
		else if (m_NumRoundsToWin == 5)
		{
			m_ScoreText.GetComponent<Text>().text = "<color=#" + ColorUtility.ToHtmlStringRGB(m_P1Color) + ">P1 WINS : " + m_P1Wins + "</color>" + " / " +
				"<color=#" + ColorUtility.ToHtmlStringRGB(m_P2Color) + ">P2 WINS : " + m_P2Wins + "</color>" + " / " +
				"<color=#" + ColorUtility.ToHtmlStringRGB(m_Yellow) + ">ROUNDS TO WIN : " + m_NumRoundsToWin + "</color>";
		}
		else if (m_NumRoundsToWin == 7)
		{
			m_ScoreText.GetComponent<Text>().text = "<color=#" + ColorUtility.ToHtmlStringRGB(m_P1Color) + ">P1 WINS : " + m_P1Wins + "</color>" + " / " +
				"<color=#" + ColorUtility.ToHtmlStringRGB(m_P2Color) + ">P2 WINS : " + m_P2Wins + "</color>" + " / " +
				"<color=#" + ColorUtility.ToHtmlStringRGB(m_Orange) + ">ROUNDS TO WIN : " + m_NumRoundsToWin + "</color>";
		}
		else if (m_NumRoundsToWin == 9)
		{
			m_ScoreText.GetComponent<Text>().text = "<color=#" + ColorUtility.ToHtmlStringRGB(m_P1Color) + ">P1 WINS : " + m_P1Wins + "</color>" + " / " +
				"<color=#" + ColorUtility.ToHtmlStringRGB(m_P2Color) + ">P2 WINS : " + m_P2Wins + "</color>" + " / " +
				"<color=#" + ColorUtility.ToHtmlStringRGB(m_Red) + ">ROUNDS TO WIN : " + m_NumRoundsToWin + "</color>";
		}
	}
		
    private void SpawnAllTanks()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            m_Tanks[i].m_Instance =
                Instantiate(m_TankPrefab, m_Tanks[i].m_SpawnPoint.position, m_Tanks[i].m_SpawnPoint.rotation) as GameObject;
            m_Tanks[i].m_PlayerNumber = i + 1;
            m_Tanks[i].Setup();
        }
    }

    private void SetCameraTargets()
    {
        Transform[] targets = new Transform[m_Tanks.Length];


        for (int i = 0; i < targets.Length; i++)
        {
            targets[i] = m_Tanks[i].m_Instance.transform;
        }
			
        m_CameraControl.m_Targets = targets;
    }


    private IEnumerator GameLoop()
    {
		yield return StartCoroutine(RoundStarting());
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());

        if (m_GameWinner != null)
        {
			if (m_P1Wins == m_NumRoundsToWin)
			{
				m_GamePausedText.GetComponent<Text>().text = "<color=#" + ColorUtility.ToHtmlStringRGB(m_P1Color) + ">PLAYER 1 " + "</color>" + "WINS THE GAME!";
			}
			else if (m_P2Wins == m_NumRoundsToWin)
			{
				m_GamePausedText.GetComponent<Text>().text = "<color=#" + ColorUtility.ToHtmlStringRGB(m_P2Color) + ">PLAYER 2 " + "</color>" + "WINS THE GAME!";
			}
				
			m_InGameMenu.SetActive(true);
			Cursor.visible = true;
			DisableTankControl();
			m_MessageText.text = "";
			AudioListener.pause = true;
        }
        else
        {
            StartCoroutine(GameLoop());
        }
    }


    private IEnumerator RoundStarting()
    {
		ResetAllTanks ();
		DisableTankControl ();

		m_CameraControl.SetStartPositionAndSize ();

		m_RoundNumber++;
		m_MessageText.text = "ROUND " + m_RoundNumber;

		yield return m_StartWait;
    }


    private IEnumerator RoundPlaying()
    {
		EnableTankControl ();

		m_MessageText.text = string.Empty;

		while(!OneTankLeft())
		{
			yield return null;
		}
    }


    private IEnumerator RoundEnding()
    {
		DisableTankControl ();

		m_RoundWinner = null;

		m_RoundWinner = GetRoundWinner ();

		if (m_RoundWinner != null)
			m_RoundWinner.m_Wins++;

		m_GameWinner = GetGameWinner ();

		string message = EndMessage ();
		m_MessageText.text = message;

		if (m_GameWinner != null)
			m_EndWait = new WaitForSeconds(7f);

		yield return m_EndWait;

		m_EndWait = new WaitForSeconds(m_EndDelay);
    }

	IEnumerator LoadWithLoadingBar()
	{
		m_LoadingAsync = SceneManager.LoadSceneAsync(1);

		while(!m_LoadingAsync.isDone)
		{
			m_LoadingBar.value = m_LoadingAsync.progress + 0.1f;
			yield return null;
		}
	}

    private bool OneTankLeft()
    {
        int numTanksLeft = 0;

        for (int i = 0; i < m_Tanks.Length; i++)
        {
            if (m_Tanks[i].m_Instance.activeSelf)
                numTanksLeft++;
        }

        return numTanksLeft <= 1;
    }

    private TankManager GetRoundWinner()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            if (m_Tanks[i].m_Instance.activeSelf)
			{
				if (i == 0)
					m_P1Wins ++;
				else if (i == 1)
					m_P2Wins ++;
				
				return m_Tanks[i];
			}    
        }

        return null;
    }


    private TankManager GetGameWinner()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            if (m_Tanks[i].m_Wins == m_NumRoundsToWin)
                return m_Tanks[i];
        }

        return null;
    }


    private string EndMessage()
    {
        string message = "DRAW!";

		if (m_RoundWinner != null)
            message = m_RoundWinner.m_ColoredPlayerText + " WINS THE ROUND!";

        message += "\n\n\n\n";

        for (int i = 0; i < m_Tanks.Length; i++)
        {
            message += m_Tanks[i].m_ColoredPlayerText + ": " + m_Tanks[i].m_Wins + " WINS\n";
        }

        if (m_GameWinner != null)
		{
            message = m_GameWinner.m_ColoredPlayerText + " WINS THE GAME!";
			m_WinSound.Play ();
		}

		return message;
    }

    private void ResetAllTanks()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            m_Tanks[i].Reset();
        }
    }
		
    private void EnableTankControl()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            m_Tanks[i].EnableControl();
        }
    }
		
    private void DisableTankControl()
    {
        for (int i = 0; i < m_Tanks.Length; i++)
        {
            m_Tanks[i].DisableControl();
        }
    }

	private void RestartGame()
	{
		m_RoundNumber = 0;
		for (int i = 0; i < m_Tanks.Length; i++)
		{
			m_Tanks[i].m_Wins = 0;
		}

		ResetAllTanks();
		SetCameraTargets();

		StartCoroutine(GameLoop());
	}

	public void ClickRematch()
	{
		SceneManager.LoadScene(2);
		AudioListener.pause = false;
	}

	public void ClickReturnToMain()
	{
		m_LoadingScreen.SetActive(true);
		StartCoroutine(LoadWithLoadingBar());
	}
}
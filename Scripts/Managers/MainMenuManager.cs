using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {

	public GameObject m_MenuButtons;
	public GameObject m_ExitOptions;
	public GameObject m_PlayOptions;
	public GameObject m_HowToPlay;
	public GameObject m_LoadingScreen;
	public GameObject m_P1ColorText;
	public GameObject m_P2ColorText;
	public GameObject m_ColorWarning;
	public GameObject m_RoundToWinText;
	public Slider m_LoadingBar;

	public Color m_Red;
	public Color m_Green;
	public Color m_BrightGreen;
	public Color m_Blue;
	public Color m_Teal;
	public Color m_Orange;
	public Color m_Yellow;
	public Color m_Purple;
	public Color m_Pink;

	private AsyncOperation m_LoadingAsync;

	public void Start(){
		
		Cursor.visible = true;
		AudioListener.pause = false;

		GlobalVar.GlobalVariables.g_Player1Color = m_Blue;
		GlobalVar.GlobalVariables.g_Player2Color = m_Red;
		GlobalVar.GlobalVariables.g_NumRoundsToWin = 5;
	}

	public void Update(){
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			m_MenuButtons.SetActive(true);
			m_PlayOptions.SetActive(false);
			m_HowToPlay.SetActive(false);
			m_ExitOptions.SetActive(false);
		}
	}
		
	public void ClickBackToMenu()
	{
		m_PlayOptions.SetActive(false);
		m_HowToPlay.SetActive(false);
		m_MenuButtons.SetActive(true);
	}

	//Play Button
	public void ClickPlay()
	{
		m_MenuButtons.SetActive(false);
		m_PlayOptions.SetActive(true);
	}

	public void ClickPlayReady()
	{
		if(GlobalVar.GlobalVariables.g_Player1Color != GlobalVar.GlobalVariables.g_Player2Color)
		{
			m_LoadingScreen.SetActive(true);
			StartCoroutine(LoadWithLoadingBar());
		}
		else
		{
			StartCoroutine(ShowColorWarning());
		}
	}

	//Player1Colors
	public void ClickPlayP1Red()
	{
		m_P1ColorText.GetComponent<Text>().color = m_Red;
		GlobalVar.GlobalVariables.g_Player1Color = m_Red;
	}

	public void ClickPlayP1Green()
	{
		m_P1ColorText.GetComponent<Text>().color = m_Green;
		GlobalVar.GlobalVariables.g_Player1Color = m_Green;
	}

	public void ClickPlayP1BrightGreen()
	{
		m_P1ColorText.GetComponent<Text>().color = m_BrightGreen;
		GlobalVar.GlobalVariables.g_Player1Color = m_BrightGreen;
	}

	public void ClickPlayP1Blue()
	{
		m_P1ColorText.GetComponent<Text>().color = m_Blue;
		GlobalVar.GlobalVariables.g_Player1Color = m_Blue;
	}

	public void ClickPlayP1Teal()
	{
		m_P1ColorText.GetComponent<Text>().color = m_Teal;
		GlobalVar.GlobalVariables.g_Player1Color = m_Teal;
	}

	public void ClickPlayP1Orange()
	{
		m_P1ColorText.GetComponent<Text>().color = m_Orange;
		GlobalVar.GlobalVariables.g_Player1Color = m_Orange;
	}

	public void ClickPlayP1Yellow()
	{
		m_P1ColorText.GetComponent<Text>().color = m_Yellow;
		GlobalVar.GlobalVariables.g_Player1Color = m_Yellow;
	}

	public void ClickPlayP1Purple()
	{
		m_P1ColorText.GetComponent<Text>().color = m_Purple;
		GlobalVar.GlobalVariables.g_Player1Color = m_Purple;
	}

	public void ClickPlayP1Pink()
	{
		m_P1ColorText.GetComponent<Text>().color = m_Pink;
		GlobalVar.GlobalVariables.g_Player1Color = m_Pink;
	}

	//Player2Colors
	public void ClickPlayP2Red()
	{
		m_P2ColorText.GetComponent<Text>().color = m_Red;
		GlobalVar.GlobalVariables.g_Player2Color = m_Red;
	}

	public void ClickPlayP2Green()
	{
		m_P2ColorText.GetComponent<Text>().color = m_Green;
		GlobalVar.GlobalVariables.g_Player2Color = m_Green;
	}

	public void ClickPlayP2BrightGreen()
	{
		m_P2ColorText.GetComponent<Text>().color = m_BrightGreen;
		GlobalVar.GlobalVariables.g_Player2Color = m_BrightGreen;
	}

	public void ClickPlayP2Blue()
	{
		m_P2ColorText.GetComponent<Text>().color = m_Blue;
		GlobalVar.GlobalVariables.g_Player2Color = m_Blue;
	}

	public void ClickPlayP2Teal()
	{
		m_P2ColorText.GetComponent<Text>().color = m_Teal;
		GlobalVar.GlobalVariables.g_Player2Color = m_Teal;
	}

	public void ClickPlayP2Orange()
	{
		m_P2ColorText.GetComponent<Text>().color = m_Orange;
		GlobalVar.GlobalVariables.g_Player2Color = m_Orange;
	}

	public void ClickPlayP2Yellow()
	{
		m_P2ColorText.GetComponent<Text>().color = m_Yellow;
		GlobalVar.GlobalVariables.g_Player2Color = m_Yellow;
	}

	public void ClickPlayP2Purple()
	{
		m_P2ColorText.GetComponent<Text>().color = m_Purple;
		GlobalVar.GlobalVariables.g_Player2Color = m_Purple;
	}

	public void ClickPlayP2Pink()
	{
		m_P2ColorText.GetComponent<Text>().color = m_Pink;
		GlobalVar.GlobalVariables.g_Player2Color = m_Pink;
	}

	//RoundToWin
	public void ClickPlayOneRound()
	{
		m_RoundToWinText.GetComponent<Text>().color = m_BrightGreen;
		m_RoundToWinText.GetComponent<Text>().text = "1 ROUNDS TO WIN";
		GlobalVar.GlobalVariables.g_NumRoundsToWin = 1;
	}

	public void ClickPlayThreeRound()
	{
		m_RoundToWinText.GetComponent<Text>().color = m_Green;
		m_RoundToWinText.GetComponent<Text>().text = "3 ROUNDS TO WIN";
		GlobalVar.GlobalVariables.g_NumRoundsToWin = 3;
	}

	public void ClickPlayFiveRound()
	{
		m_RoundToWinText.GetComponent<Text>().color = m_Yellow;
		m_RoundToWinText.GetComponent<Text>().text = "5 ROUNDS TO WIN";
		GlobalVar.GlobalVariables.g_NumRoundsToWin = 5;
	}

	public void ClickPlaySevenRound()
	{
		m_RoundToWinText.GetComponent<Text>().color = m_Orange;
		m_RoundToWinText.GetComponent<Text>().text = "7 ROUNDS TO WIN";
		GlobalVar.GlobalVariables.g_NumRoundsToWin = 7;
	}

	public void ClickPlayNineRound()
	{
		m_RoundToWinText.GetComponent<Text>().color = m_Red;
		m_RoundToWinText.GetComponent<Text>().text = "9 ROUNDS TO WIN";
		GlobalVar.GlobalVariables.g_NumRoundsToWin = 9;
	}

	//HowToPlay Button
	public void ClickHowToPlay()
	{
		m_MenuButtons.SetActive(false);
		m_HowToPlay.SetActive(true);
	}

	//Exit Button
	public void ClickExit()
	{
		m_MenuButtons.SetActive(false);
		m_ExitOptions.SetActive(true);
	}

	public void ClickExitYes()
	{
		Application.Quit();
	}

	public void ClickExitNo()
	{
		m_ExitOptions.SetActive(false);
		m_MenuButtons.SetActive(true);
	}

	//IENUMERATORS
	//Loading Screen
	IEnumerator LoadWithLoadingBar()
	{
		m_LoadingAsync = SceneManager.LoadSceneAsync(2);

		while(!m_LoadingAsync.isDone)
		{
			m_LoadingBar.value = m_LoadingAsync.progress + 0.1f;
			yield return null;
		}
	}

	//ColorWarning
	IEnumerator ShowColorWarning()
	{
		m_ColorWarning.SetActive(true);

		yield return new WaitForSeconds(3f);

		m_ColorWarning.SetActive(false);
	}
}
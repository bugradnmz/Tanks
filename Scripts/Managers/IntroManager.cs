using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroManager: MonoBehaviour {

	public float m_LogoDelay = 3f;
	public float m_WarningDelay = 6f;
	public GameObject m_LogoSprite;
	public GameObject m_WarningSprite;
	public GameObject m_LoadingScreen;
	public Slider m_LoadingBar;
	public Camera m_IntroCamera;
	public Color m_LogoBGColor;
	public Color m_WarningBGColor;

	private bool m_IsLogoWasShown;
	private bool m_IsWarningSkipped;
	private int m_NumOfKeyDown;
	private WaitForSeconds m_LogoWait;
	private WaitForSeconds m_WarningWait;
	private AsyncOperation m_LoadingAsync;

	void Start () 
	{
		Cursor.visible = false;

		m_IsLogoWasShown = false;
		m_IsWarningSkipped = false;
		m_NumOfKeyDown = 0;

		m_LogoWait = new WaitForSeconds(m_LogoDelay);
		m_WarningWait = new WaitForSeconds(m_WarningDelay);

		StartCoroutine(Intro());
	}

	void Update()
	{
		if(Input.anyKeyDown)
		{
			SkipLogo();
			m_NumOfKeyDown++;

			if (m_NumOfKeyDown >= 2 || m_IsLogoWasShown)
			{
				SkipWarning();
				m_IsWarningSkipped = true;
			}
		}
	}

	private IEnumerator Intro()
	{
		m_IntroCamera.backgroundColor = m_LogoBGColor;

		m_LogoSprite.gameObject.SetActive(true);
		m_WarningSprite.gameObject.SetActive(false);

		yield return m_LogoWait;
		m_IsLogoWasShown = true;

		if (!m_IsWarningSkipped)
		{
			m_LogoSprite.gameObject.SetActive(false);
			m_WarningSprite.gameObject.SetActive(true);

			m_IntroCamera.backgroundColor = m_WarningBGColor;
		}

		yield return m_WarningWait;

		m_LoadingScreen.SetActive(true);
		StartCoroutine(LoadWithLoadingBar());
	}

	private void SkipLogo()
	{
		m_LogoSprite.gameObject.SetActive(false);
		m_WarningSprite.gameObject.SetActive(true);

		m_IntroCamera.backgroundColor = m_WarningBGColor;
	}

	private void SkipWarning()
	{
		m_LoadingScreen.SetActive(true);
		StartCoroutine(LoadWithLoadingBar());
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
}

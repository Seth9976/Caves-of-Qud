using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000131 RID: 305
public class gameover : MonoBehaviour
{
	// Token: 0x06000B82 RID: 2946 RVA: 0x0004CB48 File Offset: 0x0004AD48
	public void OnReturnButtonPressed()
	{
		global::UnityEngine.Object.DestroyImmediate(base.gameObject);
		global::UnityEngine.Object.Instantiate(Resources.Load("Main Menu"));
	}

	// Token: 0x06000B83 RID: 2947 RVA: 0x0004CB65 File Offset: 0x0004AD65
	public void OnPlayAnotherMatchButtonPressed()
	{
		global::UnityEngine.Object.DestroyImmediate(base.gameObject);
		global::UnityEngine.Object.Instantiate(Resources.Load("Match3"));
	}

	// Token: 0x06000B84 RID: 2948 RVA: 0x0004CB84 File Offset: 0x0004AD84
	private void OnEnable()
	{
		UAP_AccessibilityManager.PauseAccessibility(true, false);
		this.m_GameLostHeading.SetActive(!gameover.GameWon);
		this.m_GameWonHeading.SetActive(gameover.GameWon);
		this.m_GameLostText.SetActive(!gameover.GameWon);
		this.m_GameWonText.SetActive(gameover.GameWon);
		this.m_MovesLabel.text = gameover.MoveCount.ToString("0");
		this.m_MovesLabel.GetComponent<UAP_BaseElement>().m_Text = "You made " + gameover.MoveCount.ToString() + " swaps.";
		this.m_TimeLabel.text = gameover.GameDuration.ToString("0") + " s";
		this.m_TimeLabel.GetComponent<UAP_BaseElement>().m_Text = "Game lasted " + gameover.GameDuration.ToString("0") + " seconds.";
		this.m_WaitingForSilence = 0;
	}

	// Token: 0x06000B85 RID: 2949 RVA: 0x0004CC7C File Offset: 0x0004AE7C
	private void Update()
	{
		if (this.m_WaitingForSilence == 0)
		{
			if (!UAP_AccessibilityManager.IsSpeaking())
			{
				this.m_WaitingForSilence = 1;
			}
			return;
		}
		if (this.m_WaitingForSilence == 1)
		{
			this.m_WaitingForSilence = 2;
			if (gameover.GameWon)
			{
				this.m_AudioPlayer.PlayOneShot(this.m_GameWon);
			}
			else
			{
				this.m_AudioPlayer.PlayOneShot(this.m_GameLost);
			}
			UAP_AccessibilityManager.Say(gameover.GameWon ? "Game Won!" : "Game Over!", false, true, UAP_AudioQueue.EInterrupt.All);
			UAP_AccessibilityManager.BlockInput(false, true);
			UAP_AccessibilityManager.PauseAccessibility(false, false);
		}
	}

	// Token: 0x0400080A RID: 2058
	public GameObject m_GameLostHeading;

	// Token: 0x0400080B RID: 2059
	public GameObject m_GameWonHeading;

	// Token: 0x0400080C RID: 2060
	public GameObject m_GameLostText;

	// Token: 0x0400080D RID: 2061
	public GameObject m_GameWonText;

	// Token: 0x0400080E RID: 2062
	public Text m_MovesLabel;

	// Token: 0x0400080F RID: 2063
	public Text m_TimeLabel;

	// Token: 0x04000810 RID: 2064
	public AudioClip m_GameWon;

	// Token: 0x04000811 RID: 2065
	public AudioClip m_GameLost;

	// Token: 0x04000812 RID: 2066
	public AudioSource m_AudioPlayer;

	// Token: 0x04000813 RID: 2067
	public static int MoveCount;

	// Token: 0x04000814 RID: 2068
	public static float GameDuration;

	// Token: 0x04000815 RID: 2069
	public static bool GameWon;

	// Token: 0x04000816 RID: 2070
	private int m_WaitingForSilence;
}

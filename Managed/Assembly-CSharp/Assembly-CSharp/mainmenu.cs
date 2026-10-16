using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000135 RID: 309
public class mainmenu : MonoBehaviour
{
	// Token: 0x06000BBE RID: 3006 RVA: 0x0004EAA3 File Offset: 0x0004CCA3
	private void Start()
	{
		gameplay.DifficultyLevel = PlayerPrefs.GetInt("Difficulty", 0);
		this.m_DifficultyDropdown.value = gameplay.DifficultyLevel;
	}

	// Token: 0x06000BBF RID: 3007 RVA: 0x0004EAC5 File Offset: 0x0004CCC5
	public void OnInstructionsButtonPressed()
	{
		if (UAP_AccessibilityManager.IsEnabled())
		{
			global::UnityEngine.Object.Instantiate(Resources.Load("Instructions"));
		}
		else
		{
			global::UnityEngine.Object.Instantiate(Resources.Load("Instructions Sighted"));
		}
		global::UnityEngine.Object.DestroyImmediate(base.gameObject);
	}

	// Token: 0x06000BC0 RID: 3008 RVA: 0x0004EAFB File Offset: 0x0004CCFB
	public void OnQuitButtonPressed()
	{
		UAP_AccessibilityManager.Say("Goodbye", true, true, UAP_AudioQueue.EInterrupt.Elements);
		Application.Quit();
	}

	// Token: 0x06000BC1 RID: 3009 RVA: 0x0004EB10 File Offset: 0x0004CD10
	public void OnPlayButtonPressed()
	{
		gameplay.DifficultyLevel = this.m_DifficultyDropdown.value;
		PlayerPrefs.SetInt("Difficulty", gameplay.DifficultyLevel);
		PlayerPrefs.Save();
		global::UnityEngine.Object.Instantiate(Resources.Load("Match3"));
		global::UnityEngine.Object.DestroyImmediate(base.gameObject);
	}

	// Token: 0x06000BC2 RID: 3010 RVA: 0x0004EB5C File Offset: 0x0004CD5C
	public void OnAccessibilityButtonPressed()
	{
		global::UnityEngine.Object.Instantiate(Resources.Load("Accessibility Settings"));
	}

	// Token: 0x04000855 RID: 2133
	public Dropdown m_DifficultyDropdown;
}

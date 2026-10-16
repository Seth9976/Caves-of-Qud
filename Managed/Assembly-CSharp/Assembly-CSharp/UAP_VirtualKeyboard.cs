using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000146 RID: 326
public class UAP_VirtualKeyboard : MonoBehaviour
{
	// Token: 0x06000CEF RID: 3311 RVA: 0x000552B8 File Offset: 0x000534B8
	private void Update()
	{
		this.m_CursorBlinkTimer -= Time.unscaledDeltaTime;
		bool flag = this.m_CursorBlinkTimer > 0f;
		if (this.m_CursorBlinkTimer < -this.m_CursorBlinkDuration)
		{
			this.m_CursorBlinkTimer = this.m_CursorBlinkDuration;
		}
		if (this.m_PasswordedText.Length != this.m_EditedText.Length)
		{
			this.m_PasswordedText = "";
			for (int i = 0; i < this.m_EditedText.Length; i++)
			{
				this.m_PasswordedText += "•";
			}
		}
		this.m_PreviewText.text = ((this.m_KeyboardMode == UAP_VirtualKeyboard.EKeyboardMode.Password) ? this.m_PasswordedText : this.m_EditedText) + (flag ? "<color='#777777'>|</color>" : "");
		this.m_PreviewTextAccessible.SetCustomText((this.m_KeyboardMode == UAP_VirtualKeyboard.EKeyboardMode.Password) ? UAP_AccessibilityManager.Localize_Internal("Keyboard_PasswordHidden") : this.m_EditedText);
		if (Input.GetKeyDown(KeyCode.Backspace))
		{
			this.OnBackSpacePressed();
			return;
		}
		if (Input.GetKeyDown(KeyCode.Return))
		{
			if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android || Input.GetKey(KeyCode.LeftShift))
			{
				this.OnReturnPressed();
				return;
			}
		}
		else if (!string.IsNullOrEmpty(Input.inputString))
		{
			this.AddLetter(Input.inputString);
		}
	}

	// Token: 0x06000CF0 RID: 3312 RVA: 0x00055400 File Offset: 0x00053600
	private void OnTextUpdated()
	{
		this.m_CursorBlinkTimer = this.m_CursorBlinkDuration;
		if (this.m_ShiftModeActive)
		{
			if (this.m_CurrentKeyboardPage == UAP_VirtualKeyboard.EKeyboardPage.Letters)
			{
				this.OnShiftKeyPressed();
			}
			else
			{
				this.m_ShiftModeActive = false;
			}
		}
		if (UAP_VirtualKeyboard.m_OnChangeListener != null)
		{
			UAP_VirtualKeyboard.m_OnChangeListener(this.m_EditedText);
		}
		if (this.m_KeyboardMode == UAP_VirtualKeyboard.EKeyboardMode.Default)
		{
			UAP_AccessibilityManager.Say(this.m_EditedText, true, true, UAP_AudioQueue.EInterrupt.Elements);
		}
	}

	// Token: 0x06000CF1 RID: 3313 RVA: 0x00055468 File Offset: 0x00053668
	private void SetKeyboardLayoutForLanguage(SystemLanguage language)
	{
		this.m_CurrentLanguage = language;
		this.m_ActiveLetterLayout = null;
		for (int i = 0; i < this.m_SupportedKeyboardLayouts.Count; i++)
		{
			if (this.m_SupportedKeyboardLayouts[i].m_SystemLanguage == language)
			{
				this.m_ActiveLetterLayout = this.m_SupportedKeyboardLayouts[i];
			}
		}
		if (this.m_ActiveLetterLayout == null)
		{
			this.m_ActiveLetterLayout = new UAP_VirtualKeyboard.UAPKeyboardLayout();
		}
		this.SetLettersLayout();
		if (this.m_PreferredLanguage == SystemLanguage.English)
		{
			this.m_LanguageButton.gameObject.SetActive(false);
			this.m_EmailAtButton.gameObject.SetActive(true);
			return;
		}
		this.m_LanguageButton.gameObject.SetActive(true);
		this.m_EmailAtButton.gameObject.SetActive(false);
		if (this.m_CurrentLanguage == this.m_PreferredLanguage)
		{
			this.m_LanguageButton.GetComponent<UAP_BaseElement>().SetCustomText("English");
			return;
		}
		this.m_LanguageButton.GetComponent<UAP_BaseElement>().SetCustomText(this.m_PreferredLanguage.ToString());
	}

	// Token: 0x06000CF2 RID: 3314 RVA: 0x0005556C File Offset: 0x0005376C
	private void SetLetterButtonsFromString(string letters, string bottomKeys)
	{
		bool flag = letters.Length > 26;
		this.m_LetterButtons[19].gameObject.SetActive(flag);
		if (flag)
		{
			float num = this.m_LetterButtons[4].transform.position.x - this.m_LetterButtons[3].transform.position.x;
			this.m_SecondButtonRow.localPosition = new Vector3(-0.5f * num, 0f, 0f);
		}
		else
		{
			this.m_SecondButtonRow.localPosition = new Vector3(0f, 0f, 0f);
		}
		int num2 = -1;
		for (int i = 0; i < this.m_LetterButtons.Count; i++)
		{
			if (flag || i != 19)
			{
				num2++;
				if (num2 < letters.Length)
				{
					if (this.m_CurrentKeyboardPage == UAP_VirtualKeyboard.EKeyboardPage.Letters)
					{
						this.m_LetterButtons[i].GetComponent<UAP_BaseElement>().SetCustomText(this.m_ShiftModeActive ? UAP_AccessibilityManager.Localize_Internal("Keyboard_CapitalLetter").Replace("X", letters[num2].ToString().ToLower()) : letters[num2].ToString().ToLower());
						this.m_LetterButtons[i].GetComponentInChildren<Text>().text = (this.m_ShiftModeActive ? letters[num2].ToString().ToUpper() : letters[num2].ToString().ToLower());
					}
					else
					{
						this.m_LetterButtons[i].GetComponent<UAP_BaseElement>().SetCustomText(letters[num2].ToString());
						this.m_LetterButtons[i].GetComponentInChildren<Text>().text = letters[num2].ToString();
					}
				}
				else
				{
					this.m_LetterButtons[i].gameObject.SetActive(false);
				}
			}
		}
		this.m_LeftOfSpace.GetComponentInChildren<Text>().text = bottomKeys[0].ToString();
		this.m_RightOfSpace.GetComponentInChildren<Text>().text = bottomKeys[1].ToString();
	}

	// Token: 0x06000CF3 RID: 3315 RVA: 0x000557AC File Offset: 0x000539AC
	public void OnShiftKeyPressed()
	{
		switch (this.m_CurrentKeyboardPage)
		{
		case UAP_VirtualKeyboard.EKeyboardPage.Numbers:
			this.SetSymbolsLayout();
			return;
		case UAP_VirtualKeyboard.EKeyboardPage.Symbols:
			this.SetNumbersLayout();
			return;
		}
		this.m_ShiftModeActive = !this.m_ShiftModeActive;
		this.m_ShiftSymbol.color = (this.m_ShiftModeActive ? Color.white : new Color32(50, 50, 50, byte.MaxValue));
		UAP_AccessibilityManager.Say(UAP_AccessibilityManager.Localize_Internal(this.m_ShiftModeActive ? "Keyboard_ShiftOn" : "Keyboard_ShiftOff"), true, true, UAP_AudioQueue.EInterrupt.Elements);
		this.SetLettersLayout();
	}

	// Token: 0x06000CF4 RID: 3316 RVA: 0x00055848 File Offset: 0x00053A48
	public void OnToggleKeyPressed()
	{
		UAP_VirtualKeyboard.EKeyboardPage currentKeyboardPage = this.m_CurrentKeyboardPage;
		if (currentKeyboardPage != UAP_VirtualKeyboard.EKeyboardPage.Letters)
		{
			if (currentKeyboardPage - UAP_VirtualKeyboard.EKeyboardPage.Numbers > 1)
			{
			}
			this.SetLettersLayout();
			return;
		}
		this.SetNumbersLayout();
	}

	// Token: 0x06000CF5 RID: 3317 RVA: 0x00055874 File Offset: 0x00053A74
	public void OnLanguageKeyPressed()
	{
		if (this.m_CurrentLanguage != this.m_PreferredLanguage)
		{
			this.SetKeyboardLayoutForLanguage(this.m_PreferredLanguage);
		}
		else
		{
			this.SetKeyboardLayoutForLanguage(SystemLanguage.English);
		}
		UAP_AccessibilityManager.Say(UAP_AccessibilityManager.Localize_Internal("Keyboard_ShowingLanguage") + this.m_CurrentLanguage.ToString(), true, true, UAP_AudioQueue.EInterrupt.Elements);
	}

	// Token: 0x06000CF6 RID: 3318 RVA: 0x000558D0 File Offset: 0x00053AD0
	private void SetLettersLayout()
	{
		if (this.m_CurrentKeyboardPage != UAP_VirtualKeyboard.EKeyboardPage.Letters)
		{
			UAP_AccessibilityManager.Say(UAP_AccessibilityManager.Localize_Internal("Keyboard_ShowingLetters"), true, true, UAP_AudioQueue.EInterrupt.Elements);
		}
		this.m_CurrentKeyboardPage = UAP_VirtualKeyboard.EKeyboardPage.Letters;
		this.m_ShiftKey.GetComponentInChildren<Text>().text = "";
		this.m_ShiftSymbol.gameObject.SetActive(true);
		this.m_ShiftKey.GetComponent<UAP_BaseElement>().SetCustomText(UAP_AccessibilityManager.Localize_Internal("Keyboard_ShiftKey"));
		this.m_SwitchKey.GetComponentInChildren<Text>().text = "123";
		this.m_SwitchKey.GetComponent<UAP_BaseElement>().SetCustomText(UAP_AccessibilityManager.Localize_Internal("Keyboard_NumbersAndSymbols"));
		this.SetLetterButtonsFromString(this.m_ActiveLetterLayout.m_Letters, this.m_ActiveLetterLayout.m_BottomKeys);
	}

	// Token: 0x06000CF7 RID: 3319 RVA: 0x0005598C File Offset: 0x00053B8C
	private void SetNumbersLayout()
	{
		if (this.m_CurrentKeyboardPage != UAP_VirtualKeyboard.EKeyboardPage.Numbers)
		{
			UAP_AccessibilityManager.Say(UAP_AccessibilityManager.Localize_Internal("Keyboard_ShowingNumbers"), true, true, UAP_AudioQueue.EInterrupt.Elements);
		}
		this.m_ShiftModeActive = false;
		this.m_CurrentKeyboardPage = UAP_VirtualKeyboard.EKeyboardPage.Numbers;
		this.m_ShiftKey.GetComponentInChildren<Text>().text = "%{<";
		this.m_ShiftSymbol.gameObject.SetActive(false);
		this.m_ShiftKey.GetComponent<UAP_BaseElement>().SetCustomText(UAP_AccessibilityManager.Localize_Internal("Keyboard_Symbols"));
		this.m_SwitchKey.GetComponentInChildren<Text>().text = "abc";
		this.m_SwitchKey.GetComponent<UAP_BaseElement>().SetCustomText(UAP_AccessibilityManager.Localize_Internal("Keyboard_Letters"));
		this.SetLetterButtonsFromString(this.m_NumbersLayout.m_Letters, this.m_NumbersLayout.m_BottomKeys);
	}

	// Token: 0x06000CF8 RID: 3320 RVA: 0x00055A50 File Offset: 0x00053C50
	private void SetSymbolsLayout()
	{
		if (this.m_CurrentKeyboardPage != UAP_VirtualKeyboard.EKeyboardPage.Symbols)
		{
			UAP_AccessibilityManager.Say(UAP_AccessibilityManager.Localize_Internal("Keyboard_ShowingSymbols"), true, true, UAP_AudioQueue.EInterrupt.Elements);
		}
		this.m_ShiftModeActive = false;
		this.m_CurrentKeyboardPage = UAP_VirtualKeyboard.EKeyboardPage.Symbols;
		this.m_ShiftKey.GetComponentInChildren<Text>().text = "123";
		this.m_ShiftSymbol.gameObject.SetActive(false);
		this.m_ShiftKey.GetComponent<UAP_BaseElement>().SetCustomText(UAP_AccessibilityManager.Localize_Internal("Keyboard_Numbers"));
		this.m_SwitchKey.GetComponentInChildren<Text>().text = "abc";
		this.m_SwitchKey.GetComponent<UAP_BaseElement>().SetCustomText(UAP_AccessibilityManager.Localize_Internal("Keyboard_Letters"));
		this.SetLetterButtonsFromString(this.m_SymbolsLayout.m_Letters, this.m_SymbolsLayout.m_BottomKeys);
	}

	// Token: 0x06000CF9 RID: 3321 RVA: 0x00055B12 File Offset: 0x00053D12
	private void AddLetter(string letter)
	{
		UAP_AccessibilityManager.Say(letter, true, true, UAP_AudioQueue.EInterrupt.Elements);
		this.m_EditedText += letter;
		this.OnTextUpdated();
	}

	// Token: 0x06000CFA RID: 3322 RVA: 0x00055B36 File Offset: 0x00053D36
	public void OnLetterKeyPressed(Button button)
	{
		this.AddLetter(button.GetComponentInChildren<Text>().text);
	}

	// Token: 0x06000CFB RID: 3323 RVA: 0x00055B49 File Offset: 0x00053D49
	public void OnSpacePressed()
	{
		this.AddLetter(" ");
		if (this.m_CurrentKeyboardPage != UAP_VirtualKeyboard.EKeyboardPage.Letters)
		{
			this.SetLettersLayout();
		}
	}

	// Token: 0x06000CFC RID: 3324 RVA: 0x00055B64 File Offset: 0x00053D64
	public void OnBackSpacePressed()
	{
		if (this.m_EditedText.Length == 0)
		{
			return;
		}
		this.m_EditedText = this.m_EditedText.Substring(0, this.m_EditedText.Length - 1);
		this.OnTextUpdated();
		this.AutoSetShiftMode();
	}

	// Token: 0x06000CFD RID: 3325 RVA: 0x00055B9F File Offset: 0x00053D9F
	public void OnReturnPressed()
	{
		if (this.m_AllowMutliLine)
		{
			this.AddLetter("\n");
			return;
		}
		this.OnDonePressed();
	}

	// Token: 0x06000CFE RID: 3326 RVA: 0x00055BBB File Offset: 0x00053DBB
	public void OnClearTextPressed()
	{
		this.m_EditedText = "";
		this.OnTextUpdated();
		this.AutoSetShiftMode();
	}

	// Token: 0x06000CFF RID: 3327 RVA: 0x00055BD4 File Offset: 0x00053DD4
	private void AutoSetShiftMode()
	{
		if (this.m_EditedText.Length == 0 && this.m_StartCapitalized && !this.m_ShiftModeActive)
		{
			if (this.m_CurrentKeyboardPage == UAP_VirtualKeyboard.EKeyboardPage.Letters)
			{
				this.OnShiftKeyPressed();
				return;
			}
			this.m_ShiftModeActive = true;
		}
	}

	// Token: 0x06000D00 RID: 3328 RVA: 0x00055C09 File Offset: 0x00053E09
	public void OnDonePressed()
	{
		if (UAP_VirtualKeyboard.m_OnFinishListener != null)
		{
			UAP_VirtualKeyboard.m_OnFinishListener(this.m_EditedText, true);
		}
		UAP_VirtualKeyboard.ClearAllListeners();
		this.CloseKeyboardOverlay();
	}

	// Token: 0x06000D01 RID: 3329 RVA: 0x00055C2E File Offset: 0x00053E2E
	public void OnCancelPressed()
	{
		if (UAP_VirtualKeyboard.m_OnFinishListener != null)
		{
			UAP_VirtualKeyboard.m_OnFinishListener(this.m_OriginalText, false);
		}
		UAP_VirtualKeyboard.ClearAllListeners();
		this.CloseKeyboardOverlay();
	}

	// Token: 0x06000D02 RID: 3330 RVA: 0x00055C53 File Offset: 0x00053E53
	private void OnApplicationFocus(bool focus)
	{
		if (!focus)
		{
			this.OnCancelPressed();
		}
	}

	// Token: 0x06000D03 RID: 3331 RVA: 0x00055C60 File Offset: 0x00053E60
	private void InitializeKeyboard(string prefilledText, UAP_VirtualKeyboard.EKeyboardMode keyboardMode = UAP_VirtualKeyboard.EKeyboardMode.Default, bool startCapitalized = true, bool alllowMultiline = false)
	{
		if (keyboardMode == UAP_VirtualKeyboard.EKeyboardMode.Password && startCapitalized)
		{
			Debug.LogWarning("[Accessibility] Password Input fields should not start capitalized. Ignoring parameter.");
			startCapitalized = false;
		}
		this.m_KeyboardMode = keyboardMode;
		this.m_StartCapitalized = startCapitalized;
		this.m_EditedText = prefilledText;
		this.m_OriginalText = prefilledText;
		this.m_CursorBlinkTimer = this.m_CursorBlinkDuration;
		this.m_AllowMutliLine = alllowMultiline;
		if (UAP_VirtualKeyboard.SupportsSystemLanguage())
		{
			this.m_PreferredLanguage = Application.systemLanguage;
		}
		else
		{
			this.m_PreferredLanguage = SystemLanguage.English;
		}
		this.SetKeyboardLayoutForLanguage(this.m_PreferredLanguage);
		this.m_ReturnKey.GetComponent<UAP_BaseElement>().SetCustomText(alllowMultiline ? UAP_AccessibilityManager.Localize_Internal("Keyboard_Return") : UAP_AccessibilityManager.Localize_Internal("Keyboard_Done"));
		this.AutoSetShiftMode();
		UAP_AccessibilityManager.Say(UAP_AccessibilityManager.Localize_Internal("Keyboard_Showing"), true, true, UAP_AudioQueue.EInterrupt.Elements);
		UAP_AccessibilityManager.Say(UAP_AccessibilityManager.Localize_Internal(this.m_ShiftModeActive ? "Keyboard_ShiftOn" : "Keyboard_ShiftOff"), true, true, UAP_AudioQueue.EInterrupt.Elements);
	}

	// Token: 0x06000D04 RID: 3332 RVA: 0x00055D3F File Offset: 0x00053F3F
	private void CloseKeyboardOverlay()
	{
		UAP_VirtualKeyboard.Instance = null;
		global::UnityEngine.Object.DestroyImmediate(base.gameObject);
		UAP_AccessibilityManager.Say(UAP_AccessibilityManager.Localize_Internal("Keyboard_Hidden"), true, true, UAP_AudioQueue.EInterrupt.Elements);
	}

	// Token: 0x06000D05 RID: 3333 RVA: 0x00055D65 File Offset: 0x00053F65
	private void OnDestroy()
	{
		UAP_VirtualKeyboard.Instance = null;
	}

	// Token: 0x06000D06 RID: 3334 RVA: 0x00055D6D File Offset: 0x00053F6D
	public static void SetOnFinishListener(UnityAction<string, bool> callback)
	{
		UAP_VirtualKeyboard.m_OnFinishListener = callback;
	}

	// Token: 0x06000D07 RID: 3335 RVA: 0x00055D75 File Offset: 0x00053F75
	public static void SetOnChangeListener(UnityAction<string> callback)
	{
		UAP_VirtualKeyboard.m_OnChangeListener = callback;
	}

	// Token: 0x06000D08 RID: 3336 RVA: 0x00055D7D File Offset: 0x00053F7D
	public static void CloseKeyboard()
	{
		if (UAP_VirtualKeyboard.Instance == null)
		{
			UAP_VirtualKeyboard.ClearAllListeners();
			return;
		}
		UAP_VirtualKeyboard.Instance.OnCancelPressed();
	}

	// Token: 0x06000D09 RID: 3337 RVA: 0x00055D9C File Offset: 0x00053F9C
	public static UAP_VirtualKeyboard ShowOnscreenKeyboard(string prefilledText = "", UAP_VirtualKeyboard.EKeyboardMode keyboardMode = UAP_VirtualKeyboard.EKeyboardMode.Default, bool startCapitalized = true, bool alllowMultiline = false)
	{
		if (UAP_VirtualKeyboard.Instance != null)
		{
			global::UnityEngine.Object.DestroyImmediate(UAP_VirtualKeyboard.Instance.gameObject);
		}
		UAP_VirtualKeyboard.ClearAllListeners();
		UAP_VirtualKeyboard component = (global::UnityEngine.Object.Instantiate(Resources.Load("UAP Virtual Keyboard")) as GameObject).GetComponent<UAP_VirtualKeyboard>();
		component.InitializeKeyboard(prefilledText, keyboardMode, startCapitalized, alllowMultiline);
		UAP_VirtualKeyboard.Instance = component;
		return component;
	}

	// Token: 0x06000D0A RID: 3338 RVA: 0x00055DF3 File Offset: 0x00053FF3
	public static void ClearAllListeners()
	{
		UAP_VirtualKeyboard.m_OnFinishListener = null;
		UAP_VirtualKeyboard.m_OnChangeListener = null;
	}

	// Token: 0x06000D0B RID: 3339 RVA: 0x00055E01 File Offset: 0x00054001
	public static bool IsOpen()
	{
		return UAP_VirtualKeyboard.Instance != null;
	}

	// Token: 0x06000D0C RID: 3340 RVA: 0x00055E10 File Offset: 0x00054010
	public static bool SupportsSystemLanguage()
	{
		List<UAP_VirtualKeyboard.UAPKeyboardLayout> supportedKeyboardLayouts = (Resources.Load("UAP Virtual Keyboard") as GameObject).GetComponent<UAP_VirtualKeyboard>().m_SupportedKeyboardLayouts;
		for (int i = 0; i < supportedKeyboardLayouts.Count; i++)
		{
			if (supportedKeyboardLayouts[i].m_SystemLanguage == Application.systemLanguage)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0400091E RID: 2334
	private const string PrefabPath = "UAP Virtual Keyboard";

	// Token: 0x0400091F RID: 2335
	[Header("Layouts")]
	public UAP_VirtualKeyboard.UAPKeyboardLayout m_NumbersLayout = new UAP_VirtualKeyboard.UAPKeyboardLayout
	{
		m_Letters = "1234567890@#$&*()'\"_ -/:;!?"
	};

	// Token: 0x04000920 RID: 2336
	public UAP_VirtualKeyboard.UAPKeyboardLayout m_SymbolsLayout = new UAP_VirtualKeyboard.UAPKeyboardLayout
	{
		m_Letters = "1234567890€£¥%^[]{}+=|\\©®™",
		m_BottomKeys = "<>"
	};

	// Token: 0x04000921 RID: 2337
	public List<UAP_VirtualKeyboard.UAPKeyboardLayout> m_SupportedKeyboardLayouts = new List<UAP_VirtualKeyboard.UAPKeyboardLayout>();

	// Token: 0x04000922 RID: 2338
	[Header("References")]
	public Transform m_SecondButtonRow;

	// Token: 0x04000923 RID: 2339
	public List<Button> m_LetterButtons = new List<Button>();

	// Token: 0x04000924 RID: 2340
	public Text m_PreviewText;

	// Token: 0x04000925 RID: 2341
	public UAP_BaseElement m_PreviewTextAccessible;

	// Token: 0x04000926 RID: 2342
	public Button m_LanguageButton;

	// Token: 0x04000927 RID: 2343
	public Button m_EmailAtButton;

	// Token: 0x04000928 RID: 2344
	public Button m_ShiftKey;

	// Token: 0x04000929 RID: 2345
	public Image m_ShiftSymbol;

	// Token: 0x0400092A RID: 2346
	public Button m_SwitchKey;

	// Token: 0x0400092B RID: 2347
	public Button m_Done;

	// Token: 0x0400092C RID: 2348
	public Button m_Cancel;

	// Token: 0x0400092D RID: 2349
	public Button m_LeftOfSpace;

	// Token: 0x0400092E RID: 2350
	public Button m_RightOfSpace;

	// Token: 0x0400092F RID: 2351
	public Button m_ReturnKey;

	// Token: 0x04000930 RID: 2352
	private string m_OriginalText = "";

	// Token: 0x04000931 RID: 2353
	private bool m_AllowMutliLine;

	// Token: 0x04000932 RID: 2354
	private UAP_VirtualKeyboard.EKeyboardMode m_KeyboardMode;

	// Token: 0x04000933 RID: 2355
	private bool m_StartCapitalized = true;

	// Token: 0x04000934 RID: 2356
	private SystemLanguage m_PreferredLanguage = SystemLanguage.English;

	// Token: 0x04000935 RID: 2357
	private UAP_VirtualKeyboard.EKeyboardPage m_CurrentKeyboardPage;

	// Token: 0x04000936 RID: 2358
	private UAP_VirtualKeyboard.UAPKeyboardLayout m_ActiveLetterLayout;

	// Token: 0x04000937 RID: 2359
	private bool m_ShiftModeActive;

	// Token: 0x04000938 RID: 2360
	private string m_EditedText = "";

	// Token: 0x04000939 RID: 2361
	private string m_PasswordedText = "";

	// Token: 0x0400093A RID: 2362
	private SystemLanguage m_CurrentLanguage = SystemLanguage.English;

	// Token: 0x0400093B RID: 2363
	private float m_CursorBlinkDuration = 0.8f;

	// Token: 0x0400093C RID: 2364
	private float m_CursorBlinkTimer = -1f;

	// Token: 0x0400093D RID: 2365
	private static UAP_VirtualKeyboard Instance;

	// Token: 0x0400093E RID: 2366
	private static UnityAction<string, bool> m_OnFinishListener;

	// Token: 0x0400093F RID: 2367
	private static UnityAction<string> m_OnChangeListener;

	// Token: 0x020015F9 RID: 5625
	[Serializable]
	public class UAPKeyboardLayout
	{
		// Token: 0x04005881 RID: 22657
		public SystemLanguage m_SystemLanguage = SystemLanguage.English;

		// Token: 0x04005882 RID: 22658
		public string m_Letters = "QWERTYUIOPASDFGHJKLZXCVBNM";

		// Token: 0x04005883 RID: 22659
		public string m_BottomKeys = ",.";
	}

	// Token: 0x020015FA RID: 5626
	public enum EKeyboardMode
	{
		// Token: 0x04005885 RID: 22661
		Default,
		// Token: 0x04005886 RID: 22662
		Password
	}

	// Token: 0x020015FB RID: 5627
	private enum EKeyboardPage
	{
		// Token: 0x04005888 RID: 22664
		Letters,
		// Token: 0x04005889 RID: 22665
		Numbers,
		// Token: 0x0400588A RID: 22666
		Symbols
	}
}

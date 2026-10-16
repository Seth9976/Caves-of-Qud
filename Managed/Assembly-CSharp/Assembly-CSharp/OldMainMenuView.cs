using System;
using ConsoleLib.Console;
using Qud.UI;
using QupKit;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XRL;
using XRL.UI;

// Token: 0x0200009D RID: 157
[UIView("OldMainMenu", true, true, false, "Menu", "OldMainMenu", false, 0, false)]
public class OldMainMenuView : BaseView
{
	// Token: 0x0600052F RID: 1327 RVA: 0x00021A7A File Offset: 0x0001FC7A
	public override void Update()
	{
		if (EventSystem.current.currentSelectedGameObject == null)
		{
			base.Select(base.FindChild("New Game"));
		}
	}

	// Token: 0x06000530 RID: 1328 RVA: 0x00021AA0 File Offset: 0x0001FCA0
	public override void Enter()
	{
		OldMainMenuView.instance = this;
		base.Enter();
		base.Select(base.FindChild("New Game"));
		base.FindChild("Version").GetComponent<global::UnityEngine.UI.Text>().text = XRLGame.CoreVersion.ToString();
		this.DisplayAlert();
	}

	// Token: 0x06000531 RID: 1329 RVA: 0x00021AF0 File Offset: 0x0001FCF0
	public void DisplayAlert()
	{
		GameObject gameObject = base.FindChild("Mod List/Alert");
		Image component = gameObject.GetComponent<Image>();
		if (ModManager.IsAnyModFailed())
		{
			component.color = Color.red;
			gameObject.SetActive(true);
			return;
		}
		if (ModManager.IsAnyModMissingDependency())
		{
			component.color = Color.yellow;
			gameObject.SetActive(true);
			return;
		}
		if (ModManager.IsScriptingUndetermined())
		{
			component.color = Color.white;
			gameObject.SetActive(true);
			return;
		}
		gameObject.SetActive(false);
	}

	// Token: 0x06000532 RID: 1330 RVA: 0x00021B68 File Offset: 0x0001FD68
	public override void OnCommand(string Command)
	{
		if (Command == "NewGame")
		{
			Keyboard.PushKey(new Keyboard.XRLKeyEvent(global::UnityEngine.KeyCode.N, 'n', false, false, false), false);
		}
		if (Command == "Continue")
		{
			Keyboard.PushKey(new Keyboard.XRLKeyEvent(global::UnityEngine.KeyCode.C, 'c', false, false, false), false);
		}
		if (Command == "Options")
		{
			Keyboard.PushMouseEvent("Options", null);
		}
		if (Command == "RedeemCode")
		{
			Keyboard.PushMouseEvent("RedeemCode", null);
		}
		if (Command == "HighScores")
		{
			Keyboard.PushKey(new Keyboard.XRLKeyEvent(global::UnityEngine.KeyCode.H, 'h', false, false, false), false);
		}
		if (Command == "Help")
		{
			Keyboard.PushKey(new Keyboard.XRLKeyEvent(global::UnityEngine.KeyCode.Question, '?', false, false, false), false);
		}
		if (Command == "ExitGame")
		{
			GameManager.Instance.Quit();
		}
		if (Command == "Credits")
		{
			UIManager.showWindow("Credits", false);
		}
		if (Command == "Modkit")
		{
			UIManager.showWindow("ModToolkit", false);
		}
		if (Command == "ModList")
		{
			UIManager.showWindow("ModManager", false);
		}
		if (Command == "Achievements")
		{
			UIManager.pushWindow("AchievementView", false);
		}
		if (Command == "ChoicePopupTest")
		{
			Keyboard.PushKey(new Keyboard.XRLKeyEvent(global::UnityEngine.KeyCode.P, 'p', false, false, false), false);
		}
	}

	// Token: 0x04000401 RID: 1025
	public static OldMainMenuView instance;
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ConsoleLib.Console;
using Cysharp.Text;
using Genkit;
using Qud.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using XRL;
using XRL.CharacterBuilds;
using XRL.UI;
using XRL.UI.Framework;
using XRL.World;

// Token: 0x02000074 RID: 116
public class TutorialManager : MonoBehaviour
{
	// Token: 0x06000434 RID: 1076 RVA: 0x0001C4EB File Offset: 0x0001A6EB
	private void Start()
	{
		if (TutorialManager.instance != null)
		{
			Debug.LogError("Two instance sof tutorialmanager!!");
		}
		TutorialManager.instance = this;
		this.ShowingPopup = false;
		this.highlightRect.gameObject.SetActive(false);
	}

	// Token: 0x06000435 RID: 1077 RVA: 0x0001C522 File Offset: 0x0001A722
	public static void BeforeRenderEvent()
	{
		TutorialStep tutorialStep = TutorialManager.currentStep;
		if (tutorialStep == null)
		{
			return;
		}
		tutorialStep.BeforeRenderEvent();
	}

	// Token: 0x06000436 RID: 1078 RVA: 0x0001C534 File Offset: 0x0001A734
	public static int AdjustExaminationRoll(int result)
	{
		TutorialStep tutorialStep = TutorialManager.currentStep;
		int? num = ((tutorialStep != null) ? new int?(tutorialStep.AdjustExaminationRoll(result)) : null);
		if (num == null)
		{
			return result;
		}
		return num.Value;
	}

	// Token: 0x06000437 RID: 1079 RVA: 0x0001C573 File Offset: 0x0001A773
	public static bool StartingLineTooltip(global::XRL.World.GameObject go, global::XRL.World.GameObject compareGo)
	{
		TutorialStep tutorialStep = TutorialManager.currentStep;
		return tutorialStep == null || tutorialStep.StartingLineTooltip(go, compareGo);
	}

	// Token: 0x06000438 RID: 1080 RVA: 0x0001C587 File Offset: 0x0001A787
	public static bool AllowTooltipUpdate()
	{
		TutorialStep tutorialStep = TutorialManager.currentStep;
		return tutorialStep == null || tutorialStep.AllowTooltipUpdate();
	}

	// Token: 0x17000077 RID: 119
	// (get) Token: 0x06000439 RID: 1081 RVA: 0x0001C599 File Offset: 0x0001A799
	// (set) Token: 0x0600043A RID: 1082 RVA: 0x0001C5A1 File Offset: 0x0001A7A1
	public Location2D PopupCellLocation
	{
		get
		{
			return this._PopupCellLocation;
		}
		set
		{
			this._PopupCellLocation = value;
		}
	}

	// Token: 0x0600043B RID: 1083 RVA: 0x0001C5AC File Offset: 0x0001A7AC
	public static async Task ShowCellPopup(Location2D cell, string text, string directionHint = "ne", int paddingX = 6, int paddingY = 6, Action after = null)
	{
		if (TutorialManager.PopupCompletion != null)
		{
			try
			{
				TutorialManager.PopupCompletion.TrySetResult(true);
				TutorialManager.PopupCompletion = null;
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("ShowCellPopup", ex, "game_exception");
			}
		}
		TutorialManager.PopupCompletion = new TaskCompletionSource<bool>();
		await The.UiContext;
		SoundManager.PlayUISound("Sounds/UI/ui_notification_question", 1f, false, true, SoundRequest.SoundEffectType.None);
		TutorialManager.instance.popupPaddingX = paddingX;
		TutorialManager.instance.popupPaddingY = paddingY;
		TutorialManager.instance.PopupDirectionHint = directionHint;
		if (TutorialManager.instance.afterPopup != null)
		{
			Action action = TutorialManager.instance.afterPopup;
			TutorialManager.instance.afterPopup = null;
			action();
		}
		TutorialManager.instance.afterPopup = after;
		GameManager.Instance.SetActiveLayersForNavCategory("Menu", false);
		TutorialManager.instance.PopupText = text;
		TutorialManager.instance.PopupControlID = null;
		TutorialManager.instance.PopupCellLocation = cell;
		SoundManager.PlayUISound("Sounds/UI/ui_notification_question", 1f, false, true, SoundRequest.SoundEffectType.None);
		TutorialManager.instance.ShowingPopup = true;
		ControlManager.ResetInput(false, true);
		await TutorialManager.PopupCompletion.Task;
	}

	// Token: 0x0600043C RID: 1084 RVA: 0x0001C619 File Offset: 0x0001A819
	public static bool AllowOverlandEncounters()
	{
		TutorialStep tutorialStep = TutorialManager.currentStep;
		return tutorialStep == null || tutorialStep.AllowOverlandEncounters();
	}

	// Token: 0x0600043D RID: 1085 RVA: 0x0001C62B File Offset: 0x0001A82B
	public static bool OnTradeOffer()
	{
		TutorialStep tutorialStep = TutorialManager.currentStep;
		return tutorialStep == null || tutorialStep.OnTradeOffer();
	}

	// Token: 0x0600043E RID: 1086 RVA: 0x0001C63D File Offset: 0x0001A83D
	public static bool OnTradeComplete()
	{
		TutorialStep tutorialStep = TutorialManager.currentStep;
		return tutorialStep == null || tutorialStep.OnTradeComplete();
	}

	// Token: 0x0600043F RID: 1087 RVA: 0x0001C650 File Offset: 0x0001A850
	public static async Task ShowIntermissionPopupAsync(string message, Action after = null, int yPadding = 0, float bottomMargin = 0f, string hint = "sw")
	{
		await TutorialManager.ShowCIDPopupAsync("RootCanvas", message, "s", "[~Accept] Continue", 0, yPadding, bottomMargin, after);
	}

	// Token: 0x06000440 RID: 1088 RVA: 0x0001C6AC File Offset: 0x0001A8AC
	public static async Task ShowCIDPopupAsync(string cid, string text, string directionHint = "ne", string buttonText = "[~Accept] Continue", int paddingX = 16, int paddingY = 16, float bottomMargin = 0f, Action after = null)
	{
		if (TutorialManager.PopupCompletion != null)
		{
			try
			{
				TutorialManager.PopupCompletion.TrySetResult(true);
				TutorialManager.PopupCompletion = null;
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("ShowCellPopup", ex, "game_exception");
			}
		}
		TutorialManager.PopupCompletion = new TaskCompletionSource<bool>();
		await The.UiContext;
		TutorialManager.instance.buttonText.SetText(TutorialManager.instance.hotkeyReplace(buttonText));
		if (TutorialManager.instance.afterPopup != null)
		{
			Action action = TutorialManager.instance.afterPopup;
			TutorialManager.instance.afterPopup = null;
			action();
		}
		TutorialManager.instance.afterPopup = after;
		TutorialManager.instance.popupPaddingX = paddingX;
		TutorialManager.instance.popupPaddingY = paddingY;
		TutorialManager.instance.popupBottomMargin = bottomMargin;
		TutorialManager.instance.PopupDirectionHint = directionHint;
		GameManager.Instance.SetActiveLayersForNavCategory("Menu", false);
		TutorialManager.instance.PopupText = text;
		TutorialManager.instance.PopupControlID = cid;
		TutorialManager.instance.PopupCellLocation = null;
		SoundManager.PlayUISound("Sounds/UI/ui_notification_question", 1f, false, true, SoundRequest.SoundEffectType.None);
		TutorialManager.instance.ShowingPopup = true;
		ControlManager.ResetInput(false, true);
		await TutorialManager.PopupCompletion.Task;
	}

	// Token: 0x06000441 RID: 1089 RVA: 0x0001C72C File Offset: 0x0001A92C
	public static void RefreshHighlightPosition()
	{
		TutorialManager.instance.Highlight(TutorialManager.instance.lastHighlight, TutorialManager.instance.lastText, TutorialManager.instance.lastDirectionHint, TutorialManager.instance.lastPaddingX, TutorialManager.instance.lastPaddingY, 0f, TutorialManager.instance.lastStyle);
		Canvas.ForceUpdateCanvases();
	}

	// Token: 0x17000078 RID: 120
	// (get) Token: 0x06000442 RID: 1090 RVA: 0x0001C789 File Offset: 0x0001A989
	public static bool IsActive
	{
		get
		{
			return TutorialManager.currentStep != null;
		}
	}

	// Token: 0x06000443 RID: 1091 RVA: 0x0001C793 File Offset: 0x0001A993
	public static void StartTutorial(TutorialStep firstStep)
	{
		The.Game.RequireSystem<TutorialManager.TutorialManagerGameSystem>();
		TutorialManager.currentStep = firstStep;
		if (TutorialManager.currentStep != null)
		{
			TutorialManager.currentStep.manager = TutorialManager.instance;
		}
	}

	// Token: 0x06000444 RID: 1092 RVA: 0x0001C7BC File Offset: 0x0001A9BC
	public static bool AllowTargetPick(global::XRL.World.GameObject go, Type ability, List<Cell> target)
	{
		TutorialStep tutorialStep = TutorialManager.currentStep;
		return tutorialStep == null || tutorialStep.AllowTargetPick(go, ability, target);
	}

	// Token: 0x06000445 RID: 1093 RVA: 0x0001C7D1 File Offset: 0x0001A9D1
	public static bool BeforePlayerEnterCell(Cell cell)
	{
		TutorialStep tutorialStep = TutorialManager.currentStep;
		return tutorialStep == null || tutorialStep.BeforePlayerEnterCell(cell);
	}

	// Token: 0x06000446 RID: 1094 RVA: 0x0001C7E4 File Offset: 0x0001A9E4
	public static bool IsEditableChargenPanel(string id)
	{
		TutorialStep tutorialStep = TutorialManager.currentStep;
		return tutorialStep == null || tutorialStep.IsEditableChargenPanel(id);
	}

	// Token: 0x06000447 RID: 1095 RVA: 0x0001C7F7 File Offset: 0x0001A9F7
	public static bool AllowMouseEvent(string ev, int x, int y, object data)
	{
		TutorialManager tutorialManager = TutorialManager.instance;
		return tutorialManager == null || !tutorialManager.ShowingPopup;
	}

	// Token: 0x06000448 RID: 1096 RVA: 0x0001C80F File Offset: 0x0001AA0F
	public static bool AllowSelectPregen(string id)
	{
		TutorialStep tutorialStep = TutorialManager.currentStep;
		return tutorialStep == null || tutorialStep.AllowSelectPregen(id);
	}

	// Token: 0x06000449 RID: 1097 RVA: 0x0001C822 File Offset: 0x0001AA22
	public static bool AllowSelectGenotype(string id)
	{
		TutorialStep tutorialStep = TutorialManager.currentStep;
		return tutorialStep == null || tutorialStep.AllowSelectGenotype(id);
	}

	// Token: 0x0600044A RID: 1098 RVA: 0x0001C835 File Offset: 0x0001AA35
	public static bool AllowInventoryInteract(global::XRL.World.GameObject go)
	{
		TutorialManager tutorialManager = TutorialManager.instance;
		if (tutorialManager != null && tutorialManager.ShowingPopup)
		{
			return false;
		}
		TutorialStep tutorialStep = TutorialManager.currentStep;
		return tutorialStep == null || tutorialStep.AllowInventoryInteract(go);
	}

	// Token: 0x0600044B RID: 1099 RVA: 0x0001C85D File Offset: 0x0001AA5D
	public static bool AllowSelectedPopupCommand(string id, QudMenuItem command)
	{
		TutorialManager tutorialManager = TutorialManager.instance;
		if (tutorialManager != null && tutorialManager.ShowingPopup)
		{
			return false;
		}
		TutorialStep tutorialStep = TutorialManager.currentStep;
		return tutorialStep == null || tutorialStep.AllowSelectedPopupCommand(id, command);
	}

	// Token: 0x0600044C RID: 1100 RVA: 0x0001C886 File Offset: 0x0001AA86
	public static bool AllowOnSelected(FrameworkDataElement element)
	{
		TutorialManager tutorialManager = TutorialManager.instance;
		if (tutorialManager != null && tutorialManager.ShowingPopup)
		{
			return false;
		}
		TutorialStep tutorialStep = TutorialManager.currentStep;
		return tutorialStep == null || tutorialStep.AllowOnSelected(element);
	}

	// Token: 0x0600044D RID: 1101 RVA: 0x0001C8AE File Offset: 0x0001AAAE
	public static bool AllowPushKey(Keyboard.XRLKeyEvent ev, bool bAllowMap = false)
	{
		TutorialManager tutorialManager = TutorialManager.instance;
		return tutorialManager == null || !tutorialManager.ShowingPopup;
	}

	// Token: 0x0600044E RID: 1102 RVA: 0x0001C8C6 File Offset: 0x0001AAC6
	public static bool AllowCommand(string Command)
	{
		TutorialManager tutorialManager = TutorialManager.instance;
		if (tutorialManager != null && tutorialManager.ShowingPopup)
		{
			return false;
		}
		TutorialStep tutorialStep = TutorialManager.currentStep;
		return tutorialStep == null || tutorialStep.AllowCommand(Command);
	}

	// Token: 0x0600044F RID: 1103 RVA: 0x0001C8EE File Offset: 0x0001AAEE
	public static void EndTutorial()
	{
		TutorialManager.currentStep = null;
	}

	// Token: 0x06000450 RID: 1104 RVA: 0x0001C8F6 File Offset: 0x0001AAF6
	public static void OnTrigger(string trigger)
	{
		TutorialStep tutorialStep = TutorialManager.currentStep;
		if (tutorialStep == null)
		{
			return;
		}
		tutorialStep.OnTrigger(trigger);
	}

	// Token: 0x06000451 RID: 1105 RVA: 0x0001C908 File Offset: 0x0001AB08
	public static async void ToggleHide()
	{
		await The.UiContext;
		TutorialManager.instance.gameObject.SetActive(!TutorialManager.instance.gameObject.activeSelf);
	}

	// Token: 0x06000452 RID: 1106 RVA: 0x0001C937 File Offset: 0x0001AB37
	public static void OnBootGame(XRLGame game, EmbarkInfo info)
	{
		if (TutorialManager.currentStep != null)
		{
			TutorialStep tutorialStep = TutorialManager.currentStep;
			if (tutorialStep == null)
			{
				return;
			}
			tutorialStep.OnBootGame(game, info);
		}
	}

	// Token: 0x06000453 RID: 1107 RVA: 0x0001C954 File Offset: 0x0001AB54
	public static async void AdvanceStep(TutorialStep nextStep)
	{
		await The.UiContext;
		TutorialManager.currentStep = nextStep;
		if (TutorialManager.currentStep != null)
		{
			TutorialManager.currentStep.manager = TutorialManager.instance;
			TutorialManager.currentStep.LateUpdate();
		}
	}

	// Token: 0x06000454 RID: 1108 RVA: 0x0001C98B File Offset: 0x0001AB8B
	public void After(int milliseconds, Action after)
	{
		TutorialManager.<>c__DisplayClass58_0 CS$<>8__locals1 = new TutorialManager.<>c__DisplayClass58_0();
		CS$<>8__locals1.milliseconds = milliseconds;
		CS$<>8__locals1.after = after;
		Task.Run(delegate
		{
			TutorialManager.<>c__DisplayClass58_0.<<After>b__0>d <<After>b__0>d;
			<<After>b__0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
			<<After>b__0>d.<>4__this = CS$<>8__locals1;
			<<After>b__0>d.<>1__state = -1;
			<<After>b__0>d.<>t__builder.Start<TutorialManager.<>c__DisplayClass58_0.<<After>b__0>d>(ref <<After>b__0>d);
			return <<After>b__0>d.<>t__builder.Task;
		});
	}

	// Token: 0x06000455 RID: 1109 RVA: 0x0001C9B4 File Offset: 0x0001ABB4
	public void HighlightObject(global::XRL.World.GameObject go, string text, string directionHint, float paddingX = 3f, float paddingY = 3f)
	{
		int? num;
		if (go == null)
		{
			num = null;
		}
		else
		{
			Cell currentCell = go.CurrentCell;
			num = ((currentCell != null) ? new int?(currentCell.X) : null);
		}
		int? num2 = num;
		int valueOrDefault = num2.GetValueOrDefault();
		int? num3;
		if (go == null)
		{
			num3 = null;
		}
		else
		{
			Cell currentCell2 = go.CurrentCell;
			num3 = ((currentCell2 != null) ? new int?(currentCell2.Y) : null);
		}
		num2 = num3;
		this.HighlightCell(valueOrDefault, num2.GetValueOrDefault(), text, directionHint, paddingX, paddingY, 0f);
	}

	// Token: 0x06000456 RID: 1110 RVA: 0x0001CA3C File Offset: 0x0001AC3C
	public string hotkeyReplace(string source)
	{
		if (source == null || (!source.Contains("~") && !source.Contains("%")))
		{
			return source;
		}
		string text3;
		using (Utf16ValueStringBuilder utf16ValueStringBuilder = ZString.CreateStringBuilder())
		{
			utf16ValueStringBuilder.Append(source);
			if (TutorialManager.keysByLength == null)
			{
				TutorialManager.keysByLength = CommandBindingManager.CommandBindings.Keys.ToList<string>();
				TutorialManager.keysByLength.Sort((string a, string b) => b.Length - a.Length);
			}
			if (utf16ValueStringBuilder.ToString().Contains("~Highlight") && ControlManager.activeControllerType != ControlManager.InputDeviceType.Gamepad)
			{
				utf16ValueStringBuilder.Replace("~Highlight", "{{W|Alt}}");
			}
			for (int i = 0; i < TutorialManager.keysByLength.Count; i++)
			{
				string text = TutorialManager.keysByLength[i];
				if (utf16ValueStringBuilder.ToString().Contains("~Highlight"))
				{
					utf16ValueStringBuilder.Replace("~" + text, ControlManager.getCommandInputFormatted(text, true, false));
					if (!utf16ValueStringBuilder.ToString().Contains("~") && !utf16ValueStringBuilder.ToString().Contains("%"))
					{
						break;
					}
				}
				else if (utf16ValueStringBuilder.ToString().Contains("~" + text))
				{
					string text2 = ControlManager.getCommandInputFormatted(text, true, false);
					if (string.IsNullOrEmpty(text2))
					{
						GameCommand gameCommand;
						if (CommandBindingManager.CommandsByID.TryGetValue(text, out gameCommand))
						{
							text2 = "{{hotkey|" + gameCommand.DisplayText + " [Unbound]}}";
						}
						else
						{
							text2 = "{{hotkey|" + text + " [Unbound]}}";
						}
					}
					utf16ValueStringBuilder.Replace("~" + text, text2);
					if (!utf16ValueStringBuilder.ToString().Contains("~") && !utf16ValueStringBuilder.ToString().Contains("%"))
					{
						break;
					}
				}
				else if (utf16ValueStringBuilder.ToString().Contains("%Highlight"))
				{
					utf16ValueStringBuilder.Replace("%" + text, ControlManager.getCommandInputFormatted(text, true, true));
					if (!utf16ValueStringBuilder.ToString().Contains("~") && !utf16ValueStringBuilder.ToString().Contains("%"))
					{
						break;
					}
				}
				else if (utf16ValueStringBuilder.ToString().Contains("%" + text))
				{
					utf16ValueStringBuilder.Replace("%" + text, ControlManager.getCommandInputFormatted(text, true, true));
					if (!utf16ValueStringBuilder.ToString().Contains("~") && !utf16ValueStringBuilder.ToString().Contains("%"))
					{
						break;
					}
				}
			}
			text3 = utf16ValueStringBuilder.ToString();
		}
		return text3;
	}

	// Token: 0x06000457 RID: 1111 RVA: 0x0001CD54 File Offset: 0x0001AF54
	public void HighlightCell(int x, int y, string text, string directionHint, float paddingX = 3f, float paddingY = 3f, float bottomMargin = 0f)
	{
		this.lastHighlight = null;
		this.lastPaddingX = paddingX;
		this.lastPaddingY = paddingY;
		this.lastStyle = "cell";
		if (text != null && (text.Contains("<noframe>") || text.Contains("<no message>")))
		{
			Image[] array = this.imagesToHideWhenNoFrame;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].enabled = false;
			}
			TextMeshProUGUI[] array2 = this.textToHideWhenNoFrame;
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i].enabled = false;
			}
		}
		else
		{
			Image[] array = this.imagesToHideWhenNoFrame;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].enabled = true;
			}
			TextMeshProUGUI[] array2 = this.textToHideWhenNoFrame;
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i].enabled = true;
			}
		}
		if (text != null)
		{
			text = "{{y|" + text + "}}";
		}
		text = this.hotkeyReplace(text);
		if (!string.IsNullOrEmpty(text) && TutorialManager.lastSoundText != text)
		{
			TutorialManager.lastSoundText = text;
			SoundManager.PlayUISound("Sounds/UI/ui_notification_question", 1f, false, true, SoundRequest.SoundEffectType.None);
		}
		this.highlightText.SetText(text);
		this.highlightRect.gameObject.SetActive(text != null);
		Vector3 tileCenter = GameManager.Instance.getTileCenter(x, y, 0);
		this.highlightRect.sizeDelta = new Vector2((16f + paddingX) / Camera.main.GetComponent<LetterboxCamera>().baseOrthographicStageScale, (24f + paddingY) / Camera.main.GetComponent<LetterboxCamera>().baseOrthographicStageScale);
		this.highlightRect.position = Camera.main.WorldToScreenPoint(tileCenter);
		this.highlightRect.pivot = new Vector2(0.5f, 0.5f);
		this.highlightRect.anchorMin = new Vector2(0f, 1f);
		this.highlightRect.anchorMax = new Vector2(0f, 1f);
		this.highlightRect.position += new Vector3(0f, bottomMargin, 0f);
		this.highlightStyle = "cell";
		if (this.bigFrame.activeSelf != (this.highlightStyle == "big" && text != "{{y|<nohighlight>}}"))
		{
			this.bigFrame.SetActive(this.highlightStyle == "big" && text != "{{y|<nohighlight>}}");
		}
		if (this.horizFrame.activeSelf != (this.highlightStyle == "horiz" && text != "{{y|<nohighlight>}}"))
		{
			this.horizFrame.SetActive(this.highlightStyle == "horiz" && text != "{{y|<nohighlight>}}");
		}
		if (this.cellFrame.activeSelf != (this.highlightStyle == "cell" && text != "{{y|<nohighlight>}}"))
		{
			this.cellFrame.SetActive(this.highlightStyle == "cell" && text != "{{y|<nohighlight>}}");
		}
		if (this.buttonArrow.activeSelf != (text != "{{y|<nohighlight>}}"))
		{
			this.buttonArrow.SetActive(text != "{{y|<nohighlight>}}");
		}
		if (text == null || GameManager.Instance.CurrentGameView == "PopupMessage")
		{
			this.SetWindowPosition(null);
			return;
		}
		this.SetWindowPosition(directionHint);
	}

	// Token: 0x06000458 RID: 1112 RVA: 0x0001D0CC File Offset: 0x0001B2CC
	public void SetWindowPosition(string directionHint)
	{
		if (directionHint == null)
		{
			if (this.windowRect.gameObject.activeSelf)
			{
				this.windowRect.gameObject.SetActive(false);
				return;
			}
		}
		else
		{
			if (!this.windowRect.gameObject.activeSelf)
			{
				this.windowRect.gameObject.SetActive(true);
			}
			directionHint = ((directionHint != null) ? directionHint.ToUpper() : null);
			if (directionHint == "N" || directionHint == null)
			{
				RectTransform rectTransform = this.windowRect;
				RectTransform rectTransform2 = this.windowRect;
				Vector2 vector = new Vector2(0.5f, 1f);
				rectTransform2.anchorMax = vector;
				rectTransform.anchorMin = vector;
				this.windowRect.pivot = new Vector2(0.5f, 0f);
			}
			if (directionHint == "S" || directionHint == null)
			{
				RectTransform rectTransform3 = this.windowRect;
				RectTransform rectTransform4 = this.windowRect;
				Vector2 vector = new Vector2(0.5f, 0f);
				rectTransform4.anchorMax = vector;
				rectTransform3.anchorMin = vector;
				this.windowRect.pivot = new Vector2(0.5f, 1f);
			}
			if (directionHint == "E" || directionHint == null)
			{
				RectTransform rectTransform5 = this.windowRect;
				RectTransform rectTransform6 = this.windowRect;
				Vector2 vector = new Vector2(1f, 0.5f);
				rectTransform6.anchorMax = vector;
				rectTransform5.anchorMin = vector;
				this.windowRect.pivot = new Vector2(0f, 0.5f);
			}
			if (directionHint == "W" || directionHint == null)
			{
				RectTransform rectTransform7 = this.windowRect;
				RectTransform rectTransform8 = this.windowRect;
				Vector2 vector = new Vector2(0f, 0.5f);
				rectTransform8.anchorMax = vector;
				rectTransform7.anchorMin = vector;
				this.windowRect.pivot = new Vector2(1f, 0.5f);
			}
			if (directionHint == "NE" || directionHint == null)
			{
				RectTransform rectTransform9 = this.windowRect;
				RectTransform rectTransform10 = this.windowRect;
				Vector2 vector = new Vector2(1f, 1f);
				rectTransform10.anchorMax = vector;
				rectTransform9.anchorMin = vector;
				this.windowRect.pivot = new Vector2(0f, 0f);
			}
			if (directionHint == "SE" || directionHint == null)
			{
				RectTransform rectTransform11 = this.windowRect;
				RectTransform rectTransform12 = this.windowRect;
				Vector2 vector = new Vector2(1f, 0f);
				rectTransform12.anchorMax = vector;
				rectTransform11.anchorMin = vector;
				this.windowRect.pivot = new Vector2(0f, 1f);
			}
			if (directionHint == "SW" || directionHint == null)
			{
				RectTransform rectTransform13 = this.windowRect;
				RectTransform rectTransform14 = this.windowRect;
				Vector2 vector = new Vector2(0f, 0f);
				rectTransform14.anchorMax = vector;
				rectTransform13.anchorMin = vector;
				this.windowRect.pivot = new Vector2(1f, 1f);
			}
			if (directionHint == "NW" || directionHint == null)
			{
				RectTransform rectTransform15 = this.windowRect;
				RectTransform rectTransform16 = this.windowRect;
				Vector2 vector = new Vector2(0f, 1f);
				rectTransform16.anchorMax = vector;
				rectTransform15.anchorMin = vector;
				this.windowRect.pivot = new Vector2(1f, 0f);
			}
			this.windowRect.anchoredPosition = new Vector2(0f, 0f);
		}
	}

	// Token: 0x06000459 RID: 1113 RVA: 0x0001D3E8 File Offset: 0x0001B5E8
	public bool HighlightByCID(string CID, string text, string directionHint, int paddingX = 64, int paddingY = 64, float bottomMargin = 0f, string style = "horiz")
	{
		if (text != null)
		{
			text = "{{y|" + text + "}}";
		}
		global::UnityEngine.GameObject gameObject = ControlId.Get(CID, false);
		object obj;
		if (gameObject == null)
		{
			obj = null;
		}
		else
		{
			global::UnityEngine.GameObject gameObject2 = gameObject.gameObject;
			obj = ((gameObject2 != null) ? gameObject2.transform : null);
		}
		RectTransform rectTransform = obj as RectTransform;
		this.Highlight(rectTransform, text, directionHint, (float)paddingX, (float)paddingY, bottomMargin, style);
		return !(rectTransform == null);
	}

	// Token: 0x0600045A RID: 1114 RVA: 0x0001D44E File Offset: 0x0001B64E
	public void ClearHighlight()
	{
		this.Highlight(null, null, null, 64f, 64f, 0f, "big");
	}

	// Token: 0x0600045B RID: 1115 RVA: 0x0001D470 File Offset: 0x0001B670
	public void Highlight(RectTransform target, string text, string directionHint, float paddingX = 64f, float paddingY = 64f, float bottomMargin = 0f, string style = "big")
	{
		this.lastText = text;
		this.lastDirectionHint = directionHint;
		this.lastPaddingX = paddingX;
		this.lastPaddingY = paddingY;
		this.lastStyle = style;
		if (this.bigFrame.activeSelf != (style == "big" && text != "{{y|<nohighlight>}}"))
		{
			this.bigFrame.SetActive(style == "big" && text != "{{y|<nohighlight>}}");
		}
		if (this.horizFrame.activeSelf != (style == "horiz" && text != "{{y|<nohighlight>}}"))
		{
			this.horizFrame.SetActive(style == "horiz" && text != "{{y|<nohighlight>}}");
		}
		if (this.cellFrame.activeSelf != (style == "cell" && text != "{{y|<nohighlight>}}"))
		{
			this.cellFrame.SetActive(style == "cell" && text != "{{y|<nohighlight>}}");
		}
		if (this.buttonArrow.activeSelf != (text != "{{y|<nohighlight>}}"))
		{
			this.buttonArrow.SetActive(text != "{{y|<nohighlight>}}");
		}
		if (text != null && (text.Contains("<noframe>") || text.Contains("<no message>") || text.Contains("<nohighlight>")))
		{
			Image[] array = this.imagesToHideWhenNoFrame;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].enabled = false;
			}
			TextMeshProUGUI[] array2 = this.textToHideWhenNoFrame;
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i].enabled = false;
			}
			foreach (global::UnityEngine.GameObject gameObject in this.goToHideWhenNoFrame)
			{
				if (gameObject.activeSelf)
				{
					gameObject.SetActive(false);
				}
			}
		}
		else
		{
			Image[] array = this.imagesToHideWhenNoFrame;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].enabled = true;
			}
			TextMeshProUGUI[] array2 = this.textToHideWhenNoFrame;
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i].enabled = true;
			}
			foreach (global::UnityEngine.GameObject gameObject2 in this.goToHideWhenNoFrame)
			{
				if (!gameObject2.activeSelf)
				{
					gameObject2.SetActive(true);
				}
			}
		}
		if (text != null)
		{
			text = "{{y|" + text + "}}";
		}
		text = this.hotkeyReplace(text);
		if (!string.IsNullOrEmpty(text) && TutorialManager.lastSoundText != text)
		{
			TutorialManager.lastSoundText = text;
			SoundManager.PlayUISound("Sounds/UI/ui_notification_question", 1f, false, true, SoundRequest.SoundEffectType.None);
		}
		this.highlightStyle = style;
		this.highlightText.SetText(text);
		if (this.highlightRect.gameObject.activeSelf != (target != null))
		{
			this.highlightRect.gameObject.SetActive(target != null);
		}
		if (target != null)
		{
			this.highlightRect.CopyFrom(target.gameObject.transform as RectTransform);
			this.highlightRect.position = target.gameObject.transform.position;
			Vector2 vector = new Vector2(this.highlightRect.sizeDelta.x + paddingX, this.highlightRect.sizeDelta.y + paddingY);
			this.highlightRect.transform.position += new Vector3(0f, bottomMargin);
			if (vector != this.highlightRect.sizeDelta)
			{
				this.highlightRect.sizeDelta = vector;
				Canvas.ForceUpdateCanvases();
			}
		}
		this.SetWindowPosition(directionHint);
		if (this.lastHighlight != target)
		{
			this.lastHighlight = target;
			Canvas.ForceUpdateCanvases();
		}
	}

	// Token: 0x0600045C RID: 1116 RVA: 0x0001D82C File Offset: 0x0001BA2C
	public void OnPopupAcceptPressed()
	{
		this.popupAcceptPressed = true;
	}

	// Token: 0x0600045D RID: 1117 RVA: 0x0001D838 File Offset: 0x0001BA38
	private void Update()
	{
		if (this.ShowingPopup)
		{
			base.transform.SetAsLastSibling();
			if (!ControlManager.EnabledLayers.Contains("UINav"))
			{
				GameManager.Instance.SetActiveLayersForNavCategory("Menu", false);
			}
			if (ControlManager.GetButtonDown("Accept", true) || this.popupAcceptPressed)
			{
				this.popupAcceptPressed = false;
				try
				{
					TaskCompletionSource<bool> popupCompletion = TutorialManager.PopupCompletion;
					if (popupCompletion != null)
					{
						popupCompletion.SetResult(true);
					}
				}
				catch (Exception ex)
				{
					MetricsManager.LogException("Tutorial Popup Update", ex, "game_exception");
				}
				this.ShowingPopup = false;
				this.ClearHighlight();
				GameManager.Instance.SetActiveLayersForNavCategory(GameManager.Instance.currentNavCategory, false);
				try
				{
					if (this.afterPopup != null)
					{
						Action action = this.afterPopup;
						this.afterPopup = null;
						action();
					}
				}
				catch (Exception ex2)
				{
					MetricsManager.LogException("After tutorial popup", ex2, "game_exception");
				}
				return;
			}
			if (this.PopupCellLocation != null)
			{
				this.HighlightCell(this.PopupCellLocation.X, this.PopupCellLocation.Y, this.PopupText, this.PopupDirectionHint, (float)this.popupPaddingX, (float)this.popupPaddingY, this.popupBottomMargin);
				this.PopupAcceptButton.SetActive(true);
				return;
			}
			if (this.PopupControlID != null)
			{
				if (!this.HighlightByCID(this.PopupControlID, this.PopupText, this.PopupDirectionHint, this.popupPaddingX, this.popupPaddingY, this.popupBottomMargin, "horiz"))
				{
					this.HighlightCell(0, 0, this.PopupText, this.PopupDirectionHint, (float)this.popupPaddingX, (float)this.popupPaddingY, this.popupBottomMargin);
				}
				this.PopupAcceptButton.SetActive(true);
				return;
			}
		}
		else if (TutorialManager.currentStep == null)
		{
			if (this.highlightRect.gameObject.activeInHierarchy)
			{
				this.highlightRect.gameObject.SetActive(false);
				return;
			}
		}
		else
		{
			this.PopupAcceptButton.SetActive(false);
			try
			{
				TutorialStep tutorialStep = TutorialManager.currentStep;
				if (tutorialStep != null)
				{
					tutorialStep.LateUpdate();
				}
			}
			catch (Exception ex3)
			{
				MetricsManager.LogException("TutorialManager::LateUpdate", ex3, "game_exception");
			}
			base.transform.SetAsLastSibling();
		}
	}

	// Token: 0x0600045E RID: 1118 RVA: 0x0001DA68 File Offset: 0x0001BC68
	protected void _GameSync()
	{
		TutorialStep tutorialStep = TutorialManager.currentStep;
		if (tutorialStep == null)
		{
			return;
		}
		tutorialStep.GameSync();
	}

	// Token: 0x0600045F RID: 1119 RVA: 0x0001DA79 File Offset: 0x0001BC79
	public static void GameSync()
	{
		TutorialManager.instance._GameSync();
	}

	// Token: 0x0400033B RID: 827
	private static TutorialManager instance;

	// Token: 0x0400033C RID: 828
	private float lastPaddingX = 64f;

	// Token: 0x0400033D RID: 829
	private float lastPaddingY = 64f;

	// Token: 0x0400033E RID: 830
	private string lastStyle = "big";

	// Token: 0x0400033F RID: 831
	public global::UnityEngine.GameObject bigFrame;

	// Token: 0x04000340 RID: 832
	public global::UnityEngine.GameObject horizFrame;

	// Token: 0x04000341 RID: 833
	public global::UnityEngine.GameObject cellFrame;

	// Token: 0x04000342 RID: 834
	private int popupPaddingX;

	// Token: 0x04000343 RID: 835
	private int popupPaddingY;

	// Token: 0x04000344 RID: 836
	private float popupBottomMargin;

	// Token: 0x04000345 RID: 837
	private bool ShowingPopup;

	// Token: 0x04000346 RID: 838
	private string PopupText;

	// Token: 0x04000347 RID: 839
	private Location2D _PopupCellLocation;

	// Token: 0x04000348 RID: 840
	public string PopupControlID;

	// Token: 0x04000349 RID: 841
	public string PopupDirectionHint;

	// Token: 0x0400034A RID: 842
	private static TaskCompletionSource<bool> PopupCompletion = null;

	// Token: 0x0400034B RID: 843
	public Action afterPopup;

	// Token: 0x0400034C RID: 844
	public UITextSkin buttonText;

	// Token: 0x0400034D RID: 845
	private static string lastSoundText = null;

	// Token: 0x0400034E RID: 846
	public RectTransform highlightRect;

	// Token: 0x0400034F RID: 847
	public string highlightStyle = "big";

	// Token: 0x04000350 RID: 848
	public static TutorialStep currentStep;

	// Token: 0x04000351 RID: 849
	public global::UnityEngine.GameObject PopupAcceptButton;

	// Token: 0x04000352 RID: 850
	private static List<string> keysByLength = null;

	// Token: 0x04000353 RID: 851
	public global::UnityEngine.GameObject buttonArrow;

	// Token: 0x04000354 RID: 852
	public RectTransform windowRect;

	// Token: 0x04000355 RID: 853
	private RectTransform lastHighlight;

	// Token: 0x04000356 RID: 854
	public UITextSkin highlightText;

	// Token: 0x04000357 RID: 855
	public string lastText;

	// Token: 0x04000358 RID: 856
	public string lastDirectionHint;

	// Token: 0x04000359 RID: 857
	public Image[] imagesToHideWhenNoFrame;

	// Token: 0x0400035A RID: 858
	public TextMeshProUGUI[] textToHideWhenNoFrame;

	// Token: 0x0400035B RID: 859
	public global::UnityEngine.GameObject[] goToHideWhenNoFrame;

	// Token: 0x0400035C RID: 860
	public static int TWIDDLE_MENU_YPADDING = 6;

	// Token: 0x0400035D RID: 861
	public bool popupAcceptPressed;

	// Token: 0x02001597 RID: 5527
	[Serializable]
	public class TutorialManagerGameSystem : IGameSystem
	{
		// Token: 0x0600D5B6 RID: 54710 RVA: 0x004F53D0 File Offset: 0x004F35D0
		public override void BeforeSave()
		{
			if (TutorialManager.currentStep != null)
			{
				TutorialStep currentStep = TutorialManager.currentStep;
				this.StepClass = ((currentStep != null) ? currentStep.GetType().ToString() : null);
				TutorialStep currentStep2 = TutorialManager.currentStep;
				this.Step = ((currentStep2 != null) ? currentStep2.step : 0);
				return;
			}
			this.StepClass = null;
			this.Step = 0;
		}

		// Token: 0x0600D5B7 RID: 54711 RVA: 0x004F5428 File Offset: 0x004F3628
		public override void AfterLoad(XRLGame game)
		{
			if (!string.IsNullOrEmpty(this.StepClass))
			{
				try
				{
					TutorialStep tutorialStep = ModManager.CreateInstance(this.StepClass) as TutorialStep;
					if (tutorialStep != null)
					{
						tutorialStep.step = this.Step;
					}
					TutorialManager.StartTutorial(tutorialStep);
					return;
				}
				catch (Exception ex)
				{
					MetricsManager.LogException("TutorialManagerGameSystem::AfterLoad", ex, "game_exception");
					return;
				}
			}
			TutorialManager.currentStep = null;
		}

		// Token: 0x040056D0 RID: 22224
		public string StepClass;

		// Token: 0x040056D1 RID: 22225
		public int Step;
	}
}

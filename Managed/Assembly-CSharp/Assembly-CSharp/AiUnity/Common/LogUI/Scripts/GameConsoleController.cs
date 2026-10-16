using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AiUnity.Common.Extensions;
using AiUnity.Common.Log;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AiUnity.Common.LogUI.Scripts
{
	// Token: 0x020002C9 RID: 713
	public class GameConsoleController : MonoBehaviour, IGameConsoleController, IButtonStateChange, IEventSystemHandler
	{
		// Token: 0x1700035E RID: 862
		// (get) Token: 0x060018A5 RID: 6309 RVA: 0x0007F98B File Offset: 0x0007DB8B
		// (set) Token: 0x060018A6 RID: 6310 RVA: 0x0007F993 File Offset: 0x0007DB93
		private LogLevels LogLevelsFilter { get; set; }

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x060018A7 RID: 6311 RVA: 0x0007F99C File Offset: 0x0007DB9C
		// (set) Token: 0x060018A8 RID: 6312 RVA: 0x0007F9A4 File Offset: 0x0007DBA4
		private List<LogMessage> LogMessages { get; set; }

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x060018A9 RID: 6313 RVA: 0x0007F9AD File Offset: 0x0007DBAD
		// (set) Token: 0x060018AA RID: 6314 RVA: 0x0007F9B5 File Offset: 0x0007DBB5
		private int MessageCharacters { get; set; }

		// Token: 0x060018AB RID: 6315 RVA: 0x0007F9C0 File Offset: 0x0007DBC0
		private void Awake()
		{
			this.LogMessages = new List<LogMessage>();
			this.messageBuffer = new StringBuilder();
			if (global::UnityEngine.Object.FindFirstObjectByType<EventSystem>() == null)
			{
				new GameObject("EventSystem", new Type[] { typeof(EventSystem) }).AddComponent<StandaloneInputModule>();
			}
			this.messageText = this.TextBoxPanel.GetComponentInChildren<Text>() ?? this.TextBoxPanel.AddComponent<Text>();
			this.messageText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
		}

		// Token: 0x060018AC RID: 6316 RVA: 0x0007FA48 File Offset: 0x0007DC48
		private void Start()
		{
			this.FontInputField.onEndEdit.AddListener(delegate(string i)
			{
				this.SetFontSize(i, false, true);
			});
			this.FilterDropDown.onValueChanged.AddListener(delegate(int i)
			{
				this.SetMaxLogLevelFilter(i, false, true);
			});
			this.gameConsoleIconTransform = (RectTransform)this.GameConsoleIcon.transform;
			this.gameConsoleIconImage = this.GameConsoleIcon.GetComponent<Image>();
			this.gameConsolePanelTransform = (RectTransform)this.GameConsolePanel.transform;
			this.gameConsoleScrollRect = this.GameConsolePanel.GetComponentsInChildren<ScrollRect>().FirstOrDefault<ScrollRect>();
			this.gameConsoleScrollRect.verticalNormalizedPosition = 0f;
			this.titlePanel = this.GameConsolePanel.transform.Find("TitlePanel").gameObject;
		}

		// Token: 0x060018AD RID: 6317 RVA: 0x0007FB10 File Offset: 0x0007DD10
		private void Update()
		{
			if (this.firstUpdate)
			{
				this.UpdateGameConsole();
				this.firstUpdate = false;
			}
			if (this.gameConsoleScrollRect != null && this.AutoScrollToggle.isOn)
			{
				this.gameConsoleScrollRect.verticalNormalizedPosition = 0f;
			}
		}

		// Token: 0x060018AE RID: 6318 RVA: 0x0007FB5D File Offset: 0x0007DD5D
		public void ButtonStateChange(ButtonState buttonState)
		{
			this.EnableDrag(buttonState == ButtonState.Pressed);
		}

		// Token: 0x060018AF RID: 6319 RVA: 0x0007FB6C File Offset: 0x0007DD6C
		public void EnableDrag(bool enable)
		{
			DragPanel[] componentsInChildren = base.GetComponentsInChildren<DragPanel>(true);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].enabled = enable;
			}
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x0007FB98 File Offset: 0x0007DD98
		public void EnableConfigurationScreen()
		{
			this.titlePanel.SetActive(!this.titlePanel.activeSelf);
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x0007FBB4 File Offset: 0x0007DDB4
		public void AddMessage(int logLevel, string message, string loggerName = null, DateTime dateTime = default(DateTime))
		{
			LogMessage logMessage = new LogMessage(message, logLevel, loggerName, dateTime);
			this.LogMessages.Add(logMessage);
			if ((this.LogLevelsFilter & (LogLevels)logLevel) == (LogLevels)logLevel)
			{
				this.AppendMessage(message);
			}
		}

		// Token: 0x060018B2 RID: 6322 RVA: 0x0007FBEA File Offset: 0x0007DDEA
		public void ClearMessages()
		{
			this.ReplaceBuffer(string.Empty);
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x0007FBF7 File Offset: 0x0007DDF7
		public IEnumerator MinHandler(bool minimize, bool animate = true)
		{
			if (minimize)
			{
				this.windowStorage.Store(this.gameConsolePanelTransform);
				this.GameConsoleIcon.SetActive(true);
				Vector3 vector = this.gameConsoleIconTransform.position + this.gameConsoleIconTransform.rect.center;
				yield return base.StartCoroutine(this.AnimateGameConsole(vector, Vector3.zero, 1f, animate));
				this.GameConsolePanel.SetActive(false);
			}
			else
			{
				this.GameConsolePanel.SetActive(true);
				yield return base.StartCoroutine(this.AnimateGameConsole(this.windowStorage.position, Vector3.one, 0f, animate));
				this.GameConsoleIcon.SetActive(false);
			}
			yield break;
		}

		// Token: 0x060018B4 RID: 6324 RVA: 0x0007FC14 File Offset: 0x0007DE14
		public void MinHandlerAnimate(bool minimize)
		{
			base.StartCoroutine(this.MinHandler(minimize, true));
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x0007FC25 File Offset: 0x0007DE25
		public void SetConsoleActive(bool consoleActive)
		{
			this.GameConsolePanel.SetActive(consoleActive);
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x0007FC33 File Offset: 0x0007DE33
		public void SetFontSize(int fontSize, bool updateControl = true, bool updateMessage = true)
		{
			if (updateControl)
			{
				this.FontInputField.text = fontSize.ToString();
			}
			if (updateMessage)
			{
				this.messageText.fontSize = fontSize;
			}
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x0007FC59 File Offset: 0x0007DE59
		public void SetIconActive(bool iconActive)
		{
			if (this.iconEnable)
			{
				this.GameConsoleIcon.SetActive(iconActive);
			}
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x0007FC6F File Offset: 0x0007DE6F
		public void SetIconEnable(bool iconEnable)
		{
			if (!iconEnable)
			{
				this.GameConsoleIcon.SetActive(false);
			}
			this.iconEnable = iconEnable;
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x0007FC88 File Offset: 0x0007DE88
		public void SetLogLevelFilter(LogLevels logLevels, bool updateControl = true, bool updateMessage = true)
		{
			this.LogLevelsFilter = logLevels;
			if (updateControl)
			{
				double num = (double)logLevels.GetFlags(false, false).Max<LogLevels>();
				this.FilterDropDown.value = ((num > 0.0) ? ((int)Math.Log(num, 2.0) + 1) : 0);
			}
			if (updateMessage)
			{
				this.FilterMessages();
			}
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x0007FCE3 File Offset: 0x0007DEE3
		public void SetMaxLogLevelFilter(int maxLevel, bool updateControl = true, bool updateMessage = true)
		{
			this.SetLogLevelFilter((int)(Math.Pow(2.0, (double)maxLevel) - 1.0), updateControl, updateMessage);
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x0007FD08 File Offset: 0x0007DF08
		private IEnumerator AnimateGameConsole(Vector3 targetConsolePosition, Vector3 targetConsoleScale, float targetIconAlpha, bool animate)
		{
			float timeToLerp = 0.8f;
			float timeLerped = (animate ? 0f : timeToLerp);
			Vector3 startingPosition = this.gameConsolePanelTransform.position;
			Vector3 startingScale = this.gameConsolePanelTransform.localScale;
			float startAlpha = this.gameConsoleIconImage.color.a;
			Color sourceColor = this.gameConsoleIconImage.color;
			while (timeLerped <= timeToLerp)
			{
				timeLerped += Time.deltaTime;
				this.gameConsolePanelTransform.position = Vector3.Lerp(startingPosition, targetConsolePosition, timeLerped / timeToLerp);
				this.gameConsolePanelTransform.localScale = Vector3.Lerp(startingScale, targetConsoleScale, timeLerped / timeToLerp);
				sourceColor.a = Mathf.Lerp(startAlpha, targetIconAlpha, timeLerped / timeToLerp * (timeLerped / timeToLerp));
				this.gameConsoleIconImage.color = sourceColor;
				if (animate)
				{
					yield return null;
				}
			}
			yield break;
		}

		// Token: 0x060018BC RID: 6332 RVA: 0x0007FD34 File Offset: 0x0007DF34
		private void AppendMessage(string message)
		{
			this.messageText.text = this.messageBuffer.Append(message).ToString();
			this.LimitMessageLength();
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x0007FD58 File Offset: 0x0007DF58
		private void FilterMessages()
		{
			this.ReplaceBuffer(string.Concat((from m in this.LogMessages
				where this.LogLevelsFilter.Has(m.LogLevels)
				select m.Message).ToArray<string>()));
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x0007FDB0 File Offset: 0x0007DFB0
		private void LimitMessageLength()
		{
			if (this.messageText.text.Length > 12000)
			{
				int num = this.messageText.text.IndexOf(Environment.NewLine, 3000);
				this.messageBuffer = new StringBuilder(this.messageText.text.Substring(num));
				this.messageText.text = this.messageBuffer.ToString();
			}
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x0007FE21 File Offset: 0x0007E021
		private void ReplaceBuffer(string buffer)
		{
			this.messageBuffer = new StringBuilder(buffer);
			this.messageText.text = this.messageBuffer.ToString();
			this.LimitMessageLength();
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x0007FE4C File Offset: 0x0007E04C
		private void SetFontSize(string fontSizeText, bool updateControl = true, bool updateMessage = true)
		{
			int num = 0;
			if (int.TryParse(fontSizeText, out num))
			{
				this.SetFontSize(num, updateControl, updateMessage);
			}
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x0007FE70 File Offset: 0x0007E070
		private void SetLogLevelFilter(int level, bool updateControl = true, bool updateMessage = true)
		{
			int num = ((level <= 0) ? level : ((int)Math.Pow(2.0, (double)((int)Math.Log((double)level, 2.0) + 1)) - 1));
			this.SetLogLevelFilter(num.ToEnum<LogLevels>(), updateControl, updateMessage);
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x0007FEB8 File Offset: 0x0007E0B8
		private void UpdateGameConsole()
		{
			this.windowStorage.Store(this.gameConsolePanelTransform);
			base.StartCoroutine(this.MinHandler(!this.GameConsolePanel.activeInHierarchy, false));
		}

		// Token: 0x04000E9C RID: 3740
		public Toggle AutoScrollToggle;

		// Token: 0x04000E9D RID: 3741
		public Dropdown FilterDropDown;

		// Token: 0x04000E9E RID: 3742
		public InputField FontInputField;

		// Token: 0x04000E9F RID: 3743
		public GameObject GameConsoleIcon;

		// Token: 0x04000EA0 RID: 3744
		public GameObject GameConsolePanel;

		// Token: 0x04000EA1 RID: 3745
		public GameObject TextBoxPanel;

		// Token: 0x04000EA2 RID: 3746
		public GameObject MoveButton;

		// Token: 0x04000EA3 RID: 3747
		private Text messageText;

		// Token: 0x04000EA4 RID: 3748
		private bool firstUpdate = true;

		// Token: 0x04000EA5 RID: 3749
		private Image gameConsoleIconImage;

		// Token: 0x04000EA6 RID: 3750
		private RectTransform gameConsoleIconTransform;

		// Token: 0x04000EA7 RID: 3751
		private RectTransform gameConsolePanelTransform;

		// Token: 0x04000EA8 RID: 3752
		private ScrollRect gameConsoleScrollRect;

		// Token: 0x04000EA9 RID: 3753
		private bool iconEnable = true;

		// Token: 0x04000EAA RID: 3754
		private StringBuilder messageBuffer;

		// Token: 0x04000EAB RID: 3755
		private RectTransformStorage windowStorage;

		// Token: 0x04000EAC RID: 3756
		private GameObject titlePanel;
	}
}

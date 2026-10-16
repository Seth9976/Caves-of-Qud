using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using XRL;
using XRL.UI;

namespace Qud.UI
{
	// Token: 0x02000412 RID: 1042
	[HasModSensitiveStaticCache]
	public class UIManager : MonoBehaviour
	{
		// Token: 0x06002779 RID: 10105 RVA: 0x000CCC10 File Offset: 0x000CAE10
		public static async void FadeUIOut(float time)
		{
			await The.UiContext;
			UIManager.instance.StartCoroutine(UIManager.instance._FadeOut(time));
		}

		// Token: 0x0600277A RID: 10106 RVA: 0x000CCC48 File Offset: 0x000CAE48
		public static async void FadeUIIn(float time)
		{
			await The.UiContext;
			UIManager.instance.StartCoroutine(UIManager.instance._FadeIn(time));
		}

		// Token: 0x0600277B RID: 10107 RVA: 0x000CCC7F File Offset: 0x000CAE7F
		private IEnumerator _FadeOut(float time)
		{
			CanvasGroup canvasGroup = base.GetComponent<CanvasGroup>();
			float start = time;
			while (time > 0f)
			{
				time -= Time.deltaTime;
				canvasGroup.alpha = Mathf.Lerp(1f, 0f, 1f - time / start);
				yield return null;
			}
			canvasGroup.alpha = 0f;
			yield break;
		}

		// Token: 0x0600277C RID: 10108 RVA: 0x000CCC95 File Offset: 0x000CAE95
		public IEnumerator _FadeIn(float time)
		{
			CanvasGroup canvasGroup = base.GetComponent<CanvasGroup>();
			float start = time;
			while (time > 0f)
			{
				time -= Time.deltaTime;
				canvasGroup.alpha = Mathf.Lerp(0f, 1f, 1f - time / start);
				yield return null;
			}
			canvasGroup.alpha = 1f;
			yield break;
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x0600277D RID: 10109 RVA: 0x000CCCAB File Offset: 0x000CAEAB
		// (set) Token: 0x0600277E RID: 10110 RVA: 0x000CCCB3 File Offset: 0x000CAEB3
		public WindowBase currentWindow
		{
			get
			{
				return this._currentWindow;
			}
			set
			{
				this._currentWindow = value;
			}
		}

		// Token: 0x0600277F RID: 10111 RVA: 0x000CCCBC File Offset: 0x000CAEBC
		public void DestroyWindow(WindowBase window)
		{
			if (window == null)
			{
				return;
			}
			this.windowsByName.Where<KeyValuePair<string, WindowBase>>((KeyValuePair<string, WindowBase> kv) => kv.Value == window).ToList<KeyValuePair<string, WindowBase>>().ForEach(delegate(KeyValuePair<string, WindowBase> kv)
			{
				this.windowsByName.Remove(kv.Key);
			});
			this.windows.Remove(window);
			global::UnityEngine.Object.Destroy(window.gameObject);
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x06002780 RID: 10112 RVA: 0x000CCD3B File Offset: 0x000CAF3B
		public static bool UseNewPopups
		{
			get
			{
				return Options.ModernUI;
			}
		}

		// Token: 0x06002781 RID: 10113 RVA: 0x000CCD42 File Offset: 0x000CAF42
		public static void pushSaveStack()
		{
			UIManager.instance.saveStack.Push(new Stack<WindowBase>(UIManager.instance.windowStack));
		}

		// Token: 0x06002782 RID: 10114 RVA: 0x000CCD62 File Offset: 0x000CAF62
		public static void popSaveStack()
		{
			UIManager.instance.windowStack = UIManager.instance.saveStack.Pop();
		}

		// Token: 0x06002783 RID: 10115 RVA: 0x000CCD7D File Offset: 0x000CAF7D
		public Vector2 WorldspaceToCanvasSpace(Vector3 WorldSpace, Camera camera)
		{
			if (camera == null)
			{
				return Vector2.zero;
			}
			return camera.WorldToScreenPoint(WorldSpace);
		}

		// Token: 0x06002784 RID: 10116 RVA: 0x000CCD9C File Offset: 0x000CAF9C
		public bool PassthroughOnTop()
		{
			if (GameManager.Instance.CurrentGameView != null && GameManager.Instance.CurrentGameView.StartsWith("ModernPopup"))
			{
				return false;
			}
			if (!Options.ModernCharacterSheet && Options.ModernUI)
			{
				if (GameManager.Instance._ActiveGameView == "Equipment")
				{
					return true;
				}
				if (GameManager.Instance._ActiveGameView == "Inventory")
				{
					return true;
				}
				if (GameManager.Instance._ActiveGameView == "Status")
				{
					return true;
				}
				if (GameManager.Instance._ActiveGameView == "Factions")
				{
					return true;
				}
				if (GameManager.Instance._ActiveGameView == "Quests")
				{
					return true;
				}
				if (GameManager.Instance._ActiveGameView == "Journal")
				{
					return true;
				}
				if (GameManager.Instance._ActiveGameView == "Tinkering")
				{
					return true;
				}
				if (GameManager.Instance._ActiveGameView == "SkillsAndPowers")
				{
					return true;
				}
			}
			return UIManager.instance.currentWindow == null || UIManager.instance.currentWindow.AllowPassthroughInput();
		}

		// Token: 0x06002785 RID: 10117 RVA: 0x000CCEC4 File Offset: 0x000CB0C4
		public bool AllowPassthroughInput()
		{
			if (!Options.ModernCharacterSheet && Options.ModernUI)
			{
				if (GameManager.Instance._ActiveGameView == "Equipment")
				{
					return true;
				}
				if (GameManager.Instance._ActiveGameView == "Inventory")
				{
					return true;
				}
				if (GameManager.Instance._ActiveGameView == "Status")
				{
					return true;
				}
				if (GameManager.Instance._ActiveGameView == "Factions")
				{
					return true;
				}
				if (GameManager.Instance._ActiveGameView == "Quests")
				{
					return true;
				}
				if (GameManager.Instance._ActiveGameView == "Journal")
				{
					return true;
				}
				if (GameManager.Instance._ActiveGameView == "Tinkering")
				{
					return true;
				}
				if (GameManager.Instance._ActiveGameView == "SkillsAndPowers")
				{
					return true;
				}
			}
			for (int i = 0; i < this.windows.Count; i++)
			{
				if (this.windows[i].Visible && !this.windows[i].AllowPassthroughInput())
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002786 RID: 10118 RVA: 0x000CCFE4 File Offset: 0x000CB1E4
		public static void showWindow(string name, bool aggressive = false)
		{
			if (UIManager.instance.currentWindow != null && UIManager.instance.currentWindow.name == name)
			{
				return;
			}
			if (aggressive)
			{
				foreach (WindowBase windowBase in UIManager.instance.windows)
				{
					windowBase._HideWithoutLeave();
				}
			}
			UIManager.instance._showWindow(name);
		}

		// Token: 0x06002787 RID: 10119 RVA: 0x000CD070 File Offset: 0x000CB270
		public static T getWindow<T>(string name) where T : WindowBase
		{
			return UIManager.instance.windowsByName[name] as T;
		}

		// Token: 0x06002788 RID: 10120 RVA: 0x000CD08C File Offset: 0x000CB28C
		public static WindowBase getWindow(string name)
		{
			if (UIManager.instance == null)
			{
				return null;
			}
			if (UIManager.instance.windowsByName == null)
			{
				return null;
			}
			if (!UIManager.instance.windowsByName.ContainsKey(name))
			{
				return null;
			}
			return UIManager.instance.windowsByName[name];
		}

		// Token: 0x06002789 RID: 10121 RVA: 0x000CD0DC File Offset: 0x000CB2DC
		public static WindowBase createWindow(string name, Type scriptType = null, Transform parent = null)
		{
			if (scriptType == null)
			{
				scriptType = typeof(WindowBase);
			}
			if (parent == null)
			{
				parent = UIManager.instance.transform;
			}
			GameObject gameObject = new GameObject();
			gameObject.SetActive(false);
			gameObject.AddComponent(scriptType);
			gameObject.transform.SetParent(parent, false);
			gameObject.transform.SetAsLastSibling();
			RectTransform component = gameObject.GetComponent<RectTransform>();
			component.anchorMin = Vector2.zero;
			component.anchorMax = Vector2.one;
			component.sizeDelta = Vector2.zero;
			WindowBase component2 = gameObject.GetComponent<WindowBase>();
			UIManager.instance.windowsByName.Set(name, component2);
			return component2;
		}

		// Token: 0x0600278A RID: 10122 RVA: 0x000CD180 File Offset: 0x000CB380
		public static WindowBase copyWindow(string name)
		{
			Queue<WindowBase> queue = UIManager.popupMessages;
			WindowBase windowBase;
			lock (queue)
			{
				if (UIManager.popupMessages.Count > 0)
				{
					windowBase = UIManager.popupMessages.Dequeue();
					windowBase.gameObject.SetActive(true);
				}
				else
				{
					windowBase = global::UnityEngine.Object.Instantiate<WindowBase>(UIManager.instance.windowsByName[name]);
					windowBase.gameObject.name = string.Format("{0} #{1}", name, UIManager.copyNumber++);
					UIManager.instance.windowsByName[windowBase.gameObject.name] = windowBase;
					windowBase.transform.SetParent(UIManager.instance.transform, false);
				}
			}
			return windowBase;
		}

		// Token: 0x0600278B RID: 10123 RVA: 0x000CD250 File Offset: 0x000CB450
		public static void releaseCopy(WindowBase window)
		{
			Queue<WindowBase> queue = UIManager.popupMessages;
			lock (queue)
			{
				if (!UIManager.popupMessages.Contains(window))
				{
					UIManager.popupMessages.Enqueue(window);
					window.gameObject.SetActive(false);
				}
			}
		}

		// Token: 0x0600278C RID: 10124 RVA: 0x000CD2B0 File Offset: 0x000CB4B0
		public static void pushWindow(string name, bool hideOld = false)
		{
			GameManager.Instance.uiQueue.awaitTask(delegate
			{
				if (UIManager.instance.currentWindow != null)
				{
					UIManager.instance.windowStack.Push(UIManager.instance.currentWindow);
					if (!hideOld)
					{
						UIManager.instance.currentWindow = null;
					}
				}
				UIManager.showWindow(name, false);
			});
		}

		// Token: 0x0600278D RID: 10125 RVA: 0x000CD2EC File Offset: 0x000CB4EC
		public static void popWindow()
		{
			if (UIManager.instance.currentWindow != null)
			{
				UIManager.instance.currentWindow.Hide();
				UIManager.instance.currentWindow = null;
			}
			if (UIManager.instance.windowStack.Count > 0)
			{
				UIManager.instance.currentWindow = UIManager.instance.windowStack.Pop();
				UIManager.instance.currentWindow.Show();
			}
		}

		// Token: 0x0600278E RID: 10126 RVA: 0x000CD360 File Offset: 0x000CB560
		private void _showWindow(string name)
		{
			DebugConsole.WriteLine("[UIManager] showing window " + name);
			if (name != null)
			{
				if (this.currentWindow != null && GameManager.Instance.GetViewData(name).OverlayMode == 0 && (!(this.currentWindow == SingletonWindowBase<StatusScreensScreen>.instance) || !(name == "PickGameObject")))
				{
					this.currentWindow.Hide();
				}
				this.currentWindow = this.windowsByName[name];
				this.currentWindow.Show();
				return;
			}
			WindowBase currentWindow = this.currentWindow;
			if (currentWindow != null)
			{
				currentWindow.Hide();
			}
			this.currentWindow = null;
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x0600278F RID: 10127 RVA: 0x000CD400 File Offset: 0x000CB600
		public static float width
		{
			get
			{
				UIManager uimanager = UIManager.instance;
				if (((uimanager != null) ? uimanager.gameObject : null) == null)
				{
					return 1920f;
				}
				UIManager uimanager2 = UIManager.instance;
				float? num;
				if (uimanager2 == null)
				{
					num = null;
				}
				else
				{
					GameObject gameObject = uimanager2.gameObject;
					if (gameObject == null)
					{
						num = null;
					}
					else
					{
						RectTransform component = gameObject.GetComponent<RectTransform>();
						num = ((component != null) ? new float?(component.sizeDelta.x) : null);
					}
				}
				float? num2 = num;
				if (num2 == null)
				{
					return 1920f;
				}
				return num2.GetValueOrDefault();
			}
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06002790 RID: 10128 RVA: 0x000CD490 File Offset: 0x000CB690
		public static float height
		{
			get
			{
				UIManager uimanager = UIManager.instance;
				if (((uimanager != null) ? uimanager.gameObject : null) == null)
				{
					return 1080f;
				}
				UIManager uimanager2 = UIManager.instance;
				float? num;
				if (uimanager2 == null)
				{
					num = null;
				}
				else
				{
					GameObject gameObject = uimanager2.gameObject;
					if (gameObject == null)
					{
						num = null;
					}
					else
					{
						RectTransform component = gameObject.GetComponent<RectTransform>();
						num = ((component != null) ? new float?(component.sizeDelta.y) : null);
					}
				}
				float? num2 = num;
				if (num2 == null)
				{
					return 1080f;
				}
				return num2.GetValueOrDefault();
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06002791 RID: 10129 RVA: 0x000CD520 File Offset: 0x000CB720
		public static float scale
		{
			get
			{
				UIManager uimanager = UIManager.instance;
				float? num;
				if (uimanager == null)
				{
					num = null;
				}
				else
				{
					CanvasScaler scaler = uimanager._scaler;
					num = ((scaler != null) ? new float?(scaler.scaleFactor) : null);
				}
				float? num2 = num;
				if (num2 == null)
				{
					return 1f;
				}
				return num2.GetValueOrDefault();
			}
		}

		// Token: 0x06002792 RID: 10130 RVA: 0x000CD578 File Offset: 0x000CB778
		public void DiscoverWindows(string basePath, Transform root)
		{
			foreach (object obj in root)
			{
				Transform transform = (Transform)obj;
				WindowBase component = transform.gameObject.GetComponent<WindowBase>();
				if (component != null)
				{
					this.windows.Add(component);
					this.windowsByName.Add(basePath + component.name, component);
				}
				else if (basePath == "")
				{
					this.DiscoverWindows(transform.name + "/", transform.transform);
				}
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06002793 RID: 10131 RVA: 0x000CD62C File Offset: 0x000CB82C
		// (set) Token: 0x06002794 RID: 10132 RVA: 0x000CD633 File Offset: 0x000CB833
		public static int WindowFramePin
		{
			get
			{
				return UIManager._WindowFramePin;
			}
			set
			{
				UIManager._WindowFramePin = value;
				PlayerPrefs.SetInt("_WindowFramePin", value);
			}
		}

		// Token: 0x06002795 RID: 10133 RVA: 0x000CD648 File Offset: 0x000CB848
		public void Init()
		{
			UIManager.mainCanvas = base.GetComponent<Canvas>();
			UIManager._WindowFramePin = PlayerPrefs.GetInt("_WindowFramePin", 0);
			UIManager.instance = this;
			this._scaler = base.GetComponent<CanvasScaler>();
			this.allKeyCodes = Enum.GetValues(typeof(KeyCode));
			this.windows = new List<WindowBase>();
			this.windowsByName = new Dictionary<string, WindowBase>();
			this.DiscoverWindows("", base.gameObject.transform);
			foreach (WindowBase windowBase in this.windows)
			{
				try
				{
					if (windowBase.GetComponent<CanvasGroup>() == null)
					{
						windowBase.gameObject.AddComponent<CanvasGroup>();
					}
					windowBase.Init();
					windowBase.Hide();
				}
				catch (Exception ex)
				{
					MetricsManager.LogException("Exception intializing window " + windowBase.name, ex, "game_exception");
				}
			}
			this.Update();
			UIManager.initComplete = true;
		}

		// Token: 0x06002796 RID: 10134 RVA: 0x000CD760 File Offset: 0x000CB960
		[ModSensitiveCacheInit]
		private static void ModReload()
		{
			GameManager.Instance.uiQueue.queueTask(delegate
			{
				if (UIManager.instance != null)
				{
					foreach (WindowBase windowBase in UIManager.instance.windows)
					{
						windowBase.Init();
					}
				}
			}, 0);
		}

		// Token: 0x06002797 RID: 10135 RVA: 0x000CD792 File Offset: 0x000CB992
		private void Update()
		{
			if (!Mathf.Approximately((float)Options.StageScale, this._scaler.scaleFactor))
			{
				this._scaler.scaleFactor = (float)Options.StageScale;
				Canvas.ForceUpdateCanvases();
			}
		}

		// Token: 0x04001382 RID: 4994
		public static Canvas mainCanvas;

		// Token: 0x04001383 RID: 4995
		private Array allKeyCodes;

		// Token: 0x04001384 RID: 4996
		public List<WindowBase> windows = new List<WindowBase>();

		// Token: 0x04001385 RID: 4997
		public Sprite DefaultSquare;

		// Token: 0x04001386 RID: 4998
		public static UIManager instance;

		// Token: 0x04001387 RID: 4999
		public Dictionary<string, WindowBase> windowsByName = new Dictionary<string, WindowBase>();

		// Token: 0x04001388 RID: 5000
		public WindowBase _currentWindow;

		// Token: 0x04001389 RID: 5001
		private Stack<Stack<WindowBase>> saveStack = new Stack<Stack<WindowBase>>();

		// Token: 0x0400138A RID: 5002
		private Stack<WindowBase> windowStack = new Stack<WindowBase>();

		// Token: 0x0400138B RID: 5003
		private static uint copyNumber = 0U;

		// Token: 0x0400138C RID: 5004
		private static Queue<WindowBase> popupMessages = new Queue<WindowBase>();

		// Token: 0x0400138D RID: 5005
		private CanvasScaler _scaler;

		// Token: 0x0400138E RID: 5006
		public static int _WindowFramePin = 0;

		// Token: 0x0400138F RID: 5007
		public static bool initComplete = false;
	}
}

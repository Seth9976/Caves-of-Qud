using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000EB RID: 235
[ExecuteInEditMode]
public class exDebugHelper : MonoBehaviour
{
	// Token: 0x060008D6 RID: 2262 RVA: 0x00038E5D File Offset: 0x0003705D
	public static void ScreenPrint(string _text)
	{
		if (exDebugHelper.instance.showScreenPrint_)
		{
			exDebugHelper.instance.txtPrint = exDebugHelper.instance.txtPrint + _text + "\n";
		}
	}

	// Token: 0x060008D7 RID: 2263 RVA: 0x00038E8C File Offset: 0x0003708C
	public static void ScreenPrint(Vector2 _pos, string _text, GUIStyle _style = null)
	{
		if (exDebugHelper.instance.showScreenDebugText)
		{
			exDebugHelper.TextInfo textInfo = new exDebugHelper.TextInfo(_pos, _text, _style);
			exDebugHelper.instance.debugTextPool.Add(textInfo);
		}
	}

	// Token: 0x060008D8 RID: 2264 RVA: 0x00038EC0 File Offset: 0x000370C0
	public static void ScreenLog(string _text, exDebugHelper.LogType _logType = exDebugHelper.LogType.None, GUIStyle _style = null, bool autoFadeOut = true)
	{
		exDebugHelper.LogInfo logInfo = new exDebugHelper.LogInfo(_text, _style, autoFadeOut ? 5f : 0f);
		exDebugHelper.instance.pendingLogs.Enqueue(logInfo);
		if (_logType != exDebugHelper.LogType.None)
		{
			switch (_logType)
			{
			case exDebugHelper.LogType.Normal:
				Debug.Log(_text);
				return;
			case exDebugHelper.LogType.Warning:
				Debug.LogWarning(_text);
				return;
			case exDebugHelper.LogType.Error:
				Debug.LogError(_text);
				break;
			default:
				return;
			}
		}
	}

	// Token: 0x060008D9 RID: 2265 RVA: 0x00038F20 File Offset: 0x00037120
	public static void SetFPSColor(Color _color)
	{
		exDebugHelper.instance.fpsStyle.normal.textColor = _color;
	}

	// Token: 0x060008DA RID: 2266 RVA: 0x00038F37 File Offset: 0x00037137
	public static float GetFPS()
	{
		return exDebugHelper.instance.fps;
	}

	// Token: 0x1700012A RID: 298
	// (get) Token: 0x060008DB RID: 2267 RVA: 0x00038F43 File Offset: 0x00037143
	// (set) Token: 0x060008DC RID: 2268 RVA: 0x00038F4B File Offset: 0x0003714B
	public bool showFps
	{
		get
		{
			return this.showFps_;
		}
		set
		{
			if (this.showFps_ != value)
			{
				this.showFps_ = value;
			}
		}
	}

	// Token: 0x1700012B RID: 299
	// (get) Token: 0x060008DD RID: 2269 RVA: 0x00038F5D File Offset: 0x0003715D
	// (set) Token: 0x060008DE RID: 2270 RVA: 0x00038F65 File Offset: 0x00037165
	public bool enableTimeScaleDebug
	{
		get
		{
			return this.enableTimeScaleDebug_;
		}
		set
		{
			if (this.enableTimeScaleDebug_ != value)
			{
				this.enableTimeScaleDebug_ = value;
			}
		}
	}

	// Token: 0x1700012C RID: 300
	// (get) Token: 0x060008DF RID: 2271 RVA: 0x00038F77 File Offset: 0x00037177
	// (set) Token: 0x060008E0 RID: 2272 RVA: 0x00038F7F File Offset: 0x0003717F
	public bool showScreenPrint
	{
		get
		{
			return this.showScreenPrint_;
		}
		set
		{
			if (this.showScreenPrint_ != value)
			{
				this.showScreenPrint_ = value;
			}
		}
	}

	// Token: 0x1700012D RID: 301
	// (get) Token: 0x060008E1 RID: 2273 RVA: 0x00038F91 File Offset: 0x00037191
	// (set) Token: 0x060008E2 RID: 2274 RVA: 0x00038F99 File Offset: 0x00037199
	public bool showScreenLog
	{
		get
		{
			return this.showScreenLog_;
		}
		set
		{
			if (this.showScreenLog_ != value)
			{
				this.showScreenLog_ = value;
			}
		}
	}

	// Token: 0x060008E3 RID: 2275 RVA: 0x00038FAC File Offset: 0x000371AC
	private void Awake()
	{
		if (exDebugHelper.instance == null)
		{
			exDebugHelper.instance = this;
		}
		this.txtPrint = "";
		this.txtFPS = "";
		if (this.showScreenDebugText)
		{
			this.debugTextPool.Clear();
		}
		base.useGUILayout = false;
	}

	// Token: 0x060008E4 RID: 2276 RVA: 0x00038FFC File Offset: 0x000371FC
	private void Start()
	{
		base.InvokeRepeating("UpdateFPS", 0f, 1f);
	}

	// Token: 0x060008E5 RID: 2277 RVA: 0x00039013 File Offset: 0x00037213
	private void Update()
	{
		this.frames++;
		this.UpdateTimeScale();
		this.UpdateLog();
		base.StartCoroutine(this.CleanDebugText());
	}

	// Token: 0x060008E6 RID: 2278 RVA: 0x0003903C File Offset: 0x0003723C
	private void OnGUI()
	{
		Vector2 vector = Vector2.zero;
		float num = this.offset.x;
		float num2 = this.offset.y;
		if (this.showFps)
		{
			GUIContent guicontent = new GUIContent(this.txtFPS);
			vector = this.fpsStyle.CalcSize(guicontent);
			switch (this.fpsAnchor)
			{
			case TextAnchor.UpperCenter:
				num += ((float)Screen.width - vector.x) * 0.5f;
				break;
			case TextAnchor.UpperRight:
				num = (float)Screen.width - vector.x - num;
				break;
			case TextAnchor.MiddleLeft:
				num2 += ((float)Screen.height - vector.y) * 0.5f;
				break;
			case TextAnchor.MiddleCenter:
				num += ((float)Screen.width - vector.x) * 0.5f;
				num2 += ((float)Screen.height - vector.y) * 0.5f;
				break;
			case TextAnchor.MiddleRight:
				num = (float)Screen.width - vector.x - num;
				num2 += ((float)Screen.height - vector.y) * 0.5f;
				break;
			case TextAnchor.LowerLeft:
				num2 = (float)Screen.height - vector.y - num2;
				break;
			case TextAnchor.LowerCenter:
				num += ((float)Screen.width - vector.x) * 0.5f;
				num2 = (float)Screen.height - vector.y - num2;
				break;
			case TextAnchor.LowerRight:
				num = (float)Screen.width - vector.x - num;
				num2 = (float)Screen.height - vector.y - num2;
				break;
			}
			GUI.Label(new Rect(num, num2, vector.x, vector.y), this.txtFPS, this.fpsStyle);
			num = 10f;
			num2 = 10f + vector.y;
		}
		if (this.enableTimeScaleDebug)
		{
			string text = "TimeScale = " + Time.timeScale.ToString("f2");
			GUIContent guicontent = new GUIContent(text);
			vector = this.timeScaleStyle.CalcSize(guicontent);
			GUI.Label(new Rect(num, num2, vector.x, vector.y), text, this.timeScaleStyle);
			num2 += vector.y;
		}
		if (this.showScreenPrint)
		{
			GUIContent guicontent = new GUIContent(this.txtPrint);
			vector = this.printStyle.CalcSize(guicontent);
			GUI.Label(new Rect(num, num2, vector.x, vector.y), this.txtPrint, this.printStyle);
		}
		if (this.showScreenLog)
		{
			bool flag = this.logStyle.alignment == TextAnchor.LowerLeft || this.logStyle.alignment == TextAnchor.LowerCenter || this.logStyle.alignment == TextAnchor.LowerRight;
			bool flag2 = this.logStyle.alignment == TextAnchor.LowerRight || this.logStyle.alignment == TextAnchor.MiddleRight || this.logStyle.alignment == TextAnchor.UpperRight;
			float num3;
			if (flag)
			{
				num3 = (float)(Screen.height - 10);
			}
			else
			{
				num3 = 50f;
			}
			for (int i = this.logs.Count - 1; i >= 0; i--)
			{
				exDebugHelper.LogInfo logInfo = this.logs[i];
				GUIContent guicontent = new GUIContent(logInfo.text);
				GUIStyle guistyle = ((logInfo.style == null) ? this.logStyle : logInfo.style);
				vector = guistyle.CalcSize(guicontent);
				guistyle.normal.textColor = new Color(guistyle.normal.textColor.r, guistyle.normal.textColor.g, guistyle.normal.textColor.b, 1f - logInfo.ratio);
				if (flag)
				{
					num3 -= vector.y;
				}
				else
				{
					num3 += vector.y;
				}
				if (flag2)
				{
					GUI.Label(new Rect((float)Screen.width - 10f - vector.x, num3, vector.x, vector.y), logInfo.text, guistyle);
				}
				else
				{
					GUI.Label(new Rect(10f, num3, vector.x, vector.y), logInfo.text, guistyle);
				}
			}
		}
		if (this.showScreenDebugText)
		{
			for (int j = 0; j < this.debugTextPool.Count; j++)
			{
				exDebugHelper.TextInfo textInfo = this.debugTextPool[j];
				GUIContent guicontent = new GUIContent(textInfo.text);
				GUIStyle guistyle2 = ((textInfo.style == null) ? GUI.skin.label : textInfo.style);
				vector = guistyle2.CalcSize(guicontent);
				Vector2 vector2 = new Vector2(textInfo.screenPos.x, (float)Screen.height - textInfo.screenPos.y) - vector * 0.5f;
				GUI.Label(new Rect(vector2.x, vector2.y, vector.x, vector.y), textInfo.text, guistyle2);
			}
		}
	}

	// Token: 0x060008E7 RID: 2279 RVA: 0x00039524 File Offset: 0x00037724
	private void UpdateFPS()
	{
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		this.fps = (float)this.frames / (realtimeSinceStartup - this.lastInterval);
		this.frames = 0;
		this.lastInterval = realtimeSinceStartup;
		this.txtFPS = "fps: " + this.fps.ToString("f2");
	}

	// Token: 0x060008E8 RID: 2280 RVA: 0x0003957C File Offset: 0x0003777C
	private void UpdateTimeScale()
	{
		if (this.enableTimeScaleDebug)
		{
			if (Input.GetKey(KeyCode.Minus))
			{
				Time.timeScale = Mathf.Max(Time.timeScale - 0.01f, 0f);
			}
			else if (Input.GetKey(KeyCode.Equals))
			{
				Time.timeScale = Mathf.Min(Time.timeScale + 0.01f, 10f);
			}
			if (Input.GetKey(KeyCode.Alpha0))
			{
				Time.timeScale = 0f;
				return;
			}
			if (Input.GetKey(KeyCode.Alpha9))
			{
				Time.timeScale = 1f;
			}
		}
	}

	// Token: 0x060008E9 RID: 2281 RVA: 0x00039600 File Offset: 0x00037800
	private IEnumerator CleanDebugText()
	{
		yield return new WaitForEndOfFrame();
		this.txtPrint = "";
		if (this.showScreenDebugText)
		{
			this.debugTextPool.Clear();
		}
		yield break;
	}

	// Token: 0x060008EA RID: 2282 RVA: 0x00039610 File Offset: 0x00037810
	private void UpdateLog()
	{
		for (int i = this.logs.Count - 1; i >= 0; i--)
		{
			exDebugHelper.LogInfo logInfo = this.logs[i];
			logInfo.Tick();
			if (logInfo.canDelete)
			{
				this.logs.RemoveAt(i);
			}
		}
		bool flag = this.logStyle.alignment == TextAnchor.LowerLeft || this.logStyle.alignment == TextAnchor.LowerCenter || this.logStyle.alignment == TextAnchor.LowerRight;
		if (this.pendingLogs.Count > 0)
		{
			int num = Mathf.CeilToInt((float)(this.pendingLogs.Count / 2));
			do
			{
				if (flag)
				{
					this.logs.Add(this.pendingLogs.Dequeue());
				}
				else
				{
					this.logs.Insert(0, this.pendingLogs.Dequeue());
				}
				num--;
				if (exDebugHelper.instance.logs.Count > exDebugHelper.instance.logCount)
				{
					for (int j = 0; j < exDebugHelper.instance.logs.Count - exDebugHelper.instance.logCount; j++)
					{
						exDebugHelper.instance.logs[j].Dead();
					}
				}
			}
			while (num > 0);
		}
	}

	// Token: 0x060008EB RID: 2283 RVA: 0x0003973F File Offset: 0x0003793F
	public static void ClearScreen()
	{
		exDebugHelper.instance.pendingLogs.Clear();
		exDebugHelper.instance.logs.Clear();
	}

	// Token: 0x040005FF RID: 1535
	public static exDebugHelper instance;

	// Token: 0x04000600 RID: 1536
	public Vector2 offset = new Vector2(10f, 10f);

	// Token: 0x04000601 RID: 1537
	public GUIStyle printStyle;

	// Token: 0x04000602 RID: 1538
	public GUIStyle fpsStyle;

	// Token: 0x04000603 RID: 1539
	public GUIStyle logStyle;

	// Token: 0x04000604 RID: 1540
	public GUIStyle timeScaleStyle;

	// Token: 0x04000605 RID: 1541
	protected string txtPrint = "screen print: ";

	// Token: 0x04000606 RID: 1542
	protected string txtFPS = "fps: ";

	// Token: 0x04000607 RID: 1543
	protected List<exDebugHelper.TextInfo> debugTextPool = new List<exDebugHelper.TextInfo>();

	// Token: 0x04000608 RID: 1544
	private float logFadeOutDuration = 0.3f;

	// Token: 0x04000609 RID: 1545
	protected List<exDebugHelper.LogInfo> logs = new List<exDebugHelper.LogInfo>();

	// Token: 0x0400060A RID: 1546
	protected CleanQueue<exDebugHelper.LogInfo> pendingLogs = new CleanQueue<exDebugHelper.LogInfo>();

	// Token: 0x0400060B RID: 1547
	[SerializeField]
	protected bool showFps_ = true;

	// Token: 0x0400060C RID: 1548
	public TextAnchor fpsAnchor;

	// Token: 0x0400060D RID: 1549
	[SerializeField]
	protected bool enableTimeScaleDebug_ = true;

	// Token: 0x0400060E RID: 1550
	[SerializeField]
	protected bool showScreenPrint_ = true;

	// Token: 0x0400060F RID: 1551
	[SerializeField]
	protected bool showScreenLog_ = true;

	// Token: 0x04000610 RID: 1552
	public int logCount = 10;

	// Token: 0x04000611 RID: 1553
	public bool showScreenDebugText;

	// Token: 0x04000612 RID: 1554
	protected int frames;

	// Token: 0x04000613 RID: 1555
	protected float fps;

	// Token: 0x04000614 RID: 1556
	protected float lastInterval;

	// Token: 0x020015D8 RID: 5592
	public enum LogType
	{
		// Token: 0x040057F4 RID: 22516
		None,
		// Token: 0x040057F5 RID: 22517
		Normal,
		// Token: 0x040057F6 RID: 22518
		Warning,
		// Token: 0x040057F7 RID: 22519
		Error
	}

	// Token: 0x020015D9 RID: 5593
	public class TextInfo
	{
		// Token: 0x0600D631 RID: 54833 RVA: 0x004F7B51 File Offset: 0x004F5D51
		public TextInfo(Vector2 _screenPos, string _text, GUIStyle _style)
		{
			this.screenPos = _screenPos;
			this.text = _text;
			this.style = _style;
		}

		// Token: 0x040057F8 RID: 22520
		public Vector2 screenPos = Vector2.zero;

		// Token: 0x040057F9 RID: 22521
		public string text;

		// Token: 0x040057FA RID: 22522
		public GUIStyle style;
	}

	// Token: 0x020015DA RID: 5594
	public class LogInfo
	{
		// Token: 0x17000CF7 RID: 3319
		// (get) Token: 0x0600D632 RID: 54834 RVA: 0x004F7B7C File Offset: 0x004F5D7C
		public float ratio
		{
			get
			{
				if (this.lifetime == 0f)
				{
					return 0f;
				}
				if (this.timer < this.lifetime - exDebugHelper.instance.logFadeOutDuration)
				{
					return 0f;
				}
				return (this.timer - (this.lifetime - exDebugHelper.instance.logFadeOutDuration)) / exDebugHelper.instance.logFadeOutDuration;
			}
		}

		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x0600D633 RID: 54835 RVA: 0x004F7BDE File Offset: 0x004F5DDE
		public bool canDelete
		{
			get
			{
				return this.timer > this.lifetime;
			}
		}

		// Token: 0x0600D634 RID: 54836 RVA: 0x004F7BEE File Offset: 0x004F5DEE
		public LogInfo(string _text, GUIStyle _style, float _lifetime)
		{
			this.text = _text;
			this.style = _style;
			this.lifetime = _lifetime;
		}

		// Token: 0x0600D635 RID: 54837 RVA: 0x004F7C24 File Offset: 0x004F5E24
		public void Dead()
		{
			if (this.lifetime > 0f)
			{
				float num = this.lifetime - exDebugHelper.instance.logFadeOutDuration;
				if (this.timer < num - 1f)
				{
					this.timer = num - 1f;
				}
			}
		}

		// Token: 0x0600D636 RID: 54838 RVA: 0x004F7C6C File Offset: 0x004F5E6C
		public void Tick()
		{
			if (this.lifetime > 0f)
			{
				this.timer += Time.deltaTime * this.speed;
			}
		}

		// Token: 0x040057FB RID: 22523
		public string text;

		// Token: 0x040057FC RID: 22524
		public GUIStyle style;

		// Token: 0x040057FD RID: 22525
		private float speed = 1f;

		// Token: 0x040057FE RID: 22526
		private float timer;

		// Token: 0x040057FF RID: 22527
		private float lifetime = 5f;
	}
}

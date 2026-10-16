using System;
using System.ComponentModel;
using System.Linq;
using AiUnity.Common.Attributes;
using AiUnity.Common.Extensions;
using AiUnity.Common.InternalLog;
using AiUnity.Common.Log;
using AiUnity.Common.Patterns;
using AiUnity.NLog.Core.Common;
using AiUnity.NLog.Core.Layouts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.Targets
{
	// Token: 0x0200021B RID: 539
	[Target("GameConsole")]
	[Preserve]
	public sealed class GameConsoleTarget : UnityConsoleTarget
	{
		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060013D7 RID: 5079 RVA: 0x00071F80 File Offset: 0x00070180
		// (set) Token: 0x060013D8 RID: 5080 RVA: 0x00071F88 File Offset: 0x00070188
		[RequiredParameter]
		[DefaultValue(true)]
		[Display("Start console", "Make Game Console Log window active at startup.", false, 0)]
		public bool ConsoleActive { get; set; }

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060013D9 RID: 5081 RVA: 0x00071F91 File Offset: 0x00070191
		// (set) Token: 0x060013DA RID: 5082 RVA: 0x00071F99 File Offset: 0x00070199
		[RequiredParameter]
		[DefaultValue(8)]
		[Display("Font size", "Font size for game console", false, 0)]
		public int FontSize { get; set; }

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060013DB RID: 5083 RVA: 0x00071FA2 File Offset: 0x000701A2
		// (set) Token: 0x060013DC RID: 5084 RVA: 0x00071FAA File Offset: 0x000701AA
		[RequiredParameter]
		[DefaultValue(true)]
		public bool IconEnable { get; set; }

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060013DD RID: 5085 RVA: 0x00071FB3 File Offset: 0x000701B3
		// (set) Token: 0x060013DE RID: 5086 RVA: 0x00071FBB File Offset: 0x000701BB
		[RequiredParameter]
		[DefaultValue("<color=orange>[${level}] ${callsite}</color>${newline}<color=white>${message}</color><color=red>${exception}</color>")]
		[Display("Layout", "Specifies the layout and content of log message.  The + icon will present a list of variables that can be added.  For the Unity Console all excepted xml formating can be used (http://docs.unity3d.com/Manual/StyledText.html)", false, -100)]
		public override Layout Layout { get; set; }

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x060013DF RID: 5087 RVA: 0x00071FC4 File Offset: 0x000701C4
		// (set) Token: 0x060013E0 RID: 5088 RVA: 0x00071FCC File Offset: 0x000701CC
		[RequiredParameter]
		[DefaultValue(LogLevels.Warn | LogLevels.Error | LogLevels.Fatal | LogLevels.Assert)]
		[Display("Filter levels", "Starting log level filter that is runtime adjustable.", false, 0)]
		public LogLevels LogLevelsFilter { get; set; }

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x060013E1 RID: 5089 RVA: 0x00071FD5 File Offset: 0x000701D5
		private static IInternalLogger Logger
		{
			get
			{
				return Singleton<NLogInternalLogger>.Instance;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x060013E2 RID: 5090 RVA: 0x00071FDC File Offset: 0x000701DC
		// (set) Token: 0x060013E3 RID: 5091 RVA: 0x00071FE4 File Offset: 0x000701E4
		private IGameConsoleController GameConsoleController { get; set; }

		// Token: 0x060013E4 RID: 5092 RVA: 0x00071FF0 File Offset: 0x000701F0
		public GameConsoleTarget()
		{
			if (Application.isPlaying)
			{
				this.Layout = "<color=orange>[${level}] ${callsite}</color>${newline}<color=white>${message}</color><color=red>${exception}</color>";
				this.LogLevelsFilter = LogLevels.Warn | LogLevels.Error | LogLevels.Fatal | LogLevels.Assert;
				this.FontSize = 8;
				this.IconEnable = true;
				this.ConsoleActive = true;
				Scene activeScene = SceneManager.GetActiveScene();
				this.UpdateNLogMessageTarget(activeScene);
				SceneManager.activeSceneChanged += delegate(Scene s1, Scene s2)
				{
					this.UpdateNLogMessageTarget(s2);
				};
			}
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x00072058 File Offset: 0x00070258
		protected override void Write(LogEventInfo logEvent)
		{
			if (Application.isPlaying && this.GameConsoleController != null)
			{
				string text = base.FixUnityConsoleXML(this.Layout.Render(logEvent) + Environment.NewLine);
				this.GameConsoleController.AddMessage((int)logEvent.Level, text, logEvent.LoggerName, logEvent.TimeStamp);
				if (logEvent.Level.Has(LogLevels.Assert) && Singleton<NLogManager>.Instance.AssertException)
				{
					Debug.Break();
					throw new AssertException(this.Layout.Render(logEvent), logEvent.Exception);
				}
			}
		}

		// Token: 0x060013E6 RID: 5094 RVA: 0x000720EC File Offset: 0x000702EC
		private void UpdateNLogMessageTarget(Scene scene)
		{
			this.GameConsoleController = scene.GetRootGameObjects().SelectMany<GameObject, IGameConsoleController>((GameObject g) => g.GetComponentsInChildren<IGameConsoleController>()).FirstOrDefault<IGameConsoleController>();
			if (this.GameConsoleController != null)
			{
				this.GameConsoleController.SetIconEnable(this.IconEnable);
				this.GameConsoleController.SetConsoleActive(this.ConsoleActive);
				this.GameConsoleController.SetFontSize(this.FontSize, true, true);
				this.GameConsoleController.SetLogLevelFilter(this.LogLevelsFilter, true, true);
				return;
			}
			GameConsoleTarget.Logger.Warn("Unable to locate GameConsole GameObject.  Please place NLog prefab GameConsole in your hierarchy.", Array.Empty<object>());
		}

		// Token: 0x04000CEE RID: 3310
		private const bool ConsoleActiveDefault = true;

		// Token: 0x04000CEF RID: 3311
		private const int FontSizeDefault = 8;

		// Token: 0x04000CF0 RID: 3312
		private const bool IconEnableDefault = true;

		// Token: 0x04000CF1 RID: 3313
		private const string GameLayoutDefault = "<color=orange>[${level}] ${callsite}</color>${newline}<color=white>${message}</color><color=red>${exception}</color>";

		// Token: 0x04000CF2 RID: 3314
		private const LogLevels LogLevelsFilterDefault = LogLevels.Warn | LogLevels.Error | LogLevels.Fatal | LogLevels.Assert;
	}
}

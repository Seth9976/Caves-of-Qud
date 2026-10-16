using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using AiUnity.Common.Extensions;
using AiUnity.Common.Log;
using AiUnity.Common.Patterns;
using UnityEngine;

namespace AiUnity.Common.InternalLog
{
	// Token: 0x020002DA RID: 730
	public abstract class InternalLogger<T> : Singleton<T>, IInternalLogger where T : new()
	{
		// Token: 0x1700036F RID: 879
		// (get) Token: 0x0600190D RID: 6413 RVA: 0x00080420 File Offset: 0x0007E620
		// (set) Token: 0x0600190E RID: 6414 RVA: 0x00080428 File Offset: 0x0007E628
		public LogLevels InternalLogLevel
		{
			get
			{
				return this.internalLogLevel;
			}
			set
			{
				if (this.internalLogLevel != value)
				{
					InternalLogger<T>.Logger.Info("Setting {0} (PlayerPref=\"{1}\") to LogLevel={2}", new object[]
					{
						base.GetType().Name,
						this.logLevelPlayerPref,
						value
					});
					PlayerPrefs.SetInt(this.logLevelPlayerPref, (int)value);
					this.internalLogLevel = value;
					if (InternalLogger<T>.Logger != this)
					{
						InternalLogger<T>.Logger.InternalLogLevel = value;
					}
				}
			}
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x0600190F RID: 6415 RVA: 0x00080499 File Offset: 0x0007E699
		public bool IsAssertEnabled
		{
			get
			{
				return this.IsEnabled(LogLevels.Assert);
			}
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06001910 RID: 6416 RVA: 0x000804A2 File Offset: 0x0007E6A2
		public bool IsDebugEnabled
		{
			get
			{
				return this.IsEnabled(LogLevels.Debug);
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06001911 RID: 6417 RVA: 0x000804AC File Offset: 0x0007E6AC
		public bool IsErrorEnabled
		{
			get
			{
				return this.IsEnabled(LogLevels.Error);
			}
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06001912 RID: 6418 RVA: 0x000804B5 File Offset: 0x0007E6B5
		public bool IsFatalEnabled
		{
			get
			{
				return this.IsEnabled(LogLevels.Fatal);
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06001913 RID: 6419 RVA: 0x000804BE File Offset: 0x0007E6BE
		public bool IsInfoEnabled
		{
			get
			{
				return this.IsEnabled(LogLevels.Info);
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06001914 RID: 6420 RVA: 0x000804C8 File Offset: 0x0007E6C8
		public bool IsTraceEnabled
		{
			get
			{
				return this.IsEnabled(LogLevels.Trace);
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06001915 RID: 6421 RVA: 0x000804D2 File Offset: 0x0007E6D2
		public bool IsWarnEnabled
		{
			get
			{
				return this.IsEnabled(LogLevels.Warn);
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06001916 RID: 6422 RVA: 0x000804DB File Offset: 0x0007E6DB
		// (set) Token: 0x06001917 RID: 6423 RVA: 0x000804E3 File Offset: 0x0007E6E3
		protected bool IncludeTimestamp { get; set; }

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06001918 RID: 6424 RVA: 0x000804EC File Offset: 0x0007E6EC
		// (set) Token: 0x06001919 RID: 6425 RVA: 0x000804F4 File Offset: 0x0007E6F4
		protected LogLevels InternalLogLevelsDefault { get; set; }

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x0600191A RID: 6426 RVA: 0x000804FD File Offset: 0x0007E6FD
		private static IInternalLogger Logger
		{
			get
			{
				return Singleton<CommonInternalLogger>.Instance;
			}
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x00080504 File Offset: 0x0007E704
		public InternalLogger()
		{
			this.logLevelPlayerPref = "AiUnity" + base.GetType().Name + "LogLevel";
			this.internalLogLevel = PlayerPrefs.GetInt(this.logLevelPlayerPref, (int)this.InternalLogLevelsDefault).ToEnum<LogLevels>();
			this.isDarkTheme = Application.isEditor && Convert.ToBoolean(PlayerPrefs.GetInt("AiUnityIsProSkin", 0));
			this.InternalLogLevelsDefault = LogLevels.Info | LogLevels.Warn | LogLevels.Error | LogLevels.Fatal;
			this.IncludeTimestamp = false;
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x00080582 File Offset: 0x0007E782
		public void Assert(string message, params object[] args)
		{
			if (this.IsAssertEnabled)
			{
				this.Write(LogLevels.Assert, message, args);
			}
		}

		// Token: 0x0600191D RID: 6429 RVA: 0x00080595 File Offset: 0x0007E795
		public void Assert(bool condition, string message, params object[] args)
		{
			if (this.IsAssertEnabled && !condition)
			{
				this.Write(LogLevels.Assert, message, args);
			}
		}

		// Token: 0x0600191E RID: 6430 RVA: 0x000805AC File Offset: 0x0007E7AC
		public void Assert(Exception e, string message, params object[] args)
		{
			if (this.IsAssertEnabled && e != null)
			{
				string text = string.Format("{0}/n<color=#ff0000ff>{1}</color>", message, (e != null) ? e.Message : "Missing exception message");
				this.Write(LogLevels.Assert, text, args);
			}
		}

		// Token: 0x0600191F RID: 6431 RVA: 0x000805E9 File Offset: 0x0007E7E9
		public void Debug(string message, params object[] args)
		{
			if (this.IsDebugEnabled)
			{
				this.Write(LogLevels.Debug, message, args);
			}
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x000805FD File Offset: 0x0007E7FD
		public void Error(string message, params object[] args)
		{
			if (this.IsErrorEnabled)
			{
				this.Write(LogLevels.Error, message, args);
			}
		}

		// Token: 0x06001921 RID: 6433 RVA: 0x00080610 File Offset: 0x0007E810
		public void Error(Exception e, string message, params object[] args)
		{
			if (this.IsErrorEnabled)
			{
				string text = ((e != null) ? e.Message : "Missing exception message");
				string text2 = string.Format("{0}/n<color=#ff0000ff>{1}</color>", message, text);
				this.Write(LogLevels.Error, text2, args);
			}
		}

		// Token: 0x06001922 RID: 6434 RVA: 0x0008064C File Offset: 0x0007E84C
		public void Fatal(string message, params object[] args)
		{
			if (this.IsFatalEnabled)
			{
				this.Write(LogLevels.Fatal, message, args);
			}
		}

		// Token: 0x06001923 RID: 6435 RVA: 0x00080660 File Offset: 0x0007E860
		public void Fatal(Exception e, string message, params object[] args)
		{
			if (this.IsFatalEnabled)
			{
				string text = ((e != null) ? e.Message : "Missing exception message");
				string text2 = string.Format("{0}/n<color=#ff0000ff>{1}</color>", message, text);
				this.Write(LogLevels.Fatal, text2, args);
			}
		}

		// Token: 0x06001924 RID: 6436 RVA: 0x0008069C File Offset: 0x0007E89C
		public void Info(string message, params object[] args)
		{
			if (this.IsInfoEnabled)
			{
				this.Write(LogLevels.Info, message, args);
			}
		}

		// Token: 0x06001925 RID: 6437 RVA: 0x000806B0 File Offset: 0x0007E8B0
		public void Log(LogLevels levels, string message, params object[] args)
		{
			if (this.IsEnabled(levels))
			{
				this.Write(levels, message, args);
			}
		}

		// Token: 0x06001926 RID: 6438 RVA: 0x000806C4 File Offset: 0x0007E8C4
		public void Trace(string message, params object[] args)
		{
			if (this.IsTraceEnabled)
			{
				this.Write(LogLevels.Trace, message, args);
			}
		}

		// Token: 0x06001927 RID: 6439 RVA: 0x000806D8 File Offset: 0x0007E8D8
		public void Warn(string message, params object[] args)
		{
			if (this.IsWarnEnabled)
			{
				this.Write(LogLevels.Warn, message, args);
			}
		}

		// Token: 0x06001928 RID: 6440 RVA: 0x000806EC File Offset: 0x0007E8EC
		public void Warn(Exception e, string message, params object[] args)
		{
			if (this.IsErrorEnabled)
			{
				string text = ((e != null) ? e.Message : "Missing exception message");
				string text2 = string.Format("{0}/n<color=#ff0000ff>{1}</color>", message, text);
				this.Write(LogLevels.Warn, text2, args);
			}
		}

		// Token: 0x06001929 RID: 6441 RVA: 0x00080728 File Offset: 0x0007E928
		protected virtual string CreateMessage(LogLevels levels, string formattedMessage)
		{
			string text;
			try
			{
				StackFrame stackFrame = new StackTrace(true).GetFrames().SkipWhile<StackFrame>((StackFrame f) => typeof(IInternalLogger).IsAssignableFrom(f.GetMethod().DeclaringType)).FirstOrDefault<StackFrame>();
				text = string.Format("{0}.{1}", stackFrame.GetMethod().DeclaringType.FullName, stackFrame.GetMethod().Name);
			}
			catch
			{
				text = string.Format("AiUnity Internal Message ({0})", base.GetType().Name);
			}
			string text2 = string.Format("[{0} (ScriptBuilder Internal)] {1}", levels, text);
			if (Application.isEditor)
			{
				string text3 = (this.isDarkTheme ? "orange" : "magenta");
				text2 = string.Format("<color={0}>{1}</color>", text3, text2);
			}
			return text2 + Environment.NewLine + formattedMessage;
		}

		// Token: 0x0600192A RID: 6442 RVA: 0x00080804 File Offset: 0x0007EA04
		protected void Write(LogLevels levels, string message, object[] args)
		{
			string text = ((args != null && args.Length != 0) ? string.Format(CultureInfo.InvariantCulture, message, args) : message);
			string text2 = this.CreateMessage(levels, text);
			if (levels.Has(LogLevels.Info) || levels.Has(LogLevels.Debug) || levels.Has(LogLevels.Trace))
			{
				global::UnityEngine.Debug.Log(text2);
				return;
			}
			if (levels.Has(LogLevels.Warn))
			{
				global::UnityEngine.Debug.LogWarning(text2);
				return;
			}
			if (levels.Has(LogLevels.Error) || levels.Has(LogLevels.Fatal))
			{
				global::UnityEngine.Debug.LogError(text2);
				return;
			}
			levels.Has(LogLevels.Assert);
		}

		// Token: 0x0600192B RID: 6443 RVA: 0x000808AA File Offset: 0x0007EAAA
		private bool IsEnabled(LogLevels level)
		{
			return this.InternalLogLevel.Has(level);
		}

		// Token: 0x04000EDA RID: 3802
		private LogLevels internalLogLevel;

		// Token: 0x04000EDB RID: 3803
		private bool isDarkTheme;

		// Token: 0x04000EDC RID: 3804
		private string logLevelPlayerPref;
	}
}

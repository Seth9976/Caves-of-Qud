using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using PlayFab.Internal;
using UnityEngine;

namespace PlayFab.Public
{
	// Token: 0x0200015F RID: 351
	public abstract class PlayFabLoggerBase : IPlayFabLogger
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x000131DC File Offset: 0x000113DC
		// (set) Token: 0x060004AF RID: 1199 RVA: 0x000131E4 File Offset: 0x000113E4
		public IPAddress ip { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x000131ED File Offset: 0x000113ED
		// (set) Token: 0x060004B1 RID: 1201 RVA: 0x000131F5 File Offset: 0x000113F5
		public int port { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x000131FE File Offset: 0x000113FE
		// (set) Token: 0x060004B3 RID: 1203 RVA: 0x00013206 File Offset: 0x00011406
		public string url { get; set; }

		// Token: 0x060004B4 RID: 1204 RVA: 0x00013210 File Offset: 0x00011410
		protected PlayFabLoggerBase()
		{
			string text = new PlayFabDataGatherer().GenerateReport();
			Queue<string> logMessageQueue = this.LogMessageQueue;
			lock (logMessageQueue)
			{
				this.LogMessageQueue.Enqueue(text);
			}
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0001329C File Offset: 0x0001149C
		public virtual void OnEnable()
		{
			SingletonMonoBehaviour<PlayFabHttp>.instance.StartCoroutine(this.RegisterLogger());
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x000132AF File Offset: 0x000114AF
		private IEnumerator RegisterLogger()
		{
			yield return new WaitForEndOfFrame();
			if (!string.IsNullOrEmpty(PlayFabSettings.LoggerHost))
			{
				Application.logMessageReceivedThreaded += this.HandleUnityLog;
			}
			yield break;
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x000132BE File Offset: 0x000114BE
		public virtual void OnDisable()
		{
			if (!string.IsNullOrEmpty(PlayFabSettings.LoggerHost))
			{
				Application.logMessageReceivedThreaded -= this.HandleUnityLog;
			}
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x000132DD File Offset: 0x000114DD
		public virtual void OnDestroy()
		{
			this._isApplicationPlaying = false;
		}

		// Token: 0x060004B9 RID: 1209
		protected abstract void BeginUploadLog();

		// Token: 0x060004BA RID: 1210
		protected abstract void UploadLog(string message);

		// Token: 0x060004BB RID: 1211
		protected abstract void EndUploadLog();

		// Token: 0x060004BC RID: 1212 RVA: 0x000132E8 File Offset: 0x000114E8
		private void HandleUnityLog(string message, string stacktrace, LogType type)
		{
			if (!PlayFabSettings.EnableRealTimeLogging)
			{
				return;
			}
			PlayFabLoggerBase.Sb.Length = 0;
			if (type == LogType.Log || type == LogType.Warning)
			{
				PlayFabLoggerBase.Sb.Append(type).Append(": ").Append(message);
				message = PlayFabLoggerBase.Sb.ToString();
				Queue<string> queue = this.LogMessageQueue;
				lock (queue)
				{
					this.LogMessageQueue.Enqueue(message);
					goto IL_00EB;
				}
			}
			if (type == LogType.Error || type == LogType.Exception)
			{
				PlayFabLoggerBase.Sb.Append(type).Append(": ").Append(message)
					.Append("\n")
					.Append(stacktrace)
					.Append(StackTraceUtility.ExtractStackTrace());
				message = PlayFabLoggerBase.Sb.ToString();
				Queue<string> queue = this.LogMessageQueue;
				lock (queue)
				{
					this.LogMessageQueue.Enqueue(message);
				}
			}
			IL_00EB:
			this.ActivateThreadWorker();
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00013404 File Offset: 0x00011604
		private void ActivateThreadWorker()
		{
			object threadLock = this._threadLock;
			lock (threadLock)
			{
				if (this._writeLogThread == null)
				{
					this._writeLogThread = new Thread(new ThreadStart(this.WriteLogThreadWorker));
					this._writeLogThread.Start();
				}
			}
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0001346C File Offset: 0x0001166C
		private void WriteLogThreadWorker()
		{
			try
			{
				object obj = this._threadLock;
				lock (obj)
				{
					this._threadKillTime = DateTime.UtcNow + PlayFabLoggerBase._threadKillTimeout;
				}
				Queue<string> queue = new Queue<string>();
				bool flag2;
				do
				{
					Queue<string> logMessageQueue = this.LogMessageQueue;
					lock (logMessageQueue)
					{
						this._pendingLogsCount = this.LogMessageQueue.Count;
						while (this.LogMessageQueue.Count > 0)
						{
							queue.Enqueue(this.LogMessageQueue.Dequeue());
						}
					}
					this.BeginUploadLog();
					while (queue.Count > 0)
					{
						this.UploadLog(queue.Dequeue());
					}
					this.EndUploadLog();
					obj = this._threadLock;
					lock (obj)
					{
						DateTime utcNow = DateTime.UtcNow;
						if (this._pendingLogsCount > 0 && this._isApplicationPlaying)
						{
							this._threadKillTime = utcNow + PlayFabLoggerBase._threadKillTimeout;
						}
						flag2 = utcNow <= this._threadKillTime;
						if (!flag2)
						{
							this._writeLogThread = null;
						}
					}
					Thread.Sleep(10000);
				}
				while (flag2);
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
				this._writeLogThread = null;
			}
		}

		// Token: 0x04000706 RID: 1798
		private static readonly StringBuilder Sb = new StringBuilder();

		// Token: 0x04000707 RID: 1799
		private readonly Queue<string> LogMessageQueue = new Queue<string>();

		// Token: 0x04000708 RID: 1800
		private const int LOG_CACHE_INTERVAL_MS = 10000;

		// Token: 0x04000709 RID: 1801
		private Thread _writeLogThread;

		// Token: 0x0400070A RID: 1802
		private readonly object _threadLock = new object();

		// Token: 0x0400070B RID: 1803
		private static readonly TimeSpan _threadKillTimeout = TimeSpan.FromSeconds(60.0);

		// Token: 0x0400070C RID: 1804
		private DateTime _threadKillTime = DateTime.UtcNow + PlayFabLoggerBase._threadKillTimeout;

		// Token: 0x0400070D RID: 1805
		private bool _isApplicationPlaying = true;

		// Token: 0x0400070E RID: 1806
		private int _pendingLogsCount;
	}
}

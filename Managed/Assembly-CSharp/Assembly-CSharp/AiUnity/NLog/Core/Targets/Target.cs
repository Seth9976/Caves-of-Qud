using System;
using System.Collections.Generic;
using System.Threading;
using AiUnity.Common.InternalLog;
using AiUnity.Common.Patterns;
using AiUnity.NLog.Core.Common;
using AiUnity.NLog.Core.Config;
using AiUnity.NLog.Core.Internal;
using AiUnity.NLog.Core.Layouts;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.Targets
{
	// Token: 0x02000228 RID: 552
	[NLogConfigurationItem]
	[Preserve]
	public abstract class Target : ISupportsInitialize, IDisposable
	{
		// Token: 0x17000276 RID: 630
		// (get) Token: 0x0600146C RID: 5228 RVA: 0x0007345D File Offset: 0x0007165D
		private static IInternalLogger Logger
		{
			get
			{
				return Singleton<NLogInternalLogger>.Instance;
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x0600146D RID: 5229 RVA: 0x00073464 File Offset: 0x00071664
		// (set) Token: 0x0600146E RID: 5230 RVA: 0x0007346C File Offset: 0x0007166C
		public string Name { get; set; }

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x0600146F RID: 5231 RVA: 0x00073475 File Offset: 0x00071675
		protected object SyncRoot
		{
			get
			{
				return this.lockObject;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06001470 RID: 5232 RVA: 0x0007347D File Offset: 0x0007167D
		// (set) Token: 0x06001471 RID: 5233 RVA: 0x00073485 File Offset: 0x00071685
		private protected LoggingConfiguration LoggingConfiguration { protected get; private set; }

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06001472 RID: 5234 RVA: 0x0007348E File Offset: 0x0007168E
		// (set) Token: 0x06001473 RID: 5235 RVA: 0x00073496 File Offset: 0x00071696
		private protected bool IsInitialized { protected get; private set; }

		// Token: 0x06001474 RID: 5236 RVA: 0x0007349F File Offset: 0x0007169F
		void ISupportsInitialize.Initialize(LoggingConfiguration configuration)
		{
			this.Initialize(configuration);
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x000734A8 File Offset: 0x000716A8
		void ISupportsInitialize.Close()
		{
			this.Close();
		}

		// Token: 0x06001476 RID: 5238 RVA: 0x000734B0 File Offset: 0x000716B0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001477 RID: 5239 RVA: 0x000734C0 File Offset: 0x000716C0
		public void Flush(AsyncContinuation asyncContinuation)
		{
			if (asyncContinuation == null)
			{
				throw new ArgumentNullException("asyncContinuation");
			}
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				if (!this.IsInitialized)
				{
					asyncContinuation(null);
				}
				else
				{
					asyncContinuation = AsyncHelpers.PreventMultipleCalls(asyncContinuation);
					try
					{
						this.FlushAsync(asyncContinuation);
					}
					catch (Exception ex)
					{
						if (ex.MustBeRethrown())
						{
							throw;
						}
						asyncContinuation(ex);
					}
				}
			}
		}

		// Token: 0x06001478 RID: 5240 RVA: 0x00073548 File Offset: 0x00071748
		public void PrecalculateVolatileLayouts(LogEventInfo logEvent)
		{
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				if (this.IsInitialized)
				{
					foreach (Layout layout in this.allLayouts)
					{
						layout.Precalculate(logEvent);
					}
				}
			}
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x000735CC File Offset: 0x000717CC
		public override string ToString()
		{
			TargetAttribute targetAttribute = (TargetAttribute)Attribute.GetCustomAttribute(base.GetType(), typeof(TargetAttribute));
			if (targetAttribute != null)
			{
				return targetAttribute.DisplayName + " Target[" + (this.Name ?? "(unnamed)") + "]";
			}
			return base.GetType().Name;
		}

		// Token: 0x0600147A RID: 5242 RVA: 0x00073628 File Offset: 0x00071828
		public void WriteAsyncLogEvent(AsyncLogEventInfo logEvent)
		{
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				if (!this.IsInitialized)
				{
					logEvent.Continuation(null);
				}
				else if (this.initializeException != null)
				{
					logEvent.Continuation(this.CreateInitException());
				}
				else
				{
					AsyncContinuation asyncContinuation = AsyncHelpers.PreventMultipleCalls(logEvent.Continuation);
					try
					{
						this.Write(logEvent.LogEvent.WithContinuation(asyncContinuation));
					}
					catch (Exception ex)
					{
						if (ex.MustBeRethrown())
						{
							throw;
						}
						if (Singleton<NLogManager>.Instance.ThrowExceptions)
						{
							throw;
						}
						asyncContinuation(ex);
					}
				}
			}
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x000736E4 File Offset: 0x000718E4
		public void WriteAsyncLogEvents(params AsyncLogEventInfo[] logEvents)
		{
			if (logEvents == null || logEvents.Length == 0)
			{
				return;
			}
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				if (!this.IsInitialized)
				{
					foreach (AsyncLogEventInfo asyncLogEventInfo in logEvents)
					{
						asyncLogEventInfo.Continuation(null);
					}
				}
				else if (this.initializeException != null)
				{
					foreach (AsyncLogEventInfo asyncLogEventInfo2 in logEvents)
					{
						asyncLogEventInfo2.Continuation(this.CreateInitException());
					}
				}
				else
				{
					AsyncLogEventInfo[] array = new AsyncLogEventInfo[logEvents.Length];
					for (int j = 0; j < logEvents.Length; j++)
					{
						array[j] = logEvents[j].LogEvent.WithContinuation(AsyncHelpers.PreventMultipleCalls(logEvents[j].Continuation));
					}
					try
					{
						this.Write(array);
					}
					catch (Exception ex)
					{
						if (ex.MustBeRethrown())
						{
							throw;
						}
						foreach (AsyncLogEventInfo asyncLogEventInfo3 in array)
						{
							asyncLogEventInfo3.Continuation(ex);
						}
					}
				}
			}
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x0007384C File Offset: 0x00071A4C
		internal void Initialize(LoggingConfiguration configuration)
		{
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				this.LoggingConfiguration = configuration;
				if (!this.IsInitialized)
				{
					PropertyHelper.CheckRequiredParameters(this);
					this.IsInitialized = true;
					try
					{
						this.InitializeTarget();
						this.initializeException = null;
					}
					catch (Exception ex)
					{
						if (ex.MustBeRethrown())
						{
							throw;
						}
						this.initializeException = ex;
						Target.Logger.Error("Error initializing target {0} {1}.", new object[] { this, ex });
						throw;
					}
				}
			}
		}

		// Token: 0x0600147D RID: 5245 RVA: 0x000738F0 File Offset: 0x00071AF0
		internal void Close()
		{
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				this.LoggingConfiguration = null;
				if (this.IsInitialized)
				{
					this.IsInitialized = false;
					try
					{
						if (this.initializeException == null)
						{
							this.CloseTarget();
						}
					}
					catch (Exception ex)
					{
						if (ex.MustBeRethrown())
						{
							throw;
						}
						Target.Logger.Error("Error closing target {0} {1}.", new object[] { this, ex });
						throw;
					}
				}
			}
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x00073988 File Offset: 0x00071B88
		internal void WriteAsyncLogEvents(AsyncLogEventInfo[] logEventInfos, AsyncContinuation continuation)
		{
			if (logEventInfos.Length == 0)
			{
				continuation(null);
				return;
			}
			AsyncLogEventInfo[] array = new AsyncLogEventInfo[logEventInfos.Length];
			int remaining = logEventInfos.Length;
			for (int i = 0; i < logEventInfos.Length; i++)
			{
				AsyncContinuation originalContinuation = logEventInfos[i].Continuation;
				AsyncContinuation asyncContinuation = delegate(Exception ex)
				{
					originalContinuation(ex);
					if (Interlocked.Decrement(ref remaining) == 0)
					{
						continuation(null);
					}
				};
				array[i] = logEventInfos[i].LogEvent.WithContinuation(asyncContinuation);
			}
			this.WriteAsyncLogEvents(array);
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x00073A1D File Offset: 0x00071C1D
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.CloseTarget();
			}
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x00073A28 File Offset: 0x00071C28
		protected virtual void InitializeTarget()
		{
			this.GetAllLayouts();
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x00073A30 File Offset: 0x00071C30
		protected virtual void CloseTarget()
		{
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x00073A32 File Offset: 0x00071C32
		protected virtual void FlushAsync(AsyncContinuation asyncContinuation)
		{
			asyncContinuation(null);
		}

		// Token: 0x06001483 RID: 5251 RVA: 0x00073A3B File Offset: 0x00071C3B
		protected virtual void Write(LogEventInfo logEvent)
		{
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x00073A40 File Offset: 0x00071C40
		protected virtual void Write(AsyncLogEventInfo logEvent)
		{
			try
			{
				this.MergeEventProperties(logEvent.LogEvent);
				this.Write(logEvent.LogEvent);
				logEvent.Continuation(null);
			}
			catch (Exception ex)
			{
				if (ex.MustBeRethrown())
				{
					throw;
				}
				logEvent.Continuation(ex);
			}
		}

		// Token: 0x06001485 RID: 5253 RVA: 0x00073AA0 File Offset: 0x00071CA0
		protected virtual void Write(AsyncLogEventInfo[] logEvents)
		{
			for (int i = 0; i < logEvents.Length; i++)
			{
				this.Write(logEvents[i]);
			}
		}

		// Token: 0x06001486 RID: 5254 RVA: 0x00073AC8 File Offset: 0x00071CC8
		private Exception CreateInitException()
		{
			return new NLogRuntimeException("Target " + ((this != null) ? this.ToString() : null) + " failed to initialize.", this.initializeException);
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x00073AF4 File Offset: 0x00071CF4
		private void GetAllLayouts()
		{
			this.allLayouts = new List<Layout>(ObjectGraphScanner.FindReachableObjects<Layout>(new object[] { this }));
			Target.Logger.Trace("{0} has {1} layouts", new object[]
			{
				this,
				this.allLayouts.Count
			});
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x00073B48 File Offset: 0x00071D48
		protected void MergeEventProperties(LogEventInfo logEvent)
		{
			if (logEvent.Parameters == null)
			{
				return;
			}
			object[] parameters = logEvent.Parameters;
			for (int i = 0; i < parameters.Length; i++)
			{
				LogEventInfo logEventInfo = parameters[i] as LogEventInfo;
				if (logEventInfo != null)
				{
					foreach (object obj in logEventInfo.Properties.Keys)
					{
						logEvent.Properties.Add(obj, logEventInfo.Properties[obj]);
					}
					logEventInfo.Properties.Clear();
				}
			}
		}

		// Token: 0x04000D30 RID: 3376
		private object lockObject = new object();

		// Token: 0x04000D31 RID: 3377
		private List<Layout> allLayouts;

		// Token: 0x04000D32 RID: 3378
		private Exception initializeException;
	}
}

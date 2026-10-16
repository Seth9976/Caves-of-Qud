using System;
using AiUnity.NLog.Core.Config;
using AiUnity.NLog.Core.Internal;

namespace AiUnity.NLog.Core.Layouts
{
	// Token: 0x0200023D RID: 573
	[NLogConfigurationItem]
	public abstract class Layout : ISupportsInitialize, IRenderable
	{
		// Token: 0x1700029D RID: 669
		// (get) Token: 0x0600150F RID: 5391 RVA: 0x000753B4 File Offset: 0x000735B4
		internal bool IsThreadAgnostic
		{
			get
			{
				return this.threadAgnostic;
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06001510 RID: 5392 RVA: 0x000753BC File Offset: 0x000735BC
		// (set) Token: 0x06001511 RID: 5393 RVA: 0x000753C4 File Offset: 0x000735C4
		private protected LoggingConfiguration LoggingConfiguration { protected get; private set; }

		// Token: 0x06001512 RID: 5394 RVA: 0x000753CD File Offset: 0x000735CD
		public static implicit operator Layout(string text)
		{
			return Layout.FromString(text);
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x000753D5 File Offset: 0x000735D5
		public static Layout FromString(string layoutText)
		{
			return Layout.FromString(layoutText, ConfigurationItemFactory.Default);
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x000753E2 File Offset: 0x000735E2
		public static Layout FromString(string layoutText, ConfigurationItemFactory configurationItemFactory)
		{
			return new SimpleLayout(layoutText, configurationItemFactory);
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x000753EB File Offset: 0x000735EB
		public virtual void Precalculate(LogEventInfo logEvent)
		{
			if (!this.threadAgnostic)
			{
				this.Render(logEvent);
			}
		}

		// Token: 0x06001516 RID: 5398 RVA: 0x000753FD File Offset: 0x000735FD
		public string Render(LogEventInfo logEvent)
		{
			if (!this.isInitialized)
			{
				this.isInitialized = true;
				this.InitializeLayout();
			}
			return this.GetFormattedMessage(logEvent);
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x0007541B File Offset: 0x0007361B
		void ISupportsInitialize.Initialize(LoggingConfiguration configuration)
		{
			this.Initialize(configuration);
		}

		// Token: 0x06001518 RID: 5400 RVA: 0x00075424 File Offset: 0x00073624
		void ISupportsInitialize.Close()
		{
			this.Close();
		}

		// Token: 0x06001519 RID: 5401 RVA: 0x0007542C File Offset: 0x0007362C
		internal void Initialize(LoggingConfiguration configuration)
		{
			if (!this.isInitialized)
			{
				this.LoggingConfiguration = configuration;
				this.isInitialized = true;
				this.threadAgnostic = true;
				object[] array = ObjectGraphScanner.FindReachableObjects<object>(new object[] { this });
				for (int i = 0; i < array.Length; i++)
				{
					if (!array[i].GetType().IsDefined(typeof(ThreadAgnosticAttribute), true))
					{
						this.threadAgnostic = false;
						break;
					}
				}
				this.InitializeLayout();
			}
		}

		// Token: 0x0600151A RID: 5402 RVA: 0x0007549D File Offset: 0x0007369D
		internal void Close()
		{
			if (this.isInitialized)
			{
				this.LoggingConfiguration = null;
				this.isInitialized = false;
				this.CloseLayout();
			}
		}

		// Token: 0x0600151B RID: 5403 RVA: 0x000754BB File Offset: 0x000736BB
		protected virtual void InitializeLayout()
		{
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x000754BD File Offset: 0x000736BD
		protected virtual void CloseLayout()
		{
		}

		// Token: 0x0600151D RID: 5405
		protected abstract string GetFormattedMessage(LogEventInfo logEvent);

		// Token: 0x04000D78 RID: 3448
		private bool isInitialized;

		// Token: 0x04000D79 RID: 3449
		private bool threadAgnostic;
	}
}

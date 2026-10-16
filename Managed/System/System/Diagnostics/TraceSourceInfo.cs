using System;

namespace System.Diagnostics
{
	// Token: 0x02000288 RID: 648
	internal class TraceSourceInfo
	{
		// Token: 0x0600146F RID: 5231 RVA: 0x0005328C File Offset: 0x0005148C
		public TraceSourceInfo(string name, SourceLevels levels)
		{
			this.name = name;
			this.levels = levels;
			this.listeners = new TraceListenerCollection();
		}

		// Token: 0x06001470 RID: 5232 RVA: 0x000532AD File Offset: 0x000514AD
		internal TraceSourceInfo(string name, SourceLevels levels, TraceImplSettings settings)
		{
			this.name = name;
			this.levels = levels;
			this.listeners = new TraceListenerCollection();
			this.listeners.Add(new DefaultTraceListener
			{
				IndentSize = settings.IndentSize
			});
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06001471 RID: 5233 RVA: 0x000532EB File Offset: 0x000514EB
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06001472 RID: 5234 RVA: 0x000532F3 File Offset: 0x000514F3
		public SourceLevels Levels
		{
			get
			{
				return this.levels;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06001473 RID: 5235 RVA: 0x000532FB File Offset: 0x000514FB
		public TraceListenerCollection Listeners
		{
			get
			{
				return this.listeners;
			}
		}

		// Token: 0x04000BAD RID: 2989
		private string name;

		// Token: 0x04000BAE RID: 2990
		private SourceLevels levels;

		// Token: 0x04000BAF RID: 2991
		private TraceListenerCollection listeners;
	}
}

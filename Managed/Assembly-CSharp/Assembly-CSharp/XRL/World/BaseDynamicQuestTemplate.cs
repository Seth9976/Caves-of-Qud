using System;
using XRL.Core;

namespace XRL.World
{
	// Token: 0x0200091A RID: 2330
	[Serializable]
	public abstract class BaseDynamicQuestTemplate
	{
		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x060059DF RID: 23007 RVA: 0x001B424C File Offset: 0x001B244C
		public ZoneManager zoneManager
		{
			get
			{
				return XRLCore.Core.Game.ZoneManager;
			}
		}

		// Token: 0x060059E0 RID: 23008
		public abstract void init(DynamicQuestContext context);

		// Token: 0x04002C82 RID: 11394
		public long id;
	}
}

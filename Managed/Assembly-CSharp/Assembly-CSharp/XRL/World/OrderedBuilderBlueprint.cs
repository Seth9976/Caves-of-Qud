using System;

namespace XRL.World
{
	// Token: 0x02000977 RID: 2423
	public struct OrderedBuilderBlueprint
	{
		// Token: 0x0600644A RID: 25674 RVA: 0x001EF29C File Offset: 0x001ED49C
		public OrderedBuilderBlueprint(ZoneBuilderBlueprint Blueprint, int Priority)
		{
			this.Blueprint = Blueprint;
			this.Priority = Priority;
		}

		// Token: 0x04002F11 RID: 12049
		public ZoneBuilderBlueprint Blueprint;

		// Token: 0x04002F12 RID: 12050
		public int Priority;
	}
}

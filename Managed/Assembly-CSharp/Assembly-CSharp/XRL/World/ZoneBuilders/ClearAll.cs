using System;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A55 RID: 2645
	public class ClearAll : ZoneBuilderSandbox
	{
		// Token: 0x06006E1F RID: 28191 RVA: 0x00259BEF File Offset: 0x00257DEF
		public bool BuildZone(Zone Z)
		{
			Z.ForeachCell(delegate(Cell c)
			{
				c.Clear(null, false, this.IncludeCombat, null);
			});
			return true;
		}

		// Token: 0x0400326E RID: 12910
		public bool IncludeCombat = true;
	}
}

using System;

namespace XRL.World.Parts
{
	// Token: 0x02001284 RID: 4740
	[Serializable]
	public class ReachabilityBuilder : IPart
	{
		// Token: 0x0600BB9D RID: 48029 RVA: 0x00448654 File Offset: 0x00446854
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID;
		}

		// Token: 0x0600BB9E RID: 48030 RVA: 0x0044866C File Offset: 0x0044686C
		public override bool HandleEvent(EnteredCellEvent E)
		{
			try
			{
				Zone currentZone = this.ParentObject.CurrentZone;
				for (int i = 0; i < currentZone.Height; i++)
				{
					for (int j = 0; j < currentZone.Width; j++)
					{
						Cell cell = currentZone.GetCell(j, i);
						if (!cell.IsReachable() && !cell.IsSolid())
						{
							currentZone.BuildReachableMap(j, i);
						}
					}
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogError("Reachability build", ex);
			}
			return base.HandleEvent(E);
		}
	}
}

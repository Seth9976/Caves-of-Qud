using System;
using Genkit;
using XRL.Rules;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A66 RID: 2662
	public class FungalTrailExileCorpse : ZoneBuilderSandbox
	{
		// Token: 0x06006E45 RID: 28229 RVA: 0x0025B050 File Offset: 0x00259250
		public bool BuildZone(Zone Z)
		{
			bool flag = false;
			int num = 20;
			foreach (Location2D location2D in base.YieldGaussianCellCluster((float)Stat.Random(40, 60), (float)Stat.Random(8, 12), 2f, 4f))
			{
				Cell cell = Z.GetCell(location2D);
				if (cell.IsEmpty())
				{
					if (num-- < 0)
					{
						break;
					}
					if (!flag)
					{
						cell.AddObject("ExileCorpse", null, null, null, null);
						flag = true;
					}
					else if (!cell.HasObjectWithBlueprint("Godshroom"))
					{
						cell.AddObject("Godshroom", null, null, null, null);
					}
				}
			}
			return flag;
		}
	}
}

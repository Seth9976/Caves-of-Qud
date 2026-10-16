using System;
using Genkit;
using XRL.World.Parts;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000A37 RID: 2615
	public class FractiPlanter : ZoneBuilderSandbox
	{
		// Token: 0x06006DB2 RID: 28082 RVA: 0x0024F050 File Offset: 0x0024D250
		public bool BuildZone(Zone Z, Point2D P)
		{
			Cell cell = Z.GetCell(P);
			if (cell != null)
			{
				int num = this.Amount.RollCached();
				for (int i = 0; i < num; i++)
				{
					GameObject gameObject = cell.AddObject("Fracti", null, null, null, null);
					if (gameObject != null)
					{
						Fracti part = gameObject.GetPart<Fracti>();
						if (part != null)
						{
							part.Grow(this.OkFloors, this.Size.RollCached(), null);
						}
					}
				}
			}
			return true;
		}

		// Token: 0x04003229 RID: 12841
		public string Size = "10-20";

		// Token: 0x0400322A RID: 12842
		public string Amount = "3-4";

		// Token: 0x0400322B RID: 12843
		public string OkFloors = "SaltPath";
	}
}

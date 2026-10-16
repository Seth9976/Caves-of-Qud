using System;
using System.Collections.Generic;
using XRL.Rules;

namespace XRL.World.ObjectBuilders
{
	// Token: 0x02000A0E RID: 2574
	public class ObjectRetinueEntry
	{
		// Token: 0x06006D15 RID: 27925 RVA: 0x0024846E File Offset: 0x0024666E
		public ObjectRetinueEntry(string Amount, string Object)
		{
			this.Amount = Amount;
			this.Object = Object;
		}

		// Token: 0x06006D16 RID: 27926 RVA: 0x00248484 File Offset: 0x00246684
		public List<GameObject> Generate()
		{
			List<GameObject> list = new List<GameObject>();
			int num = Stat.Roll(this.Amount, null);
			for (int i = 0; i < num; i++)
			{
				list.Add(GameObject.Create(this.Object));
			}
			return list;
		}

		// Token: 0x040031DE RID: 12766
		public string Amount;

		// Token: 0x040031DF RID: 12767
		public string Object;
	}
}

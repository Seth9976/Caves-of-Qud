using System;
using System.Collections.Generic;

namespace XRL
{
	// Token: 0x02000534 RID: 1332
	public class PopulationInfo : PopulationList
	{
		// Token: 0x06003904 RID: 14596 RVA: 0x00118058 File Offset: 0x00116258
		public PopulationInfo()
		{
		}

		// Token: 0x06003905 RID: 14597 RVA: 0x0011806B File Offset: 0x0011626B
		public PopulationInfo(string Name)
			: this()
		{
			this.Name = Name;
		}

		// Token: 0x06003906 RID: 14598 RVA: 0x0011807C File Offset: 0x0011627C
		public string[] GetEachUniqueObjectRoot()
		{
			List<string> list = new List<string>();
			foreach (PopulationItem populationItem in this.Items)
			{
				populationItem.GetEachUniqueObject(list);
			}
			return list.ToArray();
		}

		// Token: 0x06003907 RID: 14599 RVA: 0x001180DC File Offset: 0x001162DC
		public override void GetEachUniqueObject(List<string> List)
		{
			foreach (PopulationItem populationItem in this.Items)
			{
				populationItem.GetEachUniqueObject(List);
			}
		}

		// Token: 0x04001CD6 RID: 7382
		public Dictionary<string, PopulationGroup> GroupLookup = new Dictionary<string, PopulationGroup>();
	}
}

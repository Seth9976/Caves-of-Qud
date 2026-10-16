using System;
using System.Collections.Generic;

namespace XRL
{
	// Token: 0x0200050D RID: 1293
	public class SubtypeClass
	{
		// Token: 0x06003760 RID: 14176 RVA: 0x0010D364 File Offset: 0x0010B564
		public List<SubtypeEntry> GetAllSubtypes()
		{
			List<SubtypeEntry> list = new List<SubtypeEntry>();
			for (int i = 0; i < this.Categories.Count; i++)
			{
				list.AddRange(this.Categories[i].Subtypes);
			}
			return list;
		}

		// Token: 0x04001BC5 RID: 7109
		public string ID;

		// Token: 0x04001BC6 RID: 7110
		public string ChargenTitle;

		// Token: 0x04001BC7 RID: 7111
		public string SingluarTitle;

		// Token: 0x04001BC8 RID: 7112
		public string StatBoxDisplay = "false";

		// Token: 0x04001BC9 RID: 7113
		public List<SubtypeCategory> Categories = new List<SubtypeCategory>();
	}
}

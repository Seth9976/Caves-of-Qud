using System;
using System.Collections.Generic;

namespace XRL
{
	// Token: 0x0200053A RID: 1338
	public class PopulationStructuredResult
	{
		// Token: 0x04001CEC RID: 7404
		public string Hint;

		// Token: 0x04001CED RID: 7405
		public List<PopulationResult> Objects = new List<PopulationResult>();

		// Token: 0x04001CEE RID: 7406
		public List<PopulationStructuredResult> ChildGroups = new List<PopulationStructuredResult>();
	}
}

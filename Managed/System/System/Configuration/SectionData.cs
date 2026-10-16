using System;

namespace System.Configuration
{
	// Token: 0x020001AC RID: 428
	internal class SectionData
	{
		// Token: 0x06000B28 RID: 2856 RVA: 0x0002F4CF File Offset: 0x0002D6CF
		public SectionData(string sectionName, string typeName, bool allowLocation, AllowDefinition allowDefinition, bool requirePermission)
		{
			this.SectionName = sectionName;
			this.TypeName = typeName;
			this.AllowLocation = allowLocation;
			this.AllowDefinition = allowDefinition;
			this.RequirePermission = requirePermission;
		}

		// Token: 0x0400076C RID: 1900
		public readonly string SectionName;

		// Token: 0x0400076D RID: 1901
		public readonly string TypeName;

		// Token: 0x0400076E RID: 1902
		public readonly bool AllowLocation;

		// Token: 0x0400076F RID: 1903
		public readonly AllowDefinition AllowDefinition;

		// Token: 0x04000770 RID: 1904
		public string FileName;

		// Token: 0x04000771 RID: 1905
		public readonly bool RequirePermission;
	}
}

using System;

namespace SFB
{
	// Token: 0x0200018A RID: 394
	public struct ExtensionFilter
	{
		// Token: 0x06000F0A RID: 3850 RVA: 0x0005FA3F File Offset: 0x0005DC3F
		public ExtensionFilter(string filterName, params string[] filterExtensions)
		{
			this.Name = filterName;
			this.Extensions = filterExtensions;
		}

		// Token: 0x04000A6B RID: 2667
		public string Name;

		// Token: 0x04000A6C RID: 2668
		public string[] Extensions;
	}
}

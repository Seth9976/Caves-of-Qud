using System;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000132 RID: 306
	[AttributeUsage(AttributeTargets.Class, Inherited = true)]
	public class DisplayStringFormatAttribute : Attribute
	{
		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06001121 RID: 4385 RVA: 0x000522AC File Offset: 0x000504AC
		// (set) Token: 0x06001122 RID: 4386 RVA: 0x000522B4 File Offset: 0x000504B4
		public string formatString { get; set; }

		// Token: 0x06001123 RID: 4387 RVA: 0x000522BD File Offset: 0x000504BD
		public DisplayStringFormatAttribute(string formatString)
		{
			this.formatString = formatString;
		}
	}
}

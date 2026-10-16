using System;
using System.ComponentModel;

namespace System.Web
{
	// Token: 0x020000E8 RID: 232
	[AttributeUsage(AttributeTargets.All)]
	internal class WebCategoryAttribute : CategoryAttribute
	{
		// Token: 0x06000C77 RID: 3191 RVA: 0x000215E4 File Offset: 0x0001F7E4
		public WebCategoryAttribute(string category)
			: base(category)
		{
		}
	}
}

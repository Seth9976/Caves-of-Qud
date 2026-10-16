using System;

namespace System.Web.Util
{
	// Token: 0x02000122 RID: 290
	internal class CaseInsensitiveStringSet : StringSet
	{
		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06000E0E RID: 3598 RVA: 0x00008B4A File Offset: 0x00006D4A
		protected override bool CaseInsensitive
		{
			get
			{
				return true;
			}
		}
	}
}

using System;

namespace UnityEngine.Android
{
	// Token: 0x02000030 RID: 48
	public class AndroidLocale
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000AFAF File Offset: 0x000091AF
		public string country { get; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000AFB7 File Offset: 0x000091B7
		public string language { get; }

		// Token: 0x060002BA RID: 698 RVA: 0x0000AFBF File Offset: 0x000091BF
		internal AndroidLocale(string _country, string _language)
		{
			this.country = _country;
			this.language = _language;
		}
	}
}

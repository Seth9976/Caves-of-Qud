using System;
using System.Globalization;

namespace NUnit.Framework.Internal
{
	// Token: 0x02000065 RID: 101
	public class CultureDetector
	{
		// Token: 0x06000369 RID: 873 RVA: 0x0000799B File Offset: 0x00005B9B
		public CultureDetector()
		{
			this.currentCulture = CultureInfo.CurrentCulture;
		}

		// Token: 0x0600036A RID: 874 RVA: 0x000079B9 File Offset: 0x00005BB9
		public CultureDetector(string culture)
		{
			this.currentCulture = new CultureInfo(culture);
		}

		// Token: 0x0600036B RID: 875 RVA: 0x000079D8 File Offset: 0x00005BD8
		public bool IsCultureSupported(string[] cultures)
		{
			foreach (string text in cultures)
			{
				if (this.IsCultureSupported(text))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00007A08 File Offset: 0x00005C08
		public bool IsCultureSupported(CultureAttribute cultureAttribute)
		{
			string include = cultureAttribute.Include;
			string exclude = cultureAttribute.Exclude;
			if (include != null && !this.IsCultureSupported(include))
			{
				this.reason = string.Format("Only supported under culture {0}", include);
				return false;
			}
			if (exclude != null && this.IsCultureSupported(exclude))
			{
				this.reason = string.Format("Not supported under culture {0}", exclude);
				return false;
			}
			return true;
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00007A64 File Offset: 0x00005C64
		public bool IsCultureSupported(string culture)
		{
			culture = culture.Trim();
			if (culture.IndexOf(',') >= 0)
			{
				if (this.IsCultureSupported(culture.Split(new char[] { ',' })))
				{
					return true;
				}
			}
			else if (this.currentCulture.Name == culture || this.currentCulture.TwoLetterISOLanguageName == culture)
			{
				return true;
			}
			this.reason = "Only supported under culture " + culture;
			return false;
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600036E RID: 878 RVA: 0x00007AD9 File Offset: 0x00005CD9
		public string Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x0400006E RID: 110
		private CultureInfo currentCulture;

		// Token: 0x0400006F RID: 111
		private string reason = string.Empty;
	}
}

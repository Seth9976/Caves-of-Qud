using System;
using System.Globalization;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;

namespace NUnit.Framework
{
	// Token: 0x02000015 RID: 21
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public class CultureAttribute : IncludeExcludeAttribute, IApplyToTest
	{
		// Token: 0x06000131 RID: 305 RVA: 0x00003C70 File Offset: 0x00001E70
		public CultureAttribute()
		{
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00003C8E File Offset: 0x00001E8E
		public CultureAttribute(string cultures)
			: base(cultures)
		{
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00003CAD File Offset: 0x00001EAD
		public void ApplyToTest(Test test)
		{
			if (test.RunState != RunState.NotRunnable && !this.IsCultureSupported())
			{
				test.RunState = RunState.Skipped;
				test.Properties.Set("_SKIPREASON", base.Reason);
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00003CDC File Offset: 0x00001EDC
		private bool IsCultureSupported()
		{
			if (base.Include != null && !this.cultureDetector.IsCultureSupported(base.Include))
			{
				base.Reason = string.Format("Only supported under culture {0}", base.Include);
				return false;
			}
			if (base.Exclude != null && this.cultureDetector.IsCultureSupported(base.Exclude))
			{
				base.Reason = string.Format("Not supported under culture {0}", base.Exclude);
				return false;
			}
			return true;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00003D50 File Offset: 0x00001F50
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
			return false;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00003DB4 File Offset: 0x00001FB4
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

		// Token: 0x0400001A RID: 26
		private CultureDetector cultureDetector = new CultureDetector();

		// Token: 0x0400001B RID: 27
		private CultureInfo currentCulture = CultureInfo.CurrentCulture;
	}
}

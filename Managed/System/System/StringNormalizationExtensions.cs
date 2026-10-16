using System;
using System.Text;

namespace System
{
	// Token: 0x0200014A RID: 330
	public static class StringNormalizationExtensions
	{
		// Token: 0x060008CE RID: 2254 RVA: 0x000207BF File Offset: 0x0001E9BF
		public static bool IsNormalized(this string strInput)
		{
			return strInput.IsNormalized(NormalizationForm.FormC);
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x000207C8 File Offset: 0x0001E9C8
		public static bool IsNormalized(this string strInput, NormalizationForm normalizationForm)
		{
			if (strInput == null)
			{
				throw new ArgumentNullException("strInput");
			}
			return strInput.IsNormalized(normalizationForm);
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x000207DF File Offset: 0x0001E9DF
		public static string Normalize(this string strInput)
		{
			return strInput.Normalize(NormalizationForm.FormC);
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x000207E8 File Offset: 0x0001E9E8
		public static string Normalize(this string strInput, NormalizationForm normalizationForm)
		{
			if (strInput == null)
			{
				throw new ArgumentNullException("strInput");
			}
			return strInput.Normalize(normalizationForm);
		}
	}
}

using System;
using System.Diagnostics;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000235 RID: 565
	internal abstract class WellKnownAttributeData
	{
		// Token: 0x060015FA RID: 5626 RVA: 0x000454C3 File Offset: 0x000436C3
		public WellKnownAttributeData()
		{
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x000454CB File Offset: 0x000436CB
		[Conditional("DEBUG")]
		protected void VerifySealed(bool expected = true)
		{
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x000454CD File Offset: 0x000436CD
		[Conditional("DEBUG")]
		internal void VerifyDataStored(bool expected = true)
		{
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x000454CF File Offset: 0x000436CF
		[Conditional("DEBUG")]
		protected void SetDataStored()
		{
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x000454D1 File Offset: 0x000436D1
		[Conditional("DEBUG")]
		internal static void Seal(WellKnownAttributeData data)
		{
		}

		// Token: 0x0400091A RID: 2330
		public static readonly string StringMissingValue = "StringMissingValue";
	}
}

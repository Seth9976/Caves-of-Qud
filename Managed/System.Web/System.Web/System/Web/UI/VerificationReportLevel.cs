using System;

namespace System.Web.UI
{
	/// <summary>Specifies reporting levels for an accessibility rule defined by an <see cref="T:System.Web.UI.VerificationAttribute" /> instance.</summary>
	// Token: 0x02000244 RID: 580
	public enum VerificationReportLevel
	{
		/// <summary>The verification rule represented by the <see cref="T:System.Web.UI.VerificationAttribute" /> instance is an error.</summary>
		// Token: 0x040015FD RID: 5629
		Error,
		/// <summary>The verification rule represented by the <see cref="T:System.Web.UI.VerificationAttribute" /> instance is a warning.</summary>
		// Token: 0x040015FE RID: 5630
		Warning,
		/// <summary>The verification rule represented by the <see cref="T:System.Web.UI.VerificationAttribute" /> instance is a guideline.</summary>
		// Token: 0x040015FF RID: 5631
		Guideline
	}
}

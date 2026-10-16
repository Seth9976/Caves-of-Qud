using System;

namespace System.Web.UI
{
	/// <summary>Specifies how conditional expressions defined by an <see cref="T:System.Web.UI.VerificationAttribute" /> instance are used in verification.</summary>
	// Token: 0x02000245 RID: 581
	public enum VerificationRule
	{
		/// <summary>The conditional expression specified in an <see cref="T:System.Web.UI.VerificationAttribute" /> instance is required.</summary>
		// Token: 0x04001601 RID: 5633
		Required,
		/// <summary>The conditional expression specified in an <see cref="T:System.Web.UI.VerificationAttribute" /> instance is prohibited.</summary>
		// Token: 0x04001602 RID: 5634
		Prohibited,
		/// <summary>The conditional expression specified in an <see cref="T:System.Web.UI.VerificationAttribute" /> instance must have a left hand side that is not an empty string ("").</summary>
		// Token: 0x04001603 RID: 5635
		NotEmptyString
	}
}

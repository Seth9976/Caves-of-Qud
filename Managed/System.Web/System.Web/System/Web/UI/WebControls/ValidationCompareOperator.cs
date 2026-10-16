using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Specifies the validation comparison operators used by the <see cref="T:System.Web.UI.WebControls.CompareValidator" /> control.</summary>
	// Token: 0x0200031F RID: 799
	public enum ValidationCompareOperator
	{
		/// <summary>A comparison for equality.</summary>
		// Token: 0x040017B5 RID: 6069
		Equal,
		/// <summary>A comparison for inequality.</summary>
		// Token: 0x040017B6 RID: 6070
		NotEqual,
		/// <summary>A comparison for greater than.</summary>
		// Token: 0x040017B7 RID: 6071
		GreaterThan,
		/// <summary>A comparison for greater than or equal to.</summary>
		// Token: 0x040017B8 RID: 6072
		GreaterThanEqual,
		/// <summary>A comparison for less than.</summary>
		// Token: 0x040017B9 RID: 6073
		LessThan,
		/// <summary>A comparison for less than or equal to.</summary>
		// Token: 0x040017BA RID: 6074
		LessThanEqual,
		/// <summary>A comparison for data type only.</summary>
		// Token: 0x040017BB RID: 6075
		DataTypeCheck
	}
}

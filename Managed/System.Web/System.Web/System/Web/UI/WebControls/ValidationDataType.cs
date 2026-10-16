using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Specifies the validation data types used by the <see cref="T:System.Web.UI.WebControls.CompareValidator" /> and <see cref="T:System.Web.UI.WebControls.RangeValidator" /> controls.</summary>
	// Token: 0x02000320 RID: 800
	public enum ValidationDataType
	{
		/// <summary>A string data type. The value is treated as a <see cref="T:System.String" />.</summary>
		// Token: 0x040017BD RID: 6077
		String,
		/// <summary>A 32-bit signed integer data type. The value is treated as a <see cref="T:System.Int32" />.</summary>
		// Token: 0x040017BE RID: 6078
		Integer,
		/// <summary>A double precision floating point number data type. The value is treated as a <see cref="T:System.Double" />.</summary>
		// Token: 0x040017BF RID: 6079
		Double,
		/// <summary>A date data type. Only numeric dates are allowed. The time portion cannot be specified.</summary>
		// Token: 0x040017C0 RID: 6080
		Date,
		/// <summary>A monetary data type. The value is treated as a <see cref="T:System.Decimal" />. However, currency and grouping symbols are still allowed.</summary>
		// Token: 0x040017C1 RID: 6081
		Currency
	}
}

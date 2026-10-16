using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Specifies the unit of measurement.</summary>
	// Token: 0x0200031E RID: 798
	public enum UnitType
	{
		/// <summary>Measurement is in pixels.</summary>
		// Token: 0x040017AB RID: 6059
		Pixel = 1,
		/// <summary>Measurement is in points. A point represents 1/72 of an inch.</summary>
		// Token: 0x040017AC RID: 6060
		Point,
		/// <summary>Measurement is in picas. A pica represents 12 points.</summary>
		// Token: 0x040017AD RID: 6061
		Pica,
		/// <summary>Measurement is in inches.</summary>
		// Token: 0x040017AE RID: 6062
		Inch,
		/// <summary>Measurement is in millimeters.</summary>
		// Token: 0x040017AF RID: 6063
		Mm,
		/// <summary>Measurement is in centimeters.</summary>
		// Token: 0x040017B0 RID: 6064
		Cm,
		/// <summary>Measurement is a percentage relative to the parent element.</summary>
		// Token: 0x040017B1 RID: 6065
		Percentage,
		/// <summary>Measurement is relative to the height of the parent element's font.</summary>
		// Token: 0x040017B2 RID: 6066
		Em,
		/// <summary>Measurement is relative to the height of the lowercase letter x of the parent element's font.</summary>
		// Token: 0x040017B3 RID: 6067
		Ex
	}
}

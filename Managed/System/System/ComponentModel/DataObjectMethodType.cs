using System;

namespace System.ComponentModel
{
	/// <summary>Identifies the type of data operation performed by a method, as specified by the <see cref="T:System.ComponentModel.DataObjectMethodAttribute" /> applied to the method.</summary>
	// Token: 0x0200039A RID: 922
	public enum DataObjectMethodType
	{
		/// <summary>Indicates that a method is used for a data operation that fills a <see cref="T:System.Data.DataSet" /> object.</summary>
		// Token: 0x04000F33 RID: 3891
		Fill,
		/// <summary>Indicates that a method is used for a data operation that retrieves data.</summary>
		// Token: 0x04000F34 RID: 3892
		Select,
		/// <summary>Indicates that a method is used for a data operation that updates data.</summary>
		// Token: 0x04000F35 RID: 3893
		Update,
		/// <summary>Indicates that a method is used for a data operation that inserts data.</summary>
		// Token: 0x04000F36 RID: 3894
		Insert,
		/// <summary>Indicates that a method is used for a data operation that deletes data.</summary>
		// Token: 0x04000F37 RID: 3895
		Delete
	}
}

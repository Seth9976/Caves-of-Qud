using System;

namespace System.ComponentModel
{
	/// <summary>Defines identifiers for types of inheritance levels.</summary>
	// Token: 0x020003A2 RID: 930
	public enum InheritanceLevel
	{
		/// <summary>The object is inherited.</summary>
		// Token: 0x04000F45 RID: 3909
		Inherited = 1,
		/// <summary>The object is inherited, but has read-only access.</summary>
		// Token: 0x04000F46 RID: 3910
		InheritedReadOnly,
		/// <summary>The object is not inherited.</summary>
		// Token: 0x04000F47 RID: 3911
		NotInherited
	}
}

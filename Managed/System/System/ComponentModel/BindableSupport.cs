using System;

namespace System.ComponentModel
{
	/// <summary>Specifies values to indicate whether a property can be bound to a data element or another property.</summary>
	// Token: 0x02000386 RID: 902
	public enum BindableSupport
	{
		/// <summary>The property is not bindable at design time.</summary>
		// Token: 0x04000F05 RID: 3845
		No,
		/// <summary>The property is bindable at design time.</summary>
		// Token: 0x04000F06 RID: 3846
		Yes,
		/// <summary>The property is set to the default.</summary>
		// Token: 0x04000F07 RID: 3847
		Default
	}
}

using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the visibility a property has to the design-time serializer.</summary>
	// Token: 0x0200036C RID: 876
	public enum DesignerSerializationVisibility
	{
		/// <summary>The code generator does not produce code for the object.</summary>
		// Token: 0x04000ED0 RID: 3792
		Hidden,
		/// <summary>The code generator produces code for the object.</summary>
		// Token: 0x04000ED1 RID: 3793
		Visible,
		/// <summary>The code generator produces code for the contents of the object, rather than for the object itself.</summary>
		// Token: 0x04000ED2 RID: 3794
		Content
	}
}

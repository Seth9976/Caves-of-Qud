using System;

namespace System.Web.ModelBinding
{
	/// <summary>Enumerates model-binding behavior options.</summary>
	// Token: 0x02000512 RID: 1298
	public enum BindingBehavior
	{
		/// <summary>The property should be model bound if a value is available from the value provider.</summary>
		// Token: 0x04001F39 RID: 7993
		Optional,
		/// <summary>The property should be excluded from model binding.</summary>
		// Token: 0x04001F3A RID: 7994
		Never,
		/// <summary>The property is required for model binding.</summary>
		// Token: 0x04001F3B RID: 7995
		Required
	}
}

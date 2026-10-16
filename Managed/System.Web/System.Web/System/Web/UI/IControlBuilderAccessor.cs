using System;

namespace System.Web.UI
{
	/// <summary>Allows the control serializer to get to the builder for a control.</summary>
	// Token: 0x02000166 RID: 358
	public interface IControlBuilderAccessor
	{
		/// <summary>Gets the control builder for this control.</summary>
		/// <returns>The <see cref="T:System.Web.UI.ControlBuilder" /> that built the control; otherwise, null if no builder was used.</returns>
		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06000F19 RID: 3865
		ControlBuilder ControlBuilder { get; }
	}
}

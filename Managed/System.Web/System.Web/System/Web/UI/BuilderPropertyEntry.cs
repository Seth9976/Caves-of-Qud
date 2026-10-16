using System;

namespace System.Web.UI
{
	/// <summary>Serves as the base class for all property entries that require a control builder.</summary>
	// Token: 0x020001A4 RID: 420
	public abstract class BuilderPropertyEntry : PropertyEntry
	{
		/// <summary>Gets or sets the control builder for the property entry.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ControlBuilder" /> for this property entry.</returns>
		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06001017 RID: 4119 RVA: 0x00029F8B File Offset: 0x0002818B
		// (set) Token: 0x06001018 RID: 4120 RVA: 0x00029F93 File Offset: 0x00028193
		public ControlBuilder Builder { get; set; }
	}
}

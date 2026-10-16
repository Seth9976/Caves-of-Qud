using System;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Defines the contract a control implements to act as a configuration control for a transformer in a Web Parts connection.</summary>
	// Token: 0x0200045D RID: 1117
	public interface ITransformerConfigurationControl
	{
		/// <summary>Occurs when transformer configuration is not completed.</summary>
		// Token: 0x140000FE RID: 254
		// (add) Token: 0x060033A6 RID: 13222
		// (remove) Token: 0x060033A7 RID: 13223
		event EventHandler Cancelled;

		/// <summary>Occurs when transformer configuration is successfully completed. </summary>
		// Token: 0x140000FF RID: 255
		// (add) Token: 0x060033A8 RID: 13224
		// (remove) Token: 0x060033A9 RID: 13225
		event EventHandler Succeeded;
	}
}

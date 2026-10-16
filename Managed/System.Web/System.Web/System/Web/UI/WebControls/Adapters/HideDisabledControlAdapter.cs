using System;

namespace System.Web.UI.WebControls.Adapters
{
	/// <summary>Provides rendering capabilities for the associated Web control to modify the default markup or behavior for a specific browser.</summary>
	// Token: 0x02000457 RID: 1111
	public class HideDisabledControlAdapter : WebControlAdapter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Adapters.HideDisabledControlAdapter" /> class. </summary>
		// Token: 0x06003381 RID: 13185 RVA: 0x00087C93 File Offset: 0x00085E93
		public HideDisabledControlAdapter()
		{
		}

		// Token: 0x06003382 RID: 13186 RVA: 0x00087C9B File Offset: 0x00085E9B
		internal HideDisabledControlAdapter(WebControl c)
			: base(c)
		{
		}

		/// <summary>Writes the associated Web control to the output stream as HTML.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> containing methods to build and render the device-specific output. </param>
		// Token: 0x06003383 RID: 13187 RVA: 0x00087CBF File Offset: 0x00085EBF
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (!base.Control.IsEnabled)
			{
				return;
			}
			base.Render(writer);
		}
	}
}

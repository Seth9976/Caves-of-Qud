using System;
using System.Web.UI.Adapters;

namespace System.Web.UI.WebControls.Adapters
{
	/// <summary>Customizes rendering for the Web control to which the control adapter is attached, to modify the default markup or behavior for specific browsers.</summary>
	// Token: 0x0200045A RID: 1114
	public class WebControlAdapter : ControlAdapter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.Adapters.WebControlAdapter" /> class.</summary>
		// Token: 0x06003395 RID: 13205 RVA: 0x000430B6 File Offset: 0x000412B6
		public WebControlAdapter()
		{
		}

		// Token: 0x06003396 RID: 13206 RVA: 0x000430BE File Offset: 0x000412BE
		internal WebControlAdapter(WebControl wc)
			: base(wc)
		{
		}

		/// <summary>Generates the target-specific markup for the control to which the control adapter is attached.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> containing methods to render the target-specific output. </param>
		// Token: 0x06003397 RID: 13207 RVA: 0x00087D51 File Offset: 0x00085F51
		protected internal override void Render(HtmlTextWriter writer)
		{
			this.RenderBeginTag(writer);
			this.RenderContents(writer);
			this.RenderEndTag(writer);
		}

		/// <summary>Creates the beginning tag for the Web control in the markup that is transmitted to the target browser.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> containing methods to render the target-specific output. </param>
		// Token: 0x06003398 RID: 13208 RVA: 0x00087D68 File Offset: 0x00085F68
		protected virtual void RenderBeginTag(HtmlTextWriter writer)
		{
			this.Control.RenderBeginTag(writer);
		}

		/// <summary>Generates the target-specific inner markup for the Web control to which the control adapter is attached.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> containing methods to render the target-specific output. </param>
		// Token: 0x06003399 RID: 13209 RVA: 0x00087D76 File Offset: 0x00085F76
		protected virtual void RenderContents(HtmlTextWriter writer)
		{
			this.Control.RenderContents(writer);
		}

		/// <summary>Creates the ending tag for the Web control in the markup that is transmitted to the target browser.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> containing methods to render the target-specific output. </param>
		// Token: 0x0600339A RID: 13210 RVA: 0x00087D84 File Offset: 0x00085F84
		protected virtual void RenderEndTag(HtmlTextWriter writer)
		{
			this.Control.RenderEndTag(writer);
		}

		/// <summary>Gets a reference to the Web control to which this control adapter is attached.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.WebControl" /> to which this <see cref="T:System.Web.UI.WebControls.Adapters.WebControlAdapter" /> is attached.</returns>
		// Token: 0x17001052 RID: 4178
		// (get) Token: 0x0600339B RID: 13211 RVA: 0x00087D92 File Offset: 0x00085F92
		protected new WebControl Control
		{
			get
			{
				return (WebControl)this.control;
			}
		}

		/// <summary>Gets a value indicating whether the Web control and all its parent controls are enabled.</summary>
		/// <returns>true if the associated <see cref="T:System.Web.UI.WebControls.WebControl" /> and all its parent controls are enabled; otherwise, false.</returns>
		// Token: 0x17001053 RID: 4179
		// (get) Token: 0x0600339C RID: 13212 RVA: 0x00087D9F File Offset: 0x00085F9F
		protected bool IsEnabled
		{
			get
			{
				return this.Control.IsEnabled;
			}
		}
	}
}

using System;
using System.ComponentModel;

namespace System.Web.UI.Adapters
{
	/// <summary>Customizes rendering for the derived control to which the adapter is attached, to modify the default markup or behavior for specific browsers, and is the base class from which all control adapters inherit.</summary>
	// Token: 0x02000275 RID: 629
	public abstract class ControlAdapter
	{
		// Token: 0x060019F7 RID: 6647 RVA: 0x00042F4D File Offset: 0x0004114D
		internal ControlAdapter(Control c)
		{
			this.control = c;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Adapters.ControlAdapter" /> class.</summary>
		// Token: 0x060019F8 RID: 6648 RVA: 0x00002050 File Offset: 0x00000250
		protected ControlAdapter()
		{
		}

		/// <summary>Gets a reference to the browser capabilities of the client making the current HTTP request.</summary>
		/// <returns>An <see cref="T:System.Web.HttpBrowserCapabilities" /> specifying client browser and markup capabilities.</returns>
		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x060019F9 RID: 6649 RVA: 0x00042F5C File Offset: 0x0004115C
		protected HttpBrowserCapabilities Browser
		{
			get
			{
				Page page = this.Page;
				if (page != null)
				{
					return page.Request.Browser;
				}
				return null;
			}
		}

		/// <summary>Gets a reference to the control to which this control adapter is attached.</summary>
		/// <returns>The <see cref="T:System.Web.UI.Control" /> to which this <see cref="T:System.Web.UI.Adapters.ControlAdapter" /> is attached.</returns>
		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x060019FA RID: 6650 RVA: 0x00042F80 File Offset: 0x00041180
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected Control Control
		{
			get
			{
				return this.control;
			}
		}

		/// <summary>Gets a reference to the page where the control associated with this adapter resides.</summary>
		/// <returns>A <see cref="T:System.Web.UI.Page" /> that provides access to the page instance where the associated control is situated.</returns>
		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x060019FB RID: 6651 RVA: 0x00042F88 File Offset: 0x00041188
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected Page Page
		{
			get
			{
				Control control = this.Control;
				if (control != null)
				{
					return control.Page;
				}
				return null;
			}
		}

		/// <summary>Gets a reference to the page adapter for the page where the associated control resides.</summary>
		/// <returns>A <see cref="T:System.Web.UI.Adapters.PageAdapter" /> for the page where the control associated with the current <see cref="T:System.Web.UI.Adapters.ControlAdapter" /> is situated.</returns>
		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x060019FC RID: 6652 RVA: 0x00042FA8 File Offset: 0x000411A8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		protected PageAdapter PageAdapter
		{
			get
			{
				Page page = this.Page;
				if (page != null)
				{
					return page.PageAdapter;
				}
				return null;
			}
		}

		/// <summary>Called prior to the rendering of a control. In a derived adapter class, generates opening tags that are required by a specific target but not needed by HTML browsers.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> containing methods to render the target-specific output. </param>
		// Token: 0x060019FD RID: 6653 RVA: 0x00042FC7 File Offset: 0x000411C7
		protected internal virtual void BeginRender(HtmlTextWriter writer)
		{
			writer.BeginRender();
		}

		/// <summary>Creates the target-specific child controls for a composite control.</summary>
		// Token: 0x060019FE RID: 6654 RVA: 0x00042FD0 File Offset: 0x000411D0
		protected internal virtual void CreateChildControls()
		{
			Control control = this.Control;
			if (control != null)
			{
				control.CreateChildControls();
			}
		}

		/// <summary>Called after the rendering of a control. In a derived adapter class, generates closing tags that are required by a specific target but not needed by HTML browsers.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> containing methods to render the target-specific output. </param>
		// Token: 0x060019FF RID: 6655 RVA: 0x00042FED File Offset: 0x000411ED
		protected internal virtual void EndRender(HtmlTextWriter writer)
		{
			writer.EndRender();
		}

		/// <summary>Loads adapter control state information that was saved by <see cref="M:System.Web.UI.Adapters.ControlAdapter.SaveAdapterControlState" /> during a previous request to the page where the control associated with this control adapter resides.</summary>
		/// <param name="state">An <see cref="T:System.Object" /> that contains the adapter's control state information as a <see cref="T:System.Web.UI.StateBag" />. </param>
		// Token: 0x06001A00 RID: 6656 RVA: 0x00003946 File Offset: 0x00001B46
		protected internal virtual void LoadAdapterControlState(object state)
		{
		}

		/// <summary>Loads adapter view state information that was saved by <see cref="M:System.Web.UI.Adapters.ControlAdapter.SaveAdapterViewState" /> during a previous request to the page where the control associated with this control adapter resides.</summary>
		/// <param name="state">An <see cref="T:System.Object" /> that contains the adapter view state information as a <see cref="T:System.Web.UI.StateBag" />. </param>
		// Token: 0x06001A01 RID: 6657 RVA: 0x00003946 File Offset: 0x00001B46
		protected internal virtual void LoadAdapterViewState(object state)
		{
		}

		/// <summary>Overrides the <see cref="M:System.Web.UI.Control.OnInit(System.EventArgs)" /> method for the associated control.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001A02 RID: 6658 RVA: 0x00042FF8 File Offset: 0x000411F8
		protected internal virtual void OnInit(EventArgs e)
		{
			Control control = this.Control;
			if (control != null)
			{
				control.OnInit(e);
			}
		}

		/// <summary>Overrides the <see cref="M:System.Web.UI.Control.OnLoad(System.EventArgs)" /> method for the associated control.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001A03 RID: 6659 RVA: 0x00043018 File Offset: 0x00041218
		protected internal virtual void OnLoad(EventArgs e)
		{
			Control control = this.Control;
			if (control != null)
			{
				control.OnLoad(e);
			}
		}

		/// <summary>Overrides the <see cref="M:System.Web.UI.Control.OnPreRender(System.EventArgs)" /> method for the associated control.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001A04 RID: 6660 RVA: 0x00043038 File Offset: 0x00041238
		protected internal virtual void OnPreRender(EventArgs e)
		{
			Control control = this.Control;
			if (control != null)
			{
				control.OnPreRender(e);
			}
		}

		/// <summary>Overrides the <see cref="M:System.Web.UI.Control.OnUnload(System.EventArgs)" /> method for the associated control.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001A05 RID: 6661 RVA: 0x00043058 File Offset: 0x00041258
		protected internal virtual void OnUnload(EventArgs e)
		{
			Control control = this.Control;
			if (control != null)
			{
				control.OnUnload(e);
			}
		}

		/// <summary>Generates the target-specific markup for the control to which the control adapter is attached.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> to use to render the target-specific output. </param>
		// Token: 0x06001A06 RID: 6662 RVA: 0x00043078 File Offset: 0x00041278
		protected internal virtual void Render(HtmlTextWriter writer)
		{
			Control control = this.Control;
			if (control != null)
			{
				control.Render(writer);
			}
		}

		/// <summary>Generates the target-specific markup for the child controls in a composite control to which the control adapter is attached.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> to use to render the target-specific output. </param>
		// Token: 0x06001A07 RID: 6663 RVA: 0x00043098 File Offset: 0x00041298
		protected internal virtual void RenderChildren(HtmlTextWriter writer)
		{
			Control control = this.Control;
			if (control != null)
			{
				control.RenderChildren(writer);
			}
		}

		/// <summary>Saves control state information for the control adapter.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the adapter's control state information as a <see cref="T:System.Web.UI.StateBag" />. </returns>
		// Token: 0x06001A08 RID: 6664 RVA: 0x00003BF6 File Offset: 0x00001DF6
		protected internal virtual object SaveAdapterControlState()
		{
			return null;
		}

		/// <summary>Saves view state information for the control adapter.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the adapter view state information as a <see cref="T:System.Web.UI.StateBag" />. </returns>
		// Token: 0x06001A09 RID: 6665 RVA: 0x00003BF6 File Offset: 0x00001DF6
		protected internal virtual object SaveAdapterViewState()
		{
			return null;
		}

		// Token: 0x04001644 RID: 5700
		internal Control control;
	}
}

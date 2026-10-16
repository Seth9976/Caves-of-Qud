using System;
using System.Collections;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Serves as the base class for a set of helper zones that appear only in certain associated page display modes. </summary>
	// Token: 0x020006D9 RID: 1753
	public abstract class ToolZone : WebZone, IPostBackEventHandler
	{
		/// <summary>Associates a collection of <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDisplayMode" /> objects with a particular <see cref="T:System.Web.UI.WebControls.WebParts.ToolZone" /> zone, so that the zone can be displayed in the appropriate page display modes.</summary>
		/// <param name="associatedDisplayModes">An <see cref="T:System.Collections.ICollection" /> of <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDisplayMode" /> objects that determine when a zone can be displayed.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="associatedDisplayModes" /> parameter is equal to null or 0.</exception>
		// Token: 0x06004A5B RID: 19035 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected ToolZone(ICollection associatedDisplayModes)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Associates a single <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDisplayMode" /> object with a particular <see cref="T:System.Web.UI.WebControls.WebParts.ToolZone" /> zone, so that the zone can be displayed in the appropriate page display mode.</summary>
		/// <param name="associatedDisplayMode">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDisplayMode" /> that determines when a zone can be displayed.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="associatedDisplayMode" /> parameter is equal to null.</exception>
		// Token: 0x06004A5C RID: 19036 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected ToolZone(WebPartDisplayMode associatedDisplayMode)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the collection of <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDisplayMode" /> objects that are associated with a particular <see cref="T:System.Web.UI.WebControls.WebParts.ToolZone" /> zone.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDisplayModeCollection" /> that contains <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDisplayMode" /> objects associated with a <see cref="T:System.Web.UI.WebControls.WebParts.ToolZone" /> control. </returns>
		// Token: 0x1700170B RID: 5899
		// (get) Token: 0x06004A5D RID: 19037 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public WebPartDisplayModeCollection AssociatedDisplayModes
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a value that indicates whether a <see cref="T:System.Web.UI.WebControls.WebParts.ToolZone" /> control is currently displayed.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.WebParts.ToolZone" /> is currently displayed; otherwise, false. The default value is false.</returns>
		// Token: 0x1700170C RID: 5900
		// (get) Token: 0x06004A5E RID: 19038 RVA: 0x000C7EC8 File Offset: 0x000C60C8
		protected virtual bool Display
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets the style attributes for the editable controls contained in a <see cref="T:System.Web.UI.WebControls.WebParts.ToolZone" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> that contains style attributes for editable controls within a <see cref="T:System.Web.UI.WebControls.WebParts.ToolZone" /> control.</returns>
		// Token: 0x1700170D RID: 5901
		// (get) Token: 0x06004A5F RID: 19039 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public Style EditUIStyle
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> object in the header of a <see cref="T:System.Web.UI.WebControls.WebParts.ToolZone" /> control that is used to close the control.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> used to close a <see cref="T:System.Web.UI.WebControls.WebParts.ToolZone" /> control.</returns>
		// Token: 0x1700170E RID: 5902
		// (get) Token: 0x06004A60 RID: 19040 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public virtual WebPartVerb HeaderCloseVerb
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the style attributes for all header verbs displayed in a <see cref="T:System.Web.UI.WebControls.WebParts.ToolZone" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> that contains style attributes for header verbs within a <see cref="T:System.Web.UI.WebControls.WebParts.ToolZone" />.</returns>
		// Token: 0x1700170F RID: 5903
		// (get) Token: 0x06004A61 RID: 19041 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public Style HeaderVerbStyle
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets or sets the text in a <see cref="T:System.Web.UI.WebControls.WebParts.ToolZone" /> control that provides directions for end users.</summary>
		/// <returns>A string that contains the directions for end users. A default value appropriate to specific tool zones is provided by derived <see cref="T:System.Web.UI.WebControls.WebParts.ToolZone" /> classes.</returns>
		// Token: 0x17001710 RID: 5904
		// (get) Token: 0x06004A62 RID: 19042 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004A63 RID: 19043 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual string InstructionText
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets the style attributes for the instruction text that appears at the top of a <see cref="T:System.Web.UI.WebControls.WebParts.ToolZone" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> that contains style attributes for the instruction text within a <see cref="T:System.Web.UI.WebControls.WebParts.ToolZone" />.</returns>
		// Token: 0x17001711 RID: 5905
		// (get) Token: 0x06004A64 RID: 19044 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public Style InstructionTextStyle
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the style attributes for the contents of the labels that appear alongside the editing controls within a <see cref="T:System.Web.UI.WebControls.WebParts.ToolZone" /> control. The derived <see cref="T:System.Web.UI.WebControls.WebParts.ToolZone" /> controls, such as <see cref="T:System.Web.UI.WebControls.WebParts.CatalogZone" /> and <see cref="T:System.Web.UI.WebControls.WebParts.EditorZone" />, apply the styles to the labels.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> that contains style attributes for the labels within a <see cref="T:System.Web.UI.WebControls.WebParts.ToolZone" />.</returns>
		// Token: 0x17001712 RID: 5906
		// (get) Token: 0x06004A65 RID: 19045 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public Style LabelStyle
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Provides a base method declaration that derived <see cref="T:System.Web.UI.WebControls.WebParts.ToolZone" /> controls can override to handle the details of closing a specialized zone.</summary>
		// Token: 0x06004A66 RID: 19046
		protected abstract void Close();

		/// <summary>Provides a base method declaration that derived <see cref="T:System.Web.UI.WebControls.WebParts.ToolZone" /> controls can override to handle the details of changing page display modes for a specialized zone.</summary>
		/// <param name="sender">An <see cref="T:System.Object" /> that raises the <see cref="M:System.Web.UI.WebControls.WebParts.ToolZone.OnDisplayModeChanged(System.Object,System.Web.UI.WebControls.WebParts.WebPartDisplayModeEventArgs)" /> method.</param>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDisplayModeEventArgs" /> that contains the event data.</param>
		// Token: 0x06004A67 RID: 19047 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected virtual void OnDisplayModeChanged(object sender, WebPartDisplayModeEventArgs e)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Provides a base method declaration that derived <see cref="T:System.Web.UI.WebControls.WebParts.ToolZone" /> controls can override to handle the event of changing which Web Parts control is selected within a specialized zone.</summary>
		/// <param name="sender">An <see cref="T:System.Object" /> that raises the <see cref="M:System.Web.UI.WebControls.WebParts.ToolZone.OnSelectedWebPartChanged(System.Object,System.Web.UI.WebControls.WebParts.WebPartEventArgs)" /> method.</param>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartEventArgs" /> that contains the event data. </param>
		// Token: 0x06004A68 RID: 19048 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected virtual void OnSelectedWebPartChanged(object sender, WebPartEventArgs e)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Calls the <see cref="M:System.Web.UI.WebControls.WebParts.ToolZone.Close" /> method for a <see cref="T:System.Web.UI.WebControls.WebParts.ToolZone" /> control when the control posts back to the server.</summary>
		/// <param name="eventArgument">A <see cref="T:System.String" /> that contains the argument for the event.</param>
		// Token: 0x06004A69 RID: 19049 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Overrides the base method to render verbs in the footer of a <see cref="T:System.Web.UI.WebControls.WebParts.ToolZone" /> control.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that renders verbs in a zone's footer area. </param>
		// Token: 0x06004A6A RID: 19050 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected override void RenderFooter(HtmlTextWriter writer)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Overrides the base method to provide specialized rendering for the header area required by <see cref="T:System.Web.UI.WebControls.WebParts.ToolZone" /> controls.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that renders the header section.</param>
		// Token: 0x06004A6B RID: 19051 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected override void RenderHeader(HtmlTextWriter writer)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Renders an individual verb with a <see cref="T:System.Web.UI.WebControls.WebParts.ToolZone" /> control.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that renders a single verb.</param>
		/// <param name="verb">The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> to be rendered within a zone.</param>
		// Token: 0x06004A6C RID: 19052 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected virtual void RenderVerb(HtmlTextWriter writer, WebPartVerb verb)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Provides a base method declaration that derived <see cref="T:System.Web.UI.WebControls.WebParts.ToolZone" /> controls can override to customize the rendering of the verbs within a specialized zone.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that renders the verbs in a zone.</param>
		// Token: 0x06004A6D RID: 19053 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected virtual void RenderVerbs(HtmlTextWriter writer)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Implements the <see cref="M:System.Web.UI.IPostBackEventHandler.RaisePostBackEvent(System.String)" /> method. </summary>
		/// <param name="eventArgument">A <see cref="T:System.String" /> that contains the postback event data.</param>
		// Token: 0x06004A6E RID: 19054 RVA: 0x0000B3D8 File Offset: 0x000095D8
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}

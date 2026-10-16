using System;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Serves as the base class for all zone controls that act as containers for <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls.</summary>
	// Token: 0x020006D8 RID: 1752
	public abstract class EditorZoneBase : ToolZone
	{
		/// <summary>Initializes the class for use by an inherited class instance. This constructor can only be called by an inherited class.</summary>
		// Token: 0x06004A42 RID: 19010 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected EditorZoneBase()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> object that enables end users to apply editing changes to a control in edit mode. </summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> that enables end users to apply changes to a control.</returns>
		// Token: 0x17001700 RID: 5888
		// (get) Token: 0x06004A43 RID: 19011 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public virtual WebPartVerb ApplyVerb
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> object that enables end users to cancel editing changes to a control in edit mode. </summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> that enables end users to cancel editing changes to a control.</returns>
		// Token: 0x17001701 RID: 5889
		// (get) Token: 0x06004A44 RID: 19012 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public virtual WebPartVerb CancelVerb
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x17001702 RID: 5890
		// (get) Token: 0x06004A45 RID: 19013 RVA: 0x000C7EAC File Offset: 0x000C60AC
		protected override bool Display
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets a reference to the instance of the <see cref="T:System.Web.UI.WebControls.WebParts.EditorPartChrome" /> class associated with the <see cref="T:System.Web.UI.WebControls.WebParts.EditorZoneBase" /> zone.</summary>
		/// <returns>An <see cref="T:System.Web.UI.WebControls.WebParts.EditorPartChrome" /> that contains style characteristics for the chrome elements of <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls in a zone.</returns>
		// Token: 0x17001703 RID: 5891
		// (get) Token: 0x06004A46 RID: 19014 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public EditorPartChrome EditorPartChrome
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a collection of all the <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls contained in an <see cref="T:System.Web.UI.WebControls.WebParts.EditorZoneBase" /> zone. </summary>
		/// <returns>An <see cref="T:System.Web.UI.WebControls.WebParts.EditorPartCollection" /> that contains all the individual <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls in a zone. </returns>
		/// <exception cref="T:System.InvalidOperationException">An <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> being added to the collection does not have a value assigned to its ID property.</exception>
		// Token: 0x17001704 RID: 5892
		// (get) Token: 0x06004A47 RID: 19015 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public EditorPartCollection EditorParts
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x17001705 RID: 5893
		// (get) Token: 0x06004A48 RID: 19016 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004A49 RID: 19017 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public override string EmptyZoneText
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

		/// <summary>Gets or sets the text of a zone-level error message to display at the top of the editing user interface (UI).</summary>
		/// <returns>A localized string that contains the error message. </returns>
		// Token: 0x17001706 RID: 5894
		// (get) Token: 0x06004A4A RID: 19018 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004A4B RID: 19019 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual string ErrorText
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

		// Token: 0x17001707 RID: 5895
		// (get) Token: 0x06004A4C RID: 19020 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004A4D RID: 19021 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public override string HeaderText
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

		// Token: 0x17001708 RID: 5896
		// (get) Token: 0x06004A4E RID: 19022 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x06004A4F RID: 19023 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public override string InstructionText
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

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> object that applies editing changes to a control in edit mode, and hides the editing user interface (UI). </summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> that applies editing changes to a control and hides the editing UI.</returns>
		// Token: 0x17001709 RID: 5897
		// (get) Token: 0x06004A50 RID: 19024 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public virtual WebPartVerb OKVerb
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control that is currently being edited. </summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> that is currently in edit mode.</returns>
		// Token: 0x1700170A RID: 5898
		// (get) Token: 0x06004A51 RID: 19025 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected WebPart WebPartToEdit
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Called when a user clicks a close verb in the header of a zone, this method ends the process of editing <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls, and returns a Web Parts page's display mode to browse mode.</summary>
		// Token: 0x06004A52 RID: 19026 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected override void Close()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a reference to a new <see cref="T:System.Web.UI.WebControls.WebParts.EditorPartChrome" /> object used to render the peripheral user interface (UI) elements around an <see cref="T:System.Web.UI.WebControls.WebParts.EditorZoneBase" /> zone.</summary>
		/// <returns>An <see cref="T:System.Web.UI.WebControls.WebParts.EditorPartChrome" /> that renders the peripheral UI elements for the zone.</returns>
		// Token: 0x06004A53 RID: 19027 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected virtual EditorPartChrome CreateEditorPartChrome()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Provides a base method declaration that derived zones can override to handle the details of creating the <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls contained in a zone.</summary>
		/// <returns>An <see cref="T:System.Web.UI.WebControls.WebParts.EditorPartCollection" /> that contains the collection of <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls associated with the zone. </returns>
		// Token: 0x06004A54 RID: 19028
		protected abstract EditorPartCollection CreateEditorParts();

		/// <summary>Sets the collection of <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls associated with an <see cref="T:System.Web.UI.WebControls.WebParts.EditorZoneBase" /> zone to null, which results in the <see cref="M:System.Web.UI.WebControls.WebParts.EditorZoneBase.CreateEditorParts" /> method being called to recreate the collection.</summary>
		// Token: 0x06004A55 RID: 19029 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected void InvalidateEditorParts()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.DisplayModeChanged" /> event and destroys all <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> and child controls in the zone in preparation for a Web page to enter or exit the edit display mode. </summary>
		/// <param name="sender">An <see cref="T:System.Object" /> that raises the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.DisplayModeChanged" /> event. </param>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDisplayModeEventArgs" /> that contains the event data. </param>
		// Token: 0x06004A56 RID: 19030 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected override void OnDisplayModeChanged(object sender, WebPartDisplayModeEventArgs e)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.SelectedWebPartChanged" /> event and sets the <see cref="P:System.Web.UI.WebControls.WebParts.EditorZoneBase.EditorParts" /> collection to null in the zone in preparation for a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control to enter or exit edit mode. </summary>
		/// <param name="sender">An <see cref="T:System.Object" /> that raises the <see cref="E:System.Web.UI.WebControls.WebParts.WebPartManager.SelectedWebPartChanged" /> event. </param>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartDisplayModeEventArgs" /> that contains the event data. </param>
		// Token: 0x06004A57 RID: 19031 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected override void OnSelectedWebPartChanged(object sender, WebPartEventArgs e)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Carries out the actions associated with one of the zone verbs, or raises an event that posts back to the server.</summary>
		/// <param name="eventArgument">A <see cref="T:System.String" /> that contains the argument for the event. </param>
		// Token: 0x06004A58 RID: 19032 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected override void RaisePostBackEvent(string eventArgument)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Overrides the base method to render the body area of a zone derived from the <see cref="T:System.Web.UI.WebControls.WebParts.EditorZoneBase" /> class.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the zone's body content. </param>
		// Token: 0x06004A59 RID: 19033 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected override void RenderBody(HtmlTextWriter writer)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Renders the verbs that apply at the zone level.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the zone's body content. </param>
		// Token: 0x06004A5A RID: 19034 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected override void RenderVerbs(HtmlTextWriter writer)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}

using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Serves as the base class for controls that reside in <see cref="T:System.Web.UI.WebControls.WebParts.EditorZoneBase" /> zones and are used to edit <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls.</summary>
	// Token: 0x0200047E RID: 1150
	[Bindable(false)]
	[Designer("System.Web.UI.Design.WebControls.WebParts.EditorPartDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	public abstract class EditorPart : Part
	{
		/// <summary>Saves the values in an <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> control to the corresponding properties in the associated <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</summary>
		/// <returns>true if the action of saving values from the <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> control to the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control is successful; otherwise (if an error occurs), false. </returns>
		// Token: 0x0600341F RID: 13343
		public abstract bool ApplyChanges();

		/// <summary>Retrieves the current state of an <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> control's parent zone.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> that has the current state of the <see cref="T:System.Web.UI.WebControls.WebParts.EditorZoneBase" /> zone that contains an <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> control.</returns>
		// Token: 0x06003420 RID: 13344 RVA: 0x00003A2B File Offset: 0x00001C2B
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		protected override IDictionary GetDesignModeState()
		{
			throw new NotImplementedException();
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Web.UI.WebControls.WebParts.EditorZoneBase" /> that contains the <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> control is null.</exception>
		// Token: 0x06003421 RID: 13345 RVA: 0x00088106 File Offset: 0x00086306
		protected internal override void OnPreRender(EventArgs e)
		{
			if (this.zone == null)
			{
				throw new InvalidOperationException();
			}
			base.OnPreRender(e);
			if (!this.Display)
			{
				this.Visible = false;
			}
		}

		/// <summary>Retrieves the property values from a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control for its associated <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> control.</summary>
		// Token: 0x06003422 RID: 13346
		public abstract void SyncChanges();

		/// <summary>Gets a value that indicates whether a control should be displayed when its associated <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control is in edit mode.</summary>
		/// <returns>A Boolean value that indicates whether the control should be displayed. The default value is true.</returns>
		// Token: 0x17001071 RID: 4209
		// (get) Token: 0x06003423 RID: 13347 RVA: 0x0008812C File Offset: 0x0008632C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual bool Display
		{
			get
			{
				return this.display;
			}
		}

		/// <summary>Gets a string that contains the title text displayed in the title bar of an <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> control.</summary>
		/// <returns>A string that represents the complete, visible title of the control. The default value is a calculated, culture-specific string supplied by the .NET Framework.</returns>
		// Token: 0x17001072 RID: 4210
		// (get) Token: 0x06003424 RID: 13348 RVA: 0x00088134 File Offset: 0x00086334
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string DisplayTitle
		{
			get
			{
				return this.displayTitle;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control that is currently being edited. </summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> that is currently in edit mode.</returns>
		// Token: 0x17001073 RID: 4211
		// (get) Token: 0x06003425 RID: 13349 RVA: 0x0008813C File Offset: 0x0008633C
		protected WebPart WebPartToEdit
		{
			get
			{
				return this.webPartToEdit;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control associated with the current Web page.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> on the page. </returns>
		// Token: 0x17001074 RID: 4212
		// (get) Token: 0x06003426 RID: 13350 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected WebPartManager WebPartManager
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.WebParts.EditorZoneBase" /> zone that contains an <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> control.</summary>
		/// <returns>An <see cref="T:System.Web.UI.WebControls.WebParts.EditorZoneBase" /> that contains the control.</returns>
		// Token: 0x17001075 RID: 4213
		// (get) Token: 0x06003427 RID: 13351 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected EditorZoneBase Zone
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x04001D01 RID: 7425
		private bool display = true;

		// Token: 0x04001D02 RID: 7426
		private WebPart webPartToEdit;

		// Token: 0x04001D03 RID: 7427
		private object zone;

		// Token: 0x04001D04 RID: 7428
		private string displayTitle;
	}
}

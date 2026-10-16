using System;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Serves as the base class for all Web Parts part controls, which render a modular user interface on a Web Forms page. </summary>
	// Token: 0x02000481 RID: 1153
	public abstract class Part : Panel, INamingContainer, ICompositeControlDesignerAccessor
	{
		// Token: 0x06003433 RID: 13363 RVA: 0x0008825F File Offset: 0x0008645F
		internal Part()
		{
			this.description = "";
			this.title = "";
			this.chrome_state = PartChromeState.Normal;
			this.chrome_type = PartChromeType.Default;
		}

		// Token: 0x06003434 RID: 13364 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		public override void DataBind()
		{
			throw new NotImplementedException();
		}

		/// <summary>Allows the developer of a designer for a composite part control to recreate the control's child controls on the design surface.</summary>
		// Token: 0x06003435 RID: 13365 RVA: 0x0004D96A File Offset: 0x0004BB6A
		[global::System.MonoTODO("not sure exactly what this one does..")]
		void ICompositeControlDesignerAccessor.RecreateChildControls()
		{
			this.CreateChildControls();
		}

		/// <summary>Gets or sets whether a part control is in a minimized or normal state.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.WebParts.PartChromeState" /> values. The default is <see cref="F:System.Web.UI.WebControls.WebParts.PartChromeState.Normal" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified is not one of the <see cref="T:System.Web.UI.WebControls.WebParts.PartChromeState" /> values. </exception>
		// Token: 0x17001078 RID: 4216
		// (get) Token: 0x06003436 RID: 13366 RVA: 0x0008828B File Offset: 0x0008648B
		// (set) Token: 0x06003437 RID: 13367 RVA: 0x00088293 File Offset: 0x00086493
		public virtual PartChromeState ChromeState
		{
			get
			{
				return this.chrome_state;
			}
			set
			{
				this.chrome_state = value;
			}
		}

		/// <summary>Gets or sets the type of border that frames a Web Parts control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.WebParts.PartChromeType" /> values. The default is <see cref="F:System.Web.UI.WebControls.WebParts.PartChromeType.Default" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value is not one of the <see cref="T:System.Web.UI.WebControls.WebParts.PartChromeType" /> values. </exception>
		// Token: 0x17001079 RID: 4217
		// (get) Token: 0x06003438 RID: 13368 RVA: 0x0008829C File Offset: 0x0008649C
		// (set) Token: 0x06003439 RID: 13369 RVA: 0x000882A4 File Offset: 0x000864A4
		public virtual PartChromeType ChromeType
		{
			get
			{
				return this.chrome_type;
			}
			set
			{
				this.chrome_type = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.ControlCollection" /> object that contains the child controls for a specified server control in the user interface hierarchy.</summary>
		/// <returns>The collection of child controls for the specified server control.</returns>
		// Token: 0x1700107A RID: 4218
		// (get) Token: 0x0600343A RID: 13370 RVA: 0x000882AD File Offset: 0x000864AD
		public new virtual ControlCollection Controls
		{
			get
			{
				if (this.controls == null)
				{
					this.controls = new ControlCollection(this);
				}
				return this.controls;
			}
		}

		/// <summary>Gets or sets a brief phrase that summarizes what the part control does, for use in ToolTips and catalogs of part controls.</summary>
		/// <returns>A string that briefly summarizes the part control's functionality. The default value is an empty string ("").</returns>
		// Token: 0x1700107B RID: 4219
		// (get) Token: 0x0600343B RID: 13371 RVA: 0x000882C9 File Offset: 0x000864C9
		// (set) Token: 0x0600343C RID: 13372 RVA: 0x000882D1 File Offset: 0x000864D1
		public virtual string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		/// <summary>Gets or sets the title of a part control.</summary>
		/// <returns>A string that represents the title of the part control. The default value is an empty string ("").</returns>
		// Token: 0x1700107C RID: 4220
		// (get) Token: 0x0600343D RID: 13373 RVA: 0x000882DA File Offset: 0x000864DA
		// (set) Token: 0x0600343E RID: 13374 RVA: 0x000882E2 File Offset: 0x000864E2
		public virtual string Title
		{
			get
			{
				return this.title;
			}
			set
			{
				this.title = value;
			}
		}

		// Token: 0x04001D06 RID: 7430
		private string description;

		// Token: 0x04001D07 RID: 7431
		private string title;

		// Token: 0x04001D08 RID: 7432
		private PartChromeState chrome_state;

		// Token: 0x04001D09 RID: 7433
		private PartChromeType chrome_type;

		// Token: 0x04001D0A RID: 7434
		private ControlCollection controls;
	}
}

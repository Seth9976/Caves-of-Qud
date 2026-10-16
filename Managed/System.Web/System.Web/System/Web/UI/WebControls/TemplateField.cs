using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a field that displays custom content in a data-bound control.</summary>
	// Token: 0x02000423 RID: 1059
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class TemplateField : DataControlField
	{
		/// <summary>Gets or sets the template for displaying the alternating items in a <see cref="T:System.Web.UI.WebControls.TemplateField" /> object.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" />-implemented object that contains the template for displaying the alternating items in a <see cref="T:System.Web.UI.WebControls.TemplateField" />. The default is null, which indicates that this property is not set.</returns>
		// Token: 0x17000F35 RID: 3893
		// (get) Token: 0x06002FB7 RID: 12215 RVA: 0x0007BCB6 File Offset: 0x00079EB6
		// (set) Token: 0x06002FB8 RID: 12216 RVA: 0x0007BCBE File Offset: 0x00079EBE
		[DefaultValue(null)]
		[TemplateContainer(typeof(IDataItemContainer), BindingDirection.TwoWay)]
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual ITemplate AlternatingItemTemplate
		{
			get
			{
				return this.alternatingItemTemplate;
			}
			set
			{
				this.alternatingItemTemplate = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets a value indicating whether the value that the <see cref="T:System.Web.UI.WebControls.TemplateField" /> object is bound to should be converted to null if it is <see cref="F:System.String.Empty" />.</summary>
		/// <returns>true if the value that the <see cref="T:System.Web.UI.WebControls.TemplateField" /> is bound to should be converted to null when it is <see cref="F:System.String.Empty" />; otherwise, false. The default value is false.</returns>
		// Token: 0x17000F36 RID: 3894
		// (get) Token: 0x06002FB9 RID: 12217 RVA: 0x0007BCD0 File Offset: 0x00079ED0
		// (set) Token: 0x06002FBA RID: 12218 RVA: 0x00046A0A File Offset: 0x00044C0A
		[WebCategory("Behavior")]
		[DefaultValue(true)]
		public virtual bool ConvertEmptyStringToNull
		{
			get
			{
				object obj = base.ViewState["ConvertEmptyStringToNull"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["ConvertEmptyStringToNull"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the template for displaying an item in edit mode in a <see cref="T:System.Web.UI.WebControls.TemplateField" /> object.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" />-implemented object that contains the template for displaying an item in edit mode in a <see cref="T:System.Web.UI.WebControls.TemplateField" />. The default is null, which indicates that this property is not set.</returns>
		// Token: 0x17000F37 RID: 3895
		// (get) Token: 0x06002FBB RID: 12219 RVA: 0x0007BCF9 File Offset: 0x00079EF9
		// (set) Token: 0x06002FBC RID: 12220 RVA: 0x0007BD01 File Offset: 0x00079F01
		[DefaultValue(null)]
		[TemplateContainer(typeof(IDataItemContainer), BindingDirection.TwoWay)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		public virtual ITemplate EditItemTemplate
		{
			get
			{
				return this.editItemTemplate;
			}
			set
			{
				this.editItemTemplate = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the template for displaying the footer section of a <see cref="T:System.Web.UI.WebControls.TemplateField" /> object.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" />-implemented object that contains the template for displaying the footer section of a <see cref="T:System.Web.UI.WebControls.TemplateField" />. The default is null, which indicates that this property is not set.</returns>
		// Token: 0x17000F38 RID: 3896
		// (get) Token: 0x06002FBD RID: 12221 RVA: 0x0007BD10 File Offset: 0x00079F10
		// (set) Token: 0x06002FBE RID: 12222 RVA: 0x0007BD18 File Offset: 0x00079F18
		[DefaultValue(null)]
		[TemplateContainer(typeof(IDataItemContainer), BindingDirection.OneWay)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		public virtual ITemplate FooterTemplate
		{
			get
			{
				return this.footerTemplate;
			}
			set
			{
				this.footerTemplate = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the template for displaying the header section of a <see cref="T:System.Web.UI.WebControls.TemplateField" /> object.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" />-implemented object that contains the template for displaying the header section of a <see cref="T:System.Web.UI.WebControls.TemplateField" /> in a data-bound control. The default is null, which indicates that this property is not set.</returns>
		// Token: 0x17000F39 RID: 3897
		// (get) Token: 0x06002FBF RID: 12223 RVA: 0x0007BD27 File Offset: 0x00079F27
		// (set) Token: 0x06002FC0 RID: 12224 RVA: 0x0007BD2F File Offset: 0x00079F2F
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(IDataItemContainer), BindingDirection.OneWay)]
		public virtual ITemplate HeaderTemplate
		{
			get
			{
				return this.headerTemplate;
			}
			set
			{
				this.headerTemplate = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the template for displaying an item in insert mode in a <see cref="T:System.Web.UI.WebControls.TemplateField" /> object.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" />-implemented object that contains the template for displaying an item in insert mode in a <see cref="T:System.Web.UI.WebControls.TemplateField" />. The default is null, which indicates that this property is not set.</returns>
		// Token: 0x17000F3A RID: 3898
		// (get) Token: 0x06002FC1 RID: 12225 RVA: 0x0007BD3E File Offset: 0x00079F3E
		// (set) Token: 0x06002FC2 RID: 12226 RVA: 0x0007BD46 File Offset: 0x00079F46
		[DefaultValue(null)]
		[TemplateContainer(typeof(IDataItemContainer), BindingDirection.TwoWay)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		public virtual ITemplate InsertItemTemplate
		{
			get
			{
				return this.insertItemTemplate;
			}
			set
			{
				this.insertItemTemplate = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the template for displaying an item in a data-bound control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" />-implemented object that contains the template for displaying an item in a <see cref="T:System.Web.UI.WebControls.TemplateField" />. The default is null, which indicates that this property is not set.</returns>
		// Token: 0x17000F3B RID: 3899
		// (get) Token: 0x06002FC3 RID: 12227 RVA: 0x0007BD55 File Offset: 0x00079F55
		// (set) Token: 0x06002FC4 RID: 12228 RVA: 0x0007BD5D File Offset: 0x00079F5D
		[DefaultValue(null)]
		[TemplateContainer(typeof(IDataItemContainer), BindingDirection.TwoWay)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		public virtual ITemplate ItemTemplate
		{
			get
			{
				return this.itemTemplate;
			}
			set
			{
				this.itemTemplate = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Adds text or controls to a cell's controls collection.</summary>
		/// <param name="cell">A <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> that contains the text or controls of the <see cref="T:System.Web.UI.WebControls.DataControlField" />.</param>
		/// <param name="cellType">One of the <see cref="T:System.Web.UI.WebControls.DataControlCellType" /> values.</param>
		/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> values, specifying the state of the row that contains the <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" />.</param>
		/// <param name="rowIndex">The index of the row that the <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> is contained in.</param>
		// Token: 0x06002FC5 RID: 12229 RVA: 0x0007BD6C File Offset: 0x00079F6C
		public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
		{
			base.InitializeCell(cell, cellType, rowState, rowIndex);
			if (cellType == DataControlCellType.Header)
			{
				if (this.headerTemplate != null && this.ShowHeader)
				{
					cell.Text = string.Empty;
					this.headerTemplate.InstantiateIn(cell);
					return;
				}
			}
			else if (cellType == DataControlCellType.Footer)
			{
				if (this.footerTemplate != null)
				{
					cell.Text = string.Empty;
					this.footerTemplate.InstantiateIn(cell);
					return;
				}
			}
			else
			{
				cell.Text = string.Empty;
				if ((rowState & DataControlRowState.Insert) != DataControlRowState.Normal && this.insertItemTemplate != null)
				{
					this.insertItemTemplate.InstantiateIn(cell);
					return;
				}
				if ((rowState & DataControlRowState.Edit) != DataControlRowState.Normal && this.editItemTemplate != null)
				{
					this.editItemTemplate.InstantiateIn(cell);
					return;
				}
				if ((rowState & DataControlRowState.Alternate) != DataControlRowState.Normal && this.alternatingItemTemplate != null)
				{
					this.alternatingItemTemplate.InstantiateIn(cell);
					return;
				}
				if (this.itemTemplate != null)
				{
					this.itemTemplate.InstantiateIn(cell);
					return;
				}
				cell.Text = "&nbsp;";
			}
		}

		/// <summary>Extracts the value of the data control fields as specified by one or more two-way binding statements (DataBind) from the current table cell and adds the values to the specified <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> collection.</summary>
		/// <param name="dictionary">An <see cref="T:System.Collections.Specialized.IOrderedDictionary" />.</param>
		/// <param name="cell">A <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> that contains the text or controls of the <see cref="T:System.Web.UI.WebControls.TemplateField" />.</param>
		/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> values.</param>
		/// <param name="includeReadOnly">true to indicate that the values of read-only fields are included in the <paramref name="dictionary" /> collection; otherwise, false.</param>
		// Token: 0x06002FC6 RID: 12230 RVA: 0x0007BE58 File Offset: 0x0007A058
		public override void ExtractValuesFromCell(IOrderedDictionary dictionary, DataControlFieldCell cell, DataControlRowState rowState, bool includeReadOnly)
		{
			IBindableTemplate bindableTemplate;
			if ((rowState & DataControlRowState.Insert) != DataControlRowState.Normal)
			{
				bindableTemplate = this.insertItemTemplate as IBindableTemplate;
			}
			else if ((rowState & DataControlRowState.Edit) != DataControlRowState.Normal)
			{
				bindableTemplate = this.editItemTemplate as IBindableTemplate;
			}
			else if (this.alternatingItemTemplate != null && (rowState & DataControlRowState.Alternate) != DataControlRowState.Normal)
			{
				bindableTemplate = this.alternatingItemTemplate as IBindableTemplate;
			}
			else
			{
				bindableTemplate = this.itemTemplate as IBindableTemplate;
			}
			if (bindableTemplate != null)
			{
				IOrderedDictionary orderedDictionary = bindableTemplate.ExtractValues(cell);
				if (orderedDictionary == null)
				{
					return;
				}
				foreach (object obj in orderedDictionary)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					dictionary[dictionaryEntry.Key] = dictionaryEntry.Value;
				}
			}
		}

		/// <summary>Determines whether the controls contained in a <see cref="T:System.Web.UI.WebControls.TemplateField" /> object support page callbacks.</summary>
		/// <exception cref="T:System.NotSupportedException">The default implementation of this method is called. </exception>
		// Token: 0x06002FC7 RID: 12231 RVA: 0x0007BF18 File Offset: 0x0007A118
		public override void ValidateSupportsCallback()
		{
			throw new NotSupportedException("Callback not supported on TemplateField. Turn disable callbacks on '" + base.Control.ID + "'.");
		}

		/// <summary>Creates a new <see cref="T:System.Web.UI.WebControls.TemplateField" /> object.</summary>
		/// <returns>Always returns a new <see cref="T:System.Web.UI.WebControls.TemplateField" />.</returns>
		// Token: 0x06002FC8 RID: 12232 RVA: 0x0007BF39 File Offset: 0x0007A139
		protected override DataControlField CreateField()
		{
			return new TemplateField();
		}

		/// <summary>Copies the properties of the current <see cref="T:System.Web.UI.WebControls.TemplateField" />-derived object to the specified <see cref="T:System.Web.UI.WebControls.DataControlField" /> object.</summary>
		/// <param name="newField">The <see cref="T:System.Web.UI.WebControls.DataControlField" /> to copy the properties of the current <see cref="T:System.Web.UI.WebControls.TemplateField" /> to.</param>
		// Token: 0x06002FC9 RID: 12233 RVA: 0x0007BF40 File Offset: 0x0007A140
		protected override void CopyProperties(DataControlField newField)
		{
			base.CopyProperties(newField);
			TemplateField templateField = (TemplateField)newField;
			templateField.AlternatingItemTemplate = this.AlternatingItemTemplate;
			templateField.ConvertEmptyStringToNull = this.ConvertEmptyStringToNull;
			templateField.EditItemTemplate = this.EditItemTemplate;
			templateField.FooterTemplate = this.FooterTemplate;
			templateField.HeaderTemplate = this.HeaderTemplate;
			templateField.InsertItemTemplate = this.InsertItemTemplate;
			templateField.ItemTemplate = this.ItemTemplate;
		}

		/// <summary>Gets or sets a value that specifies whether the control validates client input.</summary>
		/// <returns>true if the control validates client input; otherwise, false.</returns>
		// Token: 0x17000F3C RID: 3900
		// (get) Token: 0x06002FCB RID: 12235 RVA: 0x0007BFB0 File Offset: 0x0007A1B0
		// (set) Token: 0x06002FCC RID: 12236 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public new ValidateRequestMode ValidateRequestMode
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return ValidateRequestMode.Inherit;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x04001BF5 RID: 7157
		private ITemplate alternatingItemTemplate;

		// Token: 0x04001BF6 RID: 7158
		private ITemplate editItemTemplate;

		// Token: 0x04001BF7 RID: 7159
		private ITemplate footerTemplate;

		// Token: 0x04001BF8 RID: 7160
		private ITemplate headerTemplate;

		// Token: 0x04001BF9 RID: 7161
		private ITemplate insertItemTemplate;

		// Token: 0x04001BFA RID: 7162
		private ITemplate itemTemplate;
	}
}

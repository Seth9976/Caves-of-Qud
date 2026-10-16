using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a column type for the <see cref="T:System.Web.UI.WebControls.DataGrid" /> control that allows you to customize the layout of controls in the column.</summary>
	// Token: 0x02000422 RID: 1058
	public class TemplateColumn : DataGridColumn
	{
		/// <summary>Calls a <see cref="T:System.Web.UI.WebControls.TableCell" /> object's base class to initialize the instance and then applies a <see cref="T:System.Web.UI.WebControls.ListItemType" /> to the cell.</summary>
		/// <param name="cell">A <see cref="T:System.Web.UI.WebControls.TableCell" /> object that represents the cell to reset.</param>
		/// <param name="columnIndex">The column number where the cell is located.</param>
		/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> values. </param>
		// Token: 0x06002FAD RID: 12205 RVA: 0x0007BBE8 File Offset: 0x00079DE8
		public override void InitializeCell(TableCell cell, int columnIndex, ListItemType itemType)
		{
			base.InitializeCell(cell, columnIndex, itemType);
			ITemplate template = null;
			switch (itemType)
			{
			case ListItemType.Header:
				template = this.HeaderTemplate;
				break;
			case ListItemType.Footer:
				template = this.FooterTemplate;
				break;
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
			case ListItemType.SelectedItem:
				template = this.ItemTemplate;
				if (template == null)
				{
					cell.Text = "&nbsp;";
				}
				break;
			case ListItemType.EditItem:
				template = this.EditItemTemplate;
				if (template == null)
				{
					template = this.ItemTemplate;
				}
				if (template == null)
				{
					cell.Text = "&nbsp;";
				}
				break;
			}
			if (template != null)
			{
				template.InstantiateIn(cell);
			}
		}

		/// <summary>Gets or sets the template for displaying the item selected for editing in a <see cref="T:System.Web.UI.WebControls.TemplateColumn" /> object.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" />-implemented object that contains the template for displaying the item being edited in the <see cref="T:System.Web.UI.WebControls.TemplateColumn" />. The default value is null, which indicates that this property is not set.</returns>
		// Token: 0x17000F31 RID: 3889
		// (get) Token: 0x06002FAE RID: 12206 RVA: 0x0007BC72 File Offset: 0x00079E72
		// (set) Token: 0x06002FAF RID: 12207 RVA: 0x0007BC7A File Offset: 0x00079E7A
		[DefaultValue(null)]
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(DataGridItem))]
		[WebSysDescription("")]
		public virtual ITemplate EditItemTemplate
		{
			get
			{
				return this.editItemTemplate;
			}
			set
			{
				this.editItemTemplate = value;
			}
		}

		/// <summary>Gets or sets the template for displaying the footer section of the <see cref="T:System.Web.UI.WebControls.TemplateColumn" /> object.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" />-implemented object that contains the template for displaying the footer section of the <see cref="T:System.Web.UI.WebControls.TemplateColumn" />. The default value is null, which indicates that this property is not set.</returns>
		// Token: 0x17000F32 RID: 3890
		// (get) Token: 0x06002FB0 RID: 12208 RVA: 0x0007BC83 File Offset: 0x00079E83
		// (set) Token: 0x06002FB1 RID: 12209 RVA: 0x0007BC8B File Offset: 0x00079E8B
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[Browsable(false)]
		[WebSysDescription("")]
		[TemplateContainer(typeof(DataGridItem))]
		public virtual ITemplate FooterTemplate
		{
			get
			{
				return this.footerTemplate;
			}
			set
			{
				this.footerTemplate = value;
			}
		}

		/// <summary>Gets or sets the template for displaying the heading section of the <see cref="T:System.Web.UI.WebControls.TemplateColumn" /> object.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" />-implemented object that contains the template for displaying the heading section of the <see cref="T:System.Web.UI.WebControls.TemplateColumn" />. The default value is null, which indicates that this property is not set.</returns>
		// Token: 0x17000F33 RID: 3891
		// (get) Token: 0x06002FB2 RID: 12210 RVA: 0x0007BC94 File Offset: 0x00079E94
		// (set) Token: 0x06002FB3 RID: 12211 RVA: 0x0007BC9C File Offset: 0x00079E9C
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("")]
		[DefaultValue(null)]
		[TemplateContainer(typeof(DataGridItem))]
		[Browsable(false)]
		public virtual ITemplate HeaderTemplate
		{
			get
			{
				return this.headerTemplate;
			}
			set
			{
				this.headerTemplate = value;
			}
		}

		/// <summary>Gets or sets the template for displaying a data item in a <see cref="T:System.Web.UI.WebControls.TemplateColumn" /> object.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" />-implemented object that contains the template for displaying a data item in the <see cref="T:System.Web.UI.WebControls.TemplateColumn" />. The default value is null, which indicates that this property is not set.</returns>
		// Token: 0x17000F34 RID: 3892
		// (get) Token: 0x06002FB4 RID: 12212 RVA: 0x0007BCA5 File Offset: 0x00079EA5
		// (set) Token: 0x06002FB5 RID: 12213 RVA: 0x0007BCAD File Offset: 0x00079EAD
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(DataGridItem))]
		[WebSysDescription("")]
		public virtual ITemplate ItemTemplate
		{
			get
			{
				return this.itemTemplate;
			}
			set
			{
				this.itemTemplate = value;
			}
		}

		// Token: 0x04001BF1 RID: 7153
		private ITemplate editItemTemplate;

		// Token: 0x04001BF2 RID: 7154
		private ITemplate footerTemplate;

		// Token: 0x04001BF3 RID: 7155
		private ITemplate headerTemplate;

		// Token: 0x04001BF4 RID: 7156
		private ITemplate itemTemplate;
	}
}

using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	/// <summary>A data bound list control that displays items using templates.</summary>
	// Token: 0x0200037D RID: 893
	[ControlValueProperty("SelectedValue")]
	[Editor("System.Web.UI.Design.WebControls.DataListComponentEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.ComponentEditor, System, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Designer("System.Web.UI.Design.WebControls.DataListDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class DataList : BaseDataList, INamingContainer, IRepeatInfoUser
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataList" /> class.</summary>
		// Token: 0x0600221A RID: 8730 RVA: 0x00055C86 File Offset: 0x00053E86
		public DataList()
		{
			this.idx = -1;
		}

		/// <summary>Gets the style properties for alternating items in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that represents the style properties for alternating items in the <see cref="T:System.Web.UI.WebControls.DataList" /> control. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object.</returns>
		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x0600221B RID: 8731 RVA: 0x00055C95 File Offset: 0x00053E95
		[WebSysDescription("")]
		[WebCategory("Style")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		public virtual TableItemStyle AlternatingItemStyle
		{
			get
			{
				if (this.alternatingItemStyle == null)
				{
					this.alternatingItemStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.alternatingItemStyle.TrackViewState();
					}
				}
				return this.alternatingItemStyle;
			}
		}

		/// <summary>Gets or sets the template for alternating items in the <see cref="T:System.Web.UI.WebControls.DataList" />.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> object that contains the template for alternating items in the <see cref="T:System.Web.UI.WebControls.DataList" /> control. The default value is null.</returns>
		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x0600221C RID: 8732 RVA: 0x00055CC3 File Offset: 0x00053EC3
		// (set) Token: 0x0600221D RID: 8733 RVA: 0x00055CCB File Offset: 0x00053ECB
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("")]
		[WebCategory("Style")]
		[Browsable(false)]
		[DefaultValue(null)]
		[TemplateContainer(typeof(DataListItem))]
		public virtual ITemplate AlternatingItemTemplate
		{
			get
			{
				return this.alternatingItemTemplate;
			}
			set
			{
				this.alternatingItemTemplate = value;
			}
		}

		/// <summary>Gets or sets the index number of the selected item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control to edit.</summary>
		/// <returns>The index number of the selected item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control to edit.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is less than 0.</exception>
		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x0600221E RID: 8734 RVA: 0x00055CD4 File Offset: 0x00053ED4
		// (set) Token: 0x0600221F RID: 8735 RVA: 0x00055CFD File Offset: 0x00053EFD
		[DefaultValue(-1)]
		[WebSysDescription("")]
		[WebCategory("Misc")]
		public virtual int EditItemIndex
		{
			get
			{
				object obj = this.ViewState["EditItemIndex"];
				if (obj != null)
				{
					return (int)obj;
				}
				return -1;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("EditItemIndex", "< -1");
				}
				this.ViewState["EditItemIndex"] = value;
			}
		}

		/// <summary>Gets the style properties for the item selected for editing in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that contains the style properties for the item selected for editing in the <see cref="T:System.Web.UI.WebControls.DataList" /> control. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object.</returns>
		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x06002220 RID: 8736 RVA: 0x00055D29 File Offset: 0x00053F29
		[WebCategory("Style")]
		[WebSysDescription("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		public virtual TableItemStyle EditItemStyle
		{
			get
			{
				if (this.editItemStyle == null)
				{
					this.editItemStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.editItemStyle.TrackViewState();
					}
				}
				return this.editItemStyle;
			}
		}

		/// <summary>Gets or sets the template for the item selected for editing in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> object that contains the template for the item selected for editing in the <see cref="T:System.Web.UI.WebControls.DataList" /> control. The default value is null.</returns>
		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x06002221 RID: 8737 RVA: 0x00055D57 File Offset: 0x00053F57
		// (set) Token: 0x06002222 RID: 8738 RVA: 0x00055D5F File Offset: 0x00053F5F
		[Browsable(false)]
		[DefaultValue(null)]
		[TemplateContainer(typeof(DataListItem))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("")]
		[WebCategory("Style")]
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

		/// <summary>Gets or sets a value that indicates whether the rows of a <see cref="T:System.Web.UI.WebControls.Table" /> control, defined in each template of a <see cref="T:System.Web.UI.WebControls.DataList" /> control, are extracted and displayed.</summary>
		/// <returns>true if the rows of a <see cref="T:System.Web.UI.WebControls.Table" /> control, defined in each template of a <see cref="T:System.Web.UI.WebControls.DataList" /> control, are extracted and displayed; otherwise, false. The default value is false.</returns>
		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x06002223 RID: 8739 RVA: 0x00055D68 File Offset: 0x00053F68
		// (set) Token: 0x06002224 RID: 8740 RVA: 0x00055D91 File Offset: 0x00053F91
		[DefaultValue(false)]
		[WebSysDescription("")]
		[WebCategory("Misc")]
		public virtual bool ExtractTemplateRows
		{
			get
			{
				object obj = this.ViewState["ExtractTemplateRows"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["ExtractTemplateRows"] = value;
			}
		}

		/// <summary>Gets the style properties for the footer section of the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that contains the style properties for the footer section of the <see cref="T:System.Web.UI.WebControls.DataList" /> control. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object.</returns>
		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x06002225 RID: 8741 RVA: 0x00055DA9 File Offset: 0x00053FA9
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("")]
		[WebCategory("Style")]
		[DefaultValue(null)]
		public virtual TableItemStyle FooterStyle
		{
			get
			{
				if (this.footerStyle == null)
				{
					this.footerStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.footerStyle.TrackViewState();
					}
				}
				return this.footerStyle;
			}
		}

		/// <summary>Gets or sets the template for the footer section of the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> object that contains the template for the footer section of the <see cref="T:System.Web.UI.WebControls.DataList" /> control. The default value is null.</returns>
		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x06002226 RID: 8742 RVA: 0x00055DD7 File Offset: 0x00053FD7
		// (set) Token: 0x06002227 RID: 8743 RVA: 0x00055DDF File Offset: 0x00053FDF
		[WebCategory("Style")]
		[WebSysDescription("")]
		[TemplateContainer(typeof(DataListItem))]
		[DefaultValue(null)]
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
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

		/// <summary>Gets or sets the grid line style for the <see cref="T:System.Web.UI.WebControls.DataList" /> control when the <see cref="P:System.Web.UI.WebControls.DataList.RepeatLayout" /> property is set to RepeatLayout.Table.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.GridLines" /> enumeration values. The default value is None.</returns>
		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x06002228 RID: 8744 RVA: 0x00055DE8 File Offset: 0x00053FE8
		// (set) Token: 0x06002229 RID: 8745 RVA: 0x00055DFF File Offset: 0x00053FFF
		[DefaultValue(GridLines.None)]
		public override GridLines GridLines
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return GridLines.None;
				}
				return this.TableStyle.GridLines;
			}
			set
			{
				this.TableStyle.GridLines = value;
			}
		}

		/// <summary>Gets the style properties for the heading section of the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that contains the style properties for the heading section of the <see cref="T:System.Web.UI.WebControls.DataList" /> control. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object.</returns>
		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x0600222A RID: 8746 RVA: 0x00055E0D File Offset: 0x0005400D
		[WebCategory("Style")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[WebSysDescription("")]
		[DefaultValue(null)]
		public virtual TableItemStyle HeaderStyle
		{
			get
			{
				if (this.headerStyle == null)
				{
					this.headerStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.headerStyle.TrackViewState();
					}
				}
				return this.headerStyle;
			}
		}

		/// <summary>Gets or sets the template for the heading section of the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> that contains the template for the heading section of the <see cref="T:System.Web.UI.WebControls.DataList" /> control. The default value is null.</returns>
		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x0600222B RID: 8747 RVA: 0x00055E3B File Offset: 0x0005403B
		// (set) Token: 0x0600222C RID: 8748 RVA: 0x00055E43 File Offset: 0x00054043
		[Browsable(false)]
		[DefaultValue(null)]
		[TemplateContainer(typeof(DataListItem))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("")]
		[WebCategory("Style")]
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

		/// <summary>Gets a collection of <see cref="T:System.Web.UI.WebControls.DataListItem" /> objects representing the individual items within the control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.DataListItemCollection" /> that contains a collection of <see cref="T:System.Web.UI.WebControls.DataListItem" /> objects representing the individual items within the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</returns>
		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x0600222D RID: 8749 RVA: 0x00055E4C File Offset: 0x0005404C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebCategory("Style")]
		[WebSysDescription("")]
		[Browsable(false)]
		public virtual DataListItemCollection Items
		{
			get
			{
				if (this.items == null)
				{
					this.items = new DataListItemCollection(this.ItemList);
				}
				return this.items;
			}
		}

		/// <summary>Gets the style properties for the items in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that contains the style properties for the items in the <see cref="T:System.Web.UI.WebControls.DataList" /> control. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object.</returns>
		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x0600222E RID: 8750 RVA: 0x00055E6D File Offset: 0x0005406D
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		[WebCategory("Style")]
		[WebSysDescription("")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual TableItemStyle ItemStyle
		{
			get
			{
				if (this.itemStyle == null)
				{
					this.itemStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.itemStyle.TrackViewState();
					}
				}
				return this.itemStyle;
			}
		}

		/// <summary>Gets or sets the template for the items in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> that contains the template for the items in the <see cref="T:System.Web.UI.WebControls.DataList" /> control. The default value is null.</returns>
		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x0600222F RID: 8751 RVA: 0x00055E9B File Offset: 0x0005409B
		// (set) Token: 0x06002230 RID: 8752 RVA: 0x00055EA3 File Offset: 0x000540A3
		[TemplateContainer(typeof(DataListItem))]
		[DefaultValue(null)]
		[Browsable(false)]
		[WebSysDescription("")]
		[WebCategory("Style")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
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

		/// <summary>Gets or sets the number of columns to display in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		/// <returns>The number of columns to display in the <see cref="T:System.Web.UI.WebControls.DataList" /> control. The default value is 0, which indicates that the items in the <see cref="T:System.Web.UI.WebControls.DataList" /> control are displayed in a single row or column, based on the value of the <see cref="P:System.Web.UI.WebControls.DataList.RepeatDirection" /> property.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified number of columns is a negative value. </exception>
		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x06002231 RID: 8753 RVA: 0x00055EAC File Offset: 0x000540AC
		// (set) Token: 0x06002232 RID: 8754 RVA: 0x00055ED5 File Offset: 0x000540D5
		[DefaultValue(0)]
		[WebSysDescription("")]
		[WebCategory("Layout")]
		public virtual int RepeatColumns
		{
			get
			{
				object obj = this.ViewState["RepeatColumns"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value", "RepeatColumns value has to be 0 for 'not set' or > 0.");
				}
				this.ViewState["RepeatColumns"] = value;
			}
		}

		/// <summary>Gets or sets whether the <see cref="T:System.Web.UI.WebControls.DataList" /> control displays vertically or horizontally.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.RepeatDirection" /> values. The default is Vertical.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is not one of the <see cref="T:System.Web.UI.WebControls.RepeatDirection" /> values. </exception>
		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x06002233 RID: 8755 RVA: 0x00055F04 File Offset: 0x00054104
		// (set) Token: 0x06002234 RID: 8756 RVA: 0x00055F2D File Offset: 0x0005412D
		[DefaultValue(RepeatDirection.Vertical)]
		[WebSysDescription("")]
		[WebCategory("Layout")]
		public virtual RepeatDirection RepeatDirection
		{
			get
			{
				object obj = this.ViewState["RepeatDirection"];
				if (obj != null)
				{
					return (RepeatDirection)obj;
				}
				return RepeatDirection.Vertical;
			}
			set
			{
				this.ViewState["RepeatDirection"] = value;
			}
		}

		/// <summary>Gets or sets whether the control is displayed in a table or flow layout.</summary>
		/// <returns>A value that specifies whether the control is displayed in a table or in flow layout.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is not one of the supported <see cref="T:System.Web.UI.WebControls.RepeatLayout" /> values.</exception>
		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x06002235 RID: 8757 RVA: 0x00055F48 File Offset: 0x00054148
		// (set) Token: 0x06002236 RID: 8758 RVA: 0x00055F71 File Offset: 0x00054171
		[WebCategory("Layout")]
		[DefaultValue(RepeatLayout.Table)]
		[WebSysDescription("")]
		public virtual RepeatLayout RepeatLayout
		{
			get
			{
				object obj = this.ViewState["RepeatLayout"];
				if (obj != null)
				{
					return (RepeatLayout)obj;
				}
				return RepeatLayout.Table;
			}
			set
			{
				if (value == RepeatLayout.OrderedList || value == RepeatLayout.UnorderedList)
				{
					throw new ArgumentOutOfRangeException(string.Format("DataList does not support the '{0}' layout.", value));
				}
				this.ViewState["RepeatLayout"] = value;
			}
		}

		/// <summary>Gets or sets the index of the selected item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		/// <returns>The index of the selected item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is less than -1.</exception>
		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x06002237 RID: 8759 RVA: 0x00055FA8 File Offset: 0x000541A8
		// (set) Token: 0x06002238 RID: 8760 RVA: 0x00055FD1 File Offset: 0x000541D1
		[WebCategory("Layout")]
		[WebSysDescription("")]
		[DefaultValue(-1)]
		[Bindable(true)]
		public virtual int SelectedIndex
		{
			get
			{
				object obj = this.ViewState["SelectedIndex"];
				if (obj != null)
				{
					return (int)obj;
				}
				return -1;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("SelectedIndex", "< -1");
				}
				this.ViewState["SelectedIndex"] = value;
			}
		}

		/// <summary>Gets the selected item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.DataListItem" /> that represents the item selected in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</returns>
		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x06002239 RID: 8761 RVA: 0x00056000 File Offset: 0x00054200
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[WebCategory("Layout")]
		[WebSysDescription("")]
		public virtual DataListItem SelectedItem
		{
			get
			{
				if (this.SelectedIndex < 0)
				{
					return null;
				}
				if (this.SelectedIndex >= this.Items.Count)
				{
					throw new ArgumentOutOfRangeException("SelectedItem", ">= Items.Count");
				}
				return this.items[this.SelectedIndex];
			}
		}

		/// <summary>Gets the style properties for the selected item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that contains the style properties for the selected item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object.</returns>
		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x0600223A RID: 8762 RVA: 0x0005604C File Offset: 0x0005424C
		[WebCategory("Style")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		[WebSysDescription("")]
		public virtual TableItemStyle SelectedItemStyle
		{
			get
			{
				if (this.selectedItemStyle == null)
				{
					this.selectedItemStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.selectedItemStyle.TrackViewState();
					}
				}
				return this.selectedItemStyle;
			}
		}

		/// <summary>Gets or sets the template for the selected item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> that contains the template for the selected item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control. The default value is null.</returns>
		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x0600223B RID: 8763 RVA: 0x0005607A File Offset: 0x0005427A
		// (set) Token: 0x0600223C RID: 8764 RVA: 0x00056082 File Offset: 0x00054282
		[WebCategory("Style")]
		[DefaultValue(null)]
		[TemplateContainer(typeof(DataListItem))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		[WebSysDescription("")]
		public virtual ITemplate SelectedItemTemplate
		{
			get
			{
				return this.selectedItemTemplate;
			}
			set
			{
				this.selectedItemTemplate = value;
			}
		}

		/// <summary>Gets the style properties of the separator between each item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that contains the style properties of the separator between each item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control. The default value is an empty <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object.</returns>
		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x0600223D RID: 8765 RVA: 0x0005608B File Offset: 0x0005428B
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("")]
		[WebCategory("Style")]
		public virtual TableItemStyle SeparatorStyle
		{
			get
			{
				if (this.separatorStyle == null)
				{
					this.separatorStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.separatorStyle.TrackViewState();
					}
				}
				return this.separatorStyle;
			}
		}

		/// <summary>Gets or sets the template for the separator between the items of the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> that contains the template for the separator between items in the <see cref="T:System.Web.UI.WebControls.DataList" /> control. The default value is null.</returns>
		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x0600223E RID: 8766 RVA: 0x000560B9 File Offset: 0x000542B9
		// (set) Token: 0x0600223F RID: 8767 RVA: 0x000560C1 File Offset: 0x000542C1
		[WebCategory("Style")]
		[Browsable(false)]
		[DefaultValue(null)]
		[TemplateContainer(typeof(DataListItem))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("")]
		public virtual ITemplate SeparatorTemplate
		{
			get
			{
				return this.separatorTemplate;
			}
			set
			{
				this.separatorTemplate = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the footer section is displayed in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		/// <returns>true if the footer section is displayed; otherwise, false. The default value is true, however this property is only examined when the <see cref="P:System.Web.UI.WebControls.DataList.FooterTemplate" /> property is not null.</returns>
		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x06002240 RID: 8768 RVA: 0x000560CC File Offset: 0x000542CC
		// (set) Token: 0x06002241 RID: 8769 RVA: 0x000532E1 File Offset: 0x000514E1
		[WebCategory("Appearance")]
		[WebSysDescription("")]
		[DefaultValue(true)]
		public virtual bool ShowFooter
		{
			get
			{
				object obj = this.ViewState["ShowFooter"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["ShowFooter"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the header section is displayed in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		/// <returns>true if the header is displayed; otherwise, false. The default value is true, however this property is only examined when the <see cref="P:System.Web.UI.WebControls.DataList.HeaderTemplate" /> property is not null.</returns>
		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x06002242 RID: 8770 RVA: 0x000560F8 File Offset: 0x000542F8
		// (set) Token: 0x06002243 RID: 8771 RVA: 0x0005330C File Offset: 0x0005150C
		[DefaultValue(true)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public virtual bool ShowHeader
		{
			get
			{
				object obj = this.ViewState["ShowHeader"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["ShowHeader"] = value;
			}
		}

		/// <summary>Gets the value of the key field for the selected data list item.</summary>
		/// <returns>The key field value for the selected data list item. The default is null, which indicates that no data list item is currently selected.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.UI.WebControls.BaseDataList.DataKeyField" /> property has not been set.</exception>
		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x06002244 RID: 8772 RVA: 0x00056124 File Offset: 0x00054324
		[global::System.MonoTODO("incomplete")]
		[Browsable(false)]
		public object SelectedValue
		{
			get
			{
				if (this.DataKeyField.Length == 0)
				{
					throw new InvalidOperationException(global::Locale.GetText("No DataKeyField present."));
				}
				int selectedIndex = this.SelectedIndex;
				if (selectedIndex >= 0 && selectedIndex < base.DataKeys.Count)
				{
					return base.DataKeys[selectedIndex];
				}
				return null;
			}
		}

		/// <summary>Gets the HTML tag that is used to render the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		/// <returns>Returns the <see cref="F:System.Web.UI.HtmlTextWriterTag.Table" /> tag if the <see cref="P:System.Web.UI.WebControls.DataList.RepeatLayout" /> is set to <see cref="F:System.Web.UI.WebControls.RepeatLayout.Table" />; otherwise, returns the <see cref="F:System.Web.UI.HtmlTextWriterTag.Span" /> tag. The default is <see cref="F:System.Web.UI.WebControls.RepeatLayout.Table" />.</returns>
		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x06002245 RID: 8773 RVA: 0x0004A624 File Offset: 0x00048824
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Table;
			}
		}

		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x06002246 RID: 8774 RVA: 0x00045292 File Offset: 0x00043492
		private TableStyle TableStyle
		{
			get
			{
				return (TableStyle)base.ControlStyle;
			}
		}

		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x06002247 RID: 8775 RVA: 0x00056175 File Offset: 0x00054375
		private ArrayList ItemList
		{
			get
			{
				if (this.list == null)
				{
					this.list = new ArrayList();
				}
				return this.list;
			}
		}

		// Token: 0x06002248 RID: 8776 RVA: 0x00056190 File Offset: 0x00054390
		private void DoItem(int i, ListItemType t, object d, bool databind)
		{
			DataListItem dataListItem = this.CreateItem(i, t);
			if (databind)
			{
				dataListItem.DataItem = d;
			}
			DataListItemEventArgs e = new DataListItemEventArgs(dataListItem);
			this.InitializeItem(dataListItem);
			this.Controls.Add(dataListItem);
			if (i != -1)
			{
				this.ItemList.Add(dataListItem);
			}
			this.OnItemCreated(e);
			if (databind)
			{
				dataListItem.DataBind();
				this.OnItemDataBound(e);
				dataListItem.DataItem = null;
			}
		}

		// Token: 0x06002249 RID: 8777 RVA: 0x000561FB File Offset: 0x000543FB
		private void DoItemInLoop(int i, object d, bool databind, ListItemType type)
		{
			this.DoItem(i, type, d, databind);
			if (this.SeparatorTemplate != null)
			{
				this.DoItem(i, ListItemType.Separator, null, databind);
			}
		}

		/// <summary>Creates the control hierarchy that is used to render the data list control, with or without the specified data source.</summary>
		/// <param name="useDataSource">true to use the control's data source; false to indicate that the control is being recreated from view state and should not be data-bound.</param>
		// Token: 0x0600224A RID: 8778 RVA: 0x0005621C File Offset: 0x0005441C
		protected override void CreateControlHierarchy(bool useDataSource)
		{
			this.Controls.Clear();
			this.ItemList.Clear();
			IEnumerable enumerable = null;
			ArrayList arrayList = null;
			if (useDataSource)
			{
				this.idx = 0;
				if (base.IsBoundUsingDataSourceID)
				{
					enumerable = this.GetData();
				}
				else
				{
					enumerable = DataSourceResolver.ResolveDataSource(this.DataSource, base.DataMember);
				}
				arrayList = base.DataKeysArray;
				arrayList.Clear();
			}
			else
			{
				this.idx = (int)this.ViewState["Items"];
			}
			if (enumerable == null && this.idx == 0)
			{
				return;
			}
			if (this.headerTemplate != null)
			{
				this.DoItem(-1, ListItemType.Header, null, useDataSource);
			}
			int selectedIndex = this.SelectedIndex;
			int editItemIndex = this.EditItemIndex;
			if (enumerable != null)
			{
				string dataKeyField = this.DataKeyField;
				using (IEnumerator enumerator = enumerable.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						if (useDataSource && !string.IsNullOrEmpty(dataKeyField))
						{
							arrayList.Add(DataBinder.GetPropertyValue(obj, dataKeyField));
						}
						ListItemType listItemType = ListItemType.Item;
						if (this.idx == editItemIndex)
						{
							listItemType = ListItemType.EditItem;
						}
						else if (this.idx == selectedIndex)
						{
							listItemType = ListItemType.SelectedItem;
						}
						else if ((this.idx & 1) != 0)
						{
							listItemType = ListItemType.AlternatingItem;
						}
						this.DoItemInLoop(this.idx, obj, useDataSource, listItemType);
						this.idx++;
					}
					goto IL_0186;
				}
			}
			for (int i = 0; i < this.idx; i++)
			{
				ListItemType listItemType = ListItemType.Item;
				if (i == editItemIndex)
				{
					listItemType = ListItemType.EditItem;
				}
				else if (i == selectedIndex)
				{
					listItemType = ListItemType.SelectedItem;
				}
				else if ((i & 1) != 0)
				{
					listItemType = ListItemType.AlternatingItem;
				}
				this.DoItemInLoop(i, null, useDataSource, listItemType);
			}
			IL_0186:
			if (this.footerTemplate != null)
			{
				this.DoItem(-1, ListItemType.Footer, null, useDataSource);
			}
			this.ViewState["Items"] = this.idx;
		}

		/// <summary>Creates the default style object that is used internally by the <see cref="T:System.Web.UI.WebControls.DataList" /> control to implement all style related properties.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableStyle" /> that contains the default style properties for the control.</returns>
		// Token: 0x0600224B RID: 8779 RVA: 0x000563EC File Offset: 0x000545EC
		protected override Style CreateControlStyle()
		{
			return new TableStyle
			{
				CellSpacing = 0
			};
		}

		/// <summary>Creates a <see cref="T:System.Web.UI.WebControls.DataListItem" /> object.</summary>
		/// <returns>A new <see cref="T:System.Web.UI.WebControls.DataListItem" /> created with the specified list-item type.</returns>
		/// <param name="itemIndex">The specified location within the <see cref="T:System.Web.UI.WebControls.DataList" /> to place the created item.</param>
		/// <param name="itemType">A <see cref="T:System.Web.UI.WebControls.ListItemType" /> that represents the specified type of the item to create.</param>
		// Token: 0x0600224C RID: 8780 RVA: 0x000563FA File Offset: 0x000545FA
		protected virtual DataListItem CreateItem(int itemIndex, ListItemType itemType)
		{
			return new DataListItem(itemIndex, itemType);
		}

		/// <summary>Initializes a <see cref="T:System.Web.UI.WebControls.DataListItem" /> object based on the specified templates and styles for the list-item type.</summary>
		/// <param name="item">The <see cref="T:System.Web.UI.WebControls.DataListItem" /> to initialize.</param>
		// Token: 0x0600224D RID: 8781 RVA: 0x00056404 File Offset: 0x00054604
		protected virtual void InitializeItem(DataListItem item)
		{
			ITemplate template = null;
			switch (item.ItemType)
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
			case ListItemType.EditItem:
				if (item.ItemType == ListItemType.EditItem && this.EditItemTemplate != null)
				{
					template = this.EditItemTemplate;
				}
				else if (item.ItemType == ListItemType.SelectedItem && this.SelectedItemTemplate != null)
				{
					template = this.SelectedItemTemplate;
				}
				else if (item.ItemType == ListItemType.AlternatingItem && this.AlternatingItemTemplate != null)
				{
					template = this.AlternatingItemTemplate;
				}
				else
				{
					template = this.ItemTemplate;
				}
				break;
			case ListItemType.Separator:
				template = this.SeparatorTemplate;
				break;
			}
			if (template != null)
			{
				template.InstantiateIn(item);
			}
		}

		/// <summary>Loads the previously saved view state of the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		/// <param name="savedState">An object that represents the state of the <see cref="T:System.Web.UI.WebControls.DataList" />.</param>
		// Token: 0x0600224E RID: 8782 RVA: 0x000564B8 File Offset: 0x000546B8
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			base.LoadViewState(array[0]);
			if (array[1] != null)
			{
				this.ItemStyle.LoadViewState(array[1]);
			}
			if (array[2] != null)
			{
				this.SelectedItemStyle.LoadViewState(array[2]);
			}
			if (array[3] != null)
			{
				this.AlternatingItemStyle.LoadViewState(array[3]);
			}
			if (array[4] != null)
			{
				this.EditItemStyle.LoadViewState(array[4]);
			}
			if (array[5] != null)
			{
				this.SeparatorStyle.LoadViewState(array[5]);
			}
			if (array[6] != null)
			{
				this.HeaderStyle.LoadViewState(array[6]);
			}
			if (array[7] != null)
			{
				this.FooterStyle.LoadViewState(array[7]);
			}
			if (array[8] != null)
			{
				base.ControlStyle.LoadViewState(array[8]);
			}
		}

		/// <returns>true if the event has been canceled; otherwise, false. The default is false.</returns>
		/// <param name="source">The source of the event.</param>
		/// <param name="e">The event data.</param>
		// Token: 0x0600224F RID: 8783 RVA: 0x00056570 File Offset: 0x00054770
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			DataListCommandEventArgs e2 = e as DataListCommandEventArgs;
			if (e2 == null)
			{
				return false;
			}
			string commandName = e2.CommandName;
			CultureInfo invariantCulture = Helpers.InvariantCulture;
			this.OnItemCommand(e2);
			if (string.Compare(commandName, "Cancel", true, invariantCulture) == 0)
			{
				this.OnCancelCommand(e2);
			}
			else if (string.Compare(commandName, "Delete", true, invariantCulture) == 0)
			{
				this.OnDeleteCommand(e2);
			}
			else if (string.Compare(commandName, "Edit", true, invariantCulture) == 0)
			{
				this.OnEditCommand(e2);
			}
			else if (string.Compare(commandName, "Select", true, invariantCulture) == 0)
			{
				this.SelectedIndex = e2.Item.ItemIndex;
				this.OnSelectedIndexChanged(e2);
			}
			else if (string.Compare(commandName, "Update", true, invariantCulture) == 0)
			{
				this.OnUpdateCommand(e2);
			}
			return true;
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DataList.CancelCommand" /> event. This allows you to provide a custom handler for the event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.DataListCommandEventArgs" /> that contains event data. </param>
		// Token: 0x06002250 RID: 8784 RVA: 0x00056628 File Offset: 0x00054828
		protected virtual void OnCancelCommand(DataListCommandEventArgs e)
		{
			DataListCommandEventHandler dataListCommandEventHandler = (DataListCommandEventHandler)base.Events[DataList.cancelCommandEvent];
			if (dataListCommandEventHandler != null)
			{
				dataListCommandEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DataList.DeleteCommand" /> event. This allows you to provide a custom handler for the event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.DataListCommandEventArgs" /> that contains event data. </param>
		// Token: 0x06002251 RID: 8785 RVA: 0x00056658 File Offset: 0x00054858
		protected virtual void OnDeleteCommand(DataListCommandEventArgs e)
		{
			DataListCommandEventHandler dataListCommandEventHandler = (DataListCommandEventHandler)base.Events[DataList.deleteCommandEvent];
			if (dataListCommandEventHandler != null)
			{
				dataListCommandEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DataList.EditCommand" /> event. This allows you to provide a custom handler for the event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.DataListCommandEventArgs" /> that contains event data. </param>
		// Token: 0x06002252 RID: 8786 RVA: 0x00056688 File Offset: 0x00054888
		protected virtual void OnEditCommand(DataListCommandEventArgs e)
		{
			DataListCommandEventHandler dataListCommandEventHandler = (DataListCommandEventHandler)base.Events[DataList.editCommandEvent];
			if (dataListCommandEventHandler != null)
			{
				dataListCommandEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.Init" /> event for the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002253 RID: 8787 RVA: 0x000566B8 File Offset: 0x000548B8
		protected internal override void OnInit(EventArgs e)
		{
			Page page = this.Page;
			if (page != null)
			{
				page.RegisterRequiresControlState(this);
			}
			base.OnInit(e);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DataList.ItemCommand" /> event. This allows you to provide a custom handler for the event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.DataListCommandEventArgs" /> that contains event data. </param>
		// Token: 0x06002254 RID: 8788 RVA: 0x000566E0 File Offset: 0x000548E0
		protected virtual void OnItemCommand(DataListCommandEventArgs e)
		{
			DataListCommandEventHandler dataListCommandEventHandler = (DataListCommandEventHandler)base.Events[DataList.itemCommandEvent];
			if (dataListCommandEventHandler != null)
			{
				dataListCommandEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DataList.ItemCreated" /> event. This allows you to provide a custom handler for the event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.DataListItemEventArgs" /> that contains event data. </param>
		// Token: 0x06002255 RID: 8789 RVA: 0x00056710 File Offset: 0x00054910
		protected virtual void OnItemCreated(DataListItemEventArgs e)
		{
			DataListItemEventHandler dataListItemEventHandler = (DataListItemEventHandler)base.Events[DataList.itemCreatedEvent];
			if (dataListItemEventHandler != null)
			{
				dataListItemEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DataList.ItemDataBound" /> event. This allows you to provide a custom handler for the event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.DataListItemEventArgs" /> that contains event data. </param>
		// Token: 0x06002256 RID: 8790 RVA: 0x00056740 File Offset: 0x00054940
		protected virtual void OnItemDataBound(DataListItemEventArgs e)
		{
			DataListItemEventHandler dataListItemEventHandler = (DataListItemEventHandler)base.Events[DataList.itemDataBoundEvent];
			if (dataListItemEventHandler != null)
			{
				dataListItemEventHandler(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.DataList.UpdateCommand" /> event. This allows you to provide a custom handler for the event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.DataListItemEventArgs" /> that contains event data. </param>
		// Token: 0x06002257 RID: 8791 RVA: 0x00056770 File Offset: 0x00054970
		protected virtual void OnUpdateCommand(DataListCommandEventArgs e)
		{
			DataListCommandEventHandler dataListCommandEventHandler = (DataListCommandEventHandler)base.Events[DataList.updateCommandEvent];
			if (dataListCommandEventHandler != null)
			{
				dataListCommandEventHandler(this, e);
			}
		}

		/// <summary>Prepares the control hierarchy for rendering in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		// Token: 0x06002258 RID: 8792 RVA: 0x000567A0 File Offset: 0x000549A0
		protected override void PrepareControlHierarchy()
		{
			if (!this.HasControls() || this.Controls.Count == 0)
			{
				return;
			}
			Style style = null;
			foreach (object obj in this.Controls)
			{
				DataListItem dataListItem = (DataListItem)obj;
				switch (dataListItem.ItemType)
				{
				case ListItemType.Header:
					if (!this.ShowHeader)
					{
						dataListItem.Visible = false;
					}
					else if (this.headerStyle != null)
					{
						dataListItem.MergeStyle(this.headerStyle);
					}
					break;
				case ListItemType.Footer:
					if (!this.ShowFooter)
					{
						dataListItem.Visible = false;
					}
					else if (this.footerStyle != null)
					{
						dataListItem.MergeStyle(this.footerStyle);
					}
					break;
				case ListItemType.Item:
					dataListItem.MergeStyle(this.itemStyle);
					break;
				case ListItemType.AlternatingItem:
					if (style == null)
					{
						if (this.alternatingItemStyle != null)
						{
							style = new TableItemStyle();
							style.CopyFrom(this.itemStyle);
							style.CopyFrom(this.alternatingItemStyle);
						}
						else
						{
							style = this.itemStyle;
						}
					}
					dataListItem.MergeStyle(style);
					break;
				case ListItemType.SelectedItem:
					if (this.selectedItemStyle != null)
					{
						dataListItem.MergeStyle(this.selectedItemStyle);
					}
					else
					{
						dataListItem.MergeStyle(this.itemStyle);
					}
					break;
				case ListItemType.EditItem:
					if (this.editItemStyle != null)
					{
						dataListItem.MergeStyle(this.editItemStyle);
					}
					else
					{
						dataListItem.MergeStyle(this.itemStyle);
					}
					break;
				case ListItemType.Separator:
					if (this.separatorStyle != null)
					{
						dataListItem.MergeStyle(this.separatorStyle);
					}
					else
					{
						dataListItem.MergeStyle(this.itemStyle);
					}
					break;
				}
			}
		}

		/// <summary>Renders the list items in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
		// Token: 0x06002259 RID: 8793 RVA: 0x00056960 File Offset: 0x00054B60
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
			if (this.Items.Count == 0)
			{
				return;
			}
			RepeatInfo repeatInfo = new RepeatInfo();
			repeatInfo.RepeatColumns = this.RepeatColumns;
			repeatInfo.RepeatDirection = this.RepeatDirection;
			repeatInfo.RepeatLayout = this.RepeatLayout;
			repeatInfo.CaptionAlign = this.CaptionAlign;
			repeatInfo.Caption = this.Caption;
			repeatInfo.UseAccessibleHeader = this.UseAccessibleHeader;
			if (this.ExtractTemplateRows)
			{
				repeatInfo.OuterTableImplied = true;
				writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
				if (base.ControlStyleCreated)
				{
					base.ControlStyle.AddAttributesToRender(writer);
				}
				writer.RenderBeginTag(HtmlTextWriterTag.Table);
				repeatInfo.RenderRepeater(writer, this, base.ControlStyle, this);
				writer.RenderEndTag();
				return;
			}
			repeatInfo.RenderRepeater(writer, this, base.ControlStyle, this);
		}

		/// <summary>Saves the changes to the control view state since the time the page was posted back to the server.</summary>
		/// <returns>The object that contains the changes to the <see cref="T:System.Web.UI.WebControls.DataList" /> view state. </returns>
		// Token: 0x0600225A RID: 8794 RVA: 0x00056A28 File Offset: 0x00054C28
		protected override object SaveViewState()
		{
			object[] array = new object[9];
			array[0] = base.SaveViewState();
			if (this.itemStyle != null)
			{
				array[1] = this.itemStyle.SaveViewState();
			}
			if (this.selectedItemStyle != null)
			{
				array[2] = this.selectedItemStyle.SaveViewState();
			}
			if (this.alternatingItemStyle != null)
			{
				array[3] = this.alternatingItemStyle.SaveViewState();
			}
			if (this.editItemStyle != null)
			{
				array[4] = this.editItemStyle.SaveViewState();
			}
			if (this.separatorStyle != null)
			{
				array[5] = this.separatorStyle.SaveViewState();
			}
			if (this.headerStyle != null)
			{
				array[6] = this.headerStyle.SaveViewState();
			}
			if (this.footerStyle != null)
			{
				array[7] = this.footerStyle.SaveViewState();
			}
			if (base.ControlStyleCreated)
			{
				array[8] = base.ControlStyle.SaveViewState();
			}
			return array;
		}

		/// <summary>Tracks view-state changes to the <see cref="T:System.Web.UI.WebControls.DataList" /> control so they can be stored in the control's <see cref="P:System.Web.UI.Control.ViewState" /> property.</summary>
		// Token: 0x0600225B RID: 8795 RVA: 0x00056AF8 File Offset: 0x00054CF8
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this.alternatingItemStyle != null)
			{
				this.alternatingItemStyle.TrackViewState();
			}
			if (this.editItemStyle != null)
			{
				this.editItemStyle.TrackViewState();
			}
			if (this.footerStyle != null)
			{
				this.footerStyle.TrackViewState();
			}
			if (this.headerStyle != null)
			{
				this.headerStyle.TrackViewState();
			}
			if (this.itemStyle != null)
			{
				this.itemStyle.TrackViewState();
			}
			if (this.selectedItemStyle != null)
			{
				this.selectedItemStyle.TrackViewState();
			}
			if (this.separatorStyle != null)
			{
				this.separatorStyle.TrackViewState();
			}
			if (base.ControlStyleCreated)
			{
				base.ControlStyle.TrackViewState();
			}
		}

		/// <summary>Occurs when the Cancel button is clicked for an item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		// Token: 0x14000071 RID: 113
		// (add) Token: 0x0600225C RID: 8796 RVA: 0x00056BA3 File Offset: 0x00054DA3
		// (remove) Token: 0x0600225D RID: 8797 RVA: 0x00056BB6 File Offset: 0x00054DB6
		[WebCategory("Action")]
		[WebSysDescription("")]
		public event DataListCommandEventHandler CancelCommand
		{
			add
			{
				base.Events.AddHandler(DataList.cancelCommandEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataList.cancelCommandEvent, value);
			}
		}

		/// <summary>Occurs when the Delete button is clicked for an item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		// Token: 0x14000072 RID: 114
		// (add) Token: 0x0600225E RID: 8798 RVA: 0x00056BC9 File Offset: 0x00054DC9
		// (remove) Token: 0x0600225F RID: 8799 RVA: 0x00056BDC File Offset: 0x00054DDC
		[WebCategory("Action")]
		[WebSysDescription("")]
		public event DataListCommandEventHandler DeleteCommand
		{
			add
			{
				base.Events.AddHandler(DataList.deleteCommandEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataList.deleteCommandEvent, value);
			}
		}

		/// <summary>Occurs when the Edit button is clicked for an item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		// Token: 0x14000073 RID: 115
		// (add) Token: 0x06002260 RID: 8800 RVA: 0x00056BEF File Offset: 0x00054DEF
		// (remove) Token: 0x06002261 RID: 8801 RVA: 0x00056C02 File Offset: 0x00054E02
		[WebCategory("Action")]
		[WebSysDescription("")]
		public event DataListCommandEventHandler EditCommand
		{
			add
			{
				base.Events.AddHandler(DataList.editCommandEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataList.editCommandEvent, value);
			}
		}

		/// <summary>Occurs when any button is clicked in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		// Token: 0x14000074 RID: 116
		// (add) Token: 0x06002262 RID: 8802 RVA: 0x00056C15 File Offset: 0x00054E15
		// (remove) Token: 0x06002263 RID: 8803 RVA: 0x00056C28 File Offset: 0x00054E28
		[WebSysDescription("")]
		[WebCategory("Action")]
		public event DataListCommandEventHandler ItemCommand
		{
			add
			{
				base.Events.AddHandler(DataList.itemCommandEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataList.itemCommandEvent, value);
			}
		}

		/// <summary>Occurs on the server when an item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control is created.</summary>
		// Token: 0x14000075 RID: 117
		// (add) Token: 0x06002264 RID: 8804 RVA: 0x00056C3B File Offset: 0x00054E3B
		// (remove) Token: 0x06002265 RID: 8805 RVA: 0x00056C4E File Offset: 0x00054E4E
		[WebSysDescription("")]
		[WebCategory("Action")]
		public event DataListItemEventHandler ItemCreated
		{
			add
			{
				base.Events.AddHandler(DataList.itemCreatedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataList.itemCreatedEvent, value);
			}
		}

		/// <summary>Occurs when an item is data bound to the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		// Token: 0x14000076 RID: 118
		// (add) Token: 0x06002266 RID: 8806 RVA: 0x00056C61 File Offset: 0x00054E61
		// (remove) Token: 0x06002267 RID: 8807 RVA: 0x00056C74 File Offset: 0x00054E74
		[WebSysDescription("")]
		[WebCategory("Action")]
		public event DataListItemEventHandler ItemDataBound
		{
			add
			{
				base.Events.AddHandler(DataList.itemDataBoundEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataList.itemDataBoundEvent, value);
			}
		}

		/// <summary>Occurs when the Update button is clicked for an item in the <see cref="T:System.Web.UI.WebControls.DataList" /> control.</summary>
		// Token: 0x14000077 RID: 119
		// (add) Token: 0x06002268 RID: 8808 RVA: 0x00056C87 File Offset: 0x00054E87
		// (remove) Token: 0x06002269 RID: 8809 RVA: 0x00056C9A File Offset: 0x00054E9A
		[WebSysDescription("")]
		[WebCategory("Action")]
		public event DataListCommandEventHandler UpdateCommand
		{
			add
			{
				base.Events.AddHandler(DataList.updateCommandEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(DataList.updateCommandEvent, value);
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.WebControls.IRepeatInfoUser.HasFooter" />.</summary>
		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x0600226A RID: 8810 RVA: 0x00056CAD File Offset: 0x00054EAD
		bool IRepeatInfoUser.HasFooter
		{
			get
			{
				return this.ShowFooter && this.footerTemplate != null;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.WebControls.IRepeatInfoUser.HasHeader" />.</summary>
		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x0600226B RID: 8811 RVA: 0x00056CC2 File Offset: 0x00054EC2
		bool IRepeatInfoUser.HasHeader
		{
			get
			{
				return this.ShowHeader && this.headerTemplate != null;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.WebControls.IRepeatInfoUser.HasSeparators" />.</summary>
		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x0600226C RID: 8812 RVA: 0x00056CD7 File Offset: 0x00054ED7
		bool IRepeatInfoUser.HasSeparators
		{
			get
			{
				return this.separatorTemplate != null;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.WebControls.IRepeatInfoUser.RepeatedItemCount" />.</summary>
		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x0600226D RID: 8813 RVA: 0x00056CE4 File Offset: 0x00054EE4
		int IRepeatInfoUser.RepeatedItemCount
		{
			get
			{
				if (this.idx == -1)
				{
					object obj = this.ViewState["Items"];
					this.idx = ((obj == null) ? 0 : ((int)obj));
				}
				return this.idx;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.WebControls.IRepeatInfoUser.GetItemStyle(System.Web.UI.WebControls.ListItemType,System.Int32)" />.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> that represents the style of the specified item type at the specified index in the list control.</returns>
		/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> enumeration values.</param>
		/// <param name="repeatIndex">The index of the item in the list control.</param>
		// Token: 0x0600226E RID: 8814 RVA: 0x00056D24 File Offset: 0x00054F24
		Style IRepeatInfoUser.GetItemStyle(ListItemType itemType, int repeatIndex)
		{
			DataListItem dataListItem;
			switch (itemType)
			{
			case ListItemType.Header:
			case ListItemType.Footer:
				if (repeatIndex >= 0 && (!this.HasControls() || repeatIndex >= this.Controls.Count))
				{
					throw new ArgumentOutOfRangeException();
				}
				dataListItem = this.FindFirstItem(itemType);
				break;
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
			case ListItemType.SelectedItem:
			case ListItemType.EditItem:
				if (repeatIndex >= 0 && (!this.HasControls() || repeatIndex >= this.Controls.Count))
				{
					throw new ArgumentOutOfRangeException();
				}
				dataListItem = this.FindBestItem(repeatIndex);
				break;
			case ListItemType.Separator:
				if (repeatIndex >= 0 && (!this.HasControls() || repeatIndex >= this.Controls.Count))
				{
					throw new ArgumentOutOfRangeException();
				}
				dataListItem = this.FindSpecificItem(itemType, repeatIndex);
				break;
			default:
				dataListItem = null;
				break;
			}
			if (dataListItem == null || !dataListItem.ControlStyleCreated)
			{
				return null;
			}
			return dataListItem.ControlStyle;
		}

		// Token: 0x0600226F RID: 8815 RVA: 0x00056DEC File Offset: 0x00054FEC
		private DataListItem FindFirstItem(ListItemType itemType)
		{
			for (int i = 0; i < this.Controls.Count; i++)
			{
				DataListItem dataListItem = this.Controls[i] as DataListItem;
				if (dataListItem != null && dataListItem.ItemType == itemType)
				{
					return dataListItem;
				}
			}
			return null;
		}

		// Token: 0x06002270 RID: 8816 RVA: 0x00056E30 File Offset: 0x00055030
		private DataListItem FindSpecificItem(ListItemType itemType, int repeatIndex)
		{
			for (int i = 0; i < this.Controls.Count; i++)
			{
				DataListItem dataListItem = this.Controls[i] as DataListItem;
				if (dataListItem != null && dataListItem.ItemType == itemType && dataListItem.ItemIndex == repeatIndex)
				{
					return dataListItem;
				}
			}
			return null;
		}

		// Token: 0x06002271 RID: 8817 RVA: 0x00056E80 File Offset: 0x00055080
		private DataListItem FindBestItem(int repeatIndex)
		{
			int i = 0;
			while (i < this.Controls.Count)
			{
				DataListItem dataListItem = this.Controls[i] as DataListItem;
				if (dataListItem != null && dataListItem.ItemIndex == repeatIndex)
				{
					ListItemType itemType = dataListItem.ItemType;
					if (itemType - ListItemType.Item <= 3)
					{
						return dataListItem;
					}
					return null;
				}
				else
				{
					i++;
				}
			}
			return null;
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.WebControls.IRepeatInfoUser.RenderItem(System.Web.UI.WebControls.ListItemType,System.Int32,System.Web.UI.WebControls.RepeatInfo,System.Web.UI.HtmlTextWriter)" />.</summary>
		/// <param name="itemType">The type of the item.</param>
		/// <param name="repeatIndex">The index of the item.</param>
		/// <param name="repeatInfo">Information that is used to render the item.</param>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object to use to render the item.</param>
		// Token: 0x06002272 RID: 8818 RVA: 0x00056ED4 File Offset: 0x000550D4
		void IRepeatInfoUser.RenderItem(ListItemType itemType, int repeatIndex, RepeatInfo repeatInfo, HtmlTextWriter writer)
		{
			if (!this.HasControls())
			{
				return;
			}
			DataListItem dataListItem = null;
			switch (itemType)
			{
			case ListItemType.Header:
			case ListItemType.Footer:
				dataListItem = this.FindFirstItem(itemType);
				break;
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
			case ListItemType.SelectedItem:
			case ListItemType.EditItem:
				dataListItem = this.FindBestItem(repeatIndex);
				break;
			case ListItemType.Separator:
				dataListItem = this.FindSpecificItem(itemType, repeatIndex);
				break;
			}
			if (dataListItem != null)
			{
				bool extractTemplateRows = this.ExtractTemplateRows;
				bool flag = this.RepeatLayout == RepeatLayout.Table;
				if (!flag || extractTemplateRows)
				{
					Style style = ((IRepeatInfoUser)this).GetItemStyle(itemType, repeatIndex);
					if (style != null)
					{
						dataListItem.ControlStyle.CopyFrom(style);
					}
				}
				dataListItem.RenderItem(writer, extractTemplateRows, flag);
			}
		}

		/// <summary>Represents the Cancel command name. This field is read-only.</summary>
		// Token: 0x0400190D RID: 6413
		public const string CancelCommandName = "Cancel";

		/// <summary>Represents the Delete command name. This field is read-only.</summary>
		// Token: 0x0400190E RID: 6414
		public const string DeleteCommandName = "Delete";

		/// <summary>Represents the Edit command name. This field is read-only.</summary>
		// Token: 0x0400190F RID: 6415
		public const string EditCommandName = "Edit";

		/// <summary>Represents the Select command name. This field is read-only.</summary>
		// Token: 0x04001910 RID: 6416
		public const string SelectCommandName = "Select";

		/// <summary>Represents the Update command name. This field is read-only.</summary>
		// Token: 0x04001911 RID: 6417
		public const string UpdateCommandName = "Update";

		// Token: 0x04001912 RID: 6418
		private static readonly object cancelCommandEvent = new object();

		// Token: 0x04001913 RID: 6419
		private static readonly object deleteCommandEvent = new object();

		// Token: 0x04001914 RID: 6420
		private static readonly object editCommandEvent = new object();

		// Token: 0x04001915 RID: 6421
		private static readonly object itemCommandEvent = new object();

		// Token: 0x04001916 RID: 6422
		private static readonly object itemCreatedEvent = new object();

		// Token: 0x04001917 RID: 6423
		private static readonly object itemDataBoundEvent = new object();

		// Token: 0x04001918 RID: 6424
		private static readonly object updateCommandEvent = new object();

		// Token: 0x04001919 RID: 6425
		private TableItemStyle alternatingItemStyle;

		// Token: 0x0400191A RID: 6426
		private TableItemStyle editItemStyle;

		// Token: 0x0400191B RID: 6427
		private TableItemStyle footerStyle;

		// Token: 0x0400191C RID: 6428
		private TableItemStyle headerStyle;

		// Token: 0x0400191D RID: 6429
		private TableItemStyle itemStyle;

		// Token: 0x0400191E RID: 6430
		private TableItemStyle selectedItemStyle;

		// Token: 0x0400191F RID: 6431
		private TableItemStyle separatorStyle;

		// Token: 0x04001920 RID: 6432
		private ITemplate alternatingItemTemplate;

		// Token: 0x04001921 RID: 6433
		private ITemplate editItemTemplate;

		// Token: 0x04001922 RID: 6434
		private ITemplate footerTemplate;

		// Token: 0x04001923 RID: 6435
		private ITemplate headerTemplate;

		// Token: 0x04001924 RID: 6436
		private ITemplate itemTemplate;

		// Token: 0x04001925 RID: 6437
		private ITemplate selectedItemTemplate;

		// Token: 0x04001926 RID: 6438
		private ITemplate separatorTemplate;

		// Token: 0x04001927 RID: 6439
		private DataListItemCollection items;

		// Token: 0x04001928 RID: 6440
		private ArrayList list;

		// Token: 0x04001929 RID: 6441
		private int idx;
	}
}

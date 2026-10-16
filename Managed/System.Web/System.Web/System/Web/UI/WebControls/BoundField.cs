using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a field that is displayed as text in a data-bound control.</summary>
	// Token: 0x02000337 RID: 823
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class BoundField : DataControlField
	{
		/// <summary>Gets or sets a value indicating whether the formatting string specified by the <see cref="P:System.Web.UI.WebControls.BoundField.DataFormatString" /> property is applied to field values when the data-bound control that contains the <see cref="T:System.Web.UI.WebControls.BoundField" /> object is in edit mode.</summary>
		/// <returns>true to apply the formatting string to field values in edit mode; otherwise, false. The default is false.</returns>
		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x06001D12 RID: 7442 RVA: 0x000469CC File Offset: 0x00044BCC
		// (set) Token: 0x06001D13 RID: 7443 RVA: 0x000469DF File Offset: 0x00044BDF
		[WebCategory("Behavior")]
		[WebSysDescription("")]
		[DefaultValue(false)]
		public virtual bool ApplyFormatInEditMode
		{
			get
			{
				return base.ViewState.GetBool("ApplyFormatInEditMode", false);
			}
			set
			{
				base.ViewState["ApplyFormatInEditMode"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether empty string values ("") are automatically converted to null values when the data field is updated in the data source.</summary>
		/// <returns>true to automatically convert empty string values to null values; otherwise, the false. The default is true.</returns>
		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x06001D14 RID: 7444 RVA: 0x000469F7 File Offset: 0x00044BF7
		// (set) Token: 0x06001D15 RID: 7445 RVA: 0x00046A0A File Offset: 0x00044C0A
		[WebCategory("Behavior")]
		[WebSysDescription("")]
		[DefaultValue(true)]
		public virtual bool ConvertEmptyStringToNull
		{
			get
			{
				return base.ViewState.GetBool("ConvertEmptyStringToNull", true);
			}
			set
			{
				base.ViewState["ConvertEmptyStringToNull"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the name of the data field to bind to the <see cref="T:System.Web.UI.WebControls.BoundField" /> object.</summary>
		/// <returns>The name of the data field to bind to the <see cref="T:System.Web.UI.WebControls.BoundField" />. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x06001D16 RID: 7446 RVA: 0x00046A28 File Offset: 0x00044C28
		// (set) Token: 0x06001D17 RID: 7447 RVA: 0x00046A3F File Offset: 0x00044C3F
		[WebSysDescription("")]
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebCategory("Data")]
		public virtual string DataField
		{
			get
			{
				return base.ViewState.GetString("DataField", string.Empty);
			}
			set
			{
				base.ViewState["DataField"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the string that specifies the display format for the value of the field.</summary>
		/// <returns>A formatting string that specifies the display format for the value of the field. The default is an empty string (""), which indicates that no special formatting is applied to the field value.</returns>
		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x06001D18 RID: 7448 RVA: 0x00046A58 File Offset: 0x00044C58
		// (set) Token: 0x06001D19 RID: 7449 RVA: 0x00046A6F File Offset: 0x00044C6F
		[DefaultValue("")]
		[WebSysDescription("")]
		[WebCategory("Data")]
		public virtual string DataFormatString
		{
			get
			{
				return base.ViewState.GetString("DataFormatString", string.Empty);
			}
			set
			{
				base.ViewState["DataFormatString"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the text that is displayed in the header of a data control.</summary>
		/// <returns>The text displayed in the header of a data control. The default value is an empty string ("").</returns>
		// Token: 0x17000907 RID: 2311
		// (get) Token: 0x06001D1A RID: 7450 RVA: 0x00046A88 File Offset: 0x00044C88
		// (set) Token: 0x06001D1B RID: 7451 RVA: 0x00046A9F File Offset: 0x00044C9F
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public override string HeaderText
		{
			get
			{
				return base.ViewState.GetString("HeaderText", string.Empty);
			}
			set
			{
				base.ViewState["HeaderText"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the caption displayed for a field when the field's value is null.</summary>
		/// <returns>The caption displayed for a field when the field's value is null. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x06001D1C RID: 7452 RVA: 0x00046AB8 File Offset: 0x00044CB8
		// (set) Token: 0x06001D1D RID: 7453 RVA: 0x00046ACF File Offset: 0x00044CCF
		[DefaultValue("")]
		[WebCategory("Behavior")]
		public virtual string NullDisplayText
		{
			get
			{
				return base.ViewState.GetString("NullDisplayText", string.Empty);
			}
			set
			{
				base.ViewState["NullDisplayText"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets a value indicating whether the value of the field can be modified in edit mode.</summary>
		/// <returns>true to prevent the value of the field from being modified in edit mode; otherwise, false. The default is false.</returns>
		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x06001D1E RID: 7454 RVA: 0x00046AE8 File Offset: 0x00044CE8
		// (set) Token: 0x06001D1F RID: 7455 RVA: 0x00046AFB File Offset: 0x00044CFB
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		[DefaultValue(false)]
		public virtual bool ReadOnly
		{
			get
			{
				return base.ViewState.GetBool("ReadOnly", false);
			}
			set
			{
				base.ViewState["ReadOnly"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets a value indicating whether field values are HTML-encoded before they are displayed in a <see cref="T:System.Web.UI.WebControls.BoundField" /> object.</summary>
		/// <returns>true if field values are HTML-encoded before they are displayed in a <see cref="T:System.Web.UI.WebControls.BoundField" /> object; otherwise, false. The default is true.</returns>
		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x06001D20 RID: 7456 RVA: 0x00046B19 File Offset: 0x00044D19
		// (set) Token: 0x06001D21 RID: 7457 RVA: 0x00046B2C File Offset: 0x00044D2C
		[WebCategory("HtmlEncode")]
		[DefaultValue(true)]
		[WebSysDescription("")]
		public virtual bool HtmlEncode
		{
			get
			{
				return base.ViewState.GetBool("HtmlEncode", true);
			}
			set
			{
				base.ViewState["HtmlEncode"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets a value that indicates whether text that is created by applying the <see cref="P:System.Web.UI.WebControls.BoundField.DataFormatString" /> property to the <see cref="T:System.Web.UI.WebControls.BoundField" /> value should be HTML encoded when it is displayed.</summary>
		/// <returns>true if the text should be HTML-encoded; otherwise, false. The default is true.</returns>
		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x06001D22 RID: 7458 RVA: 0x00046B4A File Offset: 0x00044D4A
		// (set) Token: 0x06001D23 RID: 7459 RVA: 0x00046B5D File Offset: 0x00044D5D
		[DefaultValue(true)]
		public virtual bool HtmlEncodeFormatString
		{
			get
			{
				return base.ViewState.GetBool("HtmlEncodeFormatString", true);
			}
			set
			{
				base.ViewState["HtmlEncodeFormatString"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Fills the specified <see cref="T:System.Collections.IDictionary" /> object with the values from the specified <see cref="T:System.Web.UI.WebControls.TableCell" /> object.</summary>
		/// <param name="dictionary">A <see cref="T:System.Collections.IDictionary" /> used to store the values of the specified cell.</param>
		/// <param name="cell">The <see cref="T:System.Web.UI.WebControls.TableCell" /> that contains the values to retrieve.</param>
		/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> values.</param>
		/// <param name="includeReadOnly">true to include the values of read-only fields; otherwise, false.</param>
		// Token: 0x06001D24 RID: 7460 RVA: 0x00046B7C File Offset: 0x00044D7C
		public override void ExtractValuesFromCell(IOrderedDictionary dictionary, DataControlFieldCell cell, DataControlRowState rowState, bool includeReadOnly)
		{
			if (this.IsEditable(rowState))
			{
				if (cell.Controls.Count > 0)
				{
					TextBox textBox = (TextBox)cell.Controls[0];
					dictionary[this.DataField] = textBox.Text;
					return;
				}
			}
			else if (includeReadOnly)
			{
				dictionary[this.DataField] = cell.Text;
			}
		}

		/// <summary>Initializes the <see cref="T:System.Web.UI.WebControls.BoundField" /> object.</summary>
		/// <returns>false in all cases.</returns>
		/// <param name="enableSorting">true if sorting is supported; otherwise, false.</param>
		/// <param name="control">The data control that owns the <see cref="T:System.Web.UI.WebControls.BoundField" />.</param>
		// Token: 0x06001D25 RID: 7461 RVA: 0x00046BDB File Offset: 0x00044DDB
		public override bool Initialize(bool enableSorting, Control control)
		{
			return base.Initialize(enableSorting, control);
		}

		/// <summary>Initializes the specified <see cref="T:System.Web.UI.WebControls.TableCell" /> object to the specified row state.</summary>
		/// <param name="cell">The <see cref="T:System.Web.UI.WebControls.TableCell" /> to initialize.</param>
		/// <param name="cellType">One of the <see cref="T:System.Web.UI.WebControls.DataControlCellType" /> values.</param>
		/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> values.</param>
		/// <param name="rowIndex">The zero-based index of the row.</param>
		// Token: 0x06001D26 RID: 7462 RVA: 0x00046BE5 File Offset: 0x00044DE5
		public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
		{
			base.InitializeCell(cell, cellType, rowState, rowIndex);
			if (cellType == DataControlCellType.DataCell)
			{
				this.InitializeDataCell(cell, rowState);
				if ((rowState & DataControlRowState.Insert) == DataControlRowState.Normal)
				{
					cell.DataBinding += this.OnDataBindField;
				}
			}
		}

		/// <summary>Initializes the specified <see cref="T:System.Web.UI.WebControls.TableCell" /> object to the specified row state.</summary>
		/// <param name="cell">The <see cref="T:System.Web.UI.WebControls.TableCell" /> to initialize.</param>
		/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> values.</param>
		// Token: 0x06001D27 RID: 7463 RVA: 0x00046C18 File Offset: 0x00044E18
		protected virtual void InitializeDataCell(DataControlFieldCell cell, DataControlRowState rowState)
		{
			if (this.IsEditable(rowState))
			{
				TextBox textBox = new TextBox();
				cell.Controls.Add(textBox);
				textBox.ToolTip = this.HeaderText;
			}
		}

		// Token: 0x06001D28 RID: 7464 RVA: 0x00046C4C File Offset: 0x00044E4C
		internal bool IsEditable(DataControlRowState rowState)
		{
			return ((rowState & DataControlRowState.Edit) != DataControlRowState.Normal && !this.ReadOnly) || ((rowState & DataControlRowState.Insert) != DataControlRowState.Normal && this.InsertVisible);
		}

		/// <summary>Gets a value indicating whether HTML encoding is supported by a <see cref="T:System.Web.UI.WebControls.BoundField" /> object.</summary>
		/// <returns>Always returns true to indicate that HTML encoding is supported by a <see cref="T:System.Web.UI.WebControls.BoundField" />.</returns>
		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x06001D29 RID: 7465 RVA: 0x00008B4A File Offset: 0x00006D4A
		protected virtual bool SupportsHtmlEncode
		{
			get
			{
				return true;
			}
		}

		/// <summary>Formats the specified field value for a cell in the <see cref="T:System.Web.UI.WebControls.BoundField" /> object.</summary>
		/// <returns>The field value converted to the format specified by <see cref="P:System.Web.UI.WebControls.BoundField.DataFormatString" />.</returns>
		/// <param name="dataValue">The field value to format.</param>
		/// <param name="encode">true to encode the value; otherwise, false.</param>
		// Token: 0x06001D2A RID: 7466 RVA: 0x00046C6C File Offset: 0x00044E6C
		protected virtual string FormatDataValue(object dataValue, bool encode)
		{
			bool htmlEncodeFormatString = this.HtmlEncodeFormatString;
			string text = ((dataValue != null) ? dataValue.ToString() : string.Empty);
			string text2;
			if (dataValue == null || (text.Length == 0 && this.ConvertEmptyStringToNull))
			{
				if (this.NullDisplayText.Length == 0)
				{
					encode = false;
					text2 = "&nbsp;";
				}
				else
				{
					text2 = this.NullDisplayText;
				}
			}
			else
			{
				string dataFormatString = this.DataFormatString;
				if (!string.IsNullOrEmpty(dataFormatString))
				{
					if (!encode || htmlEncodeFormatString)
					{
						text2 = string.Format(dataFormatString, dataValue);
					}
					else
					{
						text2 = string.Format(dataFormatString, encode ? HttpUtility.HtmlEncode(text) : text);
					}
				}
				else
				{
					text2 = text;
				}
			}
			if (encode && htmlEncodeFormatString)
			{
				return HttpUtility.HtmlEncode(text2);
			}
			return text2;
		}

		/// <summary>Retrieves the value of the field bound to the <see cref="T:System.Web.UI.WebControls.BoundField" /> object.</summary>
		/// <returns>The value of the field bound to the <see cref="T:System.Web.UI.WebControls.BoundField" />.</returns>
		/// <param name="controlContainer">The container for the field value.</param>
		/// <exception cref="T:System.Web.HttpException">The container specified by the <paramref name="controlContainer" /> parameter is null.- or - The container specified by the <paramref name="controlContainer" /> parameter does not have a data item.- or - The data field was not found. </exception>
		// Token: 0x06001D2B RID: 7467 RVA: 0x00046D0A File Offset: 0x00044F0A
		protected virtual object GetValue(Control controlContainer)
		{
			if (base.DesignMode)
			{
				return this.GetDesignTimeValue();
			}
			return this.GetBoundValue(controlContainer);
		}

		/// <summary>Retrieves the value used for a field's value when rendering the <see cref="T:System.Web.UI.WebControls.BoundField" /> object in a designer.</summary>
		/// <returns>The value to display in the designer as the field's value.</returns>
		// Token: 0x06001D2C RID: 7468 RVA: 0x00046D22 File Offset: 0x00044F22
		protected virtual object GetDesignTimeValue()
		{
			return "Databound";
		}

		// Token: 0x06001D2D RID: 7469 RVA: 0x00046D2C File Offset: 0x00044F2C
		private object GetBoundValue(Control controlContainer)
		{
			object dataItem = DataBinder.GetDataItem(controlContainer);
			if (dataItem == null)
			{
				throw new HttpException("A data item was not found in the container. The container must either implement IDataItemContainer, or have a property named DataItem.");
			}
			if (this.DataField == BoundField.ThisExpression)
			{
				return dataItem;
			}
			if (this.DataField == string.Empty)
			{
				return null;
			}
			return DataBinder.GetPropertyValue(dataItem, this.DataField);
		}

		/// <summary>Restores the previously stored view-state information for this field.</summary>
		/// <param name="state">Represents the control state to be restored.</param>
		// Token: 0x06001D2E RID: 7470 RVA: 0x00046D82 File Offset: 0x00044F82
		protected override void LoadViewState(object state)
		{
			base.LoadViewState(state);
		}

		/// <summary>Binds the value of a field to the <see cref="T:System.Web.UI.WebControls.BoundField" /> object.</summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		/// <exception cref="T:System.Web.HttpException">The control to which the field value is bound is not a <see cref="T:System.Web.UI.WebControls.TextBox" /> or a <see cref="T:System.Web.UI.WebControls.TableCell" />. </exception>
		// Token: 0x06001D2F RID: 7471 RVA: 0x00046D8C File Offset: 0x00044F8C
		protected virtual void OnDataBindField(object sender, EventArgs e)
		{
			Control bindingContainer = ((Control)sender).BindingContainer;
			if (!(bindingContainer is INamingContainer))
			{
				throw new HttpException("A DataControlField must be within an INamingContainer.");
			}
			object value = this.GetValue(bindingContainer);
			TextBox textBox = sender as TextBox;
			if (textBox == null)
			{
				DataControlFieldCell dataControlFieldCell = sender as DataControlFieldCell;
				if (dataControlFieldCell != null)
				{
					ControlCollection controls = dataControlFieldCell.Controls;
					if (((controls != null) ? controls.Count : 0) == 1)
					{
						textBox = controls[0] as TextBox;
					}
					if (textBox == null)
					{
						dataControlFieldCell.Text = this.FormatDataValue(value, this.SupportsHtmlEncode && this.HtmlEncode);
						return;
					}
				}
			}
			if (textBox == null)
			{
				throw new HttpException("Bound field " + this.DataField + " contains a control that isn't a TextBox.  Override OnDataBindField to inherit from BoundField and add different controls.");
			}
			if (this.ApplyFormatInEditMode)
			{
				textBox.Text = this.FormatDataValue(value, this.SupportsHtmlEncode && this.HtmlEncode);
				return;
			}
			textBox.Text = ((value != null) ? value.ToString() : this.NullDisplayText);
		}

		/// <summary>Creates an empty <see cref="T:System.Web.UI.WebControls.BoundField" /> object.</summary>
		/// <returns>An empty <see cref="T:System.Web.UI.WebControls.BoundField" />.</returns>
		// Token: 0x06001D30 RID: 7472 RVA: 0x00046E79 File Offset: 0x00045079
		protected override DataControlField CreateField()
		{
			return new BoundField();
		}

		/// <summary>Copies the properties of the current <see cref="T:System.Web.UI.WebControls.BoundField" /> object to the specified <see cref="T:System.Web.UI.WebControls.DataControlField" /> object.</summary>
		/// <param name="newField">The <see cref="T:System.Web.UI.WebControls.DataControlField" /> to copy the properties of the current <see cref="T:System.Web.UI.WebControls.BoundField" /> to.</param>
		// Token: 0x06001D31 RID: 7473 RVA: 0x00046E80 File Offset: 0x00045080
		protected override void CopyProperties(DataControlField newField)
		{
			base.CopyProperties(newField);
			BoundField boundField = (BoundField)newField;
			boundField.ConvertEmptyStringToNull = this.ConvertEmptyStringToNull;
			boundField.DataField = this.DataField;
			boundField.DataFormatString = this.DataFormatString;
			boundField.NullDisplayText = this.NullDisplayText;
			boundField.ReadOnly = this.ReadOnly;
			boundField.HtmlEncode = this.HtmlEncode;
		}

		/// <summary>Determines whether the controls contained in a <see cref="T:System.Web.UI.WebControls.BoundField" /> object support callbacks.</summary>
		// Token: 0x06001D32 RID: 7474 RVA: 0x00003946 File Offset: 0x00001B46
		public override void ValidateSupportsCallback()
		{
		}

		/// <summary>Gets or sets a value that determines whether the control validates client input or not.</summary>
		/// <returns>A value that determines whether the control validates client input or not. The default is <see cref="F:System.Web.UI.ValidateRequestMode.Inherit" />.</returns>
		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x06001D35 RID: 7477 RVA: 0x00046EF8 File Offset: 0x000450F8
		// (set) Token: 0x06001D36 RID: 7478 RVA: 0x0000B3D8 File Offset: 0x000095D8
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

		/// <summary>Represents the "this" expression.</summary>
		// Token: 0x0400181D RID: 6173
		public static readonly string ThisExpression = "!";
	}
}

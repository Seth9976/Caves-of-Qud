using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a Boolean field that is displayed as a check box in a data-bound control.</summary>
	// Token: 0x02000348 RID: 840
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class CheckBoxField : BoundField
	{
		/// <summary>Overrides the <see cref="P:System.Web.UI.WebControls.BoundField.ApplyFormatInEditMode" /> property. This property is not supported by the <see cref="T:System.Web.UI.WebControls.CheckBoxField" /> class.</summary>
		/// <returns>false in all cases. This property is not supported, and throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt is made to read or set the value of this property. </exception>
		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x06001EE4 RID: 7908 RVA: 0x0004C62A File Offset: 0x0004A82A
		// (set) Token: 0x06001EE5 RID: 7909 RVA: 0x0004C62A File Offset: 0x0004A82A
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public override bool ApplyFormatInEditMode
		{
			get
			{
				throw base.GetNotSupportedPropException("ApplyFormatInEditMode");
			}
			set
			{
				throw base.GetNotSupportedPropException("ApplyFormatInEditMode");
			}
		}

		/// <summary>Overrides the <see cref="P:System.Web.UI.WebControls.BoundField.ConvertEmptyStringToNull" /> property. This property is not supported by the <see cref="T:System.Web.UI.WebControls.CheckBoxField" /> class.</summary>
		/// <returns>false in all cases. This property is not supported and throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt is made to read or set the value of this property. </exception>
		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x06001EE6 RID: 7910 RVA: 0x0004C637 File Offset: 0x0004A837
		// (set) Token: 0x06001EE7 RID: 7911 RVA: 0x0004C637 File Offset: 0x0004A837
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool ConvertEmptyStringToNull
		{
			get
			{
				throw base.GetNotSupportedPropException("ConvertEmptyStringToNull");
			}
			set
			{
				throw base.GetNotSupportedPropException("ConvertEmptyStringToNull");
			}
		}

		/// <summary>Gets or sets the name of the data field to bind to the <see cref="T:System.Web.UI.WebControls.CheckBoxField" /> object.</summary>
		/// <returns>The name of the data field to bind to the <see cref="T:System.Web.UI.WebControls.CheckBoxField" />. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x06001EE8 RID: 7912 RVA: 0x0004C644 File Offset: 0x0004A844
		// (set) Token: 0x06001EE9 RID: 7913 RVA: 0x0004C64C File Offset: 0x0004A84C
		[TypeConverter("System.Web.UI.Design.DataSourceBooleanViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public override string DataField
		{
			get
			{
				return base.DataField;
			}
			set
			{
				base.DataField = value;
			}
		}

		/// <summary>Gets or sets the string that specifies the display format for the value of the field. This property is not supported by the <see cref="T:System.Web.UI.WebControls.CheckBoxField" /> class.</summary>
		/// <returns>A formatting string that specifies the display format for the value of the field. This property is not supported, and throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt is made to read or set the value of this property. </exception>
		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x06001EEA RID: 7914 RVA: 0x0004C655 File Offset: 0x0004A855
		// (set) Token: 0x06001EEB RID: 7915 RVA: 0x0004C655 File Offset: 0x0004A855
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string DataFormatString
		{
			get
			{
				throw base.GetNotSupportedPropException("DataFormatString");
			}
			set
			{
				throw base.GetNotSupportedPropException("DataFormatString");
			}
		}

		/// <summary>Overrides the <see cref="P:System.Web.UI.WebControls.BoundField.HtmlEncode" /> property. This property is not supported by the <see cref="T:System.Web.UI.WebControls.CheckBoxField" /> class.</summary>
		/// <returns>false in all cases. This property is not supported and throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt is made to read or set the value of this property. </exception>
		// Token: 0x170009B6 RID: 2486
		// (get) Token: 0x06001EEC RID: 7916 RVA: 0x0004C662 File Offset: 0x0004A862
		// (set) Token: 0x06001EED RID: 7917 RVA: 0x0004C662 File Offset: 0x0004A862
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool HtmlEncode
		{
			get
			{
				throw base.GetNotSupportedPropException("HtmlEncode");
			}
			set
			{
				throw base.GetNotSupportedPropException("HtmlEncode");
			}
		}

		/// <summary>Gets or sets a value that indicates whether the formatted text should be HTML encoded before it is displayed.</summary>
		/// <returns>true if the text should be HTML encoded; otherwise, false. The default is true.</returns>
		// Token: 0x170009B7 RID: 2487
		// (get) Token: 0x06001EEE RID: 7918 RVA: 0x0004C66F File Offset: 0x0004A86F
		// (set) Token: 0x06001EEF RID: 7919 RVA: 0x0004C677 File Offset: 0x0004A877
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override bool HtmlEncodeFormatString
		{
			get
			{
				return base.HtmlEncodeFormatString;
			}
			set
			{
				base.HtmlEncodeFormatString = value;
			}
		}

		/// <summary>Gets or sets the text displayed for a field when the field's value is null. This property is not supported by the <see cref="T:System.Web.UI.WebControls.CheckBoxField" /> class.</summary>
		/// <returns>The text displayed for a field with a value of null. This property is not supported, and throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt is made to read or set the value of this property. </exception>
		// Token: 0x170009B8 RID: 2488
		// (get) Token: 0x06001EF0 RID: 7920 RVA: 0x0004C680 File Offset: 0x0004A880
		// (set) Token: 0x06001EF1 RID: 7921 RVA: 0x0004C680 File Offset: 0x0004A880
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string NullDisplayText
		{
			get
			{
				throw base.GetNotSupportedPropException("NullDisplayText");
			}
			set
			{
				throw base.GetNotSupportedPropException("NullDisplayText");
			}
		}

		/// <summary>Gets a Boolean value indicating whether the control supports HTML encoding.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x06001EF2 RID: 7922 RVA: 0x00008A4D File Offset: 0x00006C4D
		protected override bool SupportsHtmlEncode
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets the caption to display next to each check box in a <see cref="T:System.Web.UI.WebControls.CheckBoxField" /> object.</summary>
		/// <returns>The caption displayed next to each check box in the <see cref="T:System.Web.UI.WebControls.CheckBoxField" />. The default is an empty string ("").</returns>
		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x06001EF3 RID: 7923 RVA: 0x00047C97 File Offset: 0x00045E97
		// (set) Token: 0x06001EF4 RID: 7924 RVA: 0x00047CAE File Offset: 0x00045EAE
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("")]
		[Localizable(true)]
		public virtual string Text
		{
			get
			{
				return base.ViewState.GetString("Text", string.Empty);
			}
			set
			{
				base.ViewState["Text"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Initializes the specified <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> object to the specified row state.</summary>
		/// <param name="cell">The <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> to initialize.</param>
		/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> values.</param>
		// Token: 0x06001EF5 RID: 7925 RVA: 0x0004C690 File Offset: 0x0004A890
		protected override void InitializeDataCell(DataControlFieldCell cell, DataControlRowState rowState)
		{
			bool flag = base.IsEditable(rowState);
			CheckBox checkBox = new CheckBox();
			checkBox.Enabled = flag;
			if (flag)
			{
				checkBox.ToolTip = this.HeaderText;
			}
			checkBox.Text = this.Text;
			cell.Controls.Add(checkBox);
		}

		/// <summary>Fills the specified <see cref="T:System.Collections.IDictionary" /> object with the values from the specified <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> object.</summary>
		/// <param name="dictionary">A <see cref="T:System.Collections.IDictionary" /> used to store the values of the specified cell.</param>
		/// <param name="cell">The <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> that contains the values to retrieve.</param>
		/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> values.</param>
		/// <param name="includeReadOnly">true to include the values of read-only fields; otherwise, false.</param>
		// Token: 0x06001EF6 RID: 7926 RVA: 0x0004C6DC File Offset: 0x0004A8DC
		public override void ExtractValuesFromCell(IOrderedDictionary dictionary, DataControlFieldCell cell, DataControlRowState rowState, bool includeReadOnly)
		{
			if (base.IsEditable(rowState) || includeReadOnly)
			{
				CheckBox checkBox = (CheckBox)cell.Controls[0];
				dictionary[this.DataField] = checkBox.Checked;
			}
		}

		/// <summary>Binds the value of a field to a check box in the <see cref="T:System.Web.UI.WebControls.CheckBoxField" /> object.</summary>
		/// <param name="sender">The source of the event. </param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.Web.HttpException">The control to which the field value is bound is not a <see cref="T:System.Web.UI.WebControls.CheckBox" /> control.- or -The field value cannot be converted to a Boolean value. </exception>
		// Token: 0x06001EF7 RID: 7927 RVA: 0x0004C720 File Offset: 0x0004A920
		protected override void OnDataBindField(object sender, EventArgs e)
		{
			try
			{
				Control control = (Control)sender;
				object value = this.GetValue(control.NamingContainer);
				CheckBox checkBox = sender as CheckBox;
				if (checkBox == null)
				{
					DataControlFieldCell dataControlFieldCell = sender as DataControlFieldCell;
					if (dataControlFieldCell != null)
					{
						ControlCollection controls = dataControlFieldCell.Controls;
						if (((controls != null) ? controls.Count : 0) == 1)
						{
							checkBox = controls[0] as CheckBox;
						}
						if (checkBox == null)
						{
							return;
						}
					}
				}
				if (checkBox == null)
				{
					throw new HttpException("CheckBox field '" + this.DataField + "' contains a control that isn't a CheckBox.  Override OnDataBindField to inherit from CheckBoxField and add different controls.");
				}
				if (value != null && value != DBNull.Value)
				{
					checkBox.Checked = (bool)value;
				}
				else if (string.IsNullOrEmpty(this.DataField))
				{
					checkBox.Visible = false;
					return;
				}
				if (!checkBox.Visible)
				{
					checkBox.Visible = true;
				}
			}
			catch (HttpException)
			{
				throw;
			}
			catch (Exception ex)
			{
				throw new HttpException(ex.Message, ex);
			}
		}

		/// <summary>Retrieves the value used for the field's value when rendering the <see cref="T:System.Web.UI.WebControls.CheckBoxField" /> object in a designer.</summary>
		/// <returns>Always returns true.</returns>
		// Token: 0x06001EF8 RID: 7928 RVA: 0x0004C810 File Offset: 0x0004AA10
		protected override object GetDesignTimeValue()
		{
			return true;
		}

		/// <summary>Creates an empty <see cref="T:System.Web.UI.WebControls.CheckBoxField" /> object.</summary>
		/// <returns>An empty <see cref="T:System.Web.UI.WebControls.CheckBoxField" />.</returns>
		// Token: 0x06001EF9 RID: 7929 RVA: 0x0004C818 File Offset: 0x0004AA18
		protected override DataControlField CreateField()
		{
			return new CheckBoxField();
		}

		/// <summary>Copies the properties of the current <see cref="T:System.Web.UI.WebControls.CheckBoxField" /> object to the specified <see cref="T:System.Web.UI.WebControls.DataControlField" /> object.</summary>
		/// <param name="newField">The <see cref="T:System.Web.UI.WebControls.DataControlField" /> to copy the properties of the current <see cref="T:System.Web.UI.WebControls.CheckBoxField" /> to.</param>
		// Token: 0x06001EFA RID: 7930 RVA: 0x0004C81F File Offset: 0x0004AA1F
		protected override void CopyProperties(DataControlField newField)
		{
			CheckBoxField checkBoxField = (CheckBoxField)newField;
			checkBoxField.DataField = this.DataField;
			checkBoxField.ReadOnly = this.ReadOnly;
			checkBoxField.Text = this.Text;
		}

		/// <summary>Determines whether the controls contained in a <see cref="T:System.Web.UI.WebControls.CheckBoxField" /> object support callbacks.</summary>
		// Token: 0x06001EFB RID: 7931 RVA: 0x00003946 File Offset: 0x00001B46
		public override void ValidateSupportsCallback()
		{
		}
	}
}

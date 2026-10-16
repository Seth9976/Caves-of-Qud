using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a field that is displayed as a hyperlink in a data-bound control.</summary>
	// Token: 0x020003AF RID: 943
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HyperLinkField : DataControlField
	{
		/// <summary>Initializes the <see cref="T:System.Web.UI.WebControls.HyperLinkField" /> object.</summary>
		/// <returns>Always returns false.</returns>
		/// <param name="enableSorting">true if sorting is supported; otherwise, false.</param>
		/// <param name="control">The data control that acts as the parent for the <see cref="T:System.Web.UI.WebControls.HyperLinkField" />.</param>
		// Token: 0x0600268E RID: 9870 RVA: 0x00046BDB File Offset: 0x00044DDB
		public override bool Initialize(bool enableSorting, Control control)
		{
			return base.Initialize(enableSorting, control);
		}

		/// <summary>Gets or sets the names of the fields from the data source used to construct the URLs for the hyperlinks in the <see cref="T:System.Web.UI.WebControls.HyperLinkField" /> object.</summary>
		/// <returns>An array containing the names of the fields from the data source used to construct the URLs for the hyperlinks in the <see cref="T:System.Web.UI.WebControls.HyperLinkField" />. The default is an empty array, indicating that <see cref="P:System.Web.UI.WebControls.HyperLinkField.DataNavigateUrlFields" /> is not set.</returns>
		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x0600268F RID: 9871 RVA: 0x00062998 File Offset: 0x00060B98
		// (set) Token: 0x06002690 RID: 9872 RVA: 0x000629D7 File Offset: 0x00060BD7
		[Editor("System.Web.UI.Design.WebControls.DataFieldEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue(null)]
		[WebCategory("Data")]
		[TypeConverter(typeof(StringArrayConverter))]
		public virtual string[] DataNavigateUrlFields
		{
			get
			{
				object obj = base.ViewState["DataNavigateUrlFields"];
				if (obj != null)
				{
					return (string[])obj;
				}
				if (HyperLinkField.emptyFields == null)
				{
					HyperLinkField.emptyFields = new string[0];
				}
				return HyperLinkField.emptyFields;
			}
			set
			{
				base.ViewState["DataNavigateUrlFields"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the string that specifies the format in which the URLs for the hyperlinks in a <see cref="T:System.Web.UI.WebControls.HyperLinkField" /> object are rendered.</summary>
		/// <returns>A string that specifies the format in which the URLs for the hyperlinks in a <see cref="T:System.Web.UI.WebControls.HyperLinkField" /> are rendered. The default is an empty string (""), which indicates that no special formatting is applied to the URL values.</returns>
		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x06002691 RID: 9873 RVA: 0x000629F0 File Offset: 0x00060BF0
		// (set) Token: 0x06002692 RID: 9874 RVA: 0x00062A1D File Offset: 0x00060C1D
		[DefaultValue("")]
		[WebCategory("Data")]
		public virtual string DataNavigateUrlFormatString
		{
			get
			{
				object obj = base.ViewState["DataNavigateUrlFormatString"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["DataNavigateUrlFormatString"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the name of the field from the data source containing the text to display for the hyperlink captions in the <see cref="T:System.Web.UI.WebControls.HyperLinkField" /> object.</summary>
		/// <returns>The name of the field from the data source containing the values to display for the hyperlink captions in the <see cref="T:System.Web.UI.WebControls.HyperLinkField" />. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x06002693 RID: 9875 RVA: 0x00062A38 File Offset: 0x00060C38
		// (set) Token: 0x06002694 RID: 9876 RVA: 0x00047C1E File Offset: 0x00045E1E
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebCategory("Data")]
		[DefaultValue("")]
		public virtual string DataTextField
		{
			get
			{
				object obj = base.ViewState["DataTextField"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["DataTextField"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Get or sets the string that specifies the format in which the hyperlink captions in a <see cref="T:System.Web.UI.WebControls.HyperLinkField" /> object are displayed.</summary>
		/// <returns>A string that specifies the format in which the hyperlink captions in a <see cref="T:System.Web.UI.WebControls.HyperLinkField" /> are displayed. The default is an empty string (""), which indicates that no special formatting is applied to the hyperlink captions.</returns>
		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x06002695 RID: 9877 RVA: 0x00062A68 File Offset: 0x00060C68
		// (set) Token: 0x06002696 RID: 9878 RVA: 0x00047C4E File Offset: 0x00045E4E
		[DefaultValue("")]
		[WebCategory("Data")]
		public virtual string DataTextFormatString
		{
			get
			{
				object obj = base.ViewState["DataTextFormatString"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["DataTextFormatString"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the URL to navigate to when a hyperlink in a <see cref="T:System.Web.UI.WebControls.HyperLinkField" /> object is clicked.</summary>
		/// <returns>The URL to navigate to when a hyperlink in a <see cref="T:System.Web.UI.WebControls.HyperLinkField" /> is clicked. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x06002697 RID: 9879 RVA: 0x00062A98 File Offset: 0x00060C98
		// (set) Token: 0x06002698 RID: 9880 RVA: 0x00062AC5 File Offset: 0x00060CC5
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.UrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[UrlProperty]
		[WebCategory("Behavior")]
		public virtual string NavigateUrl
		{
			get
			{
				object obj = base.ViewState["NavigateUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["NavigateUrl"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the target window or frame in which to display the Web page linked to when a hyperlink in a <see cref="T:System.Web.UI.WebControls.HyperLinkField" /> object is clicked.</summary>
		/// <returns>The target window or frame in which to load the Web page linked to when a hyperlink in a <see cref="T:System.Web.UI.WebControls.HyperLinkField" /> is clicked. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x06002699 RID: 9881 RVA: 0x00062AE0 File Offset: 0x00060CE0
		// (set) Token: 0x0600269A RID: 9882 RVA: 0x00062B0D File Offset: 0x00060D0D
		[DefaultValue("")]
		[WebCategory("Behavior")]
		[TypeConverter(typeof(TargetConverter))]
		public virtual string Target
		{
			get
			{
				object obj = base.ViewState["Target"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["Target"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the text to display for each hyperlink in the <see cref="T:System.Web.UI.WebControls.HyperLinkField" /> object.</summary>
		/// <returns>The text to display for each hyperlink in the <see cref="T:System.Web.UI.WebControls.HyperLinkField" />. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x0600269B RID: 9883 RVA: 0x00062B28 File Offset: 0x00060D28
		// (set) Token: 0x0600269C RID: 9884 RVA: 0x00047CAE File Offset: 0x00045EAE
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[Localizable(true)]
		public virtual string Text
		{
			get
			{
				object obj = base.ViewState["Text"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["Text"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Initializes a cell in a <see cref="T:System.Web.UI.WebControls.HyperLinkField" /> object.</summary>
		/// <param name="cell">A <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> that contains the text or controls of the <see cref="T:System.Web.UI.WebControls.HyperLinkField" />.</param>
		/// <param name="cellType">One of the <see cref="T:System.Web.UI.WebControls.DataControlCellType" /> values.</param>
		/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> values that specifies the state of the row containing the <see cref="T:System.Web.UI.WebControls.HyperLinkField" />.</param>
		/// <param name="rowIndex">The index of the row in the table.</param>
		// Token: 0x0600269D RID: 9885 RVA: 0x00062B58 File Offset: 0x00060D58
		public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
		{
			base.InitializeCell(cell, cellType, rowState, rowIndex);
			if (cellType == DataControlCellType.DataCell)
			{
				HyperLink hyperLink = new HyperLink();
				bool flag = false;
				if (this.Target.Length > 0)
				{
					hyperLink.Target = this.Target;
				}
				if (this.DataTextField.Length > 0)
				{
					flag = true;
				}
				else
				{
					hyperLink.Text = this.Text;
				}
				if (this.DataNavigateUrlFields.Length != 0)
				{
					flag = true;
				}
				else
				{
					hyperLink.NavigateUrl = this.NavigateUrl;
				}
				if (flag && cellType == DataControlCellType.DataCell && (rowState & DataControlRowState.Insert) == DataControlRowState.Normal)
				{
					cell.DataBinding += this.OnDataBindField;
				}
				hyperLink.ControlStyle.CopyFrom(base.ControlStyle);
				cell.Controls.Add(hyperLink);
			}
		}

		/// <summary>Formats the navigation URL using the format string specified by the <see cref="P:System.Web.UI.WebControls.HyperLinkField.DataNavigateUrlFormatString" /> property.</summary>
		/// <returns>The formatted URL value.</returns>
		/// <param name="dataUrlValues">An array of values to combine with the format string.</param>
		// Token: 0x0600269E RID: 9886 RVA: 0x00062C0B File Offset: 0x00060E0B
		protected virtual string FormatDataNavigateUrlValue(object[] dataUrlValues)
		{
			if (dataUrlValues == null || dataUrlValues.Length == 0)
			{
				return string.Empty;
			}
			if (this.DataNavigateUrlFormatString.Length > 0)
			{
				return string.Format(this.DataNavigateUrlFormatString, dataUrlValues);
			}
			return dataUrlValues[0].ToString();
		}

		/// <summary>Formats the caption text using the format string specified by the <see cref="P:System.Web.UI.WebControls.HyperLinkField.DataTextFormatString" /> property.</summary>
		/// <returns>The formatted text value.</returns>
		/// <param name="dataTextValue">The text value to format. </param>
		// Token: 0x0600269F RID: 9887 RVA: 0x00062C3D File Offset: 0x00060E3D
		protected virtual string FormatDataTextValue(object dataTextValue)
		{
			if (this.DataTextFormatString.Length > 0)
			{
				return string.Format(this.DataTextFormatString, dataTextValue);
			}
			if (dataTextValue == null)
			{
				return string.Empty;
			}
			return dataTextValue.ToString();
		}

		// Token: 0x060026A0 RID: 9888 RVA: 0x00062C6C File Offset: 0x00060E6C
		private void OnDataBindField(object sender, EventArgs e)
		{
			DataControlFieldCell dataControlFieldCell = (DataControlFieldCell)sender;
			HyperLink hyperLink = (HyperLink)dataControlFieldCell.Controls[0];
			object bindingContainer = dataControlFieldCell.BindingContainer;
			object dataItem = DataBinder.GetDataItem(bindingContainer);
			if (this.DataTextField.Length > 0)
			{
				if (this.textProperty == null)
				{
					this.SetupProperties(bindingContainer);
				}
				hyperLink.Text = this.FormatDataTextValue(this.textProperty.GetValue(dataItem));
			}
			string[] dataNavigateUrlFields = this.DataNavigateUrlFields;
			if (dataNavigateUrlFields.Length != 0)
			{
				if (this.urlProperties == null)
				{
					this.SetupProperties(bindingContainer);
				}
				object[] array = new object[dataNavigateUrlFields.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = this.urlProperties[i].GetValue(dataItem);
				}
				hyperLink.NavigateUrl = this.FormatDataNavigateUrlValue(array);
			}
		}

		// Token: 0x060026A1 RID: 9889 RVA: 0x00062D2C File Offset: 0x00060F2C
		private void SetupProperties(object controlContainer)
		{
			object dataItem = DataBinder.GetDataItem(controlContainer);
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(dataItem);
			if (this.DataTextField.Length > 0)
			{
				this.textProperty = properties.Find(this.DataTextField, true);
				if (this.textProperty == null)
				{
					string text = "Property '";
					string dataTextField = this.DataTextField;
					string text2 = "' not found in object of type ";
					Type type = dataItem.GetType();
					throw new InvalidOperationException(text + dataTextField + text2 + ((type != null) ? type.ToString() : null));
				}
			}
			string[] dataNavigateUrlFields = this.DataNavigateUrlFields;
			if (dataNavigateUrlFields.Length != 0)
			{
				this.urlProperties = new PropertyDescriptor[dataNavigateUrlFields.Length];
				for (int i = 0; i < dataNavigateUrlFields.Length; i++)
				{
					PropertyDescriptor propertyDescriptor = properties.Find(dataNavigateUrlFields[i], true);
					if (propertyDescriptor == null)
					{
						string text3 = "Property '";
						string text4 = dataNavigateUrlFields[i];
						string text5 = "' not found in object of type ";
						Type type2 = dataItem.GetType();
						throw new InvalidOperationException(text3 + text4 + text5 + ((type2 != null) ? type2.ToString() : null));
					}
					this.urlProperties[i] = propertyDescriptor;
				}
			}
		}

		/// <summary>Returns a new instance of the <see cref="T:System.Web.UI.WebControls.HyperLinkField" /> class.</summary>
		/// <returns>A new instance of <see cref="T:System.Web.UI.WebControls.HyperLinkField" />.</returns>
		// Token: 0x060026A2 RID: 9890 RVA: 0x00062E08 File Offset: 0x00061008
		protected override DataControlField CreateField()
		{
			return new HyperLinkField();
		}

		/// <summary>Copies the properties of the current <see cref="T:System.Web.UI.WebControls.HyperLinkField" /> object to the specified object.</summary>
		/// <param name="newField">The <see cref="T:System.Web.UI.WebControls.DataControlField" />-derived object that receives the copy.</param>
		// Token: 0x060026A3 RID: 9891 RVA: 0x00062E10 File Offset: 0x00061010
		protected override void CopyProperties(DataControlField newField)
		{
			base.CopyProperties(newField);
			HyperLinkField hyperLinkField = (HyperLinkField)newField;
			hyperLinkField.DataNavigateUrlFields = this.DataNavigateUrlFields;
			hyperLinkField.DataNavigateUrlFormatString = this.DataNavigateUrlFormatString;
			hyperLinkField.DataTextField = this.DataTextField;
			hyperLinkField.DataTextFormatString = this.DataTextFormatString;
			hyperLinkField.NavigateUrl = this.NavigateUrl;
			hyperLinkField.Target = this.Target;
			hyperLinkField.Text = this.Text;
		}

		/// <summary>Indicates that the controls contained by the <see cref="T:System.Web.UI.WebControls.HyperLinkField" /> object support callbacks.</summary>
		// Token: 0x060026A4 RID: 9892 RVA: 0x00003946 File Offset: 0x00001B46
		public override void ValidateSupportsCallback()
		{
		}

		// Token: 0x04001A49 RID: 6729
		private PropertyDescriptor textProperty;

		// Token: 0x04001A4A RID: 6730
		private PropertyDescriptor[] urlProperties;

		// Token: 0x04001A4B RID: 6731
		private static string[] emptyFields;
	}
}

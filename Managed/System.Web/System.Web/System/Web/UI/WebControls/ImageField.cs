using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a field that is displayed as an image in a data-bound control.</summary>
	// Token: 0x020003B4 RID: 948
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class ImageField : DataControlField
	{
		/// <summary>Initializes the <see cref="T:System.Web.UI.WebControls.ImageField" /> object.</summary>
		/// <returns>Always returns true.</returns>
		/// <param name="enableSorting">true if sorting is supported; otherwise, false. </param>
		/// <param name="control">The data control that contains the <see cref="T:System.Web.UI.WebControls.ImageField" />. </param>
		// Token: 0x060026F0 RID: 9968 RVA: 0x00046BDB File Offset: 0x00044DDB
		public override bool Initialize(bool enableSorting, Control control)
		{
			return base.Initialize(enableSorting, control);
		}

		/// <summary>Gets or sets the alternate text displayed for an image in the <see cref="T:System.Web.UI.WebControls.ImageField" /> object.</summary>
		/// <returns>The alternate text for an image displayed in the <see cref="T:System.Web.UI.WebControls.ImageField" /> object. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x060026F1 RID: 9969 RVA: 0x00063584 File Offset: 0x00061784
		// (set) Token: 0x060026F2 RID: 9970 RVA: 0x000635B1 File Offset: 0x000617B1
		[WebCategory("Appearance")]
		[WebSysDescription("")]
		[Localizable(true)]
		[DefaultValue("")]
		public virtual string AlternateText
		{
			get
			{
				object obj = base.ViewState["AlternateText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["AlternateText"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets a value indicating whether empty string ("") values are converted to null when the field values are returned from the data source.</summary>
		/// <returns>true if <see cref="F:System.String.Empty" /> values should be converted to null; otherwise, false. The default is true.</returns>
		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x060026F3 RID: 9971 RVA: 0x000635CC File Offset: 0x000617CC
		// (set) Token: 0x060026F4 RID: 9972 RVA: 0x00046A0A File Offset: 0x00044C0A
		[WebCategory("Behavior")]
		[WebSysDescription("")]
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

		/// <summary>Gets or sets the name of the field from the data source that contains the values to bind to the <see cref="P:System.Web.UI.WebControls.Image.AlternateText" /> property of each image in an <see cref="T:System.Web.UI.WebControls.ImageField" /> object.</summary>
		/// <returns>The name of the field to bind the <see cref="P:System.Web.UI.WebControls.Image.AlternateText" /> property of each image in an <see cref="T:System.Web.UI.WebControls.ImageField" /> object.</returns>
		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x060026F5 RID: 9973 RVA: 0x000635F8 File Offset: 0x000617F8
		// (set) Token: 0x060026F6 RID: 9974 RVA: 0x00063625 File Offset: 0x00061825
		[WebSysDescription("")]
		[WebCategory("Data")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		public virtual string DataAlternateTextField
		{
			get
			{
				object obj = base.ViewState["DataAlternateTextField"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["DataAlternateTextField"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the string that specifies the format in which the alternate text for each image in an <see cref="T:System.Web.UI.WebControls.ImageField" /> object is rendered.</summary>
		/// <returns>A string that specifies the format in which the alternate text for each image in an <see cref="T:System.Web.UI.WebControls.ImageField" /> object is rendered. The default is an empty string (""), which indicates that now special formatting is applied to the alternate text.</returns>
		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x060026F7 RID: 9975 RVA: 0x00063640 File Offset: 0x00061840
		// (set) Token: 0x060026F8 RID: 9976 RVA: 0x0006366D File Offset: 0x0006186D
		[WebCategory("Data")]
		[WebSysDescription("")]
		[DefaultValue("")]
		public virtual string DataAlternateTextFormatString
		{
			get
			{
				object obj = base.ViewState["DataAlternateTextFormatString"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["DataAlternateTextFormatString"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the name of the field from the data source that contains the values to bind to the <see cref="P:System.Web.UI.MobileControls.Image.ImageUrl" /> property of each image in an <see cref="T:System.Web.UI.WebControls.ImageField" /> object.</summary>
		/// <returns>The name of the field to bind to the <see cref="P:System.Web.UI.MobileControls.Image.ImageUrl" /> property of each image in an <see cref="T:System.Web.UI.WebControls.ImageField" /> object.</returns>
		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x060026F9 RID: 9977 RVA: 0x00063688 File Offset: 0x00061888
		// (set) Token: 0x060026FA RID: 9978 RVA: 0x000636B5 File Offset: 0x000618B5
		[WebCategory("Data")]
		[WebSysDescription("")]
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public virtual string DataImageUrlField
		{
			get
			{
				object obj = base.ViewState["DataImageUrlField"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["DataImageUrlField"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the string that specifies the format in which the URL for each image in an <see cref="T:System.Web.UI.WebControls.ImageField" /> object is rendered.</summary>
		/// <returns>A string that specifies the format in which the URL for each image in an <see cref="T:System.Web.UI.WebControls.ImageField" /> object is rendered. The default is the empty string ("") , which indicates that no special formatting is applied to the URLs.</returns>
		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x060026FB RID: 9979 RVA: 0x000636D0 File Offset: 0x000618D0
		// (set) Token: 0x060026FC RID: 9980 RVA: 0x000636FD File Offset: 0x000618FD
		[DefaultValue("")]
		[WebCategory("Data")]
		[WebSysDescription("")]
		public virtual string DataImageUrlFormatString
		{
			get
			{
				object obj = base.ViewState["DataImageUrlFormatString"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["DataImageUrlFormatString"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the text to display in an <see cref="T:System.Web.UI.WebControls.ImageField" /> object when the value of the field specified by the <see cref="P:System.Web.UI.WebControls.ImageField.DataImageUrlField" /> property is null.</summary>
		/// <returns>The text to display when the value of a field is null. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x060026FD RID: 9981 RVA: 0x00063718 File Offset: 0x00061918
		// (set) Token: 0x060026FE RID: 9982 RVA: 0x00046ACF File Offset: 0x00044CCF
		[DefaultValue("")]
		[WebCategory("Behavior")]
		[WebSysDescription("")]
		[Localizable(true)]
		public virtual string NullDisplayText
		{
			get
			{
				object obj = base.ViewState["NullDisplayText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				base.ViewState["NullDisplayText"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the URL to an alternate image displayed in an <see cref="T:System.Web.UI.WebControls.ImageField" /> object when the value of the field specified by the <see cref="P:System.Web.UI.WebControls.ImageField.DataImageUrlField" /> property is null.</summary>
		/// <returns>The URL to an alternate image displayed when the value of a field is null. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x060026FF RID: 9983 RVA: 0x00063748 File Offset: 0x00061948
		// (set) Token: 0x06002700 RID: 9984 RVA: 0x00063775 File Offset: 0x00061975
		[UrlProperty]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebCategory("Behavior")]
		[WebSysDescription("")]
		public virtual string NullImageUrl
		{
			get
			{
				object obj = base.ViewState["NullImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["NullImageUrl"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets a value indicating whether the values of the field specified by the <see cref="P:System.Web.UI.WebControls.ImageField.DataImageUrlField" /> property can be modified in edit mode.</summary>
		/// <returns>true to indicate that the field values cannot be modified in edit mode; otherwise, false. The default is false.</returns>
		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x06002701 RID: 9985 RVA: 0x00063790 File Offset: 0x00061990
		// (set) Token: 0x06002702 RID: 9986 RVA: 0x00046AFB File Offset: 0x00044CFB
		[WebCategory("Behavior")]
		[WebSysDescription("")]
		[DefaultValue(false)]
		public virtual bool ReadOnly
		{
			get
			{
				object obj = base.ViewState["ReadOnly"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["ReadOnly"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Fills the specified <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> object with the values from the specified <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> object.</summary>
		/// <param name="dictionary">An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> used to store the values of the specified cell.</param>
		/// <param name="cell">The <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> that contains the values to retrieve.</param>
		/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> values.</param>
		/// <param name="includeReadOnly">true to include the values of read-only fields; otherwise, false.</param>
		// Token: 0x06002703 RID: 9987 RVA: 0x000637BC File Offset: 0x000619BC
		public override void ExtractValuesFromCell(IOrderedDictionary dictionary, DataControlFieldCell cell, DataControlRowState rowState, bool includeReadOnly)
		{
			if ((this.ReadOnly && !includeReadOnly) || cell.Controls.Count == 0)
			{
				return;
			}
			bool flag = (rowState & (DataControlRowState.Edit | DataControlRowState.Insert)) > DataControlRowState.Normal;
			if (includeReadOnly || flag)
			{
				Control control = cell.Controls[0];
				if (control is Image)
				{
					dictionary[this.DataImageUrlField] = ((Image)control).ImageUrl;
					return;
				}
				if (control is TextBox)
				{
					dictionary[this.DataImageUrlField] = ((TextBox)control).Text;
				}
			}
		}

		/// <summary>Initializes the specified <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> object with the specified cell type, row state, and row index.</summary>
		/// <param name="cell">The <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> to initialize. </param>
		/// <param name="cellType">One of the <see cref="T:System.Web.UI.WebControls.DataControlCellType" /> values. </param>
		/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> values. </param>
		/// <param name="rowIndex">The zero-based index of the row. </param>
		// Token: 0x06002704 RID: 9988 RVA: 0x0006383D File Offset: 0x00061A3D
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

		/// <summary>Initializes the specified <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> object with the specified row state.</summary>
		/// <param name="cell">The <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> to initialize. </param>
		/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> values. </param>
		// Token: 0x06002705 RID: 9989 RVA: 0x00063870 File Offset: 0x00061A70
		protected virtual void InitializeDataCell(DataControlFieldCell cell, DataControlRowState rowState)
		{
			if ((rowState & (DataControlRowState.Edit | DataControlRowState.Insert)) > DataControlRowState.Normal && !this.ReadOnly)
			{
				TextBox textBox = new TextBox();
				cell.Controls.Add(textBox);
				return;
			}
			if (this.DataImageUrlField.Length > 0)
			{
				Image image = new Image();
				image.ControlStyle.CopyFrom(base.ControlStyle);
				cell.Controls.Add(image);
			}
		}

		/// <summary>Applies the format specified by the <see cref="P:System.Web.UI.WebControls.ImageField.DataImageUrlFormatString" /> property to a field value.</summary>
		/// <returns>The transformed value.</returns>
		/// <param name="dataValue">The value to transform.</param>
		// Token: 0x06002706 RID: 9990 RVA: 0x000638D2 File Offset: 0x00061AD2
		protected virtual string FormatImageUrlValue(object dataValue)
		{
			if (dataValue == null)
			{
				return null;
			}
			if (this.DataImageUrlFormatString.Length > 0)
			{
				return string.Format(this.DataImageUrlFormatString, dataValue);
			}
			return dataValue.ToString();
		}

		/// <summary>Applies the format specified by the <see cref="P:System.Web.UI.WebControls.ImageField.DataAlternateTextFormatString" /> property to the alternate text value contained in the specified <see cref="T:System.Web.UI.Control" /> object.</summary>
		/// <returns>The transformed value.</returns>
		/// <param name="controlContainer">The <see cref="T:System.Web.UI.Control" /> that contains the alternate text value to transform.</param>
		// Token: 0x06002707 RID: 9991 RVA: 0x000638FC File Offset: 0x00061AFC
		protected virtual string GetFormattedAlternateText(Control controlContainer)
		{
			if (this.DataAlternateTextField.Length <= 0)
			{
				return this.AlternateText;
			}
			if (this.textProperty == null)
			{
				this.textProperty = this.GetProperty(controlContainer, this.DataAlternateTextField);
			}
			object value = this.GetValue(controlContainer, this.DataAlternateTextField, ref this.textProperty);
			if (value == null || (value.ToString().Length == 0 && this.ConvertEmptyStringToNull))
			{
				return this.NullDisplayText;
			}
			if (this.DataAlternateTextFormatString.Length > 0)
			{
				return string.Format(this.DataAlternateTextFormatString, value);
			}
			return value.ToString();
		}

		/// <summary>Retrieves the value of the specified field from the specified control.</summary>
		/// <returns>The value of the specified field.</returns>
		/// <param name="controlContainer">The <see cref="T:System.Web.UI.Control" /> that contains the field value.</param>
		/// <param name="fieldName">The name of the field for which to retrieve the value.</param>
		/// <param name="cachedDescriptor">A <see cref="T:System.ComponentModel.PropertyDescriptor" />, passed by reference, that represents the properties of the field.</param>
		/// <exception cref="T:System.Web.HttpException">The <paramref name="controlContainer" /> parameter is null.- or -The data item associated with the container control is null.- or -The field specified by the <paramref name="fieldName" /> parameter could not be found.</exception>
		// Token: 0x06002708 RID: 9992 RVA: 0x00063990 File Offset: 0x00061B90
		protected virtual object GetValue(Control controlContainer, string fieldName, ref PropertyDescriptor cachedDescriptor)
		{
			if (base.DesignMode)
			{
				return this.GetDesignTimeValue();
			}
			object dataItem = DataBinder.GetDataItem(controlContainer);
			if (dataItem == null)
			{
				throw new HttpException("A data item was not found in the container. The container must either implement IDataItemContainer, or have a property named DataItem.");
			}
			if (fieldName == ImageField.ThisExpression)
			{
				return dataItem;
			}
			if (cachedDescriptor != null)
			{
				return cachedDescriptor.GetValue(dataItem);
			}
			return this.GetProperty(controlContainer, fieldName).GetValue(dataItem);
		}

		// Token: 0x06002709 RID: 9993 RVA: 0x000639EC File Offset: 0x00061BEC
		private PropertyDescriptor GetProperty(Control controlContainer, string fieldName)
		{
			if (fieldName == ImageField.ThisExpression)
			{
				return null;
			}
			IDataItemContainer dataItemContainer = (IDataItemContainer)controlContainer;
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(dataItemContainer.DataItem);
			PropertyDescriptor propertyDescriptor = ((properties != null) ? properties[fieldName] : null);
			if (propertyDescriptor == null)
			{
				string text = "Property '";
				string text2 = "' not found in object of type ";
				Type type = dataItemContainer.DataItem.GetType();
				throw new InvalidOperationException(text + fieldName + text2 + ((type != null) ? type.ToString() : null));
			}
			return propertyDescriptor;
		}

		/// <summary>Retrieves the value used for a field's value when rendering the <see cref="T:System.Web.UI.WebControls.ImageField" /> object in a designer.</summary>
		/// <returns>The value to display in the designer as the field's value.</returns>
		// Token: 0x0600270A RID: 9994 RVA: 0x00046D22 File Offset: 0x00044F22
		protected virtual string GetDesignTimeValue()
		{
			return "Databound";
		}

		/// <summary>Binds the value of a field to the <see cref="T:System.Web.UI.WebControls.ImageField" /> object.</summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		/// <exception cref="T:System.Web.HttpException">The <see cref="T:System.Web.UI.WebControls.ImageField" /> object contains a control that is not a <see cref="T:System.Web.UI.WebControls.TextBox" /> control in edit mode and is not an <see cref="T:System.Web.UI.WebControls.Image" /> control with a <see cref="T:System.Web.UI.WebControls.Label" /> control in read-only mode.</exception>
		// Token: 0x0600270B RID: 9995 RVA: 0x00063A58 File Offset: 0x00061C58
		protected virtual void OnDataBindField(object sender, EventArgs e)
		{
			Control control = (Control)sender;
			ControlCollection controlCollection = ((control != null) ? control.Controls : null);
			Control namingContainer = control.NamingContainer;
			Control control2;
			if (sender is DataControlFieldCell)
			{
				if (controlCollection.Count == 0)
				{
					return;
				}
				control2 = controlCollection[0];
			}
			else
			{
				if (!(sender is Image) && !(sender is TextBox))
				{
					return;
				}
				control2 = control;
			}
			if (this.imageProperty == null)
			{
				this.imageProperty = this.GetProperty(namingContainer, this.DataImageUrlField);
			}
			if (control2 is TextBox)
			{
				object value = this.GetValue(namingContainer, this.DataImageUrlField, ref this.imageProperty);
				((TextBox)control2).Text = ((value != null) ? value.ToString() : string.Empty);
				return;
			}
			if (control2 is Image)
			{
				Image image = (Image)control2;
				string text = this.FormatImageUrlValue(this.GetValue(namingContainer, this.DataImageUrlField, ref this.imageProperty));
				if (text == null || (this.ConvertEmptyStringToNull && text.Length == 0))
				{
					if (this.NullImageUrl == null || this.NullImageUrl.Length == 0)
					{
						control2.Visible = false;
						controlCollection.Add(new Label
						{
							Text = this.NullDisplayText
						});
					}
					else
					{
						text = this.NullImageUrl;
					}
				}
				image.ImageUrl = text;
				image.AlternateText = this.GetFormattedAlternateText(namingContainer);
			}
		}

		/// <summary>Determines whether the controls contained in an <see cref="T:System.Web.UI.WebControls.ImageField" /> object support callbacks.</summary>
		// Token: 0x0600270C RID: 9996 RVA: 0x00003946 File Offset: 0x00001B46
		public override void ValidateSupportsCallback()
		{
		}

		/// <summary>Returns a new instance of the <see cref="T:System.Web.UI.WebControls.ImageField" /> class.</summary>
		/// <returns>A new instance of the <see cref="T:System.Web.UI.WebControls.ImageField" /> class.</returns>
		// Token: 0x0600270D RID: 9997 RVA: 0x00063B9D File Offset: 0x00061D9D
		protected override DataControlField CreateField()
		{
			return new ImageField();
		}

		/// <summary>Copies the properties of the current <see cref="T:System.Web.UI.WebControls.ImageField" /> object to the specified object.</summary>
		/// <param name="newField">The <see cref="T:System.Web.UI.WebControls.DataControlField" />-derived object that receives the copy.</param>
		// Token: 0x0600270E RID: 9998 RVA: 0x00063BA4 File Offset: 0x00061DA4
		protected override void CopyProperties(DataControlField newField)
		{
			base.CopyProperties(newField);
			ImageField imageField = (ImageField)newField;
			imageField.AlternateText = this.AlternateText;
			imageField.ConvertEmptyStringToNull = this.ConvertEmptyStringToNull;
			imageField.DataAlternateTextField = this.DataAlternateTextField;
			imageField.DataAlternateTextFormatString = this.DataAlternateTextFormatString;
			imageField.DataImageUrlField = this.DataImageUrlField;
			imageField.DataImageUrlFormatString = this.DataImageUrlFormatString;
			imageField.NullDisplayText = this.NullDisplayText;
			imageField.NullImageUrl = this.NullImageUrl;
			imageField.ReadOnly = this.ReadOnly;
		}

		/// <summary>Represents the "this" expression.</summary>
		// Token: 0x04001A50 RID: 6736
		public static readonly string ThisExpression = "!";

		// Token: 0x04001A51 RID: 6737
		private PropertyDescriptor imageProperty;

		// Token: 0x04001A52 RID: 6738
		private PropertyDescriptor textProperty;
	}
}

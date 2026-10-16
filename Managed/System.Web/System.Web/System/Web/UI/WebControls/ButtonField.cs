using System;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a field that is displayed as a button in a data-bound control.</summary>
	// Token: 0x0200033D RID: 829
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class ButtonField : ButtonFieldBase
	{
		/// <summary>Gets or sets a string that represents the action to perform when a button in a <see cref="T:System.Web.UI.WebControls.ButtonField" /> object is clicked.</summary>
		/// <returns>The name of the action to perform when a button in the <see cref="T:System.Web.UI.WebControls.ButtonField" /> is clicked.</returns>
		// Token: 0x1700092A RID: 2346
		// (get) Token: 0x06001D8D RID: 7565 RVA: 0x00047BD7 File Offset: 0x00045DD7
		// (set) Token: 0x06001D8E RID: 7566 RVA: 0x00047BEE File Offset: 0x00045DEE
		[WebSysDescription("Raised when a Button Command is executed.")]
		[WebCategory("Behavior")]
		[DefaultValue("")]
		public virtual string CommandName
		{
			get
			{
				return base.ViewState.GetString("CommandName", string.Empty);
			}
			set
			{
				base.ViewState["CommandName"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the name of the data field for which the value is bound to the <see cref="P:System.Web.UI.WebControls.Button.Text" /> property of the <see cref="T:System.Web.UI.WebControls.Button" /> control that is rendered by the <see cref="T:System.Web.UI.WebControls.ButtonField" /> object.</summary>
		/// <returns>The name of the field to bind to the <see cref="T:System.Web.UI.WebControls.ButtonField" />. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.ButtonField.DataTextField" /> property is not set.</returns>
		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x06001D8F RID: 7567 RVA: 0x00047C07 File Offset: 0x00045E07
		// (set) Token: 0x06001D90 RID: 7568 RVA: 0x00047C1E File Offset: 0x00045E1E
		[DefaultValue("")]
		[WebSysDescription("")]
		[WebCategory("Data")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public virtual string DataTextField
		{
			get
			{
				return base.ViewState.GetString("DataTextField", string.Empty);
			}
			set
			{
				base.ViewState["DataTextField"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the string that specifies the display format for the value of the field.</summary>
		/// <returns>A format string that specifies the display format for the value of the field. The default is an empty string (""), which indicates that no special formatting is applied to the field value.</returns>
		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x06001D91 RID: 7569 RVA: 0x00047C37 File Offset: 0x00045E37
		// (set) Token: 0x06001D92 RID: 7570 RVA: 0x00047C4E File Offset: 0x00045E4E
		[DefaultValue("")]
		[WebSysDescription("")]
		[WebCategory("Data")]
		public virtual string DataTextFormatString
		{
			get
			{
				return base.ViewState.GetString("DataTextFormatString", string.Empty);
			}
			set
			{
				base.ViewState["DataTextFormatString"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the image to display for each button in the <see cref="T:System.Web.UI.WebControls.ButtonField" /> object.</summary>
		/// <returns>The image to display for each button in the <see cref="T:System.Web.UI.WebControls.ButtonField" />. The default is an empty string (""), which indicates that the <see cref="P:System.Web.UI.WebControls.ButtonField.ImageUrl" /> property is not set.</returns>
		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x06001D93 RID: 7571 RVA: 0x00047C67 File Offset: 0x00045E67
		// (set) Token: 0x06001D94 RID: 7572 RVA: 0x00047C7E File Offset: 0x00045E7E
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("")]
		[UrlProperty]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public virtual string ImageUrl
		{
			get
			{
				return base.ViewState.GetString("ImageUrl", string.Empty);
			}
			set
			{
				base.ViewState["ImageUrl"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the static caption that is displayed for each button in the <see cref="T:System.Web.UI.WebControls.ButtonField" /> object.</summary>
		/// <returns>The caption displayed for each button in the <see cref="T:System.Web.UI.WebControls.ButtonField" />. The default is an empty string ("").</returns>
		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06001D95 RID: 7573 RVA: 0x00047C97 File Offset: 0x00045E97
		// (set) Token: 0x06001D96 RID: 7574 RVA: 0x00047CAE File Offset: 0x00045EAE
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		[Localizable(true)]
		[DefaultValue("")]
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

		/// <summary>Initializes the current <see cref="T:System.Web.UI.WebControls.ButtonField" /> object.</summary>
		/// <returns>false, which indicates the control does not need to rebind to the data.</returns>
		/// <param name="sortingEnabled">true to enable sorting; otherwise, false. </param>
		/// <param name="control">The data control that owns the <see cref="T:System.Web.UI.WebControls.ButtonField" />. </param>
		// Token: 0x06001D97 RID: 7575 RVA: 0x00046BDB File Offset: 0x00044DDB
		public override bool Initialize(bool sortingEnabled, Control control)
		{
			return base.Initialize(sortingEnabled, control);
		}

		/// <summary>Formats the specified field value for a cell in the <see cref="T:System.Web.UI.WebControls.ButtonField" /> object.</summary>
		/// <returns>The field value converted to the format specified by the <see cref="P:System.Web.UI.WebControls.ButtonField.DataTextFormatString" /> property.</returns>
		/// <param name="dataTextValue">The field value to format. </param>
		// Token: 0x06001D98 RID: 7576 RVA: 0x00047CC7 File Offset: 0x00045EC7
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

		/// <summary>Initializes the specified <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> object to the specified row state.</summary>
		/// <param name="cell">The <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> to initialize.</param>
		/// <param name="cellType">A <see cref="T:System.Web.UI.WebControls.DataControlCellType" /> value that indicates the type of row (header, footer, or data).</param>
		/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> values.</param>
		/// <param name="rowIndex">The zero-based index of the row.</param>
		// Token: 0x06001D99 RID: 7577 RVA: 0x00047CF4 File Offset: 0x00045EF4
		public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
		{
			string text = rowIndex.ToString();
			if (cellType == DataControlCellType.DataCell)
			{
				IDataControlButton dataControlButton = DataControlButton.CreateButton(this.ButtonType, base.Control, this.Text, this.ImageUrl, this.CommandName, text, false);
				if (this.CausesValidation)
				{
					dataControlButton.Container = null;
					dataControlButton.CausesValidation = true;
					dataControlButton.ValidationGroup = this.ValidationGroup;
				}
				if (!string.IsNullOrEmpty(this.DataTextField) && (rowState & DataControlRowState.Insert) == DataControlRowState.Normal)
				{
					cell.DataBinding += this.OnDataBindField;
				}
				cell.Controls.Add((Control)dataControlButton);
				return;
			}
			base.InitializeCell(cell, cellType, rowState, rowIndex);
		}

		// Token: 0x06001D9A RID: 7578 RVA: 0x00047D98 File Offset: 0x00045F98
		private void OnDataBindField(object sender, EventArgs e)
		{
			DataControlFieldCell dataControlFieldCell = (DataControlFieldCell)sender;
			((IDataControlButton)dataControlFieldCell.Controls[0]).Text = this.FormatDataTextValue(this.GetBoundValue(dataControlFieldCell.BindingContainer));
		}

		// Token: 0x06001D9B RID: 7579 RVA: 0x00047DD4 File Offset: 0x00045FD4
		private object GetBoundValue(Control controlContainer)
		{
			IDataItemContainer dataItemContainer = controlContainer as IDataItemContainer;
			if (this.boundProperty == null)
			{
				this.boundProperty = TypeDescriptor.GetProperties(dataItemContainer.DataItem)[this.DataTextField];
				if (this.boundProperty == null)
				{
					string text = "Property '";
					string dataTextField = this.DataTextField;
					string text2 = "' not found in object of type ";
					Type type = dataItemContainer.DataItem.GetType();
					throw new InvalidOperationException(text + dataTextField + text2 + ((type != null) ? type.ToString() : null));
				}
			}
			return this.boundProperty.GetValue(dataItemContainer.DataItem);
		}

		/// <summary>Creates and returns a new instance of the <see cref="T:System.Web.UI.WebControls.ButtonField" /> class.</summary>
		/// <returns>A new instance of the  <see cref="T:System.Web.UI.WebControls.ButtonField" /> class.</returns>
		// Token: 0x06001D9C RID: 7580 RVA: 0x00047E57 File Offset: 0x00046057
		protected override DataControlField CreateField()
		{
			return new ButtonField();
		}

		/// <summary>Copies the properties of the current <see cref="T:System.Web.UI.WebControls.ButtonField" /> object to the specified <see cref="T:System.Web.UI.WebControls.DataControlField" /> object.</summary>
		/// <param name="newField">The <see cref="T:System.Web.UI.WebControls.DataControlField" /> to which to copy the properties of the current <see cref="T:System.Web.UI.WebControls.ButtonField" />.</param>
		// Token: 0x06001D9D RID: 7581 RVA: 0x00047E60 File Offset: 0x00046060
		protected override void CopyProperties(DataControlField newField)
		{
			base.CopyProperties(newField);
			ButtonField buttonField = (ButtonField)newField;
			buttonField.CommandName = this.CommandName;
			buttonField.DataTextField = this.DataTextField;
			buttonField.DataTextFormatString = this.DataTextFormatString;
			buttonField.ImageUrl = this.ImageUrl;
			buttonField.Text = this.Text;
		}

		/// <summary>Determines whether the controls that are contained in a <see cref="T:System.Web.UI.WebControls.ButtonField" /> object support callbacks.</summary>
		// Token: 0x06001D9E RID: 7582 RVA: 0x00003946 File Offset: 0x00001B46
		public override void ValidateSupportsCallback()
		{
		}

		// Token: 0x04001833 RID: 6195
		private PropertyDescriptor boundProperty;
	}
}

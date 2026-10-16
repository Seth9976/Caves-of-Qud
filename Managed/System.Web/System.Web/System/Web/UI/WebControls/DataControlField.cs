using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Serves as the base class for all data control field types, which represent a column of data in tabular data-bound controls such as <see cref="T:System.Web.UI.WebControls.DetailsView" /> and <see cref="T:System.Web.UI.WebControls.GridView" />.</summary>
	// Token: 0x0200036C RID: 876
	[DefaultProperty("HeaderText")]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public abstract class DataControlField : IStateManager, IDataSourceViewSchemaAccessor
	{
		// Token: 0x14000066 RID: 102
		// (add) Token: 0x060020E1 RID: 8417 RVA: 0x00052252 File Offset: 0x00050452
		// (remove) Token: 0x060020E2 RID: 8418 RVA: 0x00052265 File Offset: 0x00050465
		internal event EventHandler FieldChanged
		{
			add
			{
				this.events.AddHandler(DataControlField.fieldChangedEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(DataControlField.fieldChangedEvent, value);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DataControlField" /> class.</summary>
		// Token: 0x060020E3 RID: 8419 RVA: 0x00052278 File Offset: 0x00050478
		protected DataControlField()
		{
			this.viewState = new StateBag();
		}

		// Token: 0x060020E4 RID: 8420 RVA: 0x00052296 File Offset: 0x00050496
		internal void SetDirty()
		{
			this.viewState.SetDirty(true);
		}

		/// <summary>Gets a dictionary of state information that allows you to save and restore the view state of a <see cref="T:System.Web.UI.WebControls.DataControlField" /> object across multiple requests for the same page.</summary>
		/// <returns>An instance of <see cref="T:System.Web.UI.StateBag" /> that contains the <see cref="T:System.Web.UI.WebControls.DataControlField" /> view-state information.</returns>
		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x060020E5 RID: 8421 RVA: 0x000522A4 File Offset: 0x000504A4
		protected StateBag ViewState
		{
			get
			{
				return this.viewState;
			}
		}

		/// <summary>Extracts the value of the data control field from the current table cell and adds the value to the specified <see cref="T:System.Collections.IDictionary" /> collection.</summary>
		/// <param name="dictionary">An <see cref="T:System.Collections.Specialized.IOrderedDictionary" />.</param>
		/// <param name="cell">A <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> that contains the text or controls of the <see cref="T:System.Web.UI.WebControls.DataControlField" />.</param>
		/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> values.</param>
		/// <param name="includeReadOnly">true to indicate that the values of read-only fields are included in the <paramref name="dictionary" /> collection; otherwise, false.</param>
		// Token: 0x060020E6 RID: 8422 RVA: 0x00003946 File Offset: 0x00001B46
		public virtual void ExtractValuesFromCell(IOrderedDictionary dictionary, DataControlFieldCell cell, DataControlRowState rowState, bool includeReadOnly)
		{
		}

		/// <summary>Performs basic instance initialization for a data control field.</summary>
		/// <returns>Always returns false.</returns>
		/// <param name="sortingEnabled">A value that indicates whether the control supports the sorting of columns of data.</param>
		/// <param name="control">The data control that owns the <see cref="T:System.Web.UI.WebControls.DataControlField" />.</param>
		// Token: 0x060020E7 RID: 8423 RVA: 0x000522AC File Offset: 0x000504AC
		public virtual bool Initialize(bool sortingEnabled, Control control)
		{
			this.sortingEnabled = sortingEnabled;
			this.control = control;
			return false;
		}

		/// <summary>Adds text or controls to a cell's controls collection.</summary>
		/// <param name="cell">A <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> that contains the text or controls of the <see cref="T:System.Web.UI.WebControls.DataControlField" />.</param>
		/// <param name="cellType">One of the <see cref="T:System.Web.UI.WebControls.DataControlCellType" /> values.</param>
		/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> values, specifying the state of the row that contains the <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" />.</param>
		/// <param name="rowIndex">The index of the row that the <see cref="T:System.Web.UI.WebControls.DataControlFieldCell" /> is contained in.</param>
		// Token: 0x060020E8 RID: 8424 RVA: 0x000522C0 File Offset: 0x000504C0
		public virtual void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
		{
			if (cellType != DataControlCellType.Header)
			{
				if (cellType == DataControlCellType.Footer)
				{
					string footerText = this.FooterText;
					cell.Text = ((footerText.Length > 0) ? footerText : "&nbsp;");
				}
				return;
			}
			if (this.HeaderText.Length > 0 && this.sortingEnabled && this.SortExpression.Length > 0)
			{
				cell.Controls.Add((Control)DataControlButton.CreateButton(string.IsNullOrEmpty(this.HeaderImageUrl) ? ButtonType.Link : ButtonType.Image, this.control, this.HeaderText, this.HeaderImageUrl, "Sort", this.SortExpression, true));
				return;
			}
			if (this.HeaderImageUrl.Length > 0)
			{
				Image image = new Image();
				image.ImageUrl = this.HeaderImageUrl;
				cell.Controls.Add(image);
				return;
			}
			cell.Text = ((this.HeaderText.Length > 0) ? this.HeaderText : "&nbsp;");
		}

		/// <summary>Creates a duplicate copy of the current <see cref="T:System.Web.UI.WebControls.DataControlField" />-derived object.</summary>
		/// <returns>A duplicate copy of the current <see cref="T:System.Web.UI.WebControls.DataControlField" />.</returns>
		// Token: 0x060020E9 RID: 8425 RVA: 0x000523B0 File Offset: 0x000505B0
		protected internal DataControlField CloneField()
		{
			DataControlField dataControlField = this.CreateField();
			this.CopyProperties(dataControlField);
			return dataControlField;
		}

		/// <summary>When overridden in a derived class, creates an empty <see cref="T:System.Web.UI.WebControls.DataControlField" />-derived object.</summary>
		/// <returns>An empty <see cref="T:System.Web.UI.WebControls.DataControlField" />-derived object.</returns>
		// Token: 0x060020EA RID: 8426
		protected abstract DataControlField CreateField();

		/// <summary>Copies the properties of the current <see cref="T:System.Web.UI.WebControls.DataControlField" />-derived object to the specified <see cref="T:System.Web.UI.WebControls.DataControlField" /> object.</summary>
		/// <param name="newField">The <see cref="T:System.Web.UI.WebControls.DataControlField" /> to which to copy the properties of the current <see cref="T:System.Web.UI.WebControls.DataControlField" />.</param>
		// Token: 0x060020EB RID: 8427 RVA: 0x000523CC File Offset: 0x000505CC
		protected virtual void CopyProperties(DataControlField newField)
		{
			newField.AccessibleHeaderText = this.AccessibleHeaderText;
			newField.ControlStyle.CopyFrom(this.ControlStyle);
			newField.FooterStyle.CopyFrom(this.FooterStyle);
			newField.FooterText = this.FooterText;
			newField.HeaderImageUrl = this.HeaderImageUrl;
			newField.HeaderStyle.CopyFrom(this.HeaderStyle);
			newField.HeaderText = this.HeaderText;
			newField.InsertVisible = this.InsertVisible;
			newField.ItemStyle.CopyFrom(this.ItemStyle);
			newField.ShowHeader = this.ShowHeader;
			newField.SortExpression = this.SortExpression;
			newField.Visible = this.Visible;
		}

		/// <summary>Raises the FieldChanged event.</summary>
		// Token: 0x060020EC RID: 8428 RVA: 0x00052480 File Offset: 0x00050680
		protected virtual void OnFieldChanged()
		{
			EventHandler eventHandler = this.events[DataControlField.fieldChangedEvent] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, EventArgs.Empty);
			}
		}

		/// <summary>Restores the data source view's previously saved view state.</summary>
		/// <param name="savedState">An object that represents the <see cref="T:System.Web.UI.WebControls.DataControlField" /> state to restore.</param>
		// Token: 0x060020ED RID: 8429 RVA: 0x000524B4 File Offset: 0x000506B4
		protected virtual void LoadViewState(object savedState)
		{
			if (savedState == null)
			{
				return;
			}
			object[] array = (object[])savedState;
			this.viewState.LoadViewState(array[0]);
			if (array[1] != null)
			{
				((IStateManager)this.ControlStyle).LoadViewState(array[1]);
			}
			if (array[2] != null)
			{
				((IStateManager)this.FooterStyle).LoadViewState(array[2]);
			}
			if (array[3] != null)
			{
				((IStateManager)this.HeaderStyle).LoadViewState(array[3]);
			}
			if (array[4] != null)
			{
				((IStateManager)this.ItemStyle).LoadViewState(array[4]);
			}
		}

		/// <summary>Saves the changes made to the <see cref="T:System.Web.UI.WebControls.DataControlField" /> view state since the time the page was posted back to the server.</summary>
		/// <returns>The object that contains the changes to the <see cref="T:System.Web.UI.WebControls.DataControlField" /> view state. If there is no view state associated with the object, this method returns null.</returns>
		// Token: 0x060020EE RID: 8430 RVA: 0x00052528 File Offset: 0x00050728
		protected virtual object SaveViewState()
		{
			object[] array = new object[5];
			array[0] = this.viewState.SaveViewState();
			if (this.controlStyle != null)
			{
				array[1] = ((IStateManager)this.controlStyle).SaveViewState();
			}
			if (this.footerStyle != null)
			{
				array[2] = ((IStateManager)this.footerStyle).SaveViewState();
			}
			if (this.headerStyle != null)
			{
				array[3] = ((IStateManager)this.headerStyle).SaveViewState();
			}
			if (this.itemStyle != null)
			{
				array[4] = ((IStateManager)this.itemStyle).SaveViewState();
			}
			if (array[0] == null && array[1] == null && array[2] == null && array[3] == null && array[4] == null)
			{
				return null;
			}
			return array;
		}

		/// <summary>Causes the <see cref="T:System.Web.UI.WebControls.DataControlField" /> object to track changes to its view state so they can be stored in the control's <see cref="P:System.Web.UI.WebControls.DataControlField.ViewState" /> property and persisted across requests for the same page.</summary>
		// Token: 0x060020EF RID: 8431 RVA: 0x000525C0 File Offset: 0x000507C0
		protected virtual void TrackViewState()
		{
			if (this.controlStyle != null)
			{
				((IStateManager)this.controlStyle).TrackViewState();
			}
			if (this.footerStyle != null)
			{
				((IStateManager)this.footerStyle).TrackViewState();
			}
			if (this.headerStyle != null)
			{
				((IStateManager)this.headerStyle).TrackViewState();
			}
			if (this.itemStyle != null)
			{
				((IStateManager)this.itemStyle).TrackViewState();
			}
			this.viewState.TrackViewState();
			this.tracking = true;
		}

		/// <summary>When overridden in a derived class, signals that the controls contained by a field support callbacks.</summary>
		/// <exception cref="T:System.NotSupportedException">The method is called on a default instance of the <see cref="T:System.Web.UI.WebControls.DataControlField" /> class.</exception>
		// Token: 0x060020F0 RID: 8432 RVA: 0x0005262B File Offset: 0x0005082B
		public virtual void ValidateSupportsCallback()
		{
			throw new NotSupportedException("Callback not supported");
		}

		/// <summary>Restores the data control field's previously saved view state.</summary>
		/// <param name="state">An <see cref="T:System.Object" /> that contains the saved view state values for the control.</param>
		// Token: 0x060020F1 RID: 8433 RVA: 0x00052637 File Offset: 0x00050837
		void IStateManager.LoadViewState(object savedState)
		{
			this.LoadViewState(savedState);
		}

		/// <summary>Saves the changes made to the <see cref="T:System.Web.UI.WebControls.DataControlField" /> view state since the time the page was posted back to the server.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the saved view state values for the control.</returns>
		// Token: 0x060020F2 RID: 8434 RVA: 0x00052640 File Offset: 0x00050840
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		/// <summary>Causes the <see cref="T:System.Web.UI.WebControls.DataControlField" /> object to track changes to its view state so they can be stored in the control's <see cref="P:System.Web.UI.WebControls.DataControlField.ViewState" /> property and persisted across requests for the same page.</summary>
		// Token: 0x060020F3 RID: 8435 RVA: 0x00052648 File Offset: 0x00050848
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		// Token: 0x060020F4 RID: 8436 RVA: 0x00052650 File Offset: 0x00050850
		internal Exception GetNotSupportedPropException(string propName)
		{
			return new NotSupportedException("The property '" + propName + "' is not supported in " + base.GetType().Name);
		}

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x060020F5 RID: 8437 RVA: 0x00052672 File Offset: 0x00050872
		internal bool ControlStyleCreated
		{
			get
			{
				return this.controlStyle != null;
			}
		}

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x060020F6 RID: 8438 RVA: 0x0005267D File Offset: 0x0005087D
		internal bool HeaderStyleCreated
		{
			get
			{
				return this.headerStyle != null;
			}
		}

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x060020F7 RID: 8439 RVA: 0x00052688 File Offset: 0x00050888
		internal bool FooterStyleCreated
		{
			get
			{
				return this.footerStyle != null;
			}
		}

		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x060020F8 RID: 8440 RVA: 0x00052693 File Offset: 0x00050893
		internal bool ItemStyleCreated
		{
			get
			{
				return this.itemStyle != null;
			}
		}

		/// <summary>Gets or sets text that is rendered as the AbbreviatedText property value in some controls.</summary>
		/// <returns>A string that represents abbreviated text read by screen readers. The default value is an empty string ("").</returns>
		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x060020F9 RID: 8441 RVA: 0x000526A0 File Offset: 0x000508A0
		// (set) Token: 0x060020FA RID: 8442 RVA: 0x000526CD File Offset: 0x000508CD
		[Localizable(true)]
		[WebCategory("Accessibility")]
		[global::System.MonoTODO("Render this")]
		[DefaultValue("")]
		public virtual string AccessibleHeaderText
		{
			get
			{
				object obj = this.viewState["accessibleHeaderText"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.viewState["accessibleHeaderText"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets a reference to the data control that the <see cref="T:System.Web.UI.WebControls.DataControlField" /> object is associated with.</summary>
		/// <returns>The data control that owns the <see cref="T:System.Web.UI.WebControls.DataControlField" />.</returns>
		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x060020FB RID: 8443 RVA: 0x000526E6 File Offset: 0x000508E6
		protected Control Control
		{
			get
			{
				return this.control;
			}
		}

		/// <summary>Gets the style of any Web server controls contained by the <see cref="T:System.Web.UI.WebControls.DataControlField" /> object.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> that governs the appearance of Web server controls contained by the field.</returns>
		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x060020FC RID: 8444 RVA: 0x000526EE File Offset: 0x000508EE
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[WebCategory("Styles")]
		public Style ControlStyle
		{
			get
			{
				if (this.controlStyle == null)
				{
					this.controlStyle = new Style();
					if (this.IsTrackingViewState)
					{
						this.controlStyle.TrackViewState();
					}
				}
				return this.controlStyle;
			}
		}

		/// <summary>Gets a value indicating whether a data control field is currently viewed in a design-time environment.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.DataControlField" /> is currently viewed in a design-time environment; otherwise, false.</returns>
		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x060020FD RID: 8445 RVA: 0x0005271C File Offset: 0x0005091C
		protected bool DesignMode
		{
			get
			{
				return this.control != null && this.control.Site != null && this.control.Site.DesignMode;
			}
		}

		/// <summary>Gets or sets the style of the footer of the data control field.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that governs the appearance of the footer item of the <see cref="T:System.Web.UI.WebControls.DataControlField" />.</returns>
		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x060020FE RID: 8446 RVA: 0x00052745 File Offset: 0x00050945
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[WebCategory("Styles")]
		public TableItemStyle FooterStyle
		{
			get
			{
				if (this.footerStyle == null)
				{
					this.footerStyle = new TableItemStyle();
					if (this.IsTrackingViewState)
					{
						this.footerStyle.TrackViewState();
					}
				}
				return this.footerStyle;
			}
		}

		/// <summary>Gets or sets the text that is displayed in the footer item of a data control field.</summary>
		/// <returns>A string that is displayed in the footer item of the <see cref="T:System.Web.UI.WebControls.DataControlField" />.</returns>
		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x060020FF RID: 8447 RVA: 0x00052774 File Offset: 0x00050974
		// (set) Token: 0x06002100 RID: 8448 RVA: 0x000527A1 File Offset: 0x000509A1
		[Localizable(true)]
		[DefaultValue("")]
		[WebCategory("Appearance")]
		public virtual string FooterText
		{
			get
			{
				object obj = this.viewState["footerText"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.viewState["footerText"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the URL of an image that is displayed in the header item of a data control field.</summary>
		/// <returns>A string that represents a fully qualified or relative URL to an image that is displayed in the header item of the <see cref="T:System.Web.UI.WebControls.DataControlField" />.</returns>
		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x06002101 RID: 8449 RVA: 0x000527BC File Offset: 0x000509BC
		// (set) Token: 0x06002102 RID: 8450 RVA: 0x000527E9 File Offset: 0x000509E9
		[UrlProperty]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebCategory("Appearance")]
		public virtual string HeaderImageUrl
		{
			get
			{
				object obj = this.viewState["headerImageUrl"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.viewState["headerImageUrl"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets the style of the header of the data control field.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that governs the appearance of the <see cref="T:System.Web.UI.WebControls.DataControlField" /> header item.</returns>
		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x06002103 RID: 8451 RVA: 0x00052802 File Offset: 0x00050A02
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[WebCategory("Styles")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		public TableItemStyle HeaderStyle
		{
			get
			{
				if (this.headerStyle == null)
				{
					this.headerStyle = new TableItemStyle();
					if (this.IsTrackingViewState)
					{
						this.headerStyle.TrackViewState();
					}
				}
				return this.headerStyle;
			}
		}

		/// <summary>Gets or sets the text that is displayed in the header item of a data control field.</summary>
		/// <returns>A string that is displayed in the header item of the <see cref="T:System.Web.UI.WebControls.DataControlField" />.</returns>
		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x06002104 RID: 8452 RVA: 0x00052830 File Offset: 0x00050A30
		// (set) Token: 0x06002105 RID: 8453 RVA: 0x0005285D File Offset: 0x00050A5D
		[Localizable(true)]
		[WebCategory("Appearance")]
		[DefaultValue("")]
		public virtual string HeaderText
		{
			get
			{
				object obj = this.viewState["headerText"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.viewState["headerText"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.DataControlField" /> object is visible when its parent data-bound control is in insert mode.</summary>
		/// <returns>true if the field is visible when its parent data-bound control is rendered in insert mode; otherwise, false. The default value is true.</returns>
		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x06002106 RID: 8454 RVA: 0x00052878 File Offset: 0x00050A78
		// (set) Token: 0x06002107 RID: 8455 RVA: 0x000528A1 File Offset: 0x00050AA1
		[DefaultValue(true)]
		[WebCategory("Behavior")]
		public virtual bool InsertVisible
		{
			get
			{
				object obj = this.viewState["InsertVisible"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.viewState["InsertVisible"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets the style of any text-based content displayed by a data control field.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that governs the appearance of text displayed in a <see cref="T:System.Web.UI.WebControls.DataControlField" />.</returns>
		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x06002108 RID: 8456 RVA: 0x000528BF File Offset: 0x00050ABF
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Styles")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TableItemStyle ItemStyle
		{
			get
			{
				if (this.itemStyle == null)
				{
					this.itemStyle = new TableItemStyle();
					if (this.IsTrackingViewState)
					{
						this.itemStyle.TrackViewState();
					}
				}
				return this.itemStyle;
			}
		}

		/// <summary>Gets or sets a value indicating whether the header item of a data control field is rendered.</summary>
		/// <returns>true if the header item of the <see cref="T:System.Web.UI.WebControls.DataControlField" /> is rendered; otherwise, false. The default is true.</returns>
		// Token: 0x17000A71 RID: 2673
		// (get) Token: 0x06002109 RID: 8457 RVA: 0x000528F0 File Offset: 0x00050AF0
		// (set) Token: 0x0600210A RID: 8458 RVA: 0x00052919 File Offset: 0x00050B19
		[WebCategory("Behavior")]
		[DefaultValue(true)]
		public virtual bool ShowHeader
		{
			get
			{
				object obj = this.viewState["showHeader"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.viewState["showHeader"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets a sort expression that is used by a data source control to sort data.</summary>
		/// <returns>A sort expression that is used by a data source control to sort data. The default value is an empty string ("").</returns>
		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x0600210B RID: 8459 RVA: 0x00052938 File Offset: 0x00050B38
		// (set) Token: 0x0600210C RID: 8460 RVA: 0x00052965 File Offset: 0x00050B65
		[DefaultValue("")]
		[WebCategory("Behavior")]
		public virtual string SortExpression
		{
			get
			{
				object obj = this.viewState["sortExpression"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				this.viewState["sortExpression"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets or sets a value indicating whether a data control field is rendered.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.DataControlField" /> is rendered; otherwise, false. The default value is true.</returns>
		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x0600210D RID: 8461 RVA: 0x00052980 File Offset: 0x00050B80
		// (set) Token: 0x0600210E RID: 8462 RVA: 0x000529A9 File Offset: 0x00050BA9
		[WebCategory("Behavior")]
		[DefaultValue(true)]
		public bool Visible
		{
			get
			{
				object obj = this.viewState["visible"];
				return obj == null || (bool)obj;
			}
			set
			{
				if (value == this.Visible)
				{
					return;
				}
				this.viewState["visible"] = value;
				this.OnFieldChanged();
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.DataControlField" /> object is saving changes to its view state.</summary>
		/// <returns>true if the data source view is marked to save its state; otherwise, false.</returns>
		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x0600210F RID: 8463 RVA: 0x000529D1 File Offset: 0x00050BD1
		protected bool IsTrackingViewState
		{
			get
			{
				return this.tracking;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.DataControlField" /> object is saving changes to its view state.</summary>
		/// <returns>true to indicate that the <see cref="T:System.Web.UI.WebControls.DataControlField" /> is saving changes to its view state; otherwise, false.</returns>
		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x06002110 RID: 8464 RVA: 0x000529D9 File Offset: 0x00050BD9
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.IsTrackingViewState;
			}
		}

		/// <summary>Gets or sets the schema associated with this <see cref="T:System.Web.UI.WebControls.DataControlField" /> object.</summary>
		/// <returns>The schema associated with this <see cref="T:System.Web.UI.WebControls.DataControlField" />.</returns>
		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x06002111 RID: 8465 RVA: 0x000529E1 File Offset: 0x00050BE1
		// (set) Token: 0x06002112 RID: 8466 RVA: 0x000529F3 File Offset: 0x00050BF3
		object IDataSourceViewSchemaAccessor.DataSourceViewSchema
		{
			get
			{
				return this.viewState["dataSourceViewSchema"];
			}
			set
			{
				this.viewState["dataSourceViewSchema"] = value;
			}
		}

		/// <summary>Returns a string that represents this <see cref="T:System.Web.UI.WebControls.DataControlField" /> object.</summary>
		/// <returns>A string that represents this <see cref="T:System.Web.UI.WebControls.DataControlField" />.</returns>
		// Token: 0x06002113 RID: 8467 RVA: 0x00052A06 File Offset: 0x00050C06
		public override string ToString()
		{
			if (string.IsNullOrEmpty(this.HeaderText))
			{
				return base.ToString();
			}
			return this.HeaderText;
		}

		/// <summary>Gets or sets a value that specifies whether the control validates client input.</summary>
		/// <returns>true if the control validates client input; otherwise, false.</returns>
		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x06002115 RID: 8469 RVA: 0x00052A30 File Offset: 0x00050C30
		// (set) Token: 0x06002116 RID: 8470 RVA: 0x0000B3D8 File Offset: 0x000095D8
		protected internal virtual ValidateRequestMode ValidateRequestMode
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

		// Token: 0x040018B8 RID: 6328
		private static readonly object fieldChangedEvent = new object();

		// Token: 0x040018B9 RID: 6329
		private bool tracking;

		// Token: 0x040018BA RID: 6330
		private StateBag viewState;

		// Token: 0x040018BB RID: 6331
		private Control control;

		// Token: 0x040018BC RID: 6332
		private Style controlStyle;

		// Token: 0x040018BD RID: 6333
		private TableItemStyle footerStyle;

		// Token: 0x040018BE RID: 6334
		private TableItemStyle headerStyle;

		// Token: 0x040018BF RID: 6335
		private TableItemStyle itemStyle;

		// Token: 0x040018C0 RID: 6336
		private bool sortingEnabled;

		// Token: 0x040018C1 RID: 6337
		private EventHandlerList events = new EventHandlerList();
	}
}

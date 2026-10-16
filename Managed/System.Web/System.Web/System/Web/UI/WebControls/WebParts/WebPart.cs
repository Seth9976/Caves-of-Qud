using System;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Serves as the base class for custom ASP.NET Web Parts controls, adding to the base <see cref="T:System.Web.UI.WebControls.WebParts.Part" /> class features some additional user interface (UI) properties, the ability to create connections, and personalization behavior. </summary>
	// Token: 0x02000485 RID: 1157
	public abstract class WebPart : Part, IWebPart, IWebActionable, IWebEditable
	{
		/// <summary>Initializes the class for use by an inherited class instance. This constructor can only be called by an inherited class.</summary>
		// Token: 0x06003458 RID: 13400 RVA: 0x00088540 File Offset: 0x00086740
		protected WebPart()
		{
			this.verbs = new WebPartVerbCollection();
			this.allow = WebPart.Allow.Close | WebPart.Allow.Connect | WebPart.Allow.Edit | WebPart.Allow.Hide | WebPart.Allow.Minimize | WebPart.Allow.ZoneChange;
			this.auth_filter = "";
			this.catalog_icon_url = "";
			this.titleIconImageUrl = string.Empty;
			this.titleUrl = string.Empty;
			this.helpUrl = string.Empty;
			this.isStatic = false;
			this.hasUserData = false;
			this.hasSharedData = false;
			this.hidden = false;
			this.isClosed = false;
		}

		/// <summary>Sets a flag indicating that personalization data has changed for the current <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control instance. </summary>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.Web.UI.WebControls.WebParts.WebPart.WebPartManager" /> is null.</exception>
		// Token: 0x06003459 RID: 13401 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		protected void SetPersonalizationDirty()
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets a flag indicating that personalization data has changed for the specified server control that resides in a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> zone.</summary>
		/// <param name="control">The <see cref="T:System.Web.UI.Control" /> for which the personalization data has changed.</param>
		/// <exception cref="T:System.ArgumentNullException">The object in the <paramref name="control" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The control is not associated with a page.</exception>
		/// <exception cref="T:System.InvalidOperationException">The page associated with the control does not have a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" />.</exception>
		/// <exception cref="T:System.ArgumentException">The control derives from <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" />. Controls that derive from <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> should use the protected <see cref="M:System.Web.UI.WebControls.WebParts.WebPart.SetPersonalizationDirty" /> method instead. </exception>
		// Token: 0x0600345A RID: 13402 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		public static void SetPersonalizationDirty(Control control)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600345B RID: 13403 RVA: 0x000885D4 File Offset: 0x000867D4
		protected override void TrackViewState()
		{
			base.TrackViewState();
			foreach (object obj in this.verbs)
			{
				((IStateManager)obj).TrackViewState();
			}
		}

		// Token: 0x0600345C RID: 13404 RVA: 0x00088630 File Offset: 0x00086830
		internal void SetZoneIndex(int index)
		{
			this.zoneIndex = index;
		}

		/// <summary>Enables derived classes to provide custom handling when a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control is closed on a Web Parts page.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600345D RID: 13405 RVA: 0x00003946 File Offset: 0x00001B46
		protected internal virtual void OnClosing(EventArgs e)
		{
		}

		/// <summary>Enables derived classes to provide custom handling when a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control is beginning or ending the process of connecting to other controls.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600345E RID: 13406 RVA: 0x00003946 File Offset: 0x00001B46
		protected internal virtual void OnConnectModeChanged(EventArgs e)
		{
		}

		/// <summary>Enables derived classes to provide custom handling when a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control is permanently removed from a Web Parts page.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600345F RID: 13407 RVA: 0x00003946 File Offset: 0x00001B46
		protected internal virtual void OnDeleting(EventArgs e)
		{
		}

		/// <summary>Enables derived classes to provide custom handling when a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control is entering or leaving edit mode.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06003460 RID: 13408 RVA: 0x00003946 File Offset: 0x00001B46
		protected internal virtual void OnEditModeChanged(EventArgs e)
		{
		}

		/// <summary>Gets or sets a value indicating whether an end user can close a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control on a Web page.</summary>
		/// <returns>true if the control can be closed on a Web page; otherwise, false. The default value is true.</returns>
		// Token: 0x17001081 RID: 4225
		// (get) Token: 0x06003461 RID: 13409 RVA: 0x00088639 File Offset: 0x00086839
		// (set) Token: 0x06003462 RID: 13410 RVA: 0x00088646 File Offset: 0x00086846
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public virtual bool AllowClose
		{
			get
			{
				return (this.allow & WebPart.Allow.Close) > (WebPart.Allow)0;
			}
			set
			{
				if (value)
				{
					this.allow |= WebPart.Allow.Close;
					return;
				}
				this.allow &= ~WebPart.Allow.Close;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control allows other controls to form connections with it.</summary>
		/// <returns>A Boolean value that indicates whether connections can be formed with the control. The default is true.</returns>
		// Token: 0x17001082 RID: 4226
		// (get) Token: 0x06003463 RID: 13411 RVA: 0x00088669 File Offset: 0x00086869
		// (set) Token: 0x06003464 RID: 13412 RVA: 0x00088676 File Offset: 0x00086876
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public virtual bool AllowConnect
		{
			get
			{
				return (this.allow & WebPart.Allow.Connect) > (WebPart.Allow)0;
			}
			set
			{
				if (value)
				{
					this.allow |= WebPart.Allow.Connect;
					return;
				}
				this.allow &= ~WebPart.Allow.Connect;
			}
		}

		/// <summary>Gets or sets a value indicating whether an end user can modify a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control through the user interface (UI) provided by one or more <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control can be modified; otherwise, false. The default value is true.</returns>
		// Token: 0x17001083 RID: 4227
		// (get) Token: 0x06003465 RID: 13413 RVA: 0x00088699 File Offset: 0x00086899
		// (set) Token: 0x06003466 RID: 13414 RVA: 0x000886A6 File Offset: 0x000868A6
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public virtual bool AllowEdit
		{
			get
			{
				return (this.allow & WebPart.Allow.Edit) > (WebPart.Allow)0;
			}
			set
			{
				if (value)
				{
					this.allow |= WebPart.Allow.Edit;
					return;
				}
				this.allow &= ~WebPart.Allow.Edit;
			}
		}

		/// <summary>Gets or sets a value indicating whether end users are allowed to hide a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control can be hidden; otherwise, false. The default value is true.</returns>
		// Token: 0x17001084 RID: 4228
		// (get) Token: 0x06003467 RID: 13415 RVA: 0x000886C9 File Offset: 0x000868C9
		// (set) Token: 0x06003468 RID: 13416 RVA: 0x000886D6 File Offset: 0x000868D6
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public virtual bool AllowHide
		{
			get
			{
				return (this.allow & WebPart.Allow.Hide) > (WebPart.Allow)0;
			}
			set
			{
				if (value)
				{
					this.allow |= WebPart.Allow.Hide;
					return;
				}
				this.allow &= ~WebPart.Allow.Hide;
			}
		}

		/// <summary>Gets or sets a value indicating whether end users can minimize a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control can be minimized; otherwise, false. The default value is true.</returns>
		// Token: 0x17001085 RID: 4229
		// (get) Token: 0x06003469 RID: 13417 RVA: 0x000886F9 File Offset: 0x000868F9
		// (set) Token: 0x0600346A RID: 13418 RVA: 0x00088707 File Offset: 0x00086907
		[WebCategory("Behavior")]
		[WebSysDescription("")]
		public virtual bool AllowMinimize
		{
			get
			{
				return (this.allow & WebPart.Allow.Minimize) > (WebPart.Allow)0;
			}
			set
			{
				if (value)
				{
					this.allow |= WebPart.Allow.Minimize;
					return;
				}
				this.allow &= ~WebPart.Allow.Minimize;
			}
		}

		/// <summary>Gets or sets a value indicating whether a user can move a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control between <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> zones.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control can move between zones; otherwise, false. The default value is true.</returns>
		// Token: 0x17001086 RID: 4230
		// (get) Token: 0x0600346B RID: 13419 RVA: 0x0008872B File Offset: 0x0008692B
		// (set) Token: 0x0600346C RID: 13420 RVA: 0x00088739 File Offset: 0x00086939
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		public virtual bool AllowZoneChange
		{
			get
			{
				return (this.allow & WebPart.Allow.ZoneChange) > (WebPart.Allow)0;
			}
			set
			{
				if (value)
				{
					this.allow |= WebPart.Allow.ZoneChange;
					return;
				}
				this.allow &= ~WebPart.Allow.ZoneChange;
			}
		}

		/// <summary>Gets or sets an arbitrary string to determine whether a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control is authorized to be added to a page. </summary>
		/// <returns>A string that authorizes a control to be added to a Web page. The default value is an empty string ("").</returns>
		// Token: 0x17001087 RID: 4231
		// (get) Token: 0x0600346D RID: 13421 RVA: 0x0008875D File Offset: 0x0008695D
		// (set) Token: 0x0600346E RID: 13422 RVA: 0x00088765 File Offset: 0x00086965
		public virtual string AuthorizationFilter
		{
			get
			{
				return this.auth_filter;
			}
			set
			{
				this.auth_filter = value;
			}
		}

		/// <summary>Gets or sets the URL to an image that represents a Web Parts control in a catalog of controls. </summary>
		/// <returns>A string that represents the URL to an image used to represent the control in a catalog. The default value is an empty string ("").</returns>
		/// <exception cref="T:System.ArgumentException">The internal validation system has determined that the URL might contain script attacks.</exception>
		// Token: 0x17001088 RID: 4232
		// (get) Token: 0x0600346F RID: 13423 RVA: 0x0008876E File Offset: 0x0008696E
		// (set) Token: 0x06003470 RID: 13424 RVA: 0x00088776 File Offset: 0x00086976
		public virtual string CatalogIconImageUrl
		{
			get
			{
				return this.catalog_icon_url;
			}
			set
			{
				this.catalog_icon_url = value;
			}
		}

		/// <summary>Gets or sets whether a part control is in a minimized or normal state.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.WebParts.PartChromeState" /> values. The default is <see cref="F:System.Web.UI.WebControls.WebParts.PartChromeState.Normal" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified is not one of the <see cref="T:System.Web.UI.WebControls.WebParts.PartChromeState" /> values. </exception>
		// Token: 0x17001089 RID: 4233
		// (get) Token: 0x06003471 RID: 13425 RVA: 0x0008877F File Offset: 0x0008697F
		// (set) Token: 0x06003472 RID: 13426 RVA: 0x00088787 File Offset: 0x00086987
		public override PartChromeState ChromeState
		{
			get
			{
				return base.ChromeState;
			}
			set
			{
				base.ChromeState = value;
			}
		}

		/// <summary>Gets or sets the type of border that frames a Web Parts control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.WebParts.PartChromeType" /> values. The default is <see cref="F:System.Web.UI.WebControls.WebParts.PartChromeType.Default" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value is not one of the <see cref="T:System.Web.UI.WebControls.WebParts.PartChromeType" /> values. </exception>
		// Token: 0x1700108A RID: 4234
		// (get) Token: 0x06003473 RID: 13427 RVA: 0x00088790 File Offset: 0x00086990
		// (set) Token: 0x06003474 RID: 13428 RVA: 0x00088798 File Offset: 0x00086998
		public override PartChromeType ChromeType
		{
			get
			{
				return base.ChromeType;
			}
			set
			{
				base.ChromeType = value;
			}
		}

		/// <summary>Gets an error message to display to users if errors occur during the connection process.</summary>
		/// <returns>A string that contains the error message.</returns>
		// Token: 0x1700108B RID: 4235
		// (get) Token: 0x06003475 RID: 13429 RVA: 0x00018A33 File Offset: 0x00016C33
		[global::System.MonoTODO("Not implemented")]
		public string ConnectErrorMessage
		{
			get
			{
				return "";
			}
		}

		/// <summary>Gets or sets a brief phrase that summarizes what the part control does, for use in ToolTips and catalogs of part controls.</summary>
		/// <returns>A string that briefly summarizes the part control's functionality. The default value is an empty string ("").</returns>
		// Token: 0x1700108C RID: 4236
		// (get) Token: 0x06003476 RID: 13430 RVA: 0x000887A1 File Offset: 0x000869A1
		// (set) Token: 0x06003477 RID: 13431 RVA: 0x000887A9 File Offset: 0x000869A9
		public override string Description
		{
			get
			{
				return base.Description;
			}
			set
			{
				base.Description = value;
			}
		}

		/// <summary>Gets or sets the horizontal direction that content flows within the control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ContentDirection" /> that indicates the horizontal direction content will flow.</returns>
		// Token: 0x1700108D RID: 4237
		// (get) Token: 0x06003478 RID: 13432 RVA: 0x00003A2B File Offset: 0x00001C2B
		// (set) Token: 0x06003479 RID: 13433 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO("Not implemented")]
		public override ContentDirection Direction
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a string that contains the full title text actually displayed in the title bar of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control instance.</summary>
		/// <returns>A string that represents the complete, displayed title of the control. The default value is an empty string ("").</returns>
		// Token: 0x1700108E RID: 4238
		// (get) Token: 0x0600347A RID: 13434 RVA: 0x000887B2 File Offset: 0x000869B2
		public string DisplayTitle
		{
			get
			{
				return "Untitled";
			}
		}

		/// <summary>Gets or sets whether all, some, or none of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control's properties can be exported. </summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartExportMode" /> values. The default is <see cref="F:System.Web.UI.WebControls.WebParts.WebPartExportMode.None" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified is not one of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartExportMode" /> values.</exception>
		/// <exception cref="T:System.InvalidOperationException">The control is already loaded and the personalization scope of the control is set to the <see cref="F:System.Web.UI.WebControls.WebParts.PersonalizationScope.User" /> scope.</exception>
		// Token: 0x1700108F RID: 4239
		// (get) Token: 0x0600347B RID: 13435 RVA: 0x000887B9 File Offset: 0x000869B9
		// (set) Token: 0x0600347C RID: 13436 RVA: 0x000887C1 File Offset: 0x000869C1
		public virtual WebPartExportMode ExportMode
		{
			get
			{
				return this.exportMode;
			}
			set
			{
				this.exportMode = value;
			}
		}

		/// <summary>Gets a value that indicates whether a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control has any shared personalization data associated with it.</summary>
		/// <returns>A Boolean value that indicates whether the control has shared personalization data.</returns>
		// Token: 0x17001090 RID: 4240
		// (get) Token: 0x0600347D RID: 13437 RVA: 0x000887CA File Offset: 0x000869CA
		public bool HasSharedData
		{
			get
			{
				return this.hasSharedData;
			}
		}

		/// <summary>Gets a value that indicates whether a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control has any user personalization data associated with it.</summary>
		/// <returns>A Boolean value that indicates whether the control has any user personalization data.</returns>
		// Token: 0x17001091 RID: 4241
		// (get) Token: 0x0600347E RID: 13438 RVA: 0x000887D2 File Offset: 0x000869D2
		public bool HasUserData
		{
			get
			{
				return this.hasUserData;
			}
		}

		/// <summary>Gets or sets the height of a zone.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Unit" /> object that indicates the height of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZone" />. The default type of a <see cref="T:System.Web.UI.WebControls.Unit" /> is pixels, as indicated by the <see cref="P:System.Web.UI.WebControls.Unit.Type" /> property.</returns>
		// Token: 0x17001092 RID: 4242
		// (get) Token: 0x0600347F RID: 13439 RVA: 0x000887DA File Offset: 0x000869DA
		// (set) Token: 0x06003480 RID: 13440 RVA: 0x000887E2 File Offset: 0x000869E2
		public override Unit Height
		{
			get
			{
				return base.Height;
			}
			set
			{
				base.Height = value;
			}
		}

		/// <summary>Gets or sets the type of user interface (UI) used to display Help content for a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartHelpMode" /> values. The default is <see cref="F:System.Web.UI.WebControls.WebParts.WebPartHelpMode.Modal" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified is not one of the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartHelpMode" /> values.</exception>
		// Token: 0x17001093 RID: 4243
		// (get) Token: 0x06003481 RID: 13441 RVA: 0x000887EB File Offset: 0x000869EB
		// (set) Token: 0x06003482 RID: 13442 RVA: 0x000887F3 File Offset: 0x000869F3
		public virtual WebPartHelpMode HelpMode
		{
			get
			{
				return this.helpMode;
			}
			set
			{
				this.helpMode = value;
			}
		}

		/// <summary>Gets or sets the URL to a Help file for a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</summary>
		/// <returns>A string that represents the URL to a Help file. The default value is an empty string ("").</returns>
		/// <exception cref="T:System.ArgumentException">The internal validation system has determined that the URL might contain script attacks.</exception>
		// Token: 0x17001094 RID: 4244
		// (get) Token: 0x06003483 RID: 13443 RVA: 0x000887FC File Offset: 0x000869FC
		// (set) Token: 0x06003484 RID: 13444 RVA: 0x00088804 File Offset: 0x00086A04
		public virtual string HelpUrl
		{
			get
			{
				return this.helpUrl;
			}
			set
			{
				this.helpUrl = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control is displayed on a Web page.</summary>
		/// <returns>false if the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control is displayed on a Web page; otherwise, true. The default value is false.</returns>
		// Token: 0x17001095 RID: 4245
		// (get) Token: 0x06003485 RID: 13445 RVA: 0x0008880D File Offset: 0x00086A0D
		// (set) Token: 0x06003486 RID: 13446 RVA: 0x00088815 File Offset: 0x00086A15
		public virtual bool Hidden
		{
			get
			{
				return this.hidden;
			}
			set
			{
				this.hidden = value;
			}
		}

		/// <summary>Gets or sets an error message that is used if errors occur when a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control is imported.</summary>
		/// <returns>A string that contains the error message. The default value is a standard error message supplied by the Web Parts control set. </returns>
		// Token: 0x17001096 RID: 4246
		// (get) Token: 0x06003487 RID: 13447 RVA: 0x0008881E File Offset: 0x00086A1E
		// (set) Token: 0x06003488 RID: 13448 RVA: 0x00088835 File Offset: 0x00086A35
		public virtual string ImportErrorMessage
		{
			get
			{
				return this.ViewState.GetString("ImportErrorMessage", "Cannot import this Web Part.");
			}
			set
			{
				this.ViewState["ImportErrorMessage"] = value;
			}
		}

		/// <summary>Gets a value that indicates whether a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control is currently closed on a Web Parts page.</summary>
		/// <returns>A Boolean value that indicates whether the control is closed.</returns>
		// Token: 0x17001097 RID: 4247
		// (get) Token: 0x06003489 RID: 13449 RVA: 0x00088848 File Offset: 0x00086A48
		public bool IsClosed
		{
			get
			{
				return this.isClosed;
			}
		}

		/// <summary>Gets a value that indicates whether a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control is shared, meaning that it is visible to all users of a Web Parts page.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control has shared user visibility on a Web page; otherwise, false. The default value is false.</returns>
		// Token: 0x17001098 RID: 4248
		// (get) Token: 0x0600348A RID: 13450 RVA: 0x00008A4D File Offset: 0x00006C4D
		public bool IsShared
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control is standalone, meaning that it is not contained within a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> zone.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control is not contained in a <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> zone; otherwise, false. </returns>
		// Token: 0x17001099 RID: 4249
		// (get) Token: 0x0600348B RID: 13451 RVA: 0x00008B4A File Offset: 0x00006D4A
		public bool IsStandalone
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value that indicates whether a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control is a static control, which means the control is declared in the markup of a Web Parts page and not added to the page programmatically.</summary>
		/// <returns>A Boolean value that indicates whether the control is static.</returns>
		// Token: 0x1700109A RID: 4250
		// (get) Token: 0x0600348C RID: 13452 RVA: 0x00088850 File Offset: 0x00086A50
		public bool IsStatic
		{
			get
			{
				return this.isStatic;
			}
		}

		/// <summary>Gets a string that is concatenated with the <see cref="P:System.Web.UI.WebControls.WebParts.WebPart.Title" /> property value to form a complete title for a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control. </summary>
		/// <returns>A string that serves as a subtitle for the control. The default value is an empty string ("").</returns>
		// Token: 0x1700109B RID: 4251
		// (get) Token: 0x0600348D RID: 13453 RVA: 0x0000EE63 File Offset: 0x0000D063
		public virtual string Subtitle
		{
			get
			{
				return string.Empty;
			}
		}

		/// <summary>Gets or sets the title of a part control.</summary>
		/// <returns>A string that represents the title of the part control. The default value is an empty string ("").</returns>
		// Token: 0x1700109C RID: 4252
		// (get) Token: 0x0600348E RID: 13454 RVA: 0x00088858 File Offset: 0x00086A58
		// (set) Token: 0x0600348F RID: 13455 RVA: 0x00088860 File Offset: 0x00086A60
		public override string Title
		{
			get
			{
				return base.Title;
			}
			set
			{
				base.Title = value;
			}
		}

		/// <summary>Gets or sets the URL to an image used to represent a Web Parts control in the control's title bar.</summary>
		/// <returns>A string that represents the URL to an image used to represent the control in its title bar. The default value is an empty string ("").</returns>
		/// <exception cref="T:System.ArgumentException">The internal validation system has determined that the URL might contain script attacks.</exception>
		// Token: 0x1700109D RID: 4253
		// (get) Token: 0x06003490 RID: 13456 RVA: 0x00088869 File Offset: 0x00086A69
		// (set) Token: 0x06003491 RID: 13457 RVA: 0x00088871 File Offset: 0x00086A71
		public virtual string TitleIconImageUrl
		{
			get
			{
				return this.titleIconImageUrl;
			}
			set
			{
				this.titleIconImageUrl = value;
			}
		}

		/// <summary>Gets or sets a URL to supplemental information about a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control. </summary>
		/// <returns>A string that represents a URL to more information about the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control. The default value is an empty string ("").</returns>
		/// <exception cref="T:System.ArgumentException">The internal validation system has determined that the URL might contain script attacks.</exception>
		// Token: 0x1700109E RID: 4254
		// (get) Token: 0x06003492 RID: 13458 RVA: 0x0008887A File Offset: 0x00086A7A
		// (set) Token: 0x06003493 RID: 13459 RVA: 0x00088882 File Offset: 0x00086A82
		public virtual string TitleUrl
		{
			get
			{
				return this.titleUrl;
			}
			set
			{
				this.titleUrl = value;
			}
		}

		/// <summary>Gets a collection of custom verbs associated with a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerbCollection" /> that contains custom <see cref="T:System.Web.UI.WebControls.WebParts.WebPartVerb" /> objects associated with a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control. The default value is <see cref="F:System.Web.UI.WebControls.WebParts.WebPartVerbCollection.Empty" />. </returns>
		// Token: 0x1700109F RID: 4255
		// (get) Token: 0x06003494 RID: 13460 RVA: 0x0008888B File Offset: 0x00086A8B
		public virtual WebPartVerbCollection Verbs
		{
			get
			{
				return this.verbs;
			}
		}

		// Token: 0x170010A0 RID: 4256
		// (get) Token: 0x06003495 RID: 13461 RVA: 0x00088893 File Offset: 0x00086A93
		// (set) Token: 0x06003496 RID: 13462 RVA: 0x0008889B File Offset: 0x00086A9B
		public override Unit Width
		{
			get
			{
				return base.Width;
			}
			set
			{
				base.Width = value;
			}
		}

		/// <summary>Gets the index position of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control within its zone.</summary>
		/// <returns>The numerical order of a control within its zone. The first control in a zone has an index value of zero.</returns>
		// Token: 0x170010A1 RID: 4257
		// (get) Token: 0x06003497 RID: 13463 RVA: 0x000888A4 File Offset: 0x00086AA4
		public int ZoneIndex
		{
			get
			{
				return this.zoneIndex;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control to enable it to be edited by custom <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls.</summary>
		/// <returns>A <see cref="T:System.Object" /> that consists of the child control of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</returns>
		// Token: 0x170010A2 RID: 4258
		// (get) Token: 0x06003498 RID: 13464 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public virtual object WebBrowsableObject
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> control associated with a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control instance. </summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartManager" /> that is associated with a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</returns>
		// Token: 0x170010A3 RID: 4259
		// (get) Token: 0x06003499 RID: 13465 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		protected WebPartManager WebPartManager
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> zone that currently contains a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.WebParts.WebPartZoneBase" /> that currently contains a Web Parts control on a Web page. If a Web Parts control is currently closed on a page, the return value is null.</returns>
		// Token: 0x170010A4 RID: 4260
		// (get) Token: 0x0600349A RID: 13466 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public WebPartZoneBase Zone
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Returns a collection of custom <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls that can be used to edit a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control when it is in edit mode.</summary>
		/// <returns>An <see cref="T:System.Web.UI.WebControls.WebParts.EditorPartCollection" /> that contains custom <see cref="T:System.Web.UI.WebControls.WebParts.EditorPart" /> controls associated with a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</returns>
		// Token: 0x0600349B RID: 13467 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public virtual EditorPartCollection CreateEditorParts()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x04001D14 RID: 7444
		private WebPartVerbCollection verbs = new WebPartVerbCollection();

		// Token: 0x04001D15 RID: 7445
		private WebPart.Allow allow;

		// Token: 0x04001D16 RID: 7446
		private string auth_filter;

		// Token: 0x04001D17 RID: 7447
		private string catalog_icon_url;

		// Token: 0x04001D18 RID: 7448
		private WebPartExportMode exportMode;

		// Token: 0x04001D19 RID: 7449
		private string titleIconImageUrl;

		// Token: 0x04001D1A RID: 7450
		private string titleUrl;

		// Token: 0x04001D1B RID: 7451
		private string helpUrl;

		// Token: 0x04001D1C RID: 7452
		private bool isStatic;

		// Token: 0x04001D1D RID: 7453
		private bool hidden;

		// Token: 0x04001D1E RID: 7454
		private bool isClosed;

		// Token: 0x04001D1F RID: 7455
		private bool hasSharedData;

		// Token: 0x04001D20 RID: 7456
		private bool hasUserData;

		// Token: 0x04001D21 RID: 7457
		private WebPartHelpMode helpMode = WebPartHelpMode.Navigate;

		// Token: 0x04001D22 RID: 7458
		private int zoneIndex;

		// Token: 0x02000486 RID: 1158
		[Flags]
		private enum Allow
		{
			// Token: 0x04001D24 RID: 7460
			Close = 1,
			// Token: 0x04001D25 RID: 7461
			Connect = 2,
			// Token: 0x04001D26 RID: 7462
			Edit = 4,
			// Token: 0x04001D27 RID: 7463
			Hide = 8,
			// Token: 0x04001D28 RID: 7464
			Minimize = 16,
			// Token: 0x04001D29 RID: 7465
			ZoneChange = 32
		}
	}
}

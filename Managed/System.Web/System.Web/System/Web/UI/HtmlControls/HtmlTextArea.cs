using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Allows programmatic access to the &lt;textarea&gt; HTML element on the server.</summary>
	// Token: 0x02000273 RID: 627
	[SupportsEventValidation]
	[DefaultEvent("ServerChange")]
	[ValidationProperty("Value")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlTextArea : HtmlContainerControl, IPostBackDataHandler
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlTextArea" /> class.</summary>
		// Token: 0x060019DC RID: 6620 RVA: 0x00042C7F File Offset: 0x00040E7F
		public HtmlTextArea()
			: base("textarea")
		{
		}

		/// <summary>Gets or sets the width (in characters) of the <see cref="T:System.Web.UI.HtmlControls.HtmlTextArea" /> control.</summary>
		/// <returns>The width (in characters) of the <see cref="T:System.Web.UI.HtmlControls.HtmlTextArea" /> control. The default value is -1, which indicates that this property is not set.</returns>
		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x060019DD RID: 6621 RVA: 0x00042C8C File Offset: 0x00040E8C
		// (set) Token: 0x060019DE RID: 6622 RVA: 0x00042CB5 File Offset: 0x00040EB5
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public int Cols
		{
			get
			{
				string text = base.Attributes["cols"];
				if (text != null)
				{
					return Convert.ToInt32(text);
				}
				return -1;
			}
			set
			{
				if (value == -1)
				{
					base.Attributes.Remove("cols");
					return;
				}
				base.Attributes["cols"] = value.ToString(Helpers.InvariantCulture);
			}
		}

		/// <summary>Gets or sets the unique identifier name for the <see cref="T:System.Web.UI.HtmlControls.HtmlTextArea" /> control.</summary>
		/// <returns>A string that represents the value of the <see cref="P:System.Web.UI.Control.UniqueID" /> property.</returns>
		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x060019DF RID: 6623 RVA: 0x0003F6F3 File Offset: 0x0003D8F3
		// (set) Token: 0x060019E0 RID: 6624 RVA: 0x00003946 File Offset: 0x00001B46
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("")]
		public virtual string Name
		{
			get
			{
				return this.UniqueID;
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the height (in characters) of the <see cref="T:System.Web.UI.HtmlControls.HtmlTextArea" /> control.</summary>
		/// <returns>The height (in characters) of the <see cref="T:System.Web.UI.HtmlControls.HtmlTextArea" /> control. The default value is -1, which indicates that this property is not set.</returns>
		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x060019E1 RID: 6625 RVA: 0x00042CE8 File Offset: 0x00040EE8
		// (set) Token: 0x060019E2 RID: 6626 RVA: 0x00042D11 File Offset: 0x00040F11
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public int Rows
		{
			get
			{
				string text = base.Attributes["rows"];
				if (text != null)
				{
					return Convert.ToInt32(text);
				}
				return -1;
			}
			set
			{
				if (value == -1)
				{
					base.Attributes.Remove("rows");
					return;
				}
				base.Attributes["rows"] = value.ToString(Helpers.InvariantCulture);
			}
		}

		/// <summary>Gets or sets the text entered in the <see cref="T:System.Web.UI.HtmlControls.HtmlTextArea" /> control.</summary>
		/// <returns>The text entered in the <see cref="T:System.Web.UI.HtmlControls.HtmlTextArea" /> control. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x060019E3 RID: 6627 RVA: 0x00042D44 File Offset: 0x00040F44
		// (set) Token: 0x060019E4 RID: 6628 RVA: 0x00042D4C File Offset: 0x00040F4C
		[DefaultValue("")]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Value
		{
			get
			{
				return this.InnerText;
			}
			set
			{
				this.InnerText = value;
			}
		}

		/// <summary>Notifies the <see cref="T:System.Web.UI.HtmlControls.HtmlTextArea" /> control that an object was parsed and adds the object to the <see cref="T:System.Web.UI.HtmlControls.HtmlTextArea" /> control's <see cref="T:System.Web.UI.ControlCollection" /> object. </summary>
		/// <param name="obj">An <see cref="T:System.Object" /> that represents the parsed element. </param>
		/// <exception cref="T:System.Web.HttpException">The object specified by the <paramref name="obj" /> parameter can only be of the type <see cref="T:System.Web.UI.LiteralControl" /> or <see cref="T:System.Web.UI.DataBoundLiteralControl" />.</exception>
		// Token: 0x060019E5 RID: 6629 RVA: 0x00042D55 File Offset: 0x00040F55
		protected override void AddParsedSubObject(object obj)
		{
			if (!(obj is LiteralControl) && !(obj is DataBoundLiteralControl))
			{
				throw new HttpException(global::Locale.GetText("Wrong type."));
			}
			base.AddParsedSubObject(obj);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
		// Token: 0x060019E6 RID: 6630 RVA: 0x00042D80 File Offset: 0x00040F80
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			Page page = this.Page;
			if (page != null && !base.Disabled)
			{
				page.RegisterRequiresPostBack(this);
				page.RegisterEnabledControl(this);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.HtmlControls.HtmlTextArea.ServerChange" /> event of the <see cref="T:System.Web.UI.HtmlControls.HtmlTextArea" /> control. This allows you to provide a custom handler for the event.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060019E7 RID: 6631 RVA: 0x00042DB4 File Offset: 0x00040FB4
		protected virtual void OnServerChange(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[HtmlTextArea.serverChangeEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlTextArea" /> control's attributes to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered content.</param>
		// Token: 0x060019E8 RID: 6632 RVA: 0x00042DE4 File Offset: 0x00040FE4
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			Page page = this.Page;
			if (page != null)
			{
				page.ClientScript.RegisterForEventValidation(this.UniqueID);
			}
			if (base.Attributes["name"] == null)
			{
				writer.WriteAttribute("name", this.Name);
			}
			base.RenderAttributes(writer);
		}

		/// <summary>Processes the postback data for the <see cref="T:System.Web.UI.HtmlControls.HtmlTextArea" /> control.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.HtmlControls.HtmlTextArea" /> control's state has changed as a result of the postback; otherwise, false.</returns>
		/// <param name="postDataKey">The key identifier for the control.</param>
		/// <param name="postCollection">The collection of all incoming name values.</param>
		// Token: 0x060019E9 RID: 6633 RVA: 0x00042E36 File Offset: 0x00041036
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.DefaultLoadPostData(postDataKey, postCollection);
		}

		/// <summary>Calls the <see cref="M:System.Web.UI.HtmlControls.HtmlTextArea.OnServerChange(System.EventArgs)" /> method to signal the <see cref="T:System.Web.UI.HtmlControls.HtmlTextArea" /> control that the state of the control has changed.</summary>
		// Token: 0x060019EA RID: 6634 RVA: 0x00042E40 File Offset: 0x00041040
		protected virtual void RaisePostDataChangedEvent()
		{
			base.ValidateEvent(this.UniqueID, string.Empty);
			this.OnServerChange(EventArgs.Empty);
		}

		// Token: 0x060019EB RID: 6635 RVA: 0x00042E60 File Offset: 0x00041060
		internal bool DefaultLoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			string text = postCollection[postDataKey];
			if (text != null && text != this.Value)
			{
				this.Value = text;
				return true;
			}
			return false;
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IPostBackDataHandler.LoadPostData(System.String,System.Collections.Specialized.NameValueCollection)" />. </summary>
		/// <returns>true if the <see cref="T:System.Web.UI.HtmlControls.HtmlTextArea" /> control's state has changed as a result of postback; otherwise, false.</returns>
		/// <param name="postDataKey">The key identifier for the control.</param>
		/// <param name="postCollection">The collection of all incoming name values.</param>
		// Token: 0x060019EC RID: 6636 RVA: 0x00042E90 File Offset: 0x00041090
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IPostBackDataHandler.RaisePostDataChangedEvent" />.</summary>
		// Token: 0x060019ED RID: 6637 RVA: 0x00042E9A File Offset: 0x0004109A
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		/// <summary>Occurs when the content of the <see cref="T:System.Web.UI.HtmlControls.HtmlTextArea" /> control changes between posts to the server.</summary>
		// Token: 0x14000040 RID: 64
		// (add) Token: 0x060019EE RID: 6638 RVA: 0x00042EA2 File Offset: 0x000410A2
		// (remove) Token: 0x060019EF RID: 6639 RVA: 0x00042EB5 File Offset: 0x000410B5
		[WebSysDescription("")]
		[WebCategory("Action")]
		public event EventHandler ServerChange
		{
			add
			{
				base.Events.AddHandler(HtmlTextArea.serverChangeEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(HtmlTextArea.serverChangeEvent, value);
			}
		}

		// Token: 0x04001642 RID: 5698
		private static readonly object serverChangeEvent = new object();
	}
}

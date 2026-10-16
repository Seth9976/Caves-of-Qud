using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Allows programmatic access to the HTML &lt;input type= radio&gt; element on the server.</summary>
	// Token: 0x02000263 RID: 611
	[DefaultEvent("ServerChange")]
	[SupportsEventValidation]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlInputRadioButton : HtmlInputControl, IPostBackDataHandler
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputRadioButton" /> class.</summary>
		// Token: 0x060018F6 RID: 6390 RVA: 0x000410FF File Offset: 0x0003F2FF
		public HtmlInputRadioButton()
			: base("radio")
		{
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.HtmlControls.HtmlInputRadioButton" /> control is selected.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.HtmlControls.HtmlInputRadioButton" /> control is selected; otherwise, false.</returns>
		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x060018F7 RID: 6391 RVA: 0x0004110C File Offset: 0x0003F30C
		// (set) Token: 0x060018F8 RID: 6392 RVA: 0x00041128 File Offset: 0x0003F328
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[WebCategory("Misc")]
		public bool Checked
		{
			get
			{
				return base.Attributes["checked"] == "checked";
			}
			set
			{
				if (value)
				{
					base.Attributes["checked"] = "checked";
					return;
				}
				base.Attributes.Remove("checked");
			}
		}

		/// <summary>Gets or sets the name of the group that the instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputRadioButton" /> class is associated with.</summary>
		/// <returns>The group of check box controls that the instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputRadioButton" /> class is a member of.</returns>
		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x060018F9 RID: 6393 RVA: 0x00041154 File Offset: 0x0003F354
		// (set) Token: 0x060018FA RID: 6394 RVA: 0x0004117C File Offset: 0x0003F37C
		public override string Name
		{
			get
			{
				string text = base.Attributes["name"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				if (value == null)
				{
					base.Attributes.Remove("name");
					return;
				}
				base.Attributes["name"] = value;
			}
		}

		/// <summary>Gets or sets the value associated with the <see cref="T:System.Web.UI.HtmlControls.HtmlInputRadioButton" /> control.</summary>
		/// <returns>The value associated with the <see cref="T:System.Web.UI.HtmlControls.HtmlInputRadioButton" /> control.</returns>
		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x060018FB RID: 6395 RVA: 0x000411A4 File Offset: 0x0003F3A4
		// (set) Token: 0x060018FC RID: 6396 RVA: 0x00040988 File Offset: 0x0003EB88
		public override string Value
		{
			get
			{
				string text = base.Attributes["value"];
				if (text == null || text.Length == 0)
				{
					text = this.ID;
					if (text != null && text.Length == 0)
					{
						text = null;
					}
				}
				return text;
			}
			set
			{
				if (value == null)
				{
					base.Attributes.Remove("value");
					return;
				}
				base.Attributes["value"] = value;
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event and registers the <see cref="T:System.Web.UI.HtmlControls.HtmlInputRadioButton" /> control as one that requires postback handling.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060018FD RID: 6397 RVA: 0x000411E4 File Offset: 0x0003F3E4
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

		/// <summary>Raises the <see cref="E:System.Web.UI.HtmlControls.HtmlInputRadioButton.ServerChange" /> event. This allows you to create a custom event handler when the event is raised.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x060018FE RID: 6398 RVA: 0x00041218 File Offset: 0x0003F418
		protected virtual void OnServerChange(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[HtmlInputRadioButton.serverChangeEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlInputRadioButton" /> control attributes to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object. </summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that receives the rendered output.</param>
		// Token: 0x060018FF RID: 6399 RVA: 0x00041248 File Offset: 0x0003F448
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			Page page = this.Page;
			if (page != null)
			{
				page.ClientScript.RegisterForEventValidation(this.UniqueID, this.Value);
			}
			writer.WriteAttribute("value", this.Value, true);
			base.Attributes.Remove("value");
			base.RenderAttributes(writer);
		}

		/// <summary>Processes the postback data for the <see cref="T:System.Web.UI.HtmlControls.HtmlInputRadioButton" /> control.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.HtmlControls.HtmlInputRadioButton" /> control's state has changed as a result of the postback; otherwise, false.</returns>
		/// <param name="postDataKey">The key identifier for the control.</param>
		/// <param name="postCollection">The collection of all incoming name values.</param>
		// Token: 0x06001900 RID: 6400 RVA: 0x000412A0 File Offset: 0x0003F4A0
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			bool flag = postCollection[this.Name] == this.Value;
			if (this.Checked == flag)
			{
				return false;
			}
			base.ValidateEvent(this.UniqueID, this.Value);
			this.Checked = flag;
			return flag;
		}

		/// <summary>Calls the <see cref="M:System.Web.UI.HtmlControls.HtmlInputRadioButton.OnServerChange(System.EventArgs)" /> method to signal the <see cref="T:System.Web.UI.HtmlControls.HtmlInputRadioButton" /> control that the state of the control has changed.</summary>
		// Token: 0x06001901 RID: 6401 RVA: 0x000412EA File Offset: 0x0003F4EA
		protected virtual void RaisePostDataChangedEvent()
		{
			this.OnServerChange(EventArgs.Empty);
		}

		/// <summary>Implements the <see cref="M:System.Web.UI.IPostBackDataHandler.LoadPostData(System.String,System.Collections.Specialized.NameValueCollection)" /> method by calling the <see cref="M:System.Web.UI.HtmlControls.HtmlInputRadioButton.LoadPostData(System.String,System.Collections.Specialized.NameValueCollection)" /> method.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.HtmlControls.HtmlInputRadioButton" /> control's state has changed as a result of the postback; otherwise, false.</returns>
		/// <param name="postDataKey">The key identifier for the control.</param>
		/// <param name="postCollection">The collection of all incoming name values</param>
		// Token: 0x06001902 RID: 6402 RVA: 0x000412F7 File Offset: 0x0003F4F7
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		/// <summary>Implements the <see cref="M:System.Web.UI.IPostBackDataHandler.RaisePostDataChangedEvent" /> method by calling the <see cref="M:System.Web.UI.HtmlControls.HtmlInputRadioButton.RaisePostDataChangedEvent" /> method.</summary>
		// Token: 0x06001903 RID: 6403 RVA: 0x00041301 File Offset: 0x0003F501
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Web.UI.HtmlControls.HtmlInputRadioButton.Checked" /> property of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputRadioButton" /> control changes between posts to the server.</summary>
		// Token: 0x1400003C RID: 60
		// (add) Token: 0x06001904 RID: 6404 RVA: 0x00041309 File Offset: 0x0003F509
		// (remove) Token: 0x06001905 RID: 6405 RVA: 0x0004131C File Offset: 0x0003F51C
		[WebCategory("Action")]
		[WebSysDescription("")]
		public event EventHandler ServerChange
		{
			add
			{
				base.Events.AddHandler(HtmlInputRadioButton.serverChangeEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(HtmlInputRadioButton.serverChangeEvent, value);
			}
		}

		// Token: 0x04001634 RID: 5684
		private static readonly object serverChangeEvent = new object();
	}
}

using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Allows programmatic access to the HTML &lt;input type= checkbox&gt; element on the server.</summary>
	// Token: 0x0200025D RID: 605
	[DefaultEvent("ServerChange")]
	[SupportsEventValidation]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlInputCheckBox : HtmlInputControl, IPostBackDataHandler
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputCheckBox" /> class.</summary>
		// Token: 0x0600189F RID: 6303 RVA: 0x00040797 File Offset: 0x0003E997
		public HtmlInputCheckBox()
			: base("checkbox")
		{
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.UI.HtmlControls.HtmlInputCheckBox" /> is selected.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.HtmlControls.HtmlInputCheckBox" /> control is selected; otherwise, false.</returns>
		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x060018A0 RID: 6304 RVA: 0x000407A4 File Offset: 0x0003E9A4
		// (set) Token: 0x060018A1 RID: 6305 RVA: 0x000407BB File Offset: 0x0003E9BB
		[DefaultValue("")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[WebCategory("Misc")]
		[TypeConverter(typeof(MinimizableAttributeTypeConverter))]
		public bool Checked
		{
			get
			{
				return base.Attributes["checked"] != null;
			}
			set
			{
				if (!value)
				{
					base.Attributes.Remove("checked");
					return;
				}
				base.Attributes["checked"] = "checked";
			}
		}

		/// <summary>Occurs when the Web page is submitted to the server and the <see cref="T:System.Web.UI.HtmlControls.HtmlInputCheckBox" /> control changes state from the previous post.</summary>
		// Token: 0x14000039 RID: 57
		// (add) Token: 0x060018A2 RID: 6306 RVA: 0x000407E6 File Offset: 0x0003E9E6
		// (remove) Token: 0x060018A3 RID: 6307 RVA: 0x000407F9 File Offset: 0x0003E9F9
		[WebSysDescription("")]
		[WebCategory("Action")]
		public event EventHandler ServerChange
		{
			add
			{
				base.Events.AddHandler(HtmlInputCheckBox.EventServerChange, value);
			}
			remove
			{
				base.Events.RemoveHandler(HtmlInputCheckBox.EventServerChange, value);
			}
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlInputCheckBox" /> control's attributes to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> instance.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> instance that contains the output stream to render on the client.</param>
		// Token: 0x060018A4 RID: 6308 RVA: 0x0004080C File Offset: 0x0003EA0C
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			Page page = this.Page;
			if (page != null)
			{
				page.ClientScript.RegisterForEventValidation(this.UniqueID);
			}
			base.RenderAttributes(writer);
		}

		/// <summary>Raises the <see cref="M:System.Web.UI.Control.OnPreRender(System.EventArgs)" /> event and registers the control as one that requires postback handling.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data. </param>
		// Token: 0x060018A5 RID: 6309 RVA: 0x0004083C File Offset: 0x0003EA3C
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

		/// <summary>Raises the <see cref="E:System.Web.UI.HtmlControls.HtmlInputCheckBox.ServerChange" /> event. This method allows you to handle the event directly.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains event information. </param>
		// Token: 0x060018A6 RID: 6310 RVA: 0x00040870 File Offset: 0x0003EA70
		protected virtual void OnServerChange(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[HtmlInputCheckBox.EventServerChange];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060018A7 RID: 6311 RVA: 0x000408A0 File Offset: 0x0003EAA0
		private bool LoadPostDataInternal(string postDataKey, NameValueCollection postCollection)
		{
			string text = postCollection[postDataKey];
			bool flag = text != null && text.Length > 0;
			if (this.Checked != flag)
			{
				this.Checked = flag;
				return true;
			}
			return false;
		}

		// Token: 0x060018A8 RID: 6312 RVA: 0x000408D8 File Offset: 0x0003EAD8
		private void RaisePostDataChangedEventInternal()
		{
			this.OnServerChange(EventArgs.Empty);
		}

		/// <summary>Processes the postback data for the <see cref="T:System.Web.UI.HtmlControls.HtmlInputCheckBox" /> control. </summary>
		/// <returns>true if the <see cref="T:System.Web.UI.HtmlControls.HtmlInputCheckBox" /> control's state has changed as a result of the postback event; otherwise, false.</returns>
		/// <param name="postDataKey">The key identifier for the control.</param>
		/// <param name="postCollection">The collection of all incoming name values.</param>
		// Token: 0x060018A9 RID: 6313 RVA: 0x000408E5 File Offset: 0x0003EAE5
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostDataInternal(postDataKey, postCollection);
		}

		/// <summary>Calls the <see cref="M:System.Web.UI.HtmlControls.HtmlInputCheckBox.OnServerChange(System.EventArgs)" /> method to signal the <see cref="T:System.Web.UI.HtmlControls.HtmlInputCheckBox" /> control that the state of the control has changed.</summary>
		// Token: 0x060018AA RID: 6314 RVA: 0x000408EF File Offset: 0x0003EAEF
		protected virtual void RaisePostDataChangedEvent()
		{
			base.ValidateEvent(this.UniqueID, string.Empty);
			this.RaisePostDataChangedEventInternal();
		}

		/// <summary>Implements the <see cref="M:System.Web.UI.IPostBackDataHandler.LoadPostData(System.String,System.Collections.Specialized.NameValueCollection)" /> method by calling the <see cref="M:System.Web.UI.HtmlControls.HtmlInputCheckBox.LoadPostData(System.String,System.Collections.Specialized.NameValueCollection)" /> method.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.HtmlControls.HtmlInputCheckBox" /> control's state has changed as a result of the postback; otherwise, false.</returns>
		/// <param name="postDataKey">The key identifier for the control.</param>
		/// <param name="postCollection">The collection of all incoming name values.</param>
		// Token: 0x060018AB RID: 6315 RVA: 0x00040908 File Offset: 0x0003EB08
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		/// <summary>Implements the <see cref="M:System.Web.UI.IPostBackDataHandler.RaisePostDataChangedEvent" /> method by calling the <see cref="M:System.Web.UI.HtmlControls.HtmlInputCheckBox.RaisePostDataChangedEvent" /> method.</summary>
		// Token: 0x060018AC RID: 6316 RVA: 0x00040912 File Offset: 0x0003EB12
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		// Token: 0x0400162E RID: 5678
		private static readonly object EventServerChange = new object();
	}
}

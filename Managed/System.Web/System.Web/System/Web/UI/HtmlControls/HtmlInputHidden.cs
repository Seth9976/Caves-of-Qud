using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Allows programmatic access to the HTML &lt;input type=hidden&gt; element on the server.</summary>
	// Token: 0x02000260 RID: 608
	[DefaultEvent("ServerChange")]
	[SupportsEventValidation]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlInputHidden : HtmlInputControl, IPostBackDataHandler
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputHidden" /> class.</summary>
		// Token: 0x060018C7 RID: 6343 RVA: 0x00040C1F File Offset: 0x0003EE1F
		public HtmlInputHidden()
			: base("hidden")
		{
		}

		// Token: 0x060018C8 RID: 6344 RVA: 0x00040C2C File Offset: 0x0003EE2C
		private bool LoadPostDataInternal(string postDataKey, NameValueCollection postCollection)
		{
			string text = postCollection[postDataKey];
			if (text != null && text != this.Value)
			{
				base.ValidateEvent(postDataKey, string.Empty);
				this.Value = text;
				return true;
			}
			return false;
		}

		// Token: 0x060018C9 RID: 6345 RVA: 0x00040C68 File Offset: 0x0003EE68
		private void RaisePostDataChangedEventInternal()
		{
			this.OnServerChange(EventArgs.Empty);
		}

		/// <summary>Processes the postback data for the <see cref="T:System.Web.UI.HtmlControls.HtmlInputHidden" /> control.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.HtmlControls.HtmlInputHidden" /> control's state has changed as a result of the postback; otherwise, false.</returns>
		/// <param name="postDataKey">The key identifier for the control.</param>
		/// <param name="postCollection">The collection of all incoming name values.</param>
		// Token: 0x060018CA RID: 6346 RVA: 0x00040C75 File Offset: 0x0003EE75
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostDataInternal(postDataKey, postCollection);
		}

		/// <summary>Calls the <see cref="M:System.Web.UI.HtmlControls.HtmlInputHidden.OnServerChange(System.EventArgs)" /> method to signal the <see cref="T:System.Web.UI.HtmlControls.HtmlInputHidden" /> control that the state of the control has changed.</summary>
		// Token: 0x060018CB RID: 6347 RVA: 0x00040C7F File Offset: 0x0003EE7F
		protected virtual void RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEventInternal();
		}

		/// <summary>Implements the <see cref="M:System.Web.UI.IPostBackDataHandler.LoadPostData(System.String,System.Collections.Specialized.NameValueCollection)" /> interface method by calling the <see cref="M:System.Web.UI.HtmlControls.HtmlInputHidden.LoadPostData(System.String,System.Collections.Specialized.NameValueCollection)" /> method.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.HtmlControls.HtmlInputHidden" /> control's state has changed as a result of the postback; otherwise, false.</returns>
		/// <param name="postDataKey">The key identifier for the control.</param>
		/// <param name="postCollection">The collection of all incoming name values.</param>
		// Token: 0x060018CC RID: 6348 RVA: 0x00040C87 File Offset: 0x0003EE87
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		/// <summary>Implements the <see cref="M:System.Web.UI.IPostBackDataHandler.RaisePostDataChangedEvent" /> interface method by calling the <see cref="M:System.Web.UI.HtmlControls.HtmlInputHidden.RaisePostDataChangedEvent" /> method.</summary>
		// Token: 0x060018CD RID: 6349 RVA: 0x00040C91 File Offset: 0x0003EE91
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlInputHidden" /> control's attributes to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream to render on the client.</param>
		// Token: 0x060018CE RID: 6350 RVA: 0x00040C9C File Offset: 0x0003EE9C
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			Page page = this.Page;
			if (page != null)
			{
				page.ClientScript.RegisterForEventValidation(this.Name);
			}
			base.RenderAttributes(writer);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data.</param>
		// Token: 0x060018CF RID: 6351 RVA: 0x00040CCC File Offset: 0x0003EECC
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

		/// <summary>Raises the <see cref="E:System.Web.UI.HtmlControls.HtmlInputHidden.ServerChange" /> event. This method allows you to handle the event directly.</summary>
		/// <param name="e">A <see cref="T:System.EventArgs" /> that contains event data. </param>
		// Token: 0x060018D0 RID: 6352 RVA: 0x00040D00 File Offset: 0x0003EF00
		protected virtual void OnServerChange(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[HtmlInputHidden.ServerChangeEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Occurs when the <see cref="P:System.Web.UI.HtmlControls.HtmlInputControl.Value" /> property is changed on the server.</summary>
		// Token: 0x1400003A RID: 58
		// (add) Token: 0x060018D1 RID: 6353 RVA: 0x00040D2E File Offset: 0x0003EF2E
		// (remove) Token: 0x060018D2 RID: 6354 RVA: 0x00040D41 File Offset: 0x0003EF41
		[WebCategory("Action")]
		[WebSysDescription("")]
		public event EventHandler ServerChange
		{
			add
			{
				base.Events.AddHandler(HtmlInputHidden.ServerChangeEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(HtmlInputHidden.ServerChangeEvent, value);
			}
		}

		// Token: 0x060018D3 RID: 6355 RVA: 0x00040D54 File Offset: 0x0003EF54
		// Note: this type is marked as 'beforefieldinit'.
		static HtmlInputHidden()
		{
			HtmlInputHidden.ServerChangeEvent = new object();
		}
	}
}

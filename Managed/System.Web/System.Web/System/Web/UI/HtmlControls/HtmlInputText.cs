using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI.HtmlControls
{
	/// <summary>Allows programmatic access to the HTML &lt;input type= text&gt; and &lt;input type= password&gt; elements on the server.</summary>
	// Token: 0x02000266 RID: 614
	[DefaultEvent("ServerChange")]
	[ValidationProperty("Value")]
	[SupportsEventValidation]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HtmlInputText : HtmlInputControl, IPostBackDataHandler
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputText" /> class using default values.</summary>
		// Token: 0x06001913 RID: 6419 RVA: 0x00041399 File Offset: 0x0003F599
		public HtmlInputText()
			: base("text")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.HtmlControls.HtmlInputText" /> class using the specified input control type.</summary>
		/// <param name="type">The type of input control. </param>
		// Token: 0x06001914 RID: 6420 RVA: 0x00040284 File Offset: 0x0003E484
		public HtmlInputText(string type)
			: base(type)
		{
		}

		/// <summary>Gets or sets the maximum number of characters that can be entered in the text box.</summary>
		/// <returns>The maximum number of characters that can be entered in the text box.</returns>
		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06001915 RID: 6421 RVA: 0x000413A8 File Offset: 0x0003F5A8
		// (set) Token: 0x06001916 RID: 6422 RVA: 0x00040A6D File Offset: 0x0003EC6D
		[WebCategory("Behavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue("")]
		[WebSysDescription("")]
		public int MaxLength
		{
			get
			{
				string text = base.Attributes["maxlength"];
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
					base.Attributes.Remove("maxlength");
					return;
				}
				base.Attributes["maxlength"] = value.ToString();
			}
		}

		/// <summary>Gets or sets the width of the text box.</summary>
		/// <returns>The width, in characters, of the text box.</returns>
		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06001917 RID: 6423 RVA: 0x000413D4 File Offset: 0x0003F5D4
		// (set) Token: 0x06001918 RID: 6424 RVA: 0x00040ACD File Offset: 0x0003ECCD
		[DefaultValue(-1)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public int Size
		{
			get
			{
				string text = base.Attributes["size"];
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
					base.Attributes.Remove("size");
					return;
				}
				base.Attributes["size"] = value.ToString();
			}
		}

		/// <summary>Gets or sets the contents of the text box.</summary>
		/// <returns>The text contained in the text box. The default is an empty string ("").</returns>
		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06001919 RID: 6425 RVA: 0x00041400 File Offset: 0x0003F600
		// (set) Token: 0x0600191A RID: 6426 RVA: 0x00040988 File Offset: 0x0003EB88
		public override string Value
		{
			get
			{
				string text = base.Attributes["value"];
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
					base.Attributes.Remove("value");
					return;
				}
				base.Attributes["value"] = value;
			}
		}

		// Token: 0x0600191B RID: 6427 RVA: 0x00041428 File Offset: 0x0003F628
		protected internal override void Render(HtmlTextWriter writer)
		{
			Page page = this.Page;
			if (page != null)
			{
				page.ClientScript.RegisterForEventValidation(this.UniqueID);
			}
			base.Render(writer);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x0600191C RID: 6428 RVA: 0x00041458 File Offset: 0x0003F658
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

		/// <summary>Raises the <see cref="E:System.Web.UI.HtmlControls.HtmlInputText.ServerChange" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data. </param>
		// Token: 0x0600191D RID: 6429 RVA: 0x0004148C File Offset: 0x0003F68C
		protected virtual void OnServerChange(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[HtmlInputText.serverChangeEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Renders the <see cref="T:System.Web.UI.HtmlControls.HtmlInputText" /> control's attributes to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream to render on the client.</param>
		// Token: 0x0600191E RID: 6430 RVA: 0x000414BA File Offset: 0x0003F6BA
		protected override void RenderAttributes(HtmlTextWriter writer)
		{
			if (string.Compare(base.Type, 0, "password", 0, 8, true, Helpers.InvariantCulture) == 0)
			{
				base.Attributes.Remove("value");
			}
			base.RenderAttributes(writer);
		}

		// Token: 0x0600191F RID: 6431 RVA: 0x000414F0 File Offset: 0x0003F6F0
		private bool LoadPostDataInternal(string postDataKey, NameValueCollection postCollection)
		{
			string text = postCollection[postDataKey];
			if (this.Value != text)
			{
				this.Value = text;
				return true;
			}
			return false;
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x0004151D File Offset: 0x0003F71D
		private void RaisePostDataChangedEventInternal()
		{
			this.OnServerChange(EventArgs.Empty);
		}

		/// <summary>Processes the postback data for the <see cref="T:System.Web.UI.HtmlControls.HtmlInputText" /> control. </summary>
		/// <returns>true if the <see cref="T:System.Web.UI.HtmlControls.HtmlInputText" /> control's state has changed as a result of the postback; otherwise, false.</returns>
		/// <param name="postDataKey">The key identifier for the control.</param>
		/// <param name="postCollection">The collection of all incoming name values.</param>
		// Token: 0x06001921 RID: 6433 RVA: 0x0004152A File Offset: 0x0003F72A
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostDataInternal(postDataKey, postCollection);
		}

		/// <summary>Calls the <see cref="M:System.Web.UI.HtmlControls.HtmlInputText.OnServerChange(System.EventArgs)" /> method to signal the <see cref="T:System.Web.UI.HtmlControls.HtmlInputText" /> control that the state of the control has changed.</summary>
		// Token: 0x06001922 RID: 6434 RVA: 0x00041534 File Offset: 0x0003F734
		protected virtual void RaisePostDataChangedEvent()
		{
			base.ValidateEvent(this.UniqueID, string.Empty);
			this.RaisePostDataChangedEventInternal();
		}

		/// <summary>Implements the <see cref="M:System.Web.UI.IPostBackDataHandler.LoadPostData(System.String,System.Collections.Specialized.NameValueCollection)" /> interface method by calling the <see cref="M:System.Web.UI.HtmlControls.HtmlInputText.LoadPostData(System.String,System.Collections.Specialized.NameValueCollection)" /> method.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.HtmlControls.HtmlInputText" /> control's state has changed as a result of the postback; otherwise, false.</returns>
		/// <param name="postDataKey">The key identifier for the control.</param>
		/// <param name="postCollection">The collection of all incoming name values.</param>
		// Token: 0x06001923 RID: 6435 RVA: 0x0004154D File Offset: 0x0003F74D
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		/// <summary>Implements the <see cref="M:System.Web.UI.IPostBackDataHandler.RaisePostDataChangedEvent" /> interface method by calling the <see cref="M:System.Web.UI.HtmlControls.HtmlInputText.RaisePostDataChangedEvent" /> method.</summary>
		// Token: 0x06001924 RID: 6436 RVA: 0x00041557 File Offset: 0x0003F757
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		/// <summary>Occurs when the <see cref="P:System.Web.UI.HtmlControls.HtmlInputText.Value" /> property is changed on the server.</summary>
		// Token: 0x1400003E RID: 62
		// (add) Token: 0x06001925 RID: 6437 RVA: 0x0004155F File Offset: 0x0003F75F
		// (remove) Token: 0x06001926 RID: 6438 RVA: 0x00041572 File Offset: 0x0003F772
		[WebCategory("Action")]
		[WebSysDescription("")]
		public event EventHandler ServerChange
		{
			add
			{
				base.Events.AddHandler(HtmlInputText.serverChangeEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(HtmlInputText.serverChangeEvent, value);
			}
		}

		// Token: 0x04001636 RID: 5686
		private static readonly object serverChangeEvent = new object();
	}
}

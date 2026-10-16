using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.Util;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Creates a control that generates a list of items in a bulleted format.</summary>
	// Token: 0x02000339 RID: 825
	[SupportsEventValidation]
	[DefaultProperty("BulletStyle")]
	[DefaultEvent("Click")]
	[Designer("System.Web.UI.Design.WebControls.BulletedListDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class BulletedList : ListControl, IPostBackEventHandler
	{
		/// <summary>Occurs when a link button in a <see cref="T:System.Web.UI.WebControls.BulletedList" /> control is clicked.</summary>
		// Token: 0x14000048 RID: 72
		// (add) Token: 0x06001D37 RID: 7479 RVA: 0x00046F13 File Offset: 0x00045113
		// (remove) Token: 0x06001D38 RID: 7480 RVA: 0x00046F26 File Offset: 0x00045126
		public event BulletedListEventHandler Click
		{
			add
			{
				base.Events.AddHandler(BulletedList.ClickEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(BulletedList.ClickEvent, value);
			}
		}

		/// <summary>Adds the HTML attributes and styles for a <see cref="T:System.Web.UI.WebControls.BulletedList" /> control to render to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.  </param>
		// Token: 0x06001D39 RID: 7481 RVA: 0x00046F3C File Offset: 0x0004513C
		[global::System.MonoTODO("we are missing a new style enum, we should be using it")]
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			bool flag = false;
			switch (this.BulletStyle)
			{
			case BulletStyle.Numbered:
				writer.AddStyleAttribute("list-style-type", "decimal");
				flag = true;
				break;
			case BulletStyle.LowerAlpha:
				writer.AddStyleAttribute("list-style-type", "lower-alpha");
				flag = true;
				break;
			case BulletStyle.UpperAlpha:
				writer.AddStyleAttribute("list-style-type", "upper-alpha");
				flag = true;
				break;
			case BulletStyle.LowerRoman:
				writer.AddStyleAttribute("list-style-type", "lower-roman");
				flag = true;
				break;
			case BulletStyle.UpperRoman:
				writer.AddStyleAttribute("list-style-type", "upper-roman");
				flag = true;
				break;
			case BulletStyle.Disc:
				writer.AddStyleAttribute("list-style-type", "disc");
				break;
			case BulletStyle.Circle:
				writer.AddStyleAttribute("list-style-type", "circle");
				break;
			case BulletStyle.Square:
				writer.AddStyleAttribute("list-style-type", "square");
				break;
			case BulletStyle.CustomImage:
				writer.AddStyleAttribute("list-style-image", "url(" + base.ResolveClientUrl(this.BulletImageUrl) + ")");
				break;
			}
			if (flag && this.FirstBulletNumber != 1)
			{
				writer.AddAttribute("start", this.FirstBulletNumber.ToString());
			}
			base.AddAttributesToRender(writer);
		}

		/// <summary>Renders the bulleted text for each list item in a <see cref="T:System.Web.UI.WebControls.BulletedList" /> control.</summary>
		/// <param name="item">A collection of <see cref="T:System.Web.UI.WebControls.ListItem" /> objects in a <see cref="T:System.Web.UI.WebControls.BulletedList" />. </param>
		/// <param name="index">The zero-based index of the <see cref="T:System.Web.UI.WebControls.ListItem" /> to retrieve from the collection. </param>
		/// <param name="writer">The output stream that renders HTML content to the client. </param>
		// Token: 0x06001D3A RID: 7482 RVA: 0x0004707C File Offset: 0x0004527C
		protected virtual void RenderBulletText(ListItem item, int index, HtmlTextWriter writer)
		{
			string text = HttpUtility.HtmlEncode(item.Text);
			switch (this.DisplayMode)
			{
			case BulletedListDisplayMode.Text:
				if (!item.Enabled)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled", false);
					writer.RenderBeginTag(HtmlTextWriterTag.Span);
				}
				writer.Write(text);
				if (!item.Enabled)
				{
					writer.RenderEndTag();
					return;
				}
				break;
			case BulletedListDisplayMode.HyperLink:
				if (base.IsEnabled && item.Enabled)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Href, item.Value);
					if (this.Target.Length > 0)
					{
						writer.AddAttribute(HtmlTextWriterAttribute.Target, this.Target);
					}
				}
				else
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled", false);
				}
				writer.RenderBeginTag(HtmlTextWriterTag.A);
				writer.Write(text);
				writer.RenderEndTag();
				return;
			case BulletedListDisplayMode.LinkButton:
				if (base.IsEnabled && item.Enabled)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Href, this.Page.ClientScript.GetPostBackEventReference(this.GetPostBackOptions(index.ToString(Helpers.InvariantCulture)), true));
				}
				else
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled", false);
				}
				writer.RenderBeginTag(HtmlTextWriterTag.A);
				writer.Write(text);
				writer.RenderEndTag();
				break;
			default:
				return;
			}
		}

		// Token: 0x06001D3B RID: 7483 RVA: 0x000471A4 File Offset: 0x000453A4
		private PostBackOptions GetPostBackOptions(string argument)
		{
			if (this.postBackOptions == null)
			{
				this.postBackOptions = new PostBackOptions(this);
				this.postBackOptions.ActionUrl = null;
				this.postBackOptions.ValidationGroup = null;
				this.postBackOptions.RequiresJavaScriptProtocol = true;
				this.postBackOptions.ClientSubmit = true;
				this.postBackOptions.PerformValidation = this.CausesValidation && this.Page != null && this.Page.AreValidatorsUplevel(this.ValidationGroup);
				if (this.postBackOptions.PerformValidation)
				{
					this.postBackOptions.ValidationGroup = this.ValidationGroup;
				}
			}
			this.postBackOptions.Argument = argument;
			return this.postBackOptions;
		}

		/// <summary>Renders the list items of a <see cref="T:System.Web.UI.WebControls.BulletedList" /> control as bullets into the specified <see cref="T:System.Web.UI.HtmlTextWriter" />.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client. </param>
		// Token: 0x06001D3C RID: 7484 RVA: 0x00047258 File Offset: 0x00045458
		protected internal override void RenderContents(HtmlTextWriter writer)
		{
			int num = 0;
			Page page = this.Page;
			ClientScriptManager clientScriptManager = ((page != null) ? page.ClientScript : null);
			foreach (object obj in this.Items)
			{
				ListItem listItem = (ListItem)obj;
				if (page != null)
				{
					clientScriptManager.RegisterForEventValidation(this.UniqueID, listItem.Value);
				}
				if (listItem.HasAttributes)
				{
					listItem.Attributes.AddAttributes(writer);
				}
				writer.RenderBeginTag(HtmlTextWriterTag.Li);
				this.RenderBulletText(listItem, num++, writer);
				writer.RenderEndTag();
			}
		}

		/// <summary>Writes the <see cref="T:System.Web.UI.WebControls.BulletedList" /> control content to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object for display on the client.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
		// Token: 0x06001D3D RID: 7485 RVA: 0x0004730C File Offset: 0x0004550C
		protected internal override void Render(HtmlTextWriter writer)
		{
			base.Render(writer);
		}

		/// <summary>For a description of this method, see <see cref="M:System.Web.UI.IPostBackEventHandler.RaisePostBackEvent(System.String)" />.</summary>
		/// <param name="eventArgument">A string that represents an optional event argument to pass to the event handler. </param>
		// Token: 0x06001D3E RID: 7486 RVA: 0x00047315 File Offset: 0x00045515
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		/// <summary>Raises events for the <see cref="T:System.Web.UI.WebControls.BulletedList" /> control when a form is posted back to the server.</summary>
		/// <param name="eventArgument">The string representation for the index of the list item that raised the event.</param>
		// Token: 0x06001D3F RID: 7487 RVA: 0x0004731E File Offset: 0x0004551E
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			base.ValidateEvent(this.UniqueID, eventArgument);
			if (this.CausesValidation)
			{
				this.Page.Validate(this.ValidationGroup);
			}
			this.OnClick(new BulletedListEventArgs(int.Parse(eventArgument, Helpers.InvariantCulture)));
		}

		/// <summary>Gets or sets the value of the <see cref="P:System.Web.UI.WebControls.ListControl.AutoPostBack" /> property for the base class.</summary>
		/// <returns>false.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to assign a value to the <see cref="P:System.Web.UI.WebControls.BulletedList.AutoPostBack" />. </exception>
		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x06001D40 RID: 7488 RVA: 0x0004735C File Offset: 0x0004555C
		// (set) Token: 0x06001D41 RID: 7489 RVA: 0x00047364 File Offset: 0x00045564
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		public override bool AutoPostBack
		{
			get
			{
				return base.AutoPostBack;
			}
			set
			{
				throw new NotSupportedException(string.Format("This property is not supported in {0}", base.GetType()));
			}
		}

		/// <summary>Gets or sets the zero-based index of the currently selected item in a <see cref="T:System.Web.UI.WebControls.BulletedList" /> control.</summary>
		/// <returns>Always returns -1.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to assign a value to the <see cref="P:System.Web.UI.WebControls.BulletedList.SelectedIndex" />. </exception>
		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x06001D42 RID: 7490 RVA: 0x0004737B File Offset: 0x0004557B
		// (set) Token: 0x06001D43 RID: 7491 RVA: 0x00047364 File Offset: 0x00045564
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Bindable(false)]
		public override int SelectedIndex
		{
			get
			{
				return -1;
			}
			set
			{
				throw new NotSupportedException(string.Format("This property is not supported in {0}", base.GetType()));
			}
		}

		/// <summary>Gets the currently selected item in a <see cref="T:System.Web.UI.WebControls.BulletedList" /> control.</summary>
		/// <returns>null.</returns>
		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x06001D44 RID: 7492 RVA: 0x00003BF6 File Offset: 0x00001DF6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override ListItem SelectedItem
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets or sets the <see cref="P:System.Web.UI.WebControls.ListItem.Value" /> property of the selected <see cref="T:System.Web.UI.WebControls.ListItem" /> object in the <see cref="T:System.Web.UI.WebControls.BulletedList" /> control.</summary>
		/// <returns>The <see cref="P:System.Web.UI.WebControls.ListItem.Value" /> of the selected <see cref="T:System.Web.UI.WebControls.ListItem" /> in the <see cref="T:System.Web.UI.WebControls.BulletedList" />; otherwise, an empty string (""), if no item is selected.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to assign a value to the <see cref="P:System.Web.UI.WebControls.BulletedList.SelectedValue" />. </exception>
		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x06001D45 RID: 7493 RVA: 0x0000EE63 File Offset: 0x0000D063
		// (set) Token: 0x06001D46 RID: 7494 RVA: 0x00003A0D File Offset: 0x00001C0D
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Bindable(false)]
		public override string SelectedValue
		{
			get
			{
				return string.Empty;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets or sets the path to an image to display for each bullet in a <see cref="T:System.Web.UI.WebControls.BulletedList" /> control.</summary>
		/// <returns>The path to an image to display as each bullet in a <see cref="T:System.Web.UI.WebControls.BulletedList" />.</returns>
		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x06001D47 RID: 7495 RVA: 0x0004737E File Offset: 0x0004557E
		// (set) Token: 0x06001D48 RID: 7496 RVA: 0x00047395 File Offset: 0x00045595
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[UrlProperty]
		public virtual string BulletImageUrl
		{
			get
			{
				return this.ViewState.GetString("BulletImageUrl", string.Empty);
			}
			set
			{
				this.ViewState["BulletImageUrl"] = value;
			}
		}

		/// <summary>Gets or sets the bullet style for the <see cref="T:System.Web.UI.WebControls.BulletedList" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.BulletStyle" /> values. The default is <see cref="F:System.Web.UI.WebControls.BulletStyle.NotSet" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified type is not one of the <see cref="T:System.Web.UI.WebControls.BulletStyle" /> values. </exception>
		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x06001D49 RID: 7497 RVA: 0x000473A8 File Offset: 0x000455A8
		// (set) Token: 0x06001D4A RID: 7498 RVA: 0x000473BB File Offset: 0x000455BB
		[DefaultValue(BulletStyle.NotSet)]
		public virtual BulletStyle BulletStyle
		{
			get
			{
				return (BulletStyle)this.ViewState.GetInt("BulletStyle", 0);
			}
			set
			{
				if (value < BulletStyle.NotSet || value > BulletStyle.CustomImage)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["BulletStyle"] = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.ControlCollection" /> collection for the control.</summary>
		/// <returns>A control collection for the control.</returns>
		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x06001D4B RID: 7499 RVA: 0x0002FDF9 File Offset: 0x0002DFF9
		public override ControlCollection Controls
		{
			get
			{
				return new EmptyControlCollection(this);
			}
		}

		/// <summary>Gets or sets the display mode of the list content in a <see cref="T:System.Web.UI.WebControls.BulletedList" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.BulletedListDisplayMode" /> values. The default is <see cref="F:System.Web.UI.WebControls.BulletedListDisplayMode.Text" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified type is not one of the <see cref="T:System.Web.UI.WebControls.BulletedListDisplayMode" /> values. </exception>
		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x06001D4C RID: 7500 RVA: 0x000473E7 File Offset: 0x000455E7
		// (set) Token: 0x06001D4D RID: 7501 RVA: 0x000473FA File Offset: 0x000455FA
		[DefaultValue(BulletedListDisplayMode.Text)]
		public virtual BulletedListDisplayMode DisplayMode
		{
			get
			{
				return (BulletedListDisplayMode)this.ViewState.GetInt("DisplayMode", 0);
			}
			set
			{
				if (value < BulletedListDisplayMode.Text || value > BulletedListDisplayMode.LinkButton)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["DisplayMode"] = value;
			}
		}

		/// <summary>Gets or sets the value that starts the numbering of list items in an ordered <see cref="T:System.Web.UI.WebControls.BulletedList" /> control.</summary>
		/// <returns>The value that starts the numbering of list items in an ordered <see cref="T:System.Web.UI.WebControls.BulletedList" /> control. The default is 1.</returns>
		// Token: 0x17000916 RID: 2326
		// (get) Token: 0x06001D4E RID: 7502 RVA: 0x00047425 File Offset: 0x00045625
		// (set) Token: 0x06001D4F RID: 7503 RVA: 0x00047438 File Offset: 0x00045638
		[DefaultValue(1)]
		public virtual int FirstBulletNumber
		{
			get
			{
				return this.ViewState.GetInt("FirstBulletNumber", 1);
			}
			set
			{
				this.ViewState["FirstBulletNumber"] = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value for the specified <see cref="T:System.Web.UI.WebControls.BulletedList" /> control.</summary>
		/// <returns>The HTML text writer tag value.</returns>
		// Token: 0x17000917 RID: 2327
		// (get) Token: 0x06001D50 RID: 7504 RVA: 0x00047450 File Offset: 0x00045650
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				switch (this.BulletStyle)
				{
				case BulletStyle.Numbered:
				case BulletStyle.LowerAlpha:
				case BulletStyle.UpperAlpha:
				case BulletStyle.LowerRoman:
				case BulletStyle.UpperRoman:
					return HtmlTextWriterTag.Ol;
				}
				return HtmlTextWriterTag.Ul;
			}
		}

		/// <summary>Gets or sets the target window or frame in which to display the Web page content that is linked to when a hyperlink in a <see cref="T:System.Web.UI.WebControls.BulletedList" /> control is clicked.</summary>
		/// <returns>The target window or frame in which to load the Web page linked to when a hyperlink in a <see cref="T:System.Web.UI.WebControls.BulletedList" /> is clicked. The default is an empty string ("").</returns>
		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x06001D51 RID: 7505 RVA: 0x00047499 File Offset: 0x00045699
		// (set) Token: 0x06001D52 RID: 7506 RVA: 0x000444A2 File Offset: 0x000426A2
		[DefaultValue("")]
		[TypeConverter(typeof(TargetConverter))]
		public virtual string Target
		{
			get
			{
				return this.ViewState.GetString("Target", string.Empty);
			}
			set
			{
				this.ViewState["Target"] = value;
			}
		}

		/// <summary>Gets or sets the text for the <see cref="T:System.Web.UI.WebControls.BulletedList" /> control.</summary>
		/// <returns>The <see cref="P:System.Web.UI.WebControls.ListControl.SelectedValue" /> of the <see cref="T:System.Web.UI.WebControls.BulletedList" />, if one of the items in the <see cref="T:System.Web.UI.WebControls.BulletedList" /> is selected; otherwise, an empty string ("").</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt was made to assign a value to the <see cref="P:System.Web.UI.WebControls.BulletedList.Text" />. </exception>
		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x06001D53 RID: 7507 RVA: 0x0000EE63 File Offset: 0x0000D063
		// (set) Token: 0x06001D54 RID: 7508 RVA: 0x00003A0D File Offset: 0x00001C0D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string Text
		{
			get
			{
				return string.Empty;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.BulletedList.Click" /> event for the <see cref="T:System.Web.UI.WebControls.BulletedList" /> control.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.BulletedListEventArgs" /> that contains the event data. </param>
		// Token: 0x06001D55 RID: 7509 RVA: 0x000474B0 File Offset: 0x000456B0
		protected virtual void OnClick(BulletedListEventArgs e)
		{
			if (base.Events != null)
			{
				BulletedListEventHandler bulletedListEventHandler = (BulletedListEventHandler)base.Events[BulletedList.ClickEvent];
				if (bulletedListEventHandler != null)
				{
					bulletedListEventHandler(this, e);
				}
			}
		}

		// Token: 0x06001D57 RID: 7511 RVA: 0x000474EE File Offset: 0x000456EE
		// Note: this type is marked as 'beforefieldinit'.
		static BulletedList()
		{
			BulletedList.ClickEvent = new object();
		}

		/// <summary>Gets or sets a value that indicates whether the control is rendered if the data source has no data or if the control is not data-bound.</summary>
		/// <returns>true if the control is rendered if the data source has no data or if the control is not data-bound; otherwise false.</returns>
		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x06001D58 RID: 7512 RVA: 0x000474FC File Offset: 0x000456FC
		// (set) Token: 0x06001D59 RID: 7513 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual bool RenderWhenDataEmpty
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x0400182A RID: 6186
		private PostBackOptions postBackOptions;
	}
}

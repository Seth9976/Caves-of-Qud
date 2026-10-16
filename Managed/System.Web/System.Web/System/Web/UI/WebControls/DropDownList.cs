using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a control that allows the user to select a single item from a drop-down list. </summary>
	// Token: 0x0200038C RID: 908
	[ValidationProperty("SelectedItem")]
	[SupportsEventValidation]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class DropDownList : ListControl, IPostBackDataHandler
	{
		/// <summary>Gets or sets the border color of the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> that represents the border color of the control.</returns>
		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x06002394 RID: 9108 RVA: 0x0005A5BA File Offset: 0x000587BA
		// (set) Token: 0x06002395 RID: 9109 RVA: 0x0005A5C2 File Offset: 0x000587C2
		[Browsable(false)]
		public override Color BorderColor
		{
			get
			{
				return base.BorderColor;
			}
			set
			{
				base.BorderColor = value;
			}
		}

		/// <summary>Gets or sets the border style of the control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.BorderStyle" /> values.</returns>
		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x06002396 RID: 9110 RVA: 0x0005A5CB File Offset: 0x000587CB
		// (set) Token: 0x06002397 RID: 9111 RVA: 0x0005A5D3 File Offset: 0x000587D3
		[Browsable(false)]
		public override BorderStyle BorderStyle
		{
			get
			{
				return base.BorderStyle;
			}
			set
			{
				base.BorderStyle = value;
			}
		}

		/// <summary>Gets or sets the border width for the control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Unit" /> that represents the border width for the control.</returns>
		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x06002398 RID: 9112 RVA: 0x0005A5DC File Offset: 0x000587DC
		// (set) Token: 0x06002399 RID: 9113 RVA: 0x0005A5E4 File Offset: 0x000587E4
		[Browsable(false)]
		public override Unit BorderWidth
		{
			get
			{
				return base.BorderWidth;
			}
			set
			{
				base.BorderWidth = value;
			}
		}

		/// <summary>Gets or sets the index of the selected item in the <see cref="T:System.Web.UI.WebControls.DropDownList" /> control.</summary>
		/// <returns>The index of the selected item in the <see cref="T:System.Web.UI.WebControls.DropDownList" /> control. The default value is 0, which selects the first item in the list.</returns>
		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x0600239A RID: 9114 RVA: 0x0005A5F0 File Offset: 0x000587F0
		// (set) Token: 0x0600239B RID: 9115 RVA: 0x0005A62A File Offset: 0x0005882A
		[DefaultValue(0)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("")]
		[WebCategory("Misc")]
		public override int SelectedIndex
		{
			get
			{
				int selectedIndex = base.SelectedIndex;
				if (selectedIndex != -1 || this.Items.Count == 0)
				{
					return selectedIndex;
				}
				this.Items[0].Selected = true;
				return 0;
			}
			set
			{
				base.SelectedIndex = value;
			}
		}

		/// <summary>Gets a value that indicates whether the control should set the disabled attribute of the rendered HTML element to "disabled" when the control's <see cref="P:System.Web.UI.WebControls.WebControl.IsEnabled" /> property is false.</summary>
		/// <returns>true if the <see cref="P:System.Web.UI.Control.RenderingCompatibility" /> property indicates an ASP.NET version lower than 4.0; otherwise, false.</returns>
		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x0600239C RID: 9116 RVA: 0x00044E29 File Offset: 0x00043029
		public override bool SupportsDisabledAttribute
		{
			get
			{
				return base.RenderingCompatibilityLessThan40;
			}
		}

		/// <summary>Adds HTML attributes and styles that need to be rendered to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream that renders HTML contents to the client.</param>
		// Token: 0x0600239D RID: 9117 RVA: 0x0005A634 File Offset: 0x00058834
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			Page page = this.Page;
			if (page != null)
			{
				page.VerifyRenderingInServerForm(this);
			}
			if (writer == null)
			{
				return;
			}
			if (!string.IsNullOrEmpty(this.UniqueID))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Name, this.UniqueID, true);
			}
			if (!base.IsEnabled && this.SelectedIndex == -1)
			{
				this.SelectedIndex = 1;
			}
			if (this.AutoPostBack)
			{
				string text = ((page != null) ? page.ClientScript.GetPostBackEventReference(this.GetPostBackOptions(), true) : string.Empty);
				text = "setTimeout('" + text.Replace("\\", "\\\\").Replace("'", "\\'") + "', 0)";
				writer.AddAttribute(HtmlTextWriterAttribute.Onchange, base.BuildScriptAttribute("onchange", text));
			}
			base.AddAttributesToRender(writer);
		}

		// Token: 0x0600239E RID: 9118 RVA: 0x0005A6FC File Offset: 0x000588FC
		private PostBackOptions GetPostBackOptions()
		{
			PostBackOptions postBackOptions = new PostBackOptions(this);
			postBackOptions.ActionUrl = null;
			postBackOptions.ValidationGroup = null;
			postBackOptions.Argument = string.Empty;
			postBackOptions.RequiresJavaScriptProtocol = false;
			postBackOptions.ClientSubmit = true;
			Page page = this.Page;
			postBackOptions.PerformValidation = this.CausesValidation && page != null && page.AreValidatorsUplevel(this.ValidationGroup);
			if (postBackOptions.PerformValidation)
			{
				postBackOptions.ValidationGroup = this.ValidationGroup;
			}
			return postBackOptions;
		}

		/// <summary>Creates a collection to store child controls.</summary>
		/// <returns>Always returns an <see cref="T:System.Web.UI.EmptyControlCollection" />.</returns>
		// Token: 0x0600239F RID: 9119 RVA: 0x0004862A File Offset: 0x0004682A
		protected override ControlCollection CreateControlCollection()
		{
			return base.CreateControlCollection();
		}

		/// <summary>Always throws an <see cref="T:System.Web.HttpException" /> exception because multiple selection is not supported for the <see cref="T:System.Web.UI.WebControls.DropDownList" /> control.</summary>
		/// <exception cref="T:System.Web.HttpException">In all cases.</exception>
		// Token: 0x060023A0 RID: 9120 RVA: 0x0005A773 File Offset: 0x00058973
		protected internal override void VerifyMultiSelect()
		{
			throw new HttpException("DropDownList only may have a single selected item");
		}

		/// <summary>Processes postback data for the <see cref="T:System.Web.UI.WebControls.DropDownList" /> control.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.DropDownList" /> control's state changes as a result of the postback event; otherwise, false.</returns>
		/// <param name="postDataKey">The index within the posted collection that references the content to load.</param>
		/// <param name="postCollection">The collection of all incoming name values posted to the server.</param>
		// Token: 0x060023A1 RID: 9121 RVA: 0x0005A780 File Offset: 0x00058980
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			this.EnsureDataBound();
			int num = this.Items.IndexOf(postCollection[postDataKey]);
			base.ValidateEvent(postDataKey, postCollection[postDataKey]);
			if (num != this.SelectedIndex)
			{
				this.SelectedIndex = num;
				return true;
			}
			return false;
		}

		/// <summary>Raises events for the <see cref="T:System.Web.UI.WebControls.DropDownList" /> control when postback occurs.</summary>
		// Token: 0x060023A2 RID: 9122 RVA: 0x0005A7C8 File Offset: 0x000589C8
		protected virtual void RaisePostDataChangedEvent()
		{
			if (this.CausesValidation)
			{
				Page page = this.Page;
				if (page != null)
				{
					page.Validate(this.ValidationGroup);
				}
			}
			this.OnSelectedIndexChanged(EventArgs.Empty);
		}

		/// <summary>Processes posted data for the <see cref="T:System.Web.UI.WebControls.DropDownList" /> control.</summary>
		/// <returns>true if the posted content is different from the last posting; otherwise, false.</returns>
		/// <param name="postDataKey">The key value used to index an entry in the collection. </param>
		/// <param name="postCollection">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> that contains post information.  </param>
		// Token: 0x060023A3 RID: 9123 RVA: 0x0005A7FE File Offset: 0x000589FE
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		/// <summary>Raises events for the <see cref="T:System.Web.UI.WebControls.DropDownList" /> control on postback.</summary>
		// Token: 0x060023A4 RID: 9124 RVA: 0x0005A808 File Offset: 0x00058A08
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}
	}
}

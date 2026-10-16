using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a list box control that allows single or multiple item selection.</summary>
	// Token: 0x020003BA RID: 954
	[ValidationProperty("SelectedItem")]
	[SupportsEventValidation]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class ListBox : ListControl, IPostBackDataHandler
	{
		/// <summary>Gets or sets the border color of the control.</summary>
		/// <returns>A <see cref="T:System.Drawing.Color" /> object that represents the border color of the control.</returns>
		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x06002753 RID: 10067 RVA: 0x0005A5BA File Offset: 0x000587BA
		// (set) Token: 0x06002754 RID: 10068 RVA: 0x0005A5C2 File Offset: 0x000587C2
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
		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x06002755 RID: 10069 RVA: 0x0005A5CB File Offset: 0x000587CB
		// (set) Token: 0x06002756 RID: 10070 RVA: 0x0005A5D3 File Offset: 0x000587D3
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
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Unit" /> object that represents the border width of the control.</returns>
		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x06002757 RID: 10071 RVA: 0x0005A5DC File Offset: 0x000587DC
		// (set) Token: 0x06002758 RID: 10072 RVA: 0x0005A5E4 File Offset: 0x000587E4
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

		/// <summary>Gets or sets the number of rows displayed in the <see cref="T:System.Web.UI.WebControls.ListBox" /> control.</summary>
		/// <returns>The number of rows displayed in the <see cref="T:System.Web.UI.WebControls.ListBox" /> control. The default value is 4.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified number of rows is less than one or greater than 2000. </exception>
		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x06002759 RID: 10073 RVA: 0x0006449A File Offset: 0x0006269A
		// (set) Token: 0x0600275A RID: 10074 RVA: 0x000644AD File Offset: 0x000626AD
		[DefaultValue(4)]
		[WebSysDescription("")]
		[WebCategory("Appearance")]
		public virtual int Rows
		{
			get
			{
				return this.ViewState.GetInt("Rows", 4);
			}
			set
			{
				if (value < 1 || value > 2000)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["Rows"] = value;
			}
		}

		/// <summary>Gets or sets the selection mode of the <see cref="T:System.Web.UI.WebControls.ListBox" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.ListSelectionMode" /> values. The default value is Single.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified selection mode is not one of the <see cref="T:System.Web.UI.WebControls.ListSelectionMode" /> values. </exception>
		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x0600275B RID: 10075 RVA: 0x000644DC File Offset: 0x000626DC
		// (set) Token: 0x0600275C RID: 10076 RVA: 0x000644EF File Offset: 0x000626EF
		[WebSysDescription("")]
		[WebCategory("Behavior")]
		[DefaultValue(ListSelectionMode.Single)]
		public virtual ListSelectionMode SelectionMode
		{
			get
			{
				return (ListSelectionMode)this.ViewState.GetInt("SelectionMode", 0);
			}
			set
			{
				if (!Enum.IsDefined(typeof(ListSelectionMode), value))
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["SelectionMode"] = value;
			}
		}

		/// <summary>Gets the array of index values for currently selected items in the <see cref="T:System.Web.UI.WebControls.ListBox" /> control.</summary>
		/// <returns>An array of integers, each representing the index of a selected item in the list box.</returns>
		// Token: 0x0600275D RID: 10077 RVA: 0x00064529 File Offset: 0x00062729
		public virtual int[] GetSelectedIndices()
		{
			return (int[])base.GetSelectedIndicesInternal().ToArray(typeof(int));
		}

		/// <summary>Adds name, size, multiple, and onchange to the list of attributes to render.</summary>
		/// <param name="writer">The output stream that renders HTML content to the client. </param>
		// Token: 0x0600275E RID: 10078 RVA: 0x00064548 File Offset: 0x00062748
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			if (this.Page != null)
			{
				this.Page.VerifyRenderingInServerForm(this);
			}
			if (this.ID != null)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Name, this.UniqueID);
			}
			if (this.AutoPostBack)
			{
				string text = this.Page.ClientScript.GetPostBackEventReference(this.GetPostBackOptions(), true);
				text = "setTimeout('" + text.Replace("\\", "\\\\").Replace("'", "\\'") + "', 0)";
				writer.AddAttribute(HtmlTextWriterAttribute.Onchange, base.BuildScriptAttribute("onchange", text));
			}
			if (this.SelectionMode == ListSelectionMode.Multiple)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Multiple, "multiple", false);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Size, this.Rows.ToString(Helpers.InvariantCulture));
			base.AddAttributesToRender(writer);
		}

		// Token: 0x0600275F RID: 10079 RVA: 0x0006461C File Offset: 0x0006281C
		private PostBackOptions GetPostBackOptions()
		{
			PostBackOptions postBackOptions = new PostBackOptions(this);
			postBackOptions.ActionUrl = null;
			postBackOptions.ValidationGroup = null;
			postBackOptions.Argument = string.Empty;
			postBackOptions.RequiresJavaScriptProtocol = false;
			postBackOptions.ClientSubmit = true;
			postBackOptions.PerformValidation = this.CausesValidation && this.Page != null && this.Page.AreValidatorsUplevel(this.ValidationGroup);
			if (postBackOptions.PerformValidation)
			{
				postBackOptions.ValidationGroup = this.ValidationGroup;
			}
			return postBackOptions;
		}

		/// <summary>Configures the <see cref="T:System.Web.UI.WebControls.ListBox" /> control prior to rendering on the client.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
		// Token: 0x06002760 RID: 10080 RVA: 0x00064698 File Offset: 0x00062898
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			Page page = this.Page;
			if (page != null && base.IsEnabled)
			{
				page.RegisterRequiresPostBack(this);
			}
		}

		/// <summary>Loads the posted content of the list control, if it is different from the last posting.</summary>
		/// <returns>true if the posted content is different from the last posting; otherwise, false.</returns>
		/// <param name="postDataKey">The key identifier for the control, used to index the <paramref name="postCollection" />.</param>
		/// <param name="postCollection">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> that contains value information indexed by control identifiers. </param>
		// Token: 0x06002761 RID: 10081 RVA: 0x000646C8 File Offset: 0x000628C8
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			this.EnsureDataBound();
			string[] values = postCollection.GetValues(postDataKey);
			if (values == null || values.Length == 0)
			{
				int selectedIndex = this.SelectedIndex;
				this.SelectedIndex = -1;
				return selectedIndex != -1;
			}
			base.ValidateEvent(this.UniqueID, values[0]);
			if (this.SelectionMode == ListSelectionMode.Single)
			{
				return this.SelectSingle(values);
			}
			return this.SelectMultiple(values);
		}

		// Token: 0x06002762 RID: 10082 RVA: 0x00064724 File Offset: 0x00062924
		private bool SelectSingle(string[] values)
		{
			string text = values[0];
			int num = this.Items.IndexOf(text);
			int selectedIndex = this.SelectedIndex;
			if (num != selectedIndex)
			{
				this.SelectedIndex = num;
				return true;
			}
			return false;
		}

		// Token: 0x06002763 RID: 10083 RVA: 0x00064758 File Offset: 0x00062958
		private bool SelectMultiple(string[] values)
		{
			ArrayList selectedIndicesInternal = base.GetSelectedIndicesInternal();
			this.ClearSelection();
			foreach (string text in values)
			{
				ListItem listItem = this.Items.FindByValue(text);
				if (listItem != null)
				{
					listItem.Selected = true;
				}
			}
			ArrayList selectedIndicesInternal2 = base.GetSelectedIndicesInternal();
			int num = selectedIndicesInternal.Count;
			if (selectedIndicesInternal2.Count != num)
			{
				return true;
			}
			while (--num >= 0)
			{
				if ((int)selectedIndicesInternal[num] != (int)selectedIndicesInternal2[num])
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Invokes the <see cref="M:System.Web.UI.WebControls.ListControl.OnSelectedIndexChanged(System.EventArgs)" /> method whenever posted data for the <see cref="T:System.Web.UI.WebControls.ListBox" /> control has changed.</summary>
		// Token: 0x06002764 RID: 10084 RVA: 0x000647E5 File Offset: 0x000629E5
		protected virtual void RaisePostDataChangedEvent()
		{
			if (this.CausesValidation)
			{
				this.Page.Validate(this.ValidationGroup);
			}
			this.OnSelectedIndexChanged(EventArgs.Empty);
		}

		/// <summary>Loads the posted content of the list control, if it is different from the last posting.</summary>
		/// <returns>true if the posted content is different from the last posting; otherwise, false.</returns>
		/// <param name="postDataKey">The index within the posted collection that references the content to load. </param>
		/// <param name="postCollection">The collection posted to the server. </param>
		// Token: 0x06002765 RID: 10085 RVA: 0x0006480B File Offset: 0x00062A0B
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		/// <summary>Invokes the <see cref="M:System.Web.UI.WebControls.ListControl.OnSelectedIndexChanged(System.EventArgs)" /> method whenever posted data for the <see cref="T:System.Web.UI.WebControls.ListBox" /> control has changed.</summary>
		// Token: 0x06002766 RID: 10086 RVA: 0x00064815 File Offset: 0x00062A15
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		// Token: 0x06002767 RID: 10087 RVA: 0x0006481D File Offset: 0x00062A1D
		internal override bool MultiSelectOk()
		{
			return this.SelectionMode == ListSelectionMode.Multiple;
		}
	}
}

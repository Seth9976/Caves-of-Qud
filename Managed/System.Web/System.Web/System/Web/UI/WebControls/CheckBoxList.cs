using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.Util;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Creates a multi selection check box group that can be dynamically created by binding the control to a data source.</summary>
	// Token: 0x02000349 RID: 841
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class CheckBoxList : ListControl, IRepeatInfoUser, INamingContainer, IPostBackDataHandler
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> class.</summary>
		// Token: 0x06001EFD RID: 7933 RVA: 0x0004C852 File Offset: 0x0004AA52
		public CheckBoxList()
		{
			this.check_box = new CheckBox();
			this.Controls.Add(this.check_box);
		}

		/// <summary>Gets or sets the distance (in pixels) between the border and contents of the cell.</summary>
		/// <returns>The distance (in pixels) between the border and contents of the cell. The default is -1, which indicates this property is not set.</returns>
		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x06001EFE RID: 7934 RVA: 0x0004C876 File Offset: 0x0004AA76
		// (set) Token: 0x06001EFF RID: 7935 RVA: 0x0004C883 File Offset: 0x0004AA83
		[DefaultValue(-1)]
		[WebSysDescription("")]
		[WebCategory("Layout")]
		public virtual int CellPadding
		{
			get
			{
				return this.TableStyle.CellPadding;
			}
			set
			{
				this.TableStyle.CellPadding = value;
			}
		}

		/// <summary>Gets or sets the distance (in pixels) between cells.</summary>
		/// <returns>The distance (in pixels) between cells. The default is -1, which indicates that this property is not set.</returns>
		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x06001F00 RID: 7936 RVA: 0x0004C891 File Offset: 0x0004AA91
		// (set) Token: 0x06001F01 RID: 7937 RVA: 0x0004C89E File Offset: 0x0004AA9E
		[DefaultValue(-1)]
		[WebSysDescription("")]
		[WebCategory("Layout")]
		public virtual int CellSpacing
		{
			get
			{
				return this.TableStyle.CellSpacing;
			}
			set
			{
				this.TableStyle.CellSpacing = value;
			}
		}

		/// <summary>Gets or sets the number of columns to display in the <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> control.</summary>
		/// <returns>The number of columns to display in the <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> control. The default is 0, which indicates this property is not set.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The number of columns is set to a negative value. </exception>
		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x06001F02 RID: 7938 RVA: 0x0004C8AC File Offset: 0x0004AAAC
		// (set) Token: 0x06001F03 RID: 7939 RVA: 0x0004C8BF File Offset: 0x0004AABF
		[WebSysDescription("")]
		[WebCategory("Layout")]
		[DefaultValue(0)]
		public virtual int RepeatColumns
		{
			get
			{
				return this.ViewState.GetInt("RepeatColumns", 0);
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["RepeatColumns"] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the control displays vertically or horizontally.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.RepeatDirection" /> values. The default is Vertical.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified display direction of the list is not one of the <see cref="T:System.Web.UI.WebControls.RepeatDirection" /> values. </exception>
		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x06001F04 RID: 7940 RVA: 0x0004C8E6 File Offset: 0x0004AAE6
		// (set) Token: 0x06001F05 RID: 7941 RVA: 0x0004C8F9 File Offset: 0x0004AAF9
		[WebCategory("Layout")]
		[DefaultValue(RepeatDirection.Vertical)]
		[WebSysDescription("")]
		public virtual RepeatDirection RepeatDirection
		{
			get
			{
				return (RepeatDirection)this.ViewState.GetInt("RepeatDirection", 1);
			}
			set
			{
				if (value < RepeatDirection.Horizontal || value > RepeatDirection.Vertical)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["RepeatDirection"] = value;
			}
		}

		/// <summary>Gets or sets a value that specifies whether the list will be rendered by using a table element, a ul element, an ol element, or a span element.</summary>
		/// <returns>A value that specifies whether the list will be rendered by using a table element, a ul element, an ol element, or a span element. The default is <see cref="F:System.Web.UI.WebControls.RepeatLayout.Table" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified layout is not one of the <see cref="T:System.Web.UI.WebControls.RepeatLayout" /> values. </exception>
		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x06001F06 RID: 7942 RVA: 0x0004C924 File Offset: 0x0004AB24
		// (set) Token: 0x06001F07 RID: 7943 RVA: 0x0004C937 File Offset: 0x0004AB37
		[DefaultValue(RepeatLayout.Table)]
		[WebSysDescription("")]
		[WebCategory("Layout")]
		public virtual RepeatLayout RepeatLayout
		{
			get
			{
				return (RepeatLayout)this.ViewState.GetInt("RepeatLayout", 0);
			}
			set
			{
				if (value < RepeatLayout.Table || value > RepeatLayout.OrderedList)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["RepeatLayout"] = value;
			}
		}

		/// <summary>Gets or sets the text alignment for the check boxes within the group.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.TextAlign" /> values. The default value is Right.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified label text alignment is not one of the <see cref="T:System.Web.UI.WebControls.TextAlign" /> values. </exception>
		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x06001F08 RID: 7944 RVA: 0x0004BBBB File Offset: 0x00049DBB
		// (set) Token: 0x06001F09 RID: 7945 RVA: 0x0004C967 File Offset: 0x0004AB67
		[WebCategory("Appearance")]
		[DefaultValue(TextAlign.Right)]
		[WebSysDescription("")]
		public virtual TextAlign TextAlign
		{
			get
			{
				return (TextAlign)this.ViewState.GetInt("TextAlign", 2);
			}
			set
			{
				if (value < TextAlign.Left || value > TextAlign.Right)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["TextAlign"] = value;
			}
		}

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x06001F0A RID: 7946 RVA: 0x00045292 File Offset: 0x00043492
		private TableStyle TableStyle
		{
			get
			{
				return (TableStyle)base.ControlStyle;
			}
		}

		/// <summary>Creates a style object that is used internally by the <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> control to implement all style related properties.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> that contains the style properties of the control.</returns>
		// Token: 0x06001F0B RID: 7947 RVA: 0x0004C992 File Offset: 0x0004AB92
		protected override Style CreateControlStyle()
		{
			return new TableStyle(this.ViewState);
		}

		/// <summary>Searches the current naming container for a server control with the specified ID and path offset. The <see cref="M:System.Web.UI.WebControls.CheckBoxList.FindControl(System.String,System.Int32)" /> method always returns the <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> object. </summary>
		/// <returns>The current <see cref="T:System.Web.UI.WebControls.CheckBoxList" />.</returns>
		/// <param name="id">The identifier for the control to find.</param>
		/// <param name="pathOffset">The number of controls up the page control hierarchy needed to reach a naming container. </param>
		// Token: 0x06001F0C RID: 7948 RVA: 0x00002058 File Offset: 0x00000258
		protected override Control FindControl(string id, int pathOffset)
		{
			return this;
		}

		/// <summary>Configures the <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> control prior to rendering on the client.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001F0D RID: 7949 RVA: 0x0004C9A0 File Offset: 0x0004ABA0
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			Page page = this.Page;
			for (int i = 0; i < this.Items.Count; i++)
			{
				if (this.Items[i].Selected)
				{
					this.check_box.ID = i.ToString(Helpers.InvariantCulture);
					if (page != null)
					{
						page.RegisterRequiresPostBack(this.check_box);
					}
				}
			}
		}

		/// <summary>Displays the <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> on the client.</summary>
		/// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream for rendering on the client. </param>
		// Token: 0x06001F0E RID: 7950 RVA: 0x0004CA0C File Offset: 0x0004AC0C
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (this.Items.Count == 0)
			{
				return;
			}
			RepeatInfo repeatInfo = new RepeatInfo();
			repeatInfo.RepeatColumns = this.RepeatColumns;
			repeatInfo.RepeatDirection = this.RepeatDirection;
			repeatInfo.RepeatLayout = this.RepeatLayout;
			short num = 0;
			if (this.TabIndex != 0)
			{
				this.check_box.TabIndex = this.TabIndex;
				num = this.TabIndex;
				this.TabIndex = 0;
			}
			string accessKey = this.AccessKey;
			this.check_box.AccessKey = accessKey;
			this.AccessKey = null;
			repeatInfo.RenderRepeater(writer, this, this.TableStyle, this);
			if (num != 0)
			{
				this.TabIndex = num;
			}
			this.AccessKey = accessKey;
		}

		/// <summary>Processes the posted data for the <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> control.</summary>
		/// <returns>true if the state of the <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> is different from the last posting; otherwise, false.</returns>
		/// <param name="postDataKey">The key identifier for the control, used to index the <see cref="T:System.Collections.Specialized.NameValueCollection" /> specified in the <paramref name="postCollection" /> parameter.</param>
		/// <param name="postCollection">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> that contains value information indexed by control identifiers. </param>
		// Token: 0x06001F0F RID: 7951 RVA: 0x0004CAB4 File Offset: 0x0004ACB4
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			if (!base.IsEnabled)
			{
				return false;
			}
			this.EnsureDataBound();
			int num = -1;
			try
			{
				string text = postDataKey.Substring(this.ClientID.Length + 1);
				if (char.IsDigit(text[0]))
				{
					num = int.Parse(text, Helpers.InvariantCulture);
				}
			}
			catch
			{
				return false;
			}
			if (num == -1)
			{
				return false;
			}
			ListItem listItem = this.Items[num];
			if (listItem.Enabled)
			{
				string text2 = postCollection[postDataKey];
				bool flag = text2 == "on";
				if (!base.RenderingCompatibilityLessThan40)
				{
					flag = text2 == listItem.Value;
				}
				if (flag && !listItem.Selected)
				{
					listItem.Selected = true;
					return true;
				}
				if (!flag && listItem.Selected)
				{
					listItem.Selected = false;
					return true;
				}
			}
			return false;
		}

		/// <summary>Notifies the ASP.NET application that the state of the <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> control has changed.</summary>
		// Token: 0x06001F10 RID: 7952 RVA: 0x0004CB90 File Offset: 0x0004AD90
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

		/// <summary>Processes posted data for the <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> control.</summary>
		/// <returns>true if the server control's state changes as a result of the postback; otherwise, false.</returns>
		/// <param name="postDataKey">The key identifier for the control, used to index the <paramref name="postCollection" />.</param>
		/// <param name="postCollection">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> object that contains value information indexed by control identifiers. </param>
		// Token: 0x06001F11 RID: 7953 RVA: 0x0004CBC6 File Offset: 0x0004ADC6
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		/// <summary>Raised when posted data for a control has changed.</summary>
		// Token: 0x06001F12 RID: 7954 RVA: 0x0004CBD0 File Offset: 0x0004ADD0
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> control contains a footer section.</summary>
		/// <returns>false, indicating that a <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> does not contain a footer section.</returns>
		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x06001F13 RID: 7955 RVA: 0x00008A4D File Offset: 0x00006C4D
		protected virtual bool HasFooter
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether the list control contains a footer section.</summary>
		/// <returns>true if the list control contains a footer section; otherwise, false.</returns>
		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x06001F14 RID: 7956 RVA: 0x0004CBD8 File Offset: 0x0004ADD8
		bool IRepeatInfoUser.HasFooter
		{
			get
			{
				return this.HasFooter;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> control contains a heading section. </summary>
		/// <returns>false, indicating that a <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> does not contain a heading section.</returns>
		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x06001F15 RID: 7957 RVA: 0x00008A4D File Offset: 0x00006C4D
		protected virtual bool HasHeader
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether the list control contains a heading section.</summary>
		/// <returns>true if the list control contains a heading section; otherwise, false.</returns>
		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x06001F16 RID: 7958 RVA: 0x0004CBE0 File Offset: 0x0004ADE0
		bool IRepeatInfoUser.HasHeader
		{
			get
			{
				return this.HasHeader;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> control contains a separator between items in the list. </summary>
		/// <returns>false, indicating that a <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> does not contain separators.</returns>
		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x06001F17 RID: 7959 RVA: 0x00008A4D File Offset: 0x00006C4D
		protected virtual bool HasSeparators
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether the list control contains a separator between items in the list.</summary>
		/// <returns>true if the list control contains a separator; otherwise, false.</returns>
		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x06001F18 RID: 7960 RVA: 0x0004CBE8 File Offset: 0x0004ADE8
		bool IRepeatInfoUser.HasSeparators
		{
			get
			{
				return this.HasSeparators;
			}
		}

		/// <summary>Gets the number of list items in the <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> control. </summary>
		/// <returns>The number of items in the <see cref="T:System.Web.UI.WebControls.CheckBoxList" />.</returns>
		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x06001F19 RID: 7961 RVA: 0x0004CBF0 File Offset: 0x0004ADF0
		protected virtual int RepeatedItemCount
		{
			get
			{
				return this.Items.Count;
			}
		}

		/// <summary>Gets the number of items in the list control.</summary>
		/// <returns>The number of items in the list.</returns>
		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x06001F1A RID: 7962 RVA: 0x0004CBFD File Offset: 0x0004ADFD
		int IRepeatInfoUser.RepeatedItemCount
		{
			get
			{
				return this.RepeatedItemCount;
			}
		}

		/// <summary>Retrieves the style of the specified item type at the specified index in the list control.</summary>
		/// <returns>null, indicating that style attributes are not set on individual list items in a <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> control.</returns>
		/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> enumeration values. </param>
		/// <param name="repeatIndex">An ordinal index that specifies the location of the item in the list control. </param>
		// Token: 0x06001F1B RID: 7963 RVA: 0x00003BF6 File Offset: 0x00001DF6
		protected virtual Style GetItemStyle(ListItemType itemType, int repeatIndex)
		{
			return null;
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.WebControls.IRepeatInfoUser.GetItemStyle(System.Web.UI.WebControls.ListItemType,System.Int32)" />. </summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> object that represents the style of the specified item type at the specified index in the list control.</returns>
		/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> enumeration values. </param>
		/// <param name="repeatIndex">An ordinal index that specifies the location of the item in the list control. </param>
		// Token: 0x06001F1C RID: 7964 RVA: 0x0004CC05 File Offset: 0x0004AE05
		Style IRepeatInfoUser.GetItemStyle(ListItemType itemType, int repeatIndex)
		{
			return this.GetItemStyle(itemType, repeatIndex);
		}

		/// <summary>Renders a list item in the <see cref="T:System.Web.UI.WebControls.CheckBoxList" /> control.</summary>
		/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> enumeration values. </param>
		/// <param name="repeatIndex">An ordinal index that specifies the location of the item in the list control. </param>
		/// <param name="repeatInfo">A <see cref="T:System.Web.UI.WebControls.RepeatInfo" /> object that represents the information used to render the item in the list. </param>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> object that represents the output stream to render HTML content on the client. </param>
		// Token: 0x06001F1D RID: 7965 RVA: 0x0004CC10 File Offset: 0x0004AE10
		protected virtual void RenderItem(ListItemType itemType, int repeatIndex, RepeatInfo repeatInfo, HtmlTextWriter writer)
		{
			ListItem listItem = this.Items[repeatIndex];
			if (!string.IsNullOrEmpty(this.check_box.CssClass))
			{
				this.check_box.CssClass = string.Empty;
			}
			this.check_box.ID = repeatIndex.ToString(Helpers.InvariantCulture);
			this.check_box.Text = listItem.Text;
			this.check_box.AutoPostBack = this.AutoPostBack;
			this.check_box.Checked = listItem.Selected;
			this.check_box.TextAlign = this.TextAlign;
			if (!base.IsEnabled)
			{
				this.check_box.Enabled = false;
			}
			else
			{
				this.check_box.Enabled = listItem.Enabled;
			}
			this.check_box.ValidationGroup = this.ValidationGroup;
			this.check_box.CausesValidation = this.CausesValidation;
			if (this.check_box.HasAttributes)
			{
				this.check_box.Attributes.Clear();
			}
			if (listItem.HasAttributes)
			{
				this.check_box.Attributes.CopyFrom(listItem.Attributes);
			}
			if (!base.RenderingCompatibilityLessThan40)
			{
				AttributeCollection inputAttributes = this.check_box.InputAttributes;
				inputAttributes.Clear();
				inputAttributes.Add("value", listItem.Value);
			}
			this.check_box.RenderControl(writer);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.WebControls.IRepeatInfoUser.RenderItem(System.Web.UI.WebControls.ListItemType,System.Int32,System.Web.UI.WebControls.RepeatInfo,System.Web.UI.HtmlTextWriter)" />. </summary>
		/// <param name="itemType">One of the <see cref="T:System.Web.UI.WebControls.ListItemType" /> enumeration values. </param>
		/// <param name="repeatIndex">An ordinal index that specifies the location of the item in the list control. </param>
		/// <param name="repeatInfo">A <see cref="T:System.Web.UI.WebControls.RepeatInfo" /> object that represents the information used to render the item in the list. </param>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> object that represents the output stream to render HTML content on the client. </param>
		// Token: 0x06001F1E RID: 7966 RVA: 0x0004CD61 File Offset: 0x0004AF61
		void IRepeatInfoUser.RenderItem(ListItemType itemType, int repeatIndex, RepeatInfo repeatInfo, HtmlTextWriter writer)
		{
			this.RenderItem(itemType, repeatIndex, repeatInfo, writer);
		}

		// Token: 0x06001F1F RID: 7967 RVA: 0x00008B4A File Offset: 0x00006D4A
		internal override bool MultiSelectOk()
		{
			return true;
		}

		/// <summary>Gets or sets a value that indicates whether the control is rendered when the data source has no data or the control is not data-bound.</summary>
		/// <returns>true if the control is rendered when the data source has no data or the control is not data-bound; otherwise false.</returns>
		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x06001F20 RID: 7968 RVA: 0x0004CD70 File Offset: 0x0004AF70
		// (set) Token: 0x06001F21 RID: 7969 RVA: 0x0000B3D8 File Offset: 0x000095D8
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

		// Token: 0x0400187D RID: 6269
		private CheckBox check_box;
	}
}

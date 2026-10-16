using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a radio button control.</summary>
	// Token: 0x020003F3 RID: 1011
	[SupportsEventValidation]
	[Designer("System.Web.UI.Design.WebControls.CheckBoxDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class RadioButton : CheckBox, IPostBackDataHandler
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.RadioButton" /> class.</summary>
		// Token: 0x06002C94 RID: 11412 RVA: 0x000745A3 File Offset: 0x000727A3
		public RadioButton()
			: base("radio")
		{
		}

		/// <summary>Gets or sets the name of the group that the radio button belongs to.</summary>
		/// <returns>The name of the group that the radio button belongs to. The default is an empty string ("").</returns>
		// Token: 0x17000E46 RID: 3654
		// (get) Token: 0x06002C95 RID: 11413 RVA: 0x000745B0 File Offset: 0x000727B0
		// (set) Token: 0x06002C96 RID: 11414 RVA: 0x000745C7 File Offset: 0x000727C7
		[WebCategory("Behavior")]
		[WebSysDescription("")]
		[Themeable(false)]
		[DefaultValue("")]
		public virtual string GroupName
		{
			get
			{
				return this.ViewState.GetString("GroupName", string.Empty);
			}
			set
			{
				this.ViewState["GroupName"] = value;
			}
		}

		// Token: 0x17000E47 RID: 3655
		// (get) Token: 0x06002C97 RID: 11415 RVA: 0x000745DC File Offset: 0x000727DC
		internal override string NameAttribute
		{
			get
			{
				string uniqueID = this.UniqueID;
				string groupName = this.GroupName;
				if (groupName.Length == 0)
				{
					return uniqueID;
				}
				int num = -1;
				if (uniqueID != null)
				{
					num = uniqueID.LastIndexOf(base.IdSeparator);
				}
				if (num == -1)
				{
					return groupName;
				}
				return uniqueID.Substring(0, num + 1) + groupName;
			}
		}

		// Token: 0x17000E48 RID: 3656
		// (get) Token: 0x06002C98 RID: 11416 RVA: 0x0007462C File Offset: 0x0007282C
		// (set) Token: 0x06002C99 RID: 11417 RVA: 0x00061E8C File Offset: 0x0006008C
		internal string ValueAttribute
		{
			get
			{
				string text = (string)this.ViewState["Value"];
				if (text != null)
				{
					return text;
				}
				string id = this.ID;
				if (!string.IsNullOrEmpty(id))
				{
					return id;
				}
				return this.UniqueID;
			}
			set
			{
				this.ViewState["Value"] = value;
			}
		}

		// Token: 0x06002C9A RID: 11418 RVA: 0x0007466C File Offset: 0x0007286C
		internal override void InternalAddAttributesToRender(HtmlTextWriter w, bool enabled)
		{
			Page page = this.Page;
			if (page != null)
			{
				page.ClientScript.RegisterForEventValidation(this.NameAttribute, this.ValueAttribute);
			}
			base.InternalAddAttributesToRender(w, enabled);
			w.AddAttribute(HtmlTextWriterAttribute.Value, this.ValueAttribute);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06002C9B RID: 11419 RVA: 0x000746B0 File Offset: 0x000728B0
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
		}

		/// <summary>Processes postback data for the <see cref="T:System.Web.UI.WebControls.RadioButton" /> control.</summary>
		/// <returns>true if the data for the <see cref="T:System.Web.UI.WebControls.RadioButton" /> has changed; otherwise, false.</returns>
		/// <param name="postDataKey">The key identifier for the control.</param>
		/// <param name="postCollection">The collection of all incoming name values.</param>
		// Token: 0x06002C9C RID: 11420 RVA: 0x000746BC File Offset: 0x000728BC
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			string text = postCollection[this.NameAttribute];
			bool flag = text == this.ValueAttribute;
			base.ValidateEvent(this.NameAttribute, text);
			if (this.Checked == flag)
			{
				return false;
			}
			this.Checked = flag;
			return flag;
		}

		/// <summary>Raises the <see cref="E:System.Windows.Forms.RadioButton.CheckedChanged" /> event, if the <see cref="P:System.Windows.Forms.RadioButton.Checked" /> property has changed on postback.</summary>
		// Token: 0x06002C9D RID: 11421 RVA: 0x00074703 File Offset: 0x00072903
		protected override void RaisePostDataChangedEvent()
		{
			if (this.CausesValidation)
			{
				this.Page.Validate(this.ValidationGroup);
			}
			this.OnCheckedChanged(EventArgs.Empty);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IPostBackDataHandler.LoadPostData(System.String,System.Collections.Specialized.NameValueCollection)" />.</summary>
		/// <returns>true if <see cref="T:System.Web.UI.WebControls.RadioButton" /> is checked; otherwise, false. The default is false.</returns>
		/// <param name="postDataKey">A string.</param>
		/// <param name="postCollection">A name value collection that represents the posted collection of data. </param>
		// Token: 0x06002C9E RID: 11422 RVA: 0x0004C57F File Offset: 0x0004A77F
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}
	}
}

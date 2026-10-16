using System;
using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Represents a hidden field used to store a non-displayed value.</summary>
	// Token: 0x020003A7 RID: 935
	[NonVisualControl]
	[SupportsEventValidation]
	[PersistChildren(false)]
	[ParseChildren]
	[DefaultEvent("ValueChanged")]
	[Designer("System.Web.UI.Design.WebControls.HiddenFieldDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DefaultProperty("Value")]
	[ControlValueProperty("Value")]
	public class HiddenField : Control, IPostBackDataHandler
	{
		/// <summary>Occurs when the value of the <see cref="T:System.Web.UI.WebControls.HiddenField" /> control changes between posts to the server.</summary>
		// Token: 0x1400009F RID: 159
		// (add) Token: 0x06002616 RID: 9750 RVA: 0x00061E4F File Offset: 0x0006004F
		// (remove) Token: 0x06002617 RID: 9751 RVA: 0x00061E62 File Offset: 0x00060062
		public event EventHandler ValueChanged
		{
			add
			{
				base.Events.AddHandler(HiddenField.ValueChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(HiddenField.ValueChangedEvent, value);
			}
		}

		/// <summary>Gets or sets the value of the hidden field.</summary>
		/// <returns>The value of the hidden field. The default is an empty string ("").</returns>
		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x06002618 RID: 9752 RVA: 0x00061E75 File Offset: 0x00060075
		// (set) Token: 0x06002619 RID: 9753 RVA: 0x00061E8C File Offset: 0x0006008C
		[DefaultValue("")]
		[Bindable(true)]
		public virtual string Value
		{
			get
			{
				return this.ViewState.GetString("Value", string.Empty);
			}
			set
			{
				this.ViewState["Value"] = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether themes apply to this control.</summary>
		/// <returns>Always returns false to indicate that this control does not support themes.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt is made to set this property.</exception>
		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x0600261A RID: 9754 RVA: 0x00008A4D File Offset: 0x00006C4D
		// (set) Token: 0x0600261B RID: 9755 RVA: 0x00003A0D File Offset: 0x00001C0D
		[DefaultValue(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool EnableTheming
		{
			get
			{
				return false;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets or sets the skin to apply to the control.</summary>
		/// <returns>Always returns an empty string ("") to indicate that themes are not supported.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt is made to set this property.</exception>
		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x0600261C RID: 9756 RVA: 0x0000EE63 File Offset: 0x0000D063
		// (set) Token: 0x0600261D RID: 9757 RVA: 0x00003A0D File Offset: 0x00001C0D
		[DefaultValue("")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string SkinID
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

		/// <summary>Sets input focus to this control.</summary>
		/// <exception cref="T:System.NotSupportedException">An attempt is made to call this method.</exception>
		// Token: 0x0600261E RID: 9758 RVA: 0x00003A0D File Offset: 0x00001C0D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override void Focus()
		{
			throw new NotSupportedException();
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.HiddenField.ValueChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data. </param>
		// Token: 0x0600261F RID: 9759 RVA: 0x00061EA0 File Offset: 0x000600A0
		protected virtual void OnValueChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[HiddenField.ValueChangedEvent];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		/// <summary>Processes postback data for a <see cref="T:System.Web.UI.WebControls.HiddenField" /> control.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.HiddenField" /> control's state changes as a result of the postback; otherwise, false.</returns>
		/// <param name="postDataKey">The key identifier for the control.</param>
		/// <param name="postCollection">The collection of all incoming name values.</param>
		// Token: 0x06002620 RID: 9760 RVA: 0x00061ECE File Offset: 0x000600CE
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			if (this.Value != postCollection[postDataKey])
			{
				this.Value = postCollection[postDataKey];
				return true;
			}
			return false;
		}

		/// <summary>Notifies the ASP.NET application that the state of the <see cref="T:System.Web.UI.WebControls.HiddenField" /> control has changed.</summary>
		// Token: 0x06002621 RID: 9761 RVA: 0x00061EF4 File Offset: 0x000600F4
		protected virtual void RaisePostDataChangedEvent()
		{
			base.ValidateEvent(this.UniqueID, string.Empty);
			this.OnValueChanged(EventArgs.Empty);
		}

		/// <summary>Creates an <see cref="T:System.Web.UI.EmptyControlCollection" /> object used to indicate that child controls are not allowed.</summary>
		/// <returns>Always returns an <see cref="T:System.Web.UI.EmptyControlCollection" /> object.</returns>
		// Token: 0x06002622 RID: 9762 RVA: 0x0002FDF9 File Offset: 0x0002DFF9
		protected override ControlCollection CreateControlCollection()
		{
			return new EmptyControlCollection(this);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
		// Token: 0x06002623 RID: 9763 RVA: 0x0003EF60 File Offset: 0x0003D160
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
		}

		/// <summary>Renders the Web server control content to the client's browser using the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object used to render the server control content on the client's browser. </param>
		// Token: 0x06002624 RID: 9764 RVA: 0x00061F14 File Offset: 0x00060114
		protected internal override void Render(HtmlTextWriter writer)
		{
			Page page = this.Page;
			string uniqueID = this.UniqueID;
			if (page != null)
			{
				page.ClientScript.RegisterForEventValidation(uniqueID);
			}
			writer.AddAttribute(HtmlTextWriterAttribute.Type, "hidden", false);
			if (!string.IsNullOrEmpty(this.ClientID))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
			}
			if (!string.IsNullOrEmpty(uniqueID))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Name, uniqueID);
			}
			if (!string.IsNullOrEmpty(this.Value))
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Value, this.Value);
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Input);
			writer.RenderEndTag();
		}

		/// <summary>For a description of this member, see the <see cref="M:System.Web.UI.IPostBackDataHandler.LoadPostData(System.String,System.Collections.Specialized.NameValueCollection)" /> method.</summary>
		/// <returns>true if the server control's state changes as a result of the postback; otherwise, false.</returns>
		/// <param name="postDataKey">The key identifier for the control.</param>
		/// <param name="postCollection">The collection of all incoming name values.</param>
		// Token: 0x06002625 RID: 9765 RVA: 0x00061FA1 File Offset: 0x000601A1
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		/// <summary>For a description of this member, see the <see cref="M:System.Web.UI.IPostBackDataHandler.RaisePostDataChangedEvent" /> method.</summary>
		// Token: 0x06002626 RID: 9766 RVA: 0x00061FAB File Offset: 0x000601AB
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		// Token: 0x06002628 RID: 9768 RVA: 0x00061FB3 File Offset: 0x000601B3
		// Note: this type is marked as 'beforefieldinit'.
		static HiddenField()
		{
			HiddenField.ValueChangedEvent = new object();
		}
	}
}

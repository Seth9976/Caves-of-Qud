using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Displays the values of a single record from a data source using user-defined templates. The <see cref="T:System.Web.UI.WebControls.FormView" /> control allows you to edit, delete, and insert records.</summary>
	// Token: 0x02000396 RID: 918
	[SupportsEventValidation]
	[Designer("System.Web.UI.Design.WebControls.FormViewDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ControlValueProperty("SelectedValue")]
	[DefaultEvent("PageIndexChanging")]
	[DataKeyProperty("DataKey")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class FormView : CompositeDataBoundControl, IDataItemContainer, INamingContainer, IPostBackEventHandler, IPostBackContainer, IDataBoundItemControl, IDataBoundControl, IRenderOuterTable
	{
		/// <summary>Occurs when the value of the <see cref="P:System.Web.UI.WebControls.FormView.PageIndex" /> property changes after a paging operation.</summary>
		// Token: 0x14000084 RID: 132
		// (add) Token: 0x0600241A RID: 9242 RVA: 0x0005BC77 File Offset: 0x00059E77
		// (remove) Token: 0x0600241B RID: 9243 RVA: 0x0005BC8A File Offset: 0x00059E8A
		public event EventHandler PageIndexChanged
		{
			add
			{
				base.Events.AddHandler(FormView.PageIndexChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(FormView.PageIndexChangedEvent, value);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:System.Web.UI.WebControls.FormView.PageIndex" /> property changes before a paging operation.</summary>
		// Token: 0x14000085 RID: 133
		// (add) Token: 0x0600241C RID: 9244 RVA: 0x0005BC9D File Offset: 0x00059E9D
		// (remove) Token: 0x0600241D RID: 9245 RVA: 0x0005BCB0 File Offset: 0x00059EB0
		public event FormViewPageEventHandler PageIndexChanging
		{
			add
			{
				base.Events.AddHandler(FormView.PageIndexChangingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(FormView.PageIndexChangingEvent, value);
			}
		}

		/// <summary>Occurs when a button within a <see cref="T:System.Web.UI.WebControls.FormView" /> control is clicked.</summary>
		// Token: 0x14000086 RID: 134
		// (add) Token: 0x0600241E RID: 9246 RVA: 0x0005BCC3 File Offset: 0x00059EC3
		// (remove) Token: 0x0600241F RID: 9247 RVA: 0x0005BCD6 File Offset: 0x00059ED6
		public event FormViewCommandEventHandler ItemCommand
		{
			add
			{
				base.Events.AddHandler(FormView.ItemCommandEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(FormView.ItemCommandEvent, value);
			}
		}

		/// <summary>Occurs after all the rows are created in a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
		// Token: 0x14000087 RID: 135
		// (add) Token: 0x06002420 RID: 9248 RVA: 0x0005BCE9 File Offset: 0x00059EE9
		// (remove) Token: 0x06002421 RID: 9249 RVA: 0x0005BCFC File Offset: 0x00059EFC
		public event EventHandler ItemCreated
		{
			add
			{
				base.Events.AddHandler(FormView.ItemCreatedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(FormView.ItemCreatedEvent, value);
			}
		}

		/// <summary>Occurs when a Delete button within a <see cref="T:System.Web.UI.WebControls.FormView" /> control is clicked, but after the delete operation.</summary>
		// Token: 0x14000088 RID: 136
		// (add) Token: 0x06002422 RID: 9250 RVA: 0x0005BD0F File Offset: 0x00059F0F
		// (remove) Token: 0x06002423 RID: 9251 RVA: 0x0005BD22 File Offset: 0x00059F22
		public event FormViewDeletedEventHandler ItemDeleted
		{
			add
			{
				base.Events.AddHandler(FormView.ItemDeletedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(FormView.ItemDeletedEvent, value);
			}
		}

		/// <summary>Occurs when a Delete button within a <see cref="T:System.Web.UI.WebControls.FormView" /> control is clicked, but before the delete operation.</summary>
		// Token: 0x14000089 RID: 137
		// (add) Token: 0x06002424 RID: 9252 RVA: 0x0005BD35 File Offset: 0x00059F35
		// (remove) Token: 0x06002425 RID: 9253 RVA: 0x0005BD48 File Offset: 0x00059F48
		public event FormViewDeleteEventHandler ItemDeleting
		{
			add
			{
				base.Events.AddHandler(FormView.ItemDeletingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(FormView.ItemDeletingEvent, value);
			}
		}

		/// <summary>Occurs when an Insert button within a <see cref="T:System.Web.UI.WebControls.FormView" /> control is clicked, but after the insert operation.</summary>
		// Token: 0x1400008A RID: 138
		// (add) Token: 0x06002426 RID: 9254 RVA: 0x0005BD5B File Offset: 0x00059F5B
		// (remove) Token: 0x06002427 RID: 9255 RVA: 0x0005BD6E File Offset: 0x00059F6E
		public event FormViewInsertedEventHandler ItemInserted
		{
			add
			{
				base.Events.AddHandler(FormView.ItemInsertedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(FormView.ItemInsertedEvent, value);
			}
		}

		/// <summary>Occurs when an Insert button within a <see cref="T:System.Web.UI.WebControls.FormView" /> control is clicked, but before the insert operation.</summary>
		// Token: 0x1400008B RID: 139
		// (add) Token: 0x06002428 RID: 9256 RVA: 0x0005BD81 File Offset: 0x00059F81
		// (remove) Token: 0x06002429 RID: 9257 RVA: 0x0005BD94 File Offset: 0x00059F94
		public event FormViewInsertEventHandler ItemInserting
		{
			add
			{
				base.Events.AddHandler(FormView.ItemInsertingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(FormView.ItemInsertingEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Web.UI.WebControls.FormView" /> control switches between edit, insert, and read-only mode, but before the mode changes.</summary>
		// Token: 0x1400008C RID: 140
		// (add) Token: 0x0600242A RID: 9258 RVA: 0x0005BDA7 File Offset: 0x00059FA7
		// (remove) Token: 0x0600242B RID: 9259 RVA: 0x0005BDBA File Offset: 0x00059FBA
		public event FormViewModeEventHandler ModeChanging
		{
			add
			{
				base.Events.AddHandler(FormView.ModeChangingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(FormView.ModeChangingEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Web.UI.WebControls.FormView" /> control switches between edit, insert, and read-only mode, but after the mode has changed.</summary>
		// Token: 0x1400008D RID: 141
		// (add) Token: 0x0600242C RID: 9260 RVA: 0x0005BDCD File Offset: 0x00059FCD
		// (remove) Token: 0x0600242D RID: 9261 RVA: 0x0005BDE0 File Offset: 0x00059FE0
		public event EventHandler ModeChanged
		{
			add
			{
				base.Events.AddHandler(FormView.ModeChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(FormView.ModeChangedEvent, value);
			}
		}

		/// <summary>Occurs when an Update button within a <see cref="T:System.Web.UI.WebControls.FormView" /> control is clicked, but after the update operation.</summary>
		// Token: 0x1400008E RID: 142
		// (add) Token: 0x0600242E RID: 9262 RVA: 0x0005BDF3 File Offset: 0x00059FF3
		// (remove) Token: 0x0600242F RID: 9263 RVA: 0x0005BE06 File Offset: 0x0005A006
		public event FormViewUpdatedEventHandler ItemUpdated
		{
			add
			{
				base.Events.AddHandler(FormView.ItemUpdatedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(FormView.ItemUpdatedEvent, value);
			}
		}

		/// <summary>Occurs when an Update button within a <see cref="T:System.Web.UI.WebControls.FormView" /> control is clicked, but before the update operation.</summary>
		// Token: 0x1400008F RID: 143
		// (add) Token: 0x06002430 RID: 9264 RVA: 0x0005BE19 File Offset: 0x0005A019
		// (remove) Token: 0x06002431 RID: 9265 RVA: 0x0005BE2C File Offset: 0x0005A02C
		public event FormViewUpdateEventHandler ItemUpdating
		{
			add
			{
				base.Events.AddHandler(FormView.ItemUpdatingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(FormView.ItemUpdatingEvent, value);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.FormView.PageIndexChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002432 RID: 9266 RVA: 0x0005BE40 File Offset: 0x0005A040
		protected virtual void OnPageIndexChanged(EventArgs e)
		{
			if (base.Events != null)
			{
				EventHandler eventHandler = (EventHandler)base.Events[FormView.PageIndexChangedEvent];
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.FormView.PageIndexChanging" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.FormViewPageEventArgs" /> that contains the event data.</param>
		/// <exception cref="T:System.Web.HttpException">This method is called when the <see cref="T:System.Web.UI.WebControls.FormView" /> control is not bound to a data source control, the paging operation was not canceled, and an event handler is not registered for the event.</exception>
		// Token: 0x06002433 RID: 9267 RVA: 0x0005BE78 File Offset: 0x0005A078
		protected virtual void OnPageIndexChanging(FormViewPageEventArgs e)
		{
			if (base.Events != null)
			{
				FormViewPageEventHandler formViewPageEventHandler = (FormViewPageEventHandler)base.Events[FormView.PageIndexChangingEvent];
				if (formViewPageEventHandler != null)
				{
					formViewPageEventHandler(this, e);
					return;
				}
			}
			if (!base.IsBoundUsingDataSourceID)
			{
				throw new HttpException(string.Format(this.unhandledEventExceptionMessage, this.ID, "PageIndexChanging"));
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.FormView.ItemCommand" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.FormViewCommandEventArgs" /> that contains the event data.</param>
		// Token: 0x06002434 RID: 9268 RVA: 0x0005BED4 File Offset: 0x0005A0D4
		protected virtual void OnItemCommand(FormViewCommandEventArgs e)
		{
			if (base.Events != null)
			{
				FormViewCommandEventHandler formViewCommandEventHandler = (FormViewCommandEventHandler)base.Events[FormView.ItemCommandEvent];
				if (formViewCommandEventHandler != null)
				{
					formViewCommandEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.FormView.ItemCreated" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06002435 RID: 9269 RVA: 0x0005BF0C File Offset: 0x0005A10C
		protected virtual void OnItemCreated(EventArgs e)
		{
			if (base.Events != null)
			{
				EventHandler eventHandler = (EventHandler)base.Events[FormView.ItemCreatedEvent];
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.FormView.ItemDeleted" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.FormViewDeletedEventArgs" /> that contains the event data.</param>
		// Token: 0x06002436 RID: 9270 RVA: 0x0005BF44 File Offset: 0x0005A144
		protected virtual void OnItemDeleted(FormViewDeletedEventArgs e)
		{
			if (base.Events != null)
			{
				FormViewDeletedEventHandler formViewDeletedEventHandler = (FormViewDeletedEventHandler)base.Events[FormView.ItemDeletedEvent];
				if (formViewDeletedEventHandler != null)
				{
					formViewDeletedEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.FormView.ItemInserted" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.FormViewInsertedEventArgs" /> that contains the event data.</param>
		// Token: 0x06002437 RID: 9271 RVA: 0x0005BF7C File Offset: 0x0005A17C
		protected virtual void OnItemInserted(FormViewInsertedEventArgs e)
		{
			if (base.Events != null)
			{
				FormViewInsertedEventHandler formViewInsertedEventHandler = (FormViewInsertedEventHandler)base.Events[FormView.ItemInsertedEvent];
				if (formViewInsertedEventHandler != null)
				{
					formViewInsertedEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.FormView.ItemInserting" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.FormViewInsertEventArgs" /> that contains the event data.</param>
		/// <exception cref="T:System.Web.HttpException">This method is called when the <see cref="T:System.Web.UI.WebControls.FormView" /> control is not bound to a data source control, the user did not cancel the insert operation, and an event handler is not registered for the event.</exception>
		// Token: 0x06002438 RID: 9272 RVA: 0x0005BFB4 File Offset: 0x0005A1B4
		protected virtual void OnItemInserting(FormViewInsertEventArgs e)
		{
			if (base.Events != null)
			{
				FormViewInsertEventHandler formViewInsertEventHandler = (FormViewInsertEventHandler)base.Events[FormView.ItemInsertingEvent];
				if (formViewInsertEventHandler != null)
				{
					formViewInsertEventHandler(this, e);
					return;
				}
			}
			if (!base.IsBoundUsingDataSourceID)
			{
				throw new HttpException(string.Format(this.unhandledEventExceptionMessage, this.ID, "ItemInserting"));
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.FormView.ItemDeleting" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.FormViewDeleteEventArgs" /> that contains the event data.</param>
		/// <exception cref="T:System.Web.HttpException">This method is called when the <see cref="T:System.Web.UI.WebControls.FormView" /> control is not bound to a data source control, the user did not cancel the delete operation, and an event handler is not registered for the event.</exception>
		// Token: 0x06002439 RID: 9273 RVA: 0x0005C010 File Offset: 0x0005A210
		protected virtual void OnItemDeleting(FormViewDeleteEventArgs e)
		{
			if (base.Events != null)
			{
				FormViewDeleteEventHandler formViewDeleteEventHandler = (FormViewDeleteEventHandler)base.Events[FormView.ItemDeletingEvent];
				if (formViewDeleteEventHandler != null)
				{
					formViewDeleteEventHandler(this, e);
					return;
				}
			}
			if (!base.IsBoundUsingDataSourceID)
			{
				throw new HttpException(string.Format(this.unhandledEventExceptionMessage, this.ID, "ItemDeleting"));
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.FormView.ModeChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600243A RID: 9274 RVA: 0x0005C06C File Offset: 0x0005A26C
		protected virtual void OnModeChanged(EventArgs e)
		{
			if (base.Events != null)
			{
				EventHandler eventHandler = (EventHandler)base.Events[FormView.ModeChangedEvent];
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.FormView.ModeChanging" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.FormViewModeEventArgs" /> that contains the event data.</param>
		/// <exception cref="T:System.Web.HttpException">This method is called when the <see cref="T:System.Web.UI.WebControls.FormView" /> control is not bound to a data source control, the mode change was not canceled, and an event handler is not registered for the event.</exception>
		// Token: 0x0600243B RID: 9275 RVA: 0x0005C0A4 File Offset: 0x0005A2A4
		protected virtual void OnModeChanging(FormViewModeEventArgs e)
		{
			if (base.Events != null)
			{
				FormViewModeEventHandler formViewModeEventHandler = (FormViewModeEventHandler)base.Events[FormView.ModeChangingEvent];
				if (formViewModeEventHandler != null)
				{
					formViewModeEventHandler(this, e);
					return;
				}
			}
			if (!base.IsBoundUsingDataSourceID)
			{
				throw new HttpException(string.Format(this.unhandledEventExceptionMessage, this.ID, "ModeChanging"));
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.FormView.ItemUpdated" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.FormViewUpdatedEventArgs" /> that contains the event data.</param>
		// Token: 0x0600243C RID: 9276 RVA: 0x0005C100 File Offset: 0x0005A300
		protected virtual void OnItemUpdated(FormViewUpdatedEventArgs e)
		{
			if (base.Events != null)
			{
				FormViewUpdatedEventHandler formViewUpdatedEventHandler = (FormViewUpdatedEventHandler)base.Events[FormView.ItemUpdatedEvent];
				if (formViewUpdatedEventHandler != null)
				{
					formViewUpdatedEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.FormView.ItemUpdating" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.FormViewUpdateEventArgs" /> that contains the event data.</param>
		/// <exception cref="T:System.Web.HttpException">This method is called when the <see cref="T:System.Web.UI.WebControls.FormView" /> control is not bound to a data source control, the user did not cancel the update operation, and an event handler is not registered for the event.</exception>
		// Token: 0x0600243D RID: 9277 RVA: 0x0005C138 File Offset: 0x0005A338
		protected virtual void OnItemUpdating(FormViewUpdateEventArgs e)
		{
			if (base.Events != null)
			{
				FormViewUpdateEventHandler formViewUpdateEventHandler = (FormViewUpdateEventHandler)base.Events[FormView.ItemUpdatingEvent];
				if (formViewUpdateEventHandler != null)
				{
					formViewUpdateEventHandler(this, e);
					return;
				}
			}
			if (!base.IsBoundUsingDataSourceID)
			{
				throw new HttpException(string.Format(this.unhandledEventExceptionMessage, this.ID, "ItemUpdating"));
			}
		}

		/// <summary>Gets or sets a value indicating whether the paging feature is enabled.</summary>
		/// <returns>true to enable the paging feature; otherwise, false. The default is false.</returns>
		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x0600243E RID: 9278 RVA: 0x0005C194 File Offset: 0x0005A394
		// (set) Token: 0x0600243F RID: 9279 RVA: 0x0005C1BD File Offset: 0x0005A3BD
		[WebCategory("Paging")]
		[DefaultValue(false)]
		public virtual bool AllowPaging
		{
			get
			{
				object obj = this.ViewState["AllowPaging"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["AllowPaging"] = value;
				this.RequireBinding();
			}
		}

		/// <summary>Gets or sets the URL to an image to display in the background of a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
		/// <returns>The URL to an image to display in the background of the <see cref="T:System.Web.UI.WebControls.FormView" /> control. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x06002440 RID: 9280 RVA: 0x00057B0B File Offset: 0x00055D0B
		// (set) Token: 0x06002441 RID: 9281 RVA: 0x00057B2B File Offset: 0x00055D2B
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue("")]
		[UrlProperty]
		[WebCategory("Appearance")]
		public virtual string BackImageUrl
		{
			get
			{
				if (base.ControlStyleCreated)
				{
					return ((TableStyle)base.ControlStyle).BackImageUrl;
				}
				return string.Empty;
			}
			set
			{
				((TableStyle)base.ControlStyle).BackImageUrl = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.FormViewRow" /> object that represents the pager row displayed at the bottom of the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.FormViewRow" /> object that represents the bottom pager row of a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</returns>
		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x06002442 RID: 9282 RVA: 0x0005C1DB File Offset: 0x0005A3DB
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual FormViewRow BottomPagerRow
		{
			get
			{
				this.EnsureChildControls();
				return this.bottomPagerRow;
			}
		}

		/// <summary>Gets or sets the text to render in an HTML caption element in a <see cref="T:System.Web.UI.WebControls.FormView" /> control. This property is provided to make the control more accessible to users of assistive technology devices.</summary>
		/// <returns>A string that represents the text to render in an HTML caption element in a <see cref="T:System.Web.UI.WebControls.FormView" /> control. The default value is an empty string ("").</returns>
		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x06002443 RID: 9283 RVA: 0x0005C1EC File Offset: 0x0005A3EC
		// (set) Token: 0x06002444 RID: 9284 RVA: 0x0005C219 File Offset: 0x0005A419
		[WebCategory("Accessibility")]
		[DefaultValue("")]
		[Localizable(true)]
		public virtual string Caption
		{
			get
			{
				object obj = this.ViewState["Caption"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["Caption"] = value;
				this.RequireBinding();
			}
		}

		/// <summary>Gets or sets the horizontal or vertical position of the HTML caption element in a <see cref="T:System.Web.UI.WebControls.FormView" /> control. This property is provided to make the control more accessible to users of assistive technology devices.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.TableCaptionAlign" /> values. The default is TableCaptionAlign.NotSet.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is not one of the <see cref="T:System.Web.UI.WebControls.TableCaptionAlign" /> enumeration values.</exception>
		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x06002445 RID: 9285 RVA: 0x0005C234 File Offset: 0x0005A434
		// (set) Token: 0x06002446 RID: 9286 RVA: 0x0005C25D File Offset: 0x0005A45D
		[WebCategory("Accessibility")]
		[DefaultValue(TableCaptionAlign.NotSet)]
		public virtual TableCaptionAlign CaptionAlign
		{
			get
			{
				object obj = this.ViewState["CaptionAlign"];
				if (obj != null)
				{
					return (TableCaptionAlign)obj;
				}
				return TableCaptionAlign.NotSet;
			}
			set
			{
				this.ViewState["CaptionAlign"] = value;
				this.RequireBinding();
			}
		}

		/// <summary>Gets or sets the amount of space between the contents of a cell and the cell's border.</summary>
		/// <returns>The amount of space, in pixels, between the contents of a cell and the cell's border. The default value is -1, which indicates that this property is not set.</returns>
		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x06002447 RID: 9287 RVA: 0x00057BDB File Offset: 0x00055DDB
		// (set) Token: 0x06002448 RID: 9288 RVA: 0x00057BF7 File Offset: 0x00055DF7
		[WebCategory("Layout")]
		[DefaultValue(-1)]
		public virtual int CellPadding
		{
			get
			{
				if (base.ControlStyleCreated)
				{
					return ((TableStyle)base.ControlStyle).CellPadding;
				}
				return -1;
			}
			set
			{
				((TableStyle)base.ControlStyle).CellPadding = value;
			}
		}

		/// <summary>Gets or sets the amount of space between cells.</summary>
		/// <returns>The amount of space, in pixels, between cells. The default value is 0.</returns>
		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x06002449 RID: 9289 RVA: 0x00057C0A File Offset: 0x00055E0A
		// (set) Token: 0x0600244A RID: 9290 RVA: 0x00057C26 File Offset: 0x00055E26
		[WebCategory("Layout")]
		[DefaultValue(0)]
		public virtual int CellSpacing
		{
			get
			{
				if (base.ControlStyleCreated)
				{
					return ((TableStyle)base.ControlStyle).CellSpacing;
				}
				return 0;
			}
			set
			{
				((TableStyle)base.ControlStyle).CellSpacing = value;
			}
		}

		/// <summary>Gets the current data-entry mode of the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.FormViewMode" /> values.</returns>
		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x0600244B RID: 9291 RVA: 0x0005C27B File Offset: 0x0005A47B
		// (set) Token: 0x0600244C RID: 9292 RVA: 0x0005C292 File Offset: 0x0005A492
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public FormViewMode CurrentMode
		{
			get
			{
				if (!this.hasCurrentMode)
				{
					return this.DefaultMode;
				}
				return this.currentMode;
			}
			private set
			{
				this.hasCurrentMode = true;
				this.currentMode = value;
			}
		}

		/// <summary>Gets or sets the data-entry mode to which the <see cref="T:System.Web.UI.WebControls.FormView" /> control returns after an update, insert, or cancel operation.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.FormViewMode" /> values. The default is FormViewMode.ReadOnly.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is not one of the <see cref="T:System.Web.UI.WebControls.FormViewMode" /> enumeration values.</exception>
		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x0600244D RID: 9293 RVA: 0x0005C2A2 File Offset: 0x0005A4A2
		// (set) Token: 0x0600244E RID: 9294 RVA: 0x0005C2AA File Offset: 0x0005A4AA
		[DefaultValue(FormViewMode.ReadOnly)]
		[WebCategory("Behavior")]
		public virtual FormViewMode DefaultMode
		{
			get
			{
				return this.defaultMode;
			}
			set
			{
				this.defaultMode = value;
				this.RequireBinding();
			}
		}

		/// <summary>Gets or sets an array that contains the names of the key fields for the data source.</summary>
		/// <returns>An array that contains the names of the key fields for the data source.</returns>
		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x0600244F RID: 9295 RVA: 0x0005C2B9 File Offset: 0x0005A4B9
		// (set) Token: 0x06002450 RID: 9296 RVA: 0x0005C2D0 File Offset: 0x0005A4D0
		[DefaultValue(null)]
		[WebCategory("Data")]
		[TypeConverter(typeof(StringArrayConverter))]
		[Editor("System.Web.UI.Design.WebControls.DataFieldEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public virtual string[] DataKeyNames
		{
			get
			{
				if (this.dataKeyNames == null)
				{
					return this.emptyKeys;
				}
				return this.dataKeyNames;
			}
			set
			{
				this.dataKeyNames = value;
				this.RequireBinding();
			}
		}

		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x06002451 RID: 9297 RVA: 0x0005C2DF File Offset: 0x0005A4DF
		private IOrderedDictionary KeyTable
		{
			get
			{
				if (this._keyTable == null)
				{
					this._keyTable = new OrderedDictionary(this.DataKeyNames.Length);
				}
				return this._keyTable;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.DataKey" /> object that represents the primary key of the displayed record.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.DataKey" /> object that represents the primary key of the displayed record.</returns>
		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x06002452 RID: 9298 RVA: 0x0005C302 File Offset: 0x0005A502
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual DataKey DataKey
		{
			get
			{
				if (this.key == null)
				{
					this.key = new DataKey(this.KeyTable);
				}
				return this.key;
			}
		}

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x06002453 RID: 9299 RVA: 0x0005C323 File Offset: 0x0005A523
		private DataKey OldEditValues
		{
			get
			{
				if (this.oldEditValues == null)
				{
					this.oldEditValues = new DataKey(new OrderedDictionary());
				}
				return this.oldEditValues;
			}
		}

		/// <summary>Gets or sets the custom content for an item in edit mode.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> that contains the custom content for the data row when the <see cref="T:System.Web.UI.WebControls.FormView" /> control is in edit mode. The default value is null, which indicates that this property is not set.</returns>
		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x06002454 RID: 9300 RVA: 0x0005C343 File Offset: 0x0005A543
		// (set) Token: 0x06002455 RID: 9301 RVA: 0x0005C34B File Offset: 0x0005A54B
		[DefaultValue(null)]
		[TemplateContainer(typeof(FormView), BindingDirection.TwoWay)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		public virtual ITemplate EditItemTemplate
		{
			get
			{
				return this.editItemTemplate;
			}
			set
			{
				this.editItemTemplate = value;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that allows you to set the appearance of the data row when a <see cref="T:System.Web.UI.WebControls.FormView" /> control is in edit mode.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that represents the style of the data row when a <see cref="T:System.Web.UI.WebControls.FormView" /> control is in edit mode.</returns>
		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x06002456 RID: 9302 RVA: 0x0005C354 File Offset: 0x0005A554
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Styles")]
		[NotifyParentProperty(true)]
		public TableItemStyle EditRowStyle
		{
			get
			{
				if (this.editRowStyle == null)
				{
					this.editRowStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.editRowStyle.TrackViewState();
					}
				}
				return this.editRowStyle;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that allows you to set the appearance of the empty data row displayed when the data source bound to a <see cref="T:System.Web.UI.WebControls.FormView" /> control does not contain any records.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that allows you to set the appearance of the empty data row.</returns>
		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x06002457 RID: 9303 RVA: 0x0005C382 File Offset: 0x0005A582
		[WebCategory("Styles")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		public TableItemStyle EmptyDataRowStyle
		{
			get
			{
				if (this.emptyDataRowStyle == null)
				{
					this.emptyDataRowStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.emptyDataRowStyle.TrackViewState();
					}
				}
				return this.emptyDataRowStyle;
			}
		}

		/// <summary>Gets or sets the user-defined content for the empty data row rendered when a <see cref="T:System.Web.UI.WebControls.FormView" /> control is bound to a data source that does not contain any records.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> that contains the custom content for the empty data row. The default value is null, which indicates that this property is not set.</returns>
		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x06002458 RID: 9304 RVA: 0x0005C3B0 File Offset: 0x0005A5B0
		// (set) Token: 0x06002459 RID: 9305 RVA: 0x0005C3B8 File Offset: 0x0005A5B8
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[TemplateContainer(typeof(FormView), BindingDirection.OneWay)]
		public virtual ITemplate EmptyDataTemplate
		{
			get
			{
				return this.emptyDataTemplate;
			}
			set
			{
				this.emptyDataTemplate = value;
			}
		}

		/// <summary>Gets or sets the text to display in the empty data row rendered when a <see cref="T:System.Web.UI.WebControls.FormView" /> control is bound to a data source that does not contain any records.</summary>
		/// <returns>The text to display in the empty data row. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x0600245A RID: 9306 RVA: 0x0005C3C4 File Offset: 0x0005A5C4
		// (set) Token: 0x0600245B RID: 9307 RVA: 0x0005C3F1 File Offset: 0x0005A5F1
		[Localizable(true)]
		[WebCategory("Appearance")]
		[DefaultValue("")]
		public virtual string EmptyDataText
		{
			get
			{
				object obj = this.ViewState["EmptyDataText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["EmptyDataText"] = value;
				this.RequireBinding();
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.FormViewRow" /> object that represents the footer row in a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.FormViewRow" /> that represents the footer row in a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</returns>
		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x0600245C RID: 9308 RVA: 0x0005C40A File Offset: 0x0005A60A
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual FormViewRow FooterRow
		{
			get
			{
				this.EnsureChildControls();
				return this.footerRow;
			}
		}

		/// <summary>Gets or sets the user-defined content for the footer row in a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> that contains the custom content for the footer row. The default value is null, which indicates that this property is not set.</returns>
		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x0600245D RID: 9309 RVA: 0x0005C418 File Offset: 0x0005A618
		// (set) Token: 0x0600245E RID: 9310 RVA: 0x0005C420 File Offset: 0x0005A620
		[DefaultValue(null)]
		[TemplateContainer(typeof(FormView), BindingDirection.OneWay)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		public virtual ITemplate FooterTemplate
		{
			get
			{
				return this.footerTemplate;
			}
			set
			{
				this.footerTemplate = value;
			}
		}

		/// <summary>Gets or sets the text to display in the footer row of a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
		/// <returns>The text to display in the footer row. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x0600245F RID: 9311 RVA: 0x0005C42C File Offset: 0x0005A62C
		// (set) Token: 0x06002460 RID: 9312 RVA: 0x0005C459 File Offset: 0x0005A659
		[DefaultValue("")]
		[WebCategory("Appearance")]
		[Localizable(true)]
		public virtual string FooterText
		{
			get
			{
				object obj = this.ViewState["FooterText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["FooterText"] = value;
				this.RequireBinding();
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that allows you to set the appearance of the footer row in a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that represents the style of the footer row in a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</returns>
		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x06002461 RID: 9313 RVA: 0x0005C472 File Offset: 0x0005A672
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		[WebCategory("Styles")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		public TableItemStyle FooterStyle
		{
			get
			{
				if (this.footerStyle == null)
				{
					this.footerStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.footerStyle.TrackViewState();
					}
				}
				return this.footerStyle;
			}
		}

		/// <summary>Gets or sets the gridline style for a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.GridLines" /> values. The default is GridLines.None.</returns>
		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x06002462 RID: 9314 RVA: 0x0005C4A0 File Offset: 0x0005A6A0
		// (set) Token: 0x06002463 RID: 9315 RVA: 0x00057F5C File Offset: 0x0005615C
		[WebCategory("Appearance")]
		[DefaultValue(GridLines.None)]
		public virtual GridLines GridLines
		{
			get
			{
				if (base.ControlStyleCreated)
				{
					return ((TableStyle)base.ControlStyle).GridLines;
				}
				return GridLines.None;
			}
			set
			{
				((TableStyle)base.ControlStyle).GridLines = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.FormViewRow" /> object that represents the header row in a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.FormViewRow" /> that represents the header row in a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</returns>
		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x06002464 RID: 9316 RVA: 0x0005C4BC File Offset: 0x0005A6BC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual FormViewRow HeaderRow
		{
			get
			{
				this.EnsureChildControls();
				return this.headerRow;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that allows you to set the appearance of the header row in a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that represents the style of the header row in a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</returns>
		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x06002465 RID: 9317 RVA: 0x0005C4CA File Offset: 0x0005A6CA
		[WebCategory("Styles")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TableItemStyle HeaderStyle
		{
			get
			{
				if (this.headerStyle == null)
				{
					this.headerStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.headerStyle.TrackViewState();
					}
				}
				return this.headerStyle;
			}
		}

		/// <summary>Gets or sets the user-defined content for the header row in a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> that contains the custom content for the header row. The default value is null, which indicates that this property is not set.</returns>
		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x06002466 RID: 9318 RVA: 0x0005C4F8 File Offset: 0x0005A6F8
		// (set) Token: 0x06002467 RID: 9319 RVA: 0x0005C500 File Offset: 0x0005A700
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		[DefaultValue(null)]
		[TemplateContainer(typeof(FormView), BindingDirection.OneWay)]
		public virtual ITemplate HeaderTemplate
		{
			get
			{
				return this.headerTemplate;
			}
			set
			{
				this.headerTemplate = value;
			}
		}

		/// <summary>Gets or sets the text to display in the header row of a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
		/// <returns>The text to display in the header row. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x06002468 RID: 9320 RVA: 0x0005C50C File Offset: 0x0005A70C
		// (set) Token: 0x06002469 RID: 9321 RVA: 0x0005C539 File Offset: 0x0005A739
		[DefaultValue("")]
		[Localizable(true)]
		[WebCategory("Appearance")]
		public virtual string HeaderText
		{
			get
			{
				object obj = this.ViewState["HeaderText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["HeaderText"] = value;
				this.RequireBinding();
			}
		}

		/// <summary>Gets or sets the horizontal alignment of a <see cref="T:System.Web.UI.WebControls.FormView" /> control on the page.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.HorizontalAlign" /> values. The default is HorizontalAlign.NotSet.</returns>
		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x0600246A RID: 9322 RVA: 0x00058002 File Offset: 0x00056202
		// (set) Token: 0x0600246B RID: 9323 RVA: 0x0005801E File Offset: 0x0005621E
		[Category("Layout")]
		[DefaultValue(HorizontalAlign.NotSet)]
		public virtual HorizontalAlign HorizontalAlign
		{
			get
			{
				if (base.ControlStyleCreated)
				{
					return ((TableStyle)base.ControlStyle).HorizontalAlign;
				}
				return HorizontalAlign.NotSet;
			}
			set
			{
				((TableStyle)base.ControlStyle).HorizontalAlign = value;
			}
		}

		/// <summary>Gets or sets the custom content for an item in insert mode.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> that contains the custom content for the data row when the <see cref="T:System.Web.UI.WebControls.FormView" /> control is in insert mode. The default value is null, which indicates that this property is not set.</returns>
		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x0600246C RID: 9324 RVA: 0x0005C552 File Offset: 0x0005A752
		// (set) Token: 0x0600246D RID: 9325 RVA: 0x0005C55A File Offset: 0x0005A75A
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		[DefaultValue(null)]
		[TemplateContainer(typeof(FormView), BindingDirection.TwoWay)]
		public virtual ITemplate InsertItemTemplate
		{
			get
			{
				return this.insertItemTemplate;
			}
			set
			{
				this.insertItemTemplate = value;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that allows you to set the appearance of the data row in a <see cref="T:System.Web.UI.WebControls.FormView" /> control when the control is in insert mode.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that represents the style of the data row in a <see cref="T:System.Web.UI.WebControls.FormView" /> control when the control is in insert mode.</returns>
		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x0600246E RID: 9326 RVA: 0x0005C563 File Offset: 0x0005A763
		[WebCategory("Styles")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		public TableItemStyle InsertRowStyle
		{
			get
			{
				if (this.insertRowStyle == null)
				{
					this.insertRowStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.insertRowStyle.TrackViewState();
					}
				}
				return this.insertRowStyle;
			}
		}

		/// <summary>Gets or sets the custom content for the data row in a <see cref="T:System.Web.UI.WebControls.FormView" /> control when the control is in read-only mode.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> that contains the custom content for the data row when the <see cref="T:System.Web.UI.WebControls.FormView" /> control is in read-only mode. The default value is null, which indicates that this property is not set.</returns>
		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x0600246F RID: 9327 RVA: 0x0005C591 File Offset: 0x0005A791
		// (set) Token: 0x06002470 RID: 9328 RVA: 0x0005C599 File Offset: 0x0005A799
		[DefaultValue(null)]
		[TemplateContainer(typeof(FormView), BindingDirection.TwoWay)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		public virtual ITemplate ItemTemplate
		{
			get
			{
				return this.itemTemplate;
			}
			set
			{
				this.itemTemplate = value;
			}
		}

		/// <summary>Gets the total number of pages required to display every record in the data source.</summary>
		/// <returns>The number of items in the underlying data source.</returns>
		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x06002471 RID: 9329 RVA: 0x0005C5A2 File Offset: 0x0005A7A2
		// (set) Token: 0x06002472 RID: 9330 RVA: 0x0005C5AA File Offset: 0x0005A7AA
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual int PageCount
		{
			get
			{
				return this.pageCount;
			}
			private set
			{
				this.pageCount = value;
			}
		}

		/// <summary>Gets or sets the index of the displayed page.</summary>
		/// <returns>The zero-based index of the data item being displayed in a <see cref="T:System.Web.UI.WebControls.FormView" /> control from the underlying data source.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is less than -1.</exception>
		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x06002473 RID: 9331 RVA: 0x0005C5B3 File Offset: 0x0005A7B3
		// (set) Token: 0x06002474 RID: 9332 RVA: 0x0005C5BB File Offset: 0x0005A7BB
		[WebCategory("Paging")]
		[Bindable(true, BindingDirection.OneWay)]
		[DefaultValue(0)]
		public virtual int PageIndex
		{
			get
			{
				return this.pageIndex;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("PageIndex must be non-negative");
				}
				if (this.pageIndex == value || value == -1)
				{
					return;
				}
				this.pageIndex = value;
				this.RequireBinding();
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.PagerSettings" /> object that allows you to set the properties of the pager buttons in a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.PagerSettings" /> that allows you to set the properties of the pager buttons in a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</returns>
		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x06002475 RID: 9333 RVA: 0x0005C5E7 File Offset: 0x0005A7E7
		[WebCategory("Paging")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual PagerSettings PagerSettings
		{
			get
			{
				if (this.pagerSettings == null)
				{
					this.pagerSettings = new PagerSettings(this);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.pagerSettings).TrackViewState();
					}
				}
				return this.pagerSettings;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that allows you to set the appearance of the pager row in a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that represents the style of the pager row in a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</returns>
		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x06002476 RID: 9334 RVA: 0x0005C616 File Offset: 0x0005A816
		[WebCategory("Styles")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TableItemStyle PagerStyle
		{
			get
			{
				if (this.pagerStyle == null)
				{
					this.pagerStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.pagerStyle.TrackViewState();
					}
				}
				return this.pagerStyle;
			}
		}

		/// <summary>Gets or sets the custom content for the pager row in a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> that contains the custom content for the pager row. The default value is null, which indicates that this property is not set.</returns>
		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x06002477 RID: 9335 RVA: 0x0005C644 File Offset: 0x0005A844
		// (set) Token: 0x06002478 RID: 9336 RVA: 0x0005C64C File Offset: 0x0005A84C
		[Browsable(false)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(FormView))]
		[DefaultValue(null)]
		public virtual ITemplate PagerTemplate
		{
			get
			{
				return this.pagerTemplate;
			}
			set
			{
				this.pagerTemplate = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.FormViewRow" /> object that represents the data row in a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.FormViewRow" /> that represents the data row in a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</returns>
		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x06002479 RID: 9337 RVA: 0x0005C655 File Offset: 0x0005A855
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual FormViewRow Row
		{
			get
			{
				this.EnsureChildControls();
				return this.itemRow;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that allows you to set the appearance of the data row in a <see cref="T:System.Web.UI.WebControls.FormView" /> control when the control is in read-only mode.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that represents the style of the data row in a <see cref="T:System.Web.UI.WebControls.FormView" /> control when the control is in read-only mode.</returns>
		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x0600247A RID: 9338 RVA: 0x0005C663 File Offset: 0x0005A863
		[NotifyParentProperty(true)]
		[WebCategory("Styles")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public TableItemStyle RowStyle
		{
			get
			{
				if (this.rowStyle == null)
				{
					this.rowStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.rowStyle.TrackViewState();
					}
				}
				return this.rowStyle;
			}
		}

		/// <summary>Gets the data key value of the current record in a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
		/// <returns>The data key value of the current record in a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</returns>
		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x0600247B RID: 9339 RVA: 0x0005C691 File Offset: 0x0005A891
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object SelectedValue
		{
			get
			{
				return this.DataKey.Value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.FormViewRow" /> object that represents the pager row displayed at the top of a <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.FormViewRow" /> that represents the top pager row in the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</returns>
		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x0600247C RID: 9340 RVA: 0x0005C69E File Offset: 0x0005A89E
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual FormViewRow TopPagerRow
		{
			get
			{
				this.EnsureChildControls();
				return this.topPagerRow;
			}
		}

		/// <summary>Gets the data item bound to the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the data item bound to the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</returns>
		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x0600247D RID: 9341 RVA: 0x0005C6AC File Offset: 0x0005A8AC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual object DataItem
		{
			get
			{
				return this.dataItem;
			}
		}

		/// <summary>Gets the number of data items in the data source.</summary>
		/// <returns>The number of data items in the data source.</returns>
		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x0600247E RID: 9342 RVA: 0x0005C6B4 File Offset: 0x0005A8B4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public int DataItemCount
		{
			get
			{
				return this.PageCount;
			}
		}

		/// <summary>Gets the index of the data item bound to the <see cref="T:System.Web.UI.WebControls.FormView" /> control from the data source.</summary>
		/// <returns>The index of the data item bound to the <see cref="T:System.Web.UI.WebControls.FormView" /> control from the data source.</returns>
		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x0600247F RID: 9343 RVA: 0x0005C6BC File Offset: 0x0005A8BC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual int DataItemIndex
		{
			get
			{
				return this.PageIndex;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IDataItemContainer.DataItemIndex" />.</summary>
		/// <returns>An object that represents the display index.</returns>
		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x06002480 RID: 9344 RVA: 0x0005C6C4 File Offset: 0x0005A8C4
		int IDataItemContainer.DataItemIndex
		{
			get
			{
				return this.DataItemIndex;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IDataItemContainer.DisplayIndex" />.</summary>
		/// <returns>Always returns 0.</returns>
		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x06002481 RID: 9345 RVA: 0x0005C6BC File Offset: 0x0005A8BC
		int IDataItemContainer.DisplayIndex
		{
			get
			{
				return this.PageIndex;
			}
		}

		/// <summary>Gets or sets a value that indicates whether a validator control will handle exceptions that occur during insert or update operations.</summary>
		/// <returns>true if a validator control will handle exceptions that occur during insert or update operations; otherwise, false. The default is false.</returns>
		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x06002482 RID: 9346 RVA: 0x0005C6CC File Offset: 0x0005A8CC
		// (set) Token: 0x06002483 RID: 9347 RVA: 0x0005C6D4 File Offset: 0x0005A8D4
		[DefaultValue(true)]
		[global::System.MonoTODO("Make use of it in the code")]
		public virtual bool EnableModelValidation { get; set; }

		/// <summary>Gets or sets a value that indicates whether the control encloses rendered HTML in a table element in order to apply inline styles.</summary>
		/// <returns>true if the control encloses rendered HTML in a table element; otherwise, false. The default is true.</returns>
		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x06002484 RID: 9348 RVA: 0x0005C6DD File Offset: 0x0005A8DD
		// (set) Token: 0x06002485 RID: 9349 RVA: 0x0005C6E5 File Offset: 0x0005A8E5
		[DefaultValue(true)]
		public virtual bool RenderOuterTable
		{
			get
			{
				return this.renderOuterTable;
			}
			set
			{
				this.renderOuterTable = value;
			}
		}

		/// <summary>Gets the current mode of the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
		/// <returns>The current mode of the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</returns>
		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x06002486 RID: 9350 RVA: 0x0005C6F0 File Offset: 0x0005A8F0
		DataBoundControlMode IDataBoundItemControl.Mode
		{
			get
			{
				switch (this.CurrentMode)
				{
				case FormViewMode.ReadOnly:
					return DataBoundControlMode.ReadOnly;
				case FormViewMode.Edit:
					return DataBoundControlMode.Edit;
				case FormViewMode.Insert:
					return DataBoundControlMode.Insert;
				default:
					throw new InvalidOperationException("Unsupported mode value.");
				}
			}
		}

		/// <summary>Determines whether the table-specific CSS style rules that are associated with the <see cref="T:System.Web.UI.WebControls.FormView" /> control are set to their default values.</summary>
		/// <returns>The default CSS style rules that are associated with the <see cref="T:System.Web.UI.WebControls.FormView" /> control. </returns>
		// Token: 0x06002487 RID: 9351 RVA: 0x0005C728 File Offset: 0x0005A928
		protected internal virtual string ModifiedOuterTableStylePropertyName()
		{
			if (this.BackImageUrl != string.Empty)
			{
				return "BackImageUrl";
			}
			if (this.CellPadding != -1)
			{
				return "CellPadding";
			}
			if (this.CellSpacing != 0)
			{
				return "CellSpacing";
			}
			if (this.GridLines != GridLines.None)
			{
				return "GridLines";
			}
			if (this.HorizontalAlign != HorizontalAlign.NotSet)
			{
				return "HorizontalAlign";
			}
			if (base.ControlStyle.CheckBit(65024))
			{
				return "Font";
			}
			return string.Empty;
		}

		// Token: 0x06002488 RID: 9352 RVA: 0x0005C7A4 File Offset: 0x0005A9A4
		internal override string InlinePropertiesSet()
		{
			string text = base.InlinePropertiesSet();
			string text2 = this.ModifiedOuterTableStylePropertyName();
			if (string.IsNullOrEmpty(text2))
			{
				return text;
			}
			if (string.IsNullOrEmpty(text))
			{
				return text2;
			}
			return text + ", " + text2;
		}

		/// <summary>Determines whether the specified data type can be bound to a field in the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
		/// <returns>true if the specified data type can be bound to a field in the <see cref="T:System.Web.UI.WebControls.FormView" /> control; otherwise, false.</returns>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the data type to check.</param>
		// Token: 0x06002489 RID: 9353 RVA: 0x0005C7E0 File Offset: 0x0005A9E0
		public virtual bool IsBindableType(Type type)
		{
			return type.IsPrimitive || type == typeof(string) || type == typeof(DateTime) || type == typeof(Guid) || type == typeof(decimal);
		}

		/// <summary>Creates the <see cref="T:System.Web.UI.DataSourceSelectArguments" /> object that contains the arguments that are passed to the data source for processing.</summary>
		/// <returns>A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> that contains the arguments that are passed to the data source.</returns>
		// Token: 0x0600248A RID: 9354 RVA: 0x0005C840 File Offset: 0x0005AA40
		protected override DataSourceSelectArguments CreateDataSourceSelectArguments()
		{
			DataSourceSelectArguments dataSourceSelectArguments = new DataSourceSelectArguments();
			DataSourceView data = this.GetData();
			if (this.AllowPaging && data.CanPage)
			{
				dataSourceSelectArguments.StartRowIndex = this.PageIndex;
				if (data.CanRetrieveTotalRowCount)
				{
					dataSourceSelectArguments.RetrieveTotalRowCount = true;
					dataSourceSelectArguments.MaximumRows = 1;
				}
				else
				{
					dataSourceSelectArguments.MaximumRows = -1;
				}
			}
			return dataSourceSelectArguments;
		}

		/// <summary>Creates a <see cref="T:System.Web.UI.WebControls.FormViewRow" /> object using the specified item index, row type, and row state.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.FormViewRow" /> with the specified item index, row type, and row state.</returns>
		/// <param name="itemIndex">The zero-based index of the data item to display.</param>
		/// <param name="rowType">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowType" /> enumeration values.</param>
		/// <param name="rowState">A bitwise combination of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> enumeration values.</param>
		// Token: 0x0600248B RID: 9355 RVA: 0x0005C896 File Offset: 0x0005AA96
		protected virtual FormViewRow CreateRow(int itemIndex, DataControlRowType rowType, DataControlRowState rowState)
		{
			if (rowType == DataControlRowType.Pager)
			{
				return new FormViewPagerRow(itemIndex, rowType, rowState);
			}
			return new FormViewRow(itemIndex, rowType, rowState);
		}

		// Token: 0x0600248C RID: 9356 RVA: 0x00044E8A File Offset: 0x0004308A
		private void RequireBinding()
		{
			if (base.Initialized)
			{
				base.RequiresDataBinding = true;
			}
		}

		/// <summary>Creates the containing table for the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Table" /> that represents the containing table for the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</returns>
		// Token: 0x0600248D RID: 9357 RVA: 0x00058542 File Offset: 0x00056742
		protected virtual Table CreateTable()
		{
			return new ContainedTable(this);
		}

		/// <summary>Makes certain that the <see cref="T:System.Web.UI.WebControls.FormView" /> control is bound to data when appropriate.</summary>
		// Token: 0x0600248E RID: 9358 RVA: 0x0005C8B0 File Offset: 0x0005AAB0
		protected override void EnsureDataBound()
		{
			if (this.CurrentMode == FormViewMode.Insert)
			{
				if (base.RequiresDataBinding)
				{
					this.OnDataBinding(EventArgs.Empty);
					base.RequiresDataBinding = false;
					base.InternalPerformDataBinding(null);
					base.MarkAsDataBound();
					this.OnDataBound(EventArgs.Empty);
					return;
				}
			}
			else
			{
				base.EnsureDataBound();
			}
		}

		/// <summary>Creates a default table style object for the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> that contains the default table style for the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</returns>
		// Token: 0x0600248F RID: 9359 RVA: 0x0005C8FF File Offset: 0x0005AAFF
		protected override Style CreateControlStyle()
		{
			return new TableStyle(this.ViewState)
			{
				CellSpacing = 0
			};
		}

		/// <summary>Creates the control hierarchy used to render the <see cref="T:System.Web.UI.WebControls.FormView" /> control with the specified data source.</summary>
		/// <returns>The number of items created from the data source.</returns>
		/// <param name="dataSource">An <see cref="T:System.Collections.IEnumerable" /> that represents the data source used to create the control hierarchy.</param>
		/// <param name="dataBinding">true to indicate that the control hierarchy is created directly from the data source; false to indicate the control hierarchy is created from the view state.</param>
		/// <exception cref="T:System.Web.HttpException">The <see cref="T:System.Web.UI.DataSourceView" /> of the <see cref="T:System.Web.UI.IDataSource" /> to which the <see cref="T:System.Web.UI.WebControls.FormView" /> control is bound is null.</exception>
		// Token: 0x06002490 RID: 9360 RVA: 0x0005C914 File Offset: 0x0005AB14
		protected override int CreateChildControls(IEnumerable dataSource, bool dataBinding)
		{
			PagedDataSource pagedDataSource = new PagedDataSource();
			pagedDataSource.DataSource = ((this.CurrentMode != FormViewMode.Insert) ? dataSource : null);
			pagedDataSource.AllowPaging = this.AllowPaging;
			pagedDataSource.PageSize = 1;
			pagedDataSource.CurrentPageIndex = this.PageIndex;
			if (dataBinding && this.CurrentMode != FormViewMode.Insert)
			{
				DataSourceView data = this.GetData();
				if (data != null && data.CanPage)
				{
					pagedDataSource.AllowServerPaging = true;
					if (base.SelectArguments.RetrieveTotalRowCount)
					{
						pagedDataSource.VirtualCount = base.SelectArguments.TotalRowCount;
					}
				}
			}
			PagerSettings pagerSettings = this.PagerSettings;
			bool flag = this.AllowPaging && pagerSettings.Visible && pagedDataSource.PageCount > 1;
			this.Controls.Clear();
			this.table = this.CreateTable();
			this.Controls.Add(this.table);
			this.headerRow = null;
			this.footerRow = null;
			this.topPagerRow = null;
			this.bottomPagerRow = null;
			if (this.AllowPaging)
			{
				this.PageCount = pagedDataSource.DataSourceCount;
				if (this.PageIndex >= this.PageCount && this.PageCount > 0)
				{
					this.pageIndex = (pagedDataSource.CurrentPageIndex = this.PageCount - 1);
				}
				if (pagedDataSource.DataSource != null)
				{
					IEnumerator enumerator = pagedDataSource.GetEnumerator();
					if (enumerator.MoveNext())
					{
						this.dataItem = enumerator.Current;
					}
				}
			}
			else
			{
				int num = 0;
				object obj = null;
				if (pagedDataSource.DataSource != null)
				{
					IEnumerator enumerator2 = pagedDataSource.GetEnumerator();
					while (enumerator2.MoveNext())
					{
						obj = enumerator2.Current;
						if (num == this.PageIndex)
						{
							this.dataItem = enumerator2.Current;
						}
						num++;
					}
				}
				this.PageCount = num;
				if (this.PageIndex >= this.PageCount && this.PageCount > 0)
				{
					this.pageIndex = this.PageCount - 1;
					this.dataItem = obj;
				}
			}
			bool flag2 = this.PageCount == 0 && this.CurrentMode != FormViewMode.Insert;
			if (!flag2)
			{
				this.headerRow = this.CreateRow(-1, DataControlRowType.Header, DataControlRowState.Normal);
				this.InitializeRow(this.headerRow);
				this.table.Rows.Add(this.headerRow);
			}
			if ((flag && pagerSettings.Position == PagerPosition.Top) || pagerSettings.Position == PagerPosition.TopAndBottom)
			{
				this.topPagerRow = this.CreateRow(-1, DataControlRowType.Pager, DataControlRowState.Normal);
				this.InitializePager(this.topPagerRow, pagedDataSource);
				this.table.Rows.Add(this.topPagerRow);
			}
			if (this.PageCount > 0)
			{
				DataControlRowState rowState = this.GetRowState();
				this.itemRow = this.CreateRow(0, DataControlRowType.DataRow, rowState);
				this.InitializeRow(this.itemRow);
				this.table.Rows.Add(this.itemRow);
			}
			else
			{
				FormViewMode formViewMode = this.CurrentMode;
				if (formViewMode != FormViewMode.Edit)
				{
					if (formViewMode != FormViewMode.Insert)
					{
						this.itemRow = this.CreateRow(-1, DataControlRowType.EmptyDataRow, DataControlRowState.Normal);
					}
					else
					{
						this.itemRow = this.CreateRow(-1, DataControlRowType.DataRow, DataControlRowState.Insert);
					}
				}
				else
				{
					this.itemRow = this.CreateRow(-1, DataControlRowType.EmptyDataRow, DataControlRowState.Edit);
				}
				this.InitializeRow(this.itemRow);
				this.table.Rows.Add(this.itemRow);
			}
			if (!flag2)
			{
				this.footerRow = this.CreateRow(-1, DataControlRowType.Footer, DataControlRowState.Normal);
				this.InitializeRow(this.footerRow);
				this.table.Rows.Add(this.footerRow);
			}
			if ((flag && pagerSettings.Position == PagerPosition.Bottom) || pagerSettings.Position == PagerPosition.TopAndBottom)
			{
				this.bottomPagerRow = this.CreateRow(0, DataControlRowType.Pager, DataControlRowState.Normal);
				this.InitializePager(this.bottomPagerRow, pagedDataSource);
				this.table.Rows.Add(this.bottomPagerRow);
			}
			this.OnItemCreated(EventArgs.Empty);
			if (dataBinding)
			{
				this.DataBind(false);
			}
			return this.PageCount;
		}

		// Token: 0x06002491 RID: 9361 RVA: 0x0005CCD0 File Offset: 0x0005AED0
		private DataControlRowState GetRowState()
		{
			DataControlRowState dataControlRowState = DataControlRowState.Normal;
			if (this.CurrentMode == FormViewMode.Edit)
			{
				dataControlRowState |= DataControlRowState.Edit;
			}
			else if (this.CurrentMode == FormViewMode.Insert)
			{
				dataControlRowState |= DataControlRowState.Insert;
			}
			return dataControlRowState;
		}

		/// <summary>Creates the pager row for the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
		/// <param name="row">The <see cref="T:System.Web.UI.WebControls.FormViewRow" /> that contains the pager row.</param>
		/// <param name="pagedDataSource">A <see cref="T:System.Web.UI.WebControls.PagedDataSource" /> that contains the data for the current page.</param>
		// Token: 0x06002492 RID: 9362 RVA: 0x0005CCFC File Offset: 0x0005AEFC
		protected virtual void InitializePager(FormViewRow row, PagedDataSource pagedDataSource)
		{
			TableCell tableCell = new TableCell();
			tableCell.ColumnSpan = 2;
			if (this.pagerTemplate != null)
			{
				this.pagerTemplate.InstantiateIn(tableCell);
			}
			else
			{
				tableCell.Controls.Add(this.PagerSettings.CreatePagerControl(pagedDataSource.CurrentPageIndex, pagedDataSource.PageCount));
			}
			row.Cells.Add(tableCell);
		}

		/// <summary>Initializes the specified <see cref="T:System.Web.UI.WebControls.FormViewRow" /> object.</summary>
		/// <param name="row">The <see cref="T:System.Web.UI.WebControls.FormViewRow" /> to initialize.</param>
		// Token: 0x06002493 RID: 9363 RVA: 0x0005CD5C File Offset: 0x0005AF5C
		protected virtual void InitializeRow(FormViewRow row)
		{
			TableCell tableCell = new TableCell();
			if (row.RowType == DataControlRowType.DataRow)
			{
				if ((row.RowState & DataControlRowState.Edit) != DataControlRowState.Normal)
				{
					if (this.editItemTemplate != null)
					{
						this.editItemTemplate.InstantiateIn(tableCell);
					}
					else
					{
						row.Visible = false;
					}
				}
				else if ((row.RowState & DataControlRowState.Insert) != DataControlRowState.Normal)
				{
					if (this.insertItemTemplate != null)
					{
						this.insertItemTemplate.InstantiateIn(tableCell);
					}
					else
					{
						row.Visible = false;
					}
				}
				else if (this.itemTemplate != null)
				{
					this.itemTemplate.InstantiateIn(tableCell);
				}
				else
				{
					row.Visible = false;
				}
			}
			else if (row.RowType == DataControlRowType.EmptyDataRow)
			{
				if (this.emptyDataTemplate != null)
				{
					this.emptyDataTemplate.InstantiateIn(tableCell);
				}
				else if (!string.IsNullOrEmpty(this.EmptyDataText))
				{
					tableCell.Text = this.EmptyDataText;
				}
				else
				{
					row.Visible = false;
				}
			}
			else if (row.RowType == DataControlRowType.Footer)
			{
				if (this.footerTemplate != null)
				{
					this.footerTemplate.InstantiateIn(tableCell);
				}
				else if (!string.IsNullOrEmpty(this.FooterText))
				{
					tableCell.Text = this.FooterText;
				}
				else
				{
					row.Visible = false;
				}
			}
			else if (row.RowType == DataControlRowType.Header)
			{
				if (this.headerTemplate != null)
				{
					this.headerTemplate.InstantiateIn(tableCell);
				}
				else if (!string.IsNullOrEmpty(this.HeaderText))
				{
					tableCell.Text = this.HeaderText;
				}
				else
				{
					row.Visible = false;
				}
			}
			tableCell.ColumnSpan = 2;
			row.Cells.Add(tableCell);
			row.RenderJustCellContents = !this.RenderOuterTable;
		}

		// Token: 0x06002494 RID: 9364 RVA: 0x0005CEF0 File Offset: 0x0005B0F0
		private void FillRowDataKey(object dataItem)
		{
			this.KeyTable.Clear();
			if (this.cachedKeyProperties == null)
			{
				PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(dataItem);
				this.cachedKeyProperties = new PropertyDescriptor[this.DataKeyNames.Length];
				for (int i = 0; i < this.DataKeyNames.Length; i++)
				{
					PropertyDescriptor propertyDescriptor = properties.Find(this.DataKeyNames[i], true);
					if (propertyDescriptor == null)
					{
						string text = "Property '";
						string text2 = this.DataKeyNames[i];
						string text3 = "' not found in object of type ";
						Type type = dataItem.GetType();
						throw new InvalidOperationException(text + text2 + text3 + ((type != null) ? type.ToString() : null));
					}
					this.cachedKeyProperties[i] = propertyDescriptor;
				}
			}
			foreach (PropertyDescriptor propertyDescriptor2 in this.cachedKeyProperties)
			{
				this.KeyTable[propertyDescriptor2.Name] = propertyDescriptor2.GetValue(dataItem);
			}
		}

		// Token: 0x06002495 RID: 9365 RVA: 0x0005CFC4 File Offset: 0x0005B1C4
		private IOrderedDictionary GetRowValues(bool includePrimaryKey)
		{
			OrderedDictionary orderedDictionary = new OrderedDictionary();
			this.ExtractRowValues(orderedDictionary, includePrimaryKey);
			return orderedDictionary;
		}

		/// <summary>Retrieves the values of each field declared within the data row and stores them in the specified <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> object.</summary>
		/// <param name="fieldValues">An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> used to store the field values of the current data item.</param>
		/// <param name="includeKeys">true to include key fields; otherwise, false.</param>
		// Token: 0x06002496 RID: 9366 RVA: 0x0005CFE0 File Offset: 0x0005B1E0
		protected virtual void ExtractRowValues(IOrderedDictionary fieldValues, bool includeKeys)
		{
			FormViewRow row = this.Row;
			if (row == null)
			{
				return;
			}
			DataControlRowState rowState = row.RowState;
			IBindableTemplate bindableTemplate;
			if ((rowState & DataControlRowState.Insert) != DataControlRowState.Normal)
			{
				bindableTemplate = this.insertItemTemplate as IBindableTemplate;
			}
			else
			{
				if ((rowState & DataControlRowState.Edit) == DataControlRowState.Normal)
				{
					return;
				}
				bindableTemplate = this.editItemTemplate as IBindableTemplate;
			}
			if (bindableTemplate != null)
			{
				IOrderedDictionary orderedDictionary = bindableTemplate.ExtractValues(row.Cells[0]);
				if (orderedDictionary != null)
				{
					foreach (object obj in orderedDictionary)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						if (!includeKeys)
						{
							object[] array = this.DataKeyNames;
							if (Array.IndexOf<object>(array, dictionaryEntry.Key) != -1)
							{
								continue;
							}
						}
						fieldValues[dictionaryEntry.Key] = dictionaryEntry.Value;
					}
				}
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value for the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
		/// <returns>Always returns HtmlTextWriterTag.Table.</returns>
		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x06002497 RID: 9367 RVA: 0x0004A624 File Offset: 0x00048824
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Table;
			}
		}

		/// <summary>Binds the data source to the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
		// Token: 0x06002498 RID: 9368 RVA: 0x0005D0B8 File Offset: 0x0005B2B8
		public sealed override void DataBind()
		{
			this.cachedKeyProperties = null;
			base.DataBind();
			if (this.pageCount > 0)
			{
				if (this.CurrentMode == FormViewMode.Edit)
				{
					this.oldEditValues = new DataKey(this.GetRowValues(true));
				}
				this.FillRowDataKey(this.dataItem);
				this.key = new DataKey(this.KeyTable);
			}
		}

		/// <summary>Binds the specified data source to the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
		/// <param name="data">An <see cref="T:System.Collections.IEnumerable" /> that represents the data source.</param>
		// Token: 0x06002499 RID: 9369 RVA: 0x00058E17 File Offset: 0x00057017
		protected internal override void PerformDataBinding(IEnumerable data)
		{
			base.PerformDataBinding(data);
		}

		/// <summary>Sets up the control hierarchy of the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
		// Token: 0x0600249A RID: 9370 RVA: 0x0005D114 File Offset: 0x0005B314
		protected internal virtual void PrepareControlHierarchy()
		{
			if (this.table == null)
			{
				return;
			}
			this.table.Caption = this.Caption;
			this.table.CaptionAlign = this.CaptionAlign;
			foreach (object obj in this.table.Rows)
			{
				FormViewRow formViewRow = (FormViewRow)obj;
				switch (formViewRow.RowType)
				{
				case DataControlRowType.Header:
					if (this.headerStyle != null && !this.headerStyle.IsEmpty)
					{
						formViewRow.ControlStyle.CopyFrom(this.headerStyle);
					}
					break;
				case DataControlRowType.Footer:
					if (this.footerStyle != null && !this.footerStyle.IsEmpty)
					{
						formViewRow.ControlStyle.CopyFrom(this.footerStyle);
					}
					break;
				case DataControlRowType.DataRow:
					if (this.rowStyle != null && !this.rowStyle.IsEmpty)
					{
						formViewRow.ControlStyle.CopyFrom(this.rowStyle);
					}
					if ((formViewRow.RowState & (DataControlRowState.Edit | DataControlRowState.Insert)) != DataControlRowState.Normal && this.editRowStyle != null && !this.editRowStyle.IsEmpty)
					{
						formViewRow.ControlStyle.CopyFrom(this.editRowStyle);
					}
					if ((formViewRow.RowState & DataControlRowState.Insert) != DataControlRowState.Normal && this.insertRowStyle != null && !this.insertRowStyle.IsEmpty)
					{
						formViewRow.ControlStyle.CopyFrom(this.insertRowStyle);
					}
					break;
				case DataControlRowType.Pager:
					if (this.pagerStyle != null && !this.pagerStyle.IsEmpty)
					{
						formViewRow.ControlStyle.CopyFrom(this.pagerStyle);
					}
					break;
				case DataControlRowType.EmptyDataRow:
					if (this.emptyDataRowStyle != null && !this.emptyDataRowStyle.IsEmpty)
					{
						formViewRow.ControlStyle.CopyFrom(this.emptyDataRowStyle);
					}
					break;
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.Init" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600249B RID: 9371 RVA: 0x0005D320 File Offset: 0x0005B520
		protected internal override void OnInit(EventArgs e)
		{
			this.Page.RegisterRequiresControlState(this);
			base.OnInit(e);
		}

		/// <summary>Handles an event passed up through the control hierarchy.</summary>
		/// <returns>true to indicate the event should be passed further up the control hierarchy; otherwise, false.</returns>
		/// <param name="source">The source of the event.</param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x0600249C RID: 9372 RVA: 0x0005D338 File Offset: 0x0005B538
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			FormViewCommandEventArgs e2 = e as FormViewCommandEventArgs;
			if (e2 != null)
			{
				bool flag = false;
				IButtonControl buttonControl = e2.CommandSource as IButtonControl;
				if (buttonControl != null && buttonControl.CausesValidation)
				{
					this.Page.Validate(buttonControl.ValidationGroup);
					flag = true;
				}
				this.ProcessCommand(e2, flag);
				return true;
			}
			return base.OnBubbleEvent(source, e);
		}

		// Token: 0x0600249D RID: 9373 RVA: 0x0005D38D File Offset: 0x0005B58D
		private void ProcessCommand(FormViewCommandEventArgs args, bool causesValidation)
		{
			this.OnItemCommand(args);
			this.ProcessEvent(args.CommandName, args.CommandArgument as string, causesValidation);
		}

		/// <summary>Raises events for the <see cref="T:System.Web.UI.WebControls.FormView" /> control when it posts back to the server.</summary>
		/// <param name="eventArgument">The argument for the event.</param>
		// Token: 0x0600249E RID: 9374 RVA: 0x0005D3AE File Offset: 0x0005B5AE
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		/// <summary>Raises the appropriate events for the <see cref="T:System.Web.UI.WebControls.FormView" /> control when it posts back to the server.</summary>
		/// <param name="eventArgument">The event argument from which to create a <see cref="T:System.Web.UI.WebControls.CommandEventArgs" /> for the event or events that are raised.</param>
		// Token: 0x0600249F RID: 9375 RVA: 0x0005D3B8 File Offset: 0x0005B5B8
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			int num = eventArgument.IndexOf('$');
			CommandEventArgs e;
			if (num != -1)
			{
				e = new CommandEventArgs(eventArgument.Substring(0, num), eventArgument.Substring(num + 1));
			}
			else
			{
				e = new CommandEventArgs(eventArgument, null);
			}
			this.ProcessCommand(new FormViewCommandEventArgs(this, e), false);
		}

		// Token: 0x060024A0 RID: 9376 RVA: 0x0005D404 File Offset: 0x0005B604
		private void ProcessEvent(string eventName, string param, bool causesValidation)
		{
			uint num = global::<PrivateImplementationDetails>.ComputeStringHash(eventName);
			if (num <= 1847791252U)
			{
				if (num <= 900713019U)
				{
					if (num != 254900552U)
					{
						if (num != 900713019U)
						{
							return;
						}
						if (!(eventName == "Cancel"))
						{
							return;
						}
						this.CancelEdit();
						return;
					}
					else
					{
						if (!(eventName == "Insert"))
						{
							return;
						}
						this.InsertItem(causesValidation);
					}
				}
				else if (num != 907026896U)
				{
					if (num != 1469573738U)
					{
						if (num != 1847791252U)
						{
							return;
						}
						if (!(eventName == "Update"))
						{
							return;
						}
						this.UpdateItem(param, causesValidation);
						return;
					}
					else
					{
						if (!(eventName == "Delete"))
						{
							return;
						}
						this.DeleteItem();
						return;
					}
				}
				else
				{
					if (!(eventName == "Prev"))
					{
						return;
					}
					if (this.PageIndex > 0)
					{
						this.SetPageIndex(this.PageIndex - 1);
						return;
					}
				}
			}
			else if (num <= 3705854472U)
			{
				if (num != 2334404017U)
				{
					if (num != 3267849393U)
					{
						if (num != 3705854472U)
						{
							return;
						}
						if (!(eventName == "Next"))
						{
							return;
						}
						if (this.PageIndex < this.PageCount - 1)
						{
							this.SetPageIndex(this.PageIndex + 1);
							return;
						}
					}
					else
					{
						if (!(eventName == "Edit"))
						{
							return;
						}
						this.ProcessChangeMode(FormViewMode.Edit, false);
						return;
					}
				}
				else
				{
					if (!(eventName == "New"))
					{
						return;
					}
					this.ProcessChangeMode(FormViewMode.Insert, false);
					return;
				}
			}
			else if (num != 3826132025U)
			{
				if (num != 3896349078U)
				{
					if (num != 3996994017U)
					{
						return;
					}
					if (!(eventName == "First"))
					{
						return;
					}
					this.SetPageIndex(0);
					return;
				}
				else
				{
					if (!(eventName == "Page"))
					{
						return;
					}
					int num3;
					if (!(param == "First"))
					{
						if (!(param == "Last"))
						{
							if (!(param == "Next"))
							{
								if (!(param == "Prev"))
								{
									int num2 = 0;
									int.TryParse(param, out num2);
									num3 = num2 - 1;
								}
								else
								{
									num3 = this.PageIndex - 1;
								}
							}
							else
							{
								num3 = this.PageIndex + 1;
							}
						}
						else
						{
							num3 = this.PageCount - 1;
						}
					}
					else
					{
						num3 = 0;
					}
					this.SetPageIndex(num3);
					return;
				}
			}
			else
			{
				if (!(eventName == "Last"))
				{
					return;
				}
				this.SetPageIndex(this.PageCount - 1);
				return;
			}
		}

		/// <summary>Sets the index of the currently displayed page in the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
		/// <param name="index">The index to set.</param>
		// Token: 0x060024A1 RID: 9377 RVA: 0x0005D64C File Offset: 0x0005B84C
		public void SetPageIndex(int index)
		{
			FormViewPageEventArgs e = new FormViewPageEventArgs(index);
			this.OnPageIndexChanging(e);
			if (e.Cancel || !base.IsBoundUsingDataSourceID)
			{
				return;
			}
			int newPageIndex = e.NewPageIndex;
			if (newPageIndex < 0 || newPageIndex >= this.PageCount)
			{
				return;
			}
			this.EndRowEdit(false, false);
			this.PageIndex = newPageIndex;
			this.OnPageIndexChanged(EventArgs.Empty);
		}

		/// <summary>Switches the <see cref="T:System.Web.UI.WebControls.FormView" /> control to the specified data-entry mode.</summary>
		/// <param name="newMode">One of the <see cref="T:System.Web.UI.WebControls.FormViewMode" /> enumeration values.</param>
		// Token: 0x060024A2 RID: 9378 RVA: 0x0005D6A7 File Offset: 0x0005B8A7
		public void ChangeMode(FormViewMode newMode)
		{
			if (this.CurrentMode == newMode)
			{
				return;
			}
			this.CurrentMode = newMode;
			this.RequireBinding();
		}

		// Token: 0x060024A3 RID: 9379 RVA: 0x0005D6C0 File Offset: 0x0005B8C0
		private void ProcessChangeMode(FormViewMode newMode, bool cancelingEdit)
		{
			FormViewModeEventArgs e = new FormViewModeEventArgs(newMode, cancelingEdit);
			this.OnModeChanging(e);
			if (e.Cancel || !base.IsBoundUsingDataSourceID)
			{
				return;
			}
			this.ChangeMode(e.NewMode);
			this.OnModeChanged(EventArgs.Empty);
		}

		// Token: 0x060024A4 RID: 9380 RVA: 0x0005D704 File Offset: 0x0005B904
		private void CancelEdit()
		{
			this.EndRowEdit(true, true);
		}

		/// <summary>Updates the current record in the data source.</summary>
		/// <param name="causesValidation">true to perform page validation when the method is called; otherwise false.</param>
		/// <exception cref="T:System.Web.HttpException">This method is called when the <see cref="T:System.Web.UI.WebControls.FormView" /> control is not in edit mode.-or-The <see cref="T:System.Web.UI.DataSourceView" /> object associated with the <see cref="T:System.Web.UI.WebControls.FormView" /> control is null.</exception>
		// Token: 0x060024A5 RID: 9381 RVA: 0x0005D70E File Offset: 0x0005B90E
		public virtual void UpdateItem(bool causesValidation)
		{
			this.UpdateItem(null, causesValidation);
		}

		// Token: 0x060024A6 RID: 9382 RVA: 0x0005D718 File Offset: 0x0005B918
		private void UpdateItem(string param, bool causesValidation)
		{
			if (causesValidation && this.Page != null && !this.Page.IsValid)
			{
				return;
			}
			if (this.currentMode != FormViewMode.Edit)
			{
				throw new HttpException("Must be in Edit mode");
			}
			this.currentEditOldValues = this.OldEditValues.Values;
			this.currentEditRowKeys = this.DataKey.Values;
			this.currentEditNewValues = this.GetRowValues(true);
			FormViewUpdateEventArgs e = new FormViewUpdateEventArgs(param, this.currentEditRowKeys, this.currentEditOldValues, this.currentEditNewValues);
			this.OnItemUpdating(e);
			if (e.Cancel || !base.IsBoundUsingDataSourceID)
			{
				return;
			}
			DataSourceView data = this.GetData();
			if (data == null)
			{
				throw new HttpException("The DataSourceView associated to data bound control was null");
			}
			data.Update(this.currentEditRowKeys, this.currentEditNewValues, this.currentEditOldValues, new DataSourceViewOperationCallback(this.UpdateCallback));
		}

		// Token: 0x060024A7 RID: 9383 RVA: 0x0005D7EC File Offset: 0x0005B9EC
		private bool UpdateCallback(int recordsAffected, Exception exception)
		{
			FormViewUpdatedEventArgs e = new FormViewUpdatedEventArgs(recordsAffected, exception, this.currentEditRowKeys, this.currentEditOldValues, this.currentEditNewValues);
			this.OnItemUpdated(e);
			if (!e.KeepInEditMode)
			{
				this.EndRowEdit(true, false);
			}
			return e.ExceptionHandled;
		}

		/// <summary>Inserts the current record in the data source.</summary>
		/// <param name="causesValidation">true to perform page validation when the method is called; otherwise, false.</param>
		/// <exception cref="T:System.Web.HttpException">This method is called when the <see cref="T:System.Web.UI.WebControls.FormView" /> control is not in insert mode.-or-The <see cref="T:System.Web.UI.DataSourceView" /> object associated with the <see cref="T:System.Web.UI.WebControls.FormView" /> control is null.</exception>
		// Token: 0x060024A8 RID: 9384 RVA: 0x0005D830 File Offset: 0x0005BA30
		public virtual void InsertItem(bool causesValidation)
		{
			this.InsertItem(null, causesValidation);
		}

		// Token: 0x060024A9 RID: 9385 RVA: 0x0005D83C File Offset: 0x0005BA3C
		private void InsertItem(string param, bool causesValidation)
		{
			if (causesValidation && this.Page != null && !this.Page.IsValid)
			{
				return;
			}
			if (this.currentMode != FormViewMode.Insert)
			{
				throw new HttpException("Must be in Insert mode");
			}
			this.currentEditNewValues = this.GetRowValues(true);
			FormViewInsertEventArgs e = new FormViewInsertEventArgs(param, this.currentEditNewValues);
			this.OnItemInserting(e);
			if (e.Cancel || !base.IsBoundUsingDataSourceID)
			{
				return;
			}
			DataSourceView data = this.GetData();
			if (data == null)
			{
				throw new HttpException("The DataSourceView associated to data bound control was null");
			}
			data.Insert(this.currentEditNewValues, new DataSourceViewOperationCallback(this.InsertCallback));
		}

		// Token: 0x060024AA RID: 9386 RVA: 0x0005D8D4 File Offset: 0x0005BAD4
		private bool InsertCallback(int recordsAffected, Exception exception)
		{
			FormViewInsertedEventArgs e = new FormViewInsertedEventArgs(recordsAffected, exception, this.currentEditNewValues);
			this.OnItemInserted(e);
			if (!e.KeepInInsertMode)
			{
				this.EndRowEdit(true, false);
			}
			return e.ExceptionHandled;
		}

		/// <summary>Deletes the current record in the <see cref="T:System.Web.UI.WebControls.FormView" /> control from the data source.</summary>
		// Token: 0x060024AB RID: 9387 RVA: 0x0005D90C File Offset: 0x0005BB0C
		public virtual void DeleteItem()
		{
			this.currentEditRowKeys = this.DataKey.Values;
			this.currentEditNewValues = this.GetRowValues(true);
			FormViewDeleteEventArgs e = new FormViewDeleteEventArgs(this.PageIndex, this.currentEditRowKeys, this.currentEditNewValues);
			this.OnItemDeleting(e);
			if (e.Cancel || !base.IsBoundUsingDataSourceID)
			{
				return;
			}
			if (this.PageIndex > 0 && this.PageIndex == this.PageCount - 1)
			{
				int num = this.PageIndex;
				this.PageIndex = num - 1;
			}
			this.RequireBinding();
			DataSourceView data = this.GetData();
			if (data != null)
			{
				data.Delete(this.currentEditRowKeys, this.currentEditNewValues, new DataSourceViewOperationCallback(this.DeleteCallback));
				return;
			}
			FormViewDeletedEventArgs e2 = new FormViewDeletedEventArgs(0, null, this.currentEditRowKeys, this.currentEditNewValues);
			this.OnItemDeleted(e2);
		}

		// Token: 0x060024AC RID: 9388 RVA: 0x0005D9DC File Offset: 0x0005BBDC
		private bool DeleteCallback(int recordsAffected, Exception exception)
		{
			FormViewDeletedEventArgs e = new FormViewDeletedEventArgs(recordsAffected, exception, this.currentEditRowKeys, this.currentEditNewValues);
			this.OnItemDeleted(e);
			return e.ExceptionHandled;
		}

		// Token: 0x060024AD RID: 9389 RVA: 0x0005DA0A File Offset: 0x0005BC0A
		private void EndRowEdit(bool switchToDefaultMode, bool cancelingEdit)
		{
			if (switchToDefaultMode)
			{
				this.ProcessChangeMode(this.DefaultMode, cancelingEdit);
			}
			this.oldEditValues = new DataKey(new OrderedDictionary());
			this.currentEditRowKeys = null;
			this.currentEditOldValues = null;
			this.currentEditNewValues = null;
			this.RequireBinding();
		}

		/// <summary>Loads the state of the <see cref="T:System.Web.UI.WebControls.FormView" /> control properties that need to be persisted, even when the <see cref="P:System.Web.UI.Control.EnableViewState" /> property is set to false.</summary>
		/// <param name="savedState">An <see cref="T:System.Object" /> that represents the state of the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</param>
		// Token: 0x060024AE RID: 9390 RVA: 0x0005DA48 File Offset: 0x0005BC48
		protected internal override void LoadControlState(object savedState)
		{
			if (savedState == null)
			{
				return;
			}
			object[] array = (object[])savedState;
			base.LoadControlState(array[0]);
			this.pageIndex = (int)array[1];
			this.pageCount = (int)array[2];
			this.CurrentMode = (FormViewMode)array[3];
			this.defaultMode = (FormViewMode)array[4];
			this.dataKeyNames = (string[])array[5];
			if (array[6] != null)
			{
				((IStateManager)this.DataKey).LoadViewState(array[6]);
			}
			if (array[7] != null)
			{
				((IStateManager)this.OldEditValues).LoadViewState(array[7]);
			}
		}

		/// <summary>Saves the state of the <see cref="T:System.Web.UI.WebControls.FormView" /> control properties that need to be persisted, even when the <see cref="P:System.Web.UI.Control.EnableViewState" /> property is set to false.</summary>
		/// <returns>Returns the server control's current view state. If there is no view state associated with the control, this method returns null.</returns>
		// Token: 0x060024AF RID: 9391 RVA: 0x0005DAD8 File Offset: 0x0005BCD8
		protected internal override object SaveControlState()
		{
			object obj = base.SaveControlState();
			return new object[]
			{
				obj,
				this.pageIndex,
				this.pageCount,
				this.CurrentMode,
				this.defaultMode,
				this.dataKeyNames,
				(this.key == null) ? null : ((IStateManager)this.key).SaveViewState(),
				(this.oldEditValues == null) ? null : ((IStateManager)this.oldEditValues).SaveViewState()
			};
		}

		/// <summary>Marks the starting point at which to begin tracking and saving view-state changes to the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
		// Token: 0x060024B0 RID: 9392 RVA: 0x0005DB6C File Offset: 0x0005BD6C
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this.pagerSettings != null)
			{
				((IStateManager)this.pagerSettings).TrackViewState();
			}
			if (this.footerStyle != null)
			{
				((IStateManager)this.footerStyle).TrackViewState();
			}
			if (this.headerStyle != null)
			{
				((IStateManager)this.headerStyle).TrackViewState();
			}
			if (this.pagerStyle != null)
			{
				((IStateManager)this.pagerStyle).TrackViewState();
			}
			if (this.rowStyle != null)
			{
				((IStateManager)this.rowStyle).TrackViewState();
			}
			if (this.editRowStyle != null)
			{
				((IStateManager)this.editRowStyle).TrackViewState();
			}
			if (this.insertRowStyle != null)
			{
				((IStateManager)this.insertRowStyle).TrackViewState();
			}
			if (this.emptyDataRowStyle != null)
			{
				((IStateManager)this.emptyDataRowStyle).TrackViewState();
			}
		}

		/// <summary>Saves the current view state of the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the saved state of the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</returns>
		// Token: 0x060024B1 RID: 9393 RVA: 0x0005DC18 File Offset: 0x0005BE18
		protected override object SaveViewState()
		{
			object[] array = new object[10];
			array[0] = base.SaveViewState();
			array[1] = ((this.pagerSettings == null) ? null : ((IStateManager)this.pagerSettings).SaveViewState());
			array[2] = ((this.footerStyle == null) ? null : ((IStateManager)this.footerStyle).SaveViewState());
			array[3] = ((this.headerStyle == null) ? null : ((IStateManager)this.headerStyle).SaveViewState());
			array[4] = ((this.pagerStyle == null) ? null : ((IStateManager)this.pagerStyle).SaveViewState());
			array[5] = ((this.rowStyle == null) ? null : ((IStateManager)this.rowStyle).SaveViewState());
			array[6] = ((this.insertRowStyle == null) ? null : ((IStateManager)this.insertRowStyle).SaveViewState());
			array[7] = ((this.editRowStyle == null) ? null : ((IStateManager)this.editRowStyle).SaveViewState());
			array[8] = ((this.emptyDataRowStyle == null) ? null : ((IStateManager)this.emptyDataRowStyle).SaveViewState());
			for (int i = array.Length - 1; i >= 0; i--)
			{
				if (array[i] != null)
				{
					return array;
				}
			}
			return null;
		}

		/// <summary>Loads the previously saved view state of the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
		/// <param name="savedState">An <see cref="T:System.Object" /> that represents the state of the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</param>
		// Token: 0x060024B2 RID: 9394 RVA: 0x0005DD18 File Offset: 0x0005BF18
		protected override void LoadViewState(object savedState)
		{
			if (savedState == null)
			{
				base.LoadViewState(null);
				return;
			}
			object[] array = (object[])savedState;
			base.LoadViewState(array[0]);
			if (array[1] != null)
			{
				((IStateManager)this.PagerSettings).LoadViewState(array[1]);
			}
			if (array[2] != null)
			{
				((IStateManager)this.FooterStyle).LoadViewState(array[2]);
			}
			if (array[3] != null)
			{
				((IStateManager)this.HeaderStyle).LoadViewState(array[3]);
			}
			if (array[4] != null)
			{
				((IStateManager)this.PagerStyle).LoadViewState(array[4]);
			}
			if (array[5] != null)
			{
				((IStateManager)this.RowStyle).LoadViewState(array[5]);
			}
			if (array[6] != null)
			{
				((IStateManager)this.InsertRowStyle).LoadViewState(array[6]);
			}
			if (array[7] != null)
			{
				((IStateManager)this.EditRowStyle).LoadViewState(array[7]);
			}
			if (array[8] != null)
			{
				((IStateManager)this.EmptyDataRowStyle).LoadViewState(array[8]);
			}
		}

		/// <summary>Displays the <see cref="T:System.Web.UI.WebControls.FormView" /> control on the client.</summary>
		/// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that contains the output stream to render on the client.</param>
		// Token: 0x060024B3 RID: 9395 RVA: 0x0005DDD8 File Offset: 0x0005BFD8
		protected internal override void Render(HtmlTextWriter writer)
		{
			base.VerifyInlinePropertiesNotSet();
			if (this.RenderOuterTable)
			{
				this.PrepareControlHierarchy();
				if (this.table != null)
				{
					this.table.Render(writer);
					return;
				}
			}
			else if (this.table != null)
			{
				this.table.RenderChildren(writer);
			}
		}

		/// <summary>Determines the postback event options for the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
		/// <returns>The postback event options for the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</returns>
		/// <param name="buttonControl">The button control that posted the page back to the server.</param>
		/// <exception cref="T:System.ArgumentNullException">The object contained in the <paramref name="buttonControl" /> parameter is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.UI.WebControls.IButtonControl.CausesValidation" /> property of <paramref name="buttonControl" /> is true.</exception>
		// Token: 0x060024B4 RID: 9396 RVA: 0x0005DE18 File Offset: 0x0005C018
		PostBackOptions IPostBackContainer.GetPostBackOptions(IButtonControl control)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (control.CausesValidation)
			{
				throw new InvalidOperationException("A button that causes validation in FormView '" + this.ID + "' is attempting to use the container GridView as the post back target.  The button should either turn off validation or use itself as the post back container.");
			}
			return new PostBackOptions(this)
			{
				Argument = control.CommandName + "$" + control.CommandArgument,
				RequiresJavaScriptProtocol = true
			};
		}

		// Token: 0x060024B6 RID: 9398 RVA: 0x0005DEA8 File Offset: 0x0005C0A8
		// Note: this type is marked as 'beforefieldinit'.
		static FormView()
		{
			FormView.PageIndexChangedEvent = new object();
			FormView.PageIndexChangingEvent = new object();
			FormView.ItemCommandEvent = new object();
			FormView.ItemCreatedEvent = new object();
			FormView.ItemDeletedEvent = new object();
			FormView.ItemDeletingEvent = new object();
			FormView.ItemInsertedEvent = new object();
			FormView.ItemInsertingEvent = new object();
			FormView.ModeChangingEvent = new object();
			FormView.ModeChangedEvent = new object();
			FormView.ItemUpdatedEvent = new object();
			FormView.ItemUpdatingEvent = new object();
		}

		/// <summary>Gets or sets the name of the method on the page that is called when the control performs a delete operation.</summary>
		/// <returns>The name of the method on the page that is called when the control performs a delete operation.</returns>
		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x060024B7 RID: 9399 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x060024B8 RID: 9400 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public new virtual string DeleteMethod
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the name of the method on the page that is called when the control performs an insert operation.</summary>
		/// <returns>The name of the method on the page that is called when the control performs an insert operation.</returns>
		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x060024B9 RID: 9401 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x060024BA RID: 9402 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public new virtual string InsertMethod
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x060024BB RID: 9403 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		string IDataBoundControl.get_DataMember()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x060024BC RID: 9404 RVA: 0x0000B3D8 File Offset: 0x000095D8
		void IDataBoundControl.set_DataMember(string value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x060024BD RID: 9405 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		object IDataBoundControl.get_DataSource()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x060024BE RID: 9406 RVA: 0x0000B3D8 File Offset: 0x000095D8
		void IDataBoundControl.set_DataSource(object value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x060024BF RID: 9407 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		string IDataBoundControl.get_DataSourceID()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x060024C0 RID: 9408 RVA: 0x0000B3D8 File Offset: 0x000095D8
		void IDataBoundControl.set_DataSourceID(string value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x060024C1 RID: 9409 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		IDataSource IDataBoundControl.get_DataSourceObject()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets or sets the name of the method on the page that is called when the control performs an update operation.</summary>
		/// <returns>The name of the method on the page that is called when the control performs an update operation.</returns>
		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x060024C2 RID: 9410 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x060024C3 RID: 9411 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public new virtual string UpdateMethod
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x0400199F RID: 6559
		private object dataItem;

		// Token: 0x040019A0 RID: 6560
		private Table table;

		// Token: 0x040019A1 RID: 6561
		private FormViewRow headerRow;

		// Token: 0x040019A2 RID: 6562
		private FormViewRow footerRow;

		// Token: 0x040019A3 RID: 6563
		private FormViewRow bottomPagerRow;

		// Token: 0x040019A4 RID: 6564
		private FormViewRow topPagerRow;

		// Token: 0x040019A5 RID: 6565
		private FormViewRow itemRow;

		// Token: 0x040019A6 RID: 6566
		private IOrderedDictionary currentEditRowKeys;

		// Token: 0x040019A7 RID: 6567
		private IOrderedDictionary currentEditNewValues;

		// Token: 0x040019A8 RID: 6568
		private IOrderedDictionary currentEditOldValues;

		// Token: 0x040019A9 RID: 6569
		private ITemplate pagerTemplate;

		// Token: 0x040019AA RID: 6570
		private ITemplate emptyDataTemplate;

		// Token: 0x040019AB RID: 6571
		private ITemplate headerTemplate;

		// Token: 0x040019AC RID: 6572
		private ITemplate footerTemplate;

		// Token: 0x040019AD RID: 6573
		private ITemplate editItemTemplate;

		// Token: 0x040019AE RID: 6574
		private ITemplate insertItemTemplate;

		// Token: 0x040019AF RID: 6575
		private ITemplate itemTemplate;

		// Token: 0x040019B0 RID: 6576
		private PropertyDescriptor[] cachedKeyProperties;

		// Token: 0x040019B1 RID: 6577
		private readonly string[] emptyKeys = new string[0];

		// Token: 0x040019B2 RID: 6578
		private readonly string unhandledEventExceptionMessage = "The FormView '{0}' fired event {1} which wasn't handled.";

		// Token: 0x040019B3 RID: 6579
		private PagerSettings pagerSettings;

		// Token: 0x040019B4 RID: 6580
		private TableItemStyle editRowStyle;

		// Token: 0x040019B5 RID: 6581
		private TableItemStyle insertRowStyle;

		// Token: 0x040019B6 RID: 6582
		private TableItemStyle emptyDataRowStyle;

		// Token: 0x040019B7 RID: 6583
		private TableItemStyle footerStyle;

		// Token: 0x040019B8 RID: 6584
		private TableItemStyle headerStyle;

		// Token: 0x040019B9 RID: 6585
		private TableItemStyle pagerStyle;

		// Token: 0x040019BA RID: 6586
		private TableItemStyle rowStyle;

		// Token: 0x040019BB RID: 6587
		private IOrderedDictionary _keyTable;

		// Token: 0x040019BC RID: 6588
		private DataKey key;

		// Token: 0x040019BD RID: 6589
		private DataKey oldEditValues;

		// Token: 0x040019BE RID: 6590
		private bool renderOuterTable = true;

		// Token: 0x040019CB RID: 6603
		private int pageIndex;

		// Token: 0x040019CC RID: 6604
		private FormViewMode currentMode;

		// Token: 0x040019CD RID: 6605
		private bool hasCurrentMode;

		// Token: 0x040019CE RID: 6606
		private int pageCount;

		// Token: 0x040019CF RID: 6607
		private FormViewMode defaultMode;

		// Token: 0x040019D0 RID: 6608
		private string[] dataKeyNames;
	}
}

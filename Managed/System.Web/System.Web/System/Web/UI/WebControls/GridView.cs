using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Security.Permissions;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Displays the values of a data source in a table where each column represents a field and each row represents a record. The <see cref="T:System.Web.UI.WebControls.GridView" /> control enables you to select, sort, and edit these items.</summary>
	// Token: 0x020003A0 RID: 928
	[DataKeyProperty("DataKey")]
	[ControlValueProperty("SelectedValue")]
	[Designer("System.Web.UI.Design.WebControls.GridViewDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[SupportsEventValidation]
	[DefaultEvent("SelectedIndexChanged")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class GridView : CompositeDataBoundControl, ICallbackEventHandler, ICallbackContainer, IPostBackEventHandler, IPostBackContainer, IPersistedSelector, IDataKeysControl, IDataBoundListControl, IDataBoundControl, IFieldControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.GridView" /> class.</summary>
		// Token: 0x060024FC RID: 9468 RVA: 0x0005E269 File Offset: 0x0005C469
		public GridView()
		{
			this.EnableModelValidation = true;
		}

		/// <summary>Occurs when one of the pager buttons is clicked, but after the <see cref="T:System.Web.UI.WebControls.GridView" /> control handles the paging operation.</summary>
		// Token: 0x14000090 RID: 144
		// (add) Token: 0x060024FD RID: 9469 RVA: 0x0005E292 File Offset: 0x0005C492
		// (remove) Token: 0x060024FE RID: 9470 RVA: 0x0005E2A5 File Offset: 0x0005C4A5
		public event EventHandler PageIndexChanged
		{
			add
			{
				base.Events.AddHandler(GridView.PageIndexChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(GridView.PageIndexChangedEvent, value);
			}
		}

		/// <summary>Occurs when one of the pager buttons is clicked, but before the <see cref="T:System.Web.UI.WebControls.GridView" /> control handles the paging operation.</summary>
		// Token: 0x14000091 RID: 145
		// (add) Token: 0x060024FF RID: 9471 RVA: 0x0005E2B8 File Offset: 0x0005C4B8
		// (remove) Token: 0x06002500 RID: 9472 RVA: 0x0005E2CB File Offset: 0x0005C4CB
		public event GridViewPageEventHandler PageIndexChanging
		{
			add
			{
				base.Events.AddHandler(GridView.PageIndexChangingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(GridView.PageIndexChangingEvent, value);
			}
		}

		/// <summary>Occurs when the Cancel button of a row in edit mode is clicked, but before the row exits edit mode.</summary>
		// Token: 0x14000092 RID: 146
		// (add) Token: 0x06002501 RID: 9473 RVA: 0x0005E2DE File Offset: 0x0005C4DE
		// (remove) Token: 0x06002502 RID: 9474 RVA: 0x0005E2F1 File Offset: 0x0005C4F1
		public event GridViewCancelEditEventHandler RowCancelingEdit
		{
			add
			{
				base.Events.AddHandler(GridView.RowCancelingEditEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(GridView.RowCancelingEditEvent, value);
			}
		}

		/// <summary>Occurs when a button is clicked in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
		// Token: 0x14000093 RID: 147
		// (add) Token: 0x06002503 RID: 9475 RVA: 0x0005E304 File Offset: 0x0005C504
		// (remove) Token: 0x06002504 RID: 9476 RVA: 0x0005E317 File Offset: 0x0005C517
		public event GridViewCommandEventHandler RowCommand
		{
			add
			{
				base.Events.AddHandler(GridView.RowCommandEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(GridView.RowCommandEvent, value);
			}
		}

		/// <summary>Occurs when a row is created in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
		// Token: 0x14000094 RID: 148
		// (add) Token: 0x06002505 RID: 9477 RVA: 0x0005E32A File Offset: 0x0005C52A
		// (remove) Token: 0x06002506 RID: 9478 RVA: 0x0005E33D File Offset: 0x0005C53D
		public event GridViewRowEventHandler RowCreated
		{
			add
			{
				base.Events.AddHandler(GridView.RowCreatedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(GridView.RowCreatedEvent, value);
			}
		}

		/// <summary>Occurs when a data row is bound to data in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
		// Token: 0x14000095 RID: 149
		// (add) Token: 0x06002507 RID: 9479 RVA: 0x0005E350 File Offset: 0x0005C550
		// (remove) Token: 0x06002508 RID: 9480 RVA: 0x0005E363 File Offset: 0x0005C563
		public event GridViewRowEventHandler RowDataBound
		{
			add
			{
				base.Events.AddHandler(GridView.RowDataBoundEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(GridView.RowDataBoundEvent, value);
			}
		}

		/// <summary>Occurs when a row's Delete button is clicked, but after the <see cref="T:System.Web.UI.WebControls.GridView" /> control deletes the row.</summary>
		// Token: 0x14000096 RID: 150
		// (add) Token: 0x06002509 RID: 9481 RVA: 0x0005E376 File Offset: 0x0005C576
		// (remove) Token: 0x0600250A RID: 9482 RVA: 0x0005E389 File Offset: 0x0005C589
		public event GridViewDeletedEventHandler RowDeleted
		{
			add
			{
				base.Events.AddHandler(GridView.RowDeletedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(GridView.RowDeletedEvent, value);
			}
		}

		/// <summary>Occurs when a row's Delete button is clicked, but before the <see cref="T:System.Web.UI.WebControls.GridView" /> control deletes the row.</summary>
		// Token: 0x14000097 RID: 151
		// (add) Token: 0x0600250B RID: 9483 RVA: 0x0005E39C File Offset: 0x0005C59C
		// (remove) Token: 0x0600250C RID: 9484 RVA: 0x0005E3AF File Offset: 0x0005C5AF
		public event GridViewDeleteEventHandler RowDeleting
		{
			add
			{
				base.Events.AddHandler(GridView.RowDeletingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(GridView.RowDeletingEvent, value);
			}
		}

		/// <summary>Occurs when a row's Edit button is clicked, but before the <see cref="T:System.Web.UI.WebControls.GridView" /> control enters edit mode.</summary>
		// Token: 0x14000098 RID: 152
		// (add) Token: 0x0600250D RID: 9485 RVA: 0x0005E3C2 File Offset: 0x0005C5C2
		// (remove) Token: 0x0600250E RID: 9486 RVA: 0x0005E3D5 File Offset: 0x0005C5D5
		public event GridViewEditEventHandler RowEditing
		{
			add
			{
				base.Events.AddHandler(GridView.RowEditingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(GridView.RowEditingEvent, value);
			}
		}

		/// <summary>Occurs when a row's Update button is clicked, but after the <see cref="T:System.Web.UI.WebControls.GridView" /> control updates the row.</summary>
		// Token: 0x14000099 RID: 153
		// (add) Token: 0x0600250F RID: 9487 RVA: 0x0005E3E8 File Offset: 0x0005C5E8
		// (remove) Token: 0x06002510 RID: 9488 RVA: 0x0005E3FB File Offset: 0x0005C5FB
		public event GridViewUpdatedEventHandler RowUpdated
		{
			add
			{
				base.Events.AddHandler(GridView.RowUpdatedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(GridView.RowUpdatedEvent, value);
			}
		}

		/// <summary>Occurs when a row's Update button is clicked, but before the <see cref="T:System.Web.UI.WebControls.GridView" /> control updates the row.</summary>
		// Token: 0x1400009A RID: 154
		// (add) Token: 0x06002511 RID: 9489 RVA: 0x0005E40E File Offset: 0x0005C60E
		// (remove) Token: 0x06002512 RID: 9490 RVA: 0x0005E421 File Offset: 0x0005C621
		public event GridViewUpdateEventHandler RowUpdating
		{
			add
			{
				base.Events.AddHandler(GridView.RowUpdatingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(GridView.RowUpdatingEvent, value);
			}
		}

		/// <summary>Occurs when a row's Select button is clicked, but after the <see cref="T:System.Web.UI.WebControls.GridView" /> control handles the select operation.</summary>
		// Token: 0x1400009B RID: 155
		// (add) Token: 0x06002513 RID: 9491 RVA: 0x0005E434 File Offset: 0x0005C634
		// (remove) Token: 0x06002514 RID: 9492 RVA: 0x0005E447 File Offset: 0x0005C647
		public event EventHandler SelectedIndexChanged
		{
			add
			{
				base.Events.AddHandler(GridView.SelectedIndexChangedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(GridView.SelectedIndexChangedEvent, value);
			}
		}

		/// <summary>Occurs when a row's Select button is clicked, but before the <see cref="T:System.Web.UI.WebControls.GridView" /> control handles the select operation.</summary>
		// Token: 0x1400009C RID: 156
		// (add) Token: 0x06002515 RID: 9493 RVA: 0x0005E45A File Offset: 0x0005C65A
		// (remove) Token: 0x06002516 RID: 9494 RVA: 0x0005E46D File Offset: 0x0005C66D
		public event GridViewSelectEventHandler SelectedIndexChanging
		{
			add
			{
				base.Events.AddHandler(GridView.SelectedIndexChangingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(GridView.SelectedIndexChangingEvent, value);
			}
		}

		/// <summary>Occurs when the hyperlink to sort a column is clicked, but after the <see cref="T:System.Web.UI.WebControls.GridView" /> control handles the sort operation.</summary>
		// Token: 0x1400009D RID: 157
		// (add) Token: 0x06002517 RID: 9495 RVA: 0x0005E480 File Offset: 0x0005C680
		// (remove) Token: 0x06002518 RID: 9496 RVA: 0x0005E493 File Offset: 0x0005C693
		public event EventHandler Sorted
		{
			add
			{
				base.Events.AddHandler(GridView.SortedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(GridView.SortedEvent, value);
			}
		}

		/// <summary>Occurs when the hyperlink to sort a column is clicked, but before the <see cref="T:System.Web.UI.WebControls.GridView" /> control handles the sort operation.</summary>
		// Token: 0x1400009E RID: 158
		// (add) Token: 0x06002519 RID: 9497 RVA: 0x0005E4A6 File Offset: 0x0005C6A6
		// (remove) Token: 0x0600251A RID: 9498 RVA: 0x0005E4B9 File Offset: 0x0005C6B9
		public event GridViewSortEventHandler Sorting
		{
			add
			{
				base.Events.AddHandler(GridView.SortingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(GridView.SortingEvent, value);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.GridView.PageIndexChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data. </param>
		// Token: 0x0600251B RID: 9499 RVA: 0x0005E4CC File Offset: 0x0005C6CC
		protected virtual void OnPageIndexChanged(EventArgs e)
		{
			if (base.Events != null)
			{
				EventHandler eventHandler = (EventHandler)base.Events[GridView.PageIndexChangedEvent];
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.GridView.PageIndexChanging" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.GridViewPageEventArgs" /> that contains event data. </param>
		/// <exception cref="T:System.Web.HttpException">There is no handler for the <see cref="E:System.Web.UI.WebControls.GridView.PageIndexChanging" /> event.</exception>
		// Token: 0x0600251C RID: 9500 RVA: 0x0005E504 File Offset: 0x0005C704
		protected virtual void OnPageIndexChanging(GridViewPageEventArgs e)
		{
			if (base.Events != null)
			{
				GridViewPageEventHandler gridViewPageEventHandler = (GridViewPageEventHandler)base.Events[GridView.PageIndexChangingEvent];
				if (gridViewPageEventHandler != null)
				{
					gridViewPageEventHandler(this, e);
					return;
				}
			}
			if (!base.IsBoundUsingDataSourceID)
			{
				throw new HttpException(string.Format("The GridView '{0}' fired event PageIndexChanging which wasn't handled.", this.ID));
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.GridView.RowCancelingEdit" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.GridViewCancelEditEventArgs" /> that contains event data. </param>
		/// <exception cref="T:System.Web.HttpException">There is no handler for the <see cref="E:System.Web.UI.WebControls.GridView.RowCancelingEdit" /> event.</exception>
		// Token: 0x0600251D RID: 9501 RVA: 0x0005E55C File Offset: 0x0005C75C
		protected virtual void OnRowCancelingEdit(GridViewCancelEditEventArgs e)
		{
			if (base.Events != null)
			{
				GridViewCancelEditEventHandler gridViewCancelEditEventHandler = (GridViewCancelEditEventHandler)base.Events[GridView.RowCancelingEditEvent];
				if (gridViewCancelEditEventHandler != null)
				{
					gridViewCancelEditEventHandler(this, e);
					return;
				}
			}
			if (!base.IsBoundUsingDataSourceID)
			{
				throw new HttpException(string.Format("The GridView '{0}' fired event RowCancelingEdit which wasn't handled.", this.ID));
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.GridView.RowCommand" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.GridViewCommandEventArgs" /> that contains event data.</param>
		// Token: 0x0600251E RID: 9502 RVA: 0x0005E5B4 File Offset: 0x0005C7B4
		protected virtual void OnRowCommand(GridViewCommandEventArgs e)
		{
			if (base.Events != null)
			{
				GridViewCommandEventHandler gridViewCommandEventHandler = (GridViewCommandEventHandler)base.Events[GridView.RowCommandEvent];
				if (gridViewCommandEventHandler != null)
				{
					gridViewCommandEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.GridView.RowCreated" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.GridViewRowEventArgs" /> that contains event data. </param>
		// Token: 0x0600251F RID: 9503 RVA: 0x0005E5EC File Offset: 0x0005C7EC
		protected virtual void OnRowCreated(GridViewRowEventArgs e)
		{
			if (base.Events != null)
			{
				GridViewRowEventHandler gridViewRowEventHandler = (GridViewRowEventHandler)base.Events[GridView.RowCreatedEvent];
				if (gridViewRowEventHandler != null)
				{
					gridViewRowEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.GridView.RowDataBound" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.GridViewRowEventArgs" /> that contains event data.</param>
		// Token: 0x06002520 RID: 9504 RVA: 0x0005E624 File Offset: 0x0005C824
		protected virtual void OnRowDataBound(GridViewRowEventArgs e)
		{
			if (base.Events != null)
			{
				GridViewRowEventHandler gridViewRowEventHandler = (GridViewRowEventHandler)base.Events[GridView.RowDataBoundEvent];
				if (gridViewRowEventHandler != null)
				{
					gridViewRowEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.GridView.RowDeleted" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.GridViewDeletedEventArgs" /> that contains event data. </param>
		// Token: 0x06002521 RID: 9505 RVA: 0x0005E65C File Offset: 0x0005C85C
		protected virtual void OnRowDeleted(GridViewDeletedEventArgs e)
		{
			if (base.Events != null)
			{
				GridViewDeletedEventHandler gridViewDeletedEventHandler = (GridViewDeletedEventHandler)base.Events[GridView.RowDeletedEvent];
				if (gridViewDeletedEventHandler != null)
				{
					gridViewDeletedEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.GridView.RowDeleting" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.GridViewDeleteEventArgs" /> that contains event data. </param>
		/// <exception cref="T:System.Web.HttpException">There is no handler for the <see cref="E:System.Web.UI.WebControls.GridView.RowDeleting" /> event.</exception>
		// Token: 0x06002522 RID: 9506 RVA: 0x0005E694 File Offset: 0x0005C894
		protected virtual void OnRowDeleting(GridViewDeleteEventArgs e)
		{
			if (base.Events != null)
			{
				GridViewDeleteEventHandler gridViewDeleteEventHandler = (GridViewDeleteEventHandler)base.Events[GridView.RowDeletingEvent];
				if (gridViewDeleteEventHandler != null)
				{
					gridViewDeleteEventHandler(this, e);
					return;
				}
			}
			if (!base.IsBoundUsingDataSourceID)
			{
				throw new HttpException(string.Format("The GridView '{0}' fired event RowDeleting which wasn't handled.", this.ID));
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.GridView.RowEditing" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.GridViewEditEventArgs" /> that contains event data. </param>
		/// <exception cref="T:System.Web.HttpException">There is no handler for the <see cref="E:System.Web.UI.WebControls.GridView.RowEditing" /> event.</exception>
		// Token: 0x06002523 RID: 9507 RVA: 0x0005E6EC File Offset: 0x0005C8EC
		protected virtual void OnRowEditing(GridViewEditEventArgs e)
		{
			if (base.Events != null)
			{
				GridViewEditEventHandler gridViewEditEventHandler = (GridViewEditEventHandler)base.Events[GridView.RowEditingEvent];
				if (gridViewEditEventHandler != null)
				{
					gridViewEditEventHandler(this, e);
					return;
				}
			}
			if (!base.IsBoundUsingDataSourceID)
			{
				throw new HttpException(string.Format("The GridView '{0}' fired event RowEditing which wasn't handled.", this.ID));
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.GridView.RowUpdated" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.GridViewUpdatedEventArgs" /> that contains event data.</param>
		// Token: 0x06002524 RID: 9508 RVA: 0x0005E744 File Offset: 0x0005C944
		protected virtual void OnRowUpdated(GridViewUpdatedEventArgs e)
		{
			if (base.Events != null)
			{
				GridViewUpdatedEventHandler gridViewUpdatedEventHandler = (GridViewUpdatedEventHandler)base.Events[GridView.RowUpdatedEvent];
				if (gridViewUpdatedEventHandler != null)
				{
					gridViewUpdatedEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.GridView.RowUpdating" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.GridViewUpdateEventArgs" /> that contains event data.</param>
		/// <exception cref="T:System.Web.HttpException">There is no handler for the <see cref="E:System.Web.UI.WebControls.GridView.RowUpdating" /> event.</exception>
		// Token: 0x06002525 RID: 9509 RVA: 0x0005E77C File Offset: 0x0005C97C
		protected virtual void OnRowUpdating(GridViewUpdateEventArgs e)
		{
			if (base.Events != null)
			{
				GridViewUpdateEventHandler gridViewUpdateEventHandler = (GridViewUpdateEventHandler)base.Events[GridView.RowUpdatingEvent];
				if (gridViewUpdateEventHandler != null)
				{
					gridViewUpdateEventHandler(this, e);
					return;
				}
			}
			if (!base.IsBoundUsingDataSourceID)
			{
				throw new HttpException(string.Format("The GridView '{0}' fired event RowUpdating which wasn't handled.", this.ID));
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.GridView.SelectedIndexChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data.</param>
		// Token: 0x06002526 RID: 9510 RVA: 0x0005E7D4 File Offset: 0x0005C9D4
		protected virtual void OnSelectedIndexChanged(EventArgs e)
		{
			if (base.Events != null)
			{
				EventHandler eventHandler = (EventHandler)base.Events[GridView.SelectedIndexChangedEvent];
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.GridView.SelectedIndexChanging" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.GridViewSelectEventArgs" /> that contains event data.</param>
		// Token: 0x06002527 RID: 9511 RVA: 0x0005E80C File Offset: 0x0005CA0C
		protected virtual void OnSelectedIndexChanging(GridViewSelectEventArgs e)
		{
			if (base.Events != null)
			{
				GridViewSelectEventHandler gridViewSelectEventHandler = (GridViewSelectEventHandler)base.Events[GridView.SelectedIndexChangingEvent];
				if (gridViewSelectEventHandler != null)
				{
					gridViewSelectEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.GridView.Sorted" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data.</param>
		// Token: 0x06002528 RID: 9512 RVA: 0x0005E844 File Offset: 0x0005CA44
		protected virtual void OnSorted(EventArgs e)
		{
			if (base.Events != null)
			{
				EventHandler eventHandler = (EventHandler)base.Events[GridView.SortedEvent];
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.GridView.Sorting" /> event.</summary>
		/// <param name="e">A <see cref="T:System.Web.UI.WebControls.GridViewSortEventArgs" /> that contains event data.</param>
		/// <exception cref="T:System.Web.HttpException">There is no handler for the <see cref="E:System.Web.UI.WebControls.GridView.Sorting" /> event.</exception>
		// Token: 0x06002529 RID: 9513 RVA: 0x0005E87C File Offset: 0x0005CA7C
		protected virtual void OnSorting(GridViewSortEventArgs e)
		{
			if (base.Events != null)
			{
				GridViewSortEventHandler gridViewSortEventHandler = (GridViewSortEventHandler)base.Events[GridView.SortingEvent];
				if (gridViewSortEventHandler != null)
				{
					gridViewSortEventHandler(this, e);
					return;
				}
			}
			if (!base.IsBoundUsingDataSourceID)
			{
				throw new HttpException(string.Format("The GridView '{0}' fired event Sorting which wasn't handled.", this.ID));
			}
		}

		/// <summary>Gets or sets a value indicating whether the paging feature is enabled.</summary>
		/// <returns>true if the paging feature is enabled; otherwise, false. The default is false.</returns>
		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x0600252A RID: 9514 RVA: 0x0005E8D4 File Offset: 0x0005CAD4
		// (set) Token: 0x0600252B RID: 9515 RVA: 0x0005E8FD File Offset: 0x0005CAFD
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
				if (value == this.AllowPaging)
				{
					return;
				}
				this.ViewState["AllowPaging"] = value;
				this.RequireBinding();
			}
		}

		/// <summary>Gets or sets a value indicating whether the sorting feature is enabled.</summary>
		/// <returns>true if the sorting feature is enabled; otherwise, false. The default is false.</returns>
		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x0600252C RID: 9516 RVA: 0x0005E928 File Offset: 0x0005CB28
		// (set) Token: 0x0600252D RID: 9517 RVA: 0x0005E951 File Offset: 0x0005CB51
		[WebCategory("Behavior")]
		[DefaultValue(false)]
		public virtual bool AllowSorting
		{
			get
			{
				object obj = this.ViewState["AllowSorting"];
				return obj != null && (bool)obj;
			}
			set
			{
				if (value == this.AllowSorting)
				{
					return;
				}
				this.ViewState["AllowSorting"] = value;
				this.RequireBinding();
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that enables you to set the appearance of alternating data rows in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that represents the style of alternating data rows in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</returns>
		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x0600252E RID: 9518 RVA: 0x0005E979 File Offset: 0x0005CB79
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[WebCategory("Styles")]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TableItemStyle AlternatingRowStyle
		{
			get
			{
				if (this.alternatingRowStyle == null)
				{
					this.alternatingRowStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.alternatingRowStyle.TrackViewState();
					}
				}
				return this.alternatingRowStyle;
			}
		}

		/// <summary>Gets or sets a value indicating whether a <see cref="T:System.Web.UI.WebControls.CommandField" /> field column with an Edit button for each data row is automatically added to a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
		/// <returns>true to automatically add a <see cref="T:System.Web.UI.WebControls.CommandField" /> field column with an Edit button for each data row; otherwise, false. The default is false.</returns>
		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x0600252F RID: 9519 RVA: 0x0005E9A8 File Offset: 0x0005CBA8
		// (set) Token: 0x06002530 RID: 9520 RVA: 0x0005E9D1 File Offset: 0x0005CBD1
		[DefaultValue(false)]
		[WebCategory("Behavior")]
		public virtual bool AutoGenerateEditButton
		{
			get
			{
				object obj = this.ViewState["AutoGenerateEditButton"];
				return obj != null && (bool)obj;
			}
			set
			{
				if (value == this.AutoGenerateEditButton)
				{
					return;
				}
				this.ViewState["AutoGenerateEditButton"] = value;
				this.RequireBinding();
			}
		}

		/// <summary>Gets or sets a value indicating whether a <see cref="T:System.Web.UI.WebControls.CommandField" /> field column with a Delete button for each data row is automatically added to a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
		/// <returns>true to automatically add a <see cref="T:System.Web.UI.WebControls.CommandField" /> field column with a Delete button for each data row; otherwise, false. The default is false.</returns>
		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x06002531 RID: 9521 RVA: 0x0005E9FC File Offset: 0x0005CBFC
		// (set) Token: 0x06002532 RID: 9522 RVA: 0x0005EA25 File Offset: 0x0005CC25
		[WebCategory("Behavior")]
		[DefaultValue(false)]
		public virtual bool AutoGenerateDeleteButton
		{
			get
			{
				object obj = this.ViewState["AutoGenerateDeleteButton"];
				return obj != null && (bool)obj;
			}
			set
			{
				if (value == this.AutoGenerateDeleteButton)
				{
					return;
				}
				this.ViewState["AutoGenerateDeleteButton"] = value;
				this.RequireBinding();
			}
		}

		/// <summary>Gets or sets a value indicating whether a <see cref="T:System.Web.UI.WebControls.CommandField" /> field column with a Select button for each data row is automatically added to a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
		/// <returns>true to automatically add a <see cref="T:System.Web.UI.WebControls.CommandField" /> field column with a Select button for each data row; otherwise, false. The default is false.</returns>
		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x06002533 RID: 9523 RVA: 0x0005EA50 File Offset: 0x0005CC50
		// (set) Token: 0x06002534 RID: 9524 RVA: 0x0005EA79 File Offset: 0x0005CC79
		[WebCategory("Behavior")]
		[DefaultValue(false)]
		public virtual bool AutoGenerateSelectButton
		{
			get
			{
				object obj = this.ViewState["AutoGenerateSelectButton"];
				return obj != null && (bool)obj;
			}
			set
			{
				if (value == this.AutoGenerateSelectButton)
				{
					return;
				}
				this.ViewState["AutoGenerateSelectButton"] = value;
				this.RequireBinding();
			}
		}

		/// <summary>Gets or sets a value indicating whether bound fields are automatically created for each field in the data source.</summary>
		/// <returns>true to automatically create bound fields for each field in the data source; otherwise, false. The default is true.</returns>
		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x06002535 RID: 9525 RVA: 0x0005EAA4 File Offset: 0x0005CCA4
		// (set) Token: 0x06002536 RID: 9526 RVA: 0x0005EACD File Offset: 0x0005CCCD
		[DefaultValue(true)]
		[WebCategory("Behavior")]
		public virtual bool AutoGenerateColumns
		{
			get
			{
				object obj = this.ViewState["AutoGenerateColumns"];
				return obj == null || (bool)obj;
			}
			set
			{
				if (value == this.AutoGenerateColumns)
				{
					return;
				}
				this.ViewState["AutoGenerateColumns"] = value;
				this.RequireBinding();
			}
		}

		/// <summary>Gets or sets the URL to an image to display in the background of a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
		/// <returns>The URL of an image to display in the background of the <see cref="T:System.Web.UI.WebControls.GridView" /> control. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x06002537 RID: 9527 RVA: 0x00057B0B File Offset: 0x00055D0B
		// (set) Token: 0x06002538 RID: 9528 RVA: 0x00057B2B File Offset: 0x00055D2B
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
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

		/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.GridViewRow" /> object that represents the bottom pager row in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.GridViewRow" /> that represents the bottom pager row in the control.</returns>
		// Token: 0x17000BE3 RID: 3043
		// (get) Token: 0x06002539 RID: 9529 RVA: 0x0005EAF5 File Offset: 0x0005CCF5
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual GridViewRow BottomPagerRow
		{
			get
			{
				this.EnsureDataBound();
				return this.bottomPagerRow;
			}
		}

		/// <summary>Gets or sets the text to render in an HTML caption element in a <see cref="T:System.Web.UI.WebControls.GridView" /> control. This property is provided to make the control more accessible to users of assistive technology devices.</summary>
		/// <returns>A string that represents the text to render in an HTML caption element in a <see cref="T:System.Web.UI.WebControls.GridView" /> control. The default value is an empty string ("").</returns>
		// Token: 0x17000BE4 RID: 3044
		// (get) Token: 0x0600253A RID: 9530 RVA: 0x0005EB04 File Offset: 0x0005CD04
		// (set) Token: 0x0600253B RID: 9531 RVA: 0x00047FD1 File Offset: 0x000461D1
		[DefaultValue("")]
		[WebCategory("Accessibility")]
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
			}
		}

		/// <summary>Gets or sets the horizontal or vertical position of the HTML caption element in a <see cref="T:System.Web.UI.WebControls.GridView" /> control. This property is provided to make the control more accessible to users of assistive technology devices.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.TableCaptionAlign" /> values. The default is TableCaptionAlign.NotSet, which uses the browser's default setting.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is not one of the <see cref="T:System.Web.UI.WebControls.TableCaptionAlign" /> enumeration values.</exception>
		// Token: 0x17000BE5 RID: 3045
		// (get) Token: 0x0600253C RID: 9532 RVA: 0x0005EB34 File Offset: 0x0005CD34
		// (set) Token: 0x0600253D RID: 9533 RVA: 0x00047FE4 File Offset: 0x000461E4
		[DefaultValue(TableCaptionAlign.NotSet)]
		[WebCategory("Accessibility")]
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
			}
		}

		/// <summary>Gets or sets the amount of space between the contents of a cell and the cell's border.</summary>
		/// <returns>The amount of space, in pixels, between the contents of a cell and the cell's border. The default value is -1, which indicates that this property is not set.</returns>
		// Token: 0x17000BE6 RID: 3046
		// (get) Token: 0x0600253E RID: 9534 RVA: 0x00057BDB File Offset: 0x00055DDB
		// (set) Token: 0x0600253F RID: 9535 RVA: 0x00057BF7 File Offset: 0x00055DF7
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
		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x06002540 RID: 9536 RVA: 0x00057C0A File Offset: 0x00055E0A
		// (set) Token: 0x06002541 RID: 9537 RVA: 0x00057C26 File Offset: 0x00055E26
		[DefaultValue(0)]
		[WebCategory("Layout")]
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

		/// <summary>Gets a collection of <see cref="T:System.Web.UI.WebControls.DataControlField" /> objects that represent the column fields in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.DataControlFieldCollection" /> that contains all the column fields in the <see cref="T:System.Web.UI.WebControls.GridView" /> control.</returns>
		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x06002542 RID: 9538 RVA: 0x0005EB60 File Offset: 0x0005CD60
		[Editor("System.Web.UI.Design.WebControls.DataControlFieldTypeEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[WebCategory("Misc")]
		[MergableProperty(false)]
		public virtual DataControlFieldCollection Columns
		{
			get
			{
				if (this.columns == null)
				{
					this.columns = new DataControlFieldCollection();
					this.columns.FieldsChanged += this.OnFieldsChanged;
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.columns).TrackViewState();
					}
				}
				return this.columns;
			}
		}

		/// <summary>Gets or sets the control that will automatically generate the columns for a <see cref="T:System.Web.UI.WebControls.GridView" /> control that uses ASP.NET Dynamic Data features.</summary>
		/// <returns>The control that will automatically generate the columns for a <see cref="T:System.Web.UI.WebControls.GridView" /> control that uses ASP.NET Dynamic Data features.</returns>
		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x06002543 RID: 9539 RVA: 0x0005EBB0 File Offset: 0x0005CDB0
		// (set) Token: 0x06002544 RID: 9540 RVA: 0x0005EBB8 File Offset: 0x0005CDB8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public IAutoFieldGenerator ColumnsGenerator { get; set; }

		/// <summary>Gets or sets an array that contains the names of the primary key fields for the items displayed in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
		/// <returns>An array that contains the names of the primary key fields for the items displayed in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</returns>
		// Token: 0x17000BEA RID: 3050
		// (get) Token: 0x06002545 RID: 9541 RVA: 0x0005EBC1 File Offset: 0x0005CDC1
		// (set) Token: 0x06002546 RID: 9542 RVA: 0x0005EBD8 File Offset: 0x0005CDD8
		[DefaultValue(null)]
		[WebCategory("Data")]
		[TypeConverter(typeof(StringArrayConverter))]
		[Editor("System.Web.UI.Design.WebControls.DataFieldEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public virtual string[] DataKeyNames
		{
			get
			{
				if (this.dataKeyNames != null)
				{
					return this.dataKeyNames;
				}
				return this.emptyKeys;
			}
			set
			{
				this.dataKeyNames = value;
				this.RequireBinding();
			}
		}

		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x06002547 RID: 9543 RVA: 0x0005EBE7 File Offset: 0x0005CDE7
		private List<DataKey> DataKeyList
		{
			get
			{
				if (this._dataKeyList == null)
				{
					this._dataKeyList = new List<DataKey>();
				}
				return this._dataKeyList;
			}
		}

		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x06002548 RID: 9544 RVA: 0x0005EC02 File Offset: 0x0005CE02
		private List<DataKey> DataKeySuffixList
		{
			get
			{
				if (this._dataKeySuffixList == null)
				{
					this._dataKeySuffixList = new List<DataKey>();
				}
				return this._dataKeySuffixList;
			}
		}

		/// <summary>Gets a collection of <see cref="T:System.Web.UI.WebControls.DataKey" /> objects that represent the data key value of each row in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.DataKeyArray" /> that contains the data key of each row in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</returns>
		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x06002549 RID: 9545 RVA: 0x0005EC1D File Offset: 0x0005CE1D
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual DataKeyArray DataKeys
		{
			get
			{
				if (this.keys == null)
				{
					this.keys = new DataKeyArray(this.DataKeyList);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.keys).TrackViewState();
					}
				}
				return this.keys;
			}
		}

		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x0600254A RID: 9546 RVA: 0x0005EC51 File Offset: 0x0005CE51
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

		/// <summary>Gets or sets the index of the row to edit.</summary>
		/// <returns>The zero-based index of the row to edit. The default is -1, which indicates that no row is being edited.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified index is less than -1.</exception>
		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x0600254B RID: 9547 RVA: 0x0005EC71 File Offset: 0x0005CE71
		// (set) Token: 0x0600254C RID: 9548 RVA: 0x0005EC79 File Offset: 0x0005CE79
		[WebCategory("Misc")]
		[DefaultValue(-1)]
		public virtual int EditIndex
		{
			get
			{
				return this.editIndex;
			}
			set
			{
				if (value == this.editIndex)
				{
					return;
				}
				this.editIndex = value;
				this.RequireBinding();
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that enables you to set the appearance of the row selected for editing in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that represents the style of the row being edited in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</returns>
		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x0600254D RID: 9549 RVA: 0x0005EC92 File Offset: 0x0005CE92
		[WebCategory("Styles")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
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

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that enables you to set the appearance of the empty data row rendered when a <see cref="T:System.Web.UI.WebControls.GridView" /> control is bound to a data source that does not contain any records.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that enables you to set the appearance of the null row.</returns>
		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x0600254E RID: 9550 RVA: 0x0005ECC0 File Offset: 0x0005CEC0
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Styles")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
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

		/// <summary>Gets or sets the user-defined content for the empty data row rendered when a <see cref="T:System.Web.UI.WebControls.GridView" /> control is bound to a data source that does not contain any records.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> that contains the custom content for the empty data row. The default value is null, which indicates that this property is not set.</returns>
		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x0600254F RID: 9551 RVA: 0x0005ECEE File Offset: 0x0005CEEE
		// (set) Token: 0x06002550 RID: 9552 RVA: 0x0005ECF6 File Offset: 0x0005CEF6
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		[TemplateContainer(typeof(GridViewRow), BindingDirection.OneWay)]
		[DefaultValue(null)]
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

		/// <summary>Gets or sets the text to display in the empty data row rendered when a <see cref="T:System.Web.UI.WebControls.GridView" /> control is bound to a data source that does not contain any records.</summary>
		/// <returns>The text to display in the empty data row. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x06002551 RID: 9553 RVA: 0x0005ED00 File Offset: 0x0005CF00
		// (set) Token: 0x06002552 RID: 9554 RVA: 0x0005ED2D File Offset: 0x0005CF2D
		[Localizable(true)]
		[DefaultValue("")]
		[WebCategory("Appearance")]
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
				if (value == this.EmptyDataText)
				{
					return;
				}
				this.ViewState["EmptyDataText"] = value;
				this.RequireBinding();
			}
		}

		/// <summary>Gets or sets a value indicating whether client-side callbacks are used for sorting and paging operations.</summary>
		/// <returns>true to use client-side callbacks for sorting and paging operations; otherwise, false. The default is false.</returns>
		/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.UI.WebControls.GridView.Columns" /> collection contains a column that does not support callbacks, such as <see cref="T:System.Web.UI.WebControls.TemplateField" />.</exception>
		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x06002553 RID: 9555 RVA: 0x0005ED58 File Offset: 0x0005CF58
		// (set) Token: 0x06002554 RID: 9556 RVA: 0x0005ED81 File Offset: 0x0005CF81
		[DefaultValue(false)]
		[WebCategory("Behavior")]
		public virtual bool EnableSortingAndPagingCallbacks
		{
			get
			{
				object obj = this.ViewState["EnableSortingAndPagingCallbacks"];
				return obj != null && (bool)obj;
			}
			set
			{
				if (value == this.EnableSortingAndPagingCallbacks)
				{
					return;
				}
				this.ViewState["EnableSortingAndPagingCallbacks"] = value;
				this.RequireBinding();
			}
		}

		/// <summary>Gets or sets a value that indicates whether a validator control will handle exceptions that occur during insert or update operations.</summary>
		/// <returns>true if a validator control will handle exceptions that occur during insert or update operations; otherwise, false. The default is false.</returns>
		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x06002555 RID: 9557 RVA: 0x0005EDA9 File Offset: 0x0005CFA9
		// (set) Token: 0x06002556 RID: 9558 RVA: 0x0005EDB1 File Offset: 0x0005CFB1
		[DefaultValue(true)]
		[global::System.MonoTODO("Make use of it in the code")]
		public virtual bool EnableModelValidation { get; set; }

		/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.GridViewRow" /> object that represents the footer row in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.GridViewRow" /> that represents the footer row in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</returns>
		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x06002557 RID: 9559 RVA: 0x0005EDBC File Offset: 0x0005CFBC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual GridViewRow FooterRow
		{
			get
			{
				if (this.table != null)
				{
					for (int i = this.table.Rows.Count - 1; i >= 0; i--)
					{
						GridViewRow gridViewRow = (GridViewRow)this.table.Rows[i];
						DataControlRowType rowType = gridViewRow.RowType;
						if (rowType == DataControlRowType.Footer)
						{
							return gridViewRow;
						}
						if (rowType - DataControlRowType.Separator > 1)
						{
						}
					}
				}
				return null;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that enables you to set the appearance of the footer row in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that represents the style of the footer row in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</returns>
		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x06002558 RID: 9560 RVA: 0x0005EE1B File Offset: 0x0005D01B
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[WebCategory("Styles")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
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

		/// <summary>Gets or sets the gridline style for a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.GridLines" /> values. The default is GridLines.Both.</returns>
		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x06002559 RID: 9561 RVA: 0x00057F40 File Offset: 0x00056140
		// (set) Token: 0x0600255A RID: 9562 RVA: 0x00057F5C File Offset: 0x0005615C
		[WebCategory("Appearance")]
		[DefaultValue(GridLines.Both)]
		public virtual GridLines GridLines
		{
			get
			{
				if (base.ControlStyleCreated)
				{
					return ((TableStyle)base.ControlStyle).GridLines;
				}
				return GridLines.Both;
			}
			set
			{
				((TableStyle)base.ControlStyle).GridLines = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.GridViewRow" /> object that represents the header row in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.GridViewRow" /> that represents the header row in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</returns>
		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x0600255B RID: 9563 RVA: 0x0005EE4C File Offset: 0x0005D04C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual GridViewRow HeaderRow
		{
			get
			{
				if (this.table != null)
				{
					int i = 0;
					int count = this.table.Rows.Count;
					while (i < count)
					{
						GridViewRow gridViewRow = (GridViewRow)this.table.Rows[i];
						DataControlRowType rowType = gridViewRow.RowType;
						if (rowType == DataControlRowType.Header)
						{
							return gridViewRow;
						}
						if (rowType - DataControlRowType.Separator > 1)
						{
						}
						i++;
					}
				}
				return null;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that enables you to set the appearance of the header row in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that represents the style of the header row in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</returns>
		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x0600255C RID: 9564 RVA: 0x0005EEAA File Offset: 0x0005D0AA
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

		/// <summary>Gets or sets the horizontal alignment of a <see cref="T:System.Web.UI.WebControls.GridView" /> control on the page.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.HorizontalAlign" /> values. The default is HorizontalAlign.NotSet.</returns>
		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x0600255D RID: 9565 RVA: 0x00058002 File Offset: 0x00056202
		// (set) Token: 0x0600255E RID: 9566 RVA: 0x0005801E File Offset: 0x0005621E
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

		/// <summary>Gets the number of pages required to display the records of the data source in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
		/// <returns>The number of pages in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</returns>
		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x0600255F RID: 9567 RVA: 0x0005EED8 File Offset: 0x0005D0D8
		// (set) Token: 0x06002560 RID: 9568 RVA: 0x0005EEE0 File Offset: 0x0005D0E0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		/// <summary>Gets or sets the index of the currently displayed page.</summary>
		/// <returns>The zero-based index of the currently displayed page.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Web.UI.WebControls.GridView.PageIndex" /> property is set to a value less than 0.</exception>
		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x06002561 RID: 9569 RVA: 0x0005EEE9 File Offset: 0x0005D0E9
		// (set) Token: 0x06002562 RID: 9570 RVA: 0x0005EEF1 File Offset: 0x0005D0F1
		[DefaultValue(0)]
		[Browsable(true)]
		[WebCategory("Paging")]
		public virtual int PageIndex
		{
			get
			{
				return this.pageIndex;
			}
			set
			{
				if (value == this.pageIndex)
				{
					return;
				}
				this.pageIndex = value;
				this.RequireBinding();
			}
		}

		/// <summary>Gets or sets the number of records to display on a page in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
		/// <returns>The number of records to display on a single page. The default is 10.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Web.UI.WebControls.GridView.PageSize" /> property is set to a value less than 1. </exception>
		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x06002563 RID: 9571 RVA: 0x0005EF0C File Offset: 0x0005D10C
		// (set) Token: 0x06002564 RID: 9572 RVA: 0x0005EF36 File Offset: 0x0005D136
		[DefaultValue(10)]
		[WebCategory("Paging")]
		public virtual int PageSize
		{
			get
			{
				object obj = this.ViewState["PageSize"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 10;
			}
			set
			{
				if (value == this.PageSize)
				{
					return;
				}
				this.ViewState["PageSize"] = value;
				this.RequireBinding();
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.PagerSettings" /> object that enables you to set the properties of the pager buttons in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.PagerSettings" /> that enables you to set the properties of the pager buttons in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</returns>
		// Token: 0x17000BFF RID: 3071
		// (get) Token: 0x06002565 RID: 9573 RVA: 0x0005EF5E File Offset: 0x0005D15E
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

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that enables you to set the appearance of the pager row in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that represents the style of the pager row in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</returns>
		// Token: 0x17000C00 RID: 3072
		// (get) Token: 0x06002566 RID: 9574 RVA: 0x0005EF8D File Offset: 0x0005D18D
		[WebCategory("Styles")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
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

		/// <summary>Gets or sets the custom content for the pager row in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.ITemplate" /> that contains the custom content for the pager row. The default value is null, which indicates that this property is not set.</returns>
		// Token: 0x17000C01 RID: 3073
		// (get) Token: 0x06002567 RID: 9575 RVA: 0x0005EFBB File Offset: 0x0005D1BB
		// (set) Token: 0x06002568 RID: 9576 RVA: 0x0005EFC3 File Offset: 0x0005D1C3
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		[TemplateContainer(typeof(GridViewRow))]
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

		/// <summary>Gets or sets the name of the column to use as the column header for the <see cref="T:System.Web.UI.WebControls.GridView" /> control. This property is provided to make the control more accessible to users of assistive technology devices.</summary>
		/// <returns>The name of the column to use as the column header. The default is an empty string (""), which indicates that this property is not set.</returns>
		// Token: 0x17000C02 RID: 3074
		// (get) Token: 0x06002569 RID: 9577 RVA: 0x0005EFCC File Offset: 0x0005D1CC
		// (set) Token: 0x0600256A RID: 9578 RVA: 0x0005EFF9 File Offset: 0x0005D1F9
		[DefaultValue("")]
		[WebCategory("Accessibility")]
		public virtual string RowHeaderColumn
		{
			get
			{
				object obj = this.ViewState["RowHeaderColumn"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				if (value == this.RowHeaderColumn)
				{
					return;
				}
				this.ViewState["RowHeaderColumn"] = value;
				this.RequireBinding();
			}
		}

		/// <summary>Gets a collection of <see cref="T:System.Web.UI.WebControls.GridViewRow" /> objects that represent the data rows in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.GridViewRowCollection" /> that contains all the data rows in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</returns>
		// Token: 0x17000C03 RID: 3075
		// (get) Token: 0x0600256B RID: 9579 RVA: 0x0005F021 File Offset: 0x0005D221
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual GridViewRowCollection Rows
		{
			get
			{
				this.EnsureChildControls();
				if (this.rows == null)
				{
					this.rows = new GridViewRowCollection(new ArrayList());
				}
				return this.rows;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that enables you to set the appearance of the data rows in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that represents the style of the data rows in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</returns>
		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x0600256C RID: 9580 RVA: 0x0005F047 File Offset: 0x0005D247
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebCategory("Styles")]
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

		/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.DataKey" /> object that contains the data key value for the selected row in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.DataKey" /> for the selected row in a <see cref="T:System.Web.UI.WebControls.GridView" /> control. The default is null, which indicates that no row is currently selected.</returns>
		/// <exception cref="T:System.InvalidOperationException">No data keys are specified in the <see cref="P:System.Web.UI.WebControls.GridView.DataKeyNames" /> property.</exception>
		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x0600256D RID: 9581 RVA: 0x0005F078 File Offset: 0x0005D278
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual DataKey SelectedDataKey
		{
			get
			{
				if (this.DataKeyNames.Length == 0)
				{
					throw new InvalidOperationException(string.Format("Data keys must be specified on GridView '{0}' before the selected data keys can be retrieved.  Use the DataKeyNames property to specify data keys.", this.ID));
				}
				if (this.selectedIndex >= 0 && this.selectedIndex < this.DataKeys.Count)
				{
					return this.DataKeys[this.selectedIndex];
				}
				return null;
			}
		}

		/// <summary>Gets or sets the data-key value for the persisted selected item in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
		/// <returns>The data key for the persisted selected item in a <see cref="T:System.Web.UI.WebControls.GridView" /> control. The default is null, which indicates that no item is currently selected.</returns>
		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x0600256E RID: 9582 RVA: 0x0005F0D3 File Offset: 0x0005D2D3
		// (set) Token: 0x0600256F RID: 9583 RVA: 0x0005F0DB File Offset: 0x0005D2DB
		[global::System.MonoTODO]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual DataKey SelectedPersistedDataKey { get; set; }

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.WebControls.IPersistedSelector.DataKey" />.</summary>
		/// <returns>The data-key value for the persisted selected record in a data-bound control.</returns>
		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x06002570 RID: 9584 RVA: 0x0005F0E4 File Offset: 0x0005D2E4
		// (set) Token: 0x06002571 RID: 9585 RVA: 0x0005F0EC File Offset: 0x0005D2EC
		[global::System.MonoTODO]
		DataKey IPersistedSelector.DataKey
		{
			get
			{
				return this.SelectedPersistedDataKey;
			}
			set
			{
				this.SelectedPersistedDataKey = value;
			}
		}

		/// <summary>Gets or sets the index of the selected row in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
		/// <returns>The zero-based index of the selected row in a <see cref="T:System.Web.UI.WebControls.GridView" /> control. The default is -1, which indicates that no row is currently selected.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <see cref="P:System.Web.UI.WebControls.GridView.SelectedIndex" /> property is set to a value less than -1. </exception>
		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x06002572 RID: 9586 RVA: 0x0005F0F5 File Offset: 0x0005D2F5
		// (set) Token: 0x06002573 RID: 9587 RVA: 0x0005F100 File Offset: 0x0005D300
		[Bindable(true)]
		[DefaultValue(-1)]
		public virtual int SelectedIndex
		{
			get
			{
				return this.selectedIndex;
			}
			set
			{
				if (this.rows != null && this.selectedIndex >= 0 && this.selectedIndex < this.Rows.Count)
				{
					int num = this.selectedIndex;
					this.selectedIndex = -1;
					this.Rows[num].RowState = this.GetRowState(num);
				}
				this.selectedIndex = value;
				if (this.rows != null && this.selectedIndex >= 0 && this.selectedIndex < this.Rows.Count)
				{
					this.Rows[this.selectedIndex].RowState = this.GetRowState(this.selectedIndex);
				}
			}
		}

		/// <summary>Gets a reference to a <see cref="T:System.Web.UI.WebControls.GridViewRow" /> object that represents the selected row in the control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.GridViewRow" /> that represents the selected row in the control.</returns>
		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x06002574 RID: 9588 RVA: 0x0005F1A4 File Offset: 0x0005D3A4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual GridViewRow SelectedRow
		{
			get
			{
				if (this.selectedIndex >= 0 && this.selectedIndex < this.Rows.Count)
				{
					return this.Rows[this.selectedIndex];
				}
				return null;
			}
		}

		/// <summary>Gets a reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> object that enables you to set the appearance of the selected row in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
		/// <returns>A reference to the <see cref="T:System.Web.UI.WebControls.TableItemStyle" /> that represents the style of the selected row in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</returns>
		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x06002575 RID: 9589 RVA: 0x0005F1D5 File Offset: 0x0005D3D5
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[WebCategory("Styles")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TableItemStyle SelectedRowStyle
		{
			get
			{
				if (this.selectedRowStyle == null)
				{
					this.selectedRowStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						this.selectedRowStyle.TrackViewState();
					}
				}
				return this.selectedRowStyle;
			}
		}

		/// <summary>Gets the data key value of the selected row in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
		/// <returns>The data key value of the selected row in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</returns>
		// Token: 0x17000C0B RID: 3083
		// (get) Token: 0x06002576 RID: 9590 RVA: 0x0005F203 File Offset: 0x0005D403
		[Browsable(false)]
		public object SelectedValue
		{
			get
			{
				if (this.SelectedDataKey != null)
				{
					return this.SelectedDataKey.Value;
				}
				return null;
			}
		}

		/// <summary>Gets or sets a value indicating whether the footer row is displayed in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
		/// <returns>true to display the footer row; otherwise, false. The default is false.</returns>
		// Token: 0x17000C0C RID: 3084
		// (get) Token: 0x06002577 RID: 9591 RVA: 0x0005F21C File Offset: 0x0005D41C
		// (set) Token: 0x06002578 RID: 9592 RVA: 0x0005F245 File Offset: 0x0005D445
		[WebCategory("Appearance")]
		[DefaultValue(false)]
		public virtual bool ShowFooter
		{
			get
			{
				object obj = this.ViewState["ShowFooter"];
				return obj != null && (bool)obj;
			}
			set
			{
				if (value == this.ShowFooter)
				{
					return;
				}
				this.ViewState["ShowFooter"] = value;
				this.RequireBinding();
			}
		}

		/// <summary>Gets or sets a value indicating whether the header row is displayed in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
		/// <returns>true to display the header row; otherwise, false. The default is true.</returns>
		// Token: 0x17000C0D RID: 3085
		// (get) Token: 0x06002579 RID: 9593 RVA: 0x0005F270 File Offset: 0x0005D470
		// (set) Token: 0x0600257A RID: 9594 RVA: 0x0005F299 File Offset: 0x0005D499
		[WebCategory("Appearance")]
		[DefaultValue(true)]
		public virtual bool ShowHeader
		{
			get
			{
				object obj = this.ViewState["ShowHeader"];
				return obj == null || (bool)obj;
			}
			set
			{
				if (value == this.ShowHeader)
				{
					return;
				}
				this.ViewState["ShowHeader"] = value;
				this.RequireBinding();
			}
		}

		/// <summary>Gets the sort direction of the column being sorted.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.WebControls.SortDirection" /> values. The default is SortDirection.Ascending.</returns>
		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x0600257B RID: 9595 RVA: 0x0005F2C1 File Offset: 0x0005D4C1
		// (set) Token: 0x0600257C RID: 9596 RVA: 0x0005F2C9 File Offset: 0x0005D4C9
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Browsable(false)]
		[DefaultValue(SortDirection.Ascending)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual SortDirection SortDirection
		{
			get
			{
				return this.sortDirection;
			}
			private set
			{
				if (this.sortDirection == value)
				{
					return;
				}
				this.sortDirection = value;
				this.RequireBinding();
			}
		}

		/// <summary>Gets the sort expression associated with the column or columns being sorted.</summary>
		/// <returns>The sort expression associated with the column or columns being sorted.</returns>
		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x0600257D RID: 9597 RVA: 0x0005F2E2 File Offset: 0x0005D4E2
		// (set) Token: 0x0600257E RID: 9598 RVA: 0x0005F2F8 File Offset: 0x0005D4F8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string SortExpression
		{
			get
			{
				if (this.sortExpression == null)
				{
					return string.Empty;
				}
				return this.sortExpression;
			}
			private set
			{
				if (this.sortExpression == value)
				{
					return;
				}
				this.sortExpression = value;
				this.RequireBinding();
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.WebControls.GridViewRow" /> object that represents the top pager row in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.GridViewRow" /> that represents the top pager row in the control.</returns>
		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x0600257F RID: 9599 RVA: 0x0005F316 File Offset: 0x0005D516
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual GridViewRow TopPagerRow
		{
			get
			{
				this.EnsureDataBound();
				return this.topPagerRow;
			}
		}

		/// <summary>Gets or sets a value indicating whether a <see cref="T:System.Web.UI.WebControls.GridView" /> control renders its header in an accessible format. This property is provided to make the control more accessible to users of assistive technology devices.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.WebControls.GridView" /> control renders its header in an accessible format; otherwise, false. The default is true.</returns>
		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x06002580 RID: 9600 RVA: 0x0005F324 File Offset: 0x0005D524
		// (set) Token: 0x06002581 RID: 9601 RVA: 0x0005F34D File Offset: 0x0005D54D
		[WebCategory("Accessibility")]
		[DefaultValue(true)]
		public virtual bool UseAccessibleHeader
		{
			get
			{
				object obj = this.ViewState["UseAccessibleHeader"];
				return obj == null || (bool)obj;
			}
			set
			{
				if (value == this.UseAccessibleHeader)
				{
					return;
				}
				this.ViewState["UseAccessibleHeader"] = value;
				this.RequireBinding();
			}
		}

		/// <summary>Gets or sets the names of the data fields whose values are appended to the <see cref="P:System.Web.UI.Control.ClientID" /> property value to uniquely identify each instance of a data-bound control.</summary>
		/// <returns>The names of the data fields whose values are used to uniquely identify each instance of a data-bound control when ASP.NET generates the <see cref="P:System.Web.UI.Control.ClientID" /> value.</returns>
		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x06002582 RID: 9602 RVA: 0x0005F375 File Offset: 0x0005D575
		// (set) Token: 0x06002583 RID: 9603 RVA: 0x0005F37D File Offset: 0x0005D57D
		[TypeConverter(typeof(StringArrayConverter))]
		[DefaultValue(null)]
		public virtual string[] ClientIDRowSuffix { get; set; }

		/// <summary>Gets the data values that are used to uniquely identify each instance of a data-bound control when ASP.NET generates the <see cref="P:System.Web.UI.Control.ClientID" /> value.</summary>
		/// <returns>The data values that are used to uniquely identify each instance of a data-bound control when ASP.NET generates the <see cref="P:System.Web.UI.Control.ClientID" /> value.</returns>
		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x06002584 RID: 9604 RVA: 0x0005F386 File Offset: 0x0005D586
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DataKeyArray ClientIDRowSuffixDataKeys
		{
			get
			{
				if (this.rowSuffixKeys == null)
				{
					this.rowSuffixKeys = new DataKeyArray(this.DataKeySuffixList);
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.rowSuffixKeys).TrackViewState();
					}
				}
				return this.rowSuffixKeys;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the selection of a row is based on index or on data-key values.</summary>
		/// <returns>true if the row selection is based on data-key values; otherwise, false. The default value is false.</returns>
		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x06002585 RID: 9605 RVA: 0x00003A2B File Offset: 0x00001C2B
		// (set) Token: 0x06002586 RID: 9606 RVA: 0x00003A2B File Offset: 0x00001C2B
		[DefaultValue(false)]
		public virtual bool EnablePersistedSelection
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the control that automatically generates the columns for a data-bound control for use by ASP.NET Dynamic Data. </summary>
		/// <returns>The control that automatically generates the columns for a data-bound control.</returns>
		// Token: 0x17000C15 RID: 3093
		// (get) Token: 0x06002587 RID: 9607 RVA: 0x00003A2B File Offset: 0x00001C2B
		// (set) Token: 0x06002588 RID: 9608 RVA: 0x00003A2B File Offset: 0x00001C2B
		IAutoFieldGenerator IFieldControl.FieldsGenerator
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a value that indicates whether the heading of a column in the <see cref="T:System.Web.UI.WebControls.GridView" /> control is visible when the column has no data. </summary>
		/// <returns>true if the header is visible; otherwise, false.</returns>
		// Token: 0x17000C16 RID: 3094
		// (get) Token: 0x06002589 RID: 9609 RVA: 0x0005F3BA File Offset: 0x0005D5BA
		// (set) Token: 0x0600258A RID: 9610 RVA: 0x0005F3CD File Offset: 0x0005D5CD
		[DefaultValue(false)]
		public virtual bool ShowHeaderWhenEmpty
		{
			get
			{
				return this.ViewState.GetBool("ShowHeaderWhenEmpty", false);
			}
			set
			{
				if (value == this.ShowHeaderWhenEmpty)
				{
					return;
				}
				this.ViewState["ShowHeaderWhenEmpty"] = value;
				this.RequireBinding();
			}
		}

		/// <summary>Gets or sets the CSS style for a <see cref="T:System.Web.UI.WebControls.GridView" /> column when the column is sorted in ascending order. </summary>
		/// <returns>true if a style is applied to the <see cref="T:System.Web.UI.WebControls.GridView" /> control when the column is sorted in ascending order; otherwise, false.</returns>
		// Token: 0x17000C17 RID: 3095
		// (get) Token: 0x0600258B RID: 9611 RVA: 0x0005F3F5 File Offset: 0x0005D5F5
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		public TableItemStyle SortedAscendingCellStyle
		{
			get
			{
				if (this.sortedAscendingCellStyle == null)
				{
					this.sortedAscendingCellStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.sortedAscendingCellStyle).TrackViewState();
					}
				}
				return this.sortedAscendingCellStyle;
			}
		}

		/// <summary>Gets or sets the CSS style to apply to a <see cref="T:System.Web.UI.WebControls.GridView" /> column heading when the column is sorted in ascending order.</summary>
		/// <returns>true if a style is applied to the <see cref="T:System.Web.UI.WebControls.GridView" /> heading when the column is sorted in ascending order; otherwise, false.</returns>
		// Token: 0x17000C18 RID: 3096
		// (get) Token: 0x0600258C RID: 9612 RVA: 0x0005F423 File Offset: 0x0005D623
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public TableItemStyle SortedAscendingHeaderStyle
		{
			get
			{
				if (this.sortedAscendingHeaderStyle == null)
				{
					this.sortedAscendingHeaderStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.sortedAscendingHeaderStyle).TrackViewState();
					}
				}
				return this.sortedAscendingHeaderStyle;
			}
		}

		/// <summary>Gets or sets the style of a <see cref="T:System.Web.UI.WebControls.GridView" /> column when the column is sorted in descending order.</summary>
		/// <returns>true if a style is applied to the <see cref="T:System.Web.UI.WebControls.GridView" /> when the column is sorted in descending order; otherwise, false.</returns>
		// Token: 0x17000C19 RID: 3097
		// (get) Token: 0x0600258D RID: 9613 RVA: 0x0005F451 File Offset: 0x0005D651
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		public TableItemStyle SortedDescendingCellStyle
		{
			get
			{
				if (this.sortedDescendingCellStyle == null)
				{
					this.sortedDescendingCellStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.sortedDescendingCellStyle).TrackViewState();
					}
				}
				return this.sortedDescendingCellStyle;
			}
		}

		/// <summary>Gets or sets the style to apply to a <see cref="T:System.Web.UI.WebControls.GridView" /> column heading when the column is sorted in descending order.</summary>
		/// <returns>true if a style is applied to the <see cref="T:System.Web.UI.WebControls.GridView" /> heading when the column is sorted in descending order; otherwise, false.</returns>
		// Token: 0x17000C1A RID: 3098
		// (get) Token: 0x0600258E RID: 9614 RVA: 0x0005F47F File Offset: 0x0005D67F
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[NotifyParentProperty(true)]
		public TableItemStyle SortedDescendingHeaderStyle
		{
			get
			{
				if (this.sortedDescendingHeaderStyle == null)
				{
					this.sortedDescendingHeaderStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this.sortedDescendingHeaderStyle).TrackViewState();
					}
				}
				return this.sortedDescendingHeaderStyle;
			}
		}

		/// <summary>Determines whether the specified data type can be bound to a column in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
		/// <returns>true if the specified data type can be bound to a column in a <see cref="T:System.Web.UI.WebControls.GridView" /> control; otherwise, false.</returns>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the data type to test. </param>
		// Token: 0x0600258F RID: 9615 RVA: 0x0005F4B0 File Offset: 0x0005D6B0
		public virtual bool IsBindableType(Type type)
		{
			return type.IsPrimitive || type == typeof(string) || type == typeof(decimal) || type == typeof(DateTime) || type == typeof(Guid);
		}

		/// <summary>Creates the <see cref="T:System.Web.UI.DataSourceSelectArguments" /> object that contains the arguments that get passed to the data source for processing.</summary>
		/// <returns>A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> that contains the arguments that get passed to the data source.</returns>
		// Token: 0x06002590 RID: 9616 RVA: 0x0005F510 File Offset: 0x0005D710
		protected override DataSourceSelectArguments CreateDataSourceSelectArguments()
		{
			DataSourceSelectArguments empty = DataSourceSelectArguments.Empty;
			DataSourceView data = this.GetData();
			if (this.AllowPaging && data.CanPage)
			{
				empty.StartRowIndex = this.PageIndex * this.PageSize;
				if (data.CanRetrieveTotalRowCount)
				{
					empty.RetrieveTotalRowCount = true;
					empty.MaximumRows = this.PageSize;
				}
				else
				{
					empty.MaximumRows = -1;
				}
			}
			if (base.IsBoundUsingDataSourceID && !string.IsNullOrEmpty(this.sortExpression))
			{
				if (this.sortDirection == SortDirection.Ascending)
				{
					empty.SortExpression = this.sortExpression;
				}
				else
				{
					empty.SortExpression = this.sortExpression + " DESC";
				}
			}
			return empty;
		}

		/// <summary>Creates the set of column fields used to build the control hierarchy.</summary>
		/// <returns>A <see cref="T:System.Collections.ICollection" /> that contains the fields used to build the control hierarchy.</returns>
		/// <param name="dataSource">A <see cref="T:System.Web.UI.WebControls.PagedDataSource" /> that represents the data source. </param>
		/// <param name="useDataSource">true to use the data source specified by the <paramref name="dataSource" /> parameter; otherwise, false. </param>
		// Token: 0x06002591 RID: 9617 RVA: 0x0005F5B4 File Offset: 0x0005D7B4
		protected virtual ICollection CreateColumns(PagedDataSource dataSource, bool useDataSource)
		{
			bool autoGenerateColumns = this.AutoGenerateColumns;
			if (autoGenerateColumns)
			{
				IAutoFieldGenerator columnsGenerator = this.ColumnsGenerator;
				if (columnsGenerator != null)
				{
					return columnsGenerator.GenerateFields(this);
				}
			}
			ArrayList arrayList = new ArrayList();
			if (this.AutoGenerateEditButton || this.AutoGenerateDeleteButton || this.AutoGenerateSelectButton)
			{
				arrayList.Add(new CommandField
				{
					ShowEditButton = this.AutoGenerateEditButton,
					ShowDeleteButton = this.AutoGenerateDeleteButton,
					ShowSelectButton = this.AutoGenerateSelectButton
				});
			}
			arrayList.AddRange(this.Columns);
			if (autoGenerateColumns)
			{
				if (useDataSource)
				{
					this.autoFieldProperties = this.CreateAutoFieldProperties(dataSource);
				}
				if (this.autoFieldProperties != null)
				{
					foreach (AutoGeneratedFieldProperties autoGeneratedFieldProperties in this.autoFieldProperties)
					{
						arrayList.Add(this.CreateAutoGeneratedColumn(autoGeneratedFieldProperties));
					}
				}
			}
			return arrayList;
		}

		/// <summary>Creates an automatically generated column field.</summary>
		/// <returns>An <see cref="T:System.Web.UI.WebControls.AutoGeneratedField" /> that represents the automatically generated column field specified by the <paramref name="fieldProperties" /> parameter.</returns>
		/// <param name="fieldProperties">An <see cref="T:System.Web.UI.WebControls.AutoGeneratedFieldProperties" /> that represents the properties of the automatically generated column field to create.</param>
		// Token: 0x06002592 RID: 9618 RVA: 0x00058395 File Offset: 0x00056595
		protected virtual AutoGeneratedField CreateAutoGeneratedColumn(AutoGeneratedFieldProperties fieldProperties)
		{
			return new AutoGeneratedField(fieldProperties);
		}

		// Token: 0x06002593 RID: 9619 RVA: 0x0005F688 File Offset: 0x0005D888
		private AutoGeneratedFieldProperties[] CreateAutoFieldProperties(PagedDataSource source)
		{
			if (source == null)
			{
				return null;
			}
			PropertyDescriptorCollection propertyDescriptorCollection = source.GetItemProperties(new PropertyDescriptor[0]);
			Type type = null;
			List<AutoGeneratedFieldProperties> list = new List<AutoGeneratedFieldProperties>();
			if (propertyDescriptorCollection == null)
			{
				object obj = null;
				PropertyInfo property = source.DataSource.GetType().GetProperty("Item", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, null, new Type[] { typeof(int) }, null);
				if (property != null)
				{
					type = property.PropertyType;
				}
				if (type == null || type == typeof(object))
				{
					IEnumerator enumerator = source.GetEnumerator();
					if (enumerator != null && enumerator.MoveNext())
					{
						obj = enumerator.Current;
						this._dataEnumerator = enumerator;
					}
					if (obj != null)
					{
						type = obj.GetType();
					}
				}
				if (obj != null && obj is ICustomTypeDescriptor)
				{
					propertyDescriptorCollection = TypeDescriptor.GetProperties(obj);
				}
				else if (type != null)
				{
					if (this.IsBindableType(type))
					{
						AutoGeneratedFieldProperties autoGeneratedFieldProperties = new AutoGeneratedFieldProperties();
						((IStateManager)autoGeneratedFieldProperties).TrackViewState();
						autoGeneratedFieldProperties.Name = "Item";
						autoGeneratedFieldProperties.DataField = BoundField.ThisExpression;
						autoGeneratedFieldProperties.Type = type;
						list.Add(autoGeneratedFieldProperties);
					}
					else
					{
						propertyDescriptorCollection = TypeDescriptor.GetProperties(type);
					}
				}
			}
			if (propertyDescriptorCollection != null && propertyDescriptorCollection.Count > 0)
			{
				foreach (object obj2 in propertyDescriptorCollection)
				{
					PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj2;
					if (this.IsBindableType(propertyDescriptor.PropertyType) && (type == null || propertyDescriptor.ComponentType == type))
					{
						AutoGeneratedFieldProperties autoGeneratedFieldProperties2 = new AutoGeneratedFieldProperties();
						((IStateManager)autoGeneratedFieldProperties2).TrackViewState();
						autoGeneratedFieldProperties2.Name = propertyDescriptor.Name;
						autoGeneratedFieldProperties2.DataField = propertyDescriptor.Name;
						for (int i = 0; i < this.DataKeyNames.Length; i++)
						{
							if (string.Compare(this.DataKeyNames[i], propertyDescriptor.Name, StringComparison.InvariantCultureIgnoreCase) == 0)
							{
								autoGeneratedFieldProperties2.IsReadOnly = true;
								break;
							}
						}
						autoGeneratedFieldProperties2.Type = propertyDescriptor.PropertyType;
						list.Add(autoGeneratedFieldProperties2);
					}
				}
			}
			if (list.Count > 0)
			{
				return list.ToArray();
			}
			return new AutoGeneratedFieldProperties[0];
		}

		/// <summary>Creates a row in the <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.GridViewRow" /> created using the specified parameters.</returns>
		/// <param name="rowIndex">The index of the row to create. </param>
		/// <param name="dataSourceIndex">The index of the data source item to bind to the row. </param>
		/// <param name="rowType">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowType" /> values. </param>
		/// <param name="rowState">One of the <see cref="T:System.Web.UI.WebControls.DataControlRowState" /> values. </param>
		// Token: 0x06002594 RID: 9620 RVA: 0x0005F8C0 File Offset: 0x0005DAC0
		protected virtual GridViewRow CreateRow(int rowIndex, int dataSourceIndex, DataControlRowType rowType, DataControlRowState rowState)
		{
			return new GridViewRow(rowIndex, dataSourceIndex, rowType, rowState);
		}

		// Token: 0x06002595 RID: 9621 RVA: 0x00044E8A File Offset: 0x0004308A
		private void RequireBinding()
		{
			if (base.Initialized)
			{
				base.RequiresDataBinding = true;
			}
		}

		/// <summary>Creates a new child table.</summary>
		/// <returns>Always returns a new <see cref="T:System.Web.UI.WebControls.Table" /> that represents the child table.</returns>
		// Token: 0x06002596 RID: 9622 RVA: 0x00058542 File Offset: 0x00056742
		protected virtual Table CreateChildTable()
		{
			return new ContainedTable(this);
		}

		// Token: 0x06002597 RID: 9623 RVA: 0x0005F8CC File Offset: 0x0005DACC
		private void CreateHeaderRow(Table mainTable, DataControlField[] fields, bool dataBinding)
		{
			GridViewRow gridViewRow = this.CreateRow(-1, -1, DataControlRowType.Header, DataControlRowState.Normal);
			this.InitializeRow(gridViewRow, fields);
			this.OnRowCreated(new GridViewRowEventArgs(gridViewRow));
			mainTable.Rows.Add(gridViewRow);
			if (dataBinding)
			{
				gridViewRow.DataBind();
				this.OnRowDataBound(new GridViewRowEventArgs(gridViewRow));
			}
		}

		/// <summary>Creates the control hierarchy used to render the <see cref="T:System.Web.UI.WebControls.GridView" /> control using the specified data source.</summary>
		/// <returns>The number of rows created.</returns>
		/// <param name="dataSource">An <see cref="T:System.Collections.IEnumerable" /> that contains the data source for the <see cref="T:System.Web.UI.WebControls.GridView" /> control. </param>
		/// <param name="dataBinding">true to indicate that the child controls are bound to data; otherwise, false. </param>
		/// <exception cref="T:System.Web.HttpException">
		///   <paramref name="dataSource" /> returns a null <see cref="T:System.Web.UI.DataSourceView" />.-or-<paramref name="dataSource" /> does not implement the <see cref="T:System.Collections.ICollection" /> interface and cannot return a <see cref="P:System.Web.UI.DataSourceSelectArguments.TotalRowCount" />. -or-<see cref="P:System.Web.UI.WebControls.GridView.AllowPaging" /> is true and <paramref name="dataSource" /> does not implement the <see cref="T:System.Collections.ICollection" /> interface and cannot perform data source paging.-or-<paramref name="dataSource" /> does not implement the <see cref="T:System.Collections.ICollection" /> interface and <paramref name="dataBinding" /> is set to false.</exception>
		// Token: 0x06002598 RID: 9624 RVA: 0x0005F91C File Offset: 0x0005DB1C
		protected override int CreateChildControls(IEnumerable dataSource, bool dataBinding)
		{
			this.Controls.Clear();
			this.table = null;
			this.rows = null;
			if (dataSource == null)
			{
				return 0;
			}
			PagedDataSource pagedDataSource;
			if (dataBinding)
			{
				DataSourceView data = this.GetData();
				pagedDataSource = new PagedDataSource();
				pagedDataSource.DataSource = dataSource;
				if (this.AllowPaging)
				{
					pagedDataSource.AllowPaging = true;
					pagedDataSource.PageSize = this.PageSize;
					if (data.CanPage)
					{
						pagedDataSource.AllowServerPaging = true;
						if (base.SelectArguments.RetrieveTotalRowCount)
						{
							pagedDataSource.VirtualCount = base.SelectArguments.TotalRowCount;
						}
					}
					if (this.PageIndex >= pagedDataSource.PageCount)
					{
						this.pageIndex = pagedDataSource.PageCount - 1;
					}
					pagedDataSource.CurrentPageIndex = this.PageIndex;
				}
				this.PageCount = pagedDataSource.PageCount;
			}
			else
			{
				pagedDataSource = new PagedDataSource();
				pagedDataSource.DataSource = dataSource;
				if (this.AllowPaging)
				{
					pagedDataSource.AllowPaging = true;
					pagedDataSource.PageSize = this.PageSize;
					pagedDataSource.CurrentPageIndex = this.PageIndex;
				}
			}
			bool flag = this.AllowPaging && this.PageCount >= 1 && this.PagerSettings.Visible;
			ArrayList arrayList = new ArrayList();
			this._dataEnumerator = null;
			ICollection collection = this.CreateColumns(pagedDataSource, dataBinding);
			int count = collection.Count;
			DataControlField[] array = new DataControlField[count];
			collection.CopyTo(array, 0);
			for (int i = 0; i < count; i++)
			{
				DataControlField dataControlField = array[i];
				dataControlField.Initialize(this.AllowSorting, this);
				if (this.EnableSortingAndPagingCallbacks)
				{
					dataControlField.ValidateSupportsCallback();
				}
			}
			bool flag2 = false;
			IEnumerator enumerator;
			if (this._dataEnumerator != null)
			{
				enumerator = this._dataEnumerator;
				flag2 = true;
			}
			else
			{
				enumerator = pagedDataSource.GetEnumerator();
			}
			Table containedTable = this.ContainedTable;
			List<DataKey> list;
			string[] array2;
			List<DataKey> list2;
			string[] array3;
			if (dataBinding)
			{
				list = this.DataKeyList;
				array2 = this.DataKeyNames;
				list2 = this.DataKeySuffixList;
				array3 = this.ClientIDRowSuffix;
			}
			else
			{
				list = null;
				array2 = null;
				list2 = null;
				array3 = null;
			}
			while (flag2 || enumerator.MoveNext())
			{
				flag2 = false;
				object obj = enumerator.Current;
				if (arrayList.Count == 0)
				{
					if (flag && (this.PagerSettings.Position == PagerPosition.Top || this.PagerSettings.Position == PagerPosition.TopAndBottom))
					{
						this.topPagerRow = this.CreatePagerRow(count, pagedDataSource);
						this.OnRowCreated(new GridViewRowEventArgs(this.topPagerRow));
						containedTable.Rows.Add(this.topPagerRow);
						if (dataBinding)
						{
							this.topPagerRow.DataBind();
							this.OnRowDataBound(new GridViewRowEventArgs(this.topPagerRow));
						}
						if (this.PageCount == 1)
						{
							this.topPagerRow.Visible = false;
						}
					}
					if (this.ShowHeader)
					{
						this.CreateHeaderRow(containedTable, array, dataBinding);
					}
				}
				DataControlRowState rowState = this.GetRowState(arrayList.Count);
				GridViewRow gridViewRow = this.CreateRow(arrayList.Count, arrayList.Count, DataControlRowType.DataRow, rowState);
				gridViewRow.DataItem = obj;
				arrayList.Add(gridViewRow);
				this.InitializeRow(gridViewRow, array);
				this.OnRowCreated(new GridViewRowEventArgs(gridViewRow));
				containedTable.Rows.Add(gridViewRow);
				if (dataBinding)
				{
					gridViewRow.DataBind();
					if (this.EditIndex == gridViewRow.RowIndex)
					{
						this.oldEditValues = new DataKey(this.GetRowValues(gridViewRow, true, true));
					}
					list.Add(new DataKey(this.CreateRowDataKey(gridViewRow), array2));
					list2.Add(new DataKey(this.CreateRowSuffixDataKey(gridViewRow), array3));
					this.OnRowDataBound(new GridViewRowEventArgs(gridViewRow));
				}
			}
			if (arrayList.Count == 0)
			{
				if (this.ShowHeader && this.ShowHeaderWhenEmpty)
				{
					this.CreateHeaderRow(containedTable, array, dataBinding);
				}
				GridViewRow gridViewRow2 = this.CreateEmptyrRow(count);
				if (gridViewRow2 != null)
				{
					this.OnRowCreated(new GridViewRowEventArgs(gridViewRow2));
					containedTable.Rows.Add(gridViewRow2);
					if (dataBinding)
					{
						gridViewRow2.DataBind();
						this.OnRowDataBound(new GridViewRowEventArgs(gridViewRow2));
					}
				}
				if (containedTable.Rows.Count == 0)
				{
					this.table = null;
				}
				return 0;
			}
			GridViewRow gridViewRow3 = this.CreateRow(-1, -1, DataControlRowType.Footer, DataControlRowState.Normal);
			this.InitializeRow(gridViewRow3, array);
			this.OnRowCreated(new GridViewRowEventArgs(gridViewRow3));
			containedTable.Rows.Add(gridViewRow3);
			if (dataBinding)
			{
				gridViewRow3.DataBind();
				this.OnRowDataBound(new GridViewRowEventArgs(gridViewRow3));
			}
			if (flag && (this.PagerSettings.Position == PagerPosition.Bottom || this.PagerSettings.Position == PagerPosition.TopAndBottom))
			{
				this.bottomPagerRow = this.CreatePagerRow(count, pagedDataSource);
				this.OnRowCreated(new GridViewRowEventArgs(this.bottomPagerRow));
				containedTable.Rows.Add(this.bottomPagerRow);
				if (dataBinding)
				{
					this.bottomPagerRow.DataBind();
					this.OnRowDataBound(new GridViewRowEventArgs(this.bottomPagerRow));
				}
				if (this.PageCount == 1)
				{
					this.bottomPagerRow.Visible = false;
				}
			}
			this.rows = new GridViewRowCollection(arrayList);
			if (!dataBinding)
			{
				return -1;
			}
			if (this.AllowPaging)
			{
				return pagedDataSource.DataSourceCount;
			}
			return arrayList.Count;
		}

		// Token: 0x17000C1B RID: 3099
		// (get) Token: 0x06002599 RID: 9625 RVA: 0x0005FDF0 File Offset: 0x0005DFF0
		private Table ContainedTable
		{
			get
			{
				if (this.table == null)
				{
					this.table = this.CreateChildTable();
					this.Controls.Add(this.table);
				}
				return this.table;
			}
		}

		/// <summary>Creates the default style for the control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> that represents the style for the control.</returns>
		// Token: 0x0600259A RID: 9626 RVA: 0x0005FE1D File Offset: 0x0005E01D
		protected override Style CreateControlStyle()
		{
			return new TableStyle(this.ViewState)
			{
				GridLines = GridLines.Both,
				CellSpacing = 0
			};
		}

		// Token: 0x0600259B RID: 9627 RVA: 0x0005FE38 File Offset: 0x0005E038
		private DataControlRowState GetRowState(int index)
		{
			DataControlRowState dataControlRowState = ((index % 2 == 0) ? DataControlRowState.Normal : DataControlRowState.Alternate);
			if (index == this.SelectedIndex)
			{
				dataControlRowState |= DataControlRowState.Selected;
			}
			if (index == this.EditIndex)
			{
				dataControlRowState |= DataControlRowState.Edit;
			}
			return dataControlRowState;
		}

		// Token: 0x0600259C RID: 9628 RVA: 0x0005FE6C File Offset: 0x0005E06C
		private GridViewRow CreatePagerRow(int fieldCount, PagedDataSource dataSource)
		{
			GridViewRow gridViewRow = this.CreateRow(-1, -1, DataControlRowType.Pager, DataControlRowState.Normal);
			this.InitializePager(gridViewRow, fieldCount, dataSource);
			return gridViewRow;
		}

		/// <summary>Initializes the pager row displayed when the paging feature is enabled.</summary>
		/// <param name="row">A <see cref="T:System.Web.UI.WebControls.GridViewRow" /> that represents the pager row to initialize. </param>
		/// <param name="columnSpan">The number of columns the pager row should span. </param>
		/// <param name="pagedDataSource">A <see cref="T:System.Web.UI.WebControls.PagedDataSource" /> that represents the data source. </param>
		// Token: 0x0600259D RID: 9629 RVA: 0x0005FE90 File Offset: 0x0005E090
		protected virtual void InitializePager(GridViewRow row, int columnSpan, PagedDataSource pagedDataSource)
		{
			TableCell tableCell = new TableCell();
			if (columnSpan > 1)
			{
				tableCell.ColumnSpan = columnSpan;
			}
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

		// Token: 0x0600259E RID: 9630 RVA: 0x0005FEF4 File Offset: 0x0005E0F4
		private GridViewRow CreateEmptyrRow(int fieldCount)
		{
			if (this.emptyDataTemplate == null && string.IsNullOrEmpty(this.EmptyDataText))
			{
				return null;
			}
			GridViewRow gridViewRow = this.CreateRow(-1, -1, DataControlRowType.EmptyDataRow, DataControlRowState.Normal);
			TableCell tableCell = new TableCell();
			tableCell.ColumnSpan = fieldCount;
			if (this.emptyDataTemplate != null)
			{
				this.emptyDataTemplate.InstantiateIn(tableCell);
			}
			else
			{
				tableCell.Text = this.EmptyDataText;
			}
			gridViewRow.Cells.Add(tableCell);
			return gridViewRow;
		}

		/// <summary>Initializes a row in the <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
		/// <param name="row">A <see cref="T:System.Web.UI.WebControls.GridViewRow" /> that represents the row to initialize. </param>
		/// <param name="fields">An array of <see cref="T:System.Web.UI.WebControls.DataControlField" /> objects that represent the column fields in the <see cref="T:System.Web.UI.WebControls.GridView" /> control. </param>
		// Token: 0x0600259F RID: 9631 RVA: 0x0005FF60 File Offset: 0x0005E160
		protected virtual void InitializeRow(GridViewRow row, DataControlField[] fields)
		{
			bool flag = false;
			DataControlRowType rowType = row.RowType;
			DataControlCellType dataControlCellType;
			if (rowType != DataControlRowType.Header)
			{
				if (rowType != DataControlRowType.Footer)
				{
					dataControlCellType = DataControlCellType.DataCell;
				}
				else
				{
					dataControlCellType = DataControlCellType.Footer;
				}
			}
			else
			{
				dataControlCellType = DataControlCellType.Header;
				flag = this.UseAccessibleHeader;
			}
			foreach (DataControlField dataControlField in fields)
			{
				DataControlFieldCell dataControlFieldCell;
				if ((dataControlField is BoundField && ((BoundField)dataControlField).DataField == this.RowHeaderColumn) || flag)
				{
					dataControlFieldCell = new DataControlFieldHeaderCell(dataControlField, flag ? TableHeaderScope.Column : TableHeaderScope.Row);
				}
				else
				{
					dataControlFieldCell = new DataControlFieldCell(dataControlField);
				}
				row.Cells.Add(dataControlFieldCell);
				dataControlField.InitializeCell(dataControlFieldCell, dataControlCellType, row.RowState, row.RowIndex);
			}
		}

		// Token: 0x060025A0 RID: 9632 RVA: 0x0006000C File Offset: 0x0005E20C
		private void LoadAndCacheProperties(string[] names, object dataItem, ref PropertyDescriptor[] cache)
		{
			if (cache != null)
			{
				return;
			}
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(dataItem);
			int num = ((names != null) ? names.Length : 0);
			cache = new PropertyDescriptor[num];
			for (int i = 0; i < num; i++)
			{
				string text = names[i];
				PropertyDescriptor propertyDescriptor = properties.Find(text, true);
				if (propertyDescriptor == null)
				{
					string text2 = "Property '";
					string text3 = text;
					string text4 = "' not found in object of type ";
					Type type = dataItem.GetType();
					throw new InvalidOperationException(text2 + text3 + text4 + ((type != null) ? type.ToString() : null));
				}
				cache[i] = propertyDescriptor;
			}
		}

		// Token: 0x060025A1 RID: 9633 RVA: 0x00060084 File Offset: 0x0005E284
		private IOrderedDictionary CreateDictionaryFromProperties(PropertyDescriptor[] cache, object dataItem)
		{
			OrderedDictionary orderedDictionary = new OrderedDictionary();
			foreach (PropertyDescriptor propertyDescriptor in cache)
			{
				orderedDictionary[propertyDescriptor.Name] = propertyDescriptor.GetValue(dataItem);
			}
			return orderedDictionary;
		}

		// Token: 0x060025A2 RID: 9634 RVA: 0x000600C0 File Offset: 0x0005E2C0
		private IOrderedDictionary CreateRowDataKey(GridViewRow row)
		{
			object dataItem = row.DataItem;
			this.LoadAndCacheProperties(this.DataKeyNames, dataItem, ref this.cachedKeyProperties);
			return this.CreateDictionaryFromProperties(this.cachedKeyProperties, dataItem);
		}

		// Token: 0x060025A3 RID: 9635 RVA: 0x000600F4 File Offset: 0x0005E2F4
		private IOrderedDictionary CreateRowSuffixDataKey(GridViewRow row)
		{
			object dataItem = row.DataItem;
			this.LoadAndCacheProperties(this.ClientIDRowSuffix, dataItem, ref this.cachedSuffixKeyProperties);
			return this.CreateDictionaryFromProperties(this.cachedSuffixKeyProperties, dataItem);
		}

		// Token: 0x060025A4 RID: 9636 RVA: 0x00060128 File Offset: 0x0005E328
		private IOrderedDictionary GetRowValues(GridViewRow row, bool includeReadOnlyFields, bool includePrimaryKey)
		{
			OrderedDictionary orderedDictionary = new OrderedDictionary();
			this.ExtractRowValues(orderedDictionary, row, includeReadOnlyFields, includePrimaryKey);
			return orderedDictionary;
		}

		/// <summary>Retrieves the values of each field declared within the specified row and stores them in the specified <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> object.</summary>
		/// <param name="fieldValues">An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> used to store the field values.</param>
		/// <param name="row">The <see cref="T:System.Web.UI.WebControls.GridViewRow" /> from which to retrieve the field values.</param>
		/// <param name="includeReadOnlyFields">true to include read-only fields; otherwise, false.</param>
		/// <param name="includePrimaryKey">true to include the primary key field or fields; otherwise, false.</param>
		// Token: 0x060025A5 RID: 9637 RVA: 0x00060148 File Offset: 0x0005E348
		protected virtual void ExtractRowValues(IOrderedDictionary fieldValues, GridViewRow row, bool includeReadOnlyFields, bool includePrimaryKey)
		{
			foreach (object obj in row.Cells)
			{
				DataControlFieldCell dataControlFieldCell = ((TableCell)obj) as DataControlFieldCell;
				if (dataControlFieldCell != null)
				{
					DataControlField containingField = dataControlFieldCell.ContainingField;
					if (containingField == null || containingField.Visible)
					{
						dataControlFieldCell.ContainingField.ExtractValuesFromCell(fieldValues, dataControlFieldCell, row.RowState, includeReadOnlyFields);
					}
				}
			}
			if (!includePrimaryKey && this.DataKeyNames != null)
			{
				foreach (string text in this.DataKeyNames)
				{
					fieldValues.Remove(text);
				}
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value for the <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
		/// <returns>The <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value for the <see cref="T:System.Web.UI.WebControls.GridView" /> control.</returns>
		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x060025A6 RID: 9638 RVA: 0x00060200 File Offset: 0x0005E400
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				if (this.EnableSortingAndPagingCallbacks)
				{
					return HtmlTextWriterTag.Div;
				}
				return HtmlTextWriterTag.Table;
			}
		}

		/// <summary>Binds the data source to the <see cref="T:System.Web.UI.WebControls.GridView" /> control. This method cannot be inherited.</summary>
		// Token: 0x060025A7 RID: 9639 RVA: 0x00060210 File Offset: 0x0005E410
		public sealed override void DataBind()
		{
			this.DataKeyList.Clear();
			this.cachedKeyProperties = null;
			base.DataBind();
			this.keys = new DataKeyArray(this.DataKeyList);
			GridViewRow gridViewRow = this.HeaderRow;
			if (gridViewRow != null)
			{
				gridViewRow.Visible = this.ShowHeader;
			}
			gridViewRow = this.FooterRow;
			if (gridViewRow != null)
			{
				gridViewRow.Visible = this.ShowFooter;
			}
		}

		/// <summary>Binds the specified data source to the <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
		/// <param name="data">An <see cref="T:System.Collections.IEnumerable" /> that contains the data source.</param>
		// Token: 0x060025A8 RID: 9640 RVA: 0x00058E17 File Offset: 0x00057017
		protected internal override void PerformDataBinding(IEnumerable data)
		{
			base.PerformDataBinding(data);
		}

		/// <summary>Establishes the control hierarchy.</summary>
		// Token: 0x060025A9 RID: 9641 RVA: 0x00060274 File Offset: 0x0005E474
		protected internal virtual void PrepareControlHierarchy()
		{
			if (this.table == null)
			{
				return;
			}
			this.table.Caption = this.Caption;
			this.table.CaptionAlign = this.CaptionAlign;
			this.table.CopyBaseAttributes(this);
			foreach (object obj in this.table.Rows)
			{
				GridViewRow gridViewRow = (GridViewRow)obj;
				switch (gridViewRow.RowType)
				{
				case DataControlRowType.Header:
					if (this.headerStyle != null && !this.headerStyle.IsEmpty)
					{
						gridViewRow.ControlStyle.MergeWith(this.headerStyle);
					}
					gridViewRow.Visible = this.ShowHeader;
					break;
				case DataControlRowType.Footer:
					if (this.footerStyle != null && !this.footerStyle.IsEmpty)
					{
						gridViewRow.ControlStyle.MergeWith(this.footerStyle);
					}
					gridViewRow.Visible = this.ShowFooter;
					break;
				case DataControlRowType.DataRow:
					if ((gridViewRow.RowState & DataControlRowState.Edit) != DataControlRowState.Normal && this.editRowStyle != null && !this.editRowStyle.IsEmpty)
					{
						gridViewRow.ControlStyle.MergeWith(this.editRowStyle);
					}
					if ((gridViewRow.RowState & DataControlRowState.Selected) != DataControlRowState.Normal && this.selectedRowStyle != null && !this.selectedRowStyle.IsEmpty)
					{
						gridViewRow.ControlStyle.MergeWith(this.selectedRowStyle);
					}
					if ((gridViewRow.RowState & DataControlRowState.Alternate) != DataControlRowState.Normal && this.alternatingRowStyle != null && !this.alternatingRowStyle.IsEmpty)
					{
						gridViewRow.ControlStyle.MergeWith(this.alternatingRowStyle);
					}
					if (this.rowStyle != null && !this.rowStyle.IsEmpty)
					{
						gridViewRow.ControlStyle.MergeWith(this.rowStyle);
					}
					break;
				case DataControlRowType.Pager:
					if (this.pagerStyle != null && !this.pagerStyle.IsEmpty)
					{
						gridViewRow.ControlStyle.MergeWith(this.pagerStyle);
					}
					break;
				case DataControlRowType.EmptyDataRow:
					if (this.emptyDataRowStyle != null && !this.emptyDataRowStyle.IsEmpty)
					{
						gridViewRow.ControlStyle.MergeWith(this.emptyDataRowStyle);
					}
					break;
				}
				string text = this.SortExpression;
				bool flag = !string.IsNullOrEmpty(text);
				foreach (object obj2 in gridViewRow.Cells)
				{
					TableCell tableCell = (TableCell)obj2;
					DataControlFieldCell dataControlFieldCell = tableCell as DataControlFieldCell;
					if (dataControlFieldCell != null)
					{
						DataControlField containingField = dataControlFieldCell.ContainingField;
						if (containingField != null)
						{
							if (!containingField.Visible)
							{
								tableCell.Visible = false;
							}
							else
							{
								DataControlRowType rowType = gridViewRow.RowType;
								if (rowType != DataControlRowType.Header)
								{
									if (rowType != DataControlRowType.Footer)
									{
										if (containingField.ControlStyleCreated && !containingField.ControlStyle.IsEmpty)
										{
											foreach (object obj3 in tableCell.Controls)
											{
												WebControl webControl = ((Control)obj3) as WebControl;
												if (webControl != null)
												{
													webControl.ControlStyle.MergeWith(containingField.ControlStyle);
												}
											}
										}
										if (containingField.ItemStyleCreated && !containingField.ItemStyle.IsEmpty)
										{
											tableCell.ControlStyle.MergeWith(containingField.ItemStyle);
										}
										if (flag)
										{
											this.MergeWithSortingStyle(text, this.sortedAscendingCellStyle, this.sortedDescendingCellStyle, containingField, tableCell);
										}
									}
									else if (containingField.FooterStyleCreated && !containingField.FooterStyle.IsEmpty)
									{
										tableCell.ControlStyle.MergeWith(containingField.FooterStyle);
									}
								}
								else
								{
									if (containingField.HeaderStyleCreated && !containingField.HeaderStyle.IsEmpty)
									{
										tableCell.ControlStyle.MergeWith(containingField.HeaderStyle);
									}
									if (flag)
									{
										this.MergeWithSortingStyle(text, this.sortedAscendingHeaderStyle, this.sortedDescendingHeaderStyle, containingField, tableCell);
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060025AA RID: 9642 RVA: 0x000606D0 File Offset: 0x0005E8D0
		private void MergeWithSortingStyle(string sortExpression, TableItemStyle ascending, TableItemStyle descending, DataControlField field, TableCell cell)
		{
			if (string.Compare(field.SortExpression, sortExpression, StringComparison.OrdinalIgnoreCase) != 0)
			{
				return;
			}
			cell.ControlStyle.MergeWith((this.SortDirection == SortDirection.Ascending) ? ascending : descending);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.Init" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data. </param>
		// Token: 0x060025AB RID: 9643 RVA: 0x000606FC File Offset: 0x0005E8FC
		protected internal override void OnInit(EventArgs e)
		{
			Page page = this.Page;
			if (page != null)
			{
				page.RegisterRequiresControlState(this);
			}
			base.OnInit(e);
		}

		// Token: 0x060025AC RID: 9644 RVA: 0x00060721 File Offset: 0x0005E921
		private void OnFieldsChanged(object sender, EventArgs args)
		{
			this.RequireBinding();
		}

		/// <summary>Rebinds the <see cref="T:System.Web.UI.WebControls.GridView" /> control to its data after the <see cref="P:System.Web.UI.WebControls.DataBoundControl.DataMember" />, <see cref="P:System.Web.UI.WebControls.BaseDataBoundControl.DataSource" />, or <see cref="P:System.Web.UI.WebControls.DataBoundControl.DataSourceID" /> property is changed.</summary>
		// Token: 0x060025AD RID: 9645 RVA: 0x00060729 File Offset: 0x0005E929
		protected override void OnDataPropertyChanged()
		{
			base.OnDataPropertyChanged();
			this.RequireBinding();
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.DataSourceView.DataSourceViewChanged" /> event.</summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060025AE RID: 9646 RVA: 0x00060737 File Offset: 0x0005E937
		protected override void OnDataSourceViewChanged(object sender, EventArgs e)
		{
			base.OnDataSourceViewChanged(sender, e);
			this.RequireBinding();
		}

		/// <summary>Determines whether the event for the Web server control is passed up the page's user interface (UI) server control hierarchy.</summary>
		/// <returns>true if the event has been canceled; otherwise, false.</returns>
		/// <param name="source">The source of the event. </param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data. </param>
		// Token: 0x060025AF RID: 9647 RVA: 0x00060748 File Offset: 0x0005E948
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			GridViewCommandEventArgs e2 = e as GridViewCommandEventArgs;
			if (e2 != null)
			{
				bool flag = false;
				IButtonControl buttonControl = e2.CommandSource as IButtonControl;
				if (buttonControl != null && buttonControl.CausesValidation)
				{
					this.Page.Validate(buttonControl.ValidationGroup);
					flag = true;
				}
				this.OnRowCommand(e2);
				string text = e2.CommandArgument as string;
				if (text == null || text.Length == 0)
				{
					GridViewRow row = e2.Row;
					if (row != null)
					{
						text = row.RowIndex.ToString();
					}
				}
				this.ProcessEvent(e2.CommandName, text, flag);
				return true;
			}
			return base.OnBubbleEvent(source, e);
		}

		/// <summary>Raises the appropriate events for the <see cref="T:System.Web.UI.WebControls.GridView" /> control when it posts back to the server.</summary>
		/// <param name="eventArgument">The event argument from which to create a <see cref="T:System.Web.UI.WebControls.CommandEventArgs" /> for the event or events that are raised.</param>
		// Token: 0x060025B0 RID: 9648 RVA: 0x000607DE File Offset: 0x0005E9DE
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			base.ValidateEvent(this.UniqueID, eventArgument);
			this.RaisePostBackEvent(eventArgument);
		}

		/// <summary>Raises the appropriate events for the <see cref="T:System.Web.UI.WebControls.GridView" /> control when it posts back to the server.</summary>
		/// <param name="eventArgument">The event argument from which to create a <see cref="T:System.Web.UI.WebControls.CommandEventArgs" /> for the event or events that are raised.</param>
		// Token: 0x060025B1 RID: 9649 RVA: 0x000607F4 File Offset: 0x0005E9F4
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			int num = eventArgument.IndexOf('$');
			GridViewCommandEventArgs e;
			if (num != -1)
			{
				e = new GridViewCommandEventArgs(this, new CommandEventArgs(eventArgument.Substring(0, num), eventArgument.Substring(num + 1)));
			}
			else
			{
				e = new GridViewCommandEventArgs(this, new CommandEventArgs(eventArgument, null));
			}
			this.OnRowCommand(e);
			this.ProcessEvent(e.CommandName, (string)e.CommandArgument, false);
		}

		// Token: 0x060025B2 RID: 9650 RVA: 0x0006085C File Offset: 0x0005EA5C
		private void ProcessEvent(string eventName, string param, bool causesValidation)
		{
			uint num = global::<PrivateImplementationDetails>.ComputeStringHash(eventName);
			if (num <= 1847791252U)
			{
				if (num <= 907026896U)
				{
					if (num != 900713019U)
					{
						if (num != 907026896U)
						{
							return;
						}
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
					else
					{
						if (!(eventName == "Cancel"))
						{
							return;
						}
						this.CancelEdit();
						return;
					}
				}
				else if (num != 1049176909U)
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
						int num2 = int.Parse(param);
						this.UpdateRow(this.Rows[num2], num2, causesValidation);
						return;
					}
					else
					{
						if (!(eventName == "Delete"))
						{
							return;
						}
						this.DeleteRow(int.Parse(param));
						return;
					}
				}
				else
				{
					if (!(eventName == "Select"))
					{
						return;
					}
					this.SelectRow(int.Parse(param));
					return;
				}
			}
			else if (num <= 3705854472U)
			{
				if (num != 2220468209U)
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
						this.SetEditRow(int.Parse(param));
						return;
					}
				}
				else
				{
					if (!(eventName == "Sort"))
					{
						return;
					}
					this.Sort(param);
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
					int num4;
					if (!(param == "First"))
					{
						if (!(param == "Last"))
						{
							if (!(param == "Next"))
							{
								if (!(param == "Prev"))
								{
									int num3 = 0;
									int.TryParse(param, out num3);
									num4 = num3 - 1;
								}
								else
								{
									num4 = this.PageIndex - 1;
								}
							}
							else
							{
								num4 = this.PageIndex + 1;
							}
						}
						else
						{
							num4 = this.PageCount - 1;
						}
					}
					else
					{
						num4 = 0;
					}
					this.SetPageIndex(num4);
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

		// Token: 0x060025B3 RID: 9651 RVA: 0x00060AC8 File Offset: 0x0005ECC8
		private void Sort(string newSortExpression)
		{
			SortDirection sortDirection = SortDirection.Ascending;
			if (this.sortExpression == newSortExpression && this.sortDirection == SortDirection.Ascending)
			{
				sortDirection = SortDirection.Descending;
			}
			this.Sort(newSortExpression, sortDirection);
		}

		/// <summary>Sorts the <see cref="T:System.Web.UI.WebControls.GridView" /> control based on the specified sort expression and direction.</summary>
		/// <param name="sortExpression">The sort expression with which to sort the <see cref="T:System.Web.UI.WebControls.GridView" /> control.</param>
		/// <param name="sortDirection">One of the <see cref="T:System.Web.UI.WebControls.SortDirection" /> values.</param>
		/// <exception cref="T:System.Web.HttpException">The <see cref="T:System.Web.UI.WebControls.GridView" /> control is bound to a data source control, but the <see cref="T:System.Web.UI.DataSourceView" /> that is associated with the data source is null.</exception>
		// Token: 0x060025B4 RID: 9652 RVA: 0x00060AF8 File Offset: 0x0005ECF8
		public virtual void Sort(string sortExpression, SortDirection sortDirection)
		{
			GridViewSortEventArgs e = new GridViewSortEventArgs(sortExpression, sortDirection);
			this.OnSorting(e);
			if (e.Cancel)
			{
				return;
			}
			if (base.IsBoundUsingDataSourceID)
			{
				this.EditIndex = -1;
				this.PageIndex = 0;
				this.SortExpression = e.SortExpression;
				this.SortDirection = e.SortDirection;
			}
			this.OnSorted(EventArgs.Empty);
		}

		/// <summary>Selects the row to edit in a <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
		/// <param name="rowIndex">The index of the row to edit.</param>
		// Token: 0x060025B5 RID: 9653 RVA: 0x00060B58 File Offset: 0x0005ED58
		public void SelectRow(int rowIndex)
		{
			GridViewSelectEventArgs e = new GridViewSelectEventArgs(rowIndex);
			this.OnSelectedIndexChanging(e);
			if (!e.Cancel)
			{
				this.RequireBinding();
				this.SelectedIndex = e.NewSelectedIndex;
				this.OnSelectedIndexChanged(EventArgs.Empty);
			}
		}

		/// <summary>Sets the page index of the <see cref="T:System.Web.UI.WebControls.GridView" /> control by using the row index.</summary>
		/// <param name="rowIndex">The index of the row on the page to edit.</param>
		// Token: 0x060025B6 RID: 9654 RVA: 0x00060B98 File Offset: 0x0005ED98
		public void SetPageIndex(int rowIndex)
		{
			GridViewPageEventArgs e = new GridViewPageEventArgs(rowIndex);
			this.OnPageIndexChanging(e);
			if (e.Cancel || !base.IsBoundUsingDataSourceID)
			{
				return;
			}
			this.EndRowEdit();
			this.PageIndex = e.NewPageIndex;
			this.OnPageIndexChanged(EventArgs.Empty);
		}

		/// <summary>Puts a row in edit mode in a <see cref="T:System.Web.UI.WebControls.GridView" /> control by using the specified row index.</summary>
		/// <param name="rowIndex">The index of the row to edit.</param>
		// Token: 0x060025B7 RID: 9655 RVA: 0x00060BE4 File Offset: 0x0005EDE4
		public void SetEditRow(int rowIndex)
		{
			GridViewEditEventArgs e = new GridViewEditEventArgs(rowIndex);
			this.OnRowEditing(e);
			if (e.Cancel || !base.IsBoundUsingDataSourceID)
			{
				return;
			}
			this.EditIndex = e.NewEditIndex;
		}

		// Token: 0x060025B8 RID: 9656 RVA: 0x00060C1C File Offset: 0x0005EE1C
		private void CancelEdit()
		{
			GridViewCancelEditEventArgs e = new GridViewCancelEditEventArgs(this.EditIndex);
			this.OnRowCancelingEdit(e);
			if (e.Cancel || !base.IsBoundUsingDataSourceID)
			{
				return;
			}
			this.EndRowEdit();
		}

		/// <summary>Updates the record at the specified row index using the field values of the row.</summary>
		/// <param name="rowIndex">The index of the row to update.</param>
		/// <param name="causesValidation">true to perform page validation when this method is called; otherwise, false.</param>
		/// <exception cref="T:System.Web.HttpException">The <see cref="T:System.Web.UI.WebControls.GridView" /> control is bound to a data source control, but the <see cref="T:System.Web.UI.DataSourceView" /> associated with the data source is null.</exception>
		// Token: 0x060025B9 RID: 9657 RVA: 0x00060C54 File Offset: 0x0005EE54
		[global::System.MonoTODO("Support two-way binding expressions")]
		public virtual void UpdateRow(int rowIndex, bool causesValidation)
		{
			if (rowIndex != this.EditIndex)
			{
				throw new NotSupportedException();
			}
			GridViewRow gridViewRow = this.Rows[rowIndex];
			this.UpdateRow(gridViewRow, rowIndex, causesValidation);
		}

		// Token: 0x060025BA RID: 9658 RVA: 0x00060C88 File Offset: 0x0005EE88
		private void UpdateRow(GridViewRow row, int rowIndex, bool causesValidation)
		{
			if (causesValidation && this.Page != null && !this.Page.IsValid)
			{
				return;
			}
			this.currentEditOldValues = GridView.CopyOrderedDictionary(this.OldEditValues.Values);
			this.currentEditRowKeys = GridView.CopyOrderedDictionary(this.DataKeys[rowIndex].Values);
			this.currentEditNewValues = this.GetRowValues(row, false, false);
			GridViewUpdateEventArgs e = new GridViewUpdateEventArgs(rowIndex, this.currentEditRowKeys, this.currentEditOldValues, this.currentEditNewValues);
			this.OnRowUpdating(e);
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

		// Token: 0x060025BB RID: 9659 RVA: 0x00060D58 File Offset: 0x0005EF58
		private static IOrderedDictionary CopyOrderedDictionary(IOrderedDictionary sourceDic)
		{
			OrderedDictionary orderedDictionary = new OrderedDictionary();
			foreach (object obj in sourceDic.Keys)
			{
				orderedDictionary.Add(obj, sourceDic[obj]);
			}
			return orderedDictionary;
		}

		// Token: 0x060025BC RID: 9660 RVA: 0x00060DBC File Offset: 0x0005EFBC
		private bool UpdateCallback(int recordsAffected, Exception exception)
		{
			GridViewUpdatedEventArgs e = new GridViewUpdatedEventArgs(recordsAffected, exception, this.currentEditRowKeys, this.currentEditOldValues, this.currentEditNewValues);
			this.OnRowUpdated(e);
			if (!e.KeepInEditMode)
			{
				this.EndRowEdit();
			}
			return e.ExceptionHandled;
		}

		/// <summary>Deletes the record at the specified index from the data source.</summary>
		/// <param name="rowIndex">The index of the row to delete.</param>
		/// <exception cref="T:System.Web.HttpException">The <see cref="T:System.Web.UI.WebControls.GridView" /> control is not bound to a data source control.</exception>
		/// <exception cref="T:System.NotSupportedException">The data source control that the <see cref="T:System.Web.UI.WebControls.GridView" /> control is bound to does not support delete operations, or there is no delete command defined for the data source.</exception>
		// Token: 0x060025BD RID: 9661 RVA: 0x00060E00 File Offset: 0x0005F000
		public virtual void DeleteRow(int rowIndex)
		{
			GridViewRow gridViewRow = this.Rows[rowIndex];
			this.currentEditRowKeys = GridView.CopyOrderedDictionary(this.DataKeys[rowIndex].Values);
			this.currentEditNewValues = this.GetRowValues(gridViewRow, true, true);
			GridViewDeleteEventArgs e = new GridViewDeleteEventArgs(rowIndex, this.currentEditRowKeys, this.currentEditNewValues);
			this.OnRowDeleting(e);
			if (e.Cancel || !base.IsBoundUsingDataSourceID)
			{
				return;
			}
			this.RequireBinding();
			DataSourceView data = this.GetData();
			if (data != null)
			{
				data.Delete(this.currentEditRowKeys, this.currentEditNewValues, new DataSourceViewOperationCallback(this.DeleteCallback));
				return;
			}
			GridViewDeletedEventArgs e2 = new GridViewDeletedEventArgs(0, null, this.currentEditRowKeys, this.currentEditNewValues);
			this.OnRowDeleted(e2);
		}

		// Token: 0x060025BE RID: 9662 RVA: 0x00060EBC File Offset: 0x0005F0BC
		private bool DeleteCallback(int recordsAffected, Exception exception)
		{
			GridViewDeletedEventArgs e = new GridViewDeletedEventArgs(recordsAffected, exception, this.currentEditRowKeys, this.currentEditNewValues);
			this.OnRowDeleted(e);
			return e.ExceptionHandled;
		}

		// Token: 0x060025BF RID: 9663 RVA: 0x00060EEA File Offset: 0x0005F0EA
		private void EndRowEdit()
		{
			this.EditIndex = -1;
			this.oldEditValues = new DataKey(new OrderedDictionary());
			this.currentEditRowKeys = null;
			this.currentEditOldValues = null;
			this.currentEditNewValues = null;
		}

		/// <summary>Loads the state of the properties in the <see cref="T:System.Web.UI.WebControls.GridView" /> control that need to be persisted, even when the <see cref="P:System.Web.UI.Control.EnableViewState" /> property is set to false.</summary>
		/// <param name="savedState">An <see cref="T:System.Object" /> that contains the saved control state values for the control.</param>
		// Token: 0x060025C0 RID: 9664 RVA: 0x00060F18 File Offset: 0x0005F118
		protected internal override void LoadControlState(object savedState)
		{
			if (savedState == null)
			{
				return;
			}
			object[] array = (object[])savedState;
			base.LoadControlState(array[0]);
			this.pageIndex = (int)array[1];
			this.selectedIndex = (int)array[2];
			this.editIndex = (int)array[3];
			this.sortExpression = (string)array[4];
			this.sortDirection = (SortDirection)array[5];
			this.DataKeyNames = (string[])array[6];
			if (array[7] != null)
			{
				this.LoadDataKeyArrayState((object[])array[7], out this.keys);
			}
			if (array[8] != null)
			{
				((IStateManager)this.OldEditValues).LoadViewState(array[8]);
			}
			this.pageCount = (int)array[9];
			if (array[10] != null)
			{
				this.ClientIDRowSuffix = (string[])array[10];
			}
			if (array[11] != null)
			{
				this.LoadDataKeyArrayState((object[])array[11], out this.rowSuffixKeys);
			}
		}

		/// <summary>Saves the state of the properties in the <see cref="T:System.Web.UI.WebControls.GridView" /> control that need to be persisted, even when the <see cref="P:System.Web.UI.Control.EnableViewState" /> property is set to false.</summary>
		/// <returns>Returns the server control's current view state. If there is no view state associated with the control, this method returns null.</returns>
		// Token: 0x060025C1 RID: 9665 RVA: 0x00060FF8 File Offset: 0x0005F1F8
		protected internal override object SaveControlState()
		{
			if (this.EnableSortingAndPagingCallbacks)
			{
				Page page = this.Page;
				ClientScriptManager clientScriptManager = ((page != null) ? page.ClientScript : null);
				if (clientScriptManager != null)
				{
					clientScriptManager.RegisterHiddenField(this.ClientID + "_Page", this.PageIndex.ToString());
					clientScriptManager.RegisterHiddenField(this.ClientID + "_SortExpression", this.SortExpression);
					clientScriptManager.RegisterHiddenField(this.ClientID + "_SortDirection", ((int)this.SortDirection).ToString());
				}
			}
			object obj = base.SaveControlState();
			return new object[]
			{
				obj,
				this.pageIndex,
				this.selectedIndex,
				this.editIndex,
				this.sortExpression,
				this.sortDirection,
				this.DataKeyNames,
				this.SaveDataKeyArrayState(this.keys),
				(this.oldEditValues == null) ? null : ((IStateManager)this.oldEditValues).SaveViewState(),
				this.pageCount,
				this.ClientIDRowSuffix,
				this.SaveDataKeyArrayState(this.rowSuffixKeys)
			};
		}

		// Token: 0x060025C2 RID: 9666 RVA: 0x00061138 File Offset: 0x0005F338
		private object[] SaveDataKeyArrayState(DataKeyArray keys)
		{
			if (keys == null)
			{
				return null;
			}
			object[] array = new object[keys.Count];
			for (int i = 0; i < keys.Count; i++)
			{
				array[i] = ((IStateManager)keys[i]).SaveViewState();
			}
			return array;
		}

		// Token: 0x060025C3 RID: 9667 RVA: 0x00061178 File Offset: 0x0005F378
		private void LoadDataKeyArrayState(object[] state, out DataKeyArray keys)
		{
			List<DataKey> dataKeyList = this.DataKeyList;
			string[] array = this.DataKeyNames;
			int num = array.Length;
			for (int i = 0; i < state.Length; i++)
			{
				DataKey dataKey = new DataKey(new OrderedDictionary(num), array);
				((IStateManager)dataKey).LoadViewState(state[i]);
				dataKeyList.Add(dataKey);
			}
			keys = new DataKeyArray(dataKeyList);
		}

		/// <summary>Tracks view-state changes to the <see cref="T:System.Web.UI.WebControls.GridView" /> control so they can be stored in the control's <see cref="T:System.Web.UI.StateBag" /> object. This object is accessible through the <see cref="P:System.Web.UI.Control.ViewState" /> property.</summary>
		// Token: 0x060025C4 RID: 9668 RVA: 0x000611D0 File Offset: 0x0005F3D0
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this.columns != null)
			{
				((IStateManager)this.columns).TrackViewState();
			}
			if (this.pagerSettings != null)
			{
				((IStateManager)this.pagerSettings).TrackViewState();
			}
			if (this.alternatingRowStyle != null)
			{
				((IStateManager)this.alternatingRowStyle).TrackViewState();
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
			if (this.selectedRowStyle != null)
			{
				((IStateManager)this.selectedRowStyle).TrackViewState();
			}
			if (this.editRowStyle != null)
			{
				((IStateManager)this.editRowStyle).TrackViewState();
			}
			if (this.emptyDataRowStyle != null)
			{
				((IStateManager)this.emptyDataRowStyle).TrackViewState();
			}
			if (this.sortedAscendingCellStyle != null)
			{
				((IStateManager)this.sortedAscendingCellStyle).TrackViewState();
			}
			if (this.sortedAscendingHeaderStyle != null)
			{
				((IStateManager)this.sortedAscendingHeaderStyle).TrackViewState();
			}
			if (this.sortedDescendingCellStyle != null)
			{
				((IStateManager)this.sortedDescendingCellStyle).TrackViewState();
			}
			if (this.sortedDescendingHeaderStyle != null)
			{
				((IStateManager)this.sortedDescendingHeaderStyle).TrackViewState();
			}
			if (this.rowSuffixKeys != null)
			{
				((IStateManager)this.rowSuffixKeys).TrackViewState();
			}
			if (this.keys != null)
			{
				((IStateManager)this.keys).TrackViewState();
			}
			if (this.autoFieldProperties != null)
			{
				AutoGeneratedFieldProperties[] array = this.autoFieldProperties;
				for (int i = 0; i < array.Length; i++)
				{
					((IStateManager)array[i]).TrackViewState();
				}
			}
		}

		/// <summary>Loads the previously saved view state of the <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
		/// <returns>A <see cref="T:System.Object" /> that contains the saved view state values for the control.</returns>
		// Token: 0x060025C5 RID: 9669 RVA: 0x00061338 File Offset: 0x0005F538
		protected override object SaveViewState()
		{
			object[] array = null;
			if (this.autoFieldProperties != null)
			{
				object[] array2 = new object[this.autoFieldProperties.Length];
				bool flag = true;
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i] = ((IStateManager)this.autoFieldProperties[i]).SaveViewState();
					if (array2[i] != null)
					{
						flag = false;
					}
				}
				if (!flag)
				{
					array = array2;
				}
			}
			object[] array3 = new object[]
			{
				base.SaveViewState(),
				(this.columns == null) ? null : ((IStateManager)this.columns).SaveViewState(),
				(this.pagerSettings == null) ? null : ((IStateManager)this.pagerSettings).SaveViewState(),
				(this.alternatingRowStyle == null) ? null : ((IStateManager)this.alternatingRowStyle).SaveViewState(),
				(this.footerStyle == null) ? null : ((IStateManager)this.footerStyle).SaveViewState(),
				(this.headerStyle == null) ? null : ((IStateManager)this.headerStyle).SaveViewState(),
				(this.pagerStyle == null) ? null : ((IStateManager)this.pagerStyle).SaveViewState(),
				(this.rowStyle == null) ? null : ((IStateManager)this.rowStyle).SaveViewState(),
				(this.selectedRowStyle == null) ? null : ((IStateManager)this.selectedRowStyle).SaveViewState(),
				(this.editRowStyle == null) ? null : ((IStateManager)this.editRowStyle).SaveViewState(),
				(this.emptyDataRowStyle == null) ? null : ((IStateManager)this.emptyDataRowStyle).SaveViewState(),
				array,
				(this.sortedAscendingCellStyle == null) ? null : ((IStateManager)this.sortedAscendingCellStyle).SaveViewState(),
				(this.sortedAscendingHeaderStyle == null) ? null : ((IStateManager)this.sortedAscendingHeaderStyle).SaveViewState(),
				(this.sortedDescendingCellStyle == null) ? null : ((IStateManager)this.sortedDescendingCellStyle).SaveViewState(),
				(this.sortedDescendingHeaderStyle == null) ? null : ((IStateManager)this.sortedDescendingHeaderStyle).SaveViewState()
			};
			for (int j = array3.Length - 1; j >= 0; j--)
			{
				if (array3[j] != null)
				{
					return array3;
				}
			}
			return null;
		}

		/// <summary>Loads the previously saved view state of the <see cref="T:System.Web.UI.WebControls.GridView" /> control.</summary>
		/// <param name="savedState">An <see cref="T:System.Object" /> that contains the saved view state values for the control. </param>
		// Token: 0x060025C6 RID: 9670 RVA: 0x00061528 File Offset: 0x0005F728
		protected override void LoadViewState(object savedState)
		{
			if (savedState == null)
			{
				base.LoadViewState(null);
				return;
			}
			object[] array = (object[])savedState;
			if (array[11] != null)
			{
				object[] array2 = (object[])array[11];
				this.autoFieldProperties = new AutoGeneratedFieldProperties[array2.Length];
				for (int i = 0; i < array2.Length; i++)
				{
					IStateManager stateManager = new AutoGeneratedFieldProperties();
					stateManager.TrackViewState();
					stateManager.LoadViewState(array2[i]);
					this.autoFieldProperties[i] = (AutoGeneratedFieldProperties)stateManager;
				}
			}
			base.LoadViewState(array[0]);
			if (array[1] != null)
			{
				((IStateManager)this.Columns).LoadViewState(array[1]);
			}
			if (array[2] != null)
			{
				((IStateManager)this.PagerSettings).LoadViewState(array[2]);
			}
			if (array[3] != null)
			{
				((IStateManager)this.AlternatingRowStyle).LoadViewState(array[3]);
			}
			if (array[4] != null)
			{
				((IStateManager)this.FooterStyle).LoadViewState(array[4]);
			}
			if (array[5] != null)
			{
				((IStateManager)this.HeaderStyle).LoadViewState(array[5]);
			}
			if (array[6] != null)
			{
				((IStateManager)this.PagerStyle).LoadViewState(array[6]);
			}
			if (array[7] != null)
			{
				((IStateManager)this.RowStyle).LoadViewState(array[7]);
			}
			if (array[8] != null)
			{
				((IStateManager)this.SelectedRowStyle).LoadViewState(array[8]);
			}
			if (array[9] != null)
			{
				((IStateManager)this.EditRowStyle).LoadViewState(array[9]);
			}
			if (array[10] != null)
			{
				((IStateManager)this.EmptyDataRowStyle).LoadViewState(array[10]);
			}
			if (array[12] != null)
			{
				((IStateManager)this.sortedAscendingCellStyle).LoadViewState(array[12]);
			}
			if (array[13] != null)
			{
				((IStateManager)this.sortedAscendingHeaderStyle).LoadViewState(array[13]);
			}
			if (array[14] != null)
			{
				((IStateManager)this.sortedDescendingCellStyle).LoadViewState(array[14]);
			}
			if (array[15] != null)
			{
				((IStateManager)this.sortedDescendingHeaderStyle).LoadViewState(array[15]);
			}
		}

		/// <summary>Creates the arguments for the callback handler in the <see cref="M:System.Web.UI.ClientScriptManager.GetCallbackEventReference(System.Web.UI.Control,System.String,System.String,System.String,System.Boolean)" /> method.</summary>
		/// <param name="eventArgument">The argument to pass to the event handler.</param>
		// Token: 0x060025C7 RID: 9671 RVA: 0x000616B5 File Offset: 0x0005F8B5
		void ICallbackEventHandler.RaiseCallbackEvent(string eventArgument)
		{
			this.RaiseCallbackEvent(eventArgument);
		}

		/// <summary>Creates the arguments for the callback handler in the <see cref="M:System.Web.UI.ClientScriptManager.GetCallbackEventReference(System.Web.UI.Control,System.String,System.String,System.String,System.Boolean)" /> method.</summary>
		/// <param name="eventArgument">The argument to pass to the event handler.</param>
		// Token: 0x060025C8 RID: 9672 RVA: 0x000616C0 File Offset: 0x0005F8C0
		protected virtual void RaiseCallbackEvent(string eventArgument)
		{
			string[] array = eventArgument.Split('|', StringSplitOptions.None);
			this.PageIndex = int.Parse(array[0]);
			this.SortExpression = HttpUtility.UrlDecode(array[1]);
			this.SortDirection = (SortDirection)int.Parse(array[2]);
			this.RaisePostBackEvent(array[3]);
			this.DataBind();
		}

		/// <summary>Returns the result of a callback event that targets a control.</summary>
		/// <returns>The results of the callback.</returns>
		// Token: 0x060025C9 RID: 9673 RVA: 0x00061710 File Offset: 0x0005F910
		string ICallbackEventHandler.GetCallbackResult()
		{
			return this.GetCallbackResult();
		}

		/// <summary>Returns the result of a callback event that targets a control.</summary>
		/// <returns>The results of the callback.</returns>
		// Token: 0x060025CA RID: 9674 RVA: 0x00061718 File Offset: 0x0005F918
		protected virtual string GetCallbackResult()
		{
			this.PrepareControlHierarchy();
			StringWriter stringWriter = new StringWriter();
			stringWriter.Write(string.Concat(new string[]
			{
				this.PageIndex.ToString(),
				"|",
				this.SortExpression,
				"|",
				((int)this.SortDirection).ToString(),
				"|"
			}));
			HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
			this.RenderGrid(htmlTextWriter);
			return stringWriter.ToString();
		}

		/// <summary>Creates the callback script for a button that performs a sorting operation.</summary>
		/// <returns>The callback script for a button that performs a sorting operation.</returns>
		/// <param name="buttonControl">The button control for which to create the callback script.</param>
		/// <param name="argument">The arguments to pass to the callback script.</param>
		// Token: 0x060025CB RID: 9675 RVA: 0x00061799 File Offset: 0x0005F999
		string ICallbackContainer.GetCallbackScript(IButtonControl buttonControl, string argument)
		{
			return this.GetCallbackScript(buttonControl, argument);
		}

		/// <summary>Creates the callback script for a button that performs a sorting operation.</summary>
		/// <returns>The callback script for a button that performs a sorting operation.</returns>
		/// <param name="buttonControl">The button control for which to create the callback script.</param>
		/// <param name="argument">The arguments to pass to the callback script.</param>
		// Token: 0x060025CC RID: 9676 RVA: 0x000617A4 File Offset: 0x0005F9A4
		protected virtual string GetCallbackScript(IButtonControl buttonControl, string argument)
		{
			if (this.EnableSortingAndPagingCallbacks)
			{
				Page page = this.Page;
				if (page != null)
				{
					page.ClientScript.RegisterForEventValidation(this.UniqueID, argument);
				}
				return string.Concat(new string[] { "javascript:GridView_ClientEvent (\"", this.ClientID, "\",\"", buttonControl.CommandName, "$", buttonControl.CommandArgument, "\"); return false;" });
			}
			return null;
		}

		/// <summary>Sets the initialized state of the data-bound control before the control is loaded.</summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060025CD RID: 9677 RVA: 0x00061820 File Offset: 0x0005FA20
		protected override void OnPagePreLoad(object sender, EventArgs e)
		{
			base.OnPagePreLoad(sender, e);
			Page page = this.Page;
			if (page != null && page.IsPostBack && this.EnableSortingAndPagingCallbacks)
			{
				HttpRequest request = page.Request;
				if (request != null)
				{
					int num;
					if (int.TryParse(request.Form[this.ClientID + "_Page"], out num))
					{
						this.PageIndex = num;
					}
					if (int.TryParse(request.Form[this.ClientID + "_SortDirection"], out num))
					{
						this.SortDirection = (SortDirection)num;
					}
					this.SortExpression = request.Form[this.ClientID + "_SortExpression"];
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060025CE RID: 9678 RVA: 0x000618DC File Offset: 0x0005FADC
		protected internal override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			if (this.EnableSortingAndPagingCallbacks)
			{
				Page page = this.Page;
				ClientScriptManager clientScriptManager = ((page != null) ? page.ClientScript : null);
				if (clientScriptManager != null)
				{
					if (!clientScriptManager.IsClientScriptIncludeRegistered(typeof(GridView), "GridView.js"))
					{
						string webResourceUrl = clientScriptManager.GetWebResourceUrl(typeof(GridView), "GridView.js");
						clientScriptManager.RegisterClientScriptInclude(typeof(GridView), "GridView.js", webResourceUrl);
					}
					string text = this.ClientID + "_data";
					string text2 = string.Format("var {0} = new Object ();\n{0}.pageIndex = {1};\n{0}.sortExp = {2};\n{0}.sortDir = {3};\n{0}.uid = {4};\n{0}.form = {5};\n", new object[]
					{
						text,
						ClientScriptManager.GetScriptLiteral(this.PageIndex),
						ClientScriptManager.GetScriptLiteral((this.SortExpression == null) ? string.Empty : this.SortExpression),
						ClientScriptManager.GetScriptLiteral((int)this.SortDirection),
						ClientScriptManager.GetScriptLiteral(this.UniqueID),
						page.theForm
					});
					clientScriptManager.RegisterStartupScript(typeof(TreeView), this.UniqueID, text2, true);
					clientScriptManager.GetCallbackEventReference(this, "null", string.Empty, "null");
					clientScriptManager.GetPostBackClientHyperlink(this, string.Empty);
				}
			}
		}

		/// <summary>Renders the Web server control content to the client's browser using the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object.</summary>
		/// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> used to render the server control content on the client's browser. </param>
		// Token: 0x060025CF RID: 9679 RVA: 0x00061A16 File Offset: 0x0005FC16
		protected internal override void Render(HtmlTextWriter writer)
		{
			this.PrepareControlHierarchy();
			if (this.EnableSortingAndPagingCallbacks)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID + "_div");
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Div);
			this.RenderGrid(writer);
			writer.RenderEndTag();
		}

		// Token: 0x060025D0 RID: 9680 RVA: 0x00061A53 File Offset: 0x0005FC53
		private void RenderGrid(HtmlTextWriter writer)
		{
			if (this.table == null)
			{
				return;
			}
			this.table.Render(writer);
		}

		/// <summary>Creates a <see cref="T:System.Web.UI.PostBackOptions" /> object that represents the postback behavior of the specified button control.</summary>
		/// <returns>A <see cref="T:System.Web.UI.PostBackOptions" /> that represents the postback behavior of the specified button control.</returns>
		/// <param name="buttonControl">The button control for which to create the callback script.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buttonControl" /> parameter contains null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="buttonControl" /> causes validation and is attempting to post back to the same container it validates.</exception>
		// Token: 0x060025D1 RID: 9681 RVA: 0x00061A6C File Offset: 0x0005FC6C
		PostBackOptions IPostBackContainer.GetPostBackOptions(IButtonControl control)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (control.CausesValidation)
			{
				throw new InvalidOperationException("A button that causes validation in GridView '" + this.ID + "' is attempting to use the container GridView as the post back target.  The button should either turn off validation or use itself as the post back container.");
			}
			return new PostBackOptions(this)
			{
				Argument = control.CommandName + "$" + control.CommandArgument,
				RequiresJavaScriptProtocol = true
			};
		}

		// Token: 0x060025D2 RID: 9682 RVA: 0x00061AD4 File Offset: 0x0005FCD4
		// Note: this type is marked as 'beforefieldinit'.
		static GridView()
		{
			GridView.PageIndexChangedEvent = new object();
			GridView.PageIndexChangingEvent = new object();
			GridView.RowCancelingEditEvent = new object();
			GridView.RowCommandEvent = new object();
			GridView.RowCreatedEvent = new object();
			GridView.RowDataBoundEvent = new object();
			GridView.RowDeletedEvent = new object();
			GridView.RowDeletingEvent = new object();
			GridView.RowEditingEvent = new object();
			GridView.RowUpdatedEvent = new object();
			GridView.RowUpdatingEvent = new object();
			GridView.SelectedIndexChangedEvent = new object();
			GridView.SelectedIndexChangingEvent = new object();
			GridView.SortedEvent = new object();
			GridView.SortingEvent = new object();
		}

		/// <summary>Gets or sets a value that indicates whether custom paging is enabled.</summary>
		/// <returns>true if custom paging is enabled; otherwise, false. The default is false.</returns>
		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x060025D3 RID: 9683 RVA: 0x00061B78 File Offset: 0x0005FD78
		// (set) Token: 0x060025D4 RID: 9684 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual bool AllowCustomPaging
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

		/// <summary>Gets or sets the name of the method to call in order to delete data.</summary>
		/// <returns>The name of the method.</returns>
		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x060025D5 RID: 9685 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x060025D6 RID: 9686 RVA: 0x0000B3D8 File Offset: 0x000095D8
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

		// Token: 0x060025D7 RID: 9687 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		string IDataBoundControl.get_DataMember()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x060025D8 RID: 9688 RVA: 0x0000B3D8 File Offset: 0x000095D8
		void IDataBoundControl.set_DataMember(string value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x060025D9 RID: 9689 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		object IDataBoundControl.get_DataSource()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x060025DA RID: 9690 RVA: 0x0000B3D8 File Offset: 0x000095D8
		void IDataBoundControl.set_DataSource(object value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x060025DB RID: 9691 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		string IDataBoundControl.get_DataSourceID()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x060025DC RID: 9692 RVA: 0x0000B3D8 File Offset: 0x000095D8
		void IDataBoundControl.set_DataSourceID(string value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x060025DD RID: 9693 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		IDataSource IDataBoundControl.get_DataSourceObject()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets or sets the name of the method to call in order to update data.</summary>
		/// <returns>The name of the method.</returns>
		// Token: 0x17000C1F RID: 3103
		// (get) Token: 0x060025DE RID: 9694 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		// (set) Token: 0x060025DF RID: 9695 RVA: 0x0000B3D8 File Offset: 0x000095D8
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

		/// <summary>Gets or sets the virtual number of items in the data source that the <see cref="T:System.Web.UI.WebControls.GridView" /> control is bound to when custom paging is used.</summary>
		/// <returns>The virtual number of items in the data source that the <see cref="T:System.Web.UI.WebControls.GridView" /> control is bound to when custom paging is used.</returns>
		// Token: 0x17000C20 RID: 3104
		// (get) Token: 0x060025E0 RID: 9696 RVA: 0x00061B94 File Offset: 0x0005FD94
		// (set) Token: 0x060025E1 RID: 9697 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual int VirtualItemCount
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x040019F1 RID: 6641
		private Table table;

		// Token: 0x040019F2 RID: 6642
		private GridViewRowCollection rows;

		// Token: 0x040019F3 RID: 6643
		private GridViewRow bottomPagerRow;

		// Token: 0x040019F4 RID: 6644
		private GridViewRow topPagerRow;

		// Token: 0x040019F5 RID: 6645
		private IOrderedDictionary currentEditRowKeys;

		// Token: 0x040019F6 RID: 6646
		private IOrderedDictionary currentEditNewValues;

		// Token: 0x040019F7 RID: 6647
		private IOrderedDictionary currentEditOldValues;

		// Token: 0x040019F8 RID: 6648
		private ITemplate pagerTemplate;

		// Token: 0x040019F9 RID: 6649
		private ITemplate emptyDataTemplate;

		// Token: 0x040019FA RID: 6650
		private PropertyDescriptor[] cachedKeyProperties;

		// Token: 0x040019FB RID: 6651
		private PropertyDescriptor[] cachedSuffixKeyProperties;

		// Token: 0x040019FC RID: 6652
		private DataControlFieldCollection columns;

		// Token: 0x040019FD RID: 6653
		private PagerSettings pagerSettings;

		// Token: 0x040019FE RID: 6654
		private TableItemStyle alternatingRowStyle;

		// Token: 0x040019FF RID: 6655
		private TableItemStyle editRowStyle;

		// Token: 0x04001A00 RID: 6656
		private TableItemStyle emptyDataRowStyle;

		// Token: 0x04001A01 RID: 6657
		private TableItemStyle footerStyle;

		// Token: 0x04001A02 RID: 6658
		private TableItemStyle headerStyle;

		// Token: 0x04001A03 RID: 6659
		private TableItemStyle pagerStyle;

		// Token: 0x04001A04 RID: 6660
		private TableItemStyle rowStyle;

		// Token: 0x04001A05 RID: 6661
		private TableItemStyle selectedRowStyle;

		// Token: 0x04001A06 RID: 6662
		private TableItemStyle sortedAscendingCellStyle;

		// Token: 0x04001A07 RID: 6663
		private TableItemStyle sortedAscendingHeaderStyle;

		// Token: 0x04001A08 RID: 6664
		private TableItemStyle sortedDescendingCellStyle;

		// Token: 0x04001A09 RID: 6665
		private TableItemStyle sortedDescendingHeaderStyle;

		// Token: 0x04001A0A RID: 6666
		private List<DataKey> _dataKeySuffixList;

		// Token: 0x04001A0B RID: 6667
		private DataKeyArray rowSuffixKeys;

		// Token: 0x04001A0C RID: 6668
		private List<DataKey> _dataKeyList;

		// Token: 0x04001A0D RID: 6669
		private DataKeyArray keys;

		// Token: 0x04001A0E RID: 6670
		private DataKey oldEditValues;

		// Token: 0x04001A0F RID: 6671
		private AutoGeneratedFieldProperties[] autoFieldProperties;

		// Token: 0x04001A10 RID: 6672
		private string[] dataKeyNames;

		// Token: 0x04001A11 RID: 6673
		private readonly string[] emptyKeys = new string[0];

		// Token: 0x04001A12 RID: 6674
		private IEnumerator _dataEnumerator;

		// Token: 0x04001A22 RID: 6690
		private int pageIndex;

		// Token: 0x04001A23 RID: 6691
		private int selectedIndex = -1;

		// Token: 0x04001A24 RID: 6692
		private int editIndex = -1;

		// Token: 0x04001A25 RID: 6693
		private int pageCount;

		// Token: 0x04001A26 RID: 6694
		private SortDirection sortDirection;

		// Token: 0x04001A27 RID: 6695
		private string sortExpression;

		// Token: 0x04001A2C RID: 6700
		private const string onPreRenderScript = "var {0} = new Object ();\n{0}.pageIndex = {1};\n{0}.sortExp = {2};\n{0}.sortDir = {3};\n{0}.uid = {4};\n{0}.form = {5};\n";
	}
}

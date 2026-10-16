using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Reflection;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Supports the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control and provides an interface for data-bound controls to perform data operations with business and data objects.</summary>
	// Token: 0x020003DA RID: 986
	public class ObjectDataSourceView : DataSourceView, IStateManager
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> class.</summary>
		/// <param name="owner">The <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> that the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> is associated with. </param>
		/// <param name="name">A unique name for the data source view, within the scope of the data source control that owns it.</param>
		/// <param name="context">The current <see cref="T:System.Web.HttpContext" />.</param>
		// Token: 0x06002AB7 RID: 10935 RVA: 0x0006ED3F File Offset: 0x0006CF3F
		public ObjectDataSourceView(ObjectDataSource owner, string name, HttpContext context)
			: base(owner, name)
		{
			this.owner = owner;
			this.context = context;
		}

		/// <summary>Occurs when a <see cref="Overload:System.Web.UI.WebControls.ObjectDataSourceView.Delete" /> operation has completed.</summary>
		// Token: 0x140000BF RID: 191
		// (add) Token: 0x06002AB8 RID: 10936 RVA: 0x0006ED57 File Offset: 0x0006CF57
		// (remove) Token: 0x06002AB9 RID: 10937 RVA: 0x0006ED6A File Offset: 0x0006CF6A
		public event ObjectDataSourceStatusEventHandler Deleted
		{
			add
			{
				base.Events.AddHandler(ObjectDataSourceView.DeletedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ObjectDataSourceView.DeletedEvent, value);
			}
		}

		/// <summary>Occurs before a <see cref="Overload:System.Web.UI.WebControls.ObjectDataSourceView.Delete" /> operation.</summary>
		// Token: 0x140000C0 RID: 192
		// (add) Token: 0x06002ABA RID: 10938 RVA: 0x0006ED7D File Offset: 0x0006CF7D
		// (remove) Token: 0x06002ABB RID: 10939 RVA: 0x0006ED90 File Offset: 0x0006CF90
		public event ObjectDataSourceMethodEventHandler Deleting
		{
			add
			{
				base.Events.AddHandler(ObjectDataSourceView.DeletingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ObjectDataSourceView.DeletingEvent, value);
			}
		}

		/// <summary>Occurs before a filter operation.</summary>
		// Token: 0x140000C1 RID: 193
		// (add) Token: 0x06002ABC RID: 10940 RVA: 0x0006EDA3 File Offset: 0x0006CFA3
		// (remove) Token: 0x06002ABD RID: 10941 RVA: 0x0006EDB6 File Offset: 0x0006CFB6
		public event ObjectDataSourceFilteringEventHandler Filtering
		{
			add
			{
				base.Events.AddHandler(ObjectDataSourceView.FilteringEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ObjectDataSourceView.FilteringEvent, value);
			}
		}

		/// <summary>Occurs when an <see cref="Overload:System.Web.UI.WebControls.ObjectDataSourceView.Insert" /> operation has completed.</summary>
		// Token: 0x140000C2 RID: 194
		// (add) Token: 0x06002ABE RID: 10942 RVA: 0x0006EDC9 File Offset: 0x0006CFC9
		// (remove) Token: 0x06002ABF RID: 10943 RVA: 0x0006EDDC File Offset: 0x0006CFDC
		public event ObjectDataSourceStatusEventHandler Inserted
		{
			add
			{
				base.Events.AddHandler(ObjectDataSourceView.InsertedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ObjectDataSourceView.InsertedEvent, value);
			}
		}

		/// <summary>Occurs before an <see cref="Overload:System.Web.UI.WebControls.ObjectDataSourceView.Insert" /> operation.</summary>
		// Token: 0x140000C3 RID: 195
		// (add) Token: 0x06002AC0 RID: 10944 RVA: 0x0006EDEF File Offset: 0x0006CFEF
		// (remove) Token: 0x06002AC1 RID: 10945 RVA: 0x0006EE02 File Offset: 0x0006D002
		public event ObjectDataSourceMethodEventHandler Inserting
		{
			add
			{
				base.Events.AddHandler(ObjectDataSourceView.InsertingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ObjectDataSourceView.InsertingEvent, value);
			}
		}

		/// <summary>Occurs after the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object creates an instance of the type that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.TypeName" /> property.</summary>
		// Token: 0x140000C4 RID: 196
		// (add) Token: 0x06002AC2 RID: 10946 RVA: 0x0006EE15 File Offset: 0x0006D015
		// (remove) Token: 0x06002AC3 RID: 10947 RVA: 0x0006EE28 File Offset: 0x0006D028
		public event ObjectDataSourceObjectEventHandler ObjectCreated
		{
			add
			{
				base.Events.AddHandler(ObjectDataSourceView.ObjectCreatedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ObjectDataSourceView.ObjectCreatedEvent, value);
			}
		}

		/// <summary>Occurs before the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object creates an instance of the type that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.TypeName" /> property.</summary>
		// Token: 0x140000C5 RID: 197
		// (add) Token: 0x06002AC4 RID: 10948 RVA: 0x0006EE3B File Offset: 0x0006D03B
		// (remove) Token: 0x06002AC5 RID: 10949 RVA: 0x0006EE4E File Offset: 0x0006D04E
		public event ObjectDataSourceObjectEventHandler ObjectCreating
		{
			add
			{
				base.Events.AddHandler(ObjectDataSourceView.ObjectCreatingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ObjectDataSourceView.ObjectCreatingEvent, value);
			}
		}

		/// <summary>Occurs when the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object discards an instance of an object that it has created. </summary>
		// Token: 0x140000C6 RID: 198
		// (add) Token: 0x06002AC6 RID: 10950 RVA: 0x0006EE61 File Offset: 0x0006D061
		// (remove) Token: 0x06002AC7 RID: 10951 RVA: 0x0006EE74 File Offset: 0x0006D074
		public event ObjectDataSourceDisposingEventHandler ObjectDisposing
		{
			add
			{
				base.Events.AddHandler(ObjectDataSourceView.ObjectDisposingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ObjectDataSourceView.ObjectDisposingEvent, value);
			}
		}

		/// <summary>Occurs when a data retrieval operation has completed.</summary>
		// Token: 0x140000C7 RID: 199
		// (add) Token: 0x06002AC8 RID: 10952 RVA: 0x0006EE87 File Offset: 0x0006D087
		// (remove) Token: 0x06002AC9 RID: 10953 RVA: 0x0006EE9A File Offset: 0x0006D09A
		public event ObjectDataSourceStatusEventHandler Selected
		{
			add
			{
				base.Events.AddHandler(ObjectDataSourceView.SelectedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ObjectDataSourceView.SelectedEvent, value);
			}
		}

		/// <summary>Occurs before a data retrieval operation.</summary>
		// Token: 0x140000C8 RID: 200
		// (add) Token: 0x06002ACA RID: 10954 RVA: 0x0006EEAD File Offset: 0x0006D0AD
		// (remove) Token: 0x06002ACB RID: 10955 RVA: 0x0006EEC0 File Offset: 0x0006D0C0
		public event ObjectDataSourceSelectingEventHandler Selecting
		{
			add
			{
				base.Events.AddHandler(ObjectDataSourceView.SelectingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ObjectDataSourceView.SelectingEvent, value);
			}
		}

		/// <summary>Occurs when an <see cref="Overload:System.Web.UI.WebControls.ObjectDataSourceView.Update" /> operation has completed.</summary>
		// Token: 0x140000C9 RID: 201
		// (add) Token: 0x06002ACC RID: 10956 RVA: 0x0006EED3 File Offset: 0x0006D0D3
		// (remove) Token: 0x06002ACD RID: 10957 RVA: 0x0006EEE6 File Offset: 0x0006D0E6
		public event ObjectDataSourceStatusEventHandler Updated
		{
			add
			{
				base.Events.AddHandler(ObjectDataSourceView.UpdatedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ObjectDataSourceView.UpdatedEvent, value);
			}
		}

		/// <summary>Occurs before an <see cref="Overload:System.Web.UI.WebControls.ObjectDataSourceView.Update" /> operation.</summary>
		// Token: 0x140000CA RID: 202
		// (add) Token: 0x06002ACE RID: 10958 RVA: 0x0006EEF9 File Offset: 0x0006D0F9
		// (remove) Token: 0x06002ACF RID: 10959 RVA: 0x0006EF0C File Offset: 0x0006D10C
		public event ObjectDataSourceMethodEventHandler Updating
		{
			add
			{
				base.Events.AddHandler(ObjectDataSourceView.UpdatingEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(ObjectDataSourceView.UpdatingEvent, value);
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ObjectDataSourceView.Deleted" /> event after the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object has completed a delete operation.</summary>
		/// <param name="e">An  <see cref="T:System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs" /> that contains the event data.</param>
		// Token: 0x06002AD0 RID: 10960 RVA: 0x0006EF20 File Offset: 0x0006D120
		protected virtual void OnDeleted(ObjectDataSourceStatusEventArgs e)
		{
			if (base.Events != null)
			{
				ObjectDataSourceStatusEventHandler objectDataSourceStatusEventHandler = (ObjectDataSourceStatusEventHandler)base.Events[ObjectDataSourceView.DeletedEvent];
				if (objectDataSourceStatusEventHandler != null)
				{
					objectDataSourceStatusEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ObjectDataSourceView.Deleting" /> event before the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object attempts a delete operation.</summary>
		/// <param name="e">An <see cref="T:System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs" /> that contains the event data.</param>
		// Token: 0x06002AD1 RID: 10961 RVA: 0x0006EF58 File Offset: 0x0006D158
		protected virtual void OnDeleting(ObjectDataSourceMethodEventArgs e)
		{
			if (base.Events != null)
			{
				ObjectDataSourceMethodEventHandler objectDataSourceMethodEventHandler = (ObjectDataSourceMethodEventHandler)base.Events[ObjectDataSourceView.DeletingEvent];
				if (objectDataSourceMethodEventHandler != null)
				{
					objectDataSourceMethodEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ObjectDataSourceView.Filtering" /> event before the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object attempts a filtering operation.</summary>
		/// <param name="e">An <see cref="T:System.Web.UI.WebControls.ObjectDataSourceFilteringEventArgs" /> that contains the event data.</param>
		// Token: 0x06002AD2 RID: 10962 RVA: 0x0006EF90 File Offset: 0x0006D190
		protected virtual void OnFiltering(ObjectDataSourceFilteringEventArgs e)
		{
			if (base.Events != null)
			{
				ObjectDataSourceFilteringEventHandler objectDataSourceFilteringEventHandler = (ObjectDataSourceFilteringEventHandler)base.Events[ObjectDataSourceView.FilteringEvent];
				if (objectDataSourceFilteringEventHandler != null)
				{
					objectDataSourceFilteringEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ObjectDataSourceView.Inserted" /> event after the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object has completed an insert operation.</summary>
		/// <param name="e">An  <see cref="T:System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs" /> that contains the event data.</param>
		// Token: 0x06002AD3 RID: 10963 RVA: 0x0006EFC8 File Offset: 0x0006D1C8
		protected virtual void OnInserted(ObjectDataSourceStatusEventArgs e)
		{
			if (base.Events != null)
			{
				ObjectDataSourceStatusEventHandler objectDataSourceStatusEventHandler = (ObjectDataSourceStatusEventHandler)base.Events[ObjectDataSourceView.InsertedEvent];
				if (objectDataSourceStatusEventHandler != null)
				{
					objectDataSourceStatusEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ObjectDataSourceView.Inserting" /> event before the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object attempts an insert operation.</summary>
		/// <param name="e">An <see cref="T:System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs" /> that contains the event data.</param>
		// Token: 0x06002AD4 RID: 10964 RVA: 0x0006F000 File Offset: 0x0006D200
		protected virtual void OnInserting(ObjectDataSourceMethodEventArgs e)
		{
			if (base.Events != null)
			{
				ObjectDataSourceMethodEventHandler objectDataSourceMethodEventHandler = (ObjectDataSourceMethodEventHandler)base.Events[ObjectDataSourceView.InsertingEvent];
				if (objectDataSourceMethodEventHandler != null)
				{
					objectDataSourceMethodEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ObjectDataSourceView.ObjectCreated" /> event after the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> creates an instance of the object that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.TypeName" /> property. </summary>
		/// <param name="e">An  <see cref="T:System.Web.UI.WebControls.ObjectDataSourceEventArgs" /> that contains the event data.</param>
		// Token: 0x06002AD5 RID: 10965 RVA: 0x0006F038 File Offset: 0x0006D238
		protected virtual void OnObjectCreated(ObjectDataSourceEventArgs e)
		{
			if (base.Events != null)
			{
				ObjectDataSourceObjectEventHandler objectDataSourceObjectEventHandler = (ObjectDataSourceObjectEventHandler)base.Events[ObjectDataSourceView.ObjectCreatedEvent];
				if (objectDataSourceObjectEventHandler != null)
				{
					objectDataSourceObjectEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ObjectDataSourceView.ObjectCreating" /> event before the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object creates an instance of a business object to perform a data operation.</summary>
		/// <param name="e">An <see cref="T:System.Web.UI.WebControls.ObjectDataSourceEventArgs" /> that contains the event data.</param>
		// Token: 0x06002AD6 RID: 10966 RVA: 0x0006F070 File Offset: 0x0006D270
		protected virtual void OnObjectCreating(ObjectDataSourceEventArgs e)
		{
			if (base.Events != null)
			{
				ObjectDataSourceObjectEventHandler objectDataSourceObjectEventHandler = (ObjectDataSourceObjectEventHandler)base.Events[ObjectDataSourceView.ObjectCreatingEvent];
				if (objectDataSourceObjectEventHandler != null)
				{
					objectDataSourceObjectEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ObjectDataSourceView.ObjectDisposing" /> event before the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object discards an instantiated type. </summary>
		/// <param name="e">An  <see cref="T:System.Web.UI.WebControls.ObjectDataSourceDisposingEventArgs" /> that contains the event data.</param>
		// Token: 0x06002AD7 RID: 10967 RVA: 0x0006F0A8 File Offset: 0x0006D2A8
		protected virtual void OnObjectDisposing(ObjectDataSourceDisposingEventArgs e)
		{
			if (base.Events != null)
			{
				ObjectDataSourceDisposingEventHandler objectDataSourceDisposingEventHandler = (ObjectDataSourceDisposingEventHandler)base.Events[ObjectDataSourceView.ObjectDisposingEvent];
				if (objectDataSourceDisposingEventHandler != null)
				{
					objectDataSourceDisposingEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ObjectDataSourceView.Selected" /> event after the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object has completed a data retrieval operation.</summary>
		/// <param name="e">An  <see cref="T:System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs" /> that contains the event data. </param>
		// Token: 0x06002AD8 RID: 10968 RVA: 0x0006F0E0 File Offset: 0x0006D2E0
		protected virtual void OnSelected(ObjectDataSourceStatusEventArgs e)
		{
			if (base.Events != null)
			{
				ObjectDataSourceStatusEventHandler objectDataSourceStatusEventHandler = (ObjectDataSourceStatusEventHandler)base.Events[ObjectDataSourceView.SelectedEvent];
				if (objectDataSourceStatusEventHandler != null)
				{
					objectDataSourceStatusEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ObjectDataSourceView.Selecting" /> event before the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object attempts a data retrieval operation.</summary>
		/// <param name="e">An <see cref="T:System.Web.UI.WebControls.ObjectDataSourceSelectingEventArgs" /> that contains the event data.</param>
		// Token: 0x06002AD9 RID: 10969 RVA: 0x0006F118 File Offset: 0x0006D318
		protected virtual void OnSelecting(ObjectDataSourceSelectingEventArgs e)
		{
			if (base.Events != null)
			{
				ObjectDataSourceSelectingEventHandler objectDataSourceSelectingEventHandler = (ObjectDataSourceSelectingEventHandler)base.Events[ObjectDataSourceView.SelectingEvent];
				if (objectDataSourceSelectingEventHandler != null)
				{
					objectDataSourceSelectingEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ObjectDataSourceView.Updated" /> event after the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object has completed an update operation.</summary>
		/// <param name="e">An  <see cref="T:System.Web.UI.WebControls.ObjectDataSourceStatusEventArgs" /> that contains the event data.</param>
		// Token: 0x06002ADA RID: 10970 RVA: 0x0006F150 File Offset: 0x0006D350
		protected virtual void OnUpdated(ObjectDataSourceStatusEventArgs e)
		{
			if (base.Events != null)
			{
				ObjectDataSourceStatusEventHandler objectDataSourceStatusEventHandler = (ObjectDataSourceStatusEventHandler)base.Events[ObjectDataSourceView.UpdatedEvent];
				if (objectDataSourceStatusEventHandler != null)
				{
					objectDataSourceStatusEventHandler(this, e);
				}
			}
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.WebControls.ObjectDataSourceView.Updating" /> event before the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object attempts an update operation.</summary>
		/// <param name="e">An <see cref="T:System.Web.UI.WebControls.ObjectDataSourceMethodEventArgs" /> that contains the event data.</param>
		// Token: 0x06002ADB RID: 10971 RVA: 0x0006F188 File Offset: 0x0006D388
		protected virtual void OnUpdating(ObjectDataSourceMethodEventArgs e)
		{
			if (base.Events != null)
			{
				ObjectDataSourceMethodEventHandler objectDataSourceMethodEventHandler = (ObjectDataSourceMethodEventHandler)base.Events[ObjectDataSourceView.UpdatingEvent];
				if (objectDataSourceMethodEventHandler != null)
				{
					objectDataSourceMethodEventHandler(this, e);
				}
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object that is associated with the current <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control supports the delete operation.</summary>
		/// <returns>true, if the operation is supported; otherwise, false. Deletion is not supported, if the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.DeleteMethod" /> property is an empty string ("").</returns>
		// Token: 0x17000DB0 RID: 3504
		// (get) Token: 0x06002ADC RID: 10972 RVA: 0x0006F1BE File Offset: 0x0006D3BE
		public override bool CanDelete
		{
			get
			{
				return this.DeleteMethod.Length > 0;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object that is associated with the current <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control supports the insert operation.</summary>
		/// <returns>true, if the operation is supported; otherwise, false. Insertion is not supported, if the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.InsertMethod" /> property is an empty string.</returns>
		// Token: 0x17000DB1 RID: 3505
		// (get) Token: 0x06002ADD RID: 10973 RVA: 0x0006F1CE File Offset: 0x0006D3CE
		public override bool CanInsert
		{
			get
			{
				return this.InsertMethod.Length > 0;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object that is associated with the current <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control supports paging through the retrieved data.</summary>
		/// <returns>true, if the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.EnablePaging" /> value is set to true; otherwise, false. </returns>
		// Token: 0x17000DB2 RID: 3506
		// (get) Token: 0x06002ADE RID: 10974 RVA: 0x0006F1DE File Offset: 0x0006D3DE
		public override bool CanPage
		{
			get
			{
				return this.EnablePaging;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object that is associated with the current <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control supports retrieving the total number of data rows, in addition to the set of data.</summary>
		/// <returns>true, if the operation is supported; otherwise, false.</returns>
		// Token: 0x17000DB3 RID: 3507
		// (get) Token: 0x06002ADF RID: 10975 RVA: 0x0006F1E6 File Offset: 0x0006D3E6
		public override bool CanRetrieveTotalRowCount
		{
			get
			{
				return this.SelectCountMethod.Length > 0 || !this.EnablePaging;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object that is associated with the current <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control supports a sorted view on the underlying data source.</summary>
		/// <returns>true.</returns>
		// Token: 0x17000DB4 RID: 3508
		// (get) Token: 0x06002AE0 RID: 10976 RVA: 0x00008B4A File Offset: 0x00006D4A
		public override bool CanSort
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object that is associated with the current <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control supports the update operation.</summary>
		/// <returns>true, if the operation is supported; otherwise, false. Updating is not supported if the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.UpdateMethod" /> property is an empty string ("").</returns>
		// Token: 0x17000DB5 RID: 3509
		// (get) Token: 0x06002AE1 RID: 10977 RVA: 0x0006F201 File Offset: 0x0006D401
		public override bool CanUpdate
		{
			get
			{
				return this.UpdateMethod.Length > 0;
			}
		}

		/// <summary>Gets or sets a value that determines how the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control performs updates and deletes when data in a row in the underlying data storage changes during the time of the operation.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.ConflictOptions" /> values. The default is the <see cref="F:System.Web.UI.ConflictOptions.OverwriteChanges" /> value.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is not one of the <see cref="T:System.Web.UI.ConflictOptions" /> values.</exception>
		// Token: 0x17000DB6 RID: 3510
		// (get) Token: 0x06002AE2 RID: 10978 RVA: 0x0006F211 File Offset: 0x0006D411
		// (set) Token: 0x06002AE3 RID: 10979 RVA: 0x0006F219 File Offset: 0x0006D419
		public ConflictOptions ConflictDetection
		{
			get
			{
				return this.conflictDetection;
			}
			set
			{
				if (this.ConflictDetection == value)
				{
					return;
				}
				this.conflictDetection = value;
				this.OnDataSourceViewChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets a value indicating whether <see cref="T:System.Web.UI.WebControls.Parameter" /> values that are passed to an update, insert, or delete operation are automatically converted from null to the <see cref="F:System.DBNull.Value" /> value.</summary>
		/// <returns>true, if null in <see cref="T:System.Web.UI.WebControls.Parameter" /> objects passed to the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> is automatically converted to the <see cref="F:System.DBNull.Value" /> value; otherwise, false. The default is false.</returns>
		// Token: 0x17000DB7 RID: 3511
		// (get) Token: 0x06002AE4 RID: 10980 RVA: 0x0006F237 File Offset: 0x0006D437
		// (set) Token: 0x06002AE5 RID: 10981 RVA: 0x0006F23F File Offset: 0x0006D43F
		public bool ConvertNullToDBNull
		{
			get
			{
				return this.convertNullToDBNull;
			}
			set
			{
				this.convertNullToDBNull = value;
			}
		}

		/// <summary>Gets or sets the name of a class that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control uses for a parameter in a data operation. The <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control uses the specified class instead of the <see cref="T:System.Web.UI.WebControls.Parameter" /> objects that are in the various parameters collections.</summary>
		/// <returns>A partially or fully qualified class name that identifies the type of the object that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> can use as a parameter for a <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Insert" />, <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Update" />, or <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Delete" /> operation. The default is an empty string ("").</returns>
		// Token: 0x17000DB8 RID: 3512
		// (get) Token: 0x06002AE6 RID: 10982 RVA: 0x0006F248 File Offset: 0x0006D448
		// (set) Token: 0x06002AE7 RID: 10983 RVA: 0x0006F25E File Offset: 0x0006D45E
		public string DataObjectTypeName
		{
			get
			{
				if (this.dataObjectTypeName == null)
				{
					return string.Empty;
				}
				return this.dataObjectTypeName;
			}
			set
			{
				if (this.DataObjectTypeName == value)
				{
					return;
				}
				this.dataObjectTypeName = value;
				this.dataObjectType = null;
				this.OnDataSourceViewChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object invokes to delete data.</summary>
		/// <returns>A string that represents the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> uses to delete data. The default is an empty string ("").</returns>
		// Token: 0x17000DB9 RID: 3513
		// (get) Token: 0x06002AE8 RID: 10984 RVA: 0x0006F288 File Offset: 0x0006D488
		// (set) Token: 0x06002AE9 RID: 10985 RVA: 0x0006F29E File Offset: 0x0006D49E
		public string DeleteMethod
		{
			get
			{
				if (this.deleteMethod == null)
				{
					return string.Empty;
				}
				return this.deleteMethod;
			}
			set
			{
				this.deleteMethod = value;
			}
		}

		/// <summary>Gets the parameters collection that contains the parameters that are used by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.DeleteMethod" /> method.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains the parameters used by the method specified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.DeleteMethod" /> property.</returns>
		// Token: 0x17000DBA RID: 3514
		// (get) Token: 0x06002AEA RID: 10986 RVA: 0x0006F2A7 File Offset: 0x0006D4A7
		public ParameterCollection DeleteParameters
		{
			get
			{
				if (this.deleteParameters == null)
				{
					this.deleteParameters = new ParameterCollection();
				}
				return this.deleteParameters;
			}
		}

		/// <summary>Gets or sets a value indicating whether the data source control supports paging through the set of data that it retrieves.</summary>
		/// <returns>true, if the data source control supports paging through the data it retrieves; otherwise, false.</returns>
		// Token: 0x17000DBB RID: 3515
		// (get) Token: 0x06002AEB RID: 10987 RVA: 0x0006F2C2 File Offset: 0x0006D4C2
		// (set) Token: 0x06002AEC RID: 10988 RVA: 0x0006F2CA File Offset: 0x0006D4CA
		public bool EnablePaging
		{
			get
			{
				return this.enablePaging;
			}
			set
			{
				if (this.EnablePaging == value)
				{
					return;
				}
				this.enablePaging = value;
				this.OnDataSourceViewChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets a filtering expression that is applied when the business object method that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.SelectMethod" /> property is called.</summary>
		/// <returns>A string that represents a filtering expression applied when data is retrieved using the business object method identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.SelectMethod" /> property.</returns>
		/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.FilterExpression" /> property was set and the <see cref="M:System.Web.UI.WebControls.ObjectDataSourceView.Select(System.Web.UI.DataSourceSelectArguments)" /> method does not return a <see cref="T:System.Data.DataSet" />. </exception>
		// Token: 0x17000DBC RID: 3516
		// (get) Token: 0x06002AED RID: 10989 RVA: 0x0006F2E8 File Offset: 0x0006D4E8
		// (set) Token: 0x06002AEE RID: 10990 RVA: 0x0006F2FE File Offset: 0x0006D4FE
		public string FilterExpression
		{
			get
			{
				if (this.filterExpression == null)
				{
					return string.Empty;
				}
				return this.filterExpression;
			}
			set
			{
				if (this.FilterExpression == value)
				{
					return;
				}
				this.filterExpression = value;
				this.OnDataSourceViewChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets a collection of parameters that are associated with any parameter placeholders that are in the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.FilterExpression" /> string.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains a set of parameters associated with any parameter placeholders found in the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.FilterExpression" /> property.</returns>
		/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.FilterExpression" /> property was set and the <see cref="M:System.Web.UI.WebControls.ObjectDataSourceView.Select(System.Web.UI.DataSourceSelectArguments)" /> method does not return a <see cref="T:System.Data.DataSet" />. </exception>
		// Token: 0x17000DBD RID: 3517
		// (get) Token: 0x06002AEF RID: 10991 RVA: 0x0006F324 File Offset: 0x0006D524
		public ParameterCollection FilterParameters
		{
			get
			{
				if (this.filterParameters == null)
				{
					this.filterParameters = new ParameterCollection();
					this.filterParameters.ParametersChanged += this.OnParametersChanged;
					if (this.IsTrackingViewState)
					{
						((IStateManager)this.filterParameters).TrackViewState();
					}
				}
				return this.filterParameters;
			}
		}

		/// <summary>Gets or sets the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object invokes to insert data.</summary>
		/// <returns>A string that represents the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> uses to insert data. The default value is an empty string ("").</returns>
		// Token: 0x17000DBE RID: 3518
		// (get) Token: 0x06002AF0 RID: 10992 RVA: 0x0006F374 File Offset: 0x0006D574
		// (set) Token: 0x06002AF1 RID: 10993 RVA: 0x0006F38A File Offset: 0x0006D58A
		public string InsertMethod
		{
			get
			{
				if (this.insertMethod == null)
				{
					return string.Empty;
				}
				return this.insertMethod;
			}
			set
			{
				this.insertMethod = value;
			}
		}

		/// <summary>Gets the parameters collection that contains the parameters that are used by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.InsertMethod" /> method.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains the parameters used by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.InsertMethod" /> property.</returns>
		// Token: 0x17000DBF RID: 3519
		// (get) Token: 0x06002AF2 RID: 10994 RVA: 0x0006F393 File Offset: 0x0006D593
		public ParameterCollection InsertParameters
		{
			get
			{
				if (this.insertParameters == null)
				{
					this.insertParameters = new ParameterCollection();
				}
				return this.insertParameters;
			}
		}

		/// <summary>Gets or sets the name of the data retrieval method parameter that is used to indicate the number of records to retrieve for data source paging support.</summary>
		/// <returns>The name of the method parameter used to indicate the number of records to retrieve. The default is "maximumRows".</returns>
		// Token: 0x17000DC0 RID: 3520
		// (get) Token: 0x06002AF3 RID: 10995 RVA: 0x0006F3AE File Offset: 0x0006D5AE
		// (set) Token: 0x06002AF4 RID: 10996 RVA: 0x0006F3C4 File Offset: 0x0006D5C4
		public string MaximumRowsParameterName
		{
			get
			{
				if (this.maximumRowsParameterName == null)
				{
					return "maximumRows";
				}
				return this.maximumRowsParameterName;
			}
			set
			{
				if (this.MaximumRowsParameterName == value)
				{
					return;
				}
				this.maximumRowsParameterName = value;
				this.OnDataSourceViewChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets a format string to apply to the names of the parameters for original values that are passed to the Delete or Update methods.</summary>
		/// <returns>A string that represents a format string applied to the names of any <paramref name="oldValues" /> passed to the <see cref="Overload:System.Web.UI.WebControls.ObjectDataSourceView.Delete" /> or <see cref="Overload:System.Web.UI.WebControls.ObjectDataSourceView.Update" /> method. The default is "{0}", which means the parameter name is simply the field name.</returns>
		// Token: 0x17000DC1 RID: 3521
		// (get) Token: 0x06002AF5 RID: 10997 RVA: 0x0006F3E7 File Offset: 0x0006D5E7
		// (set) Token: 0x06002AF6 RID: 10998 RVA: 0x0006F3FD File Offset: 0x0006D5FD
		[DefaultValue("{0}")]
		public string OldValuesParameterFormatString
		{
			get
			{
				if (this.oldValuesParameterFormatString == null)
				{
					return "{0}";
				}
				return this.oldValuesParameterFormatString;
			}
			set
			{
				if (this.OldValuesParameterFormatString == value)
				{
					return;
				}
				this.oldValuesParameterFormatString = value;
				this.OnDataSourceViewChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> control invokes to retrieve a row count.</summary>
		/// <returns>A string that represents the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> uses to retrieve a row count. The default is an empty string (""). </returns>
		// Token: 0x17000DC2 RID: 3522
		// (get) Token: 0x06002AF7 RID: 10999 RVA: 0x0006F420 File Offset: 0x0006D620
		// (set) Token: 0x06002AF8 RID: 11000 RVA: 0x0006F436 File Offset: 0x0006D636
		public string SelectCountMethod
		{
			get
			{
				if (this.selectCountMethod == null)
				{
					return string.Empty;
				}
				return this.selectCountMethod;
			}
			set
			{
				if (this.SelectCountMethod == value)
				{
					return;
				}
				this.selectCountMethod = value;
				this.OnDataSourceViewChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> control invokes to retrieve data.</summary>
		/// <returns>A string that represents the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> uses to retrieve data. The default is an empty string ("").</returns>
		// Token: 0x17000DC3 RID: 3523
		// (get) Token: 0x06002AF9 RID: 11001 RVA: 0x0006F459 File Offset: 0x0006D659
		// (set) Token: 0x06002AFA RID: 11002 RVA: 0x0006F46F File Offset: 0x0006D66F
		public string SelectMethod
		{
			get
			{
				if (this.selectMethod == null)
				{
					return string.Empty;
				}
				return this.selectMethod;
			}
			set
			{
				if (this.SelectMethod == value)
				{
					return;
				}
				this.selectMethod = value;
				this.OnDataSourceViewChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets the parameters collection containing the parameters that are used by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.SelectMethod" /> method.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains the parameters used by the method specified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSource.SelectMethod" /> property.</returns>
		// Token: 0x17000DC4 RID: 3524
		// (get) Token: 0x06002AFB RID: 11003 RVA: 0x0006F494 File Offset: 0x0006D694
		public ParameterCollection SelectParameters
		{
			get
			{
				if (this.selectParameters == null)
				{
					this.selectParameters = new ParameterCollection();
					this.selectParameters.ParametersChanged += this.OnParametersChanged;
					if (this.IsTrackingViewState)
					{
						((IStateManager)this.selectParameters).TrackViewState();
					}
				}
				return this.selectParameters;
			}
		}

		/// <summary>Gets or sets the name of the data retrieval method parameter that is used to specify a sort expression for data source sorting support.</summary>
		/// <returns>The name of the method parameter used to indicate the parameter that accepts this sort expression value. The default is an empty string ("").</returns>
		// Token: 0x17000DC5 RID: 3525
		// (get) Token: 0x06002AFC RID: 11004 RVA: 0x0006F4E4 File Offset: 0x0006D6E4
		// (set) Token: 0x06002AFD RID: 11005 RVA: 0x0006F4FA File Offset: 0x0006D6FA
		public string SortParameterName
		{
			get
			{
				if (this.sortParameterName == null)
				{
					return string.Empty;
				}
				return this.sortParameterName;
			}
			set
			{
				this.sortParameterName = value;
			}
		}

		/// <summary>Gets or sets the name of the data retrieval method parameter that is used to indicate the integer index of the first record to retrieve from the results set for data source paging support.</summary>
		/// <returns>The name of the business object method parameter used to indicate the first record to retrieve. The default is "startRowIndex".</returns>
		// Token: 0x17000DC6 RID: 3526
		// (get) Token: 0x06002AFE RID: 11006 RVA: 0x0006F503 File Offset: 0x0006D703
		// (set) Token: 0x06002AFF RID: 11007 RVA: 0x0006F519 File Offset: 0x0006D719
		public string StartRowIndexParameterName
		{
			get
			{
				if (this.startRowIndexParameterName == null)
				{
					return "startRowIndex";
				}
				return this.startRowIndexParameterName;
			}
			set
			{
				if (this.StartRowIndexParameterName == value)
				{
					return;
				}
				this.startRowIndexParameterName = value;
				this.OnDataSourceViewChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets the name of the class that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> control represents.</summary>
		/// <returns>A partially or fully qualified class name that identifies the type of the object that the <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> represents. The default is an empty string.</returns>
		// Token: 0x17000DC7 RID: 3527
		// (get) Token: 0x06002B00 RID: 11008 RVA: 0x0006F53C File Offset: 0x0006D73C
		// (set) Token: 0x06002B01 RID: 11009 RVA: 0x0006F552 File Offset: 0x0006D752
		public string TypeName
		{
			get
			{
				if (this.typeName == null)
				{
					return string.Empty;
				}
				return this.typeName;
			}
			set
			{
				if (this.TypeName == value)
				{
					return;
				}
				this.typeName = value;
				this.objectType = null;
				this.OnDataSourceViewChanged(EventArgs.Empty);
			}
		}

		/// <summary>Gets or sets the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object invokes to update data.</summary>
		/// <returns>A string that represents the name of the method or function that the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> uses to update data. The default is an empty string ("").</returns>
		// Token: 0x17000DC8 RID: 3528
		// (get) Token: 0x06002B02 RID: 11010 RVA: 0x0006F57C File Offset: 0x0006D77C
		// (set) Token: 0x06002B03 RID: 11011 RVA: 0x0006F592 File Offset: 0x0006D792
		public string UpdateMethod
		{
			get
			{
				if (this.updateMethod == null)
				{
					return string.Empty;
				}
				return this.updateMethod;
			}
			set
			{
				this.updateMethod = value;
			}
		}

		/// <summary>Gets the parameters collection containing the parameters that are used by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.UpdateMethod" /> method.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.ParameterCollection" /> that contains the parameters used by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.UpdateMethod" /> property.</returns>
		// Token: 0x17000DC9 RID: 3529
		// (get) Token: 0x06002B04 RID: 11012 RVA: 0x0006F59B File Offset: 0x0006D79B
		public ParameterCollection UpdateParameters
		{
			get
			{
				if (this.updateParameters == null)
				{
					this.updateParameters = new ParameterCollection();
				}
				return this.updateParameters;
			}
		}

		// Token: 0x17000DCA RID: 3530
		// (get) Token: 0x06002B05 RID: 11013 RVA: 0x0006F5B8 File Offset: 0x0006D7B8
		private Type ObjectType
		{
			get
			{
				if (this.objectType == null)
				{
					this.objectType = HttpApplication.LoadType(this.TypeName);
					if (this.objectType == null)
					{
						throw new InvalidOperationException("Type not found: " + this.TypeName);
					}
				}
				return this.objectType;
			}
		}

		// Token: 0x17000DCB RID: 3531
		// (get) Token: 0x06002B06 RID: 11014 RVA: 0x0006F610 File Offset: 0x0006D810
		private Type DataObjectType
		{
			get
			{
				if (this.dataObjectType == null)
				{
					this.dataObjectType = HttpApplication.LoadType(this.DataObjectTypeName);
					if (this.dataObjectType == null)
					{
						throw new InvalidOperationException("Type not found: " + this.DataObjectTypeName);
					}
				}
				return this.dataObjectType;
			}
		}

		/// <summary>Retrieves data from the object that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.TypeName" /> property by calling the method that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.SelectMethod" /> property and passing any values in the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.SelectParameters" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerable" /> list of data rows. For more information, see <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.SelectMethod" />.</returns>
		/// <param name="arguments">A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> used to request operations on the data beyond basic data retrieval.</param>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="arguments" /> passed to the <see cref="M:System.Web.UI.WebControls.ObjectDataSourceView.Select(System.Web.UI.DataSourceSelectArguments)" /> method specify that the data source should perform some additional work while retrieving data to enable paging or sorting through the retrieved data, but the data source control does not support the requested capability.</exception>
		// Token: 0x06002B07 RID: 11015 RVA: 0x0006F666 File Offset: 0x0006D866
		public IEnumerable Select(DataSourceSelectArguments arguments)
		{
			return this.ExecuteSelect(arguments);
		}

		/// <summary>Performs an update operation by calling the method that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.UpdateMethod" /> property and using any parameters that are supplied in the <paramref name="keys" />, <paramref name="values" />, or <paramref name="oldValues" /> collections.</summary>
		/// <returns>The number of rows updated; otherwise, -1, if the number is not known.</returns>
		/// <param name="keys">A <see cref="T:System.Collections.IDictionary" /> of the key values used to identify the item to update. These parameters are used with the method specified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.UpdateMethod" /> property to perform the update operation. If there are no parameters associated with the method, pass null.</param>
		/// <param name="values">A <see cref="T:System.Collections.IDictionary" /> of new values to apply to the data source. These parameters are used with the method specified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.UpdateMethod" /> property to perform the update database operation. If there are no parameters associated with the method, pass null. </param>
		/// <param name="oldValues">A <see cref="T:System.Collections.IDictionary" /> that contains the additional non-key values used to match the item in the data source. Row values are passed to the delete method, only if the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.ConflictDetection" /> property is set to the <see cref="F:System.Web.UI.ConflictOptions.CompareAllValues" /> field.</param>
		// Token: 0x06002B08 RID: 11016 RVA: 0x0006F66F File Offset: 0x0006D86F
		public int Update(IDictionary keys, IDictionary values, IDictionary oldValues)
		{
			return this.ExecuteUpdate(keys, values, oldValues);
		}

		/// <summary>Performs a delete operation by calling the business object method that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.DeleteMethod" /> property using the specified <paramref name="keys" /> and <paramref name="oldValues" /> collections.</summary>
		/// <returns>The number of rows deleted; otherwise, -1, if the number is not known.</returns>
		/// <param name="keys">A <see cref="T:System.Collections.IDictionary" /> of the key values used to identify the item to delete. These parameters are used with the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.DeleteMethod" /> property to perform the delete operation. If there are no parameters associated with the method, pass null.</param>
		/// <param name="oldValues">A <see cref="T:System.Collections.IDictionary" /> that contains the additional non-key values used to match the item in the data source. Row values are passed to the method only if the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.ConflictDetection" /> property is set to the <see cref="F:System.Web.UI.ConflictOptions.CompareAllValues" /> field.</param>
		// Token: 0x06002B09 RID: 11017 RVA: 0x0006F67A File Offset: 0x0006D87A
		public int Delete(IDictionary keys, IDictionary oldValues)
		{
			return this.ExecuteDelete(keys, oldValues);
		}

		/// <summary>Performs an insert operation by calling the business object method that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.InsertMethod" /> property using the specified <paramref name="values" /> collection.</summary>
		/// <returns>The number of rows inserted; otherwise, -1, if the number is not known.</returns>
		/// <param name="values">A <see cref="T:System.Collections.IDictionary" /> collection of parameters used with the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.InsertMethod" /> property to perform the insert operation. If there are no parameters associated with the method, pass null.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.CanInsert" /> property returns false.</exception>
		// Token: 0x06002B0A RID: 11018 RVA: 0x0006F684 File Offset: 0x0006D884
		public int Insert(IDictionary values)
		{
			return this.ExecuteInsert(values);
		}

		/// <summary>Performs an insert operation by calling the business object method that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.InsertMethod" /> property using the specified <paramref name="values" /> collection.</summary>
		/// <returns>The number of rows inserted; otherwise, -1, if the number is not known. For more information, see <see cref="Overload:System.Web.UI.WebControls.ObjectDataSourceView.Insert" />.</returns>
		/// <param name="values">A <see cref="T:System.Collections.IDictionary" /> of parameters used with the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.InsertMethod" /> property to perform the insert operation. If there are no parameters associated with the method, pass null.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.CanInsert" /> property returns false.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="values" /> is null or empty.</exception>
		// Token: 0x06002B0B RID: 11019 RVA: 0x0006F690 File Offset: 0x0006D890
		protected override int ExecuteInsert(IDictionary values)
		{
			if (!this.CanInsert)
			{
				throw new NotSupportedException("Insert operation not supported.");
			}
			IOrderedDictionary orderedDictionary;
			MethodInfo methodInfo;
			if (this.DataObjectTypeName.Length == 0)
			{
				orderedDictionary = this.MergeParameterValues(this.InsertParameters, values, null);
				methodInfo = this.GetObjectMethod(this.InsertMethod, orderedDictionary, DataObjectMethodType.Insert);
			}
			else
			{
				methodInfo = this.ResolveDataObjectMethod(this.InsertMethod, values, null, out orderedDictionary);
			}
			ObjectDataSourceMethodEventArgs e = new ObjectDataSourceMethodEventArgs(orderedDictionary);
			this.OnInserting(e);
			if (e.Cancel)
			{
				return -1;
			}
			ObjectDataSourceStatusEventArgs e2 = this.InvokeMethod(methodInfo, orderedDictionary);
			this.OnInserted(e2);
			if (e2.Exception != null && !e2.ExceptionHandled)
			{
				throw e2.Exception;
			}
			if (this.owner.EnableCaching)
			{
				this.owner.Cache.Expire();
			}
			this.OnDataSourceViewChanged(EventArgs.Empty);
			return -1;
		}

		/// <summary>Performs a delete operation using the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.DeleteMethod" /> method and the specified <paramref name="keys" /> and <paramref name="oldValues" /> collection.</summary>
		/// <returns>The number of rows deleted; otherwise, -1, if the number is not known. For more information, see <see cref="Overload:System.Web.UI.WebControls.ObjectDataSourceView.Delete" />.</returns>
		/// <param name="keys">A <see cref="T:System.Collections.IDictionary" /> of parameters used with the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.DeleteMethod" /> property to perform the delete operation. If there are no parameters associated with the method, pass null.</param>
		/// <param name="oldValues">A <see cref="T:System.Collections.IDictionary" /> that contains row values that are evaluated, only if the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.ConflictDetection" /> property is set to the  <see cref="F:System.Web.UI.ConflictOptions.CompareAllValues" /> field.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.CanDelete" /> property returns false.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.ConflictDetection" /> property is set to the <see cref="F:System.Web.UI.ConflictOptions.CompareAllValues" /> value, and no values are passed in the <paramref name="oldValues" /> collection.</exception>
		// Token: 0x06002B0C RID: 11020 RVA: 0x0006F758 File Offset: 0x0006D958
		protected override int ExecuteDelete(IDictionary keys, IDictionary oldValues)
		{
			if (!this.CanDelete)
			{
				throw new NotSupportedException("Delete operation not supported.");
			}
			if (this.ConflictDetection == ConflictOptions.CompareAllValues && (oldValues == null || oldValues.Count == 0))
			{
				throw new InvalidOperationException("ConflictDetection is set to CompareAllValues and oldValues collection is null or empty.");
			}
			IDictionary dictionary = this.BuildOldValuesList(keys, oldValues, false);
			IOrderedDictionary orderedDictionary;
			MethodInfo methodInfo;
			if (this.DataObjectTypeName.Length == 0)
			{
				orderedDictionary = this.MergeParameterValues(this.DeleteParameters, null, dictionary);
				methodInfo = this.GetObjectMethod(this.DeleteMethod, orderedDictionary, DataObjectMethodType.Delete);
			}
			else
			{
				methodInfo = this.ResolveDataObjectMethod(this.DeleteMethod, dictionary, null, out orderedDictionary);
			}
			ObjectDataSourceMethodEventArgs e = new ObjectDataSourceMethodEventArgs(orderedDictionary);
			this.OnDeleting(e);
			if (e.Cancel)
			{
				return -1;
			}
			ObjectDataSourceStatusEventArgs e2 = this.InvokeMethod(methodInfo, orderedDictionary);
			this.OnDeleted(e2);
			if (e2.Exception != null && !e2.ExceptionHandled)
			{
				throw e2.Exception;
			}
			if (this.owner.EnableCaching)
			{
				this.owner.Cache.Expire();
			}
			this.OnDataSourceViewChanged(EventArgs.Empty);
			return -1;
		}

		/// <summary>Performs an update operation by calling the method that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.UpdateMethod" /> property and using any parameters that are supplied in the <paramref name="keys" />, <paramref name="values" />, or <paramref name="oldValues" /> collections.</summary>
		/// <returns>The number of rows updated; or -1, if the number is not known. For more information, see <see cref="M:System.Web.UI.WebControls.ObjectDataSource.Update" />.</returns>
		/// <param name="keys">A <see cref="T:System.Collections.IDictionary" /> of primary keys to use with the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.UpdateMethod" /> property to perform the update database operation. If there are no keys associated with the method, pass null.</param>
		/// <param name="values">A <see cref="T:System.Collections.IDictionary" /> of values to be used with the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.UpdateMethod" /> to perform the update database operation. If there are no parameters associated with the method, pass null. </param>
		/// <param name="oldValues">A <see cref="T:System.Collections.IDictionary" /> that represents the original values in the underlying data store. If there are no parameters associated with the query, pass null.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.CanInsert" /> property returns false.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="oldValues" /> is null or empty and <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.ConflictDetection" /> is set to <see cref="F:System.Web.UI.ConflictOptions.CompareAllValues" />.</exception>
		// Token: 0x06002B0D RID: 11021 RVA: 0x0006F84C File Offset: 0x0006DA4C
		protected override int ExecuteUpdate(IDictionary keys, IDictionary values, IDictionary oldValues)
		{
			IDictionary dictionary = this.BuildOldValuesList(keys, oldValues, true);
			IOrderedDictionary orderedDictionary;
			MethodInfo methodInfo;
			if (this.DataObjectTypeName.Length == 0)
			{
				orderedDictionary = this.MergeParameterValues(this.UpdateParameters, values, dictionary);
				methodInfo = this.GetObjectMethod(this.UpdateMethod, orderedDictionary, DataObjectMethodType.Update);
			}
			else
			{
				if (this.ConflictDetection != ConflictOptions.CompareAllValues)
				{
					dictionary = null;
				}
				IDictionary dictionary2 = new Hashtable();
				if (keys != null)
				{
					foreach (object obj in keys)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						dictionary2[dictionaryEntry.Key] = dictionaryEntry.Value;
					}
				}
				if (values != null)
				{
					foreach (object obj2 in values)
					{
						DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
						dictionary2[dictionaryEntry2.Key] = dictionaryEntry2.Value;
					}
				}
				methodInfo = this.ResolveDataObjectMethod(this.UpdateMethod, dictionary2, dictionary, out orderedDictionary);
			}
			ObjectDataSourceMethodEventArgs e = new ObjectDataSourceMethodEventArgs(orderedDictionary);
			this.OnUpdating(e);
			if (e.Cancel)
			{
				return -1;
			}
			ObjectDataSourceStatusEventArgs e2 = this.InvokeMethod(methodInfo, orderedDictionary);
			this.OnUpdated(e2);
			if (e2.Exception != null && !e2.ExceptionHandled)
			{
				throw e2.Exception;
			}
			if (this.owner.EnableCaching)
			{
				this.owner.Cache.Expire();
			}
			this.OnDataSourceViewChanged(EventArgs.Empty);
			return -1;
		}

		// Token: 0x06002B0E RID: 11022 RVA: 0x0006F9E4 File Offset: 0x0006DBE4
		private IDictionary BuildOldValuesList(IDictionary keys, IDictionary oldValues, bool keysWin)
		{
			IDictionary dictionary;
			if (this.ConflictDetection == ConflictOptions.CompareAllValues)
			{
				dictionary = new Hashtable();
				if (keys != null && !keysWin)
				{
					foreach (object obj in keys)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						dictionary[dictionaryEntry.Key] = dictionaryEntry.Value;
					}
				}
				if (oldValues != null)
				{
					foreach (object obj2 in oldValues)
					{
						DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
						dictionary[dictionaryEntry2.Key] = dictionaryEntry2.Value;
					}
				}
				if (keys == null || !keysWin)
				{
					return dictionary;
				}
				using (IDictionaryEnumerator dictionaryEnumerator = keys.GetEnumerator())
				{
					while (dictionaryEnumerator.MoveNext())
					{
						object obj3 = dictionaryEnumerator.Current;
						DictionaryEntry dictionaryEntry3 = (DictionaryEntry)obj3;
						dictionary[dictionaryEntry3.Key] = dictionaryEntry3.Value;
					}
					return dictionary;
				}
			}
			dictionary = keys;
			return dictionary;
		}

		/// <summary>Retrieves data from the object that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.TypeName" /> property by calling the method that is identified by the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.SelectMethod" /> property and passing any values in the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.SelectParameters" /> collection.</summary>
		/// <returns>A <see cref="T:System.Collections.IEnumerable" /> list of data rows.</returns>
		/// <param name="arguments">A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> used to request operations on the data beyond basic data retrieval.</param>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="arguments" /> passed to the <see cref="M:System.Web.UI.WebControls.ObjectDataSourceView.ExecuteSelect(System.Web.UI.DataSourceSelectArguments)" /> method specify that the data source should perform some additional work while retrieving data to enable paging or sorting through the retrieved data, but the data source control does not support the requested capability.- or -The object returned by the <see cref="M:System.Web.UI.WebControls.ObjectDataSourceView.ExecuteSelect(System.Web.UI.DataSourceSelectArguments)" /> method is not a <see cref="T:System.Data.DataSet" /> or <see cref="T:System.Data.DataTable" />, and caching is enabled. Only <see cref="T:System.Data.DataSet" /> and <see cref="T:System.Data.DataTable" /> objects can be cached for the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> control.- or -Both caching and client impersonation are enabled. The <see cref="T:System.Web.UI.WebControls.ObjectDataSource" /> does not support caching when client impersonation is enabled.</exception>
		/// <exception cref="T:System.InvalidOperationException">The object returned by the <see cref="M:System.Web.UI.WebControls.ObjectDataSourceView.ExecuteSelect(System.Web.UI.DataSourceSelectArguments)" /> method is a <see cref="T:System.Data.DataSet" />, but has no tables in its <see cref="P:System.Data.DataSet.Tables" /> collection.- or - The <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.EnablePaging" /> property is set to true, but the <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.StartRowIndexParameterName" /> and <see cref="P:System.Web.UI.WebControls.ObjectDataSourceView.MaximumRowsParameterName" /> properties are not set.</exception>
		// Token: 0x06002B0F RID: 11023 RVA: 0x0006FB10 File Offset: 0x0006DD10
		protected internal override IEnumerable ExecuteSelect(DataSourceSelectArguments arguments)
		{
			arguments.RaiseUnsupportedCapabilitiesError(this);
			IOrderedDictionary orderedDictionary = this.MergeParameterValues(this.SelectParameters, null, null);
			ObjectDataSourceSelectingEventArgs e = new ObjectDataSourceSelectingEventArgs(orderedDictionary, arguments, false);
			object obj = null;
			if (this.owner.EnableCaching)
			{
				obj = this.owner.Cache.GetCachedObject(this.SelectMethod, this.SelectParameters);
			}
			if (obj == null)
			{
				this.OnSelecting(e);
				if (e.Cancel)
				{
					return new ArrayList();
				}
				if (this.CanPage)
				{
					if (this.StartRowIndexParameterName.Length == 0)
					{
						throw new InvalidOperationException("Paging is enabled, but the StartRowIndexParameterName property is not set.");
					}
					if (this.MaximumRowsParameterName.Length == 0)
					{
						throw new InvalidOperationException("Paging is enabled, but the MaximumRowsParameterName property is not set.");
					}
					orderedDictionary[this.StartRowIndexParameterName] = arguments.StartRowIndex;
					orderedDictionary[this.MaximumRowsParameterName] = arguments.MaximumRows;
				}
				if (this.SortParameterName.Length > 0)
				{
					orderedDictionary[this.SortParameterName] = arguments.SortExpression;
				}
				obj = this.InvokeSelect(this.SelectMethod, orderedDictionary);
				if (this.CanRetrieveTotalRowCount && arguments.RetrieveTotalRowCount)
				{
					arguments.TotalRowCount = this.QueryTotalRowCount(this.MergeParameterValues(this.SelectParameters, null, null), arguments);
				}
				if (this.owner.EnableCaching)
				{
					this.owner.Cache.SetCachedObject(this.SelectMethod, this.SelectParameters, obj);
				}
			}
			if (this.FilterExpression.Length > 0 && !(obj is DataGrid) && !(obj is DataView) && !(obj is DataTable))
			{
				throw new NotSupportedException("The FilterExpression property was set and the Select method does not return a DataSet, DataTable, or DataView.");
			}
			if (this.owner.EnableCaching && obj is IDataReader)
			{
				throw new NotSupportedException("Data source does not support caching objects that implement IDataReader");
			}
			if (obj is DataSet)
			{
				DataSet dataSet = (DataSet)obj;
				if (dataSet.Tables.Count == 0)
				{
					throw new InvalidOperationException("The select method returnet a DataSet which doesn't contain any table.");
				}
				obj = dataSet.Tables[0];
			}
			if (obj is DataTable)
			{
				DataView dataView = new DataView((DataTable)obj);
				if (arguments.SortExpression != null && arguments.SortExpression.Length > 0)
				{
					dataView.Sort = arguments.SortExpression;
				}
				if (this.FilterExpression.Length > 0)
				{
					IOrderedDictionary values = this.FilterParameters.GetValues(this.context, this.owner);
					ObjectDataSourceFilteringEventArgs e2 = new ObjectDataSourceFilteringEventArgs(values);
					this.OnFiltering(e2);
					if (!e2.Cancel)
					{
						object[] array = new object[values.Count];
						for (int i = 0; i < array.Length; i++)
						{
							array[i] = values[i];
							if (array[i] == null)
							{
								return dataView;
							}
						}
						dataView.RowFilter = string.Format(this.FilterExpression, array);
					}
				}
				return dataView;
			}
			if (obj is IEnumerable)
			{
				return (IEnumerable)obj;
			}
			return new object[] { obj };
		}

		// Token: 0x06002B10 RID: 11024 RVA: 0x0006FDD0 File Offset: 0x0006DFD0
		private int QueryTotalRowCount(IOrderedDictionary mergedParameters, DataSourceSelectArguments arguments)
		{
			ObjectDataSourceSelectingEventArgs e = new ObjectDataSourceSelectingEventArgs(mergedParameters, arguments, true);
			this.OnSelecting(e);
			if (e.Cancel)
			{
				return 0;
			}
			return (int)Convert.ChangeType(this.InvokeSelect(this.SelectCountMethod, mergedParameters), typeof(int));
		}

		// Token: 0x06002B11 RID: 11025 RVA: 0x0006FE18 File Offset: 0x0006E018
		private object InvokeSelect(string methodName, IOrderedDictionary paramValues)
		{
			MethodInfo objectMethod = this.GetObjectMethod(methodName, paramValues, DataObjectMethodType.Select);
			ObjectDataSourceStatusEventArgs e = this.InvokeMethod(objectMethod, paramValues);
			this.OnSelected(e);
			if (e.Exception != null && !e.ExceptionHandled)
			{
				throw e.Exception;
			}
			return e.ReturnValue;
		}

		// Token: 0x06002B12 RID: 11026 RVA: 0x0006FE5C File Offset: 0x0006E05C
		private ObjectDataSourceStatusEventArgs InvokeMethod(MethodInfo method, IOrderedDictionary paramValues)
		{
			object obj = null;
			if (!method.IsStatic)
			{
				obj = this.CreateObjectInstance();
			}
			ParameterInfo[] parameters = method.GetParameters();
			ArrayList arrayList;
			object[] parameterArray = this.GetParameterArray(parameters, paramValues, out arrayList);
			if (parameterArray == null)
			{
				throw this.CreateMethodException(method.Name, paramValues);
			}
			object obj2 = null;
			Hashtable hashtable = null;
			ObjectDataSourceStatusEventArgs e;
			try
			{
				obj2 = method.Invoke(obj, parameterArray);
				if (arrayList != null)
				{
					hashtable = new Hashtable();
					foreach (object obj3 in arrayList)
					{
						ParameterInfo parameterInfo = (ParameterInfo)obj3;
						hashtable[parameterInfo.Name] = parameterArray[parameterInfo.Position];
					}
				}
				e = new ObjectDataSourceStatusEventArgs(obj2, hashtable, null);
			}
			catch (Exception ex)
			{
				e = new ObjectDataSourceStatusEventArgs(obj2, hashtable, ex);
			}
			finally
			{
				if (obj != null)
				{
					this.DisposeObjectInstance(obj);
				}
			}
			return e;
		}

		// Token: 0x06002B13 RID: 11027 RVA: 0x0006FF5C File Offset: 0x0006E15C
		private MethodInfo GetObjectMethod(string methodName, IOrderedDictionary parameters, DataObjectMethodType methodType)
		{
			MemberInfo[] member = this.ObjectType.GetMember(methodName, MemberTypes.Method, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
			if (member.Length > 1)
			{
				DataObjectMethodAttribute dataObjectMethodAttribute = null;
				MethodInfo methodInfo = null;
				bool flag = false;
				foreach (MethodInfo methodInfo2 in member)
				{
					ParameterInfo[] parameters2 = methodInfo2.GetParameters();
					if (parameters2.Length == parameters.Count)
					{
						object[] customAttributes = methodInfo2.GetCustomAttributes(typeof(DataObjectMethodAttribute), true);
						DataObjectMethodAttribute dataObjectMethodAttribute2 = ((customAttributes != null && customAttributes.Length != 0) ? ((DataObjectMethodAttribute)customAttributes[0]) : null);
						if (dataObjectMethodAttribute2 == null || dataObjectMethodAttribute2.MethodType == methodType)
						{
							bool flag2 = true;
							foreach (ParameterInfo parameterInfo in parameters2)
							{
								if (!parameters.Contains(parameterInfo.Name))
								{
									flag2 = false;
									break;
								}
							}
							if (flag2)
							{
								if (dataObjectMethodAttribute2 != null)
								{
									if (dataObjectMethodAttribute != null)
									{
										if (dataObjectMethodAttribute.IsDefault)
										{
											if (dataObjectMethodAttribute2.IsDefault)
											{
												methodInfo = null;
												break;
											}
											goto IL_0100;
										}
										else
										{
											methodInfo = null;
											flag = !dataObjectMethodAttribute2.IsDefault;
										}
									}
									else
									{
										methodInfo = null;
									}
								}
								if (methodInfo == null)
								{
									dataObjectMethodAttribute = dataObjectMethodAttribute2;
									methodInfo = methodInfo2;
								}
								else
								{
									flag = true;
								}
							}
						}
					}
					IL_0100:;
				}
				if (!flag && methodInfo != null)
				{
					return methodInfo;
				}
			}
			else if (member.Length == 1)
			{
				MethodInfo methodInfo3 = member[0] as MethodInfo;
				if (methodInfo3 != null && methodInfo3.GetParameters().Length == parameters.Count)
				{
					return methodInfo3;
				}
			}
			throw this.CreateMethodException(methodName, parameters);
		}

		// Token: 0x06002B14 RID: 11028 RVA: 0x000700C0 File Offset: 0x0006E2C0
		private MethodInfo ResolveDataObjectMethod(string methodName, IDictionary values, IDictionary oldValues, out IOrderedDictionary paramValues)
		{
			MethodInfo methodInfo;
			if (oldValues != null)
			{
				methodInfo = this.ObjectType.GetMethod(methodName, new Type[] { this.DataObjectType, this.DataObjectType });
			}
			else
			{
				methodInfo = this.ObjectType.GetMethod(methodName, new Type[] { this.DataObjectType });
			}
			if (methodInfo == null)
			{
				string[] array = new string[9];
				array[0] = "ObjectDataSource ";
				array[1] = this.owner.ID;
				array[2] = " could not find a method named '";
				array[3] = methodName;
				array[4] = "' with parameters of type '";
				int num = 5;
				Type type = this.DataObjectType;
				array[num] = ((type != null) ? type.ToString() : null);
				array[6] = "' in '";
				int num2 = 7;
				Type type2 = this.ObjectType;
				array[num2] = ((type2 != null) ? type2.ToString() : null);
				array[8] = "'.";
				throw new InvalidOperationException(string.Concat(array));
			}
			paramValues = new OrderedDictionary(StringComparer.InvariantCultureIgnoreCase);
			ParameterInfo[] parameters = methodInfo.GetParameters();
			if (oldValues != null)
			{
				if (this.FormatOldParameter(parameters[0].Name) == parameters[1].Name)
				{
					paramValues[parameters[0].Name] = this.CreateDataObject(values);
					paramValues[parameters[1].Name] = this.CreateDataObject(oldValues);
				}
				else
				{
					if (!(this.FormatOldParameter(parameters[1].Name) == parameters[0].Name))
					{
						throw new InvalidOperationException("Method '" + methodName + "' does not have any parameter that fits the value of OldValuesParameterFormatString.");
					}
					paramValues[parameters[0].Name] = this.CreateDataObject(oldValues);
					paramValues[parameters[1].Name] = this.CreateDataObject(values);
				}
			}
			else
			{
				paramValues[parameters[0].Name] = this.CreateDataObject(values);
			}
			return methodInfo;
		}

		// Token: 0x06002B15 RID: 11029 RVA: 0x00070274 File Offset: 0x0006E474
		private Exception CreateMethodException(string methodName, IOrderedDictionary parameters)
		{
			string text = "";
			foreach (object obj in parameters.Keys)
			{
				string text2 = (string)obj;
				text = text + text2 + ", ";
			}
			string[] array = new string[9];
			array[0] = "ObjectDataSource ";
			array[1] = this.owner.ID;
			array[2] = " could not find a method named '";
			array[3] = methodName;
			array[4] = "' with parameters ";
			array[5] = text;
			array[6] = "in type '";
			int num = 7;
			Type type = this.ObjectType;
			array[num] = ((type != null) ? type.ToString() : null);
			array[8] = "'.";
			return new InvalidOperationException(string.Concat(array));
		}

		// Token: 0x06002B16 RID: 11030 RVA: 0x00070340 File Offset: 0x0006E540
		private object CreateDataObject(IDictionary values)
		{
			object obj = Activator.CreateInstance(this.DataObjectType);
			foreach (object obj2 in values)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj2;
				PropertyInfo property = this.DataObjectType.GetProperty((string)dictionaryEntry.Key);
				if (property == null)
				{
					string[] array = new string[5];
					array[0] = "Property ";
					int num = 1;
					object key = dictionaryEntry.Key;
					array[num] = ((key != null) ? key.ToString() : null);
					array[2] = " not found in type '";
					int num2 = 3;
					Type type = this.DataObjectType;
					array[num2] = ((type != null) ? type.ToString() : null);
					array[4] = "'.";
					throw new InvalidOperationException(string.Concat(array));
				}
				object[] customAttributes = property.GetCustomAttributes(typeof(TypeConverterAttribute), true);
				Type propertyType = property.PropertyType;
				object value = dictionaryEntry.Value;
				object obj3 = this.ConvertParameterWithTypeConverter(customAttributes, propertyType, value);
				if (obj3 == null)
				{
					obj3 = this.ConvertParameter(propertyType, value);
				}
				property.SetValue(obj, obj3, null);
			}
			return obj;
		}

		// Token: 0x06002B17 RID: 11031 RVA: 0x00070464 File Offset: 0x0006E664
		private object CreateObjectInstance()
		{
			ObjectDataSourceEventArgs e = new ObjectDataSourceEventArgs(null);
			this.OnObjectCreating(e);
			if (e.ObjectInstance != null)
			{
				return e.ObjectInstance;
			}
			object obj = Activator.CreateInstance(this.ObjectType);
			e.ObjectInstance = obj;
			this.OnObjectCreated(e);
			return e.ObjectInstance;
		}

		// Token: 0x06002B18 RID: 11032 RVA: 0x000704B0 File Offset: 0x0006E6B0
		private void DisposeObjectInstance(object obj)
		{
			ObjectDataSourceDisposingEventArgs e = new ObjectDataSourceDisposingEventArgs(obj);
			this.OnObjectDisposing(e);
			if (!e.Cancel)
			{
				IDisposable disposable = obj as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}

		// Token: 0x06002B19 RID: 11033 RVA: 0x000704E4 File Offset: 0x0006E6E4
		private object FindValueByName(string name, IDictionary values, bool format)
		{
			if (values == null)
			{
				return null;
			}
			foreach (object obj in values)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				string text = (format ? this.FormatOldParameter(dictionaryEntry.Key.ToString()) : dictionaryEntry.Key.ToString());
				if (string.Compare(name, text, StringComparison.InvariantCultureIgnoreCase) == 0)
				{
					return values[dictionaryEntry.Key];
				}
			}
			return null;
		}

		// Token: 0x06002B1A RID: 11034 RVA: 0x0007057C File Offset: 0x0006E77C
		private IOrderedDictionary MergeParameterValues(ParameterCollection viewParams, IDictionary values, IDictionary oldValues)
		{
			IOrderedDictionary values2 = viewParams.GetValues(this.context, this.owner);
			OrderedDictionary orderedDictionary = new OrderedDictionary(StringComparer.InvariantCultureIgnoreCase);
			foreach (object obj in values2.Keys)
			{
				string text = (string)obj;
				orderedDictionary[text] = values2[text];
				if (oldValues != null)
				{
					object obj2 = this.FindValueByName(text, oldValues, true);
					if (obj2 != null)
					{
						object obj3 = viewParams[text].ConvertValue(obj2);
						orderedDictionary[text] = obj3;
					}
				}
				if (values != null)
				{
					object obj4 = this.FindValueByName(text, values, false);
					if (obj4 != null)
					{
						object obj5 = viewParams[text].ConvertValue(obj4);
						orderedDictionary[text] = obj5;
					}
				}
			}
			if (values != null)
			{
				foreach (object obj6 in values)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj6;
					if (this.FindValueByName((string)dictionaryEntry.Key, orderedDictionary, false) == null)
					{
						orderedDictionary[dictionaryEntry.Key] = dictionaryEntry.Value;
					}
				}
			}
			if (oldValues != null)
			{
				foreach (object obj7 in oldValues)
				{
					DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj7;
					string text2 = this.FormatOldParameter((string)dictionaryEntry2.Key);
					if (this.FindValueByName(text2, orderedDictionary, false) == null)
					{
						orderedDictionary[text2] = dictionaryEntry2.Value;
					}
				}
			}
			return orderedDictionary;
		}

		// Token: 0x06002B1B RID: 11035 RVA: 0x0007073C File Offset: 0x0006E93C
		private object[] GetParameterArray(ParameterInfo[] methodParams, IOrderedDictionary viewParams, out ArrayList outParamInfos)
		{
			outParamInfos = null;
			object[] array = new object[methodParams.Length];
			foreach (ParameterInfo parameterInfo in methodParams)
			{
				if (!viewParams.Contains(parameterInfo.Name))
				{
					return null;
				}
				array[parameterInfo.Position] = this.ConvertParameter(parameterInfo.ParameterType, viewParams[parameterInfo.Name]);
				if (parameterInfo.ParameterType.IsByRef)
				{
					if (outParamInfos == null)
					{
						outParamInfos = new ArrayList();
					}
					outParamInfos.Add(parameterInfo);
				}
			}
			return array;
		}

		// Token: 0x06002B1C RID: 11036 RVA: 0x000707BC File Offset: 0x0006E9BC
		private object ConvertParameterWithTypeConverter(object[] attributes, Type targetType, object value)
		{
			if (attributes == null || attributes.Length == 0 || value == null)
			{
				return null;
			}
			for (int i = 0; i < attributes.Length; i++)
			{
				TypeConverterAttribute typeConverterAttribute = attributes[i] as TypeConverterAttribute;
				if (typeConverterAttribute != null)
				{
					Type type = HttpApplication.LoadType(typeConverterAttribute.ConverterTypeName, false);
					if (!(type == null))
					{
						TypeConverter typeConverter = Activator.CreateInstance(type, new object[] { targetType }) as TypeConverter;
						if (typeConverter != null)
						{
							if (typeConverter.CanConvertFrom(value.GetType()))
							{
								return typeConverter.ConvertFrom(value);
							}
							if (typeConverter.CanConvertFrom(typeof(string)))
							{
								return typeConverter.ConvertFrom(value.ToString());
							}
						}
					}
				}
			}
			return null;
		}

		// Token: 0x06002B1D RID: 11037 RVA: 0x0007085B File Offset: 0x0006EA5B
		private object ConvertParameter(Type targetType, object value)
		{
			return this.ConvertParameter(Type.GetTypeCode(targetType), value);
		}

		// Token: 0x06002B1E RID: 11038 RVA: 0x0007086A File Offset: 0x0006EA6A
		private object ConvertParameter(TypeCode targetType, object value)
		{
			if (value == null)
			{
				if (targetType != TypeCode.Object && targetType != TypeCode.String)
				{
					value = 0;
				}
				else if (this.ConvertNullToDBNull)
				{
					return DBNull.Value;
				}
			}
			if (targetType == TypeCode.Object || targetType == TypeCode.Empty)
			{
				return value;
			}
			return Convert.ChangeType(value, targetType);
		}

		// Token: 0x06002B1F RID: 11039 RVA: 0x000708A0 File Offset: 0x0006EAA0
		private string FormatOldParameter(string name)
		{
			string text = this.OldValuesParameterFormatString;
			if (text.Length > 0)
			{
				return string.Format(text, name);
			}
			return name;
		}

		// Token: 0x06002B20 RID: 11040 RVA: 0x000302D4 File Offset: 0x0002E4D4
		private void OnParametersChanged(object sender, EventArgs args)
		{
			this.OnDataSourceViewChanged(EventArgs.Empty);
		}

		/// <summary>Restores previously saved view state for the data source view.</summary>
		/// <param name="savedState">An object that represents the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> state to restore.</param>
		// Token: 0x06002B21 RID: 11041 RVA: 0x000708C8 File Offset: 0x0006EAC8
		protected virtual void LoadViewState(object savedState)
		{
			object[] array = ((savedState == null) ? new object[5] : ((object[])savedState));
			((IStateManager)this.SelectParameters).LoadViewState(array[0]);
			((IStateManager)this.UpdateParameters).LoadViewState(array[1]);
			((IStateManager)this.DeleteParameters).LoadViewState(array[2]);
			((IStateManager)this.InsertParameters).LoadViewState(array[3]);
			((IStateManager)this.FilterParameters).LoadViewState(array[4]);
		}

		/// <summary>Saves the changes to the view state for the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object since the time when the page was posted back to the server.</summary>
		/// <returns>The object that contains the changes to the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> view state; otherwise null, if there is no view state associated with the object.</returns>
		// Token: 0x06002B22 RID: 11042 RVA: 0x00070930 File Offset: 0x0006EB30
		protected virtual object SaveViewState()
		{
			object[] array = new object[5];
			if (this.selectParameters != null)
			{
				array[0] = ((IStateManager)this.selectParameters).SaveViewState();
			}
			if (this.updateParameters != null)
			{
				array[1] = ((IStateManager)this.updateParameters).SaveViewState();
			}
			if (this.deleteParameters != null)
			{
				array[2] = ((IStateManager)this.deleteParameters).SaveViewState();
			}
			if (this.insertParameters != null)
			{
				array[3] = ((IStateManager)this.insertParameters).SaveViewState();
			}
			if (this.filterParameters != null)
			{
				array[4] = ((IStateManager)this.filterParameters).SaveViewState();
			}
			object[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				if (array2[i] != null)
				{
					return array;
				}
			}
			return null;
		}

		/// <summary>Causes the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object to track changes to its view state so that the changes can be stored in the <see cref="P:System.Web.UI.Control.ViewState" /> object for the control and persisted across requests for the same page.</summary>
		// Token: 0x06002B23 RID: 11043 RVA: 0x000709CA File Offset: 0x0006EBCA
		protected virtual void TrackViewState()
		{
			this.isTrackingViewState = true;
			if (this.selectParameters != null)
			{
				((IStateManager)this.selectParameters).TrackViewState();
			}
			if (this.filterParameters != null)
			{
				((IStateManager)this.filterParameters).TrackViewState();
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> object is saving changes to its view state.</summary>
		/// <returns>true, if the data source view is marked to save its state; otherwise, false.</returns>
		// Token: 0x17000DCC RID: 3532
		// (get) Token: 0x06002B24 RID: 11044 RVA: 0x000709F9 File Offset: 0x0006EBF9
		protected bool IsTrackingViewState
		{
			get
			{
				return this.isTrackingViewState;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Web.UI.IStateManager.IsTrackingViewState" />.</summary>
		/// <returns>true, if the data source view is marked to save its state; otherwise, false.</returns>
		// Token: 0x17000DCD RID: 3533
		// (get) Token: 0x06002B25 RID: 11045 RVA: 0x00070A01 File Offset: 0x0006EC01
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.IsTrackingViewState;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IStateManager.TrackViewState" />.</summary>
		// Token: 0x06002B26 RID: 11046 RVA: 0x00070A09 File Offset: 0x0006EC09
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IStateManager.LoadViewState(System.Object)" />.</summary>
		/// <param name="savedState">An object that represents the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> state to restore.</param>
		// Token: 0x06002B27 RID: 11047 RVA: 0x00070A11 File Offset: 0x0006EC11
		void IStateManager.LoadViewState(object savedState)
		{
			this.LoadViewState(savedState);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Web.UI.IStateManager.SaveViewState" />.</summary>
		/// <returns>The object that contains the changes to the <see cref="T:System.Web.UI.WebControls.ObjectDataSourceView" /> view state; otherwise, null.</returns>
		// Token: 0x06002B28 RID: 11048 RVA: 0x00070A1A File Offset: 0x0006EC1A
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		// Token: 0x06002B29 RID: 11049 RVA: 0x00070A24 File Offset: 0x0006EC24
		// Note: this type is marked as 'beforefieldinit'.
		static ObjectDataSourceView()
		{
			ObjectDataSourceView.DeletedEvent = new object();
			ObjectDataSourceView.DeletingEvent = new object();
			ObjectDataSourceView.FilteringEvent = new object();
			ObjectDataSourceView.InsertedEvent = new object();
			ObjectDataSourceView.InsertingEvent = new object();
			ObjectDataSourceView.ObjectCreatedEvent = new object();
			ObjectDataSourceView.ObjectCreatingEvent = new object();
			ObjectDataSourceView.ObjectDisposingEvent = new object();
			ObjectDataSourceView.SelectedEvent = new object();
			ObjectDataSourceView.SelectingEvent = new object();
			ObjectDataSourceView.UpdatedEvent = new object();
			ObjectDataSourceView.UpdatingEvent = new object();
		}

		/// <summary>Gets a or sets a value that indicates what culture information is used when converting string values to actual property types in order to construct an object of the type indicated by <see cref="P:System.Web.UI.WebControls.ObjectDataSource.DataObjectTypeName" />.</summary>
		/// <returns>The culture information. The default value is <see cref="F:System.Web.UI.WebControls.ParsingCulture.Invariant" />.</returns>
		// Token: 0x17000DCE RID: 3534
		// (get) Token: 0x06002B2A RID: 11050 RVA: 0x00070AAC File Offset: 0x0006ECAC
		// (set) Token: 0x06002B2B RID: 11051 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public ParsingCulture ParsingCulture
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return ParsingCulture.Invariant;
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x04001AE7 RID: 6887
		private ObjectDataSource owner;

		// Token: 0x04001AE8 RID: 6888
		private HttpContext context;

		// Token: 0x04001AE9 RID: 6889
		private Type objectType;

		// Token: 0x04001AEA RID: 6890
		private Type dataObjectType;

		// Token: 0x04001AEB RID: 6891
		private bool convertNullToDBNull;

		// Token: 0x04001AEC RID: 6892
		private bool enablePaging;

		// Token: 0x04001AED RID: 6893
		private string dataObjectTypeName;

		// Token: 0x04001AEE RID: 6894
		private string filterExpression;

		// Token: 0x04001AEF RID: 6895
		private string maximumRowsParameterName;

		// Token: 0x04001AF0 RID: 6896
		private string oldValuesParameterFormatString;

		// Token: 0x04001AF1 RID: 6897
		private string deleteMethod;

		// Token: 0x04001AF2 RID: 6898
		private string insertMethod;

		// Token: 0x04001AF3 RID: 6899
		private string selectCountMethod;

		// Token: 0x04001AF4 RID: 6900
		private string selectMethod;

		// Token: 0x04001AF5 RID: 6901
		private string sortParameterName;

		// Token: 0x04001AF6 RID: 6902
		private string startRowIndexParameterName;

		// Token: 0x04001AF7 RID: 6903
		private string typeName;

		// Token: 0x04001AF8 RID: 6904
		private string updateMethod;

		// Token: 0x04001AF9 RID: 6905
		private bool isTrackingViewState;

		// Token: 0x04001AFA RID: 6906
		private ParameterCollection selectParameters;

		// Token: 0x04001AFB RID: 6907
		private ParameterCollection updateParameters;

		// Token: 0x04001AFC RID: 6908
		private ParameterCollection deleteParameters;

		// Token: 0x04001AFD RID: 6909
		private ParameterCollection insertParameters;

		// Token: 0x04001AFE RID: 6910
		private ParameterCollection filterParameters;

		// Token: 0x04001B0B RID: 6923
		private ConflictOptions conflictDetection;
	}
}

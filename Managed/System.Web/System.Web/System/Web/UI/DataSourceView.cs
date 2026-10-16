using System;
using System.Collections;
using System.ComponentModel;
using Unity;

namespace System.Web.UI
{
	/// <summary>Serves as the base class for all data source view classes, which define the capabilities of data source controls.</summary>
	// Token: 0x020001C1 RID: 449
	public abstract class DataSourceView
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.DataSourceView" /> class.</summary>
		/// <param name="owner">The data source control that the <see cref="T:System.Web.UI.DataSourceView" /> is associated with.</param>
		/// <param name="viewName">The name of the <see cref="T:System.Web.UI.DataSourceView" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="owner" /> is null.- or -<paramref name="viewName" /> is null.</exception>
		// Token: 0x06001245 RID: 4677 RVA: 0x0003029A File Offset: 0x0002E49A
		protected DataSourceView(IDataSource owner, string viewName)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			this.viewName = viewName;
			owner.DataSourceChanged += this.OnDataSourceChanged;
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x000302D4 File Offset: 0x0002E4D4
		private void OnDataSourceChanged(object sender, EventArgs e)
		{
			this.OnDataSourceViewChanged(EventArgs.Empty);
		}

		/// <summary>Performs an asynchronous delete operation on the list of data that the <see cref="T:System.Web.UI.DataSourceView" /> object represents.</summary>
		/// <param name="keys">An <see cref="T:System.Collections.IDictionary" /> of object or row keys to be deleted by the <see cref="M:System.Web.UI.DataSourceView.ExecuteDelete(System.Collections.IDictionary,System.Collections.IDictionary)" /> operation.</param>
		/// <param name="oldValues">An <see cref="T:System.Collections.IDictionary" /> of name/value pairs that represent data elements and their original values.</param>
		/// <param name="callback">A <see cref="T:System.Web.UI.DataSourceViewOperationCallback" /> delegate that is used to notify a data-bound control when the asynchronous operation is complete.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Web.UI.DataSourceViewOperationCallback" /> supplied is null. </exception>
		// Token: 0x06001247 RID: 4679 RVA: 0x000302E4 File Offset: 0x0002E4E4
		public virtual void Delete(IDictionary keys, IDictionary oldValues, DataSourceViewOperationCallback callback)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callBack");
			}
			int num;
			try
			{
				num = this.ExecuteDelete(keys, oldValues);
			}
			catch (Exception ex)
			{
				if (!callback(0, ex))
				{
					throw;
				}
				return;
			}
			callback(num, null);
		}

		/// <summary>Performs a delete operation on the list of data that the <see cref="T:System.Web.UI.DataSourceView" /> object represents.</summary>
		/// <returns>The number of items that were deleted from the underlying data storage.</returns>
		/// <param name="keys">An <see cref="T:System.Collections.IDictionary" /> of object or row keys to be deleted by the <see cref="M:System.Web.UI.DataSourceView.ExecuteDelete(System.Collections.IDictionary,System.Collections.IDictionary)" /> operation.</param>
		/// <param name="oldValues">An <see cref="T:System.Collections.IDictionary" /> of name/value pairs that represent data elements and their original values.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="M:System.Web.UI.DataSourceView.ExecuteDelete(System.Collections.IDictionary,System.Collections.IDictionary)" /> operation is not supported by the <see cref="T:System.Web.UI.DataSourceView" />. </exception>
		// Token: 0x06001248 RID: 4680 RVA: 0x00003A0D File Offset: 0x00001C0D
		protected virtual int ExecuteDelete(IDictionary keys, IDictionary oldValues)
		{
			throw new NotSupportedException();
		}

		/// <summary>Performs an insert operation on the list of data that the <see cref="T:System.Web.UI.DataSourceView" /> object represents.</summary>
		/// <returns>The number of items that were inserted into the underlying data storage.</returns>
		/// <param name="values">An <see cref="T:System.Collections.IDictionary" /> of name/value pairs used during an insert operation.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="M:System.Web.UI.DataSourceView.ExecuteInsert(System.Collections.IDictionary)" /> operation is not supported by the <see cref="T:System.Web.UI.DataSourceView" />. </exception>
		// Token: 0x06001249 RID: 4681 RVA: 0x00003A0D File Offset: 0x00001C0D
		protected virtual int ExecuteInsert(IDictionary values)
		{
			throw new NotSupportedException();
		}

		/// <summary>Gets a list of data from the underlying data storage.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerable" /> list of data from the underlying data storage.</returns>
		/// <param name="arguments">A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> that is used to request operations on the data beyond basic data retrieval.</param>
		// Token: 0x0600124A RID: 4682
		protected internal abstract IEnumerable ExecuteSelect(DataSourceSelectArguments arguments);

		/// <summary>Performs an update operation on the list of data that the <see cref="T:System.Web.UI.DataSourceView" /> object represents.</summary>
		/// <returns>The number of items that were updated in the underlying data storage.</returns>
		/// <param name="keys">An <see cref="T:System.Collections.IDictionary" /> of object or row keys to be updated by the update operation.</param>
		/// <param name="values">An <see cref="T:System.Collections.IDictionary" /> of name/value pairs that represent data elements and their new values.</param>
		/// <param name="oldValues">An <see cref="T:System.Collections.IDictionary" /> of name/value pairs that represent data elements and their original values.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="M:System.Web.UI.DataSourceView.ExecuteUpdate(System.Collections.IDictionary,System.Collections.IDictionary,System.Collections.IDictionary)" /> operation is not supported by the <see cref="T:System.Web.UI.DataSourceView" />. </exception>
		// Token: 0x0600124B RID: 4683 RVA: 0x00003A0D File Offset: 0x00001C0D
		protected virtual int ExecuteUpdate(IDictionary keys, IDictionary values, IDictionary oldValues)
		{
			throw new NotSupportedException();
		}

		/// <summary>Performs an asynchronous insert operation on the list of data that the <see cref="T:System.Web.UI.DataSourceView" /> object represents.</summary>
		/// <param name="values">An <see cref="T:System.Collections.IDictionary" /> of name/value pairs used during an insert operation.</param>
		/// <param name="callback">A <see cref="T:System.Web.UI.DataSourceViewOperationCallback" /> delegate that is used to notify a data-bound control when the asynchronous operation is complete. </param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Web.UI.DataSourceViewOperationCallback" /> supplied is null.</exception>
		// Token: 0x0600124C RID: 4684 RVA: 0x00030334 File Offset: 0x0002E534
		public virtual void Insert(IDictionary values, DataSourceViewOperationCallback callback)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			int num;
			try
			{
				num = this.ExecuteInsert(values);
			}
			catch (Exception ex)
			{
				if (!callback(0, ex))
				{
					throw;
				}
				return;
			}
			callback(num, null);
		}

		/// <summary>Raises the <see cref="E:System.Web.UI.DataSourceView.DataSourceViewChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data.</param>
		// Token: 0x0600124D RID: 4685 RVA: 0x00030384 File Offset: 0x0002E584
		protected virtual void OnDataSourceViewChanged(EventArgs e)
		{
			if (this.eventsList != null)
			{
				EventHandler eventHandler = this.eventsList[DataSourceView.EventDataSourceViewChanged] as EventHandler;
				if (eventHandler != null)
				{
					eventHandler(this, e);
				}
			}
		}

		/// <summary>Called by the <see cref="M:System.Web.UI.DataSourceSelectArguments.RaiseUnsupportedCapabilitiesError(System.Web.UI.DataSourceView)" /> method to compare the capabilities requested for an <see cref="M:System.Web.UI.DataSourceView.ExecuteSelect(System.Web.UI.DataSourceSelectArguments)" /> operation against those that the view supports.</summary>
		/// <param name="capability">One of the <see cref="T:System.Web.UI.DataSourceCapabilities" /> values that is compared against the capabilities that the view supports.</param>
		/// <exception cref="T:System.NotSupportedException">The data source view does not support the data source capability specified.</exception>
		// Token: 0x0600124E RID: 4686 RVA: 0x000303BC File Offset: 0x0002E5BC
		protected internal virtual void RaiseUnsupportedCapabilityError(DataSourceCapabilities capability)
		{
			if ((capability & DataSourceCapabilities.Sort) != DataSourceCapabilities.None && !this.CanSort)
			{
				throw new NotSupportedException("Sort Capabilites");
			}
			if ((capability & DataSourceCapabilities.Page) != DataSourceCapabilities.None && !this.CanPage)
			{
				throw new NotSupportedException("Page Capabilites");
			}
			if ((capability & DataSourceCapabilities.RetrieveTotalRowCount) != DataSourceCapabilities.None && !this.CanRetrieveTotalRowCount)
			{
				throw new NotSupportedException("RetrieveTotalRowCount Capabilites");
			}
		}

		/// <summary>Gets a list of data asynchronously from the underlying data storage.</summary>
		/// <param name="arguments">A <see cref="T:System.Web.UI.DataSourceSelectArguments" /> that is used to request operations on the data beyond basic data retrieval.</param>
		/// <param name="callback">A <see cref="T:System.Web.UI.DataSourceViewSelectCallback" /> delegate that is used to notify a data-bound control when the asynchronous operation is complete.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Web.UI.DataSourceViewSelectCallback" /> supplied is null.</exception>
		// Token: 0x0600124F RID: 4687 RVA: 0x00030414 File Offset: 0x0002E614
		public virtual void Select(DataSourceSelectArguments arguments, DataSourceViewSelectCallback callback)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callBack");
			}
			arguments.RaiseUnsupportedCapabilitiesError(this);
			IEnumerable enumerable = this.ExecuteSelect(arguments);
			callback(enumerable);
		}

		/// <summary>Performs an asynchronous update operation on the list of data that the <see cref="T:System.Web.UI.DataSourceView" /> object represents.</summary>
		/// <param name="keys">An <see cref="T:System.Collections.IDictionary" /> of object or row keys to be updated by the update operation.</param>
		/// <param name="values">An <see cref="T:System.Collections.IDictionary" /> of name/value pairs that represent data elements and their new values.</param>
		/// <param name="oldValues">An <see cref="T:System.Collections.IDictionary" /> of name/value pairs that represent data elements and their original values.</param>
		/// <param name="callback">A <see cref="T:System.Web.UI.DataSourceViewOperationCallback" /> delegate that is used to notify a data-bound control when the asynchronous operation is complete.</param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Web.UI.DataSourceViewOperationCallback" /> supplied is null.</exception>
		// Token: 0x06001250 RID: 4688 RVA: 0x00030448 File Offset: 0x0002E648
		public virtual void Update(IDictionary keys, IDictionary values, IDictionary oldValues, DataSourceViewOperationCallback callback)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			int num;
			try
			{
				num = this.ExecuteUpdate(keys, values, oldValues);
			}
			catch (Exception ex)
			{
				if (!callback(0, ex))
				{
					throw;
				}
				return;
			}
			callback(num, null);
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.DataSourceView" /> object associated with the current <see cref="T:System.Web.UI.DataSourceControl" /> object supports the <see cref="M:System.Web.UI.DataSourceView.ExecuteDelete(System.Collections.IDictionary,System.Collections.IDictionary)" /> operation.</summary>
		/// <returns>true if the operation is supported; otherwise, false. The base class implementation returns false.</returns>
		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06001251 RID: 4689 RVA: 0x00008A4D File Offset: 0x00006C4D
		public virtual bool CanDelete
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.DataSourceView" /> object associated with the current <see cref="T:System.Web.UI.DataSourceControl" /> object supports the <see cref="M:System.Web.UI.DataSourceView.ExecuteInsert(System.Collections.IDictionary)" /> operation.</summary>
		/// <returns>true if the operation is supported; otherwise, false. The base class implementation returns false.</returns>
		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06001252 RID: 4690 RVA: 0x00008A4D File Offset: 0x00006C4D
		public virtual bool CanInsert
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.DataSourceView" /> object associated with the current <see cref="T:System.Web.UI.DataSourceControl" /> object supports paging through the data retrieved by the <see cref="M:System.Web.UI.DataSourceView.ExecuteSelect(System.Web.UI.DataSourceSelectArguments)" /> method.</summary>
		/// <returns>true if the operation is supported; otherwise, false. The base class implementation returns false.</returns>
		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06001253 RID: 4691 RVA: 0x00008A4D File Offset: 0x00006C4D
		public virtual bool CanPage
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.DataSourceView" /> object associated with the current <see cref="T:System.Web.UI.DataSourceControl" /> object supports retrieving the total number of data rows, instead of the data.</summary>
		/// <returns>true if the operation is supported; otherwise, false. The base class implementation returns false.</returns>
		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06001254 RID: 4692 RVA: 0x00008A4D File Offset: 0x00006C4D
		public virtual bool CanRetrieveTotalRowCount
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.DataSourceView" /> object associated with the current <see cref="T:System.Web.UI.DataSourceControl" /> object supports a sorted view on the underlying data source.</summary>
		/// <returns>true if the operation is supported; otherwise, false. The default implementation returns false.</returns>
		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06001255 RID: 4693 RVA: 0x00008A4D File Offset: 0x00006C4D
		public virtual bool CanSort
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.DataSourceView" /> object associated with the current <see cref="T:System.Web.UI.DataSourceControl" /> object supports the <see cref="M:System.Web.UI.DataSourceView.ExecuteUpdate(System.Collections.IDictionary,System.Collections.IDictionary,System.Collections.IDictionary)" /> operation.</summary>
		/// <returns>true if the operation is supported; otherwise, false. The default implementation returns false.</returns>
		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06001256 RID: 4694 RVA: 0x00008A4D File Offset: 0x00006C4D
		public virtual bool CanUpdate
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a list of event-handler delegates for the data source view.</summary>
		/// <returns>The list of event-handler delegates.</returns>
		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06001257 RID: 4695 RVA: 0x0003049C File Offset: 0x0002E69C
		protected EventHandlerList Events
		{
			get
			{
				if (this.eventsList == null)
				{
					this.eventsList = new EventHandlerList();
				}
				return this.eventsList;
			}
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x000304B7 File Offset: 0x0002E6B7
		internal bool HasEvents()
		{
			return this.eventsList != null;
		}

		/// <summary>Gets the name of the data source view.</summary>
		/// <returns>The name of the <see cref="T:System.Web.UI.DataSourceView" />, if it has one. The default value is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06001259 RID: 4697 RVA: 0x000304C2 File Offset: 0x0002E6C2
		public string Name
		{
			get
			{
				return this.viewName;
			}
		}

		/// <summary>Occurs when the data source view has changed.</summary>
		// Token: 0x1400002A RID: 42
		// (add) Token: 0x0600125A RID: 4698 RVA: 0x000304CA File Offset: 0x0002E6CA
		// (remove) Token: 0x0600125B RID: 4699 RVA: 0x000304DD File Offset: 0x0002E6DD
		public event EventHandler DataSourceViewChanged
		{
			add
			{
				this.Events.AddHandler(DataSourceView.EventDataSourceViewChanged, value);
			}
			remove
			{
				this.Events.RemoveHandler(DataSourceView.EventDataSourceViewChanged, value);
			}
		}

		/// <summary>Determines whether the specified command can be executed.</summary>
		/// <returns>true if the command can be executed; otherwise, false.</returns>
		/// <param name="commandName">The name of the command.</param>
		// Token: 0x0600125D RID: 4701 RVA: 0x000304FC File Offset: 0x0002E6FC
		public virtual bool CanExecute(string commandName)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Executes the specified command.</summary>
		/// <returns>The number of items that were affected by the operation.</returns>
		/// <param name="commandName">The name of the command.</param>
		/// <param name="keys">A dictionary of object keys or row keys to act on.</param>
		/// <param name="values">A dictionary of name/value pairs that represent data elements and their values.</param>
		// Token: 0x0600125E RID: 4702 RVA: 0x00030518 File Offset: 0x0002E718
		protected virtual int ExecuteCommand(string commandName, IDictionary keys, IDictionary values)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Executes the specified command.</summary>
		/// <param name="commandName">The name of the command.</param>
		/// <param name="keys">A dictionary of object keys or row keys to act on.</param>
		/// <param name="values">A dictionary of name/value pairs that represent data elements and their values.</param>
		/// <param name="callback">A <see cref="T:System.Web.UI.DataSourceViewOperationCallback" /> object.</param>
		// Token: 0x0600125F RID: 4703 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual void ExecuteCommand(string commandName, IDictionary keys, IDictionary values, DataSourceViewOperationCallback callback)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400141D RID: 5149
		private string viewName = string.Empty;

		// Token: 0x0400141E RID: 5150
		private EventHandlerList eventsList;

		// Token: 0x0400141F RID: 5151
		private static readonly object EventDataSourceViewChanged = new object();
	}
}

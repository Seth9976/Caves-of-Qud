using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI
{
	/// <summary>Provides a collection of <see cref="T:System.Web.UI.DataBinding" /> objects for an ASP.NET server control. This class cannot be inherited.</summary>
	// Token: 0x020001BC RID: 444
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class DataBindingCollection : ICollection, IEnumerable
	{
		/// <summary>Occurs when the collection of <see cref="T:System.Web.UI.DataBinding" /> objects is changed. </summary>
		// Token: 0x14000028 RID: 40
		// (add) Token: 0x060011F2 RID: 4594 RVA: 0x0002FBAA File Offset: 0x0002DDAA
		// (remove) Token: 0x060011F3 RID: 4595 RVA: 0x0002FBBD File Offset: 0x0002DDBD
		public event EventHandler Changed
		{
			add
			{
				this.events.AddHandler(DataBindingCollection.changedEvent, value);
			}
			remove
			{
				this.events.RemoveHandler(DataBindingCollection.changedEvent, value);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.DataBindingCollection" /> class.</summary>
		// Token: 0x060011F4 RID: 4596 RVA: 0x0002FBD0 File Offset: 0x0002DDD0
		public DataBindingCollection()
		{
			this.list = new Hashtable();
			this.removed = new ArrayList();
		}

		/// <summary>Gets the number of <see cref="T:System.Web.UI.DataBinding" /> objects in the <see cref="T:System.Web.UI.DataBindingCollection" /> object.</summary>
		/// <returns>The number of <see cref="T:System.Web.UI.DataBinding" /> objects in the <see cref="T:System.Web.UI.DataBindingCollection" />.</returns>
		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x060011F5 RID: 4597 RVA: 0x0002FBF9 File Offset: 0x0002DDF9
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.DataBindingCollection" /> collection is read-only.</summary>
		/// <returns>true if the collection is read-only; otherwise, false. The default is false.</returns>
		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x060011F6 RID: 4598 RVA: 0x0002FC06 File Offset: 0x0002DE06
		public bool IsReadOnly
		{
			get
			{
				return this.list.IsReadOnly;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.DataBindingCollection" /> collection is synchronized (thread safe).</summary>
		/// <returns>Always false.</returns>
		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x060011F7 RID: 4599 RVA: 0x0002FC13 File Offset: 0x0002DE13
		public bool IsSynchronized
		{
			get
			{
				return this.list.IsSynchronized;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.DataBinding" /> object with the specified property name.</summary>
		/// <returns>The <see cref="T:System.Web.UI.DataBinding" /> with the specified property name. If no object with the specified name exists, this value is null.</returns>
		/// <param name="propertyName">The name of the property to be found. </param>
		// Token: 0x170005DE RID: 1502
		public DataBinding this[string propertyName]
		{
			get
			{
				return this.list[propertyName] as DataBinding;
			}
		}

		/// <summary>Gets an array of the names of the <see cref="T:System.Web.UI.DataBinding" /> objects removed from the collection.</summary>
		/// <returns>The array of names of the <see cref="T:System.Web.UI.DataBinding" /> objects removed from the collection. </returns>
		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x060011F9 RID: 4601 RVA: 0x0002FC33 File Offset: 0x0002DE33
		public string[] RemovedBindings
		{
			get
			{
				return (string[])this.removed.ToArray(typeof(string));
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Web.UI.DataBindingCollection" /> collection.</summary>
		/// <returns>The <see cref="T:System.Object" /> to be used to synchronize access to the collection.</returns>
		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x060011FA RID: 4602 RVA: 0x0002FC4F File Offset: 0x0002DE4F
		public object SyncRoot
		{
			get
			{
				return this.list.SyncRoot;
			}
		}

		/// <summary>Adds the specified <see cref="T:System.Web.UI.DataBinding" /> object to the <see cref="T:System.Web.UI.DataBindingCollection" /> collection.</summary>
		/// <param name="binding">The data-binding object to add to the collection. </param>
		// Token: 0x060011FB RID: 4603 RVA: 0x0002FC5C File Offset: 0x0002DE5C
		public void Add(DataBinding binding)
		{
			this.list.Add(binding.PropertyName, binding);
			this.RaiseChanged();
		}

		/// <summary>Removes all <see cref="T:System.Web.UI.DataBinding" /> objects from the <see cref="T:System.Web.UI.DataBindingCollection" /> collection.</summary>
		// Token: 0x060011FC RID: 4604 RVA: 0x0002FC76 File Offset: 0x0002DE76
		public void Clear()
		{
			this.list.Clear();
		}

		/// <summary>Copies the <see cref="T:System.Web.UI.DataBindingCollection" /> values to a one-dimensional <see cref="T:System.Array" />, beginning at the <see cref="T:System.Array" /> object's specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from <see cref="T:System.Web.UI.DataBindingCollection" />. </param>
		/// <param name="index">The index in the array, specified by the <paramref name="array" /> parameter, where copying begins. </param>
		// Token: 0x060011FD RID: 4605 RVA: 0x0002FC83 File Offset: 0x0002DE83
		public void CopyTo(Array array, int index)
		{
			this.list.Values.CopyTo(array, index);
		}

		/// <summary>Returns an enumerator to iterate through the <see cref="T:System.Web.UI.DataBindingCollection" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that contains the collection's members.</returns>
		// Token: 0x060011FE RID: 4606 RVA: 0x0002FC97 File Offset: 0x0002DE97
		public IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		/// <summary>Removes the specified <see cref="T:System.Web.UI.DataBinding" /> object from the <see cref="T:System.Web.UI.DataBindingCollection" /> collection and adds it to the <see cref="P:System.Web.UI.DataBindingCollection.RemovedBindings" /> collection.</summary>
		/// <param name="binding">The <see cref="T:System.Web.UI.DataBinding" /> to be removed from the <see cref="T:System.Web.UI.DataBindingCollection" />. </param>
		// Token: 0x060011FF RID: 4607 RVA: 0x0002FCA4 File Offset: 0x0002DEA4
		public void Remove(DataBinding binding)
		{
			string propertyName = binding.PropertyName;
			this.Remove(propertyName);
		}

		/// <summary>Removes the <see cref="T:System.Web.UI.DataBinding" /> object associated with the specified property name from the <see cref="T:System.Web.UI.DataBindingCollection" /> collection and adds it to the <see cref="P:System.Web.UI.DataBindingCollection.RemovedBindings" /> collection.</summary>
		/// <param name="propertyName">The property name associated with the <see cref="T:System.Web.UI.DataBinding" /> to be removed. </param>
		// Token: 0x06001200 RID: 4608 RVA: 0x0002FCBF File Offset: 0x0002DEBF
		public void Remove(string propertyName)
		{
			this.removed.Add(propertyName);
			this.list.Remove(propertyName);
			this.RaiseChanged();
		}

		/// <summary>Removes the <see cref="T:System.Web.UI.DataBinding" /> object, associated with the specified property name, from the <see cref="T:System.Web.UI.DataBindingCollection" /> collection and controls whether to add the binding to the <see cref="P:System.Web.UI.DataBindingCollection.RemovedBindings" /> list.</summary>
		/// <param name="propertyName">The property associated with the <see cref="T:System.Web.UI.DataBinding" /> to be removed. </param>
		/// <param name="addToRemovedList">A Boolean value that indicates whether to add the property name to the <see cref="P:System.Web.UI.DataBindingCollection.RemovedBindings" /> list. true indicates that the <paramref name="propertyName" /> parameter will be added to the <see cref="P:System.Web.UI.DataBindingCollection.RemovedBindings" /> property, and false indicates that <paramref name="propertyName" /> will not be added to the <see cref="P:System.Web.UI.DataBindingCollection.RemovedBindings" /> property. </param>
		// Token: 0x06001201 RID: 4609 RVA: 0x0002FCE0 File Offset: 0x0002DEE0
		public void Remove(string propertyName, bool addToRemovedList)
		{
			if (addToRemovedList)
			{
				this.removed.Add(string.Empty);
			}
			else
			{
				this.removed.Add(propertyName);
			}
			this.list.Remove(propertyName);
		}

		/// <summary>Determines whether the data-binding collection contains a specific <see cref="T:System.Web.UI.DataBinding" /> object.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.DataBindingCollection" /> contains an element with the specified name; otherwise, false.</returns>
		/// <param name="propertyName">The name of the object to locate in the collection.</param>
		// Token: 0x06001202 RID: 4610 RVA: 0x0002FD11 File Offset: 0x0002DF11
		public bool Contains(string propertyName)
		{
			return this.list.Contains(propertyName);
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x0002FD20 File Offset: 0x0002DF20
		internal void RaiseChanged()
		{
			EventHandler eventHandler = this.events[DataBindingCollection.changedEvent] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, EventArgs.Empty);
			}
		}

		// Token: 0x0400140E RID: 5134
		private static readonly object changedEvent = new object();

		// Token: 0x0400140F RID: 5135
		private Hashtable list;

		// Token: 0x04001410 RID: 5136
		private ArrayList removed;

		// Token: 0x04001411 RID: 5137
		private EventHandlerList events = new EventHandlerList();
	}
}

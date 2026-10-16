using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Web.UI
{
	/// <summary>Provides a base class for all strongly typed collections that manage <see cref="T:System.Web.UI.IStateManager" /> objects.</summary>
	// Token: 0x02000225 RID: 549
	public abstract class StateManagedCollection : IList, ICollection, IEnumerable, IStateManager
	{
		/// <summary>When overridden in a derived class, creates an instance of a class that implements <see cref="T:System.Web.UI.IStateManager" />. The type of object created is based on the specified member of the collection returned by the <see cref="M:System.Web.UI.StateManagedCollection.GetKnownTypes" /> method.</summary>
		/// <returns>An instance of a class derived from <see cref="T:System.Web.UI.IStateManager" />, according to the <paramref name="index" /> provided.</returns>
		/// <param name="index">The index, from the ordered list of types returned by <see cref="M:System.Web.UI.StateManagedCollection.GetKnownTypes" />, of the type of <see cref="T:System.Web.UI.IStateManager" /> to create.</param>
		/// <exception cref="T:System.InvalidOperationException">In all cases when not overridden in a derived class.</exception>
		// Token: 0x06001680 RID: 5760 RVA: 0x00003BF6 File Offset: 0x00001DF6
		protected virtual object CreateKnownType(int index)
		{
			return null;
		}

		/// <summary>Forces the entire <see cref="T:System.Web.UI.StateManagedCollection" /> collection to be serialized into view state. </summary>
		// Token: 0x06001681 RID: 5761 RVA: 0x0003A750 File Offset: 0x00038950
		public void SetDirty()
		{
			this.saveEverything = true;
			for (int i = 0; i < this.items.Count; i++)
			{
				this.SetDirtyObject(this.items[i]);
			}
		}

		/// <summary>When overridden in a derived class, instructs an object contained by the collection to record its entire state to view state, rather than recording only change information.</summary>
		/// <param name="o">The <see cref="T:System.Web.UI.IStateManager" /> that should serialize itself completely.</param>
		// Token: 0x06001682 RID: 5762
		protected abstract void SetDirtyObject(object o);

		/// <summary>When overridden in a derived class, gets an array of <see cref="T:System.Web.UI.IStateManager" /> types that the <see cref="T:System.Web.UI.StateManagedCollection" /> collection can contain.</summary>
		/// <returns>An ordered array of <see cref="T:System.Type" /> objects that identify the types of <see cref="T:System.Web.UI.IStateManager" /> objects the collection can contain. The default implementation returns null.</returns>
		// Token: 0x06001683 RID: 5763 RVA: 0x00003BF6 File Offset: 0x00001DF6
		protected virtual Type[] GetKnownTypes()
		{
			return null;
		}

		/// <summary>When overridden in a derived class, performs additional work before the <see cref="M:System.Web.UI.StateManagedCollection.Clear" /> method removes all items from the collection.</summary>
		// Token: 0x06001684 RID: 5764 RVA: 0x00003946 File Offset: 0x00001B46
		protected virtual void OnClear()
		{
		}

		/// <summary>When overridden in a derived class, performs additional work after the <see cref="M:System.Web.UI.StateManagedCollection.Clear" /> method finishes removing all items from the collection.</summary>
		// Token: 0x06001685 RID: 5765 RVA: 0x00003946 File Offset: 0x00001B46
		protected virtual void OnClearComplete()
		{
		}

		/// <summary>When overridden in a derived class, performs additional work before the <see cref="M:System.Web.UI.StateManagedCollection.System.Collections.IList.Insert(System.Int32,System.Object)" /> or <see cref="M:System.Web.UI.StateManagedCollection.System.Collections.IList.Add(System.Object)" /> method adds an item to the collection.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted by the <see cref="M:System.Web.UI.StateManagedCollection.System.Collections.IList.Insert(System.Int32,System.Object)" /> method.</param>
		/// <param name="value">The object to insert into the <see cref="T:System.Web.UI.StateManagedCollection" />.</param>
		// Token: 0x06001686 RID: 5766 RVA: 0x00003946 File Offset: 0x00001B46
		protected virtual void OnInsert(int index, object value)
		{
		}

		/// <summary>When overridden in a derived class, performs additional work after the <see cref="M:System.Web.UI.StateManagedCollection.System.Collections.IList.Insert(System.Int32,System.Object)" /> or <see cref="M:System.Web.UI.StateManagedCollection.System.Collections.IList.Add(System.Object)" /> method adds an item to the collection.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> is inserted by the <see cref="M:System.Web.UI.StateManagedCollection.System.Collections.IList.Insert(System.Int32,System.Object)" /> method.</param>
		/// <param name="value">The object inserted into the <see cref="T:System.Web.UI.StateManagedCollection" />.</param>
		// Token: 0x06001687 RID: 5767 RVA: 0x00003946 File Offset: 0x00001B46
		protected virtual void OnInsertComplete(int index, object value)
		{
		}

		/// <summary>When overridden in a derived class, performs additional work before the <see cref="M:System.Web.UI.StateManagedCollection.System.Collections.IList.Remove(System.Object)" /> or <see cref="M:System.Web.UI.StateManagedCollection.System.Collections.IList.RemoveAt(System.Int32)" /> method removes the specified item from the collection.</summary>
		/// <param name="index">The zero-based index of the item to remove, which is used when <see cref="M:System.Web.UI.StateManagedCollection.System.Collections.IList.RemoveAt(System.Int32)" /> is called.</param>
		/// <param name="value">The object to remove from the <see cref="T:System.Web.UI.StateManagedCollection" />, which is used when <see cref="M:System.Web.UI.StateManagedCollection.System.Collections.IList.Remove(System.Object)" /> is called.</param>
		// Token: 0x06001688 RID: 5768 RVA: 0x00003946 File Offset: 0x00001B46
		protected virtual void OnRemove(int index, object value)
		{
		}

		/// <summary>When overridden in a derived class, performs additional work after the <see cref="M:System.Web.UI.StateManagedCollection.System.Collections.IList.Remove(System.Object)" /> or <see cref="M:System.Web.UI.StateManagedCollection.System.Collections.IList.RemoveAt(System.Int32)" /> method removes the specified item from the collection.</summary>
		/// <param name="index">The zero-based index of the item to remove, which is used when <see cref="M:System.Web.UI.StateManagedCollection.System.Collections.IList.RemoveAt(System.Int32)" /> is called.</param>
		/// <param name="value">The object removed from the <see cref="T:System.Web.UI.StateManagedCollection" />, which is used when <see cref="M:System.Web.UI.StateManagedCollection.System.Collections.IList.Remove(System.Object)" /> is called.</param>
		// Token: 0x06001689 RID: 5769 RVA: 0x00003946 File Offset: 0x00001B46
		protected virtual void OnRemoveComplete(int index, object value)
		{
		}

		/// <summary>When overridden in a derived class, validates an element of the <see cref="T:System.Web.UI.StateManagedCollection" /> collection.</summary>
		/// <param name="value">The <see cref="T:System.Web.UI.IStateManager" /> to validate.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x0600168A RID: 5770 RVA: 0x0003A78C File Offset: 0x0003898C
		protected virtual void OnValidate(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
		}

		/// <summary>Restores the previously saved view state of the <see cref="T:System.Web.UI.StateManagedCollection" /> collection and the <see cref="T:System.Web.UI.IStateManager" /> items it contains.</summary>
		/// <param name="savedState">An object that represents the collection and collection elements' state to restore.</param>
		// Token: 0x0600168B RID: 5771 RVA: 0x0003A79C File Offset: 0x0003899C
		void IStateManager.LoadViewState(object savedState)
		{
			if (savedState == null)
			{
				foreach (object obj in this.items)
				{
					((IStateManager)obj).LoadViewState(null);
				}
				return;
			}
			Triplet triplet = savedState as Triplet;
			if (triplet == null)
			{
				throw new InvalidOperationException("Internal error.");
			}
			List<int> list = triplet.First as List<int>;
			List<object> list2 = triplet.Second as List<object>;
			List<object> list3 = triplet.Third as List<object>;
			this.saveEverything = list == null;
			if (this.saveEverything)
			{
				this.Clear();
				int i = 0;
				while (i < list2.Count)
				{
					object obj2 = list3[i];
					IStateManager stateManager;
					if (obj2 is Type)
					{
						stateManager = (IStateManager)Activator.CreateInstance((Type)obj2);
						goto IL_00E3;
					}
					if (obj2 is int)
					{
						stateManager = (IStateManager)this.CreateKnownType((int)obj2);
						goto IL_00E3;
					}
					IL_0102:
					i++;
					continue;
					IL_00E3:
					stateManager.TrackViewState();
					stateManager.LoadViewState(list2[i]);
					((IList)this).Add(stateManager);
					goto IL_0102;
				}
				return;
			}
			for (int j = 0; j < list.Count; j++)
			{
				int num = list[j];
				if (num < this.Count)
				{
					IStateManager stateManager = ((IList)this)[num] as IStateManager;
					stateManager.TrackViewState();
					stateManager.LoadViewState(list2[j]);
				}
				else
				{
					object obj2 = list3[j];
					IStateManager stateManager;
					if (obj2 is Type)
					{
						stateManager = (IStateManager)Activator.CreateInstance((Type)obj2);
					}
					else
					{
						if (!(obj2 is int))
						{
							goto IL_01BA;
						}
						stateManager = (IStateManager)this.CreateKnownType((int)obj2);
					}
					stateManager.TrackViewState();
					stateManager.LoadViewState(list2[j]);
					((IList)this).Add(stateManager);
				}
				IL_01BA:;
			}
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x0003A988 File Offset: 0x00038B88
		private void AddListItem<T>(ref List<T> list, T item)
		{
			if (list == null)
			{
				list = new List<T>();
			}
			list.Add(item);
		}

		/// <summary>Saves the changes to the <see cref="T:System.Web.UI.StateManagedCollection" /> collection and each <see cref="T:System.Web.UI.IStateManager" /> object it contains since the time the page was posted back to the server.</summary>
		/// <returns>The object that contains the changes to the view state of the <see cref="T:System.Web.UI.StateManagedCollection" /> and the items it contains. If no view state is associated with the collection and its elements, this method returns null.</returns>
		// Token: 0x0600168D RID: 5773 RVA: 0x0003A9A0 File Offset: 0x00038BA0
		object IStateManager.SaveViewState()
		{
			Type[] knownTypes = this.GetKnownTypes();
			bool flag = false;
			bool flag2 = knownTypes != null && knownTypes.Length != 0;
			int count = this.items.Count;
			List<int> list = null;
			List<object> list2 = null;
			List<object> list3 = null;
			for (int i = 0; i < count; i++)
			{
				IStateManager stateManager = this.items[i] as IStateManager;
				if (stateManager != null)
				{
					stateManager.TrackViewState();
					object obj = stateManager.SaveViewState();
					if (this.saveEverything || obj != null)
					{
						flag = true;
						Type type = stateManager.GetType();
						int num = (flag2 ? Array.IndexOf<Type>(knownTypes, type) : (-1));
						if (!this.saveEverything)
						{
							this.AddListItem<int>(ref list, i);
						}
						this.AddListItem<object>(ref list2, obj);
						if (num == -1)
						{
							this.AddListItem<object>(ref list3, type);
						}
						else
						{
							this.AddListItem<object>(ref list3, num);
						}
					}
				}
			}
			if (!flag)
			{
				return null;
			}
			return new Triplet(list, list2, list3);
		}

		/// <summary>Causes the <see cref="T:System.Web.UI.StateManagedCollection" /> collection and each of the <see cref="T:System.Web.UI.IStateManager" /> objects it contains to track changes to their view state so they can be persisted across requests for the same page.</summary>
		// Token: 0x0600168E RID: 5774 RVA: 0x0003AA88 File Offset: 0x00038C88
		void IStateManager.TrackViewState()
		{
			this.isTrackingViewState = true;
			if (this.items != null && this.items.Count > 0)
			{
				foreach (object obj in this.items)
				{
					IStateManager stateManager = obj as IStateManager;
					if (stateManager != null)
					{
						stateManager.TrackViewState();
					}
				}
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.StateManagedCollection" /> collection is saving changes to its view state.</summary>
		/// <returns>true if the collection is marked to save its own state and the state of all the <see cref="T:System.Web.UI.IStateManager" /> items it contains; otherwise, false.</returns>
		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x0600168F RID: 5775 RVA: 0x0003AB00 File Offset: 0x00038D00
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.isTrackingViewState;
			}
		}

		/// <summary>Removes all items from the <see cref="T:System.Web.UI.StateManagedCollection" /> collection.</summary>
		// Token: 0x06001690 RID: 5776 RVA: 0x0003AB08 File Offset: 0x00038D08
		public void Clear()
		{
			this.OnClear();
			this.items.Clear();
			this.OnClearComplete();
			if (this.isTrackingViewState)
			{
				this.SetDirty();
			}
		}

		/// <summary>Returns an iterator that iterates through the <see cref="T:System.Web.UI.StateManagedCollection" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the <see cref="T:System.Web.UI.StateManagedCollection" />.</returns>
		// Token: 0x06001691 RID: 5777 RVA: 0x0003AB2F File Offset: 0x00038D2F
		public IEnumerator GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		/// <summary>Copies the elements of the <see cref="T:System.Web.UI.StateManagedCollection" /> collection to an array, starting at a particular array index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the <see cref="T:System.Web.UI.StateManagedCollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.- or -<paramref name="index" /> is greater than or equal to the length of <paramref name="array" />.- or -The number of elements in the source <see cref="T:System.Web.UI.StateManagedCollection" /> is greater than the available space from the <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		// Token: 0x06001692 RID: 5778 RVA: 0x0003AB3C File Offset: 0x00038D3C
		public void CopyTo(Array array, int index)
		{
			this.items.CopyTo(array, index);
		}

		/// <summary>Returns an iterator that iterates through the <see cref="T:System.Web.UI.StateManagedCollection" /> collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the <see cref="T:System.Web.UI.StateManagedCollection" />.</returns>
		// Token: 0x06001693 RID: 5779 RVA: 0x0003AB4B File Offset: 0x00038D4B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Adds an item to the <see cref="T:System.Web.UI.StateManagedCollection" /> collection.</summary>
		/// <returns>The position at which the new element was inserted.</returns>
		/// <param name="value">The object to add to the <see cref="T:System.Web.UI.StateManagedCollection" />.</param>
		/// <exception cref="T:System.ArgumentNullException">The specified <paramref name="value" /> is null.</exception>
		// Token: 0x06001694 RID: 5780 RVA: 0x0003AB54 File Offset: 0x00038D54
		int IList.Add(object value)
		{
			this.OnValidate(value);
			if (this.isTrackingViewState)
			{
				((IStateManager)value).TrackViewState();
				this.SetDirtyObject(value);
			}
			this.OnInsert(-1, value);
			this.items.Add(value);
			this.OnInsertComplete(-1, value);
			return this.Count - 1;
		}

		/// <summary>Inserts an item into the <see cref="T:System.Web.UI.StateManagedCollection" /> collection at the specified index.</summary>
		/// <param name="index">The zero-based index at which <paramref name="value" /> should be inserted.</param>
		/// <param name="value">The object to insert into the <see cref="T:System.Web.UI.StateManagedCollection" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified <paramref name="index" /> is out of range of the collection.</exception>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Web.UI.StateManagedCollection" /> is read-only.</exception>
		/// <exception cref="T:System.ArgumentNullException">The specified <paramref name="value" /> is null.</exception>
		// Token: 0x06001695 RID: 5781 RVA: 0x0003ABA7 File Offset: 0x00038DA7
		void IList.Insert(int index, object value)
		{
			this.OnValidate(value);
			if (this.isTrackingViewState)
			{
				((IStateManager)value).TrackViewState();
				this.SetDirty();
			}
			this.OnInsert(index, value);
			this.items.Insert(index, value);
			this.OnInsertComplete(index, value);
		}

		/// <summary>Removes the first occurrence of the specified object from the <see cref="T:System.Web.UI.StateManagedCollection" /> collection.</summary>
		/// <param name="value">The object to remove from the <see cref="T:System.Web.UI.StateManagedCollection" />.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Web.UI.StateManagedCollection" /> is read-only.</exception>
		// Token: 0x06001696 RID: 5782 RVA: 0x0003ABE8 File Offset: 0x00038DE8
		void IList.Remove(object value)
		{
			if (value == null)
			{
				return;
			}
			this.OnValidate(value);
			int num = ((IList)this).IndexOf(value);
			if (num >= 0)
			{
				((IList)this).RemoveAt(num);
			}
		}

		/// <summary>Removes the <see cref="T:System.Web.UI.IStateManager" /> element at the specified index.</summary>
		/// <param name="index">The zero-based index of the item to remove.</param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Web.UI.StateManagedCollection" /> is read-only.</exception>
		// Token: 0x06001697 RID: 5783 RVA: 0x0003AC18 File Offset: 0x00038E18
		void IList.RemoveAt(int index)
		{
			object obj = this.items[index];
			this.OnRemove(index, obj);
			this.items.RemoveAt(index);
			this.OnRemoveComplete(index, obj);
			if (this.isTrackingViewState)
			{
				this.SetDirty();
			}
		}

		/// <summary>Removes all items from the <see cref="T:System.Web.UI.StateManagedCollection" /> collection.</summary>
		// Token: 0x06001698 RID: 5784 RVA: 0x0003AC5C File Offset: 0x00038E5C
		void IList.Clear()
		{
			this.Clear();
		}

		/// <summary>Determines whether the <see cref="T:System.Web.UI.StateManagedCollection" /> collection contains a specific value.</summary>
		/// <returns>true if the object is found in the <see cref="T:System.Web.UI.StateManagedCollection" />; otherwise, false. If null is passed for the value parameter, false is returned.</returns>
		/// <param name="value">The object to locate in the <see cref="T:System.Web.UI.StateManagedCollection" />.</param>
		// Token: 0x06001699 RID: 5785 RVA: 0x0003AC64 File Offset: 0x00038E64
		bool IList.Contains(object value)
		{
			if (value == null)
			{
				return false;
			}
			this.OnValidate(value);
			return this.items.Contains(value);
		}

		/// <summary>Determines the index of a specified item in the <see cref="T:System.Web.UI.StateManagedCollection" /> collection.</summary>
		/// <returns>The index of <paramref name="value" />, if it is found in the list; otherwise, -1.</returns>
		/// <param name="value">The object to locate in the <see cref="T:System.Web.UI.StateManagedCollection" />.</param>
		// Token: 0x0600169A RID: 5786 RVA: 0x0003AC7E File Offset: 0x00038E7E
		int IList.IndexOf(object value)
		{
			if (value == null)
			{
				return -1;
			}
			this.OnValidate(value);
			return this.items.IndexOf(value);
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Web.UI.StateManagedCollection" /> collection.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Web.UI.StateManagedCollection" />.</returns>
		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x0600169B RID: 5787 RVA: 0x0003AC98 File Offset: 0x00038E98
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Web.UI.StateManagedCollection" /> collection.</summary>
		/// <returns>The number of elements in the <see cref="T:System.Web.UI.StateManagedCollection" />.</returns>
		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x0600169C RID: 5788 RVA: 0x0003AC98 File Offset: 0x00038E98
		int ICollection.Count
		{
			get
			{
				return this.items.Count;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.StateManagedCollection" /> collection is synchronized (thread safe). This method returns false in all cases.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x0600169D RID: 5789 RVA: 0x00008A4D File Offset: 0x00006C4D
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Web.UI.StateManagedCollection" /> collection. This method returns null in all cases.</summary>
		/// <returns>null in all cases.</returns>
		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x0600169E RID: 5790 RVA: 0x00002058 File Offset: 0x00000258
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.StateManagedCollection" /> collection has a fixed size. This method returns false in all cases.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x0600169F RID: 5791 RVA: 0x00008A4D File Offset: 0x00006C4D
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Web.UI.StateManagedCollection" /> collection is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.StateManagedCollection" /> is read-only; otherwise, false.</returns>
		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x060016A0 RID: 5792 RVA: 0x00008A4D File Offset: 0x00006C4D
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.IStateManager" /> element at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified <paramref name="index" /> is out of range of the collection.</exception>
		// Token: 0x1700073D RID: 1853
		object IList.this[int index]
		{
			get
			{
				return this.items[index];
			}
			set
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				this.OnValidate(value);
				if (this.isTrackingViewState)
				{
					((IStateManager)value).TrackViewState();
					this.SetDirty();
				}
				this.items[index] = value;
			}
		}

		// Token: 0x04001578 RID: 5496
		private ArrayList items = new ArrayList();

		// Token: 0x04001579 RID: 5497
		private bool saveEverything;

		// Token: 0x0400157A RID: 5498
		private bool isTrackingViewState;
	}
}

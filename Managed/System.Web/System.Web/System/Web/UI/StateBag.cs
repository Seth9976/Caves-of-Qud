using System;
using System.Collections;
using System.Collections.Specialized;
using System.Security.Permissions;

namespace System.Web.UI
{
	/// <summary>Manages the view state of ASP.NET server controls, including pages. This class cannot be inherited.</summary>
	// Token: 0x02000223 RID: 547
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class StateBag : IDictionary, ICollection, IEnumerable, IStateManager
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.StateBag" /> class that allows stored state values to be case-insensitive.</summary>
		/// <param name="ignoreCase">true to ignore case; otherwise, false. </param>
		// Token: 0x06001654 RID: 5716 RVA: 0x0003A324 File Offset: 0x00038524
		public StateBag(bool ignoreCase)
		{
			this.ht = new HybridDictionary(ignoreCase);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.StateBag" /> class. This is the default constructor for this class.</summary>
		// Token: 0x06001655 RID: 5717 RVA: 0x0003A338 File Offset: 0x00038538
		public StateBag()
			: this(false)
		{
		}

		/// <summary>Restores the previously saved view state of the <see cref="T:System.Web.UI.StateBag" /> object.</summary>
		/// <param name="state">An object that represents the <see cref="T:System.Web.UI.StateBag" /> state to restore. </param>
		// Token: 0x06001656 RID: 5718 RVA: 0x0003A341 File Offset: 0x00038541
		void IStateManager.LoadViewState(object savedState)
		{
			this.LoadViewState(savedState);
		}

		/// <summary>Saves the changes to the <see cref="T:System.Web.UI.StateBag" /> object since the time the page was posted back to the server.</summary>
		/// <returns>The object that contains the changes to the view state of the <see cref="T:System.Web.UI.StateBag" />. If there are no changes, or there are no <see cref="T:System.Web.UI.StateItem" /> elements in the <see cref="T:System.Web.UI.StateBag" />, this method returns null.</returns>
		// Token: 0x06001657 RID: 5719 RVA: 0x0003A34A File Offset: 0x0003854A
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		/// <summary>Causes the <see cref="T:System.Web.UI.StateBag" /> object to track changes to its state so that it can be persisted across requests.</summary>
		// Token: 0x06001658 RID: 5720 RVA: 0x0003A352 File Offset: 0x00038552
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		/// <summary>Gets a value indicating whether state changes are being tracked.</summary>
		/// <returns>true if the <see cref="T:System.Web.UI.StateBag" /> is marked to save changes to its state; otherwise, false.</returns>
		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06001659 RID: 5721 RVA: 0x0003A35A File Offset: 0x0003855A
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.track;
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x0600165A RID: 5722 RVA: 0x0003A35A File Offset: 0x0003855A
		internal bool IsTrackingViewState
		{
			get
			{
				return this.track;
			}
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x0003A364 File Offset: 0x00038564
		internal void LoadViewState(object savedState)
		{
			if (savedState == null)
			{
				return;
			}
			foreach (object obj in ((Hashtable)savedState))
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				this.Add((string)dictionaryEntry.Key, dictionaryEntry.Value);
			}
		}

		// Token: 0x0600165C RID: 5724 RVA: 0x0003A3D4 File Offset: 0x000385D4
		internal object SaveViewState()
		{
			Hashtable hashtable = null;
			foreach (object obj in this.ht)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				StateItem stateItem = (StateItem)dictionaryEntry.Value;
				if (stateItem.IsDirty)
				{
					if (hashtable == null)
					{
						hashtable = new Hashtable();
					}
					hashtable.Add(dictionaryEntry.Key, stateItem.Value);
				}
			}
			return hashtable;
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x0003A460 File Offset: 0x00038660
		internal void TrackViewState()
		{
			this.track = true;
		}

		/// <summary>Adds a new <see cref="T:System.Web.UI.StateItem" /> object to the <see cref="T:System.Web.UI.StateBag" /> object. If the item already exists in the <see cref="T:System.Web.UI.StateBag" /> object, this method updates the value of the item.</summary>
		/// <returns>Returns a <see cref="T:System.Web.UI.StateItem" /> that represents the object added to view state.</returns>
		/// <param name="key">The attribute name for the <see cref="T:System.Web.UI.StateItem" />. </param>
		/// <param name="value">The value of the item to add to the <see cref="T:System.Web.UI.StateBag" />. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="key" /> is null.- or -The number of characters in <paramref name="key" /> is 0. </exception>
		// Token: 0x0600165E RID: 5726 RVA: 0x0003A46C File Offset: 0x0003866C
		public StateItem Add(string key, object value)
		{
			StateItem stateItem = this.ht[key] as StateItem;
			if (stateItem == null)
			{
				stateItem = (this.ht[key] = new StateItem(value));
			}
			stateItem.Value = value;
			stateItem.IsDirty |= this.track;
			return stateItem;
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x0003A4C0 File Offset: 0x000386C0
		internal string GetString(string key, string def)
		{
			string text = (string)this[key];
			if (text != null)
			{
				return text;
			}
			return def;
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x0003A4E0 File Offset: 0x000386E0
		internal bool GetBool(string key, bool def)
		{
			object obj = this[key];
			if (obj != null)
			{
				return (bool)obj;
			}
			return def;
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x0003A500 File Offset: 0x00038700
		internal char GetChar(string key, char def)
		{
			object obj = this[key];
			if (obj != null)
			{
				return (char)obj;
			}
			return def;
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x0003A520 File Offset: 0x00038720
		internal int GetInt(string key, int def)
		{
			object obj = this[key];
			if (obj != null)
			{
				return (int)obj;
			}
			return def;
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x0003A540 File Offset: 0x00038740
		internal short GetShort(string key, short def)
		{
			object obj = this[key];
			if (obj != null)
			{
				return (short)obj;
			}
			return def;
		}

		/// <summary>Removes all items from the current <see cref="T:System.Web.UI.StateBag" /> object.</summary>
		// Token: 0x06001664 RID: 5732 RVA: 0x0003A560 File Offset: 0x00038760
		public void Clear()
		{
			this.ht.Clear();
		}

		/// <summary>Returns an enumerator that iterates over all the key/value pairs of the <see cref="T:System.Web.UI.StateItem" /> objects stored in the <see cref="T:System.Web.UI.StateBag" /> object.</summary>
		/// <returns>The enumerator to iterate through the state bag.</returns>
		// Token: 0x06001665 RID: 5733 RVA: 0x0003A56D File Offset: 0x0003876D
		public IDictionaryEnumerator GetEnumerator()
		{
			return this.ht.GetEnumerator();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IEnumerable.GetEnumerator" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06001666 RID: 5734 RVA: 0x0003A57A File Offset: 0x0003877A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Checks a <see cref="T:System.Web.UI.StateItem" /> object stored in the <see cref="T:System.Web.UI.StateBag" /> object to evaluate whether it has been modified since the call to <see cref="M:System.Web.UI.Control.TrackViewState" />.</summary>
		/// <returns>true if the item has been modified; otherwise, false.</returns>
		/// <param name="key">The key of the item to check. </param>
		// Token: 0x06001667 RID: 5735 RVA: 0x0003A584 File Offset: 0x00038784
		public bool IsItemDirty(string key)
		{
			StateItem stateItem = this.ht[key] as StateItem;
			return stateItem != null && stateItem.IsDirty;
		}

		/// <summary>Removes the specified key/value pair from the <see cref="T:System.Web.UI.StateBag" /> object.</summary>
		/// <param name="key">The item to remove. </param>
		// Token: 0x06001668 RID: 5736 RVA: 0x0003A5AE File Offset: 0x000387AE
		public void Remove(string key)
		{
			this.ht.Remove(key);
		}

		/// <summary>Sets the <see cref="P:System.Web.SessionState.ISessionStateItemCollection.Dirty" /> property for the specified <see cref="T:System.Web.UI.StateItem" /> object in the <see cref="T:System.Web.UI.StateBag" /> object.</summary>
		/// <param name="key">The key that identifies which <see cref="T:System.Web.UI.StateItem" /> in the <see cref="T:System.Web.UI.StateBag" /> to set. </param>
		/// <param name="dirty">true to mark the state of the item as modified; otherwise, false.</param>
		// Token: 0x06001669 RID: 5737 RVA: 0x0003A5BC File Offset: 0x000387BC
		public void SetItemDirty(string key, bool dirty)
		{
			StateItem stateItem = (StateItem)this.ht[key];
			if (stateItem != null)
			{
				stateItem.IsDirty = dirty;
			}
		}

		/// <summary>Gets the number of <see cref="T:System.Web.UI.StateItem" /> objects in the <see cref="T:System.Web.UI.StateBag" /> object.</summary>
		/// <returns>The number of items in the <see cref="T:System.Web.UI.StateBag" />.</returns>
		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x0600166A RID: 5738 RVA: 0x0003A5E5 File Offset: 0x000387E5
		public int Count
		{
			get
			{
				return this.ht.Count;
			}
		}

		/// <summary>Gets or sets the value of an item stored in the <see cref="T:System.Web.UI.StateBag" /> object.</summary>
		/// <returns>The specified item in the <see cref="T:System.Web.UI.StateBag" /> object.</returns>
		/// <param name="key">The key for the item. </param>
		// Token: 0x1700072C RID: 1836
		public object this[string key]
		{
			get
			{
				StateItem stateItem = this.ht[key] as StateItem;
				if (stateItem != null)
				{
					return stateItem.Value;
				}
				return null;
			}
			set
			{
				if (value == null && !this.IsTrackingViewState)
				{
					this.Remove(key);
					return;
				}
				this.Add(key, value);
			}
		}

		/// <summary>Gets a collection of keys representing the items in the <see cref="T:System.Web.UI.StateBag" /> object.</summary>
		/// <returns>The collection of keys.</returns>
		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x0600166D RID: 5741 RVA: 0x0003A63C File Offset: 0x0003883C
		public ICollection Keys
		{
			get
			{
				return this.ht.Keys;
			}
		}

		/// <summary>Gets a collection of the view-state values stored in the <see cref="T:System.Web.UI.StateBag" /> object.</summary>
		/// <returns>The collection of view-state values.</returns>
		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x0600166E RID: 5742 RVA: 0x0003A649 File Offset: 0x00038849
		public ICollection Values
		{
			get
			{
				return this.ht.Values;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IDictionary.Add(System.Object,System.Object)" />.</summary>
		/// <param name="key">The <see cref="T:System.Object" /> to use as the key of the element to add.</param>
		/// <param name="value">The <see cref="T:System.Object" /> to use as the value of the element to add. </param>
		// Token: 0x0600166F RID: 5743 RVA: 0x0003A656 File Offset: 0x00038856
		void IDictionary.Add(object key, object value)
		{
			this.Add((string)key, value);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IDictionary.Remove(System.Object)" />.</summary>
		/// <param name="key">The key of the element to remove. </param>
		// Token: 0x06001670 RID: 5744 RVA: 0x0003A666 File Offset: 0x00038866
		void IDictionary.Remove(object key)
		{
			this.Remove((string)key);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" />.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x06001671 RID: 5745 RVA: 0x0003A674 File Offset: 0x00038874
		void ICollection.CopyTo(Array array, int index)
		{
			this.ht.CopyTo(array, index);
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IDictionary.Contains(System.Object)" />.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> contains an element with the key; otherwise, false.</returns>
		/// <param name="key">The key to locate in the <see cref="T:System.Collections.IDictionary" /> object.</param>
		// Token: 0x06001672 RID: 5746 RVA: 0x0003A683 File Offset: 0x00038883
		bool IDictionary.Contains(object key)
		{
			return this.ht.Contains(key);
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.IsSynchronized" />.</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe); otherwise, false.</returns>
		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06001673 RID: 5747 RVA: 0x00008A4D File Offset: 0x00006C4D
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.ICollection.SyncRoot" />.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.</returns>
		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06001674 RID: 5748 RVA: 0x0003A691 File Offset: 0x00038891
		object ICollection.SyncRoot
		{
			get
			{
				return this.ht;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IDictionary.Item(System.Object)" />.</summary>
		/// <returns>The element with the specified <paramref name="key" />.</returns>
		/// <param name="key">The key of the element to get.</param>
		// Token: 0x17000731 RID: 1841
		object IDictionary.this[object key]
		{
			get
			{
				return this[(string)key];
			}
			set
			{
				this[(string)key] = value;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IDictionary.IsFixedSize" />.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> object has a fixed size; otherwise, false.</returns>
		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06001677 RID: 5751 RVA: 0x00008A4D File Offset: 0x00006C4D
		bool IDictionary.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IDictionary.IsReadOnly" />.</summary>
		/// <returns>true if the <see cref="T:System.Collections.IDictionary" /> object is read-only; otherwise, false.</returns>
		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x06001678 RID: 5752 RVA: 0x00008A4D File Offset: 0x00006C4D
		bool IDictionary.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Sets the state of the <see cref="T:System.Web.UI.StateBag" /> object as well as the <see cref="P:System.Web.SessionState.ISessionStateItemCollection.Dirty" /> property of each of the <see cref="T:System.Web.UI.StateItem" /> objects contained by it.</summary>
		/// <param name="dirty">true to mark the state of the collection and its items as modified; otherwise, false.</param>
		// Token: 0x06001679 RID: 5753 RVA: 0x0003A6B8 File Offset: 0x000388B8
		public void SetDirty(bool dirty)
		{
			foreach (object obj in this.ht)
			{
				((StateItem)((DictionaryEntry)obj).Value).IsDirty = dirty;
			}
		}

		// Token: 0x04001574 RID: 5492
		private HybridDictionary ht;

		// Token: 0x04001575 RID: 5493
		private bool track;
	}
}

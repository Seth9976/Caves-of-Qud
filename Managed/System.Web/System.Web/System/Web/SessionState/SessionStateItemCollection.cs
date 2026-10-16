using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Web.Util;

namespace System.Web.SessionState
{
	/// <summary>A collection of objects stored in session state. This class cannot be inherited.</summary>
	// Token: 0x0200049C RID: 1180
	public sealed class SessionStateItemCollection : NameObjectCollectionBase, ISessionStateItemCollection, ICollection, IEnumerable
	{
		// Token: 0x06003585 RID: 13701 RVA: 0x0008ABEC File Offset: 0x00088DEC
		private static bool IsMutable(object o)
		{
			return o != null && Type.GetTypeCode(o.GetType()) == TypeCode.Object;
		}

		/// <summary>Creates a new, empty <see cref="T:System.Web.SessionState.SessionStateItemCollection" /> object.</summary>
		// Token: 0x06003586 RID: 13702 RVA: 0x00006641 File Offset: 0x00004841
		public SessionStateItemCollection()
		{
		}

		// Token: 0x06003587 RID: 13703 RVA: 0x0008AC01 File Offset: 0x00088E01
		internal SessionStateItemCollection(int capacity)
			: base(capacity)
		{
		}

		/// <summary>Gets or sets a value indicating whether the collection has been marked as changed.</summary>
		/// <returns>true if the <see cref="T:System.Web.SessionState.SessionStateItemCollection" /> contents have been changed; otherwise, false.</returns>
		// Token: 0x170010F3 RID: 4339
		// (get) Token: 0x06003588 RID: 13704 RVA: 0x0008AC0A File Offset: 0x00088E0A
		// (set) Token: 0x06003589 RID: 13705 RVA: 0x0008AC12 File Offset: 0x00088E12
		public bool Dirty
		{
			get
			{
				return this.is_dirty;
			}
			set
			{
				this.is_dirty = value;
			}
		}

		/// <summary>Gets or sets a value in the collection by numerical index.</summary>
		/// <returns>The value in the collection stored at the specified index. If the specified key is not found, attempting to get it returns null, and attempting to set it creates a new element using the specified key.</returns>
		/// <param name="index">The numerical index of the value in the collection.</param>
		// Token: 0x170010F4 RID: 4340
		public object this[int index]
		{
			get
			{
				object obj = base.BaseGet(index);
				if (SessionStateItemCollection.IsMutable(obj))
				{
					this.is_dirty = true;
				}
				return obj;
			}
			set
			{
				base.BaseSet(index, value);
				this.is_dirty = true;
			}
		}

		/// <summary>Gets or sets a value in the collection by name.</summary>
		/// <returns>The value in the collection with the specified name. If the specified key is not found, attempting to get it returns null, and attempting to set it creates a new element using the specified key.</returns>
		/// <param name="name">The key name of the value in the collection.</param>
		// Token: 0x170010F5 RID: 4341
		public object this[string name]
		{
			get
			{
				object obj = base.BaseGet(name);
				if (SessionStateItemCollection.IsMutable(obj))
				{
					this.is_dirty = true;
				}
				return obj;
			}
			set
			{
				base.BaseSet(name, value);
				this.is_dirty = true;
			}
		}

		/// <summary>Gets a collection of the variable names for all values stored in the collection.</summary>
		/// <returns>The <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> collection that contains all the collection keys. </returns>
		// Token: 0x170010F6 RID: 4342
		// (get) Token: 0x0600358E RID: 13710 RVA: 0x0008AC6D File Offset: 0x00088E6D
		public override NameObjectCollectionBase.KeysCollection Keys
		{
			get
			{
				return base.Keys;
			}
		}

		/// <summary>Removes all values and keys from the session-state collection.</summary>
		// Token: 0x0600358F RID: 13711 RVA: 0x0008AC75 File Offset: 0x00088E75
		public void Clear()
		{
			if (this.Count > 0)
			{
				base.BaseClear();
				this.is_dirty = true;
			}
		}

		/// <summary>Creates a <see cref="T:System.Web.SessionState.SessionStateItemCollection" /> collection from a storage location that is written to using the <see cref="M:System.Web.SessionState.SessionStateItemCollection.Serialize(System.IO.BinaryWriter)" /> method.</summary>
		/// <returns>A <see cref="T:System.Web.SessionState.SessionStateItemCollection" /> collection populated with the contents from a storage location that is written to using the <see cref="M:System.Web.SessionState.SessionStateItemCollection.Serialize(System.IO.BinaryWriter)" /> method.</returns>
		/// <param name="reader">The <see cref="T:System.IO.BinaryReader" /> used to read the serialized collection from a stream or encoded string.</param>
		/// <exception cref="T:System.Web.HttpException">The session state information is invalid or corrupted</exception>
		// Token: 0x06003590 RID: 13712 RVA: 0x0008AC90 File Offset: 0x00088E90
		public static SessionStateItemCollection Deserialize(BinaryReader reader)
		{
			int i = reader.ReadInt32();
			SessionStateItemCollection sessionStateItemCollection = new SessionStateItemCollection(i);
			while (i > 0)
			{
				sessionStateItemCollection[reader.ReadString()] = AltSerialization.Deserialize(reader);
				i--;
			}
			return sessionStateItemCollection;
		}

		/// <summary>Writes the contents of the collection to a <see cref="T:System.IO.BinaryWriter" />.</summary>
		/// <param name="writer">The <see cref="T:System.IO.BinaryWriter" /> used to write the serialized collection to a stream or encoded string.</param>
		// Token: 0x06003591 RID: 13713 RVA: 0x0008ACC8 File Offset: 0x00088EC8
		public void Serialize(BinaryWriter writer)
		{
			writer.Write(this.Count);
			foreach (object obj in base.Keys)
			{
				string text = (string)obj;
				writer.Write(text);
				AltSerialization.Serialize(writer, base.BaseGet(text));
			}
		}

		/// <summary>Returns an enumerator that can be used to read all the key names in the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can iterate through the variable names in the session-state collection.</returns>
		// Token: 0x06003592 RID: 13714 RVA: 0x0008AD3C File Offset: 0x00088F3C
		public override IEnumerator GetEnumerator()
		{
			return base.GetEnumerator();
		}

		/// <summary>Deletes an item from the collection.</summary>
		/// <param name="name">The name of the item to delete from the collection. </param>
		// Token: 0x06003593 RID: 13715 RVA: 0x0008AD44 File Offset: 0x00088F44
		public void Remove(string name)
		{
			base.BaseRemove(name);
			this.is_dirty = true;
		}

		/// <summary>Deletes an item at a specified index from the collection.</summary>
		/// <param name="index">The index of the item to remove from the collection. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.- or -<paramref name="index" /> is equal to or greater than <see cref="P:System.Collections.ICollection.Count" />.</exception>
		// Token: 0x06003594 RID: 13716 RVA: 0x0008AD54 File Offset: 0x00088F54
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
			this.is_dirty = true;
		}

		// Token: 0x04001D6F RID: 7535
		private bool is_dirty;
	}
}

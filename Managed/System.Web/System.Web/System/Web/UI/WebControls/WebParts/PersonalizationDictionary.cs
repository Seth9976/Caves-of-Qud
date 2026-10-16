using System;
using System.Collections;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Encapsulates basic validation rules that indicate whether a piece of custom data is considered personalizable in either <see cref="F:System.Web.UI.WebControls.WebParts.PersonalizationScope.User" /> or <see cref="F:System.Web.UI.WebControls.WebParts.PersonalizationScope.Shared" /> scope.</summary>
	// Token: 0x020006D6 RID: 1750
	public class PersonalizationDictionary : ICollection, IEnumerable, IDictionary
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationDictionary" /> class. </summary>
		// Token: 0x06004A22 RID: 18978 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public PersonalizationDictionary()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationDictionary" /> class using the provided parameter. </summary>
		/// <param name="initialSize">The estimated number of entries to be stored in the dictionary.</param>
		// Token: 0x06004A23 RID: 18979 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public PersonalizationDictionary(int initialSize)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the number of entries in the dictionary.</summary>
		/// <returns>The number of entries in the dictionary.</returns>
		// Token: 0x170016F5 RID: 5877
		// (get) Token: 0x06004A24 RID: 18980 RVA: 0x000C7DCC File Offset: 0x000C5FCC
		public virtual int Count
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets whether the personalization dictionary is of a fixed size.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x170016F6 RID: 5878
		// (get) Token: 0x06004A25 RID: 18981 RVA: 0x000C7DE8 File Offset: 0x000C5FE8
		public virtual bool IsFixedSize
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets whether the personalization dictionary is read-only.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x170016F7 RID: 5879
		// (get) Token: 0x06004A26 RID: 18982 RVA: 0x000C7E04 File Offset: 0x000C6004
		public virtual bool IsReadOnly
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets whether the personalization dictionary is synchronized.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x170016F8 RID: 5880
		// (get) Token: 0x06004A27 RID: 18983 RVA: 0x000C7E20 File Offset: 0x000C6020
		public virtual bool IsSynchronized
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		/// <summary>Gets or sets an entry in the personalization dictionary. </summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationEntry" /> representing custom personalization information identified by the <paramref name="key" /> parameter.</returns>
		/// <param name="key">The key of the entry to be retrieved or changed.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="key" /> is either an empty string (""), or trimming <paramref name="key" /> results in an empty string.</exception>
		// Token: 0x170016F9 RID: 5881
		public virtual PersonalizationEntry this[string key]
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

		/// <summary>Gets a reference to an <see cref="T:System.Collections.ICollection" /> object containing the keys for the personalization dictionary.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the keys for the personalization dictionary.</returns>
		// Token: 0x170016FA RID: 5882
		// (get) Token: 0x06004A2A RID: 18986 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public virtual ICollection Keys
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the personalization dictionary.</summary>
		/// <returns>An object that can be used to synchronize access to the personalization dictionary.</returns>
		// Token: 0x170016FB RID: 5883
		// (get) Token: 0x06004A2B RID: 18987 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public virtual object SyncRoot
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		// Token: 0x06004A2C RID: 18988 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		object IDictionary.get_Item(object key)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		// Token: 0x06004A2D RID: 18989 RVA: 0x0000B3D8 File Offset: 0x000095D8
		void IDictionary.set_Item(object key, object value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a reference to an <see cref="T:System.Collections.ICollection" /> object containing the values in the personalization dictionary.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the values for the personalization dictionary.</returns>
		// Token: 0x170016FC RID: 5884
		// (get) Token: 0x06004A2E RID: 18990 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public virtual ICollection Values
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Adds personalization entries to the personalization dictionary.</summary>
		/// <param name="key">The unique identifier for a piece of state information.</param>
		/// <param name="value">A piece of state information to be added to the personalization dictionary. This value can be null.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="key" /> is a duplicate of a key already in the dictionary.- or -<paramref name="key" /> is either an empty string ("") or trimming <paramref name="key" /> results in an empty string.- or -<paramref name="value" /> is not a <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationEntry" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.- or - <paramref name="value" /> is null.</exception>
		// Token: 0x06004A2F RID: 18991 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual void Add(string key, PersonalizationEntry value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Removes all custom state information from the current <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationDictionary" /> object.</summary>
		// Token: 0x06004A30 RID: 18992 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual void Clear()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns a value indicating whether a piece of custom state information with the same key is contained in the personalization dictionary.</summary>
		/// <returns>true if the provided key matches a key in the personalization dictionary; otherwise, false.</returns>
		/// <param name="key">A key value.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="key" /> is either an empty string (""), or trimming <paramref name="key" /> results in an empty string.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		// Token: 0x06004A31 RID: 18993 RVA: 0x000C7E3C File Offset: 0x000C603C
		public virtual bool Contains(string key)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Copies the personalization dictionary state entries as <see cref="T:System.Collections.DictionaryEntry" /> instances into the specified array.</summary>
		/// <param name="array">The array the <see cref="T:System.Collections.DictionaryEntry" /> instances are copied into.</param>
		/// <param name="index">The index location at which to begin copying.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional.- or -<paramref name="array" /> is the wrong size based on the <paramref name="index" /> parameter.- or - <paramref name="array" /> has insufficient capacity to hold the values contained in the personalization dictionary.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		// Token: 0x06004A32 RID: 18994 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual void CopyTo(DictionaryEntry[] array, int index)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns an enumerator that can be used to iterate through the entries in the personalization dictionary.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionaryEnumerator" /> for the personalization dictionary.</returns>
		// Token: 0x06004A33 RID: 18995 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public virtual IDictionaryEnumerator GetEnumerator()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Removes a custom state entry based on the provided key.</summary>
		/// <param name="key">The key of the entry to be removed.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="key" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="key" /> is either an empty string (""), or trimming <paramref name="key" /> results in an empty string.</exception>
		// Token: 0x06004A34 RID: 18996 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public virtual void Remove(string key)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Implements the <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" /> method for the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationDictionary" /> class.</summary>
		/// <param name="array">An <see cref="T:System.Array" /> of <see cref="T:System.Collections.DictionaryEntry" /> items to copy into a <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationDictionary" />.</param>
		/// <param name="index">The starting point in a <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationDictionary" /> at which to insert <paramref name="array" />. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is not an array of <see cref="T:System.Collections.DictionaryEntry" /> items.</exception>
		// Token: 0x06004A35 RID: 18997 RVA: 0x0000B3D8 File Offset: 0x000095D8
		void ICollection.CopyTo(Array array, int index)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Implements the <see cref="M:System.Collections.IDictionary.Add(System.Object,System.Object)" /> method for the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationDictionary" /> class. </summary>
		/// <param name="key">The <see cref="T:System.String" /> to use as the key for an item in the personalization dictionary. </param>
		/// <param name="value">The <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationEntry" /> to add to the personalization dictionary. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="key" /> is not a <see cref="T:System.String" /> object.- or -<paramref name="value" /> is not a <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationEntry" /> object.</exception>
		// Token: 0x06004A36 RID: 18998 RVA: 0x0000B3D8 File Offset: 0x000095D8
		void IDictionary.Add(object key, object value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Implements the <see cref="M:System.Collections.IDictionary.Contains(System.Object)" /> method for the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationDictionary" /> class.</summary>
		/// <returns>true if <paramref name="key" /> exists in the personalization dictionary; otherwise false.</returns>
		/// <param name="key">A <see cref="T:System.String" /> object that identifies a particular <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationEntry" /> to check for existence in the personalization dictionary.  </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="key" /> is not a <see cref="T:System.String" />.</exception>
		// Token: 0x06004A37 RID: 18999 RVA: 0x000C7E58 File Offset: 0x000C6058
		bool IDictionary.Contains(object key)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Implements the <see cref="M:System.Collections.IDictionary.Remove(System.Object)" /> method for the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationDictionary" /> class.</summary>
		/// <param name="key">A <see cref="T:System.String" /> that identifies a particular <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationEntry" /> object to remove from the personalization dictionary. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="key" /> is not a <see cref="T:System.String" />.</exception>
		// Token: 0x06004A38 RID: 19000 RVA: 0x0000B3D8 File Offset: 0x000095D8
		void IDictionary.Remove(object key)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Implements the <see cref="M:System.Collections.IEnumerable.GetEnumerator" /> method for the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationDictionary" /> class.</summary>
		/// <returns>An instance of <see cref="T:System.Collections.IEnumerator" /> to enumerate through the items in a personalization dictionary.</returns>
		// Token: 0x06004A39 RID: 19001 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		IEnumerator IEnumerable.GetEnumerator()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}

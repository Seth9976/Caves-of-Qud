using System;
using System.Collections;
using Unity;

namespace System.Web.UI.WebControls.WebParts
{
	/// <summary>Provides a collection of <see cref="T:System.Web.UI.WebControls.WebParts.SharedPersonalizationStateInfo" /> and <see cref="T:System.Web.UI.WebControls.WebParts.UserPersonalizationStateInfo" /> objects.</summary>
	// Token: 0x020007AF RID: 1967
	[Serializable]
	public sealed class PersonalizationStateInfoCollection : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationStateInfoCollection" /> class. </summary>
		// Token: 0x06004F74 RID: 20340 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public PersonalizationStateInfoCollection()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the number of items in a collection.</summary>
		/// <returns>The number of items in a collection.</returns>
		// Token: 0x1700183A RID: 6202
		// (get) Token: 0x06004F75 RID: 20341 RVA: 0x000C8EDC File Offset: 0x000C70DC
		public int Count
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationStateInfoCollection" /> collection is synchronized (thread safe).</summary>
		/// <returns>true if access to the collection is synchronized; otherwise, false. The value is always false for <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationStateInfoCollection" /> objects created by the Web Parts control set.</returns>
		// Token: 0x1700183B RID: 6203
		// (get) Token: 0x06004F76 RID: 20342 RVA: 0x000C8EF8 File Offset: 0x000C70F8
		public bool IsSynchronized
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
		}

		// Token: 0x06004F77 RID: 20343 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public PersonalizationStateInfo get_Item(int index)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets an element from the collection based on the specified parameters. </summary>
		/// <returns>An element from the collection based on the specified parameters.</returns>
		/// <param name="path">The relative application path of the personalization state object to be retrieved.</param>
		/// <param name="username">The user name of the <see cref="T:System.Web.UI.WebControls.WebParts.UserPersonalizationStateInfo" /> object to be retrieved.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null.</exception>
		// Token: 0x1700183C RID: 6204
		public PersonalizationStateInfo this[string path, string username]
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationStateInfoCollection" /> instance.</summary>
		/// <returns>An object that can be used to synchronize access to the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationStateInfoCollection" />. The Web Parts control set returns a reference to the current collection object.</returns>
		// Token: 0x1700183D RID: 6205
		// (get) Token: 0x06004F79 RID: 20345 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public object SyncRoot
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Adds a <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationStateInfo" />-derived instance to the end of the collection.</summary>
		/// <param name="data">The <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationStateInfo" />-derived instance to be added.</param>
		/// <exception cref="T:System.NotSupportedException">The collection was marked as read-only.</exception>
		/// <exception cref="T:System.ArgumentException">An attempt was made to add an object to the collection when an instance of the same shared or per-user state already exists in the collection.</exception>
		/// <exception cref="T:System.ArgumentNullException">The data parameter is null.</exception>
		// Token: 0x06004F7A RID: 20346 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void Add(PersonalizationStateInfo data)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Clears the collection of all items. </summary>
		/// <exception cref="T:System.NotSupportedException">The collection was marked as read-only.</exception>
		// Token: 0x06004F7B RID: 20347 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void Clear()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Copies the elements of the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationStateInfoCollection" /> collection into a <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationStateInfo" /> array, starting at the specified position.</summary>
		/// <param name="array">The array the elements in the collection are copied into.</param>
		/// <param name="index">The location at which to start the copy operation.</param>
		// Token: 0x06004F7C RID: 20348 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void CopyTo(PersonalizationStateInfo[] array, int index)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Returns a standard enumerator capable of iterating over the collection. This method cannot be inherited.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		// Token: 0x06004F7D RID: 20349 RVA: 0x0000E7D3 File Offset: 0x0000C9D3
		public IEnumerator GetEnumerator()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Removes a <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationStateInfo" />-derived object from the collection.</summary>
		/// <param name="path">The relative application path of the personalization state object to be removed.</param>
		/// <param name="username">The user name of the <see cref="T:System.Web.UI.WebControls.WebParts.UserPersonalizationStateInfo" />-derived object to be removed.</param>
		/// <exception cref="T:System.NotSupportedException">The collection was marked as read-only.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null.- or -both parameters are null.</exception>
		// Token: 0x06004F7E RID: 20350 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void Remove(string path, string username)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Marks the collection as read-only.</summary>
		// Token: 0x06004F7F RID: 20351 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public void SetReadOnly()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Implements the base <see cref="M:System.Collections.ICollection.CopyTo(System.Array,System.Int32)" /> method. </summary>
		/// <param name="array">The array into which a collection of <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationStateInfo" />  objects will be copied.</param>
		/// <param name="index">The point in <paramref name="array" /> at which to start copying the <see cref="T:System.Web.UI.WebControls.WebParts.PersonalizationStateInfo" />  objects. </param>
		// Token: 0x06004F80 RID: 20352 RVA: 0x0000B3D8 File Offset: 0x000095D8
		void ICollection.CopyTo(Array array, int index)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}

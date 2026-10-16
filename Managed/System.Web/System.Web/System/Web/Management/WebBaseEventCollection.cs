using System;
using System.Collections;
using Unity;

namespace System.Web.Management
{
	/// <summary>Contains a collection of <see cref="T:System.Web.Management.WebBaseEvent" /> objects. This class cannot be inherited. </summary>
	// Token: 0x02000740 RID: 1856
	public sealed class WebBaseEventCollection : ReadOnlyCollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.WebBaseEventCollection" /> class. </summary>
		/// <param name="events">The collection of <see cref="T:System.Web.Management.WebBaseEvent" /> objects.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="events" /> is null.</exception>
		// Token: 0x06004C67 RID: 19559 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public WebBaseEventCollection(ICollection events)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the <see cref="T:System.Web.Management.WebBaseEvent" /> object at the specified index.</summary>
		/// <returns>The <see cref="T:System.Web.Management.WebBaseEvent" /> object at the specified index.</returns>
		/// <param name="index">The collection index of the <see cref="T:System.Web.Management.WebBaseEvent" /> object you want to retrieve.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified index is outside the range of the collection.</exception>
		// Token: 0x17001793 RID: 6035
		public WebBaseEvent this[int index]
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Indicates whether the collection contains the specified <see cref="T:System.Web.Management.WebBaseEvent" /> object.</summary>
		/// <returns>true if the collection contains the specified element; otherwise, false. </returns>
		/// <param name="value">The <see cref="T:System.Web.Management.WebBaseEvent" /> object to search for.</param>
		// Token: 0x06004C69 RID: 19561 RVA: 0x000C85E4 File Offset: 0x000C67E4
		public bool Contains(WebBaseEvent value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return default(bool);
		}

		/// <summary>Gets the index of the specified <see cref="T:System.Web.Management.WebBaseEvent" /> object.</summary>
		/// <returns>The index of the specified <see cref="T:System.Web.Management.WebBaseEvent" /> object within the collection.</returns>
		/// <param name="value">The <see cref="T:System.Web.Management.WebBaseEvent" /> object for which to obtain the index.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The object is not in the collection.</exception>
		// Token: 0x06004C6A RID: 19562 RVA: 0x000C8600 File Offset: 0x000C6800
		public int IndexOf(WebBaseEvent value)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}
	}
}

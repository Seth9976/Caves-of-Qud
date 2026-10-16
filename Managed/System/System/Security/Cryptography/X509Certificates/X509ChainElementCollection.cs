using System;
using System.Collections;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Represents a collection of <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElement" /> objects. This class cannot be inherited.</summary>
	// Token: 0x020002DE RID: 734
	public sealed class X509ChainElementCollection : ICollection, IEnumerable
	{
		// Token: 0x06001715 RID: 5909 RVA: 0x0005B6BA File Offset: 0x000598BA
		internal X509ChainElementCollection()
		{
			this._list = new ArrayList();
		}

		/// <summary>Gets the number of elements in the collection.</summary>
		/// <returns>An integer representing the number of elements in the collection.</returns>
		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06001716 RID: 5910 RVA: 0x0005B6CD File Offset: 0x000598CD
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		/// <summary>Gets a value indicating whether the collection of chain elements is synchronized.</summary>
		/// <returns>Always returns false.</returns>
		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06001717 RID: 5911 RVA: 0x0005B6DA File Offset: 0x000598DA
		public bool IsSynchronized
		{
			get
			{
				return this._list.IsSynchronized;
			}
		}

		/// <summary>Gets the <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElement" /> object at the specified index.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElement" /> object.</returns>
		/// <param name="index">An integer value. </param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is greater than or equal to the length of the collection. </exception>
		// Token: 0x17000473 RID: 1139
		public X509ChainElement this[int index]
		{
			get
			{
				return (X509ChainElement)this._list[index];
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to an <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementCollection" /> object.</summary>
		/// <returns>A pointer reference to the current object.</returns>
		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06001719 RID: 5913 RVA: 0x0005B6FA File Offset: 0x000598FA
		public object SyncRoot
		{
			get
			{
				return this._list.SyncRoot;
			}
		}

		/// <summary>Copies an <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementCollection" /> object into an array, starting at the specified index.</summary>
		/// <param name="array">An array of <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElement" /> objects. </param>
		/// <param name="index">An integer representing the index value. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified <paramref name="index" /> is less than zero, or greater than or equal to the length of the array. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> plus the current count is greater than the length of the array. </exception>
		// Token: 0x0600171A RID: 5914 RVA: 0x0005B707 File Offset: 0x00059907
		public void CopyTo(X509ChainElement[] array, int index)
		{
			this._list.CopyTo(array, index);
		}

		/// <summary>Copies an <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementCollection" /> object into an array, starting at the specified index.</summary>
		/// <param name="array">An array to copy the <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementCollection" /> object to.</param>
		/// <param name="index">The index of <paramref name="array" /> at which to start copying.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified <paramref name="index" /> is less than zero, or greater than or equal to the length of the array. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> plus the current count is greater than the length of the array. </exception>
		// Token: 0x0600171B RID: 5915 RVA: 0x0005B707 File Offset: 0x00059907
		void ICollection.CopyTo(Array array, int index)
		{
			this._list.CopyTo(array, index);
		}

		/// <summary>Gets an <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementEnumerator" /> object that can be used to navigate through a collection of chain elements.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementEnumerator" /> object.</returns>
		// Token: 0x0600171C RID: 5916 RVA: 0x0005B716 File Offset: 0x00059916
		public X509ChainElementEnumerator GetEnumerator()
		{
			return new X509ChainElementEnumerator(this._list);
		}

		/// <summary>Gets an <see cref="T:System.Collections.IEnumerator" /> object that can be used to navigate a collection of chain elements.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object.</returns>
		// Token: 0x0600171D RID: 5917 RVA: 0x0005B716 File Offset: 0x00059916
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new X509ChainElementEnumerator(this._list);
		}

		// Token: 0x0600171E RID: 5918 RVA: 0x0005B723 File Offset: 0x00059923
		internal void Add(X509Certificate2 certificate)
		{
			this._list.Add(new X509ChainElement(certificate));
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x0005B737 File Offset: 0x00059937
		internal void Clear()
		{
			this._list.Clear();
		}

		// Token: 0x06001720 RID: 5920 RVA: 0x0005B744 File Offset: 0x00059944
		internal bool Contains(X509Certificate2 certificate)
		{
			for (int i = 0; i < this._list.Count; i++)
			{
				if (certificate.Equals((this._list[i] as X509ChainElement).Certificate))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000D29 RID: 3369
		private ArrayList _list;
	}
}

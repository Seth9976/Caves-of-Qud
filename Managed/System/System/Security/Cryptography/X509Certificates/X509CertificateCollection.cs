using System;
using System.Collections;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Defines a collection that stores <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> objects.</summary>
	// Token: 0x020002D9 RID: 729
	[Serializable]
	public class X509CertificateCollection : CollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> class.</summary>
		// Token: 0x060016D8 RID: 5848 RVA: 0x0004E33C File Offset: 0x0004C53C
		public X509CertificateCollection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> class from an array of <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> objects.</summary>
		/// <param name="value">The array of <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> objects with which to initialize the new object. </param>
		// Token: 0x060016D9 RID: 5849 RVA: 0x0005AF10 File Offset: 0x00059110
		public X509CertificateCollection(X509Certificate[] value)
		{
			this.AddRange(value);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> class from another <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</summary>
		/// <param name="value">The <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> with which to initialize the new object. </param>
		// Token: 0x060016DA RID: 5850 RVA: 0x0005AF1F File Offset: 0x0005911F
		public X509CertificateCollection(X509CertificateCollection value)
		{
			this.AddRange(value);
		}

		/// <summary>Gets or sets the entry at the specified index of the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> at the specified index of the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</returns>
		/// <param name="index">The zero-based index of the entry to locate in the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="index" /> parameter is outside the valid range of indexes for the collection. </exception>
		// Token: 0x17000461 RID: 1121
		public X509Certificate this[int index]
		{
			get
			{
				return (X509Certificate)base.InnerList[index];
			}
			set
			{
				base.InnerList[index] = value;
			}
		}

		/// <summary>Adds an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> with the specified value to the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</summary>
		/// <returns>The index into the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> at which the new <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> was inserted.</returns>
		/// <param name="value">The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> to add to the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />. </param>
		// Token: 0x060016DD RID: 5853 RVA: 0x0005AF41 File Offset: 0x00059141
		public int Add(X509Certificate value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return base.InnerList.Add(value);
		}

		/// <summary>Copies the elements of an array of type <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> to the end of the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</summary>
		/// <param name="value">The array of type <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> containing the objects to add to the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		// Token: 0x060016DE RID: 5854 RVA: 0x0005AF60 File Offset: 0x00059160
		public void AddRange(X509Certificate[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			for (int i = 0; i < value.Length; i++)
			{
				base.InnerList.Add(value[i]);
			}
		}

		/// <summary>Copies the elements of the specified <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> to the end of the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</summary>
		/// <param name="value">The <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> containing the objects to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null. </exception>
		// Token: 0x060016DF RID: 5855 RVA: 0x0005AF98 File Offset: 0x00059198
		public void AddRange(X509CertificateCollection value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			for (int i = 0; i < value.InnerList.Count; i++)
			{
				base.InnerList.Add(value[i]);
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> contains the specified <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" />.</summary>
		/// <returns>true if the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> is contained in this collection; otherwise, false.</returns>
		/// <param name="value">The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> to locate. </param>
		// Token: 0x060016E0 RID: 5856 RVA: 0x0005AFDC File Offset: 0x000591DC
		public bool Contains(X509Certificate value)
		{
			if (value == null)
			{
				return false;
			}
			byte[] certHash = value.GetCertHash();
			for (int i = 0; i < base.InnerList.Count; i++)
			{
				X509Certificate x509Certificate = (X509Certificate)base.InnerList[i];
				if (this.Compare(x509Certificate.GetCertHash(), certHash))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Copies the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> values in the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> to a one-dimensional <see cref="T:System.Array" /> instance at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />. </param>
		/// <param name="index">The index into <paramref name="array" /> to begin copying. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="array" /> parameter is multidimensional.-or- The number of elements in the <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> is greater than the available space between <paramref name="arrayIndex" /> and the end of <paramref name="array" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="array" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="arrayIndex" /> parameter is less than the <paramref name="array" /> parameter's lower bound. </exception>
		// Token: 0x060016E1 RID: 5857 RVA: 0x0004E426 File Offset: 0x0004C626
		public void CopyTo(X509Certificate[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}

		/// <summary>Returns an enumerator that can iterate through the <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</summary>
		/// <returns>An enumerator of the subelements of <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> you can use to iterate through the collection.</returns>
		// Token: 0x060016E2 RID: 5858 RVA: 0x0005B02F File Offset: 0x0005922F
		public new X509CertificateCollection.X509CertificateEnumerator GetEnumerator()
		{
			return new X509CertificateCollection.X509CertificateEnumerator(this);
		}

		/// <summary>Builds a hash value based on all values contained in the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</summary>
		/// <returns>A hash value based on all values contained in the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</returns>
		// Token: 0x060016E3 RID: 5859 RVA: 0x0005B037 File Offset: 0x00059237
		public override int GetHashCode()
		{
			return base.InnerList.GetHashCode();
		}

		/// <summary>Returns the index of the specified <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> in the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</summary>
		/// <returns>The index of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> specified by the <paramref name="value" /> parameter in the <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />, if found; otherwise, -1.</returns>
		/// <param name="value">The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> to locate. </param>
		// Token: 0x060016E4 RID: 5860 RVA: 0x0004E435 File Offset: 0x0004C635
		public int IndexOf(X509Certificate value)
		{
			return base.InnerList.IndexOf(value);
		}

		/// <summary>Inserts a <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> into the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index where <paramref name="value" /> should be inserted. </param>
		/// <param name="value">The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> to insert. </param>
		// Token: 0x060016E5 RID: 5861 RVA: 0x0004E443 File Offset: 0x0004C643
		public void Insert(int index, X509Certificate value)
		{
			base.InnerList.Insert(index, value);
		}

		/// <summary>Removes a specific <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> from the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</summary>
		/// <param name="value">The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> to remove from the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />. </param>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> specified by the <paramref name="value" /> parameter is not found in the current <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />. </exception>
		// Token: 0x060016E6 RID: 5862 RVA: 0x0005B044 File Offset: 0x00059244
		public void Remove(X509Certificate value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this.IndexOf(value) == -1)
			{
				throw new ArgumentException("value", global::Locale.GetText("Not part of the collection."));
			}
			base.InnerList.Remove(value);
		}

		// Token: 0x060016E7 RID: 5863 RVA: 0x0005B080 File Offset: 0x00059280
		private bool Compare(byte[] array1, byte[] array2)
		{
			if (array1 == null && array2 == null)
			{
				return true;
			}
			if (array1 == null || array2 == null)
			{
				return false;
			}
			if (array1.Length != array2.Length)
			{
				return false;
			}
			for (int i = 0; i < array1.Length; i++)
			{
				if (array1[i] != array2[i])
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Enumerates the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> objects in an <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</summary>
		// Token: 0x020002DA RID: 730
		public class X509CertificateEnumerator : IEnumerator
		{
			/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection.X509CertificateEnumerator" /> class for the specified <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</summary>
			/// <param name="mappings">The <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> to enumerate. </param>
			// Token: 0x060016E8 RID: 5864 RVA: 0x0005B0C0 File Offset: 0x000592C0
			public X509CertificateEnumerator(X509CertificateCollection mappings)
			{
				this.enumerator = ((IEnumerable)mappings).GetEnumerator();
			}

			/// <summary>Gets the current <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> in the <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</summary>
			/// <returns>The current <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> in the <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" />.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
			// Token: 0x17000462 RID: 1122
			// (get) Token: 0x060016E9 RID: 5865 RVA: 0x0005B0D4 File Offset: 0x000592D4
			public X509Certificate Current
			{
				get
				{
					return (X509Certificate)this.enumerator.Current;
				}
			}

			/// <summary>For a description of this member, see <see cref="P:System.Collections.IEnumerator.Current" />.</summary>
			/// <returns>The current X.509 certificate object in the <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> object.</returns>
			/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
			// Token: 0x17000463 RID: 1123
			// (get) Token: 0x060016EA RID: 5866 RVA: 0x0005B0E6 File Offset: 0x000592E6
			object IEnumerator.Current
			{
				get
				{
					return this.enumerator.Current;
				}
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IEnumerator.MoveNext" />.</summary>
			/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was instantiated. </exception>
			// Token: 0x060016EB RID: 5867 RVA: 0x0005B0F3 File Offset: 0x000592F3
			bool IEnumerator.MoveNext()
			{
				return this.enumerator.MoveNext();
			}

			/// <summary>For a description of this member, see <see cref="M:System.Collections.IEnumerator.Reset" />.</summary>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was instantiated. </exception>
			// Token: 0x060016EC RID: 5868 RVA: 0x0005B100 File Offset: 0x00059300
			void IEnumerator.Reset()
			{
				this.enumerator.Reset();
			}

			/// <summary>Advances the enumerator to the next element of the collection.</summary>
			/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
			/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was instantiated. </exception>
			// Token: 0x060016ED RID: 5869 RVA: 0x0005B0F3 File Offset: 0x000592F3
			public bool MoveNext()
			{
				return this.enumerator.MoveNext();
			}

			/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
			/// <exception cref="T:System.InvalidOperationException">The collection is modified after the enumerator is instantiated. </exception>
			// Token: 0x060016EE RID: 5870 RVA: 0x0005B100 File Offset: 0x00059300
			public void Reset()
			{
				this.enumerator.Reset();
			}

			// Token: 0x04000D22 RID: 3362
			private IEnumerator enumerator;
		}
	}
}

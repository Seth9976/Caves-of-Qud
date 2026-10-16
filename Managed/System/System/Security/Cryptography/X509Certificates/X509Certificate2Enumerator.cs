using System;
using System.Collections;
using Unity;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Supports a simple iteration over a <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object. This class cannot be inherited.</summary>
	// Token: 0x020002D5 RID: 725
	public sealed class X509Certificate2Enumerator : IEnumerator
	{
		// Token: 0x06001682 RID: 5762 RVA: 0x0005A46F File Offset: 0x0005866F
		internal X509Certificate2Enumerator(X509Certificate2Collection collection)
		{
			this.enumerator = ((IEnumerable)collection).GetEnumerator();
		}

		/// <summary>Gets the current element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</summary>
		/// <returns>The current element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06001683 RID: 5763 RVA: 0x0005A483 File Offset: 0x00058683
		public X509Certificate2 Current
		{
			get
			{
				return (X509Certificate2)this.enumerator.Current;
			}
		}

		/// <summary>Advances the enumerator to the next element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</summary>
		/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
		// Token: 0x06001684 RID: 5764 RVA: 0x0005A495 File Offset: 0x00058695
		public bool MoveNext()
		{
			return this.enumerator.MoveNext();
		}

		/// <summary>Sets the enumerator to its initial position, which is before the first element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</summary>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
		// Token: 0x06001685 RID: 5765 RVA: 0x0005A4A2 File Offset: 0x000586A2
		public void Reset()
		{
			this.enumerator.Reset();
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IEnumerator.Current" />.</summary>
		/// <returns>The current element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06001686 RID: 5766 RVA: 0x0005A4AF File Offset: 0x000586AF
		object IEnumerator.Current
		{
			get
			{
				return this.enumerator.Current;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IEnumerator.MoveNext" />.</summary>
		/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
		// Token: 0x06001687 RID: 5767 RVA: 0x0005A495 File Offset: 0x00058695
		bool IEnumerator.MoveNext()
		{
			return this.enumerator.MoveNext();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IEnumerator.Reset" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
		// Token: 0x06001688 RID: 5768 RVA: 0x0005A4A2 File Offset: 0x000586A2
		void IEnumerator.Reset()
		{
			this.enumerator.Reset();
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x00013BAE File Offset: 0x00011DAE
		internal X509Certificate2Enumerator()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000D1A RID: 3354
		private IEnumerator enumerator;
	}
}

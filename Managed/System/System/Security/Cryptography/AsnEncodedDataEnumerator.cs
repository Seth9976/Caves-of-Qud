using System;
using System.Collections;
using Unity;

namespace System.Security.Cryptography
{
	/// <summary>Provides the ability to navigate through an <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object. This class cannot be inherited.</summary>
	// Token: 0x020002B7 RID: 695
	public sealed class AsnEncodedDataEnumerator : IEnumerator
	{
		// Token: 0x060015C6 RID: 5574 RVA: 0x0005756E File Offset: 0x0005576E
		internal AsnEncodedDataEnumerator(AsnEncodedDataCollection asnEncodedDatas)
		{
			this._asnEncodedDatas = asnEncodedDatas;
			this._current = -1;
		}

		/// <summary>Gets the current <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object in an <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object.</summary>
		/// <returns>The current <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object in the collection.</returns>
		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x060015C7 RID: 5575 RVA: 0x00057584 File Offset: 0x00055784
		public AsnEncodedData Current
		{
			get
			{
				return this._asnEncodedDatas[this._current];
			}
		}

		/// <summary>Gets the current <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object in an <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object.</summary>
		/// <returns>The current <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object.</returns>
		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x060015C8 RID: 5576 RVA: 0x00057584 File Offset: 0x00055784
		object IEnumerator.Current
		{
			get
			{
				return this._asnEncodedDatas[this._current];
			}
		}

		/// <summary>Advances to the next <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object in an <see cref="T:System.Security.Cryptography.AsnEncodedDataCollection" /> object.</summary>
		/// <returns>true, if the enumerator was successfully advanced to the next element; false, if the enumerator has passed the end of the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x060015C9 RID: 5577 RVA: 0x00057597 File Offset: 0x00055797
		public bool MoveNext()
		{
			if (this._current >= this._asnEncodedDatas.Count - 1)
			{
				return false;
			}
			this._current++;
			return true;
		}

		/// <summary>Sets an enumerator to its initial position.</summary>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x060015CA RID: 5578 RVA: 0x000575BF File Offset: 0x000557BF
		public void Reset()
		{
			this._current = -1;
		}

		// Token: 0x060015CB RID: 5579 RVA: 0x00013BAE File Offset: 0x00011DAE
		internal AsnEncodedDataEnumerator()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000C52 RID: 3154
		private readonly AsnEncodedDataCollection _asnEncodedDatas;

		// Token: 0x04000C53 RID: 3155
		private int _current;
	}
}

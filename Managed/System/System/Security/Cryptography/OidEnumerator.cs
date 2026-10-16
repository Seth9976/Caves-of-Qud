using System;
using System.Collections;
using Unity;

namespace System.Security.Cryptography
{
	/// <summary>Provides the ability to navigate through an <see cref="T:System.Security.Cryptography.OidCollection" /> object. This class cannot be inherited.</summary>
	// Token: 0x020002BA RID: 698
	public sealed class OidEnumerator : IEnumerator
	{
		// Token: 0x060015E2 RID: 5602 RVA: 0x000578D2 File Offset: 0x00055AD2
		internal OidEnumerator(OidCollection oids)
		{
			this._oids = oids;
			this._current = -1;
		}

		/// <summary>Gets the current <see cref="T:System.Security.Cryptography.Oid" /> object in an <see cref="T:System.Security.Cryptography.OidCollection" /> object.</summary>
		/// <returns>The current <see cref="T:System.Security.Cryptography.Oid" /> object in the collection.</returns>
		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x060015E3 RID: 5603 RVA: 0x000578E8 File Offset: 0x00055AE8
		public Oid Current
		{
			get
			{
				return this._oids[this._current];
			}
		}

		/// <summary>Gets the current <see cref="T:System.Security.Cryptography.Oid" /> object in an <see cref="T:System.Security.Cryptography.OidCollection" /> object.</summary>
		/// <returns>The current <see cref="T:System.Security.Cryptography.Oid" /> object.</returns>
		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x060015E4 RID: 5604 RVA: 0x000578FB File Offset: 0x00055AFB
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		/// <summary>Advances to the next <see cref="T:System.Security.Cryptography.Oid" /> object in an <see cref="T:System.Security.Cryptography.OidCollection" /> object.</summary>
		/// <returns>true, if the enumerator was successfully advanced to the next element; false, if the enumerator has passed the end of the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x060015E5 RID: 5605 RVA: 0x00057903 File Offset: 0x00055B03
		public bool MoveNext()
		{
			if (this._current >= this._oids.Count - 1)
			{
				return false;
			}
			this._current++;
			return true;
		}

		/// <summary>Sets an enumerator to its initial position.</summary>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x060015E6 RID: 5606 RVA: 0x0005792B File Offset: 0x00055B2B
		public void Reset()
		{
			this._current = -1;
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x00013BAE File Offset: 0x00011DAE
		internal OidEnumerator()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000C58 RID: 3160
		private readonly OidCollection _oids;

		// Token: 0x04000C59 RID: 3161
		private int _current;
	}
}

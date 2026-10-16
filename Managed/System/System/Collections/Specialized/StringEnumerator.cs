using System;
using Unity;

namespace System.Collections.Specialized
{
	/// <summary>Supports a simple iteration over a <see cref="T:System.Collections.Specialized.StringCollection" />.</summary>
	// Token: 0x020004B4 RID: 1204
	public class StringEnumerator
	{
		// Token: 0x060026DC RID: 9948 RVA: 0x00087373 File Offset: 0x00085573
		internal StringEnumerator(StringCollection mappings)
		{
			this._temp = mappings;
			this._baseEnumerator = this._temp.GetEnumerator();
		}

		/// <summary>Gets the current element in the collection.</summary>
		/// <returns>The current element in the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x060026DD RID: 9949 RVA: 0x00087393 File Offset: 0x00085593
		public string Current
		{
			get
			{
				return (string)this._baseEnumerator.Current;
			}
		}

		/// <summary>Advances the enumerator to the next element of the collection.</summary>
		/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
		// Token: 0x060026DE RID: 9950 RVA: 0x000873A5 File Offset: 0x000855A5
		public bool MoveNext()
		{
			return this._baseEnumerator.MoveNext();
		}

		/// <summary>Sets the enumerator to its initial position, which is before the first element in the collection.</summary>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
		// Token: 0x060026DF RID: 9951 RVA: 0x000873B2 File Offset: 0x000855B2
		public void Reset()
		{
			this._baseEnumerator.Reset();
		}

		// Token: 0x060026E0 RID: 9952 RVA: 0x00013BAE File Offset: 0x00011DAE
		internal StringEnumerator()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001534 RID: 5428
		private IEnumerator _baseEnumerator;

		// Token: 0x04001535 RID: 5429
		private IEnumerable _temp;
	}
}

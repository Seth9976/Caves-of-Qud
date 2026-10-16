using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Roslyn.Utilities
{
	// Token: 0x0200005F RID: 95
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class SetWithInsertionOrder<[Nullable(2)] T> : IEnumerable<T>, IEnumerable, IReadOnlySet<T>
	{
		// Token: 0x060001FB RID: 507 RVA: 0x0000707F File Offset: 0x0000527F
		public bool Add(T value)
		{
			if (this._set == null)
			{
				this._set = new HashSet<T>();
				this._elements = new ArrayBuilder<T>();
			}
			if (!this._set.Add(value))
			{
				return false;
			}
			this._elements.Add(value);
			return true;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x000070BC File Offset: 0x000052BC
		public bool Insert(int index, T value)
		{
			if (this._set == null)
			{
				if (index > 0)
				{
					throw new IndexOutOfRangeException();
				}
				this.Add(value);
			}
			else
			{
				if (!this._set.Add(value))
				{
					return false;
				}
				try
				{
					this._elements.Insert(index, value);
				}
				catch
				{
					this._set.Remove(value);
					throw;
				}
			}
			return true;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00007128 File Offset: 0x00005328
		public bool Remove(T value)
		{
			if (this._set == null)
			{
				return false;
			}
			if (!this._set.Remove(value))
			{
				return false;
			}
			this._elements.RemoveAt(this._elements.IndexOf(value));
			return true;
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060001FE RID: 510 RVA: 0x0000715C File Offset: 0x0000535C
		public int Count
		{
			get
			{
				ArrayBuilder<T> elements = this._elements;
				if (elements == null)
				{
					return 0;
				}
				return elements.Count;
			}
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0000716F File Offset: 0x0000536F
		public bool Contains(T value)
		{
			HashSet<T> set = this._set;
			return set != null && set.Contains(value);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00007183 File Offset: 0x00005383
		public IEnumerator<T> GetEnumerator()
		{
			if (this._elements != null)
			{
				return this._elements.GetEnumerator();
			}
			return SpecializedCollections.EmptyEnumerator<T>();
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000719E File Offset: 0x0000539E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000202 RID: 514 RVA: 0x000071A6 File Offset: 0x000053A6
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> AsImmutable()
		{
			return this._elements.ToImmutableArrayOrEmpty<T>();
		}

		// Token: 0x17000041 RID: 65
		public T this[int i]
		{
			get
			{
				return this._elements[i];
			}
		}

		// Token: 0x04000087 RID: 135
		[Nullable(new byte[] { 2, 1 })]
		private HashSet<T> _set;

		// Token: 0x04000088 RID: 136
		[Nullable(new byte[] { 2, 1 })]
		private ArrayBuilder<T> _elements;
	}
}

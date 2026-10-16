using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000123 RID: 291
	[NullableContext(1)]
	[Nullable(0)]
	internal class UnionCollection<[Nullable(2)] T> : ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x06000A2E RID: 2606 RVA: 0x0001D65D File Offset: 0x0001B85D
		public static ICollection<T> Create(ICollection<T> coll1, ICollection<T> coll2)
		{
			if (coll1.Count == 0)
			{
				return coll2;
			}
			if (coll2.Count == 0)
			{
				return coll1;
			}
			return new UnionCollection<T>(ImmutableArray.Create<ICollection<T>>(coll1, coll2));
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0001D680 File Offset: 0x0001B880
		public static ICollection<T> Create<[Nullable(2)] TOrig>([Nullable(new byte[] { 0, 1 })] ImmutableArray<TOrig> collections, Func<TOrig, ICollection<T>> selector)
		{
			int length = collections.Length;
			if (length == 0)
			{
				return SpecializedCollections.EmptyCollection<T>();
			}
			if (length != 1)
			{
				return new UnionCollection<T>(ImmutableArray.CreateRange<TOrig, ICollection<T>>(collections, selector));
			}
			return selector.Invoke(collections[0]);
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0001D6BF File Offset: 0x0001B8BF
		private UnionCollection([Nullable(new byte[] { 0, 1, 1 })] ImmutableArray<ICollection<T>> collections)
		{
			this._collections = collections;
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0001D6D5 File Offset: 0x0001B8D5
		public void Add(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x0001D6DC File Offset: 0x0001B8DC
		public void Clear()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0001D6E4 File Offset: 0x0001B8E4
		public bool Contains(T item)
		{
			ImmutableArray<ICollection<T>>.Enumerator enumerator = this._collections.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.Contains(item))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0001D720 File Offset: 0x0001B920
		public void CopyTo(T[] array, int arrayIndex)
		{
			int num = arrayIndex;
			foreach (ICollection<T> collection in this._collections)
			{
				collection.CopyTo(array, num);
				num += collection.Count;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000A35 RID: 2613 RVA: 0x0001D764 File Offset: 0x0001B964
		public int Count
		{
			get
			{
				if (this._count == -1)
				{
					this._count = Enumerable.Sum<ICollection<T>>(this._collections, (ICollection<T> c) => c.Count);
				}
				return this._count;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000A36 RID: 2614 RVA: 0x0001D7B5 File Offset: 0x0001B9B5
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x0001D7B8 File Offset: 0x0001B9B8
		public bool Remove(T item)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x0001D7BF File Offset: 0x0001B9BF
		public IEnumerator<T> GetEnumerator()
		{
			return Enumerable.SelectMany<ICollection<T>, T>(this._collections, (ICollection<T> c) => c).GetEnumerator();
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0001D7F5 File Offset: 0x0001B9F5
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040002CD RID: 717
		[Nullable(new byte[] { 0, 1, 1 })]
		private readonly ImmutableArray<ICollection<T>> _collections;

		// Token: 0x040002CE RID: 718
		private int _count = -1;
	}
}

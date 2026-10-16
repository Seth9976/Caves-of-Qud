using System;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Roslyn.Utilities
{
	// Token: 0x02000054 RID: 84
	[NullableContext(1)]
	[Nullable(0)]
	internal struct OneOrMany<T>
	{
		// Token: 0x060001C6 RID: 454 RVA: 0x00006960 File Offset: 0x00004B60
		public OneOrMany(T one)
		{
			this._one = one;
			this._many = default(ImmutableArray<T>);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00006975 File Offset: 0x00004B75
		public OneOrMany([Nullable(new byte[] { 0, 1 })] ImmutableArray<T> many)
		{
			if (many.IsDefault)
			{
				throw new ArgumentNullException("many");
			}
			this._one = default(T);
			this._many = many;
		}

		// Token: 0x17000038 RID: 56
		public T this[int index]
		{
			get
			{
				if (!this._many.IsDefault)
				{
					return this._many[index];
				}
				if (index != 0)
				{
					throw new IndexOutOfRangeException();
				}
				return this._one;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x000069DC File Offset: 0x00004BDC
		public int Count
		{
			get
			{
				if (!this._many.IsDefault)
				{
					return this._many.Length;
				}
				return 1;
			}
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00006A0C File Offset: 0x00004C0C
		[return: Nullable(new byte[] { 0, 1 })]
		public OneOrMany<T> Add(T one)
		{
			ArrayBuilder<T> instance = ArrayBuilder<T>.GetInstance();
			if (this._many.IsDefault)
			{
				instance.Add(this._one);
			}
			else
			{
				instance.AddRange(this._many);
			}
			instance.Add(one);
			return new OneOrMany<T>(instance.ToImmutableAndFree());
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00006A5C File Offset: 0x00004C5C
		public bool Contains(T item)
		{
			if (this.Count == 1)
			{
				return item.Equals(this._one);
			}
			OneOrMany<T>.Enumerator enumerator = this.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (item.Equals(enumerator.Current))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00006ABC File Offset: 0x00004CBC
		[return: Nullable(new byte[] { 0, 1 })]
		public OneOrMany<T> RemoveAll(T item)
		{
			if (this._many.IsDefault)
			{
				if (!item.Equals(this._one))
				{
					return this;
				}
				return default(OneOrMany<T>);
			}
			else
			{
				ArrayBuilder<T> instance = ArrayBuilder<T>.GetInstance();
				OneOrMany<T>.Enumerator enumerator = this.GetEnumerator();
				while (enumerator.MoveNext())
				{
					if (!item.Equals(enumerator.Current))
					{
						instance.Add(enumerator.Current);
					}
				}
				if (instance.Count == 0)
				{
					return default(OneOrMany<T>);
				}
				if (instance.Count != this.Count)
				{
					return new OneOrMany<T>(instance.ToImmutableAndFree());
				}
				return this;
			}
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00006B76 File Offset: 0x00004D76
		[NullableContext(0)]
		public OneOrMany<T>.Enumerator GetEnumerator()
		{
			return new OneOrMany<T>.Enumerator(this);
		}

		// Token: 0x04000080 RID: 128
		[Nullable(2)]
		private readonly T _one;

		// Token: 0x04000081 RID: 129
		[Nullable(new byte[] { 0, 1 })]
		private readonly ImmutableArray<T> _many;

		// Token: 0x02000502 RID: 1282
		[Nullable(0)]
		internal struct Enumerator
		{
			// Token: 0x060032F8 RID: 13048 RVA: 0x0009094D File Offset: 0x0008EB4D
			internal Enumerator([Nullable(new byte[] { 0, 1 })] OneOrMany<T> collection)
			{
				this._collection = collection;
				this._index = -1;
			}

			// Token: 0x060032F9 RID: 13049 RVA: 0x00090960 File Offset: 0x0008EB60
			public bool MoveNext()
			{
				this._index++;
				return this._index < this._collection.Count;
			}

			// Token: 0x17000EAF RID: 3759
			// (get) Token: 0x060032FA RID: 13050 RVA: 0x00090994 File Offset: 0x0008EB94
			public T Current
			{
				get
				{
					return this._collection[this._index];
				}
			}

			// Token: 0x0400140A RID: 5130
			[Nullable(new byte[] { 0, 1 })]
			private readonly OneOrMany<T> _collection;

			// Token: 0x0400140B RID: 5131
			private int _index;
		}
	}
}

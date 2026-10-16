using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Microsoft.CodeAnalysis.PooledObjects
{
	// Token: 0x0200029A RID: 666
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("Count = {Count,nq}")]
	[DebuggerTypeProxy(typeof(ArrayBuilder<>.DebuggerProxy))]
	internal sealed class ArrayBuilder<[Nullable(2)] T> : IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable, IReadOnlyList<T>
	{
		// Token: 0x06001C07 RID: 7175 RVA: 0x00052FFB File Offset: 0x000511FB
		public ArrayBuilder(int size)
		{
			this._builder = ImmutableArray.CreateBuilder<T>(size);
		}

		// Token: 0x06001C08 RID: 7176 RVA: 0x0005300F File Offset: 0x0005120F
		public ArrayBuilder()
			: this(8)
		{
		}

		// Token: 0x06001C09 RID: 7177 RVA: 0x00053018 File Offset: 0x00051218
		private ArrayBuilder(ObjectPool<ArrayBuilder<T>> pool)
			: this()
		{
			this._pool = pool;
		}

		// Token: 0x06001C0A RID: 7178 RVA: 0x00053027 File Offset: 0x00051227
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> ToImmutable()
		{
			return this._builder.ToImmutable();
		}

		// Token: 0x06001C0B RID: 7179 RVA: 0x00053034 File Offset: 0x00051234
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> ToImmutableAndClear()
		{
			ImmutableArray<T> immutableArray;
			if (this.Count == 0)
			{
				immutableArray = ImmutableArray<T>.Empty;
			}
			else if (this._builder.Capacity == this.Count)
			{
				immutableArray = this._builder.MoveToImmutable();
			}
			else
			{
				immutableArray = this.ToImmutable();
				this.Clear();
			}
			return immutableArray;
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06001C0C RID: 7180 RVA: 0x00053080 File Offset: 0x00051280
		// (set) Token: 0x06001C0D RID: 7181 RVA: 0x0005308D File Offset: 0x0005128D
		public int Count
		{
			get
			{
				return this._builder.Count;
			}
			set
			{
				this._builder.Count = value;
			}
		}

		// Token: 0x17000794 RID: 1940
		public T this[int index]
		{
			get
			{
				return this._builder[index];
			}
			set
			{
				this._builder[index] = value;
			}
		}

		// Token: 0x06001C10 RID: 7184 RVA: 0x000530B8 File Offset: 0x000512B8
		public void SetItem(int index, T value)
		{
			while (index > this._builder.Count)
			{
				this._builder.Add(default(T));
			}
			if (index == this._builder.Count)
			{
				this._builder.Add(value);
				return;
			}
			this._builder[index] = value;
		}

		// Token: 0x06001C11 RID: 7185 RVA: 0x00053111 File Offset: 0x00051311
		public void Add(T item)
		{
			this._builder.Add(item);
		}

		// Token: 0x06001C12 RID: 7186 RVA: 0x0005311F File Offset: 0x0005131F
		public void Insert(int index, T item)
		{
			this._builder.Insert(index, item);
		}

		// Token: 0x06001C13 RID: 7187 RVA: 0x0005312E File Offset: 0x0005132E
		public void EnsureCapacity(int capacity)
		{
			if (this._builder.Capacity < capacity)
			{
				this._builder.Capacity = capacity;
			}
		}

		// Token: 0x06001C14 RID: 7188 RVA: 0x0005314A File Offset: 0x0005134A
		public void Clear()
		{
			this._builder.Clear();
		}

		// Token: 0x06001C15 RID: 7189 RVA: 0x00053157 File Offset: 0x00051357
		public bool Contains(T item)
		{
			return this._builder.Contains(item);
		}

		// Token: 0x06001C16 RID: 7190 RVA: 0x00053165 File Offset: 0x00051365
		public int IndexOf(T item)
		{
			return this._builder.IndexOf(item);
		}

		// Token: 0x06001C17 RID: 7191 RVA: 0x00053173 File Offset: 0x00051373
		public int IndexOf(T item, IEqualityComparer<T> equalityComparer)
		{
			return this._builder.IndexOf(item, 0, this._builder.Count, equalityComparer);
		}

		// Token: 0x06001C18 RID: 7192 RVA: 0x0005318E File Offset: 0x0005138E
		public int IndexOf(T item, int startIndex, int count)
		{
			return this._builder.IndexOf(item, startIndex, count);
		}

		// Token: 0x06001C19 RID: 7193 RVA: 0x0005319E File Offset: 0x0005139E
		public int FindIndex(Predicate<T> match)
		{
			return this.FindIndex(0, this.Count, match);
		}

		// Token: 0x06001C1A RID: 7194 RVA: 0x000531AE File Offset: 0x000513AE
		public int FindIndex(int startIndex, Predicate<T> match)
		{
			return this.FindIndex(startIndex, this.Count - startIndex, match);
		}

		// Token: 0x06001C1B RID: 7195 RVA: 0x000531C0 File Offset: 0x000513C0
		public int FindIndex(int startIndex, int count, Predicate<T> match)
		{
			int num = startIndex + count;
			for (int i = startIndex; i < num; i++)
			{
				if (match.Invoke(this._builder[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001C1C RID: 7196 RVA: 0x000531F4 File Offset: 0x000513F4
		public bool Remove(T element)
		{
			return this._builder.Remove(element);
		}

		// Token: 0x06001C1D RID: 7197 RVA: 0x00053202 File Offset: 0x00051402
		public void RemoveAt(int index)
		{
			this._builder.RemoveAt(index);
		}

		// Token: 0x06001C1E RID: 7198 RVA: 0x00053210 File Offset: 0x00051410
		public void RemoveLast()
		{
			this._builder.RemoveAt(this._builder.Count - 1);
		}

		// Token: 0x06001C1F RID: 7199 RVA: 0x0005322A File Offset: 0x0005142A
		public void ReverseContents()
		{
			this._builder.Reverse();
		}

		// Token: 0x06001C20 RID: 7200 RVA: 0x00053237 File Offset: 0x00051437
		public void Sort()
		{
			this._builder.Sort();
		}

		// Token: 0x06001C21 RID: 7201 RVA: 0x00053244 File Offset: 0x00051444
		public void Sort(IComparer<T> comparer)
		{
			this._builder.Sort(comparer);
		}

		// Token: 0x06001C22 RID: 7202 RVA: 0x00053252 File Offset: 0x00051452
		public void Sort(Comparison<T> compare)
		{
			this.Sort(Comparer<T>.Create(compare));
		}

		// Token: 0x06001C23 RID: 7203 RVA: 0x00053260 File Offset: 0x00051460
		public void Sort(int startIndex, IComparer<T> comparer)
		{
			this._builder.Sort(startIndex, this._builder.Count - startIndex, comparer);
		}

		// Token: 0x06001C24 RID: 7204 RVA: 0x0005327C File Offset: 0x0005147C
		public T[] ToArray()
		{
			return this._builder.ToArray();
		}

		// Token: 0x06001C25 RID: 7205 RVA: 0x00053289 File Offset: 0x00051489
		public void CopyTo(T[] array, int start)
		{
			this._builder.CopyTo(array, start);
		}

		// Token: 0x06001C26 RID: 7206 RVA: 0x00053298 File Offset: 0x00051498
		public T Last()
		{
			return this._builder[this._builder.Count - 1];
		}

		// Token: 0x06001C27 RID: 7207 RVA: 0x000532B2 File Offset: 0x000514B2
		public T First()
		{
			return this._builder[0];
		}

		// Token: 0x06001C28 RID: 7208 RVA: 0x000532C0 File Offset: 0x000514C0
		public bool Any()
		{
			return this._builder.Count > 0;
		}

		// Token: 0x06001C29 RID: 7209 RVA: 0x000532D0 File Offset: 0x000514D0
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> ToImmutableOrNull()
		{
			if (this.Count == 0)
			{
				return default(ImmutableArray<T>);
			}
			return this.ToImmutable();
		}

		// Token: 0x06001C2A RID: 7210 RVA: 0x000532F8 File Offset: 0x000514F8
		[NullableContext(0)]
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<U> ToDowncastedImmutable<U>() where U : T
		{
			if (this.Count == 0)
			{
				return ImmutableArray<U>.Empty;
			}
			ArrayBuilder<U> instance = ArrayBuilder<U>.GetInstance(this.Count);
			foreach (T t in this)
			{
				instance.Add((U)((object)t));
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06001C2B RID: 7211 RVA: 0x00053350 File Offset: 0x00051550
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<T> ToImmutableAndFree()
		{
			ImmutableArray<T> immutableArray;
			if (this.Count == 0)
			{
				immutableArray = ImmutableArray<T>.Empty;
			}
			else if (this._builder.Capacity == this.Count)
			{
				immutableArray = this._builder.MoveToImmutable();
			}
			else
			{
				immutableArray = this.ToImmutable();
			}
			this.Free();
			return immutableArray;
		}

		// Token: 0x06001C2C RID: 7212 RVA: 0x0005339C File Offset: 0x0005159C
		public T[] ToArrayAndFree()
		{
			T[] array = this.ToArray();
			this.Free();
			return array;
		}

		// Token: 0x06001C2D RID: 7213 RVA: 0x000533AC File Offset: 0x000515AC
		public void Free()
		{
			ObjectPool<ArrayBuilder<T>> pool = this._pool;
			if (pool != null && this._builder.Capacity < 128)
			{
				if (this.Count != 0)
				{
					this.Clear();
				}
				pool.Free(this);
				return;
			}
		}

		// Token: 0x06001C2E RID: 7214 RVA: 0x000533EB File Offset: 0x000515EB
		public static ArrayBuilder<T> GetInstance()
		{
			return ArrayBuilder<T>.s_poolInstance.Allocate();
		}

		// Token: 0x06001C2F RID: 7215 RVA: 0x000533F7 File Offset: 0x000515F7
		public static ArrayBuilder<T> GetInstance(int capacity)
		{
			ArrayBuilder<T> instance = ArrayBuilder<T>.GetInstance();
			instance.EnsureCapacity(capacity);
			return instance;
		}

		// Token: 0x06001C30 RID: 7216 RVA: 0x00053408 File Offset: 0x00051608
		public static ArrayBuilder<T> GetInstance(int capacity, T fillWithValue)
		{
			ArrayBuilder<T> instance = ArrayBuilder<T>.GetInstance();
			instance.EnsureCapacity(capacity);
			for (int i = 0; i < capacity; i++)
			{
				instance.Add(fillWithValue);
			}
			return instance;
		}

		// Token: 0x06001C31 RID: 7217 RVA: 0x00053436 File Offset: 0x00051636
		public static ObjectPool<ArrayBuilder<T>> CreatePool()
		{
			return ArrayBuilder<T>.CreatePool(128);
		}

		// Token: 0x06001C32 RID: 7218 RVA: 0x00053442 File Offset: 0x00051642
		public static ObjectPool<ArrayBuilder<T>> CreatePool(int size)
		{
			ObjectPool<ArrayBuilder<T>> pool = null;
			pool = new ObjectPool<ArrayBuilder<T>>(() => new ArrayBuilder<T>(pool), size);
			return pool;
		}

		// Token: 0x06001C33 RID: 7219 RVA: 0x0005346D File Offset: 0x0005166D
		[NullableContext(0)]
		public ArrayBuilder<T>.Enumerator GetEnumerator()
		{
			return new ArrayBuilder<T>.Enumerator(this);
		}

		// Token: 0x06001C34 RID: 7220 RVA: 0x00053475 File Offset: 0x00051675
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this._builder.GetEnumerator();
		}

		// Token: 0x06001C35 RID: 7221 RVA: 0x00053482 File Offset: 0x00051682
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._builder.GetEnumerator();
		}

		// Token: 0x06001C36 RID: 7222 RVA: 0x00053490 File Offset: 0x00051690
		[return: Nullable(new byte[] { 1, 1, 0, 1 })]
		internal Dictionary<K, ImmutableArray<T>> ToDictionary<K>(Func<T, K> keySelector, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<K> comparer = null)
		{
			if (this.Count == 1)
			{
				Dictionary<K, ImmutableArray<T>> dictionary = new Dictionary<K, ImmutableArray<T>>(1, comparer);
				T t = this[0];
				dictionary.Add(keySelector.Invoke(t), ImmutableArray.Create<T>(t));
				return dictionary;
			}
			if (this.Count == 0)
			{
				return new Dictionary<K, ImmutableArray<T>>(comparer);
			}
			Dictionary<K, ArrayBuilder<T>> dictionary2 = new Dictionary<K, ArrayBuilder<T>>(this.Count, comparer);
			for (int i = 0; i < this.Count; i++)
			{
				T t2 = this[i];
				K k = keySelector.Invoke(t2);
				ArrayBuilder<T> instance;
				if (!dictionary2.TryGetValue(k, ref instance))
				{
					instance = ArrayBuilder<T>.GetInstance();
					dictionary2.Add(k, instance);
				}
				instance.Add(t2);
			}
			Dictionary<K, ImmutableArray<T>> dictionary3 = new Dictionary<K, ImmutableArray<T>>(dictionary2.Count, comparer);
			foreach (KeyValuePair<K, ArrayBuilder<T>> keyValuePair in dictionary2)
			{
				dictionary3.Add(keyValuePair.Key, keyValuePair.Value.ToImmutableAndFree());
			}
			return dictionary3;
		}

		// Token: 0x06001C37 RID: 7223 RVA: 0x00053594 File Offset: 0x00051794
		public void AddRange(ArrayBuilder<T> items)
		{
			this._builder.AddRange(items._builder);
		}

		// Token: 0x06001C38 RID: 7224 RVA: 0x000535A7 File Offset: 0x000517A7
		public void AddRange<[Nullable(0)] U>(ArrayBuilder<U> items) where U : T
		{
			this._builder.AddRange<U>(items._builder);
		}

		// Token: 0x06001C39 RID: 7225 RVA: 0x000535BA File Offset: 0x000517BA
		public void AddRange([Nullable(new byte[] { 0, 1 })] ImmutableArray<T> items)
		{
			this._builder.AddRange(items);
		}

		// Token: 0x06001C3A RID: 7226 RVA: 0x000535C8 File Offset: 0x000517C8
		public void AddRange([Nullable(new byte[] { 0, 1 })] ImmutableArray<T> items, int length)
		{
			this._builder.AddRange(items, length);
		}

		// Token: 0x06001C3B RID: 7227 RVA: 0x000535D8 File Offset: 0x000517D8
		public void AddRange([Nullable(new byte[] { 0, 1 })] ImmutableArray<T> items, int start, int length)
		{
			int i = start;
			int num = start + length;
			while (i < num)
			{
				this.Add(items[i]);
				i++;
			}
		}

		// Token: 0x06001C3C RID: 7228 RVA: 0x00053603 File Offset: 0x00051803
		public void AddRange<S>([Nullable(new byte[] { 0, 1 })] ImmutableArray<S> items) where S : class, T
		{
			this.AddRange(ImmutableArray<T>.CastUp<S>(items));
		}

		// Token: 0x06001C3D RID: 7229 RVA: 0x00053614 File Offset: 0x00051814
		public void AddRange(T[] items, int start, int length)
		{
			int i = start;
			int num = start + length;
			while (i < num)
			{
				this.Add(items[i]);
				i++;
			}
		}

		// Token: 0x06001C3E RID: 7230 RVA: 0x0005363E File Offset: 0x0005183E
		public void AddRange(IEnumerable<T> items)
		{
			this._builder.AddRange(items);
		}

		// Token: 0x06001C3F RID: 7231 RVA: 0x0005364C File Offset: 0x0005184C
		public void AddRange(params T[] items)
		{
			this._builder.AddRange(items);
		}

		// Token: 0x06001C40 RID: 7232 RVA: 0x0005365A File Offset: 0x0005185A
		public void AddRange(T[] items, int length)
		{
			this._builder.AddRange(items, length);
		}

		// Token: 0x06001C41 RID: 7233 RVA: 0x00053669 File Offset: 0x00051869
		public void Clip(int limit)
		{
			this._builder.Count = limit;
		}

		// Token: 0x06001C42 RID: 7234 RVA: 0x00053677 File Offset: 0x00051877
		public void ZeroInit(int count)
		{
			this._builder.Clear();
			this._builder.Count = count;
		}

		// Token: 0x06001C43 RID: 7235 RVA: 0x00053690 File Offset: 0x00051890
		public void AddMany(T item, int count)
		{
			for (int i = 0; i < count; i++)
			{
				this.Add(item);
			}
		}

		// Token: 0x06001C44 RID: 7236 RVA: 0x000536B0 File Offset: 0x000518B0
		public void RemoveDuplicates()
		{
			PooledHashSet<T> instance = PooledHashSet<T>.GetInstance();
			int num = 0;
			for (int i = 0; i < this.Count; i++)
			{
				if (instance.Add(this[i]))
				{
					this[num] = this[i];
					num++;
				}
			}
			this.Clip(num);
			instance.Free();
		}

		// Token: 0x06001C45 RID: 7237 RVA: 0x00053704 File Offset: 0x00051904
		public void SortAndRemoveDuplicates(IComparer<T> comparer)
		{
			if (this.Count <= 1)
			{
				return;
			}
			this.Sort(comparer);
			int num = 0;
			for (int i = 1; i < this.Count; i++)
			{
				if (comparer.Compare(this[num], this[i]) < 0)
				{
					num++;
					this[num] = this[i];
				}
			}
			this.Clip(num + 1);
		}

		// Token: 0x06001C46 RID: 7238 RVA: 0x00053768 File Offset: 0x00051968
		[return: Nullable(new byte[] { 0, 1 })]
		public ImmutableArray<S> SelectDistinct<[Nullable(2)] S>(Func<T, S> selector)
		{
			ArrayBuilder<S> instance = ArrayBuilder<S>.GetInstance(this.Count);
			PooledHashSet<S> instance2 = PooledHashSet<S>.GetInstance();
			foreach (T t in this)
			{
				S s = selector.Invoke(t);
				if (instance2.Add(s))
				{
					instance.Add(s);
				}
			}
			instance2.Free();
			return instance.ToImmutableAndFree();
		}

		// Token: 0x04000D46 RID: 3398
		private readonly ImmutableArray<T>.Builder _builder;

		// Token: 0x04000D47 RID: 3399
		[Nullable(new byte[] { 2, 1, 1 })]
		private readonly ObjectPool<ArrayBuilder<T>> _pool;

		// Token: 0x04000D48 RID: 3400
		private static readonly ObjectPool<ArrayBuilder<T>> s_poolInstance = ArrayBuilder<T>.CreatePool();

		// Token: 0x02000638 RID: 1592
		[Nullable(0)]
		private sealed class DebuggerProxy
		{
			// Token: 0x06003862 RID: 14434 RVA: 0x0009D0D5 File Offset: 0x0009B2D5
			public DebuggerProxy(ArrayBuilder<T> builder)
			{
				this._builder = builder;
			}

			// Token: 0x17000FBF RID: 4031
			// (get) Token: 0x06003863 RID: 14435 RVA: 0x0009D0E4 File Offset: 0x0009B2E4
			[DebuggerBrowsable(3)]
			public T[] A
			{
				get
				{
					T[] array = new T[this._builder.Count];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = this._builder[i];
					}
					return array;
				}
			}

			// Token: 0x04001844 RID: 6212
			private readonly ArrayBuilder<T> _builder;
		}

		// Token: 0x02000639 RID: 1593
		[Nullable(0)]
		internal struct Enumerator
		{
			// Token: 0x06003864 RID: 14436 RVA: 0x0009D124 File Offset: 0x0009B324
			public Enumerator(ArrayBuilder<T> builder)
			{
				this._builder = builder;
				this._index = -1;
			}

			// Token: 0x17000FC0 RID: 4032
			// (get) Token: 0x06003865 RID: 14437 RVA: 0x0009D134 File Offset: 0x0009B334
			public T Current
			{
				get
				{
					return this._builder[this._index];
				}
			}

			// Token: 0x06003866 RID: 14438 RVA: 0x0009D147 File Offset: 0x0009B347
			public bool MoveNext()
			{
				this._index++;
				return this._index < this._builder.Count;
			}

			// Token: 0x04001845 RID: 6213
			private readonly ArrayBuilder<T> _builder;

			// Token: 0x04001846 RID: 6214
			private int _index;
		}
	}
}

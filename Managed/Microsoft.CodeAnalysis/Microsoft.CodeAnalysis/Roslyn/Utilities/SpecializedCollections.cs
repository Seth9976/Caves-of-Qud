using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Roslyn.Utilities
{
	// Token: 0x02000061 RID: 97
	[NullableContext(1)]
	[Nullable(0)]
	internal static class SpecializedCollections
	{
		// Token: 0x06000209 RID: 521 RVA: 0x00007206 File Offset: 0x00005406
		public static IEnumerator<T> EmptyEnumerator<[Nullable(2)] T>()
		{
			return SpecializedCollections.Empty.Enumerator<T>.Instance;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000720D File Offset: 0x0000540D
		public static IEnumerable<T> EmptyEnumerable<[Nullable(2)] T>()
		{
			return SpecializedCollections.Empty.List<T>.Instance;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00007214 File Offset: 0x00005414
		public static ICollection<T> EmptyCollection<[Nullable(2)] T>()
		{
			return SpecializedCollections.Empty.List<T>.Instance;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000721B File Offset: 0x0000541B
		public static IList<T> EmptyList<[Nullable(2)] T>()
		{
			return SpecializedCollections.Empty.List<T>.Instance;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00007222 File Offset: 0x00005422
		public static IReadOnlyList<T> EmptyBoxedImmutableArray<[Nullable(2)] T>()
		{
			return SpecializedCollections.Empty.BoxedImmutableArray<T>.Instance;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00007229 File Offset: 0x00005429
		public static IReadOnlyList<T> EmptyReadOnlyList<[Nullable(2)] T>()
		{
			return SpecializedCollections.Empty.List<T>.Instance;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00007230 File Offset: 0x00005430
		public static ISet<T> EmptySet<[Nullable(2)] T>()
		{
			return SpecializedCollections.Empty.Set<T>.Instance;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00007237 File Offset: 0x00005437
		public static IReadOnlySet<T> EmptyReadOnlySet<[Nullable(2)] T>()
		{
			return SpecializedCollections.Empty.Set<T>.Instance;
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000723E File Offset: 0x0000543E
		public static IDictionary<TKey, TValue> EmptyDictionary<TKey, [Nullable(2)] TValue>()
		{
			return SpecializedCollections.Empty.Dictionary<TKey, TValue>.Instance;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00007245 File Offset: 0x00005445
		public static IReadOnlyDictionary<TKey, TValue> EmptyReadOnlyDictionary<TKey, [Nullable(2)] TValue>()
		{
			return SpecializedCollections.Empty.Dictionary<TKey, TValue>.Instance;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000724C File Offset: 0x0000544C
		public static IEnumerable<T> SingletonEnumerable<[Nullable(2)] T>(T value)
		{
			return new SpecializedCollections.Singleton.List<T>(value);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00007254 File Offset: 0x00005454
		public static ICollection<T> SingletonCollection<[Nullable(2)] T>(T value)
		{
			return new SpecializedCollections.Singleton.List<T>(value);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000725C File Offset: 0x0000545C
		public static IEnumerator<T> SingletonEnumerator<[Nullable(2)] T>(T value)
		{
			return new SpecializedCollections.Singleton.Enumerator<T>(value);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00007264 File Offset: 0x00005464
		public static IReadOnlyList<T> SingletonReadOnlyList<[Nullable(2)] T>(T value)
		{
			return new SpecializedCollections.Singleton.List<T>(value);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000726C File Offset: 0x0000546C
		public static IList<T> SingletonList<[Nullable(2)] T>(T value)
		{
			return new SpecializedCollections.Singleton.List<T>(value);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00007274 File Offset: 0x00005474
		public static IEnumerable<T> ReadOnlyEnumerable<[Nullable(2)] T>(IEnumerable<T> values)
		{
			return new SpecializedCollections.ReadOnly.Enumerable<IEnumerable<T>, T>(values);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000727C File Offset: 0x0000547C
		public static ICollection<T> ReadOnlyCollection<[Nullable(2)] T>([Nullable(new byte[] { 2, 1 })] ICollection<T> collection)
		{
			if (collection != null && collection.Count != 0)
			{
				return new SpecializedCollections.ReadOnly.Collection<ICollection<T>, T>(collection);
			}
			return SpecializedCollections.EmptyCollection<T>();
		}

		// Token: 0x0600021A RID: 538 RVA: 0x000072A4 File Offset: 0x000054A4
		public static ISet<T> ReadOnlySet<[Nullable(2)] T>([Nullable(new byte[] { 2, 1 })] ISet<T> set)
		{
			if (set != null && set.Count != 0)
			{
				return new SpecializedCollections.ReadOnly.Set<ISet<T>, T>(set);
			}
			return SpecializedCollections.EmptySet<T>();
		}

		// Token: 0x0600021B RID: 539 RVA: 0x000072CC File Offset: 0x000054CC
		public static IReadOnlySet<T> StronglyTypedReadOnlySet<[Nullable(2)] T>([Nullable(new byte[] { 2, 1 })] ISet<T> set)
		{
			if (set != null && set.Count != 0)
			{
				return new SpecializedCollections.ReadOnly.Set<ISet<T>, T>(set);
			}
			return SpecializedCollections.EmptyReadOnlySet<T>();
		}

		// Token: 0x0200050A RID: 1290
		[Nullable(0)]
		private static class Empty
		{
			// Token: 0x02000799 RID: 1945
			[Nullable(new byte[] { 0, 1 })]
			internal class Collection<[Nullable(2)] T> : SpecializedCollections.Empty.Enumerable<T>, ICollection<T>, IEnumerable<T>, IEnumerable
			{
				// Token: 0x06003F09 RID: 16137 RVA: 0x000B1277 File Offset: 0x000AF477
				protected Collection()
				{
				}

				// Token: 0x06003F0A RID: 16138 RVA: 0x000B127F File Offset: 0x000AF47F
				public void Add(T item)
				{
					throw new NotSupportedException();
				}

				// Token: 0x06003F0B RID: 16139 RVA: 0x000B1286 File Offset: 0x000AF486
				public void Clear()
				{
					throw new NotSupportedException();
				}

				// Token: 0x06003F0C RID: 16140 RVA: 0x000B128D File Offset: 0x000AF48D
				public bool Contains(T item)
				{
					return false;
				}

				// Token: 0x06003F0D RID: 16141 RVA: 0x000B1290 File Offset: 0x000AF490
				public void CopyTo(T[] array, int arrayIndex)
				{
				}

				// Token: 0x17001168 RID: 4456
				// (get) Token: 0x06003F0E RID: 16142 RVA: 0x000B1292 File Offset: 0x000AF492
				public int Count
				{
					get
					{
						return 0;
					}
				}

				// Token: 0x17001169 RID: 4457
				// (get) Token: 0x06003F0F RID: 16143 RVA: 0x000B1295 File Offset: 0x000AF495
				public bool IsReadOnly
				{
					get
					{
						return true;
					}
				}

				// Token: 0x06003F10 RID: 16144 RVA: 0x000B1298 File Offset: 0x000AF498
				public bool Remove(T item)
				{
					throw new NotSupportedException();
				}

				// Token: 0x04001D8C RID: 7564
				public static readonly ICollection<T> Instance = new SpecializedCollections.Empty.Collection<T>();
			}

			// Token: 0x0200079A RID: 1946
			[Nullable(0)]
			internal class Dictionary<TKey, [Nullable(2)] TValue> : SpecializedCollections.Empty.Collection<KeyValuePair<TKey, TValue>>, IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable, IReadOnlyDictionary<TKey, TValue>, IReadOnlyCollection<KeyValuePair<TKey, TValue>>
			{
				// Token: 0x06003F12 RID: 16146 RVA: 0x000B12AB File Offset: 0x000AF4AB
				private Dictionary()
				{
				}

				// Token: 0x06003F13 RID: 16147 RVA: 0x000B12B3 File Offset: 0x000AF4B3
				public void Add(TKey key, TValue value)
				{
					throw new NotSupportedException();
				}

				// Token: 0x06003F14 RID: 16148 RVA: 0x000B12BA File Offset: 0x000AF4BA
				public bool ContainsKey(TKey key)
				{
					return false;
				}

				// Token: 0x1700116A RID: 4458
				// (get) Token: 0x06003F15 RID: 16149 RVA: 0x000B12BD File Offset: 0x000AF4BD
				public ICollection<TKey> Keys
				{
					get
					{
						return SpecializedCollections.Empty.Collection<TKey>.Instance;
					}
				}

				// Token: 0x1700116B RID: 4459
				// (get) Token: 0x06003F16 RID: 16150 RVA: 0x000B12C4 File Offset: 0x000AF4C4
				IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys
				{
					get
					{
						return this.Keys;
					}
				}

				// Token: 0x1700116C RID: 4460
				// (get) Token: 0x06003F17 RID: 16151 RVA: 0x000B12CC File Offset: 0x000AF4CC
				IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values
				{
					get
					{
						return this.Values;
					}
				}

				// Token: 0x06003F18 RID: 16152 RVA: 0x000B12D4 File Offset: 0x000AF4D4
				public bool Remove(TKey key)
				{
					throw new NotSupportedException();
				}

				// Token: 0x06003F19 RID: 16153 RVA: 0x000B12DB File Offset: 0x000AF4DB
				public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
				{
					value = default(TValue);
					return false;
				}

				// Token: 0x1700116D RID: 4461
				// (get) Token: 0x06003F1A RID: 16154 RVA: 0x000B12E5 File Offset: 0x000AF4E5
				public ICollection<TValue> Values
				{
					get
					{
						return SpecializedCollections.Empty.Collection<TValue>.Instance;
					}
				}

				// Token: 0x1700116E RID: 4462
				public TValue this[TKey key]
				{
					get
					{
						throw new NotSupportedException();
					}
					set
					{
						throw new NotSupportedException();
					}
				}

				// Token: 0x04001D8D RID: 7565
				public new static readonly SpecializedCollections.Empty.Dictionary<TKey, TValue> Instance = new SpecializedCollections.Empty.Dictionary<TKey, TValue>();
			}

			// Token: 0x0200079B RID: 1947
			[Nullable(0)]
			internal class Enumerable<[Nullable(2)] T> : IEnumerable<T>, IEnumerable
			{
				// Token: 0x06003F1E RID: 16158 RVA: 0x000B1306 File Offset: 0x000AF506
				public IEnumerator<T> GetEnumerator()
				{
					return this._enumerator;
				}

				// Token: 0x06003F1F RID: 16159 RVA: 0x000B130E File Offset: 0x000AF50E
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x04001D8E RID: 7566
				private readonly IEnumerator<T> _enumerator = SpecializedCollections.Empty.Enumerator<T>.Instance;
			}

			// Token: 0x0200079C RID: 1948
			[NullableContext(0)]
			internal class Enumerator : IEnumerator
			{
				// Token: 0x06003F21 RID: 16161 RVA: 0x000B1329 File Offset: 0x000AF529
				protected Enumerator()
				{
				}

				// Token: 0x1700116F RID: 4463
				// (get) Token: 0x06003F22 RID: 16162 RVA: 0x000B1331 File Offset: 0x000AF531
				[Nullable(2)]
				public object Current
				{
					[NullableContext(2)]
					get
					{
						throw new InvalidOperationException();
					}
				}

				// Token: 0x06003F23 RID: 16163 RVA: 0x000B1338 File Offset: 0x000AF538
				public bool MoveNext()
				{
					return false;
				}

				// Token: 0x06003F24 RID: 16164 RVA: 0x000B133B File Offset: 0x000AF53B
				public void Reset()
				{
					throw new InvalidOperationException();
				}

				// Token: 0x04001D8F RID: 7567
				[Nullable(1)]
				public static readonly IEnumerator Instance = new SpecializedCollections.Empty.Enumerator();
			}

			// Token: 0x0200079D RID: 1949
			[NullableContext(0)]
			internal class Enumerator<[Nullable(2)] T> : SpecializedCollections.Empty.Enumerator, IEnumerator<T>, IEnumerator, IDisposable
			{
				// Token: 0x06003F26 RID: 16166 RVA: 0x000B134E File Offset: 0x000AF54E
				protected Enumerator()
				{
				}

				// Token: 0x17001170 RID: 4464
				// (get) Token: 0x06003F27 RID: 16167 RVA: 0x000B1358 File Offset: 0x000AF558
				[Nullable(1)]
				public new T Current
				{
					[NullableContext(1)]
					get
					{
						throw new InvalidOperationException();
					}
				}

				// Token: 0x06003F28 RID: 16168 RVA: 0x000B136A File Offset: 0x000AF56A
				public void Dispose()
				{
				}

				// Token: 0x04001D90 RID: 7568
				[Nullable(1)]
				public new static readonly IEnumerator<T> Instance = new SpecializedCollections.Empty.Enumerator<T>();
			}

			// Token: 0x0200079E RID: 1950
			[NullableContext(0)]
			internal static class BoxedImmutableArray<[Nullable(2)] T>
			{
				// Token: 0x04001D91 RID: 7569
				[Nullable(1)]
				public static readonly IReadOnlyList<T> Instance = ImmutableArray<T>.Empty;
			}

			// Token: 0x0200079F RID: 1951
			[Nullable(new byte[] { 0, 1 })]
			internal class List<[Nullable(2)] T> : SpecializedCollections.Empty.Collection<T>, IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IReadOnlyList<T>, IReadOnlyCollection<T>
			{
				// Token: 0x06003F2B RID: 16171 RVA: 0x000B1389 File Offset: 0x000AF589
				protected List()
				{
				}

				// Token: 0x06003F2C RID: 16172 RVA: 0x000B1391 File Offset: 0x000AF591
				public int IndexOf(T item)
				{
					return -1;
				}

				// Token: 0x06003F2D RID: 16173 RVA: 0x000B1394 File Offset: 0x000AF594
				public void Insert(int index, T item)
				{
					throw new NotSupportedException();
				}

				// Token: 0x06003F2E RID: 16174 RVA: 0x000B139B File Offset: 0x000AF59B
				public void RemoveAt(int index)
				{
					throw new NotSupportedException();
				}

				// Token: 0x17001171 RID: 4465
				public T this[int index]
				{
					get
					{
						throw new ArgumentOutOfRangeException("index");
					}
					set
					{
						throw new NotSupportedException();
					}
				}

				// Token: 0x04001D92 RID: 7570
				public new static readonly SpecializedCollections.Empty.List<T> Instance = new SpecializedCollections.Empty.List<T>();
			}

			// Token: 0x020007A0 RID: 1952
			[Nullable(new byte[] { 0, 1 })]
			internal class Set<[Nullable(2)] T> : SpecializedCollections.Empty.Collection<T>, ISet<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IReadOnlySet<T>
			{
				// Token: 0x06003F32 RID: 16178 RVA: 0x000B13C1 File Offset: 0x000AF5C1
				protected Set()
				{
				}

				// Token: 0x06003F33 RID: 16179 RVA: 0x000B13C9 File Offset: 0x000AF5C9
				public new bool Add(T item)
				{
					throw new NotSupportedException();
				}

				// Token: 0x06003F34 RID: 16180 RVA: 0x000B13D0 File Offset: 0x000AF5D0
				public void ExceptWith(IEnumerable<T> other)
				{
					throw new NotSupportedException();
				}

				// Token: 0x06003F35 RID: 16181 RVA: 0x000B13D7 File Offset: 0x000AF5D7
				public void IntersectWith(IEnumerable<T> other)
				{
					throw new NotSupportedException();
				}

				// Token: 0x06003F36 RID: 16182 RVA: 0x000B13DE File Offset: 0x000AF5DE
				public bool IsProperSubsetOf(IEnumerable<T> other)
				{
					return !other.IsEmpty<T>();
				}

				// Token: 0x06003F37 RID: 16183 RVA: 0x000B13E9 File Offset: 0x000AF5E9
				public bool IsProperSupersetOf(IEnumerable<T> other)
				{
					return false;
				}

				// Token: 0x06003F38 RID: 16184 RVA: 0x000B13EC File Offset: 0x000AF5EC
				public bool IsSubsetOf(IEnumerable<T> other)
				{
					return true;
				}

				// Token: 0x06003F39 RID: 16185 RVA: 0x000B13EF File Offset: 0x000AF5EF
				public bool IsSupersetOf(IEnumerable<T> other)
				{
					return other.IsEmpty<T>();
				}

				// Token: 0x06003F3A RID: 16186 RVA: 0x000B13F7 File Offset: 0x000AF5F7
				public bool Overlaps(IEnumerable<T> other)
				{
					return false;
				}

				// Token: 0x06003F3B RID: 16187 RVA: 0x000B13FA File Offset: 0x000AF5FA
				public bool SetEquals(IEnumerable<T> other)
				{
					return other.IsEmpty<T>();
				}

				// Token: 0x06003F3C RID: 16188 RVA: 0x000B1402 File Offset: 0x000AF602
				public void SymmetricExceptWith(IEnumerable<T> other)
				{
					throw new NotSupportedException();
				}

				// Token: 0x06003F3D RID: 16189 RVA: 0x000B1409 File Offset: 0x000AF609
				public void UnionWith(IEnumerable<T> other)
				{
					throw new NotSupportedException();
				}

				// Token: 0x04001D93 RID: 7571
				public new static readonly SpecializedCollections.Empty.Set<T> Instance = new SpecializedCollections.Empty.Set<T>();
			}
		}

		// Token: 0x0200050B RID: 1291
		[Nullable(0)]
		private static class ReadOnly
		{
			// Token: 0x020007A1 RID: 1953
			[Nullable(new byte[] { 0, 1, 1 })]
			internal class Collection<[Nullable(0)] TUnderlying, [Nullable(2)] T> : SpecializedCollections.ReadOnly.Enumerable<TUnderlying, T>, ICollection<T>, IEnumerable<T>, IEnumerable where TUnderlying : ICollection<T>
			{
				// Token: 0x06003F3F RID: 16191 RVA: 0x000B141C File Offset: 0x000AF61C
				public Collection(TUnderlying underlying)
					: base(underlying)
				{
				}

				// Token: 0x06003F40 RID: 16192 RVA: 0x000B1425 File Offset: 0x000AF625
				public void Add(T item)
				{
					throw new NotSupportedException();
				}

				// Token: 0x06003F41 RID: 16193 RVA: 0x000B142C File Offset: 0x000AF62C
				public void Clear()
				{
					throw new NotSupportedException();
				}

				// Token: 0x06003F42 RID: 16194 RVA: 0x000B1434 File Offset: 0x000AF634
				public bool Contains(T item)
				{
					TUnderlying underlying = this.Underlying;
					return underlying.Contains(item);
				}

				// Token: 0x06003F43 RID: 16195 RVA: 0x000B1458 File Offset: 0x000AF658
				public void CopyTo(T[] array, int arrayIndex)
				{
					TUnderlying underlying = this.Underlying;
					underlying.CopyTo(array, arrayIndex);
				}

				// Token: 0x17001172 RID: 4466
				// (get) Token: 0x06003F44 RID: 16196 RVA: 0x000B147C File Offset: 0x000AF67C
				public int Count
				{
					get
					{
						TUnderlying underlying = this.Underlying;
						return underlying.Count;
					}
				}

				// Token: 0x17001173 RID: 4467
				// (get) Token: 0x06003F45 RID: 16197 RVA: 0x000B149D File Offset: 0x000AF69D
				public bool IsReadOnly
				{
					get
					{
						return true;
					}
				}

				// Token: 0x06003F46 RID: 16198 RVA: 0x000B14A0 File Offset: 0x000AF6A0
				public bool Remove(T item)
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x020007A2 RID: 1954
			[Nullable(0)]
			internal class Enumerable<[Nullable(0)] TUnderlying> : IEnumerable where TUnderlying : IEnumerable
			{
				// Token: 0x06003F47 RID: 16199 RVA: 0x000B14A7 File Offset: 0x000AF6A7
				public Enumerable(TUnderlying underlying)
				{
					this.Underlying = underlying;
				}

				// Token: 0x06003F48 RID: 16200 RVA: 0x000B14B8 File Offset: 0x000AF6B8
				public IEnumerator GetEnumerator()
				{
					TUnderlying underlying = this.Underlying;
					return underlying.GetEnumerator();
				}

				// Token: 0x04001D94 RID: 7572
				protected readonly TUnderlying Underlying;
			}

			// Token: 0x020007A3 RID: 1955
			[Nullable(new byte[] { 0, 1 })]
			internal class Enumerable<[Nullable(0)] TUnderlying, [Nullable(2)] T> : SpecializedCollections.ReadOnly.Enumerable<TUnderlying>, IEnumerable<T>, IEnumerable where TUnderlying : IEnumerable<T>
			{
				// Token: 0x06003F49 RID: 16201 RVA: 0x000B14D9 File Offset: 0x000AF6D9
				public Enumerable(TUnderlying underlying)
					: base(underlying)
				{
				}

				// Token: 0x06003F4A RID: 16202 RVA: 0x000B14E4 File Offset: 0x000AF6E4
				public new IEnumerator<T> GetEnumerator()
				{
					TUnderlying underlying = this.Underlying;
					return underlying.GetEnumerator();
				}
			}

			// Token: 0x020007A4 RID: 1956
			[Nullable(new byte[] { 0, 1, 1 })]
			internal class Set<[Nullable(0)] TUnderlying, [Nullable(2)] T> : SpecializedCollections.ReadOnly.Collection<TUnderlying, T>, ISet<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IReadOnlySet<T> where TUnderlying : ISet<T>
			{
				// Token: 0x06003F4B RID: 16203 RVA: 0x000B1505 File Offset: 0x000AF705
				public Set(TUnderlying underlying)
					: base(underlying)
				{
				}

				// Token: 0x06003F4C RID: 16204 RVA: 0x000B150E File Offset: 0x000AF70E
				public new bool Add(T item)
				{
					throw new NotSupportedException();
				}

				// Token: 0x06003F4D RID: 16205 RVA: 0x000B1515 File Offset: 0x000AF715
				public void ExceptWith(IEnumerable<T> other)
				{
					throw new NotSupportedException();
				}

				// Token: 0x06003F4E RID: 16206 RVA: 0x000B151C File Offset: 0x000AF71C
				public void IntersectWith(IEnumerable<T> other)
				{
					throw new NotSupportedException();
				}

				// Token: 0x06003F4F RID: 16207 RVA: 0x000B1524 File Offset: 0x000AF724
				public bool IsProperSubsetOf(IEnumerable<T> other)
				{
					TUnderlying underlying = this.Underlying;
					return underlying.IsProperSubsetOf(other);
				}

				// Token: 0x06003F50 RID: 16208 RVA: 0x000B1548 File Offset: 0x000AF748
				public bool IsProperSupersetOf(IEnumerable<T> other)
				{
					TUnderlying underlying = this.Underlying;
					return underlying.IsProperSupersetOf(other);
				}

				// Token: 0x06003F51 RID: 16209 RVA: 0x000B156C File Offset: 0x000AF76C
				public bool IsSubsetOf(IEnumerable<T> other)
				{
					TUnderlying underlying = this.Underlying;
					return underlying.IsSubsetOf(other);
				}

				// Token: 0x06003F52 RID: 16210 RVA: 0x000B1590 File Offset: 0x000AF790
				public bool IsSupersetOf(IEnumerable<T> other)
				{
					TUnderlying underlying = this.Underlying;
					return underlying.IsSupersetOf(other);
				}

				// Token: 0x06003F53 RID: 16211 RVA: 0x000B15B4 File Offset: 0x000AF7B4
				public bool Overlaps(IEnumerable<T> other)
				{
					TUnderlying underlying = this.Underlying;
					return underlying.Overlaps(other);
				}

				// Token: 0x06003F54 RID: 16212 RVA: 0x000B15D8 File Offset: 0x000AF7D8
				public bool SetEquals(IEnumerable<T> other)
				{
					TUnderlying underlying = this.Underlying;
					return underlying.SetEquals(other);
				}

				// Token: 0x06003F55 RID: 16213 RVA: 0x000B15FA File Offset: 0x000AF7FA
				public void SymmetricExceptWith(IEnumerable<T> other)
				{
					throw new NotSupportedException();
				}

				// Token: 0x06003F56 RID: 16214 RVA: 0x000B1601 File Offset: 0x000AF801
				public void UnionWith(IEnumerable<T> other)
				{
					throw new NotSupportedException();
				}
			}
		}

		// Token: 0x0200050C RID: 1292
		[Nullable(0)]
		private static class Singleton
		{
			// Token: 0x020007A5 RID: 1957
			[Nullable(0)]
			internal sealed class List<[Nullable(2)] T> : IReadOnlyList<T>, IEnumerable<T>, IEnumerable, IReadOnlyCollection<T>, IList<T>, ICollection<T>
			{
				// Token: 0x06003F57 RID: 16215 RVA: 0x000B1608 File Offset: 0x000AF808
				public List(T value)
				{
					this._loneValue = value;
				}

				// Token: 0x06003F58 RID: 16216 RVA: 0x000B1617 File Offset: 0x000AF817
				public void Add(T item)
				{
					throw new NotSupportedException();
				}

				// Token: 0x06003F59 RID: 16217 RVA: 0x000B161E File Offset: 0x000AF81E
				public void Clear()
				{
					throw new NotSupportedException();
				}

				// Token: 0x06003F5A RID: 16218 RVA: 0x000B1625 File Offset: 0x000AF825
				public bool Contains(T item)
				{
					return EqualityComparer<T>.Default.Equals(this._loneValue, item);
				}

				// Token: 0x06003F5B RID: 16219 RVA: 0x000B1638 File Offset: 0x000AF838
				public void CopyTo(T[] array, int arrayIndex)
				{
					array[arrayIndex] = this._loneValue;
				}

				// Token: 0x17001174 RID: 4468
				// (get) Token: 0x06003F5C RID: 16220 RVA: 0x000B1647 File Offset: 0x000AF847
				public int Count
				{
					get
					{
						return 1;
					}
				}

				// Token: 0x17001175 RID: 4469
				// (get) Token: 0x06003F5D RID: 16221 RVA: 0x000B164A File Offset: 0x000AF84A
				public bool IsReadOnly
				{
					get
					{
						return true;
					}
				}

				// Token: 0x06003F5E RID: 16222 RVA: 0x000B164D File Offset: 0x000AF84D
				public bool Remove(T item)
				{
					throw new NotSupportedException();
				}

				// Token: 0x06003F5F RID: 16223 RVA: 0x000B1654 File Offset: 0x000AF854
				public IEnumerator<T> GetEnumerator()
				{
					return new SpecializedCollections.Singleton.Enumerator<T>(this._loneValue);
				}

				// Token: 0x06003F60 RID: 16224 RVA: 0x000B1661 File Offset: 0x000AF861
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x17001176 RID: 4470
				public T this[int index]
				{
					get
					{
						if (index != 0)
						{
							throw new IndexOutOfRangeException();
						}
						return this._loneValue;
					}
					set
					{
						throw new NotSupportedException();
					}
				}

				// Token: 0x06003F63 RID: 16227 RVA: 0x000B1681 File Offset: 0x000AF881
				public int IndexOf(T item)
				{
					if (object.Equals(this._loneValue, item))
					{
						return 0;
					}
					return -1;
				}

				// Token: 0x06003F64 RID: 16228 RVA: 0x000B169E File Offset: 0x000AF89E
				public void Insert(int index, T item)
				{
					throw new NotSupportedException();
				}

				// Token: 0x06003F65 RID: 16229 RVA: 0x000B16A5 File Offset: 0x000AF8A5
				public void RemoveAt(int index)
				{
					throw new NotSupportedException();
				}

				// Token: 0x04001D95 RID: 7573
				private readonly T _loneValue;
			}

			// Token: 0x020007A6 RID: 1958
			[Nullable(0)]
			internal class Enumerator<[Nullable(2)] T> : IEnumerator<T>, IEnumerator, IDisposable
			{
				// Token: 0x06003F66 RID: 16230 RVA: 0x000B16AC File Offset: 0x000AF8AC
				public Enumerator(T value)
				{
					this._loneValue = value;
					this._moveNextCalled = false;
				}

				// Token: 0x17001177 RID: 4471
				// (get) Token: 0x06003F67 RID: 16231 RVA: 0x000B16C2 File Offset: 0x000AF8C2
				public T Current
				{
					get
					{
						return this._loneValue;
					}
				}

				// Token: 0x17001178 RID: 4472
				// (get) Token: 0x06003F68 RID: 16232 RVA: 0x000B16CA File Offset: 0x000AF8CA
				[Nullable(2)]
				object IEnumerator.Current
				{
					[NullableContext(2)]
					get
					{
						return this._loneValue;
					}
				}

				// Token: 0x06003F69 RID: 16233 RVA: 0x000B16D7 File Offset: 0x000AF8D7
				public void Dispose()
				{
				}

				// Token: 0x06003F6A RID: 16234 RVA: 0x000B16D9 File Offset: 0x000AF8D9
				public bool MoveNext()
				{
					if (!this._moveNextCalled)
					{
						this._moveNextCalled = true;
						return true;
					}
					return false;
				}

				// Token: 0x06003F6B RID: 16235 RVA: 0x000B16ED File Offset: 0x000AF8ED
				public void Reset()
				{
					this._moveNextCalled = false;
				}

				// Token: 0x04001D96 RID: 7574
				private readonly T _loneValue;

				// Token: 0x04001D97 RID: 7575
				private bool _moveNextCalled;
			}
		}
	}
}

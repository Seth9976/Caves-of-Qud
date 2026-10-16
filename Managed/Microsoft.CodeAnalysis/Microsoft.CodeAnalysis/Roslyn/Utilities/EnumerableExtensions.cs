using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Roslyn.Utilities
{
	// Token: 0x0200003D RID: 61
	[NullableContext(1)]
	[Nullable(0)]
	internal static class EnumerableExtensions
	{
		// Token: 0x0600011B RID: 283 RVA: 0x00004D30 File Offset: 0x00002F30
		public static IEnumerable<T> Do<[Nullable(2)] T>(this IEnumerable<T> source, Action<T> action)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			IList<T> list = source as IList<T>;
			if (list != null)
			{
				int i = 0;
				int count = list.Count;
				while (i < count)
				{
					action.Invoke(list[i]);
					i++;
				}
			}
			else
			{
				foreach (T t in source)
				{
					action.Invoke(t);
				}
			}
			return source;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00004DC4 File Offset: 0x00002FC4
		public static ReadOnlyCollection<T> ToReadOnlyCollection<[Nullable(2)] T>(this IEnumerable<T> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new ReadOnlyCollection<T>(Enumerable.ToList<T>(source));
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00004DDF File Offset: 0x00002FDF
		public static IEnumerable<T> Concat<[Nullable(2)] T>(this IEnumerable<T> source, T value)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return source.ConcatWorker(value);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00004DF6 File Offset: 0x00002FF6
		private static IEnumerable<T> ConcatWorker<[Nullable(2)] T>(this IEnumerable<T> source, T value)
		{
			foreach (T t in source)
			{
				yield return t;
			}
			IEnumerator<T> enumerator = null;
			yield return value;
			yield break;
			yield break;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00004E0D File Offset: 0x0000300D
		public static bool SetEquals<[Nullable(2)] T>(this IEnumerable<T> source1, IEnumerable<T> source2, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> comparer)
		{
			if (source1 == null)
			{
				throw new ArgumentNullException("source1");
			}
			if (source2 == null)
			{
				throw new ArgumentNullException("source2");
			}
			return source1.ToSet<T>(comparer).SetEquals(source2);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00004E38 File Offset: 0x00003038
		public static bool SetEquals<[Nullable(2)] T>(this IEnumerable<T> source1, IEnumerable<T> source2)
		{
			if (source1 == null)
			{
				throw new ArgumentNullException("source1");
			}
			if (source2 == null)
			{
				throw new ArgumentNullException("source2");
			}
			return source1.ToSet<T>().SetEquals(source2);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00004E62 File Offset: 0x00003062
		public static ISet<T> ToSet<[Nullable(2)] T>(this IEnumerable<T> source, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<T> comparer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new HashSet<T>(source, comparer);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00004E79 File Offset: 0x00003079
		public static ISet<T> ToSet<[Nullable(2)] T>(this IEnumerable<T> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return (source as ISet<T>) ?? new HashSet<T>(source);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00004E9C File Offset: 0x0000309C
		public static IReadOnlyCollection<T> ToCollection<[Nullable(2)] T>(this IEnumerable<T> sequence)
		{
			IReadOnlyCollection<T> readOnlyCollection = sequence as IReadOnlyCollection<T>;
			if (readOnlyCollection == null)
			{
				return Enumerable.ToList<T>(sequence);
			}
			return readOnlyCollection;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00004EBD File Offset: 0x000030BD
		[NullableContext(0)]
		public static T? FirstOrNull<T>([Nullable(new byte[] { 1, 0 })] this IEnumerable<T> source) where T : struct
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return Enumerable.FirstOrDefault<T?>(Enumerable.Cast<T?>(source));
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00004ED8 File Offset: 0x000030D8
		[NullableContext(0)]
		public static T? FirstOrNull<T>([Nullable(new byte[] { 1, 0 })] this IEnumerable<T> source, [Nullable(new byte[] { 1, 0 })] Func<T, bool> predicate) where T : struct
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return Enumerable.FirstOrDefault<T?>(Enumerable.Cast<T?>(source), (T? v) => predicate.Invoke(v.Value));
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00004F17 File Offset: 0x00003117
		[NullableContext(0)]
		public static T? LastOrNull<T>([Nullable(new byte[] { 1, 0 })] this IEnumerable<T> source) where T : struct
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return Enumerable.LastOrDefault<T?>(Enumerable.Cast<T?>(source));
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00004F34 File Offset: 0x00003134
		[NullableContext(0)]
		public static T? SingleOrNull<T>([Nullable(new byte[] { 1, 0 })] this IEnumerable<T> source, [Nullable(new byte[] { 1, 0 })] Func<T, bool> predicate) where T : struct
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return Enumerable.SingleOrDefault<T?>(Enumerable.Cast<T?>(source), (T? v) => predicate.Invoke(v.Value));
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00004F74 File Offset: 0x00003174
		public static bool IsSingle<[Nullable(2)] T>(this IEnumerable<T> list)
		{
			bool flag;
			using (IEnumerator<T> enumerator = list.GetEnumerator())
			{
				flag = enumerator.MoveNext() && !enumerator.MoveNext();
			}
			return flag;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00004FBC File Offset: 0x000031BC
		public static bool IsEmpty<[Nullable(2)] T>(this IEnumerable<T> source)
		{
			IReadOnlyCollection<T> readOnlyCollection = source as IReadOnlyCollection<T>;
			if (readOnlyCollection != null)
			{
				return readOnlyCollection.Count == 0;
			}
			ICollection<T> collection = source as ICollection<T>;
			if (collection != null)
			{
				return collection.Count == 0;
			}
			ICollection collection2 = source as ICollection;
			if (collection2 != null)
			{
				return collection2.Count == 0;
			}
			string text = source as string;
			if (text != null)
			{
				return text.Length == 0;
			}
			using (IEnumerator<T> enumerator = source.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					T t = enumerator.Current;
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000505C File Offset: 0x0000325C
		public static bool IsEmpty<[Nullable(2)] T>(this IReadOnlyCollection<T> source)
		{
			return source.Count == 0;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00005067 File Offset: 0x00003267
		public static bool IsEmpty<[Nullable(2)] T>(this ICollection<T> source)
		{
			return source.Count == 0;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00005072 File Offset: 0x00003272
		public static bool IsEmpty(this string source)
		{
			return source.Length == 0;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000507D File Offset: 0x0000327D
		public static bool IsEmpty<[Nullable(2)] T>(this T[] source)
		{
			return source.Length == 0;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00005084 File Offset: 0x00003284
		public static bool IsEmpty<[Nullable(2)] T>(this List<T> source)
		{
			return source.Count == 0;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000508F File Offset: 0x0000328F
		public static IEnumerable<T> WhereNotNull<T>([Nullable(new byte[] { 1, 2 })] this IEnumerable<T> source) where T : class
		{
			if (source == null)
			{
				return SpecializedCollections.EmptyEnumerable<T>();
			}
			return Enumerable.Where<T>(source, EnumerableExtensions.s_notNullTest);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x000050A5 File Offset: 0x000032A5
		public static T[] AsArray<[Nullable(2)] T>(this IEnumerable<T> source)
		{
			return (source as T[]) ?? Enumerable.ToArray<T>(source);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000050B7 File Offset: 0x000032B7
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 1 })]
		public static ImmutableArray<TResult> SelectAsArray<TSource, TResult>([Nullable(new byte[] { 2, 1 })] this IEnumerable<TSource> source, [Nullable(1)] Func<TSource, TResult> selector)
		{
			if (source == null)
			{
				return ImmutableArray<TResult>.Empty;
			}
			ArrayBuilder<TResult> instance = ArrayBuilder<TResult>.GetInstance();
			instance.AddRange(Enumerable.Select<TSource, TResult>(source, selector));
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000050DC File Offset: 0x000032DC
		[NullableContext(2)]
		[return: Nullable(new byte[] { 0, 0, 1 })]
		public static ValueTask<ImmutableArray<TResult>> SelectAsArrayAsync<TItem, TResult>([Nullable(1)] this IEnumerable<TItem> source, [Nullable(new byte[] { 1, 1, 0, 1 })] Func<TItem, ValueTask<TResult>> selector)
		{
			EnumerableExtensions.<SelectAsArrayAsync>d__24<TItem, TResult> <SelectAsArrayAsync>d__;
			<SelectAsArrayAsync>d__.<>t__builder = AsyncValueTaskMethodBuilder<ImmutableArray<TResult>>.Create();
			<SelectAsArrayAsync>d__.source = source;
			<SelectAsArrayAsync>d__.selector = selector;
			<SelectAsArrayAsync>d__.<>1__state = -1;
			<SelectAsArrayAsync>d__.<>t__builder.Start<EnumerableExtensions.<SelectAsArrayAsync>d__24<TItem, TResult>>(ref <SelectAsArrayAsync>d__);
			return <SelectAsArrayAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00005128 File Offset: 0x00003328
		public static bool All(this IEnumerable<bool> source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			using (IEnumerator<bool> enumerator = source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00005180 File Offset: 0x00003380
		public static int IndexOf<[Nullable(2)] T>(this IEnumerable<T> sequence, T value)
		{
			IList<T> list = sequence as IList<T>;
			int num;
			if (list == null)
			{
				IReadOnlyList<T> readOnlyList = sequence as IReadOnlyList<T>;
				if (readOnlyList == null)
				{
					num = sequence.EnumeratingIndexOf(value, EqualityComparer<T>.Default);
				}
				else
				{
					num = readOnlyList.IndexOf(value, EqualityComparer<T>.Default);
				}
			}
			else
			{
				num = list.IndexOf(value);
			}
			return num;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000051CC File Offset: 0x000033CC
		public static int IndexOf<[Nullable(2)] T>(this IEnumerable<T> sequence, T value, IEqualityComparer<T> comparer)
		{
			IReadOnlyList<T> readOnlyList = sequence as IReadOnlyList<T>;
			int num;
			if (readOnlyList != null)
			{
				num = readOnlyList.IndexOf(value, comparer);
			}
			else
			{
				num = sequence.EnumeratingIndexOf(value, comparer);
			}
			return num;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000051F8 File Offset: 0x000033F8
		private static int EnumeratingIndexOf<[Nullable(2)] T>(this IEnumerable<T> sequence, T value, IEqualityComparer<T> comparer)
		{
			int num = 0;
			foreach (T t in sequence)
			{
				if (comparer.Equals(t, value))
				{
					return num;
				}
				num++;
			}
			return -1;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00005250 File Offset: 0x00003450
		public static int IndexOf<[Nullable(2)] T>(this IReadOnlyList<T> list, T value, IEqualityComparer<T> comparer)
		{
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				if (comparer.Equals(list[i], value))
				{
					return i;
				}
				i++;
			}
			return -1;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00005283 File Offset: 0x00003483
		public static IEnumerable<T> Flatten<[Nullable(2)] T>(this IEnumerable<IEnumerable<T>> sequence)
		{
			if (sequence == null)
			{
				throw new ArgumentNullException("sequence");
			}
			return Enumerable.SelectMany<IEnumerable<T>, T>(sequence, (IEnumerable<T> s) => s);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x000052B8 File Offset: 0x000034B8
		public static IOrderedEnumerable<T> OrderBy<[Nullable(2)] T>(this IEnumerable<T> source, [Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
		{
			return Enumerable.OrderBy<T, T>(source, Functions<T>.Identity, comparer);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x000052C6 File Offset: 0x000034C6
		public static IOrderedEnumerable<T> OrderByDescending<[Nullable(2)] T>(this IEnumerable<T> source, [Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
		{
			return Enumerable.OrderByDescending<T, T>(source, Functions<T>.Identity, comparer);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000052D4 File Offset: 0x000034D4
		public static IOrderedEnumerable<T> OrderBy<[Nullable(2)] T>(this IEnumerable<T> source, Comparison<T> compare)
		{
			return source.OrderBy<T>(Comparer<T>.Create(compare));
		}

		// Token: 0x0600013C RID: 316 RVA: 0x000052E2 File Offset: 0x000034E2
		public static IOrderedEnumerable<T> OrderByDescending<[Nullable(2)] T>(this IEnumerable<T> source, Comparison<T> compare)
		{
			return source.OrderByDescending<T>(Comparer<T>.Create(compare));
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000052F0 File Offset: 0x000034F0
		public static IOrderedEnumerable<T> Order<[Nullable(0)] T>(this IEnumerable<T> source) where T : IComparable<T>
		{
			return source.OrderBy<T>(EnumerableExtensions.Comparisons<T>.Comparer);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000052FD File Offset: 0x000034FD
		public static IOrderedEnumerable<T> ThenBy<[Nullable(2)] T>(this IOrderedEnumerable<T> source, [Nullable(new byte[] { 2, 1 })] IComparer<T> comparer)
		{
			return Enumerable.ThenBy<T, T>(source, Functions<T>.Identity, comparer);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000530B File Offset: 0x0000350B
		public static IOrderedEnumerable<T> ThenBy<[Nullable(2)] T>(this IOrderedEnumerable<T> source, Comparison<T> compare)
		{
			return source.ThenBy<T>(Comparer<T>.Create(compare));
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00005319 File Offset: 0x00003519
		public static IOrderedEnumerable<T> ThenBy<[Nullable(0)] T>(this IOrderedEnumerable<T> source) where T : IComparable<T>
		{
			return source.ThenBy<T>(EnumerableExtensions.Comparisons<T>.Comparer);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00005328 File Offset: 0x00003528
		public static bool IsSorted<[Nullable(2)] T>(this IEnumerable<T> enumerable, IComparer<T> comparer)
		{
			bool flag;
			using (IEnumerator<T> enumerator = enumerable.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					flag = true;
				}
				else
				{
					T t = enumerator.Current;
					while (enumerator.MoveNext())
					{
						if (comparer.Compare(t, enumerator.Current) > 0)
						{
							return false;
						}
						t = enumerator.Current;
					}
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00005394 File Offset: 0x00003594
		public static bool Contains<[Nullable(2)] T>(this IEnumerable<T> sequence, Func<T, bool> predicate)
		{
			return Enumerable.Any<T>(sequence, predicate);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x000053A0 File Offset: 0x000035A0
		[NullableContext(2)]
		public static bool Contains([Nullable(new byte[] { 1, 2 })] this IEnumerable<string> sequence, string s)
		{
			using (IEnumerator<string> enumerator = sequence.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current == s)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000053F0 File Offset: 0x000035F0
		public static IComparer<T> ToComparer<[Nullable(2)] T>(this Comparison<T> comparison)
		{
			return Comparer<T>.Create(comparison);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000053F8 File Offset: 0x000035F8
		public static ImmutableDictionary<K, V> ToImmutableDictionaryOrEmpty<K, [Nullable(2)] V>([Nullable(new byte[] { 2, 0, 1, 1 })] this IEnumerable<KeyValuePair<K, V>> items)
		{
			if (items == null)
			{
				return ImmutableDictionary.Create<K, V>();
			}
			return ImmutableDictionary.CreateRange<K, V>(items);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00005409 File Offset: 0x00003609
		public static ImmutableDictionary<K, V> ToImmutableDictionaryOrEmpty<K, [Nullable(2)] V>([Nullable(new byte[] { 2, 0, 1, 1 })] this IEnumerable<KeyValuePair<K, V>> items, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<K> keyComparer)
		{
			if (items == null)
			{
				return ImmutableDictionary.Create<K, V>(keyComparer);
			}
			return ImmutableDictionary.CreateRange<K, V>(keyComparer, items);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000541C File Offset: 0x0000361C
		[NullableContext(0)]
		internal static IList<IList<T>> Transpose<T>(this IEnumerable<IEnumerable<T>> data)
		{
			return Enumerable.ToArray<IList<T>>(data.TransposeInternal<T>());
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00005429 File Offset: 0x00003629
		[NullableContext(0)]
		private static IEnumerable<IList<T>> TransposeInternal<T>(this IEnumerable<IEnumerable<T>> data)
		{
			List<IEnumerator<T>> enumerators = new List<IEnumerator<T>>();
			int width = 0;
			foreach (IEnumerable<T> enumerable in data)
			{
				enumerators.Add(enumerable.GetEnumerator());
				width++;
			}
			try
			{
				for (;;)
				{
					T[] array = null;
					for (int i = 0; i < width; i++)
					{
						IEnumerator<T> enumerator2 = enumerators[i];
						if (!enumerator2.MoveNext())
						{
							goto Block_8;
						}
						if (array == null)
						{
							array = new T[width];
						}
						array[i] = enumerator2.Current;
					}
					yield return array;
				}
				Block_8:
				yield break;
			}
			finally
			{
				foreach (IEnumerator<T> enumerator4 in enumerators)
				{
					enumerator4.Dispose();
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000543C File Offset: 0x0000363C
		[return: Nullable(new byte[] { 1, 1, 0, 1 })]
		internal static Dictionary<K, ImmutableArray<T>> ToDictionary<K, [Nullable(2)] T>(this IEnumerable<T> data, Func<T, K> keySelector, [Nullable(new byte[] { 2, 1 })] IEqualityComparer<K> comparer = null)
		{
			Dictionary<K, ImmutableArray<T>> dictionary = new Dictionary<K, ImmutableArray<T>>(comparer);
			foreach (IGrouping<K, T> grouping in Enumerable.GroupBy<T, K>(data, keySelector, comparer))
			{
				ImmutableArray<T> immutableArray = grouping.AsImmutable<T>();
				dictionary.Add(grouping.Key, immutableArray);
			}
			return dictionary;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x000054A0 File Offset: 0x000036A0
		[NullableContext(2)]
		internal static TSource AsSingleton<TSource>([Nullable(new byte[] { 2, 1 })] this IEnumerable<TSource> source)
		{
			if (source == null)
			{
				TSource tsource = default(TSource);
				return tsource;
			}
			IList<TSource> list = source as IList<TSource>;
			if (list == null)
			{
				TSource tsource;
				using (IEnumerator<TSource> enumerator = source.GetEnumerator())
				{
					if (!enumerator.MoveNext())
					{
						tsource = default(TSource);
						tsource = tsource;
					}
					else
					{
						TSource tsource2 = enumerator.Current;
						if (enumerator.MoveNext())
						{
							tsource = default(TSource);
						}
						else
						{
							tsource = tsource2;
						}
					}
				}
				return tsource;
			}
			if (list.Count != 1)
			{
				TSource tsource = default(TSource);
				return tsource;
			}
			return list[0];
		}

		// Token: 0x04000063 RID: 99
		private static readonly Func<object, bool> s_notNullTest = (object x) => x != null;

		// Token: 0x020004F4 RID: 1268
		[Nullable(0)]
		private static class Comparisons<[Nullable(0)] T> where T : IComparable<T>
		{
			// Token: 0x040013E1 RID: 5089
			public static readonly Comparison<T> CompareTo = (T t1, T t2) => t1.CompareTo(t2);

			// Token: 0x040013E2 RID: 5090
			public static readonly IComparer<T> Comparer = Comparer<T>.Create(EnumerableExtensions.Comparisons<T>.CompareTo);
		}
	}
}

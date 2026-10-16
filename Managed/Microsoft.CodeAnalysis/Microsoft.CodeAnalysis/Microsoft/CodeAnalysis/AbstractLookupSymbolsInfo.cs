using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000107 RID: 263
	[NullableContext(1)]
	[Nullable(0)]
	internal abstract class AbstractLookupSymbolsInfo<TSymbol> where TSymbol : class, ISymbolInternal
	{
		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000901 RID: 2305 RVA: 0x00019E6E File Offset: 0x0001806E
		// (set) Token: 0x06000902 RID: 2306 RVA: 0x00019E76 File Offset: 0x00018076
		[Nullable(2)]
		internal string FilterName
		{
			[NullableContext(2)]
			get;
			[NullableContext(2)]
			set;
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x00019E7F File Offset: 0x0001807F
		protected AbstractLookupSymbolsInfo(IEqualityComparer<string> comparer)
		{
			this._comparer = comparer;
			this._nameMap = new Dictionary<string, AbstractLookupSymbolsInfo<TSymbol>.UniqueSymbolOrArities>(comparer);
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x00019E9A File Offset: 0x0001809A
		public bool CanBeAdded(string name)
		{
			return this.FilterName == null || this._comparer.Equals(name, this.FilterName);
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x00019EB8 File Offset: 0x000180B8
		public void AddSymbol(TSymbol symbol, string name, int arity)
		{
			AbstractLookupSymbolsInfo<TSymbol>.UniqueSymbolOrArities uniqueSymbolOrArities;
			if (!this._nameMap.TryGetValue(name, ref uniqueSymbolOrArities))
			{
				uniqueSymbolOrArities = new AbstractLookupSymbolsInfo<TSymbol>.UniqueSymbolOrArities(arity, symbol);
				this._nameMap.Add(name, uniqueSymbolOrArities);
				return;
			}
			uniqueSymbolOrArities.AddSymbol(symbol, arity);
			this._nameMap[name] = uniqueSymbolOrArities;
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000906 RID: 2310 RVA: 0x00019F02 File Offset: 0x00018102
		public ICollection<string> Names
		{
			get
			{
				return this._nameMap.Keys;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000907 RID: 2311 RVA: 0x00019F0F File Offset: 0x0001810F
		public int Count
		{
			get
			{
				return this._nameMap.Count;
			}
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x00019F1C File Offset: 0x0001811C
		public bool TryGetAritiesAndUniqueSymbol(string name, [Nullable(new byte[] { 2, 0 })] out AbstractLookupSymbolsInfo<TSymbol>.IArityEnumerable arities, [Nullable(2)] out TSymbol uniqueSymbol)
		{
			AbstractLookupSymbolsInfo<TSymbol>.UniqueSymbolOrArities uniqueSymbolOrArities;
			if (!this._nameMap.TryGetValue(name, ref uniqueSymbolOrArities))
			{
				arities = null;
				uniqueSymbol = default(TSymbol);
				return false;
			}
			uniqueSymbolOrArities.GetUniqueSymbolOrArities(out arities, out uniqueSymbol);
			return true;
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x00019F4F File Offset: 0x0001814F
		public void Clear()
		{
			this._nameMap.Clear();
			this.FilterName = null;
		}

		// Token: 0x04000290 RID: 656
		private readonly IEqualityComparer<string> _comparer;

		// Token: 0x04000291 RID: 657
		[Nullable(new byte[] { 1, 1, 0, 0 })]
		private readonly Dictionary<string, AbstractLookupSymbolsInfo<TSymbol>.UniqueSymbolOrArities> _nameMap;

		// Token: 0x02000536 RID: 1334
		[NullableContext(0)]
		public struct ArityEnumerator : IEnumerator<int>, IEnumerator, IDisposable
		{
			// Token: 0x06003393 RID: 13203 RVA: 0x00091BBB File Offset: 0x0008FDBB
			[NullableContext(2)]
			internal ArityEnumerator(int bitVector, HashSet<int> arities)
			{
				this._current = -1;
				this._low32bits = bitVector;
				if (arities == null)
				{
					this._arities = null;
					return;
				}
				this._arities = Enumerable.ToArray<int>(arities);
				Array.Sort<int>(this._arities);
			}

			// Token: 0x17000EBF RID: 3775
			// (get) Token: 0x06003394 RID: 13204 RVA: 0x00091BED File Offset: 0x0008FDED
			public int Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x06003395 RID: 13205 RVA: 0x00091BF5 File Offset: 0x0008FDF5
			public void Dispose()
			{
				this._arities = null;
			}

			// Token: 0x17000EC0 RID: 3776
			// (get) Token: 0x06003396 RID: 13206 RVA: 0x00091BFE File Offset: 0x0008FDFE
			[Nullable(2)]
			object IEnumerator.Current
			{
				[NullableContext(2)]
				get
				{
					return this._current;
				}
			}

			// Token: 0x06003397 RID: 13207 RVA: 0x00091C0C File Offset: 0x0008FE0C
			public bool MoveNext()
			{
				if (this._current == 2147483647)
				{
					return false;
				}
				int num = this._current + 1;
				this._current = num;
				int i;
				for (i = num; i < 32; i++)
				{
					if (((this._low32bits >> i) & 1) != 0)
					{
						this._current = i;
						return true;
					}
				}
				if (this._arities != null)
				{
					int num2 = this._arities.BinarySearch(i);
					if (num2 < 0)
					{
						num2 = ~num2;
					}
					if (num2 < this._arities.Length)
					{
						this._current = this._arities[num2];
						return true;
					}
				}
				this._current = int.MaxValue;
				return false;
			}

			// Token: 0x06003398 RID: 13208 RVA: 0x00091CA0 File Offset: 0x0008FEA0
			public void Reset()
			{
				this._current = -1;
			}

			// Token: 0x040014CA RID: 5322
			private int _current;

			// Token: 0x040014CB RID: 5323
			private readonly int _low32bits;

			// Token: 0x040014CC RID: 5324
			[Nullable(2)]
			private int[] _arities;

			// Token: 0x040014CD RID: 5325
			private const int resetValue = -1;

			// Token: 0x040014CE RID: 5326
			private const int reachedEndValue = 2147483647;
		}

		// Token: 0x02000537 RID: 1335
		[NullableContext(0)]
		public interface IArityEnumerable
		{
			// Token: 0x06003399 RID: 13209
			AbstractLookupSymbolsInfo<TSymbol>.ArityEnumerator GetEnumerator();

			// Token: 0x17000EC1 RID: 3777
			// (get) Token: 0x0600339A RID: 13210
			int Count { get; }
		}

		// Token: 0x02000538 RID: 1336
		[NullableContext(0)]
		private struct UniqueSymbolOrArities : AbstractLookupSymbolsInfo<TSymbol>.IArityEnumerable
		{
			// Token: 0x0600339B RID: 13211 RVA: 0x00091CA9 File Offset: 0x0008FEA9
			[NullableContext(1)]
			public UniqueSymbolOrArities(int arity, TSymbol uniqueSymbol)
			{
				this._uniqueSymbolOrArities = uniqueSymbol;
				this._arityBitVectorOrUniqueArity = arity;
			}

			// Token: 0x0600339C RID: 13212 RVA: 0x00091CC0 File Offset: 0x0008FEC0
			[NullableContext(1)]
			public void AddSymbol(TSymbol symbol, int arity)
			{
				if (symbol != null && symbol == this._uniqueSymbolOrArities)
				{
					return;
				}
				if (this.HasUniqueSymbol)
				{
					this._uniqueSymbolOrArities = null;
					int arityBitVectorOrUniqueArity = this._arityBitVectorOrUniqueArity;
					this._arityBitVectorOrUniqueArity = 0;
					this.AddArity(arityBitVectorOrUniqueArity);
				}
				this.AddArity(arity);
			}

			// Token: 0x17000EC2 RID: 3778
			// (get) Token: 0x0600339D RID: 13213 RVA: 0x00091D0F File Offset: 0x0008FF0F
			private bool HasUniqueSymbol
			{
				get
				{
					return this._uniqueSymbolOrArities != null && !(this._uniqueSymbolOrArities is HashSet<int>);
				}
			}

			// Token: 0x0600339E RID: 13214 RVA: 0x00091D2C File Offset: 0x0008FF2C
			private void AddArity(int arity)
			{
				if (arity < 32)
				{
					int num = 1 << arity;
					this._arityBitVectorOrUniqueArity |= num;
					return;
				}
				HashSet<int> hashSet = this._uniqueSymbolOrArities as HashSet<int>;
				if (hashSet == null)
				{
					hashSet = new HashSet<int>();
					this._uniqueSymbolOrArities = hashSet;
				}
				hashSet.Add(arity);
			}

			// Token: 0x0600339F RID: 13215 RVA: 0x00091D78 File Offset: 0x0008FF78
			[NullableContext(2)]
			public void GetUniqueSymbolOrArities([Nullable(new byte[] { 2, 0 })] out AbstractLookupSymbolsInfo<TSymbol>.IArityEnumerable arities, out TSymbol uniqueSymbol)
			{
				if (this.HasUniqueSymbol)
				{
					arities = null;
					uniqueSymbol = (TSymbol)((object)this._uniqueSymbolOrArities);
					return;
				}
				AbstractLookupSymbolsInfo<TSymbol>.IArityEnumerable arityEnumerable2;
				if (this._uniqueSymbolOrArities != null || this._arityBitVectorOrUniqueArity != 0)
				{
					AbstractLookupSymbolsInfo<TSymbol>.IArityEnumerable arityEnumerable = this;
					arityEnumerable2 = arityEnumerable;
				}
				else
				{
					arityEnumerable2 = null;
				}
				arities = arityEnumerable2;
				uniqueSymbol = default(TSymbol);
			}

			// Token: 0x060033A0 RID: 13216 RVA: 0x00091DCB File Offset: 0x0008FFCB
			public AbstractLookupSymbolsInfo<TSymbol>.ArityEnumerator GetEnumerator()
			{
				return new AbstractLookupSymbolsInfo<TSymbol>.ArityEnumerator(this._arityBitVectorOrUniqueArity, (HashSet<int>)this._uniqueSymbolOrArities);
			}

			// Token: 0x17000EC3 RID: 3779
			// (get) Token: 0x060033A1 RID: 13217 RVA: 0x00091DE4 File Offset: 0x0008FFE4
			public int Count
			{
				get
				{
					int num = BitArithmeticUtilities.CountBits(this._arityBitVectorOrUniqueArity);
					HashSet<int> hashSet = (HashSet<int>)this._uniqueSymbolOrArities;
					if (hashSet != null)
					{
						num += hashSet.Count;
					}
					return num;
				}
			}

			// Token: 0x040014CF RID: 5327
			[Nullable(2)]
			private object _uniqueSymbolOrArities;

			// Token: 0x040014D0 RID: 5328
			private int _arityBitVectorOrUniqueArity;
		}
	}
}

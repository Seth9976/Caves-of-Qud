using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x02000162 RID: 354
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class MetadataAggregator
	{
		/// <param name="baseReader" />
		/// <param name="deltaReaders" />
		// Token: 0x06000B23 RID: 2851 RVA: 0x000216E7 File Offset: 0x0001F8E7
		public MetadataAggregator(MetadataReader baseReader, IReadOnlyList<MetadataReader> deltaReaders)
			: this(baseReader, null, null, deltaReaders)
		{
		}

		/// <param name="baseTableRowCounts" />
		/// <param name="baseHeapSizes" />
		/// <param name="deltaReaders" />
		// Token: 0x06000B24 RID: 2852 RVA: 0x000216F3 File Offset: 0x0001F8F3
		[NullableContext(2)]
		public MetadataAggregator(IReadOnlyList<int> baseTableRowCounts, IReadOnlyList<int> baseHeapSizes, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<MetadataReader> deltaReaders)
			: this(null, baseTableRowCounts, baseHeapSizes, deltaReaders)
		{
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x00021700 File Offset: 0x0001F900
		private MetadataAggregator(MetadataReader baseReader, IReadOnlyList<int> baseTableRowCounts, IReadOnlyList<int> baseHeapSizes, IReadOnlyList<MetadataReader> deltaReaders)
		{
			if (baseTableRowCounts == null)
			{
				if (baseReader == null)
				{
					throw new ArgumentNullException("baseReader");
				}
				if (baseReader.GetTableRowCount(TableIndex.EncMap) != 0)
				{
					throw new ArgumentException(SR.BaseReaderMustBeFullMetadataReader, "baseReader");
				}
				MetadataAggregator.CalculateBaseCounts(baseReader, out baseTableRowCounts, out baseHeapSizes);
			}
			else
			{
				if (baseTableRowCounts.Count != MetadataTokens.TableCount)
				{
					throw new ArgumentException(SR.Format(SR.ExpectedListOfSize, MetadataTokens.TableCount), "baseTableRowCounts");
				}
				if (baseHeapSizes == null)
				{
					throw new ArgumentNullException("baseHeapSizes");
				}
				if (baseHeapSizes.Count != MetadataTokens.HeapCount)
				{
					throw new ArgumentException(SR.Format(SR.ExpectedListOfSize, MetadataTokens.HeapCount), "baseTableRowCounts");
				}
			}
			if (deltaReaders == null || deltaReaders.Count == 0)
			{
				throw new ArgumentException(SR.ExpectedNonEmptyList, "deltaReaders");
			}
			for (int i = 0; i < deltaReaders.Count; i++)
			{
				if (deltaReaders[i].GetTableRowCount(TableIndex.EncMap) == 0 || !deltaReaders[i].IsMinimalDelta)
				{
					throw new ArgumentException(SR.ReadersMustBeDeltaReaders, "deltaReaders");
				}
			}
			this._heapSizes = MetadataAggregator.CalculateHeapSizes(baseHeapSizes, deltaReaders);
			this._rowCounts = MetadataAggregator.CalculateRowCounts(baseTableRowCounts, deltaReaders);
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x0002182B File Offset: 0x0001FA2B
		internal MetadataAggregator(MetadataAggregator.RowCounts[][] rowCounts, int[][] heapSizes)
		{
			this._rowCounts = MetadataAggregator.ToImmutable<MetadataAggregator.RowCounts>(rowCounts);
			this._heapSizes = MetadataAggregator.ToImmutable<int>(heapSizes);
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x0002184C File Offset: 0x0001FA4C
		private static void CalculateBaseCounts(MetadataReader baseReader, out IReadOnlyList<int> baseTableRowCounts, out IReadOnlyList<int> baseHeapSizes)
		{
			int[] array = new int[MetadataTokens.TableCount];
			int[] array2 = new int[MetadataTokens.HeapCount];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = baseReader.GetTableRowCount((TableIndex)i);
			}
			for (int j = 0; j < array2.Length; j++)
			{
				array2[j] = baseReader.GetHeapSize((HeapIndex)j);
			}
			baseTableRowCounts = array;
			baseHeapSizes = array2;
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x000218A8 File Offset: 0x0001FAA8
		private static ImmutableArray<ImmutableArray<int>> CalculateHeapSizes(IReadOnlyList<int> baseSizes, IReadOnlyList<MetadataReader> deltaReaders)
		{
			int num = 1 + deltaReaders.Count;
			int[] array = new int[num];
			int[] array2 = new int[num];
			int[] array3 = new int[num];
			int[] array4 = new int[num];
			array[0] = baseSizes[0];
			array2[0] = baseSizes[1];
			array3[0] = baseSizes[2];
			array4[0] = baseSizes[3] / 16;
			for (int i = 0; i < deltaReaders.Count; i++)
			{
				array[i + 1] = array[i] + deltaReaders[i].GetHeapSize(HeapIndex.UserString);
				array2[i + 1] = array2[i] + deltaReaders[i].GetHeapSize(HeapIndex.String);
				array3[i + 1] = array3[i] + deltaReaders[i].GetHeapSize(HeapIndex.Blob);
				array4[i + 1] = array4[i] + deltaReaders[i].GetHeapSize(HeapIndex.Guid) / 16;
			}
			return ImmutableArray.Create<ImmutableArray<int>>(array.ToImmutableArray<int>(), array2.ToImmutableArray<int>(), array3.ToImmutableArray<int>(), array4.ToImmutableArray<int>());
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x000219A4 File Offset: 0x0001FBA4
		private static ImmutableArray<ImmutableArray<MetadataAggregator.RowCounts>> CalculateRowCounts(IReadOnlyList<int> baseRowCounts, IReadOnlyList<MetadataReader> deltaReaders)
		{
			MetadataAggregator.RowCounts[][] baseRowCounts2 = MetadataAggregator.GetBaseRowCounts(baseRowCounts, 1 + deltaReaders.Count);
			for (int i = 1; i <= deltaReaders.Count; i++)
			{
				MetadataAggregator.CalculateDeltaRowCountsForGeneration(baseRowCounts2, i, ref deltaReaders[i - 1].EncMapTable);
			}
			return MetadataAggregator.ToImmutable<MetadataAggregator.RowCounts>(baseRowCounts2);
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x000219EC File Offset: 0x0001FBEC
		private static ImmutableArray<ImmutableArray<T>> ToImmutable<T>(T[][] array)
		{
			ImmutableArray<T>[] array2 = new ImmutableArray<T>[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = array[i].ToImmutableArray<T>();
			}
			return array2.ToImmutableArray<ImmutableArray<T>>();
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x00021A28 File Offset: 0x0001FC28
		internal static MetadataAggregator.RowCounts[][] GetBaseRowCounts(IReadOnlyList<int> baseRowCounts, int generations)
		{
			MetadataAggregator.RowCounts[][] array = new MetadataAggregator.RowCounts[MetadataTokens.TableCount][];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new MetadataAggregator.RowCounts[generations];
				array[i][0].AggregateInserts = baseRowCounts[i];
			}
			return array;
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x00021A70 File Offset: 0x0001FC70
		internal static void CalculateDeltaRowCountsForGeneration(MetadataAggregator.RowCounts[][] rowCounts, int generation, ref EnCMapTableReader encMapTable)
		{
			foreach (MetadataAggregator.RowCounts[] array in rowCounts)
			{
				array[generation].AggregateInserts = array[generation - 1].AggregateInserts;
			}
			int numberOfRows = encMapTable.NumberOfRows;
			for (int j = 1; j <= numberOfRows; j++)
			{
				uint token = encMapTable.GetToken(j);
				int num = (int)(token & 16777215U);
				MetadataAggregator.RowCounts[] array2 = rowCounts[(int)(token >> 24)];
				if (num > array2[generation].AggregateInserts)
				{
					if (num != array2[generation].AggregateInserts + 1)
					{
						throw new BadImageFormatException(SR.EnCMapNotSorted);
					}
					array2[generation].AggregateInserts = num;
				}
				else
				{
					MetadataAggregator.RowCounts[] array3 = array2;
					array3[generation].Updates = array3[generation].Updates + 1;
				}
			}
		}

		/// <summary>Calculates the handle of the entity within the metadata generation it is defined in, given a handle of an entity in an aggregate metadata.</summary>
		/// <param name="handle">Handle of an entity in an aggregate metadata.</param>
		/// <param name="generation">The generation the entity is defined in.</param>
		/// <returns>Handle of the entity within the metadata <paramref name="generation" />.</returns>
		// Token: 0x06000B2D RID: 2861 RVA: 0x00021B34 File Offset: 0x0001FD34
		public Handle GetGenerationHandle(Handle handle, out int generation)
		{
			if (handle.IsVirtual)
			{
				throw new NotSupportedException();
			}
			if (!handle.IsHeapHandle)
			{
				int rowId = handle.RowId;
				ImmutableArray<MetadataAggregator.RowCounts> immutableArray = this._rowCounts[(int)handle.Type];
				generation = immutableArray.BinarySearch(new MetadataAggregator.RowCounts
				{
					AggregateInserts = rowId
				});
				if (generation >= 0)
				{
					while (generation > 0)
					{
						if (immutableArray[generation - 1].AggregateInserts != rowId)
						{
							break;
						}
						generation--;
					}
				}
				else
				{
					generation = ~generation;
					if (generation >= immutableArray.Length)
					{
						throw new ArgumentException(SR.HandleBelongsToFutureGeneration, "handle");
					}
				}
				int num = ((generation == 0) ? rowId : (rowId - immutableArray[generation - 1].AggregateInserts + immutableArray[generation].Updates));
				return new Handle((byte)handle.Type, num);
			}
			int offset = handle.Offset;
			HeapIndex heapIndex;
			MetadataTokens.TryGetHeapIndex(handle.Kind, out heapIndex);
			ImmutableArray<int> immutableArray2 = this._heapSizes[(int)heapIndex];
			int num2 = ((handle.Type == 114U) ? (offset - 1) : offset);
			generation = immutableArray2.BinarySearch(num2);
			if (generation >= 0)
			{
				do
				{
					generation++;
					if (generation >= immutableArray2.Length)
					{
						break;
					}
				}
				while (immutableArray2[generation] == num2);
			}
			else
			{
				generation = ~generation;
			}
			if (generation >= immutableArray2.Length)
			{
				throw new ArgumentException(SR.HandleBelongsToFutureGeneration, "handle");
			}
			int num3 = ((handle.Type == 114U || generation == 0) ? offset : (offset - immutableArray2[generation - 1]));
			return new Handle((byte)handle.Type, num3);
		}

		// Token: 0x040008DC RID: 2268
		private readonly ImmutableArray<ImmutableArray<int>> _heapSizes;

		// Token: 0x040008DD RID: 2269
		private readonly ImmutableArray<ImmutableArray<MetadataAggregator.RowCounts>> _rowCounts;

		// Token: 0x02000200 RID: 512
		[NullableContext(0)]
		internal struct RowCounts : IComparable<MetadataAggregator.RowCounts>
		{
			// Token: 0x06000D3D RID: 3389 RVA: 0x00026BDC File Offset: 0x00024DDC
			public int CompareTo(MetadataAggregator.RowCounts other)
			{
				return this.AggregateInserts - other.AggregateInserts;
			}

			// Token: 0x06000D3E RID: 3390 RVA: 0x00026BEB File Offset: 0x00024DEB
			[NullableContext(1)]
			public override string ToString()
			{
				return string.Format("+0x{0:x} ~0x{1:x}", this.AggregateInserts, this.Updates);
			}

			// Token: 0x04000BCA RID: 3018
			public int AggregateInserts;

			// Token: 0x04000BCB RID: 3019
			public int Updates;
		}
	}
}

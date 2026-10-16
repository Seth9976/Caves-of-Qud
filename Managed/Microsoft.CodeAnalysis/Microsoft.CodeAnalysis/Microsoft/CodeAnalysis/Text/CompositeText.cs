using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Text
{
	// Token: 0x020002E3 RID: 739
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class CompositeText : SourceText
	{
		// Token: 0x06001F9C RID: 8092 RVA: 0x00061728 File Offset: 0x0005F928
		[NullableContext(2)]
		private CompositeText([Nullable(new byte[] { 0, 1 })] ImmutableArray<SourceText> segments, Encoding encoding, SourceHashAlgorithm checksumAlgorithm)
			: base(default(ImmutableArray<byte>), checksumAlgorithm, null)
		{
			this._segments = segments;
			this._encoding = encoding;
			CompositeText.ComputeLengthAndStorageSize(segments, out this._length, out this._storageSize);
			this._segmentOffsets = new int[segments.Length];
			int num = 0;
			for (int i = 0; i < this._segmentOffsets.Length; i++)
			{
				this._segmentOffsets[i] = num;
				num += this._segments[i].Length;
			}
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x06001F9D RID: 8093 RVA: 0x000617B1 File Offset: 0x0005F9B1
		[Nullable(2)]
		public override Encoding Encoding
		{
			[NullableContext(2)]
			get
			{
				return this._encoding;
			}
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x06001F9E RID: 8094 RVA: 0x000617B9 File Offset: 0x0005F9B9
		public override int Length
		{
			get
			{
				return this._length;
			}
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x06001F9F RID: 8095 RVA: 0x000617C1 File Offset: 0x0005F9C1
		internal override int StorageSize
		{
			get
			{
				return this._storageSize;
			}
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x06001FA0 RID: 8096 RVA: 0x000617C9 File Offset: 0x0005F9C9
		[Nullable(new byte[] { 0, 1 })]
		internal override ImmutableArray<SourceText> Segments
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get
			{
				return this._segments;
			}
		}

		// Token: 0x1700081A RID: 2074
		public override char this[int position]
		{
			get
			{
				int num;
				int num2;
				this.GetIndexAndOffset(position, out num, out num2);
				return this._segments[num][num2];
			}
		}

		// Token: 0x06001FA2 RID: 8098 RVA: 0x00061804 File Offset: 0x0005FA04
		public override SourceText GetSubText(TextSpan span)
		{
			base.CheckSubSpan(span);
			int start = span.Start;
			int num = span.Length;
			int num2;
			int num3;
			this.GetIndexAndOffset(start, out num2, out num3);
			ArrayBuilder<SourceText> instance = ArrayBuilder<SourceText>.GetInstance();
			SourceText sourceText2;
			try
			{
				while (num2 < this._segments.Length && num > 0)
				{
					SourceText sourceText = this._segments[num2];
					int num4 = Math.Min(num, sourceText.Length - num3);
					CompositeText.AddSegments(instance, sourceText.GetSubText(new TextSpan(num3, num4)));
					num -= num4;
					num2++;
					num3 = 0;
				}
				sourceText2 = CompositeText.ToSourceText(instance, this, false);
			}
			finally
			{
				instance.Free();
			}
			return sourceText2;
		}

		// Token: 0x06001FA3 RID: 8099 RVA: 0x000618C0 File Offset: 0x0005FAC0
		private void GetIndexAndOffset(int position, out int index, out int offset)
		{
			int num = this._segmentOffsets.BinarySearch(position);
			index = ((num >= 0) ? num : (~num - 1));
			offset = position - this._segmentOffsets[index];
		}

		// Token: 0x06001FA4 RID: 8100 RVA: 0x000618F4 File Offset: 0x0005FAF4
		private bool CheckCopyToArguments(int sourceIndex, char[] destination, int destinationIndex, int count)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			if (sourceIndex < 0)
			{
				throw new ArgumentOutOfRangeException("sourceIndex");
			}
			if (destinationIndex < 0)
			{
				throw new ArgumentOutOfRangeException("destinationIndex");
			}
			if (count < 0 || count > this.Length - sourceIndex || count > destination.Length - destinationIndex)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			return count > 0;
		}

		// Token: 0x06001FA5 RID: 8101 RVA: 0x00061958 File Offset: 0x0005FB58
		public override void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
		{
			if (!this.CheckCopyToArguments(sourceIndex, destination, destinationIndex, count))
			{
				return;
			}
			int num;
			int num2;
			this.GetIndexAndOffset(sourceIndex, out num, out num2);
			while (num < this._segments.Length && count > 0)
			{
				SourceText sourceText = this._segments[num];
				int num3 = Math.Min(count, sourceText.Length - num2);
				sourceText.CopyTo(num2, destination, destinationIndex, num3);
				count -= num3;
				destinationIndex += num3;
				num++;
				num2 = 0;
			}
		}

		// Token: 0x06001FA6 RID: 8102 RVA: 0x000619D4 File Offset: 0x0005FBD4
		internal static void AddSegments(ArrayBuilder<SourceText> segments, SourceText text)
		{
			CompositeText compositeText = text as CompositeText;
			if (compositeText == null)
			{
				segments.Add(text);
				return;
			}
			segments.AddRange(compositeText._segments);
		}

		// Token: 0x06001FA7 RID: 8103 RVA: 0x00061A00 File Offset: 0x0005FC00
		internal static SourceText ToSourceText(ArrayBuilder<SourceText> segments, SourceText original, bool adjustSegments)
		{
			if (adjustSegments)
			{
				CompositeText.TrimInaccessibleText(segments);
				CompositeText.ReduceSegmentCountIfNecessary(segments);
			}
			if (segments.Count == 0)
			{
				return SourceText.From(string.Empty, original.Encoding, original.ChecksumAlgorithm);
			}
			if (segments.Count == 1)
			{
				return segments[0];
			}
			return new CompositeText(segments.ToImmutable(), original.Encoding, original.ChecksumAlgorithm);
		}

		// Token: 0x06001FA8 RID: 8104 RVA: 0x00061A64 File Offset: 0x0005FC64
		private static void ReduceSegmentCountIfNecessary(ArrayBuilder<SourceText> segments)
		{
			if (segments.Count > 64)
			{
				int minimalSegmentSizeToUseForCombining = CompositeText.GetMinimalSegmentSizeToUseForCombining(segments);
				CompositeText.CombineSegments(segments, minimalSegmentSizeToUseForCombining);
			}
		}

		// Token: 0x06001FA9 RID: 8105 RVA: 0x00061A8C File Offset: 0x0005FC8C
		private static int GetMinimalSegmentSizeToUseForCombining(ArrayBuilder<SourceText> segments)
		{
			for (int i = 32; i <= 134217727; i *= 2)
			{
				if (CompositeText.GetSegmentCountIfCombined(segments, i) <= 32)
				{
					return i;
				}
			}
			return 134217727;
		}

		// Token: 0x06001FAA RID: 8106 RVA: 0x00061ABC File Offset: 0x0005FCBC
		private static int GetSegmentCountIfCombined(ArrayBuilder<SourceText> segments, int segmentSize)
		{
			int num = 0;
			for (int i = 0; i < segments.Count - 1; i++)
			{
				if (segments[i].Length <= segmentSize)
				{
					int num2 = 1;
					for (int j = i + 1; j < segments.Count; j++)
					{
						if (segments[j].Length <= segmentSize)
						{
							num2++;
						}
					}
					if (num2 > 1)
					{
						int num3 = num2 - 1;
						num += num3;
						i += num3;
					}
				}
			}
			return segments.Count - num;
		}

		// Token: 0x06001FAB RID: 8107 RVA: 0x00061B30 File Offset: 0x0005FD30
		private static void CombineSegments(ArrayBuilder<SourceText> segments, int segmentSize)
		{
			for (int i = 0; i < segments.Count - 1; i++)
			{
				if (segments[i].Length <= segmentSize)
				{
					int num = segments[i].Length;
					int j = 1;
					for (int k = i + 1; k < segments.Count; k++)
					{
						if (segments[k].Length <= segmentSize)
						{
							j++;
							num += segments[k].Length;
						}
					}
					if (j > 1)
					{
						Encoding encoding = segments[i].Encoding;
						SourceHashAlgorithm checksumAlgorithm = segments[i].ChecksumAlgorithm;
						SourceTextWriter sourceTextWriter = SourceTextWriter.Create(encoding, checksumAlgorithm, num);
						while (j > 0)
						{
							segments[i].Write(sourceTextWriter, default(CancellationToken));
							segments.RemoveAt(i);
							j--;
						}
						SourceText sourceText = sourceTextWriter.ToSourceText();
						segments.Insert(i, sourceText);
					}
				}
			}
		}

		// Token: 0x06001FAC RID: 8108 RVA: 0x00061C10 File Offset: 0x0005FE10
		private static void ComputeLengthAndStorageSize(IReadOnlyList<SourceText> segments, out int length, out int size)
		{
			HashSet<SourceText> hashSet = CompositeText.s_uniqueSourcesPool.Allocate();
			length = 0;
			for (int i = 0; i < segments.Count; i++)
			{
				SourceText sourceText = segments[i];
				length += sourceText.Length;
				hashSet.Add(sourceText.StorageKey);
			}
			size = 0;
			foreach (SourceText sourceText2 in hashSet)
			{
				size += sourceText2.StorageSize;
			}
			hashSet.Clear();
			CompositeText.s_uniqueSourcesPool.Free(hashSet);
		}

		// Token: 0x06001FAD RID: 8109 RVA: 0x00061CB8 File Offset: 0x0005FEB8
		private static void TrimInaccessibleText(ArrayBuilder<SourceText> segments)
		{
			int num;
			int num2;
			CompositeText.ComputeLengthAndStorageSize(segments, out num, out num2);
			if (num < num2 / 2)
			{
				Encoding encoding = segments[0].Encoding;
				SourceHashAlgorithm checksumAlgorithm = segments[0].ChecksumAlgorithm;
				SourceTextWriter sourceTextWriter = SourceTextWriter.Create(encoding, checksumAlgorithm, num);
				foreach (SourceText sourceText in segments)
				{
					sourceText.Write(sourceTextWriter, default(CancellationToken));
				}
				segments.Clear();
				segments.Add(sourceTextWriter.ToSourceText());
			}
		}

		// Token: 0x04000DE5 RID: 3557
		[Nullable(new byte[] { 0, 1 })]
		private readonly ImmutableArray<SourceText> _segments;

		// Token: 0x04000DE6 RID: 3558
		private readonly int _length;

		// Token: 0x04000DE7 RID: 3559
		private readonly int _storageSize;

		// Token: 0x04000DE8 RID: 3560
		private readonly int[] _segmentOffsets;

		// Token: 0x04000DE9 RID: 3561
		[Nullable(2)]
		private readonly Encoding _encoding;

		// Token: 0x04000DEA RID: 3562
		internal const int TARGET_SEGMENT_COUNT_AFTER_REDUCTION = 32;

		// Token: 0x04000DEB RID: 3563
		internal const int MAXIMUM_SEGMENT_COUNT_BEFORE_REDUCTION = 64;

		// Token: 0x04000DEC RID: 3564
		private const int INITIAL_SEGMENT_SIZE_FOR_COMBINING = 32;

		// Token: 0x04000DED RID: 3565
		private const int MAXIMUM_SEGMENT_SIZE_FOR_COMBINING = 134217727;

		// Token: 0x04000DEE RID: 3566
		private static readonly ObjectPool<HashSet<SourceText>> s_uniqueSourcesPool = new ObjectPool<HashSet<SourceText>>(() => new HashSet<SourceText>(), 5);
	}
}

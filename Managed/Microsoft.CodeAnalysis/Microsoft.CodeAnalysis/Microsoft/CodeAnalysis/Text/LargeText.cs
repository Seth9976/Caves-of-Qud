using System;
using System.Collections.Immutable;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.Text
{
	// Token: 0x020002E4 RID: 740
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class LargeText : SourceText
	{
		// Token: 0x06001FAF RID: 8111 RVA: 0x00061D50 File Offset: 0x0005FF50
		[NullableContext(0)]
		internal LargeText([Nullable(new byte[] { 0, 1 })] ImmutableArray<char[]> chunks, [Nullable(2)] Encoding encodingOpt, ImmutableArray<byte> checksum, SourceHashAlgorithm checksumAlgorithm, ImmutableArray<byte> embeddedTextBlob)
			: base(checksum, checksumAlgorithm, embeddedTextBlob)
		{
			this._chunks = chunks;
			this._encodingOpt = encodingOpt;
			this._chunkStartOffsets = new int[chunks.Length];
			int num = 0;
			for (int i = 0; i < chunks.Length; i++)
			{
				this._chunkStartOffsets[i] = num;
				num += chunks[i].Length;
			}
			this._length = num;
		}

		// Token: 0x06001FB0 RID: 8112 RVA: 0x00061DBC File Offset: 0x0005FFBC
		[NullableContext(2)]
		internal LargeText([Nullable(new byte[] { 0, 1 })] ImmutableArray<char[]> chunks, Encoding encodingOpt, SourceHashAlgorithm checksumAlgorithm)
			: this(chunks, encodingOpt, default(ImmutableArray<byte>), checksumAlgorithm, default(ImmutableArray<byte>))
		{
		}

		// Token: 0x06001FB1 RID: 8113 RVA: 0x00061DE4 File Offset: 0x0005FFE4
		internal static SourceText Decode(Stream stream, Encoding encoding, SourceHashAlgorithm checksumAlgorithm, bool throwIfBinaryDetected, bool canBeEmbedded)
		{
			stream.Seek(0L, 0);
			long length = stream.Length;
			if (length == 0L)
			{
				return SourceText.From(string.Empty, encoding, checksumAlgorithm);
			}
			int maxCharCountOrThrowIfHuge = encoding.GetMaxCharCountOrThrowIfHuge(stream);
			int num = (int)length;
			SourceText sourceText;
			using (StreamReader streamReader = new StreamReader(stream, encoding, true, Math.Min(num, 4096), true))
			{
				ImmutableArray<char[]> immutableArray = LargeText.ReadChunksFromTextReader(streamReader, maxCharCountOrThrowIfHuge, throwIfBinaryDetected);
				ImmutableArray<byte> immutableArray2 = SourceText.CalculateChecksum(stream, checksumAlgorithm);
				ImmutableArray<byte> immutableArray3 = (canBeEmbedded ? EmbeddedText.CreateBlob(stream) : default(ImmutableArray<byte>));
				sourceText = new LargeText(immutableArray, streamReader.CurrentEncoding, immutableArray2, checksumAlgorithm, immutableArray3);
			}
			return sourceText;
		}

		// Token: 0x06001FB2 RID: 8114 RVA: 0x00061E8C File Offset: 0x0006008C
		internal static SourceText Decode(TextReader reader, int length, [Nullable(2)] Encoding encodingOpt, SourceHashAlgorithm checksumAlgorithm)
		{
			if (length == 0)
			{
				return SourceText.From(string.Empty, encodingOpt, checksumAlgorithm);
			}
			return new LargeText(LargeText.ReadChunksFromTextReader(reader, length, false), encodingOpt, checksumAlgorithm);
		}

		// Token: 0x06001FB3 RID: 8115 RVA: 0x00061EB0 File Offset: 0x000600B0
		[return: Nullable(new byte[] { 0, 1 })]
		private static ImmutableArray<char[]> ReadChunksFromTextReader(TextReader reader, int maxCharRemainingGuess, bool throwIfBinaryDetected)
		{
			ArrayBuilder<char[]> instance = ArrayBuilder<char[]>.GetInstance(1 + maxCharRemainingGuess / 40960);
			while (reader.Peek() != -1)
			{
				int num = 40960;
				if (maxCharRemainingGuess < 40960)
				{
					num = Math.Max(maxCharRemainingGuess - 64, 64);
				}
				char[] array = new char[num];
				int num2 = reader.ReadBlock(array, 0, array.Length);
				if (num2 == 0)
				{
					break;
				}
				maxCharRemainingGuess -= num2;
				if (num2 < array.Length)
				{
					Array.Resize<char>(ref array, num2);
				}
				if (throwIfBinaryDetected && LargeText.IsBinary(array))
				{
					throw new InvalidDataException();
				}
				instance.Add(array);
			}
			return instance.ToImmutableAndFree();
		}

		// Token: 0x06001FB4 RID: 8116 RVA: 0x00061F38 File Offset: 0x00060138
		private static bool IsBinary(char[] chunk)
		{
			int i = 1;
			while (i < chunk.Length)
			{
				if (chunk[i] == '\0')
				{
					if (chunk[i - 1] == '\0')
					{
						return true;
					}
					i++;
				}
				else
				{
					i += 2;
				}
			}
			return false;
		}

		// Token: 0x06001FB5 RID: 8117 RVA: 0x00061F68 File Offset: 0x00060168
		private int GetIndexFromPosition(int position)
		{
			int num = this._chunkStartOffsets.BinarySearch(position);
			if (num < 0)
			{
				return ~num - 1;
			}
			return num;
		}

		// Token: 0x1700081B RID: 2075
		public override char this[int position]
		{
			get
			{
				int indexFromPosition = this.GetIndexFromPosition(position);
				return this._chunks[indexFromPosition][position - this._chunkStartOffsets[indexFromPosition]];
			}
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x06001FB7 RID: 8119 RVA: 0x00061FBB File Offset: 0x000601BB
		[Nullable(2)]
		public override Encoding Encoding
		{
			[NullableContext(2)]
			get
			{
				return this._encodingOpt;
			}
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x06001FB8 RID: 8120 RVA: 0x00061FC3 File Offset: 0x000601C3
		public override int Length
		{
			get
			{
				return this._length;
			}
		}

		// Token: 0x06001FB9 RID: 8121 RVA: 0x00061FCC File Offset: 0x000601CC
		public override void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
		{
			if (count == 0)
			{
				return;
			}
			int num = this.GetIndexFromPosition(sourceIndex);
			int num2 = sourceIndex - this._chunkStartOffsets[num];
			for (;;)
			{
				char[] array = this._chunks[num];
				int num3 = Math.Min(array.Length - num2, count);
				Array.Copy(array, num2, destination, destinationIndex, num3);
				count -= num3;
				if (count <= 0)
				{
					break;
				}
				destinationIndex += num3;
				num2 = 0;
				num++;
			}
		}

		// Token: 0x06001FBA RID: 8122 RVA: 0x00062030 File Offset: 0x00060230
		public override void Write(TextWriter writer, TextSpan span, CancellationToken cancellationToken = default(CancellationToken))
		{
			if (span.Start < 0 || span.Start > this._length || span.End > this._length)
			{
				throw new ArgumentOutOfRangeException("span");
			}
			int num = span.Length;
			if (num == 0)
			{
				return;
			}
			LargeTextWriter largeTextWriter = writer as LargeTextWriter;
			int num2 = this.GetIndexFromPosition(span.Start);
			int num3 = span.Start - this._chunkStartOffsets[num2];
			for (;;)
			{
				cancellationToken.ThrowIfCancellationRequested();
				char[] array = this._chunks[num2];
				int num4 = Math.Min(array.Length - num3, num);
				if (largeTextWriter != null && num3 == 0 && num4 == array.Length)
				{
					largeTextWriter.AppendChunk(array);
				}
				else
				{
					writer.Write(array, num3, num4);
				}
				num -= num4;
				if (num <= 0)
				{
					break;
				}
				num3 = 0;
				num2++;
			}
		}

		// Token: 0x06001FBB RID: 8123 RVA: 0x000620FD File Offset: 0x000602FD
		protected override TextLineCollection GetLinesCore()
		{
			return new SourceText.LineInfo(this, this.ParseLineStarts());
		}

		// Token: 0x06001FBC RID: 8124 RVA: 0x0006210C File Offset: 0x0006030C
		private int[] ParseLineStarts()
		{
			int num = 0;
			int num2 = 0;
			int num3 = -1;
			ArrayBuilder<int> instance = ArrayBuilder<int>.GetInstance();
			foreach (char[] array in this._chunks)
			{
				foreach (char c in array)
				{
					num2++;
					if (c - '\u000e' > 'q')
					{
						if (c <= '\r')
						{
							if (c != '\n')
							{
								if (c != '\r')
								{
									goto IL_0089;
								}
								num3 = num2;
							}
							else if (num3 == num2 - 1)
							{
								num = num2;
								goto IL_0089;
							}
						}
						else if (c != '\u0085' && c != '\u2028' && c != '\u2029')
						{
							goto IL_0089;
						}
						instance.Add(num);
						num = num2;
					}
					IL_0089:;
				}
			}
			instance.Add(num);
			return instance.ToArrayAndFree();
		}

		// Token: 0x04000DEF RID: 3567
		internal const int ChunkSize = 40960;

		// Token: 0x04000DF0 RID: 3568
		[Nullable(new byte[] { 0, 1 })]
		private readonly ImmutableArray<char[]> _chunks;

		// Token: 0x04000DF1 RID: 3569
		private readonly int[] _chunkStartOffsets;

		// Token: 0x04000DF2 RID: 3570
		private readonly int _length;

		// Token: 0x04000DF3 RID: 3571
		[Nullable(2)]
		private readonly Encoding _encodingOpt;
	}
}

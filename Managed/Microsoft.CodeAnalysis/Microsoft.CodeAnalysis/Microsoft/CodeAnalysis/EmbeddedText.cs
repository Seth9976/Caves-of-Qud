using System;
using System.Collections.Immutable;
using System.IO;
using System.IO.Compression;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Cci;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x0200018B RID: 395
	public sealed class EmbeddedText
	{
		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06001032 RID: 4146 RVA: 0x0002F364 File Offset: 0x0002D564
		[Nullable(1)]
		public string FilePath
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06001033 RID: 4147 RVA: 0x0002F36C File Offset: 0x0002D56C
		public SourceHashAlgorithm ChecksumAlgorithm { get; }

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06001034 RID: 4148 RVA: 0x0002F374 File Offset: 0x0002D574
		public ImmutableArray<byte> Checksum { get; }

		// Token: 0x06001035 RID: 4149 RVA: 0x0002F37C File Offset: 0x0002D57C
		private EmbeddedText([Nullable(1)] string filePath, ImmutableArray<byte> checksum, SourceHashAlgorithm checksumAlgorithm, ImmutableArray<byte> blob)
		{
			this.FilePath = filePath;
			this.Checksum = checksum;
			this.ChecksumAlgorithm = checksumAlgorithm;
			this.Blob = blob;
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06001036 RID: 4150 RVA: 0x0002F3A1 File Offset: 0x0002D5A1
		internal ImmutableArray<byte> Blob { get; }

		// Token: 0x06001037 RID: 4151 RVA: 0x0002F3AC File Offset: 0x0002D5AC
		[NullableContext(1)]
		public static EmbeddedText FromSource(string filePath, SourceText text)
		{
			EmbeddedText.ValidateFilePath(filePath);
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			if (!text.CanBeEmbedded)
			{
				throw new ArgumentException(CodeAnalysisResources.SourceTextCannotBeEmbedded, "text");
			}
			if (!text.PrecomputedEmbeddedTextBlob.IsDefault)
			{
				return new EmbeddedText(filePath, text.GetChecksum(), text.ChecksumAlgorithm, text.PrecomputedEmbeddedTextBlob);
			}
			return new EmbeddedText(filePath, text.GetChecksum(), text.ChecksumAlgorithm, EmbeddedText.CreateBlob(text));
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x0002F428 File Offset: 0x0002D628
		[NullableContext(1)]
		public static EmbeddedText FromStream(string filePath, Stream stream, SourceHashAlgorithm checksumAlgorithm = SourceHashAlgorithm.Sha1)
		{
			EmbeddedText.ValidateFilePath(filePath);
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanRead || !stream.CanSeek)
			{
				throw new ArgumentException(CodeAnalysisResources.StreamMustSupportReadAndSeek, "stream");
			}
			SourceText.ValidateChecksumAlgorithm(checksumAlgorithm);
			return new EmbeddedText(filePath, SourceText.CalculateChecksum(stream, checksumAlgorithm), checksumAlgorithm, EmbeddedText.CreateBlob(stream));
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x0002F484 File Offset: 0x0002D684
		[NullableContext(1)]
		public static EmbeddedText FromBytes(string filePath, [Nullable(0)] ArraySegment<byte> bytes, SourceHashAlgorithm checksumAlgorithm = SourceHashAlgorithm.Sha1)
		{
			EmbeddedText.ValidateFilePath(filePath);
			if (bytes.Array == null)
			{
				throw new ArgumentNullException("bytes");
			}
			SourceText.ValidateChecksumAlgorithm(checksumAlgorithm);
			return new EmbeddedText(filePath, SourceText.CalculateChecksum(bytes.Array, bytes.Offset, bytes.Count, checksumAlgorithm), checksumAlgorithm, EmbeddedText.CreateBlob(bytes));
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x0002F4D9 File Offset: 0x0002D6D9
		[NullableContext(1)]
		private static void ValidateFilePath(string filePath)
		{
			if (filePath == null)
			{
				throw new ArgumentNullException("filePath");
			}
			if (filePath.Length == 0)
			{
				throw new ArgumentException(CodeAnalysisResources.ArgumentCannotBeEmpty, "filePath");
			}
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x0002F504 File Offset: 0x0002D704
		internal static ImmutableArray<byte> CreateBlob([Nullable(1)] Stream stream)
		{
			long length = stream.Length;
			if (length > 2147483647L)
			{
				throw new IOException(CodeAnalysisResources.StreamIsTooLong);
			}
			stream.Seek(0L, 0);
			int num = (int)length;
			if (num < 200)
			{
				using (PooledBlobBuilder instance = PooledBlobBuilder.GetInstance(1024))
				{
					instance.WriteInt32(0);
					int num2 = instance.TryWriteBytes(stream, num);
					if (num != num2)
					{
						throw new EndOfStreamException();
					}
					return instance.ToImmutableArray();
				}
			}
			ImmutableArray<byte> immutableArray;
			using (BlobBuildingStream instance2 = BlobBuildingStream.GetInstance())
			{
				instance2.WriteInt32(num);
				using (EmbeddedText.CountingDeflateStream countingDeflateStream = new EmbeddedText.CountingDeflateStream(instance2, 0, true))
				{
					stream.CopyTo(countingDeflateStream);
					if (num != countingDeflateStream.BytesWritten)
					{
						throw new EndOfStreamException();
					}
				}
				immutableArray = instance2.ToImmutableArray();
			}
			return immutableArray;
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x0002F5F8 File Offset: 0x0002D7F8
		internal static ImmutableArray<byte> CreateBlob(ArraySegment<byte> bytes)
		{
			if (bytes.Count < 200)
			{
				using (PooledBlobBuilder instance = PooledBlobBuilder.GetInstance(1024))
				{
					instance.WriteInt32(0);
					instance.WriteBytes(bytes.Array, bytes.Offset, bytes.Count);
					return instance.ToImmutableArray();
				}
			}
			ImmutableArray<byte> immutableArray;
			using (BlobBuildingStream instance2 = BlobBuildingStream.GetInstance())
			{
				instance2.WriteInt32(bytes.Count);
				using (EmbeddedText.CountingDeflateStream countingDeflateStream = new EmbeddedText.CountingDeflateStream(instance2, 0, true))
				{
					countingDeflateStream.Write(bytes.Array, bytes.Offset, bytes.Count);
				}
				immutableArray = instance2.ToImmutableArray();
			}
			return immutableArray;
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x0002F6D4 File Offset: 0x0002D8D4
		private static ImmutableArray<byte> CreateBlob([Nullable(1)] SourceText text)
		{
			int num;
			try
			{
				num = text.Encoding.GetMaxByteCount(text.Length);
			}
			catch (ArgumentOutOfRangeException)
			{
				num = int.MaxValue;
			}
			ImmutableArray<byte> immutableArray;
			using (BlobBuildingStream instance = BlobBuildingStream.GetInstance())
			{
				if (num < 200)
				{
					instance.WriteInt32(0);
					using (StreamWriter streamWriter = new StreamWriter(instance, text.Encoding, Math.Max(1, text.Length), true))
					{
						text.Write(streamWriter, default(CancellationToken));
						goto IL_00D4;
					}
				}
				Blob blob = instance.ReserveBytes(4);
				using (EmbeddedText.CountingDeflateStream countingDeflateStream = new EmbeddedText.CountingDeflateStream(instance, 0, true))
				{
					using (StreamWriter streamWriter2 = new StreamWriter(countingDeflateStream, text.Encoding, 1024, true))
					{
						text.Write(streamWriter2, default(CancellationToken));
					}
					new BlobWriter(blob).WriteInt32(countingDeflateStream.BytesWritten);
				}
				IL_00D4:
				immutableArray = instance.ToImmutableArray();
			}
			return immutableArray;
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x0002F80C File Offset: 0x0002DA0C
		internal DebugSourceInfo GetDebugSourceInfo()
		{
			return new DebugSourceInfo(this.Checksum, this.ChecksumAlgorithm, this.Blob);
		}

		// Token: 0x04000481 RID: 1153
		internal const int CompressionThreshold = 200;

		// Token: 0x020005B2 RID: 1458
		[NullableContext(1)]
		[Nullable(0)]
		private sealed class CountingDeflateStream : DeflateStream
		{
			// Token: 0x060035FC RID: 13820 RVA: 0x000983F9 File Offset: 0x000965F9
			public CountingDeflateStream(Stream stream, CompressionLevel compressionLevel, bool leaveOpen)
				: base(stream, compressionLevel, leaveOpen)
			{
			}

			// Token: 0x17000F48 RID: 3912
			// (get) Token: 0x060035FD RID: 13821 RVA: 0x00098404 File Offset: 0x00096604
			// (set) Token: 0x060035FE RID: 13822 RVA: 0x0009840C File Offset: 0x0009660C
			public int BytesWritten { get; private set; }

			// Token: 0x060035FF RID: 13823 RVA: 0x00098415 File Offset: 0x00096615
			public override void Write(byte[] array, int offset, int count)
			{
				base.Write(array, offset, count);
				checked
				{
					this.BytesWritten += count;
				}
			}

			// Token: 0x06003600 RID: 13824 RVA: 0x00098430 File Offset: 0x00096630
			public override void WriteByte(byte value)
			{
				base.WriteByte(value);
				int bytesWritten = this.BytesWritten;
				this.BytesWritten = checked(bytesWritten + 1);
			}

			// Token: 0x06003601 RID: 13825 RVA: 0x00098454 File Offset: 0x00096654
			public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
			{
				throw ExceptionUtilities.Unreachable;
			}
		}
	}
}

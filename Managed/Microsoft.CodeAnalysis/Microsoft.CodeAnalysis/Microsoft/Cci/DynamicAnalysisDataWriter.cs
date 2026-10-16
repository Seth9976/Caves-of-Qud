using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis.Collections;

namespace Microsoft.Cci
{
	// Token: 0x020000AE RID: 174
	internal class DynamicAnalysisDataWriter
	{
		// Token: 0x060005BE RID: 1470 RVA: 0x00015720 File Offset: 0x00013920
		public DynamicAnalysisDataWriter(int documentCountEstimate, int methodCountEstimate)
		{
			this._blobs = new Dictionary<ImmutableArray<byte>, BlobHandle>(1 + methodCountEstimate + 4 * documentCountEstimate, ByteSequenceComparer.Instance);
			this._guids = new Dictionary<Guid, GuidHandle>(documentCountEstimate);
			this._guidWriter = new BlobBuilder(16 * documentCountEstimate);
			this._documentTable = new List<DynamicAnalysisDataWriter.DocumentRow>(documentCountEstimate);
			this._documentIndex = new Dictionary<DebugSourceDocument, int>(documentCountEstimate);
			this._methodTable = new List<DynamicAnalysisDataWriter.MethodRow>(methodCountEstimate);
			this._blobs.Add(ImmutableArray<byte>.Empty, default(BlobHandle));
			this._blobHeapSize = 1;
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x000157AC File Offset: 0x000139AC
		internal void SerializeMethodDynamicAnalysisData(IMethodBody bodyOpt)
		{
			DynamicAnalysisMethodBodyData dynamicAnalysisMethodBodyData = ((bodyOpt != null) ? bodyOpt.DynamicAnalysisData : null);
			if (dynamicAnalysisMethodBodyData == null)
			{
				this._methodTable.Add(default(DynamicAnalysisDataWriter.MethodRow));
				return;
			}
			BlobHandle blobHandle = this.SerializeSpans(dynamicAnalysisMethodBodyData.Spans, this._documentIndex);
			this._methodTable.Add(new DynamicAnalysisDataWriter.MethodRow
			{
				Spans = blobHandle
			});
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0001580D File Offset: 0x00013A0D
		private BlobHandle GetOrAddBlob(BlobBuilder builder)
		{
			return this.GetOrAddBlob(builder.ToImmutableArray());
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x0001581C File Offset: 0x00013A1C
		private BlobHandle GetOrAddBlob(ImmutableArray<byte> blob)
		{
			BlobHandle blobHandle;
			if (!this._blobs.TryGetValue(blob, ref blobHandle))
			{
				blobHandle = MetadataTokens.BlobHandle(this._blobHeapSize);
				this._blobs.Add(blob, blobHandle);
				this._blobHeapSize += DynamicAnalysisDataWriter.GetCompressedIntegerLength(blob.Length) + blob.Length;
			}
			return blobHandle;
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00015874 File Offset: 0x00013A74
		private static int GetCompressedIntegerLength(int length)
		{
			if (length <= 127)
			{
				return 1;
			}
			if (length > 16383)
			{
				return 4;
			}
			return 2;
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00015888 File Offset: 0x00013A88
		private GuidHandle GetOrAddGuid(Guid guid)
		{
			if (guid == Guid.Empty)
			{
				return default(GuidHandle);
			}
			GuidHandle guidHandle;
			if (this._guids.TryGetValue(guid, ref guidHandle))
			{
				return guidHandle;
			}
			guidHandle = MetadataTokens.GuidHandle((this._guidWriter.Count >> 4) + 1);
			this._guids.Add(guid, guidHandle);
			this._guidWriter.WriteBytes(guid.ToByteArray());
			return guidHandle;
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x000158F4 File Offset: 0x00013AF4
		private BlobHandle SerializeSpans(ImmutableArray<SourceSpan> spans, Dictionary<DebugSourceDocument, int> documentIndex)
		{
			if (spans.Length == 0)
			{
				return default(BlobHandle);
			}
			BlobBuilder blobBuilder = new BlobBuilder(4 + spans.Length * 4);
			int num = -1;
			int num2 = -1;
			DebugSourceDocument debugSourceDocument = spans[0].Document;
			blobBuilder.WriteCompressedInteger(this.GetOrAddDocument(debugSourceDocument, documentIndex));
			for (int i = 0; i < spans.Length; i++)
			{
				DebugSourceDocument document = spans[i].Document;
				if (debugSourceDocument != document)
				{
					blobBuilder.WriteInt16(0);
					blobBuilder.WriteCompressedInteger(this.GetOrAddDocument(document, documentIndex));
					debugSourceDocument = document;
				}
				this.SerializeDeltaLinesAndColumns(blobBuilder, spans[i]);
				if (num < 0)
				{
					blobBuilder.WriteCompressedInteger(spans[i].StartLine);
					blobBuilder.WriteCompressedInteger(spans[i].StartColumn);
				}
				else
				{
					blobBuilder.WriteCompressedSignedInteger(spans[i].StartLine - num);
					blobBuilder.WriteCompressedSignedInteger(spans[i].StartColumn - num2);
				}
				num = spans[i].StartLine;
				num2 = spans[i].StartColumn;
			}
			return this.GetOrAddBlob(blobBuilder);
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00015A20 File Offset: 0x00013C20
		private void SerializeDeltaLinesAndColumns(BlobBuilder writer, SourceSpan span)
		{
			int num = span.EndLine - span.StartLine;
			int num2 = span.EndColumn - span.StartColumn;
			writer.WriteCompressedInteger(num);
			if (num == 0)
			{
				writer.WriteCompressedInteger(num2);
				return;
			}
			writer.WriteCompressedSignedInteger(num2);
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00015A62 File Offset: 0x00013C62
		internal int GetOrAddDocument(DebugSourceDocument document)
		{
			return this.GetOrAddDocument(document, this._documentIndex);
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x00015A74 File Offset: 0x00013C74
		private int GetOrAddDocument(DebugSourceDocument document, Dictionary<DebugSourceDocument, int> index)
		{
			int num;
			if (!index.TryGetValue(document, ref num))
			{
				num = this._documentTable.Count + 1;
				index.Add(document, num);
				DebugSourceInfo sourceInfo = document.GetSourceInfo();
				this._documentTable.Add(new DynamicAnalysisDataWriter.DocumentRow
				{
					Name = this.SerializeDocumentName(document.Location),
					HashAlgorithm = (sourceInfo.Checksum.IsDefault ? default(GuidHandle) : this.GetOrAddGuid(sourceInfo.ChecksumAlgorithmId)),
					Hash = (sourceInfo.Checksum.IsDefault ? default(BlobHandle) : this.GetOrAddBlob(sourceInfo.Checksum))
				});
			}
			return num;
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x00015B34 File Offset: 0x00013D34
		private BlobHandle SerializeDocumentName(string name)
		{
			int num = DynamicAnalysisDataWriter.Count(name, DynamicAnalysisDataWriter.s_separator1[0]);
			int num2 = DynamicAnalysisDataWriter.Count(name, DynamicAnalysisDataWriter.s_separator2[0]);
			char[] array = ((num >= num2) ? DynamicAnalysisDataWriter.s_separator1 : DynamicAnalysisDataWriter.s_separator2);
			BlobBuilder blobBuilder = new BlobBuilder(1 + Math.Max(num, num2) * 2);
			blobBuilder.WriteByte((byte)array[0]);
			foreach (string text in name.Split(array))
			{
				BlobHandle orAddBlob = this.GetOrAddBlob(ImmutableArray.Create<byte>(MetadataWriter.s_utf8Encoding.GetBytes(text)));
				blobBuilder.WriteCompressedInteger(MetadataTokens.GetHeapOffset(orAddBlob));
			}
			return this.GetOrAddBlob(blobBuilder);
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x00015BDC File Offset: 0x00013DDC
		private static int Count(string str, char c)
		{
			int num = 0;
			for (int i = 0; i < str.Length; i++)
			{
				if (str.get_Chars(i) == c)
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x00015C0C File Offset: 0x00013E0C
		internal void SerializeMetadataTables(BlobBuilder writer)
		{
			DynamicAnalysisDataWriter.Sizes sizes = new DynamicAnalysisDataWriter.Sizes(this._blobHeapSize, this._guidWriter.Count);
			this.SerializeHeader(writer, sizes);
			this.SerializeDocumentTable(writer, sizes);
			this.SerializeMethodTable(writer, sizes);
			writer.LinkSuffix(this._guidWriter);
			this.WriteBlobHeap(writer);
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x00015C5C File Offset: 0x00013E5C
		private void WriteBlobHeap(BlobBuilder builder)
		{
			BlobWriter blobWriter = new BlobWriter(builder.ReserveBytes(this._blobHeapSize));
			foreach (KeyValuePair<ImmutableArray<byte>, BlobHandle> keyValuePair in this._blobs)
			{
				int heapOffset = MetadataTokens.GetHeapOffset(keyValuePair.Value);
				ImmutableArray<byte> key = keyValuePair.Key;
				blobWriter.Offset = heapOffset;
				blobWriter.WriteCompressedInteger(key.Length);
				blobWriter.WriteBytes(key);
			}
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x00015CF0 File Offset: 0x00013EF0
		private void SerializeHeader(BlobBuilder writer, DynamicAnalysisDataWriter.Sizes sizes)
		{
			writer.WriteByte(68);
			writer.WriteByte(65);
			writer.WriteByte(77);
			writer.WriteByte(68);
			writer.WriteByte(0);
			writer.WriteByte(2);
			writer.WriteInt32(this._documentTable.Count);
			writer.WriteInt32(this._methodTable.Count);
			writer.WriteInt32(sizes.GuidHeapSize);
			writer.WriteInt32(sizes.BlobHeapSize);
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x00015D68 File Offset: 0x00013F68
		private void SerializeDocumentTable(BlobBuilder writer, DynamicAnalysisDataWriter.Sizes sizes)
		{
			foreach (DynamicAnalysisDataWriter.DocumentRow documentRow in this._documentTable)
			{
				writer.WriteReference(MetadataTokens.GetHeapOffset(documentRow.Name), sizes.BlobIndexSize == 2);
				writer.WriteReference(MetadataTokens.GetHeapOffset(documentRow.HashAlgorithm), sizes.GuidIndexSize == 2);
				writer.WriteReference(MetadataTokens.GetHeapOffset(documentRow.Hash), sizes.BlobIndexSize == 2);
			}
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x00015E04 File Offset: 0x00014004
		private void SerializeMethodTable(BlobBuilder writer, DynamicAnalysisDataWriter.Sizes sizes)
		{
			foreach (DynamicAnalysisDataWriter.MethodRow methodRow in this._methodTable)
			{
				writer.WriteReference(MetadataTokens.GetHeapOffset(methodRow.Spans), sizes.BlobIndexSize == 2);
			}
		}

		// Token: 0x040001DE RID: 478
		private readonly Dictionary<ImmutableArray<byte>, BlobHandle> _blobs;

		// Token: 0x040001DF RID: 479
		private int _blobHeapSize;

		// Token: 0x040001E0 RID: 480
		private readonly Dictionary<Guid, GuidHandle> _guids;

		// Token: 0x040001E1 RID: 481
		private readonly BlobBuilder _guidWriter;

		// Token: 0x040001E2 RID: 482
		private readonly List<DynamicAnalysisDataWriter.DocumentRow> _documentTable;

		// Token: 0x040001E3 RID: 483
		private readonly List<DynamicAnalysisDataWriter.MethodRow> _methodTable;

		// Token: 0x040001E4 RID: 484
		private readonly Dictionary<DebugSourceDocument, int> _documentIndex;

		// Token: 0x040001E5 RID: 485
		private static readonly char[] s_separator1 = new char[] { '/' };

		// Token: 0x040001E6 RID: 486
		private static readonly char[] s_separator2 = new char[] { '\\' };

		// Token: 0x02000529 RID: 1321
		private struct DocumentRow
		{
			// Token: 0x040014AC RID: 5292
			public BlobHandle Name;

			// Token: 0x040014AD RID: 5293
			public GuidHandle HashAlgorithm;

			// Token: 0x040014AE RID: 5294
			public BlobHandle Hash;
		}

		// Token: 0x0200052A RID: 1322
		private struct MethodRow
		{
			// Token: 0x040014AF RID: 5295
			public BlobHandle Spans;
		}

		// Token: 0x0200052B RID: 1323
		private struct Sizes
		{
			// Token: 0x06003373 RID: 13171 RVA: 0x0009181C File Offset: 0x0008FA1C
			public Sizes(int blobHeapSize, int guidHeapSize)
			{
				this.BlobHeapSize = blobHeapSize;
				this.GuidHeapSize = guidHeapSize;
				this.BlobIndexSize = ((blobHeapSize <= 65535) ? 2 : 4);
				this.GuidIndexSize = ((guidHeapSize <= 65535) ? 2 : 4);
			}

			// Token: 0x040014B0 RID: 5296
			public readonly int BlobHeapSize;

			// Token: 0x040014B1 RID: 5297
			public readonly int GuidHeapSize;

			// Token: 0x040014B2 RID: 5298
			public readonly int BlobIndexSize;

			// Token: 0x040014B3 RID: 5299
			public readonly int GuidIndexSize;
		}
	}
}

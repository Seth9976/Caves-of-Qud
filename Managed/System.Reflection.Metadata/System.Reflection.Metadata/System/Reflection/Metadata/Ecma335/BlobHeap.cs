using System;
using System.Reflection.Internal;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Reflection.Metadata.Ecma335
{
	// Token: 0x020000E5 RID: 229
	[NullableContext(1)]
	[Nullable(0)]
	internal struct BlobHeap
	{
		// Token: 0x0600089E RID: 2206 RVA: 0x00017CD4 File Offset: 0x00015ED4
		internal BlobHeap(MemoryBlock block, MetadataKind metadataKind)
		{
			this._lazyVirtualHeap = null;
			this.Block = block;
			if (BlobHeap.s_virtualValues == null && metadataKind != MetadataKind.Ecma335)
			{
				BlobHeap.s_virtualValues = new byte[][]
				{
					default(byte[]),
					new byte[] { 176, 63, 95, 127, 17, 213, 10, 58 },
					new byte[]
					{
						0, 36, 0, 0, 4, 128, 0, 0, 148, 0,
						0, 0, 6, 2, 0, 0, 0, 36, 0, 0,
						82, 83, 65, 49, 0, 4, 0, 0, 1, 0,
						1, 0, 7, 209, 250, 87, 196, 174, 217, 240,
						163, 46, 132, 170, 15, 174, 253, 13, 233, 232,
						253, 106, 236, 143, 135, 251, 3, 118, 108, 131,
						76, 153, 146, 30, 178, 59, 231, 154, 217, 213,
						220, 193, 221, 154, 210, 54, 19, 33, 2, 144,
						11, 114, 60, 249, 128, 149, 127, 196, 225, 119,
						16, 143, 198, 7, 119, 79, 41, 232, 50, 14,
						146, 234, 5, 236, 228, 232, 33, 192, 165, 239,
						232, 241, 100, 92, 76, 12, 147, 193, 171, 153,
						40, 93, 98, 44, 170, 101, 44, 29, 250, 214,
						61, 116, 93, 111, 45, 229, 241, 126, 94, 175,
						15, 196, 150, 61, 38, 28, 138, 18, 67, 101,
						24, 32, 109, 192, 147, 52, 77, 90, 210, 147
					},
					new byte[]
					{
						1, 0, 0, 0, 0, 0, 1, 0, 84, 2,
						13, 65, 108, 108, 111, 119, 77, 117, 108, 116,
						105, 112, 108, 101, 0
					},
					new byte[]
					{
						1, 0, 0, 0, 0, 0, 1, 0, 84, 2,
						13, 65, 108, 108, 111, 119, 77, 117, 108, 116,
						105, 112, 108, 101, 1
					}
				};
			}
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x00017D5C File Offset: 0x00015F5C
		internal byte[] GetBytes(BlobHandle handle)
		{
			if (handle.IsVirtual)
			{
				return this.GetVirtualBlobBytes(handle, true);
			}
			int heapOffset = handle.GetHeapOffset();
			int num2;
			int num = this.Block.PeekCompressedInteger(heapOffset, out num2);
			if (num == 2147483647)
			{
				return EmptyArray<byte>.Instance;
			}
			return this.Block.PeekBytes(heapOffset + num2, num);
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x00017DB0 File Offset: 0x00015FB0
		internal MemoryBlock GetMemoryBlock(BlobHandle handle)
		{
			if (handle.IsVirtual)
			{
				return this.GetVirtualHandleMemoryBlock(handle);
			}
			int num;
			int num2;
			this.Block.PeekHeapValueOffsetAndSize(handle.GetHeapOffset(), out num, out num2);
			return this.Block.GetMemoryBlockAt(num, num2);
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x00017DF4 File Offset: 0x00015FF4
		private MemoryBlock GetVirtualHandleMemoryBlock(BlobHandle handle)
		{
			VirtualHeap orCreateVirtualHeap = VirtualHeap.GetOrCreateVirtualHeap(ref this._lazyVirtualHeap);
			VirtualHeap virtualHeap = orCreateVirtualHeap;
			MemoryBlock memoryBlock2;
			lock (virtualHeap)
			{
				MemoryBlock memoryBlock;
				if (!orCreateVirtualHeap.TryGetMemoryBlock(handle.RawValue, out memoryBlock))
				{
					memoryBlock = orCreateVirtualHeap.AddBlob(handle.RawValue, this.GetVirtualBlobBytes(handle, false));
				}
				memoryBlock2 = memoryBlock;
			}
			return memoryBlock2;
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x00017E64 File Offset: 0x00016064
		internal BlobReader GetBlobReader(BlobHandle handle)
		{
			return new BlobReader(this.GetMemoryBlock(handle));
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x00017E74 File Offset: 0x00016074
		internal BlobHandle GetNextHandle(BlobHandle handle)
		{
			if (handle.IsVirtual)
			{
				return default(BlobHandle);
			}
			int num;
			int num2;
			if (!this.Block.PeekHeapValueOffsetAndSize(handle.GetHeapOffset(), out num, out num2))
			{
				return default(BlobHandle);
			}
			int num3 = num + num2;
			if (num3 >= this.Block.Length)
			{
				return default(BlobHandle);
			}
			return BlobHandle.FromOffset(num3);
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x00017ED8 File Offset: 0x000160D8
		internal byte[] GetVirtualBlobBytes(BlobHandle handle, bool unique)
		{
			BlobHandle.VirtualIndex virtualIndex = handle.GetVirtualIndex();
			byte[] array = BlobHeap.s_virtualValues[(int)virtualIndex];
			if (virtualIndex - BlobHandle.VirtualIndex.AttributeUsage_AllowSingle <= 1)
			{
				array = (byte[])array.Clone();
				handle.SubstituteTemplateParameters(array);
			}
			else if (unique)
			{
				array = (byte[])array.Clone();
			}
			return array;
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x00017F24 File Offset: 0x00016124
		public string GetDocumentName(DocumentNameBlobHandle handle)
		{
			BlobReader blobReader = this.GetBlobReader(handle);
			int num = (int)blobReader.ReadByte();
			if (num > 127)
			{
				throw new BadImageFormatException(SR.Format(SR.InvalidDocumentName, num));
			}
			PooledStringBuilder instance = PooledStringBuilder.GetInstance();
			StringBuilder builder = instance.Builder;
			bool flag = true;
			while (blobReader.RemainingBytes > 0)
			{
				if (num != 0 && !flag)
				{
					builder.Append((char)num);
				}
				BlobReader blobReader2 = this.GetBlobReader(blobReader.ReadBlobHandle());
				builder.Append(blobReader2.ReadUTF8(blobReader2.Length));
				flag = false;
			}
			return instance.ToStringAndFree();
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x00017FBC File Offset: 0x000161BC
		internal bool DocumentNameEquals(DocumentNameBlobHandle handle, string other, bool ignoreCase)
		{
			BlobReader blobReader = this.GetBlobReader(handle);
			int num = (int)blobReader.ReadByte();
			if (num > 127)
			{
				return false;
			}
			int num2 = StringUtils.IgnoreCaseMask(ignoreCase);
			int num3 = 0;
			bool flag = true;
			while (blobReader.RemainingBytes > 0)
			{
				if (num != 0 && !flag)
				{
					if (num3 == other.Length || !StringUtils.IsEqualAscii((int)other.get_Chars(num3), num, num2))
					{
						return false;
					}
					num3++;
				}
				MemoryBlock memoryBlock = this.GetMemoryBlock(blobReader.ReadBlobHandle());
				int num4;
				MemoryBlock.FastComparisonResult fastComparisonResult = memoryBlock.Utf8NullTerminatedFastCompare(0, other, num3, out num4, '\0', ignoreCase);
				if (fastComparisonResult == MemoryBlock.FastComparisonResult.Inconclusive)
				{
					return this.GetDocumentName(handle).Equals(other, ignoreCase ? 5 : 4);
				}
				if (fastComparisonResult == MemoryBlock.FastComparisonResult.Unequal || num4 - num3 != memoryBlock.Length)
				{
					return false;
				}
				num3 = num4;
				flag = false;
			}
			return num3 == other.Length;
		}

		// Token: 0x04000540 RID: 1344
		private static byte[][] s_virtualValues;

		// Token: 0x04000541 RID: 1345
		internal readonly MemoryBlock Block;

		// Token: 0x04000542 RID: 1346
		private VirtualHeap _lazyVirtualHeap;
	}
}

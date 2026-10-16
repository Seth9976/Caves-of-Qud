using System;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Reflection.Internal
{
	// Token: 0x02000184 RID: 388
	[NullableContext(1)]
	[Nullable(0)]
	[DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
	internal readonly struct MemoryBlock
	{
		// Token: 0x06000BCD RID: 3021 RVA: 0x000238D0 File Offset: 0x00021AD0
		[NullableContext(0)]
		internal unsafe MemoryBlock(byte* buffer, int length)
		{
			this.Pointer = buffer;
			this.Length = length;
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x000238E0 File Offset: 0x00021AE0
		[NullableContext(0)]
		internal unsafe static MemoryBlock CreateChecked(byte* buffer, int length)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length");
			}
			if (buffer == null && length != 0)
			{
				throw new ArgumentNullException("buffer");
			}
			return new MemoryBlock(buffer, length);
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x0002390B File Offset: 0x00021B0B
		[MethodImpl(256)]
		private void CheckBounds(int offset, int byteCount)
		{
			if ((ulong)offset + (ulong)byteCount > (ulong)((long)this.Length))
			{
				Throw.OutOfBounds();
			}
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x00023920 File Offset: 0x00021B20
		[NullableContext(2)]
		internal byte[] ToArray()
		{
			if (this.Pointer != null)
			{
				return this.PeekBytes(0, this.Length);
			}
			return null;
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x0002393C File Offset: 0x00021B3C
		private string GetDebuggerDisplay()
		{
			if (this.Pointer == null)
			{
				return "<null>";
			}
			int num;
			return this.GetDebuggerDisplay(out num);
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x00023964 File Offset: 0x00021B64
		internal string GetDebuggerDisplay(out int displayedBytes)
		{
			displayedBytes = Math.Min(this.Length, 64);
			string text = BitConverter.ToString(this.PeekBytes(0, displayedBytes));
			if (displayedBytes < this.Length)
			{
				text += "-...";
			}
			return text;
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x000239A8 File Offset: 0x00021BA8
		internal string GetDebuggerDisplay(int offset)
		{
			if (this.Pointer == null)
			{
				return "<null>";
			}
			int num;
			string text = this.GetDebuggerDisplay(out num);
			if (offset < num)
			{
				text = text.Insert(offset * 3, "*");
			}
			else if (num == this.Length)
			{
				text += "*";
			}
			else
			{
				text += "*...";
			}
			return text;
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x00023A07 File Offset: 0x00021C07
		internal MemoryBlock GetMemoryBlockAt(int offset, int length)
		{
			this.CheckBounds(offset, length);
			return new MemoryBlock(this.Pointer + offset, length);
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x00023A1F File Offset: 0x00021C1F
		internal unsafe byte PeekByte(int offset)
		{
			this.CheckBounds(offset, 1);
			return this.Pointer[offset];
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x00023A34 File Offset: 0x00021C34
		internal int PeekInt32(int offset)
		{
			uint num = this.PeekUInt32(offset);
			if ((long)num != (long)((ulong)num))
			{
				Throw.ValueOverflow();
			}
			return (int)num;
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x00023A58 File Offset: 0x00021C58
		[MethodImpl(256)]
		internal unsafe uint PeekUInt32(int offset)
		{
			this.CheckBounds(offset, 4);
			byte* ptr = this.Pointer + offset;
			return (uint)((int)(*ptr) | ((int)ptr[1] << 8) | ((int)ptr[2] << 16) | ((int)ptr[3] << 24));
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x00023A90 File Offset: 0x00021C90
		internal unsafe int PeekCompressedInteger(int offset, out int numberOfBytesRead)
		{
			this.CheckBounds(offset, 0);
			byte* ptr = this.Pointer + offset;
			long num = (long)(this.Length - offset);
			if (num == 0L)
			{
				numberOfBytesRead = 0;
				return int.MaxValue;
			}
			byte b = *ptr;
			if ((b & 128) == 0)
			{
				numberOfBytesRead = 1;
				return (int)b;
			}
			if ((b & 64) == 0)
			{
				if (num >= 2L)
				{
					numberOfBytesRead = 2;
					return ((int)(b & 63) << 8) | (int)ptr[1];
				}
			}
			else if ((b & 32) == 0 && num >= 4L)
			{
				numberOfBytesRead = 4;
				return ((int)(b & 31) << 24) | ((int)ptr[1] << 16) | ((int)ptr[2] << 8) | (int)ptr[3];
			}
			numberOfBytesRead = 0;
			return int.MaxValue;
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x00023B24 File Offset: 0x00021D24
		[MethodImpl(256)]
		internal unsafe ushort PeekUInt16(int offset)
		{
			this.CheckBounds(offset, 2);
			byte* ptr = this.Pointer + offset;
			return (ushort)((int)(*ptr) | ((int)ptr[1] << 8));
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x00023B4C File Offset: 0x00021D4C
		internal uint PeekTaggedReference(int offset, bool smallRefSize)
		{
			return this.PeekReferenceUnchecked(offset, smallRefSize);
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x00023B56 File Offset: 0x00021D56
		internal uint PeekReferenceUnchecked(int offset, bool smallRefSize)
		{
			if (!smallRefSize)
			{
				return this.PeekUInt32(offset);
			}
			return (uint)this.PeekUInt16(offset);
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x00023B6C File Offset: 0x00021D6C
		internal int PeekReference(int offset, bool smallRefSize)
		{
			if (smallRefSize)
			{
				return (int)this.PeekUInt16(offset);
			}
			uint num = this.PeekUInt32(offset);
			if (!TokenTypeIds.IsValidRowId(num))
			{
				Throw.ReferenceOverflow();
			}
			return (int)num;
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x00023B9C File Offset: 0x00021D9C
		internal int PeekHeapReference(int offset, bool smallRefSize)
		{
			if (smallRefSize)
			{
				return (int)this.PeekUInt16(offset);
			}
			uint num = this.PeekUInt32(offset);
			if (!HeapHandleType.IsValidHeapOffset(num))
			{
				Throw.ReferenceOverflow();
			}
			return (int)num;
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x00023BCC File Offset: 0x00021DCC
		internal unsafe Guid PeekGuid(int offset)
		{
			this.CheckBounds(offset, sizeof(Guid));
			byte* ptr = this.Pointer + offset;
			if (BitConverter.IsLittleEndian)
			{
				return *(Guid*)ptr;
			}
			return new Guid((int)(*ptr) | ((int)ptr[1] << 8) | ((int)ptr[2] << 16) | ((int)ptr[3] << 24), (short)((int)ptr[4] | ((int)ptr[5] << 8)), (short)((int)ptr[6] | ((int)ptr[7] << 8)), ptr[8], ptr[9], ptr[10], ptr[11], ptr[12], ptr[13], ptr[14], ptr[15]);
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x00023C5C File Offset: 0x00021E5C
		internal unsafe string PeekUtf16(int offset, int byteCount)
		{
			this.CheckBounds(offset, byteCount);
			byte* ptr = this.Pointer + offset;
			if (BitConverter.IsLittleEndian)
			{
				return new string((char*)ptr, 0, byteCount / 2);
			}
			return Encoding.Unicode.GetString(ptr, byteCount);
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x00023C98 File Offset: 0x00021E98
		internal string PeekUtf8(int offset, int byteCount)
		{
			this.CheckBounds(offset, byteCount);
			return Encoding.UTF8.GetString(this.Pointer + offset, byteCount);
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x00023CB8 File Offset: 0x00021EB8
		internal string PeekUtf8NullTerminated(int offset, [Nullable(2)] byte[] prefix, MetadataStringDecoder utf8Decoder, out int numberOfBytesRead, char terminator = '\0')
		{
			this.CheckBounds(offset, 0);
			int utf8NullTerminatedLength = this.GetUtf8NullTerminatedLength(offset, out numberOfBytesRead, terminator);
			return EncodingHelper.DecodeUtf8(this.Pointer + offset, utf8NullTerminatedLength, prefix, utf8Decoder);
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x00023CEC File Offset: 0x00021EEC
		internal unsafe int GetUtf8NullTerminatedLength(int offset, out int numberOfBytesRead, char terminator = '\0')
		{
			this.CheckBounds(offset, 0);
			byte* ptr = this.Pointer + offset;
			byte* ptr2 = this.Pointer + this.Length;
			byte* ptr3;
			for (ptr3 = ptr; ptr3 < ptr2; ptr3++)
			{
				byte b = *ptr3;
				if (b == 0 || (char)b == terminator)
				{
					break;
				}
			}
			int num = (int)((long)(ptr3 - ptr));
			numberOfBytesRead = num;
			if (ptr3 < ptr2)
			{
				numberOfBytesRead++;
			}
			return num;
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x00023D48 File Offset: 0x00021F48
		internal unsafe int Utf8NullTerminatedOffsetOfAsciiChar(int startOffset, char asciiChar)
		{
			this.CheckBounds(startOffset, 0);
			for (int i = startOffset; i < this.Length; i++)
			{
				byte b = this.Pointer[i];
				if (b == 0)
				{
					break;
				}
				if ((char)b == asciiChar)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x00023D84 File Offset: 0x00021F84
		internal bool Utf8NullTerminatedEquals(int offset, string text, MetadataStringDecoder utf8Decoder, char terminator, bool ignoreCase)
		{
			int num;
			MemoryBlock.FastComparisonResult fastComparisonResult = this.Utf8NullTerminatedFastCompare(offset, text, 0, out num, terminator, ignoreCase);
			if (fastComparisonResult == MemoryBlock.FastComparisonResult.Inconclusive)
			{
				int num2;
				string text2 = this.PeekUtf8NullTerminated(offset, null, utf8Decoder, out num2, terminator);
				return text2.Equals(text, ignoreCase ? 5 : 4);
			}
			return fastComparisonResult == MemoryBlock.FastComparisonResult.Equal;
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x00023DC8 File Offset: 0x00021FC8
		internal bool Utf8NullTerminatedStartsWith(int offset, string text, MetadataStringDecoder utf8Decoder, char terminator, bool ignoreCase)
		{
			int num;
			MemoryBlock.FastComparisonResult fastComparisonResult = this.Utf8NullTerminatedFastCompare(offset, text, 0, out num, terminator, ignoreCase);
			if (fastComparisonResult <= MemoryBlock.FastComparisonResult.BytesStartWithText)
			{
				return true;
			}
			if (fastComparisonResult - MemoryBlock.FastComparisonResult.TextStartsWithBytes > 1)
			{
				int num2;
				string text2 = this.PeekUtf8NullTerminated(offset, null, utf8Decoder, out num2, terminator);
				return text2.StartsWith(text, ignoreCase ? 5 : 4);
			}
			return false;
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x00023E14 File Offset: 0x00022014
		internal unsafe MemoryBlock.FastComparisonResult Utf8NullTerminatedFastCompare(int offset, string text, int textStart, out int firstDifferenceIndex, char terminator, bool ignoreCase)
		{
			this.CheckBounds(offset, 0);
			byte* ptr = this.Pointer + offset;
			byte* ptr2 = this.Pointer + this.Length;
			byte* ptr3 = ptr;
			int num = StringUtils.IgnoreCaseMask(ignoreCase);
			int num2 = textStart;
			while (num2 < text.Length && ptr3 != ptr2)
			{
				byte b = *ptr3;
				if (b == 0 || (char)b == terminator)
				{
					break;
				}
				char c = text.get_Chars(num2);
				if ((b & 128) == 0 && StringUtils.IsEqualAscii((int)c, (int)b, num))
				{
					num2++;
					ptr3++;
				}
				else
				{
					firstDifferenceIndex = num2;
					if (c <= '\u007f')
					{
						return MemoryBlock.FastComparisonResult.Unequal;
					}
					return MemoryBlock.FastComparisonResult.Inconclusive;
				}
			}
			firstDifferenceIndex = num2;
			bool flag = num2 == text.Length;
			bool flag2 = ptr3 == ptr2 || *ptr3 == 0 || (char)(*ptr3) == terminator;
			if (flag && flag2)
			{
				return MemoryBlock.FastComparisonResult.Equal;
			}
			if (!flag)
			{
				return MemoryBlock.FastComparisonResult.TextStartsWithBytes;
			}
			return MemoryBlock.FastComparisonResult.BytesStartWithText;
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x00023EDC File Offset: 0x000220DC
		internal unsafe bool Utf8NullTerminatedStringStartsWithAsciiPrefix(int offset, string asciiPrefix)
		{
			this.CheckBounds(offset, 0);
			if (asciiPrefix.Length > this.Length - offset)
			{
				return false;
			}
			byte* ptr = this.Pointer + offset;
			for (int i = 0; i < asciiPrefix.Length; i++)
			{
				if (asciiPrefix.get_Chars(i) != (char)(*ptr))
				{
					return false;
				}
				ptr++;
			}
			return true;
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x00023F30 File Offset: 0x00022130
		internal unsafe int CompareUtf8NullTerminatedStringWithAsciiString(int offset, string asciiString)
		{
			this.CheckBounds(offset, 0);
			byte* ptr = this.Pointer + offset;
			int num = this.Length - offset;
			for (int i = 0; i < asciiString.Length; i++)
			{
				if (i > num)
				{
					return -1;
				}
				if ((char)(*ptr) != asciiString.get_Chars(i))
				{
					return (int)((char)(*ptr) - asciiString.get_Chars(i));
				}
				ptr++;
			}
			if (*ptr != 0)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x00023F8F File Offset: 0x0002218F
		internal byte[] PeekBytes(int offset, int byteCount)
		{
			this.CheckBounds(offset, byteCount);
			return BlobUtilities.ReadBytes(this.Pointer + offset, byteCount);
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x00023FA7 File Offset: 0x000221A7
		internal int IndexOf(byte b, int start)
		{
			this.CheckBounds(start, 0);
			return this.IndexOfUnchecked(b, start);
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x00023FBC File Offset: 0x000221BC
		internal unsafe int IndexOfUnchecked(byte b, int start)
		{
			byte* ptr = this.Pointer + start;
			byte* ptr2 = this.Pointer + this.Length;
			while (ptr < ptr2)
			{
				if (*ptr == b)
				{
					return (int)((long)(ptr - this.Pointer));
				}
				ptr++;
			}
			return -1;
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x00024000 File Offset: 0x00022200
		internal int BinarySearch(string[] asciiKeys, int offset)
		{
			int i = 0;
			int num = asciiKeys.Length - 1;
			while (i <= num)
			{
				int num2 = i + (num - i >> 1);
				string text = asciiKeys[num2];
				int num3 = this.CompareUtf8NullTerminatedStringWithAsciiString(offset, text);
				if (num3 == 0)
				{
					return num2;
				}
				if (num3 < 0)
				{
					num = num2 - 1;
				}
				else
				{
					i = num2 + 1;
				}
			}
			return ~i;
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x00024048 File Offset: 0x00022248
		internal int BinarySearchForSlot(int rowCount, int rowSize, int referenceListOffset, uint referenceValue, bool isReferenceSmall)
		{
			int num = 0;
			int num2 = rowCount - 1;
			uint num3 = this.PeekReferenceUnchecked(num * rowSize + referenceListOffset, isReferenceSmall);
			uint num4 = this.PeekReferenceUnchecked(num2 * rowSize + referenceListOffset, isReferenceSmall);
			if (num2 != 1)
			{
				while (num2 - num > 1)
				{
					if (referenceValue <= num3)
					{
						if (referenceValue != num3)
						{
							return num - 1;
						}
						return num;
					}
					else if (referenceValue >= num4)
					{
						if (referenceValue != num4)
						{
							return num2 + 1;
						}
						return num2;
					}
					else
					{
						int num5 = (num + num2) / 2;
						uint num6 = this.PeekReferenceUnchecked(num5 * rowSize + referenceListOffset, isReferenceSmall);
						if (referenceValue > num6)
						{
							num = num5;
							num3 = num6;
						}
						else
						{
							if (referenceValue >= num6)
							{
								return num5;
							}
							num2 = num5;
							num4 = num6;
						}
					}
				}
				return num;
			}
			if (referenceValue >= num4)
			{
				return num2;
			}
			return num;
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x000240E4 File Offset: 0x000222E4
		internal int BinarySearchReference(int rowCount, int rowSize, int referenceOffset, uint referenceValue, bool isReferenceSmall)
		{
			int i = 0;
			int num = rowCount - 1;
			while (i <= num)
			{
				int num2 = (i + num) / 2;
				uint num3 = this.PeekReferenceUnchecked(num2 * rowSize + referenceOffset, isReferenceSmall);
				if (referenceValue > num3)
				{
					i = num2 + 1;
				}
				else
				{
					if (referenceValue >= num3)
					{
						return num2;
					}
					num = num2 - 1;
				}
			}
			return -1;
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x0002412C File Offset: 0x0002232C
		internal int BinarySearchReference(int[] ptrTable, int rowSize, int referenceOffset, uint referenceValue, bool isReferenceSmall)
		{
			int i = 0;
			int num = ptrTable.Length - 1;
			while (i <= num)
			{
				int num2 = (i + num) / 2;
				uint num3 = this.PeekReferenceUnchecked((ptrTable[num2] - 1) * rowSize + referenceOffset, isReferenceSmall);
				if (referenceValue > num3)
				{
					i = num2 + 1;
				}
				else
				{
					if (referenceValue >= num3)
					{
						return num2;
					}
					num = num2 - 1;
				}
			}
			return -1;
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x00024178 File Offset: 0x00022378
		internal void BinarySearchReferenceRange(int rowCount, int rowSize, int referenceOffset, uint referenceValue, bool isReferenceSmall, out int startRowNumber, out int endRowNumber)
		{
			int num = this.BinarySearchReference(rowCount, rowSize, referenceOffset, referenceValue, isReferenceSmall);
			if (num == -1)
			{
				startRowNumber = -1;
				endRowNumber = -1;
				return;
			}
			startRowNumber = num;
			while (startRowNumber > 0 && this.PeekReferenceUnchecked((startRowNumber - 1) * rowSize + referenceOffset, isReferenceSmall) == referenceValue)
			{
				startRowNumber--;
			}
			endRowNumber = num;
			while (endRowNumber + 1 < rowCount && this.PeekReferenceUnchecked((endRowNumber + 1) * rowSize + referenceOffset, isReferenceSmall) == referenceValue)
			{
				endRowNumber++;
			}
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x000241F4 File Offset: 0x000223F4
		internal void BinarySearchReferenceRange(int[] ptrTable, int rowSize, int referenceOffset, uint referenceValue, bool isReferenceSmall, out int startRowNumber, out int endRowNumber)
		{
			int num = this.BinarySearchReference(ptrTable, rowSize, referenceOffset, referenceValue, isReferenceSmall);
			if (num == -1)
			{
				startRowNumber = -1;
				endRowNumber = -1;
				return;
			}
			startRowNumber = num;
			while (startRowNumber > 0 && this.PeekReferenceUnchecked((ptrTable[startRowNumber - 1] - 1) * rowSize + referenceOffset, isReferenceSmall) == referenceValue)
			{
				startRowNumber--;
			}
			endRowNumber = num;
			while (endRowNumber + 1 < ptrTable.Length && this.PeekReferenceUnchecked((ptrTable[endRowNumber + 1] - 1) * rowSize + referenceOffset, isReferenceSmall) == referenceValue)
			{
				endRowNumber++;
			}
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x0002427C File Offset: 0x0002247C
		internal int LinearSearchReference(int rowSize, int referenceOffset, uint referenceValue, bool isReferenceSmall)
		{
			int i = referenceOffset;
			int length = this.Length;
			while (i < length)
			{
				uint num = this.PeekReferenceUnchecked(i, isReferenceSmall);
				if (num == referenceValue)
				{
					return i / rowSize;
				}
				i += rowSize;
			}
			return -1;
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x000242B0 File Offset: 0x000224B0
		internal bool IsOrderedByReferenceAscending(int rowSize, int referenceOffset, bool isReferenceSmall)
		{
			int i = referenceOffset;
			int length = this.Length;
			uint num = 0U;
			while (i < length)
			{
				uint num2 = this.PeekReferenceUnchecked(i, isReferenceSmall);
				if (num2 < num)
				{
					return false;
				}
				num = num2;
				i += rowSize;
			}
			return true;
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x000242E4 File Offset: 0x000224E4
		internal int[] BuildPtrTable(int numberOfRows, int rowSize, int referenceOffset, bool isReferenceSmall)
		{
			int[] array = new int[numberOfRows];
			uint[] unsortedReferences = new uint[numberOfRows];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = i + 1;
			}
			this.ReadColumn(unsortedReferences, rowSize, referenceOffset, isReferenceSmall);
			Array.Sort<int>(array, (int a, int b) => unsortedReferences[a - 1].CompareTo(unsortedReferences[b - 1]));
			return array;
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x00024344 File Offset: 0x00022544
		private void ReadColumn(uint[] result, int rowSize, int referenceOffset, bool isReferenceSmall)
		{
			int i = referenceOffset;
			int length = this.Length;
			int num = 0;
			while (i < length)
			{
				result[num] = this.PeekReferenceUnchecked(i, isReferenceSmall);
				i += rowSize;
				num++;
			}
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x00024378 File Offset: 0x00022578
		internal bool PeekHeapValueOffsetAndSize(int index, out int offset, out int size)
		{
			int num2;
			int num = this.PeekCompressedInteger(index, out num2);
			if (num == 2147483647)
			{
				offset = 0;
				size = 0;
				return false;
			}
			offset = index + num2;
			size = num;
			return true;
		}

		// Token: 0x04000967 RID: 2407
		[Nullable(0)]
		internal unsafe readonly byte* Pointer;

		// Token: 0x04000968 RID: 2408
		internal readonly int Length;

		// Token: 0x0200020B RID: 523
		[NullableContext(0)]
		internal enum FastComparisonResult
		{
			// Token: 0x04000BE9 RID: 3049
			Equal,
			// Token: 0x04000BEA RID: 3050
			BytesStartWithText,
			// Token: 0x04000BEB RID: 3051
			TextStartsWithBytes,
			// Token: 0x04000BEC RID: 3052
			Unequal,
			// Token: 0x04000BED RID: 3053
			Inconclusive
		}
	}
}

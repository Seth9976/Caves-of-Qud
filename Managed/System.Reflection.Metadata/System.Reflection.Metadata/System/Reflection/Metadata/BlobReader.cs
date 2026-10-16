using System;
using System.Diagnostics;
using System.Reflection.Internal;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection.Metadata
{
	// Token: 0x02000073 RID: 115
	[DebuggerDisplay("{GetDebuggerDisplay(),nq}")]
	public struct BlobReader
	{
		/// <summary>Creates a reader of the specified memory block.</summary>
		/// <param name="buffer">A pointer to the start of the memory block.</param>
		/// <param name="length">Length in bytes of the memory block.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is <see langword="null" /> and <paramref name="length" /> is greater than zero.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="length" /> is negative.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The current platform is not little-endian.</exception>
		// Token: 0x060004B9 RID: 1209 RVA: 0x0000B650 File Offset: 0x00009850
		public unsafe BlobReader(byte* buffer, int length)
		{
			this = new BlobReader(MemoryBlock.CreateChecked(buffer, length));
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0000B65F File Offset: 0x0000985F
		internal BlobReader(MemoryBlock block)
		{
			this._block = block;
			this._currentPointer = block.Pointer;
			this._endPointer = block.Pointer + block.Length;
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0000B688 File Offset: 0x00009888
		[NullableContext(1)]
		internal string GetDebuggerDisplay()
		{
			if (this._block.Pointer == null)
			{
				return "<null>";
			}
			int num;
			string text = this._block.GetDebuggerDisplay(out num);
			if (this.Offset < num)
			{
				text = text.Insert(this.Offset * 3, "*");
			}
			else if (num == this._block.Length)
			{
				text += "*";
			}
			else
			{
				text += "*...";
			}
			return text;
		}

		/// <summary>Gets a pointer to the byte at the start of the underlying memory block.</summary>
		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x0000B700 File Offset: 0x00009900
		public unsafe byte* StartPointer
		{
			get
			{
				return this._block.Pointer;
			}
		}

		/// <summary>Gets a pointer to the byte at the current position of the reader.</summary>
		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x0000B70D File Offset: 0x0000990D
		public unsafe byte* CurrentPointer
		{
			get
			{
				return this._currentPointer;
			}
		}

		/// <summary>Gets the total length of the underlying memory block.</summary>
		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x0000B715 File Offset: 0x00009915
		public int Length
		{
			get
			{
				return this._block.Length;
			}
		}

		/// <summary>Gets or sets the offset from the start of the blob to the current position.</summary>
		/// <exception cref="T:System.BadImageFormatException">The offset is set outside the bounds of the underlying reader.</exception>
		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x0000B722 File Offset: 0x00009922
		// (set) Token: 0x060004C0 RID: 1216 RVA: 0x0000B73A File Offset: 0x0000993A
		public int Offset
		{
			get
			{
				return (int)((long)(this._currentPointer - this._block.Pointer));
			}
			set
			{
				if (value > this._block.Length)
				{
					Throw.OutOfBounds();
				}
				this._currentPointer = this._block.Pointer + value;
			}
		}

		/// <summary>Gets the number of bytes remaining from current position to the end of the underlying memory block.</summary>
		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x0000B762 File Offset: 0x00009962
		public int RemainingBytes
		{
			get
			{
				return (int)((long)(this._endPointer - this._currentPointer));
			}
		}

		/// <summary>Repositions the reader to the start of the underlying memory block.</summary>
		// Token: 0x060004C2 RID: 1218 RVA: 0x0000B775 File Offset: 0x00009975
		public void Reset()
		{
			this._currentPointer = this._block.Pointer;
		}

		/// <summary>Repositions the reader forward by the number of bytes required to satisfy the given alignment.</summary>
		/// <param name="alignment" />
		// Token: 0x060004C3 RID: 1219 RVA: 0x0000B788 File Offset: 0x00009988
		public void Align(byte alignment)
		{
			if (!this.TryAlign(alignment))
			{
				Throw.OutOfBounds();
			}
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0000B798 File Offset: 0x00009998
		internal bool TryAlign(byte alignment)
		{
			int num = this.Offset & (int)(alignment - 1);
			if (num != 0)
			{
				int num2 = (int)alignment - num;
				if (num2 > this.RemainingBytes)
				{
					return false;
				}
				this._currentPointer += num2;
			}
			return true;
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0000B7D1 File Offset: 0x000099D1
		internal MemoryBlock GetMemoryBlockAt(int offset, int length)
		{
			this.CheckBounds(offset, length);
			return new MemoryBlock(this._currentPointer + offset, length);
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0000B7E9 File Offset: 0x000099E9
		[MethodImpl(256)]
		private void CheckBounds(int offset, int byteCount)
		{
			if ((ulong)offset + (ulong)byteCount > (ulong)((long)(this._endPointer - this._currentPointer)))
			{
				Throw.OutOfBounds();
			}
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0000B807 File Offset: 0x00009A07
		[MethodImpl(256)]
		private void CheckBounds(int byteCount)
		{
			if ((ulong)byteCount > (ulong)((long)(this._endPointer - this._currentPointer)))
			{
				Throw.OutOfBounds();
			}
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0000B824 File Offset: 0x00009A24
		[MethodImpl(256)]
		private unsafe byte* GetCurrentPointerAndAdvance(int length)
		{
			byte* currentPointer = this._currentPointer;
			if (length > (int)((uint)((long)(this._endPointer - currentPointer))))
			{
				Throw.OutOfBounds();
			}
			this._currentPointer = currentPointer + length;
			return currentPointer;
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0000B858 File Offset: 0x00009A58
		[MethodImpl(256)]
		private unsafe byte* GetCurrentPointerAndAdvance1()
		{
			byte* currentPointer = this._currentPointer;
			if (currentPointer == this._endPointer)
			{
				Throw.OutOfBounds();
			}
			this._currentPointer = currentPointer + 1;
			return currentPointer;
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0000B884 File Offset: 0x00009A84
		public bool ReadBoolean()
		{
			return this.ReadByte() > 0;
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0000B88F File Offset: 0x00009A8F
		public unsafe sbyte ReadSByte()
		{
			return *(sbyte*)this.GetCurrentPointerAndAdvance1();
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0000B898 File Offset: 0x00009A98
		public unsafe byte ReadByte()
		{
			return *this.GetCurrentPointerAndAdvance1();
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x0000B8A4 File Offset: 0x00009AA4
		public unsafe char ReadChar()
		{
			byte* currentPointerAndAdvance = this.GetCurrentPointerAndAdvance(2);
			return (char)((int)(*currentPointerAndAdvance) + ((int)currentPointerAndAdvance[1] << 8));
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0000B8C4 File Offset: 0x00009AC4
		public unsafe short ReadInt16()
		{
			byte* currentPointerAndAdvance = this.GetCurrentPointerAndAdvance(2);
			return (short)((int)(*currentPointerAndAdvance) + ((int)currentPointerAndAdvance[1] << 8));
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x0000B8E4 File Offset: 0x00009AE4
		public unsafe ushort ReadUInt16()
		{
			byte* currentPointerAndAdvance = this.GetCurrentPointerAndAdvance(2);
			return (ushort)((int)(*currentPointerAndAdvance) + ((int)currentPointerAndAdvance[1] << 8));
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x0000B904 File Offset: 0x00009B04
		public unsafe int ReadInt32()
		{
			byte* currentPointerAndAdvance = this.GetCurrentPointerAndAdvance(4);
			return (int)(*currentPointerAndAdvance) + ((int)currentPointerAndAdvance[1] << 8) + ((int)currentPointerAndAdvance[2] << 16) + ((int)currentPointerAndAdvance[3] << 24);
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x0000B934 File Offset: 0x00009B34
		public unsafe uint ReadUInt32()
		{
			byte* currentPointerAndAdvance = this.GetCurrentPointerAndAdvance(4);
			return (uint)((int)(*currentPointerAndAdvance) + ((int)currentPointerAndAdvance[1] << 8) + ((int)currentPointerAndAdvance[2] << 16) + ((int)currentPointerAndAdvance[3] << 24));
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x0000B964 File Offset: 0x00009B64
		public unsafe long ReadInt64()
		{
			byte* currentPointerAndAdvance = this.GetCurrentPointerAndAdvance(8);
			uint num = (uint)((int)(*currentPointerAndAdvance) + ((int)currentPointerAndAdvance[1] << 8) + ((int)currentPointerAndAdvance[2] << 16) + ((int)currentPointerAndAdvance[3] << 24));
			uint num2 = (uint)((int)currentPointerAndAdvance[4] + ((int)currentPointerAndAdvance[5] << 8) + ((int)currentPointerAndAdvance[6] << 16) + ((int)currentPointerAndAdvance[7] << 24));
			return (long)((ulong)num + ((ulong)num2 << 32));
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x0000B9B7 File Offset: 0x00009BB7
		public ulong ReadUInt64()
		{
			return (ulong)this.ReadInt64();
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0000B9C0 File Offset: 0x00009BC0
		public unsafe float ReadSingle()
		{
			int num = this.ReadInt32();
			return *(float*)(&num);
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0000B9D8 File Offset: 0x00009BD8
		public unsafe double ReadDouble()
		{
			long num = this.ReadInt64();
			return *(double*)(&num);
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0000B9F0 File Offset: 0x00009BF0
		public unsafe Guid ReadGuid()
		{
			byte* currentPointerAndAdvance = this.GetCurrentPointerAndAdvance(16);
			if (BitConverter.IsLittleEndian)
			{
				return *(Guid*)currentPointerAndAdvance;
			}
			return new Guid((int)(*currentPointerAndAdvance) | ((int)currentPointerAndAdvance[1] << 8) | ((int)currentPointerAndAdvance[2] << 16) | ((int)currentPointerAndAdvance[3] << 24), (short)((int)currentPointerAndAdvance[4] | ((int)currentPointerAndAdvance[5] << 8)), (short)((int)currentPointerAndAdvance[6] | ((int)currentPointerAndAdvance[7] << 8)), currentPointerAndAdvance[8], currentPointerAndAdvance[9], currentPointerAndAdvance[10], currentPointerAndAdvance[11], currentPointerAndAdvance[12], currentPointerAndAdvance[13], currentPointerAndAdvance[14], currentPointerAndAdvance[15]);
		}

		/// <summary>Reads a <see cref="T:System.Decimal" /> number.</summary>
		/// <exception cref="T:System.BadImageFormatException">The data at the current position was not a valid <see cref="T:System.Decimal" /> number.</exception>
		// Token: 0x060004D7 RID: 1239 RVA: 0x0000BA74 File Offset: 0x00009C74
		public unsafe decimal ReadDecimal()
		{
			byte* currentPointerAndAdvance = this.GetCurrentPointerAndAdvance(13);
			byte b = *currentPointerAndAdvance & 127;
			if (b > 28)
			{
				throw new BadImageFormatException(SR.ValueTooLarge);
			}
			return new decimal((int)currentPointerAndAdvance[1] | ((int)currentPointerAndAdvance[2] << 8) | ((int)currentPointerAndAdvance[3] << 16) | ((int)currentPointerAndAdvance[4] << 24), (int)currentPointerAndAdvance[5] | ((int)currentPointerAndAdvance[6] << 8) | ((int)currentPointerAndAdvance[7] << 16) | ((int)currentPointerAndAdvance[8] << 24), (int)currentPointerAndAdvance[9] | ((int)currentPointerAndAdvance[10] << 8) | ((int)currentPointerAndAdvance[11] << 16) | ((int)currentPointerAndAdvance[12] << 24), (*currentPointerAndAdvance & 128) > 0, b);
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0000BB07 File Offset: 0x00009D07
		public DateTime ReadDateTime()
		{
			return new DateTime(this.ReadInt64());
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0000BB14 File Offset: 0x00009D14
		public SignatureHeader ReadSignatureHeader()
		{
			return new SignatureHeader(this.ReadByte());
		}

		/// <summary>Searches for a specified byte in the blob following the current position.</summary>
		/// <param name="value">The byte value to find.</param>
		/// <returns>The index relative to the current position, or -1 if the byte is not found in the blob following the current position.</returns>
		// Token: 0x060004DA RID: 1242 RVA: 0x0000BB24 File Offset: 0x00009D24
		public int IndexOf(byte value)
		{
			int offset = this.Offset;
			int num = this._block.IndexOfUnchecked(value, offset);
			if (num < 0)
			{
				return -1;
			}
			return num - offset;
		}

		/// <summary>Reads a UTF8 encoded string starting at the current position.</summary>
		/// <param name="byteCount">The number of bytes to read.</param>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="byteCount" /> bytes not available.</exception>
		/// <returns>The string.</returns>
		// Token: 0x060004DB RID: 1243 RVA: 0x0000BB50 File Offset: 0x00009D50
		[NullableContext(1)]
		public string ReadUTF8(int byteCount)
		{
			string text = this._block.PeekUtf8(this.Offset, byteCount);
			this._currentPointer += byteCount;
			return text;
		}

		/// <summary>Reads a UTF16 (little-endian) encoded string starting at the current position.</summary>
		/// <param name="byteCount">The number of bytes to read.</param>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="byteCount" /> bytes not available.</exception>
		/// <returns>The string.</returns>
		// Token: 0x060004DC RID: 1244 RVA: 0x0000BB80 File Offset: 0x00009D80
		[NullableContext(1)]
		public string ReadUTF16(int byteCount)
		{
			string text = this._block.PeekUtf16(this.Offset, byteCount);
			this._currentPointer += byteCount;
			return text;
		}

		/// <summary>Reads bytes starting at the current position.</summary>
		/// <param name="byteCount">The number of bytes to read.</param>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="byteCount" /> bytes not available.</exception>
		/// <returns>The byte array.</returns>
		// Token: 0x060004DD RID: 1245 RVA: 0x0000BBB0 File Offset: 0x00009DB0
		[NullableContext(1)]
		public byte[] ReadBytes(int byteCount)
		{
			byte[] array = this._block.PeekBytes(this.Offset, byteCount);
			this._currentPointer += byteCount;
			return array;
		}

		/// <summary>Reads bytes starting at the current position and writes them to the specified buffer starting at the specified offset.</summary>
		/// <param name="byteCount">The number of bytes to read.</param>
		/// <param name="buffer">The destination buffer the bytes read will be written to.</param>
		/// <param name="bufferOffset">The offset in the destination buffer where the bytes read will be written.</param>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="byteCount" /> bytes not available.</exception>
		// Token: 0x060004DE RID: 1246 RVA: 0x0000BBDF File Offset: 0x00009DDF
		[NullableContext(1)]
		public unsafe void ReadBytes(int byteCount, byte[] buffer, int bufferOffset)
		{
			Marshal.Copy((IntPtr)((void*)this.GetCurrentPointerAndAdvance(byteCount)), buffer, bufferOffset, byteCount);
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x0000BBF8 File Offset: 0x00009DF8
		[NullableContext(1)]
		internal string ReadUtf8NullTerminated()
		{
			int num;
			string text = this._block.PeekUtf8NullTerminated(this.Offset, null, MetadataStringDecoder.DefaultUTF8, out num, '\0');
			this._currentPointer += num;
			return text;
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x0000BC30 File Offset: 0x00009E30
		private int ReadCompressedIntegerOrInvalid()
		{
			int num2;
			int num = this._block.PeekCompressedInteger(this.Offset, out num2);
			this._currentPointer += num2;
			return num;
		}

		/// <summary>Reads an unsigned compressed integer value. See Metadata Specification section II.23.2: Blobs and signatures.</summary>
		/// <param name="value">The value of the compressed integer that was read.</param>
		/// <returns>
		///   <see langword="true" /> if the value was read successfully. <see langword="false" /> if the data at the current position was not a valid compressed integer.</returns>
		// Token: 0x060004E1 RID: 1249 RVA: 0x0000BC60 File Offset: 0x00009E60
		public bool TryReadCompressedInteger(out int value)
		{
			value = this.ReadCompressedIntegerOrInvalid();
			return value != int.MaxValue;
		}

		/// <summary>Reads an unsigned compressed integer value. See Metadata Specification section II.23.2: Blobs and signatures.</summary>
		/// <exception cref="T:System.BadImageFormatException">The data at the current position was not a valid compressed integer.</exception>
		/// <returns>The value of the compressed integer that was read.</returns>
		// Token: 0x060004E2 RID: 1250 RVA: 0x0000BC78 File Offset: 0x00009E78
		public int ReadCompressedInteger()
		{
			int num;
			if (!this.TryReadCompressedInteger(out num))
			{
				Throw.InvalidCompressedInteger();
			}
			return num;
		}

		/// <summary>Reads a signed compressed integer value. See Metadata Specification section II.23.2: Blobs and signatures.</summary>
		/// <param name="value">The value of the compressed integer that was read.</param>
		/// <returns>
		///   <see langword="true" /> if the value was read successfully. <see langword="false" /> if the data at the current position was not a valid compressed integer.</returns>
		// Token: 0x060004E3 RID: 1251 RVA: 0x0000BC98 File Offset: 0x00009E98
		public bool TryReadCompressedSignedInteger(out int value)
		{
			int num;
			value = this._block.PeekCompressedInteger(this.Offset, out num);
			if (value == 2147483647)
			{
				return false;
			}
			bool flag = (value & 1) != 0;
			value >>= 1;
			if (flag)
			{
				if (num != 1)
				{
					if (num != 2)
					{
						value |= -268435456;
					}
					else
					{
						value |= -8192;
					}
				}
				else
				{
					value |= -64;
				}
			}
			this._currentPointer += num;
			return true;
		}

		/// <summary>Reads a signed compressed integer value. See Metadata Specification section II.23.2: Blobs and signatures.</summary>
		/// <exception cref="T:System.BadImageFormatException">The data at the current position was not a valid compressed integer.</exception>
		/// <returns>The value of the compressed integer that was read.</returns>
		// Token: 0x060004E4 RID: 1252 RVA: 0x0000BD10 File Offset: 0x00009F10
		public int ReadCompressedSignedInteger()
		{
			int num;
			if (!this.TryReadCompressedSignedInteger(out num))
			{
				Throw.InvalidCompressedInteger();
			}
			return num;
		}

		/// <summary>Reads a type code encoded in a serialized custom attribute value.</summary>
		/// <returns>
		///   <see cref="F:System.Reflection.Metadata.SerializationTypeCode.Invalid" /> if the encoding is invalid.</returns>
		// Token: 0x060004E5 RID: 1253 RVA: 0x0000BD30 File Offset: 0x00009F30
		public SerializationTypeCode ReadSerializationTypeCode()
		{
			int num = this.ReadCompressedIntegerOrInvalid();
			if (num > 255)
			{
				return SerializationTypeCode.Invalid;
			}
			return (SerializationTypeCode)num;
		}

		/// <summary>Reads a type code encoded in a signature.</summary>
		/// <returns>The type code encoded in the serialized custom attribute value if the encoding is valid, or <see cref="F:System.Reflection.Metadata.SignatureTypeCode.Invalid" /> if the encoding is invalid.</returns>
		// Token: 0x060004E6 RID: 1254 RVA: 0x0000BD50 File Offset: 0x00009F50
		public SignatureTypeCode ReadSignatureTypeCode()
		{
			int num = this.ReadCompressedIntegerOrInvalid();
			if (num - 17 <= 1)
			{
				return SignatureTypeCode.TypeHandle;
			}
			if (num > 255)
			{
				return SignatureTypeCode.Invalid;
			}
			return (SignatureTypeCode)num;
		}

		/// <summary>Reads a string encoded as a compressed integer containing its length followed by its contents in UTF8. Null strings are encoded as a single 0xFF byte.</summary>
		/// <exception cref="T:System.BadImageFormatException">The encoding is invalid.</exception>
		/// <returns>A string value, or <see langword="null" />.</returns>
		// Token: 0x060004E7 RID: 1255 RVA: 0x0000BD7C File Offset: 0x00009F7C
		[NullableContext(2)]
		public string ReadSerializedString()
		{
			int num;
			if (this.TryReadCompressedInteger(out num))
			{
				return this.ReadUTF8(num).TrimEnd(BlobReader.s_nullCharArray);
			}
			if (this.ReadByte() != 255)
			{
				Throw.InvalidSerializedString();
			}
			return null;
		}

		/// <summary>Reads a type handle encoded in a signature as TypeDefOrRefOrSpecEncoded (see ECMA-335 II.23.2.8).</summary>
		/// <returns>The handle when the encoding is valid. Otherwise, a handle where the <see cref="P:System.Reflection.Metadata.EntityHandle.IsNil" /> property is <see langword="true" />.</returns>
		// Token: 0x060004E8 RID: 1256 RVA: 0x0000BDB8 File Offset: 0x00009FB8
		public EntityHandle ReadTypeHandle()
		{
			uint num = (uint)this.ReadCompressedIntegerOrInvalid();
			uint num2 = BlobReader.s_corEncodeTokenArray[(int)(num & 3U)];
			if (num == 2147483647U || num2 == 0U)
			{
				return default(EntityHandle);
			}
			return new EntityHandle(num2 | (num >> 2));
		}

		/// <summary>Reads a Blob heap handle encoded as a compressed integer.</summary>
		// Token: 0x060004E9 RID: 1257 RVA: 0x0000BDF5 File Offset: 0x00009FF5
		public BlobHandle ReadBlobHandle()
		{
			return BlobHandle.FromOffset(this.ReadCompressedInteger());
		}

		/// <summary>Reads a constant value (see ECMA-335 Partition II section 22.9) from the current position.</summary>
		/// <param name="typeCode" />
		/// <exception cref="T:System.BadImageFormatException">Error while reading from the blob.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="typeCode" /> is not a valid <see cref="T:System.Reflection.Metadata.ConstantTypeCode" />.</exception>
		/// <returns>A boxed constant value. To avoid allocating the object use Read* methods directly.</returns>
		// Token: 0x060004EA RID: 1258 RVA: 0x0000BE04 File Offset: 0x0000A004
		[NullableContext(2)]
		public object ReadConstant(ConstantTypeCode typeCode)
		{
			switch (typeCode)
			{
			case ConstantTypeCode.Boolean:
				return this.ReadBoolean();
			case ConstantTypeCode.Char:
				return this.ReadChar();
			case ConstantTypeCode.SByte:
				return this.ReadSByte();
			case ConstantTypeCode.Byte:
				return this.ReadByte();
			case ConstantTypeCode.Int16:
				return this.ReadInt16();
			case ConstantTypeCode.UInt16:
				return this.ReadUInt16();
			case ConstantTypeCode.Int32:
				return this.ReadInt32();
			case ConstantTypeCode.UInt32:
				return this.ReadUInt32();
			case ConstantTypeCode.Int64:
				return this.ReadInt64();
			case ConstantTypeCode.UInt64:
				return this.ReadUInt64();
			case ConstantTypeCode.Single:
				return this.ReadSingle();
			case ConstantTypeCode.Double:
				return this.ReadDouble();
			case ConstantTypeCode.String:
				return this.ReadUTF16(this.RemainingBytes);
			case ConstantTypeCode.NullReference:
				if (this.ReadUInt32() != 0U)
				{
					throw new BadImageFormatException(SR.InvalidConstantValue);
				}
				return null;
			}
			throw new ArgumentOutOfRangeException("typeCode");
		}

		// Token: 0x0400030F RID: 783
		private static readonly char[] s_nullCharArray = new char[1];

		// Token: 0x04000310 RID: 784
		internal const int InvalidCompressedInteger = 2147483647;

		// Token: 0x04000311 RID: 785
		private readonly MemoryBlock _block;

		// Token: 0x04000312 RID: 786
		private unsafe readonly byte* _endPointer;

		// Token: 0x04000313 RID: 787
		private unsafe byte* _currentPointer;

		// Token: 0x04000314 RID: 788
		private static readonly uint[] s_corEncodeTokenArray = new uint[] { 33554432U, 16777216U, 452984832U, 0U };
	}
}

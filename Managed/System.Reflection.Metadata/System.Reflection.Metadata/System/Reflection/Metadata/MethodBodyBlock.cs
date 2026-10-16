using System;
using System.Collections.Immutable;
using System.Reflection.Internal;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	/// <summary>Represents the method body in ECMA 335 assembly.</summary>
	// Token: 0x020000AA RID: 170
	public sealed class MethodBodyBlock
	{
		// Token: 0x06000692 RID: 1682 RVA: 0x00010BED File Offset: 0x0000EDED
		private MethodBodyBlock(bool localVariablesInitialized, ushort maxStack, StandaloneSignatureHandle localSignatureHandle, MemoryBlock il, ImmutableArray<ExceptionRegion> exceptionRegions, int size)
		{
			this._localVariablesInitialized = localVariablesInitialized;
			this._maxStack = maxStack;
			this._localSignature = localSignatureHandle;
			this._il = il;
			this._exceptionRegions = exceptionRegions;
			this._size = size;
		}

		/// <summary>Gets the size of the method body, including the header, IL, and exception regions.</summary>
		/// <returns>The size of the method body.</returns>
		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x00010C22 File Offset: 0x0000EE22
		public int Size
		{
			get
			{
				return this._size;
			}
		}

		/// <summary>Gets the maximum number of items on the evaluation stack for this method.</summary>
		/// <returns>The maximum number of items on the evaluation stack.</returns>
		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x00010C2A File Offset: 0x0000EE2A
		public int MaxStack
		{
			get
			{
				return (int)this._maxStack;
			}
		}

		/// <summary>Gets a value that indicates whether local variables in this method are initialized to default values of their types.</summary>
		/// <returns>
		///   <see langword="true" /> if local variables are initialized; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x00010C32 File Offset: 0x0000EE32
		public bool LocalVariablesInitialized
		{
			get
			{
				return this._localVariablesInitialized;
			}
		}

		/// <summary>Gets the handle to the local variables signature.</summary>
		/// <returns>The handle to the local variables signature.</returns>
		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x00010C3A File Offset: 0x0000EE3A
		public StandaloneSignatureHandle LocalSignature
		{
			get
			{
				return this._localSignature;
			}
		}

		/// <summary>Gets the array of exception regions in this method body.</summary>
		/// <returns>The array of exception regions.</returns>
		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x00010C42 File Offset: 0x0000EE42
		public ImmutableArray<ExceptionRegion> ExceptionRegions
		{
			get
			{
				return this._exceptionRegions;
			}
		}

		/// <summary>Gets the IL bytecode of this method body as a byte array.</summary>
		/// <returns>A byte array with the IL bytecode of this method body.</returns>
		// Token: 0x06000698 RID: 1688 RVA: 0x00010C4A File Offset: 0x0000EE4A
		[NullableContext(2)]
		public byte[] GetILBytes()
		{
			return this._il.ToArray();
		}

		/// <summary>Gets the IL bytecode of this method body as an immutable array.</summary>
		/// <returns>An immutable byte array with the IL bytecode of this method body.</returns>
		// Token: 0x06000699 RID: 1689 RVA: 0x00010C58 File Offset: 0x0000EE58
		public ImmutableArray<byte> GetILContent()
		{
			byte[] ilbytes = this.GetILBytes();
			return ImmutableByteArrayInterop.DangerousCreateFromUnderlyingArray(ref ilbytes);
		}

		/// <summary>Gets a blob reader that reads the IL bytecode of this method body.</summary>
		/// <returns>A blob reader that reads the IL bytecode of this method body.</returns>
		// Token: 0x0600069A RID: 1690 RVA: 0x00010C73 File Offset: 0x0000EE73
		public BlobReader GetILReader()
		{
			return new BlobReader(this._il);
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Reflection.Metadata.MethodBodyBlock" /> class using the specified blob reader.</summary>
		/// <param name="reader">The blob reader to read the method body.</param>
		/// <exception cref="T:System.BadImageFormatException">
		///     The method body data in the specified blob reader is invalid.</exception>
		/// <returns>A new instance of the <see cref="T:System.Reflection.Metadata.MethodBodyBlock" /> class.</returns>
		// Token: 0x0600069B RID: 1691 RVA: 0x00010C80 File Offset: 0x0000EE80
		[NullableContext(1)]
		public static MethodBodyBlock Create(BlobReader reader)
		{
			int offset = reader.Offset;
			byte b = reader.ReadByte();
			int num;
			if ((b & 3) == 2)
			{
				num = b >> 2;
				return new MethodBodyBlock(false, 8, default(StandaloneSignatureHandle), reader.GetMemoryBlockAt(0, num), ImmutableArray<ExceptionRegion>.Empty, 1 + num);
			}
			if ((b & 3) != 3)
			{
				throw new BadImageFormatException(SR.Format(SR.InvalidMethodHeader1, b));
			}
			byte b2 = reader.ReadByte();
			if (b2 >> 4 != 3)
			{
				throw new BadImageFormatException(SR.Format(SR.InvalidMethodHeader2, b, b2));
			}
			bool flag = (b & 16) == 16;
			bool flag2 = (b & 8) == 8;
			ushort num2 = reader.ReadUInt16();
			num = reader.ReadInt32();
			int num3 = reader.ReadInt32();
			StandaloneSignatureHandle standaloneSignatureHandle;
			if (num3 == 0)
			{
				standaloneSignatureHandle = default(StandaloneSignatureHandle);
			}
			else
			{
				if (((long)num3 & 2130706432L) != 285212672L)
				{
					throw new BadImageFormatException(SR.Format(SR.InvalidLocalSignatureToken, (uint)num3));
				}
				standaloneSignatureHandle = StandaloneSignatureHandle.FromRowId(num3 & 16777215);
			}
			MemoryBlock memoryBlockAt = reader.GetMemoryBlockAt(0, num);
			reader.Offset += num;
			ImmutableArray<ExceptionRegion> immutableArray;
			if (flag2)
			{
				reader.Align(4);
				byte b3 = reader.ReadByte();
				if ((b3 & 1) != 1)
				{
					throw new BadImageFormatException(SR.Format(SR.InvalidSehHeader, b3));
				}
				bool flag3 = (b3 & 64) == 64;
				int num4 = (int)reader.ReadByte();
				if (flag3)
				{
					num4 += (int)reader.ReadUInt16() << 8;
					immutableArray = MethodBodyBlock.ReadFatExceptionHandlers(ref reader, num4 / 24);
				}
				else
				{
					reader.Offset += 2;
					immutableArray = MethodBodyBlock.ReadSmallExceptionHandlers(ref reader, num4 / 12);
				}
			}
			else
			{
				immutableArray = ImmutableArray<ExceptionRegion>.Empty;
			}
			return new MethodBodyBlock(flag, num2, standaloneSignatureHandle, memoryBlockAt, immutableArray, reader.Offset - offset);
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x00010E48 File Offset: 0x0000F048
		private static ImmutableArray<ExceptionRegion> ReadSmallExceptionHandlers(ref BlobReader memReader, int count)
		{
			ExceptionRegion[] array = new ExceptionRegion[count];
			for (int i = 0; i < array.Length; i++)
			{
				ExceptionRegionKind exceptionRegionKind = (ExceptionRegionKind)memReader.ReadUInt16();
				ushort num = memReader.ReadUInt16();
				byte b = memReader.ReadByte();
				ushort num2 = memReader.ReadUInt16();
				byte b2 = memReader.ReadByte();
				int num3 = memReader.ReadInt32();
				array[i] = new ExceptionRegion(exceptionRegionKind, (int)num, (int)b, (int)num2, (int)b2, num3);
			}
			return ImmutableArray.Create<ExceptionRegion>(array);
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x00010EB4 File Offset: 0x0000F0B4
		private static ImmutableArray<ExceptionRegion> ReadFatExceptionHandlers(ref BlobReader memReader, int count)
		{
			ExceptionRegion[] array = new ExceptionRegion[count];
			for (int i = 0; i < array.Length; i++)
			{
				ExceptionRegionKind exceptionRegionKind = (ExceptionRegionKind)memReader.ReadUInt32();
				int num = memReader.ReadInt32();
				int num2 = memReader.ReadInt32();
				int num3 = memReader.ReadInt32();
				int num4 = memReader.ReadInt32();
				int num5 = memReader.ReadInt32();
				array[i] = new ExceptionRegion(exceptionRegionKind, num, num2, num3, num4, num5);
			}
			return ImmutableArray.Create<ExceptionRegion>(array);
		}

		// Token: 0x0400041D RID: 1053
		private readonly MemoryBlock _il;

		// Token: 0x0400041E RID: 1054
		private readonly int _size;

		// Token: 0x0400041F RID: 1055
		private readonly ushort _maxStack;

		// Token: 0x04000420 RID: 1056
		private readonly bool _localVariablesInitialized;

		// Token: 0x04000421 RID: 1057
		private readonly StandaloneSignatureHandle _localSignature;

		// Token: 0x04000422 RID: 1058
		private readonly ImmutableArray<ExceptionRegion> _exceptionRegions;

		// Token: 0x04000423 RID: 1059
		private const byte ILTinyFormat = 2;

		// Token: 0x04000424 RID: 1060
		private const byte ILFatFormat = 3;

		// Token: 0x04000425 RID: 1061
		private const byte ILFormatMask = 3;

		// Token: 0x04000426 RID: 1062
		private const int ILTinyFormatSizeShift = 2;

		// Token: 0x04000427 RID: 1063
		private const byte ILMoreSects = 8;

		// Token: 0x04000428 RID: 1064
		private const byte ILInitLocals = 16;

		// Token: 0x04000429 RID: 1065
		private const byte ILFatFormatHeaderSize = 3;

		// Token: 0x0400042A RID: 1066
		private const int ILFatFormatHeaderSizeShift = 4;

		// Token: 0x0400042B RID: 1067
		private const byte SectEHTable = 1;

		// Token: 0x0400042C RID: 1068
		private const byte SectFatFormat = 64;
	}
}

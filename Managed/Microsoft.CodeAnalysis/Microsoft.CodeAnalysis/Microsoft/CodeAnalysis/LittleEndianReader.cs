using System;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000194 RID: 404
	internal ref struct LittleEndianReader
	{
		// Token: 0x06001061 RID: 4193 RVA: 0x0002FF5C File Offset: 0x0002E15C
		public LittleEndianReader(ReadOnlySpan<byte> span)
		{
			this._span = span;
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x0002FF65 File Offset: 0x0002E165
		internal uint ReadUInt32()
		{
			uint num = BinaryPrimitives.ReadUInt32LittleEndian(this._span);
			this._span = this._span.Slice(4);
			return num;
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x0002FF84 File Offset: 0x0002E184
		internal unsafe byte ReadByte()
		{
			byte b = *this._span[0];
			this._span = this._span.Slice(1);
			return b;
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x0002FFA5 File Offset: 0x0002E1A5
		internal ushort ReadUInt16()
		{
			ushort num = BinaryPrimitives.ReadUInt16LittleEndian(this._span);
			this._span = this._span.Slice(2);
			return num;
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x0002FFC4 File Offset: 0x0002E1C4
		internal ReadOnlySpan<byte> ReadBytes(int byteCount)
		{
			ReadOnlySpan<byte> readOnlySpan = this._span.Slice(0, byteCount);
			this._span = this._span.Slice(byteCount);
			return readOnlySpan;
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x0002FFE5 File Offset: 0x0002E1E5
		internal int ReadInt32()
		{
			int num = BinaryPrimitives.ReadInt32LittleEndian(this._span);
			this._span = this._span.Slice(4);
			return num;
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x00030004 File Offset: 0x0002E204
		[NullableContext(1)]
		internal byte[] ReadReversed(int byteCount)
		{
			byte[] array = this.ReadBytes(byteCount).ToArray();
			array.ReverseContents<byte>();
			return array;
		}

		// Token: 0x04000506 RID: 1286
		private ReadOnlySpan<byte> _span;
	}
}

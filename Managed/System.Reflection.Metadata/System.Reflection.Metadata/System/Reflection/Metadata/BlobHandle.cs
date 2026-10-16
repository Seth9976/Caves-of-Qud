using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x0200006F RID: 111
	public readonly struct BlobHandle : IEquatable<BlobHandle>
	{
		// Token: 0x06000495 RID: 1173 RVA: 0x0000B3D9 File Offset: 0x000095D9
		private BlobHandle(uint value)
		{
			this._value = value;
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0000B3E2 File Offset: 0x000095E2
		internal static BlobHandle FromOffset(int heapOffset)
		{
			return new BlobHandle((uint)heapOffset);
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0000B3EA File Offset: 0x000095EA
		internal static BlobHandle FromVirtualIndex(BlobHandle.VirtualIndex virtualIndex, ushort virtualValue)
		{
			return new BlobHandle((uint)(int.MinValue | ((int)virtualValue << 8) | (int)virtualIndex));
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0000B3FC File Offset: 0x000095FC
		[NullableContext(1)]
		internal unsafe void SubstituteTemplateParameters(byte[] blob)
		{
			fixed (byte* ptr = &blob[2])
			{
				byte* ptr2 = ptr;
				*(int*)ptr2 = (int)this.VirtualValue;
			}
		}

		/// <param name="handle" />
		// Token: 0x06000499 RID: 1177 RVA: 0x0000B41F File Offset: 0x0000961F
		public static implicit operator Handle(BlobHandle handle)
		{
			return new Handle((byte)(((handle._value & 2147483648U) >> 24) | 113U), (int)(handle._value & 536870911U));
		}

		/// <param name="handle" />
		// Token: 0x0600049A RID: 1178 RVA: 0x0000B445 File Offset: 0x00009645
		public static explicit operator BlobHandle(Handle handle)
		{
			if ((handle.VType & 127) != 113)
			{
				Throw.InvalidCast();
			}
			return new BlobHandle((uint)(((int)(handle.VType & 128) << 24) | handle.Offset));
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x0000B477 File Offset: 0x00009677
		internal uint RawValue
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x0000B47F File Offset: 0x0000967F
		public bool IsNil
		{
			get
			{
				return this._value == 0U;
			}
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0000B48A File Offset: 0x0000968A
		internal int GetHeapOffset()
		{
			return (int)this._value;
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0000B492 File Offset: 0x00009692
		internal BlobHandle.VirtualIndex GetVirtualIndex()
		{
			return (BlobHandle.VirtualIndex)(this._value & 255U);
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x0000B4A1 File Offset: 0x000096A1
		internal bool IsVirtual
		{
			get
			{
				return (this._value & 2147483648U) > 0U;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x0000B4B2 File Offset: 0x000096B2
		private ushort VirtualValue
		{
			get
			{
				return (ushort)(this._value >> 8);
			}
		}

		/// <param name="obj" />
		// Token: 0x060004A1 RID: 1185 RVA: 0x0000B4BD File Offset: 0x000096BD
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is BlobHandle && this.Equals((BlobHandle)obj);
		}

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x060004A2 RID: 1186 RVA: 0x0000B4D5 File Offset: 0x000096D5
		public bool Equals(BlobHandle other)
		{
			return this._value == other._value;
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0000B4E5 File Offset: 0x000096E5
		public override int GetHashCode()
		{
			return (int)this._value;
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x060004A4 RID: 1188 RVA: 0x0000B4ED File Offset: 0x000096ED
		public static bool operator ==(BlobHandle left, BlobHandle right)
		{
			return left.Equals(right);
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x060004A5 RID: 1189 RVA: 0x0000B4F7 File Offset: 0x000096F7
		public static bool operator !=(BlobHandle left, BlobHandle right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0400030A RID: 778
		private readonly uint _value;

		// Token: 0x0400030B RID: 779
		internal const int TemplateParameterOffset_AttributeUsageTarget = 2;

		// Token: 0x0200019A RID: 410
		internal enum VirtualIndex : byte
		{
			// Token: 0x04000A23 RID: 2595
			Nil,
			// Token: 0x04000A24 RID: 2596
			ContractPublicKeyToken,
			// Token: 0x04000A25 RID: 2597
			ContractPublicKey,
			// Token: 0x04000A26 RID: 2598
			AttributeUsage_AllowSingle,
			// Token: 0x04000A27 RID: 2599
			AttributeUsage_AllowMultiple,
			// Token: 0x04000A28 RID: 2600
			Count
		}
	}
}

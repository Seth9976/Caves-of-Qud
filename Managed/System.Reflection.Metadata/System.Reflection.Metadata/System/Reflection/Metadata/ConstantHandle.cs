using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x02000069 RID: 105
	public readonly struct ConstantHandle : IEquatable<ConstantHandle>
	{
		// Token: 0x0600043F RID: 1087 RVA: 0x0000AD92 File Offset: 0x00008F92
		private ConstantHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0000AD9B File Offset: 0x00008F9B
		internal static ConstantHandle FromRowId(int rowId)
		{
			return new ConstantHandle(rowId);
		}

		/// <param name="handle" />
		// Token: 0x06000441 RID: 1089 RVA: 0x0000ADA3 File Offset: 0x00008FA3
		public static implicit operator Handle(ConstantHandle handle)
		{
			return new Handle(11, handle._rowId);
		}

		/// <param name="handle" />
		// Token: 0x06000442 RID: 1090 RVA: 0x0000ADB2 File Offset: 0x00008FB2
		public static implicit operator EntityHandle(ConstantHandle handle)
		{
			return new EntityHandle((uint)(184549376L | (long)handle._rowId));
		}

		/// <param name="handle" />
		// Token: 0x06000443 RID: 1091 RVA: 0x0000ADC8 File Offset: 0x00008FC8
		public static explicit operator ConstantHandle(Handle handle)
		{
			if (handle.VType != 11)
			{
				Throw.InvalidCast();
			}
			return new ConstantHandle(handle.RowId);
		}

		/// <param name="handle" />
		// Token: 0x06000444 RID: 1092 RVA: 0x0000ADE6 File Offset: 0x00008FE6
		public static explicit operator ConstantHandle(EntityHandle handle)
		{
			if (handle.VType != 184549376U)
			{
				Throw.InvalidCast();
			}
			return new ConstantHandle(handle.RowId);
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x0000AE07 File Offset: 0x00009007
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x0000AE12 File Offset: 0x00009012
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x06000447 RID: 1095 RVA: 0x0000AE1A File Offset: 0x0000901A
		public static bool operator ==(ConstantHandle left, ConstantHandle right)
		{
			return left._rowId == right._rowId;
		}

		/// <param name="obj" />
		// Token: 0x06000448 RID: 1096 RVA: 0x0000AE2A File Offset: 0x0000902A
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is ConstantHandle && ((ConstantHandle)obj)._rowId == this._rowId;
		}

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000449 RID: 1097 RVA: 0x0000AE49 File Offset: 0x00009049
		public bool Equals(ConstantHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0000AE5C File Offset: 0x0000905C
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x0600044B RID: 1099 RVA: 0x0000AE77 File Offset: 0x00009077
		public static bool operator !=(ConstantHandle left, ConstantHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x040002FE RID: 766
		private const uint tokenType = 184549376U;

		// Token: 0x040002FF RID: 767
		private const byte tokenTypeSmall = 11;

		// Token: 0x04000300 RID: 768
		private readonly int _rowId;
	}
}

using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x02000068 RID: 104
	public readonly struct DeclarativeSecurityAttributeHandle : IEquatable<DeclarativeSecurityAttributeHandle>
	{
		// Token: 0x06000432 RID: 1074 RVA: 0x0000AC9A File Offset: 0x00008E9A
		private DeclarativeSecurityAttributeHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x0000ACA3 File Offset: 0x00008EA3
		internal static DeclarativeSecurityAttributeHandle FromRowId(int rowId)
		{
			return new DeclarativeSecurityAttributeHandle(rowId);
		}

		/// <param name="handle" />
		// Token: 0x06000434 RID: 1076 RVA: 0x0000ACAB File Offset: 0x00008EAB
		public static implicit operator Handle(DeclarativeSecurityAttributeHandle handle)
		{
			return new Handle(14, handle._rowId);
		}

		/// <param name="handle" />
		// Token: 0x06000435 RID: 1077 RVA: 0x0000ACBA File Offset: 0x00008EBA
		public static implicit operator EntityHandle(DeclarativeSecurityAttributeHandle handle)
		{
			return new EntityHandle((uint)(234881024L | (long)handle._rowId));
		}

		/// <param name="handle" />
		// Token: 0x06000436 RID: 1078 RVA: 0x0000ACD0 File Offset: 0x00008ED0
		public static explicit operator DeclarativeSecurityAttributeHandle(Handle handle)
		{
			if (handle.VType != 14)
			{
				Throw.InvalidCast();
			}
			return new DeclarativeSecurityAttributeHandle(handle.RowId);
		}

		/// <param name="handle" />
		// Token: 0x06000437 RID: 1079 RVA: 0x0000ACEE File Offset: 0x00008EEE
		public static explicit operator DeclarativeSecurityAttributeHandle(EntityHandle handle)
		{
			if (handle.VType != 234881024U)
			{
				Throw.InvalidCast();
			}
			return new DeclarativeSecurityAttributeHandle(handle.RowId);
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x0000AD0F File Offset: 0x00008F0F
		public bool IsNil
		{
			get
			{
				return this._rowId == 0;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x0000AD1A File Offset: 0x00008F1A
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x0600043A RID: 1082 RVA: 0x0000AD22 File Offset: 0x00008F22
		public static bool operator ==(DeclarativeSecurityAttributeHandle left, DeclarativeSecurityAttributeHandle right)
		{
			return left._rowId == right._rowId;
		}

		/// <param name="obj" />
		// Token: 0x0600043B RID: 1083 RVA: 0x0000AD32 File Offset: 0x00008F32
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is DeclarativeSecurityAttributeHandle && ((DeclarativeSecurityAttributeHandle)obj)._rowId == this._rowId;
		}

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600043C RID: 1084 RVA: 0x0000AD51 File Offset: 0x00008F51
		public bool Equals(DeclarativeSecurityAttributeHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0000AD64 File Offset: 0x00008F64
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x0600043E RID: 1086 RVA: 0x0000AD7F File Offset: 0x00008F7F
		public static bool operator !=(DeclarativeSecurityAttributeHandle left, DeclarativeSecurityAttributeHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x040002FB RID: 763
		private const uint tokenType = 234881024U;

		// Token: 0x040002FC RID: 764
		private const byte tokenTypeSmall = 14;

		// Token: 0x040002FD RID: 765
		private readonly int _rowId;
	}
}

using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x0200005D RID: 93
	public readonly struct MemberReferenceHandle : IEquatable<MemberReferenceHandle>
	{
		// Token: 0x0600039F RID: 927 RVA: 0x0000A1CA File Offset: 0x000083CA
		private MemberReferenceHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0000A1D3 File Offset: 0x000083D3
		internal static MemberReferenceHandle FromRowId(int rowId)
		{
			return new MemberReferenceHandle(rowId);
		}

		/// <param name="handle" />
		// Token: 0x060003A1 RID: 929 RVA: 0x0000A1DB File Offset: 0x000083DB
		public static implicit operator Handle(MemberReferenceHandle handle)
		{
			return new Handle(10, handle._rowId);
		}

		/// <param name="handle" />
		// Token: 0x060003A2 RID: 930 RVA: 0x0000A1EA File Offset: 0x000083EA
		public static implicit operator EntityHandle(MemberReferenceHandle handle)
		{
			return new EntityHandle((uint)(167772160L | (long)handle._rowId));
		}

		/// <param name="handle" />
		// Token: 0x060003A3 RID: 931 RVA: 0x0000A200 File Offset: 0x00008400
		public static explicit operator MemberReferenceHandle(Handle handle)
		{
			if (handle.VType != 10)
			{
				Throw.InvalidCast();
			}
			return new MemberReferenceHandle(handle.RowId);
		}

		/// <param name="handle" />
		// Token: 0x060003A4 RID: 932 RVA: 0x0000A21E File Offset: 0x0000841E
		public static explicit operator MemberReferenceHandle(EntityHandle handle)
		{
			if (handle.VType != 167772160U)
			{
				Throw.InvalidCast();
			}
			return new MemberReferenceHandle(handle.RowId);
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x0000A23F File Offset: 0x0000843F
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x0000A24A File Offset: 0x0000844A
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x060003A7 RID: 935 RVA: 0x0000A252 File Offset: 0x00008452
		public static bool operator ==(MemberReferenceHandle left, MemberReferenceHandle right)
		{
			return left._rowId == right._rowId;
		}

		/// <param name="obj" />
		// Token: 0x060003A8 RID: 936 RVA: 0x0000A262 File Offset: 0x00008462
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is MemberReferenceHandle && ((MemberReferenceHandle)obj)._rowId == this._rowId;
		}

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x060003A9 RID: 937 RVA: 0x0000A281 File Offset: 0x00008481
		public bool Equals(MemberReferenceHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000A294 File Offset: 0x00008494
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x060003AB RID: 939 RVA: 0x0000A2AF File Offset: 0x000084AF
		public static bool operator !=(MemberReferenceHandle left, MemberReferenceHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x040002DA RID: 730
		private const uint tokenType = 167772160U;

		// Token: 0x040002DB RID: 731
		private const byte tokenTypeSmall = 10;

		// Token: 0x040002DC RID: 732
		private readonly int _rowId;
	}
}

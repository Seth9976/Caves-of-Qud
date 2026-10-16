using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x02000062 RID: 98
	public readonly struct ParameterHandle : IEquatable<ParameterHandle>
	{
		// Token: 0x060003E0 RID: 992 RVA: 0x0000A69E File Offset: 0x0000889E
		private ParameterHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000A6A7 File Offset: 0x000088A7
		internal static ParameterHandle FromRowId(int rowId)
		{
			return new ParameterHandle(rowId);
		}

		/// <param name="handle" />
		// Token: 0x060003E2 RID: 994 RVA: 0x0000A6AF File Offset: 0x000088AF
		public static implicit operator Handle(ParameterHandle handle)
		{
			return new Handle(8, handle._rowId);
		}

		/// <param name="handle" />
		// Token: 0x060003E3 RID: 995 RVA: 0x0000A6BD File Offset: 0x000088BD
		public static implicit operator EntityHandle(ParameterHandle handle)
		{
			return new EntityHandle((uint)(134217728L | (long)handle._rowId));
		}

		/// <param name="handle" />
		// Token: 0x060003E4 RID: 996 RVA: 0x0000A6D3 File Offset: 0x000088D3
		public static explicit operator ParameterHandle(Handle handle)
		{
			if (handle.VType != 8)
			{
				Throw.InvalidCast();
			}
			return new ParameterHandle(handle.RowId);
		}

		/// <param name="handle" />
		// Token: 0x060003E5 RID: 997 RVA: 0x0000A6F0 File Offset: 0x000088F0
		public static explicit operator ParameterHandle(EntityHandle handle)
		{
			if (handle.VType != 134217728U)
			{
				Throw.InvalidCast();
			}
			return new ParameterHandle(handle.RowId);
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x0000A711 File Offset: 0x00008911
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x0000A71C File Offset: 0x0000891C
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x060003E8 RID: 1000 RVA: 0x0000A724 File Offset: 0x00008924
		public static bool operator ==(ParameterHandle left, ParameterHandle right)
		{
			return left._rowId == right._rowId;
		}

		/// <param name="obj" />
		// Token: 0x060003E9 RID: 1001 RVA: 0x0000A734 File Offset: 0x00008934
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is ParameterHandle && ((ParameterHandle)obj)._rowId == this._rowId;
		}

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x060003EA RID: 1002 RVA: 0x0000A753 File Offset: 0x00008953
		public bool Equals(ParameterHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000A764 File Offset: 0x00008964
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x060003EC RID: 1004 RVA: 0x0000A77F File Offset: 0x0000897F
		public static bool operator !=(ParameterHandle left, ParameterHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x040002E9 RID: 745
		private const uint tokenType = 134217728U;

		// Token: 0x040002EA RID: 746
		private const byte tokenTypeSmall = 8;

		// Token: 0x040002EB RID: 747
		private readonly int _rowId;
	}
}

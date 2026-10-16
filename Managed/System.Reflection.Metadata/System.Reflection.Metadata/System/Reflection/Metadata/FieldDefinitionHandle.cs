using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x0200005E RID: 94
	public readonly struct FieldDefinitionHandle : IEquatable<FieldDefinitionHandle>
	{
		// Token: 0x060003AC RID: 940 RVA: 0x0000A2C2 File Offset: 0x000084C2
		private FieldDefinitionHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000A2CB File Offset: 0x000084CB
		internal static FieldDefinitionHandle FromRowId(int rowId)
		{
			return new FieldDefinitionHandle(rowId);
		}

		/// <param name="handle" />
		// Token: 0x060003AE RID: 942 RVA: 0x0000A2D3 File Offset: 0x000084D3
		public static implicit operator Handle(FieldDefinitionHandle handle)
		{
			return new Handle(4, handle._rowId);
		}

		/// <param name="handle" />
		// Token: 0x060003AF RID: 943 RVA: 0x0000A2E1 File Offset: 0x000084E1
		public static implicit operator EntityHandle(FieldDefinitionHandle handle)
		{
			return new EntityHandle((uint)(67108864L | (long)handle._rowId));
		}

		/// <param name="handle" />
		// Token: 0x060003B0 RID: 944 RVA: 0x0000A2F7 File Offset: 0x000084F7
		public static explicit operator FieldDefinitionHandle(Handle handle)
		{
			if (handle.VType != 4)
			{
				Throw.InvalidCast();
			}
			return new FieldDefinitionHandle(handle.RowId);
		}

		/// <param name="handle" />
		// Token: 0x060003B1 RID: 945 RVA: 0x0000A314 File Offset: 0x00008514
		public static explicit operator FieldDefinitionHandle(EntityHandle handle)
		{
			if (handle.VType != 67108864U)
			{
				Throw.InvalidCast();
			}
			return new FieldDefinitionHandle(handle.RowId);
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x0000A335 File Offset: 0x00008535
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x0000A340 File Offset: 0x00008540
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x060003B4 RID: 948 RVA: 0x0000A348 File Offset: 0x00008548
		public static bool operator ==(FieldDefinitionHandle left, FieldDefinitionHandle right)
		{
			return left._rowId == right._rowId;
		}

		/// <param name="obj" />
		// Token: 0x060003B5 RID: 949 RVA: 0x0000A358 File Offset: 0x00008558
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is FieldDefinitionHandle && ((FieldDefinitionHandle)obj)._rowId == this._rowId;
		}

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x060003B6 RID: 950 RVA: 0x0000A377 File Offset: 0x00008577
		public bool Equals(FieldDefinitionHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000A388 File Offset: 0x00008588
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x060003B8 RID: 952 RVA: 0x0000A3A3 File Offset: 0x000085A3
		public static bool operator !=(FieldDefinitionHandle left, FieldDefinitionHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x040002DD RID: 733
		private const uint tokenType = 67108864U;

		// Token: 0x040002DE RID: 734
		private const byte tokenTypeSmall = 4;

		// Token: 0x040002DF RID: 735
		private readonly int _rowId;
	}
}

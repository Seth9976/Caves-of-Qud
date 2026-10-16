using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x02000064 RID: 100
	public readonly struct GenericParameterConstraintHandle : IEquatable<GenericParameterConstraintHandle>
	{
		// Token: 0x060003FA RID: 1018 RVA: 0x0000A88A File Offset: 0x00008A8A
		private GenericParameterConstraintHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000A893 File Offset: 0x00008A93
		internal static GenericParameterConstraintHandle FromRowId(int rowId)
		{
			return new GenericParameterConstraintHandle(rowId);
		}

		/// <param name="handle" />
		// Token: 0x060003FC RID: 1020 RVA: 0x0000A89B File Offset: 0x00008A9B
		public static implicit operator Handle(GenericParameterConstraintHandle handle)
		{
			return new Handle(44, handle._rowId);
		}

		/// <param name="handle" />
		// Token: 0x060003FD RID: 1021 RVA: 0x0000A8AA File Offset: 0x00008AAA
		public static implicit operator EntityHandle(GenericParameterConstraintHandle handle)
		{
			return new EntityHandle((uint)(738197504L | (long)handle._rowId));
		}

		/// <param name="handle" />
		// Token: 0x060003FE RID: 1022 RVA: 0x0000A8C0 File Offset: 0x00008AC0
		public static explicit operator GenericParameterConstraintHandle(Handle handle)
		{
			if (handle.VType != 44)
			{
				Throw.InvalidCast();
			}
			return new GenericParameterConstraintHandle(handle.RowId);
		}

		/// <param name="handle" />
		// Token: 0x060003FF RID: 1023 RVA: 0x0000A8DE File Offset: 0x00008ADE
		public static explicit operator GenericParameterConstraintHandle(EntityHandle handle)
		{
			if (handle.VType != 738197504U)
			{
				Throw.InvalidCast();
			}
			return new GenericParameterConstraintHandle(handle.RowId);
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x0000A8FF File Offset: 0x00008AFF
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x0000A90A File Offset: 0x00008B0A
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x06000402 RID: 1026 RVA: 0x0000A912 File Offset: 0x00008B12
		public static bool operator ==(GenericParameterConstraintHandle left, GenericParameterConstraintHandle right)
		{
			return left._rowId == right._rowId;
		}

		/// <param name="obj" />
		// Token: 0x06000403 RID: 1027 RVA: 0x0000A922 File Offset: 0x00008B22
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is GenericParameterConstraintHandle && ((GenericParameterConstraintHandle)obj)._rowId == this._rowId;
		}

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000404 RID: 1028 RVA: 0x0000A941 File Offset: 0x00008B41
		public bool Equals(GenericParameterConstraintHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000A954 File Offset: 0x00008B54
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x06000406 RID: 1030 RVA: 0x0000A96F File Offset: 0x00008B6F
		public static bool operator !=(GenericParameterConstraintHandle left, GenericParameterConstraintHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x040002EF RID: 751
		private const uint tokenType = 738197504U;

		// Token: 0x040002F0 RID: 752
		private const byte tokenTypeSmall = 44;

		// Token: 0x040002F1 RID: 753
		private readonly int _rowId;
	}
}

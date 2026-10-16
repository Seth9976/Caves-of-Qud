using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x02000060 RID: 96
	public readonly struct PropertyDefinitionHandle : IEquatable<PropertyDefinitionHandle>
	{
		// Token: 0x060003C6 RID: 966 RVA: 0x0000A4AE File Offset: 0x000086AE
		private PropertyDefinitionHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000A4B7 File Offset: 0x000086B7
		internal static PropertyDefinitionHandle FromRowId(int rowId)
		{
			return new PropertyDefinitionHandle(rowId);
		}

		/// <param name="handle" />
		// Token: 0x060003C8 RID: 968 RVA: 0x0000A4BF File Offset: 0x000086BF
		public static implicit operator Handle(PropertyDefinitionHandle handle)
		{
			return new Handle(23, handle._rowId);
		}

		/// <param name="handle" />
		// Token: 0x060003C9 RID: 969 RVA: 0x0000A4CE File Offset: 0x000086CE
		public static implicit operator EntityHandle(PropertyDefinitionHandle handle)
		{
			return new EntityHandle((uint)(385875968L | (long)handle._rowId));
		}

		/// <param name="handle" />
		// Token: 0x060003CA RID: 970 RVA: 0x0000A4E4 File Offset: 0x000086E4
		public static explicit operator PropertyDefinitionHandle(Handle handle)
		{
			if (handle.VType != 23)
			{
				Throw.InvalidCast();
			}
			return new PropertyDefinitionHandle(handle.RowId);
		}

		/// <param name="handle" />
		// Token: 0x060003CB RID: 971 RVA: 0x0000A502 File Offset: 0x00008702
		public static explicit operator PropertyDefinitionHandle(EntityHandle handle)
		{
			if (handle.VType != 385875968U)
			{
				Throw.InvalidCast();
			}
			return new PropertyDefinitionHandle(handle.RowId);
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060003CC RID: 972 RVA: 0x0000A523 File Offset: 0x00008723
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x060003CD RID: 973 RVA: 0x0000A52E File Offset: 0x0000872E
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x060003CE RID: 974 RVA: 0x0000A536 File Offset: 0x00008736
		public static bool operator ==(PropertyDefinitionHandle left, PropertyDefinitionHandle right)
		{
			return left._rowId == right._rowId;
		}

		/// <param name="obj" />
		// Token: 0x060003CF RID: 975 RVA: 0x0000A546 File Offset: 0x00008746
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is PropertyDefinitionHandle && ((PropertyDefinitionHandle)obj)._rowId == this._rowId;
		}

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x060003D0 RID: 976 RVA: 0x0000A565 File Offset: 0x00008765
		public bool Equals(PropertyDefinitionHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000A578 File Offset: 0x00008778
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x060003D2 RID: 978 RVA: 0x0000A593 File Offset: 0x00008793
		public static bool operator !=(PropertyDefinitionHandle left, PropertyDefinitionHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x040002E3 RID: 739
		private const uint tokenType = 385875968U;

		// Token: 0x040002E4 RID: 740
		private const byte tokenTypeSmall = 23;

		// Token: 0x040002E5 RID: 741
		private readonly int _rowId;
	}
}

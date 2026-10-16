using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x02000067 RID: 103
	public readonly struct CustomAttributeHandle : IEquatable<CustomAttributeHandle>
	{
		// Token: 0x06000425 RID: 1061 RVA: 0x0000ABA2 File Offset: 0x00008DA2
		private CustomAttributeHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000ABAB File Offset: 0x00008DAB
		internal static CustomAttributeHandle FromRowId(int rowId)
		{
			return new CustomAttributeHandle(rowId);
		}

		/// <param name="handle" />
		// Token: 0x06000427 RID: 1063 RVA: 0x0000ABB3 File Offset: 0x00008DB3
		public static implicit operator Handle(CustomAttributeHandle handle)
		{
			return new Handle(12, handle._rowId);
		}

		/// <param name="handle" />
		// Token: 0x06000428 RID: 1064 RVA: 0x0000ABC2 File Offset: 0x00008DC2
		public static implicit operator EntityHandle(CustomAttributeHandle handle)
		{
			return new EntityHandle((uint)(201326592L | (long)handle._rowId));
		}

		/// <param name="handle" />
		// Token: 0x06000429 RID: 1065 RVA: 0x0000ABD8 File Offset: 0x00008DD8
		public static explicit operator CustomAttributeHandle(Handle handle)
		{
			if (handle.VType != 12)
			{
				Throw.InvalidCast();
			}
			return new CustomAttributeHandle(handle.RowId);
		}

		/// <param name="handle" />
		// Token: 0x0600042A RID: 1066 RVA: 0x0000ABF6 File Offset: 0x00008DF6
		public static explicit operator CustomAttributeHandle(EntityHandle handle)
		{
			if (handle.VType != 201326592U)
			{
				Throw.InvalidCast();
			}
			return new CustomAttributeHandle(handle.RowId);
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x0000AC17 File Offset: 0x00008E17
		public bool IsNil
		{
			get
			{
				return this._rowId == 0;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x0000AC22 File Offset: 0x00008E22
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x0600042D RID: 1069 RVA: 0x0000AC2A File Offset: 0x00008E2A
		public static bool operator ==(CustomAttributeHandle left, CustomAttributeHandle right)
		{
			return left._rowId == right._rowId;
		}

		/// <param name="obj" />
		// Token: 0x0600042E RID: 1070 RVA: 0x0000AC3A File Offset: 0x00008E3A
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is CustomAttributeHandle && ((CustomAttributeHandle)obj)._rowId == this._rowId;
		}

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600042F RID: 1071 RVA: 0x0000AC59 File Offset: 0x00008E59
		public bool Equals(CustomAttributeHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000AC6C File Offset: 0x00008E6C
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x06000431 RID: 1073 RVA: 0x0000AC87 File Offset: 0x00008E87
		public static bool operator !=(CustomAttributeHandle left, CustomAttributeHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x040002F8 RID: 760
		private const uint tokenType = 201326592U;

		// Token: 0x040002F9 RID: 761
		private const byte tokenTypeSmall = 12;

		// Token: 0x040002FA RID: 762
		private readonly int _rowId;
	}
}

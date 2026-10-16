using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x0200005C RID: 92
	public readonly struct TypeSpecificationHandle : IEquatable<TypeSpecificationHandle>
	{
		// Token: 0x06000392 RID: 914 RVA: 0x0000A0D2 File Offset: 0x000082D2
		private TypeSpecificationHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000A0DB File Offset: 0x000082DB
		internal static TypeSpecificationHandle FromRowId(int rowId)
		{
			return new TypeSpecificationHandle(rowId);
		}

		/// <param name="handle" />
		// Token: 0x06000394 RID: 916 RVA: 0x0000A0E3 File Offset: 0x000082E3
		public static implicit operator Handle(TypeSpecificationHandle handle)
		{
			return new Handle(27, handle._rowId);
		}

		/// <param name="handle" />
		// Token: 0x06000395 RID: 917 RVA: 0x0000A0F2 File Offset: 0x000082F2
		public static implicit operator EntityHandle(TypeSpecificationHandle handle)
		{
			return new EntityHandle((uint)(452984832L | (long)handle._rowId));
		}

		/// <param name="handle" />
		// Token: 0x06000396 RID: 918 RVA: 0x0000A108 File Offset: 0x00008308
		public static explicit operator TypeSpecificationHandle(Handle handle)
		{
			if (handle.VType != 27)
			{
				Throw.InvalidCast();
			}
			return new TypeSpecificationHandle(handle.RowId);
		}

		/// <param name="handle" />
		// Token: 0x06000397 RID: 919 RVA: 0x0000A126 File Offset: 0x00008326
		public static explicit operator TypeSpecificationHandle(EntityHandle handle)
		{
			if (handle.VType != 452984832U)
			{
				Throw.InvalidCast();
			}
			return new TypeSpecificationHandle(handle.RowId);
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000398 RID: 920 RVA: 0x0000A147 File Offset: 0x00008347
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000399 RID: 921 RVA: 0x0000A152 File Offset: 0x00008352
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x0600039A RID: 922 RVA: 0x0000A15A File Offset: 0x0000835A
		public static bool operator ==(TypeSpecificationHandle left, TypeSpecificationHandle right)
		{
			return left._rowId == right._rowId;
		}

		/// <param name="obj" />
		// Token: 0x0600039B RID: 923 RVA: 0x0000A16A File Offset: 0x0000836A
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is TypeSpecificationHandle && ((TypeSpecificationHandle)obj)._rowId == this._rowId;
		}

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600039C RID: 924 RVA: 0x0000A189 File Offset: 0x00008389
		public bool Equals(TypeSpecificationHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x0600039D RID: 925 RVA: 0x0000A19C File Offset: 0x0000839C
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x0600039E RID: 926 RVA: 0x0000A1B7 File Offset: 0x000083B7
		public static bool operator !=(TypeSpecificationHandle left, TypeSpecificationHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x040002D7 RID: 727
		private const uint tokenType = 452984832U;

		// Token: 0x040002D8 RID: 728
		private const byte tokenTypeSmall = 27;

		// Token: 0x040002D9 RID: 729
		private readonly int _rowId;
	}
}

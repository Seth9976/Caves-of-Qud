using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x0200005B RID: 91
	public readonly struct TypeReferenceHandle : IEquatable<TypeReferenceHandle>
	{
		// Token: 0x06000385 RID: 901 RVA: 0x00009FDE File Offset: 0x000081DE
		private TypeReferenceHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00009FE7 File Offset: 0x000081E7
		internal static TypeReferenceHandle FromRowId(int rowId)
		{
			return new TypeReferenceHandle(rowId);
		}

		/// <param name="handle" />
		// Token: 0x06000387 RID: 903 RVA: 0x00009FEF File Offset: 0x000081EF
		public static implicit operator Handle(TypeReferenceHandle handle)
		{
			return new Handle(1, handle._rowId);
		}

		/// <param name="handle" />
		// Token: 0x06000388 RID: 904 RVA: 0x00009FFD File Offset: 0x000081FD
		public static implicit operator EntityHandle(TypeReferenceHandle handle)
		{
			return new EntityHandle((uint)(16777216L | (long)handle._rowId));
		}

		/// <param name="handle" />
		// Token: 0x06000389 RID: 905 RVA: 0x0000A013 File Offset: 0x00008213
		public static explicit operator TypeReferenceHandle(Handle handle)
		{
			if (handle.VType != 1)
			{
				Throw.InvalidCast();
			}
			return new TypeReferenceHandle(handle.RowId);
		}

		/// <param name="handle" />
		// Token: 0x0600038A RID: 906 RVA: 0x0000A030 File Offset: 0x00008230
		public static explicit operator TypeReferenceHandle(EntityHandle handle)
		{
			if (handle.VType != 16777216U)
			{
				Throw.InvalidCast();
			}
			return new TypeReferenceHandle(handle.RowId);
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600038B RID: 907 RVA: 0x0000A051 File Offset: 0x00008251
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x0600038C RID: 908 RVA: 0x0000A05C File Offset: 0x0000825C
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x0600038D RID: 909 RVA: 0x0000A064 File Offset: 0x00008264
		public static bool operator ==(TypeReferenceHandle left, TypeReferenceHandle right)
		{
			return left._rowId == right._rowId;
		}

		/// <param name="obj" />
		// Token: 0x0600038E RID: 910 RVA: 0x0000A074 File Offset: 0x00008274
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is TypeReferenceHandle && ((TypeReferenceHandle)obj)._rowId == this._rowId;
		}

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600038F RID: 911 RVA: 0x0000A093 File Offset: 0x00008293
		public bool Equals(TypeReferenceHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000A0A4 File Offset: 0x000082A4
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x06000391 RID: 913 RVA: 0x0000A0BF File Offset: 0x000082BF
		public static bool operator !=(TypeReferenceHandle left, TypeReferenceHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x040002D4 RID: 724
		private const uint tokenType = 16777216U;

		// Token: 0x040002D5 RID: 725
		private const byte tokenTypeSmall = 1;

		// Token: 0x040002D6 RID: 726
		private readonly int _rowId;
	}
}

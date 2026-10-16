using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x02000063 RID: 99
	public readonly struct GenericParameterHandle : IEquatable<GenericParameterHandle>
	{
		// Token: 0x060003ED RID: 1005 RVA: 0x0000A792 File Offset: 0x00008992
		private GenericParameterHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0000A79B File Offset: 0x0000899B
		internal static GenericParameterHandle FromRowId(int rowId)
		{
			return new GenericParameterHandle(rowId);
		}

		/// <param name="handle" />
		// Token: 0x060003EF RID: 1007 RVA: 0x0000A7A3 File Offset: 0x000089A3
		public static implicit operator Handle(GenericParameterHandle handle)
		{
			return new Handle(42, handle._rowId);
		}

		/// <param name="handle" />
		// Token: 0x060003F0 RID: 1008 RVA: 0x0000A7B2 File Offset: 0x000089B2
		public static implicit operator EntityHandle(GenericParameterHandle handle)
		{
			return new EntityHandle((uint)(704643072L | (long)handle._rowId));
		}

		/// <param name="handle" />
		// Token: 0x060003F1 RID: 1009 RVA: 0x0000A7C8 File Offset: 0x000089C8
		public static explicit operator GenericParameterHandle(Handle handle)
		{
			if (handle.VType != 42)
			{
				Throw.InvalidCast();
			}
			return new GenericParameterHandle(handle.RowId);
		}

		/// <param name="handle" />
		// Token: 0x060003F2 RID: 1010 RVA: 0x0000A7E6 File Offset: 0x000089E6
		public static explicit operator GenericParameterHandle(EntityHandle handle)
		{
			if (handle.VType != 704643072U)
			{
				Throw.InvalidCast();
			}
			return new GenericParameterHandle(handle.RowId);
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x0000A807 File Offset: 0x00008A07
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x0000A812 File Offset: 0x00008A12
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x060003F5 RID: 1013 RVA: 0x0000A81A File Offset: 0x00008A1A
		public static bool operator ==(GenericParameterHandle left, GenericParameterHandle right)
		{
			return left._rowId == right._rowId;
		}

		/// <param name="obj" />
		// Token: 0x060003F6 RID: 1014 RVA: 0x0000A82A File Offset: 0x00008A2A
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is GenericParameterHandle && ((GenericParameterHandle)obj)._rowId == this._rowId;
		}

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x060003F7 RID: 1015 RVA: 0x0000A849 File Offset: 0x00008A49
		public bool Equals(GenericParameterHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000A85C File Offset: 0x00008A5C
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x060003F9 RID: 1017 RVA: 0x0000A877 File Offset: 0x00008A77
		public static bool operator !=(GenericParameterHandle left, GenericParameterHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x040002EC RID: 748
		private const uint tokenType = 704643072U;

		// Token: 0x040002ED RID: 749
		private const byte tokenTypeSmall = 42;

		// Token: 0x040002EE RID: 750
		private readonly int _rowId;
	}
}

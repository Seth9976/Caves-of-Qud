using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x02000057 RID: 87
	public readonly struct MethodImplementationHandle : IEquatable<MethodImplementationHandle>
	{
		// Token: 0x06000351 RID: 849 RVA: 0x00009C03 File Offset: 0x00007E03
		private MethodImplementationHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00009C0C File Offset: 0x00007E0C
		internal static MethodImplementationHandle FromRowId(int rowId)
		{
			return new MethodImplementationHandle(rowId);
		}

		/// <param name="handle" />
		// Token: 0x06000353 RID: 851 RVA: 0x00009C14 File Offset: 0x00007E14
		public static implicit operator Handle(MethodImplementationHandle handle)
		{
			return new Handle(25, handle._rowId);
		}

		/// <param name="handle" />
		// Token: 0x06000354 RID: 852 RVA: 0x00009C23 File Offset: 0x00007E23
		public static implicit operator EntityHandle(MethodImplementationHandle handle)
		{
			return new EntityHandle((uint)(419430400L | (long)handle._rowId));
		}

		/// <param name="handle" />
		// Token: 0x06000355 RID: 853 RVA: 0x00009C39 File Offset: 0x00007E39
		public static explicit operator MethodImplementationHandle(Handle handle)
		{
			if (handle.VType != 25)
			{
				Throw.InvalidCast();
			}
			return new MethodImplementationHandle(handle.RowId);
		}

		/// <param name="handle" />
		// Token: 0x06000356 RID: 854 RVA: 0x00009C57 File Offset: 0x00007E57
		public static explicit operator MethodImplementationHandle(EntityHandle handle)
		{
			if (handle.VType != 419430400U)
			{
				Throw.InvalidCast();
			}
			return new MethodImplementationHandle(handle.RowId);
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000357 RID: 855 RVA: 0x00009C78 File Offset: 0x00007E78
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000358 RID: 856 RVA: 0x00009C83 File Offset: 0x00007E83
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x06000359 RID: 857 RVA: 0x00009C8B File Offset: 0x00007E8B
		public static bool operator ==(MethodImplementationHandle left, MethodImplementationHandle right)
		{
			return left._rowId == right._rowId;
		}

		/// <param name="obj" />
		// Token: 0x0600035A RID: 858 RVA: 0x00009C9B File Offset: 0x00007E9B
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is MethodImplementationHandle && ((MethodImplementationHandle)obj)._rowId == this._rowId;
		}

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600035B RID: 859 RVA: 0x00009CBA File Offset: 0x00007EBA
		public bool Equals(MethodImplementationHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00009CCC File Offset: 0x00007ECC
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x0600035D RID: 861 RVA: 0x00009CE7 File Offset: 0x00007EE7
		public static bool operator !=(MethodImplementationHandle left, MethodImplementationHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x040002C8 RID: 712
		private const uint tokenType = 419430400U;

		// Token: 0x040002C9 RID: 713
		private const byte tokenTypeSmall = 25;

		// Token: 0x040002CA RID: 714
		private readonly int _rowId;
	}
}

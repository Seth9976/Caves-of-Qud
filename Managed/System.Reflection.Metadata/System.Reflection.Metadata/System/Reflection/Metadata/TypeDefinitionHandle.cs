using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x02000059 RID: 89
	public readonly struct TypeDefinitionHandle : IEquatable<TypeDefinitionHandle>
	{
		// Token: 0x0600036B RID: 875 RVA: 0x00009DF2 File Offset: 0x00007FF2
		private TypeDefinitionHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00009DFB File Offset: 0x00007FFB
		internal static TypeDefinitionHandle FromRowId(int rowId)
		{
			return new TypeDefinitionHandle(rowId);
		}

		/// <param name="handle" />
		// Token: 0x0600036D RID: 877 RVA: 0x00009E03 File Offset: 0x00008003
		public static implicit operator Handle(TypeDefinitionHandle handle)
		{
			return new Handle(2, handle._rowId);
		}

		/// <param name="handle" />
		// Token: 0x0600036E RID: 878 RVA: 0x00009E11 File Offset: 0x00008011
		public static implicit operator EntityHandle(TypeDefinitionHandle handle)
		{
			return new EntityHandle((uint)(33554432L | (long)handle._rowId));
		}

		/// <param name="handle" />
		// Token: 0x0600036F RID: 879 RVA: 0x00009E27 File Offset: 0x00008027
		public static explicit operator TypeDefinitionHandle(Handle handle)
		{
			if (handle.VType != 2)
			{
				Throw.InvalidCast();
			}
			return new TypeDefinitionHandle(handle.RowId);
		}

		/// <param name="handle" />
		// Token: 0x06000370 RID: 880 RVA: 0x00009E44 File Offset: 0x00008044
		public static explicit operator TypeDefinitionHandle(EntityHandle handle)
		{
			if (handle.VType != 33554432U)
			{
				Throw.InvalidCast();
			}
			return new TypeDefinitionHandle(handle.RowId);
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000371 RID: 881 RVA: 0x00009E65 File Offset: 0x00008065
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000372 RID: 882 RVA: 0x00009E70 File Offset: 0x00008070
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x06000373 RID: 883 RVA: 0x00009E78 File Offset: 0x00008078
		public static bool operator ==(TypeDefinitionHandle left, TypeDefinitionHandle right)
		{
			return left._rowId == right._rowId;
		}

		/// <param name="obj" />
		// Token: 0x06000374 RID: 884 RVA: 0x00009E88 File Offset: 0x00008088
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is TypeDefinitionHandle && ((TypeDefinitionHandle)obj)._rowId == this._rowId;
		}

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000375 RID: 885 RVA: 0x00009EA7 File Offset: 0x000080A7
		public bool Equals(TypeDefinitionHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00009EB8 File Offset: 0x000080B8
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x06000377 RID: 887 RVA: 0x00009ED3 File Offset: 0x000080D3
		public static bool operator !=(TypeDefinitionHandle left, TypeDefinitionHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x040002CE RID: 718
		private const uint tokenType = 33554432U;

		// Token: 0x040002CF RID: 719
		private const byte tokenTypeSmall = 2;

		// Token: 0x040002D0 RID: 720
		private readonly int _rowId;
	}
}

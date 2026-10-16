using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x020000C3 RID: 195
	public readonly struct ImportScopeHandle : IEquatable<ImportScopeHandle>
	{
		// Token: 0x0600075B RID: 1883 RVA: 0x00011F2E File Offset: 0x0001012E
		private ImportScopeHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x00011F37 File Offset: 0x00010137
		internal static ImportScopeHandle FromRowId(int rowId)
		{
			return new ImportScopeHandle(rowId);
		}

		/// <param name="handle" />
		// Token: 0x0600075D RID: 1885 RVA: 0x00011F3F File Offset: 0x0001013F
		public static implicit operator Handle(ImportScopeHandle handle)
		{
			return new Handle(53, handle._rowId);
		}

		/// <param name="handle" />
		// Token: 0x0600075E RID: 1886 RVA: 0x00011F4E File Offset: 0x0001014E
		public static implicit operator EntityHandle(ImportScopeHandle handle)
		{
			return new EntityHandle((uint)(889192448L | (long)handle._rowId));
		}

		/// <param name="handle" />
		// Token: 0x0600075F RID: 1887 RVA: 0x00011F64 File Offset: 0x00010164
		public static explicit operator ImportScopeHandle(Handle handle)
		{
			if (handle.VType != 53)
			{
				Throw.InvalidCast();
			}
			return new ImportScopeHandle(handle.RowId);
		}

		/// <param name="handle" />
		// Token: 0x06000760 RID: 1888 RVA: 0x00011F82 File Offset: 0x00010182
		public static explicit operator ImportScopeHandle(EntityHandle handle)
		{
			if (handle.VType != 889192448U)
			{
				Throw.InvalidCast();
			}
			return new ImportScopeHandle(handle.RowId);
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x00011FA3 File Offset: 0x000101A3
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000762 RID: 1890 RVA: 0x00011FAE File Offset: 0x000101AE
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x06000763 RID: 1891 RVA: 0x00011FB6 File Offset: 0x000101B6
		public static bool operator ==(ImportScopeHandle left, ImportScopeHandle right)
		{
			return left._rowId == right._rowId;
		}

		/// <param name="obj" />
		// Token: 0x06000764 RID: 1892 RVA: 0x00011FC6 File Offset: 0x000101C6
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is ImportScopeHandle && ((ImportScopeHandle)obj)._rowId == this._rowId;
		}

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000765 RID: 1893 RVA: 0x00011FE5 File Offset: 0x000101E5
		public bool Equals(ImportScopeHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x00011FF8 File Offset: 0x000101F8
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x06000767 RID: 1895 RVA: 0x00012013 File Offset: 0x00010213
		public static bool operator !=(ImportScopeHandle left, ImportScopeHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x04000468 RID: 1128
		private const uint tokenType = 889192448U;

		// Token: 0x04000469 RID: 1129
		private const byte tokenTypeSmall = 53;

		// Token: 0x0400046A RID: 1130
		private readonly int _rowId;
	}
}

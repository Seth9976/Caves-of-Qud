using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x020000C0 RID: 192
	public readonly struct LocalScopeHandle : IEquatable<LocalScopeHandle>
	{
		// Token: 0x06000734 RID: 1844 RVA: 0x00011C47 File Offset: 0x0000FE47
		private LocalScopeHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x00011C50 File Offset: 0x0000FE50
		internal static LocalScopeHandle FromRowId(int rowId)
		{
			return new LocalScopeHandle(rowId);
		}

		/// <param name="handle" />
		// Token: 0x06000736 RID: 1846 RVA: 0x00011C58 File Offset: 0x0000FE58
		public static implicit operator Handle(LocalScopeHandle handle)
		{
			return new Handle(50, handle._rowId);
		}

		/// <param name="handle" />
		// Token: 0x06000737 RID: 1847 RVA: 0x00011C67 File Offset: 0x0000FE67
		public static implicit operator EntityHandle(LocalScopeHandle handle)
		{
			return new EntityHandle((uint)(838860800L | (long)handle._rowId));
		}

		/// <param name="handle" />
		// Token: 0x06000738 RID: 1848 RVA: 0x00011C7D File Offset: 0x0000FE7D
		public static explicit operator LocalScopeHandle(Handle handle)
		{
			if (handle.VType != 50)
			{
				Throw.InvalidCast();
			}
			return new LocalScopeHandle(handle.RowId);
		}

		/// <param name="handle" />
		// Token: 0x06000739 RID: 1849 RVA: 0x00011C9B File Offset: 0x0000FE9B
		public static explicit operator LocalScopeHandle(EntityHandle handle)
		{
			if (handle.VType != 838860800U)
			{
				Throw.InvalidCast();
			}
			return new LocalScopeHandle(handle.RowId);
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x0600073A RID: 1850 RVA: 0x00011CBC File Offset: 0x0000FEBC
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x00011CC7 File Offset: 0x0000FEC7
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x0600073C RID: 1852 RVA: 0x00011CCF File Offset: 0x0000FECF
		public static bool operator ==(LocalScopeHandle left, LocalScopeHandle right)
		{
			return left._rowId == right._rowId;
		}

		/// <param name="obj" />
		// Token: 0x0600073D RID: 1853 RVA: 0x00011CDF File Offset: 0x0000FEDF
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is LocalScopeHandle && ((LocalScopeHandle)obj)._rowId == this._rowId;
		}

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600073E RID: 1854 RVA: 0x00011CFE File Offset: 0x0000FEFE
		public bool Equals(LocalScopeHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x00011D10 File Offset: 0x0000FF10
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x06000740 RID: 1856 RVA: 0x00011D2B File Offset: 0x0000FF2B
		public static bool operator !=(LocalScopeHandle left, LocalScopeHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x0400045F RID: 1119
		private const uint tokenType = 838860800U;

		// Token: 0x04000460 RID: 1120
		private const byte tokenTypeSmall = 50;

		// Token: 0x04000461 RID: 1121
		private readonly int _rowId;
	}
}

using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x020000BE RID: 190
	public readonly struct DocumentHandle : IEquatable<DocumentHandle>
	{
		// Token: 0x06000719 RID: 1817 RVA: 0x00011A4C File Offset: 0x0000FC4C
		private DocumentHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x00011A55 File Offset: 0x0000FC55
		internal static DocumentHandle FromRowId(int rowId)
		{
			return new DocumentHandle(rowId);
		}

		/// <param name="handle" />
		// Token: 0x0600071B RID: 1819 RVA: 0x00011A5D File Offset: 0x0000FC5D
		public static implicit operator Handle(DocumentHandle handle)
		{
			return new Handle(48, handle._rowId);
		}

		/// <param name="handle" />
		// Token: 0x0600071C RID: 1820 RVA: 0x00011A6C File Offset: 0x0000FC6C
		public static implicit operator EntityHandle(DocumentHandle handle)
		{
			return new EntityHandle((uint)(805306368L | (long)handle._rowId));
		}

		/// <param name="handle" />
		// Token: 0x0600071D RID: 1821 RVA: 0x00011A82 File Offset: 0x0000FC82
		public static explicit operator DocumentHandle(Handle handle)
		{
			if (handle.VType != 48)
			{
				Throw.InvalidCast();
			}
			return new DocumentHandle(handle.RowId);
		}

		/// <param name="handle" />
		// Token: 0x0600071E RID: 1822 RVA: 0x00011AA0 File Offset: 0x0000FCA0
		public static explicit operator DocumentHandle(EntityHandle handle)
		{
			if (handle.VType != 805306368U)
			{
				Throw.InvalidCast();
			}
			return new DocumentHandle(handle.RowId);
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x0600071F RID: 1823 RVA: 0x00011AC1 File Offset: 0x0000FCC1
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x00011ACC File Offset: 0x0000FCCC
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x06000721 RID: 1825 RVA: 0x00011AD4 File Offset: 0x0000FCD4
		public static bool operator ==(DocumentHandle left, DocumentHandle right)
		{
			return left._rowId == right._rowId;
		}

		/// <param name="obj" />
		// Token: 0x06000722 RID: 1826 RVA: 0x00011AE4 File Offset: 0x0000FCE4
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is DocumentHandle && ((DocumentHandle)obj)._rowId == this._rowId;
		}

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000723 RID: 1827 RVA: 0x00011B03 File Offset: 0x0000FD03
		public bool Equals(DocumentHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00011B14 File Offset: 0x0000FD14
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x06000725 RID: 1829 RVA: 0x00011B2F File Offset: 0x0000FD2F
		public static bool operator !=(DocumentHandle left, DocumentHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x04000459 RID: 1113
		private const uint tokenType = 805306368U;

		// Token: 0x0400045A RID: 1114
		private const byte tokenTypeSmall = 48;

		// Token: 0x0400045B RID: 1115
		private readonly int _rowId;
	}
}

using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x0200005A RID: 90
	public readonly struct ExportedTypeHandle : IEquatable<ExportedTypeHandle>
	{
		// Token: 0x06000378 RID: 888 RVA: 0x00009EE6 File Offset: 0x000080E6
		private ExportedTypeHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00009EEF File Offset: 0x000080EF
		internal static ExportedTypeHandle FromRowId(int rowId)
		{
			return new ExportedTypeHandle(rowId);
		}

		/// <param name="handle" />
		// Token: 0x0600037A RID: 890 RVA: 0x00009EF7 File Offset: 0x000080F7
		public static implicit operator Handle(ExportedTypeHandle handle)
		{
			return new Handle(39, handle._rowId);
		}

		/// <param name="handle" />
		// Token: 0x0600037B RID: 891 RVA: 0x00009F06 File Offset: 0x00008106
		public static implicit operator EntityHandle(ExportedTypeHandle handle)
		{
			return new EntityHandle((uint)(654311424L | (long)handle._rowId));
		}

		/// <param name="handle" />
		// Token: 0x0600037C RID: 892 RVA: 0x00009F1C File Offset: 0x0000811C
		public static explicit operator ExportedTypeHandle(Handle handle)
		{
			if (handle.VType != 39)
			{
				Throw.InvalidCast();
			}
			return new ExportedTypeHandle(handle.RowId);
		}

		/// <param name="handle" />
		// Token: 0x0600037D RID: 893 RVA: 0x00009F3A File Offset: 0x0000813A
		public static explicit operator ExportedTypeHandle(EntityHandle handle)
		{
			if (handle.VType != 654311424U)
			{
				Throw.InvalidCast();
			}
			return new ExportedTypeHandle(handle.RowId);
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600037E RID: 894 RVA: 0x00009F5B File Offset: 0x0000815B
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600037F RID: 895 RVA: 0x00009F66 File Offset: 0x00008166
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x06000380 RID: 896 RVA: 0x00009F6E File Offset: 0x0000816E
		public static bool operator ==(ExportedTypeHandle left, ExportedTypeHandle right)
		{
			return left._rowId == right._rowId;
		}

		/// <param name="obj" />
		// Token: 0x06000381 RID: 897 RVA: 0x00009F7E File Offset: 0x0000817E
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is ExportedTypeHandle && ((ExportedTypeHandle)obj)._rowId == this._rowId;
		}

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000382 RID: 898 RVA: 0x00009F9D File Offset: 0x0000819D
		public bool Equals(ExportedTypeHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00009FB0 File Offset: 0x000081B0
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x06000384 RID: 900 RVA: 0x00009FCB File Offset: 0x000081CB
		public static bool operator !=(ExportedTypeHandle left, ExportedTypeHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x040002D1 RID: 721
		private const uint tokenType = 654311424U;

		// Token: 0x040002D2 RID: 722
		private const byte tokenTypeSmall = 39;

		// Token: 0x040002D3 RID: 723
		private readonly int _rowId;
	}
}

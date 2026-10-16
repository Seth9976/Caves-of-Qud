using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x02000058 RID: 88
	public readonly struct MethodSpecificationHandle : IEquatable<MethodSpecificationHandle>
	{
		// Token: 0x0600035E RID: 862 RVA: 0x00009CFA File Offset: 0x00007EFA
		private MethodSpecificationHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00009D03 File Offset: 0x00007F03
		internal static MethodSpecificationHandle FromRowId(int rowId)
		{
			return new MethodSpecificationHandle(rowId);
		}

		/// <param name="handle" />
		// Token: 0x06000360 RID: 864 RVA: 0x00009D0B File Offset: 0x00007F0B
		public static implicit operator Handle(MethodSpecificationHandle handle)
		{
			return new Handle(43, handle._rowId);
		}

		/// <param name="handle" />
		// Token: 0x06000361 RID: 865 RVA: 0x00009D1A File Offset: 0x00007F1A
		public static implicit operator EntityHandle(MethodSpecificationHandle handle)
		{
			return new EntityHandle((uint)(721420288L | (long)handle._rowId));
		}

		/// <param name="handle" />
		// Token: 0x06000362 RID: 866 RVA: 0x00009D30 File Offset: 0x00007F30
		public static explicit operator MethodSpecificationHandle(Handle handle)
		{
			if (handle.VType != 43)
			{
				Throw.InvalidCast();
			}
			return new MethodSpecificationHandle(handle.RowId);
		}

		/// <param name="handle" />
		// Token: 0x06000363 RID: 867 RVA: 0x00009D4E File Offset: 0x00007F4E
		public static explicit operator MethodSpecificationHandle(EntityHandle handle)
		{
			if (handle.VType != 721420288U)
			{
				Throw.InvalidCast();
			}
			return new MethodSpecificationHandle(handle.RowId);
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000364 RID: 868 RVA: 0x00009D6F File Offset: 0x00007F6F
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000365 RID: 869 RVA: 0x00009D7A File Offset: 0x00007F7A
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x06000366 RID: 870 RVA: 0x00009D82 File Offset: 0x00007F82
		public static bool operator ==(MethodSpecificationHandle left, MethodSpecificationHandle right)
		{
			return left._rowId == right._rowId;
		}

		/// <param name="obj" />
		// Token: 0x06000367 RID: 871 RVA: 0x00009D92 File Offset: 0x00007F92
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is MethodSpecificationHandle && ((MethodSpecificationHandle)obj)._rowId == this._rowId;
		}

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000368 RID: 872 RVA: 0x00009DB1 File Offset: 0x00007FB1
		public bool Equals(MethodSpecificationHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00009DC4 File Offset: 0x00007FC4
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x0600036A RID: 874 RVA: 0x00009DDF File Offset: 0x00007FDF
		public static bool operator !=(MethodSpecificationHandle left, MethodSpecificationHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x040002CB RID: 715
		private const uint tokenType = 721420288U;

		// Token: 0x040002CC RID: 716
		private const byte tokenTypeSmall = 43;

		// Token: 0x040002CD RID: 717
		private readonly int _rowId;
	}
}

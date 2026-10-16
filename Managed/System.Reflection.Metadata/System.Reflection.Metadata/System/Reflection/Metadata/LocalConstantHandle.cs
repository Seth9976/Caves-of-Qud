using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x020000C2 RID: 194
	public readonly struct LocalConstantHandle : IEquatable<LocalConstantHandle>
	{
		// Token: 0x0600074E RID: 1870 RVA: 0x00011E36 File Offset: 0x00010036
		private LocalConstantHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x00011E3F File Offset: 0x0001003F
		internal static LocalConstantHandle FromRowId(int rowId)
		{
			return new LocalConstantHandle(rowId);
		}

		/// <param name="handle" />
		// Token: 0x06000750 RID: 1872 RVA: 0x00011E47 File Offset: 0x00010047
		public static implicit operator Handle(LocalConstantHandle handle)
		{
			return new Handle(52, handle._rowId);
		}

		/// <param name="handle" />
		// Token: 0x06000751 RID: 1873 RVA: 0x00011E56 File Offset: 0x00010056
		public static implicit operator EntityHandle(LocalConstantHandle handle)
		{
			return new EntityHandle((uint)(872415232L | (long)handle._rowId));
		}

		/// <param name="handle" />
		// Token: 0x06000752 RID: 1874 RVA: 0x00011E6C File Offset: 0x0001006C
		public static explicit operator LocalConstantHandle(Handle handle)
		{
			if (handle.VType != 52)
			{
				Throw.InvalidCast();
			}
			return new LocalConstantHandle(handle.RowId);
		}

		/// <param name="handle" />
		// Token: 0x06000753 RID: 1875 RVA: 0x00011E8A File Offset: 0x0001008A
		public static explicit operator LocalConstantHandle(EntityHandle handle)
		{
			if (handle.VType != 872415232U)
			{
				Throw.InvalidCast();
			}
			return new LocalConstantHandle(handle.RowId);
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x00011EAB File Offset: 0x000100AB
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000755 RID: 1877 RVA: 0x00011EB6 File Offset: 0x000100B6
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x06000756 RID: 1878 RVA: 0x00011EBE File Offset: 0x000100BE
		public static bool operator ==(LocalConstantHandle left, LocalConstantHandle right)
		{
			return left._rowId == right._rowId;
		}

		/// <param name="obj" />
		// Token: 0x06000757 RID: 1879 RVA: 0x00011ECE File Offset: 0x000100CE
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is LocalConstantHandle && ((LocalConstantHandle)obj)._rowId == this._rowId;
		}

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000758 RID: 1880 RVA: 0x00011EED File Offset: 0x000100ED
		public bool Equals(LocalConstantHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x00011F00 File Offset: 0x00010100
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x0600075A RID: 1882 RVA: 0x00011F1B File Offset: 0x0001011B
		public static bool operator !=(LocalConstantHandle left, LocalConstantHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x04000465 RID: 1125
		private const uint tokenType = 872415232U;

		// Token: 0x04000466 RID: 1126
		private const byte tokenTypeSmall = 52;

		// Token: 0x04000467 RID: 1127
		private readonly int _rowId;
	}
}

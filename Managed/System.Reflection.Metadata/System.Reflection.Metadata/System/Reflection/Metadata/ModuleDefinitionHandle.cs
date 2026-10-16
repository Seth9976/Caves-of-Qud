using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x02000053 RID: 83
	public readonly struct ModuleDefinitionHandle : IEquatable<ModuleDefinitionHandle>
	{
		// Token: 0x0600031C RID: 796 RVA: 0x00009828 File Offset: 0x00007A28
		internal ModuleDefinitionHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00009831 File Offset: 0x00007A31
		internal static ModuleDefinitionHandle FromRowId(int rowId)
		{
			return new ModuleDefinitionHandle(rowId);
		}

		/// <param name="handle" />
		// Token: 0x0600031E RID: 798 RVA: 0x00009839 File Offset: 0x00007A39
		public static implicit operator Handle(ModuleDefinitionHandle handle)
		{
			return new Handle(0, handle._rowId);
		}

		/// <param name="handle" />
		// Token: 0x0600031F RID: 799 RVA: 0x00009847 File Offset: 0x00007A47
		public static implicit operator EntityHandle(ModuleDefinitionHandle handle)
		{
			return new EntityHandle((uint)(0L | (long)handle._rowId));
		}

		/// <param name="handle" />
		// Token: 0x06000320 RID: 800 RVA: 0x00009859 File Offset: 0x00007A59
		public static explicit operator ModuleDefinitionHandle(Handle handle)
		{
			if (handle.VType != 0)
			{
				Throw.InvalidCast();
			}
			return new ModuleDefinitionHandle(handle.RowId);
		}

		/// <param name="handle" />
		// Token: 0x06000321 RID: 801 RVA: 0x00009875 File Offset: 0x00007A75
		public static explicit operator ModuleDefinitionHandle(EntityHandle handle)
		{
			if (handle.VType != 0U)
			{
				Throw.InvalidCast();
			}
			return new ModuleDefinitionHandle(handle.RowId);
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000322 RID: 802 RVA: 0x00009891 File Offset: 0x00007A91
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0000989C File Offset: 0x00007A9C
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x06000324 RID: 804 RVA: 0x000098A4 File Offset: 0x00007AA4
		public static bool operator ==(ModuleDefinitionHandle left, ModuleDefinitionHandle right)
		{
			return left._rowId == right._rowId;
		}

		/// <param name="obj" />
		// Token: 0x06000325 RID: 805 RVA: 0x000098B4 File Offset: 0x00007AB4
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is ModuleDefinitionHandle && ((ModuleDefinitionHandle)obj)._rowId == this._rowId;
		}

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000326 RID: 806 RVA: 0x000098D3 File Offset: 0x00007AD3
		public bool Equals(ModuleDefinitionHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x06000327 RID: 807 RVA: 0x000098E4 File Offset: 0x00007AE4
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x06000328 RID: 808 RVA: 0x000098FF File Offset: 0x00007AFF
		public static bool operator !=(ModuleDefinitionHandle left, ModuleDefinitionHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x040002BC RID: 700
		private const uint tokenType = 0U;

		// Token: 0x040002BD RID: 701
		private const byte tokenTypeSmall = 0;

		// Token: 0x040002BE RID: 702
		private readonly int _rowId;
	}
}

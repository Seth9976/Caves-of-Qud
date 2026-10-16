using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x02000065 RID: 101
	public readonly struct ModuleReferenceHandle : IEquatable<ModuleReferenceHandle>
	{
		// Token: 0x06000407 RID: 1031 RVA: 0x0000A982 File Offset: 0x00008B82
		private ModuleReferenceHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000A98B File Offset: 0x00008B8B
		internal static ModuleReferenceHandle FromRowId(int rowId)
		{
			return new ModuleReferenceHandle(rowId);
		}

		/// <param name="handle" />
		// Token: 0x06000409 RID: 1033 RVA: 0x0000A993 File Offset: 0x00008B93
		public static implicit operator Handle(ModuleReferenceHandle handle)
		{
			return new Handle(26, handle._rowId);
		}

		/// <param name="handle" />
		// Token: 0x0600040A RID: 1034 RVA: 0x0000A9A2 File Offset: 0x00008BA2
		public static implicit operator EntityHandle(ModuleReferenceHandle handle)
		{
			return new EntityHandle((uint)(436207616L | (long)handle._rowId));
		}

		/// <param name="handle" />
		// Token: 0x0600040B RID: 1035 RVA: 0x0000A9B8 File Offset: 0x00008BB8
		public static explicit operator ModuleReferenceHandle(Handle handle)
		{
			if (handle.VType != 26)
			{
				Throw.InvalidCast();
			}
			return new ModuleReferenceHandle(handle.RowId);
		}

		/// <param name="handle" />
		// Token: 0x0600040C RID: 1036 RVA: 0x0000A9D6 File Offset: 0x00008BD6
		public static explicit operator ModuleReferenceHandle(EntityHandle handle)
		{
			if (handle.VType != 436207616U)
			{
				Throw.InvalidCast();
			}
			return new ModuleReferenceHandle(handle.RowId);
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x0000A9F7 File Offset: 0x00008BF7
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x0000AA02 File Offset: 0x00008C02
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x0600040F RID: 1039 RVA: 0x0000AA0A File Offset: 0x00008C0A
		public static bool operator ==(ModuleReferenceHandle left, ModuleReferenceHandle right)
		{
			return left._rowId == right._rowId;
		}

		/// <param name="obj" />
		// Token: 0x06000410 RID: 1040 RVA: 0x0000AA1A File Offset: 0x00008C1A
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is ModuleReferenceHandle && ((ModuleReferenceHandle)obj)._rowId == this._rowId;
		}

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000411 RID: 1041 RVA: 0x0000AA39 File Offset: 0x00008C39
		public bool Equals(ModuleReferenceHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000AA4C File Offset: 0x00008C4C
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x06000413 RID: 1043 RVA: 0x0000AA67 File Offset: 0x00008C67
		public static bool operator !=(ModuleReferenceHandle left, ModuleReferenceHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x040002F2 RID: 754
		private const uint tokenType = 436207616U;

		// Token: 0x040002F3 RID: 755
		private const byte tokenTypeSmall = 26;

		// Token: 0x040002F4 RID: 756
		private readonly int _rowId;
	}
}

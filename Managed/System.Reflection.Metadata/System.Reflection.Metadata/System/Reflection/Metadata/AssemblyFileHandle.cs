using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x0200006B RID: 107
	public readonly struct AssemblyFileHandle : IEquatable<AssemblyFileHandle>
	{
		// Token: 0x06000459 RID: 1113 RVA: 0x0000AF82 File Offset: 0x00009182
		private AssemblyFileHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0000AF8B File Offset: 0x0000918B
		internal static AssemblyFileHandle FromRowId(int rowId)
		{
			return new AssemblyFileHandle(rowId);
		}

		/// <param name="handle" />
		// Token: 0x0600045B RID: 1115 RVA: 0x0000AF93 File Offset: 0x00009193
		public static implicit operator Handle(AssemblyFileHandle handle)
		{
			return new Handle(38, handle._rowId);
		}

		/// <param name="handle" />
		// Token: 0x0600045C RID: 1116 RVA: 0x0000AFA2 File Offset: 0x000091A2
		public static implicit operator EntityHandle(AssemblyFileHandle handle)
		{
			return new EntityHandle((uint)(637534208L | (long)handle._rowId));
		}

		/// <param name="handle" />
		// Token: 0x0600045D RID: 1117 RVA: 0x0000AFB8 File Offset: 0x000091B8
		public static explicit operator AssemblyFileHandle(Handle handle)
		{
			if (handle.VType != 38)
			{
				Throw.InvalidCast();
			}
			return new AssemblyFileHandle(handle.RowId);
		}

		/// <param name="handle" />
		// Token: 0x0600045E RID: 1118 RVA: 0x0000AFD6 File Offset: 0x000091D6
		public static explicit operator AssemblyFileHandle(EntityHandle handle)
		{
			if (handle.VType != 637534208U)
			{
				Throw.InvalidCast();
			}
			return new AssemblyFileHandle(handle.RowId);
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x0000AFF7 File Offset: 0x000091F7
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x0000B002 File Offset: 0x00009202
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x06000461 RID: 1121 RVA: 0x0000B00A File Offset: 0x0000920A
		public static bool operator ==(AssemblyFileHandle left, AssemblyFileHandle right)
		{
			return left._rowId == right._rowId;
		}

		/// <param name="obj" />
		// Token: 0x06000462 RID: 1122 RVA: 0x0000B01A File Offset: 0x0000921A
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is AssemblyFileHandle && ((AssemblyFileHandle)obj)._rowId == this._rowId;
		}

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000463 RID: 1123 RVA: 0x0000B039 File Offset: 0x00009239
		public bool Equals(AssemblyFileHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0000B04C File Offset: 0x0000924C
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x06000465 RID: 1125 RVA: 0x0000B067 File Offset: 0x00009267
		public static bool operator !=(AssemblyFileHandle left, AssemblyFileHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x04000304 RID: 772
		private const uint tokenType = 637534208U;

		// Token: 0x04000305 RID: 773
		private const byte tokenTypeSmall = 38;

		// Token: 0x04000306 RID: 774
		private readonly int _rowId;
	}
}

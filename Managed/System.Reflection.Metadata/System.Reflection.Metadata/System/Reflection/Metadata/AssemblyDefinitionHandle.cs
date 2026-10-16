using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x02000054 RID: 84
	public readonly struct AssemblyDefinitionHandle : IEquatable<AssemblyDefinitionHandle>
	{
		// Token: 0x06000329 RID: 809 RVA: 0x00009912 File Offset: 0x00007B12
		internal AssemblyDefinitionHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000991B File Offset: 0x00007B1B
		internal static AssemblyDefinitionHandle FromRowId(int rowId)
		{
			return new AssemblyDefinitionHandle(rowId);
		}

		/// <param name="handle" />
		// Token: 0x0600032B RID: 811 RVA: 0x00009923 File Offset: 0x00007B23
		public static implicit operator Handle(AssemblyDefinitionHandle handle)
		{
			return new Handle(32, handle._rowId);
		}

		/// <param name="handle" />
		// Token: 0x0600032C RID: 812 RVA: 0x00009932 File Offset: 0x00007B32
		public static implicit operator EntityHandle(AssemblyDefinitionHandle handle)
		{
			return new EntityHandle((uint)(536870912L | (long)handle._rowId));
		}

		/// <param name="handle" />
		// Token: 0x0600032D RID: 813 RVA: 0x00009948 File Offset: 0x00007B48
		public static explicit operator AssemblyDefinitionHandle(Handle handle)
		{
			if (handle.VType != 32)
			{
				Throw.InvalidCast();
			}
			return new AssemblyDefinitionHandle(handle.RowId);
		}

		/// <param name="handle" />
		// Token: 0x0600032E RID: 814 RVA: 0x00009966 File Offset: 0x00007B66
		public static explicit operator AssemblyDefinitionHandle(EntityHandle handle)
		{
			if (handle.VType != 536870912U)
			{
				Throw.InvalidCast();
			}
			return new AssemblyDefinitionHandle(handle.RowId);
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600032F RID: 815 RVA: 0x00009987 File Offset: 0x00007B87
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000330 RID: 816 RVA: 0x00009992 File Offset: 0x00007B92
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x06000331 RID: 817 RVA: 0x0000999A File Offset: 0x00007B9A
		public static bool operator ==(AssemblyDefinitionHandle left, AssemblyDefinitionHandle right)
		{
			return left._rowId == right._rowId;
		}

		/// <param name="obj" />
		// Token: 0x06000332 RID: 818 RVA: 0x000099AA File Offset: 0x00007BAA
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is AssemblyDefinitionHandle && ((AssemblyDefinitionHandle)obj)._rowId == this._rowId;
		}

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000333 RID: 819 RVA: 0x000099C9 File Offset: 0x00007BC9
		public bool Equals(AssemblyDefinitionHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x000099DC File Offset: 0x00007BDC
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x06000335 RID: 821 RVA: 0x000099F7 File Offset: 0x00007BF7
		public static bool operator !=(AssemblyDefinitionHandle left, AssemblyDefinitionHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x040002BF RID: 703
		private const uint tokenType = 536870912U;

		// Token: 0x040002C0 RID: 704
		private const byte tokenTypeSmall = 32;

		// Token: 0x040002C1 RID: 705
		private readonly int _rowId;
	}
}

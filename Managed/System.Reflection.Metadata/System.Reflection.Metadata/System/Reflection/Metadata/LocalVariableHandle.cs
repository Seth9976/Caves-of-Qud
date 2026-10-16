using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x020000C1 RID: 193
	public readonly struct LocalVariableHandle : IEquatable<LocalVariableHandle>
	{
		// Token: 0x06000741 RID: 1857 RVA: 0x00011D3E File Offset: 0x0000FF3E
		private LocalVariableHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x00011D47 File Offset: 0x0000FF47
		internal static LocalVariableHandle FromRowId(int rowId)
		{
			return new LocalVariableHandle(rowId);
		}

		/// <param name="handle" />
		// Token: 0x06000743 RID: 1859 RVA: 0x00011D4F File Offset: 0x0000FF4F
		public static implicit operator Handle(LocalVariableHandle handle)
		{
			return new Handle(51, handle._rowId);
		}

		/// <param name="handle" />
		// Token: 0x06000744 RID: 1860 RVA: 0x00011D5E File Offset: 0x0000FF5E
		public static implicit operator EntityHandle(LocalVariableHandle handle)
		{
			return new EntityHandle((uint)(855638016L | (long)handle._rowId));
		}

		/// <param name="handle" />
		// Token: 0x06000745 RID: 1861 RVA: 0x00011D74 File Offset: 0x0000FF74
		public static explicit operator LocalVariableHandle(Handle handle)
		{
			if (handle.VType != 51)
			{
				Throw.InvalidCast();
			}
			return new LocalVariableHandle(handle.RowId);
		}

		/// <param name="handle" />
		// Token: 0x06000746 RID: 1862 RVA: 0x00011D92 File Offset: 0x0000FF92
		public static explicit operator LocalVariableHandle(EntityHandle handle)
		{
			if (handle.VType != 855638016U)
			{
				Throw.InvalidCast();
			}
			return new LocalVariableHandle(handle.RowId);
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x00011DB3 File Offset: 0x0000FFB3
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000748 RID: 1864 RVA: 0x00011DBE File Offset: 0x0000FFBE
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x06000749 RID: 1865 RVA: 0x00011DC6 File Offset: 0x0000FFC6
		public static bool operator ==(LocalVariableHandle left, LocalVariableHandle right)
		{
			return left._rowId == right._rowId;
		}

		/// <param name="obj" />
		// Token: 0x0600074A RID: 1866 RVA: 0x00011DD6 File Offset: 0x0000FFD6
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is LocalVariableHandle && ((LocalVariableHandle)obj)._rowId == this._rowId;
		}

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600074B RID: 1867 RVA: 0x00011DF5 File Offset: 0x0000FFF5
		public bool Equals(LocalVariableHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x00011E08 File Offset: 0x00010008
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x0600074D RID: 1869 RVA: 0x00011E23 File Offset: 0x00010023
		public static bool operator !=(LocalVariableHandle left, LocalVariableHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x04000462 RID: 1122
		private const uint tokenType = 855638016U;

		// Token: 0x04000463 RID: 1123
		private const byte tokenTypeSmall = 51;

		// Token: 0x04000464 RID: 1124
		private readonly int _rowId;
	}
}

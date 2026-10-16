using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x020000BF RID: 191
	public readonly struct MethodDebugInformationHandle : IEquatable<MethodDebugInformationHandle>
	{
		// Token: 0x06000726 RID: 1830 RVA: 0x00011B42 File Offset: 0x0000FD42
		private MethodDebugInformationHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x00011B4B File Offset: 0x0000FD4B
		internal static MethodDebugInformationHandle FromRowId(int rowId)
		{
			return new MethodDebugInformationHandle(rowId);
		}

		/// <param name="handle" />
		// Token: 0x06000728 RID: 1832 RVA: 0x00011B53 File Offset: 0x0000FD53
		public static implicit operator Handle(MethodDebugInformationHandle handle)
		{
			return new Handle(49, handle._rowId);
		}

		/// <param name="handle" />
		// Token: 0x06000729 RID: 1833 RVA: 0x00011B62 File Offset: 0x0000FD62
		public static implicit operator EntityHandle(MethodDebugInformationHandle handle)
		{
			return new EntityHandle((uint)(822083584L | (long)handle._rowId));
		}

		/// <param name="handle" />
		// Token: 0x0600072A RID: 1834 RVA: 0x00011B78 File Offset: 0x0000FD78
		public static explicit operator MethodDebugInformationHandle(Handle handle)
		{
			if (handle.VType != 49)
			{
				Throw.InvalidCast();
			}
			return new MethodDebugInformationHandle(handle.RowId);
		}

		/// <param name="handle" />
		// Token: 0x0600072B RID: 1835 RVA: 0x00011B96 File Offset: 0x0000FD96
		public static explicit operator MethodDebugInformationHandle(EntityHandle handle)
		{
			if (handle.VType != 822083584U)
			{
				Throw.InvalidCast();
			}
			return new MethodDebugInformationHandle(handle.RowId);
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x0600072C RID: 1836 RVA: 0x00011BB7 File Offset: 0x0000FDB7
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x00011BC2 File Offset: 0x0000FDC2
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x0600072E RID: 1838 RVA: 0x00011BCA File Offset: 0x0000FDCA
		public static bool operator ==(MethodDebugInformationHandle left, MethodDebugInformationHandle right)
		{
			return left._rowId == right._rowId;
		}

		/// <param name="obj" />
		// Token: 0x0600072F RID: 1839 RVA: 0x00011BDA File Offset: 0x0000FDDA
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is MethodDebugInformationHandle && ((MethodDebugInformationHandle)obj)._rowId == this._rowId;
		}

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000730 RID: 1840 RVA: 0x00011BF9 File Offset: 0x0000FDF9
		public bool Equals(MethodDebugInformationHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x00011C0C File Offset: 0x0000FE0C
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x06000732 RID: 1842 RVA: 0x00011C27 File Offset: 0x0000FE27
		public static bool operator !=(MethodDebugInformationHandle left, MethodDebugInformationHandle right)
		{
			return left._rowId != right._rowId;
		}

		/// <summary>Returns a handle to a <see cref="T:System.Reflection.Metadata.MethodDefinition" /> that corresponds to this handle.</summary>
		/// <returns>A method definition handle that corresponds to this handle.</returns>
		// Token: 0x06000733 RID: 1843 RVA: 0x00011C3A File Offset: 0x0000FE3A
		public MethodDefinitionHandle ToDefinitionHandle()
		{
			return MethodDefinitionHandle.FromRowId(this._rowId);
		}

		// Token: 0x0400045C RID: 1116
		private const uint tokenType = 822083584U;

		// Token: 0x0400045D RID: 1117
		private const byte tokenTypeSmall = 49;

		// Token: 0x0400045E RID: 1118
		private readonly int _rowId;
	}
}

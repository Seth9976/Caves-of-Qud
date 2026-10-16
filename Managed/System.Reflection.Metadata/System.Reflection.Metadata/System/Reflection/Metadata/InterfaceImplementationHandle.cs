using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x02000055 RID: 85
	public readonly struct InterfaceImplementationHandle : IEquatable<InterfaceImplementationHandle>
	{
		// Token: 0x06000336 RID: 822 RVA: 0x00009A0A File Offset: 0x00007C0A
		internal InterfaceImplementationHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00009A13 File Offset: 0x00007C13
		internal static InterfaceImplementationHandle FromRowId(int rowId)
		{
			return new InterfaceImplementationHandle(rowId);
		}

		/// <param name="handle" />
		// Token: 0x06000338 RID: 824 RVA: 0x00009A1B File Offset: 0x00007C1B
		public static implicit operator Handle(InterfaceImplementationHandle handle)
		{
			return new Handle(9, handle._rowId);
		}

		/// <param name="handle" />
		// Token: 0x06000339 RID: 825 RVA: 0x00009A2A File Offset: 0x00007C2A
		public static implicit operator EntityHandle(InterfaceImplementationHandle handle)
		{
			return new EntityHandle((uint)(150994944L | (long)handle._rowId));
		}

		/// <param name="handle" />
		// Token: 0x0600033A RID: 826 RVA: 0x00009A40 File Offset: 0x00007C40
		public static explicit operator InterfaceImplementationHandle(Handle handle)
		{
			if (handle.VType != 9)
			{
				Throw.InvalidCast();
			}
			return new InterfaceImplementationHandle(handle.RowId);
		}

		/// <param name="handle" />
		// Token: 0x0600033B RID: 827 RVA: 0x00009A5E File Offset: 0x00007C5E
		public static explicit operator InterfaceImplementationHandle(EntityHandle handle)
		{
			if (handle.VType != 150994944U)
			{
				Throw.InvalidCast();
			}
			return new InterfaceImplementationHandle(handle.RowId);
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600033C RID: 828 RVA: 0x00009A7F File Offset: 0x00007C7F
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x0600033D RID: 829 RVA: 0x00009A8A File Offset: 0x00007C8A
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x0600033E RID: 830 RVA: 0x00009A92 File Offset: 0x00007C92
		public static bool operator ==(InterfaceImplementationHandle left, InterfaceImplementationHandle right)
		{
			return left._rowId == right._rowId;
		}

		/// <param name="obj" />
		// Token: 0x0600033F RID: 831 RVA: 0x00009AA2 File Offset: 0x00007CA2
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is InterfaceImplementationHandle && ((InterfaceImplementationHandle)obj)._rowId == this._rowId;
		}

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000340 RID: 832 RVA: 0x00009AC1 File Offset: 0x00007CC1
		public bool Equals(InterfaceImplementationHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00009AD4 File Offset: 0x00007CD4
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x06000342 RID: 834 RVA: 0x00009AEF File Offset: 0x00007CEF
		public static bool operator !=(InterfaceImplementationHandle left, InterfaceImplementationHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x040002C2 RID: 706
		private const uint tokenType = 150994944U;

		// Token: 0x040002C3 RID: 707
		private const byte tokenTypeSmall = 9;

		// Token: 0x040002C4 RID: 708
		private readonly int _rowId;
	}
}

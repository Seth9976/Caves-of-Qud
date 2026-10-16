using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	/// <summary>Represents a handle to the user string heap.</summary>
	// Token: 0x0200006C RID: 108
	public readonly struct UserStringHandle : IEquatable<UserStringHandle>
	{
		// Token: 0x06000466 RID: 1126 RVA: 0x0000B07A File Offset: 0x0000927A
		private UserStringHandle(int offset)
		{
			this._offset = offset;
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0000B083 File Offset: 0x00009283
		internal static UserStringHandle FromOffset(int heapOffset)
		{
			return new UserStringHandle(heapOffset);
		}

		/// <param name="handle" />
		// Token: 0x06000468 RID: 1128 RVA: 0x0000B08B File Offset: 0x0000928B
		public static implicit operator Handle(UserStringHandle handle)
		{
			return new Handle(112, handle._offset);
		}

		/// <param name="handle" />
		// Token: 0x06000469 RID: 1129 RVA: 0x0000B09A File Offset: 0x0000929A
		public static explicit operator UserStringHandle(Handle handle)
		{
			if (handle.VType != 112)
			{
				Throw.InvalidCast();
			}
			return new UserStringHandle(handle.Offset);
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x0000B0B8 File Offset: 0x000092B8
		public bool IsNil
		{
			get
			{
				return this._offset == 0;
			}
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0000B0C3 File Offset: 0x000092C3
		internal int GetHeapOffset()
		{
			return this._offset;
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x0600046C RID: 1132 RVA: 0x0000B0CB File Offset: 0x000092CB
		public static bool operator ==(UserStringHandle left, UserStringHandle right)
		{
			return left._offset == right._offset;
		}

		/// <param name="obj" />
		// Token: 0x0600046D RID: 1133 RVA: 0x0000B0DB File Offset: 0x000092DB
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is UserStringHandle && ((UserStringHandle)obj)._offset == this._offset;
		}

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x0600046E RID: 1134 RVA: 0x0000B0FA File Offset: 0x000092FA
		public bool Equals(UserStringHandle other)
		{
			return this._offset == other._offset;
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0000B10C File Offset: 0x0000930C
		public override int GetHashCode()
		{
			return this._offset.GetHashCode();
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x06000470 RID: 1136 RVA: 0x0000B127 File Offset: 0x00009327
		public static bool operator !=(UserStringHandle left, UserStringHandle right)
		{
			return left._offset != right._offset;
		}

		// Token: 0x04000307 RID: 775
		private readonly int _offset;
	}
}

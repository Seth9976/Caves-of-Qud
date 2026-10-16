using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x020000C4 RID: 196
	public readonly struct CustomDebugInformationHandle : IEquatable<CustomDebugInformationHandle>
	{
		// Token: 0x06000768 RID: 1896 RVA: 0x00012026 File Offset: 0x00010226
		private CustomDebugInformationHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0001202F File Offset: 0x0001022F
		internal static CustomDebugInformationHandle FromRowId(int rowId)
		{
			return new CustomDebugInformationHandle(rowId);
		}

		/// <param name="handle" />
		// Token: 0x0600076A RID: 1898 RVA: 0x00012037 File Offset: 0x00010237
		public static implicit operator Handle(CustomDebugInformationHandle handle)
		{
			return new Handle(55, handle._rowId);
		}

		/// <param name="handle" />
		// Token: 0x0600076B RID: 1899 RVA: 0x00012046 File Offset: 0x00010246
		public static implicit operator EntityHandle(CustomDebugInformationHandle handle)
		{
			return new EntityHandle((uint)(922746880L | (long)handle._rowId));
		}

		/// <param name="handle" />
		// Token: 0x0600076C RID: 1900 RVA: 0x0001205C File Offset: 0x0001025C
		public static explicit operator CustomDebugInformationHandle(Handle handle)
		{
			if (handle.VType != 55)
			{
				Throw.InvalidCast();
			}
			return new CustomDebugInformationHandle(handle.RowId);
		}

		/// <param name="handle" />
		// Token: 0x0600076D RID: 1901 RVA: 0x0001207A File Offset: 0x0001027A
		public static explicit operator CustomDebugInformationHandle(EntityHandle handle)
		{
			if (handle.VType != 922746880U)
			{
				Throw.InvalidCast();
			}
			return new CustomDebugInformationHandle(handle.RowId);
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x0600076E RID: 1902 RVA: 0x0001209B File Offset: 0x0001029B
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x0600076F RID: 1903 RVA: 0x000120A6 File Offset: 0x000102A6
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x06000770 RID: 1904 RVA: 0x000120AE File Offset: 0x000102AE
		public static bool operator ==(CustomDebugInformationHandle left, CustomDebugInformationHandle right)
		{
			return left._rowId == right._rowId;
		}

		/// <param name="obj" />
		// Token: 0x06000771 RID: 1905 RVA: 0x000120BE File Offset: 0x000102BE
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is CustomDebugInformationHandle && ((CustomDebugInformationHandle)obj)._rowId == this._rowId;
		}

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000772 RID: 1906 RVA: 0x000120DD File Offset: 0x000102DD
		public bool Equals(CustomDebugInformationHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x000120F0 File Offset: 0x000102F0
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x06000774 RID: 1908 RVA: 0x0001210B File Offset: 0x0001030B
		public static bool operator !=(CustomDebugInformationHandle left, CustomDebugInformationHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x0400046B RID: 1131
		private const uint tokenType = 922746880U;

		// Token: 0x0400046C RID: 1132
		private const byte tokenTypeSmall = 55;

		// Token: 0x0400046D RID: 1133
		private readonly int _rowId;
	}
}

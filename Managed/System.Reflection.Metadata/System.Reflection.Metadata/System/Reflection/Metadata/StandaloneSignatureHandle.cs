using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x02000061 RID: 97
	public readonly struct StandaloneSignatureHandle : IEquatable<StandaloneSignatureHandle>
	{
		// Token: 0x060003D3 RID: 979 RVA: 0x0000A5A6 File Offset: 0x000087A6
		private StandaloneSignatureHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000A5AF File Offset: 0x000087AF
		internal static StandaloneSignatureHandle FromRowId(int rowId)
		{
			return new StandaloneSignatureHandle(rowId);
		}

		/// <param name="handle" />
		// Token: 0x060003D5 RID: 981 RVA: 0x0000A5B7 File Offset: 0x000087B7
		public static implicit operator Handle(StandaloneSignatureHandle handle)
		{
			return new Handle(17, handle._rowId);
		}

		/// <param name="handle" />
		// Token: 0x060003D6 RID: 982 RVA: 0x0000A5C6 File Offset: 0x000087C6
		public static implicit operator EntityHandle(StandaloneSignatureHandle handle)
		{
			return new EntityHandle((uint)(285212672L | (long)handle._rowId));
		}

		/// <param name="handle" />
		// Token: 0x060003D7 RID: 983 RVA: 0x0000A5DC File Offset: 0x000087DC
		public static explicit operator StandaloneSignatureHandle(Handle handle)
		{
			if (handle.VType != 17)
			{
				Throw.InvalidCast();
			}
			return new StandaloneSignatureHandle(handle.RowId);
		}

		/// <param name="handle" />
		// Token: 0x060003D8 RID: 984 RVA: 0x0000A5FA File Offset: 0x000087FA
		public static explicit operator StandaloneSignatureHandle(EntityHandle handle)
		{
			if (handle.VType != 285212672U)
			{
				Throw.InvalidCast();
			}
			return new StandaloneSignatureHandle(handle.RowId);
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x0000A61B File Offset: 0x0000881B
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060003DA RID: 986 RVA: 0x0000A626 File Offset: 0x00008826
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x060003DB RID: 987 RVA: 0x0000A62E File Offset: 0x0000882E
		public static bool operator ==(StandaloneSignatureHandle left, StandaloneSignatureHandle right)
		{
			return left._rowId == right._rowId;
		}

		/// <param name="obj" />
		// Token: 0x060003DC RID: 988 RVA: 0x0000A63E File Offset: 0x0000883E
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is StandaloneSignatureHandle && ((StandaloneSignatureHandle)obj)._rowId == this._rowId;
		}

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x060003DD RID: 989 RVA: 0x0000A65D File Offset: 0x0000885D
		public bool Equals(StandaloneSignatureHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000A670 File Offset: 0x00008870
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x060003DF RID: 991 RVA: 0x0000A68B File Offset: 0x0000888B
		public static bool operator !=(StandaloneSignatureHandle left, StandaloneSignatureHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x040002E6 RID: 742
		private const uint tokenType = 285212672U;

		// Token: 0x040002E7 RID: 743
		private const byte tokenTypeSmall = 17;

		// Token: 0x040002E8 RID: 744
		private readonly int _rowId;
	}
}

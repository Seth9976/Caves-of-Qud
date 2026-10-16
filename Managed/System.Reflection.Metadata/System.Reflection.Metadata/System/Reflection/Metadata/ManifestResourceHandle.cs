using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x0200006A RID: 106
	public readonly struct ManifestResourceHandle : IEquatable<ManifestResourceHandle>
	{
		// Token: 0x0600044C RID: 1100 RVA: 0x0000AE8A File Offset: 0x0000908A
		private ManifestResourceHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0000AE93 File Offset: 0x00009093
		internal static ManifestResourceHandle FromRowId(int rowId)
		{
			return new ManifestResourceHandle(rowId);
		}

		/// <param name="handle" />
		// Token: 0x0600044E RID: 1102 RVA: 0x0000AE9B File Offset: 0x0000909B
		public static implicit operator Handle(ManifestResourceHandle handle)
		{
			return new Handle(40, handle._rowId);
		}

		/// <param name="handle" />
		// Token: 0x0600044F RID: 1103 RVA: 0x0000AEAA File Offset: 0x000090AA
		public static implicit operator EntityHandle(ManifestResourceHandle handle)
		{
			return new EntityHandle((uint)(671088640L | (long)handle._rowId));
		}

		/// <param name="handle" />
		// Token: 0x06000450 RID: 1104 RVA: 0x0000AEC0 File Offset: 0x000090C0
		public static explicit operator ManifestResourceHandle(Handle handle)
		{
			if (handle.VType != 40)
			{
				Throw.InvalidCast();
			}
			return new ManifestResourceHandle(handle.RowId);
		}

		/// <param name="handle" />
		// Token: 0x06000451 RID: 1105 RVA: 0x0000AEDE File Offset: 0x000090DE
		public static explicit operator ManifestResourceHandle(EntityHandle handle)
		{
			if (handle.VType != 671088640U)
			{
				Throw.InvalidCast();
			}
			return new ManifestResourceHandle(handle.RowId);
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x0000AEFF File Offset: 0x000090FF
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x0000AF0A File Offset: 0x0000910A
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x06000454 RID: 1108 RVA: 0x0000AF12 File Offset: 0x00009112
		public static bool operator ==(ManifestResourceHandle left, ManifestResourceHandle right)
		{
			return left._rowId == right._rowId;
		}

		/// <param name="obj" />
		// Token: 0x06000455 RID: 1109 RVA: 0x0000AF22 File Offset: 0x00009122
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is ManifestResourceHandle && ((ManifestResourceHandle)obj)._rowId == this._rowId;
		}

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x06000456 RID: 1110 RVA: 0x0000AF41 File Offset: 0x00009141
		public bool Equals(ManifestResourceHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0000AF54 File Offset: 0x00009154
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x06000458 RID: 1112 RVA: 0x0000AF6F File Offset: 0x0000916F
		public static bool operator !=(ManifestResourceHandle left, ManifestResourceHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x04000301 RID: 769
		private const uint tokenType = 671088640U;

		// Token: 0x04000302 RID: 770
		private const byte tokenTypeSmall = 40;

		// Token: 0x04000303 RID: 771
		private readonly int _rowId;
	}
}

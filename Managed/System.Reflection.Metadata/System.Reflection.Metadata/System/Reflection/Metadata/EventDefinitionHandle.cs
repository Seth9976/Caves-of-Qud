using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x0200005F RID: 95
	public readonly struct EventDefinitionHandle : IEquatable<EventDefinitionHandle>
	{
		// Token: 0x060003B9 RID: 953 RVA: 0x0000A3B6 File Offset: 0x000085B6
		private EventDefinitionHandle(int rowId)
		{
			this._rowId = rowId;
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000A3BF File Offset: 0x000085BF
		internal static EventDefinitionHandle FromRowId(int rowId)
		{
			return new EventDefinitionHandle(rowId);
		}

		/// <param name="handle" />
		// Token: 0x060003BB RID: 955 RVA: 0x0000A3C7 File Offset: 0x000085C7
		public static implicit operator Handle(EventDefinitionHandle handle)
		{
			return new Handle(20, handle._rowId);
		}

		/// <param name="handle" />
		// Token: 0x060003BC RID: 956 RVA: 0x0000A3D6 File Offset: 0x000085D6
		public static implicit operator EntityHandle(EventDefinitionHandle handle)
		{
			return new EntityHandle((uint)(335544320L | (long)handle._rowId));
		}

		/// <param name="handle" />
		// Token: 0x060003BD RID: 957 RVA: 0x0000A3EC File Offset: 0x000085EC
		public static explicit operator EventDefinitionHandle(Handle handle)
		{
			if (handle.VType != 20)
			{
				Throw.InvalidCast();
			}
			return new EventDefinitionHandle(handle.RowId);
		}

		/// <param name="handle" />
		// Token: 0x060003BE RID: 958 RVA: 0x0000A40A File Offset: 0x0000860A
		public static explicit operator EventDefinitionHandle(EntityHandle handle)
		{
			if (handle.VType != 335544320U)
			{
				Throw.InvalidCast();
			}
			return new EventDefinitionHandle(handle.RowId);
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060003BF RID: 959 RVA: 0x0000A42B File Offset: 0x0000862B
		public bool IsNil
		{
			get
			{
				return this.RowId == 0;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x0000A436 File Offset: 0x00008636
		internal int RowId
		{
			get
			{
				return this._rowId;
			}
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x060003C1 RID: 961 RVA: 0x0000A43E File Offset: 0x0000863E
		public static bool operator ==(EventDefinitionHandle left, EventDefinitionHandle right)
		{
			return left._rowId == right._rowId;
		}

		/// <param name="obj" />
		// Token: 0x060003C2 RID: 962 RVA: 0x0000A44E File Offset: 0x0000864E
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is EventDefinitionHandle && ((EventDefinitionHandle)obj)._rowId == this._rowId;
		}

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x060003C3 RID: 963 RVA: 0x0000A46D File Offset: 0x0000866D
		public bool Equals(EventDefinitionHandle other)
		{
			return this._rowId == other._rowId;
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000A480 File Offset: 0x00008680
		public override int GetHashCode()
		{
			return this._rowId.GetHashCode();
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x060003C5 RID: 965 RVA: 0x0000A49B File Offset: 0x0000869B
		public static bool operator !=(EventDefinitionHandle left, EventDefinitionHandle right)
		{
			return left._rowId != right._rowId;
		}

		// Token: 0x040002E0 RID: 736
		private const uint tokenType = 335544320U;

		// Token: 0x040002E1 RID: 737
		private const byte tokenTypeSmall = 20;

		// Token: 0x040002E2 RID: 738
		private readonly int _rowId;
	}
}

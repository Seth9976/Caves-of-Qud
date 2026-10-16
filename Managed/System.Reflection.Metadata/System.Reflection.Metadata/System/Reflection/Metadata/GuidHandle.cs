using System;
using System.Runtime.CompilerServices;

namespace System.Reflection.Metadata
{
	// Token: 0x02000070 RID: 112
	public readonly struct GuidHandle : IEquatable<GuidHandle>
	{
		// Token: 0x060004A6 RID: 1190 RVA: 0x0000B504 File Offset: 0x00009704
		private GuidHandle(int index)
		{
			this._index = index;
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0000B50D File Offset: 0x0000970D
		internal static GuidHandle FromIndex(int heapIndex)
		{
			return new GuidHandle(heapIndex);
		}

		/// <param name="handle" />
		// Token: 0x060004A8 RID: 1192 RVA: 0x0000B515 File Offset: 0x00009715
		public static implicit operator Handle(GuidHandle handle)
		{
			return new Handle(114, handle._index);
		}

		/// <param name="handle" />
		// Token: 0x060004A9 RID: 1193 RVA: 0x0000B524 File Offset: 0x00009724
		public static explicit operator GuidHandle(Handle handle)
		{
			if (handle.VType != 114)
			{
				Throw.InvalidCast();
			}
			return new GuidHandle(handle.Offset);
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x0000B542 File Offset: 0x00009742
		public bool IsNil
		{
			get
			{
				return this._index == 0;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x0000B54D File Offset: 0x0000974D
		internal int Index
		{
			get
			{
				return this._index;
			}
		}

		/// <param name="obj" />
		// Token: 0x060004AC RID: 1196 RVA: 0x0000B555 File Offset: 0x00009755
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is GuidHandle && this.Equals((GuidHandle)obj);
		}

		/// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>
		///   <see langword="true" /> if the current object is equal to the <paramref name="other" /> parameter; otherwise, <see langword="false" />.</returns>
		// Token: 0x060004AD RID: 1197 RVA: 0x0000B56D File Offset: 0x0000976D
		public bool Equals(GuidHandle other)
		{
			return this._index == other._index;
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0000B57D File Offset: 0x0000977D
		public override int GetHashCode()
		{
			return this._index;
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x060004AF RID: 1199 RVA: 0x0000B585 File Offset: 0x00009785
		public static bool operator ==(GuidHandle left, GuidHandle right)
		{
			return left.Equals(right);
		}

		/// <param name="left" />
		/// <param name="right" />
		// Token: 0x060004B0 RID: 1200 RVA: 0x0000B58F File Offset: 0x0000978F
		public static bool operator !=(GuidHandle left, GuidHandle right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0400030C RID: 780
		private readonly int _index;
	}
}

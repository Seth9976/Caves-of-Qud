using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000269 RID: 617
	internal struct TypeLayout : IEquatable<TypeLayout>
	{
		// Token: 0x06001778 RID: 6008 RVA: 0x0004622C File Offset: 0x0004442C
		public TypeLayout(LayoutKind kind, int size, byte alignment)
		{
			this._kind = kind + 1;
			this._size = size;
			this._alignment = (short)alignment;
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06001779 RID: 6009 RVA: 0x00046246 File Offset: 0x00044446
		public LayoutKind Kind
		{
			get
			{
				if (this._kind != 0)
				{
					return this._kind - 1;
				}
				return 3;
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x0600177A RID: 6010 RVA: 0x0004625A File Offset: 0x0004445A
		public short Alignment
		{
			get
			{
				return this._alignment;
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x0600177B RID: 6011 RVA: 0x00046262 File Offset: 0x00044462
		public int Size
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x0004626A File Offset: 0x0004446A
		public bool Equals(TypeLayout other)
		{
			return this._size == other._size && this._alignment == other._alignment && this._kind == other._kind;
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x00046298 File Offset: 0x00044498
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is TypeLayout && this.Equals((TypeLayout)obj);
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x000462B0 File Offset: 0x000444B0
		public override int GetHashCode()
		{
			return Hash.Combine(Hash.Combine(this.Size, (int)this.Alignment), (int)this._kind);
		}

		// Token: 0x04000999 RID: 2457
		private readonly byte _kind;

		// Token: 0x0400099A RID: 2458
		private readonly short _alignment;

		// Token: 0x0400099B RID: 2459
		private readonly int _size;
	}
}

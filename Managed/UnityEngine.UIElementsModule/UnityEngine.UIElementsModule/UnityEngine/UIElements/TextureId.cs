using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x02000332 RID: 818
	internal struct TextureId
	{
		// Token: 0x060017B9 RID: 6073 RVA: 0x0005BB65 File Offset: 0x00059D65
		public TextureId(int index)
		{
			this.m_Index = index + 1;
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x060017BA RID: 6074 RVA: 0x0005BB71 File Offset: 0x00059D71
		public int index
		{
			get
			{
				return this.m_Index - 1;
			}
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x0005BB7C File Offset: 0x00059D7C
		public bool IsValid()
		{
			return this.m_Index > 0;
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x0005BB98 File Offset: 0x00059D98
		public float ConvertToGpu()
		{
			return (float)this.index;
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x0005BBB4 File Offset: 0x00059DB4
		public override bool Equals(object obj)
		{
			bool flag = !(obj is TextureId);
			return !flag && (TextureId)obj == this;
		}

		// Token: 0x060017BE RID: 6078 RVA: 0x0005BBEC File Offset: 0x00059DEC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(TextureId other)
		{
			return this.m_Index == other.m_Index;
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x0005BC0C File Offset: 0x00059E0C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return this.m_Index.GetHashCode();
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x0005BC2C File Offset: 0x00059E2C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(TextureId left, TextureId right)
		{
			return left.m_Index == right.m_Index;
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x0005BC4C File Offset: 0x00059E4C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(TextureId left, TextureId right)
		{
			return !(left == right);
		}

		// Token: 0x04000CF5 RID: 3317
		private readonly int m_Index;

		// Token: 0x04000CF6 RID: 3318
		public static readonly TextureId invalid = new TextureId(-1);
	}
}

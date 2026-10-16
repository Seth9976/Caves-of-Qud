using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200044F RID: 1103
	internal struct TransformData : IStyleDataGroup<TransformData>, IEquatable<TransformData>
	{
		// Token: 0x0600223E RID: 8766 RVA: 0x0007BFC0 File Offset: 0x0007A1C0
		public TransformData Copy()
		{
			return this;
		}

		// Token: 0x0600223F RID: 8767 RVA: 0x0007BFD8 File Offset: 0x0007A1D8
		public void CopyFrom(ref TransformData other)
		{
			this = other;
		}

		// Token: 0x06002240 RID: 8768 RVA: 0x0007BFE8 File Offset: 0x0007A1E8
		public static bool operator ==(TransformData lhs, TransformData rhs)
		{
			return lhs.rotate == rhs.rotate && lhs.scale == rhs.scale && lhs.transformOrigin == rhs.transformOrigin && lhs.translate == rhs.translate;
		}

		// Token: 0x06002241 RID: 8769 RVA: 0x0007C048 File Offset: 0x0007A248
		public static bool operator !=(TransformData lhs, TransformData rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06002242 RID: 8770 RVA: 0x0007C064 File Offset: 0x0007A264
		public bool Equals(TransformData other)
		{
			return other == this;
		}

		// Token: 0x06002243 RID: 8771 RVA: 0x0007C084 File Offset: 0x0007A284
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is TransformData && this.Equals((TransformData)obj);
		}

		// Token: 0x06002244 RID: 8772 RVA: 0x0007C0BC File Offset: 0x0007A2BC
		public override int GetHashCode()
		{
			int num = this.rotate.GetHashCode();
			num = (num * 397) ^ this.scale.GetHashCode();
			num = (num * 397) ^ this.transformOrigin.GetHashCode();
			return (num * 397) ^ this.translate.GetHashCode();
		}

		// Token: 0x04000F77 RID: 3959
		public Rotate rotate;

		// Token: 0x04000F78 RID: 3960
		public Scale scale;

		// Token: 0x04000F79 RID: 3961
		public TransformOrigin transformOrigin;

		// Token: 0x04000F7A RID: 3962
		public Translate translate;
	}
}

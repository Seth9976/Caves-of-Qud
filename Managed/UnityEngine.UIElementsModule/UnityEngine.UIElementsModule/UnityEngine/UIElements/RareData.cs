using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200044E RID: 1102
	internal struct RareData : IStyleDataGroup<RareData>, IEquatable<RareData>
	{
		// Token: 0x06002237 RID: 8759 RVA: 0x0007BD98 File Offset: 0x00079F98
		public RareData Copy()
		{
			return this;
		}

		// Token: 0x06002238 RID: 8760 RVA: 0x0007BDB0 File Offset: 0x00079FB0
		public void CopyFrom(ref RareData other)
		{
			this = other;
		}

		// Token: 0x06002239 RID: 8761 RVA: 0x0007BDC0 File Offset: 0x00079FC0
		public static bool operator ==(RareData lhs, RareData rhs)
		{
			return lhs.cursor == rhs.cursor && lhs.textOverflow == rhs.textOverflow && lhs.unityBackgroundImageTintColor == rhs.unityBackgroundImageTintColor && lhs.unityOverflowClipBox == rhs.unityOverflowClipBox && lhs.unitySliceBottom == rhs.unitySliceBottom && lhs.unitySliceLeft == rhs.unitySliceLeft && lhs.unitySliceRight == rhs.unitySliceRight && lhs.unitySliceScale == rhs.unitySliceScale && lhs.unitySliceTop == rhs.unitySliceTop && lhs.unitySliceType == rhs.unitySliceType && lhs.unityTextOverflowPosition == rhs.unityTextOverflowPosition;
		}

		// Token: 0x0600223A RID: 8762 RVA: 0x0007BE80 File Offset: 0x0007A080
		public static bool operator !=(RareData lhs, RareData rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x0600223B RID: 8763 RVA: 0x0007BE9C File Offset: 0x0007A09C
		public bool Equals(RareData other)
		{
			return other == this;
		}

		// Token: 0x0600223C RID: 8764 RVA: 0x0007BEBC File Offset: 0x0007A0BC
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is RareData && this.Equals((RareData)obj);
		}

		// Token: 0x0600223D RID: 8765 RVA: 0x0007BEF4 File Offset: 0x0007A0F4
		public override int GetHashCode()
		{
			int num = this.cursor.GetHashCode();
			num = (num * 397) ^ (int)this.textOverflow;
			num = (num * 397) ^ this.unityBackgroundImageTintColor.GetHashCode();
			num = (num * 397) ^ (int)this.unityOverflowClipBox;
			num = (num * 397) ^ this.unitySliceBottom;
			num = (num * 397) ^ this.unitySliceLeft;
			num = (num * 397) ^ this.unitySliceRight;
			num = (num * 397) ^ this.unitySliceScale.GetHashCode();
			num = (num * 397) ^ this.unitySliceTop;
			num = (num * 397) ^ (int)this.unitySliceType;
			return (num * 397) ^ (int)this.unityTextOverflowPosition;
		}

		// Token: 0x04000F6C RID: 3948
		public Cursor cursor;

		// Token: 0x04000F6D RID: 3949
		public TextOverflow textOverflow;

		// Token: 0x04000F6E RID: 3950
		public Color unityBackgroundImageTintColor;

		// Token: 0x04000F6F RID: 3951
		public OverflowClipBox unityOverflowClipBox;

		// Token: 0x04000F70 RID: 3952
		public int unitySliceBottom;

		// Token: 0x04000F71 RID: 3953
		public int unitySliceLeft;

		// Token: 0x04000F72 RID: 3954
		public int unitySliceRight;

		// Token: 0x04000F73 RID: 3955
		public float unitySliceScale;

		// Token: 0x04000F74 RID: 3956
		public int unitySliceTop;

		// Token: 0x04000F75 RID: 3957
		public SliceType unitySliceType;

		// Token: 0x04000F76 RID: 3958
		public TextOverflowPosition unityTextOverflowPosition;
	}
}

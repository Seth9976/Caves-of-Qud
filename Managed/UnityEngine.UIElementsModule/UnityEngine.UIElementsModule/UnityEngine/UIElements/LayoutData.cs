using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200044D RID: 1101
	internal struct LayoutData : IStyleDataGroup<LayoutData>, IEquatable<LayoutData>
	{
		// Token: 0x06002230 RID: 8752 RVA: 0x0007B780 File Offset: 0x00079980
		public LayoutData Copy()
		{
			return this;
		}

		// Token: 0x06002231 RID: 8753 RVA: 0x0007B798 File Offset: 0x00079998
		public void CopyFrom(ref LayoutData other)
		{
			this = other;
		}

		// Token: 0x06002232 RID: 8754 RVA: 0x0007B7A8 File Offset: 0x000799A8
		public static bool operator ==(LayoutData lhs, LayoutData rhs)
		{
			return lhs.alignContent == rhs.alignContent && lhs.alignItems == rhs.alignItems && lhs.alignSelf == rhs.alignSelf && lhs.borderBottomWidth == rhs.borderBottomWidth && lhs.borderLeftWidth == rhs.borderLeftWidth && lhs.borderRightWidth == rhs.borderRightWidth && lhs.borderTopWidth == rhs.borderTopWidth && lhs.bottom == rhs.bottom && lhs.display == rhs.display && lhs.flexBasis == rhs.flexBasis && lhs.flexDirection == rhs.flexDirection && lhs.flexGrow == rhs.flexGrow && lhs.flexShrink == rhs.flexShrink && lhs.flexWrap == rhs.flexWrap && lhs.height == rhs.height && lhs.justifyContent == rhs.justifyContent && lhs.left == rhs.left && lhs.marginBottom == rhs.marginBottom && lhs.marginLeft == rhs.marginLeft && lhs.marginRight == rhs.marginRight && lhs.marginTop == rhs.marginTop && lhs.maxHeight == rhs.maxHeight && lhs.maxWidth == rhs.maxWidth && lhs.minHeight == rhs.minHeight && lhs.minWidth == rhs.minWidth && lhs.paddingBottom == rhs.paddingBottom && lhs.paddingLeft == rhs.paddingLeft && lhs.paddingRight == rhs.paddingRight && lhs.paddingTop == rhs.paddingTop && lhs.position == rhs.position && lhs.right == rhs.right && lhs.top == rhs.top && lhs.width == rhs.width;
		}

		// Token: 0x06002233 RID: 8755 RVA: 0x0007BA38 File Offset: 0x00079C38
		public static bool operator !=(LayoutData lhs, LayoutData rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06002234 RID: 8756 RVA: 0x0007BA54 File Offset: 0x00079C54
		public bool Equals(LayoutData other)
		{
			return other == this;
		}

		// Token: 0x06002235 RID: 8757 RVA: 0x0007BA74 File Offset: 0x00079C74
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is LayoutData && this.Equals((LayoutData)obj);
		}

		// Token: 0x06002236 RID: 8758 RVA: 0x0007BAAC File Offset: 0x00079CAC
		public override int GetHashCode()
		{
			int num = (int)this.alignContent;
			num = (num * 397) ^ (int)this.alignItems;
			num = (num * 397) ^ (int)this.alignSelf;
			num = (num * 397) ^ this.borderBottomWidth.GetHashCode();
			num = (num * 397) ^ this.borderLeftWidth.GetHashCode();
			num = (num * 397) ^ this.borderRightWidth.GetHashCode();
			num = (num * 397) ^ this.borderTopWidth.GetHashCode();
			num = (num * 397) ^ this.bottom.GetHashCode();
			num = (num * 397) ^ (int)this.display;
			num = (num * 397) ^ this.flexBasis.GetHashCode();
			num = (num * 397) ^ (int)this.flexDirection;
			num = (num * 397) ^ this.flexGrow.GetHashCode();
			num = (num * 397) ^ this.flexShrink.GetHashCode();
			num = (num * 397) ^ (int)this.flexWrap;
			num = (num * 397) ^ this.height.GetHashCode();
			num = (num * 397) ^ (int)this.justifyContent;
			num = (num * 397) ^ this.left.GetHashCode();
			num = (num * 397) ^ this.marginBottom.GetHashCode();
			num = (num * 397) ^ this.marginLeft.GetHashCode();
			num = (num * 397) ^ this.marginRight.GetHashCode();
			num = (num * 397) ^ this.marginTop.GetHashCode();
			num = (num * 397) ^ this.maxHeight.GetHashCode();
			num = (num * 397) ^ this.maxWidth.GetHashCode();
			num = (num * 397) ^ this.minHeight.GetHashCode();
			num = (num * 397) ^ this.minWidth.GetHashCode();
			num = (num * 397) ^ this.paddingBottom.GetHashCode();
			num = (num * 397) ^ this.paddingLeft.GetHashCode();
			num = (num * 397) ^ this.paddingRight.GetHashCode();
			num = (num * 397) ^ this.paddingTop.GetHashCode();
			num = (num * 397) ^ (int)this.position;
			num = (num * 397) ^ this.right.GetHashCode();
			num = (num * 397) ^ this.top.GetHashCode();
			return (num * 397) ^ this.width.GetHashCode();
		}

		// Token: 0x04000F4B RID: 3915
		public Align alignContent;

		// Token: 0x04000F4C RID: 3916
		public Align alignItems;

		// Token: 0x04000F4D RID: 3917
		public Align alignSelf;

		// Token: 0x04000F4E RID: 3918
		public float borderBottomWidth;

		// Token: 0x04000F4F RID: 3919
		public float borderLeftWidth;

		// Token: 0x04000F50 RID: 3920
		public float borderRightWidth;

		// Token: 0x04000F51 RID: 3921
		public float borderTopWidth;

		// Token: 0x04000F52 RID: 3922
		public Length bottom;

		// Token: 0x04000F53 RID: 3923
		public DisplayStyle display;

		// Token: 0x04000F54 RID: 3924
		public Length flexBasis;

		// Token: 0x04000F55 RID: 3925
		public FlexDirection flexDirection;

		// Token: 0x04000F56 RID: 3926
		public float flexGrow;

		// Token: 0x04000F57 RID: 3927
		public float flexShrink;

		// Token: 0x04000F58 RID: 3928
		public Wrap flexWrap;

		// Token: 0x04000F59 RID: 3929
		public Length height;

		// Token: 0x04000F5A RID: 3930
		public Justify justifyContent;

		// Token: 0x04000F5B RID: 3931
		public Length left;

		// Token: 0x04000F5C RID: 3932
		public Length marginBottom;

		// Token: 0x04000F5D RID: 3933
		public Length marginLeft;

		// Token: 0x04000F5E RID: 3934
		public Length marginRight;

		// Token: 0x04000F5F RID: 3935
		public Length marginTop;

		// Token: 0x04000F60 RID: 3936
		public Length maxHeight;

		// Token: 0x04000F61 RID: 3937
		public Length maxWidth;

		// Token: 0x04000F62 RID: 3938
		public Length minHeight;

		// Token: 0x04000F63 RID: 3939
		public Length minWidth;

		// Token: 0x04000F64 RID: 3940
		public Length paddingBottom;

		// Token: 0x04000F65 RID: 3941
		public Length paddingLeft;

		// Token: 0x04000F66 RID: 3942
		public Length paddingRight;

		// Token: 0x04000F67 RID: 3943
		public Length paddingTop;

		// Token: 0x04000F68 RID: 3944
		public Position position;

		// Token: 0x04000F69 RID: 3945
		public Length right;

		// Token: 0x04000F6A RID: 3946
		public Length top;

		// Token: 0x04000F6B RID: 3947
		public Length width;
	}
}

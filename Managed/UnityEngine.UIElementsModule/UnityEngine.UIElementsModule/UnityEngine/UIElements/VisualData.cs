using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000451 RID: 1105
	internal struct VisualData : IStyleDataGroup<VisualData>, IEquatable<VisualData>
	{
		// Token: 0x0600224C RID: 8780 RVA: 0x0007C398 File Offset: 0x0007A598
		public VisualData Copy()
		{
			return this;
		}

		// Token: 0x0600224D RID: 8781 RVA: 0x0007C3B0 File Offset: 0x0007A5B0
		public void CopyFrom(ref VisualData other)
		{
			this = other;
		}

		// Token: 0x0600224E RID: 8782 RVA: 0x0007C3C0 File Offset: 0x0007A5C0
		public static bool operator ==(VisualData lhs, VisualData rhs)
		{
			return lhs.backgroundColor == rhs.backgroundColor && lhs.backgroundImage == rhs.backgroundImage && lhs.backgroundPositionX == rhs.backgroundPositionX && lhs.backgroundPositionY == rhs.backgroundPositionY && lhs.backgroundRepeat == rhs.backgroundRepeat && lhs.backgroundSize == rhs.backgroundSize && lhs.borderBottomColor == rhs.borderBottomColor && lhs.borderBottomLeftRadius == rhs.borderBottomLeftRadius && lhs.borderBottomRightRadius == rhs.borderBottomRightRadius && lhs.borderLeftColor == rhs.borderLeftColor && lhs.borderRightColor == rhs.borderRightColor && lhs.borderTopColor == rhs.borderTopColor && lhs.borderTopLeftRadius == rhs.borderTopLeftRadius && lhs.borderTopRightRadius == rhs.borderTopRightRadius && lhs.opacity == rhs.opacity && lhs.overflow == rhs.overflow;
		}

		// Token: 0x0600224F RID: 8783 RVA: 0x0007C514 File Offset: 0x0007A714
		public static bool operator !=(VisualData lhs, VisualData rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06002250 RID: 8784 RVA: 0x0007C530 File Offset: 0x0007A730
		public bool Equals(VisualData other)
		{
			return other == this;
		}

		// Token: 0x06002251 RID: 8785 RVA: 0x0007C550 File Offset: 0x0007A750
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is VisualData && this.Equals((VisualData)obj);
		}

		// Token: 0x06002252 RID: 8786 RVA: 0x0007C588 File Offset: 0x0007A788
		public override int GetHashCode()
		{
			int num = this.backgroundColor.GetHashCode();
			num = (num * 397) ^ this.backgroundImage.GetHashCode();
			num = (num * 397) ^ this.backgroundPositionX.GetHashCode();
			num = (num * 397) ^ this.backgroundPositionY.GetHashCode();
			num = (num * 397) ^ this.backgroundRepeat.GetHashCode();
			num = (num * 397) ^ this.backgroundSize.GetHashCode();
			num = (num * 397) ^ this.borderBottomColor.GetHashCode();
			num = (num * 397) ^ this.borderBottomLeftRadius.GetHashCode();
			num = (num * 397) ^ this.borderBottomRightRadius.GetHashCode();
			num = (num * 397) ^ this.borderLeftColor.GetHashCode();
			num = (num * 397) ^ this.borderRightColor.GetHashCode();
			num = (num * 397) ^ this.borderTopColor.GetHashCode();
			num = (num * 397) ^ this.borderTopLeftRadius.GetHashCode();
			num = (num * 397) ^ this.borderTopRightRadius.GetHashCode();
			num = (num * 397) ^ this.opacity.GetHashCode();
			return (num * 397) ^ (int)this.overflow;
		}

		// Token: 0x04000F7F RID: 3967
		public Color backgroundColor;

		// Token: 0x04000F80 RID: 3968
		public Background backgroundImage;

		// Token: 0x04000F81 RID: 3969
		public BackgroundPosition backgroundPositionX;

		// Token: 0x04000F82 RID: 3970
		public BackgroundPosition backgroundPositionY;

		// Token: 0x04000F83 RID: 3971
		public BackgroundRepeat backgroundRepeat;

		// Token: 0x04000F84 RID: 3972
		public BackgroundSize backgroundSize;

		// Token: 0x04000F85 RID: 3973
		public Color borderBottomColor;

		// Token: 0x04000F86 RID: 3974
		public Length borderBottomLeftRadius;

		// Token: 0x04000F87 RID: 3975
		public Length borderBottomRightRadius;

		// Token: 0x04000F88 RID: 3976
		public Color borderLeftColor;

		// Token: 0x04000F89 RID: 3977
		public Color borderRightColor;

		// Token: 0x04000F8A RID: 3978
		public Color borderTopColor;

		// Token: 0x04000F8B RID: 3979
		public Length borderTopLeftRadius;

		// Token: 0x04000F8C RID: 3980
		public Length borderTopRightRadius;

		// Token: 0x04000F8D RID: 3981
		public float opacity;

		// Token: 0x04000F8E RID: 3982
		public OverflowInternal overflow;
	}
}

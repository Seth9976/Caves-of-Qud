using System;

namespace ModelShark
{
	// Token: 0x020001A5 RID: 421
	public class TooltipOverflow
	{
		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000FDC RID: 4060 RVA: 0x000635C4 File Offset: 0x000617C4
		public bool IsAny
		{
			get
			{
				return this.BottomLeftCorner || this.TopLeftCorner || this.TopRightCorner || this.BottomRightCorner;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000FDD RID: 4061 RVA: 0x000635E6 File Offset: 0x000617E6
		public bool TopEdge
		{
			get
			{
				return this.TopLeftCorner && this.TopRightCorner;
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000FDE RID: 4062 RVA: 0x000635F8 File Offset: 0x000617F8
		public bool RightEdge
		{
			get
			{
				return this.TopRightCorner && this.BottomRightCorner;
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000FDF RID: 4063 RVA: 0x0006360A File Offset: 0x0006180A
		public bool LeftEdge
		{
			get
			{
				return this.TopLeftCorner && this.BottomLeftCorner;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000FE0 RID: 4064 RVA: 0x0006361C File Offset: 0x0006181C
		public bool BottomEdge
		{
			get
			{
				return this.BottomLeftCorner && this.BottomRightCorner;
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000FE1 RID: 4065 RVA: 0x0006362E File Offset: 0x0006182E
		// (set) Token: 0x06000FE2 RID: 4066 RVA: 0x00063636 File Offset: 0x00061836
		public bool TopRightCorner { get; set; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000FE3 RID: 4067 RVA: 0x0006363F File Offset: 0x0006183F
		// (set) Token: 0x06000FE4 RID: 4068 RVA: 0x00063647 File Offset: 0x00061847
		public bool TopLeftCorner { get; set; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000FE5 RID: 4069 RVA: 0x00063650 File Offset: 0x00061850
		// (set) Token: 0x06000FE6 RID: 4070 RVA: 0x00063658 File Offset: 0x00061858
		public bool BottomRightCorner { get; set; }

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000FE7 RID: 4071 RVA: 0x00063661 File Offset: 0x00061861
		// (set) Token: 0x06000FE8 RID: 4072 RVA: 0x00063669 File Offset: 0x00061869
		public bool BottomLeftCorner { get; set; }

		// Token: 0x06000FE9 RID: 4073 RVA: 0x00063674 File Offset: 0x00061874
		public TipPosition SuggestNewPosition(TipPosition fromPosition)
		{
			bool flag = fromPosition == TipPosition.MouseBottomLeftCorner || fromPosition == TipPosition.MouseTopLeftCorner || fromPosition == TipPosition.MouseBottomRightCorner || fromPosition == TipPosition.MouseTopRightCorner || fromPosition == TipPosition.MouseTopMiddle || fromPosition == TipPosition.MouseLeftMiddle || fromPosition == TipPosition.MouseRightMiddle || fromPosition == TipPosition.MouseBottomMiddle;
			switch (fromPosition)
			{
			case TipPosition.TopRightCorner:
			case TipPosition.MouseTopRightCorner:
				if (this.TopEdge && this.RightEdge)
				{
					if (!flag)
					{
						return TipPosition.BottomLeftCorner;
					}
					return TipPosition.MouseBottomLeftCorner;
				}
				else if (this.TopEdge)
				{
					if (!flag)
					{
						return TipPosition.BottomRightCorner;
					}
					return TipPosition.MouseBottomRightCorner;
				}
				else if (this.RightEdge)
				{
					if (!flag)
					{
						return TipPosition.TopLeftCorner;
					}
					return TipPosition.MouseTopLeftCorner;
				}
				break;
			case TipPosition.BottomRightCorner:
			case TipPosition.MouseBottomRightCorner:
				if (this.BottomEdge && this.RightEdge)
				{
					if (!flag)
					{
						return TipPosition.TopLeftCorner;
					}
					return TipPosition.MouseTopLeftCorner;
				}
				else if (this.BottomEdge)
				{
					if (!flag)
					{
						return TipPosition.TopRightCorner;
					}
					return TipPosition.MouseTopRightCorner;
				}
				else if (this.RightEdge)
				{
					if (!flag)
					{
						return TipPosition.BottomLeftCorner;
					}
					return TipPosition.MouseBottomLeftCorner;
				}
				break;
			case TipPosition.TopLeftCorner:
			case TipPosition.MouseTopLeftCorner:
				if (this.TopEdge && this.LeftEdge)
				{
					if (!flag)
					{
						return TipPosition.BottomRightCorner;
					}
					return TipPosition.MouseBottomRightCorner;
				}
				else if (this.TopEdge)
				{
					if (!flag)
					{
						return TipPosition.BottomLeftCorner;
					}
					return TipPosition.MouseBottomLeftCorner;
				}
				else if (this.LeftEdge)
				{
					if (!flag)
					{
						return TipPosition.TopRightCorner;
					}
					return TipPosition.MouseTopRightCorner;
				}
				break;
			case TipPosition.BottomLeftCorner:
			case TipPosition.MouseBottomLeftCorner:
				if (this.BottomEdge && this.LeftEdge)
				{
					if (!flag)
					{
						return TipPosition.TopRightCorner;
					}
					return TipPosition.MouseTopRightCorner;
				}
				else if (this.BottomEdge)
				{
					if (!flag)
					{
						return TipPosition.TopLeftCorner;
					}
					return TipPosition.MouseTopLeftCorner;
				}
				else if (this.LeftEdge)
				{
					if (!flag)
					{
						return TipPosition.BottomRightCorner;
					}
					return TipPosition.MouseBottomRightCorner;
				}
				break;
			case TipPosition.TopMiddle:
			case TipPosition.MouseTopMiddle:
				if (this.TopEdge && this.RightEdge)
				{
					if (!flag)
					{
						return TipPosition.BottomLeftCorner;
					}
					return TipPosition.MouseBottomLeftCorner;
				}
				else if (this.TopEdge && this.LeftEdge)
				{
					if (!flag)
					{
						return TipPosition.BottomRightCorner;
					}
					return TipPosition.MouseBottomRightCorner;
				}
				else if (this.TopEdge)
				{
					if (!flag)
					{
						return TipPosition.BottomMiddle;
					}
					return TipPosition.MouseBottomMiddle;
				}
				else if (this.RightEdge)
				{
					if (!flag)
					{
						return TipPosition.LeftMiddle;
					}
					return TipPosition.MouseLeftMiddle;
				}
				else if (this.LeftEdge)
				{
					if (!flag)
					{
						return TipPosition.RightMiddle;
					}
					return TipPosition.MouseRightMiddle;
				}
				break;
			case TipPosition.BottomMiddle:
			case TipPosition.MouseBottomMiddle:
				if (this.BottomEdge && this.RightEdge)
				{
					if (!flag)
					{
						return TipPosition.TopLeftCorner;
					}
					return TipPosition.MouseTopLeftCorner;
				}
				else if (this.BottomEdge && this.LeftEdge)
				{
					if (!flag)
					{
						return TipPosition.TopRightCorner;
					}
					return TipPosition.MouseTopRightCorner;
				}
				else if (this.BottomEdge)
				{
					if (!flag)
					{
						return TipPosition.TopMiddle;
					}
					return TipPosition.MouseTopMiddle;
				}
				else if (this.RightEdge)
				{
					if (!flag)
					{
						return TipPosition.LeftMiddle;
					}
					return TipPosition.MouseLeftMiddle;
				}
				else if (this.LeftEdge)
				{
					if (!flag)
					{
						return TipPosition.RightMiddle;
					}
					return TipPosition.MouseRightMiddle;
				}
				break;
			case TipPosition.RightMiddle:
			case TipPosition.MouseRightMiddle:
				if (this.TopEdge && this.RightEdge)
				{
					if (!flag)
					{
						return TipPosition.BottomLeftCorner;
					}
					return TipPosition.MouseBottomLeftCorner;
				}
				else if (this.BottomEdge && this.RightEdge)
				{
					if (!flag)
					{
						return TipPosition.TopLeftCorner;
					}
					return TipPosition.MouseTopLeftCorner;
				}
				else if (this.TopEdge)
				{
					if (!flag)
					{
						return TipPosition.BottomMiddle;
					}
					return TipPosition.MouseBottomMiddle;
				}
				else if (this.BottomEdge)
				{
					if (!flag)
					{
						return TipPosition.TopMiddle;
					}
					return TipPosition.MouseTopMiddle;
				}
				else if (this.RightEdge)
				{
					if (!flag)
					{
						return TipPosition.LeftMiddle;
					}
					return TipPosition.MouseLeftMiddle;
				}
				break;
			case TipPosition.LeftMiddle:
			case TipPosition.MouseLeftMiddle:
				if (this.TopEdge && this.LeftEdge)
				{
					if (!flag)
					{
						return TipPosition.BottomRightCorner;
					}
					return TipPosition.MouseBottomRightCorner;
				}
				else if (this.BottomEdge && this.LeftEdge)
				{
					if (!flag)
					{
						return TipPosition.TopRightCorner;
					}
					return TipPosition.MouseTopRightCorner;
				}
				else if (this.TopEdge)
				{
					if (!flag)
					{
						return TipPosition.BottomMiddle;
					}
					return TipPosition.MouseBottomMiddle;
				}
				else if (this.BottomEdge)
				{
					if (!flag)
					{
						return TipPosition.TopMiddle;
					}
					return TipPosition.MouseTopMiddle;
				}
				else if (this.LeftEdge)
				{
					if (!flag)
					{
						return TipPosition.RightMiddle;
					}
					return TipPosition.MouseRightMiddle;
				}
				break;
			}
			return fromPosition;
		}
	}
}

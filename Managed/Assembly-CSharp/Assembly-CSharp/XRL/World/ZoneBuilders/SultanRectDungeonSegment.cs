using System;
using Genkit;
using UnityEngine;

namespace XRL.World.ZoneBuilders
{
	// Token: 0x02000AF0 RID: 2800
	public class SultanRectDungeonSegment : ISultanDungeonSegment
	{
		// Token: 0x06007042 RID: 28738 RVA: 0x0027C4A8 File Offset: 0x0027A6A8
		public SultanRectDungeonSegment(Rect2D r)
		{
			this.rect = r;
		}

		// Token: 0x06007043 RID: 28739 RVA: 0x0027C4B7 File Offset: 0x0027A6B7
		public override int width()
		{
			return this.rect.Width;
		}

		// Token: 0x06007044 RID: 28740 RVA: 0x0027C4C4 File Offset: 0x0027A6C4
		public override int height()
		{
			return this.rect.Height;
		}

		// Token: 0x06007045 RID: 28741 RVA: 0x0027C4D1 File Offset: 0x0027A6D1
		public override bool HasCustomColor(int x, int y)
		{
			return false;
		}

		// Token: 0x06007046 RID: 28742 RVA: 0x0027C4D4 File Offset: 0x0027A6D4
		public override Color32 GetCustomColor(int x, int y)
		{
			return new Color32(0, 0, 0, 0);
		}

		// Token: 0x06007047 RID: 28743 RVA: 0x0027C4DF File Offset: 0x0027A6DF
		public override bool contains(int x, int y)
		{
			return this.contains(Location2D.Get(x, y));
		}

		// Token: 0x06007048 RID: 28744 RVA: 0x0027C4EE File Offset: 0x0027A6EE
		public override bool contains(Location2D pos)
		{
			return this.rect.Contains(pos.X, pos.Y);
		}

		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06007049 RID: 28745 RVA: 0x0027C507 File Offset: 0x0027A707
		public override int x1
		{
			get
			{
				return this.rect.x1;
			}
		}

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x0600704A RID: 28746 RVA: 0x0027C514 File Offset: 0x0027A714
		public override int y1
		{
			get
			{
				return this.rect.y1;
			}
		}

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x0600704B RID: 28747 RVA: 0x0027C521 File Offset: 0x0027A721
		public override int x2
		{
			get
			{
				return this.rect.x2;
			}
		}

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x0600704C RID: 28748 RVA: 0x0027C52E File Offset: 0x0027A72E
		public override int y2
		{
			get
			{
				return this.rect.y2;
			}
		}

		// Token: 0x040033B7 RID: 13239
		private Rect2D rect;
	}
}

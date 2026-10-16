using System;
using XRL.Language;

namespace XRL.World.Parts
{
	// Token: 0x02000DF6 RID: 3574
	[Serializable]
	public class ZoneLandmarkGroup : IZoneLandmark
	{
		// Token: 0x060090BF RID: 37055 RVA: 0x00342E74 File Offset: 0x00341074
		public void CacheBounds()
		{
			long timeTicks = The.Game.TimeTicks;
			if (timeTicks - 100L <= this.CacheTick)
			{
				return;
			}
			this.CacheTick = timeTicks;
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell == null)
			{
				return;
			}
			this.X1 = (this.Y1 = int.MaxValue);
			this.X2 = (this.Y2 = int.MinValue);
			this.Found = 0;
			if (this.ScanGet == null)
			{
				this.ScanGet = (int x, int y) => this.ParentObject.CurrentZone.GetCell(x, y).HasObject(this.Blueprint);
			}
			if (this.ScanSet == null)
			{
				this.ScanSet = delegate(int x, int y)
				{
					if (x < this.X1)
					{
						this.X1 = x;
					}
					if (y < this.Y1)
					{
						this.Y1 = y;
					}
					if (x > this.X2)
					{
						this.X2 = x;
					}
					if (y > this.Y2)
					{
						this.Y2 = y;
					}
					this.Found++;
				};
			}
			Tools.ScanFill(currentCell.X, currentCell.Y, currentCell.ParentZone.Width, currentCell.ParentZone.Height, this.ScanGet, this.ScanSet, this.Diagonal);
			if (this.Padding != 0)
			{
				this.X1 = Math.Max(this.X1 - this.Padding, 0);
				this.Y1 = Math.Max(this.Y1 - this.Padding, 0);
				this.X2 = Math.Min(this.X2 + this.Padding, currentCell.ParentZone.Width - 1);
				this.Y2 = Math.Min(this.Y2 + this.Padding, currentCell.ParentZone.Height - 1);
			}
		}

		// Token: 0x060090C0 RID: 37056 RVA: 0x00342FD4 File Offset: 0x003411D4
		public override string GetDisplayName()
		{
			if (!this.DisplayName.IsNullOrEmpty())
			{
				return this.DisplayName;
			}
			if (this.DisplayNameCache == null)
			{
				this.DisplayNameCache = GameObjectFactory.Factory.GetBlueprint(this.Blueprint).CachedDisplayNameStripped;
				if (this.Found > 1 || this.Count > 1)
				{
					this.DisplayNameCache = Grammar.Pluralize(this.DisplayNameCache);
				}
			}
			return this.DisplayNameCache;
		}

		// Token: 0x060090C1 RID: 37057 RVA: 0x00343044 File Offset: 0x00341244
		public override bool IsApplicable(int X, int Y)
		{
			this.CacheBounds();
			return this.Found >= this.Count && X >= this.X1 && X <= this.X2 && Y >= this.Y1 && Y <= this.Y2;
		}

		// Token: 0x04003C4A RID: 15434
		public string DisplayName;

		// Token: 0x04003C4B RID: 15435
		public string Blueprint;

		// Token: 0x04003C4C RID: 15436
		public int Count;

		// Token: 0x04003C4D RID: 15437
		public int Padding;

		// Token: 0x04003C4E RID: 15438
		public bool Diagonal;

		// Token: 0x04003C4F RID: 15439
		[NonSerialized]
		private string DisplayNameCache;

		// Token: 0x04003C50 RID: 15440
		[NonSerialized]
		private long CacheTick = -2147483648L;

		// Token: 0x04003C51 RID: 15441
		[NonSerialized]
		private int X1;

		// Token: 0x04003C52 RID: 15442
		[NonSerialized]
		private int Y1;

		// Token: 0x04003C53 RID: 15443
		[NonSerialized]
		private int X2;

		// Token: 0x04003C54 RID: 15444
		[NonSerialized]
		private int Y2;

		// Token: 0x04003C55 RID: 15445
		[NonSerialized]
		private int Found;

		// Token: 0x04003C56 RID: 15446
		[NonSerialized]
		private Func<int, int, bool> ScanGet;

		// Token: 0x04003C57 RID: 15447
		[NonSerialized]
		private Action<int, int> ScanSet;
	}
}

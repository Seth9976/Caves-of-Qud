using System;

namespace XRL.World.Parts
{
	// Token: 0x02001283 RID: 4739
	[Serializable]
	public class RandomWorldTile : IPart
	{
		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x0600BB94 RID: 48020 RVA: 0x00448408 File Offset: 0x00446608
		public Random DoubleSeed
		{
			get
			{
				Random random;
				if ((random = this._Rnd) == null)
				{
					random = (this._Rnd = new Random((this.ParentObject.CurrentCell.X / 2) ^ this.ParentObject.CurrentCell.Y ^ The.Game.GetWorldSeed(null)));
				}
				return random;
			}
		}

		// Token: 0x0600BB95 RID: 48021 RVA: 0x0044845C File Offset: 0x0044665C
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BB96 RID: 48022 RVA: 0x0044845F File Offset: 0x0044665F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ZoneBuiltEvent.ID;
		}

		// Token: 0x0600BB97 RID: 48023 RVA: 0x00448475 File Offset: 0x00446675
		public override bool HandleEvent(ZoneBuiltEvent E)
		{
			this.RandomizeTile();
			return base.HandleEvent(E);
		}

		// Token: 0x0600BB98 RID: 48024 RVA: 0x00448484 File Offset: 0x00446684
		public void RandomizeTile()
		{
			if (this.IsDoubleTile() && this.SetDoubleTile())
			{
				return;
			}
			if (!this.Tiles.IsNullOrEmpty())
			{
				this.ParentObject.Render.Tile = PickRandomTile.RandomVariant(this.Tiles.GetRandomSubstring(',', false, null), null);
			}
		}

		// Token: 0x0600BB99 RID: 48025 RVA: 0x004484D4 File Offset: 0x004466D4
		public bool IsDoubleTile()
		{
			return !this.DoubleTiles.IsNullOrEmpty() && this.DoubleChance > 0 && this.ParentObject.CurrentCell != null && this.DoubleSeed.NextDouble() <= (double)this.DoubleChance / 100.0;
		}

		// Token: 0x0600BB9A RID: 48026 RVA: 0x0044852C File Offset: 0x0044672C
		public bool IsDoubleSibling(GameObject Object)
		{
			RandomWorldTile randomWorldTile;
			return Object.TryGetPart<RandomWorldTile>(out randomWorldTile) && randomWorldTile.DoubleChance == this.DoubleChance && randomWorldTile.DoubleTiles == this.DoubleTiles;
		}

		// Token: 0x0600BB9B RID: 48027 RVA: 0x00448564 File Offset: 0x00446764
		public bool SetDoubleTile()
		{
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell == null)
			{
				return false;
			}
			bool flag = currentCell.X % 2 == 0;
			Cell cellFromDirection = currentCell.GetCellFromDirection(flag ? "E" : "W", true);
			GameObject gameObject = ((cellFromDirection != null) ? cellFromDirection.GetFirstObject(new Predicate<GameObject>(this.IsDoubleSibling)) : null);
			if (gameObject == null)
			{
				return false;
			}
			Render render = this.ParentObject.Render;
			string text = this.DoubleTiles.GetRandomSubstring(',', false, this.DoubleSeed);
			text = PickRandomTile.RandomVariant(text, this.DoubleSeed);
			text = text.Replace("[DIR]", (flag != render.HFlip) ? "left" : "right");
			render.Tile = text;
			if (!flag)
			{
				gameObject.Render.ColorString = render.ColorString;
				gameObject.Render.DetailColor = render.DetailColor;
			}
			return true;
		}

		// Token: 0x04004B40 RID: 19264
		public string Tiles;

		// Token: 0x04004B41 RID: 19265
		public string DoubleTiles;

		// Token: 0x04004B42 RID: 19266
		public int DoubleChance = 35;

		// Token: 0x04004B43 RID: 19267
		[NonSerialized]
		private Random _Rnd;
	}
}

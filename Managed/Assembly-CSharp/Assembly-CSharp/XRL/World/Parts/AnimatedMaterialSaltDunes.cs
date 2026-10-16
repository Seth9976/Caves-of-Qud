using System;
using XRL.Core;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x020012C9 RID: 4809
	[Serializable]
	public class AnimatedMaterialSaltDunes : IPart
	{
		// Token: 0x0600BDB4 RID: 48564 RVA: 0x004582B0 File Offset: 0x004564B0
		public AnimatedMaterialSaltDunes()
		{
			this.nFrameOffset = Stat.RandomCosmetic(0, 60);
		}

		// Token: 0x0600BDB5 RID: 48565 RVA: 0x004582C6 File Offset: 0x004564C6
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BDB6 RID: 48566 RVA: 0x004582CC File Offset: 0x004564CC
		public override bool Render(RenderEvent E)
		{
			if (this.ParentObject.Physics == null || this.ParentObject.Render == null || this.ParentObject.Physics.CurrentCell == null)
			{
				return true;
			}
			if ((XRLCore.CurrentFrame + this.nFrameOffset) % 60 % 30 == 0 && (this.ParentObject.Render.RenderString == "÷" || this.ParentObject.Render.RenderString == "~") && (this.ParentObject.Render.Tile == "Terrain/sw_desert.bmp" || this.ParentObject.Render.Tile == "Terrain/sw_plains.bmp") && Stat.RandomCosmetic(1, 20) == 1)
			{
				Cell currentCell = this.ParentObject.Physics.CurrentCell;
				Zone parentZone = currentCell.ParentZone;
				string text = "W";
				if (Stat.RandomCosmetic(1, 2) == 1)
				{
					text = "SW";
				}
				Cell cellFromDirection = currentCell.GetCellFromDirection(text, true);
				if (currentCell.X != 0 && currentCell.Y != parentZone.Height - 1 && cellFromDirection != null && cellFromDirection.Objects.Count > 0 && cellFromDirection.Objects[0].GetPart("AnimatedMaterialSaltDunes") != null)
				{
					Render part = cellFromDirection.Objects[0].GetPart<Render>();
					this.ParentObject.Render.RenderString = part.RenderString;
					this.ParentObject.Render.Tile = part.Tile;
				}
				else if (Stat.RandomCosmetic(1, 2) == 1)
				{
					this.ParentObject.Render.RenderString = "÷";
				}
				else
				{
					this.ParentObject.Render.RenderString = "~";
				}
				if ((!(this.ParentObject.Render.RenderString == "÷") && !(this.ParentObject.Render.RenderString == "~")) || (!(this.ParentObject.Render.Tile == "Terrain/sw_desert.bmp") && !(this.ParentObject.Render.Tile == "Terrain/sw_plains.bmp")))
				{
					this.ParentObject.Render.RenderString = "÷";
					this.ParentObject.Render.Tile = "Terrain/sw_desert.bmp";
				}
			}
			return true;
		}

		// Token: 0x0600BDB7 RID: 48567 RVA: 0x0045852E File Offset: 0x0045672E
		public override bool FireEvent(Event E)
		{
			return base.FireEvent(E);
		}

		// Token: 0x04004C55 RID: 19541
		public int nFrameOffset;
	}
}

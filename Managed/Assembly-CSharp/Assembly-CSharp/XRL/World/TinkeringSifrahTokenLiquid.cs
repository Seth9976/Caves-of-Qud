using System;
using XRL.Liquids;
using XRL.UI;
using XRL.World.Parts;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x02000912 RID: 2322
	[Serializable]
	public class TinkeringSifrahTokenLiquid : SifrahPrioritizableToken
	{
		// Token: 0x06005893 RID: 22675 RVA: 0x001ACC3F File Offset: 0x001AAE3F
		public TinkeringSifrahTokenLiquid()
		{
			this.Description = "use liquid";
			this.Tile = "Items/sw_vial.bmp";
			this.RenderString = "~";
		}

		// Token: 0x06005894 RID: 22676 RVA: 0x001ACC68 File Offset: 0x001AAE68
		public TinkeringSifrahTokenLiquid(string LiquidID)
			: this()
		{
			this.LiquidID = LiquidID;
			BaseLiquid liquid = LiquidVolume.GetLiquid(this.LiquidID);
			this.Description = "use " + liquid.GetName();
			if (LiquidID == "water")
			{
				this.Tile = "Items/sw_waterskin.bmp";
				this.ColorString = "&w";
				this.DetailColor = 'y';
				return;
			}
			if (LiquidID == "oil")
			{
				this.Tile = "Items/sw_waterskin.bmp";
				this.ColorString = "&K";
				this.DetailColor = 'y';
				return;
			}
			if (LiquidID == "wine")
			{
				this.Tile = "Items/sw_jug.bmp";
				this.ColorString = "&w";
				this.DetailColor = 'm';
				return;
			}
			if (LiquidID == "brainbrine")
			{
				this.Tile = "Items/sw_jug6.bmp";
				this.ColorString = "&w";
				this.DetailColor = 'g';
				return;
			}
			if (LiquidID == "proteangunk")
			{
				this.Tile = "Items/sw_jug7.bmp";
				this.ColorString = "&w";
				this.DetailColor = 'c';
				return;
			}
			if (LiquidID == "slime")
			{
				this.Tile = "Items/sw_jar2.bmp";
				this.ColorString = "&w";
				this.DetailColor = 'g';
				return;
			}
			if (LiquidID == "acid")
			{
				this.Tile = "Items/sw_spray.bmp";
				this.ColorString = "&G";
				this.DetailColor = 'C';
				return;
			}
			if (LiquidID == "lava")
			{
				this.Tile = "Items/sw_vial.bmp";
				this.ColorString = "&y";
				this.DetailColor = 'R';
				return;
			}
			if (LiquidID == "neutronflux")
			{
				this.Tile = "Items/sw_vial.bmp";
				this.ColorString = "&y";
				this.DetailColor = 'B';
				return;
			}
			this.Tile = "Items/sw_jug2.bmp";
			this.TileColor = "&y";
			string color = liquid.GetColor();
			if (!string.IsNullOrEmpty(color))
			{
				this.ColorString = "&" + color;
				this.DetailColor = color[0];
			}
		}

		// Token: 0x06005895 RID: 22677 RVA: 0x001ACE78 File Offset: 0x001AB078
		public override int GetPriority()
		{
			int num = The.Player.GetFreeDrams(this.LiquidID, null, null, null, false);
			if (num > 0)
			{
				BaseLiquid liquid = LiquidVolume.GetLiquid(this.LiquidID);
				num = Math.Max((int)((float)num / liquid.GetValuePerDram()), 1);
			}
			return num;
		}

		// Token: 0x06005896 RID: 22678 RVA: 0x001ACEC1 File Offset: 0x001AB0C1
		public override int GetTiebreakerPriority()
		{
			return 1;
		}

		// Token: 0x06005897 RID: 22679 RVA: 0x001ACEC4 File Offset: 0x001AB0C4
		public override string GetDescription(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			int num = The.Player.GetFreeDrams(this.LiquidID, null, null, null, false) - Game.GetTimesChosen(this, Slot);
			return string.Concat(new string[]
			{
				this.Description,
				" [have {{C|",
				num.ToString(),
				"}} ",
				(num == 1) ? "dram" : "drams",
				"]"
			});
		}

		// Token: 0x06005898 RID: 22680 RVA: 0x001ACF38 File Offset: 0x001AB138
		public override bool GetDisabled(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			return The.Player.GetFreeDrams(this.LiquidID, null, null, null, false) - Game.GetTimesChosen(this, Slot) <= 0 || base.GetDisabled(Game, Slot, ContextObject);
		}

		// Token: 0x06005899 RID: 22681 RVA: 0x001ACF68 File Offset: 0x001AB168
		public override bool CheckTokenUse(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			int timesChosen = Game.GetTimesChosen(this, Slot);
			if (The.Player.GetFreeDrams(this.LiquidID, null, null, null, false) - timesChosen <= 0)
			{
				Popup.ShowFail("You do not have any " + ((timesChosen > 0) ? " more" : "") + LiquidVolume.GetLiquid(this.LiquidID).GetName() + ".", true, true, true);
				return false;
			}
			return base.CheckTokenUse(Game, Slot, ContextObject);
		}

		// Token: 0x0600589A RID: 22682 RVA: 0x001ACFDE File Offset: 0x001AB1DE
		public override void UseToken(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			The.Player.UseDrams(1, this.LiquidID, null, null, null, null, false);
		}

		// Token: 0x04002C21 RID: 11297
		public string LiquidID;
	}
}

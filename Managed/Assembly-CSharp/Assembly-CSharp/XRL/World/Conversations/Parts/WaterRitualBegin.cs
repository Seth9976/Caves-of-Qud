using System;
using XRL.UI;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000C20 RID: 3104
	public class WaterRitualBegin : IWaterRitualPart
	{
		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x06007C3A RID: 31802 RVA: 0x002EA93F File Offset: 0x002E8B3F
		public override bool Affordable
		{
			get
			{
				return this.Drams > 0;
			}
		}

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x06007C3B RID: 31803 RVA: 0x002EA94A File Offset: 0x002E8B4A
		public override bool Available
		{
			get
			{
				return The.Speaker.InSameOrAdjacentCellTo(The.Player);
			}
		}

		// Token: 0x06007C3C RID: 31804 RVA: 0x002EA95B File Offset: 0x002E8B5B
		public override void Awake()
		{
			WaterRitual.Reset();
			this.Visible = true;
		}

		// Token: 0x06007C3D RID: 31805 RVA: 0x002EA969 File Offset: 0x002E8B69
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || ID == GetChoiceTagEvent.ID || ID == PrepareTextEvent.ID || ID == EnterElementEvent.ID;
		}

		// Token: 0x06007C3E RID: 31806 RVA: 0x002EA990 File Offset: 0x002E8B90
		public override bool HandleEvent(EnterElementEvent E)
		{
			if (!The.Speaker.InSameOrAdjacentCellTo(The.Player))
			{
				The.Player.ShowFailure("You are too far from " + The.Speaker.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " to perform the water ritual.");
				return false;
			}
			bool flag = false;
			if (!The.Speaker.HasIntProperty("WaterRitualed"))
			{
				string sifrahWaterRitual = Options.SifrahWaterRitual;
				if (sifrahWaterRitual == "Always")
				{
					flag = true;
				}
				else if (sifrahWaterRitual != "Never")
				{
					DialogResult dialogResult = Popup.ShowYesNoCancel(string.Concat(new string[]
					{
						"Do you want to play a game of Sifrah to perform the formal water ritual with ",
						The.Speaker.t(int.MaxValue, null, null, false, false, false, false, true, true, true, false, null, false, true, false, null, true, null, false),
						"? The formal ritual can be much more impactful. If you do not play the game of Sifrah, the informal water ritual will consume 1 dram of ",
						WaterRitual.LiquidName,
						"."
					}), "Sounds/UI/ui_notification", true, DialogResult.Cancel);
					if (dialogResult == DialogResult.Yes)
					{
						flag = true;
					}
					else if (dialogResult == DialogResult.Cancel)
					{
						return false;
					}
				}
			}
			if (flag)
			{
				FormalWaterRitualSifrah formalWaterRitualSifrah = new FormalWaterRitualSifrah(The.Speaker);
				formalWaterRitualSifrah.Play(The.Speaker);
				if (formalWaterRitualSifrah.Abort)
				{
					return false;
				}
				The.Speaker.SetIntProperty("SifrahWaterRitual", 1, false);
				The.Speaker.SetIntProperty("WaterRitualPerformance", formalWaterRitualSifrah.Performance, false);
			}
			else
			{
				if (!this.Affordable)
				{
					Popup.ShowFail("You don't have enough " + WaterRitual.Liquid.GetName() + " to begin the ritual.", true, true, true);
					return false;
				}
				The.Player.UseDrams(1, WaterRitual.Liquid.ID, null, null, null, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007C3F RID: 31807 RVA: 0x002EAB3D File Offset: 0x002E8D3D
		public override bool HandleEvent(PrepareTextEvent E)
		{
			this.Drams = The.Player.GetFreeDrams(WaterRitual.Liquid.ID, null, null, null, false);
			return base.HandleEvent(E);
		}

		// Token: 0x06007C40 RID: 31808 RVA: 0x002EAB64 File Offset: 0x002E8D64
		public override bool HandleEvent(GetChoiceTagEvent E)
		{
			E.Tag = string.Concat(new string[]
			{
				"{{",
				this.Lowlight,
				"|[begin water ritual",
				(Options.SifrahWaterRitual == "Never") ? ("; {{" + this.Numeric + "|1}} dram of " + (this.Affordable ? WaterRitual.LiquidName : WaterRitual.LiquidNameStripped)) : "",
				"]}}"
			});
			return base.HandleEvent(E);
		}

		// Token: 0x0400387F RID: 14463
		public int Drams;
	}
}

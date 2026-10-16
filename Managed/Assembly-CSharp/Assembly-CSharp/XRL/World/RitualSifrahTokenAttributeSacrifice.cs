using System;
using XRL.UI;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008C8 RID: 2248
	[Serializable]
	public class RitualSifrahTokenAttributeSacrifice : SifrahPrioritizableToken
	{
		// Token: 0x06005760 RID: 22368 RVA: 0x001A8624 File Offset: 0x001A6824
		public RitualSifrahTokenAttributeSacrifice()
		{
			this.Description = "sacrifice a point of an attribute";
			this.Tile = "Creatures/sw_pulsed_field_magnet.bmp";
			this.RenderString = "ã";
			this.ColorString = "&K";
			this.DetailColor = 'r';
		}

		// Token: 0x06005761 RID: 22369 RVA: 0x001A8660 File Offset: 0x001A6860
		public RitualSifrahTokenAttributeSacrifice(string Attribute)
			: this()
		{
			this.Attribute = Attribute;
			string attribute = this.Attribute;
			if (!(attribute == "Strength"))
			{
				if (!(attribute == "Agility"))
				{
					if (!(attribute == "Toughness"))
					{
						if (!(attribute == "Intelligence"))
						{
							if (!(attribute == "Willpower"))
							{
								if (attribute == "Ego")
								{
									this.Tile = "Items/ms_ego_sacrifice.bmp";
									this.ColorString = "&M";
								}
							}
							else
							{
								this.Tile = "Items/ms_willpower_sacrifice.bmp";
								this.ColorString = "&Y";
							}
						}
						else
						{
							this.Tile = "Items/ms_intelligence_sacrifice.bmp";
							this.ColorString = "&B";
						}
					}
					else
					{
						this.Tile = "Items/ms_toughness_sacrifice.bmp";
						this.ColorString = "&w";
					}
				}
				else
				{
					this.Tile = "Items/ms_agility_sacrifice.bmp";
					this.ColorString = "&W";
				}
			}
			else
			{
				this.Tile = "Items/ms_strength_sacrifice.bmp";
				this.ColorString = "&G";
			}
			this.Description = "sacrifice a point of " + Attribute;
		}

		// Token: 0x06005762 RID: 22370 RVA: 0x001A8773 File Offset: 0x001A6973
		public override int GetPriority()
		{
			if (!this.IsAvailable())
			{
				return 0;
			}
			return 1 + The.Player.Stat("Attribute", 0) / 10;
		}

		// Token: 0x06005763 RID: 22371 RVA: 0x001A8794 File Offset: 0x001A6994
		public override int GetTiebreakerPriority()
		{
			return 0;
		}

		// Token: 0x06005764 RID: 22372 RVA: 0x001A8797 File Offset: 0x001A6997
		public bool IsAvailable()
		{
			return The.Player.Stat(this.Attribute, 0) > 1;
		}

		// Token: 0x06005765 RID: 22373 RVA: 0x001A87AD File Offset: 0x001A69AD
		public override bool GetDisabled(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			return !this.IsAvailable() || base.GetDisabled(Game, Slot, ContextObject);
		}

		// Token: 0x06005766 RID: 22374 RVA: 0x001A87C2 File Offset: 0x001A69C2
		public override bool CheckTokenUse(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			if (!this.IsAvailable())
			{
				Popup.ShowFail("Your " + this.Attribute + " is too depleted to do that.", true, true, true);
				return false;
			}
			return base.CheckTokenUse(Game, Slot, ContextObject);
		}

		// Token: 0x06005767 RID: 22375 RVA: 0x001A87F4 File Offset: 0x001A69F4
		public override int GetPowerup(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			if (Slot.CurrentMove == Slot.Token)
			{
				return 3;
			}
			return base.GetPowerup(Game, Slot, ContextObject);
		}

		// Token: 0x06005768 RID: 22376 RVA: 0x001A8810 File Offset: 0x001A6A10
		public override void UseToken(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			Statistic stat = The.Player.GetStat(this.Attribute);
			int baseValue = stat.BaseValue;
			stat.BaseValue = baseValue - 1;
			base.UseToken(Game, Slot, ContextObject);
		}

		// Token: 0x04002C03 RID: 11267
		public string Attribute;
	}
}

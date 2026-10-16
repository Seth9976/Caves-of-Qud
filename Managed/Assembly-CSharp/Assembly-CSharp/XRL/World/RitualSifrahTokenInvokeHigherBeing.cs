using System;
using System.Collections.Generic;
using System.Text;
using XRL.UI;
using XRL.World.Parts;

namespace XRL.World
{
	/// This class is not used in the base game.
	// Token: 0x020008CF RID: 2255
	[Serializable]
	public class RitualSifrahTokenInvokeHigherBeing : SifrahPrioritizableToken
	{
		// Token: 0x0600577D RID: 22397 RVA: 0x001A8EB1 File Offset: 0x001A70B1
		public RitualSifrahTokenInvokeHigherBeing()
		{
			this.Description = "invoke a higher being";
			this.Tile = "Items/sw_chiral_rings.bmp";
			this.RenderString = "\u0015";
			this.ColorString = "&K";
			this.DetailColor = 'm';
		}

		// Token: 0x0600577E RID: 22398 RVA: 0x001A8EED File Offset: 0x001A70ED
		public RitualSifrahTokenInvokeHigherBeing(Worshippable Being, List<Worshippable> Beings = null)
			: this()
		{
			this.SetBeing(Being, Beings);
		}

		// Token: 0x0600577F RID: 22399 RVA: 0x001A8F00 File Offset: 0x001A7100
		public static string GenerateLabel(Worshippable Being, List<Worshippable> Beings = null, string Verb = null)
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			if (!Verb.IsNullOrEmpty())
			{
				stringBuilder.Append(Verb).Append(' ');
			}
			stringBuilder.Append(Being.Name);
			if (Worshippable.Multiple(Beings, Being))
			{
				if (!Verb.IsNullOrEmpty())
				{
					stringBuilder.Append(',');
				}
				stringBuilder.Append(" in the manner of ").Append(Faction.GetFormattedName(Being.Faction));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06005780 RID: 22400 RVA: 0x001A8F74 File Offset: 0x001A7174
		public void SetBeing(Worshippable Being, List<Worshippable> Beings = null)
		{
			this.Being = Being;
			this.Description = RitualSifrahTokenInvokeHigherBeing.GenerateLabel(Being, Beings, "invoke");
			string text = Being.Blueprints;
			if (text != null)
			{
				if (text.Contains(";") && !text.Contains(";;"))
				{
					text = text.Replace(";", ";;");
				}
				foreach (string text2 in text.CachedDoubleSemicolonExpansion())
				{
					GameObjectBlueprint blueprintIfExists = GameObjectFactory.Factory.GetBlueprintIfExists(text2);
					if (blueprintIfExists != null)
					{
						string partParameter = blueprintIfExists.GetPartParameter<string>("Render", "Tile", this.Tile);
						if (!partParameter.IsNullOrEmpty())
						{
							this.Tile = partParameter;
							string tag = blueprintIfExists.GetTag("WorshipColorPool", "");
							if (!tag.IsNullOrEmpty())
							{
								string text3 = "WorshipColorPool/" + tag + "/ColorString/" + Being.Name;
								string text4 = "WorshipColorPool/" + tag + "/DetailColor/" + Being.Name;
								if (The.Game.HasStringGameState(text3) && The.Game.HasStringGameState(text4))
								{
									this.ColorString = The.Game.GetStringGameState(text3, "");
									this.DetailColor = The.Game.GetStringGameState(text4, "")[0];
								}
								else
								{
									string text5 = "WorshipColorPool/" + tag + "/UsedColorSets";
									string stringGameState = The.Game.GetStringGameState(text5, "");
									int num = 0;
									string randomColor;
									string randomColor2;
									string text6;
									do
									{
										randomColor = Crayons.GetRandomColor(null);
										randomColor2 = Crayons.GetRandomColor(null);
										text6 = randomColor + randomColor2;
									}
									while ((randomColor == randomColor2 && ++num < 100) || (stringGameState.HasDelimitedSubstring(',', text6, StringComparison.Ordinal) && ++num < 20));
									The.Game.SetStringGameState(text5, stringGameState.AddDelimitedSubstring(',', text6, StringComparison.Ordinal));
									this.ColorString = "&" + randomColor;
									this.DetailColor = randomColor2[0];
									The.Game.SetStringGameState(text3, this.ColorString);
									The.Game.SetStringGameState(text4, randomColor2);
								}
							}
							string tag2 = blueprintIfExists.GetTag("WorshipColorString", "");
							if (!tag2.IsNullOrEmpty())
							{
								this.ColorString = tag2;
							}
							string tag3 = blueprintIfExists.GetTag("WorshipDetailColor", "");
							if (!tag3.IsNullOrEmpty())
							{
								this.DetailColor = tag3[0];
								break;
							}
							break;
						}
					}
				}
			}
		}

		// Token: 0x06005781 RID: 22401 RVA: 0x001A9220 File Offset: 0x001A7420
		public override bool GetDisabled(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			return !this.IsAvailable() || base.GetDisabled(Game, Slot, ContextObject);
		}

		// Token: 0x06005782 RID: 22402 RVA: 0x001A9238 File Offset: 0x001A7438
		public override int GetPriority()
		{
			if (!this.IsAvailable())
			{
				return 0;
			}
			if (this.Being == null)
			{
				return Math.Max(Factions.GetWorshippables().Count - 1, 0);
			}
			if (!The.Game.PlayerReputation.HasWorshipped(this.Being.Faction, 0))
			{
				int worshipValence = The.Game.PlayerReputation.GetWorshipValence(this.Being.Faction);
				if (worshipValence < 0)
				{
					int num = Math.Max(10 + worshipValence / 10, 0);
					string name = this.Being.Name;
					return num + ((name != null) ? name.Length : 1);
				}
			}
			int num2 = 1073741823 + this.Being.Power;
			string name2 = this.Being.Name;
			return num2 + ((name2 != null) ? name2.Length : 0);
		}

		// Token: 0x06005783 RID: 22403 RVA: 0x001A92F8 File Offset: 0x001A74F8
		public override int GetTiebreakerPriority()
		{
			if (this.Being == null)
			{
				return 7;
			}
			return 20 + this.Being.Power + (int)(this.Being.Name.IsNullOrEmpty() ? '\0' : (this.Being.Name[0] - 'A'));
		}

		// Token: 0x06005784 RID: 22404 RVA: 0x001A9347 File Offset: 0x001A7547
		public bool IsAvailable()
		{
			return this.Being == null || !The.Game.PlayerReputation.HasBlasphemed(this.Being, 36000);
		}

		// Token: 0x06005785 RID: 22405 RVA: 0x001A9370 File Offset: 0x001A7570
		public override bool CheckTokenUse(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			if (this.Being == null)
			{
				List<Worshippable> worshippables = Factions.GetWorshippables();
				if (!worshippables.IsNullOrEmpty<Worshippable>())
				{
					string[] array = new string[worshippables.Count];
					char[] array2 = new char[worshippables.Count];
					char c = 'a';
					int num = 0;
					foreach (Worshippable worshippable in worshippables)
					{
						array[num] = RitualSifrahTokenInvokeHigherBeing.GenerateLabel(worshippable, worshippables, null);
						array2[num] = ((c > 'z') ? ' ' : c);
						if (c <= 'z')
						{
							c += '\u0001';
						}
						num++;
					}
					int num2 = Popup.PickOption("Invoke whom?", null, "", "Sounds/UI/ui_notification", array, array2, null, null, null, null, null, 2, 60, 0, -1, true, false, false, true, false, null, null);
					if (num2 < 0)
					{
						return false;
					}
					this.SetBeing(worshippables[num2], null);
				}
			}
			if (!this.IsAvailable())
			{
				Popup.ShowFail("You have blasphemed against " + this.Being.Name + ".", true, true, true);
				return false;
			}
			return true;
		}

		// Token: 0x06005786 RID: 22406 RVA: 0x001A948C File Offset: 0x001A768C
		public override int GetPowerup(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			if (Slot.CurrentMove == Slot.Token && this.Being != null && this.Being.Power >= 50)
			{
				return 1;
			}
			return base.GetPowerup(Game, Slot, ContextObject);
		}

		// Token: 0x06005787 RID: 22407 RVA: 0x001A94BE File Offset: 0x001A76BE
		public override void UseToken(SifrahGame Game, SifrahSlot Slot, GameObject ContextObject)
		{
			The.Game.PlayerReputation.WorshipPerformed(this.Being);
		}

		// Token: 0x04002C04 RID: 11268
		public Worshippable Being;
	}
}

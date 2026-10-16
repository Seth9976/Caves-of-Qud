using System;
using ConsoleLib.Console;
using XRL.UI;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x020010B6 RID: 4278
	[Serializable]
	public class Toolbox : IPoweredPart
	{
		// Token: 0x0600AA6A RID: 43626 RVA: 0x003E51F9 File Offset: 0x003E33F9
		public Toolbox()
		{
			this.ChargeUse = 0;
			this.WorksOnEquipper = true;
			this.WorksOnCarrier = true;
			this.NameForStatus = "ToolSystems";
		}

		// Token: 0x0600AA6B RID: 43627 RVA: 0x003E5228 File Offset: 0x003E3428
		public override bool SameAs(IPart p)
		{
			Toolbox toolbox = p as Toolbox;
			return toolbox.PoweredDisassembleBonus == this.PoweredDisassembleBonus && toolbox.UnpoweredDisassembleBonus == this.UnpoweredDisassembleBonus && toolbox.PoweredInspectBonus == this.PoweredInspectBonus && toolbox.UnpoweredInspectBonus == this.UnpoweredInspectBonus && toolbox.PoweredReverseEngineerBonus == this.PoweredReverseEngineerBonus && toolbox.UnpoweredReverseEngineerBonus == this.UnpoweredReverseEngineerBonus && toolbox.PoweredBonusModBonus == this.PoweredBonusModBonus && toolbox.UnpoweredBonusModBonus == this.UnpoweredBonusModBonus && toolbox.TrackAsToolbox == this.TrackAsToolbox && !(toolbox.BehaviorDescription != this.BehaviorDescription) && toolbox.ComputePowerFactor == this.ComputePowerFactor && base.SameAs(p);
		}

		// Token: 0x0600AA6C RID: 43628 RVA: 0x003E52F8 File Offset: 0x003E34F8
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<GetDebugInternalsEvent>.ID || (ID == GetShortDescriptionEvent.ID && !string.IsNullOrEmpty(this.BehaviorDescription)) || ID == GetTinkeringBonusEvent.ID;
		}

		// Token: 0x0600AA6D RID: 43629 RVA: 0x003E532C File Offset: 0x003E352C
		public override bool HandleEvent(GetDebugInternalsEvent E)
		{
			E.AddEntry(this, "PoweredDisassembleBonus", this.PoweredDisassembleBonus);
			E.AddEntry(this, "UnpoweredDisassembleBonus", this.UnpoweredDisassembleBonus);
			E.AddEntry(this, "PoweredInspectBonus", this.PoweredInspectBonus);
			E.AddEntry(this, "UnpoweredInspectBonus", this.UnpoweredInspectBonus);
			E.AddEntry(this, "PoweredReverseEngineerBonus", this.PoweredReverseEngineerBonus);
			E.AddEntry(this, "UnpoweredReverseEngineerBonus", this.UnpoweredReverseEngineerBonus);
			E.AddEntry(this, "PoweredBonusModBonus", this.PoweredBonusModBonus);
			E.AddEntry(this, "UnpoweredBonusModBonus", this.UnpoweredBonusModBonus);
			E.AddEntry(this, "TrackAsToolbox", this.TrackAsToolbox);
			E.AddEntry(this, "BehaviorDescription", this.BehaviorDescription);
			E.AddEntry(this, "ComputePowerFactor", this.ComputePowerFactor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600AA6E RID: 43630 RVA: 0x003E5408 File Offset: 0x003E3608
		public override bool HandleEvent(GetTinkeringBonusEvent E)
		{
			string type = E.Type;
			if (!(type == "Disassemble"))
			{
				if (!(type == "Inspect"))
				{
					if (!(type == "ReverseEngineer"))
					{
						if (type == "BonusMod")
						{
							if (!this.HandleBonus(E, this.PoweredBonusModBonus, this.UnpoweredBonusModBonus))
							{
								return false;
							}
						}
					}
					else if (!this.HandleBonus(E, this.PoweredReverseEngineerBonus, this.UnpoweredReverseEngineerBonus))
					{
						return false;
					}
				}
				else if (!this.HandleBonus(E, this.PoweredInspectBonus, this.UnpoweredInspectBonus))
				{
					return false;
				}
			}
			else if (!this.HandleBonus(E, this.PoweredDisassembleBonus, this.UnpoweredDisassembleBonus))
			{
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AA6F RID: 43631 RVA: 0x003E54B8 File Offset: 0x003E36B8
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (!string.IsNullOrEmpty(this.BehaviorDescription))
			{
				E.Postfix.AppendRules(this.BehaviorDescription, base.GetEventSensitiveAddStatusSummary(E));
			}
			if (this.ComputePowerFactor > 0f)
			{
				E.Postfix.AppendRules("Compute power on the local lattice increases this item's effectiveness.");
			}
			else if (this.ComputePowerFactor < 0f)
			{
				E.Postfix.AppendRules("Compute power on the local lattice decreases this item's effectiveness.");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AA70 RID: 43632 RVA: 0x003E5530 File Offset: 0x003E3730
		private bool HandleBonus(GetTinkeringBonusEvent E, int PoweredBonus, int UnpoweredBonus)
		{
			if (PoweredBonus == 0 && UnpoweredBonus == 0)
			{
				return true;
			}
			if (this.ChargeUse > 0 && PoweredBonus != UnpoweredBonus)
			{
				if ((!this.TrackAsToolbox || ((PoweredBonus >= 0) ? (E.ToolboxBonus < PoweredBonus) : (E.ToolboxBonus > PoweredBonus))) && base.IsReady(false, true, false, true, false, false, false, false, false, false, 1, null, false, 0L, null) && this.IsObjectActivePartSubject(E.Actor))
				{
					ActivePartStatus activePartStatus = base.GetActivePartStatus(true, false, false, false, false, false, false, false, false, false, this.ParentObject.Count, null, false, 0L, null);
					if (activePartStatus == ActivePartStatus.Operational)
					{
						this.ApplyBonus(E, PoweredBonus);
						if (E.Actor.IsPlayer())
						{
							this.ParentObject.RemoveIntProperty("ToolboxUseWhenInoperative");
						}
					}
					else
					{
						if (E.Interruptable && E.Actor.IsPlayer() && this.ParentObject.GetIntProperty("ToolboxUseWhenInoperative", 0) == 0)
						{
							string text;
							if (activePartStatus == ActivePartStatus.Unpowered)
							{
								text = "unpowered";
							}
							else if (activePartStatus == ActivePartStatus.Booting)
							{
								text = "still starting up";
							}
							else
							{
								text = "inoperative";
							}
							string text2;
							if (UnpoweredBonus > 0)
							{
								if (activePartStatus == ActivePartStatus.Unpowered)
								{
									text2 = ", using " + this.ParentObject.them + " without power";
								}
								else
								{
									text2 = " without " + this.ParentObject.its + " full benefits";
								}
							}
							else
							{
								text2 = " without " + this.ParentObject.its + " benefits";
							}
							if (Popup.ShowYesNo(string.Concat(new string[]
							{
								ColorUtility.CapitalizeExceptFormatting((this.ParentObject.HasProperName ? this.ParentObject.The : "Your ") + this.ParentObject.DisplayNameOnly),
								this.ParentObject.Is,
								" ",
								text,
								". Do you want to continue",
								text2,
								"?"
							}), "Sounds/UI/ui_notification", true, DialogResult.Yes, null) != DialogResult.Yes)
							{
								AutoAct.Interrupt(null, null, null, false);
								return false;
							}
							this.ParentObject.SetIntProperty("ToolboxUseWhenInoperative", 1, false);
						}
						this.ApplyBonus(E, UnpoweredBonus);
					}
				}
			}
			else if (base.IsReady(false, false, false, true, false, false, false, false, false, false, 1, null, false, 0L, null) && this.IsObjectActivePartSubject(E.Actor))
			{
				this.ApplyBonus(E, UnpoweredBonus);
			}
			return true;
		}

		// Token: 0x0600AA71 RID: 43633 RVA: 0x003E57B0 File Offset: 0x003E39B0
		private void ApplyBonus(GetTinkeringBonusEvent E, int Bonus)
		{
			int num = GetAvailableComputePowerEvent.AdjustUp(this, Bonus, this.ComputePowerFactor);
			if (!this.TrackAsToolbox)
			{
				E.Bonus += num * this.ParentObject.Count;
				return;
			}
			if (num > 0)
			{
				int num2 = ((E.ToolboxBonus > 0) ? (num - E.ToolboxBonus) : num);
				if (num2 > 0)
				{
					E.Bonus += num2;
					E.ToolboxBonus += num2;
					return;
				}
			}
			else if (num < 0)
			{
				int num3 = ((E.ToolboxBonus < 0) ? (num - E.ToolboxBonus) : num);
				if (num3 < 0)
				{
					E.Bonus += num3;
					E.ToolboxBonus += num3;
				}
			}
		}

		// Token: 0x0400462F RID: 17967
		public int PoweredDisassembleBonus;

		// Token: 0x04004630 RID: 17968
		public int UnpoweredDisassembleBonus;

		// Token: 0x04004631 RID: 17969
		public int PoweredInspectBonus;

		// Token: 0x04004632 RID: 17970
		public int UnpoweredInspectBonus;

		// Token: 0x04004633 RID: 17971
		public int PoweredReverseEngineerBonus;

		// Token: 0x04004634 RID: 17972
		public int UnpoweredReverseEngineerBonus;

		// Token: 0x04004635 RID: 17973
		public int PoweredBonusModBonus;

		// Token: 0x04004636 RID: 17974
		public int UnpoweredBonusModBonus;

		// Token: 0x04004637 RID: 17975
		public bool TrackAsToolbox = true;

		// Token: 0x04004638 RID: 17976
		public string BehaviorDescription;

		// Token: 0x04004639 RID: 17977
		public float ComputePowerFactor;
	}
}

using System;
using XRL.Language;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000F28 RID: 3880
	[Serializable]
	public class CyberneticsFireSuppressionSystem : IPart
	{
		// Token: 0x06009AB5 RID: 39605 RVA: 0x00381CDF File Offset: 0x0037FEDF
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009AB6 RID: 39606 RVA: 0x00381CE2 File Offset: 0x0037FEE2
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x06009AB7 RID: 39607 RVA: 0x00381CE8 File Offset: 0x0037FEE8
		public override void TurnTick(long TimeTick, int Amount1)
		{
			GameObject implantee = this.ParentObject.Implantee;
			if (implantee != null && implantee.IsAflame() && implantee.IsActivatedAbilityToggledOn(this.ActivatedAbilityID))
			{
				int num = this.Amount.RollCached();
				string name = LiquidVolume.GetLiquid("gel").GetName(null);
				implantee.PlayWorldSound("Sounds/Abilities/sfx_ability_thickLiquidSpray", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				if (implantee.IsPlayer())
				{
					IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
					{
						"Your ",
						this.ParentObject.DisplayNameOnlyDirect,
						" discharges ",
						num.ToString(),
						" ",
						(num == 1) ? "dram" : "drams",
						" of ",
						name,
						" all over you."
					}), null, true);
				}
				else if (IComponent<GameObject>.Visible(implantee))
				{
					IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
					{
						Grammar.MakePossessive(implantee.DisplayNameOnlyDirect),
						" ",
						this.ParentObject.DisplayNameOnlyDirect,
						" discharges ",
						num.ToString(),
						" ",
						(num == 1) ? "dram" : "drams",
						" of ",
						name,
						" all over ",
						implantee.them,
						"."
					}), null, true);
				}
				implantee.ApplyEffect(new LiquidCovered("gel", num, 1, false, null, false), null);
				implantee.LiquidSplash(LiquidVolume.GetLiquid("gel"));
				base.PlayWorldSound(this.Sound, 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
			}
		}

		// Token: 0x06009AB8 RID: 39608 RVA: 0x00381ED5 File Offset: 0x003800D5
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009AB9 RID: 39609 RVA: 0x00381ED8 File Offset: 0x003800D8
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CommandEvent>.ID || ID == ImplantedEvent.ID || ID == UnimplantedEvent.ID;
		}

		// Token: 0x06009ABA RID: 39610 RVA: 0x00381F00 File Offset: 0x00380100
		public override bool HandleEvent(ImplantedEvent E)
		{
			this.ActivatedAbilityID = E.Implantee.AddActivatedAbility("Fire Suppression", CyberneticsFireSuppressionSystem.COMMAND_NAME, "Cybernetics", null, "\a", null, true, true, false, false, false, true, false, false, true, true, false, false, -1, null, null, null, null, null);
			return base.HandleEvent(E);
		}

		// Token: 0x06009ABB RID: 39611 RVA: 0x00381F4D File Offset: 0x0038014D
		public override bool HandleEvent(UnimplantedEvent E)
		{
			E.Implantee.RemoveActivatedAbility(ref this.ActivatedAbilityID);
			return base.HandleEvent(E);
		}

		// Token: 0x06009ABC RID: 39612 RVA: 0x00381F68 File Offset: 0x00380168
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == CyberneticsFireSuppressionSystem.COMMAND_NAME && E.Actor == this.ParentObject.Implantee)
			{
				this.ParentObject.Implantee.ToggleActivatedAbility(this.ActivatedAbilityID, false, null);
				this.ParentObject.ModIntProperty("ActivatedAbilityCommandsProcessed", 1, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0400404D RID: 16461
		public static readonly string COMMAND_NAME = "CommandToggleFireSuppressionSystem";

		// Token: 0x0400404E RID: 16462
		public Guid ActivatedAbilityID = Guid.Empty;

		// Token: 0x0400404F RID: 16463
		public string Amount = "2-4";

		// Token: 0x04004050 RID: 16464
		public string Sound = "Sounds/Abilities/sfx_ability_thickLiquidSpray";
	}
}

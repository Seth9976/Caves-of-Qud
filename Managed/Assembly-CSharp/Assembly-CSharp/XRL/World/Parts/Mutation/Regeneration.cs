using System;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001453 RID: 5203
	[Serializable]
	public class Regeneration : BaseMutation
	{
		// Token: 0x0600CB85 RID: 52101 RVA: 0x004A0413 File Offset: 0x0049E613
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("Regenerating");
			Registrar.Register("BeginTakeAction");
			Registrar.Register("BeforeDecapitate");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600CB86 RID: 52102 RVA: 0x004A043E File Offset: 0x0049E63E
		public override string GetDescription()
		{
			return "Your wounds heal very quickly.";
		}

		// Token: 0x0600CB87 RID: 52103 RVA: 0x004A0448 File Offset: 0x0049E648
		public override string GetLevelText(int Level)
		{
			string text = "";
			text += "Your full natural healing rate applies in combat.\n";
			text = text + "+{{rules|" + ((int)(100f * this.GetRegenerationBonus(Level))).ToString() + "%}} faster natural healing rate\n";
			text = text + "{{rules|" + this.GetRegenerationChance(Level).ToString() + "%}} chance to regrow a missing limb each round\n";
			if (this.GetRegenerationChance(Level) >= 100)
			{
				text += "{{rules|You cannot be decapitated.}}\n";
			}
			if (Level < 5)
			{
				text = text + "{{rules|" + this.GetDebuffChance(Level).ToString() + "}}% chance to remove a {{rules|minor physical debuff}} at random each round";
			}
			else
			{
				text = text + "{{rules|" + this.GetDebuffChance(Level).ToString() + "}}% chance to remove a {{rules|physical debuff}} at random each round";
			}
			return text;
		}

		// Token: 0x0600CB88 RID: 52104 RVA: 0x004A050C File Offset: 0x0049E70C
		public int GetDebuffChance(int Level)
		{
			return 1 + Level / 3;
		}

		// Token: 0x0600CB89 RID: 52105 RVA: 0x004A0513 File Offset: 0x0049E713
		public int GetRegenerationChance(int Level)
		{
			return Level * 10;
		}

		// Token: 0x0600CB8A RID: 52106 RVA: 0x004A0519 File Offset: 0x0049E719
		public float GetRegenerationBonus(int Level)
		{
			return 0.1f + 0.1f * (float)Level;
		}

		// Token: 0x0600CB8B RID: 52107 RVA: 0x004A052C File Offset: 0x0049E72C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeforeDecapitate")
			{
				if (this.GetRegenerationChance(base.Level) >= 100)
				{
					if (this.ParentObject.IsPlayer())
					{
						IComponent<GameObject>.AddPlayerMessage("{{G|You were decapitated, but a new head regrew immediately!}}", null, true);
					}
					return false;
				}
			}
			else if (E.ID == "Regenerating")
			{
				int num = E.GetIntParameter("Amount", 0);
				num += (int)Math.Ceiling((double)((float)num * this.GetRegenerationBonus(base.Level)));
				E.SetParameter("Amount", num);
			}
			else if (E.ID == "BeginTakeAction")
			{
				if (this.GetDebuffChance(base.Level).in100())
				{
					this.ParentObject.FireEvent(Event.New("Regenera", "Level", base.Level, "Involuntary", 1));
				}
				if (this.GetRegenerationChance(base.Level).in100())
				{
					RegenerateLimbEvent.Send(this.ParentObject, null, null, false, false, true, false, null, null, null, null, null);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600CB8C RID: 52108 RVA: 0x004A0658 File Offset: 0x0049E858
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600CB8D RID: 52109 RVA: 0x004A0661 File Offset: 0x0049E861
		public override bool Mutate(GameObject GO, int Level)
		{
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600CB8E RID: 52110 RVA: 0x004A066B File Offset: 0x0049E86B
		public override bool Unmutate(GameObject GO)
		{
			return base.Unmutate(GO);
		}

		// Token: 0x04004F20 RID: 20256
		public int nRegrowCount;

		// Token: 0x04004F21 RID: 20257
		public int nNextLimb = 5000;
	}
}

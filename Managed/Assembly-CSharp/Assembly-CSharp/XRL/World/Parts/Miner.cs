using System;
using XRL.Language;
using XRL.Rules;
using XRL.World.AI.GoalHandlers;
using XRL.World.Capabilities;
using XRL.World.Parts.Skill;

namespace XRL.World.Parts
{
	// Token: 0x02000EDC RID: 3804
	[Serializable]
	public class Miner : IPart
	{
		// Token: 0x06009837 RID: 38967 RVA: 0x003749DC File Offset: 0x00372BDC
		public override bool SameAs(IPart Part)
		{
			Miner miner = Part as Miner;
			return miner.MineCooldown == this.MineCooldown && !(miner.Cooldown != this.Cooldown) && !(miner.MineType != this.MineType) && !(miner.MineName != this.MineName) && !(miner.MineTimer != this.MineTimer) && miner.MaxMinesPerZone == this.MaxMinesPerZone && miner.Mark == this.Mark && base.SameAs(Part);
		}

		// Token: 0x06009838 RID: 38968 RVA: 0x00374A7C File Offset: 0x00372C7C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (ID == SingletonEvent<BeginTakeActionEvent>.ID && !this.ParentObject.IsPlayer()) || ID == PooledEvent<CommandEvent>.ID || (ID == AfterObjectCreatedEvent.ID && this.MineType.IsNullOrEmpty()) || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x06009839 RID: 38969 RVA: 0x00374ACF File Offset: 0x00372CCF
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == Miner.COMMAND_NAME && !base.IsMyActivatedAbilityCoolingDown(this.ActivatedAbilityID, null))
			{
				this.SetMineOrBomb();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600983A RID: 38970 RVA: 0x00374AFF File Offset: 0x00372CFF
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.ActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600983B RID: 38971 RVA: 0x00374B22 File Offset: 0x00372D22
		public override bool HandleEvent(AfterObjectCreatedEvent E)
		{
			this.SetupMinerConfiguration();
			return base.HandleEvent(E);
		}

		// Token: 0x0600983C RID: 38972 RVA: 0x00374B34 File Offset: 0x00372D34
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			int mineCooldown = this.MineCooldown;
			if (this.MineCooldown > 0)
			{
				this.MineCooldown--;
			}
			if (!this.ParentObject.IsPlayer() && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && this.ParentObject.FireEvent("CanAIDoIndependentBehavior") && !this.ParentObject.HasGoal("LayMineGoal") && !this.ParentObject.HasGoal("WanderRandomly") && !this.ParentObject.HasGoal("Flee"))
			{
				if (mineCooldown > 0 || this.ParentObject.CurrentZone.CountObjects("MineShell") >= this.MaxMinesPerZone)
				{
					GameObject target = this.ParentObject.Target;
					if (target != null)
					{
						this.ParentObject.Brain.PushGoal(new Flee(target, Stat.Random(10, 20), false, true));
					}
				}
				else
				{
					Cell randomElement = this.ParentObject.CurrentZone.GetEmptyReachableCells().GetRandomElement<Cell>(null);
					if (randomElement == null)
					{
						this.ParentObject.Brain.PushGoal(new WanderRandomly(5));
					}
					else
					{
						this.ParentObject.Brain.Goals.Clear();
						this.ParentObject.Brain.PushGoal(new LayMineGoal(randomElement.Location, this.MineType, this.MineName, this.MineTimer, 12 + this.Mark * 3));
					}
					this.MineCooldown = this.Cooldown.RollCached();
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600983D RID: 38973 RVA: 0x00374CC8 File Offset: 0x00372EC8
		public void SetMineOrBomb()
		{
			int num = this.MineTimer.RollCached();
			int num2 = 12 + this.Mark * 3;
			GameObject gameObject = ((num > 0) ? Tinkering_LayMine.CreateBomb(this.MineType, this.ParentObject, num) : Tinkering_LayMine.CreateMine(this.MineType, this.ParentObject));
			if (num2 > 0)
			{
				gameObject.RequirePart<Hidden>(false).Difficulty = num2;
			}
			if (this.ParentObject.IsPlayer())
			{
				this.ParentObject.PlayWorldOrUISound("Sounds/Abilities/sfx_ability_lay_mine", null);
			}
			this.ParentObject.CurrentCell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			this.ParentObject.UseEnergy(1000);
			base.CooldownMyActivatedAbility(this.ActivatedAbilityID, this.Cooldown.RollCached(), null, null, false);
		}

		// Token: 0x0600983E RID: 38974 RVA: 0x00374D9C File Offset: 0x00372F9C
		public void SetupMinerConfiguration()
		{
			if (this.MineType.IsNullOrEmpty())
			{
				string text = this.Mark.ToString();
				string text2 = "Explosives " + text;
				int num = 0;
				GameObject gameObject = null;
				while (++num <= 1000)
				{
					this.MineType = PopulationManager.RollOneFrom(text2, null, null).Blueprint;
					gameObject = GameObject.CreateSample(this.MineType);
					string tag = gameObject.GetTag("Mark", null);
					if (gameObject.HasPart<Tinkering_Layable>() && !(tag != text) && !gameObject.HasTag("NoMiners") && Scanning.GetScanTypeFor(gameObject) == Scanning.Scan.Tech)
					{
						string displayName = gameObject.Render.DisplayName;
						int num2 = displayName.IndexOf("grenade");
						if (num2 != -1)
						{
							this.MineName = displayName.Substring(0, num2);
						}
						else
						{
							this.MineName = displayName + " ";
						}
						if (!this.ParentObject.HasProperName)
						{
							if (this.MineTimer == "-1")
							{
								this.ParentObject.Render.DisplayName = this.MineName + "miner mk " + Grammar.GetRomanNumeral(this.Mark);
							}
							else
							{
								this.ParentObject.Render.DisplayName = this.MineName + "bomber mk " + Grammar.GetRomanNumeral(this.Mark);
							}
						}
						if (GameObject.Validate(ref gameObject))
						{
							gameObject.Obliterate(null, false, null);
							goto IL_0166;
						}
						goto IL_0166;
					}
				}
				throw new Exception("cannot generate layable grenade");
			}
			IL_0166:
			if (this.ActivatedAbilityID == Guid.Empty)
			{
				this.ActivatedAbilityID = base.AddMyActivatedAbility(string.Concat(new string[]
				{
					"Lay Mine [",
					this.MineName,
					"mk ",
					Grammar.GetRomanNumeral(this.Mark),
					"]"
				}), Miner.COMMAND_NAME, "Skills", "Lay Mine", "\u0012", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			}
		}

		// Token: 0x0600983F RID: 38975 RVA: 0x00374F90 File Offset: 0x00373190
		public void CollectStats(Templates.StatCollector stats)
		{
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), this.Cooldown);
			stats.Set("MineType", this.MineType, false, 0);
			stats.Set("MineName", this.MineName, false, 0);
			stats.Set("MineTimer", this.MineTimer, false, 0);
			stats.Set("MineMark", Grammar.GetRomanNumeral(this.Mark), false, 0);
			stats.Set("MineAn", Grammar.IndefiniteArticleShouldBeAn(this.MineName) ? "an" : "a", false, 0);
			stats.Set("MineDisplayName", this.MineName + " mk " + Grammar.GetRomanNumeral(this.Mark), false, 0);
		}

		// Token: 0x04003F6D RID: 16237
		public static readonly string COMMAND_NAME = "CommandPlaceMine";

		// Token: 0x04003F6E RID: 16238
		public int MineCooldown;

		// Token: 0x04003F6F RID: 16239
		public string Cooldown = "5-10";

		// Token: 0x04003F70 RID: 16240
		public string MineType;

		// Token: 0x04003F71 RID: 16241
		public string MineName;

		// Token: 0x04003F72 RID: 16242
		public string MineTimer = "-1";

		// Token: 0x04003F73 RID: 16243
		public int MaxMinesPerZone = 15;

		// Token: 0x04003F74 RID: 16244
		public int Mark = 1;

		// Token: 0x04003F75 RID: 16245
		public Guid ActivatedAbilityID = Guid.Empty;
	}
}

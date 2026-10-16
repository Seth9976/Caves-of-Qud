using System;
using System.Collections.Generic;
using XRL.World.Capabilities;
using XRL.World.Effects;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001442 RID: 5186
	[Serializable]
	public class GasGeneration : BaseMutation
	{
		// Token: 0x0600CA6C RID: 51820 RVA: 0x0049A5E2 File Offset: 0x004987E2
		public GasGeneration()
		{
			this.SyncFromBlueprint();
		}

		// Token: 0x0600CA6D RID: 51821 RVA: 0x0049A60E File Offset: 0x0049880E
		public GasGeneration(string GasObject)
		{
			this.GasObject = GasObject;
			this.SyncFromBlueprint();
		}

		// Token: 0x0600CA6E RID: 51822 RVA: 0x0049A641 File Offset: 0x00498841
		public override IPart DeepCopy(GameObject Parent, Func<GameObject, GameObject> MapInv)
		{
			GasGeneration gasGeneration = base.DeepCopy(Parent, MapInv) as GasGeneration;
			gasGeneration.SyncFromBlueprint();
			return gasGeneration;
		}

		// Token: 0x0600CA6F RID: 51823 RVA: 0x0049A656 File Offset: 0x00498856
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			base.Read(Basis, Reader);
			this.SyncFromBlueprint();
		}

		// Token: 0x0600CA70 RID: 51824 RVA: 0x0049A666 File Offset: 0x00498866
		private GameObjectBlueprint GeneratedGasBlueprint()
		{
			return GameObjectFactory.Factory.GetBlueprint(this.GasObject);
		}

		// Token: 0x0600CA71 RID: 51825 RVA: 0x0049A678 File Offset: 0x00498878
		protected void SyncFromBlueprint()
		{
			this.ReleaseAbilityCommand = "GasGenerationCommand" + this.GasObject;
			GameObjectBlueprint gameObjectBlueprint = this.GeneratedGasBlueprint();
			base.SetDisplayName(gameObjectBlueprint.GetTag("GasGenerationName", null) ?? (this.GasObject + " Generation"));
			this.GasType = gameObjectBlueprint.GetPartParameter<string>("Gas", "GasType", null);
			this.AddSeeping = gameObjectBlueprint.GetTag("GasGenerationAddSeeping", "").EqualsNoCase("true");
			this.AlreadySeeping = gameObjectBlueprint.GetPartParameter<bool>("Gas", "Seeping", false);
			string partParameter = gameObjectBlueprint.GetPartParameter<string>("Render", "DisplayName", null);
			if (!string.IsNullOrEmpty(partParameter))
			{
				this.Description = "You release a burst of " + partParameter + " around yourself.";
				return;
			}
			this.Description = "You release a gaseous burst around yourself.";
		}

		// Token: 0x0600CA72 RID: 51826 RVA: 0x0049A752 File Offset: 0x00498952
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == PooledEvent<CheckGasCanAffectEvent>.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x0600CA73 RID: 51827 RVA: 0x0049A778 File Offset: 0x00498978
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.ActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600CA74 RID: 51828 RVA: 0x0049A79C File Offset: 0x0049899C
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (E.Distance <= 1 && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null))
			{
				E.Add(this.GetReleaseAbilityCommand(), 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CA75 RID: 51829 RVA: 0x0049A7CF File Offset: 0x004989CF
		public override bool HandleEvent(CheckGasCanAffectEvent E)
		{
			return !(E.Gas.GasType == this.GasType) && base.HandleEvent(E);
		}

		// Token: 0x0600CA76 RID: 51830 RVA: 0x0049A7F2 File Offset: 0x004989F2
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register(this.GetReleaseAbilityCommand());
			Registrar.Register("EndTurn");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600CA77 RID: 51831 RVA: 0x0049A814 File Offset: 0x00498A14
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			bool flag = stats.mode.Contains("ability");
			stats.Set("Duration", this.GetReleaseDuration(Level), !flag, 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), this.GetReleaseCooldown(Level), 0);
		}

		// Token: 0x0600CA78 RID: 51832 RVA: 0x0049A864 File Offset: 0x00498A64
		public override string GetDescription()
		{
			return this.Description;
		}

		// Token: 0x0600CA79 RID: 51833 RVA: 0x0049A86C File Offset: 0x00498A6C
		public virtual int GetReleaseDuration(int Level)
		{
			return 1 + Level / 2;
		}

		// Token: 0x0600CA7A RID: 51834 RVA: 0x0049A873 File Offset: 0x00498A73
		public virtual int GetReleaseCooldown(int Level)
		{
			return 40;
		}

		// Token: 0x0600CA7B RID: 51835 RVA: 0x0049A877 File Offset: 0x00498A77
		public virtual string GetReleaseAbilityName()
		{
			return "Gas Generation";
		}

		// Token: 0x0600CA7C RID: 51836 RVA: 0x0049A87E File Offset: 0x00498A7E
		public virtual string GetReleaseAbilityCommand()
		{
			return this.ReleaseAbilityCommand;
		}

		// Token: 0x0600CA7D RID: 51837 RVA: 0x0049A888 File Offset: 0x00498A88
		public override string GetLevelText(int Level)
		{
			string text = "";
			text = text + "Releases gas for {{rules|" + this.GetReleaseDuration(Level).ToString() + "}} rounds";
			if (Level != base.Level)
			{
				string tag = this.GeneratedGasBlueprint().GetTag("LevelEffectDescription", "");
				if (tag != null)
				{
					if (Level > base.Level)
					{
						text = text + "\n{{rules|Increased " + tag + "}}";
					}
					else
					{
						text = text + "\n{{rules|Decreased " + tag + "}}";
					}
				}
			}
			return text + "\nCooldown: " + this.GetReleaseCooldown(Level).ToString() + " rounds";
		}

		// Token: 0x0600CA7E RID: 51838 RVA: 0x0049A930 File Offset: 0x00498B30
		public override bool Render(RenderEvent E)
		{
			if (base.IsMyActivatedAbilityToggledOn(this.ActivatedAbilityID, null))
			{
				string tag = this.GeneratedGasBlueprint().GetTag("ActivationColorString", "");
				if (tag != null)
				{
					E.ColorString = tag;
				}
			}
			return true;
		}

		// Token: 0x0600CA7F RID: 51839 RVA: 0x0049A96D File Offset: 0x00498B6D
		public virtual int GetGasDensityForLevel(int Level)
		{
			return this.GasObjectDensity;
		}

		// Token: 0x0600CA80 RID: 51840 RVA: 0x0049A978 File Offset: 0x00498B78
		public virtual void PumpGas()
		{
			GameObject parentObject = this.ParentObject;
			if (parentObject != null)
			{
				parentObject.PlayWorldSound("Sounds/Abilities/sfx_ability_gasMutation_passiveRelease", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			List<Cell> adjacentCells = this.ParentObject.CurrentCell.GetAdjacentCells(true);
			List<Cell> list = new List<Cell>(8);
			foreach (Cell cell in adjacentCells)
			{
				if (this.AddSeeping || this.AlreadySeeping || !cell.IsOccluding())
				{
					list.Add(cell);
				}
			}
			if (list.Count == 0)
			{
				list.Add(this.ParentObject.CurrentCell);
			}
			Phased phased;
			Omniphase omniphase;
			Phase.carryOverPrep(this.ParentObject, out phased, out omniphase);
			Event @event = Event.New("CreatorModifyGas", "Gas", null);
			foreach (Cell cell2 in list)
			{
				GameObject gameObject = GameObject.Create(this.GasObject);
				Gas part = gameObject.GetPart<Gas>();
				part.Creator = this.ParentObject;
				part.Density = this.GetGasDensityForLevel(base.Level) / list.Count;
				if (this.AddSeeping)
				{
					part.Seeping = true;
				}
				part.Level = base.Level;
				Phase.carryOver(this.ParentObject, gameObject, phased, omniphase);
				@event.SetParameter("Gas", part);
				this.ParentObject.FireEvent(@event);
				cell2.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			}
		}

		// Token: 0x0600CA81 RID: 51841 RVA: 0x0049AB40 File Offset: 0x00498D40
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EndTurn")
			{
				if (base.IsMyActivatedAbilityToggledOn(this.ActivatedAbilityID, null))
				{
					if (this.ParentObject.OnWorldMap())
					{
						this.BillowsTimer = -1;
					}
					else
					{
						this.BillowsTimer--;
					}
					if (this.BillowsTimer < 0)
					{
						base.ToggleMyActivatedAbility(this.ActivatedAbilityID, null, false, null);
						base.DidX("stop", "releasing " + this.GeneratedGasBlueprint().DisplayName(), null, null, null, null, null, false, false, null, null, false, false, false, false, false, null);
					}
					else
					{
						this.PumpGas();
					}
				}
			}
			else if (E.ID == this.GetReleaseAbilityCommand())
			{
				if (this.ParentObject.OnWorldMap())
				{
					this.ParentObject.Fail("You cannot do that on the world map.");
					return false;
				}
				this.ParentObject.PlayWorldSound("Sounds/Abilities/sfx_ability_gasMutation_activeRelease", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				base.UseEnergy(1000, "Physical Mutation Gas Generation");
				base.CooldownMyActivatedAbility(this.ActivatedAbilityID, this.GetReleaseCooldown(base.Level), null, null, false);
				base.ToggleMyActivatedAbility(this.ActivatedAbilityID, null, false, null);
				if (base.IsMyActivatedAbilityToggledOn(this.ActivatedAbilityID, null))
				{
					this.BillowsTimer = this.GetReleaseDuration(base.Level);
					this.PumpGas();
					base.DidX("start", "releasing " + this.GeneratedGasBlueprint().DisplayName(), null, null, null, null, null, false, false, null, null, false, false, false, false, false, null);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600CA82 RID: 51842 RVA: 0x0049ACFE File Offset: 0x00498EFE
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600CA83 RID: 51843 RVA: 0x0049AD08 File Offset: 0x00498F08
		public override bool Mutate(GameObject GO, int Level)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility(this.GetReleaseAbilityName(), this.GetReleaseAbilityCommand(), "Physical Mutations", null, "á", null, true, false, true, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600CA84 RID: 51844 RVA: 0x0049AD53 File Offset: 0x00498F53
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.Unmutate(GO);
		}

		// Token: 0x04004EEE RID: 20206
		public string GasObject = "AcidGas";

		// Token: 0x04004EEF RID: 20207
		public int BillowsTimer = 20;

		// Token: 0x04004EF0 RID: 20208
		public int GasObjectDensity = 800;

		// Token: 0x04004EF1 RID: 20209
		[NonSerialized]
		private bool AddSeeping;

		// Token: 0x04004EF2 RID: 20210
		[NonSerialized]
		private bool AlreadySeeping;

		// Token: 0x04004EF3 RID: 20211
		[NonSerialized]
		private string Description;

		// Token: 0x04004EF4 RID: 20212
		[NonSerialized]
		private string GasType;

		// Token: 0x04004EF5 RID: 20213
		[NonSerialized]
		private string ReleaseAbilityCommand;
	}
}

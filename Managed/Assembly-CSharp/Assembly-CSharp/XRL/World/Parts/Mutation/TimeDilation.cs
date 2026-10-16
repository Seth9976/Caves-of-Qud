using System;
using XRL.Rules;
using XRL.World.Effects;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001402 RID: 5122
	[Serializable]
	public class TimeDilation : BaseMutation
	{
		// Token: 0x0600C6D9 RID: 50905 RVA: 0x00487E86 File Offset: 0x00486086
		public TimeDilation()
		{
			base.Type = "Mental";
		}

		// Token: 0x0600C6DA RID: 50906 RVA: 0x00487EA1 File Offset: 0x004860A1
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == PooledEvent<CommandEvent>.ID || ID == SingletonEvent<EndTurnEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C6DB RID: 50907 RVA: 0x00487ED0 File Offset: 0x004860D0
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (E.Distance <= 10 && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && GameObject.Validate(E.Target) && !E.Target.IsInStasis() && base.CheckMyRealityDistortionAdvisability(null, null, null, null, null, null, 0))
			{
				E.Add(TimeDilation.COMMAND_NAME, 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C6DC RID: 50908 RVA: 0x00487F40 File Offset: 0x00486140
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == TimeDilation.COMMAND_NAME)
			{
				if (this.ParentObject.OnWorldMap())
				{
					return this.ParentObject.Fail("You cannot do that on the world map.");
				}
				if (!this.ParentObject.FireEvent(Event.New("InitiateRealityDistortionLocal", "Object", this.ParentObject, "Mutation", this), E))
				{
					return false;
				}
				this.Duration = 16;
				base.PlayWorldSound("Sounds/Abilities/sfx_ability_mutation_timeDilation_activate", 0.5f, 0f, true, null, 0f, 1f, 1f, int.MaxValue);
				this.ApplyField();
				string text = "Time";
				string text2 = "begins";
				string text3 = "to distort around";
				GameObject parentObject = this.ParentObject;
				string text4 = null;
				string text5 = null;
				GameObject gameObject = null;
				string text6 = null;
				GameObject parentObject2 = this.ParentObject;
				IComponent<GameObject>.XDidYToZ(text, text2, text3, parentObject, text4, text5, gameObject, text6, this.ParentObject, null, false, false, false, false, false, null, null, parentObject2, false, false, false, false, false, null);
				base.UseEnergy(1000, "Mental Mutation TimeDilation");
				base.CooldownMyActivatedAbility(this.ActivatedAbilityID, TimeDilation.GetCooldown(base.Level), null, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C6DD RID: 50909 RVA: 0x00488050 File Offset: 0x00486250
		public override bool HandleEvent(EndTurnEvent E)
		{
			if (this.Duration > 0)
			{
				this.Duration--;
				if (this.Duration > 0)
				{
					this.ApplyField();
				}
				else
				{
					base.PlayWorldSound("Sounds/Abilities/sfx_ability_mutation_timeDilation_deactivate", 0.5f, 0f, true, null, 0f, 1f, 1f, int.MaxValue);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C6DE RID: 50910 RVA: 0x004880B7 File Offset: 0x004862B7
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("time", this.BaseElementWeight);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C6DF RID: 50911 RVA: 0x004880DF File Offset: 0x004862DF
		public override string GetDescription()
		{
			return "You distort time around your person in order to slow down your enemies.";
		}

		// Token: 0x0600C6E0 RID: 50912 RVA: 0x004880E8 File Offset: 0x004862E8
		public override string GetLevelText(int Level)
		{
			return "Creatures within " + this.Range.ToString() + " tiles are slowed according to how close they are to you.\n" + "Distance 1: creatures receive a {{rules|" + ((int)(TimeDilation.CalculateQuicknessPenaltyMultiplier(1.0, this.Range, Level) * 100.0)).ToString() + "%}} Quickness penalty\n" + "Distance 4: creatures receive a {{rules|" + ((int)(TimeDilation.CalculateQuicknessPenaltyMultiplier(4.0, this.Range, Level) * 100.0)).ToString() + "%}} Quickness penalty\n" + "Distance 7: creatures receive a {{rules|" + ((int)(TimeDilation.CalculateQuicknessPenaltyMultiplier(7.0, this.Range, Level) * 100.0)).ToString() + "%}} Quickness penalty\n" + "Duration: 15 rounds\n" + "Cooldown: " + TimeDilation.GetCooldown(Level).ToString() + " rounds";
		}

		// Token: 0x0600C6E1 RID: 50913 RVA: 0x004881DC File Offset: 0x004863DC
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			stats.Set("Range", this.Range, false, 0);
			stats.Set("QuicknessPenalty1", (int)(TimeDilation.CalculateQuicknessPenaltyMultiplier(1.0, this.Range, Level) * 100.0), false, 0);
			stats.Set("QuicknessPenalty4", (int)(TimeDilation.CalculateQuicknessPenaltyMultiplier(4.0, this.Range, Level) * 100.0), false, 0);
			stats.Set("QuicknessPenalty7", (int)(TimeDilation.CalculateQuicknessPenaltyMultiplier(7.0, this.Range, Level) * 100.0), false, 0);
			stats.Set("Duration", 15, false, 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), TimeDilation.GetCooldown(Level), 0);
		}

		// Token: 0x0600C6E2 RID: 50914 RVA: 0x004882AC File Offset: 0x004864AC
		public static double CalculateQuicknessPenaltyMultiplier(double Distance, int Range, int MutationLevel)
		{
			return Math.Pow((double)((float)Range) - Distance, 2.0) * (double)(0.0005f * (float)MutationLevel + 0.0085f);
		}

		// Token: 0x0600C6E3 RID: 50915 RVA: 0x004882D4 File Offset: 0x004864D4
		public override bool Render(RenderEvent E)
		{
			if (this.Duration > 0)
			{
				Cell currentCell = this.ParentObject.CurrentCell;
				this.Frame--;
				if (this.Frame < 0)
				{
					this.Frame = 180;
					for (int i = 0; i < Stat.RandomCosmetic(1, 3); i++)
					{
						float num = (float)Stat.RandomCosmetic(4, 14) / 3f;
						for (int j = 0; j < 360; j++)
						{
							The.ParticleManager.Add("@", (float)currentCell.X, (float)currentCell.Y, (float)Math.Sin((double)((float)j) * 0.017) / num, (float)Math.Cos((double)((float)j) * 0.017) / num);
						}
					}
				}
			}
			return true;
		}

		// Token: 0x0600C6E4 RID: 50916 RVA: 0x00488398 File Offset: 0x00486598
		public static int GetCooldown(int Level)
		{
			return 150;
		}

		// Token: 0x0600C6E5 RID: 50917 RVA: 0x0048839F File Offset: 0x0048659F
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C6E6 RID: 50918 RVA: 0x004883A8 File Offset: 0x004865A8
		public override bool Mutate(GameObject GO, int Level)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Time Dilation", "CommandTimeDilation", "Mental Mutations", null, "ä", null, false, false, false, false, true, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C6E7 RID: 50919 RVA: 0x004883F1 File Offset: 0x004865F1
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.Unmutate(GO);
		}

		// Token: 0x0600C6E8 RID: 50920 RVA: 0x00488408 File Offset: 0x00486608
		public static void ApplyField(GameObject src, int Range = 9, bool Independent = false, int Duration = 15, int Level = 1, IPart Mutation = null)
		{
			if (src.IsValid() && !src.OnWorldMap())
			{
				Cell currentCell = src.CurrentCell;
				if (currentCell != null)
				{
					foreach (GameObject gameObject in currentCell.ParentZone.FastSquareVisibility(currentCell.X, currentCell.Y, Range, "Combat", src, false, true, null))
					{
						if (gameObject != src && gameObject.FireEvent("CanApplyTimeDilated") && IComponent<GameObject>.CheckRealityDistortionAccessibility(gameObject, null, src, null, Mutation, 0))
						{
							if (Independent)
							{
								double num = src.RealDistanceTo(gameObject);
								if (num <= (double)Range)
								{
									double num2 = TimeDilation.CalculateQuicknessPenaltyMultiplier(num, Range, Level);
									gameObject.ApplyEffect(new TimeDilatedIndependent(Duration, num2), null);
									IComponent<GameObject>.XDidY(src, "begin", "to distort around " + src.the + src.ShortDisplayNameWithoutTitles, null, "Time", null, null, null, false, false, null, null, false, false, false, false, false, null);
								}
							}
							else
							{
								gameObject.ApplyEffect(new TimeDilated(src), null);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600C6E9 RID: 50921 RVA: 0x0048852C File Offset: 0x0048672C
		public void ApplyField()
		{
			TimeDilation.ApplyField(this.ParentObject, this.Range, false, 15, 1, this);
		}

		// Token: 0x04004E45 RID: 20037
		public const int DEFAULT_RANGE = 9;

		// Token: 0x04004E46 RID: 20038
		public const int DEFAULT_DURATION = 15;

		// Token: 0x04004E47 RID: 20039
		public static readonly string COMMAND_NAME = "CommandTimeDilation";

		// Token: 0x04004E48 RID: 20040
		public int Duration;

		// Token: 0x04004E49 RID: 20041
		public int Range = 9;

		// Token: 0x04004E4A RID: 20042
		public int Frame;
	}
}

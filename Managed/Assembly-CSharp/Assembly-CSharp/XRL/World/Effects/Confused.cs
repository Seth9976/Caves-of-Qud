using System;
using System.Collections.Generic;
using System.Text;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000C47 RID: 3143
	[Serializable]
	public class Confused : Effect, ITierInitialized
	{
		// Token: 0x0600805B RID: 32859 RVA: 0x002F42D1 File Offset: 0x002F24D1
		public Confused()
		{
			this.DisplayName = "{{R|confused}}";
		}

		// Token: 0x0600805C RID: 32860 RVA: 0x002F42EF File Offset: 0x002F24EF
		public Confused(int Duration = 1, int Level = 0, int MentalPenalty = 0, string NameForChecks = "Confusion")
			: this()
		{
			this.Duration = Duration;
			this.Level = Level;
			this.MentalPenalty = MentalPenalty;
			this.NameForChecks = NameForChecks;
		}

		// Token: 0x0600805D RID: 32861 RVA: 0x002F4314 File Offset: 0x002F2514
		public void Initialize(int Tier)
		{
			Tier = Stat.Random(Tier - 2, Tier + 2);
			if (Tier < 1)
			{
				Tier = 1;
			}
			if (Tier > 8)
			{
				Tier = 8;
			}
			this.Duration = Stat.Random(15, 60);
			this.Level = Stat.Random(1, 10);
			this.MentalPenalty = this.Level;
		}

		// Token: 0x0600805E RID: 32862 RVA: 0x002F4365 File Offset: 0x002F2565
		public override int GetEffectType()
		{
			return 117440514;
		}

		// Token: 0x0600805F RID: 32863 RVA: 0x002F436C File Offset: 0x002F256C
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06008060 RID: 32864 RVA: 0x002F4370 File Offset: 0x002F2570
		public override string GetDetails()
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			stringBuilder.Append("Acts semi-randomly.\n-").Append(this.Level).Append(" DV\n-")
				.Append(this.Level)
				.Append(" MA");
			if (this.MentalPenalty > 0)
			{
				stringBuilder.Append("\n-").Append(this.MentalPenalty).Append(" to all mental attributes");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06008061 RID: 32865 RVA: 0x002F43EC File Offset: 0x002F25EC
		public override bool Apply(GameObject Object)
		{
			if (Object.Brain == null)
			{
				return false;
			}
			if (!Object.FireEvent("CanApplyConfusion"))
			{
				return false;
			}
			if (!Object.FireEvent("ApplyConfusion"))
			{
				return false;
			}
			if (!ApplyEffectEvent.Check(Object, this.NameForChecks, this, null))
			{
				return false;
			}
			if (Object.HasEffect<Confused>())
			{
				return false;
			}
			if (Object != null)
			{
				Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_bewilderment", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			this.ApplyChanges();
			base.DidX("become", "confused", "!", null, null, null, Object, false, false, null, null, false, false, false, false, false, null);
			return base.Apply(Object);
		}

		// Token: 0x06008062 RID: 32866 RVA: 0x002F449A File Offset: 0x002F269A
		public override void Remove(GameObject Object)
		{
			if (!Object.IsPlayer())
			{
				Object.Brain.Goals.Clear();
				Object.Brain.Target = null;
			}
			this.UnapplyChanges();
			base.Remove(Object);
		}

		// Token: 0x06008063 RID: 32867 RVA: 0x002F44D0 File Offset: 0x002F26D0
		private void ApplyChanges()
		{
			base.StatShifter.SetStatShift(base.Object, "DV", -this.Level, false);
			base.StatShifter.SetStatShift(base.Object, "MA", -this.Level, false);
			base.StatShifter.SetStatShift(base.Object, "Willpower", -this.MentalPenalty, false);
			base.StatShifter.SetStatShift(base.Object, "Intelligence", -this.MentalPenalty, false);
			base.StatShifter.SetStatShift(base.Object, "Ego", -this.MentalPenalty, false);
		}

		// Token: 0x06008064 RID: 32868 RVA: 0x002F4578 File Offset: 0x002F2778
		private void UnapplyChanges()
		{
			base.StatShifter.RemoveStatShifts();
		}

		// Token: 0x06008065 RID: 32869 RVA: 0x002F4585 File Offset: 0x002F2785
		public override bool Render(RenderEvent E)
		{
			if (this.Duration == 0)
			{
				return true;
			}
			E.RenderEffectIndicator("?", null, "&R", "R", 35, 25);
			return true;
		}

		// Token: 0x06008066 RID: 32870 RVA: 0x002F45AC File Offset: 0x002F27AC
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<IsConversationallyResponsiveEvent>.ID || ID == PooledEvent<GetCompanionStatusEvent>.ID || ID == GetLostChanceEvent.ID;
		}

		// Token: 0x06008067 RID: 32871 RVA: 0x002F45D2 File Offset: 0x002F27D2
		public override bool HandleEvent(GetLostChanceEvent E)
		{
			if (E.Actor == base.Object)
			{
				E.PercentageBonus -= this.Level * 100;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008068 RID: 32872 RVA: 0x002F45FF File Offset: 0x002F27FF
		public override bool HandleEvent(GetCompanionStatusEvent E)
		{
			if (E.Object == base.Object)
			{
				E.AddStatus("confused", 80);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008069 RID: 32873 RVA: 0x002F4624 File Offset: 0x002F2824
		public override bool HandleEvent(IsConversationallyResponsiveEvent E)
		{
			if (E.Speaker == base.Object)
			{
				if (E.Mental && !E.Physical)
				{
					E.Message = base.Object.Poss("mind", true) + " is in disarray.";
				}
				else
				{
					E.Message = base.Object.Does("don't", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " seem to understand you.";
				}
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600806A RID: 32874 RVA: 0x002F46BC File Offset: 0x002F28BC
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterDeepCopyWithoutEffects");
			Registrar.Register("BeforeDeepCopyWithoutEffects");
			Registrar.Register("BeginTakeAction");
			Registrar.Register("CanApplyConfusion");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600806B RID: 32875 RVA: 0x002F46F4 File Offset: 0x002F28F4
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeginTakeAction")
			{
				if (this.Duration > 0)
				{
					this.Duration--;
				}
				if (this.Duration > 0 && !base.Object.IsPlayer())
				{
					int num = Stat.Random(1, 100);
					if (num <= 50)
					{
						base.Object.Move(Directions.GetRandomDirection(), false, false, false, false, true, true, null, null, false, null, null, null, false, false, null, null, 0);
						base.Object.UseEnergy(base.Object.Energy.Value);
						return false;
					}
					if (num <= 60)
					{
						GameObject @object = base.Object;
						Cell currentCell = base.currentCell;
						GameObject gameObject;
						if (currentCell == null)
						{
							gameObject = null;
						}
						else
						{
							List<Cell> localAdjacentCells = currentCell.GetLocalAdjacentCells(5, false);
							if (localAdjacentCells == null)
							{
								gameObject = null;
							}
							else
							{
								Cell randomElement = localAdjacentCells.GetRandomElement<Cell>(null);
								if (randomElement == null)
								{
									gameObject = null;
								}
								else
								{
									List<GameObject> objectsInCell = randomElement.GetObjectsInCell();
									gameObject = ((objectsInCell != null) ? objectsInCell.GetRandomElement<GameObject>(null) : null);
								}
							}
						}
						@object.Target = gameObject;
						base.Object.UseEnergy(base.Object.Energy.Value);
						return false;
					}
				}
			}
			else
			{
				if (E.ID == "CanApplyConfusion")
				{
					return false;
				}
				if (E.ID == "BeforeDeepCopyWithoutEffects")
				{
					this.UnapplyChanges();
				}
				else if (E.ID == "AfterDeepCopyWithoutEffects")
				{
					this.ApplyChanges();
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x040038F7 RID: 14583
		public const string DEFAULT_CHECK_VS = "Confusion";

		// Token: 0x040038F8 RID: 14584
		public int Level;

		// Token: 0x040038F9 RID: 14585
		public int AppliedPlayerConfusion;

		// Token: 0x040038FA RID: 14586
		public int MentalPenalty;

		// Token: 0x040038FB RID: 14587
		public string NameForChecks = "Confusion";
	}
}

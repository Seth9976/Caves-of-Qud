using System;
using System.Collections.Generic;
using System.Text;
using XRL.Core;
using XRL.Rules;

namespace XRL.World.Effects
{
	// Token: 0x02000D3F RID: 3391
	[Serializable]
	public class FuriouslyConfused : Effect
	{
		// Token: 0x060085DD RID: 34269 RVA: 0x00303CA3 File Offset: 0x00301EA3
		public FuriouslyConfused()
		{
			this.DisplayName = "{{R|FURIOUS}}";
		}

		// Token: 0x060085DE RID: 34270 RVA: 0x00303CB6 File Offset: 0x00301EB6
		public FuriouslyConfused(int Duration, int Level, int MentalPenalty)
			: this()
		{
			this.Duration = Duration;
			this.Level = Level;
			this.MentalPenalty = MentalPenalty;
		}

		// Token: 0x060085DF RID: 34271 RVA: 0x00303CD3 File Offset: 0x00301ED3
		public override int GetEffectType()
		{
			return 117440514;
		}

		// Token: 0x060085E0 RID: 34272 RVA: 0x00303CDA File Offset: 0x00301EDA
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x060085E1 RID: 34273 RVA: 0x00303CE0 File Offset: 0x00301EE0
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

		// Token: 0x060085E2 RID: 34274 RVA: 0x00303D5C File Offset: 0x00301F5C
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
			if (!ApplyEffectEvent.Check(Object, "Confusion", this, null))
			{
				return false;
			}
			if (Object.HasEffect<Confused>())
			{
				return false;
			}
			if (Object != null)
			{
				Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_enraged_big", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			if (Object != null)
			{
				Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_bewilderment", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			this.ApplyChanges();
			base.DidX("become", "confused", "!", null, null, null, Object, false, false, null, null, false, false, false, false, false, null);
			return base.Apply(Object);
		}

		// Token: 0x060085E3 RID: 34275 RVA: 0x00303E36 File Offset: 0x00302036
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

		// Token: 0x060085E4 RID: 34276 RVA: 0x00303E6C File Offset: 0x0030206C
		private void ApplyChanges()
		{
			base.StatShifter.SetStatShift(base.Object, "DV", -this.Level, false);
			base.StatShifter.SetStatShift(base.Object, "MA", -this.Level, false);
			base.StatShifter.SetStatShift(base.Object, "Willpower", -this.MentalPenalty, false);
			base.StatShifter.SetStatShift(base.Object, "Intelligence", -this.MentalPenalty, false);
			base.StatShifter.SetStatShift(base.Object, "Ego", -this.MentalPenalty, false);
		}

		// Token: 0x060085E5 RID: 34277 RVA: 0x00303F14 File Offset: 0x00302114
		private void UnapplyChanges()
		{
			base.StatShifter.RemoveStatShifts();
		}

		// Token: 0x060085E6 RID: 34278 RVA: 0x00303F24 File Offset: 0x00302124
		public override bool Render(RenderEvent E)
		{
			if (this.Duration == 0)
			{
				return true;
			}
			int num = XRLCore.CurrentFrame % 60;
			if (num > 35 && num < 60)
			{
				E.Tile = null;
				E.RenderString = "?";
				E.ColorString = "&R";
			}
			return true;
		}

		// Token: 0x060085E7 RID: 34279 RVA: 0x00303F6C File Offset: 0x0030216C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == GetLostChanceEvent.ID;
		}

		// Token: 0x060085E8 RID: 34280 RVA: 0x00303F8C File Offset: 0x0030218C
		public override bool HandleEvent(BeginTakeActionEvent E)
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
			return base.HandleEvent(E);
		}

		// Token: 0x060085E9 RID: 34281 RVA: 0x00304097 File Offset: 0x00302297
		public override bool HandleEvent(GetLostChanceEvent E)
		{
			if (E.Actor == base.Object)
			{
				E.PercentageBonus -= this.Level * 100;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060085EA RID: 34282 RVA: 0x003040C4 File Offset: 0x003022C4
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterDeepCopyWithoutEffects");
			Registrar.Register("BeforeDeepCopyWithoutEffects");
			Registrar.Register("CanApplyConfusion");
			base.Register(Object, Registrar);
		}

		// Token: 0x060085EB RID: 34283 RVA: 0x003040F0 File Offset: 0x003022F0
		public override bool FireEvent(Event E)
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
			return base.FireEvent(E);
		}

		// Token: 0x040039C8 RID: 14792
		public int Level;

		// Token: 0x040039C9 RID: 14793
		public int AppliedPlayerConfusion;

		// Token: 0x040039CA RID: 14794
		public int MentalPenalty;
	}
}

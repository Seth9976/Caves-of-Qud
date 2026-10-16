using System;
using System.Collections.Generic;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000ECF RID: 3791
	[Serializable]
	public class Impaler : IPart
	{
		// Token: 0x060097D4 RID: 38868 RVA: 0x00371C4D File Offset: 0x0036FE4D
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID || ID == GetNavigationWeightEvent.ID || ID == PooledEvent<InterruptAutowalkEvent>.ID || ID == ObjectEnteredCellEvent.ID;
		}

		// Token: 0x060097D5 RID: 38869 RVA: 0x00371C7C File Offset: 0x0036FE7C
		public override bool HandleEvent(GetNavigationWeightEvent E)
		{
			if (E.Smart && (!this.NeedsToBeHidden || !this.ParentObject.HasPart<Hidden>()))
			{
				E.Uncacheable = true;
				if (E.Actor.IsCombatObject() && (this.ParentObject.IsHostileTowards(E.Actor) || (!this.RequiresHostility && (!this.NotIfAllied || !this.ParentObject.IsAlliedTowards(E.Actor)))) && this.ParentObject.PhaseAndFlightMatches(E.Actor))
				{
					E.MinWeight(99);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060097D6 RID: 38870 RVA: 0x00371D14 File Offset: 0x0036FF14
		public override bool HandleEvent(InterruptAutowalkEvent E)
		{
			if ((!this.NeedsToBeHidden || !this.ParentObject.HasPart<Hidden>()) && E.Actor.IsCombatObject() && (this.ParentObject.IsHostileTowards(E.Actor) || (!this.RequiresHostility && (!this.NotIfAllied || !this.ParentObject.IsAlliedTowards(E.Actor)))) && this.ParentObject.PhaseAndFlightMatches(E.Actor))
			{
				E.Because = "you don't want to step on " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " " + E.Actor.DescribeDirectionToward(this.ParentObject, false, false);
				E.IndicateObject = this.ParentObject;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060097D7 RID: 38871 RVA: 0x00371DFC File Offset: 0x0036FFFC
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (this.HasOtherImpaler(E.Cell) && !this.ParentObject.IsCombat)
			{
				this.ParentObject.Obliterate(null, false, null);
				return false;
			}
			if (this.ClusterSize != "1")
			{
				List<Cell> list = new List<Cell>(this.ParentObject.CurrentCell.GetLocalEmptyAdjacentCells()).ShuffleInPlace<Cell>(null);
				int num = this.ClusterSize.RollCached();
				int num2 = 0;
				while (num2 < num && num2 < list.Count)
				{
					Cell cell = list[num2];
					if (this.HasOtherImpaler(cell) || (!this.AlternateBlueprint.IsNullOrEmpty() && cell.HasObject(this.AlternateBlueprint)))
					{
						num++;
					}
					else if (this.AlternateBlueprintChance.in100())
					{
						cell.AddObject(this.AlternateBlueprint, null, null, null, null);
					}
					else
					{
						GameObject gameObject = GameObject.Create(this.ParentObject.Blueprint);
						gameObject.Brain.TakeBaseAllegiance(this.ParentObject);
						gameObject.GetPart<Impaler>().ClusterSize = "1";
						cell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					}
					num2++;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060097D8 RID: 38872 RVA: 0x00371F34 File Offset: 0x00370134
		public override bool HandleEvent(ObjectEnteredCellEvent E)
		{
			if (E.Object != this.ParentObject && E.Object.IsCombatObject())
			{
				if (this.ParentObject.IsHostileTowards(E.Object) || (!this.RequiresHostility && (!this.NotIfAllied || !this.ParentObject.IsAlliedTowards(E.Object))))
				{
					if (this.ParentObject.PhaseAndFlightMatches(E.Object))
					{
						Hidden part = this.ParentObject.GetPart<Hidden>();
						if (!this.NeedsToBeHidden || (part != null && !part.Found))
						{
							if (E.Object.IsVisible())
							{
								string text = GameText.VariableReplace(this.Message, this.ParentObject, E.Object, false);
								if (!text.IsNullOrEmpty())
								{
									IComponent<GameObject>.AddPlayerMessage(text, null, true);
								}
								string text2 = GameText.VariableReplace(this.FloatMessage, this.ParentObject, E.Object, true);
								if (!text2.IsNullOrEmpty())
								{
									E.Object.ParticleText(text2, IComponent<GameObject>.ConsequentialColorChar(null, E.Object), false, 1.5f, -8f);
								}
								if (part != null)
								{
									part.Reveal(true);
								}
							}
							GameObject @object = E.Object;
							int num = this.Damage.RollCached();
							string damageAttributes = this.DamageAttributes;
							GameObject parentObject = this.ParentObject;
							if (@object.TakeDamage(num, this.DamageMessage, damageAttributes, null, null, parentObject, null, null, null, null, false, false, false, false, false, false, false, false, false, 5, null) && E.Object.IsValid())
							{
								E.Object.Bloodsplatter();
								E.Object.ApplyEffect(new Bleeding(this.BleedDamage, this.BleedSave, this.ParentObject, false, false, false, false), null);
							}
							if (this.DestroyOnStrike)
							{
								this.ParentObject.Destroy(null, false, false, null);
							}
						}
					}
				}
				else if (E.Object.IsPlayer())
				{
					base.Reveal();
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060097D9 RID: 38873 RVA: 0x00372112 File Offset: 0x00370312
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x060097DA RID: 38874 RVA: 0x00372118 File Offset: 0x00370318
		public bool HasOtherImpaler(Cell C)
		{
			int i = 0;
			int count = C.Objects.Count;
			while (i < count)
			{
				if (C.Objects[i] != this.ParentObject && C.Objects[i].HasPart<Impaler>())
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x04003F21 RID: 16161
		public string ClusterSize = "1";

		// Token: 0x04003F22 RID: 16162
		public string Damage = "1d10";

		// Token: 0x04003F23 RID: 16163
		public string BleedDamage = "1d2";

		// Token: 0x04003F24 RID: 16164
		public string AlternateBlueprint = "Luminous Hoarshroom";

		// Token: 0x04003F25 RID: 16165
		public string Message = "{{R|=subject.An= =verb:strike==subject.directionIfAny=!}}";

		// Token: 0x04003F26 RID: 16166
		public string DamageAttributes = "Stabbing";

		// Token: 0x04003F27 RID: 16167
		public string DamageMessage = "from %t impalement.";

		// Token: 0x04003F28 RID: 16168
		public string FloatMessage = "*impaled*";

		// Token: 0x04003F29 RID: 16169
		public int AlternateBlueprintChance = 15;

		// Token: 0x04003F2A RID: 16170
		public int BleedSave = 20;

		// Token: 0x04003F2B RID: 16171
		public bool RequiresHostility = true;

		// Token: 0x04003F2C RID: 16172
		public bool NotIfAllied = true;

		// Token: 0x04003F2D RID: 16173
		public bool NeedsToBeHidden = true;

		// Token: 0x04003F2E RID: 16174
		public bool DestroyOnStrike;
	}
}

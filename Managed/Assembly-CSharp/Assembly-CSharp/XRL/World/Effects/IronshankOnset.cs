using System;
using XRL.World.Parts;

namespace XRL.World.Effects
{
	// Token: 0x02000D24 RID: 3364
	[Serializable]
	public class IronshankOnset : Effect, ITierInitialized
	{
		// Token: 0x06008496 RID: 33942 RVA: 0x002FE9BA File Offset: 0x002FCBBA
		public IronshankOnset()
		{
			this.DisplayName = "stiff legs";
		}

		// Token: 0x06008497 RID: 33943 RVA: 0x002FE9CD File Offset: 0x002FCBCD
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06008498 RID: 33944 RVA: 0x002FE9D0 File Offset: 0x002FCBD0
		public override int GetEffectType()
		{
			return 100679700;
		}

		// Token: 0x06008499 RID: 33945 RVA: 0x002FE9D7 File Offset: 0x002FCBD7
		public override string GetDetails()
		{
			return "Legs ache at the joints.";
		}

		// Token: 0x0600849A RID: 33946 RVA: 0x002FE9E0 File Offset: 0x002FCBE0
		public override bool Apply(GameObject Object)
		{
			if (!Object.HasStat("MoveSpeed"))
			{
				return false;
			}
			if (!Ironshank.IsInfectable(Object))
			{
				return false;
			}
			if (Object.HasEffect<Ironshank>() || Object.HasEffect<IronshankOnset>())
			{
				return false;
			}
			if (!Object.FireEvent("ApplyDiseaseOnset"))
			{
				return false;
			}
			if (!ApplyEffectEvent.Check(Object, "DiseaseOnset", this, null))
			{
				return false;
			}
			if (!Object.FireEvent("ApplyIronshank"))
			{
				return false;
			}
			if (!ApplyEffectEvent.Check(Object, "Ironshank", this, null))
			{
				return false;
			}
			this.Duration = 1;
			return true;
		}

		// Token: 0x0600849B RID: 33947 RVA: 0x002FEA60 File Offset: 0x002FCC60
		public override string GetDescription()
		{
			return "stiff legs";
		}

		// Token: 0x0600849C RID: 33948 RVA: 0x002FEA67 File Offset: 0x002FCC67
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDiseaseOnsetEvent>.ID || ID == ModifyDefendingSaveEvent.ID;
		}

		// Token: 0x0600849D RID: 33949 RVA: 0x002FEA88 File Offset: 0x002FCC88
		public override bool HandleEvent(GetDiseaseOnsetEvent E)
		{
			if (E.Effect == null)
			{
				E.Effect = this;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600849E RID: 33950 RVA: 0x002FEAB0 File Offset: 0x002FCCB0
		public override bool HandleEvent(ModifyDefendingSaveEvent E)
		{
			if (this.Bonus != 0 && E.Vs == "Ironshank Disease Onset")
			{
				E.Roll += this.Bonus;
				if (E.Actual)
				{
					this.Bonus = 0;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600849F RID: 33951 RVA: 0x002FEB00 File Offset: 0x002FCD00
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("DrinkingFrom");
			Registrar.Register("Eating");
			Registrar.Register("EndTurn");
			base.Register(Object, Registrar);
		}

		// Token: 0x060084A0 RID: 33952 RVA: 0x002FEB2C File Offset: 0x002FCD2C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EndTurn")
			{
				if (!Ironshank.IsInfectable(base.Object))
				{
					this.Duration = 0;
				}
				else
				{
					this.Count++;
					if (this.Count >= 1200)
					{
						this.Count = 0;
						this.Days++;
						if (base.Object.MakeSave("Toughness", 13, null, null, "Ironshank Disease Onset", false, false, false, false, null))
						{
							this.Stage--;
							if (this.SawSore && this.Stage > -2 && base.Object.IsPlayer())
							{
								IComponent<GameObject>.AddPlayerMessage("You feel a bit better.", null, true);
							}
						}
						else
						{
							this.Stage++;
							if (this.Stage < 3)
							{
								if (base.Object.IsPlayer())
								{
									IComponent<GameObject>.AddPlayerMessage("Your legs ache at the joints.", null, true);
								}
								this.SawSore = true;
							}
						}
						if (this.Stage <= -2)
						{
							if (this.SawSore && base.Object.IsPlayer())
							{
								IComponent<GameObject>.AddPlayerMessage("You feel better.", null, true);
							}
							this.Duration = 0;
							return true;
						}
						if (this.Stage >= 3 || this.Days >= 5)
						{
							this.Duration = 0;
							base.Object.ApplyEffect(new Ironshank(), null);
						}
					}
				}
			}
			else if (E.ID == "DrinkingFrom")
			{
				LiquidVolume liquidVolume = E.GetGameObjectParameter("Container").LiquidVolume;
				if ((liquidVolume.ComponentLiquids.ContainsKey("honey") || liquidVolume.ComponentLiquids.ContainsKey("gel")) && this.Bonus < 2)
				{
					this.Bonus = 2;
				}
			}
			else if (E.ID == "Eating")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Food");
				if (gameObjectParameter == null)
				{
					return true;
				}
				if (this.Bonus > 0)
				{
					return true;
				}
				if (gameObjectParameter.Blueprint.Contains("Yuckwheat") && this.Bonus < 2)
				{
					this.Bonus = 2;
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003988 RID: 14728
		public int Stage;

		// Token: 0x04003989 RID: 14729
		public int Bonus;

		// Token: 0x0400398A RID: 14730
		public int Days;

		// Token: 0x0400398B RID: 14731
		public int Count;

		// Token: 0x0400398C RID: 14732
		public bool SawSore;

		// Token: 0x0400398D RID: 14733
		public bool Mature;
	}
}

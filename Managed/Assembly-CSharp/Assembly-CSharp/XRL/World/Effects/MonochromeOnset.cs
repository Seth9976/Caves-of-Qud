using System;
using XRL.World.Parts;

namespace XRL.World.Effects
{
	// Token: 0x02000D26 RID: 3366
	[Serializable]
	public class MonochromeOnset : Effect, ITierInitialized
	{
		// Token: 0x060084AD RID: 33965 RVA: 0x002FF294 File Offset: 0x002FD494
		public MonochromeOnset()
		{
			this.DisplayName = "blurry vision";
		}

		// Token: 0x060084AE RID: 33966 RVA: 0x002FF2A7 File Offset: 0x002FD4A7
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x060084AF RID: 33967 RVA: 0x002FF2AA File Offset: 0x002FD4AA
		public override string GetDetails()
		{
			return "Vision is blurred.";
		}

		// Token: 0x060084B0 RID: 33968 RVA: 0x002FF2B4 File Offset: 0x002FD4B4
		public override bool Apply(GameObject Object)
		{
			if (Object.HasEffect<Monochrome>())
			{
				return false;
			}
			if (Object.HasEffect<MonochromeOnset>())
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
			if (!Object.FireEvent("ApplyMonochrome"))
			{
				return false;
			}
			if (!ApplyEffectEvent.Check(Object, "Monochrome", this, null))
			{
				return false;
			}
			this.Duration = 1;
			return true;
		}

		// Token: 0x060084B1 RID: 33969 RVA: 0x002FF31D File Offset: 0x002FD51D
		public override int GetEffectType()
		{
			return 100679700;
		}

		// Token: 0x060084B2 RID: 33970 RVA: 0x002FF324 File Offset: 0x002FD524
		public override string GetDescription()
		{
			return "blurry vision";
		}

		// Token: 0x060084B3 RID: 33971 RVA: 0x002FF32B File Offset: 0x002FD52B
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDiseaseOnsetEvent>.ID || ID == ModifyDefendingSaveEvent.ID;
		}

		// Token: 0x060084B4 RID: 33972 RVA: 0x002FF34C File Offset: 0x002FD54C
		public override bool HandleEvent(GetDiseaseOnsetEvent E)
		{
			if (E.Effect == null)
			{
				E.Effect = this;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060084B5 RID: 33973 RVA: 0x002FF374 File Offset: 0x002FD574
		public override bool HandleEvent(ModifyDefendingSaveEvent E)
		{
			if (this.Bonus != 0 && E.Vs == "Monochrome Disease Onset")
			{
				E.Roll += this.Bonus;
				if (E.Actual)
				{
					this.Bonus = 0;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060084B6 RID: 33974 RVA: 0x002FF3C4 File Offset: 0x002FD5C4
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("DrinkingFrom");
			Registrar.Register("Eating");
			Registrar.Register("EndTurn");
			base.Register(Object, Registrar);
		}

		// Token: 0x060084B7 RID: 33975 RVA: 0x002FF3F0 File Offset: 0x002FD5F0
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EndTurn")
			{
				this.Count++;
				if (this.Count >= 1200)
				{
					this.Count = 0;
					this.Days++;
					if (base.Object.MakeSave("Toughness", 13, null, null, "Monochrome Disease Onset", false, false, false, false, null))
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
								IComponent<GameObject>.AddPlayerMessage("Your vision blurs.", null, true);
							}
							this.SawSore = true;
						}
					}
					if (this.Stage <= -2)
					{
						if (this.SawSore && base.Object.IsPlayer())
						{
							IComponent<GameObject>.AddPlayerMessage("Your vision clears up.", null, true);
						}
						this.Duration = 0;
						return true;
					}
					if (this.Stage >= 3 || this.Days >= 5)
					{
						this.Duration = 0;
						base.Object.ApplyEffect(new Monochrome(), null);
					}
				}
			}
			else if (E.ID == "DrinkingFrom")
			{
				LiquidVolume liquidVolume = E.GetGameObjectParameter("Container").LiquidVolume;
				if (liquidVolume != null && liquidVolume.HasPrimaryOrSecondaryLiquid("honey") && this.Bonus < 2)
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

		// Token: 0x04003992 RID: 14738
		public int Stage;

		// Token: 0x04003993 RID: 14739
		public int Bonus;

		// Token: 0x04003994 RID: 14740
		public int Days;

		// Token: 0x04003995 RID: 14741
		public int Count;

		// Token: 0x04003996 RID: 14742
		public bool SawSore;

		// Token: 0x04003997 RID: 14743
		public bool Mature;
	}
}

using System;
using XRL.UI;
using XRL.World.Parts;

namespace XRL.World.Effects
{
	// Token: 0x02000D22 RID: 3362
	[Serializable]
	public class GlotrotOnset : Effect, ITierInitialized
	{
		// Token: 0x06008477 RID: 33911 RVA: 0x002FDC14 File Offset: 0x002FBE14
		public GlotrotOnset()
		{
			this.DisplayName = "{{r|sore throat}}";
		}

		// Token: 0x06008478 RID: 33912 RVA: 0x002FDC27 File Offset: 0x002FBE27
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06008479 RID: 33913 RVA: 0x002FDC2A File Offset: 0x002FBE2A
		public override int GetEffectType()
		{
			return 100679700;
		}

		// Token: 0x0600847A RID: 33914 RVA: 0x002FDC31 File Offset: 0x002FBE31
		public override string GetDetails()
		{
			return "Throat is sore and tongue is swollen.";
		}

		// Token: 0x0600847B RID: 33915 RVA: 0x002FDC38 File Offset: 0x002FBE38
		public override bool Apply(GameObject Object)
		{
			if (Object.HasEffect<Glotrot>() || Object.HasEffect<GlotrotOnset>())
			{
				return false;
			}
			if (Object.FireEvent("ApplyDiseaseOnset") && ApplyEffectEvent.Check(Object, "DiseaseOnset", this, null) && Object.FireEvent("ApplyGlotrot") && ApplyEffectEvent.Check(Object, "Glotrot", this, null))
			{
				this.Duration = 1;
				return true;
			}
			return false;
		}

		// Token: 0x0600847C RID: 33916 RVA: 0x002FDC99 File Offset: 0x002FBE99
		public override string GetDescription()
		{
			return "{{r|sore throat}}";
		}

		// Token: 0x0600847D RID: 33917 RVA: 0x002FDCA0 File Offset: 0x002FBEA0
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDiseaseOnsetEvent>.ID || ID == ModifyDefendingSaveEvent.ID;
		}

		// Token: 0x0600847E RID: 33918 RVA: 0x002FDCC0 File Offset: 0x002FBEC0
		public override bool HandleEvent(GetDiseaseOnsetEvent E)
		{
			if (E.Effect == null)
			{
				E.Effect = this;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600847F RID: 33919 RVA: 0x002FDCE8 File Offset: 0x002FBEE8
		public override bool HandleEvent(ModifyDefendingSaveEvent E)
		{
			if (this.Bonus != 0 && E.Vs == "Glotrot Disease Onset")
			{
				E.Roll += this.Bonus;
				if (E.Actual)
				{
					this.Bonus = 0;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008480 RID: 33920 RVA: 0x002FDD38 File Offset: 0x002FBF38
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("Eating");
			Registrar.Register("EndTurn");
			Registrar.Register("DrinkingFrom");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008481 RID: 33921 RVA: 0x002FDD64 File Offset: 0x002FBF64
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EndTurn")
			{
				this.Count++;
				if (this.Count >= 1200 && this.Duration > 0)
				{
					this.Count = 0;
					this.Days++;
					if (base.Object.MakeSave("Toughness", 13, null, null, "Glotrot Disease Onset", false, false, false, false, null))
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
								IComponent<GameObject>.AddPlayerMessage("Your throat feels sore.", null, true);
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
					}
					else if (this.Stage >= 3 || this.Days >= 5)
					{
						this.Duration = 0;
						base.Object.ApplyEffect(new Glotrot(), null);
					}
				}
			}
			else if (E.ID == "DrinkingFrom")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Container");
				if (Glotrot.IsFlamingIck(gameObjectParameter))
				{
					this.Duration = 0;
					if (base.Object.IsPlayer())
					{
						Popup.Show("It tastes even worse than you had imagined -- like a dead turtle boiled in phlegm.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
				}
				else
				{
					LiquidVolume liquidVolume = gameObjectParameter.LiquidVolume;
					if (liquidVolume != null && liquidVolume.HasPrimaryOrSecondaryLiquid("honey") && this.Bonus < 3)
					{
						this.Bonus = 3;
					}
				}
			}
			else if (E.ID == "Eating")
			{
				GameObject gameObjectParameter2 = E.GetGameObjectParameter("Food");
				if (gameObjectParameter2 == null)
				{
					return true;
				}
				if (this.Bonus > 0)
				{
					return true;
				}
				if (gameObjectParameter2.Blueprint.Contains("Yuckwheat") && this.Bonus < 3)
				{
					this.Bonus = 3;
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0400397F RID: 14719
		public int Stage;

		// Token: 0x04003980 RID: 14720
		public int Bonus;

		// Token: 0x04003981 RID: 14721
		public int Days;

		// Token: 0x04003982 RID: 14722
		public int Count;

		// Token: 0x04003983 RID: 14723
		public bool SawSore;
	}
}

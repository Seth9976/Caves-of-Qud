using System;
using System.Text;
using ConsoleLib.Console;
using HistoryKit;
using XRL.Core;
using XRL.Rules;
using XRL.UI;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000D83 RID: 3459
	[Serializable]
	public class ShadeOil_Tonic : ITonicEffect, ITierInitialized
	{
		// Token: 0x06008965 RID: 35173 RVA: 0x00312532 File Offset: 0x00310732
		public ShadeOil_Tonic()
		{
		}

		// Token: 0x06008966 RID: 35174 RVA: 0x00312545 File Offset: 0x00310745
		public ShadeOil_Tonic(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x06008967 RID: 35175 RVA: 0x00312554 File Offset: 0x00310754
		public void Initialize(int Tier)
		{
			if (If.CoinFlip(null, null))
			{
				this.bOverdose = true;
			}
			this.Duration = Stat.Roll(41, 50);
		}

		// Token: 0x06008968 RID: 35176 RVA: 0x00312575 File Offset: 0x00310775
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06008969 RID: 35177 RVA: 0x00312578 File Offset: 0x00310778
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x0600896A RID: 35178 RVA: 0x0031257B File Offset: 0x0031077B
		public override int GetEffectType()
		{
			return base.GetEffectType() | 256;
		}

		// Token: 0x0600896B RID: 35179 RVA: 0x00312589 File Offset: 0x00310789
		public override string GetDescription()
		{
			return "{{shade|shade oil}} tonic";
		}

		// Token: 0x0600896C RID: 35180 RVA: 0x00312590 File Offset: 0x00310790
		public override string GetStateDescription()
		{
			return "under the effects of {{shade|shade oil}} tonic";
		}

		// Token: 0x0600896D RID: 35181 RVA: 0x00312597 File Offset: 0x00310797
		public override string GetDetails()
		{
			if (base.Object.IsTrueKin())
			{
				return "+8 DV\nCan phase out of the spatial dimension for 7-9 turns.";
			}
			return "+8 DV\n25% chance per turn to phase out of the spatial dimension for 1-3 turns.";
		}

		// Token: 0x0600896E RID: 35182 RVA: 0x003125B4 File Offset: 0x003107B4
		public override bool Apply(GameObject Object)
		{
			base.DidX("begin", "to flicker in and out of corporeality", null, null, null, Object, null, false, false, null, null, false, false, false, true, false, null);
			this.ApplyChanges(false);
			if (Object.GetLongProperty("Overdosing", 0L) == 1L || this.bOverdose)
			{
				this.FireEvent(Event.New("Overdose", 0, 0, 0));
			}
			return true;
		}

		// Token: 0x0600896F RID: 35183 RVA: 0x00312618 File Offset: 0x00310818
		public override void Remove(GameObject Object)
		{
			base.DidX("assume", "full corporeal form again", null, null, null, null, null, false, false, null, null, false, false, false, false, false, null);
			this.UnapplyChanges();
			base.Remove(Object);
		}

		// Token: 0x06008970 RID: 35184 RVA: 0x00312654 File Offset: 0x00310854
		private void ApplyChanges(bool Silent = false)
		{
			base.StatShifter.SetStatShift(base.Object, "DV", 8, false);
			if (base.Object.IsTrueKin())
			{
				this.ActivatedAbilityID = base.Object.AddActivatedAbility("Phase", "ShadeOilPhase", "Tonics", null, "°", null, false, false, false, false, false, false, Silent, false, true, true, false, false, -1, null, Renderable.UITile("Mutations/phasing.bmp", 'W', 'C', " ", '\0'), null, null, null);
			}
		}

		// Token: 0x06008971 RID: 35185 RVA: 0x003126D3 File Offset: 0x003108D3
		private void UnapplyChanges()
		{
			base.StatShifter.RemoveStatShifts(base.Object);
			base.Object.RemoveActivatedAbility(ref this.ActivatedAbilityID);
		}

		// Token: 0x06008972 RID: 35186 RVA: 0x003126F8 File Offset: 0x003108F8
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID || ID == PooledEvent<GetPsionicSifrahSetupEvent>.ID;
		}

		// Token: 0x06008973 RID: 35187 RVA: 0x0031271E File Offset: 0x0031091E
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.ActivatedAbilityID, new Action<Templates.StatCollector>(this.GetStats), null);
			return base.HandleEvent(E);
		}

		// Token: 0x06008974 RID: 35188 RVA: 0x00312741 File Offset: 0x00310941
		public void GetStats(Templates.StatCollector stats)
		{
			stats.Set("Duration", "7-9 rounds", false, 0);
		}

		// Token: 0x06008975 RID: 35189 RVA: 0x00312755 File Offset: 0x00310955
		public override bool HandleEvent(GetPsionicSifrahSetupEvent E)
		{
			if (E.Type == "RealityDistortion")
			{
				E.Rating++;
				E.Turns++;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008976 RID: 35190 RVA: 0x0031278C File Offset: 0x0031098C
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			if (this.Duration > 0 && !base.Object.IsTrueKin() && !base.Object.OnWorldMap() && !base.Object.HasEffect<Phased>() && 25.in100())
			{
				base.Object.ApplyEffect(new Phased(Stat.Random(1, 3)), null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008977 RID: 35191 RVA: 0x003127F2 File Offset: 0x003109F2
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterDeepCopyWithoutEffects");
			Registrar.Register("BeforeDeepCopyWithoutEffects");
			Registrar.Register("Overdose");
			Registrar.Register("ShadeOilPhase");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008978 RID: 35192 RVA: 0x00312828 File Offset: 0x00310A28
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ShadeOilPhase")
			{
				if (base.Object.OnWorldMap())
				{
					if (base.Object.IsPlayer())
					{
						Popup.ShowFail("You cannot do that on the world map.", true, true, true);
					}
				}
				else
				{
					base.Object.RemoveActivatedAbility(ref this.ActivatedAbilityID);
					base.Object.ApplyEffect(new Phased(Stat.Random(7, 9)), null);
				}
			}
			else if (E.ID == "TonicAutoApplied")
			{
				if (this.Duration > 0 && !base.Object.HasEffect<Phased>())
				{
					if (base.Object.IsTrueKin())
					{
						StringBuilder stringBuilder = Event.NewStringBuilder(null);
						GameObject gameObjectParameter = E.GetGameObjectParameter("By");
						stringBuilder.Append(ColorUtility.CapitalizeExceptFormatting(this.GetDescription())).Append(" has been applied");
						if (gameObjectParameter != null)
						{
							stringBuilder.Append(" by ");
							if (!gameObjectParameter.HasProperName)
							{
								if (gameObjectParameter.GetObjectContext() == base.Object)
								{
									stringBuilder.Append("your ");
								}
								else
								{
									stringBuilder.Append(gameObjectParameter.a);
								}
							}
							stringBuilder.Append(gameObjectParameter.ShortDisplayName);
						}
						stringBuilder.Append(". Do you wish to phase out immediately?");
						if (Popup.ShowYesNo(stringBuilder.ToString(), "Sounds/UI/ui_notification", false, DialogResult.Yes, null) == DialogResult.Yes)
						{
							this.FireEvent(Event.New("ShadeOilPhase", 0, 0, 0));
						}
					}
					else if (25.in100())
					{
						base.Object.ApplyEffect(new Phased(Stat.Random(1, 3)), null);
					}
				}
			}
			else if (E.ID == "Overdose")
			{
				if (this.Duration > 0 && !base.Object.HasProperty("EvilTwin"))
				{
					this.Duration = 0;
					ShadeOil_Tonic.ApplyOverdose(base.Object);
				}
			}
			else if (E.ID == "BeforeDeepCopyWithoutEffects")
			{
				this.UnapplyChanges();
			}
			else if (E.ID == "AfterDeepCopyWithoutEffects")
			{
				this.ApplyChanges(false);
			}
			return base.FireEvent(E);
		}

		// Token: 0x06008979 RID: 35193 RVA: 0x00312A43 File Offset: 0x00310C43
		public override void ApplyAllergy(GameObject Object)
		{
			ShadeOil_Tonic.ApplyOverdose(Object);
		}

		// Token: 0x0600897A RID: 35194 RVA: 0x00312A4C File Offset: 0x00310C4C
		public static void ApplyOverdose(GameObject Object)
		{
			if (Object.IsPlayer())
			{
				if (Object.GetLongProperty("Overdosing", 0L) == 1L)
				{
					Popup.Show("Your mutant physiology reacts adversely to the tonic. You flicker through spacetime uncontrollably.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				else
				{
					Popup.Show("The tonics you ingested react adversely to each other. You flicker through spacetime uncontrollably.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
			}
			int i = 0;
			int num = Stat.Random(1, 3);
			while (i < num)
			{
				EvilTwin.CreateEvilTwin(Object, "Shadow", null, "{{c|You sense a shadowy presence nearby.}}", "&K", null, null, true, "It's shadow you.", "Entropic-100");
				i++;
			}
		}

		// Token: 0x0600897B RID: 35195 RVA: 0x00312AD8 File Offset: 0x00310CD8
		public override bool Render(RenderEvent E)
		{
			int num = XRLCore.CurrentFrame % 60;
			if (this.Duration > 0 && num > 0 && num < 10)
			{
				E.Tile = null;
				E.RenderString = "@";
				E.ColorString = "&K";
			}
			return true;
		}

		// Token: 0x04003A7B RID: 14971
		public bool bOverdose;

		// Token: 0x04003A7C RID: 14972
		public Guid ActivatedAbilityID = Guid.Empty;
	}
}

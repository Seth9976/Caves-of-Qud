using System;
using XRL.Core;
using XRL.Rules;
using XRL.UI;
using XRL.World.Capabilities;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;

namespace XRL.World.Effects
{
	// Token: 0x02000D8F RID: 3471
	[Serializable]
	public class SphynxSalt_Tonic : ITonicEffect, ITierInitialized
	{
		// Token: 0x06008A2C RID: 35372 RVA: 0x00314DE5 File Offset: 0x00312FE5
		public SphynxSalt_Tonic()
		{
		}

		// Token: 0x06008A2D RID: 35373 RVA: 0x00314DED File Offset: 0x00312FED
		public SphynxSalt_Tonic(int Duration)
			: this()
		{
			this.Duration = Duration;
		}

		// Token: 0x06008A2E RID: 35374 RVA: 0x00314DFC File Offset: 0x00312FFC
		public void Initialize(int Tier)
		{
			if (50.in100())
			{
				this.Overdose = true;
			}
			this.Duration = Stat.Roll(20, 40);
		}

		// Token: 0x06008A2F RID: 35375 RVA: 0x00314E1D File Offset: 0x0031301D
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x06008A30 RID: 35376 RVA: 0x00314E20 File Offset: 0x00313020
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x06008A31 RID: 35377 RVA: 0x00314E23 File Offset: 0x00313023
		public override string GetDescription()
		{
			return "{{sphynx|sphynx}} {{Y|salt}} tonic";
		}

		// Token: 0x06008A32 RID: 35378 RVA: 0x00314E2A File Offset: 0x0031302A
		public override string GetStateDescription()
		{
			return "under the effects of {{sphynx|sphynx}} {{Y|salt}} tonic";
		}

		// Token: 0x06008A33 RID: 35379 RVA: 0x00314E34 File Offset: 0x00313034
		public override string GetDetails()
		{
			string text;
			if (base.Object.IsTrueKin())
			{
				text = "Immune to confusing attacks.\nCan peer into the near future.";
			}
			else
			{
				text = "Immune to confusing attacks.\nActivated mental mutations cool down twice as quickly.";
			}
			if (Options.AnySifrah)
			{
				text += "\nAdds a bonus turn in psionic Sifrah games.";
			}
			return text;
		}

		// Token: 0x06008A34 RID: 35380 RVA: 0x00314E70 File Offset: 0x00313070
		public override bool Apply(GameObject Object)
		{
			if (Object.IsPlayer())
			{
				Popup.Show("The clouds part in your mind and a ray of clarity strikes through.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			if (Object.GetLongProperty("Overdosing", 0L) == 1L || this.Overdose)
			{
				this.FireEvent(Event.New("Overdose", 0, 0, 0));
				if (Options.AnySifrah)
				{
					PsionicSifrah.AwardInsight();
				}
			}
			else if (Options.AnySifrah && 30.in100())
			{
				PsionicSifrah.AwardInsight();
			}
			int num = 0;
			while (Object.HasEffect<Confused>() && ++num < 20)
			{
				Object.RemoveEffect<Confused>();
			}
			if (Object.HasRegisteredEvent("InitiatePrecognition"))
			{
				Event @event = Event.New("InitiatePrecognition", "Duration", this.Duration);
				Object.FireEvent(@event);
				this.Duration = @event.GetIntParameter("Duration", 0);
			}
			SphynxSalt_Tonic effect = Object.GetEffect<SphynxSalt_Tonic>();
			if (effect != null)
			{
				effect.Duration += this.Duration;
				return false;
			}
			if (Object.IsPlayer())
			{
				this.WasPlayer = true;
				if (Object.IsTrueKin())
				{
					this.GlimpseID = Precognition.Save();
				}
			}
			else
			{
				this.WasPlayer = false;
				if (SensePsychic.SensePsychicFromPlayer(Object) != null)
				{
					IComponent<GameObject>.AddPlayerMessage("You sense a subtle psychic disturbance.", null, true);
				}
			}
			this.HitpointsAtSave = Object.hitpoints;
			this.TemperatureAtSave = Object.Physics.Temperature;
			return true;
		}

		// Token: 0x06008A35 RID: 35381 RVA: 0x00314FC0 File Offset: 0x003131C0
		public override void Remove(GameObject Object)
		{
			if (Object.IsPlayer())
			{
				if (Object.IsTrueKin() && this.WasPlayer)
				{
					if (Popup.ShowYesNo("Your {{sphynx|sphynx}} {{Y|salt}} is about to run out. Would you like to return to the start of your vision?", "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.Yes)
					{
						AutoAct.Interrupt(null, null, null, false);
						Precognition.Load(this.GlimpseID, Object);
					}
				}
				else
				{
					Popup.Show("Your mind clouds over once again.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
			}
			base.Remove(Object);
		}

		// Token: 0x06008A36 RID: 35382 RVA: 0x0031502C File Offset: 0x0031322C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == PooledEvent<GetPsionicSifrahSetupEvent>.ID;
		}

		// Token: 0x06008A37 RID: 35383 RVA: 0x0031504A File Offset: 0x0031324A
		public override bool HandleEvent(GetPsionicSifrahSetupEvent E)
		{
			E.Turns++;
			return base.HandleEvent(E);
		}

		// Token: 0x06008A38 RID: 35384 RVA: 0x00315064 File Offset: 0x00313264
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			if (this.Duration > 0)
			{
				ActivatedAbilities activatedAbilities = base.Object.ActivatedAbilities;
				if (((activatedAbilities != null) ? activatedAbilities.AbilityByGuid : null) != null)
				{
					foreach (ActivatedAbilityEntry activatedAbilityEntry in activatedAbilities.AbilityByGuid.Values)
					{
						if (activatedAbilityEntry.Class.Contains("Mental"))
						{
							int cooldown = activatedAbilityEntry.Cooldown;
							if (cooldown > 10)
							{
								activatedAbilityEntry.Cooldown = cooldown - 10;
							}
							else if (cooldown > 0)
							{
								activatedAbilityEntry.Cooldown = 0;
							}
						}
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008A39 RID: 35385 RVA: 0x00315118 File Offset: 0x00313318
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("ApplyAttackConfusion");
			Registrar.Register("Overdose");
			Registrar.Register("BeforeDie");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008A3A RID: 35386 RVA: 0x00315144 File Offset: 0x00313344
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ApplyAttackConfusion")
			{
				return false;
			}
			if (E.ID == "BeforeDie" && this.Duration > 0 && base.Object.IsTrueKin())
			{
				return Precognition.OnBeforeDie(base.Object, Guid.Empty, this.GlimpseID, ref this.Duration, ref this.HitpointsAtSave, ref this.TemperatureAtSave, ref this.ActivatedSegment, this.WasPlayer, true, null);
			}
			if (E.ID == "Overdose" && this.Duration > 0)
			{
				this.Duration = 0;
				SphynxSalt_Tonic.ApplyOverdose(base.Object);
			}
			return base.FireEvent(E);
		}

		// Token: 0x06008A3B RID: 35387 RVA: 0x003151F8 File Offset: 0x003133F8
		public override void ApplyAllergy(GameObject Object)
		{
			SphynxSalt_Tonic.ApplyOverdose(Object);
		}

		// Token: 0x06008A3C RID: 35388 RVA: 0x00315200 File Offset: 0x00313400
		public static void ApplyOverdose(GameObject Object)
		{
			if (Object.IsPlayer())
			{
				if (Object.GetLongProperty("Overdosing", 0L) == 1L)
				{
					Popup.Show("Your mutant physiology reacts adversely to the tonic. You cannot see to see -- your mind cracks as a bell struck by a hammer.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				else
				{
					Popup.Show("The tonics you ingested react adversely to each other. You cannot see to see -- your mind cracks as a bell struck by a hammer.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
			}
			if (Object.HasPart<ActivatedAbilities>())
			{
				ActivatedAbilities part = Object.GetPart<ActivatedAbilities>();
				if (part == null || part.AbilityByGuid == null)
				{
					return;
				}
				foreach (ActivatedAbilityEntry activatedAbilityEntry in part.AbilityByGuid.Values)
				{
					if (activatedAbilityEntry.Class.Contains("Mental") && activatedAbilityEntry.Cooldown <= 2000 && activatedAbilityEntry.Cooldown >= -1)
					{
						activatedAbilityEntry.Cooldown = 2021;
					}
				}
			}
		}

		// Token: 0x06008A3D RID: 35389 RVA: 0x003152E4 File Offset: 0x003134E4
		public override bool Render(RenderEvent E)
		{
			if (this.Duration > 0)
			{
				int num = XRLCore.CurrentFrame % 60;
				if (num > 30 && num < 40)
				{
					E.Tile = null;
					E.RenderString = "ì";
					E.ColorString = "&C";
				}
			}
			return true;
		}

		// Token: 0x04003A92 RID: 14994
		public bool WasPlayer;

		// Token: 0x04003A93 RID: 14995
		public int HitpointsAtSave;

		// Token: 0x04003A94 RID: 14996
		public int TemperatureAtSave;

		// Token: 0x04003A95 RID: 14997
		public bool Overdose;

		// Token: 0x04003A96 RID: 14998
		private long ActivatedSegment;

		// Token: 0x04003A97 RID: 14999
		[NonSerialized]
		private Guid GlimpseID;
	}
}

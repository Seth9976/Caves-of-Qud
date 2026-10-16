using System;
using XRL.Rules;
using XRL.World.Capabilities;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02001044 RID: 4164
	[Serializable]
	public class GrandfatherHorn : IActivePart
	{
		// Token: 0x0600A5C2 RID: 42434 RVA: 0x003CA9E0 File Offset: 0x003C8BE0
		public GrandfatherHorn()
		{
			this.WorksOnEquipper = true;
		}

		// Token: 0x0600A5C3 RID: 42435 RVA: 0x003CAA34 File Offset: 0x003C8C34
		public override bool SameAs(IPart p)
		{
			GrandfatherHorn grandfatherHorn = p as GrandfatherHorn;
			return grandfatherHorn.Cooldown == this.Cooldown && !(grandfatherHorn.CooldownTime != this.CooldownTime) && !(grandfatherHorn.Power != this.Power) && !(grandfatherHorn.Duration != this.Duration) && !(grandfatherHorn.HarmonyTag != this.HarmonyTag) && base.SameAs(p);
		}

		// Token: 0x0600A5C4 RID: 42436 RVA: 0x003CAAB3 File Offset: 0x003C8CB3
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveItemListEvent.ID || ID == SingletonEvent<EndTurnEvent>.ID || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID;
		}

		// Token: 0x0600A5C5 RID: 42437 RVA: 0x003CAAE4 File Offset: 0x003C8CE4
		public override bool HandleEvent(AIGetOffensiveItemListEvent E)
		{
			if (this.Cooldown <= 0 && E.Actor == this.ParentObject.Equipped && E.Distance < 10 && this.IsInHarmony(E.Actor) && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null) && Stat.Random(1, 6) == 6)
			{
				E.Add("BlowGrandfatherHorn", 1, this.ParentObject, true, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A5C6 RID: 42438 RVA: 0x003CAB74 File Offset: 0x003C8D74
		public override bool HandleEvent(EndTurnEvent E)
		{
			if (this.Cooldown > 0 && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				this.Cooldown--;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A5C7 RID: 42439 RVA: 0x003CABC8 File Offset: 0x003C8DC8
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (this.Cooldown <= 0 && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				E.AddAction("Blow", "blow", "BlowGrandfatherHorn", null, 'b', false, 0, 0, false, false, false, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A5C8 RID: 42440 RVA: 0x003CAC30 File Offset: 0x003C8E30
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "BlowGrandfatherHorn" && this.Cooldown <= 0 && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				Cell currentCell = this.ParentObject.GetCurrentCell();
				if (currentCell != null)
				{
					this.ParentObject.PlayWorldSound("Sounds/Interact/sfx_interact_grandfatherHorn_blow", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
					this.ParentObject.Soundwave();
					foreach (GameObject gameObject in currentCell.ParentZone.FastFloodVisibility(currentCell.X, currentCell.Y, 10, "Brain", this.ParentObject, null, null))
					{
						if (!this.IsInHarmony(gameObject))
						{
							base.PerformMentalAttack(new Mental.Attack(Terrified.OfAttacker), E.Actor, gameObject, null, "Fear GrandfatherHorn", this.Power, 2, this.Duration.RollCached(), int.MinValue, 0, 0);
						}
					}
				}
				this.Cooldown = Stat.Roll(this.CooldownTime, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A5C9 RID: 42441 RVA: 0x003CAD90 File Offset: 0x003C8F90
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600A5CA RID: 42442 RVA: 0x003CAD93 File Offset: 0x003C8F93
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AdjustWeaponScore");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600A5CB RID: 42443 RVA: 0x003CADA8 File Offset: 0x003C8FA8
		public override bool FireEvent(Event E)
		{
			if (E.ID == "AdjustWeaponScore" && this.IsInHarmony(E.GetGameObjectParameter("User")))
			{
				E.SetParameter("Score", E.GetIntParameter("Score", 0) + 10);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600A5CC RID: 42444 RVA: 0x003CADFC File Offset: 0x003C8FFC
		public bool IsInHarmony(GameObject who)
		{
			return who != null && (who.HasTagOrProperty(this.HarmonyTag) || who.HasSkill("Customs_Sharer"));
		}

		// Token: 0x0600A5CD RID: 42445 RVA: 0x003CAE1E File Offset: 0x003C901E
		public override bool GetActivePartLocallyDefinedFailure()
		{
			return !this.IsInHarmony(this.ParentObject.Equipped);
		}

		// Token: 0x0600A5CE RID: 42446 RVA: 0x003CAE34 File Offset: 0x003C9034
		public override string GetActivePartLocallyDefinedFailureDescription()
		{
			return this.DisharmonyDescription;
		}

		// Token: 0x04004474 RID: 17524
		public int Cooldown;

		// Token: 0x04004475 RID: 17525
		public string CooldownTime = "100-200";

		// Token: 0x04004476 RID: 17526
		public string Power = "1d8";

		// Token: 0x04004477 RID: 17527
		public string Duration = "2d4+1";

		// Token: 0x04004478 RID: 17528
		public string HarmonyTag = "Cervine";

		// Token: 0x04004479 RID: 17529
		public string DisharmonyDescription = "DisharmoniousUser";
	}
}

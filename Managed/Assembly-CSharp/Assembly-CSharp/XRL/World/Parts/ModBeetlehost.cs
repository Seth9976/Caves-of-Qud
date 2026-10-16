using System;

namespace XRL.World.Parts
{
	// Token: 0x02001129 RID: 4393
	[Serializable]
	public class ModBeetlehost : IModification
	{
		// Token: 0x0600ADD0 RID: 44496 RVA: 0x00404E7C File Offset: 0x0040307C
		public ModBeetlehost()
		{
		}

		// Token: 0x0600ADD1 RID: 44497 RVA: 0x00404E8C File Offset: 0x0040308C
		public ModBeetlehost(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600ADD2 RID: 44498 RVA: 0x00404E9D File Offset: 0x0040309D
		public override void Configure()
		{
			this.WorksOnHolder = true;
			this.ChargeUse = 1000;
			this.IsEMPSensitive = true;
			this.IsBootSensitive = true;
			base.IsTechScannable = true;
			this.NameForStatus = "AutomataFab";
		}

		// Token: 0x0600ADD3 RID: 44499 RVA: 0x00404ED1 File Offset: 0x004030D1
		public override void TierConfigure()
		{
			this.ChargeUse = Math.Max(1000 - this.Tier * 150, 100);
		}

		// Token: 0x0600ADD4 RID: 44500 RVA: 0x00404EF2 File Offset: 0x004030F2
		public override void ApplyModification(GameObject Object)
		{
			if (this.ChargeUse > 0 && !Object.IsCreature)
			{
				Object.RequirePart<EnergyCellSocket>(false);
				base.IncreaseDifficultyAndComplexity(3, 2, null);
			}
		}

		// Token: 0x0600ADD5 RID: 44501 RVA: 0x00404F16 File Offset: 0x00403116
		public override bool SameAs(IPart p)
		{
			return (p as ModBeetlehost).Chance == this.Chance && base.SameAs(p);
		}

		// Token: 0x0600ADD6 RID: 44502 RVA: 0x00404F34 File Offset: 0x00403134
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600ADD7 RID: 44503 RVA: 0x00404F4A File Offset: 0x0040314A
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (!this.ParentObject.HasTag("Creature"))
			{
				E.Postfix.AppendRules("When powered, discharges a clockwork beetle friend on hit. Drains cell power quickly.", base.GetEventSensitiveAddStatusSummary(E));
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600ADD8 RID: 44504 RVA: 0x00404F7D File Offset: 0x0040317D
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600ADD9 RID: 44505 RVA: 0x00404F80 File Offset: 0x00403180
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("WeaponHit");
			Registrar.Register("AttackerAfterDamage");
			Registrar.Register("DealingMissileDamage");
			Registrar.Register("WeaponMissileWeaponHit");
			Registrar.Register("WeaponPseudoThrowHit");
			Registrar.Register("WeaponThrowHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600ADDA RID: 44506 RVA: 0x00404FD8 File Offset: 0x004031D8
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WeaponHit" || E.ID == "AttackerAfterDamage" || E.ID == "DealingMissileDamage" || E.ID == "WeaponMissileWeaponHit" || E.ID == "WeaponPseudoThrowHit" || E.ID == "WeaponThrowHit")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
				GameObject gameObjectParameter2 = E.GetGameObjectParameter("Defender");
				GameObject gameObjectParameter3 = E.GetGameObjectParameter("Weapon");
				GameObject gameObjectParameter4 = E.GetGameObjectParameter("Projectile");
				Cell currentCell = gameObjectParameter2.CurrentCell;
				if (currentCell != null)
				{
					Cell randomElement = currentCell.GetLocalEmptyAdjacentCells().GetRandomElement<Cell>(null);
					if (randomElement != null && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
					{
						GameObject gameObject = gameObjectParameter;
						GameObject gameObject2 = gameObjectParameter3;
						string text = "Modification ModBeetlehost Activation";
						GameObject gameObject3 = gameObjectParameter2;
						GameObject gameObject4 = gameObjectParameter4;
						if (GetSpecialEffectChanceEvent.GetFor(gameObject, gameObject2, text, this.Chance, gameObject3, gameObject4, true, false).in100())
						{
							gameObjectParameter2.Bloodsplatter();
							GameObject gameObject5 = GameObject.Create("ClockworkBeetle");
							gameObject5.MakeActive(false);
							randomElement.AddObject(gameObject5, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
							if (gameObject5.Brain != null)
							{
								gameObject5.Brain.PartyLeader = gameObjectParameter;
								gameObject5.Brain.Target = gameObjectParameter2;
								gameObject5.IsTrifling = true;
							}
							if (!gameObjectParameter2.IsPlayer())
							{
								gameObjectParameter2.Target = gameObject5;
							}
						}
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x040047DD RID: 18397
		public int Chance = 100;
	}
}

using System;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02001082 RID: 4226
	[Serializable]
	public class PsychicMeridian : IActivePart
	{
		// Token: 0x0600A82C RID: 43052 RVA: 0x003D7FD1 File Offset: 0x003D61D1
		public PsychicMeridian()
		{
			this.IsRealityDistortionBased = true;
			this.WorksOnWearer = true;
		}

		// Token: 0x0600A82D RID: 43053 RVA: 0x003D8001 File Offset: 0x003D6201
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600A82E RID: 43054 RVA: 0x003D8004 File Offset: 0x003D6204
		public override bool WantTurnTick()
		{
			return this.Cooldown != null;
		}

		// Token: 0x0600A82F RID: 43055 RVA: 0x003D800F File Offset: 0x003D620F
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (this.CurrentCooldown > 0)
			{
				this.CurrentCooldown = Math.Max(0, this.CurrentCooldown - Amount);
			}
		}

		// Token: 0x0600A830 RID: 43056 RVA: 0x003D802E File Offset: 0x003D622E
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AfterMentalDefendEvent.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == EquippedEvent.ID || ID == UnequippedEvent.ID;
		}

		// Token: 0x0600A831 RID: 43057 RVA: 0x003D805C File Offset: 0x003D625C
		public override bool HandleEvent(EquippedEvent E)
		{
			E.Actor.RegisterPartEvent(this, "DefenderAfterAttack");
			E.Actor.RegisterPartEvent(this, "DefenderProjectileHit");
			return base.HandleEvent(E);
		}

		// Token: 0x0600A832 RID: 43058 RVA: 0x003D8087 File Offset: 0x003D6287
		public override bool HandleEvent(UnequippedEvent E)
		{
			E.Actor.UnregisterPartEvent(this, "DefenderAfterAttack");
			E.Actor.UnregisterPartEvent(this, "DefenderProjectileHit");
			return base.HandleEvent(E);
		}

		// Token: 0x0600A833 RID: 43059 RVA: 0x003D80B4 File Offset: 0x003D62B4
		public override bool HandleEvent(AfterMentalDefendEvent E)
		{
			if ((!this.Guarded || E.Penetrations <= 0) && this.CurrentCooldown <= 0 && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				this.AfflictNosebleed(E.Attacker);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A834 RID: 43060 RVA: 0x003D8118 File Offset: 0x003D6318
		public void AfflictNosebleed(GameObject Attacker)
		{
			GameObject activePartFirstSubject = this.GetActivePartFirstSubject();
			GameObject gameObject = (this.TargetWearer ? activePartFirstSubject : Attacker);
			if (!this.TargetWearer && (activePartFirstSubject.IsPlayer() || gameObject.IsPlayer()))
			{
				IComponent<GameObject>.XDidY(gameObject, "impale", gameObject.itself + " on " + activePartFirstSubject.poss("psychic barbs", true), null, null, null, null, gameObject, false, false, null, null, false, false, false, false, false, null);
			}
			gameObject.ApplyEffect(new Nosebleed(this.NosebleedDamage, this.NosebleedSave, activePartFirstSubject, true, false, false), null);
			if (!string.IsNullOrEmpty(this.Cooldown))
			{
				this.CurrentCooldown = this.Cooldown.RollCached();
			}
		}

		// Token: 0x0600A835 RID: 43061 RVA: 0x003D81C2 File Offset: 0x003D63C2
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				E.Add("stars", 1);
				E.Add("time", 1);
				E.Add("scholarship", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A836 RID: 43062 RVA: 0x003D81FD File Offset: 0x003D63FD
		public override bool AllowStaticRegistration()
		{
			return false;
		}

		// Token: 0x0600A837 RID: 43063 RVA: 0x003D8200 File Offset: 0x003D6400
		public override bool FireEvent(Event E)
		{
			if ((E.ID == "DefenderAfterAttack" || E.ID == "DefenderProjectileHit") && this.CurrentCooldown <= 0)
			{
				if (this.Guarded && E.GetIntParameter("Penetrations", 0) > 0)
				{
					return true;
				}
				if (base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
				{
					Damage damage = E.GetParameter("Damage") as Damage;
					if (damage != null && damage.HasAttribute("Mental"))
					{
						GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
						if (gameObjectParameter == null)
						{
							return true;
						}
						this.AfflictNosebleed(gameObjectParameter);
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004568 RID: 17768
		public string NosebleedDamage = "1d2+4";

		// Token: 0x04004569 RID: 17769
		public int NosebleedSave = 30;

		// Token: 0x0400456A RID: 17770
		public string Cooldown;

		// Token: 0x0400456B RID: 17771
		public int CurrentCooldown;

		// Token: 0x0400456C RID: 17772
		public bool TargetWearer;

		// Token: 0x0400456D RID: 17773
		public bool Guarded = true;
	}
}

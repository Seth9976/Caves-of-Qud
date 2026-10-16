using System;
using System.Text;
using XRL.Core;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02001102 RID: 4354
	[Serializable]
	public class CooldownAmmoLoader : IPart
	{
		// Token: 0x0600AC45 RID: 44101 RVA: 0x003F273C File Offset: 0x003F093C
		public override bool SameAs(IPart p)
		{
			CooldownAmmoLoader cooldownAmmoLoader = p as CooldownAmmoLoader;
			return !(cooldownAmmoLoader.Cooldown != this.Cooldown) && !(cooldownAmmoLoader.ProjectileObject != this.ProjectileObject) && cooldownAmmoLoader.Readout == this.Readout && base.SameAs(p);
		}

		// Token: 0x0600AC46 RID: 44102 RVA: 0x003F2794 File Offset: 0x003F0994
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<AIWantUseWeaponEvent>.ID || ID == PooledEvent<CheckLoadAmmoEvent>.ID || ID == PooledEvent<CheckReadyToFireEvent>.ID || ID == PooledEvent<GetAmmoCountAvailableEvent>.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == PooledEvent<GetMissileWeaponProjectileEvent>.ID || ID == PooledEvent<GetMissileWeaponStatusEvent>.ID || ID == PooledEvent<GetNotReadyToFireMessageEvent>.ID || ID == PooledEvent<GetProjectileBlueprintEvent>.ID || ID == PooledEvent<LoadAmmoEvent>.ID;
		}

		// Token: 0x0600AC47 RID: 44103 RVA: 0x003F27FD File Offset: 0x003F09FD
		public override bool HandleEvent(AIWantUseWeaponEvent E)
		{
			return !this.CooldownActive() && base.HandleEvent(E);
		}

		// Token: 0x0600AC48 RID: 44104 RVA: 0x003F2810 File Offset: 0x003F0A10
		public override bool HandleEvent(GetAmmoCountAvailableEvent E)
		{
			if (this.CooldownActive())
			{
				E.Count = 0;
				return false;
			}
			E.Register(1);
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC49 RID: 44105 RVA: 0x003F2834 File Offset: 0x003F0A34
		public override bool HandleEvent(CheckLoadAmmoEvent E)
		{
			if (this.CooldownActive())
			{
				if (E.Message == null)
				{
					E.Message = this.GetCoolingDownMessage();
				}
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC4A RID: 44106 RVA: 0x003F2868 File Offset: 0x003F0A68
		public override bool HandleEvent(LoadAmmoEvent E)
		{
			if (this.CooldownActive())
			{
				if (E.Message == null)
				{
					E.Message = this.GetCoolingDownMessage();
				}
				return false;
			}
			if (!this.ProjectileObject.IsNullOrEmpty())
			{
				E.Projectile = GameObject.Create(this.ProjectileObject, 0, 0, null, null, null, "Projectile", null);
			}
			this.LastFireTimeTick = The.Game.TimeTicks;
			this.CurrentCooldown = Stat.Roll(this.Cooldown, null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC4B RID: 44107 RVA: 0x003F28E7 File Offset: 0x003F0AE7
		public override bool HandleEvent(CheckReadyToFireEvent E)
		{
			return !this.CooldownActive() && base.HandleEvent(E);
		}

		// Token: 0x0600AC4C RID: 44108 RVA: 0x003F28FA File Offset: 0x003F0AFA
		public override bool HandleEvent(GetNotReadyToFireMessageEvent E)
		{
			if (this.CooldownActive())
			{
				E.Message = this.GetCoolingDownMessage();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC4D RID: 44109 RVA: 0x003F2918 File Offset: 0x003F0B18
		public override bool HandleEvent(GetMissileWeaponStatusEvent E)
		{
			if (E.Override == null)
			{
				if (this.CooldownActive())
				{
					if (E.Status != null)
					{
						E.Status.ammoTotal = 0;
						E.Status.ammoRemaining = 0;
					}
					E.Items.Length = 0;
					this.ApplyCooldownDisplay(E.Items);
					E.Override = this;
				}
				else if (E.Status != null)
				{
					E.Status.ammoTotal = 1;
					E.Status.ammoRemaining = 1;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC4E RID: 44110 RVA: 0x003F299E File Offset: 0x003F0B9E
		public override bool HandleEvent(GetProjectileBlueprintEvent E)
		{
			if (!this.ProjectileObject.IsNullOrEmpty())
			{
				E.Blueprint = this.ProjectileObject;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC4F RID: 44111 RVA: 0x003F29C0 File Offset: 0x003F0BC0
		public override bool HandleEvent(GetMissileWeaponProjectileEvent E)
		{
			if (!this.ProjectileObject.IsNullOrEmpty())
			{
				E.Blueprint = this.ProjectileObject;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC50 RID: 44112 RVA: 0x003F29E4 File Offset: 0x003F0BE4
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood())
			{
				E.AddMissileWeaponDamageTag(GetMissileWeaponPerformanceEvent.GetFor(this.ParentObject.Equipped, this.ParentObject, null, null, null, null, null, null, null, null, null, null, null, false));
				if (!E.Reference && this.CooldownActive())
				{
					string cooldownDisplay = this.GetCooldownDisplay();
					if (!cooldownDisplay.IsNullOrEmpty())
					{
						E.AddTag(cooldownDisplay, -5);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AC51 RID: 44113 RVA: 0x003F2A78 File Offset: 0x003F0C78
		public bool CooldownActive()
		{
			return this.LastFireTimeTick != The.Game.TimeTicks && this.LastFireTimeTick + (long)this.CurrentCooldown > The.Game.TimeTicks;
		}

		// Token: 0x0600AC52 RID: 44114 RVA: 0x003F2AA8 File Offset: 0x003F0CA8
		public string GetCoolingDownMessage()
		{
			int num = (int)(this.LastFireTimeTick + (long)this.CurrentCooldown - The.Game.TimeTicks);
			if (this.Readout)
			{
				return string.Concat(new string[]
				{
					this.ParentObject.Does("need", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
					" ",
					num.Things("more round", "more rounds"),
					" before ",
					this.ParentObject.it,
					" can be fired again."
				});
			}
			return this.ParentObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " unresponsive as " + this.ParentObject.does("cool", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, true, true, null, true, null, false) + " down.";
		}

		// Token: 0x0600AC53 RID: 44115 RVA: 0x003F2BC4 File Offset: 0x003F0DC4
		public bool ApplyCooldownDisplay(StringBuilder SB)
		{
			long num = this.LastFireTimeTick + (long)this.CurrentCooldown - XRLCore.Core.Game.TimeTicks;
			if (num > 0L)
			{
				SB.Append("{{y|");
				if (this.Readout)
				{
					SB.Append('[').Append(num).Append(" sec]");
				}
				else
				{
					SB.Append("[cooldown]");
				}
				SB.Append("}}");
				return true;
			}
			return false;
		}

		// Token: 0x0600AC54 RID: 44116 RVA: 0x003F2C40 File Offset: 0x003F0E40
		public string GetCooldownDisplay()
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			this.ApplyCooldownDisplay(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x0400474E RID: 18254
		public string Cooldown = "2d6";

		// Token: 0x0400474F RID: 18255
		public string ProjectileObject;

		// Token: 0x04004750 RID: 18256
		public bool Readout;

		// Token: 0x04004751 RID: 18257
		private int CurrentCooldown;

		// Token: 0x04004752 RID: 18258
		public long LastFireTimeTick;
	}
}

using System;
using System.Collections.Generic;
using XRL.Collections;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	/// <remarks>
	/// overload behavior: if <see cref="!:IsPowerLoadSensitive" /> is true,
	/// which it is by default, a chance to target a given projectile that
	/// is over 0 will be increased by ((power load - 100) / 30), i.e.
	/// 10 for the standard overload power load of 400.
	/// </remarks>
	// Token: 0x0200107C RID: 4220
	[Serializable]
	public class PointDefense : IPoweredPart
	{
		// Token: 0x0600A7EC RID: 42988 RVA: 0x003D5D10 File Offset: 0x003D3F10
		public PointDefense()
		{
			this.WorksOnEquipper = true;
			this.IsPowerLoadSensitive = true;
			this.NameForStatus = "PointDefenseTracking";
		}

		// Token: 0x0600A7ED RID: 42989 RVA: 0x003D5D80 File Offset: 0x003D3F80
		public override bool SameAs(IPart p)
		{
			PointDefense pointDefense = p as PointDefense;
			return pointDefense.MinRange == this.MinRange && pointDefense.MaxRange == this.MaxRange && pointDefense.TargetExplosives == this.TargetExplosives && pointDefense.TargetThrownWeapons == this.TargetThrownWeapons && pointDefense.TargetArrows == this.TargetArrows && pointDefense.TargetSlugs == this.TargetSlugs && pointDefense.TargetEnergy == this.TargetEnergy && pointDefense.UsesSelfEquipment == this.UsesSelfEquipment && pointDefense.UsesEquipperEquipment == this.UsesEquipperEquipment && pointDefense.ShowComputeMessage == this.ShowComputeMessage && !(pointDefense.EquipmentEvent != this.EquipmentEvent) && pointDefense.ComputePowerFactor == this.ComputePowerFactor && base.SameAs(p);
		}

		// Token: 0x0600A7EE RID: 42990 RVA: 0x003D5E60 File Offset: 0x003D4060
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetShortDescriptionEvent.ID || ID == PooledEvent<ProjectileMovingEvent>.ID;
		}

		// Token: 0x0600A7EF RID: 42991 RVA: 0x003D5E80 File Offset: 0x003D4080
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.ShowComputeMessage)
			{
				if (this.ComputePowerFactor > 0f)
				{
					E.Postfix.AppendRules("Compute power on the local lattice increases this item's effectiveness.");
				}
				else if (this.ComputePowerFactor < 0f)
				{
					E.Postfix.AppendRules("Compute power on the local lattice decreases this item's effectiveness.");
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A7F0 RID: 42992 RVA: 0x003D5EDC File Offset: 0x003D40DC
		public override bool HandleEvent(ProjectileMovingEvent E)
		{
			if (E.Launcher == this.WeaponSystem && this.WeaponSystem != null)
			{
				if (E.Cell != this.TargetCell)
				{
					goto IL_04F6;
				}
				try
				{
					if (!this.TargetProjectile.PhaseMatches(this.ActivePhase))
					{
						IComponent<GameObject>.XDidYToZ(this.WeaponSystem, "intercept", this.TargetProjectile, ", but " + E.Projectile.does("pass", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " through " + this.TargetProjectile.them, "!", null, null, this.ActiveActor, null, false, false, false, false, false, null, null, null, false, false, IComponent<GameObject>.Visible(this.ActiveActor) || IComponent<GameObject>.Visible(this.WeaponSystem), false, false, null);
						goto IL_04F6;
					}
					if (!PointDefenseInterceptEvent.Check(this.TargetProjectile, E.Projectile, this.WeaponSystem))
					{
						IComponent<GameObject>.XDidYToZ(this.WeaponSystem, "intercept", this.TargetProjectile, ", but " + E.Projectile.does("fail", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " to affect " + this.TargetProjectile.them, "!", null, null, this.ActiveActor, null, false, false, false, false, false, null, null, null, false, false, IComponent<GameObject>.Visible(this.ActiveActor) || IComponent<GameObject>.Visible(this.WeaponSystem), false, false, null);
						goto IL_04F6;
					}
					IComponent<GameObject>.XDidYToZ(this.WeaponSystem, "intercept", this.TargetProjectile, null, "!", null, null, this.ActiveActor, null, false, false, false, false, false, null, null, null, false, false, IComponent<GameObject>.Visible(this.ActiveActor) || IComponent<GameObject>.Visible(this.WeaponSystem), false, false, null);
					if (!this.TargetProjectile.IsReal || !GameObject.Validate(this.TargetProjectile))
					{
						return false;
					}
					E.HitOverride = this.TargetProjectile;
					if (this.TargetingPlayerProjectile)
					{
						E.ActivateShowUninvolved = true;
						goto IL_04F6;
					}
					goto IL_04F6;
				}
				finally
				{
					this.TargetCell = null;
					this.TargetProjectile = null;
					this.ActiveActor = null;
				}
			}
			if (E.Defender == null)
			{
				Cell currentCell = this.ParentObject.GetCurrentCell();
				if (currentCell != null)
				{
					int num = currentCell.PathDistanceTo(E.Cell);
					if (num >= this.MinRange && num <= this.MaxRange)
					{
						GameObject gameObject = this.GetActivePartFirstSubject() ?? this.ParentObject;
						if (gameObject != E.Attacker)
						{
							int? num2 = null;
							if (this.ChanceToTargetProjectile(E.Projectile, ref num2, new bool?(E.Throw)).in100() && E.Attacker != gameObject && !gameObject.IsLedBy(E.Attacker) && !E.Attacker.IsLedBy(gameObject) && (E.Attacker == null || E.Attacker.IsHostileTowards(gameObject) || this.IsCellOnPath(currentCell, E.Path)))
							{
								bool flag = true;
								bool flag2 = false;
								bool flag3 = false;
								bool flag4 = false;
								bool flag5 = false;
								bool flag6 = false;
								bool flag7 = false;
								bool flag8 = false;
								bool flag9 = false;
								bool flag10 = false;
								int num3 = 1;
								int? num4 = num2;
								if (base.IsReady(flag, flag2, flag3, flag4, flag5, flag6, flag7, flag8, flag9, flag10, num3, null, false, 0L, num4) && this.PathClear(currentCell, E.Cell, gameObject))
								{
									if (this.UsesSelfEquipment || this.UsesEquipperEquipment)
									{
										this.WeaponSystem = null;
										if (this.UsesSelfEquipment)
										{
											Body body = this.ParentObject.Body;
											this.WeaponSystem = ((body != null) ? body.FindEquipmentByEvent(this.EquipmentEvent) : null);
										}
										if (this.UsesEquipperEquipment && this.WeaponSystem == null && this.ParentObject.Equipped == null)
										{
											Body body2 = this.ParentObject.Equipped.Body;
											this.WeaponSystem = ((body2 != null) ? body2.FindEquipmentByEvent(this.EquipmentEvent) : null);
										}
										if (this.WeaponSystem == null)
										{
											goto IL_04F6;
										}
									}
									else
									{
										this.WeaponSystem = this.ParentObject;
									}
									this.ActiveActor = gameObject;
									this.ActivePhase = Phase.getWeaponPhase(gameObject, GetActivationPhaseEvent.GetFor(this.WeaponSystem, 0));
									this.TargetProjectile = E.Projectile;
									this.TargetingPlayerProjectile = E.Attacker != null && E.Attacker.IsPlayer();
									this.TargetCell = E.Cell;
									try
									{
										Event @event = Event.New("CommandFireMissile", 0, 0, 0);
										@event.SetParameter("Owner", gameObject);
										@event.SetParameter("TargetCell", this.TargetCell);
										@event.SetParameter("ScreenBuffer", E.ScreenBuffer);
										@event.SetParameter("MessageAsFrom", this.WeaponSystem);
										@event.SetParameter("EnergyMultiplier", 0f);
										this.WeaponSystem.FireEvent(@event);
									}
									finally
									{
										this.WeaponSystem = null;
										this.TargetCell = null;
										this.TargetProjectile = null;
										this.ActiveActor = null;
									}
								}
							}
						}
					}
				}
			}
			IL_04F6:
			return base.HandleEvent(E);
		}

		// Token: 0x0600A7F1 RID: 42993 RVA: 0x003D641C File Offset: 0x003D461C
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600A7F2 RID: 42994 RVA: 0x003D6420 File Offset: 0x003D4620
		public override void TurnTick(long TimeTick, int Amount)
		{
			base.ConsumeChargeIfOperational(false, false, false, false, false, false, false, false, false, Amount, null, false, 0, false, null);
		}

		// Token: 0x0600A7F3 RID: 42995 RVA: 0x003D6454 File Offset: 0x003D4654
		public bool PathClear(Cell OurCell, Cell TargetCell, GameObject Actor)
		{
			Zone parentZone = OurCell.ParentZone;
			List<Point> list = Zone.Line(OurCell.X, OurCell.Y, TargetCell.X, TargetCell.Y, true);
			int i = 1;
			int num = list.Count - 1;
			while (i < num)
			{
				Cell cell = parentZone.GetCell(list[i].X, list[i].Y);
				if (cell.IsOccludingFor(Actor))
				{
					return false;
				}
				using (ScopeDisposedList<GameObject> scopeDisposedCopy = cell.Objects.GetScopeDisposedCopy<GameObject>())
				{
					int j = 0;
					int count = scopeDisposedCopy.Count;
					while (j < count)
					{
						GameObject gameObject = scopeDisposedCopy[j];
						if (gameObject.IsCombatObject() && !Actor.IsHostileTowards(gameObject))
						{
							return false;
						}
						if ((gameObject.ConsiderSolidFor(Actor, false) || GetMissileCoverPercentageEvent.GetFor(gameObject, Actor, null, false, false) > 0) && !OkayToDamageEvent.Check(gameObject, Actor))
						{
							return false;
						}
						j++;
					}
				}
				i++;
			}
			return true;
		}

		// Token: 0x0600A7F4 RID: 42996 RVA: 0x003D6564 File Offset: 0x003D4764
		public int ChanceToTargetProjectile(GameObject Projectile, ref int? PowerLoad, bool? Thrown = null)
		{
			int num = 0;
			if (this.TargetThrownWeapons > 0 && num < this.TargetThrownWeapons)
			{
				bool? flag = Thrown;
				bool flag2 = true;
				if ((flag.GetValueOrDefault() == flag2) & (flag != null))
				{
					num = this.TargetThrownWeapons;
				}
				else if (Thrown == null && Projectile.HasPart<ThrownWeapon>())
				{
					num = this.TargetThrownWeapons;
				}
			}
			if (this.TargetExplosives > 0 && num < this.TargetExplosives && IsExplosiveEvent.Check(Projectile))
			{
				num = this.TargetExplosives;
			}
			if (this.TargetArrows > 0 && num < this.TargetArrows && (Projectile.HasPart<AmmoArrow>() || Projectile.HasPart<AmmoDart>() || Projectile.HasTag("Arrow")))
			{
				num = this.TargetArrows;
			}
			if (this.TargetSlugs > 0 && num < this.TargetSlugs && (Projectile.HasPart<AmmoSlug>() || Projectile.HasPart<AmmoShotgunShell>() || Projectile.HasTag("Slug")))
			{
				num = this.TargetSlugs;
			}
			if (this.TargetEnergy > 0 && num < this.TargetEnergy && this.ProjectileIsEnergy(Projectile))
			{
				num = this.TargetEnergy;
			}
			if (num > 0)
			{
				num = GetAvailableComputePowerEvent.AdjustUp(this, num, this.ComputePowerFactor);
				if (this.IsPowerLoadSensitive)
				{
					int num2 = PowerLoad.GetValueOrDefault();
					if (PowerLoad == null)
					{
						num2 = this.MyPowerLoadLevel();
						PowerLoad = new int?(num2);
					}
					num += IComponent<GameObject>.PowerLoadBonus(PowerLoad.Value, 100, 30);
				}
			}
			return num;
		}

		// Token: 0x0600A7F5 RID: 42997 RVA: 0x003D66BC File Offset: 0x003D48BC
		public int ChanceToTargetProjectile(GameObject Projectile, bool? Thrown = null)
		{
			int? num = new int?(100);
			return this.ChanceToTargetProjectile(Projectile, ref num, Thrown);
		}

		// Token: 0x0600A7F6 RID: 42998 RVA: 0x003D66DC File Offset: 0x003D48DC
		private bool ProjectileIsEnergy(GameObject Projectile)
		{
			Projectile projectile;
			if (Projectile.TryGetPart<Projectile>(out projectile) && !projectile.Attributes.IsNullOrEmpty())
			{
				if (Damage.ContainsLightDamage(projectile.Attributes))
				{
					return true;
				}
				if (Damage.ContainsHeatDamage(projectile.Attributes))
				{
					return true;
				}
				if (Damage.ContainsColdDamage(projectile.Attributes))
				{
					return true;
				}
				if (Damage.ContainsDisintegrationDamage(projectile.Attributes))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600A7F7 RID: 42999 RVA: 0x003D6740 File Offset: 0x003D4940
		public bool IsCellOnPath(Cell C, List<Point> Path)
		{
			int i = 0;
			int count = Path.Count;
			while (i < count)
			{
				if (Path[i].X == C.X && Path[i].Y == C.Y)
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x04004547 RID: 17735
		public int MinRange = 2;

		// Token: 0x04004548 RID: 17736
		public int MaxRange = 10;

		// Token: 0x04004549 RID: 17737
		public int TargetExplosives = 100;

		// Token: 0x0400454A RID: 17738
		public int TargetThrownWeapons = 100;

		// Token: 0x0400454B RID: 17739
		public int TargetArrows = 100;

		// Token: 0x0400454C RID: 17740
		public int TargetSlugs;

		// Token: 0x0400454D RID: 17741
		public int TargetEnergy;

		// Token: 0x0400454E RID: 17742
		public bool UsesSelfEquipment;

		// Token: 0x0400454F RID: 17743
		public bool UsesEquipperEquipment;

		// Token: 0x04004550 RID: 17744
		public bool ShowComputeMessage = true;

		// Token: 0x04004551 RID: 17745
		public string EquipmentEvent = "UseForPointDefense";

		// Token: 0x04004552 RID: 17746
		public float ComputePowerFactor = 1f;

		// Token: 0x04004553 RID: 17747
		[NonSerialized]
		private Cell TargetCell;

		// Token: 0x04004554 RID: 17748
		[NonSerialized]
		private bool TargetingPlayerProjectile;

		// Token: 0x04004555 RID: 17749
		[NonSerialized]
		private GameObject TargetProjectile;

		// Token: 0x04004556 RID: 17750
		[NonSerialized]
		private GameObject WeaponSystem;

		// Token: 0x04004557 RID: 17751
		[NonSerialized]
		private GameObject ActiveActor;

		// Token: 0x04004558 RID: 17752
		[NonSerialized]
		private int ActivePhase;
	}
}

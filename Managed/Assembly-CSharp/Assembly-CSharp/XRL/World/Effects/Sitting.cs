using System;
using XRL.Rules;
using XRL.World.Parts;

namespace XRL.World.Effects
{
	// Token: 0x02000D8B RID: 3467
	[Serializable]
	public class Sitting : Effect, ITierInitialized
	{
		// Token: 0x060089F1 RID: 35313 RVA: 0x00313F69 File Offset: 0x00312169
		public Sitting()
		{
			this.Duration = 1;
			this.DisplayName = "{{C|sitting}}";
		}

		// Token: 0x060089F2 RID: 35314 RVA: 0x00313F83 File Offset: 0x00312183
		public Sitting(int Level)
			: this()
		{
			this.Level = Level;
		}

		// Token: 0x060089F3 RID: 35315 RVA: 0x00313F92 File Offset: 0x00312192
		public Sitting(int Level, string DamageAttributes)
			: this(Level)
		{
			this.DamageAttributes = DamageAttributes;
		}

		// Token: 0x060089F4 RID: 35316 RVA: 0x00313FA4 File Offset: 0x003121A4
		public Sitting(GameObject SittingOn)
		{
			this.SittingOn = SittingOn;
			this.Duration = 1;
			this.DisplayName = "{{C|sitting on " + SittingOn.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false) + "}}";
		}

		// Token: 0x060089F5 RID: 35317 RVA: 0x00313FFE File Offset: 0x003121FE
		public Sitting(GameObject SittingOn, int Level)
			: this(SittingOn)
		{
			this.Level = Level;
		}

		// Token: 0x060089F6 RID: 35318 RVA: 0x0031400E File Offset: 0x0031220E
		public Sitting(GameObject SittingOn, int Level, string DamageAttributes)
			: this(SittingOn, Level)
		{
			this.DamageAttributes = DamageAttributes;
		}

		// Token: 0x060089F7 RID: 35319 RVA: 0x0031401F File Offset: 0x0031221F
		public void Initialize(int Tier)
		{
			this.Level = Stat.Random(-5, 5);
		}

		// Token: 0x060089F8 RID: 35320 RVA: 0x0031402F File Offset: 0x0031222F
		public override int GetEffectType()
		{
			return 83886208;
		}

		// Token: 0x060089F9 RID: 35321 RVA: 0x00314036 File Offset: 0x00312236
		public override bool SameAs(Effect e)
		{
			return false;
		}

		// Token: 0x060089FA RID: 35322 RVA: 0x0031403C File Offset: 0x0031223C
		public override string GetDetails()
		{
			if (this.Level >= 5)
			{
				return "Improves natural healing rate.\nAids in examining and disassembling artifacts.\n-6 DV.\nMust spend a turn to stand up before moving.";
			}
			if (this.Level > -2)
			{
				return "Slightly improves natural healing rate.\nAids in examining and disassembling artifacts.\n-6 DV.\nMust spend a turn to stand up before moving.";
			}
			if (this.Level == -2)
			{
				return "Slightly improves natural healing rate.\n-6 DV.\nMust spend a turn to stand up before moving.";
			}
			if (this.Level > -10)
			{
				return "Slightly improves natural healing rate.\nDistracts from examining and disassembling artifacts.\n-6 DV.\nMust spend a turn to stand up before moving.";
			}
			return "Inflicts ongoing damage.\nDistracts from examining and disassembling artifacts.\n-6 DV.\nMust spend a turn to stand up before moving.";
		}

		// Token: 0x060089FB RID: 35323 RVA: 0x00314090 File Offset: 0x00312290
		public override bool Apply(GameObject Object)
		{
			if (!Object.CanChangeBodyPosition("Sitting", false, false, false, false))
			{
				return false;
			}
			if (!Object.FireEvent("ApplySitting"))
			{
				return false;
			}
			if (Object.CurrentCell != null)
			{
				if (Object != null)
				{
					Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_plop", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				}
				Object.CurrentCell.FireEvent(Event.New("ObjectSitting", "Object", Object, "SittingOn", this.SittingOn));
			}
			Object.BodyPositionChanged("Sitting", false);
			this.ApplyStats();
			return true;
		}

		// Token: 0x060089FC RID: 35324 RVA: 0x00314130 File Offset: 0x00312330
		public override void Remove(GameObject Object)
		{
			this.Duration = 0;
			Object.BodyPositionChanged("NotSitting", false);
			if (Object != null)
			{
				Object.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_standUp", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			this.UnapplyStats();
			base.Remove(Object);
		}

		// Token: 0x060089FD RID: 35325 RVA: 0x0031418C File Offset: 0x0031238C
		public bool IsSittingOnValid()
		{
			return GameObject.Validate(ref this.SittingOn) && this.SittingOn.CurrentCell != null && GameObject.Validate(base.Object) && base.Object.CurrentCell != null && this.SittingOn.CurrentCell == base.Object.CurrentCell && this.SittingOn.PhaseAndFlightMatches(base.Object);
		}

		// Token: 0x060089FE RID: 35326 RVA: 0x00314208 File Offset: 0x00312408
		public bool CheckSittingOn()
		{
			if (!this.IsSittingOnValid())
			{
				this.SittingOn = null;
				this.Level = 0;
				this.DamageAttributes = null;
				return false;
			}
			if (this.SittingOn != null)
			{
				this.DisplayName = "{{C|sitting on " + this.SittingOn.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false) + "}}";
			}
			return true;
		}

		// Token: 0x060089FF RID: 35327 RVA: 0x0031427B File Offset: 0x0031247B
		private void ApplyStats()
		{
			base.StatShifter.SetStatShift("DV", -6, false);
		}

		// Token: 0x06008A00 RID: 35328 RVA: 0x00314291 File Offset: 0x00312491
		private void UnapplyStats()
		{
			base.StatShifter.RemoveStatShifts();
		}

		// Token: 0x06008A01 RID: 35329 RVA: 0x0031429E File Offset: 0x0031249E
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<EndTurnEvent>.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetTinkeringBonusEvent.ID;
		}

		// Token: 0x06008A02 RID: 35330 RVA: 0x003142C4 File Offset: 0x003124C4
		public override bool HandleEvent(GetTinkeringBonusEvent E)
		{
			if (E.Type == "Inspect" || E.Type == "Disassemble")
			{
				int num = Math.Min(2 + this.Level, 4);
				if (num != 0)
				{
					E.Bonus += num;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008A03 RID: 35331 RVA: 0x0031431C File Offset: 0x0031251C
		public override bool HandleEvent(EndTurnEvent E)
		{
			if (this.CheckSittingOn())
			{
				if (this.Level > -10)
				{
					int num = this.HealCounter + 1;
					this.HealCounter = num;
					if (num >= 10 - this.Level)
					{
						base.Object.Heal(1, false, false, false);
						this.HealCounter = 0;
					}
				}
				else
				{
					GameObject @object = base.Object;
					int num2 = -this.Level - 9;
					string damageAttributes = this.DamageAttributes;
					GameObject sittingOn = this.SittingOn;
					if (@object.TakeDamage(num2, (this.SittingOn == null) ? "from sitting!" : "from %O!", damageAttributes, null, null, null, sittingOn, null, null, null, false, false, false, false, false, false, false, false, false, 5, null) && 50.in100())
					{
						base.Object.Bloodsplatter();
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008A04 RID: 35332 RVA: 0x003143DC File Offset: 0x003125DC
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (!E.Reference)
			{
				this.CheckSittingOn();
				if (this.SittingOn != null)
				{
					E.AddTag("[{{B|sitting on " + this.SittingOn.an(int.MaxValue, null, null, false, true, false, false, true, false, true, false, false, true, false, null, false, null, false) + "}}]", 20);
				}
				else
				{
					E.AddTag("[{{B|sitting}}]", 20);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008A05 RID: 35333 RVA: 0x00314458 File Offset: 0x00312658
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AfterDeepCopyWithoutEffects");
			Registrar.Register("BeforeDeepCopyWithoutEffects");
			Registrar.Register("BodyPositionChanged");
			Registrar.Register("LeaveCell");
			Registrar.Register("MovementModeChanged");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008A06 RID: 35334 RVA: 0x003144A4 File Offset: 0x003126A4
		private void StandUp(Event E = null)
		{
			this.CheckSittingOn();
			if (this.SittingOn != null)
			{
				this.SittingOn.GetPart<Chair>().StandUp(base.Object, E, this);
				return;
			}
			base.DidX("stand", "up", null, null, null, base.Object, null, false, false, null, null, false, false, false, false, false, null);
			base.Object.UseEnergy(1000, "Position", null, null, false);
			base.Object.RemoveEffect(this, true);
		}

		// Token: 0x06008A07 RID: 35335 RVA: 0x00314530 File Offset: 0x00312730
		public override bool FireEvent(Event E)
		{
			if (E.ID == "LeaveCell")
			{
				if (this.Duration > 0)
				{
					Cell cell = E.GetParameter("Cell") as Cell;
					GameObject sittingOn = this.SittingOn;
					Chair chair = ((sittingOn != null) ? sittingOn.GetPart<Chair>() : null);
					if (cell == null || chair == null || !chair.Securing || cell.HasObject(this.SittingOn))
					{
						this.StandUp(E);
					}
				}
			}
			else if (E.ID == "MovementModeChanged" || E.ID == "BodyPositionChanged")
			{
				if (this.Duration > 0 && E.GetStringParameter("To", null) != "Asleep")
				{
					this.StandUp(E);
				}
			}
			else if (E.ID == "BeforeDeepCopyWithoutEffects")
			{
				this.UnapplyStats();
			}
			else if (E.ID == "AfterDeepCopyWithoutEffects")
			{
				this.ApplyStats();
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003A89 RID: 14985
		public int HealCounter;

		// Token: 0x04003A8A RID: 14986
		public string DamageAttributes;

		// Token: 0x04003A8B RID: 14987
		public GameObject SittingOn;

		// Token: 0x04003A8C RID: 14988
		public int Level;
	}
}

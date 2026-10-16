using System;
using System.Collections.Generic;
using XRL.Rules;
using XRL.World.AI.GoalHandlers;

namespace XRL.World.Effects
{
	// Token: 0x02000D9C RID: 3484
	[Serializable]
	public class Terrified : Effect, ITierInitialized
	{
		// Token: 0x06008AFC RID: 35580 RVA: 0x003180CE File Offset: 0x003162CE
		public Terrified()
		{
			this.DisplayName = "{{W|terrified}}";
		}

		// Token: 0x06008AFD RID: 35581 RVA: 0x003180E1 File Offset: 0x003162E1
		public Terrified(int Duration)
			: this()
		{
			this.Duration = Duration + 1;
		}

		// Token: 0x06008AFE RID: 35582 RVA: 0x003180F2 File Offset: 0x003162F2
		public Terrified(int Duration, GameObject TerrifiedOf, bool Psionic = false, bool Silent = false)
			: this(Duration)
		{
			this.TerrifiedOfID = TerrifiedOf.ID;
			this.TerrifiedOfObject = TerrifiedOf;
			this.Psionic = Psionic;
			this.Silent = Silent;
		}

		// Token: 0x06008AFF RID: 35583 RVA: 0x0031811D File Offset: 0x0031631D
		public Terrified(int Duration, GlobalLocation TerrifiedOfLocation, bool Psionic = false, bool Silent = false)
			: this(Duration)
		{
			this.TerrifiedOfLocation = TerrifiedOfLocation;
			this.Psionic = Psionic;
			this.Silent = Silent;
		}

		// Token: 0x06008B00 RID: 35584 RVA: 0x0031813C File Offset: 0x0031633C
		public Terrified(int Duration, Cell TerrifiedOfCell)
			: this(Duration, new GlobalLocation(TerrifiedOfCell), false, false)
		{
		}

		// Token: 0x06008B01 RID: 35585 RVA: 0x0031814D File Offset: 0x0031634D
		public Terrified(int Duration, Cell TerrifiedOfCell, bool Psionic = false, bool Silent = false)
			: this(Duration, TerrifiedOfCell)
		{
			this.Psionic = Psionic;
			this.Silent = Silent;
		}

		// Token: 0x06008B02 RID: 35586 RVA: 0x00318166 File Offset: 0x00316366
		public void Initialize(int Tier)
		{
			this.Duration = Stat.Random(5, 10);
		}

		// Token: 0x06008B03 RID: 35587 RVA: 0x00318176 File Offset: 0x00316376
		public override bool UseStandardDurationCountdown()
		{
			return true;
		}

		// Token: 0x06008B04 RID: 35588 RVA: 0x0031817C File Offset: 0x0031637C
		public override int GetEffectType()
		{
			int num = 117440514;
			if (this.Psionic)
			{
				num |= 32768;
			}
			return num;
		}

		// Token: 0x06008B05 RID: 35589 RVA: 0x003181A0 File Offset: 0x003163A0
		public override string GetDescription()
		{
			return "{{W|terrified}}";
		}

		// Token: 0x06008B06 RID: 35590 RVA: 0x003181A7 File Offset: 0x003163A7
		public override string GetDetails()
		{
			return "Fleeing in panic.";
		}

		// Token: 0x06008B07 RID: 35591 RVA: 0x003181B0 File Offset: 0x003163B0
		public override bool Apply(GameObject Object)
		{
			if (!Object.FireEvent("CanApplyFear") || !Object.FireEvent("ApplyFear"))
			{
				return false;
			}
			Object.RemoveEffect<Terrified>();
			if (!this.CheckGoal())
			{
				return false;
			}
			Object.ParticleText("&W!", false);
			if (!this.Silent)
			{
				base.DidX("are", "overwhelmed with terror", "!", null, null, null, Object, false, false, null, null, false, false, false, false, false, null);
			}
			Object.FireEvent("FearApplied");
			return true;
		}

		// Token: 0x06008B08 RID: 35592 RVA: 0x0031822E File Offset: 0x0031642E
		public override void Remove(GameObject Object)
		{
			IMovementGoal goal = this.Goal;
			if (goal != null)
			{
				goal.FailToParent();
			}
			base.Remove(Object);
		}

		// Token: 0x06008B09 RID: 35593 RVA: 0x00318248 File Offset: 0x00316448
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<BeginTakeActionEvent>.ID;
		}

		// Token: 0x06008B0A RID: 35594 RVA: 0x0031825E File Offset: 0x0031645E
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			if (this.Duration > 0)
			{
				this.CheckGoal();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008B0B RID: 35595 RVA: 0x00318277 File Offset: 0x00316477
		private bool CheckGoal()
		{
			return this.Goal != null || this.SetUpGoal();
		}

		// Token: 0x06008B0C RID: 35596 RVA: 0x00318289 File Offset: 0x00316489
		private bool SetUpGoal()
		{
			return this.SetUpObjectGoal() || this.SetUpLocationGoal();
		}

		// Token: 0x06008B0D RID: 35597 RVA: 0x0031829C File Offset: 0x0031649C
		private bool SetUpObjectGoal()
		{
			if (this.TerrifiedOfID.IsNullOrEmpty())
			{
				Cell currentCell = base.Object.CurrentCell;
				List<GameObject> list = currentCell.ParentZone.FastSquareVisibility(currentCell.X, currentCell.Y, 6, "Physics", base.Object, false, false, null);
				if (list.Count <= 0)
				{
					return false;
				}
				this.TerrifiedOfID = list.GetRandomElement<GameObject>(null).ID;
			}
			if (!GameObject.Validate(ref this.TerrifiedOfObject))
			{
				this.TerrifiedOfObject = GameObject.FindByID(this.TerrifiedOfID);
			}
			if (this.TerrifiedOfObject == null)
			{
				return false;
			}
			if (base.Object.Brain == null)
			{
				return false;
			}
			this.Goal = new Flee(this.TerrifiedOfObject, -1, true, true);
			base.Object.Brain.PushGoal(this.Goal);
			return true;
		}

		// Token: 0x06008B0E RID: 35598 RVA: 0x0031836C File Offset: 0x0031656C
		private bool SetUpLocationGoal()
		{
			if (this.TerrifiedOfLocation == null)
			{
				return false;
			}
			if (base.Object.Brain == null)
			{
				return false;
			}
			Cell cell = this.TerrifiedOfLocation.ResolveCell();
			if (cell == null)
			{
				return false;
			}
			this.Goal = new FleeLocation(cell, -1, true, false);
			base.Object.Brain.PushGoal(this.Goal);
			return true;
		}

		// Token: 0x06008B0F RID: 35599 RVA: 0x003183CA File Offset: 0x003165CA
		public static bool OfAttacker(MentalAttackEvent E)
		{
			return Terrified.Attack(E, E.Attacker, null, false, false);
		}

		// Token: 0x06008B10 RID: 35600 RVA: 0x003183DB File Offset: 0x003165DB
		public static bool OfCell(MentalAttackEvent E)
		{
			return Terrified.Attack(E, null, E.Attacker.CurrentCell, false, false);
		}

		// Token: 0x06008B11 RID: 35601 RVA: 0x003183F4 File Offset: 0x003165F4
		public static bool Attack(MentalAttackEvent E, GameObject Object = null, Cell Cell = null, bool Psionic = false, bool Silent = false)
		{
			if (E.Penetrations > 0)
			{
				if (Object != null)
				{
					Terrified terrified = new Terrified(E.Magnitude, Object, Psionic, Silent);
					if (E.Defender.ApplyEffect(terrified, null))
					{
						return true;
					}
				}
				else if (Cell != null)
				{
					Terrified terrified2 = new Terrified(E.Magnitude, Cell, Psionic, Silent);
					if (E.Defender.ApplyEffect(terrified2, null))
					{
						return true;
					}
				}
			}
			IComponent<GameObject>.XDidY(E.Defender, "resist", "becoming afraid", null, null, null, E.Defender, null, false, false, null, null, false, false, false, false, false, null);
			return false;
		}

		// Token: 0x04003AC0 RID: 15040
		public string TerrifiedOfID;

		// Token: 0x04003AC1 RID: 15041
		public GlobalLocation TerrifiedOfLocation;

		// Token: 0x04003AC2 RID: 15042
		public bool Silent;

		// Token: 0x04003AC3 RID: 15043
		public bool Psionic;

		// Token: 0x04003AC4 RID: 15044
		[NonSerialized]
		private IMovementGoal Goal;

		// Token: 0x04003AC5 RID: 15045
		[NonSerialized]
		private GameObject TerrifiedOfObject;
	}
}

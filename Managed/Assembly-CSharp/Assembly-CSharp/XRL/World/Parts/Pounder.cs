using System;

namespace XRL.World.Parts
{
	/// This part is not used in the base game.
	// Token: 0x02000EF1 RID: 3825
	[Serializable]
	public class Pounder : IActivePart
	{
		// Token: 0x060098D2 RID: 39122 RVA: 0x00377A5A File Offset: 0x00375C5A
		public Pounder()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x060098D3 RID: 39123 RVA: 0x00377A70 File Offset: 0x00375C70
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x060098D4 RID: 39124 RVA: 0x00377A73 File Offset: 0x00375C73
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetToHitModifierEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x060098D5 RID: 39125 RVA: 0x00377A91 File Offset: 0x00375C91
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x060098D6 RID: 39126 RVA: 0x00377A94 File Offset: 0x00375C94
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.CheckTargetAdjacency();
		}

		// Token: 0x060098D7 RID: 39127 RVA: 0x00377A9C File Offset: 0x00375C9C
		public override bool HandleEvent(GetToHitModifierEvent E)
		{
			if (E.Checking == "Actor" && this.IsObjectActivePartSubject(E.Actor) && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				E.Modifier += this.GetPounderBonus();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x060098D8 RID: 39128 RVA: 0x00377B0C File Offset: 0x00375D0C
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			int pounderBonus = this.GetPounderBonus();
			E.Postfix.AppendRules(string.Concat(new string[]
			{
				"Pounder: Receives +1 to its to-hit and penetration rolls for every ",
				this.TurnsPerBonus.Things("turn", null),
				" ",
				this.ParentObject.IsCreature ? this.ParentObject.itis : (this.ParentObject.its + " wielder is"),
				" next to ",
				this.ParentObject.IsCreature ? this.ParentObject.its : "their",
				" target. (currently ",
				pounderBonus.Signed(false),
				")"
			}), base.GetEventSensitiveAddStatusSummary(E));
			return base.HandleEvent(E);
		}

		// Token: 0x060098D9 RID: 39129 RVA: 0x00377BE0 File Offset: 0x00375DE0
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x060098DA RID: 39130 RVA: 0x00377BE3 File Offset: 0x00375DE3
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AttackerGetWeaponPenModifier");
			Registrar.Register("GetWeaponPenModifier");
			base.Register(Object, Registrar);
		}

		// Token: 0x060098DB RID: 39131 RVA: 0x00377C03 File Offset: 0x00375E03
		public int GetPounderBonus()
		{
			return this.TurnsNextToTarget / this.TurnsPerBonus;
		}

		// Token: 0x060098DC RID: 39132 RVA: 0x00377C14 File Offset: 0x00375E14
		public override bool FireEvent(Event E)
		{
			if ((E.ID == "GetWeaponPenModifier" || E.ID == "AttackerGetWeaponPenModifier") && base.WasReady())
			{
				E.SetParameter("Penetrations", E.GetIntParameter("Penetrations", 0) + this.GetPounderBonus());
			}
			return base.FireEvent(E);
		}

		// Token: 0x060098DD RID: 39133 RVA: 0x00377C74 File Offset: 0x00375E74
		public void CheckTargetAdjacency()
		{
			GameObject gameObject = this.ParentObject.Equipped ?? this.ParentObject;
			GameObject target = gameObject.Target;
			if (target == null || gameObject.DistanceTo(target) > 1 || !base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				this.TurnsNextToTarget = 0;
				return;
			}
			this.TurnsNextToTarget++;
		}

		// Token: 0x04003FA3 RID: 16291
		public int TurnsPerBonus = 1;

		// Token: 0x04003FA4 RID: 16292
		public int TurnsNextToTarget;
	}
}

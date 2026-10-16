using System;
using XRL.Rules;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x020010DE RID: 4318
	[Serializable]
	public class EmitGasOnHit : IActivePart
	{
		// Token: 0x0600AB6B RID: 43883 RVA: 0x003EBD78 File Offset: 0x003E9F78
		public EmitGasOnHit()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600AB6C RID: 43884 RVA: 0x003EBDD0 File Offset: 0x003E9FD0
		public override bool SameAs(IPart p)
		{
			EmitGasOnHit emitGasOnHit = p as EmitGasOnHit;
			return emitGasOnHit.Chance == this.Chance && emitGasOnHit.ChanceEach == this.ChanceEach && !(emitGasOnHit.GasBlueprint != this.GasBlueprint) && !(emitGasOnHit.CellDensity != this.CellDensity) && !(emitGasOnHit.AdjacentDensity != this.AdjacentDensity) && !(emitGasOnHit.BehaviorDescription != this.BehaviorDescription) && base.SameAs(p);
		}

		// Token: 0x0600AB6D RID: 43885 RVA: 0x003EBE5F File Offset: 0x003EA05F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (ID == GetShortDescriptionEvent.ID && !string.IsNullOrEmpty(this.BehaviorDescription));
		}

		// Token: 0x0600AB6E RID: 43886 RVA: 0x003EBE85 File Offset: 0x003EA085
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (!string.IsNullOrEmpty(this.BehaviorDescription))
			{
				E.Postfix.AppendRules(this.BehaviorDescription);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AB6F RID: 43887 RVA: 0x003EBEAD File Offset: 0x003EA0AD
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600AB70 RID: 43888 RVA: 0x003EBEB0 File Offset: 0x003EA0B0
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("AttackerHit");
			Registrar.Register("ProjectileHit");
			Registrar.Register("WeaponHit");
			Registrar.Register("WeaponThrowHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AB71 RID: 43889 RVA: 0x003EBEE8 File Offset: 0x003EA0E8
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ProjectileHit" || E.ID == "WeaponHit" || E.ID == "AttackerHit" || E.ID == "WeaponThrowHit")
			{
				this.CheckApply(E);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600AB72 RID: 43890 RVA: 0x003EBF4C File Offset: 0x003EA14C
		public int CheckApply(Event E)
		{
			if (base.IsDisabled(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				return 0;
			}
			GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
			GameObject gameObjectParameter2 = E.GetGameObjectParameter("Defender");
			GameObject gameObjectParameter3 = E.GetGameObjectParameter("Weapon");
			GameObject gameObjectParameter4 = E.GetGameObjectParameter("Projectile");
			Cell cell = E.GetParameter("ImpactCell") as Cell;
			GameObject gameObject = gameObjectParameter;
			GameObject parentObject = this.ParentObject;
			string text = "Part EmitGasOnHit Activation Main";
			GameObject gameObject2 = gameObjectParameter2;
			GameObject gameObject3 = gameObjectParameter4;
			if (!GetSpecialEffectChanceEvent.GetFor(gameObject, parentObject, text, this.Chance, gameObject2, gameObject3, true, false).in100())
			{
				return 0;
			}
			GameObject gameObject4 = gameObjectParameter;
			GameObject parentObject2 = this.ParentObject;
			string text2 = "Part EmitGasOnHit Activation Each";
			gameObject3 = gameObjectParameter2;
			gameObject2 = gameObjectParameter4;
			int @for = GetSpecialEffectChanceEvent.GetFor(gameObject4, parentObject2, text2, this.ChanceEach, gameObject3, gameObject2, true, false);
			GameObject gameObject5;
			if ((gameObject5 = gameObjectParameter4) == null)
			{
				gameObject5 = gameObjectParameter3 ?? gameObjectParameter;
			}
			GameObject gameObject6 = gameObject5;
			bool flag = gameObject6 != null && gameObject6.HasEffect<Phased>();
			bool flag2 = gameObject6 != null && gameObject6.HasEffect<Omniphase>();
			return this.EmitGas(gameObjectParameter, gameObjectParameter2, cell, @for, flag, flag2);
		}

		// Token: 0x0600AB73 RID: 43891 RVA: 0x003EC050 File Offset: 0x003EA250
		public int EmitGas(GameObject Creator, GameObject Object, Cell ImpactCell, int UseChanceEach, bool Phased, bool Omniphase)
		{
			int num = 0;
			Cell cell = ((Object != null) ? Object.CurrentCell : null);
			if (cell == null)
			{
				cell = ImpactCell;
			}
			if (cell != null)
			{
				Event @event = Event.New("CreatorModifyGas", "Gas", null);
				this.EmitGas(cell, Creator, @event, this.CellDensity.RollCached(), this.GasLevel.RollCached(), UseChanceEach, Phased, Omniphase);
				foreach (Cell cell2 in cell.GetLocalAdjacentCells())
				{
					this.EmitGas(cell2, Creator, @event, this.AdjacentDensity.RollCached(), this.GasLevel.RollCached(), UseChanceEach, Phased, Omniphase);
				}
			}
			return num;
		}

		// Token: 0x0600AB74 RID: 43892 RVA: 0x003EC118 File Offset: 0x003EA318
		public bool EmitGas(Cell C, GameObject Creator, Event E, int Density, int Level, int UseChanceEach, bool Phased, bool Omniphase)
		{
			if (!UseChanceEach.in100())
			{
				return false;
			}
			GameObject gameObject = C.GetFirstObject(this.GasBlueprint);
			if (gameObject == null)
			{
				gameObject = GameObject.Create(this.GasBlueprint);
				Gas part = gameObject.GetPart<Gas>();
				part.Density = Density;
				part.Level = Level;
				part.Creator = Creator;
				if (Phased)
				{
					gameObject.ForceApplyEffect(new Phased(Stat.Random(23, 32)), null);
				}
				if (Omniphase)
				{
					gameObject.ForceApplyEffect(new Omniphase(Stat.Random(46, 64)), null);
				}
				E.SetParameter("Gas", part);
				if (Creator != null)
				{
					Creator.FireEvent(E);
				}
				C.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			}
			else
			{
				Gas part2 = gameObject.GetPart<Gas>();
				part2.Density += Density;
				if (part2.Level < Level || part2.Density < Density * 2)
				{
					part2.Level = Level;
				}
			}
			return true;
		}

		// Token: 0x040046B3 RID: 18099
		public int Chance = 100;

		// Token: 0x040046B4 RID: 18100
		public int ChanceEach = 100;

		// Token: 0x040046B5 RID: 18101
		public string GasBlueprint = "PoisonGas";

		// Token: 0x040046B6 RID: 18102
		public string CellDensity = "4d10";

		// Token: 0x040046B7 RID: 18103
		public string AdjacentDensity = "2d10";

		// Token: 0x040046B8 RID: 18104
		public string GasLevel = "1";

		// Token: 0x040046B9 RID: 18105
		public string BehaviorDescription;
	}
}

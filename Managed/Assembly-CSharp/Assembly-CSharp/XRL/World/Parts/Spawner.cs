using System;
using XRL.World.AI;

namespace XRL.World.Parts
{
	// Token: 0x02000E8F RID: 3727
	[Serializable]
	public class Spawner : IPart
	{
		// Token: 0x0600961A RID: 38426 RVA: 0x00365EF8 File Offset: 0x003640F8
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID;
		}

		// Token: 0x0600961B RID: 38427 RVA: 0x00365F10 File Offset: 0x00364110
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (!E.Forced && !E.System && !E.Direction.IsNullOrEmpty() && !E.Cell.OnWorldMap())
			{
				if (!string.IsNullOrEmpty(this.TrailBlueprint))
				{
					E.Cell.AddObject(this.TrailBlueprint, null, null, null, null);
				}
				this.Spawn(E.Cell);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600961C RID: 38428 RVA: 0x00365F80 File Offset: 0x00364180
		public void Spawn(Cell Cell)
		{
			if (this.CurrentCooldown > 0)
			{
				return;
			}
			if (this.CombatOnly && !this.ParentObject.IsPlayer() && this.ParentObject.Target == null)
			{
				return;
			}
			if (!this.SpawnChance.in100())
			{
				return;
			}
			if (this.SpawnCheckLimit > 0 && !this.SpawnCheckBlueprint.IsNullOrEmpty() && Cell.ParentZone.CountObjects(this.SpawnCheckBlueprint) >= this.SpawnCheckLimit)
			{
				return;
			}
			if (!this.SpawnCooldown.IsNullOrEmpty())
			{
				this.CurrentCooldown = this.SpawnCooldown.RollCached();
			}
			GameObject gameObject = GameObject.Create(this.SpawnBlueprint);
			if (this.SpawnAdjacent)
			{
				Cell = Cell.GetNavigableAdjacentCells(gameObject, 5, true).GetRandomElement<Cell>(null);
			}
			SpawnVessel part = gameObject.GetPart<SpawnVessel>();
			if (part != null)
			{
				part.SpawnedBy = this.ParentObject;
				if (this.PassAttitudes)
				{
					gameObject.TakeAllegiance<AllyBirth>(this.ParentObject);
					part.AdjustAttitude = true;
				}
			}
			Cell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			if (!this.SpawnMessage.IsNullOrEmpty())
			{
				base.EmitMessage(GameText.VariableReplace(this.SpawnMessage, this.ParentObject, gameObject, false), ' ', false, false, false, null, null);
			}
			if (!this.SpawnVerb.IsNullOrEmpty())
			{
				base.DidXToY(this.SpawnVerb, gameObject, null, null, null, null, this.ParentObject, null, false, false, true, false, false, null, null, null, false, false, false, false, false, null);
			}
		}

		// Token: 0x0600961D RID: 38429 RVA: 0x003660E3 File Offset: 0x003642E3
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600961E RID: 38430 RVA: 0x003660E6 File Offset: 0x003642E6
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (this.CurrentCooldown > 0)
			{
				this.CurrentCooldown -= Amount;
			}
			if (this.SpawnOnTurnTick)
			{
				this.Spawn(this.ParentObject.CurrentCell);
			}
		}

		// Token: 0x0600961F RID: 38431 RVA: 0x00366118 File Offset: 0x00364318
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("VillageInit");
		}

		// Token: 0x06009620 RID: 38432 RVA: 0x00366125 File Offset: 0x00364325
		public override bool FireEvent(Event E)
		{
			if (E.ID == "VillageInit")
			{
				this.CombatOnly = true;
				if (this.VillagePassAttitudes)
				{
					this.PassAttitudes = true;
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003E10 RID: 15888
		public string TrailBlueprint;

		// Token: 0x04003E11 RID: 15889
		public string SpawnBlueprint;

		// Token: 0x04003E12 RID: 15890
		public string SpawnCheckBlueprint;

		// Token: 0x04003E13 RID: 15891
		public string SpawnMessage;

		// Token: 0x04003E14 RID: 15892
		public string SpawnVerb;

		// Token: 0x04003E15 RID: 15893
		public string SpawnCooldown;

		// Token: 0x04003E16 RID: 15894
		public int SpawnChance = 100;

		// Token: 0x04003E17 RID: 15895
		public int SpawnCheckLimit;

		// Token: 0x04003E18 RID: 15896
		public bool SpawnOnEnteredCell;

		// Token: 0x04003E19 RID: 15897
		public bool SpawnOnTurnTick;

		// Token: 0x04003E1A RID: 15898
		public bool SpawnAdjacent;

		// Token: 0x04003E1B RID: 15899
		public bool PassAttitudes = true;

		// Token: 0x04003E1C RID: 15900
		public bool VillagePassAttitudes = true;

		// Token: 0x04003E1D RID: 15901
		public bool CombatOnly;

		// Token: 0x04003E1E RID: 15902
		public int CurrentCooldown;
	}
}

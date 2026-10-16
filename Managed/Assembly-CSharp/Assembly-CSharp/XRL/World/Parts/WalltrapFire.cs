using System;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x020012A3 RID: 4771
	[Serializable]
	public class WalltrapFire : IPart
	{
		// Token: 0x0600BC81 RID: 48257 RVA: 0x0044E932 File Offset: 0x0044CB32
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600BC82 RID: 48258 RVA: 0x0044E935 File Offset: 0x0044CB35
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("WalltrapTrigger");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600BC83 RID: 48259 RVA: 0x0044E94A File Offset: 0x0044CB4A
		public bool ShouldFlame(GameObject Object)
		{
			if (!this.ParentObject.PhaseMatches(Object))
			{
				return false;
			}
			if (Object.IsCombatObject())
			{
				return true;
			}
			Object.HasTagOrProperty("Plant");
			return Object.isFurniture();
		}

		// Token: 0x0600BC84 RID: 48260 RVA: 0x0044E980 File Offset: 0x0044CB80
		public void Flame(Cell C)
		{
			string jetDamage = this.JetDamage;
			if (C != null)
			{
				foreach (GameObject gameObject in C.GetObjects(new Predicate<GameObject>(this.ShouldFlame)))
				{
					gameObject.TemperatureChange(this.JetTemperature.RollCached(), this.ParentObject, false, false, false, false, 0, null, null);
					if (25.in100() && IComponent<GameObject>.Visible(gameObject))
					{
						gameObject.Smoke();
					}
				}
				foreach (GameObject gameObject2 in C.GetObjectsViaEventList())
				{
					if (gameObject2.IsCombatObject() || gameObject2.HasPart<CrossFlameOnStep>() || gameObject2.HasPart<BurnOffGas>())
					{
						gameObject2.TakeDamage(jetDamage.RollCached(), "from a {{fiery|jet of flames}}!", "Fire Heat", null, null, null, null, this.ParentObject, null, null, false, true, false, false, false, false, false, false, false, 5, null);
					}
				}
			}
		}

		// Token: 0x0600BC85 RID: 48261 RVA: 0x0044EAB4 File Offset: 0x0044CCB4
		public void FireJet(string D)
		{
			Cell cell = this.ParentObject.CurrentCell;
			for (int i = 0; i < this.JetLength; i++)
			{
				cell = cell.GetCellFromDirection(D, true);
				if (cell == null)
				{
					break;
				}
				this.Flame(cell);
				if (cell.ParentZone.IsActive() && cell.IsVisible())
				{
					cell.Flameburst(null);
				}
				if (cell.IsSolid(true))
				{
					break;
				}
			}
		}

		// Token: 0x0600BC86 RID: 48262 RVA: 0x0044EB18 File Offset: 0x0044CD18
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WalltrapTrigger")
			{
				Cell currentCell = this.ParentObject.CurrentCell;
				if (currentCell != null && !base.IsBroken() && !base.IsRusted() && !base.IsEMPed())
				{
					foreach (string text in Directions.CardinalDirectionList)
					{
						Cell cellFromDirection = currentCell.GetCellFromDirection(text, true);
						if (cellFromDirection != null && !cellFromDirection.IsSolid(true))
						{
							this.FireJet(text);
						}
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004B8C RID: 19340
		public int JetLength = 6;

		// Token: 0x04004B8D RID: 19341
		public string JetDamage = "3d5+12";

		// Token: 0x04004B8E RID: 19342
		public string JetTemperature = "2d50+200";
	}
}

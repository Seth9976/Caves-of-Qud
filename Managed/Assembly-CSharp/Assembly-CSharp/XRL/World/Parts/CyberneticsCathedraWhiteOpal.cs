using System;
using System.Collections.Generic;
using XRL.World.Capabilities;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000F22 RID: 3874
	[Serializable]
	public class CyberneticsCathedraWhiteOpal : CyberneticsCathedra
	{
		// Token: 0x06009A81 RID: 39553 RVA: 0x00380EE8 File Offset: 0x0037F0E8
		public override void OnImplanted(GameObject Object)
		{
			base.OnImplanted(Object);
			this.ActivatedAbilityID = Object.AddActivatedAbility("Glitter Bomb", "CommandActivateCathedra", "Cybernetics", null, "á", null, false, false, false, false, false, false, false, false, true, true, false, false, -1, "CommandCathedraWhiteOpal", null, null, null, null);
		}

		// Token: 0x06009A82 RID: 39554 RVA: 0x00380F34 File Offset: 0x0037F134
		public override void CollectStats(Templates.StatCollector stats)
		{
			base.CollectStats(stats);
			stats.Set("Duration", this.GetDuration(null), false, 0);
		}

		// Token: 0x06009A83 RID: 39555 RVA: 0x00380F54 File Offset: 0x0037F154
		public int GetDuration(GameObject Actor = null)
		{
			int level = base.GetLevel(Actor);
			return 1 + level / 2;
		}

		// Token: 0x06009A84 RID: 39556 RVA: 0x00380F70 File Offset: 0x0037F170
		public override void Activate(GameObject Actor)
		{
			int level = base.GetLevel(Actor);
			this.BillowsTimer = this.GetDuration(null);
			IComponent<GameObject>.XDidY(Actor, "start", "streaming ribbons of {{glittering|glitter}}", "!", null, null, null, null, false, false, null, null, false, false, false, false, false, null);
			CyberneticsCathedraWhiteOpal.GlitterBomb(Actor, level, 800);
			base.Activate(Actor);
		}

		// Token: 0x06009A85 RID: 39557 RVA: 0x00380FC8 File Offset: 0x0037F1C8
		public override bool HandleEvent(EndTurnEvent E)
		{
			if (this.BillowsTimer < 0)
			{
				return true;
			}
			GameObject user = base.User;
			if (user == null)
			{
				this.BillowsTimer = 0;
				return true;
			}
			CyberneticsCathedraWhiteOpal.GlitterBomb(user, base.GetLevel(user), 800);
			this.BillowsTimer--;
			return base.HandleEvent(E);
		}

		// Token: 0x06009A86 RID: 39558 RVA: 0x0038101C File Offset: 0x0037F21C
		public static void GlitterBomb(GameObject Actor, int Level, int Density)
		{
			if (Actor.OnWorldMap())
			{
				return;
			}
			List<Cell> cells = new List<Cell>(8);
			Actor.CurrentCell.ForeachAdjacentCell(delegate(Cell C)
			{
				if (!C.IsOccluding())
				{
					cells.Add(C);
				}
			}, true);
			if (cells.Count == 0)
			{
				cells.Add(Actor.CurrentCell);
			}
			Phased phased;
			Omniphase omniphase;
			Phase.carryOverPrep(Actor, out phased, out omniphase);
			Event @event = Event.New("CreatorModifyGas", "Gas", null);
			if (Actor != null)
			{
				Actor.PlayWorldSound("Sounds/Grenade/sfx_grenade_glitter_explode", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			}
			foreach (Cell cell in cells)
			{
				GameObject gameObject = GameObject.Create("GlitterGas");
				Gas part = gameObject.GetPart<Gas>();
				part.Creator = Actor;
				part.Density = Density / cells.Count;
				part.Level = Level;
				Phase.carryOver(Actor, gameObject, phased, omniphase);
				@event.SetParameter("Gas", part);
				Actor.FireEvent(@event);
				cell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			}
		}

		// Token: 0x04004043 RID: 16451
		public int BillowsTimer = -1;
	}
}

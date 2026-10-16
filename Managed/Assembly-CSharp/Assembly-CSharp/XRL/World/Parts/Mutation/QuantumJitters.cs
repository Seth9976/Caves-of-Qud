using System;
using XRL.Rules;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013D9 RID: 5081
	[Serializable]
	public class QuantumJitters : BaseMutation
	{
		// Token: 0x0600C453 RID: 50259 RVA: 0x0047802C File Offset: 0x0047622C
		public QuantumJitters()
		{
			base.Type = "Mental";
		}

		// Token: 0x0600C454 RID: 50260 RVA: 0x0047803F File Offset: 0x0047623F
		public override bool CanLevel()
		{
			return false;
		}

		// Token: 0x0600C455 RID: 50261 RVA: 0x00478042 File Offset: 0x00476242
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C456 RID: 50262 RVA: 0x00478058 File Offset: 0x00476258
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("chance", this.BaseElementWeight);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C457 RID: 50263 RVA: 0x00478080 File Offset: 0x00476280
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EndTurn");
			Registrar.Register("BeforeCooldownActivatedAbility");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C458 RID: 50264 RVA: 0x004780A0 File Offset: 0x004762A0
		public override string GetDescription()
		{
			return "Your willful acts sometimes dent spacetime.\n\nWhenever you use an activated ability, there's a small chance your focus slips and you dent spacetime in the local region, causing 1-2 spacetime vortices to appear. This chance increases the longer you go without using an activated ability.";
		}

		// Token: 0x0600C459 RID: 50265 RVA: 0x004780A7 File Offset: 0x004762A7
		public override string GetLevelText(int Level)
		{
			return "";
		}

		// Token: 0x0600C45A RID: 50266 RVA: 0x004780B0 File Offset: 0x004762B0
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EndTurn")
			{
				this.counter++;
			}
			else if (E.ID == "BeforeCooldownActivatedAbility" && !E.HasFlag("Involuntary"))
			{
				if (Math.Min(this.counter, 250).in1000())
				{
					this.Sunder();
				}
				this.counter = 0;
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C45B RID: 50267 RVA: 0x0047812C File Offset: 0x0047632C
		public void Sunder()
		{
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell == null || currentCell.ParentZone.IsWorldMap())
			{
				return;
			}
			if (this.ParentObject.IsPlayer())
			{
				IComponent<GameObject>.AddPlayerMessage("Your focus slips, causing you to dent spacetime in the local region.", null, true);
			}
			this.ParentObject.SpatialDistortionBlip(null, "&C");
			int num = Stat.Random(1, 2);
			foreach (Cell cell in currentCell.GetLocalAdjacentCells().InRandomOrder<Cell>(null))
			{
				SpaceTimeVortex part = cell.AddObject("Space-Time Vortex", null, null, null, null).GetPart<SpaceTimeVortex>();
				if (part != null)
				{
					part.Delay = 1;
				}
				num--;
				if (num == 0)
				{
					break;
				}
			}
		}

		// Token: 0x0600C45C RID: 50268 RVA: 0x004781F0 File Offset: 0x004763F0
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x04004DD3 RID: 19923
		public int counter;
	}
}

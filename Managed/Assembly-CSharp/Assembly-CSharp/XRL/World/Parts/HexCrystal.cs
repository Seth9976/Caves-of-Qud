using System;
using XRL.Rules;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	// Token: 0x02001270 RID: 4720
	[Serializable]
	public class HexCrystal : IPart
	{
		// Token: 0x0600BB01 RID: 47873 RVA: 0x00444F8E File Offset: 0x0044318E
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x0600BB02 RID: 47874 RVA: 0x00444F91 File Offset: 0x00443191
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AfterDieEvent.ID;
		}

		// Token: 0x0600BB03 RID: 47875 RVA: 0x00444FA7 File Offset: 0x004431A7
		public bool IsValidTarget(GameObject Object)
		{
			return GameObject.Validate(Object) && Object.IsCreature && !Object.HasPart<MentalShield>();
		}

		// Token: 0x0600BB04 RID: 47876 RVA: 0x00444FC4 File Offset: 0x004431C4
		public override bool HandleEvent(AfterDieEvent E)
		{
			base.PlayWorldSound("shatter", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
			this.ParentObject.CrystalSpray();
			if (this.IsValidTarget(E.Killer) && 3.in100())
			{
				Cell currentCell = this.ParentObject.CurrentCell;
				int num = Stat.Random(1, 6);
				int i = 0;
				int num2 = num;
				while (i < num2)
				{
					Cell closestPassableCellFor = currentCell.getClosestPassableCellFor(E.Killer);
					if (closestPassableCellFor != null)
					{
						EvilTwin.CreateEvilTwin(E.Killer, "anti-", closestPassableCellFor, null, "&K", null, null, true, "It's negative you.", null);
					}
					i++;
				}
				if (num == 1)
				{
					E.Killer.EmitMessage("As the prism shatters, a reflection of " + E.Killer.t(int.MaxValue, null, null, false, false, false, false, false, true, true, true, null, false, true, false, null, true, null, false) + " is caught on the limen of realities and appears out of nowhere.", null, null, false);
				}
				else
				{
					E.Killer.EmitMessage("As the prism shatters, reflections of " + E.Killer.t(int.MaxValue, null, null, false, false, false, false, false, true, true, true, null, false, true, false, null, true, null, false) + " are caught on the limen of realities and appear out of nowhere.", null, null, false);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04004B14 RID: 19220
		public const int REFLECTION_CHANCE = 3;
	}
}

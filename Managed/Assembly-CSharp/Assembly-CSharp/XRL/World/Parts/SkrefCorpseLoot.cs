using System;
using Qud.API;
using XRL.Rules;
using XRL.UI;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000F6E RID: 3950
	[Serializable]
	public class SkrefCorpseLoot : IPart
	{
		// Token: 0x06009D2C RID: 40236 RVA: 0x0038EA6B File Offset: 0x0038CC6B
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009D2D RID: 40237 RVA: 0x0038EA6E File Offset: 0x0038CC6E
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EnteredCell");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009D2E RID: 40238 RVA: 0x0038EA84 File Offset: 0x0038CC84
		public override bool Render(RenderEvent E)
		{
			if (!this.bSeen)
			{
				this.bSeen = true;
				Popup.ShowSpace("You stumble upon some flattened remains.", null, "Sounds/UI/ui_notification", null, true, true, null);
				JournalAPI.AddAccomplishment("You stumbled upon some flattened remains.", string.Concat(new string[]
				{
					"On the ",
					Calendar.GetDay(),
					" of ",
					Calendar.GetMonth(),
					", a fallen cherub gifted his broken wings to the traveler =name=."
				}), string.Concat(new string[]
				{
					"On the ",
					Calendar.GetDay(),
					" of ",
					Calendar.GetMonth(),
					", =name= stumbled upon the flatted remains of a vainglorious humanoid. From that day forth, =name= always kept some mechanical wings on ",
					The.Player.GetPronounProvider(false).PossessiveAdjective,
					" person."
				}), null, "general", MuralCategory.FindsObject, MuralWeight.High, null, -1L, true);
				Achievement.FIND_APPRENTICE.Unlock();
				JournalMapNote mapNote = JournalAPI.GetMapNote("$skrefcorpse");
				if (mapNote != null)
				{
					mapNote.Reveal(null, false);
				}
			}
			return true;
		}

		// Token: 0x06009D2F RID: 40239 RVA: 0x0038EB74 File Offset: 0x0038CD74
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EnteredCell")
			{
				if (this.bCreated)
				{
					return true;
				}
				this.bCreated = true;
				Cell currentCell = this.ParentObject.CurrentCell;
				GameObject gameObject = GameObject.Create("Mechanical Wings");
				gameObject.ApplyEffect(new Broken(), null);
				currentCell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				currentCell.AddObject(GameObject.Create("Wire Strand 50"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				if (25.in100())
				{
					currentCell.AddObject(GameObject.Create("Wire Strand 50"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				}
				if (25.in100())
				{
					currentCell.AddObject(GameObject.Create("Wire Strand 20"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				}
				if (25.in100())
				{
					currentCell.AddObject(GameObject.Create("Wire Strand 10"), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				}
				int i = 0;
				int num = Stat.Random(1, 2);
				while (i < num)
				{
					currentCell.AddObject(PopulationManager.CreateOneFrom("Junk 3", null, 0, 0, null, null, null, null, null), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
					i++;
				}
				currentCell.AddObject(PopulationManager.CreateOneFrom("Armor 2", null, 0, 0, null, null, null, null, null), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				currentCell.AddObject(PopulationManager.CreateOneFrom("Armor 1", null, 0, 0, null, null, null, null, null), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				currentCell.AddObject(PopulationManager.CreateOneFrom("Melee Weapons 2", null, 0, 0, null, null, null, null, null), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				this.ParentObject.UnregisterPartEvent(this, "EnteredCell");
				this.ParentObject.Bloodsplatter();
			}
			return base.FireEvent(E);
		}

		// Token: 0x040040F2 RID: 16626
		public bool bCreated;

		// Token: 0x040040F3 RID: 16627
		public bool bSeen;
	}
}

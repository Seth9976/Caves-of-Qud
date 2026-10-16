using System;
using Qud.API;

namespace XRL.World.Parts
{
	// Token: 0x02000DED RID: 3565
	[Serializable]
	public class TakenAccomplishment : IPart
	{
		// Token: 0x0600908E RID: 37006 RVA: 0x00342498 File Offset: 0x00340698
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x0600908F RID: 37007 RVA: 0x0034249B File Offset: 0x0034069B
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EquippedEvent.ID || ID == TakenEvent.ID;
		}

		// Token: 0x06009090 RID: 37008 RVA: 0x003424B9 File Offset: 0x003406B9
		public override bool HandleEvent(EquippedEvent E)
		{
			this.Trigger(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x06009091 RID: 37009 RVA: 0x003424CE File Offset: 0x003406CE
		public override bool HandleEvent(TakenEvent E)
		{
			this.Trigger(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x06009092 RID: 37010 RVA: 0x003424E4 File Offset: 0x003406E4
		public void Trigger(GameObject who)
		{
			if (!this.Triggered && who != null && who.IsPlayer())
			{
				string text = this.Text.StartReplace().AddObject(this.ParentObject, "this").ToString();
				string text2 = this.Hagiograph.StartReplace().AddObject(this.ParentObject, "this").ForceThirdPerson()
					.ToString();
				MuralCategory muralCategory = MuralCategoryHelpers.parseCategory(this.HagiographCategory);
				MuralWeight muralWeight = MuralCategoryHelpers.parseWeight(this.HagiographWeight);
				JournalAPI.AddAccomplishment(text, text2, this.Gospel.StartReplace().AddObject(this.ParentObject, "this").ForceThirdPerson()
					.ToString(), null, "general", muralCategory, muralWeight, null, -1L, true);
				this.Triggered = true;
			}
		}

		// Token: 0x04003C2E RID: 15406
		public string Text = "You got it!";

		// Token: 0x04003C2F RID: 15407
		public bool Triggered;

		// Token: 0x04003C30 RID: 15408
		public string Hagiograph;

		// Token: 0x04003C31 RID: 15409
		public string HagiographCategory;

		// Token: 0x04003C32 RID: 15410
		public string HagiographWeight;

		// Token: 0x04003C33 RID: 15411
		public string Gospel;
	}
}

using System;
using Qud.API;

namespace XRL.World.Parts
{
	// Token: 0x02000DEE RID: 3566
	[Serializable]
	public class EatenAccomplishment : IPart
	{
		// Token: 0x06009094 RID: 37012 RVA: 0x003425BA File Offset: 0x003407BA
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x06009095 RID: 37013 RVA: 0x003425BD File Offset: 0x003407BD
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (ID == AfterInventoryActionEvent.ID && !this.Triggered);
		}

		// Token: 0x06009096 RID: 37014 RVA: 0x003425E0 File Offset: 0x003407E0
		public override bool HandleEvent(AfterInventoryActionEvent E)
		{
			if (E.Command == "Eat" && !this.Triggered && E.Actor.IsPlayer())
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
			return base.HandleEvent(E);
		}

		// Token: 0x04003C34 RID: 15412
		public string Text = "You ate it!";

		// Token: 0x04003C35 RID: 15413
		public bool Triggered;

		// Token: 0x04003C36 RID: 15414
		public string Hagiograph;

		// Token: 0x04003C37 RID: 15415
		public string HagiographCategory;

		// Token: 0x04003C38 RID: 15416
		public string HagiographWeight;

		// Token: 0x04003C39 RID: 15417
		public string Gospel;
	}
}

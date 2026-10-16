using System;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02001061 RID: 4193
	[Serializable]
	public class MakeFussOnTaken : IPart
	{
		// Token: 0x0600A6E7 RID: 42727 RVA: 0x003D0613 File Offset: 0x003CE813
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600A6E8 RID: 42728 RVA: 0x003D0616 File Offset: 0x003CE816
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EquippedEvent.ID || ID == TakenEvent.ID;
		}

		// Token: 0x0600A6E9 RID: 42729 RVA: 0x003D0634 File Offset: 0x003CE834
		public override bool HandleEvent(EquippedEvent E)
		{
			this.MakeFuss(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A6EA RID: 42730 RVA: 0x003D0649 File Offset: 0x003CE849
		public override bool HandleEvent(TakenEvent E)
		{
			this.MakeFuss(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A6EB RID: 42731 RVA: 0x003D0660 File Offset: 0x003CE860
		public void MakeFuss(GameObject Actor)
		{
			if (Actor != null && Actor.IsPlayer())
			{
				if (this.ParentObject.Understood())
				{
					CompleteQuestOnTaken part = this.ParentObject.GetPart<CompleteQuestOnTaken>();
					if (part == null || !The.Game.HasQuest(part.Quest) || The.Game.FinishedQuest(part.Quest))
					{
						Popup.Show(string.Concat(new string[]
						{
							"You have ",
							this.Action,
							" ",
							this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
							"!"
						}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
				}
				this.ParentObject.RemovePart(this);
			}
		}

		// Token: 0x040044F6 RID: 17654
		public string Action = "found";
	}
}

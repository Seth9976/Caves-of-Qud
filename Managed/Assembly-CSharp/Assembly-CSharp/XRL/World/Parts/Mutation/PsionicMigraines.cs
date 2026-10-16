using System;
using XRL.UI;
using XRL.World.Anatomy;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013D8 RID: 5080
	[Serializable]
	public class PsionicMigraines : BaseMutation
	{
		// Token: 0x0600C44B RID: 50251 RVA: 0x00477EFA File Offset: 0x004760FA
		public PsionicMigraines()
		{
			base.Type = "Mental";
		}

		// Token: 0x0600C44C RID: 50252 RVA: 0x00477F0D File Offset: 0x0047610D
		public override bool CanLevel()
		{
			return false;
		}

		// Token: 0x0600C44D RID: 50253 RVA: 0x00477F10 File Offset: 0x00476110
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeginEquip");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C44E RID: 50254 RVA: 0x00477F25 File Offset: 0x00476125
		public override string GetDescription()
		{
			return "You suffer from powerful psionic migraines that render your head extremely sensitive.\n\nYou can't wear hats or helmets.";
		}

		// Token: 0x0600C44F RID: 50255 RVA: 0x00477F2C File Offset: 0x0047612C
		public override string GetLevelText(int Level)
		{
			return "";
		}

		// Token: 0x0600C450 RID: 50256 RVA: 0x00477F34 File Offset: 0x00476134
		public override bool FireEvent(Event E)
		{
			if (E.ID == "BeginEquip" && (E.GetParameter("BodyPart") as BodyPart).Type == "Head")
			{
				if (this.ParentObject.IsPlayer() && !E.HasParameter("AutoEquipTry") && !E.IsSilent())
				{
					Popup.Show("Your head is too sensitive to wear that.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				return false;
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C451 RID: 50257 RVA: 0x00477FB4 File Offset: 0x004761B4
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C452 RID: 50258 RVA: 0x00477FC0 File Offset: 0x004761C0
		public override bool Mutate(GameObject GO, int Level)
		{
			Body body = GO.Body;
			if (body != null)
			{
				foreach (BodyPart bodyPart in body.GetPart("Head"))
				{
					bodyPart.UnequipPartAndChildren(false, null, false);
				}
			}
			return base.Mutate(GO, Level);
		}
	}
}

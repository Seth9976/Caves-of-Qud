using System;
using XRL.World.Anatomy;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02001175 RID: 4469
	[Serializable]
	public class ModTwoFaced : IModification
	{
		// Token: 0x0600B1D0 RID: 45520 RVA: 0x00412BB7 File Offset: 0x00410DB7
		public ModTwoFaced()
		{
		}

		// Token: 0x0600B1D1 RID: 45521 RVA: 0x00412BBF File Offset: 0x00410DBF
		public ModTwoFaced(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B1D2 RID: 45522 RVA: 0x00412BC8 File Offset: 0x00410DC8
		public override void Configure()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600B1D3 RID: 45523 RVA: 0x00412BD4 File Offset: 0x00410DD4
		public override bool ModificationApplicable(GameObject Object)
		{
			return IModification.CheckWornSlot(Object, "Head", null, null, null, true, true, true, false);
		}

		// Token: 0x0600B1D4 RID: 45524 RVA: 0x00412BF3 File Offset: 0x00410DF3
		public override void ApplyModification(GameObject Object)
		{
			base.IncreaseDifficultyIfComplex(1, null);
		}

		// Token: 0x0600B1D5 RID: 45525 RVA: 0x00412BFD File Offset: 0x00410DFD
		public override bool SameAs(IPart Part)
		{
			return (Part as ModTwoFaced).ExtraFaceID == this.ExtraFaceID && base.SameAs(Part);
		}

		// Token: 0x0600B1D6 RID: 45526 RVA: 0x00412C1B File Offset: 0x00410E1B
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<BeforeDismemberEvent>.ID || ID == EquippedEvent.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID || ID == UnequippedEvent.ID;
		}

		// Token: 0x0600B1D7 RID: 45527 RVA: 0x00412C51 File Offset: 0x00410E51
		public override bool HandleEvent(BeforeDismemberEvent E)
		{
			if (E.IsObject && E.Part != null && E.Part.IDMatch(this.ExtraFaceID))
			{
				this.ParentObject.ApplyEffect(new Broken(), null);
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B1D8 RID: 45528 RVA: 0x00412C91 File Offset: 0x00410E91
		public override bool HandleEvent(EquippedEvent E)
		{
			this.AddFace(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B1D9 RID: 45529 RVA: 0x00412CA6 File Offset: 0x00410EA6
		public override bool HandleEvent(UnequippedEvent E)
		{
			this.RemoveFace(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B1DA RID: 45530 RVA: 0x00412CBB File Offset: 0x00410EBB
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Understood() && !E.Object.HasProperName)
			{
				E.AddAdjective("two-faced", 0);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B1DB RID: 45531 RVA: 0x00412CE5 File Offset: 0x00410EE5
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(ModTwoFaced.GetDescription(this.Tier));
			return base.HandleEvent(E);
		}

		// Token: 0x0600B1DC RID: 45532 RVA: 0x00412D05 File Offset: 0x00410F05
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B1DD RID: 45533 RVA: 0x00412D08 File Offset: 0x00410F08
		public static string GetDescription(int Tier)
		{
			return "Two-faced: This item grants an additional face slot.";
		}

		// Token: 0x0600B1DE RID: 45534 RVA: 0x00412D10 File Offset: 0x00410F10
		public void AddFace(GameObject Subject = null)
		{
			if (Subject == null)
			{
				Subject = this.ParentObject.Equipped;
			}
			if (Subject == null)
			{
				return;
			}
			BodyPart bodyPart = this.ParentObject.EquippedOn();
			if (bodyPart == null)
			{
				return;
			}
			BodyPart bodyPart2 = bodyPart;
			string text = "Extra Face";
			int num = 0;
			string text2 = null;
			string text3 = null;
			string text4 = null;
			string text5 = null;
			string text6 = null;
			int? num2 = new int?(BodyPartCategory.BestGuessForCategoryDerivedFromGameObject(this.ParentObject));
			bool? flag = new bool?(true);
			string[] array = new string[] { "Fungal Outcrop", "Icy Outcrop" };
			BodyPart bodyPart3 = bodyPart2.AddPartAt(text, num, text2, text3, text4, text5, text6, num2, null, null, null, null, null, null, flag, null, null, null, null, null, "Face", array, true);
			this.ExtraFaceID = bodyPart3.ID;
			Subject.WantToReequip();
		}

		// Token: 0x0600B1DF RID: 45535 RVA: 0x00412E0C File Offset: 0x0041100C
		public void RemoveFace(GameObject Subject = null)
		{
			if (this.ExtraFaceID == 0)
			{
				return;
			}
			if (Subject == null)
			{
				Subject = this.ParentObject.Equipped;
			}
			if (Subject != null)
			{
				Body body = Subject.Body;
				if (body != null)
				{
					body.RemovePartByID(this.ExtraFaceID);
				}
			}
			this.ExtraFaceID = 0;
			if (Subject != null)
			{
				Subject.WantToReequip();
			}
		}

		// Token: 0x0400482D RID: 18477
		public int ExtraFaceID;
	}
}

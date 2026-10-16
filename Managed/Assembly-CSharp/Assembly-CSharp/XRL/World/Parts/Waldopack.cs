using System;
using XRL.UI;
using XRL.World.Anatomy;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	/// This part is not used in the base game.
	// Token: 0x020010BC RID: 4284
	[Serializable]
	public class Waldopack : IPoweredPart
	{
		// Token: 0x0600AA9C RID: 43676 RVA: 0x003E67DE File Offset: 0x003E49DE
		public Waldopack()
		{
			this.WorksOnEquipper = true;
			this.NameForStatus = "ServoSystems";
		}

		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x0600AA9D RID: 43677 RVA: 0x003E67F8 File Offset: 0x003E49F8
		public string ManagerID
		{
			get
			{
				return this.ParentObject.ID + "::Waldopack";
			}
		}

		// Token: 0x0600AA9E RID: 43678 RVA: 0x003E680F File Offset: 0x003E4A0F
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600AA9F RID: 43679 RVA: 0x003E6812 File Offset: 0x003E4A12
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<BeforeDismemberEvent>.ID || ID == EquippedEvent.ID || ID == GetShortDescriptionEvent.ID || ID == GetTinkeringBonusEvent.ID || ID == UnequippedEvent.ID || ID == PooledEvent<GetMeleeAttackChanceEvent>.ID;
		}

		// Token: 0x0600AAA0 RID: 43680 RVA: 0x003E6850 File Offset: 0x003E4A50
		public override bool HandleEvent(BeforeDismemberEvent E)
		{
			if (E.IsObject)
			{
				BodyPart part = E.Part;
				if (((part != null) ? part.Manager : null) != null && E.Part.Manager == this.ManagerID)
				{
					this.ParentObject.ApplyEffect(new Broken(), null);
					return false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AAA1 RID: 43681 RVA: 0x003E68AC File Offset: 0x003E4AAC
		public override bool HandleEvent(EquippedEvent E)
		{
			if (base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				this.AddArm(E.Actor);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AAA2 RID: 43682 RVA: 0x003E68F4 File Offset: 0x003E4AF4
		public override bool HandleEvent(UnequippedEvent E)
		{
			this.RemoveArm(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600AAA3 RID: 43683 RVA: 0x003E690C File Offset: 0x003E4B0C
		public override bool HandleEvent(GetMeleeAttackChanceEvent E)
		{
			if (E.Intrinsic && !E.Primary)
			{
				BodyPart bodyPart = E.BodyPart;
				if (((bodyPart != null) ? bodyPart.Manager : null) != null && E.BodyPart.Manager == this.ManagerID)
				{
					E.Chance -= 7;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AAA4 RID: 43684 RVA: 0x003E696C File Offset: 0x003E4B6C
		public override bool HandleEvent(GetTinkeringBonusEvent E)
		{
			if (E.ForSifrah && this.IsObjectActivePartSubject(E.Actor) && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				E.Bonus++;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AAA5 RID: 43685 RVA: 0x003E69CC File Offset: 0x003E4BCC
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (Options.AnySifrah)
			{
				E.Postfix.AppendRules("Adds a bonus turn in tinkering Sifrah games.");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AAA6 RID: 43686 RVA: 0x003E69ED File Offset: 0x003E4BED
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600AAA7 RID: 43687 RVA: 0x003E69F0 File Offset: 0x003E4BF0
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (!base.OnWorldMap)
			{
				base.ConsumeChargeIfOperational(false, false, false, false, false, false, false, false, false, Amount, null, false, 0, false, null);
			}
		}

		// Token: 0x0600AAA8 RID: 43688 RVA: 0x003E6A2B File Offset: 0x003E4C2B
		public override bool AllowStaticRegistration()
		{
			return false;
		}

		// Token: 0x0600AAA9 RID: 43689 RVA: 0x003E6A30 File Offset: 0x003E4C30
		public void AddArm(GameObject Subject = null)
		{
			if (Subject == null)
			{
				Subject = this.ParentObject.Equipped;
			}
			Body body = ((Subject != null) ? Subject.Body : null);
			if (body == null)
			{
				return;
			}
			BodyPart body2 = body.GetBody();
			string text = "Servo-Arm";
			int num = 0;
			string text2 = null;
			string text3 = null;
			string text4 = null;
			string text5 = null;
			string managerID = this.ManagerID;
			bool? flag = new bool?(true);
			string[] array = new string[] { "Hands", "Feet", "Roots", "Thrown Weapon" };
			BodyPart bodyPart = body2.AddPartAt(text, num, text2, text3, text4, text5, managerID, null, null, null, null, null, null, null, flag, null, null, null, null, null, "Arm", array, true);
			string text6 = "Servo-Claw";
			int num2 = 0;
			string text7 = null;
			string text8 = null;
			string text9 = null;
			string text10 = null;
			flag = new bool?(true);
			bodyPart.AddPart(text6, num2, text7, text8, text9, text10, this.ManagerID, null, null, null, null, null, null, null, flag, null, null, null, null, null, true);
			Subject.WantToReequip();
		}

		// Token: 0x0600AAAA RID: 43690 RVA: 0x003E6BC0 File Offset: 0x003E4DC0
		public void RemoveArm(GameObject Subject = null)
		{
			if (Subject == null)
			{
				Subject = this.ParentObject.Equipped;
			}
			if (Subject != null)
			{
				Subject.RemoveBodyPartsByManager(this.ManagerID, true);
			}
			if (Subject != null)
			{
				Subject.WantToReequip();
			}
		}
	}
}

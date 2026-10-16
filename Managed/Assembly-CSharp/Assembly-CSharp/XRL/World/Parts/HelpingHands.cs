using System;
using XRL.World.Anatomy;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x0200104A RID: 4170
	[Serializable]
	public class HelpingHands : IPart
	{
		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x0600A5FE RID: 42494 RVA: 0x003CC24A File Offset: 0x003CA44A
		public string ManagerID
		{
			get
			{
				return this.ParentObject.ID + "::HelpingHands";
			}
		}

		// Token: 0x0600A5FF RID: 42495 RVA: 0x003CC261 File Offset: 0x003CA461
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600A600 RID: 42496 RVA: 0x003CC264 File Offset: 0x003CA464
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<BeforeDismemberEvent>.ID || ID == ExamineCriticalFailureEvent.ID || ID == EquippedEvent.ID || ID == UnequippedEvent.ID || ID == PooledEvent<GetMeleeAttackChanceEvent>.ID;
		}

		// Token: 0x0600A601 RID: 42497 RVA: 0x003CC29C File Offset: 0x003CA49C
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

		// Token: 0x0600A602 RID: 42498 RVA: 0x003CC2F8 File Offset: 0x003CA4F8
		public override bool HandleEvent(EquippedEvent E)
		{
			if (this.ParentObject.IsWorn(null))
			{
				this.AddArms(E.Actor);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A603 RID: 42499 RVA: 0x003CC31B File Offset: 0x003CA51B
		public override bool HandleEvent(UnequippedEvent E)
		{
			this.RemoveArms(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A604 RID: 42500 RVA: 0x003CC330 File Offset: 0x003CA530
		public override bool HandleEvent(ExamineCriticalFailureEvent E)
		{
			return !this.ExamineFailure(E, 50) && base.HandleEvent(E);
		}

		// Token: 0x0600A605 RID: 42501 RVA: 0x003CC348 File Offset: 0x003CA548
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

		// Token: 0x0600A606 RID: 42502 RVA: 0x003CC3A6 File Offset: 0x003CA5A6
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600A607 RID: 42503 RVA: 0x003CC3AC File Offset: 0x003CA5AC
		public void AddArms(GameObject Subject = null)
		{
			if (Subject == null)
			{
				Subject = this.ParentObject.Equipped;
			}
			if (Subject == null)
			{
				return;
			}
			Body body = Subject.Body;
			if (body == null)
			{
				return;
			}
			BodyPart body2 = body.GetBody();
			BodyPart bodyPart = body2;
			string text = "Robo-Arm";
			int num = 2;
			string text2 = null;
			string text3 = null;
			string text4 = null;
			string text5 = null;
			bool? flag = new bool?(true);
			string managerID = this.ManagerID;
			string[] array = new string[] { "Hands", "Feet", "Roots", "Thrown Weapon" };
			BodyPart bodyPart2 = bodyPart.AddPartAt(text, num, text2, text3, text4, text5, managerID, null, null, null, null, null, null, null, flag, null, null, null, null, null, "Arm", array, true);
			BodyPart bodyPart3 = bodyPart2;
			string text6 = "Robo-Hand";
			int num2 = 2;
			string text7 = null;
			string text8 = "Robo-Hands";
			string text9 = null;
			string text10 = null;
			flag = new bool?(true);
			bodyPart3.AddPart(text6, num2, text7, text8, text9, text10, this.ManagerID, null, null, null, null, null, null, null, flag, null, null, null, null, null, true);
			BodyPart bodyPart4 = body2;
			BodyPart bodyPart5 = bodyPart2;
			string text11 = "Robo-Arm";
			int num3 = 1;
			string text12 = null;
			string text13 = null;
			string text14 = null;
			string text15 = null;
			flag = new bool?(true);
			BodyPart bodyPart6 = bodyPart4.AddPartAt(bodyPart5, text11, num3, text12, text13, text14, text15, this.ManagerID, null, null, null, null, null, null, null, flag, null, null, null, null, null, true);
			string text16 = "Robo-Hand";
			int num4 = 1;
			string text17 = null;
			string text18 = "Robo-Hands";
			string text19 = null;
			string text20 = null;
			flag = new bool?(true);
			bodyPart6.AddPart(text16, num4, text17, text18, text19, text20, this.ManagerID, null, null, null, null, null, null, null, flag, null, null, null, null, null, true);
			BodyPart bodyPart7 = body2;
			string text21 = "Robo-Hands";
			int num5 = 0;
			string text22 = null;
			string text23 = null;
			string text24 = "Robo-Hands";
			string text25 = null;
			flag = new bool?(true);
			string managerID2 = this.ManagerID;
			array = new string[] { "Feet", "Roots", "Thrown Weapon" };
			bodyPart7.AddPartAt(text21, num5, text22, text23, text24, text25, managerID2, null, null, null, null, null, null, null, flag, null, null, null, null, null, "Hands", array, true);
		}

		// Token: 0x0600A608 RID: 42504 RVA: 0x003CC73D File Offset: 0x003CA93D
		public void RemoveArms(GameObject Subject = null)
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

		// Token: 0x0600A609 RID: 42505 RVA: 0x003CC76C File Offset: 0x003CA96C
		private bool ExamineFailure(IExamineEvent E, int Chance)
		{
			if (E.Pass == 1 && GlobalConfig.GetBoolSetting("ContextualExamineFailures", false) && Chance.in100() && !base.IsBroken() && !base.IsRusted())
			{
				base.EmitMessage(string.Concat(new string[]
				{
					"Two tubular sections of ",
					this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
					" flail around and batter ",
					E.Actor.IsPlayer() ? "you" : E.Actor.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
					"!"
				}), ' ', false, E.Actor.IsPlayer(), false, null, null);
				E.Actor.TakeDamage("3d4".RollCached(), "from %t pummeling!", "Melee Unarmed", null, null, E.Actor, null, this.ParentObject, null, null, false, false, true, false, false, false, false, false, E.Actor.IsPlayer(), 5, null);
				E.Identify = true;
				return true;
			}
			return false;
		}
	}
}

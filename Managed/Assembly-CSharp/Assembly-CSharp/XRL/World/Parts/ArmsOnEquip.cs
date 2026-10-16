using System;
using System.Collections.Generic;
using XRL.Language;
using XRL.World.Anatomy;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000FFA RID: 4090
	[Serializable]
	public class ArmsOnEquip : IPart
	{
		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x0600A2FA RID: 41722 RVA: 0x003B8F4C File Offset: 0x003B714C
		public virtual string ManagerID
		{
			get
			{
				string text;
				if ((text = this._ManagerID) == null)
				{
					text = (this._ManagerID = this.ParentObject.ID + "::" + base.GetType().Name);
				}
				return text;
			}
		}

		// Token: 0x0600A2FB RID: 41723 RVA: 0x003B8F8C File Offset: 0x003B718C
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600A2FC RID: 41724 RVA: 0x003B8F90 File Offset: 0x003B7190
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || (ID == PooledEvent<BeforeDismemberEvent>.ID && this.BreakOnDismember) || ID == EquippedEvent.ID || ID == UnequippedEvent.ID || (ID == PooledEvent<GetMeleeAttackChanceEvent>.ID && this.AttackChanceAdjust != 0) || (ID == GetShortDescriptionEvent.ID && this.Describe);
		}

		// Token: 0x0600A2FD RID: 41725 RVA: 0x003B8FEC File Offset: 0x003B71EC
		public override bool HandleEvent(BeforeDismemberEvent E)
		{
			if (this.BreakOnDismember && E.IsObject)
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

		// Token: 0x0600A2FE RID: 41726 RVA: 0x003B9050 File Offset: 0x003B7250
		public override bool HandleEvent(EquippedEvent E)
		{
			if (this.ParentObject.IsWorn(null))
			{
				this.AddArms(E.Actor);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A2FF RID: 41727 RVA: 0x003B9073 File Offset: 0x003B7273
		public override bool HandleEvent(UnequippedEvent E)
		{
			this.RemoveArms(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A300 RID: 41728 RVA: 0x003B9088 File Offset: 0x003B7288
		public override bool HandleEvent(GetMeleeAttackChanceEvent E)
		{
			if (this.AttackChanceAdjust != 0 && E.Intrinsic && !E.Primary)
			{
				BodyPart bodyPart = E.BodyPart;
				if (((bodyPart != null) ? bodyPart.Manager : null) != null && E.BodyPart.Manager == this.ManagerID)
				{
					E.Chance += this.AttackChanceAdjust;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A301 RID: 41729 RVA: 0x003B90F4 File Offset: 0x003B72F4
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.Describe)
			{
				BodyPartType bodyPartType;
				BodyPartType bodyPartType2;
				BodyPartType bodyPartType3;
				this.ResolveTypes(out bodyPartType, out bodyPartType2, out bodyPartType3);
				E.Postfix.Append("\n{{rules|").Append("Grants ").Append(this.Pairs * 2)
					.Append(' ')
					.Append(Grammar.Pluralize(bodyPartType.Name));
				GameObjectBlueprint gameObjectBlueprint;
				if (!this.DefaultHandBehavior.IsNullOrEmpty() && GameObjectFactory.Factory.Blueprints.TryGetValue(this.DefaultHandBehavior, out gameObjectBlueprint))
				{
					E.Postfix.Append(" with ").Append(Grammar.Pluralize(gameObjectBlueprint.CachedDisplayNameStripped));
				}
				E.Postfix.Append("}}");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A302 RID: 41730 RVA: 0x003B91B6 File Offset: 0x003B73B6
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600A303 RID: 41731 RVA: 0x003B91BC File Offset: 0x003B73BC
		public void ResolveTypes(out BodyPartType ArmType, out BodyPartType HandType, out BodyPartType HandsType)
		{
			Dictionary<string, BodyPartType> bodyPartTypeTable = Anatomies.BodyPartTypeTable;
			HandsType = bodyPartTypeTable[this.BaseHands.Coalesce("Hands")];
			HandType = bodyPartTypeTable[this.BaseHand.Coalesce(HandsType.ImpliedBy, "Hand")];
			ArmType = bodyPartTypeTable[this.BaseArm.Coalesce(HandType.UsuallyOnVariant, "Arm")];
		}

		// Token: 0x0600A304 RID: 41732 RVA: 0x003B9228 File Offset: 0x003B7428
		public void AddArms(GameObject Subject = null)
		{
			if (Subject == null)
			{
				Subject = this.ParentObject.Equipped;
			}
			Body body = ((Subject != null) ? Subject.Body : null);
			BodyPart bodyPart = ((body != null) ? body.GetBody() : null);
			if (bodyPart == null)
			{
				return;
			}
			BodyPartType bodyPartType;
			BodyPartType bodyPartType2;
			BodyPartType bodyPartType3;
			this.ResolveTypes(out bodyPartType, out bodyPartType2, out bodyPartType3);
			int num = ((this.Pairs == 1) ? 0 : 4);
			int? num2 = (this.Category.IsNullOrEmpty() ? null : new int?(BodyPartCategory.GetCode(this.Category, true)));
			for (int i = 0; i < this.Pairs; i++)
			{
				string text = ((i == 0) ? (this.ManagerID + " Hands") : string.Format("{0} Hands {1}", this.ManagerID, i + 1));
				BodyPart bodyPart2 = bodyPart;
				BodyPartType bodyPartType4 = bodyPartType;
				int num3 = 2 | num;
				string text2 = null;
				string text3 = null;
				string text4 = null;
				string text5 = null;
				bool? flag = new bool?(this.Extrinsic);
				string managerID = this.ManagerID;
				string[] array = new string[] { "Hands", "Feet", "Roots", "Thrown Weapon" };
				BodyPart bodyPart3 = bodyPart2.AddPartAt(bodyPartType4, num3, text2, text3, text4, text5, managerID, num2, null, null, null, null, null, null, flag, null, null, null, null, null, "Arm", array, true);
				BodyPart bodyPart4 = bodyPart3;
				BodyPartType bodyPartType5 = bodyPartType2;
				int num4 = 2 | num;
				flag = new bool?(this.Extrinsic);
				string text6 = this.ManagerID;
				string text7 = text;
				bodyPart4.AddPart(bodyPartType5, num4, this.DefaultHandBehavior, text7, null, null, text6, num2, null, null, null, null, null, null, flag, null, null, null, null, null, true);
				BodyPart bodyPart5 = bodyPart;
				BodyPartType bodyPartType6 = bodyPartType;
				BodyPart bodyPart6 = bodyPart3;
				BodyPartType bodyPartType7 = bodyPartType6;
				int num5 = 1 | num;
				string text8 = null;
				string text9 = null;
				string text10 = null;
				string text11 = null;
				flag = new bool?(this.Extrinsic);
				BodyPart bodyPart7 = bodyPart5.AddPartAt(bodyPart6, bodyPartType7, num5, text8, text9, text10, text11, this.ManagerID, num2, null, null, null, null, null, null, flag, null, null, null, null, null, true);
				BodyPartType bodyPartType8 = bodyPartType2;
				int num6 = 1 | num;
				flag = new bool?(this.Extrinsic);
				text7 = this.ManagerID;
				text6 = text;
				bodyPart7.AddPart(bodyPartType8, num6, this.DefaultHandBehavior, text6, null, null, text7, num2, null, null, null, null, null, null, flag, null, null, null, null, null, true);
				BodyPart bodyPart8 = bodyPart;
				BodyPartType bodyPartType9 = bodyPartType3;
				int num7 = 0;
				string text12 = null;
				string text13 = null;
				string text14 = text;
				string text15 = null;
				flag = new bool?(this.Extrinsic);
				string managerID2 = this.ManagerID;
				array = new string[] { "Feet", "Roots", "Thrown Weapon" };
				bodyPart8.AddPartAt(bodyPartType9, num7, text12, text13, text14, text15, managerID2, num2, null, null, null, null, null, null, flag, null, null, null, null, null, "Hands", array, true);
				if (num != 0)
				{
					num <<= 1;
				}
			}
			Subject.WantToReequip();
		}

		// Token: 0x0600A305 RID: 41733 RVA: 0x003B9669 File Offset: 0x003B7869
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

		// Token: 0x0400437D RID: 17277
		public string BaseArm;

		// Token: 0x0400437E RID: 17278
		public string BaseHand;

		// Token: 0x0400437F RID: 17279
		public string BaseHands;

		// Token: 0x04004380 RID: 17280
		public string DefaultHandBehavior;

		// Token: 0x04004381 RID: 17281
		public string Category;

		// Token: 0x04004382 RID: 17282
		public int Pairs = 1;

		// Token: 0x04004383 RID: 17283
		public int AttackChanceAdjust;

		// Token: 0x04004384 RID: 17284
		public bool BreakOnDismember;

		// Token: 0x04004385 RID: 17285
		public bool Extrinsic = true;

		// Token: 0x04004386 RID: 17286
		public bool Describe = true;

		// Token: 0x04004387 RID: 17287
		[NonSerialized]
		protected string _ManagerID;
	}
}

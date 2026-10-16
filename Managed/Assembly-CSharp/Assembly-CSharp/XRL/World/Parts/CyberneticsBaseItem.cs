using System;
using XRL.UI;
using XRL.World.Anatomy;
using XRL.World.Effects;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	// Token: 0x02000F19 RID: 3865
	[Serializable]
	public class CyberneticsBaseItem : IPart, IContextRelationManager
	{
		// Token: 0x06009A00 RID: 39424 RVA: 0x0037E870 File Offset: 0x0037CA70
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009A01 RID: 39425 RVA: 0x0037E874 File Offset: 0x0037CA74
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<BeginBeingUnequippedEvent>.ID || ID == PooledEvent<CanBeUnequippedEvent>.ID || ID == EnteredCellEvent.ID || ID == EquippedEvent.ID || ID == PooledEvent<GetContextEvent>.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID || ID == ImplantedEvent.ID || ID == RemoveFromContextEvent.ID || ID == PooledEvent<ReplaceInContextEvent>.ID || ID == OnDestroyObjectEvent.ID || ID == TakenEvent.ID || ID == TryRemoveFromContextEvent.ID || ID == UnimplantedEvent.ID;
		}

		// Token: 0x06009A02 RID: 39426 RVA: 0x0037E8FD File Offset: 0x0037CAFD
		public override bool HandleEvent(CanBeUnequippedEvent E)
		{
			return (E.Forced || this.ImplantedOn == null) && base.HandleEvent(E);
		}

		// Token: 0x06009A03 RID: 39427 RVA: 0x0037E918 File Offset: 0x0037CB18
		public override bool HandleEvent(BeginBeingUnequippedEvent E)
		{
			if (!E.Forced && this.ImplantedOn != null)
			{
				E.AddFailureMessage("You can't remove " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ".");
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009A04 RID: 39428 RVA: 0x0037E980 File Offset: 0x0037CB80
		public override bool HandleEvent(ImplantedEvent E)
		{
			this.ImplantedOn = E.Implantee;
			this.ParentObject.SetIntProperty("CannotEquip", 1, false);
			this.ParentObject.SetIntProperty("CannotDrop", 1, false);
			this.ParentObject.SetIntProperty("NoRemoveOptionInInventory", 1, false);
			int cyberneticRejectionSyndromeChance = this.GetCyberneticRejectionSyndromeChance(E.Implantee);
			if (cyberneticRejectionSyndromeChance > 0)
			{
				string cyberneticRejectionSyndromeKey = CyberneticsBaseItem.GetCyberneticRejectionSyndromeKey(E.Implantee);
				if (!this.ParentObject.HasIntProperty(cyberneticRejectionSyndromeKey))
				{
					this.ParentObject.SetIntProperty(cyberneticRejectionSyndromeKey, cyberneticRejectionSyndromeChance.in100() ? 1 : 0, false);
				}
				if (this.ParentObject.GetIntProperty(cyberneticRejectionSyndromeKey, 0) > 0)
				{
					E.Implantee.ForceApplyEffect(new CyberneticRejectionSyndrome(this.Cost), null);
				}
			}
			if (Sidebar.CurrentTarget == this.ParentObject)
			{
				Sidebar.CurrentTarget = null;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009A05 RID: 39429 RVA: 0x0037EA5C File Offset: 0x0037CC5C
		public override bool HandleEvent(UnimplantedEvent E)
		{
			this.ClearImplantConfiguration();
			if (E.Implantee.IsMutant())
			{
				string cyberneticRejectionSyndromeKey = CyberneticsBaseItem.GetCyberneticRejectionSyndromeKey(E.Implantee);
				CyberneticRejectionSyndrome cyberneticRejectionSyndrome;
				if (this.ParentObject.GetIntProperty(cyberneticRejectionSyndromeKey, 0) > 0 && E.Implantee.TryGetEffect<CyberneticRejectionSyndrome>(out cyberneticRejectionSyndrome))
				{
					cyberneticRejectionSyndrome.Reduce(this.Cost);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009A06 RID: 39430 RVA: 0x0037EABC File Offset: 0x0037CCBC
		public override bool HandleEvent(GetContextEvent E)
		{
			if (GameObject.Validate(ref this.ImplantedOn))
			{
				E.ObjectContext = this.ImplantedOn;
				E.BodyPartContext = this.ImplantedOn.FindCybernetics(this.ParentObject);
				E.Relation = 4;
				E.RelationManager = this;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009A07 RID: 39431 RVA: 0x0037EB10 File Offset: 0x0037CD10
		public override bool HandleEvent(ReplaceInContextEvent E)
		{
			if (GameObject.Validate(ref this.ImplantedOn))
			{
				BodyPart bodyPart = this.ImplantedOn.FindCybernetics(this.ParentObject);
				if (bodyPart != null)
				{
					this.ParentObject.Unimplant(false, null);
					bodyPart.Implant(E.Replacement, false, false);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009A08 RID: 39432 RVA: 0x0037EB62 File Offset: 0x0037CD62
		public override bool HandleEvent(RemoveFromContextEvent E)
		{
			this.ParentObject.Unimplant(false, null);
			return base.HandleEvent(E);
		}

		// Token: 0x06009A09 RID: 39433 RVA: 0x0037EB79 File Offset: 0x0037CD79
		public override bool HandleEvent(TryRemoveFromContextEvent E)
		{
			return !GameObject.Validate(ref this.ImplantedOn) && base.HandleEvent(E);
		}

		// Token: 0x06009A0A RID: 39434 RVA: 0x0037EB91 File Offset: 0x0037CD91
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (!E.IncludeImplantPrefix && !E.UsingAdjunctNoun && !this.ParentObject.HasProperName && E.Understood())
			{
				E.AddAdjective("[{{W|Implant}}] -", 40);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009A0B RID: 39435 RVA: 0x0037EBCC File Offset: 0x0037CDCC
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			string @for = GetCyberneticsBehaviorDescriptionEvent.GetFor(this.ParentObject, this.BehaviorDescription);
			if (!@for.IsNullOrEmpty())
			{
				E.Postfix.AppendRules(@for).Append('\n');
			}
			if (this.ParentObject != null && this.ParentObject.HasTag("CyberneticsDestroyOnRemoval"))
			{
				E.Postfix.AppendRules("Destroyed when uninstalled.");
			}
			if (!this.Slots.IsNullOrEmpty())
			{
				E.Postfix.AppendRules("Target body parts: " + this.Slots.Replace(",", ", "));
			}
			E.Postfix.AppendRules("License points: " + this.Cost.ToString()).AppendRules("Only compatible with True Kin genotypes");
			return base.HandleEvent(E);
		}

		// Token: 0x06009A0C RID: 39436 RVA: 0x0037EC9C File Offset: 0x0037CE9C
		public override bool HandleEvent(EquippedEvent E)
		{
			this.ClearImplantConfiguration();
			return base.HandleEvent(E);
		}

		// Token: 0x06009A0D RID: 39437 RVA: 0x0037ECAB File Offset: 0x0037CEAB
		public override bool HandleEvent(TakenEvent E)
		{
			this.ClearImplantConfiguration();
			return base.HandleEvent(E);
		}

		// Token: 0x06009A0E RID: 39438 RVA: 0x0037ECBA File Offset: 0x0037CEBA
		public override bool HandleEvent(EnteredCellEvent E)
		{
			this.ClearImplantConfiguration();
			return base.HandleEvent(E);
		}

		// Token: 0x06009A0F RID: 39439 RVA: 0x0037ECC9 File Offset: 0x0037CEC9
		public override bool HandleEvent(OnDestroyObjectEvent E)
		{
			this.ParentObject.Unimplant(false, null);
			return base.HandleEvent(E);
		}

		// Token: 0x06009A10 RID: 39440 RVA: 0x0037ECE0 File Offset: 0x0037CEE0
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009A11 RID: 39441 RVA: 0x0037ECE3 File Offset: 0x0037CEE3
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CanBeTaken");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009A12 RID: 39442 RVA: 0x0037ECF8 File Offset: 0x0037CEF8
		private void ClearImplantConfiguration()
		{
			this.ImplantedOn = null;
			this.ParentObject.RemoveIntProperty("CannotEquip");
			this.ParentObject.RemoveIntProperty("CannotDrop");
			this.ParentObject.RemoveIntProperty("NoRemoveOptionInInventory");
		}

		// Token: 0x06009A13 RID: 39443 RVA: 0x0037ED31 File Offset: 0x0037CF31
		public override bool FireEvent(Event E)
		{
			return (!(E.ID == "CanBeTaken") || E.HasFlag("Forced") || this.ImplantedOn == null) && base.FireEvent(E);
		}

		// Token: 0x06009A14 RID: 39444 RVA: 0x0037ED63 File Offset: 0x0037CF63
		public static string GetCyberneticRejectionSyndromeKey(GameObject Implantee)
		{
			return "CyberneticRejection" + Implantee.GeneID;
		}

		// Token: 0x06009A15 RID: 39445 RVA: 0x0037ED78 File Offset: 0x0037CF78
		public int GetCyberneticRejectionSyndromeChance(GameObject Implantee)
		{
			if (!Implantee.IsMutant())
			{
				return 0;
			}
			int num = 5 + this.Cost + this.ParentObject.GetIntProperty("CyberneticRejectionSyndromeModifier", 0);
			Mutations mutations;
			if (Implantee.TryGetPart<Mutations>(out mutations) && mutations.MutationList != null)
			{
				foreach (BaseMutation baseMutation in mutations.MutationList)
				{
					if (baseMutation.IsPhysical() || this.Slots == "Head")
					{
						num += baseMutation.Level;
					}
					else
					{
						num++;
					}
				}
			}
			return 0;
		}

		// Token: 0x06009A16 RID: 39446 RVA: 0x0037EE28 File Offset: 0x0037D028
		public bool RestoreContextRelation(GameObject Object, GameObject ObjectContext, Cell CellContext, BodyPart BodyPartContext, int Relation, bool Silent = true)
		{
			if (Relation != 4 || BodyPartContext == null)
			{
				return false;
			}
			if (BodyPartContext.Cybernetics == Object && Object.Implantee == ObjectContext)
			{
				return true;
			}
			BodyPartContext.Implant(Object, false, Silent);
			return true;
		}

		// Token: 0x0400402C RID: 16428
		public GameObject ImplantedOn;

		// Token: 0x0400402D RID: 16429
		public string Slots;

		// Token: 0x0400402E RID: 16430
		public int Cost;

		// Token: 0x0400402F RID: 16431
		public string BehaviorDescription;
	}
}

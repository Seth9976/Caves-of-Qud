using System;
using XRL.UI;
using XRL.Wish;
using XRL.World.Parts.Skill;
using XRL.World.Tinkering;

namespace XRL.World.Parts
{
	// Token: 0x02001125 RID: 4389
	[HasWishCommand]
	[Serializable]
	public abstract class IModification : IActivePart
	{
		// Token: 0x0600ADA0 RID: 44448 RVA: 0x0040469A File Offset: 0x0040289A
		public IModification()
		{
			this.Configure();
		}

		// Token: 0x0600ADA1 RID: 44449 RVA: 0x004046A8 File Offset: 0x004028A8
		public IModification(int Tier)
			: this()
		{
			this.ApplyTier(Tier);
		}

		// Token: 0x0600ADA2 RID: 44450 RVA: 0x004046B7 File Offset: 0x004028B7
		public void ApplyTier(int Tier)
		{
			this.Tier = Tier;
			this.TierConfigure();
		}

		// Token: 0x0600ADA3 RID: 44451 RVA: 0x004046C6 File Offset: 0x004028C6
		public virtual void Configure()
		{
		}

		// Token: 0x0600ADA4 RID: 44452 RVA: 0x004046C8 File Offset: 0x004028C8
		public virtual void TierConfigure()
		{
		}

		// Token: 0x0600ADA5 RID: 44453 RVA: 0x004046CA File Offset: 0x004028CA
		public virtual int GetModificationSlotUsage()
		{
			return 1;
		}

		// Token: 0x0600ADA6 RID: 44454 RVA: 0x004046CD File Offset: 0x004028CD
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetInventoryActionsAlwaysEvent.ID;
		}

		// Token: 0x0600ADA7 RID: 44455 RVA: 0x004046E4 File Offset: 0x004028E4
		public override bool HandleEvent(GetInventoryActionsAlwaysEvent E)
		{
			GameObject actor = E.Actor;
			if (actor != null && actor.IsPlayer() && this.ParentObject != null && !this.ParentObject.HasPart<TinkerItem>() && E.Actor.HasPart<Tinkering_ReverseEngineer>())
			{
				TinkerItem tinkerItem = this.ParentObject.RequirePart<TinkerItem>(false);
				if (tinkerItem != null)
				{
					tinkerItem.Bits = "";
					tinkerItem.CanDisassemble = true;
					tinkerItem.CanBuild = false;
					tinkerItem.HandleEvent(E);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600ADA8 RID: 44456 RVA: 0x0040475F File Offset: 0x0040295F
		public override bool SameAs(IPart p)
		{
			return (p as IModification).Tier == this.Tier && base.SameAs(p);
		}

		// Token: 0x0600ADA9 RID: 44457 RVA: 0x0040477D File Offset: 0x0040297D
		public virtual string GetModificationDisplayName()
		{
			return null;
		}

		// Token: 0x0600ADAA RID: 44458 RVA: 0x00404780 File Offset: 0x00402980
		public virtual bool ModificationApplicable(GameObject obj)
		{
			return true;
		}

		// Token: 0x0600ADAB RID: 44459 RVA: 0x00404783 File Offset: 0x00402983
		public virtual bool BeingAppliedBy(GameObject obj, GameObject who)
		{
			return true;
		}

		// Token: 0x0600ADAC RID: 44460 RVA: 0x00404786 File Offset: 0x00402986
		public virtual void ApplyModification(GameObject obj)
		{
		}

		// Token: 0x0600ADAD RID: 44461 RVA: 0x00404788 File Offset: 0x00402988
		public virtual void ApplyModification()
		{
			this.ApplyModification(this.ParentObject);
		}

		// Token: 0x0600ADAE RID: 44462 RVA: 0x00404798 File Offset: 0x00402998
		protected void IncreaseDifficulty(int Amount, GameObject obj = null)
		{
			if (obj == null)
			{
				obj = this.ParentObject;
			}
			Examiner examiner = (obj.IsCreature ? obj.GetPart<Examiner>() : obj.RequirePart<Examiner>(false));
			if (examiner != null)
			{
				examiner.Difficulty += Amount;
			}
		}

		// Token: 0x0600ADAF RID: 44463 RVA: 0x004047DC File Offset: 0x004029DC
		protected void IncreaseDifficultyIfDifficult(int Amount, GameObject obj = null)
		{
			if (obj == null)
			{
				obj = this.ParentObject;
			}
			Examiner part = obj.GetPart<Examiner>();
			if (part != null && part.Difficulty > 0)
			{
				part.Difficulty += Amount;
			}
		}

		// Token: 0x0600ADB0 RID: 44464 RVA: 0x00404818 File Offset: 0x00402A18
		protected void IncreaseDifficultyIfComplex(int Amount, GameObject obj = null)
		{
			if (obj == null)
			{
				obj = this.ParentObject;
			}
			Examiner part = obj.GetPart<Examiner>();
			if (part != null && part.Complexity > 0)
			{
				part.Difficulty += Amount;
			}
		}

		// Token: 0x0600ADB1 RID: 44465 RVA: 0x00404854 File Offset: 0x00402A54
		protected void IncreaseComplexity(int Amount, GameObject obj = null)
		{
			if (obj == null)
			{
				obj = this.ParentObject;
			}
			Examiner examiner = (obj.IsCreature ? obj.GetPart<Examiner>() : obj.RequirePart<Examiner>(false));
			if (examiner != null)
			{
				examiner.Complexity += Amount;
				obj.RequirePart<IsTechScannableUnlessAlive>(false);
			}
		}

		// Token: 0x0600ADB2 RID: 44466 RVA: 0x004048A0 File Offset: 0x00402AA0
		protected void IncreaseComplexityIfComplex(int Amount, GameObject obj = null)
		{
			if (obj == null)
			{
				obj = this.ParentObject;
			}
			Examiner part = obj.GetPart<Examiner>();
			if (part != null && part.Complexity > 0)
			{
				part.Complexity += Amount;
				obj.RequirePart<IsTechScannableUnlessAlive>(false);
			}
		}

		// Token: 0x0600ADB3 RID: 44467 RVA: 0x004048E4 File Offset: 0x00402AE4
		protected void IncreaseDifficultyAndComplexity(int Amount1, int Amount2, GameObject obj = null)
		{
			if (obj == null)
			{
				obj = this.ParentObject;
			}
			Examiner examiner = (obj.IsCreature ? obj.GetPart<Examiner>() : obj.RequirePart<Examiner>(false));
			if (examiner != null)
			{
				examiner.Difficulty += Amount1;
				examiner.Complexity += Amount2;
				obj.RequirePart<IsTechScannableUnlessAlive>(false);
			}
		}

		// Token: 0x0600ADB4 RID: 44468 RVA: 0x0040493C File Offset: 0x00402B3C
		protected void IncreaseDifficultyAndComplexityIfComplex(int Amount1, int Amount2, GameObject obj = null)
		{
			if (obj == null)
			{
				obj = this.ParentObject;
			}
			Examiner part = obj.GetPart<Examiner>();
			if (part != null && part.Complexity > 0)
			{
				part.Difficulty += Amount1;
				part.Complexity += Amount2;
				obj.RequirePart<IsTechScannableUnlessAlive>(false);
			}
		}

		// Token: 0x0600ADB5 RID: 44469 RVA: 0x0040498C File Offset: 0x00402B8C
		protected static bool CheckWornSlot(GameObject Object, string Valid1, string Valid2 = null, string Valid3 = null, string Valid4 = null, bool AllowGeneric = true, bool AllowMagnetized = true, bool AllowPartial = true, bool Invert = false)
		{
			Armor part = Object.GetPart<Armor>();
			if (part == null || part.WornOn == null)
			{
				return false;
			}
			if (part.WornOn != Valid1 && part.WornOn != Valid2 && part.WornOn != Valid3 && part.WornOn != Valid4 && (!AllowGeneric || part.WornOn != "*"))
			{
				bool flag = false;
				if (AllowMagnetized && part.WornOn == "Floating Nearby" && Object.HasPart<ModMagnetized>())
				{
					string partParameter = Object.GetBlueprint(true).GetPartParameter<string>("Armor", "WornOn", null);
					if (!partParameter.IsNullOrEmpty() && (partParameter == Valid1 || partParameter == Valid2 || partParameter == Valid3 || partParameter == Valid4 || (AllowGeneric && partParameter == "*")))
					{
						flag = true;
					}
				}
				if (!flag)
				{
					return Invert;
				}
			}
			string usesSlots = Object.UsesSlots;
			if (!usesSlots.IsNullOrEmpty())
			{
				if (AllowPartial)
				{
					if (!usesSlots.CachedCommaExpansion().Contains(Valid1) && !usesSlots.CachedCommaExpansion().Contains(Valid2) && !usesSlots.CachedCommaExpansion().Contains(Valid3) && !usesSlots.CachedCommaExpansion().Contains(Valid4))
					{
						return Invert;
					}
				}
				else if (usesSlots != part.WornOn)
				{
					return Invert;
				}
			}
			return !Invert;
		}

		// Token: 0x0600ADB6 RID: 44470 RVA: 0x00404AEC File Offset: 0x00402CEC
		[WishCommand("modify", null)]
		public static void WishModify(string Argument)
		{
			string modName;
			string objectName;
			Argument.Split(':', out objectName, out modName);
			if (modName.IsNullOrEmpty())
			{
				Popup.ShowFail("No modification specified.", true, true, true);
				return;
			}
			ModEntry modEntry = ModificationFactory.ModList.Find((ModEntry x) => x.Part.EqualsNoCase(modName)) ?? ModificationFactory.ModList.Find((ModEntry x) => x.TinkerDisplayName.EqualsNoCase(modName));
			if (modEntry == null)
			{
				Popup.ShowFail("No modification by the name '" + modName + "' could be found.", true, true, true);
				return;
			}
			GameObjectBlueprint gameObjectBlueprint = GameObjectFactory.Factory.BlueprintList.Find((GameObjectBlueprint x) => x.Name.EqualsNoCase(objectName)) ?? GameObjectFactory.Factory.BlueprintList.Find((GameObjectBlueprint x) => x.CachedDisplayNameStripped.EqualsNoCase(objectName));
			if (gameObjectBlueprint == null)
			{
				Popup.ShowFail("No blueprint by the name '" + objectName + "' could be found.", true, true, true);
				return;
			}
			GameObject gameObject = gameObjectBlueprint.createUnmodified();
			ItemModding.ApplyModification(gameObject, modEntry.Part, gameObject.GetTechTier(), true, null, false);
			if (gameObject.Takeable)
			{
				The.Player.TakeObject(gameObject, false, false, new int?(0), null, null);
				return;
			}
			The.Player.CurrentCell.getClosestEmptyCell().AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
		}

		// Token: 0x040047DB RID: 18395
		public int Tier;
	}
}

using System;
using XRL.World.Anatomy;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013D3 RID: 5075
	[Serializable]
	public class HooksForFeet : BaseDefaultEquipmentMutation
	{
		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x0600C41B RID: 50203 RVA: 0x004776F7 File Offset: 0x004758F7
		public string BlueprintName
		{
			get
			{
				return this.Variant.Coalesce("Hooks");
			}
		}

		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x0600C41C RID: 50204 RVA: 0x00477709 File Offset: 0x00475909
		public GameObjectBlueprint Blueprint
		{
			get
			{
				if (this._Blueprint == null)
				{
					this._Blueprint = GameObjectFactory.Factory.GetBlueprint(this.BlueprintName);
				}
				return this._Blueprint;
			}
		}

		// Token: 0x0600C41E RID: 50206 RVA: 0x00477737 File Offset: 0x00475937
		public override bool CanLevel()
		{
			return false;
		}

		// Token: 0x0600C41F RID: 50207 RVA: 0x0047773A File Offset: 0x0047593A
		public override bool GeneratesEquipment()
		{
			return true;
		}

		// Token: 0x0600C420 RID: 50208 RVA: 0x00477740 File Offset: 0x00475940
		public override string GetDescription()
		{
			string tag = this.Blueprint.GetTag("VariantDescription", "");
			if (tag.IsNullOrEmpty())
			{
				return "You have " + this.Blueprint.DisplayName() + " for feet.\n\nYou cannot wear shoes.";
			}
			return tag;
		}

		// Token: 0x0600C421 RID: 50209 RVA: 0x00477787 File Offset: 0x00475987
		public override string GetLevelText(int Level)
		{
			return "";
		}

		// Token: 0x0600C422 RID: 50210 RVA: 0x0047778E File Offset: 0x0047598E
		public override void SetVariant(string Variant)
		{
			base.SetVariant(Variant);
			this._Blueprint = null;
		}

		// Token: 0x0600C423 RID: 50211 RVA: 0x004777A0 File Offset: 0x004759A0
		public override void OnRegenerateDefaultEquipment(Body Body)
		{
			if (!GameObject.Validate(ref this.HooksObject))
			{
				this.HooksObject = GameObject.Create(this.Blueprint, 0, 0, null, null, null, null, null);
			}
			string text = this.Blueprint.GetPartParameter<string>("Armor", "WornOn", null) ?? this.Blueprint.GetPartParameter<string>("MeleeWeapon", "Slot", "Feet");
			BodyPart bodyPart = base.RequireRegisteredSlot(Body, text);
			if (bodyPart != null && bodyPart.Equipped != this.HooksObject && (bodyPart.Equipped == null || bodyPart.ForceUnequip(true, false, false)) && !this.ParentObject.ForceEquipObject(this.HooksObject, bodyPart, true, new int?(0)))
			{
				MetricsManager.LogError("HooksForFeet force equip on " + (((bodyPart != null) ? bodyPart.Name : null) ?? "NULL") + " failed");
			}
			base.OnRegenerateDefaultEquipment(Body);
		}

		// Token: 0x0600C424 RID: 50212 RVA: 0x0047787F File Offset: 0x00475A7F
		public override bool Unmutate(GameObject GO)
		{
			base.CleanUpMutationEquipment(GO, ref this.HooksObject);
			return base.Unmutate(GO);
		}

		// Token: 0x04004DCD RID: 19917
		public GameObject HooksObject;

		// Token: 0x04004DCE RID: 19918
		[NonSerialized]
		protected GameObjectBlueprint _Blueprint;
	}
}

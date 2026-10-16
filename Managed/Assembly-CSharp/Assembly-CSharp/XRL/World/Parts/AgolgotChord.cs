using System;
using System.Text;
using XRL.Language;
using XRL.World.Anatomy;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	// Token: 0x02000EDD RID: 3805
	public class AgolgotChord : INephalChord
	{
		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x06009842 RID: 38978 RVA: 0x00375099 File Offset: 0x00373299
		public override string Source
		{
			get
			{
				return "Agolgot";
			}
		}

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x06009843 RID: 38979 RVA: 0x003750A0 File Offset: 0x003732A0
		public virtual int ArmPairs
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x06009844 RID: 38980 RVA: 0x003750A3 File Offset: 0x003732A3
		public virtual string DefaultBehavior
		{
			get
			{
				return "Nephal_Claw_Chord";
			}
		}

		// Token: 0x06009845 RID: 38981 RVA: 0x003750AC File Offset: 0x003732AC
		public override void Initialize()
		{
			MultipleArms part;
			if (!this.ParentObject.TryGetPart<MultipleArms>(out part))
			{
				this.MultipleArmsID = this.ParentObject.RequirePart<Mutations>(false).AddMutationMod(typeof(MultipleArms), null, 1, Mutations.MutationModifierTracker.SourceType.External, Grammar.MakePossessive(this.SourceName) + " chord");
				part = this.ParentObject.GetPart<MultipleArms>();
			}
			if (part.Pairs < this.ArmPairs)
			{
				part.AdjustRank(this.ArmPairs - part.Pairs);
			}
			foreach (BodyPart bodyPart in this.ParentObject.GetBodyPartsByManager(part.AdditionsManagerID, true))
			{
				if (bodyPart.Type == "Hand")
				{
					GameObject defaultBehavior = bodyPart.DefaultBehavior;
					if (defaultBehavior != null)
					{
						defaultBehavior.Obliterate(null, false, null);
					}
					bodyPart.DefaultBehavior = null;
					bodyPart.DefaultBehaviorBlueprint = this.DefaultBehavior;
				}
			}
			this.ParentObject.Body.RegenerateDefaultEquipment();
		}

		// Token: 0x06009846 RID: 38982 RVA: 0x003751C4 File Offset: 0x003733C4
		public override void Remove()
		{
			if (this.MultipleArmsID != Guid.Empty)
			{
				this.ParentObject.RequirePart<Mutations>(false).RemoveMutationMod(this.MultipleArmsID);
			}
		}

		// Token: 0x06009847 RID: 38983 RVA: 0x003751F0 File Offset: 0x003733F0
		public override void AppendRules(StringBuilder Postfix)
		{
			if (this.DefaultBehaviorDesc.IsNullOrEmpty())
			{
				string cachedDisplayNameStripped = GameObjectFactory.Factory.GetBlueprint(this.DefaultBehavior).CachedDisplayNameStripped;
				this.DefaultBehaviorDesc = Grammar.Pluralize(cachedDisplayNameStripped);
			}
			Postfix.Append("\n• +").Append(this.ArmPairs * 2).Append(" arms with ")
				.Append(this.DefaultBehaviorDesc);
		}

		// Token: 0x04003F76 RID: 16246
		public Guid MultipleArmsID;

		// Token: 0x04003F77 RID: 16247
		[NonSerialized]
		private string DefaultBehaviorDesc;
	}
}

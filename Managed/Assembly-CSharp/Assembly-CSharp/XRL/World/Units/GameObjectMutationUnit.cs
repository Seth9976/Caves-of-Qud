using System;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;

namespace XRL.World.Units
{
	// Token: 0x020009AE RID: 2478
	[Serializable]
	public class GameObjectMutationUnit : GameObjectUnit
	{
		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x06006AE2 RID: 27362 RVA: 0x00226EA4 File Offset: 0x002250A4
		public MutationEntry Entry
		{
			get
			{
				MutationEntry mutationEntry;
				if ((mutationEntry = this._Entry) == null)
				{
					mutationEntry = (this._Entry = (MutationFactory.TryGetMutationEntry(this.Name, this.Class, out this._Entry) ? this._Entry : null));
				}
				return mutationEntry;
			}
		}

		// Token: 0x06006AE3 RID: 27363 RVA: 0x00226EE8 File Offset: 0x002250E8
		public override void Apply(GameObject Object)
		{
			Mutations mutations = Object.RequirePart<Mutations>(false);
			Mutations mutations2 = mutations;
			MutationEntry entry = this.Entry;
			BaseMutation baseMutation;
			if ((baseMutation = mutations2.GetMutationByName(((entry != null) ? entry.Name : null) ?? this.Name)) == null)
			{
				Mutations mutations3 = mutations;
				MutationEntry entry2 = this.Entry;
				baseMutation = mutations3.GetMutation(((entry2 != null) ? entry2.Class : null) ?? this.Class);
			}
			BaseMutation baseMutation2 = baseMutation;
			if (baseMutation2 != null)
			{
				if (this.Enhance)
				{
					mutations.LevelMutation(baseMutation2, baseMutation2.BaseLevel + this.Level);
				}
				return;
			}
			MutationEntry entry3 = this.Entry;
			string text = ((entry3 != null) ? entry3.Class : null) ?? this.Class;
			if (text != null)
			{
				mutations.AddMutation(text, this.Level);
			}
		}

		// Token: 0x06006AE4 RID: 27364 RVA: 0x00226F94 File Offset: 0x00225194
		public override void Remove(GameObject Object)
		{
			MutationEntry entry = this.Entry;
			BaseMutation baseMutation = Object.GetPart(((entry != null) ? entry.Class : null) ?? this.Class) as BaseMutation;
			if (baseMutation != null)
			{
				Mutations part = Object.GetPart<Mutations>();
				if (part == null)
				{
					return;
				}
				part.RemoveMutation(baseMutation, true);
			}
		}

		// Token: 0x06006AE5 RID: 27365 RVA: 0x00226FDE File Offset: 0x002251DE
		public override void Reset()
		{
			base.Reset();
			this.Name = null;
			this.Class = null;
			this._Entry = null;
			this.Level = 0;
			this.Enhance = true;
		}

		// Token: 0x06006AE6 RID: 27366 RVA: 0x0022700C File Offset: 0x0022520C
		public override string GetDescription(bool Inscription = false)
		{
			if (this.Name == null)
			{
				MutationEntry entry = this.Entry;
				this.Name = ((entry != null) ? entry.GetDisplayName(false) : null);
			}
			if (this.Name.IsNullOrEmpty() && !this.Class.IsNullOrEmpty())
			{
				try
				{
					MutationEntry entry2 = this.Entry;
					BaseMutation baseMutation = ((entry2 != null) ? entry2.Mutation : null) ?? Mutations.GetGenericMutation(this.Class, null, null);
					this.Name = baseMutation.GetDisplayName(true);
					this.ShouldShowLevel = baseMutation.ShouldShowLevel();
				}
				catch
				{
					this.Name = "ERR:" + this.Class;
				}
			}
			if (!this.ShouldShowLevel)
			{
				return this.Name;
			}
			return this.Name + " at level " + this.Level.ToString();
		}

		// Token: 0x040030F0 RID: 12528
		public string Name;

		// Token: 0x040030F1 RID: 12529
		public string Class;

		// Token: 0x040030F2 RID: 12530
		public int Level;

		// Token: 0x040030F3 RID: 12531
		public bool Enhance = true;

		// Token: 0x040030F4 RID: 12532
		public bool ShouldShowLevel = true;

		// Token: 0x040030F5 RID: 12533
		[NonSerialized]
		private MutationEntry _Entry;
	}
}

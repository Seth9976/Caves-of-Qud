using System;
using XRL.Language;

namespace XRL.World.Parts
{
	// Token: 0x02000F52 RID: 3922
	[Serializable]
	public class NoteExpandedSlots : IPart
	{
		// Token: 0x06009C81 RID: 40065 RVA: 0x0038BF8F File Offset: 0x0038A18F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x06009C82 RID: 40066 RVA: 0x0038BFA8 File Offset: 0x0038A1A8
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (!this.Slots.IsNullOrEmpty())
			{
				E.Postfix.AppendRules(string.Concat(new string[]
				{
					this.ParentObject.IndicativeProximal,
					" ",
					this.ParentObject.IsPlural ? Grammar.Pluralize(this.ParentObject.GetDescriptiveCategory()) : this.ParentObject.GetDescriptiveCategory(),
					this.ParentObject.GetVerb("are", true, false, false, true, false),
					", unusually, able to function in ",
					Grammar.A(Grammar.MakeOrList(this.Slots.CachedCommaExpansion(), true), false).ToLower(),
					"."
				}));
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009C83 RID: 40067 RVA: 0x0038C06D File Offset: 0x0038A26D
		public void AddSlot(string Slot)
		{
			this.Slots = this.Slots.AddDelimitedSubstring(',', Slot, StringComparison.Ordinal);
		}

		// Token: 0x040040BD RID: 16573
		public string Slots;
	}
}

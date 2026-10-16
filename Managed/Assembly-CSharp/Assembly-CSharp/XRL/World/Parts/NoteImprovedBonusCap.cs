using System;
using System.Text;
using XRL.Language;

namespace XRL.World.Parts
{
	// Token: 0x020010E9 RID: 4329
	[Serializable]
	public class NoteImprovedBonusCap : IPart
	{
		// Token: 0x0600ABAF RID: 43951 RVA: 0x003EE722 File Offset: 0x003EC922
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600ABB0 RID: 43952 RVA: 0x003EE738 File Offset: 0x003EC938
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			StringBuilder postfix = E.Postfix;
			string[] array = new string[11];
			array[0] = this.ParentObject.IndicativeProximal;
			array[1] = " ";
			array[2] = (this.ParentObject.IsPlural ? Grammar.Pluralize(this.ParentObject.GetDescriptiveCategory()) : this.ParentObject.GetDescriptiveCategory());
			array[3] = this.ParentObject.GetVerb("have", true, false, false, true, false);
			array[4] = " ";
			array[5] = this.Amount.Signed(false);
			array[6] = " to ";
			array[7] = this.ParentObject.its;
			array[8] = " maximum ";
			int num = 9;
			MeleeWeapon part = this.ParentObject.GetPart<MeleeWeapon>();
			array[num] = ((part != null) ? part.Stat : null) ?? "Strength";
			array[10] = " bonus.";
			postfix.AppendRules(string.Concat(array));
			return base.HandleEvent(E);
		}

		// Token: 0x040046EF RID: 18159
		public int Amount;
	}
}

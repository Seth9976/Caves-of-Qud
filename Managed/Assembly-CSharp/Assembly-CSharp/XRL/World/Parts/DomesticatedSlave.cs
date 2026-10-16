using System;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02000EA0 RID: 3744
	[Serializable]
	public class DomesticatedSlave : IBondedCompanion
	{
		// Token: 0x06009676 RID: 38518 RVA: 0x003684E4 File Offset: 0x003666E4
		public DomesticatedSlave()
			: base(null, null, null, null, null, false)
		{
		}

		// Token: 0x06009677 RID: 38519 RVA: 0x003684F2 File Offset: 0x003666F2
		public DomesticatedSlave(GameObject EnslavedBy = null, string Faction = "Templar", string Honorific = "domesticated", string Title = null, string ConversationID = "TemplarDomesticant", bool StripGear = false)
			: base(EnslavedBy, Faction, Honorific, Title, ConversationID, StripGear)
		{
		}

		// Token: 0x06009678 RID: 38520 RVA: 0x00368504 File Offset: 0x00366704
		public override void InitializeBondedCompanion()
		{
			string text = this.ParentObject.GetxTag("TextFragments", "YounglingNoise", null);
			this.ParentObject.RemovePart<Preacher>();
			Preacher preacher = this.ParentObject.AddPart<Preacher>(true, false);
			preacher.Book = "TemplarDomesticant";
			preacher.Filter = "Lallated";
			preacher.FilterExtras = text;
			preacher.Prefix = "=subject.T= =verb:say= {{W|'";
			preacher.ChatWait = Stat.Random(20, 30);
			preacher.Volume = 10;
			preacher.SmartUse = false;
			ConversationScript conversationScript = this.ParentObject.RequirePart<ConversationScript>(false);
			conversationScript.Filter = "Lallated";
			conversationScript.FilterExtras = text;
			GameObject gameObject;
			string text2;
			if (!this.ParentObject.GetBlueprint(true).IsBodyPartOccupied("Face"))
			{
				gameObject = GameObject.Create("Gentling Mask");
				text2 = "Face";
			}
			else
			{
				gameObject = GameObject.Create("Gentling Collar");
				text2 = "Head";
			}
			if (gameObject != null)
			{
				gameObject.RequirePart<SlaveMask>(false);
				this.ParentObject.ForceEquipObject(gameObject, text2, false, null);
			}
		}
	}
}

using System;

namespace XRL.World.Parts
{
	// Token: 0x02000E81 RID: 3713
	[Serializable]
	public class MechanimistLibrarian : IPart
	{
		// Token: 0x060095C2 RID: 38338 RVA: 0x003629FC File Offset: 0x00360BFC
		public override void Initialize()
		{
			try
			{
				this.ParentObject.GiveProperName("Sheba Hagadias", true, null, false, null, null, null, null);
				this.ParentObject.RequirePart<Titles>(false).AddTitle(this.ParentObject.GetCreatureType(false, false), 0);
				this.ParentObject.RequirePart<Titles>(false).AddTitle("librarian of the Stilt", 0);
				this.ParentObject.RequirePart<DisplayNameColor>(false).SetColor("Y");
				this.ParentObject.Render.ColorString = "&W";
				this.ParentObject.Render.TileColor = "&C";
				this.ParentObject.Render.DetailColor = "W";
				if (this.ParentObject.BaseStat("Intelligence", 0) < 27)
				{
					this.ParentObject.GetStat("Intelligence").BaseValue = 27;
				}
				this.ParentObject.RemovePart<AISitting>();
				this.ParentObject.ReceiveObject("Cloth Robe", false, 0, 0, null, null, null, null);
				this.ParentObject.ReceiveObject("Spectacles", false, 0, 0, null, null, null, null);
				this.ParentObject.SetStringProperty("Mayor", "Mechanimists", false);
				this.ParentObject.GetPart<Description>().Short = "In the narthex of the Stilt, cloistered beneath a marble arch and close to =pronouns.possessive= Argent Fathers, =pronouns.subjective= =verb:muse:afterpronoun= over a tattered codex. =pronouns.Subjective==verb:'re:afterpronoun= safe here, but it wasn't always that way. As a youngling, =pronouns.possessive= own kind understood =pronouns.objective= little. Only when =pronouns.subjective= =verb:were:afterpronoun= gifted a copy of the Canticles Chromaic did =pronouns.subjective= learn comfort, or mirth, or reason. =pronouns.Possessive= journey to the Stilt took several years, but now that =pronouns.subjective==verb:'re:afterpronoun= here, Sheba =verb:seek= to consolidate all the learning of the ages tucked away in Qud's innumerable chrome nooks. Here, =pronouns.subjective= =verb:prepare:afterpronoun= a residence where pilgrims can study the wisdom of others and bring themselves nearer to the divinity of the Kasaphescence.";
				if (this.ParentObject.GetGender(false).Name != "female")
				{
					this.ParentObject.SetPronounSet("she/her");
				}
				this.ParentObject.SetIntProperty("Librarian", 1, false);
				this.ParentObject.RemovePart<Chat>();
				this.ParentObject.RemovePart<ConversationScript>();
				this.ParentObject.RemovePart<Miner>();
				this.ParentObject.RemovePart<TurretTinker>();
				this.ParentObject.AddPart<ConversationScript>(new ConversationScript("MechanimistLibrarian", true), true, false);
				this.ParentObject.RequirePart<AISuppressIndependentBehavior>(false);
				this.ParentObject.RequirePart<Interesting>(false);
				TakeOnRoleEvent.Send(this.ParentObject, "Librarian");
				this.ParentObject.RemovePart(this);
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("Librarian", ex, "game_exception");
			}
		}
	}
}

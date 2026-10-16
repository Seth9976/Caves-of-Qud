using System;
using System.Collections.Generic;
using Qud.API;
using XRL.Language;
using XRL.Rules;
using XRL.UI;

namespace XRL.World.Parts
{
	// Token: 0x02001035 RID: 4149
	[Serializable]
	public class MumblesInfection : IPart
	{
		// Token: 0x0600A540 RID: 42304 RVA: 0x003C6B01 File Offset: 0x003C4D01
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			this.Visited = Reader.ReadDictionary<string, bool>();
			base.Read(Basis, Reader);
		}

		// Token: 0x0600A541 RID: 42305 RVA: 0x003C6B17 File Offset: 0x003C4D17
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			Writer.Write<string, bool>(this.Visited);
			base.Write(Basis, Writer);
		}

		// Token: 0x0600A542 RID: 42306 RVA: 0x003C6B2D File Offset: 0x003C4D2D
		public override bool AllowStaticRegistration()
		{
			return false;
		}

		// Token: 0x0600A543 RID: 42307 RVA: 0x003C6B30 File Offset: 0x003C4D30
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EnteredCell");
			Registrar.Register("Equipped");
			Registrar.Register("Unequipped");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600A544 RID: 42308 RVA: 0x003C6B5C File Offset: 0x003C4D5C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EnteredCell")
			{
				if (this.ParentObject.Physics.Equipped != null && this.ParentObject.Physics.Equipped.IsPlayer())
				{
					string zoneID = IComponent<GameObject>.ThePlayer.Physics.CurrentCell.ParentZone.ZoneID;
					if (!IComponent<GameObject>.ThePlayer.Physics.CurrentCell.ParentZone.IsWorldMap())
					{
						if (this.Visited.ContainsKey(zoneID))
						{
							return true;
						}
						this.Visited.Add(zoneID, true);
						if (Stat.Random(0, 100) <= this.Chance)
						{
							IBaseJournalEntry randomUnrevealedNote = JournalAPI.GetRandomUnrevealedNote(null);
							string text;
							if (randomUnrevealedNote is JournalMapNote)
							{
								text = "The location of " + Grammar.InitLowerIfArticle(randomUnrevealedNote.Text);
							}
							else
							{
								text = randomUnrevealedNote.Text;
							}
							Popup.Show("The mouths on your skin begin to mumble coherently, revealing the wisdom of a trillion microbes:\n\n" + text, null, "Sounds/UI/ui_notification", true, true, true, true, null);
							randomUnrevealedNote.Reveal(this.ParentObject.DisplayName, false);
							Achievement.LEARN_SECRET_FROM_MUMBLEMOUTH.Unlock();
						}
					}
				}
			}
			else if (E.ID == "Equipped")
			{
				E.GetGameObjectParameter("EquippingObject").RegisterPartEvent(this, "EnteredCell");
			}
			else if (E.ID == "Unequipped")
			{
				E.GetGameObjectParameter("UnequippingObject").UnregisterPartEvent(this, "EnteredCell");
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004453 RID: 17491
		public int Chance = 5;

		// Token: 0x04004454 RID: 17492
		[NonSerialized]
		public Dictionary<string, bool> Visited = new Dictionary<string, bool>();
	}
}

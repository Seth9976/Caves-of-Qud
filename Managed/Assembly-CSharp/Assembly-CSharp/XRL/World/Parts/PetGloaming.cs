using System;
using System.Collections.Generic;
using Qud.API;
using XRL.Language;
using XRL.Rules;
using XRL.UI;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x020011A7 RID: 4519
	[Serializable]
	public class PetGloaming : IPart
	{
		// Token: 0x0600B3A2 RID: 45986 RVA: 0x00418746 File Offset: 0x00416946
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("EnteredCell");
			Registrar.Register("ApplyRealityStabilized");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B3A3 RID: 45987 RVA: 0x00418768 File Offset: 0x00416968
		public override bool Render(RenderEvent E)
		{
			if (this.Gloaming > 0 && (this.gloamingFrames > 0 || Stat.RandomCosmetic(1, 30) <= 1))
			{
				if (this.gloamingFrames > 0)
				{
					this.gloamingFrames--;
				}
				else
				{
					this.gloamingFrames = Stat.Random(2, 6);
				}
				if (this.gloamingTarget == null)
				{
					this.gloamingTarget = GameObject.Create(this.gloamingBlueprint);
				}
				if (this.gloamingTarget != null)
				{
					E.RenderString = this.gloamingTarget.Render.RenderString;
					E.Tile = this.gloamingTarget.Render.Tile;
				}
			}
			return true;
		}

		// Token: 0x0600B3A4 RID: 45988 RVA: 0x0041880C File Offset: 0x00416A0C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "ApplyRealityStabilized")
			{
				if (this.ParentObject.CurrentCell != null && this.gloamingBlueprint != null && this.Gloaming > 0)
				{
					this.Gloaming = 0;
					Cell currentCell = this.ParentObject.CurrentCell;
					this.ParentObject.Brain.PartyLeader = null;
					this.ParentObject.Destroy(null, false, false, null);
					GameObject gameObject = currentCell.AddObject(this.gloamingBlueprint, null, null, null, null);
					IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
					{
						this.ParentObject.Poss("astral tether", true),
						" snaps and ",
						this.ParentObject.its,
						" binal specter substantiates as ",
						gameObject.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
						"."
					}), null, true);
					gameObject.SetActive();
					IComponent<GameObject>.ThePlayer.ApplyEffect(new ResummonGloaming(), null);
				}
			}
			else if (E.ID == "EnteredCell" && this.ParentObject.CurrentZone != null && !this.visitedZones.Contains(this.ParentObject.CurrentZone.ZoneID))
			{
				this.visitedZones.Add(this.ParentObject.CurrentZone.ZoneID);
				if (this.SecretChance.in100())
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
					Popup.Show(string.Concat(new string[]
					{
						this.ParentObject.Does("beat", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
						" ",
						this.ParentObject.its,
						" wings, and the shattered voices of a trillion worlds ride the current of air and harmonize into one, revealing the following wisdom:\n\n",
						text
					}), null, "Sounds/UI/ui_notification", true, true, true, true, null);
					randomUnrevealedNote.Reveal(this.ParentObject.DisplayName, false);
				}
				if (this.Gloaming > 0 && this.StopGloamingChance.in100())
				{
					this.Gloaming = 0;
					this.ParentObject.RemoveEffect<Gleaming>();
					IComponent<GameObject>.AddPlayerMessage(this.ParentObject.Does("stop", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " gleaming.", null, true);
				}
				else if (this.GloamingChance.in100())
				{
					this.Gloaming = 1;
					this.gloamingTarget = EncountersAPI.GetACreature(null);
					this.gloamingBlueprint = this.gloamingTarget.Blueprint;
					if (!this.ParentObject.HasEffect<Gleaming>())
					{
						this.ParentObject.ApplyEffect(new Gleaming(), null);
						IComponent<GameObject>.AddPlayerMessage(this.ParentObject.Does("start", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " to gleam with an {{K|unearthly light}}.", null, true);
					}
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004871 RID: 18545
		public List<string> visitedZones = new List<string>();

		// Token: 0x04004872 RID: 18546
		public int Gloaming;

		// Token: 0x04004873 RID: 18547
		[NonSerialized]
		public GameObject gloamingTarget;

		// Token: 0x04004874 RID: 18548
		public string gloamingBlueprint;

		// Token: 0x04004875 RID: 18549
		public int GloamingChance = 10;

		// Token: 0x04004876 RID: 18550
		public int StopGloamingChance = 30;

		// Token: 0x04004877 RID: 18551
		public int SecretChance = 2;

		// Token: 0x04004878 RID: 18552
		public float lastGloam;

		// Token: 0x04004879 RID: 18553
		public int gloamingFrames;
	}
}

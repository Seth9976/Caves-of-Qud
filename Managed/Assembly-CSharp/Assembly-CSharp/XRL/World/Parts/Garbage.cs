using System;
using System.Collections.Generic;
using Qud.API;
using XRL.Rules;
using XRL.UI;
using XRL.World.Capabilities;
using XRL.World.Parts.Skill;

namespace XRL.World.Parts
{
	// Token: 0x02001039 RID: 4153
	[Serializable]
	public class Garbage : IPart
	{
		// Token: 0x0600A55D RID: 42333 RVA: 0x003C7790 File Offset: 0x003C5990
		public override bool SameAs(IPart Part)
		{
			Garbage garbage = Part as Garbage;
			int? depth = garbage.Depth;
			int? depth2 = this.Depth;
			return ((depth.GetValueOrDefault() == depth2.GetValueOrDefault()) & (depth != null == (depth2 != null))) && garbage.Level == this.Level && base.SameAs(Part);
		}

		// Token: 0x0600A55E RID: 42334 RVA: 0x003C77F0 File Offset: 0x003C59F0
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<EndTurnEvent>.ID || (ID == EnteredCellEvent.ID && this.Depth == null) || ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID || ID == ObjectEnteringCellEvent.ID;
		}

		// Token: 0x0600A55F RID: 42335 RVA: 0x003C7840 File Offset: 0x003C5A40
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			if (IComponent<GameObject>.ThePlayer.HasPart<TrashRifling>())
			{
				E.AddAction("Rifle", "rifle", Garbage.RIFLE_INTERACTION, null, 'r', false, 100, 0, false, false, true, false, true, null, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A560 RID: 42336 RVA: 0x003C7884 File Offset: 0x003C5A84
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == Garbage.RIFLE_INTERACTION && this.AttemptRifle(E.Actor, E.Auto, E.FromCell, E.Generated))
			{
				E.RequestInterfaceExit();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A561 RID: 42337 RVA: 0x003C78D0 File Offset: 0x003C5AD0
		public override bool HandleEvent(EnteredCellEvent E)
		{
			int num = this.Depth.GetValueOrDefault();
			if (this.Depth == null)
			{
				num = this.ParentObject.CurrentCell.ParentZone.GetZoneZ();
				this.Depth = new int?(num);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A562 RID: 42338 RVA: 0x003C791F File Offset: 0x003C5B1F
		public override bool HandleEvent(ObjectEnteringCellEvent E)
		{
			if (E.Type != "Charge")
			{
				this.AttemptRifle(E.Object, true, E.Cell, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A563 RID: 42339 RVA: 0x003C7950 File Offset: 0x003C5B50
		public bool AttemptRifle(GameObject Actor, bool Automatic = false, Cell FromCell = null, List<GameObject> Tracking = null)
		{
			TrashRifling part = Actor.GetPart<TrashRifling>();
			if (part == null || ((Automatic || !Actor.IsPlayer()) && !part.IsActive()))
			{
				return false;
			}
			Customs_TrashDivining part2 = Actor.GetPart<Customs_TrashDivining>();
			Tinkering_Scavenger part3 = Actor.GetPart<Tinkering_Scavenger>();
			Cell currentCell = this.ParentObject.GetCurrentCell();
			GameObject gameObject = null;
			bool flag = false;
			if (Automatic)
			{
				if (this.ParentObject.IsImportant())
				{
					return false;
				}
				if (Actor.IsPlayer())
				{
					if (Actor.ArePerceptibleHostilesNearby(false, false, null, null, null, Options.AutoexploreIgnoreEasyEnemies, Options.AutoexploreIgnoreDistantEnemies, false, false))
					{
						return false;
					}
				}
				else if (Actor.Target != null)
				{
					return false;
				}
			}
			if (!Actor.CheckFrozen(false, true, true, null))
			{
				if (!Automatic && Actor.IsPlayer())
				{
					Actor.CheckFrozen(false, false, false, null);
				}
				return false;
			}
			Cell cell = FromCell ?? Actor.GetCurrentCell();
			string text = null;
			if (currentCell != null && currentCell != cell)
			{
				if (Actor.IsPlayer() || IComponent<GameObject>.Visible(Actor))
				{
					text = Directions.GetDirectionDescription(Actor, cell.GetDirectionFromCell(currentCell, false));
				}
				else
				{
					text = "to their " + Directions.GetExpandedDirection(cell.GetDirectionFromCell(currentCell, false));
				}
			}
			this.ParentObject.PlayWorldSound("Sounds/Interact/sfx_interact_garbage_rifle", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			bool flag2 = true;
			if (part2 != null)
			{
				if (gameObject == null)
				{
					gameObject = this.ParentObject.RemoveOne();
				}
				if ((Actor.IsPlayer() || (Actor.IsPlayerLed() && The.Player.HasSkill("Customs_TrashDivining"))) && GetSkillEffectChanceEvent.GetFor(Actor, gameObject, part2, 5, true, false).in100())
				{
					IBaseJournalEntry randomUnrevealedNote = JournalAPI.GetRandomUnrevealedNote(null);
					string text2;
					if (randomUnrevealedNote is JournalMapNote)
					{
						text2 = string.Concat(new string[]
						{
							"Rifling through ",
							gameObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
							(text == null) ? "" : (" " + text),
							", ",
							Actor.does("piece", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
							" together clues and",
							Actor.GetVerb("determine", true, false, false, true, false),
							" the location of:\n\n"
						});
						flag2 = false;
					}
					else
					{
						text2 = string.Concat(new string[]
						{
							"Rifling through ",
							gameObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
							(text == null) ? "" : (" " + text),
							", ",
							Actor.does("piece", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
							" together clues and",
							Actor.GetVerb("arrive", true, false, false, true, false),
							" at the following conclusion:\n\n"
						});
						flag2 = false;
					}
					text2 += randomUnrevealedNote.Text;
					base.EmitMessage(text2, ' ', false, Options.PopupJournalNote, false, null, null);
					randomUnrevealedNote.Reveal(this.ParentObject.DisplayName, false);
				}
			}
			if (part3 != null)
			{
				if (gameObject == null)
				{
					gameObject = this.ParentObject.RemoveOne();
				}
				int num = Stat.Random(1, 100);
				int num2 = Tier.Constrain((this.Depth.Value - 10) / 4 + this.Level);
				if (num > 75)
				{
					if (num <= 99)
					{
						GameObject gameObject2 = GameObjectFactory.create(PopulationManager.RollOneFrom("Scrap " + num2.ToString(), null, null).Blueprint);
						currentCell.AddObject(gameObject2, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						if (Actor.IsPlayer())
						{
							IComponent<GameObject>.EmitMessage(Actor, string.Concat(new string[]
							{
								"You rifle through ",
								gameObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
								(text == null) ? "" : (" " + text),
								", and find ",
								gameObject2.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
								"."
							}), ' ', !Automatic, !Options.ShowScavengeItemAsMessage, false, null, null);
						}
						flag = true;
						flag2 = false;
					}
					else
					{
						GameObject gameObject3 = GameObjectFactory.create(PopulationManager.RollOneFrom("Junk " + num2.ToString(), null, null).Blueprint);
						currentCell.AddObject(gameObject3, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						if (Actor.IsPlayer())
						{
							base.EmitMessage(string.Concat(new string[]
							{
								"You rifle through ",
								gameObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
								(text == null) ? "" : (" " + text),
								", and find ",
								gameObject3.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
								"."
							}), ' ', !Automatic, !Options.ShowScavengeItemAsMessage, false, null, null);
						}
						flag = true;
						flag2 = false;
					}
				}
			}
			if (gameObject != null)
			{
				if (Actor.IsPlayer())
				{
					if (flag2)
					{
						IComponent<GameObject>.EmitMessage(Actor, "{{K|You rifle through " + gameObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ((text == null) ? "" : (" " + text)) + ", but you find nothing.}}", ' ', !Automatic, false, false, null, null);
					}
				}
				else if (IComponent<GameObject>.Visible(Actor))
				{
					IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
					{
						Actor.Does("rifle", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false),
						" through ",
						gameObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
						(text == null) ? "" : (" " + text),
						"."
					}), null, true);
				}
				else if (IComponent<GameObject>.Visible(gameObject))
				{
					IComponent<GameObject>.AddPlayerMessage("Somebody rifles through " + gameObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + ((text == null) ? "" : (" " + text)) + ".", null, true);
				}
				if (IComponent<GameObject>.Visible(gameObject))
				{
					gameObject.DustPuff("&y", 15);
				}
				gameObject.Destroy(null, false, false, null);
				Actor.UseEnergy(1000, "Skill", null, null, false);
			}
			return flag;
		}

		// Token: 0x04004458 RID: 17496
		public static readonly string RIFLE_INTERACTION = "RifleThroughGarbage";

		// Token: 0x04004459 RID: 17497
		public int? Depth;

		// Token: 0x0400445A RID: 17498
		public int Level;
	}
}

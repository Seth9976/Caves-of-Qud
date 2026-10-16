using System;
using Qud.API;
using XRL.Core;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013C6 RID: 5062
	[Serializable]
	public class Amnesia : BaseMutation
	{
		// Token: 0x0600C393 RID: 50067 RVA: 0x00476163 File Offset: 0x00474363
		public Amnesia()
		{
			base.Type = "Mental";
		}

		// Token: 0x0600C394 RID: 50068 RVA: 0x0047617E File Offset: 0x0047437E
		public override bool CanLevel()
		{
			return false;
		}

		// Token: 0x0600C395 RID: 50069 RVA: 0x00476181 File Offset: 0x00474381
		public override void RegisterActive(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register(The.Game, PooledEvent<SecretVisibilityChangedEvent>.ID, 0, false);
		}

		// Token: 0x0600C396 RID: 50070 RVA: 0x00476195 File Offset: 0x00474395
		public override string GetDescription()
		{
			return "You forget things and places.\n\nWhenever you learn a new secret, there's a small chance you forget a secret.\nWhenever you return to a map you previously visited, there's a small chance you forget the layout.";
		}

		// Token: 0x0600C397 RID: 50071 RVA: 0x0047619C File Offset: 0x0047439C
		public override string GetLevelText(int Level)
		{
			return "";
		}

		// Token: 0x0600C398 RID: 50072 RVA: 0x004761A3 File Offset: 0x004743A3
		public bool DoesSecretTrigger(IBaseJournalEntry Entry)
		{
			return Entry is JournalSultanNote || Entry is JournalMapNote || Entry is JournalObservation || Entry is JournalRecipeNote;
		}

		// Token: 0x0600C399 RID: 50073 RVA: 0x004761D0 File Offset: 0x004743D0
		public static bool AffectsSecret(IBaseJournalEntry Note)
		{
			return Note != null && Note.Forgettable() && (Note.Has("gossip") || Note.Has("sultan") || Note.Has("village") || Note is JournalMapNote);
		}

		// Token: 0x0600C39A RID: 50074 RVA: 0x0047621E File Offset: 0x0047441E
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == EnteredCellEvent.ID;
		}

		// Token: 0x0600C39B RID: 50075 RVA: 0x00476234 File Offset: 0x00474434
		public override bool HandleEvent(SecretVisibilityChangedEvent E)
		{
			if (this.ParentObject != null && this.ParentObject.IsPlayer() && this.LastProccedTurn != The.Game.Turns && E.Entry.Revealed && this.DoesSecretTrigger(E.Entry) && 5.in100())
			{
				this.LastProccedTurn = The.Game.Turns;
				IBaseJournalEntry randomElement = JournalAPI.GetKnownNotes(new Predicate<IBaseJournalEntry>(Amnesia.AffectsSecret)).GetRandomElement<IBaseJournalEntry>(null);
				if (randomElement != null && randomElement != E.Entry)
				{
					randomElement.Forget(false);
					IComponent<GameObject>.AddPlayerMessage("You feel like you forgot something important.", null, true);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C39C RID: 50076 RVA: 0x004762DC File Offset: 0x004744DC
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (5.in100() && this.ParentObject.IsPlayer())
			{
				Zone zone = this.ParentObject.CurrentZone;
				if (zone != null && this.currentZone != zone.ZoneID && !zone.IsWorldMap() && zone.LastPlayerPresence != -1L && zone.LastPlayerPresence < XRLCore.CurrentTurn - 2L)
				{
					zone.ClearExploredMap();
					GameObject parentObject = this.ParentObject;
					if (parentObject != null)
					{
						parentObject.PlayWorldSound("sfx_ability_mutation_mentalDefect_generic_activate", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
					}
					IComponent<GameObject>.AddPlayerMessage("This place feels vaguely familiar.", null, true);
					GenericDeepNotifyEvent.Send(this.ParentObject, "AmnesiaTriggered", this.ParentObject, this.ParentObject, 0);
				}
				this.currentZone = zone.ZoneID;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C39D RID: 50077 RVA: 0x004763C5 File Offset: 0x004745C5
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C39E RID: 50078 RVA: 0x004763CE File Offset: 0x004745CE
		public override bool Mutate(GameObject GO, int Level)
		{
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C39F RID: 50079 RVA: 0x004763D8 File Offset: 0x004745D8
		public override bool Unmutate(GameObject GO)
		{
			return base.Unmutate(GO);
		}

		// Token: 0x04004DBE RID: 19902
		[NonSerialized]
		private long LastProccedTurn = -1L;

		// Token: 0x04004DBF RID: 19903
		public string currentZone;
	}
}

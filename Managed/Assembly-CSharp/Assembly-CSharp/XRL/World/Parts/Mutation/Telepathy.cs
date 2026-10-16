using System;
using XRL.UI;
using XRL.World.Effects;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013FE RID: 5118
	[Serializable]
	public class Telepathy : BaseMutation
	{
		// Token: 0x0600C692 RID: 50834 RVA: 0x00486322 File Offset: 0x00484522
		public Telepathy()
		{
			base.Type = "Mental";
		}

		// Token: 0x0600C693 RID: 50835 RVA: 0x00486335 File Offset: 0x00484535
		public override bool CanLevel()
		{
			return false;
		}

		// Token: 0x0600C694 RID: 50836 RVA: 0x00486338 File Offset: 0x00484538
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CommandEvent>.ID;
		}

		// Token: 0x0600C695 RID: 50837 RVA: 0x00486350 File Offset: 0x00484550
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == Telepathy.COMMAND_NAME)
			{
				if (this.RealityDistortionBased && !this.ParentObject.IsRealityDistortionUsable(null, null))
				{
					RealityStabilized.ShowGenericInterdictMessage(this.ParentObject, null);
					return false;
				}
				SoundManager.PreloadClipSet("Sounds/Abilities/sfx_ability_telepathy");
				Cell cell = base.PickDestinationCell(80, AllowVis.OnlyExplored, true, false, false, true, PickTarget.PickStyle.EmptyCell, "Telepathy", false, null);
				if (cell != null)
				{
					this.ParentObject.PlayWorldSound("Sounds/Abilities/sfx_ability_telepathy", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
					cell.PlayWorldSound("Sounds/Abilities/sfx_ability_telepathy", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
					if (this.RealityDistortionBased)
					{
						Event @event = Event.New("InitiateRealityDistortionTransit", "Object", this.ParentObject, "Mutation", this, "Cell", cell);
						if (!this.ParentObject.FireEvent(@event, E) || !cell.FireEvent(@event, E))
						{
							return false;
						}
					}
					cell.ForeachObjectWithPart("ConversationScript", delegate(GameObject GO)
					{
						GO.GetPart<ConversationScript>().AttemptConversation(false, new bool?(true), null);
					});
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C696 RID: 50838 RVA: 0x0048648C File Offset: 0x0048468C
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600C697 RID: 50839 RVA: 0x00486490 File Offset: 0x00484690
		public override string GetDescription()
		{
			string text = "";
			text += "You may communicate with others through the psychic aether.\n\n";
			text += "Chat with anyone in vision\nTakes you much less time to issue orders to companions";
			if (Options.AnySifrah)
			{
				text += "\nUseful in many social and psionic Sifrah games.";
			}
			return text;
		}

		// Token: 0x0600C698 RID: 50840 RVA: 0x004864CF File Offset: 0x004846CF
		public override string GetLevelText(int Level)
		{
			return "";
		}

		// Token: 0x0600C699 RID: 50841 RVA: 0x004864D6 File Offset: 0x004846D6
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C69A RID: 50842 RVA: 0x004864E0 File Offset: 0x004846E0
		public override bool Mutate(GameObject GO, int Level)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Telepathy", Telepathy.COMMAND_NAME, "Mental Mutations", null, "\u000e", null, false, false, false, false, this.RealityDistortionBased, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C69B RID: 50843 RVA: 0x0048652E File Offset: 0x0048472E
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.Unmutate(GO);
		}

		// Token: 0x04004E42 RID: 20034
		public static readonly string COMMAND_NAME = "CommandTelepathy";

		// Token: 0x04004E43 RID: 20035
		public bool RealityDistortionBased;
	}
}

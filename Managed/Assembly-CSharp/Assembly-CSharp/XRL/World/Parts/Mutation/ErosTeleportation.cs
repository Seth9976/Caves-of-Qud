using System;
using XRL.Rules;
using XRL.UI;
using XRL.World.Effects;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x0200145E RID: 5214
	[Serializable]
	public class ErosTeleportation : BaseMutation
	{
		// Token: 0x0600CC4E RID: 52302 RVA: 0x004A381B File Offset: 0x004A1A1B
		public ErosTeleportation()
		{
			base.Type = "Mental";
		}

		// Token: 0x0600CC4F RID: 52303 RVA: 0x004A382E File Offset: 0x004A1A2E
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600CC50 RID: 52304 RVA: 0x004A3831 File Offset: 0x004A1A31
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetMovementAbilityListEvent.ID || ID == AIGetOffensiveAbilityListEvent.ID || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600CC51 RID: 52305 RVA: 0x004A3858 File Offset: 0x004A1A58
		public override bool HandleEvent(AIGetMovementAbilityListEvent E)
		{
			if (E.TargetCell != null && E.Distance > 1 && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && E.Actor.PartyLeader != null && E.Actor.PartyLeader.Target != null && E.Actor.PartyLeader.DistanceTo(E.Target) <= 1 && IComponent<GameObject>.CheckRealityDistortionAdvisability(E.Actor, E.TargetCell, E.Actor, null, this, null, 0))
			{
				E.Add("CommandLeaderTeleport", 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CC52 RID: 52306 RVA: 0x004A3900 File Offset: 0x004A1B00
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (E.Distance > 3 && GameObject.Validate(E.Target) && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && E.Actor.PartyLeader != null && E.Actor.PartyLeader.Target != null && E.Actor.PartyLeader.DistanceTo(E.Target) <= 1 && base.CheckMyRealityDistortionAdvisability(null, null, null, null, null, null, 0))
			{
				E.Add("CommandLeaderTeleport", 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CC53 RID: 52307 RVA: 0x004A3999 File Offset: 0x004A1B99
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("travel", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CC54 RID: 52308 RVA: 0x004A39BC File Offset: 0x004A1BBC
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CommandLeaderTeleport");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600CC55 RID: 52309 RVA: 0x004A39D1 File Offset: 0x004A1BD1
		public override string GetDescription()
		{
			return "You teleport to a nearby location near your leader.";
		}

		// Token: 0x0600CC56 RID: 52310 RVA: 0x004A39D8 File Offset: 0x004A1BD8
		public override string GetLevelText(int Level)
		{
			return "Cooldown: " + this.GetCooldown(Level).ToString() + " rounds";
		}

		// Token: 0x0600CC57 RID: 52311 RVA: 0x004A3A04 File Offset: 0x004A1C04
		public int GetCooldown(int Level)
		{
			int num = 125 - 10 * Level;
			if (num < 5)
			{
				num = 5;
			}
			return num;
		}

		// Token: 0x0600CC58 RID: 52312 RVA: 0x004A3A20 File Offset: 0x004A1C20
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), this.GetCooldown(Level), 0);
		}

		// Token: 0x0600CC59 RID: 52313 RVA: 0x004A3A40 File Offset: 0x004A1C40
		public static bool Cast(ErosTeleportation mutation = null, string level = "5-6", Event E = null, Cell Destination = null)
		{
			if (mutation == null)
			{
				mutation = new ErosTeleportation();
				mutation.ParentObject = The.Player;
				mutation.Level = Stat.Roll(level, null);
			}
			Cell cell = null;
			GameObject parentObject = mutation.ParentObject;
			if (!parentObject.IsRealityDistortionUsable(null, null))
			{
				RealityStabilized.ShowGenericInterdictMessage(parentObject, null);
				return false;
			}
			if (parentObject.PartyLeader != null)
			{
				Cell currentCell = parentObject.PartyLeader.CurrentCell;
				if (currentCell != null)
				{
					cell = currentCell.GetEmptyAdjacentCells().GetRandomElement<Cell>(null);
				}
			}
			if (cell == null)
			{
				return false;
			}
			if (parentObject.IsPlayer())
			{
				if (!cell.IsExplored())
				{
					Popup.ShowFail("You can only teleport to a place you have seen before!", true, true, true);
					return false;
				}
				if (!cell.IsEmptyOfSolid(true))
				{
					Popup.ShowFail("You may only teleport into an empty square!", true, true, true);
					return false;
				}
			}
			Event @event = Event.New("InitiateRealityDistortionTransit", "Object", parentObject, "Mutation", mutation, "Cell", cell);
			if (!parentObject.FireEvent(@event, E) || !cell.FireEvent(@event, E))
			{
				return false;
			}
			parentObject.ParticleBlip("&C\u000f", 10, 0L, false);
			parentObject.TeleportTo(cell, new int?(0), true, false, false, false, "disappear", "appear");
			parentObject.TeleportSwirl(null, "&C", true, null, 'ù', false);
			parentObject.ParticleBlip("&C\u000f", 10, 0L, false);
			mutation.UseEnergy(1000, "Mental Mutation E-Ros Teleportation");
			mutation.CooldownMyActivatedAbility(mutation.ActivatedAbilityID, Math.Max(125 - 10 * mutation.Level, 5), null, null, false);
			IComponent<GameObject>.EmitMessage(parentObject, "E-Ros yells, {{W|'I'm coming, " + parentObject.PartyLeader.BaseDisplayName + "!'}}", ' ', false, false, false, null, null);
			parentObject.ParticleText("I'm coming, " + parentObject.PartyLeader.BaseDisplayName + "!", 'W', false, 1.5f, -8f);
			return true;
		}

		// Token: 0x0600CC5A RID: 52314 RVA: 0x004A3BF5 File Offset: 0x004A1DF5
		public override bool FireEvent(Event E)
		{
			return (!(E.ID == "CommandLeaderTeleport") || ErosTeleportation.Cast(this, null, E, E.GetParameter("TargetCell") as Cell)) && base.FireEvent(E);
		}

		// Token: 0x0600CC5B RID: 52315 RVA: 0x004A3C2C File Offset: 0x004A1E2C
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600CC5C RID: 52316 RVA: 0x004A3C38 File Offset: 0x004A1E38
		public override bool Mutate(GameObject GO, int Level)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Teleport", "CommandLeaderTeleport", "Mental Mutations", null, "\u001d", null, false, false, false, false, true, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600CC5D RID: 52317 RVA: 0x004A3C81 File Offset: 0x004A1E81
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.Unmutate(GO);
		}
	}
}

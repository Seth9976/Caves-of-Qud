using System;
using System.Collections.Generic;
using XRL.World.AI;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x0200064A RID: 1610
	[GameEvent(Cascade = 64, Cache = MinEvent.Cache.Pool)]
	public class AIHelpBroadcastEvent : PooledEvent<AIHelpBroadcastEvent>
	{
		// Token: 0x060045D3 RID: 17875 RVA: 0x00164FC2 File Offset: 0x001631C2
		public override int GetCascadeLevel()
		{
			return AIHelpBroadcastEvent.CascadeLevel;
		}

		// Token: 0x060045D4 RID: 17876 RVA: 0x00164FC9 File Offset: 0x001631C9
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x060045D5 RID: 17877 RVA: 0x00164FD4 File Offset: 0x001631D4
		public override void Reset()
		{
			base.Reset();
			this.Actor = null;
			this.Target = null;
			this.Item = null;
			this.Factions.Clear();
			this.Cause = HelpCause.General;
			this.Magnitude = 1f;
			this.Flood.Clear();
		}

		// Token: 0x060045D6 RID: 17878 RVA: 0x00165024 File Offset: 0x00163224
		public static void Send(GameObject Actor, GameObject Target, GameObject Item = null, string Faction = null, int Radius = 20, float Magnitude = 1f, HelpCause Cause = HelpCause.General)
		{
			Cell currentCell = Actor.CurrentCell;
			if (currentCell == null)
			{
				return;
			}
			AIHelpBroadcastEvent aihelpBroadcastEvent = PooledEvent<AIHelpBroadcastEvent>.FromPool();
			aihelpBroadcastEvent.Actor = Actor;
			aihelpBroadcastEvent.Target = Target;
			aihelpBroadcastEvent.Item = Item;
			aihelpBroadcastEvent.Cause = Cause;
			aihelpBroadcastEvent.Magnitude = Magnitude;
			Brain brain = Actor.Brain;
			AllegianceSet allegianceSet = ((brain != null) ? brain.Allegiance : null);
			if (allegianceSet != null)
			{
				foreach (KeyValuePair<string, int> keyValuePair in allegianceSet)
				{
					if (Brain.GetAllegianceLevel(new int?(keyValuePair.Value)) == Brain.AllegianceLevel.Member)
					{
						aihelpBroadcastEvent.Factions.Add(keyValuePair.Key);
					}
				}
			}
			Physics physics = Actor.Physics;
			string text = ((physics != null) ? physics.Owner : null);
			if (!text.IsNullOrEmpty() && !aihelpBroadcastEvent.Factions.Contains(text))
			{
				aihelpBroadcastEvent.Factions.Add(text);
			}
			if (!Faction.IsNullOrEmpty() && !aihelpBroadcastEvent.Factions.Contains(Faction))
			{
				aihelpBroadcastEvent.Factions.Add(Faction);
			}
			aihelpBroadcastEvent.Flood.Clear();
			currentCell.ParentZone.FastFloodVisibility(currentCell.X, currentCell.Y, 20, aihelpBroadcastEvent.Flood, typeof(Brain), Actor, null, null);
			Brain brain2 = Actor.Brain;
			GameObject gameObject = ((brain2 != null) ? brain2.GetFinalLeader() : null);
			if (((gameObject != null) ? gameObject.Brain : null) != null && gameObject.HandleEvent(aihelpBroadcastEvent))
			{
				gameObject.Brain.HandleEvent(aihelpBroadcastEvent);
			}
			foreach (GameObject gameObject2 in aihelpBroadcastEvent.Flood)
			{
				if (gameObject2 != Actor && gameObject2 != gameObject && gameObject2.HandleEvent(aihelpBroadcastEvent))
				{
					gameObject2.Brain.HandleEvent(aihelpBroadcastEvent);
				}
			}
			PooledEvent<AIHelpBroadcastEvent>.ResetTo(ref aihelpBroadcastEvent);
		}

		// Token: 0x04002121 RID: 8481
		public new static readonly int CascadeLevel = 64;

		// Token: 0x04002122 RID: 8482
		public GameObject Actor;

		// Token: 0x04002123 RID: 8483
		public GameObject Target;

		// Token: 0x04002124 RID: 8484
		public GameObject Item;

		// Token: 0x04002125 RID: 8485
		public List<string> Factions = new List<string>();

		// Token: 0x04002126 RID: 8486
		public HelpCause Cause;

		// Token: 0x04002127 RID: 8487
		public float Magnitude = 1f;

		// Token: 0x04002128 RID: 8488
		private List<GameObject> Flood = new List<GameObject>();
	}
}

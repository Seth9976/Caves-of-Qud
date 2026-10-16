using System;
using XRL.World.Parts.Mutation;

namespace XRL.World
{
	// Token: 0x0200076D RID: 1901
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetMutationTermEvent : PooledEvent<GetMutationTermEvent>
	{
		// Token: 0x06004E3B RID: 20027 RVA: 0x0017FC3B File Offset: 0x0017DE3B
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004E3C RID: 20028 RVA: 0x0017FC44 File Offset: 0x0017DE44
		public override void Reset()
		{
			base.Reset();
			this.Creature = null;
			this.Mutation = null;
			this.Term = null;
			this.Color = null;
		}

		// Token: 0x06004E3D RID: 20029 RVA: 0x0017FC68 File Offset: 0x0017DE68
		public static GetMutationTermEvent FromPool(GameObject Creature, BaseMutation Mutation, string Term, string Color)
		{
			GetMutationTermEvent getMutationTermEvent = PooledEvent<GetMutationTermEvent>.FromPool();
			getMutationTermEvent.Creature = Creature;
			getMutationTermEvent.Mutation = Mutation;
			getMutationTermEvent.Term = Term;
			getMutationTermEvent.Color = Color;
			return getMutationTermEvent;
		}

		// Token: 0x06004E3E RID: 20030 RVA: 0x0017FC8C File Offset: 0x0017DE8C
		public static void GetFor(GameObject Creature, out string Term, out string Color, BaseMutation Mutation = null)
		{
			Term = "mutation";
			Color = "M";
			bool flag = true;
			if (flag && GameObject.Validate(ref Creature) && Creature.HasRegisteredEvent("GetMutationTerm"))
			{
				Event @event = Event.New("GetMutationTerm", 0, 0, 0);
				@event.SetParameter("Creature", Creature);
				@event.SetParameter("Mutation", Mutation);
				@event.SetParameter("Term", Term);
				@event.SetParameter("Color", Color);
				flag = Creature.FireEvent(@event);
				Term = @event.GetStringParameter("Term", null);
				Color = @event.GetStringParameter("Color", null);
			}
			if (flag && GameObject.Validate(ref Creature) && Creature.WantEvent(PooledEvent<GetMutationTermEvent>.ID, MinEvent.CascadeLevel))
			{
				GetMutationTermEvent getMutationTermEvent = GetMutationTermEvent.FromPool(Creature, Mutation, Term, Color);
				flag = Creature.HandleEvent(getMutationTermEvent);
				Term = getMutationTermEvent.Term;
				Color = getMutationTermEvent.Color;
			}
		}

		// Token: 0x06004E3F RID: 20031 RVA: 0x0017FD6C File Offset: 0x0017DF6C
		public static string GetFor(GameObject Creature, BaseMutation Mutation = null)
		{
			string text = "mutation";
			string text2 = "M";
			bool flag = true;
			if (flag && GameObject.Validate(ref Creature) && Creature.HasRegisteredEvent("GetMutationTerm"))
			{
				Event @event = Event.New("GetMutationTerm", 0, 0, 0);
				@event.SetParameter("Creature", Creature);
				@event.SetParameter("Mutation", Mutation);
				@event.SetParameter("Term", text);
				@event.SetParameter("Color", text2);
				flag = Creature.FireEvent(@event);
				text = @event.GetStringParameter("Term", null);
				text2 = @event.GetStringParameter("Color", null);
			}
			if (flag && GameObject.Validate(ref Creature) && Creature.WantEvent(PooledEvent<GetMutationTermEvent>.ID, MinEvent.CascadeLevel))
			{
				GetMutationTermEvent getMutationTermEvent = GetMutationTermEvent.FromPool(Creature, Mutation, text, text2);
				flag = Creature.HandleEvent(getMutationTermEvent);
				text = getMutationTermEvent.Term;
				text2 = getMutationTermEvent.Color;
			}
			return text;
		}

		// Token: 0x0400262C RID: 9772
		public const string DEFAULT_TERM = "mutation";

		// Token: 0x0400262D RID: 9773
		public const string DEFAULT_COLOR = "M";

		// Token: 0x0400262E RID: 9774
		public GameObject Creature;

		// Token: 0x0400262F RID: 9775
		public BaseMutation Mutation;

		// Token: 0x04002630 RID: 9776
		public string Term;

		// Token: 0x04002631 RID: 9777
		public string Color;
	}
}

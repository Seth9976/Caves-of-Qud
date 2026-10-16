using System;
using System.Collections.Generic;

namespace XRL.World.Conversations
{
	/// <summary>Fired by the IfCommand predicate and controls visibility similarly to IsElementVisibleEvent.</summary>
	// Token: 0x02000BD5 RID: 3029
	[ConversationEvent(Action = ConversationEvent.Action.Get)]
	public class PredicateEvent : IDelegateEvent
	{
		// Token: 0x060079A5 RID: 31141 RVA: 0x002DCD44 File Offset: 0x002DAF44
		public PredicateEvent()
			: base(PredicateEvent.ID)
		{
		}

		// Token: 0x060079A6 RID: 31142 RVA: 0x002DCD51 File Offset: 0x002DAF51
		public static int CountPool()
		{
			if (PredicateEvent.Pool != null)
			{
				return PredicateEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060079A7 RID: 31143 RVA: 0x002DCD66 File Offset: 0x002DAF66
		public static void ResetPool()
		{
			while (PredicateEvent.PoolCounter > 0)
			{
				PredicateEvent.Pool[--PredicateEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060079A8 RID: 31144 RVA: 0x002DCD8E File Offset: 0x002DAF8E
		public static void ResetTo(ref PredicateEvent E)
		{
			ConversationEvent.ResetTo<PredicateEvent>(E, PredicateEvent.Pool, ref PredicateEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060079A9 RID: 31145 RVA: 0x002DCDA4 File Offset: 0x002DAFA4
		public static PredicateEvent FromPool()
		{
			return ConversationEvent.FromPool<PredicateEvent>(ref PredicateEvent.Pool, ref PredicateEvent.PoolCounter, 8192);
		}

		// Token: 0x060079AA RID: 31146 RVA: 0x002DCDBA File Offset: 0x002DAFBA
		public static PredicateEvent FromPool(IConversationElement Element, string Delegate, string Command, bool Result = false)
		{
			PredicateEvent predicateEvent = PredicateEvent.FromPool();
			predicateEvent.Element = Element;
			predicateEvent.Delegate = Delegate;
			predicateEvent.Command = Command;
			predicateEvent.Result = Result;
			return predicateEvent;
		}

		// Token: 0x060079AB RID: 31147 RVA: 0x002DCDDD File Offset: 0x002DAFDD
		public override void Reset()
		{
			base.Reset();
			this.Result = false;
		}

		// Token: 0x060079AC RID: 31148 RVA: 0x002DCDEC File Offset: 0x002DAFEC
		public override bool HandlePartDispatch(IConversationPart Part)
		{
			return Part.HandleEvent(this);
		}

		// Token: 0x060079AD RID: 31149 RVA: 0x002DCDF8 File Offset: 0x002DAFF8
		public static bool GetFor(IConversationElement Element, string Delegate, string Command, bool Result = false)
		{
			if (Element.WantEvent(PredicateEvent.ID))
			{
				PredicateEvent predicateEvent = PredicateEvent.FromPool(Element, Delegate, Command, Result);
				Element.HandleEvent(predicateEvent);
				return predicateEvent.Result;
			}
			return false;
		}

		// Token: 0x040037A0 RID: 14240
		public new static readonly int ID = ConversationEvent.RegisterEvent(typeof(PredicateEvent), null, new ConversationEvent.EventPoolCount(PredicateEvent.CountPool), new ConversationEvent.EventPoolReset(PredicateEvent.ResetPool));

		// Token: 0x040037A1 RID: 14241
		private static List<PredicateEvent> Pool;

		// Token: 0x040037A2 RID: 14242
		private static int PoolCounter;

		// Token: 0x040037A3 RID: 14243
		[ConversationEvent.ParameterAttribute(Get = true)]
		public bool Result;
	}
}

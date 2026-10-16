using System;
using System.Collections.Generic;
using ConsoleLib.Console;

namespace XRL.World.Conversations
{
	/// <summary>Fired before the current node is rendered to screen.</summary>
	// Token: 0x02000BD8 RID: 3032
	[ConversationEvent(Action = ConversationEvent.Action.Send)]
	public class RenderNodeEvent : ConversationEvent
	{
		// Token: 0x060079C2 RID: 31170 RVA: 0x002DD0C8 File Offset: 0x002DB2C8
		public RenderNodeEvent()
			: base(RenderNodeEvent.ID)
		{
		}

		// Token: 0x060079C3 RID: 31171 RVA: 0x002DD0D5 File Offset: 0x002DB2D5
		public static int CountPool()
		{
			if (RenderNodeEvent.Pool != null)
			{
				return RenderNodeEvent.Pool.Count;
			}
			return 0;
		}

		// Token: 0x060079C4 RID: 31172 RVA: 0x002DD0EA File Offset: 0x002DB2EA
		public static void ResetPool()
		{
			while (RenderNodeEvent.PoolCounter > 0)
			{
				RenderNodeEvent.Pool[--RenderNodeEvent.PoolCounter].Reset();
			}
		}

		// Token: 0x060079C5 RID: 31173 RVA: 0x002DD112 File Offset: 0x002DB312
		public static void ResetTo(ref RenderNodeEvent E)
		{
			ConversationEvent.ResetTo<RenderNodeEvent>(E, RenderNodeEvent.Pool, ref RenderNodeEvent.PoolCounter);
			E = null;
		}

		// Token: 0x060079C6 RID: 31174 RVA: 0x002DD128 File Offset: 0x002DB328
		public static RenderNodeEvent FromPool()
		{
			return ConversationEvent.FromPool<RenderNodeEvent>(ref RenderNodeEvent.Pool, ref RenderNodeEvent.PoolCounter, 8192);
		}

		// Token: 0x060079C7 RID: 31175 RVA: 0x002DD13E File Offset: 0x002DB33E
		public static RenderNodeEvent FromPool(IConversationElement Element, string Title, IRenderable Icon)
		{
			RenderNodeEvent renderNodeEvent = RenderNodeEvent.FromPool();
			renderNodeEvent.Element = Element;
			renderNodeEvent.Title = Title;
			renderNodeEvent.Icon = Icon;
			return renderNodeEvent;
		}

		// Token: 0x060079C8 RID: 31176 RVA: 0x002DD15A File Offset: 0x002DB35A
		public override void Reset()
		{
			base.Reset();
			this.Title = null;
			this.Icon = null;
		}

		// Token: 0x060079C9 RID: 31177 RVA: 0x002DD170 File Offset: 0x002DB370
		public override bool HandlePartDispatch(IConversationPart Part)
		{
			return Part.HandleEvent(this);
		}

		// Token: 0x060079CA RID: 31178 RVA: 0x002DD17C File Offset: 0x002DB37C
		public static void Send(IConversationElement Element, ref string Title, ref IRenderable Icon)
		{
			if (Element.WantEvent(RenderNodeEvent.ID))
			{
				RenderNodeEvent renderNodeEvent = RenderNodeEvent.FromPool(Element, Title, Icon);
				Element.HandleEvent(renderNodeEvent);
				Title = renderNodeEvent.Title;
				Icon = renderNodeEvent.Icon;
			}
		}

		// Token: 0x040037AD RID: 14253
		public new static readonly int ID = ConversationEvent.RegisterEvent(typeof(RenderNodeEvent), null, new ConversationEvent.EventPoolCount(RenderNodeEvent.CountPool), new ConversationEvent.EventPoolReset(RenderNodeEvent.ResetPool));

		// Token: 0x040037AE RID: 14254
		private static List<RenderNodeEvent> Pool;

		// Token: 0x040037AF RID: 14255
		private static int PoolCounter;

		// Token: 0x040037B0 RID: 14256
		[ConversationEvent.ParameterAttribute(Reference = true)]
		public string Title;

		// Token: 0x040037B1 RID: 14257
		[ConversationEvent.ParameterAttribute(Reference = true)]
		public IRenderable Icon;
	}
}

using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000225 RID: 549
	[EventCategory(EventCategory.Command)]
	public abstract class CommandEventBase<T> : EventBase<T>, ICommandEvent where T : CommandEventBase<T>, new()
	{
		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06001049 RID: 4169 RVA: 0x00047770 File Offset: 0x00045970
		// (set) Token: 0x0600104A RID: 4170 RVA: 0x000477AF File Offset: 0x000459AF
		public string commandName
		{
			get
			{
				bool flag = this.m_CommandName == null && base.imguiEvent != null;
				string text;
				if (flag)
				{
					text = base.imguiEvent.commandName;
				}
				else
				{
					text = this.m_CommandName;
				}
				return text;
			}
			protected set
			{
				this.m_CommandName = value;
			}
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x000477B9 File Offset: 0x000459B9
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x000477CA File Offset: 0x000459CA
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.BubblesOrTricklesDown;
			this.commandName = null;
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x000477E0 File Offset: 0x000459E0
		public static T GetPooled(Event systemEvent)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.imguiEvent = systemEvent;
			return pooled;
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x00047808 File Offset: 0x00045A08
		public static T GetPooled(string commandName)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.commandName = commandName;
			return pooled;
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x0004782E File Offset: 0x00045A2E
		internal override void Dispatch(BaseVisualElementPanel panel)
		{
			EventDispatchUtilities.DispatchToFocusedElementOrPanelRoot(this, panel);
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x00047839 File Offset: 0x00045A39
		protected CommandEventBase()
		{
			this.LocalInit();
		}

		// Token: 0x040009D5 RID: 2517
		private string m_CommandName;
	}
}

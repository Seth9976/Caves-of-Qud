using System;
using JetBrains.Annotations;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x0200022B RID: 555
	public abstract class EventBase : IDisposable
	{
		// Token: 0x06001064 RID: 4196 RVA: 0x00047E30 File Offset: 0x00046030
		protected static long RegisterEventType()
		{
			return EventBase.s_LastTypeId += 1L;
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06001065 RID: 4197 RVA: 0x00047E50 File Offset: 0x00046050
		public virtual long eventTypeId
		{
			get
			{
				return -1L;
			}
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06001066 RID: 4198 RVA: 0x00047E54 File Offset: 0x00046054
		internal int eventCategories { get; }

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06001067 RID: 4199 RVA: 0x00047E5C File Offset: 0x0004605C
		// (set) Token: 0x06001068 RID: 4200 RVA: 0x00047E64 File Offset: 0x00046064
		public long timestamp { get; private set; }

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06001069 RID: 4201 RVA: 0x00047E6D File Offset: 0x0004606D
		// (set) Token: 0x0600106A RID: 4202 RVA: 0x00047E75 File Offset: 0x00046075
		internal ulong eventId { get; private set; }

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x0600106B RID: 4203 RVA: 0x00047E7E File Offset: 0x0004607E
		// (set) Token: 0x0600106C RID: 4204 RVA: 0x00047E86 File Offset: 0x00046086
		internal ulong triggerEventId { get; private set; }

		// Token: 0x0600106D RID: 4205 RVA: 0x00047E8F File Offset: 0x0004608F
		internal void SetTriggerEventId(ulong id)
		{
			this.triggerEventId = id;
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x0600106E RID: 4206 RVA: 0x00047E9A File Offset: 0x0004609A
		// (set) Token: 0x0600106F RID: 4207 RVA: 0x00047EA2 File Offset: 0x000460A2
		internal EventBase.EventPropagation propagation { get; set; }

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06001070 RID: 4208 RVA: 0x00047EAB File Offset: 0x000460AB
		// (set) Token: 0x06001071 RID: 4209 RVA: 0x00047EB3 File Offset: 0x000460B3
		private EventBase.LifeCycleStatus lifeCycleStatus { get; set; }

		// Token: 0x06001072 RID: 4210 RVA: 0x00002152 File Offset: 0x00000352
		[Obsolete("Override PreDispatch(IPanel panel) instead.")]
		protected virtual void PreDispatch()
		{
		}

		// Token: 0x06001073 RID: 4211 RVA: 0x00047EBC File Offset: 0x000460BC
		protected internal virtual void PreDispatch(IPanel panel)
		{
			this.PreDispatch();
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x00002152 File Offset: 0x00000352
		[Obsolete("Override PostDispatch(IPanel panel) instead.")]
		protected virtual void PostDispatch()
		{
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x00047EC6 File Offset: 0x000460C6
		protected internal virtual void PostDispatch(IPanel panel)
		{
			this.PostDispatch();
			this.processed = true;
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x00047ED8 File Offset: 0x000460D8
		internal virtual void Dispatch([JetBrains.Annotations.NotNull] BaseVisualElementPanel panel)
		{
			EventDispatchUtilities.DefaultDispatch(this, panel);
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06001077 RID: 4215 RVA: 0x00047EE4 File Offset: 0x000460E4
		// (set) Token: 0x06001078 RID: 4216 RVA: 0x00047F04 File Offset: 0x00046104
		public bool bubbles
		{
			get
			{
				return (this.propagation & EventBase.EventPropagation.Bubbles) > EventBase.EventPropagation.None;
			}
			protected set
			{
				if (value)
				{
					this.propagation |= EventBase.EventPropagation.Bubbles;
				}
				else
				{
					this.propagation &= ~EventBase.EventPropagation.Bubbles;
				}
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06001079 RID: 4217 RVA: 0x00047F3C File Offset: 0x0004613C
		// (set) Token: 0x0600107A RID: 4218 RVA: 0x00047F5C File Offset: 0x0004615C
		public bool tricklesDown
		{
			get
			{
				return (this.propagation & EventBase.EventPropagation.TricklesDown) > EventBase.EventPropagation.None;
			}
			protected set
			{
				if (value)
				{
					this.propagation |= EventBase.EventPropagation.TricklesDown;
				}
				else
				{
					this.propagation &= ~EventBase.EventPropagation.TricklesDown;
				}
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x0600107B RID: 4219 RVA: 0x00047F94 File Offset: 0x00046194
		// (set) Token: 0x0600107C RID: 4220 RVA: 0x00047FB4 File Offset: 0x000461B4
		internal bool skipDisabledElements
		{
			get
			{
				return (this.propagation & EventBase.EventPropagation.SkipDisabledElements) > EventBase.EventPropagation.None;
			}
			set
			{
				if (value)
				{
					this.propagation |= EventBase.EventPropagation.SkipDisabledElements;
				}
				else
				{
					this.propagation &= ~EventBase.EventPropagation.SkipDisabledElements;
				}
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x0600107D RID: 4221 RVA: 0x00047FEC File Offset: 0x000461EC
		internal bool bubblesOrTricklesDown
		{
			get
			{
				return (this.propagation & EventBase.EventPropagation.BubblesOrTricklesDown) > EventBase.EventPropagation.None;
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x0600107E RID: 4222 RVA: 0x00047FF9 File Offset: 0x000461F9
		// (set) Token: 0x0600107F RID: 4223 RVA: 0x00048001 File Offset: 0x00046201
		[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
		internal VisualElement elementTarget { get; set; }

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06001080 RID: 4224 RVA: 0x0004800A File Offset: 0x0004620A
		// (set) Token: 0x06001081 RID: 4225 RVA: 0x00048012 File Offset: 0x00046212
		public IEventHandler target
		{
			get
			{
				return this.elementTarget;
			}
			set
			{
				this.elementTarget = value as VisualElement;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06001082 RID: 4226 RVA: 0x00048024 File Offset: 0x00046224
		// (set) Token: 0x06001083 RID: 4227 RVA: 0x00048044 File Offset: 0x00046244
		public bool isPropagationStopped
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.PropagationStopped) > EventBase.LifeCycleStatus.None;
			}
			private set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.PropagationStopped;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.PropagationStopped;
				}
			}
		}

		// Token: 0x06001084 RID: 4228 RVA: 0x0004807C File Offset: 0x0004627C
		public void StopPropagation()
		{
			this.isPropagationStopped = true;
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x06001085 RID: 4229 RVA: 0x00048088 File Offset: 0x00046288
		// (set) Token: 0x06001086 RID: 4230 RVA: 0x000480A8 File Offset: 0x000462A8
		public bool isImmediatePropagationStopped
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.ImmediatePropagationStopped) > EventBase.LifeCycleStatus.None;
			}
			private set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.ImmediatePropagationStopped;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.ImmediatePropagationStopped;
				}
			}
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x000480E0 File Offset: 0x000462E0
		public void StopImmediatePropagation()
		{
			this.isPropagationStopped = true;
			this.isImmediatePropagationStopped = true;
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x06001088 RID: 4232 RVA: 0x000480F3 File Offset: 0x000462F3
		[Obsolete("Use isPropagationStopped. Before proceeding, make sure you understand the latest changes to UIToolkit event propagation rules by visiting Unity's manual page https://docs.unity3d.com/Manual/UIE-Events-Dispatching.html")]
		public bool isDefaultPrevented
		{
			get
			{
				return this.isPropagationStopped;
			}
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x000480FB File Offset: 0x000462FB
		[Obsolete("Use StopPropagation and/or FocusController.IgnoreEvent. Before proceeding, make sure you understand the latest changes to UIToolkit event propagation rules by visiting Unity's manual page https://docs.unity3d.com/Manual/UIE-Events-Dispatching.html")]
		public void PreventDefault()
		{
			this.StopPropagation();
			VisualElement elementTarget = this.elementTarget;
			if (elementTarget != null)
			{
				FocusController focusController = elementTarget.focusController;
				if (focusController != null)
				{
					focusController.IgnoreEvent(this);
				}
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x0600108A RID: 4234 RVA: 0x00048123 File Offset: 0x00046323
		// (set) Token: 0x0600108B RID: 4235 RVA: 0x0004812B File Offset: 0x0004632B
		public PropagationPhase propagationPhase { get; internal set; }

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x0600108C RID: 4236 RVA: 0x00048134 File Offset: 0x00046334
		// (set) Token: 0x0600108D RID: 4237 RVA: 0x0004814C File Offset: 0x0004634C
		public virtual IEventHandler currentTarget
		{
			get
			{
				return this.m_CurrentTarget;
			}
			internal set
			{
				this.m_CurrentTarget = value;
				bool flag = this.imguiEvent != null;
				if (flag)
				{
					VisualElement visualElement = this.currentTarget as VisualElement;
					bool flag2 = visualElement != null;
					if (flag2)
					{
						this.imguiEvent.mousePosition = visualElement.WorldToLocal(this.originalMousePosition);
					}
					else
					{
						this.imguiEvent.mousePosition = this.originalMousePosition;
					}
				}
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x0600108E RID: 4238 RVA: 0x000481B4 File Offset: 0x000463B4
		// (set) Token: 0x0600108F RID: 4239 RVA: 0x000481D4 File Offset: 0x000463D4
		public bool dispatch
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.Dispatching) > EventBase.LifeCycleStatus.None;
			}
			internal set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.Dispatching;
					this.dispatched = true;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.Dispatching;
				}
			}
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x00048214 File Offset: 0x00046414
		internal void MarkReceivedByDispatcher()
		{
			Debug.Assert(!this.dispatched, "Events cannot be dispatched more than once.");
			this.dispatched = true;
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06001091 RID: 4241 RVA: 0x00048234 File Offset: 0x00046434
		// (set) Token: 0x06001092 RID: 4242 RVA: 0x00048254 File Offset: 0x00046454
		private bool dispatched
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.Dispatched) > EventBase.LifeCycleStatus.None;
			}
			set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.Dispatched;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.Dispatched;
				}
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06001093 RID: 4243 RVA: 0x00048290 File Offset: 0x00046490
		// (set) Token: 0x06001094 RID: 4244 RVA: 0x000482B4 File Offset: 0x000464B4
		internal bool processed
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.Processed) > EventBase.LifeCycleStatus.None;
			}
			private set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.Processed;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.Processed;
				}
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06001095 RID: 4245 RVA: 0x000482F4 File Offset: 0x000464F4
		// (set) Token: 0x06001096 RID: 4246 RVA: 0x00048318 File Offset: 0x00046518
		internal bool processedByFocusController
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.ProcessedByFocusController) > EventBase.LifeCycleStatus.None;
			}
			set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.ProcessedByFocusController;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.ProcessedByFocusController;
				}
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06001097 RID: 4247 RVA: 0x00048358 File Offset: 0x00046558
		// (set) Token: 0x06001098 RID: 4248 RVA: 0x00048378 File Offset: 0x00046578
		internal bool propagateToIMGUI
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.PropagateToIMGUI) > EventBase.LifeCycleStatus.None;
			}
			set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.PropagateToIMGUI;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.PropagateToIMGUI;
				}
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06001099 RID: 4249 RVA: 0x000483B4 File Offset: 0x000465B4
		// (set) Token: 0x0600109A RID: 4250 RVA: 0x000483D4 File Offset: 0x000465D4
		private bool imguiEventIsValid
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.IMGUIEventIsValid) > EventBase.LifeCycleStatus.None;
			}
			set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.IMGUIEventIsValid;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.IMGUIEventIsValid;
				}
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x0600109B RID: 4251 RVA: 0x00048410 File Offset: 0x00046610
		// (set) Token: 0x0600109C RID: 4252 RVA: 0x00048434 File Offset: 0x00046634
		public Event imguiEvent
		{
			get
			{
				return this.imguiEventIsValid ? this.m_ImguiEvent : null;
			}
			protected set
			{
				bool flag = this.m_ImguiEvent == null;
				if (flag)
				{
					this.m_ImguiEvent = new Event();
				}
				bool flag2 = value != null;
				if (flag2)
				{
					this.m_ImguiEvent.CopyFrom(value);
					this.imguiEventIsValid = true;
					this.originalMousePosition = value.mousePosition;
				}
				else
				{
					this.imguiEventIsValid = false;
				}
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x0600109D RID: 4253 RVA: 0x00048494 File Offset: 0x00046694
		// (set) Token: 0x0600109E RID: 4254 RVA: 0x0004849C File Offset: 0x0004669C
		public Vector2 originalMousePosition { get; private set; }

		// Token: 0x0600109F RID: 4255 RVA: 0x000484A5 File Offset: 0x000466A5
		protected virtual void Init()
		{
			this.LocalInit();
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x000484B0 File Offset: 0x000466B0
		private void LocalInit()
		{
			this.timestamp = Panel.TimeSinceStartupMs();
			this.triggerEventId = 0UL;
			ulong num = EventBase.s_NextEventId;
			EventBase.s_NextEventId = num + 1UL;
			this.eventId = num;
			this.propagation = EventBase.EventPropagation.None;
			this.elementTarget = null;
			this.isPropagationStopped = false;
			this.isImmediatePropagationStopped = false;
			this.propagationPhase = PropagationPhase.None;
			this.originalMousePosition = Vector2.zero;
			this.m_CurrentTarget = null;
			this.dispatch = false;
			this.propagateToIMGUI = true;
			this.dispatched = false;
			this.processed = false;
			this.processedByFocusController = false;
			this.imguiEventIsValid = false;
			this.pooled = false;
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x0004855B File Offset: 0x0004675B
		protected EventBase()
			: this(EventCategory.Default)
		{
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x00048566 File Offset: 0x00046766
		internal EventBase(EventCategory category)
		{
			this.eventCategories = 1 << (int)category;
			this.m_ImguiEvent = null;
			this.LocalInit();
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x060010A3 RID: 4259 RVA: 0x0004858C File Offset: 0x0004678C
		// (set) Token: 0x060010A4 RID: 4260 RVA: 0x000485AC File Offset: 0x000467AC
		protected bool pooled
		{
			get
			{
				return (this.lifeCycleStatus & EventBase.LifeCycleStatus.Pooled) > EventBase.LifeCycleStatus.None;
			}
			set
			{
				if (value)
				{
					this.lifeCycleStatus |= EventBase.LifeCycleStatus.Pooled;
				}
				else
				{
					this.lifeCycleStatus &= ~EventBase.LifeCycleStatus.Pooled;
				}
			}
		}

		// Token: 0x060010A5 RID: 4261
		internal abstract void Acquire();

		// Token: 0x060010A6 RID: 4262
		public abstract void Dispose();

		// Token: 0x040009DD RID: 2525
		private static long s_LastTypeId;

		// Token: 0x040009DF RID: 2527
		private static ulong s_NextEventId;

		// Token: 0x040009E7 RID: 2535
		private IEventHandler m_CurrentTarget;

		// Token: 0x040009E8 RID: 2536
		private Event m_ImguiEvent;

		// Token: 0x0200022C RID: 556
		[Flags]
		internal enum EventPropagation
		{
			// Token: 0x040009EB RID: 2539
			None = 0,
			// Token: 0x040009EC RID: 2540
			Bubbles = 1,
			// Token: 0x040009ED RID: 2541
			TricklesDown = 2,
			// Token: 0x040009EE RID: 2542
			SkipDisabledElements = 4,
			// Token: 0x040009EF RID: 2543
			BubblesOrTricklesDown = 3
		}

		// Token: 0x0200022D RID: 557
		[Flags]
		private enum LifeCycleStatus
		{
			// Token: 0x040009F1 RID: 2545
			None = 0,
			// Token: 0x040009F2 RID: 2546
			PropagationStopped = 1,
			// Token: 0x040009F3 RID: 2547
			ImmediatePropagationStopped = 2,
			// Token: 0x040009F4 RID: 2548
			Dispatching = 4,
			// Token: 0x040009F5 RID: 2549
			Pooled = 8,
			// Token: 0x040009F6 RID: 2550
			IMGUIEventIsValid = 16,
			// Token: 0x040009F7 RID: 2551
			PropagateToIMGUI = 32,
			// Token: 0x040009F8 RID: 2552
			Dispatched = 64,
			// Token: 0x040009F9 RID: 2553
			Processed = 128,
			// Token: 0x040009FA RID: 2554
			ProcessedByFocusController = 256
		}
	}
}

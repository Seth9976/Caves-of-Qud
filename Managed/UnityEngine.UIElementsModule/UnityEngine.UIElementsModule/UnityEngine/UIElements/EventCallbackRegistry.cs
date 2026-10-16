using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine.Pool;

namespace UnityEngine.UIElements
{
	// Token: 0x02000239 RID: 569
	internal class EventCallbackRegistry
	{
		// Token: 0x060010E0 RID: 4320 RVA: 0x00048CE4 File Offset: 0x00046EE4
		private static EventCallbackList GetCallbackList(EventCallbackList initializer = null)
		{
			return EventCallbackRegistry.s_ListPool.Get(initializer);
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x00048D01 File Offset: 0x00046F01
		private static void ReleaseCallbackList(EventCallbackList toRelease)
		{
			EventCallbackRegistry.s_ListPool.Release(toRelease);
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x00048D10 File Offset: 0x00046F10
		private ref EventCallbackRegistry.DynamicCallbackList GetDynamicCallbackList(TrickleDown useTrickleDown)
		{
			return ref useTrickleDown == TrickleDown.TrickleDown ? ref this.m_TrickleDownCallbacks : ref this.m_BubbleUpCallbacks;
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x00048D34 File Offset: 0x00046F34
		public void RegisterCallback<TEventType>([NotNull] EventCallback<TEventType> callback, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown, InvokePolicy invokePolicy = InvokePolicy.Default) where TEventType : EventBase<TEventType>, new()
		{
			long num = EventBase<TEventType>.TypeId();
			ref EventCallbackRegistry.DynamicCallbackList dynamicCallbackList = ref this.GetDynamicCallbackList(useTrickleDown);
			EventCallbackList eventCallbackList = dynamicCallbackList.GetCallbackListForReading();
			EventCallbackFunctor<TEventType> eventCallbackFunctor = eventCallbackList.Find(num, callback) as EventCallbackFunctor<TEventType>;
			bool flag = eventCallbackFunctor != null;
			if (flag)
			{
				eventCallbackFunctor.invokePolicy = invokePolicy;
			}
			else
			{
				eventCallbackList = dynamicCallbackList.GetCallbackListForWriting();
				eventCallbackList.Add(EventCallbackFunctor<TEventType>.GetPooled(num, callback, invokePolicy));
			}
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x00048D90 File Offset: 0x00046F90
		public void RegisterCallback<TEventType, TCallbackArgs>([NotNull] EventCallback<TEventType, TCallbackArgs> callback, TCallbackArgs userArgs, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown, InvokePolicy invokePolicy = InvokePolicy.Default) where TEventType : EventBase<TEventType>, new()
		{
			long num = EventBase<TEventType>.TypeId();
			ref EventCallbackRegistry.DynamicCallbackList dynamicCallbackList = ref this.GetDynamicCallbackList(useTrickleDown);
			EventCallbackList eventCallbackList = dynamicCallbackList.GetCallbackListForReading();
			EventCallbackFunctor<TEventType, TCallbackArgs> eventCallbackFunctor = eventCallbackList.Find(num, callback) as EventCallbackFunctor<TEventType, TCallbackArgs>;
			bool flag = eventCallbackFunctor != null;
			if (flag)
			{
				eventCallbackFunctor.invokePolicy = invokePolicy;
				eventCallbackFunctor.userArgs = userArgs;
			}
			else
			{
				eventCallbackList = dynamicCallbackList.GetCallbackListForWriting();
				eventCallbackList.Add(EventCallbackFunctor<TEventType, TCallbackArgs>.GetPooled(num, callback, userArgs, invokePolicy));
			}
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x00048DF8 File Offset: 0x00046FF8
		public bool UnregisterCallback<TEventType>([NotNull] EventCallback<TEventType> callback, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown) where TEventType : EventBase<TEventType>, new()
		{
			return this.GetDynamicCallbackList(useTrickleDown).UnregisterCallback(EventBase<TEventType>.TypeId(), callback);
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x00048E1C File Offset: 0x0004701C
		public bool UnregisterCallback<TEventType, TCallbackArgs>([NotNull] EventCallback<TEventType, TCallbackArgs> callback, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown) where TEventType : EventBase<TEventType>, new()
		{
			return this.GetDynamicCallbackList(useTrickleDown).UnregisterCallback(EventBase<TEventType>.TypeId(), callback);
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x00048E40 File Offset: 0x00047040
		internal void InvokeCallbacks(EventBase evt, PropagationPhase propagationPhase)
		{
			VisualElement visualElement = (VisualElement)evt.currentTarget;
			BaseVisualElementPanel elementPanel = visualElement.elementPanel;
			if (propagationPhase != PropagationPhase.TrickleDown)
			{
				if (propagationPhase != PropagationPhase.BubbleUp)
				{
					throw new ArgumentOutOfRangeException("propagationPhase", "Propagation phases other than TrickleDown and BubbleUp are not supported");
				}
				this.GetDynamicCallbackList(TrickleDown.NoTrickleDown).Invoke(evt, elementPanel, visualElement);
			}
			else
			{
				this.GetDynamicCallbackList(TrickleDown.TrickleDown).Invoke(evt, elementPanel, visualElement);
			}
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x00048EA8 File Offset: 0x000470A8
		public bool HasTrickleDownHandlers()
		{
			return this.m_TrickleDownCallbacks.Count > 0;
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x00048EC8 File Offset: 0x000470C8
		public bool HasBubbleHandlers()
		{
			return this.m_BubbleUpCallbacks.Count > 0;
		}

		// Token: 0x04000A11 RID: 2577
		private static readonly EventCallbackListPool s_ListPool = new EventCallbackListPool();

		// Token: 0x04000A12 RID: 2578
		internal EventCallbackRegistry.DynamicCallbackList m_TrickleDownCallbacks = EventCallbackRegistry.DynamicCallbackList.Create(TrickleDown.TrickleDown);

		// Token: 0x04000A13 RID: 2579
		internal EventCallbackRegistry.DynamicCallbackList m_BubbleUpCallbacks = EventCallbackRegistry.DynamicCallbackList.Create(TrickleDown.NoTrickleDown);

		// Token: 0x0200023A RID: 570
		internal struct DynamicCallbackList
		{
			// Token: 0x17000309 RID: 777
			// (get) Token: 0x060010EC RID: 4332 RVA: 0x00048F15 File Offset: 0x00047115
			public int Count
			{
				get
				{
					return this.m_Callbacks.Count;
				}
			}

			// Token: 0x060010ED RID: 4333 RVA: 0x00048F24 File Offset: 0x00047124
			public static EventCallbackRegistry.DynamicCallbackList Create(TrickleDown useTrickleDown)
			{
				return new EventCallbackRegistry.DynamicCallbackList
				{
					m_UseTrickleDown = useTrickleDown,
					m_Callbacks = EventCallbackList.EmptyList,
					m_TemporaryCallbacks = null,
					m_UnregisteredCallbacksDuringInvoke = null,
					m_IsInvoking = 0
				};
			}

			// Token: 0x060010EE RID: 4334 RVA: 0x00048F6C File Offset: 0x0004716C
			[NotNull]
			public EventCallbackList GetCallbackListForWriting()
			{
				EventCallbackList eventCallbackList;
				if (this.m_IsInvoking != 0)
				{
					if ((eventCallbackList = this.m_TemporaryCallbacks) == null)
					{
						eventCallbackList = (this.m_TemporaryCallbacks = EventCallbackRegistry.GetCallbackList(this.m_Callbacks));
					}
				}
				else
				{
					eventCallbackList = ((this.m_Callbacks != EventCallbackList.EmptyList) ? this.m_Callbacks : (this.m_Callbacks = EventCallbackRegistry.GetCallbackList(null)));
				}
				return eventCallbackList;
			}

			// Token: 0x060010EF RID: 4335 RVA: 0x00048FCC File Offset: 0x000471CC
			[NotNull]
			public readonly EventCallbackList GetCallbackListForReading()
			{
				return this.m_TemporaryCallbacks ?? this.m_Callbacks;
			}

			// Token: 0x060010F0 RID: 4336 RVA: 0x00048FF0 File Offset: 0x000471F0
			public bool UnregisterCallback(long eventTypeId, [NotNull] Delegate callback)
			{
				EventCallbackList callbackListForWriting = this.GetCallbackListForWriting();
				EventCallbackFunctorBase eventCallbackFunctorBase;
				bool flag = !callbackListForWriting.Remove(eventTypeId, callback, out eventCallbackFunctorBase);
				bool flag2;
				if (flag)
				{
					flag2 = false;
				}
				else
				{
					bool flag3 = this.m_IsInvoking > 0;
					if (flag3)
					{
						List<EventCallbackFunctorBase> list;
						if ((list = this.m_UnregisteredCallbacksDuringInvoke) == null)
						{
							list = (this.m_UnregisteredCallbacksDuringInvoke = CollectionPool<List<EventCallbackFunctorBase>, EventCallbackFunctorBase>.Get());
						}
						list.Add(eventCallbackFunctorBase);
					}
					else
					{
						eventCallbackFunctorBase.Dispose();
					}
					flag2 = true;
				}
				return flag2;
			}

			// Token: 0x060010F1 RID: 4337 RVA: 0x00049058 File Offset: 0x00047258
			public unsafe void Invoke(EventBase evt, BaseVisualElementPanel panel, VisualElement target)
			{
				this.BeginInvoke();
				try
				{
					bool flag = !evt.skipDisabledElements || target.enabledInHierarchy;
					long eventTypeId = evt.eventTypeId;
					Span<EventCallbackFunctorBase> span = this.m_Callbacks.Span;
					for (int i = 0; i < span.Length; i++)
					{
						EventCallbackFunctorBase eventCallbackFunctorBase = *span[i];
						bool flag2 = eventCallbackFunctorBase.eventTypeId == eventTypeId && target.elementPanel == panel && (flag || (eventCallbackFunctorBase.invokePolicy & InvokePolicy.IncludeDisabled) > InvokePolicy.Default);
						if (flag2)
						{
							bool flag3 = (eventCallbackFunctorBase.invokePolicy & InvokePolicy.Once) > InvokePolicy.Default;
							if (flag3)
							{
								eventCallbackFunctorBase.UnregisterCallback(target, this.m_UseTrickleDown);
							}
							eventCallbackFunctorBase.Invoke(evt);
							bool isImmediatePropagationStopped = evt.isImmediatePropagationStopped;
							if (isImmediatePropagationStopped)
							{
								break;
							}
						}
					}
				}
				finally
				{
					this.EndInvoke();
				}
			}

			// Token: 0x060010F2 RID: 4338 RVA: 0x0004913C File Offset: 0x0004733C
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private void BeginInvoke()
			{
				this.m_IsInvoking++;
			}

			// Token: 0x060010F3 RID: 4339 RVA: 0x00049150 File Offset: 0x00047350
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private void EndInvoke()
			{
				this.m_IsInvoking--;
				bool flag = this.m_IsInvoking == 0;
				if (flag)
				{
					bool flag2 = this.m_TemporaryCallbacks != null;
					if (flag2)
					{
						bool flag3 = this.m_Callbacks != EventCallbackList.EmptyList;
						if (flag3)
						{
							EventCallbackRegistry.ReleaseCallbackList(this.m_Callbacks);
						}
						this.m_Callbacks = EventCallbackRegistry.GetCallbackList(this.m_TemporaryCallbacks);
						EventCallbackRegistry.ReleaseCallbackList(this.m_TemporaryCallbacks);
						this.m_TemporaryCallbacks = null;
						bool flag4 = this.m_UnregisteredCallbacksDuringInvoke != null;
						if (flag4)
						{
							foreach (EventCallbackFunctorBase eventCallbackFunctorBase in this.m_UnregisteredCallbacksDuringInvoke)
							{
								eventCallbackFunctorBase.Dispose();
							}
							CollectionPool<List<EventCallbackFunctorBase>, EventCallbackFunctorBase>.Release(this.m_UnregisteredCallbacksDuringInvoke);
							this.m_UnregisteredCallbacksDuringInvoke = null;
						}
					}
				}
			}

			// Token: 0x04000A14 RID: 2580
			private TrickleDown m_UseTrickleDown;

			// Token: 0x04000A15 RID: 2581
			[NotNull]
			private EventCallbackList m_Callbacks;

			// Token: 0x04000A16 RID: 2582
			[CanBeNull]
			private EventCallbackList m_TemporaryCallbacks;

			// Token: 0x04000A17 RID: 2583
			[CanBeNull]
			private List<EventCallbackFunctorBase> m_UnregisteredCallbacksDuringInvoke;

			// Token: 0x04000A18 RID: 2584
			private int m_IsInvoking;
		}
	}
}

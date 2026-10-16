using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x0200023E RID: 574
	public abstract class CallbackEventHandler : IEventHandler
	{
		// Token: 0x0600110F RID: 4367 RVA: 0x0004A134 File Offset: 0x00048334
		public void RegisterCallback<TEventType>(EventCallback<TEventType> callback, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown) where TEventType : EventBase<TEventType>, new()
		{
			bool flag = callback == null;
			if (flag)
			{
				throw new ArgumentException("callback parameter is null");
			}
			EventCallbackRegistry eventCallbackRegistry;
			if ((eventCallbackRegistry = this.m_CallbackRegistry) == null)
			{
				eventCallbackRegistry = (this.m_CallbackRegistry = new EventCallbackRegistry());
			}
			eventCallbackRegistry.RegisterCallback<TEventType>(callback, useTrickleDown, InvokePolicy.Default);
			this.AddEventCategories<TEventType>(useTrickleDown);
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x0004A180 File Offset: 0x00048380
		public void RegisterCallbackOnce<TEventType>(EventCallback<TEventType> callback, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown) where TEventType : EventBase<TEventType>, new()
		{
			bool flag = callback == null;
			if (flag)
			{
				throw new ArgumentException("callback parameter is null");
			}
			EventCallbackRegistry eventCallbackRegistry;
			if ((eventCallbackRegistry = this.m_CallbackRegistry) == null)
			{
				eventCallbackRegistry = (this.m_CallbackRegistry = new EventCallbackRegistry());
			}
			eventCallbackRegistry.RegisterCallback<TEventType>(callback, useTrickleDown, InvokePolicy.Once);
			this.AddEventCategories<TEventType>(useTrickleDown);
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x0004A1CC File Offset: 0x000483CC
		private void AddEventCategories<TEventType>(TrickleDown useTrickleDown) where TEventType : EventBase<TEventType>, new()
		{
			VisualElement visualElement = this as VisualElement;
			bool flag = visualElement != null;
			if (flag)
			{
				visualElement.AddEventCallbackCategories(1 << (int)EventBase<TEventType>.EventCategory, useTrickleDown);
			}
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x0004A200 File Offset: 0x00048400
		public void RegisterCallback<TEventType, TUserArgsType>(EventCallback<TEventType, TUserArgsType> callback, TUserArgsType userArgs, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown) where TEventType : EventBase<TEventType>, new()
		{
			bool flag = callback == null;
			if (flag)
			{
				throw new ArgumentException("callback parameter is null");
			}
			EventCallbackRegistry eventCallbackRegistry;
			if ((eventCallbackRegistry = this.m_CallbackRegistry) == null)
			{
				eventCallbackRegistry = (this.m_CallbackRegistry = new EventCallbackRegistry());
			}
			eventCallbackRegistry.RegisterCallback<TEventType, TUserArgsType>(callback, userArgs, useTrickleDown, InvokePolicy.Default);
			this.AddEventCategories<TEventType>(useTrickleDown);
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x0004A24C File Offset: 0x0004844C
		public void RegisterCallbackOnce<TEventType, TUserArgsType>(EventCallback<TEventType, TUserArgsType> callback, TUserArgsType userArgs, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown) where TEventType : EventBase<TEventType>, new()
		{
			bool flag = callback == null;
			if (flag)
			{
				throw new ArgumentException("callback parameter is null");
			}
			EventCallbackRegistry eventCallbackRegistry;
			if ((eventCallbackRegistry = this.m_CallbackRegistry) == null)
			{
				eventCallbackRegistry = (this.m_CallbackRegistry = new EventCallbackRegistry());
			}
			eventCallbackRegistry.RegisterCallback<TEventType, TUserArgsType>(callback, userArgs, useTrickleDown, InvokePolicy.Once);
			this.AddEventCategories<TEventType>(useTrickleDown);
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x0004A298 File Offset: 0x00048498
		internal void RegisterCallback<TEventType>(EventCallback<TEventType> callback, InvokePolicy invokePolicy, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown) where TEventType : EventBase<TEventType>, new()
		{
			EventCallbackRegistry eventCallbackRegistry;
			if ((eventCallbackRegistry = this.m_CallbackRegistry) == null)
			{
				eventCallbackRegistry = (this.m_CallbackRegistry = new EventCallbackRegistry());
			}
			eventCallbackRegistry.RegisterCallback<TEventType>(callback, useTrickleDown, invokePolicy);
			this.AddEventCategories<TEventType>(useTrickleDown);
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x0004A2D0 File Offset: 0x000484D0
		public void UnregisterCallback<TEventType>(EventCallback<TEventType> callback, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown) where TEventType : EventBase<TEventType>, new()
		{
			bool flag = callback == null;
			if (flag)
			{
				throw new ArgumentException("callback parameter is null");
			}
			EventCallbackRegistry callbackRegistry = this.m_CallbackRegistry;
			if (callbackRegistry != null)
			{
				callbackRegistry.UnregisterCallback<TEventType>(callback, useTrickleDown);
			}
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x0004A308 File Offset: 0x00048508
		public void UnregisterCallback<TEventType, TUserArgsType>(EventCallback<TEventType, TUserArgsType> callback, TrickleDown useTrickleDown = TrickleDown.NoTrickleDown) where TEventType : EventBase<TEventType>, new()
		{
			bool flag = callback == null;
			if (flag)
			{
				throw new ArgumentException("callback parameter is null");
			}
			EventCallbackRegistry callbackRegistry = this.m_CallbackRegistry;
			if (callbackRegistry != null)
			{
				callbackRegistry.UnregisterCallback<TEventType, TUserArgsType>(callback, useTrickleDown);
			}
		}

		// Token: 0x06001117 RID: 4375
		public abstract void SendEvent(EventBase e);

		// Token: 0x06001118 RID: 4376
		internal abstract void SendEvent(EventBase e, DispatchMode dispatchMode);

		// Token: 0x06001119 RID: 4377
		internal abstract void HandleEvent(EventBase e);

		// Token: 0x0600111A RID: 4378 RVA: 0x0004A340 File Offset: 0x00048540
		void IEventHandler.HandleEvent(EventBase evt)
		{
			bool flag = evt == null;
			if (!flag)
			{
				this.HandleEvent(evt);
			}
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x0004A360 File Offset: 0x00048560
		public bool HasTrickleDownHandlers()
		{
			return this.m_CallbackRegistry != null && this.m_CallbackRegistry.HasTrickleDownHandlers();
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x0004A388 File Offset: 0x00048588
		public bool HasBubbleUpHandlers()
		{
			return this.m_CallbackRegistry != null && this.m_CallbackRegistry.HasBubbleHandlers();
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x00002152 File Offset: 0x00000352
		[EventInterest(EventInterestOptions.Inherit)]
		[Obsolete("Use HandleEventBubbleUp. Before proceeding, make sure you understand the latest changes to UIToolkit event propagation rules by visiting Unity's manual page https://docs.unity3d.com/Manual/UIE-Events-Dispatching.html")]
		protected virtual void ExecuteDefaultActionAtTarget(EventBase evt)
		{
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x00002152 File Offset: 0x00000352
		[EventInterest(EventInterestOptions.Inherit)]
		protected virtual void HandleEventBubbleUp(EventBase evt)
		{
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x00002152 File Offset: 0x00000352
		[EventInterest(EventInterestOptions.Inherit)]
		internal virtual void HandleEventBubbleUpDisabled(EventBase evt)
		{
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x0004A3B0 File Offset: 0x000485B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void HandleEventBubbleUpInternal(EventBase evt)
		{
			this.HandleEventBubbleUp(evt);
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x00002152 File Offset: 0x00000352
		[EventInterest(EventInterestOptions.Inherit)]
		protected virtual void HandleEventTrickleDown(EventBase evt)
		{
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x00002152 File Offset: 0x00000352
		[EventInterest(EventInterestOptions.Inherit)]
		internal virtual void HandleEventTrickleDownDisabled(EventBase evt)
		{
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x0004A3BA File Offset: 0x000485BA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void HandleEventTrickleDownInternal(EventBase evt)
		{
			this.HandleEventTrickleDown(evt);
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x00002152 File Offset: 0x00000352
		[Obsolete("Use HandleEventBubbleUp. Before proceeding, make sure you understand the latest changes to UIToolkit event propagation rules by visiting Unity's manual page https://docs.unity3d.com/Manual/UIE-Events-Dispatching.html")]
		[EventInterest(EventInterestOptions.Inherit)]
		protected virtual void ExecuteDefaultAction(EventBase evt)
		{
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x00002152 File Offset: 0x00000352
		[EventInterest(EventInterestOptions.Inherit)]
		[Obsolete("Use HandleEventBubbleUpDisabled.")]
		internal virtual void ExecuteDefaultActionDisabledAtTarget(EventBase evt)
		{
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x00002152 File Offset: 0x00000352
		[EventInterest(EventInterestOptions.Inherit)]
		[Obsolete("Use HandleEventBubbleUpDisabled.")]
		internal virtual void ExecuteDefaultActionDisabled(EventBase evt)
		{
		}

		// Token: 0x06001127 RID: 4391 RVA: 0x0004A3C4 File Offset: 0x000485C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void ExecuteDefaultActionInternal(EventBase evt)
		{
			this.ExecuteDefaultAction(evt);
		}

		// Token: 0x06001128 RID: 4392 RVA: 0x0004A3CE File Offset: 0x000485CE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void ExecuteDefaultActionDisabledInternal(EventBase evt)
		{
			this.ExecuteDefaultActionDisabled(evt);
		}

		// Token: 0x06001129 RID: 4393 RVA: 0x0004A3D8 File Offset: 0x000485D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void ExecuteDefaultActionAtTargetInternal(EventBase evt)
		{
			this.ExecuteDefaultActionAtTarget(evt);
		}

		// Token: 0x0600112A RID: 4394 RVA: 0x0004A3E2 File Offset: 0x000485E2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void ExecuteDefaultActionDisabledAtTargetInternal(EventBase evt)
		{
			this.ExecuteDefaultActionDisabledAtTarget(evt);
		}

		// Token: 0x0600112B RID: 4395 RVA: 0x0004A3EC File Offset: 0x000485EC
		protected void NotifyPropertyChanged(in BindingId property)
		{
			VisualElement visualElement = this as VisualElement;
			bool flag = ((visualElement != null) ? visualElement.elementPanel : null) == null;
			if (!flag)
			{
				using (PropertyChangedEvent pooled = PropertyChangedEvent.GetPooled(in property))
				{
					pooled.target = this;
					this.SendEvent(pooled);
				}
			}
		}

		// Token: 0x04000A20 RID: 2592
		internal bool isIMGUIContainer = false;

		// Token: 0x04000A21 RID: 2593
		internal EventCallbackRegistry m_CallbackRegistry;

		// Token: 0x04000A22 RID: 2594
		internal const string HandleEventBubbleUpName = "HandleEventBubbleUp";

		// Token: 0x04000A23 RID: 2595
		internal const string HandleEventTrickleDownName = "HandleEventTrickleDown";

		// Token: 0x04000A24 RID: 2596
		internal const string ExecuteDefaultActionName = "ExecuteDefaultAction";

		// Token: 0x04000A25 RID: 2597
		internal const string ExecuteDefaultActionAtTargetName = "ExecuteDefaultActionAtTarget";
	}
}

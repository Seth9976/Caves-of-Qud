using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200007E RID: 126
	public class ContextualMenuManipulator : PointerManipulator
	{
		// Token: 0x060004BB RID: 1211 RVA: 0x00016654 File Offset: 0x00014854
		public ContextualMenuManipulator(Action<ContextualMenuPopulateEvent> menuBuilder)
		{
			this.m_MenuBuilder = menuBuilder;
			base.activators.Add(new ManipulatorActivationFilter
			{
				button = MouseButton.RightMouse
			});
			bool flag = this.IsOSXContextualMenuPlatform();
			if (flag)
			{
				base.activators.Add(new ManipulatorActivationFilter
				{
					button = MouseButton.LeftMouse,
					modifiers = EventModifiers.Control
				});
			}
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x000166C4 File Offset: 0x000148C4
		protected override void RegisterCallbacksOnTarget()
		{
			bool flag = this.IsOSXContextualMenuPlatform();
			if (flag)
			{
				base.target.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDownEventOSX), TrickleDown.NoTrickleDown);
				base.target.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUpEventOSX), TrickleDown.NoTrickleDown);
				base.target.RegisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMoveEventOSX), TrickleDown.NoTrickleDown);
			}
			else
			{
				base.target.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUpEvent), TrickleDown.NoTrickleDown);
				base.target.RegisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMoveEvent), TrickleDown.NoTrickleDown);
			}
			base.target.RegisterCallback<KeyUpEvent>(new EventCallback<KeyUpEvent>(this.OnKeyUpEvent), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<ContextualMenuPopulateEvent>(new EventCallback<ContextualMenuPopulateEvent>(this.OnContextualMenuEvent), TrickleDown.NoTrickleDown);
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x00016794 File Offset: 0x00014994
		protected override void UnregisterCallbacksFromTarget()
		{
			bool flag = this.IsOSXContextualMenuPlatform();
			if (flag)
			{
				base.target.UnregisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDownEventOSX), TrickleDown.NoTrickleDown);
				base.target.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUpEventOSX), TrickleDown.NoTrickleDown);
				base.target.UnregisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMoveEventOSX), TrickleDown.NoTrickleDown);
			}
			else
			{
				base.target.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUpEvent), TrickleDown.NoTrickleDown);
				base.target.UnregisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMoveEvent), TrickleDown.NoTrickleDown);
			}
			base.target.UnregisterCallback<KeyUpEvent>(new EventCallback<KeyUpEvent>(this.OnKeyUpEvent), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<ContextualMenuPopulateEvent>(new EventCallback<ContextualMenuPopulateEvent>(this.OnContextualMenuEvent), TrickleDown.NoTrickleDown);
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x00016864 File Offset: 0x00014A64
		protected bool IsOSXContextualMenuPlatform()
		{
			return UIElementsUtility.isOSXContextualMenuPlatform;
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0001687B File Offset: 0x00014A7B
		private void OnPointerUpEvent(IPointerEvent evt)
		{
			this.ProcessPointerEvent(evt);
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x00016888 File Offset: 0x00014A88
		private void OnPointerDownEventOSX(IPointerEvent evt)
		{
			BaseVisualElementPanel elementPanel = base.target.elementPanel;
			bool flag = ((elementPanel != null) ? elementPanel.contextualMenuManager : null) != null;
			if (flag)
			{
				base.target.elementPanel.contextualMenuManager.displayMenuHandledOSX = false;
			}
			this.ProcessPointerEvent(evt);
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x000168D4 File Offset: 0x00014AD4
		private void OnPointerUpEventOSX(IPointerEvent evt)
		{
			BaseVisualElementPanel elementPanel = base.target.elementPanel;
			bool flag = ((elementPanel != null) ? elementPanel.contextualMenuManager : null) != null && base.target.elementPanel.contextualMenuManager.displayMenuHandledOSX;
			if (!flag)
			{
				this.ProcessPointerEvent(evt);
			}
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x00016924 File Offset: 0x00014B24
		private void OnPointerMoveEvent(PointerMoveEvent evt)
		{
			bool isPointerUp = evt.isPointerUp;
			if (isPointerUp)
			{
				this.OnPointerUpEvent(evt);
			}
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00016944 File Offset: 0x00014B44
		private void OnPointerMoveEventOSX(PointerMoveEvent evt)
		{
			bool isPointerUp = evt.isPointerUp;
			if (isPointerUp)
			{
				this.OnPointerUpEventOSX(evt);
			}
			else
			{
				bool isPointerDown = evt.isPointerDown;
				if (isPointerDown)
				{
					this.OnPointerDownEventOSX(evt);
				}
			}
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00016978 File Offset: 0x00014B78
		private void ProcessPointerEvent(IPointerEvent evt)
		{
			bool flag = base.CanStartManipulation(evt);
			if (flag)
			{
				this.DoDisplayMenu(evt as EventBase);
			}
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x000169A0 File Offset: 0x00014BA0
		private void OnKeyUpEvent(KeyUpEvent evt)
		{
			bool flag = evt.keyCode == KeyCode.Menu;
			if (flag)
			{
				this.DoDisplayMenu(evt);
			}
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x000169CC File Offset: 0x00014BCC
		private void DoDisplayMenu(EventBase evt)
		{
			BaseVisualElementPanel elementPanel = base.target.elementPanel;
			bool flag = ((elementPanel != null) ? elementPanel.contextualMenuManager : null) != null;
			if (flag)
			{
				base.target.elementPanel.contextualMenuManager.DisplayMenu(evt, base.target);
				evt.StopPropagation();
			}
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00016A1E File Offset: 0x00014C1E
		private void OnContextualMenuEvent(ContextualMenuPopulateEvent evt)
		{
			Action<ContextualMenuPopulateEvent> menuBuilder = this.m_MenuBuilder;
			if (menuBuilder != null)
			{
				menuBuilder(evt);
			}
		}

		// Token: 0x04000253 RID: 595
		private Action<ContextualMenuPopulateEvent> m_MenuBuilder;
	}
}

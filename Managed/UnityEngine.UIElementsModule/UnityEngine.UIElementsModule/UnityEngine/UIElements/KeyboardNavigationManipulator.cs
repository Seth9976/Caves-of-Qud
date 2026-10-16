using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020002E5 RID: 741
	public class KeyboardNavigationManipulator : Manipulator
	{
		// Token: 0x06001528 RID: 5416 RVA: 0x00054FBB File Offset: 0x000531BB
		public KeyboardNavigationManipulator(Action<KeyboardNavigationOperation, EventBase> action)
		{
			this.m_Action = action;
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x00054FCC File Offset: 0x000531CC
		protected override void RegisterCallbacksOnTarget()
		{
			base.target.RegisterCallback<NavigationMoveEvent>(new EventCallback<NavigationMoveEvent>(this.OnNavigationMove), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<NavigationSubmitEvent>(new EventCallback<NavigationSubmitEvent>(this.OnNavigationSubmit), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<NavigationCancelEvent>(new EventCallback<NavigationCancelEvent>(this.OnNavigationCancel), TrickleDown.NoTrickleDown);
			base.target.RegisterCallback<KeyDownEvent>(new EventCallback<KeyDownEvent>(this.OnKeyDown), TrickleDown.NoTrickleDown);
		}

		// Token: 0x0600152A RID: 5418 RVA: 0x00055040 File Offset: 0x00053240
		protected override void UnregisterCallbacksFromTarget()
		{
			base.target.UnregisterCallback<NavigationMoveEvent>(new EventCallback<NavigationMoveEvent>(this.OnNavigationMove), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<NavigationSubmitEvent>(new EventCallback<NavigationSubmitEvent>(this.OnNavigationSubmit), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<NavigationCancelEvent>(new EventCallback<NavigationCancelEvent>(this.OnNavigationCancel), TrickleDown.NoTrickleDown);
			base.target.UnregisterCallback<KeyDownEvent>(new EventCallback<KeyDownEvent>(this.OnKeyDown), TrickleDown.NoTrickleDown);
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x000550B4 File Offset: 0x000532B4
		internal void OnKeyDown(KeyDownEvent evt)
		{
			KeyboardNavigationManipulator.<>c__DisplayClass4_0 CS$<>8__locals1;
			CS$<>8__locals1.evt = evt;
			KeyboardNavigationOperation keyboardNavigationOperation = KeyboardNavigationManipulator.<OnKeyDown>g__GetOperation|4_0(ref CS$<>8__locals1);
			bool flag = keyboardNavigationOperation > KeyboardNavigationOperation.None;
			if (flag)
			{
				this.Invoke(keyboardNavigationOperation, CS$<>8__locals1.evt);
			}
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x000550EB File Offset: 0x000532EB
		private void OnNavigationCancel(NavigationCancelEvent evt)
		{
			this.Invoke(KeyboardNavigationOperation.Cancel, evt);
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x000550F7 File Offset: 0x000532F7
		private void OnNavigationSubmit(NavigationSubmitEvent evt)
		{
			this.Invoke(KeyboardNavigationOperation.Submit, evt);
		}

		// Token: 0x0600152E RID: 5422 RVA: 0x00055104 File Offset: 0x00053304
		private void OnNavigationMove(NavigationMoveEvent evt)
		{
			switch (evt.direction)
			{
			case NavigationMoveEvent.Direction.Left:
				this.Invoke(KeyboardNavigationOperation.MoveLeft, evt);
				break;
			case NavigationMoveEvent.Direction.Up:
				this.Invoke(KeyboardNavigationOperation.Previous, evt);
				break;
			case NavigationMoveEvent.Direction.Right:
				this.Invoke(KeyboardNavigationOperation.MoveRight, evt);
				break;
			case NavigationMoveEvent.Direction.Down:
				this.Invoke(KeyboardNavigationOperation.Next, evt);
				break;
			}
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x00055161 File Offset: 0x00053361
		private void Invoke(KeyboardNavigationOperation operation, EventBase evt)
		{
			Action<KeyboardNavigationOperation, EventBase> action = this.m_Action;
			if (action != null)
			{
				action(operation, evt);
			}
		}

		// Token: 0x06001530 RID: 5424 RVA: 0x00055178 File Offset: 0x00053378
		[CompilerGenerated]
		internal static KeyboardNavigationOperation <OnKeyDown>g__GetOperation|4_0(ref KeyboardNavigationManipulator.<>c__DisplayClass4_0 A_0)
		{
			KeyCode keyCode = A_0.evt.keyCode;
			KeyCode keyCode2 = keyCode;
			if (keyCode2 != KeyCode.A)
			{
				switch (keyCode2)
				{
				case KeyCode.UpArrow:
				case KeyCode.DownArrow:
				case KeyCode.RightArrow:
				case KeyCode.LeftArrow:
					A_0.evt.StopPropagation();
					break;
				case KeyCode.Home:
					return KeyboardNavigationOperation.Begin;
				case KeyCode.End:
					return KeyboardNavigationOperation.End;
				case KeyCode.PageUp:
					return KeyboardNavigationOperation.PageUp;
				case KeyCode.PageDown:
					return KeyboardNavigationOperation.PageDown;
				}
			}
			else if (A_0.evt.actionKey)
			{
				return KeyboardNavigationOperation.SelectAll;
			}
			return KeyboardNavigationOperation.None;
		}

		// Token: 0x04000B7E RID: 2942
		private readonly Action<KeyboardNavigationOperation, EventBase> m_Action;
	}
}

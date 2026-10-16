using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000276 RID: 630
	public class NavigationMoveEvent : NavigationEventBase<NavigationMoveEvent>
	{
		// Token: 0x06001264 RID: 4708 RVA: 0x0004C52F File Offset: 0x0004A72F
		static NavigationMoveEvent()
		{
			EventBase<NavigationMoveEvent>.SetCreateFunction(() => new NavigationMoveEvent());
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x0004C548 File Offset: 0x0004A748
		internal static NavigationMoveEvent.Direction DetermineMoveDirection(float x, float y, float deadZone = 0.6f)
		{
			bool flag = new Vector2(x, y).sqrMagnitude < deadZone * deadZone;
			NavigationMoveEvent.Direction direction;
			if (flag)
			{
				direction = NavigationMoveEvent.Direction.None;
			}
			else
			{
				bool flag2 = Mathf.Abs(x) > Mathf.Abs(y);
				if (flag2)
				{
					bool flag3 = x > 0f;
					if (flag3)
					{
						direction = NavigationMoveEvent.Direction.Right;
					}
					else
					{
						direction = NavigationMoveEvent.Direction.Left;
					}
				}
				else
				{
					bool flag4 = y > 0f;
					if (flag4)
					{
						direction = NavigationMoveEvent.Direction.Up;
					}
					else
					{
						direction = NavigationMoveEvent.Direction.Down;
					}
				}
			}
			return direction;
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06001266 RID: 4710 RVA: 0x0004C5B3 File Offset: 0x0004A7B3
		// (set) Token: 0x06001267 RID: 4711 RVA: 0x0004C5BB File Offset: 0x0004A7BB
		public NavigationMoveEvent.Direction direction { get; private set; }

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06001268 RID: 4712 RVA: 0x0004C5C4 File Offset: 0x0004A7C4
		// (set) Token: 0x06001269 RID: 4713 RVA: 0x0004C5CC File Offset: 0x0004A7CC
		public Vector2 move { get; private set; }

		// Token: 0x0600126A RID: 4714 RVA: 0x0004C5D8 File Offset: 0x0004A7D8
		public static NavigationMoveEvent GetPooled(Vector2 moveVector, EventModifiers modifiers = EventModifiers.None)
		{
			NavigationMoveEvent pooled = NavigationEventBase<NavigationMoveEvent>.GetPooled(NavigationDeviceType.Unknown, modifiers);
			pooled.direction = NavigationMoveEvent.DetermineMoveDirection(moveVector.x, moveVector.y, 0.6f);
			pooled.move = moveVector;
			return pooled;
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x0004C618 File Offset: 0x0004A818
		internal static NavigationMoveEvent GetPooled(Vector2 moveVector, NavigationDeviceType deviceType, EventModifiers modifiers = EventModifiers.None)
		{
			NavigationMoveEvent pooled = NavigationEventBase<NavigationMoveEvent>.GetPooled(deviceType, modifiers);
			pooled.direction = NavigationMoveEvent.DetermineMoveDirection(moveVector.x, moveVector.y, 0.6f);
			pooled.move = moveVector;
			return pooled;
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x0004C658 File Offset: 0x0004A858
		public static NavigationMoveEvent GetPooled(NavigationMoveEvent.Direction direction, EventModifiers modifiers = EventModifiers.None)
		{
			NavigationMoveEvent pooled = NavigationEventBase<NavigationMoveEvent>.GetPooled(NavigationDeviceType.Unknown, modifiers);
			pooled.direction = direction;
			pooled.move = Vector2.zero;
			return pooled;
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x0004C688 File Offset: 0x0004A888
		internal static NavigationMoveEvent GetPooled(NavigationMoveEvent.Direction direction, NavigationDeviceType deviceType, EventModifiers modifiers = EventModifiers.None)
		{
			NavigationMoveEvent pooled = NavigationEventBase<NavigationMoveEvent>.GetPooled(deviceType, modifiers);
			pooled.direction = direction;
			pooled.move = Vector2.zero;
			return pooled;
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x0004C6B7 File Offset: 0x0004A8B7
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x0004C6C8 File Offset: 0x0004A8C8
		public NavigationMoveEvent()
		{
			this.LocalInit();
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x0004C6D9 File Offset: 0x0004A8D9
		private void LocalInit()
		{
			this.direction = NavigationMoveEvent.Direction.None;
			this.move = Vector2.zero;
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x0004C6F0 File Offset: 0x0004A8F0
		protected internal override void PostDispatch(IPanel panel)
		{
			panel.focusController.SwitchFocusOnEvent(panel.focusController.GetLeafFocusedElement(), this);
			base.PostDispatch(panel);
		}

		// Token: 0x02000277 RID: 631
		public enum Direction
		{
			// Token: 0x04000A5F RID: 2655
			None,
			// Token: 0x04000A60 RID: 2656
			Left,
			// Token: 0x04000A61 RID: 2657
			Up,
			// Token: 0x04000A62 RID: 2658
			Right,
			// Token: 0x04000A63 RID: 2659
			Down,
			// Token: 0x04000A64 RID: 2660
			Next,
			// Token: 0x04000A65 RID: 2661
			Previous
		}
	}
}

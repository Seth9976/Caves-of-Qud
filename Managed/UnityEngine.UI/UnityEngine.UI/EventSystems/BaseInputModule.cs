using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace UnityEngine.EventSystems
{
	// Token: 0x0200006A RID: 106
	[RequireComponent(typeof(EventSystem))]
	public abstract class BaseInputModule : UIBehaviour
	{
		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x0001988B File Offset: 0x00017A8B
		// (set) Token: 0x06000613 RID: 1555 RVA: 0x00019893 File Offset: 0x00017A93
		protected internal bool sendPointerHoverToParent
		{
			get
			{
				return this.m_SendPointerHoverToParent;
			}
			set
			{
				this.m_SendPointerHoverToParent = value;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x0001989C File Offset: 0x00017A9C
		public BaseInput input
		{
			get
			{
				if (this.m_InputOverride != null)
				{
					return this.m_InputOverride;
				}
				if (this.m_DefaultInput == null)
				{
					foreach (BaseInput baseInput in base.GetComponents<BaseInput>())
					{
						if (baseInput != null && baseInput.GetType() == typeof(BaseInput))
						{
							this.m_DefaultInput = baseInput;
							break;
						}
					}
					if (this.m_DefaultInput == null)
					{
						this.m_DefaultInput = base.gameObject.AddComponent<BaseInput>();
					}
				}
				return this.m_DefaultInput;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x00019933 File Offset: 0x00017B33
		// (set) Token: 0x06000616 RID: 1558 RVA: 0x0001993B File Offset: 0x00017B3B
		public BaseInput inputOverride
		{
			get
			{
				return this.m_InputOverride;
			}
			set
			{
				this.m_InputOverride = value;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x00019944 File Offset: 0x00017B44
		protected EventSystem eventSystem
		{
			get
			{
				return this.m_EventSystem;
			}
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x0001994C File Offset: 0x00017B4C
		protected override void OnEnable()
		{
			base.OnEnable();
			this.m_EventSystem = base.GetComponent<EventSystem>();
			this.m_EventSystem.UpdateModules();
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0001996B File Offset: 0x00017B6B
		protected override void OnDisable()
		{
			this.m_EventSystem.UpdateModules();
			base.OnDisable();
		}

		// Token: 0x0600061A RID: 1562
		public abstract void Process();

		// Token: 0x0600061B RID: 1563 RVA: 0x00019980 File Offset: 0x00017B80
		protected static RaycastResult FindFirstRaycast(List<RaycastResult> candidates)
		{
			int count = candidates.Count;
			for (int i = 0; i < count; i++)
			{
				if (!(candidates[i].gameObject == null))
				{
					return candidates[i];
				}
			}
			return default(RaycastResult);
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x000199C8 File Offset: 0x00017BC8
		protected static MoveDirection DetermineMoveDirection(float x, float y)
		{
			return BaseInputModule.DetermineMoveDirection(x, y, 0.6f);
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x000199D8 File Offset: 0x00017BD8
		protected static MoveDirection DetermineMoveDirection(float x, float y, float deadZone)
		{
			if (new Vector2(x, y).sqrMagnitude < deadZone * deadZone)
			{
				return MoveDirection.None;
			}
			if (Mathf.Abs(x) > Mathf.Abs(y))
			{
				if (x <= 0f)
				{
					return MoveDirection.Left;
				}
				return MoveDirection.Right;
			}
			else
			{
				if (y <= 0f)
				{
					return MoveDirection.Down;
				}
				return MoveDirection.Up;
			}
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x00019A20 File Offset: 0x00017C20
		protected static GameObject FindCommonRoot(GameObject g1, GameObject g2)
		{
			if (g1 == null || g2 == null)
			{
				return null;
			}
			Transform transform = g1.transform;
			while (transform != null)
			{
				Transform transform2 = g2.transform;
				while (transform2 != null)
				{
					if (transform == transform2)
					{
						return transform.gameObject;
					}
					transform2 = transform2.parent;
				}
				transform = transform.parent;
			}
			return null;
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x00019A84 File Offset: 0x00017C84
		protected void HandlePointerExitAndEnter(PointerEventData currentPointerData, GameObject newEnterTarget)
		{
			if (newEnterTarget == null || currentPointerData.pointerEnter == null)
			{
				int count = currentPointerData.hovered.Count;
				for (int i = 0; i < count; i++)
				{
					currentPointerData.fullyExited = true;
					ExecuteEvents.Execute<IPointerMoveHandler>(currentPointerData.hovered[i], currentPointerData, ExecuteEvents.pointerMoveHandler);
					ExecuteEvents.Execute<IPointerExitHandler>(currentPointerData.hovered[i], currentPointerData, ExecuteEvents.pointerExitHandler);
				}
				currentPointerData.hovered.Clear();
				if (newEnterTarget == null)
				{
					currentPointerData.pointerEnter = null;
					return;
				}
			}
			if (currentPointerData.pointerEnter == newEnterTarget && newEnterTarget)
			{
				if (currentPointerData.IsPointerMoving())
				{
					int count2 = currentPointerData.hovered.Count;
					for (int j = 0; j < count2; j++)
					{
						ExecuteEvents.Execute<IPointerMoveHandler>(currentPointerData.hovered[j], currentPointerData, ExecuteEvents.pointerMoveHandler);
					}
				}
				return;
			}
			GameObject gameObject = BaseInputModule.FindCommonRoot(currentPointerData.pointerEnter, newEnterTarget);
			Component component = (Component)newEnterTarget.GetComponentInParent<IPointerExitHandler>();
			GameObject gameObject2 = ((component != null) ? component.gameObject : null);
			if (currentPointerData.pointerEnter != null)
			{
				Transform transform = currentPointerData.pointerEnter.transform;
				while (transform != null && (!this.m_SendPointerHoverToParent || !(gameObject != null) || !(gameObject.transform == transform)) && (this.m_SendPointerHoverToParent || !(gameObject2 == transform.gameObject)))
				{
					currentPointerData.fullyExited = transform.gameObject != gameObject && currentPointerData.pointerEnter != newEnterTarget;
					ExecuteEvents.Execute<IPointerMoveHandler>(transform.gameObject, currentPointerData, ExecuteEvents.pointerMoveHandler);
					ExecuteEvents.Execute<IPointerExitHandler>(transform.gameObject, currentPointerData, ExecuteEvents.pointerExitHandler);
					currentPointerData.hovered.Remove(transform.gameObject);
					if (this.m_SendPointerHoverToParent)
					{
						transform = transform.parent;
					}
					if (gameObject != null && gameObject.transform == transform)
					{
						break;
					}
					if (!this.m_SendPointerHoverToParent)
					{
						transform = transform.parent;
					}
				}
			}
			GameObject pointerEnter = currentPointerData.pointerEnter;
			currentPointerData.pointerEnter = newEnterTarget;
			if (newEnterTarget != null)
			{
				Transform transform2 = newEnterTarget.transform;
				while (transform2 != null)
				{
					currentPointerData.reentered = transform2.gameObject == gameObject && transform2.gameObject != pointerEnter;
					if (this.m_SendPointerHoverToParent && currentPointerData.reentered)
					{
						break;
					}
					ExecuteEvents.Execute<IPointerEnterHandler>(transform2.gameObject, currentPointerData, ExecuteEvents.pointerEnterHandler);
					ExecuteEvents.Execute<IPointerMoveHandler>(transform2.gameObject, currentPointerData, ExecuteEvents.pointerMoveHandler);
					currentPointerData.hovered.Add(transform2.gameObject);
					if (!this.m_SendPointerHoverToParent && transform2.gameObject.GetComponent<IPointerEnterHandler>() != null)
					{
						break;
					}
					if (this.m_SendPointerHoverToParent)
					{
						transform2 = transform2.parent;
					}
					if (gameObject != null && gameObject.transform == transform2)
					{
						break;
					}
					if (!this.m_SendPointerHoverToParent)
					{
						transform2 = transform2.parent;
					}
				}
			}
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x00019D8C File Offset: 0x00017F8C
		protected virtual AxisEventData GetAxisEventData(float x, float y, float moveDeadZone)
		{
			if (this.m_AxisEventData == null)
			{
				this.m_AxisEventData = new AxisEventData(this.eventSystem);
			}
			this.m_AxisEventData.Reset();
			this.m_AxisEventData.moveVector = new Vector2(x, y);
			this.m_AxisEventData.moveDir = BaseInputModule.DetermineMoveDirection(x, y, moveDeadZone);
			return this.m_AxisEventData;
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x00019DE8 File Offset: 0x00017FE8
		protected virtual BaseEventData GetBaseEventData()
		{
			if (this.m_BaseEventData == null)
			{
				this.m_BaseEventData = new BaseEventData(this.eventSystem);
			}
			this.m_BaseEventData.Reset();
			return this.m_BaseEventData;
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00019E14 File Offset: 0x00018014
		public virtual bool IsPointerOverGameObject(int pointerId)
		{
			return false;
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00019E17 File Offset: 0x00018017
		public virtual bool ShouldActivateModule()
		{
			return base.enabled && base.gameObject.activeInHierarchy;
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00019E2E File Offset: 0x0001802E
		public virtual void DeactivateModule()
		{
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00019E30 File Offset: 0x00018030
		public virtual void ActivateModule()
		{
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00019E32 File Offset: 0x00018032
		public virtual void UpdateModule()
		{
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00019E34 File Offset: 0x00018034
		public virtual bool IsModuleSupported()
		{
			return true;
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00019E37 File Offset: 0x00018037
		public virtual int ConvertUIToolkitPointerId(PointerEventData sourcePointerData)
		{
			if (sourcePointerData.pointerId >= 0)
			{
				return PointerId.touchPointerIdBase + sourcePointerData.pointerId;
			}
			return PointerId.mousePointerId;
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x00019E54 File Offset: 0x00018054
		public virtual Vector2 ConvertPointerEventScrollDeltaToTicks(Vector2 scrollDelta)
		{
			return scrollDelta / this.input.mouseScrollDeltaPerTick;
		}

		// Token: 0x04000209 RID: 521
		[NonSerialized]
		protected List<RaycastResult> m_RaycastResultCache = new List<RaycastResult>();

		// Token: 0x0400020A RID: 522
		[SerializeField]
		private bool m_SendPointerHoverToParent = true;

		// Token: 0x0400020B RID: 523
		private AxisEventData m_AxisEventData;

		// Token: 0x0400020C RID: 524
		private EventSystem m_EventSystem;

		// Token: 0x0400020D RID: 525
		private BaseEventData m_BaseEventData;

		// Token: 0x0400020E RID: 526
		protected BaseInput m_InputOverride;

		// Token: 0x0400020F RID: 527
		private BaseInput m_DefaultInput;
	}
}

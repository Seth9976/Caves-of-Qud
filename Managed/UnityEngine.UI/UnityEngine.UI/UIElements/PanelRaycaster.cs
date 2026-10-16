using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UnityEngine.UIElements
{
	// Token: 0x0200004C RID: 76
	[AddComponentMenu("UI Toolkit/Panel Raycaster (UI Toolkit)")]
	public class PanelRaycaster : BaseRaycaster, IRuntimePanelComponent
	{
		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x00017EB5 File Offset: 0x000160B5
		// (set) Token: 0x06000525 RID: 1317 RVA: 0x00017EC0 File Offset: 0x000160C0
		public IPanel panel
		{
			get
			{
				return this.m_Panel;
			}
			set
			{
				BaseRuntimePanel baseRuntimePanel = (BaseRuntimePanel)value;
				if (this.m_Panel != baseRuntimePanel)
				{
					this.UnregisterCallbacks();
					this.m_Panel = baseRuntimePanel;
					this.RegisterCallbacks();
				}
			}
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x00017EF0 File Offset: 0x000160F0
		private void RegisterCallbacks()
		{
			if (this.m_Panel != null)
			{
				this.m_Panel.destroyed += this.OnPanelDestroyed;
			}
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x00017F11 File Offset: 0x00016111
		private void UnregisterCallbacks()
		{
			if (this.m_Panel != null)
			{
				this.m_Panel.destroyed -= this.OnPanelDestroyed;
			}
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00017F32 File Offset: 0x00016132
		private void OnPanelDestroyed()
		{
			this.panel = null;
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x00017F3B File Offset: 0x0001613B
		private GameObject selectableGameObject
		{
			get
			{
				BaseRuntimePanel panel = this.m_Panel;
				if (panel == null)
				{
					return null;
				}
				return panel.selectableGameObject;
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x00017F4E File Offset: 0x0001614E
		public override int sortOrderPriority
		{
			get
			{
				BaseRuntimePanel panel = this.m_Panel;
				return Mathf.FloorToInt((panel != null) ? panel.sortingPriority : 0f);
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x00017F6B File Offset: 0x0001616B
		public override int renderOrderPriority
		{
			get
			{
				int maxValue = int.MaxValue;
				int s_ResolvedSortingIndexMax = UIElementsRuntimeUtility.s_ResolvedSortingIndexMax;
				BaseRuntimePanel panel = this.m_Panel;
				return maxValue - (s_ResolvedSortingIndexMax - ((panel != null) ? panel.resolvedSortingIndex : 0));
			}
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00017F8C File Offset: 0x0001618C
		public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
		{
			if (this.m_Panel == null)
			{
				return;
			}
			int targetDisplay = this.m_Panel.targetDisplay;
			Vector3 relativeMousePositionForRaycast = MultipleDisplayUtilities.GetRelativeMousePositionForRaycast(eventData);
			if ((int)relativeMousePositionForRaycast.z != targetDisplay)
			{
				return;
			}
			Vector3 vector = relativeMousePositionForRaycast;
			Vector2 delta = eventData.delta;
			float num = (float)Screen.height;
			if (targetDisplay > 0 && targetDisplay < Display.displays.Length)
			{
				num = (float)Display.displays[targetDisplay].systemHeight;
			}
			vector.y = num - vector.y;
			delta.y = -delta.y;
			EventSystem eventSystem = UIElementsRuntimeUtility.activeEventSystem as EventSystem;
			if (eventSystem == null || eventSystem.currentInputModule == null)
			{
				return;
			}
			int num2 = eventSystem.currentInputModule.ConvertUIToolkitPointerId(eventData);
			IEventHandler capturingElement = this.m_Panel.GetCapturingElement(num2);
			VisualElement visualElement = capturingElement as VisualElement;
			if (visualElement != null && visualElement.panel != this.m_Panel)
			{
				return;
			}
			IPanel playerPanelWithSoftPointerCapture = PointerDeviceState.GetPlayerPanelWithSoftPointerCapture(num2);
			if (playerPanelWithSoftPointerCapture != null && playerPanelWithSoftPointerCapture != this.m_Panel)
			{
				return;
			}
			if (capturingElement == null && playerPanelWithSoftPointerCapture == null)
			{
				Vector2 vector2;
				Vector2 vector3;
				if (!this.m_Panel.ScreenToPanel(vector, delta, out vector2, out vector3, false))
				{
					return;
				}
				if (this.m_Panel.Pick(vector2) == null)
				{
					return;
				}
			}
			resultAppendList.Add(new RaycastResult
			{
				gameObject = this.selectableGameObject,
				module = this,
				screenPosition = relativeMousePositionForRaycast,
				displayIndex = this.m_Panel.targetDisplay
			});
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x000180FB File Offset: 0x000162FB
		public override Camera eventCamera
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040001AB RID: 427
		private BaseRuntimePanel m_Panel;
	}
}

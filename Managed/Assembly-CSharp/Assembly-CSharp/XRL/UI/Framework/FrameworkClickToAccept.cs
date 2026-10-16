using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace XRL.UI.Framework
{
	// Token: 0x02001507 RID: 5383
	[RequireComponent(typeof(FrameworkContext))]
	public class FrameworkClickToAccept : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, IPointerDownHandler, IPointerUpHandler
	{
		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x0600D1EA RID: 53738 RVA: 0x004DF353 File Offset: 0x004DD553
		public NavigationContext context
		{
			get
			{
				FrameworkContext component = base.GetComponent<FrameworkContext>();
				if (component == null)
				{
					return null;
				}
				return component.context;
			}
		}

		// Token: 0x0600D1EB RID: 53739 RVA: 0x004DF368 File Offset: 0x004DD568
		public void OnPointerClick(PointerEventData eventData)
		{
			if (!this.requireActiveContext || (this.context != null && this.context.IsActive(true)))
			{
				if (!this.requireActiveContext && !this.context.IsActive(false))
				{
					this.context.Activate(false);
				}
				NavigationController.instance.FireInputButtonEvent(InputButtonTypes.AcceptButton, new Dictionary<string, object> { { "PointerEventData", eventData } });
			}
		}

		// Token: 0x0600D1EC RID: 53740 RVA: 0x004DF3D3 File Offset: 0x004DD5D3
		public void OnPointerDown(PointerEventData eventData)
		{
		}

		// Token: 0x0600D1ED RID: 53741 RVA: 0x004DF3D5 File Offset: 0x004DD5D5
		public void OnPointerUp(PointerEventData eventData)
		{
		}

		// Token: 0x04005319 RID: 21273
		public bool requireActiveContext = true;
	}
}

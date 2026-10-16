using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace XRL.UI.Framework
{
	// Token: 0x02001509 RID: 5385
	[RequireComponent(typeof(FrameworkContext))]
	public class FrameworkHoverable : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler
	{
		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x0600D1F3 RID: 53747 RVA: 0x004DF44D File Offset: 0x004DD64D
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

		// Token: 0x0600D1F4 RID: 53748 RVA: 0x004DF460 File Offset: 0x004DD660
		public void OnPointerEnter(PointerEventData eventData)
		{
			if (!eventData.IsPointerMoving() || Input.GetMouseButton(0))
			{
				this.waitingToMove = true;
				this.waitingPointerLocation = Input.mousePosition;
				return;
			}
			NavigationContext context = this.context;
			if (context == null)
			{
				return;
			}
			context.Activate(false);
		}

		// Token: 0x0600D1F5 RID: 53749 RVA: 0x004DF49C File Offset: 0x004DD69C
		public void OnPointerExit(PointerEventData eventData)
		{
			this.waitingToMove = false;
		}

		// Token: 0x0600D1F6 RID: 53750 RVA: 0x004DF4A8 File Offset: 0x004DD6A8
		public void Update()
		{
			if (this.waitingToMove && !Input.GetMouseButton(0) && (this.waitingPointerLocation - Input.mousePosition).magnitude >= this.movementOffset)
			{
				NavigationContext context = this.context;
				if (context != null)
				{
					context.Activate(false);
				}
				this.waitingToMove = false;
			}
		}

		// Token: 0x0400531B RID: 21275
		public float movementOffset = 2f;

		// Token: 0x0400531C RID: 21276
		public bool waitingToMove;

		// Token: 0x0400531D RID: 21277
		public Vector2 waitingPointerLocation;
	}
}

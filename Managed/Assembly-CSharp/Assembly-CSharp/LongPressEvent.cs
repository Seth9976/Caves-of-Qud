using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

// Token: 0x02000170 RID: 368
public class LongPressEvent : UIBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IPointerExitHandler
{
	// Token: 0x06000E61 RID: 3681 RVA: 0x0005C5A8 File Offset: 0x0005A7A8
	private void Update()
	{
		if (this.isPointerDown && !this.longPressTriggered && Time.time - this.timePressStarted > this.durationThreshold)
		{
			this.longPressTriggered = true;
			this.onLongPress.Invoke();
		}
	}

	// Token: 0x06000E62 RID: 3682 RVA: 0x0005C5E0 File Offset: 0x0005A7E0
	public void OnPointerDown(PointerEventData eventData)
	{
		this.timePressStarted = Time.time;
		this.isPointerDown = true;
		this.longPressTriggered = false;
	}

	// Token: 0x06000E63 RID: 3683 RVA: 0x0005C5FB File Offset: 0x0005A7FB
	public void OnPointerUp(PointerEventData eventData)
	{
		this.isPointerDown = false;
	}

	// Token: 0x06000E64 RID: 3684 RVA: 0x0005C604 File Offset: 0x0005A804
	public void OnPointerExit(PointerEventData eventData)
	{
		this.isPointerDown = false;
	}

	// Token: 0x04000A06 RID: 2566
	[Tooltip("How long must pointer be down on this object to trigger a long press")]
	public float durationThreshold = 1f;

	// Token: 0x04000A07 RID: 2567
	public UnityEvent onLongPress = new UnityEvent();

	// Token: 0x04000A08 RID: 2568
	private bool isPointerDown;

	// Token: 0x04000A09 RID: 2569
	private bool longPressTriggered;

	// Token: 0x04000A0A RID: 2570
	private float timePressStarted;
}

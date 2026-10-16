using System;
using System.Text;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UIElements;

namespace UnityEngine.InputSystem.UI
{
	// Token: 0x02000087 RID: 135
	public class ExtendedPointerEventData : PointerEventData
	{
		// Token: 0x06000B0F RID: 2831 RVA: 0x0003B926 File Offset: 0x00039B26
		public ExtendedPointerEventData(EventSystem eventSystem)
			: base(eventSystem)
		{
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000B10 RID: 2832 RVA: 0x0003B92F File Offset: 0x00039B2F
		// (set) Token: 0x06000B11 RID: 2833 RVA: 0x0003B937 File Offset: 0x00039B37
		public InputControl control { get; set; }

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000B12 RID: 2834 RVA: 0x0003B940 File Offset: 0x00039B40
		// (set) Token: 0x06000B13 RID: 2835 RVA: 0x0003B948 File Offset: 0x00039B48
		public InputDevice device { get; set; }

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000B14 RID: 2836 RVA: 0x0003B951 File Offset: 0x00039B51
		// (set) Token: 0x06000B15 RID: 2837 RVA: 0x0003B959 File Offset: 0x00039B59
		public int touchId { get; set; }

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x0003B962 File Offset: 0x00039B62
		// (set) Token: 0x06000B17 RID: 2839 RVA: 0x0003B96A File Offset: 0x00039B6A
		public UIPointerType pointerType { get; set; }

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000B18 RID: 2840 RVA: 0x0003B973 File Offset: 0x00039B73
		// (set) Token: 0x06000B19 RID: 2841 RVA: 0x0003B97B File Offset: 0x00039B7B
		public int uiToolkitPointerId { get; set; }

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000B1A RID: 2842 RVA: 0x0003B984 File Offset: 0x00039B84
		// (set) Token: 0x06000B1B RID: 2843 RVA: 0x0003B98C File Offset: 0x00039B8C
		public Vector3 trackedDevicePosition { get; set; }

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000B1C RID: 2844 RVA: 0x0003B995 File Offset: 0x00039B95
		// (set) Token: 0x06000B1D RID: 2845 RVA: 0x0003B99D File Offset: 0x00039B9D
		public Quaternion trackedDeviceOrientation { get; set; }

		// Token: 0x06000B1E RID: 2846 RVA: 0x0003B9A8 File Offset: 0x00039BA8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(base.ToString());
			stringBuilder.AppendLine("button: " + base.button.ToString());
			stringBuilder.AppendLine("clickTime: " + base.clickTime.ToString());
			stringBuilder.AppendLine("clickCount: " + base.clickCount.ToString());
			string text = "device: ";
			InputDevice device = this.device;
			stringBuilder.AppendLine(text + ((device != null) ? device.ToString() : null));
			stringBuilder.AppendLine("pointerType: " + this.pointerType.ToString());
			stringBuilder.AppendLine("touchId: " + this.touchId.ToString());
			stringBuilder.AppendLine("pressPosition: " + base.pressPosition.ToString());
			stringBuilder.AppendLine("trackedDevicePosition: " + this.trackedDevicePosition.ToString());
			stringBuilder.AppendLine("trackedDeviceOrientation: " + this.trackedDeviceOrientation.ToString());
			stringBuilder.AppendLine("pressure" + base.pressure.ToString());
			stringBuilder.AppendLine("radius: " + base.radius.ToString());
			stringBuilder.AppendLine("azimuthAngle: " + base.azimuthAngle.ToString());
			stringBuilder.AppendLine("altitudeAngle: " + base.altitudeAngle.ToString());
			stringBuilder.AppendLine("twist: " + base.twist.ToString());
			stringBuilder.AppendLine("displayIndex: " + base.displayIndex.ToString());
			return stringBuilder.ToString();
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x0003BBC9 File Offset: 0x00039DC9
		internal static int MakePointerIdForTouch(int deviceId, int touchId)
		{
			return (deviceId << 24) + touchId;
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x0003BBD1 File Offset: 0x00039DD1
		internal static int TouchIdFromPointerId(int pointerId)
		{
			return pointerId & 255;
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x0003BBDC File Offset: 0x00039DDC
		internal unsafe void ReadDeviceState()
		{
			Pen pen = this.control.parent as Pen;
			if (pen != null)
			{
				this.uiToolkitPointerId = ExtendedPointerEventData.GetPenPointerId(pen);
				base.pressure = pen.pressure.magnitude;
				base.azimuthAngle = (pen.tilt.value.x + 1f) * 3.1415927f / 2f;
				base.altitudeAngle = (pen.tilt.value.y + 1f) * 3.1415927f / 2f;
				base.twist = *pen.twist.value * 3.1415927f * 2f;
				base.displayIndex = pen.displayIndex.ReadValue();
				return;
			}
			TouchControl touchControl = this.control.parent as TouchControl;
			if (touchControl != null)
			{
				this.uiToolkitPointerId = ExtendedPointerEventData.GetTouchPointerId(touchControl);
				base.pressure = touchControl.pressure.magnitude;
				base.radius = *touchControl.radius.value;
				base.displayIndex = touchControl.displayIndex.ReadValue();
				return;
			}
			Touchscreen touchscreen = this.control.parent as Touchscreen;
			if (touchscreen != null)
			{
				this.uiToolkitPointerId = ExtendedPointerEventData.GetTouchPointerId(touchscreen.primaryTouch);
				base.pressure = touchscreen.pressure.magnitude;
				base.radius = *touchscreen.radius.value;
				base.displayIndex = touchscreen.displayIndex.ReadValue();
				return;
			}
			this.uiToolkitPointerId = PointerId.mousePointerId;
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x0003BD60 File Offset: 0x00039F60
		private static int GetPenPointerId(Pen pen)
		{
			int num = 0;
			foreach (InputDevice inputDevice in InputSystem.devices)
			{
				Pen pen2 = inputDevice as Pen;
				if (pen2 != null)
				{
					if (pen == pen2)
					{
						return PointerId.penPointerIdBase + Mathf.Min(num, PointerId.penPointerCount - 1);
					}
					num++;
				}
			}
			return PointerId.penPointerIdBase;
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x0003BDE0 File Offset: 0x00039FE0
		private static int GetTouchPointerId(TouchControl touchControl)
		{
			int num = ((Touchscreen)touchControl.device).touches.IndexOfReference(touchControl);
			return PointerId.touchPointerIdBase + Mathf.Clamp(num, 0, PointerId.touchPointerCount - 1);
		}
	}
}

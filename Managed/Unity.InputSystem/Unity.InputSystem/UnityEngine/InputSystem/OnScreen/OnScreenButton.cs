using System;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem.OnScreen
{
	// Token: 0x02000096 RID: 150
	[AddComponentMenu("Input/On-Screen Button")]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.inputsystem@1.19/manual/OnScreen.html#on-screen-buttons")]
	public class OnScreenButton : OnScreenControl, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler
	{
		// Token: 0x06000BFF RID: 3071 RVA: 0x0004036B File Offset: 0x0003E56B
		public void OnPointerUp(PointerEventData eventData)
		{
			base.SendValueToControl<float>(0f);
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x00040378 File Offset: 0x0003E578
		public void OnPointerDown(PointerEventData eventData)
		{
			base.SendValueToControl<float>(1f);
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000C01 RID: 3073 RVA: 0x00040385 File Offset: 0x0003E585
		// (set) Token: 0x06000C02 RID: 3074 RVA: 0x0004038D File Offset: 0x0003E58D
		protected override string controlPathInternal
		{
			get
			{
				return this.m_ControlPath;
			}
			set
			{
				this.m_ControlPath = value;
			}
		}

		// Token: 0x04000469 RID: 1129
		[InputControl(layout = "Button")]
		[SerializeField]
		private string m_ControlPath;
	}
}

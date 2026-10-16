using System;

namespace UnityEngine.InputSystem.Users
{
	// Token: 0x02000084 RID: 132
	[Serializable]
	internal class InputUserSettings
	{
		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000AEF RID: 2799 RVA: 0x0003B804 File Offset: 0x00039A04
		// (set) Token: 0x06000AF0 RID: 2800 RVA: 0x0003B80C File Offset: 0x00039A0C
		public string customBindings { get; set; }

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000AF1 RID: 2801 RVA: 0x0003B815 File Offset: 0x00039A15
		// (set) Token: 0x06000AF2 RID: 2802 RVA: 0x0003B81D File Offset: 0x00039A1D
		public bool invertMouseX { get; set; }

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000AF3 RID: 2803 RVA: 0x0003B826 File Offset: 0x00039A26
		// (set) Token: 0x06000AF4 RID: 2804 RVA: 0x0003B82E File Offset: 0x00039A2E
		public bool invertMouseY { get; set; }

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000AF5 RID: 2805 RVA: 0x0003B837 File Offset: 0x00039A37
		// (set) Token: 0x06000AF6 RID: 2806 RVA: 0x0003B83F File Offset: 0x00039A3F
		public float? mouseSmoothing { get; set; }

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000AF7 RID: 2807 RVA: 0x0003B848 File Offset: 0x00039A48
		// (set) Token: 0x06000AF8 RID: 2808 RVA: 0x0003B850 File Offset: 0x00039A50
		public float? mouseSensitivity { get; set; }

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000AF9 RID: 2809 RVA: 0x0003B859 File Offset: 0x00039A59
		// (set) Token: 0x06000AFA RID: 2810 RVA: 0x0003B861 File Offset: 0x00039A61
		public bool invertStickX { get; set; }

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000AFB RID: 2811 RVA: 0x0003B86A File Offset: 0x00039A6A
		// (set) Token: 0x06000AFC RID: 2812 RVA: 0x0003B872 File Offset: 0x00039A72
		public bool invertStickY { get; set; }

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000AFD RID: 2813 RVA: 0x0003B87B File Offset: 0x00039A7B
		// (set) Token: 0x06000AFE RID: 2814 RVA: 0x0003B883 File Offset: 0x00039A83
		public bool swapSticks { get; set; }

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000AFF RID: 2815 RVA: 0x0003B88C File Offset: 0x00039A8C
		// (set) Token: 0x06000B00 RID: 2816 RVA: 0x0003B894 File Offset: 0x00039A94
		public bool swapBumpers { get; set; }

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000B01 RID: 2817 RVA: 0x0003B89D File Offset: 0x00039A9D
		// (set) Token: 0x06000B02 RID: 2818 RVA: 0x0003B8A5 File Offset: 0x00039AA5
		public bool swapTriggers { get; set; }

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000B03 RID: 2819 RVA: 0x0003B8AE File Offset: 0x00039AAE
		// (set) Token: 0x06000B04 RID: 2820 RVA: 0x0003B8B6 File Offset: 0x00039AB6
		public bool swapDpadAndLeftStick { get; set; }

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000B05 RID: 2821 RVA: 0x0003B8BF File Offset: 0x00039ABF
		// (set) Token: 0x06000B06 RID: 2822 RVA: 0x0003B8C7 File Offset: 0x00039AC7
		public float vibrationStrength { get; set; }

		// Token: 0x06000B07 RID: 2823 RVA: 0x0003B8D0 File Offset: 0x00039AD0
		public virtual void Apply(IInputActionCollection actions)
		{
		}

		// Token: 0x040003E8 RID: 1000
		[SerializeField]
		private string m_CustomBindings;
	}
}

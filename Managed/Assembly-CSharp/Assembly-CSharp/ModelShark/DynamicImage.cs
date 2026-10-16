using System;
using UnityEngine;
using UnityEngine.UI;

namespace ModelShark
{
	// Token: 0x02000193 RID: 403
	public class DynamicImage : MonoBehaviour
	{
		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000F5A RID: 3930 RVA: 0x00060BC6 File Offset: 0x0005EDC6
		// (set) Token: 0x06000F5B RID: 3931 RVA: 0x00060BCE File Offset: 0x0005EDCE
		public string Name { get; set; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000F5C RID: 3932 RVA: 0x00060BD7 File Offset: 0x0005EDD7
		public Image PlaceholderImage
		{
			get
			{
				return base.GetComponent<Image>();
			}
		}

		// Token: 0x04000A88 RID: 2696
		public string placeholderName;

		// Token: 0x04000A89 RID: 2697
		[HideInInspector]
		public Image image;
	}
}

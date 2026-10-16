using System;
using UnityEngine;
using UnityEngine.UI;

namespace ModelShark
{
	// Token: 0x02000192 RID: 402
	[RequireComponent(typeof(Button))]
	public class CloseAllTooltips : MonoBehaviour
	{
		// Token: 0x06000F58 RID: 3928 RVA: 0x00060B88 File Offset: 0x0005ED88
		private void Start()
		{
			base.gameObject.GetComponent<Button>().onClick.AddListener(delegate
			{
				TooltipManager.Instance.CloseAll();
			});
		}
	}
}

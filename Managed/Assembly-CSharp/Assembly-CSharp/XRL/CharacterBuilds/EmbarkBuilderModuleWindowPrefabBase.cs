using System;
using UnityEngine;
using XRL.UI.Framework;

namespace XRL.CharacterBuilds
{
	// Token: 0x0200058F RID: 1423
	public class EmbarkBuilderModuleWindowPrefabBase<T, PrefabComponent> : EmbarkBuilderModuleWindowBase<T> where T : AbstractEmbarkBuilderModule where PrefabComponent : MonoBehaviour, IFrameworkContext
	{
		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06003D28 RID: 15656 RVA: 0x0012BD28 File Offset: 0x00129F28
		public PrefabComponent prefabComponent
		{
			get
			{
				PrefabComponent prefabComponent;
				if ((prefabComponent = this._prefabComponent) == null)
				{
					prefabComponent = (this._prefabComponent = base.GetComponentInChildren<PrefabComponent>(true));
				}
				return prefabComponent;
			}
		}

		// Token: 0x06003D29 RID: 15657 RVA: 0x0012BD54 File Offset: 0x00129F54
		public override NavigationContext GetNavigationContext()
		{
			return this.prefabComponent.GetNavigationContext();
		}

		// Token: 0x06003D2A RID: 15658 RVA: 0x0012BD66 File Offset: 0x00129F66
		public override void EnsureContentsActive()
		{
			this.prefabComponent.gameObject.SetActive(true);
		}

		// Token: 0x04001E68 RID: 7784
		private PrefabComponent _prefabComponent;
	}
}

using System;
using ConsoleLib.Console;
using Qud.UI;
using UnityEngine;

namespace XRL.CharacterBuilds
{
	// Token: 0x02000590 RID: 1424
	public class EmbarkBuilderModuleWindowDescriptor
	{
		// Token: 0x06003D2C RID: 15660 RVA: 0x0012BD86 File Offset: 0x00129F86
		public EmbarkBuilderModuleWindowDescriptor(AbstractEmbarkBuilderModule module)
		{
			this.module = module;
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06003D2D RID: 15661 RVA: 0x0012BD95 File Offset: 0x00129F95
		public bool enabled
		{
			get
			{
				AbstractBuilderModuleWindowBase abstractBuilderModuleWindowBase = this.window;
				return (abstractBuilderModuleWindowBase == null || abstractBuilderModuleWindowBase.isWindowEnabled) && this.module.enabled;
			}
		}

		// Token: 0x06003D2E RID: 15662 RVA: 0x0012BDB8 File Offset: 0x00129FB8
		public virtual void show()
		{
			this.window.EnsureContentsActive();
			this.window.BeforeShow(this);
			this.module.builder.activeWindow = this;
			UIManager.showWindow(this.viewID, false);
		}

		// Token: 0x06003D2F RID: 15663 RVA: 0x0012BDF0 File Offset: 0x00129FF0
		public AbstractBuilderModuleWindowBase getWindow()
		{
			if (this.viewID == null)
			{
				this.viewID = Guid.NewGuid().ToString();
			}
			if (this.window == null)
			{
				if (this.prefab == null)
				{
					this.window = UIManager.getWindow(this.viewID) as AbstractBuilderModuleWindowBase;
				}
				else
				{
					GameObject gameObject = Resources.Load("Prefabs/" + this.prefab) as GameObject;
					if (gameObject != null)
					{
						gameObject.SetActive(false);
						this.window = this.module.builder.createWindow(this.viewID, this.windowType);
						this.window.name = this.name;
						GameObject gameObject2 = global::UnityEngine.Object.Instantiate<GameObject>(gameObject);
						gameObject2 = this.window.InstantiatePrefab(gameObject2);
						gameObject2.transform.SetParent(this.window.transform, false);
					}
				}
			}
			if (this.window == null)
			{
				Debug.LogError(this.viewID + " not found to init module window");
			}
			return this.window;
		}

		// Token: 0x06003D30 RID: 15664 RVA: 0x0012BF04 File Offset: 0x0012A104
		public virtual void windowInit(AbstractBuilderModuleWindowBase window)
		{
			this.window = window;
			window._module = this.module;
			this.window.Init();
		}

		// Token: 0x04001E69 RID: 7785
		public string name;

		// Token: 0x04001E6A RID: 7786
		public string viewID;

		// Token: 0x04001E6B RID: 7787
		public string tile;

		// Token: 0x04001E6C RID: 7788
		public string prefab;

		// Token: 0x04001E6D RID: 7789
		public string title;

		// Token: 0x04001E6E RID: 7790
		public Type windowType;

		// Token: 0x04001E6F RID: 7791
		public IRenderable navIcon;

		// Token: 0x04001E70 RID: 7792
		public AbstractEmbarkBuilderModule module;

		// Token: 0x04001E71 RID: 7793
		public AbstractBuilderModuleWindowBase window;
	}
}

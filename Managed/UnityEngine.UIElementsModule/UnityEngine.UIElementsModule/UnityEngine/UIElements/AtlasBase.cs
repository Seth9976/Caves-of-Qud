using System;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x02000005 RID: 5
	internal abstract class AtlasBase
	{
		// Token: 0x06000009 RID: 9 RVA: 0x0000212C File Offset: 0x0000032C
		public virtual bool TryGetAtlas(VisualElement ctx, Texture2D src, out TextureId atlas, out RectInt atlasRect)
		{
			atlas = TextureId.invalid;
			atlasRect = default(RectInt);
			return false;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002152 File Offset: 0x00000352
		public virtual void ReturnAtlas(VisualElement ctx, Texture2D src, TextureId atlas)
		{
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002152 File Offset: 0x00000352
		public virtual void Reset()
		{
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002152 File Offset: 0x00000352
		protected virtual void OnAssignedToPanel(IPanel panel)
		{
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002152 File Offset: 0x00000352
		protected virtual void OnRemovedFromPanel(IPanel panel)
		{
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002152 File Offset: 0x00000352
		protected virtual void OnUpdateDynamicTextures(IPanel panel)
		{
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002155 File Offset: 0x00000355
		internal void InvokeAssignedToPanel(IPanel panel)
		{
			this.OnAssignedToPanel(panel);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002160 File Offset: 0x00000360
		internal void InvokeRemovedFromPanel(IPanel panel)
		{
			this.OnRemovedFromPanel(panel);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000216B File Offset: 0x0000036B
		internal void InvokeUpdateDynamicTextures(IPanel panel)
		{
			this.OnUpdateDynamicTextures(panel);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002178 File Offset: 0x00000378
		protected static void RepaintTexturedElements(IPanel panel)
		{
			Panel panel2 = panel as Panel;
			UIRRepaintUpdater uirrepaintUpdater = ((panel2 != null) ? panel2.GetUpdater(VisualTreeUpdatePhase.Repaint) : null) as UIRRepaintUpdater;
			if (uirrepaintUpdater != null)
			{
				RenderChain renderChain = uirrepaintUpdater.renderChain;
				if (renderChain != null)
				{
					renderChain.RepaintTexturedElements();
				}
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000021B8 File Offset: 0x000003B8
		protected TextureId AllocateDynamicTexture()
		{
			return this.textureRegistry.AllocAndAcquireDynamic();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000021D5 File Offset: 0x000003D5
		protected void FreeDynamicTexture(TextureId id)
		{
			this.textureRegistry.Release(id);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000021E5 File Offset: 0x000003E5
		protected void SetDynamicTexture(TextureId id, Texture texture)
		{
			this.textureRegistry.UpdateDynamic(id, texture);
		}

		// Token: 0x04000001 RID: 1
		internal TextureRegistry textureRegistry = TextureRegistry.instance;
	}
}

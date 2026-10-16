using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002C9 RID: 713
	public static class RuntimePanelUtils
	{
		// Token: 0x0600147D RID: 5245 RVA: 0x00051CA4 File Offset: 0x0004FEA4
		public static Vector2 ScreenToPanel(IPanel panel, Vector2 screenPosition)
		{
			return ((BaseRuntimePanel)panel).ScreenToPanel(screenPosition);
		}

		// Token: 0x0600147E RID: 5246 RVA: 0x00051CC4 File Offset: 0x0004FEC4
		public static Vector2 CameraTransformWorldToPanel(IPanel panel, Vector3 worldPosition, Camera camera)
		{
			Vector2 vector = camera.WorldToScreenPoint(worldPosition);
			float editorDisplayHeight = UIElementsRuntimeUtility.GetEditorDisplayHeight(camera.targetDisplay);
			vector = UIElementsRuntimeUtility.FlipY(vector, editorDisplayHeight);
			return ((BaseRuntimePanel)panel).ScreenToPanel(vector);
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x00051D04 File Offset: 0x0004FF04
		public static Rect CameraTransformWorldToPanelRect(IPanel panel, Vector3 worldPosition, Vector2 worldSize, Camera camera)
		{
			worldSize.y = -worldSize.y;
			Vector2 vector = RuntimePanelUtils.CameraTransformWorldToPanel(panel, worldPosition, camera);
			Vector3 vector2 = worldPosition + camera.worldToCameraMatrix.MultiplyVector(worldSize);
			Vector2 vector3 = RuntimePanelUtils.CameraTransformWorldToPanel(panel, vector2, camera);
			return new Rect(vector, vector3 - vector);
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x00051D60 File Offset: 0x0004FF60
		public static void ResetDynamicAtlas(this IPanel panel)
		{
			BaseVisualElementPanel baseVisualElementPanel = panel as BaseVisualElementPanel;
			bool flag = baseVisualElementPanel == null;
			if (!flag)
			{
				DynamicAtlas dynamicAtlas = baseVisualElementPanel.atlas as DynamicAtlas;
				if (dynamicAtlas != null)
				{
					dynamicAtlas.Reset();
				}
			}
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x00051D98 File Offset: 0x0004FF98
		public static void ResetRenderer(this IPanel panel)
		{
			BaseVisualElementPanel baseVisualElementPanel = panel as BaseVisualElementPanel;
			bool flag = baseVisualElementPanel == null;
			if (!flag)
			{
				IPanelRenderer panelRenderer = baseVisualElementPanel.panelRenderer;
				if (panelRenderer != null)
				{
					panelRenderer.Reset();
				}
			}
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x00051DCC File Offset: 0x0004FFCC
		public static void SetTextureDirty(this IPanel panel, Texture2D texture)
		{
			BaseVisualElementPanel baseVisualElementPanel = panel as BaseVisualElementPanel;
			bool flag = baseVisualElementPanel == null;
			if (!flag)
			{
				DynamicAtlas dynamicAtlas = baseVisualElementPanel.atlas as DynamicAtlas;
				if (dynamicAtlas != null)
				{
					dynamicAtlas.SetDirty(texture);
				}
			}
		}
	}
}

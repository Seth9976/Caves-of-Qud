using System;
using System.ComponentModel;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000017 RID: 23
	[ExcludeFromObjectFactory]
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("GUIElement has been removed. Consider using https://docs.unity3d.com/ScriptReference/UIElements.Image.html, https://docs.unity3d.com/ScriptReference/UIElements.TextElement.html or TextMeshPro instead.", true)]
	[ExcludeFromPreset]
	public sealed class GUIElement
	{
		// Token: 0x060001E2 RID: 482 RVA: 0x000089EC File Offset: 0x00006BEC
		private static void FeatureRemoved()
		{
			throw new Exception("GUIElement has been removed from Unity. Consider using https://docs.unity3d.com/ScriptReference/UIElements.Image.html, https://docs.unity3d.com/ScriptReference/UIElements.TextElement.html or TextMeshPro instead.");
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x000089FC File Offset: 0x00006BFC
		[Obsolete("GUIElement has been removed. Consider using https://docs.unity3d.com/ScriptReference/UIElements.Image.html, https://docs.unity3d.com/ScriptReference/UIElements.TextElement.html or TextMeshPro instead.", true)]
		public bool HitTest(Vector3 screenPosition)
		{
			GUIElement.FeatureRemoved();
			return false;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00008A18 File Offset: 0x00006C18
		[Obsolete("GUIElement has been removed. Consider using https://docs.unity3d.com/ScriptReference/UIElements.Image.html, https://docs.unity3d.com/ScriptReference/UIElements.TextElement.html or TextMeshPro instead.", true)]
		public bool HitTest(Vector3 screenPosition, [DefaultValue("null")] Camera camera)
		{
			GUIElement.FeatureRemoved();
			return false;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00008A34 File Offset: 0x00006C34
		[Obsolete("GUIElement has been removed. Consider using https://docs.unity3d.com/ScriptReference/UIElements.Image.html, https://docs.unity3d.com/ScriptReference/UIElements.TextElement.html or TextMeshPro instead.", true)]
		public Rect GetScreenRect([DefaultValue("null")] Camera camera)
		{
			GUIElement.FeatureRemoved();
			return new Rect(0f, 0f, 0f, 0f);
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00008A68 File Offset: 0x00006C68
		[Obsolete("GUIElement has been removed. Consider using https://docs.unity3d.com/ScriptReference/UIElements.Image.html, https://docs.unity3d.com/ScriptReference/UIElements.TextElement.html or TextMeshPro instead.", true)]
		public Rect GetScreenRect()
		{
			GUIElement.FeatureRemoved();
			return new Rect(0f, 0f, 0f, 0f);
		}
	}
}

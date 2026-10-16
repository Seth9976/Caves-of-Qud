using System;
using System.Collections.Generic;
using Unity.Profiling;
using Unity.Properties;
using UnityEngine.Internal;

namespace UnityEngine.UIElements
{
	// Token: 0x020002DE RID: 734
	public abstract class ImmediateModeElement : VisualElement
	{
		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06001511 RID: 5393 RVA: 0x00054E08 File Offset: 0x00053008
		// (set) Token: 0x06001512 RID: 5394 RVA: 0x00054E20 File Offset: 0x00053020
		[CreateProperty]
		public bool cullingEnabled
		{
			get
			{
				return this.m_CullingEnabled;
			}
			set
			{
				bool flag = this.m_CullingEnabled == value;
				if (!flag)
				{
					this.m_CullingEnabled = value;
					base.IncrementVersion(VersionChangeType.Repaint);
					base.NotifyPropertyChanged(in ImmediateModeElement.cullingEnabledProperty);
				}
			}
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x00054E5C File Offset: 0x0005305C
		public ImmediateModeElement()
		{
			base.generateVisualContent = (Action<MeshGenerationContext>)Delegate.Combine(base.generateVisualContent, new Action<MeshGenerationContext>(this.OnGenerateVisualContent));
			Type type = base.GetType();
			bool flag = !ImmediateModeElement.s_Markers.TryGetValue(type, out this.m_ImmediateRepaintMarker);
			if (flag)
			{
				this.m_ImmediateRepaintMarker = new ProfilerMarker(base.typeName + ".ImmediateRepaint");
				ImmediateModeElement.s_Markers[type] = this.m_ImmediateRepaintMarker;
			}
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x00054EEC File Offset: 0x000530EC
		private void OnGenerateVisualContent(MeshGenerationContext mgc)
		{
			BaseRuntimePanel baseRuntimePanel = base.elementPanel as BaseRuntimePanel;
			bool flag = baseRuntimePanel != null && baseRuntimePanel.drawsInCameras;
			if (flag)
			{
				Debug.LogError("ImmediateModeElement cannot be used in a panel drawn by cameras.");
			}
			else
			{
				mgc.entryRecorder.DrawImmediate(mgc.parentEntry, new Action(this.CallImmediateRepaint), this.cullingEnabled);
			}
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x00054F48 File Offset: 0x00053148
		private void CallImmediateRepaint()
		{
			using (this.m_ImmediateRepaintMarker.Auto())
			{
				this.ImmediateRepaint();
			}
		}

		// Token: 0x06001516 RID: 5398
		protected abstract void ImmediateRepaint();

		// Token: 0x04000B69 RID: 2921
		internal static readonly BindingId cullingEnabledProperty = "cullingEnabled";

		// Token: 0x04000B6A RID: 2922
		private static readonly Dictionary<Type, ProfilerMarker> s_Markers = new Dictionary<Type, ProfilerMarker>();

		// Token: 0x04000B6B RID: 2923
		private readonly ProfilerMarker m_ImmediateRepaintMarker;

		// Token: 0x04000B6C RID: 2924
		private bool m_CullingEnabled = false;

		// Token: 0x020002DF RID: 735
		[ExcludeFromDocs]
		[Serializable]
		public new class UxmlSerializedData : VisualElement.UxmlSerializedData
		{
		}
	}
}

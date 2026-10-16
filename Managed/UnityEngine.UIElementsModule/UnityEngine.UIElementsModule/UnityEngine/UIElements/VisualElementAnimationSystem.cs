using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Profiling;
using UnityEngine.UIElements.Experimental;

namespace UnityEngine.UIElements
{
	// Token: 0x020005CC RID: 1484
	internal class VisualElementAnimationSystem : BaseVisualTreeUpdater
	{
		// Token: 0x06002BCD RID: 11213 RVA: 0x0009E274 File Offset: 0x0009C474
		private long CurrentTimeMs()
		{
			return Panel.TimeSinceStartupMs();
		}

		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x06002BCE RID: 11214 RVA: 0x0009E28B File Offset: 0x0009C48B
		public override ProfilerMarker profilerMarker
		{
			get
			{
				return VisualElementAnimationSystem.s_ProfilerMarker;
			}
		}

		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x06002BCF RID: 11215 RVA: 0x0009E292 File Offset: 0x0009C492
		private static ProfilerMarker stylePropertyAnimationProfilerMarker
		{
			get
			{
				return VisualElementAnimationSystem.s_StylePropertyAnimationProfilerMarker;
			}
		}

		// Token: 0x06002BD0 RID: 11216 RVA: 0x0009E299 File Offset: 0x0009C499
		public void UnregisterAnimation(IValueAnimationUpdate anim)
		{
			this.m_Animations.Remove(anim);
			this.m_IterationListDirty = true;
		}

		// Token: 0x06002BD1 RID: 11217 RVA: 0x0009E2B0 File Offset: 0x0009C4B0
		public void UnregisterAnimations(List<IValueAnimationUpdate> anims)
		{
			foreach (IValueAnimationUpdate valueAnimationUpdate in anims)
			{
				this.m_Animations.Remove(valueAnimationUpdate);
			}
			this.m_IterationListDirty = true;
		}

		// Token: 0x06002BD2 RID: 11218 RVA: 0x0009E310 File Offset: 0x0009C510
		public void RegisterAnimation(IValueAnimationUpdate anim)
		{
			this.m_Animations.Add(anim);
			this.m_HasNewAnimations = true;
			this.m_IterationListDirty = true;
		}

		// Token: 0x06002BD3 RID: 11219 RVA: 0x0009E330 File Offset: 0x0009C530
		public void RegisterAnimations(List<IValueAnimationUpdate> anims)
		{
			foreach (IValueAnimationUpdate valueAnimationUpdate in anims)
			{
				this.m_Animations.Add(valueAnimationUpdate);
			}
			this.m_HasNewAnimations = true;
			this.m_IterationListDirty = true;
		}

		// Token: 0x06002BD4 RID: 11220 RVA: 0x0009E398 File Offset: 0x0009C598
		public override void Update()
		{
			long num = Panel.TimeSinceStartupMs();
			bool iterationListDirty = this.m_IterationListDirty;
			if (iterationListDirty)
			{
				this.m_IterationList = this.m_Animations.ToList<IValueAnimationUpdate>();
				this.m_IterationListDirty = false;
			}
			bool flag = this.m_HasNewAnimations || this.lastUpdate != num;
			if (flag)
			{
				foreach (IValueAnimationUpdate valueAnimationUpdate in this.m_IterationList)
				{
					valueAnimationUpdate.Tick(num);
				}
				this.m_HasNewAnimations = false;
				this.lastUpdate = num;
			}
			IStylePropertyAnimationSystem styleAnimationSystem = base.panel.styleAnimationSystem;
			using (VisualElementAnimationSystem.stylePropertyAnimationProfilerMarker.Auto())
			{
				styleAnimationSystem.Update();
			}
		}

		// Token: 0x06002BD5 RID: 11221 RVA: 0x00002152 File Offset: 0x00000352
		public override void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType)
		{
		}

		// Token: 0x040013EF RID: 5103
		private HashSet<IValueAnimationUpdate> m_Animations = new HashSet<IValueAnimationUpdate>();

		// Token: 0x040013F0 RID: 5104
		private List<IValueAnimationUpdate> m_IterationList = new List<IValueAnimationUpdate>();

		// Token: 0x040013F1 RID: 5105
		private bool m_HasNewAnimations = false;

		// Token: 0x040013F2 RID: 5106
		private bool m_IterationListDirty = false;

		// Token: 0x040013F3 RID: 5107
		private static readonly string s_Description = "Animation Update";

		// Token: 0x040013F4 RID: 5108
		private static readonly ProfilerMarker s_ProfilerMarker = new ProfilerMarker(VisualElementAnimationSystem.s_Description);

		// Token: 0x040013F5 RID: 5109
		private static readonly string s_StylePropertyAnimationDescription = "StylePropertyAnimation Update";

		// Token: 0x040013F6 RID: 5110
		private static readonly ProfilerMarker s_StylePropertyAnimationProfilerMarker = new ProfilerMarker(VisualElementAnimationSystem.s_StylePropertyAnimationDescription);

		// Token: 0x040013F7 RID: 5111
		private long lastUpdate;
	}
}

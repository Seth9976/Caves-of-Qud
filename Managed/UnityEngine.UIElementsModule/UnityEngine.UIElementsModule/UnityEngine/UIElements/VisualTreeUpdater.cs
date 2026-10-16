using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020005D8 RID: 1496
	internal sealed class VisualTreeUpdater : IDisposable
	{
		// Token: 0x06002C1B RID: 11291 RVA: 0x0009F8F4 File Offset: 0x0009DAF4
		public long[] GetUpdatersFrameCount()
		{
			return this.m_UpdaterArray.GetUpdatersFrameCount();
		}

		// Token: 0x06002C1C RID: 11292 RVA: 0x0009F911 File Offset: 0x0009DB11
		public VisualTreeUpdater(BaseVisualElementPanel panel)
		{
			this.m_Panel = panel;
			this.m_UpdaterArray = new VisualTreeUpdater.UpdaterArray();
			this.SetDefaultUpdaters();
		}

		// Token: 0x06002C1D RID: 11293 RVA: 0x0009F934 File Offset: 0x0009DB34
		public void Dispose()
		{
			for (int i = 0; i < 7; i++)
			{
				IVisualTreeUpdater visualTreeUpdater = this.m_UpdaterArray[i];
				visualTreeUpdater.Dispose();
			}
		}

		// Token: 0x06002C1E RID: 11294 RVA: 0x0009F968 File Offset: 0x0009DB68
		public void UpdateVisualTree()
		{
			for (int i = 0; i < 7; i++)
			{
				IVisualTreeUpdater visualTreeUpdater = this.m_UpdaterArray[i];
				using (visualTreeUpdater.profilerMarker.Auto())
				{
					visualTreeUpdater.Update();
					IVisualTreeUpdater visualTreeUpdater2 = visualTreeUpdater;
					long num = visualTreeUpdater2.FrameCount + 1L;
					visualTreeUpdater2.FrameCount = num;
				}
			}
		}

		// Token: 0x06002C1F RID: 11295 RVA: 0x0009F9E4 File Offset: 0x0009DBE4
		public void UpdateVisualTreePhase(VisualTreeUpdatePhase phase)
		{
			IVisualTreeUpdater visualTreeUpdater = this.m_UpdaterArray[phase];
			using (visualTreeUpdater.profilerMarker.Auto())
			{
				visualTreeUpdater.Update();
				IVisualTreeUpdater visualTreeUpdater2 = visualTreeUpdater;
				long num = visualTreeUpdater2.FrameCount + 1L;
				visualTreeUpdater2.FrameCount = num;
			}
		}

		// Token: 0x06002C20 RID: 11296 RVA: 0x0009FA4C File Offset: 0x0009DC4C
		public void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType)
		{
			for (int i = 0; i < 7; i++)
			{
				IVisualTreeUpdater visualTreeUpdater = this.m_UpdaterArray[i];
				visualTreeUpdater.OnVersionChanged(ve, versionChangeType);
			}
		}

		// Token: 0x06002C21 RID: 11297 RVA: 0x0009FA82 File Offset: 0x0009DC82
		public void SetUpdater(IVisualTreeUpdater updater, VisualTreeUpdatePhase phase)
		{
			IVisualTreeUpdater visualTreeUpdater = this.m_UpdaterArray[phase];
			if (visualTreeUpdater != null)
			{
				visualTreeUpdater.Dispose();
			}
			updater.panel = this.m_Panel;
			this.m_UpdaterArray[phase] = updater;
		}

		// Token: 0x06002C22 RID: 11298 RVA: 0x0009FAB8 File Offset: 0x0009DCB8
		public void SetUpdater<T>(VisualTreeUpdatePhase phase) where T : IVisualTreeUpdater, new()
		{
			IVisualTreeUpdater visualTreeUpdater = this.m_UpdaterArray[phase];
			if (visualTreeUpdater != null)
			{
				visualTreeUpdater.Dispose();
			}
			T t = new T();
			t.panel = this.m_Panel;
			T t2 = t;
			this.m_UpdaterArray[phase] = t2;
		}

		// Token: 0x06002C23 RID: 11299 RVA: 0x0009FB10 File Offset: 0x0009DD10
		public IVisualTreeUpdater GetUpdater(VisualTreeUpdatePhase phase)
		{
			return this.m_UpdaterArray[phase];
		}

		// Token: 0x06002C24 RID: 11300 RVA: 0x0009FB2E File Offset: 0x0009DD2E
		private void SetDefaultUpdaters()
		{
			this.SetUpdater<VisualTreeBindingsUpdater>(VisualTreeUpdatePhase.Bindings);
			this.SetUpdater<VisualTreeDataBindingsUpdater>(VisualTreeUpdatePhase.DataBinding);
			this.SetUpdater<VisualElementAnimationSystem>(VisualTreeUpdatePhase.Animation);
			this.SetUpdater<VisualTreeStyleUpdater>(VisualTreeUpdatePhase.Styles);
			this.SetUpdater<UIRLayoutUpdater>(VisualTreeUpdatePhase.Layout);
			this.SetUpdater<VisualTreeHierarchyFlagsUpdater>(VisualTreeUpdatePhase.TransformClip);
			this.SetUpdater<UIRRepaintUpdater>(VisualTreeUpdatePhase.Repaint);
		}

		// Token: 0x04001438 RID: 5176
		private BaseVisualElementPanel m_Panel;

		// Token: 0x04001439 RID: 5177
		private VisualTreeUpdater.UpdaterArray m_UpdaterArray;

		// Token: 0x020005D9 RID: 1497
		private class UpdaterArray
		{
			// Token: 0x06002C25 RID: 11301 RVA: 0x0009FB69 File Offset: 0x0009DD69
			public UpdaterArray()
			{
				this.m_VisualTreeUpdaters = new IVisualTreeUpdater[7];
			}

			// Token: 0x17000AE6 RID: 2790
			public IVisualTreeUpdater this[VisualTreeUpdatePhase phase]
			{
				get
				{
					return this.m_VisualTreeUpdaters[(int)phase];
				}
				set
				{
					this.m_VisualTreeUpdaters[(int)phase] = value;
				}
			}

			// Token: 0x17000AE7 RID: 2791
			public IVisualTreeUpdater this[int index]
			{
				get
				{
					return this.m_VisualTreeUpdaters[index];
				}
				set
				{
					this.m_VisualTreeUpdaters[index] = value;
				}
			}

			// Token: 0x06002C2A RID: 11306 RVA: 0x0009FBC4 File Offset: 0x0009DDC4
			public long[] GetUpdatersFrameCount()
			{
				long[] array = new long[this.m_VisualTreeUpdaters.Length];
				for (int i = 0; i < this.m_VisualTreeUpdaters.Length; i++)
				{
					array[i] = this.m_VisualTreeUpdaters[i].FrameCount;
				}
				return array;
			}

			// Token: 0x0400143A RID: 5178
			private IVisualTreeUpdater[] m_VisualTreeUpdaters;
		}
	}
}

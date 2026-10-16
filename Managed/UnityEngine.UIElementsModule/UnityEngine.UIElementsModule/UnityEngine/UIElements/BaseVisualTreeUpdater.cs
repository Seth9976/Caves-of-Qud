using System;
using System.Diagnostics;
using Unity.Profiling;

namespace UnityEngine.UIElements
{
	// Token: 0x020005DB RID: 1499
	internal abstract class BaseVisualTreeUpdater : IVisualTreeUpdater, IDisposable
	{
		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x06002C32 RID: 11314 RVA: 0x0009FC10 File Offset: 0x0009DE10
		// (set) Token: 0x06002C33 RID: 11315 RVA: 0x0009FC28 File Offset: 0x0009DE28
		long IVisualTreeUpdater.FrameCount
		{
			get
			{
				return this.frameCount;
			}
			set
			{
				this.frameCount = value;
			}
		}

		// Token: 0x1400004E RID: 78
		// (add) Token: 0x06002C34 RID: 11316 RVA: 0x0009FC34 File Offset: 0x0009DE34
		// (remove) Token: 0x06002C35 RID: 11317 RVA: 0x0009FC6C File Offset: 0x0009DE6C
		[field: DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public event Action<BaseVisualElementPanel> panelChanged;

		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x06002C36 RID: 11318 RVA: 0x0009FCA4 File Offset: 0x0009DEA4
		// (set) Token: 0x06002C37 RID: 11319 RVA: 0x0009FCBC File Offset: 0x0009DEBC
		public BaseVisualElementPanel panel
		{
			get
			{
				return this.m_Panel;
			}
			set
			{
				this.m_Panel = value;
				bool flag = this.panelChanged != null;
				if (flag)
				{
					this.panelChanged(value);
				}
			}
		}

		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x06002C38 RID: 11320 RVA: 0x0009FCEC File Offset: 0x0009DEEC
		public VisualElement visualTree
		{
			get
			{
				return this.panel.visualTree;
			}
		}

		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x06002C39 RID: 11321
		public abstract ProfilerMarker profilerMarker { get; }

		// Token: 0x06002C3A RID: 11322 RVA: 0x0009FD09 File Offset: 0x0009DF09
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002C3B RID: 11323 RVA: 0x00002152 File Offset: 0x00000352
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x06002C3C RID: 11324
		public abstract void Update();

		// Token: 0x06002C3D RID: 11325
		public abstract void OnVersionChanged(VisualElement ve, VersionChangeType versionChangeType);

		// Token: 0x0400143B RID: 5179
		private long frameCount;

		// Token: 0x0400143D RID: 5181
		private BaseVisualElementPanel m_Panel;
	}
}

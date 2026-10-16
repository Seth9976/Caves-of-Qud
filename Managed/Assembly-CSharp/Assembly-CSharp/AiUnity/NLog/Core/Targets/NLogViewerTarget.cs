using System;
using System.Collections.Generic;
using System.ComponentModel;
using AiUnity.Common.Attributes;
using AiUnity.NLog.Core.Common;
using AiUnity.NLog.Core.Config;
using AiUnity.NLog.Core.LayoutRenderers;
using AiUnity.NLog.Core.Layouts;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.Targets
{
	// Token: 0x02000223 RID: 547
	[Target("NLogViewer")]
	[Preserve]
	public class NLogViewerTarget : NetworkTarget
	{
		// Token: 0x0600143B RID: 5179 RVA: 0x00072CDE File Offset: 0x00070EDE
		public NLogViewerTarget()
		{
			this.Parameters = new List<NLogViewerParameterInfo>();
			this.Renderer.Parameters = this.Parameters;
			base.NewLine = false;
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x0600143C RID: 5180 RVA: 0x00072D14 File Offset: 0x00070F14
		// (set) Token: 0x0600143D RID: 5181 RVA: 0x00072D21 File Offset: 0x00070F21
		public bool IncludeNLogData
		{
			get
			{
				return this.Renderer.IncludeNLogData;
			}
			set
			{
				this.Renderer.IncludeNLogData = value;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x0600143E RID: 5182 RVA: 0x00072D2F File Offset: 0x00070F2F
		// (set) Token: 0x0600143F RID: 5183 RVA: 0x00072D3C File Offset: 0x00070F3C
		public string AppInfo
		{
			get
			{
				return this.Renderer.AppInfo;
			}
			set
			{
				this.Renderer.AppInfo = value;
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06001440 RID: 5184 RVA: 0x00072D4A File Offset: 0x00070F4A
		// (set) Token: 0x06001441 RID: 5185 RVA: 0x00072D57 File Offset: 0x00070F57
		public bool IncludeCallSite
		{
			get
			{
				return this.Renderer.IncludeCallSite;
			}
			set
			{
				this.Renderer.IncludeCallSite = value;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06001442 RID: 5186 RVA: 0x00072D65 File Offset: 0x00070F65
		// (set) Token: 0x06001443 RID: 5187 RVA: 0x00072D72 File Offset: 0x00070F72
		[Display("Source Info", "Send message source to NLogViewer.", false, 0)]
		[DefaultValue(true)]
		public bool IncludeSourceInfo
		{
			get
			{
				return this.Renderer.IncludeSourceInfo;
			}
			set
			{
				this.Renderer.IncludeSourceInfo = value;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06001444 RID: 5188 RVA: 0x00072D80 File Offset: 0x00070F80
		// (set) Token: 0x06001445 RID: 5189 RVA: 0x00072D8D File Offset: 0x00070F8D
		public bool IncludeMdc
		{
			get
			{
				return this.Renderer.IncludeMdc;
			}
			set
			{
				this.Renderer.IncludeMdc = value;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06001446 RID: 5190 RVA: 0x00072D9B File Offset: 0x00070F9B
		// (set) Token: 0x06001447 RID: 5191 RVA: 0x00072DA8 File Offset: 0x00070FA8
		public bool IncludeNdc
		{
			get
			{
				return this.Renderer.IncludeNdc;
			}
			set
			{
				this.Renderer.IncludeNdc = value;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06001448 RID: 5192 RVA: 0x00072DB6 File Offset: 0x00070FB6
		// (set) Token: 0x06001449 RID: 5193 RVA: 0x00072DC3 File Offset: 0x00070FC3
		public string NdcItemSeparator
		{
			get
			{
				return this.Renderer.NdcItemSeparator;
			}
			set
			{
				this.Renderer.NdcItemSeparator = value;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x0600144A RID: 5194 RVA: 0x00072DD1 File Offset: 0x00070FD1
		// (set) Token: 0x0600144B RID: 5195 RVA: 0x00072DD9 File Offset: 0x00070FD9
		[ArrayParameter(typeof(NLogViewerParameterInfo), "parameter")]
		public IList<NLogViewerParameterInfo> Parameters { get; private set; }

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x0600144C RID: 5196 RVA: 0x00072DE2 File Offset: 0x00070FE2
		public Log4JXmlEventLayoutRenderer Renderer
		{
			get
			{
				return this.layout.Renderer;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x0600144D RID: 5197 RVA: 0x00072DEF File Offset: 0x00070FEF
		// (set) Token: 0x0600144E RID: 5198 RVA: 0x00072DF7 File Offset: 0x00070FF7
		public override Layout Layout
		{
			get
			{
				return this.layout;
			}
			set
			{
			}
		}

		// Token: 0x04000D1B RID: 3355
		private readonly Log4JXmlEventLayout layout = new Log4JXmlEventLayout();
	}
}

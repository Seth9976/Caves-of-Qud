using System;
using AiUnity.Common.Attributes;
using AiUnity.NLog.Core.Config;
using AiUnity.NLog.Core.Layouts;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.Targets
{
	// Token: 0x0200021F RID: 543
	[NLogConfigurationItem]
	[Preserve]
	public class MethodCallParameter
	{
		// Token: 0x0600141C RID: 5148 RVA: 0x00072A66 File Offset: 0x00070C66
		public MethodCallParameter()
		{
			this.Type = typeof(string);
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x00072A7E File Offset: 0x00070C7E
		public MethodCallParameter(Layout layout)
		{
			this.Type = typeof(string);
			this.Layout = layout;
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x00072A9D File Offset: 0x00070C9D
		public MethodCallParameter(string parameterName, Layout layout)
		{
			this.Type = typeof(string);
			this.Name = parameterName;
			this.Layout = layout;
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x00072AC3 File Offset: 0x00070CC3
		public MethodCallParameter(string name, Layout layout, Type type)
		{
			this.Type = type;
			this.Name = name;
			this.Layout = layout;
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06001420 RID: 5152 RVA: 0x00072AE0 File Offset: 0x00070CE0
		// (set) Token: 0x06001421 RID: 5153 RVA: 0x00072AE8 File Offset: 0x00070CE8
		public string Name { get; set; }

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06001422 RID: 5154 RVA: 0x00072AF1 File Offset: 0x00070CF1
		// (set) Token: 0x06001423 RID: 5155 RVA: 0x00072AF9 File Offset: 0x00070CF9
		public Type Type { get; set; }

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06001424 RID: 5156 RVA: 0x00072B02 File Offset: 0x00070D02
		// (set) Token: 0x06001425 RID: 5157 RVA: 0x00072B0A File Offset: 0x00070D0A
		[RequiredParameter]
		public Layout Layout { get; set; }

		// Token: 0x06001426 RID: 5158 RVA: 0x00072B13 File Offset: 0x00070D13
		internal object GetValue(LogEventInfo logEvent)
		{
			return Convert.ChangeType(this.Layout.Render(logEvent), this.Type);
		}
	}
}

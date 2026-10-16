using System;
using System.ComponentModel;
using AiUnity.Common.Attributes;
using AiUnity.NLog.Core.Layouts;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.Targets
{
	// Token: 0x02000229 RID: 553
	[Preserve]
	public abstract class TargetWithLayout : Target
	{
		// Token: 0x0600148A RID: 5258 RVA: 0x00073BF7 File Offset: 0x00071DF7
		protected TargetWithLayout()
		{
			this.Layout = "[${level}] ${callsite}${newline}${message}${exception}";
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x0600148B RID: 5259 RVA: 0x00073C0F File Offset: 0x00071E0F
		// (set) Token: 0x0600148C RID: 5260 RVA: 0x00073C17 File Offset: 0x00071E17
		[RequiredParameter]
		[Display("Layout", "Specifies the layout and content of log message.  The + icon will present a list of variables that can be added.  For the Unity Console all excepted xml formating can be used (http://docs.unity3d.com/Manual/StyledText.html)", false, -100)]
		[DefaultValue("[${level}] ${callsite}${newline}${message}${exception}")]
		public virtual Layout Layout { get; set; }

		// Token: 0x04000D36 RID: 3382
		private const string LayoutDefault = "[${level}] ${callsite}${newline}${message}${exception}";
	}
}

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using AiUnity.NLog.Core.Config;
using AiUnity.NLog.Core.Internal;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.LayoutRenderers
{
	// Token: 0x02000258 RID: 600
	[LayoutRenderer("stacktrace", false)]
	[ThreadAgnostic]
	[Preserve]
	public class StackTraceLayoutRenderer : LayoutRenderer, IUsesStackTrace
	{
		// Token: 0x060015E6 RID: 5606 RVA: 0x00077847 File Offset: 0x00075A47
		public StackTraceLayoutRenderer()
		{
			this.Separator = " => ";
			this.TopFrames = 3;
			this.Format = StackTraceFormat.Flat;
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x060015E7 RID: 5607 RVA: 0x00077868 File Offset: 0x00075A68
		// (set) Token: 0x060015E8 RID: 5608 RVA: 0x00077870 File Offset: 0x00075A70
		[DefaultValue("Flat")]
		public StackTraceFormat Format { get; set; }

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x060015E9 RID: 5609 RVA: 0x00077879 File Offset: 0x00075A79
		// (set) Token: 0x060015EA RID: 5610 RVA: 0x00077881 File Offset: 0x00075A81
		[DefaultValue(3)]
		public int TopFrames { get; set; }

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x060015EB RID: 5611 RVA: 0x0007788A File Offset: 0x00075A8A
		// (set) Token: 0x060015EC RID: 5612 RVA: 0x00077892 File Offset: 0x00075A92
		[DefaultValue(" => ")]
		public string Separator { get; set; }

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x060015ED RID: 5613 RVA: 0x0007789B File Offset: 0x00075A9B
		StackTraceUsage IUsesStackTrace.StackTraceUsage
		{
			get
			{
				return StackTraceUsage.WithoutSource;
			}
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x000778A0 File Offset: 0x00075AA0
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			bool flag = true;
			int num = logEvent.UserStackFrameNumber + this.TopFrames - 1;
			if (num >= logEvent.StackTrace.FrameCount)
			{
				num = logEvent.StackTrace.FrameCount - 1;
			}
			switch (this.Format)
			{
			case StackTraceFormat.Raw:
			{
				for (int i = num; i >= logEvent.UserStackFrameNumber; i--)
				{
					StackFrame frame = logEvent.StackTrace.GetFrame(i);
					builder.Append(frame.ToString());
				}
				return;
			}
			case StackTraceFormat.Flat:
			{
				for (int j = num; j >= logEvent.UserStackFrameNumber; j--)
				{
					StackFrame frame2 = logEvent.StackTrace.GetFrame(j);
					if (!flag)
					{
						builder.Append(this.Separator);
					}
					Type declaringType = frame2.GetMethod().DeclaringType;
					if (declaringType != null)
					{
						builder.Append(declaringType.Name);
					}
					else
					{
						builder.Append("<no type>");
					}
					builder.Append(".");
					builder.Append(frame2.GetMethod().Name);
					flag = false;
				}
				return;
			}
			case StackTraceFormat.DetailedFlat:
			{
				for (int k = num; k >= logEvent.UserStackFrameNumber; k--)
				{
					StackFrame frame3 = logEvent.StackTrace.GetFrame(k);
					if (!flag)
					{
						builder.Append(this.Separator);
					}
					builder.Append("[");
					builder.Append(frame3.GetMethod());
					builder.Append("]");
					flag = false;
				}
				return;
			}
			default:
				return;
			}
		}
	}
}

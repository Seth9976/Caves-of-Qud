using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using AiUnity.NLog.Core.Config;
using AiUnity.NLog.Core.Internal;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.LayoutRenderers
{
	// Token: 0x02000244 RID: 580
	[LayoutRenderer("callsite", false)]
	[ThreadAgnostic]
	[Preserve]
	public class CallSiteLayoutRenderer : LayoutRenderer, IUsesStackTrace
	{
		// Token: 0x06001549 RID: 5449 RVA: 0x00076001 File Offset: 0x00074201
		public CallSiteLayoutRenderer()
		{
			this.ClassName = true;
			this.MethodName = true;
			this.CleanNamesOfAnonymousDelegates = false;
			this.FileName = false;
			this.IncludeSourcePath = true;
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x0600154A RID: 5450 RVA: 0x0007602C File Offset: 0x0007422C
		// (set) Token: 0x0600154B RID: 5451 RVA: 0x00076034 File Offset: 0x00074234
		[DefaultValue(true)]
		public bool ClassName { get; set; }

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x0600154C RID: 5452 RVA: 0x0007603D File Offset: 0x0007423D
		// (set) Token: 0x0600154D RID: 5453 RVA: 0x00076045 File Offset: 0x00074245
		[DefaultValue(true)]
		public bool MethodName { get; set; }

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x0600154E RID: 5454 RVA: 0x0007604E File Offset: 0x0007424E
		// (set) Token: 0x0600154F RID: 5455 RVA: 0x00076056 File Offset: 0x00074256
		[DefaultValue(false)]
		public bool CleanNamesOfAnonymousDelegates { get; set; }

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06001550 RID: 5456 RVA: 0x0007605F File Offset: 0x0007425F
		// (set) Token: 0x06001551 RID: 5457 RVA: 0x00076067 File Offset: 0x00074267
		[DefaultValue(0)]
		public int SkipFrames { get; set; }

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06001552 RID: 5458 RVA: 0x00076070 File Offset: 0x00074270
		// (set) Token: 0x06001553 RID: 5459 RVA: 0x00076078 File Offset: 0x00074278
		[DefaultValue(false)]
		public bool FileName { get; set; }

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06001554 RID: 5460 RVA: 0x00076081 File Offset: 0x00074281
		// (set) Token: 0x06001555 RID: 5461 RVA: 0x00076089 File Offset: 0x00074289
		[DefaultValue(true)]
		public bool IncludeSourcePath { get; set; }

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06001556 RID: 5462 RVA: 0x00076092 File Offset: 0x00074292
		StackTraceUsage IUsesStackTrace.StackTraceUsage
		{
			get
			{
				if (this.FileName)
				{
					return StackTraceUsage.WithSource;
				}
				return StackTraceUsage.WithoutSource;
			}
		}

		// Token: 0x06001557 RID: 5463 RVA: 0x000760A0 File Offset: 0x000742A0
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			StackFrame stackFrame = ((logEvent.StackTrace != null) ? logEvent.StackTrace.GetFrame(logEvent.UserStackFrameNumber + this.SkipFrames) : null);
			if (stackFrame != null)
			{
				MethodBase method = stackFrame.GetMethod();
				if (this.ClassName)
				{
					if (method.DeclaringType != null)
					{
						string text = method.DeclaringType.FullName;
						if (this.CleanNamesOfAnonymousDelegates && text.Contains("+<>"))
						{
							int num = text.IndexOf("+<>");
							text = text.Substring(0, num);
						}
						builder.Append(text);
					}
					else
					{
						builder.Append("<no type>");
					}
				}
				if (this.MethodName)
				{
					if (this.ClassName)
					{
						builder.Append(".");
					}
					if (method != null)
					{
						string text2 = method.Name;
						if (this.CleanNamesOfAnonymousDelegates && text2.Contains("__") && text2.StartsWith("<") && text2.Contains(">"))
						{
							int num2 = text2.IndexOf('<') + 1;
							int num3 = text2.IndexOf('>');
							text2 = text2.Substring(num2, num3 - num2);
						}
						builder.Append(text2);
					}
					else
					{
						builder.Append("<no method>");
					}
				}
				if (this.FileName)
				{
					string fileName = stackFrame.GetFileName();
					if (fileName != null)
					{
						builder.Append("(");
						if (this.IncludeSourcePath)
						{
							builder.Append(fileName);
						}
						else
						{
							builder.Append(Path.GetFileName(fileName));
						}
						builder.Append(":");
						builder.Append(stackFrame.GetFileLineNumber());
						builder.Append(")");
					}
				}
			}
		}
	}
}

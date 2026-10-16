using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using AiUnity.Common.InternalLog;
using AiUnity.Common.Patterns;
using AiUnity.NLog.Core.Common;
using AiUnity.NLog.Core.Config;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.LayoutRenderers
{
	// Token: 0x02000249 RID: 585
	[LayoutRenderer("exception", false)]
	[ThreadAgnostic]
	[Preserve]
	public class ExceptionLayoutRenderer : LayoutRenderer
	{
		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06001574 RID: 5492 RVA: 0x000764BF File Offset: 0x000746BF
		private static IInternalLogger Logger
		{
			get
			{
				return Singleton<NLogInternalLogger>.Instance;
			}
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x000764C8 File Offset: 0x000746C8
		public ExceptionLayoutRenderer()
		{
			this.Format = (this.InnerFormat = "Header,Message,Method,Type,Stacktrace,Footer");
			this.Separator = Environment.NewLine;
			this.InnerExceptionSeparator = Environment.NewLine;
			this.MaxInnerExceptionLevel = 10;
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06001576 RID: 5494 RVA: 0x00076518 File Offset: 0x00074718
		// (set) Token: 0x06001577 RID: 5495 RVA: 0x00076520 File Offset: 0x00074720
		[DefaultParameter]
		public string Format
		{
			get
			{
				return this.format;
			}
			set
			{
				this.format = value;
				this.exceptionDataTargets = this.CompileFormat(value);
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06001578 RID: 5496 RVA: 0x00076536 File Offset: 0x00074736
		// (set) Token: 0x06001579 RID: 5497 RVA: 0x0007653E File Offset: 0x0007473E
		public string InnerFormat
		{
			get
			{
				return this.innerFormat;
			}
			set
			{
				this.innerFormat = value;
				this.innerExceptionDataTargets = this.CompileFormat(value);
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x0600157A RID: 5498 RVA: 0x00076554 File Offset: 0x00074754
		// (set) Token: 0x0600157B RID: 5499 RVA: 0x0007655C File Offset: 0x0007475C
		[DefaultValue("\n")]
		public string Separator { get; set; }

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x0600157C RID: 5500 RVA: 0x00076565 File Offset: 0x00074765
		// (set) Token: 0x0600157D RID: 5501 RVA: 0x0007656D File Offset: 0x0007476D
		[DefaultValue(0)]
		public int MaxInnerExceptionLevel { get; set; }

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x0600157E RID: 5502 RVA: 0x00076576 File Offset: 0x00074776
		// (set) Token: 0x0600157F RID: 5503 RVA: 0x0007657E File Offset: 0x0007477E
		public string InnerExceptionSeparator { get; set; }

		// Token: 0x06001580 RID: 5504 RVA: 0x00076588 File Offset: 0x00074788
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			if (logEvent.Exception != null)
			{
				StringBuilder stringBuilder = new StringBuilder(128);
				string text = string.Empty;
				ExceptionLayoutRenderer.ExceptionDataTarget[] array = this.exceptionDataTargets;
				for (int i = 0; i < array.Length; i++)
				{
					array[i](stringBuilder, logEvent.Exception);
					stringBuilder.Append(text);
					text = this.Separator;
				}
				Exception ex = logEvent.Exception.InnerException;
				int num = 0;
				while (ex != null && num < this.MaxInnerExceptionLevel)
				{
					text = string.Empty;
					array = this.innerExceptionDataTargets ?? this.exceptionDataTargets;
					for (int i = 0; i < array.Length; i++)
					{
						array[i](stringBuilder, ex);
						stringBuilder.Append(text);
						text = this.Separator;
					}
					ex = ex.InnerException;
					num++;
				}
				builder.Append(stringBuilder.ToString());
			}
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x00076668 File Offset: 0x00074868
		private ExceptionLayoutRenderer.ExceptionDataTarget[] CompileFormat(string formatSpecifier)
		{
			string[] array = formatSpecifier.Replace(" ", string.Empty).Split(',', StringSplitOptions.None);
			List<ExceptionLayoutRenderer.ExceptionDataTarget> list = new List<ExceptionLayoutRenderer.ExceptionDataTarget>();
			string[] array2 = array;
			int i = 0;
			while (i < array2.Length)
			{
				string text = array2[i];
				string text2 = text.ToUpper(CultureInfo.InvariantCulture);
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text2);
				if (num <= 1947902685U)
				{
					if (num <= 338683789U)
					{
						if (num != 48695600U)
						{
							if (num != 338683789U)
							{
								goto IL_0262;
							}
							if (!(text2 == "TYPE"))
							{
								goto IL_0262;
							}
							list.Add(new ExceptionLayoutRenderer.ExceptionDataTarget(this.AppendType));
						}
						else
						{
							if (!(text2 == "METHOD"))
							{
								goto IL_0262;
							}
							list.Add(new ExceptionLayoutRenderer.ExceptionDataTarget(this.AppendMethod));
						}
					}
					else if (num != 931238212U)
					{
						if (num != 1947902685U)
						{
							goto IL_0262;
						}
						if (!(text2 == "SHORTTYPE"))
						{
							goto IL_0262;
						}
						list.Add(new ExceptionLayoutRenderer.ExceptionDataTarget(this.AppendShortType));
					}
					else
					{
						if (!(text2 == "MESSAGE"))
						{
							goto IL_0262;
						}
						list.Add(new ExceptionLayoutRenderer.ExceptionDataTarget(this.AppendMessage));
					}
				}
				else if (num <= 2796732965U)
				{
					if (num != 2263853280U)
					{
						if (num != 2796732965U)
						{
							goto IL_0262;
						}
						if (!(text2 == "TOSTRING"))
						{
							goto IL_0262;
						}
						list.Add(new ExceptionLayoutRenderer.ExceptionDataTarget(this.AppendToString));
					}
					else
					{
						if (!(text2 == "HEADER"))
						{
							goto IL_0262;
						}
						list.Add(new ExceptionLayoutRenderer.ExceptionDataTarget(this.AppendHeader));
					}
				}
				else if (num != 2935185010U)
				{
					if (num != 3288857317U)
					{
						if (num != 4209261660U)
						{
							goto IL_0262;
						}
						if (!(text2 == "STACKTRACE"))
						{
							goto IL_0262;
						}
						list.Add(new ExceptionLayoutRenderer.ExceptionDataTarget(this.AppendStackTrace));
					}
					else
					{
						if (!(text2 == "DATA"))
						{
							goto IL_0262;
						}
						list.Add(new ExceptionLayoutRenderer.ExceptionDataTarget(this.AppendData));
					}
				}
				else
				{
					if (!(text2 == "FOOTER"))
					{
						goto IL_0262;
					}
					list.Add(new ExceptionLayoutRenderer.ExceptionDataTarget(this.AppendFooter));
				}
				IL_027B:
				i++;
				continue;
				IL_0262:
				ExceptionLayoutRenderer.Logger.Warn("Unknown exception data target: {0}", new object[] { text });
				goto IL_027B;
			}
			return list.ToArray();
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x00076904 File Offset: 0x00074B04
		private void AppendHeader(StringBuilder sb, Exception ex)
		{
			string text = string.Format("Exception: {0}{1}", ex.GetType().FullName, Environment.NewLine);
			string text2 = new string('*', text.Length);
			sb.Append(Environment.NewLine + text2 + Environment.NewLine);
			sb.Append(text);
			sb.Append(text2 + Environment.NewLine);
		}

		// Token: 0x06001583 RID: 5507 RVA: 0x0007696C File Offset: 0x00074B6C
		private void AppendFooter(StringBuilder sb, Exception ex)
		{
			string text = string.Format("Exception: {0}{1}", ex.GetType().FullName, Environment.NewLine);
			string text2 = new string('*', text.Length);
			sb.Append(text2);
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x000769AC File Offset: 0x00074BAC
		protected virtual void AppendMessage(StringBuilder sb, Exception ex)
		{
			try
			{
				sb.AppendFormat("{0,-11} {1}", "Message:", ex.Message);
			}
			catch (Exception ex2)
			{
				string text = string.Format("Exception in {0}.AppendMessage(): {1}.", typeof(ExceptionLayoutRenderer).FullName, ex2.GetType().FullName);
				sb.Append("NLog message:" + text);
				if (ExceptionLayoutRenderer.Logger.IsWarnEnabled)
				{
					ExceptionLayoutRenderer.Logger.Warn(text, Array.Empty<object>());
				}
			}
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x00076A38 File Offset: 0x00074C38
		protected virtual void AppendMethod(StringBuilder sb, Exception ex)
		{
			if (ex.TargetSite != null)
			{
				sb.AppendFormat("{0,-12} {1}", "Method:", ex.TargetSite.ToString());
			}
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x00076A64 File Offset: 0x00074C64
		protected virtual void AppendStackTrace(StringBuilder sb, Exception ex)
		{
			sb.AppendFormat("{1}Stack Trace:{1}{0}", ex.StackTrace, Environment.NewLine);
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x00076A7D File Offset: 0x00074C7D
		protected virtual void AppendToString(StringBuilder sb, Exception ex)
		{
			sb.Append(ex.ToString());
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x00076A8C File Offset: 0x00074C8C
		protected virtual void AppendType(StringBuilder sb, Exception ex)
		{
			sb.AppendFormat("{0,-13} {1}", "Type:", ex.GetType().FullName);
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x00076AAA File Offset: 0x00074CAA
		protected virtual void AppendShortType(StringBuilder sb, Exception ex)
		{
			sb.Append(ex.GetType().Name);
		}

		// Token: 0x0600158A RID: 5514 RVA: 0x00076AC0 File Offset: 0x00074CC0
		protected virtual void AppendData(StringBuilder sb, Exception ex)
		{
			string text = string.Empty;
			foreach (object obj in ex.Data.Keys)
			{
				sb.Append(text);
				sb.AppendFormat("{0}: {1}", obj, ex.Data[obj]);
				text = ";";
			}
		}

		// Token: 0x04000D95 RID: 3477
		private string format;

		// Token: 0x04000D96 RID: 3478
		private string innerFormat = string.Empty;

		// Token: 0x04000D97 RID: 3479
		private ExceptionLayoutRenderer.ExceptionDataTarget[] exceptionDataTargets;

		// Token: 0x04000D98 RID: 3480
		private ExceptionLayoutRenderer.ExceptionDataTarget[] innerExceptionDataTargets;

		// Token: 0x02001672 RID: 5746
		// (Invoke) Token: 0x0600D819 RID: 55321
		private delegate void ExceptionDataTarget(StringBuilder sb, Exception ex);
	}
}

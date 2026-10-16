using System;
using System.Collections.ObjectModel;
using System.Text;
using AiUnity.Common.InternalLog;
using AiUnity.Common.Patterns;
using AiUnity.NLog.Core.Common;
using AiUnity.NLog.Core.Config;
using AiUnity.NLog.Core.Internal;
using AiUnity.NLog.Core.LayoutRenderers;

namespace AiUnity.NLog.Core.Layouts
{
	// Token: 0x02000242 RID: 578
	[Layout("SimpleLayout")]
	[ThreadAgnostic]
	[AppDomainFixedOutput]
	public class SimpleLayout : Layout
	{
		// Token: 0x06001538 RID: 5432 RVA: 0x00075DA5 File Offset: 0x00073FA5
		public SimpleLayout()
			: this(string.Empty)
		{
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x00075DB2 File Offset: 0x00073FB2
		public SimpleLayout(string txt)
			: this(txt, ConfigurationItemFactory.Default)
		{
		}

		// Token: 0x0600153A RID: 5434 RVA: 0x00075DC0 File Offset: 0x00073FC0
		public SimpleLayout(string txt, ConfigurationItemFactory configurationItemFactory)
		{
			this.configurationItemFactory = configurationItemFactory;
			this.Text = txt;
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x00075DD6 File Offset: 0x00073FD6
		internal SimpleLayout(LayoutRenderer[] renderers, string text, ConfigurationItemFactory configurationItemFactory)
		{
			this.configurationItemFactory = configurationItemFactory;
			this.SetRenderers(renderers, text);
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x0600153C RID: 5436 RVA: 0x00075DED File Offset: 0x00073FED
		private static IInternalLogger Logger
		{
			get
			{
				return Singleton<NLogInternalLogger>.Instance;
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x0600153D RID: 5437 RVA: 0x00075DF4 File Offset: 0x00073FF4
		// (set) Token: 0x0600153E RID: 5438 RVA: 0x00075DFC File Offset: 0x00073FFC
		public string Text
		{
			get
			{
				return this.layoutText;
			}
			set
			{
				string text;
				LayoutRenderer[] array = LayoutParser.CompileLayout(this.configurationItemFactory, new SimpleStringReader(value), false, out text);
				this.SetRenderers(array, text);
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x0600153F RID: 5439 RVA: 0x00075E26 File Offset: 0x00074026
		// (set) Token: 0x06001540 RID: 5440 RVA: 0x00075E2E File Offset: 0x0007402E
		public ReadOnlyCollection<LayoutRenderer> Renderers { get; private set; }

		// Token: 0x06001541 RID: 5441 RVA: 0x00075E37 File Offset: 0x00074037
		public new static implicit operator SimpleLayout(string text)
		{
			return new SimpleLayout(text);
		}

		// Token: 0x06001542 RID: 5442 RVA: 0x00075E3F File Offset: 0x0007403F
		public static string Escape(string text)
		{
			return text.Replace("${", "${literal:text=${}");
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x00075E51 File Offset: 0x00074051
		public static string Evaluate(string text, LogEventInfo logEvent)
		{
			return new SimpleLayout(text).Render(logEvent);
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x00075E5F File Offset: 0x0007405F
		public static string Evaluate(string text)
		{
			return SimpleLayout.Evaluate(text, LogEventInfo.CreateNullEvent());
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x00075E6C File Offset: 0x0007406C
		public override string ToString()
		{
			return "'" + this.Text + "'";
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x00075E84 File Offset: 0x00074084
		internal void SetRenderers(LayoutRenderer[] renderers, string text)
		{
			this.Renderers = new ReadOnlyCollection<LayoutRenderer>(renderers);
			if (this.Renderers.Count == 1 && this.Renderers[0] is LiteralLayoutRenderer)
			{
				this.fixedText = ((LiteralLayoutRenderer)this.Renderers[0]).Text;
			}
			else
			{
				this.fixedText = null;
			}
			this.layoutText = text;
		}

		// Token: 0x06001547 RID: 5447 RVA: 0x00075EEC File Offset: 0x000740EC
		protected override string GetFormattedMessage(LogEventInfo logEvent)
		{
			if (this.fixedText != null)
			{
				return this.fixedText;
			}
			string text;
			if (logEvent.TryGetCachedLayoutValue(this, out text))
			{
				return text;
			}
			int num = this.maxRenderedLength;
			if (num > 16384)
			{
				num = 16384;
			}
			StringBuilder stringBuilder = new StringBuilder(num);
			foreach (LayoutRenderer layoutRenderer in this.Renderers)
			{
				try
				{
					layoutRenderer.Render(stringBuilder, logEvent);
				}
				catch (Exception ex)
				{
					if (ex.MustBeRethrown())
					{
						throw;
					}
					if (SimpleLayout.Logger.IsWarnEnabled)
					{
						SimpleLayout.Logger.Warn("Exception in {0}.Append(): {1}.", new object[]
						{
							layoutRenderer.GetType().FullName,
							ex
						});
					}
				}
			}
			if (stringBuilder.Length > this.maxRenderedLength)
			{
				this.maxRenderedLength = stringBuilder.Length;
			}
			string text2 = stringBuilder.ToString();
			logEvent.AddCachedLayoutValue(this, text2);
			return text2;
		}

		// Token: 0x04000D7F RID: 3455
		private const int MaxInitialRenderBufferLength = 16384;

		// Token: 0x04000D80 RID: 3456
		private int maxRenderedLength;

		// Token: 0x04000D81 RID: 3457
		private string fixedText;

		// Token: 0x04000D82 RID: 3458
		private string layoutText;

		// Token: 0x04000D83 RID: 3459
		private ConfigurationItemFactory configurationItemFactory;
	}
}

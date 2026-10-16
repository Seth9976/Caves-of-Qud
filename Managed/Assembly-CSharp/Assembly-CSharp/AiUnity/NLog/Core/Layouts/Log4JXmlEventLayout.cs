using System;
using AiUnity.NLog.Core.LayoutRenderers;

namespace AiUnity.NLog.Core.Layouts
{
	// Token: 0x02000241 RID: 577
	[Layout("Log4JXmlEventLayout")]
	public class Log4JXmlEventLayout : Layout
	{
		// Token: 0x06001534 RID: 5428 RVA: 0x00075D53 File Offset: 0x00073F53
		public Log4JXmlEventLayout()
		{
			this.Renderer = new Log4JXmlEventLayoutRenderer();
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06001535 RID: 5429 RVA: 0x00075D66 File Offset: 0x00073F66
		// (set) Token: 0x06001536 RID: 5430 RVA: 0x00075D6E File Offset: 0x00073F6E
		public Log4JXmlEventLayoutRenderer Renderer { get; private set; }

		// Token: 0x06001537 RID: 5431 RVA: 0x00075D78 File Offset: 0x00073F78
		protected override string GetFormattedMessage(LogEventInfo logEvent)
		{
			string text;
			if (logEvent.TryGetCachedLayoutValue(this, out text))
			{
				return text;
			}
			return logEvent.AddCachedLayoutValue(this, this.Renderer.Render(logEvent));
		}
	}
}

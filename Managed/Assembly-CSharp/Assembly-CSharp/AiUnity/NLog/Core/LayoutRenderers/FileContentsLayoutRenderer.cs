using System;
using System.IO;
using System.Text;
using AiUnity.Common.InternalLog;
using AiUnity.Common.Patterns;
using AiUnity.NLog.Core.Common;
using AiUnity.NLog.Core.Config;
using AiUnity.NLog.Core.Internal;
using AiUnity.NLog.Core.Layouts;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.LayoutRenderers
{
	// Token: 0x0200024A RID: 586
	[LayoutRenderer("file-contents", false)]
	[Preserve]
	public class FileContentsLayoutRenderer : LayoutRenderer
	{
		// Token: 0x0600158B RID: 5515 RVA: 0x00076B40 File Offset: 0x00074D40
		public FileContentsLayoutRenderer()
		{
			this.Encoding = Encoding.Default;
			this.lastFileName = string.Empty;
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x0600158C RID: 5516 RVA: 0x00076B5E File Offset: 0x00074D5E
		private static IInternalLogger Logger
		{
			get
			{
				return Singleton<NLogInternalLogger>.Instance;
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x0600158D RID: 5517 RVA: 0x00076B65 File Offset: 0x00074D65
		// (set) Token: 0x0600158E RID: 5518 RVA: 0x00076B6D File Offset: 0x00074D6D
		[DefaultParameter]
		public Layout FileName { get; set; }

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x0600158F RID: 5519 RVA: 0x00076B76 File Offset: 0x00074D76
		// (set) Token: 0x06001590 RID: 5520 RVA: 0x00076B7E File Offset: 0x00074D7E
		public Encoding Encoding { get; set; }

		// Token: 0x06001591 RID: 5521 RVA: 0x00076B88 File Offset: 0x00074D88
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			lock (this)
			{
				string text = this.FileName.Render(logEvent);
				if (text != this.lastFileName)
				{
					this.currentFileContents = this.ReadFileContents(text);
					this.lastFileName = text;
				}
			}
			builder.Append(this.currentFileContents);
		}

		// Token: 0x06001592 RID: 5522 RVA: 0x00076BFC File Offset: 0x00074DFC
		private string ReadFileContents(string fileName)
		{
			string text;
			try
			{
				using (StreamReader streamReader = new StreamReader(fileName, this.Encoding))
				{
					text = streamReader.ReadToEnd();
				}
			}
			catch (Exception ex)
			{
				if (ex.MustBeRethrown())
				{
					throw;
				}
				FileContentsLayoutRenderer.Logger.Error("Cannot read file contents: {0} {1}", new object[] { fileName, ex });
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x04000D9C RID: 3484
		private string lastFileName;

		// Token: 0x04000D9D RID: 3485
		private string currentFileContents;
	}
}

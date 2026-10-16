using System;
using System.IO;
using System.Text;

namespace Ionic.Zip
{
	// Token: 0x02000038 RID: 56
	public class ReadOptions
	{
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0000F705 File Offset: 0x0000D905
		// (set) Token: 0x06000285 RID: 645 RVA: 0x0000F70D File Offset: 0x0000D90D
		public EventHandler<ReadProgressEventArgs> ReadProgress { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000286 RID: 646 RVA: 0x0000F716 File Offset: 0x0000D916
		// (set) Token: 0x06000287 RID: 647 RVA: 0x0000F71E File Offset: 0x0000D91E
		public TextWriter StatusMessageWriter { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000288 RID: 648 RVA: 0x0000F727 File Offset: 0x0000D927
		// (set) Token: 0x06000289 RID: 649 RVA: 0x0000F72F File Offset: 0x0000D92F
		public Encoding Encoding { get; set; }
	}
}

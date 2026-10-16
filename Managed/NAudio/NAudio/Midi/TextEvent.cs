using System;
using System.IO;
using System.Text;
using NAudio.Utils;

namespace NAudio.Midi
{
	// Token: 0x020000F0 RID: 240
	public class TextEvent : MetaEvent
	{
		// Token: 0x06000566 RID: 1382 RVA: 0x0001181C File Offset: 0x0000FA1C
		public TextEvent(BinaryReader br, int length)
		{
			Encoding instance = ByteEncoding.Instance;
			this.text = instance.GetString(br.ReadBytes(length));
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00011848 File Offset: 0x0000FA48
		public TextEvent(string text, MetaEventType metaEventType, long absoluteTime)
			: base(metaEventType, text.Length, absoluteTime)
		{
			this.text = text;
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x0001185F File Offset: 0x0000FA5F
		// (set) Token: 0x06000569 RID: 1385 RVA: 0x00011867 File Offset: 0x0000FA67
		public string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
				this.metaDataLength = this.text.Length;
			}
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00011881 File Offset: 0x0000FA81
		public override string ToString()
		{
			return string.Format("{0} {1}", base.ToString(), this.text);
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0001189C File Offset: 0x0000FA9C
		public override void Export(ref long absoluteTime, BinaryWriter writer)
		{
			base.Export(ref absoluteTime, writer);
			Encoding instance = ByteEncoding.Instance;
			byte[] bytes = instance.GetBytes(this.text);
			writer.Write(bytes);
		}

		// Token: 0x040005D8 RID: 1496
		private string text;
	}
}

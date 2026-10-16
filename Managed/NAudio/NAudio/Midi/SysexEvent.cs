using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NAudio.Midi
{
	// Token: 0x020000EE RID: 238
	public class SysexEvent : MidiEvent
	{
		// Token: 0x0600055A RID: 1370 RVA: 0x00011638 File Offset: 0x0000F838
		public static SysexEvent ReadSysexEvent(BinaryReader br)
		{
			SysexEvent sysexEvent = new SysexEvent();
			List<byte> list = new List<byte>();
			bool flag = true;
			while (flag)
			{
				byte b = br.ReadByte();
				if (b == 247)
				{
					flag = false;
				}
				else
				{
					list.Add(b);
				}
			}
			sysexEvent.data = list.ToArray();
			return sysexEvent;
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00011680 File Offset: 0x0000F880
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in this.data)
			{
				stringBuilder.AppendFormat("{0:X2} ", b);
			}
			return string.Format("{0} Sysex: {1} bytes\r\n{2}", base.AbsoluteTime, this.data.Length, stringBuilder.ToString());
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x000116E6 File Offset: 0x0000F8E6
		public override void Export(ref long absoluteTime, BinaryWriter writer)
		{
			base.Export(ref absoluteTime, writer);
			writer.Write(this.data, 0, this.data.Length);
			writer.Write(247);
		}

		// Token: 0x040005D6 RID: 1494
		private byte[] data;
	}
}

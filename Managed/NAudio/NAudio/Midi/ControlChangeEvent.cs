using System;
using System.IO;

namespace NAudio.Midi
{
	// Token: 0x020000CF RID: 207
	public class ControlChangeEvent : MidiEvent
	{
		// Token: 0x0600048C RID: 1164 RVA: 0x0000F2F4 File Offset: 0x0000D4F4
		public ControlChangeEvent(BinaryReader br)
		{
			byte b = br.ReadByte();
			this.controllerValue = br.ReadByte();
			if ((b & 128) != 0)
			{
				throw new InvalidDataException("Invalid controller");
			}
			this.controller = (MidiController)b;
			if ((this.controllerValue & 128) != 0)
			{
				throw new InvalidDataException(string.Format("Invalid controllerValue {0} for controller {1}, Pos 0x{2:X}", this.controllerValue, this.controller, br.BaseStream.Position));
			}
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0000F379 File Offset: 0x0000D579
		public ControlChangeEvent(long absoluteTime, int channel, MidiController controller, int controllerValue)
			: base(absoluteTime, channel, MidiCommandCode.ControlChange)
		{
			this.Controller = controller;
			this.ControllerValue = controllerValue;
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0000F397 File Offset: 0x0000D597
		public override string ToString()
		{
			return string.Format("{0} Controller {1} Value {2}", base.ToString(), this.controller, this.controllerValue);
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0000F3C0 File Offset: 0x0000D5C0
		public override int GetAsShortMessage()
		{
			byte b = (byte)this.controller;
			return base.GetAsShortMessage() + ((int)b << 8) + ((int)this.controllerValue << 16);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0000F3E8 File Offset: 0x0000D5E8
		public override void Export(ref long absoluteTime, BinaryWriter writer)
		{
			base.Export(ref absoluteTime, writer);
			writer.Write((byte)this.controller);
			writer.Write(this.controllerValue);
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x0000F40A File Offset: 0x0000D60A
		// (set) Token: 0x06000492 RID: 1170 RVA: 0x0000F412 File Offset: 0x0000D612
		public MidiController Controller
		{
			get
			{
				return this.controller;
			}
			set
			{
				if (value < MidiController.BankSelect || value > (MidiController)127)
				{
					throw new ArgumentOutOfRangeException("value", "Controller number must be in the range 0-127");
				}
				this.controller = value;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x0000F434 File Offset: 0x0000D634
		// (set) Token: 0x06000494 RID: 1172 RVA: 0x0000F43C File Offset: 0x0000D63C
		public int ControllerValue
		{
			get
			{
				return (int)this.controllerValue;
			}
			set
			{
				if (value < 0 || value > 127)
				{
					throw new ArgumentOutOfRangeException("value", "Controller Value must be in the range 0-127");
				}
				this.controllerValue = (byte)value;
			}
		}

		// Token: 0x04000542 RID: 1346
		private MidiController controller;

		// Token: 0x04000543 RID: 1347
		private byte controllerValue;
	}
}

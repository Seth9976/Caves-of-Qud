using System;
using System.IO;
using System.Text;

namespace NAudio.SoundFont
{
	// Token: 0x020000C0 RID: 192
	internal class PresetBuilder : StructureBuilder<Preset>
	{
		// Token: 0x06000435 RID: 1077 RVA: 0x0000DF34 File Offset: 0x0000C134
		public override Preset Read(BinaryReader br)
		{
			Preset preset = new Preset();
			string text = Encoding.UTF8.GetString(br.ReadBytes(20), 0, 20);
			if (text.IndexOf('\0') >= 0)
			{
				text = text.Substring(0, text.IndexOf('\0'));
			}
			preset.Name = text;
			preset.PatchNumber = br.ReadUInt16();
			preset.Bank = br.ReadUInt16();
			preset.startPresetZoneIndex = br.ReadUInt16();
			preset.library = br.ReadUInt32();
			preset.genre = br.ReadUInt32();
			preset.morphology = br.ReadUInt32();
			if (this.lastPreset != null)
			{
				this.lastPreset.endPresetZoneIndex = preset.startPresetZoneIndex - 1;
			}
			this.data.Add(preset);
			this.lastPreset = preset;
			return preset;
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x0000DFF5 File Offset: 0x0000C1F5
		public override void Write(BinaryWriter bw, Preset preset)
		{
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x0000DFF7 File Offset: 0x0000C1F7
		public override int Length
		{
			get
			{
				return 38;
			}
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0000DFFC File Offset: 0x0000C1FC
		public void LoadZones(Zone[] presetZones)
		{
			for (int i = 0; i < this.data.Count - 1; i++)
			{
				Preset preset = this.data[i];
				preset.Zones = new Zone[(int)(preset.endPresetZoneIndex - preset.startPresetZoneIndex + 1)];
				Array.Copy(presetZones, (int)preset.startPresetZoneIndex, preset.Zones, 0, preset.Zones.Length);
			}
			this.data.RemoveAt(this.data.Count - 1);
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x0000E07B File Offset: 0x0000C27B
		public Preset[] Presets
		{
			get
			{
				return this.data.ToArray();
			}
		}

		// Token: 0x0400050A RID: 1290
		private Preset lastPreset;
	}
}

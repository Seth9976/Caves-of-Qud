using System;
using System.IO;

namespace NAudio.Midi
{
	// Token: 0x020000EA RID: 234
	public class PatchChangeEvent : MidiEvent
	{
		// Token: 0x0600053C RID: 1340 RVA: 0x00010DA7 File Offset: 0x0000EFA7
		public static string GetPatchName(int patchNumber)
		{
			return PatchChangeEvent.patchNames[patchNumber];
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x00010DB0 File Offset: 0x0000EFB0
		public PatchChangeEvent(BinaryReader br)
		{
			this.patch = br.ReadByte();
			if ((this.patch & 128) != 0)
			{
				throw new FormatException("Invalid patch");
			}
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00010DDD File Offset: 0x0000EFDD
		public PatchChangeEvent(long absoluteTime, int channel, int patchNumber)
			: base(absoluteTime, channel, MidiCommandCode.PatchChange)
		{
			this.Patch = patchNumber;
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x00010DF3 File Offset: 0x0000EFF3
		// (set) Token: 0x06000540 RID: 1344 RVA: 0x00010DFB File Offset: 0x0000EFFB
		public int Patch
		{
			get
			{
				return (int)this.patch;
			}
			set
			{
				if (value < 0 || value > 127)
				{
					throw new ArgumentOutOfRangeException("value", "Patch number must be in the range 0-127");
				}
				this.patch = (byte)value;
			}
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x00010E1E File Offset: 0x0000F01E
		public override string ToString()
		{
			return string.Format("{0} {1}", base.ToString(), PatchChangeEvent.GetPatchName((int)this.patch));
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x00010E3B File Offset: 0x0000F03B
		public override int GetAsShortMessage()
		{
			return base.GetAsShortMessage() + ((int)this.patch << 8);
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00010E4C File Offset: 0x0000F04C
		public override void Export(ref long absoluteTime, BinaryWriter writer)
		{
			base.Export(ref absoluteTime, writer);
			writer.Write(this.patch);
		}

		// Token: 0x040005CD RID: 1485
		private byte patch;

		// Token: 0x040005CE RID: 1486
		private static readonly string[] patchNames = new string[]
		{
			"Acoustic Grand", "Bright Acoustic", "Electric Grand", "Honky-Tonk", "Electric Piano 1", "Electric Piano 2", "Harpsichord", "Clav", "Celesta", "Glockenspiel",
			"Music Box", "Vibraphone", "Marimba", "Xylophone", "Tubular Bells", "Dulcimer", "Drawbar Organ", "Percussive Organ", "Rock Organ", "Church Organ",
			"Reed Organ", "Accoridan", "Harmonica", "Tango Accordian", "Acoustic Guitar(nylon)", "Acoustic Guitar(steel)", "Electric Guitar(jazz)", "Electric Guitar(clean)", "Electric Guitar(muted)", "Overdriven Guitar",
			"Distortion Guitar", "Guitar Harmonics", "Acoustic Bass", "Electric Bass(finger)", "Electric Bass(pick)", "Fretless Bass", "Slap Bass 1", "Slap Bass 2", "Synth Bass 1", "Synth Bass 2",
			"Violin", "Viola", "Cello", "Contrabass", "Tremolo Strings", "Pizzicato Strings", "Orchestral Strings", "Timpani", "String Ensemble 1", "String Ensemble 2",
			"SynthStrings 1", "SynthStrings 2", "Choir Aahs", "Voice Oohs", "Synth Voice", "Orchestra Hit", "Trumpet", "Trombone", "Tuba", "Muted Trumpet",
			"French Horn", "Brass Section", "SynthBrass 1", "SynthBrass 2", "Soprano Sax", "Alto Sax", "Tenor Sax", "Baritone Sax", "Oboe", "English Horn",
			"Bassoon", "Clarinet", "Piccolo", "Flute", "Recorder", "Pan Flute", "Blown Bottle", "Skakuhachi", "Whistle", "Ocarina",
			"Lead 1 (square)", "Lead 2 (sawtooth)", "Lead 3 (calliope)", "Lead 4 (chiff)", "Lead 5 (charang)", "Lead 6 (voice)", "Lead 7 (fifths)", "Lead 8 (bass+lead)", "Pad 1 (new age)", "Pad 2 (warm)",
			"Pad 3 (polysynth)", "Pad 4 (choir)", "Pad 5 (bowed)", "Pad 6 (metallic)", "Pad 7 (halo)", "Pad 8 (sweep)", "FX 1 (rain)", "FX 2 (soundtrack)", "FX 3 (crystal)", "FX 4 (atmosphere)",
			"FX 5 (brightness)", "FX 6 (goblins)", "FX 7 (echoes)", "FX 8 (sci-fi)", "Sitar", "Banjo", "Shamisen", "Koto", "Kalimba", "Bagpipe",
			"Fiddle", "Shanai", "Tinkle Bell", "Agogo", "Steel Drums", "Woodblock", "Taiko Drum", "Melodic Tom", "Synth Drum", "Reverse Cymbal",
			"Guitar Fret Noise", "Breath Noise", "Seashore", "Bird Tweet", "Telephone Ring", "Helicopter", "Applause", "Gunshot"
		};
	}
}

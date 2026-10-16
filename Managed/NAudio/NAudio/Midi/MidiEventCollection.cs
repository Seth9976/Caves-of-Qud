using System;
using System.Collections;
using System.Collections.Generic;
using NAudio.Utils;

namespace NAudio.Midi
{
	// Token: 0x020000D5 RID: 213
	public class MidiEventCollection : IEnumerable<IList<MidiEvent>>, IEnumerable
	{
		// Token: 0x060004A1 RID: 1185 RVA: 0x0000F705 File Offset: 0x0000D905
		public MidiEventCollection(int midiFileType, int deltaTicksPerQuarterNote)
		{
			this.midiFileType = midiFileType;
			this.deltaTicksPerQuarterNote = deltaTicksPerQuarterNote;
			this.startAbsoluteTime = 0L;
			this.trackEvents = new List<IList<MidiEvent>>();
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x0000F72E File Offset: 0x0000D92E
		public int Tracks
		{
			get
			{
				return this.trackEvents.Count;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x0000F73B File Offset: 0x0000D93B
		// (set) Token: 0x060004A4 RID: 1188 RVA: 0x0000F743 File Offset: 0x0000D943
		public long StartAbsoluteTime
		{
			get
			{
				return this.startAbsoluteTime;
			}
			set
			{
				this.startAbsoluteTime = value;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x0000F74C File Offset: 0x0000D94C
		public int DeltaTicksPerQuarterNote
		{
			get
			{
				return this.deltaTicksPerQuarterNote;
			}
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0000F754 File Offset: 0x0000D954
		public IList<MidiEvent> GetTrackEvents(int trackNumber)
		{
			return this.trackEvents[trackNumber];
		}

		// Token: 0x170000ED RID: 237
		public IList<MidiEvent> this[int trackNumber]
		{
			get
			{
				return this.trackEvents[trackNumber];
			}
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x0000F770 File Offset: 0x0000D970
		public IList<MidiEvent> AddTrack()
		{
			return this.AddTrack(null);
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0000F77C File Offset: 0x0000D97C
		public IList<MidiEvent> AddTrack(IList<MidiEvent> initialEvents)
		{
			List<MidiEvent> list = new List<MidiEvent>();
			if (initialEvents != null)
			{
				list.AddRange(initialEvents);
			}
			this.trackEvents.Add(list);
			return list;
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0000F7A6 File Offset: 0x0000D9A6
		public void RemoveTrack(int track)
		{
			this.trackEvents.RemoveAt(track);
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0000F7B4 File Offset: 0x0000D9B4
		public void Clear()
		{
			this.trackEvents.Clear();
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x0000F7C1 File Offset: 0x0000D9C1
		// (set) Token: 0x060004AD RID: 1197 RVA: 0x0000F7C9 File Offset: 0x0000D9C9
		public int MidiFileType
		{
			get
			{
				return this.midiFileType;
			}
			set
			{
				if (this.midiFileType != value)
				{
					this.midiFileType = value;
					if (value == 0)
					{
						this.FlattenToOneTrack();
						return;
					}
					this.ExplodeToManyTracks();
				}
			}
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0000F7EC File Offset: 0x0000D9EC
		public void AddEvent(MidiEvent midiEvent, int originalTrack)
		{
			if (this.midiFileType == 0)
			{
				this.EnsureTracks(1);
				this.trackEvents[0].Add(midiEvent);
				return;
			}
			if (originalTrack == 0)
			{
				MidiCommandCode commandCode = midiEvent.CommandCode;
				if (commandCode <= MidiCommandCode.KeyAfterTouch)
				{
					if (commandCode != MidiCommandCode.NoteOff && commandCode != MidiCommandCode.NoteOn && commandCode != MidiCommandCode.KeyAfterTouch)
					{
						goto IL_00A1;
					}
				}
				else if (commandCode <= MidiCommandCode.PatchChange)
				{
					if (commandCode != MidiCommandCode.ControlChange && commandCode != MidiCommandCode.PatchChange)
					{
						goto IL_00A1;
					}
				}
				else if (commandCode != MidiCommandCode.ChannelAfterTouch && commandCode != MidiCommandCode.PitchWheelChange)
				{
					goto IL_00A1;
				}
				this.EnsureTracks(midiEvent.Channel + 1);
				this.trackEvents[midiEvent.Channel].Add(midiEvent);
				return;
				IL_00A1:
				this.EnsureTracks(1);
				this.trackEvents[0].Add(midiEvent);
				return;
			}
			this.EnsureTracks(originalTrack + 1);
			this.trackEvents[originalTrack].Add(midiEvent);
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0000F8D0 File Offset: 0x0000DAD0
		private void EnsureTracks(int count)
		{
			for (int i = this.trackEvents.Count; i < count; i++)
			{
				this.trackEvents.Add(new List<MidiEvent>());
			}
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0000F904 File Offset: 0x0000DB04
		private void ExplodeToManyTracks()
		{
			IList<MidiEvent> list = this.trackEvents[0];
			this.Clear();
			foreach (MidiEvent midiEvent in list)
			{
				this.AddEvent(midiEvent, 0);
			}
			this.PrepareForExport();
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0000F968 File Offset: 0x0000DB68
		private void FlattenToOneTrack()
		{
			bool flag = false;
			for (int i = 1; i < this.trackEvents.Count; i++)
			{
				foreach (MidiEvent midiEvent in this.trackEvents[i])
				{
					if (!MidiEvent.IsEndTrack(midiEvent))
					{
						this.trackEvents[0].Add(midiEvent);
						flag = true;
					}
				}
			}
			for (int j = this.trackEvents.Count - 1; j > 0; j--)
			{
				this.RemoveTrack(j);
			}
			if (flag)
			{
				this.PrepareForExport();
			}
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0000FA18 File Offset: 0x0000DC18
		public void PrepareForExport()
		{
			MidiEventComparer midiEventComparer = new MidiEventComparer();
			foreach (IList<MidiEvent> list in this.trackEvents)
			{
				List<MidiEvent> list2 = (List<MidiEvent>)list;
				MergeSort.Sort<MidiEvent>(list2, midiEventComparer);
				int i = 0;
				while (i < list2.Count - 1)
				{
					if (MidiEvent.IsEndTrack(list2[i]))
					{
						list2.RemoveAt(i);
					}
					else
					{
						i++;
					}
				}
			}
			int j = 0;
			while (j < this.trackEvents.Count)
			{
				IList<MidiEvent> list3 = this.trackEvents[j];
				if (list3.Count == 0)
				{
					this.RemoveTrack(j);
				}
				else if (list3.Count == 1 && MidiEvent.IsEndTrack(list3[0]))
				{
					this.RemoveTrack(j);
				}
				else
				{
					if (!MidiEvent.IsEndTrack(list3[list3.Count - 1]))
					{
						list3.Add(new MetaEvent(MetaEventType.EndTrack, 0, list3[list3.Count - 1].AbsoluteTime));
					}
					j++;
				}
			}
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0000FB3C File Offset: 0x0000DD3C
		public IEnumerator<IList<MidiEvent>> GetEnumerator()
		{
			return this.trackEvents.GetEnumerator();
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0000FB4E File Offset: 0x0000DD4E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.trackEvents.GetEnumerator();
		}

		// Token: 0x0400057C RID: 1404
		private int midiFileType;

		// Token: 0x0400057D RID: 1405
		private List<IList<MidiEvent>> trackEvents;

		// Token: 0x0400057E RID: 1406
		private int deltaTicksPerQuarterNote;

		// Token: 0x0400057F RID: 1407
		private long startAbsoluteTime;
	}
}

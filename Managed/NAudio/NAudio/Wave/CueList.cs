using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NAudio.Utils;

namespace NAudio.Wave
{
	// Token: 0x020001E0 RID: 480
	public class CueList
	{
		// Token: 0x06000A81 RID: 2689 RVA: 0x0001EBD1 File Offset: 0x0001CDD1
		public CueList()
		{
		}

		// Token: 0x06000A82 RID: 2690 RVA: 0x0001EBE4 File Offset: 0x0001CDE4
		public void Add(Cue cue)
		{
			this.cues.Add(cue);
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x0001EBF4 File Offset: 0x0001CDF4
		public int[] CuePositions
		{
			get
			{
				int[] array = new int[this.cues.Count];
				for (int i = 0; i < this.cues.Count; i++)
				{
					array[i] = this.cues[i].Position;
				}
				return array;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000A84 RID: 2692 RVA: 0x0001EC40 File Offset: 0x0001CE40
		public string[] CueLabels
		{
			get
			{
				string[] array = new string[this.cues.Count];
				for (int i = 0; i < this.cues.Count; i++)
				{
					array[i] = this.cues[i].Label;
				}
				return array;
			}
		}

		// Token: 0x06000A85 RID: 2693 RVA: 0x0001EC8C File Offset: 0x0001CE8C
		internal CueList(byte[] cueChunkData, byte[] listChunkData)
		{
			int num = BitConverter.ToInt32(cueChunkData, 0);
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			int[] array = new int[num];
			int num2 = 0;
			int num3 = 4;
			while (cueChunkData.Length - num3 >= 24)
			{
				dictionary[BitConverter.ToInt32(cueChunkData, num3)] = num2;
				array[num2] = BitConverter.ToInt32(cueChunkData, num3 + 20);
				num3 += 24;
				num2++;
			}
			string[] array2 = new string[num];
			int num4 = 0;
			int num5 = ChunkIdentifier.ChunkIdentifierToInt32("labl");
			int num6 = 4;
			while (listChunkData.Length - num6 >= 16)
			{
				if (BitConverter.ToInt32(listChunkData, num6) == num5)
				{
					num4 = BitConverter.ToInt32(listChunkData, num6 + 4) - 4;
					int num7 = BitConverter.ToInt32(listChunkData, num6 + 8);
					num2 = dictionary[num7];
					array2[num2] = Encoding.Default.GetString(listChunkData, num6 + 12, num4 - 1);
				}
				num6 += num4 + num4 % 2 + 12;
			}
			for (int i = 0; i < num; i++)
			{
				this.cues.Add(new Cue(array[i], array2[i]));
			}
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x0001EDA0 File Offset: 0x0001CFA0
		internal byte[] GetRIFFChunks()
		{
			if (this.Count == 0)
			{
				return null;
			}
			int num = 12 + 24 * this.Count;
			int num2 = 12;
			for (int i = 0; i < this.Count; i++)
			{
				int num3 = this[i].Label.Length + 1;
				num2 += num3 + num3 % 2 + 12;
			}
			byte[] array = new byte[num + num2];
			int num4 = ChunkIdentifier.ChunkIdentifierToInt32("cue ");
			int num5 = ChunkIdentifier.ChunkIdentifierToInt32("data");
			int num6 = ChunkIdentifier.ChunkIdentifierToInt32("LIST");
			int num7 = ChunkIdentifier.ChunkIdentifierToInt32("adtl");
			int num8 = ChunkIdentifier.ChunkIdentifierToInt32("labl");
			using (MemoryStream memoryStream = new MemoryStream(array))
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(memoryStream))
				{
					binaryWriter.Write(num4);
					binaryWriter.Write(num - 8);
					binaryWriter.Write(this.Count);
					for (int j = 0; j < this.Count; j++)
					{
						int position = this[j].Position;
						binaryWriter.Write(j);
						binaryWriter.Write(position);
						binaryWriter.Write(num5);
						binaryWriter.Seek(8, 1);
						binaryWriter.Write(position);
					}
					binaryWriter.Write(num6);
					binaryWriter.Write(num2 - 8);
					binaryWriter.Write(num7);
					for (int k = 0; k < this.Count; k++)
					{
						binaryWriter.Write(num8);
						binaryWriter.Write(this[k].Label.Length + 1 + 4);
						binaryWriter.Write(k);
						binaryWriter.Write(Encoding.Default.GetBytes(this[k].Label.ToCharArray()));
						if (this[k].Label.Length % 2 == 0)
						{
							binaryWriter.Seek(2, 1);
						}
						else
						{
							binaryWriter.Seek(1, 1);
						}
					}
				}
			}
			return array;
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000A87 RID: 2695 RVA: 0x0001EFCC File Offset: 0x0001D1CC
		public int Count
		{
			get
			{
				return this.cues.Count;
			}
		}

		// Token: 0x17000239 RID: 569
		public Cue this[int index]
		{
			get
			{
				return this.cues[index];
			}
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x0001EFE8 File Offset: 0x0001D1E8
		internal static CueList FromChunks(WaveFileReader reader)
		{
			CueList cueList = null;
			byte[] array = null;
			byte[] array2 = null;
			foreach (RiffChunk riffChunk in reader.ExtraChunks)
			{
				if (riffChunk.IdentifierAsString.ToLower() == "cue ")
				{
					array = reader.GetChunkData(riffChunk);
				}
				else if (riffChunk.IdentifierAsString.ToLower() == "list")
				{
					array2 = reader.GetChunkData(riffChunk);
				}
			}
			if (array != null && array2 != null)
			{
				cueList = new CueList(array, array2);
			}
			return cueList;
		}

		// Token: 0x04000B6B RID: 2923
		private readonly List<Cue> cues = new List<Cue>();
	}
}

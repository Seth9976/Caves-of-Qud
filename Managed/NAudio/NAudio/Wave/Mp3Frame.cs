using System;
using System.IO;

namespace NAudio.Wave
{
	// Token: 0x020000A8 RID: 168
	public class Mp3Frame
	{
		// Token: 0x060003A5 RID: 933 RVA: 0x0000CA7B File Offset: 0x0000AC7B
		public static Mp3Frame LoadFromStream(Stream input)
		{
			return Mp3Frame.LoadFromStream(input, true);
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000CA84 File Offset: 0x0000AC84
		public static Mp3Frame LoadFromStream(Stream input, bool readData)
		{
			Mp3Frame mp3Frame = new Mp3Frame();
			mp3Frame.FileOffset = input.Position;
			byte[] array = new byte[4];
			int num = input.Read(array, 0, array.Length);
			if (num < array.Length)
			{
				return null;
			}
			while (!Mp3Frame.IsValidHeader(array, mp3Frame))
			{
				array[0] = array[1];
				array[1] = array[2];
				array[2] = array[3];
				num = input.Read(array, 3, 1);
				if (num < 1)
				{
					return null;
				}
				mp3Frame.FileOffset += 1L;
			}
			int num2 = mp3Frame.FrameLength - 4;
			if (readData)
			{
				mp3Frame.RawData = new byte[mp3Frame.FrameLength];
				Array.Copy(array, mp3Frame.RawData, 4);
				num = input.Read(mp3Frame.RawData, 4, num2);
				if (num < num2)
				{
					throw new EndOfStreamException("Unexpected end of stream before frame complete");
				}
			}
			else
			{
				input.Position += (long)num2;
			}
			return mp3Frame;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000CB50 File Offset: 0x0000AD50
		private Mp3Frame()
		{
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000CB58 File Offset: 0x0000AD58
		private static bool IsValidHeader(byte[] headerBytes, Mp3Frame frame)
		{
			if (headerBytes[0] != 255 || (headerBytes[1] & 224) != 224)
			{
				return false;
			}
			frame.MpegVersion = (MpegVersion)((headerBytes[1] & 24) >> 3);
			if (frame.MpegVersion == MpegVersion.Reserved)
			{
				return false;
			}
			frame.MpegLayer = (MpegLayer)((headerBytes[1] & 6) >> 1);
			if (frame.MpegLayer == MpegLayer.Reserved)
			{
				return false;
			}
			int num = ((frame.MpegLayer == MpegLayer.Layer1) ? 0 : ((frame.MpegLayer == MpegLayer.Layer2) ? 1 : 2));
			frame.CrcPresent = (headerBytes[1] & 1) == 0;
			frame.BitRateIndex = (headerBytes[2] & 240) >> 4;
			if (frame.BitRateIndex == 15)
			{
				return false;
			}
			int num2 = ((frame.MpegVersion == MpegVersion.Version1) ? 0 : 1);
			frame.BitRate = Mp3Frame.bitRates[num2, num, frame.BitRateIndex] * 1000;
			if (frame.BitRate == 0)
			{
				return false;
			}
			int num3 = (headerBytes[2] & 12) >> 2;
			if (num3 == 3)
			{
				return false;
			}
			if (frame.MpegVersion == MpegVersion.Version1)
			{
				frame.SampleRate = Mp3Frame.sampleRatesVersion1[num3];
			}
			else if (frame.MpegVersion == MpegVersion.Version2)
			{
				frame.SampleRate = Mp3Frame.sampleRatesVersion2[num3];
			}
			else
			{
				frame.SampleRate = Mp3Frame.sampleRatesVersion25[num3];
			}
			bool flag = (headerBytes[2] & 2) == 2;
			byte b = headerBytes[2];
			frame.ChannelMode = (ChannelMode)((headerBytes[3] & 192) >> 6);
			frame.ChannelExtension = (headerBytes[3] & 48) >> 4;
			if (frame.ChannelExtension != 0 && frame.ChannelMode != ChannelMode.JointStereo)
			{
				return false;
			}
			frame.Copyright = (headerBytes[3] & 8) == 8;
			byte b2 = headerBytes[3];
			byte b3 = headerBytes[3];
			int num4 = (flag ? 1 : 0);
			frame.SampleCount = Mp3Frame.samplesPerFrame[num2, num];
			int num5 = frame.SampleCount / 8;
			if (frame.MpegLayer == MpegLayer.Layer1)
			{
				frame.FrameLength = (num5 * frame.BitRate / frame.SampleRate + num4) * 4;
			}
			else
			{
				frame.FrameLength = num5 * frame.BitRate / frame.SampleRate + num4;
			}
			return frame.FrameLength <= 16384;
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x0000CD47 File Offset: 0x0000AF47
		// (set) Token: 0x060003AA RID: 938 RVA: 0x0000CD4F File Offset: 0x0000AF4F
		public int SampleRate { get; private set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060003AB RID: 939 RVA: 0x0000CD58 File Offset: 0x0000AF58
		// (set) Token: 0x060003AC RID: 940 RVA: 0x0000CD60 File Offset: 0x0000AF60
		public int FrameLength { get; private set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060003AD RID: 941 RVA: 0x0000CD69 File Offset: 0x0000AF69
		// (set) Token: 0x060003AE RID: 942 RVA: 0x0000CD71 File Offset: 0x0000AF71
		public int BitRate { get; private set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060003AF RID: 943 RVA: 0x0000CD7A File Offset: 0x0000AF7A
		// (set) Token: 0x060003B0 RID: 944 RVA: 0x0000CD82 File Offset: 0x0000AF82
		public byte[] RawData { get; private set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x0000CD8B File Offset: 0x0000AF8B
		// (set) Token: 0x060003B2 RID: 946 RVA: 0x0000CD93 File Offset: 0x0000AF93
		public MpegVersion MpegVersion { get; private set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x0000CD9C File Offset: 0x0000AF9C
		// (set) Token: 0x060003B4 RID: 948 RVA: 0x0000CDA4 File Offset: 0x0000AFA4
		public MpegLayer MpegLayer { get; private set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x0000CDAD File Offset: 0x0000AFAD
		// (set) Token: 0x060003B6 RID: 950 RVA: 0x0000CDB5 File Offset: 0x0000AFB5
		public ChannelMode ChannelMode { get; private set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x0000CDBE File Offset: 0x0000AFBE
		// (set) Token: 0x060003B8 RID: 952 RVA: 0x0000CDC6 File Offset: 0x0000AFC6
		public int SampleCount { get; private set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x0000CDCF File Offset: 0x0000AFCF
		// (set) Token: 0x060003BA RID: 954 RVA: 0x0000CDD7 File Offset: 0x0000AFD7
		public int ChannelExtension { get; private set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060003BB RID: 955 RVA: 0x0000CDE0 File Offset: 0x0000AFE0
		// (set) Token: 0x060003BC RID: 956 RVA: 0x0000CDE8 File Offset: 0x0000AFE8
		public int BitRateIndex { get; private set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060003BD RID: 957 RVA: 0x0000CDF1 File Offset: 0x0000AFF1
		// (set) Token: 0x060003BE RID: 958 RVA: 0x0000CDF9 File Offset: 0x0000AFF9
		public bool Copyright { get; private set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060003BF RID: 959 RVA: 0x0000CE02 File Offset: 0x0000B002
		// (set) Token: 0x060003C0 RID: 960 RVA: 0x0000CE0A File Offset: 0x0000B00A
		public bool CrcPresent { get; private set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x0000CE13 File Offset: 0x0000B013
		// (set) Token: 0x060003C2 RID: 962 RVA: 0x0000CE1B File Offset: 0x0000B01B
		public long FileOffset { get; private set; }

		// Token: 0x04000464 RID: 1124
		private const int MaxFrameLength = 16384;

		// Token: 0x04000465 RID: 1125
		private static readonly int[,,] bitRates = new int[,,]
		{
			{
				{
					0, 32, 64, 96, 128, 160, 192, 224, 256, 288,
					320, 352, 384, 416, 448
				},
				{
					0, 32, 48, 56, 64, 80, 96, 112, 128, 160,
					192, 224, 256, 320, 384
				},
				{
					0, 32, 40, 48, 56, 64, 80, 96, 112, 128,
					160, 192, 224, 256, 320
				}
			},
			{
				{
					0, 32, 48, 56, 64, 80, 96, 112, 128, 144,
					160, 176, 192, 224, 256
				},
				{
					0, 8, 16, 24, 32, 40, 48, 56, 64, 80,
					96, 112, 128, 144, 160
				},
				{
					0, 8, 16, 24, 32, 40, 48, 56, 64, 80,
					96, 112, 128, 144, 160
				}
			}
		};

		// Token: 0x04000466 RID: 1126
		private static readonly int[,] samplesPerFrame = new int[,]
		{
			{ 384, 1152, 1152 },
			{ 384, 1152, 576 }
		};

		// Token: 0x04000467 RID: 1127
		private static readonly int[] sampleRatesVersion1 = new int[] { 44100, 48000, 32000 };

		// Token: 0x04000468 RID: 1128
		private static readonly int[] sampleRatesVersion2 = new int[] { 22050, 24000, 16000 };

		// Token: 0x04000469 RID: 1129
		private static readonly int[] sampleRatesVersion25 = new int[] { 11025, 12000, 8000 };
	}
}

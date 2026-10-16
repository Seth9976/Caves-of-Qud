using System;
using System.IO;

// Token: 0x0200013C RID: 316
public class WAV
{
	// Token: 0x06000BE7 RID: 3047 RVA: 0x0004F082 File Offset: 0x0004D282
	private static float bytesToFloat(byte firstByte, byte secondByte)
	{
		return (float)((short)(((int)secondByte << 8) | (int)firstByte)) / 32768f;
	}

	// Token: 0x06000BE8 RID: 3048 RVA: 0x0004F094 File Offset: 0x0004D294
	private static int bytesToInt(byte[] bytes, int offset = 0)
	{
		int num = 0;
		for (int i = 0; i < 4; i++)
		{
			num |= (int)bytes[offset + i] << i * 8;
		}
		return num;
	}

	// Token: 0x06000BE9 RID: 3049 RVA: 0x0004F0BF File Offset: 0x0004D2BF
	private static byte[] GetBytes(string filename)
	{
		return File.ReadAllBytes(filename);
	}

	// Token: 0x17000146 RID: 326
	// (get) Token: 0x06000BEA RID: 3050 RVA: 0x0004F0C7 File Offset: 0x0004D2C7
	// (set) Token: 0x06000BEB RID: 3051 RVA: 0x0004F0CF File Offset: 0x0004D2CF
	public float[] LeftChannel { get; internal set; }

	// Token: 0x17000147 RID: 327
	// (get) Token: 0x06000BEC RID: 3052 RVA: 0x0004F0D8 File Offset: 0x0004D2D8
	// (set) Token: 0x06000BED RID: 3053 RVA: 0x0004F0E0 File Offset: 0x0004D2E0
	public float[] RightChannel { get; internal set; }

	// Token: 0x17000148 RID: 328
	// (get) Token: 0x06000BEE RID: 3054 RVA: 0x0004F0E9 File Offset: 0x0004D2E9
	// (set) Token: 0x06000BEF RID: 3055 RVA: 0x0004F0F1 File Offset: 0x0004D2F1
	public int ChannelCount { get; internal set; }

	// Token: 0x17000149 RID: 329
	// (get) Token: 0x06000BF0 RID: 3056 RVA: 0x0004F0FA File Offset: 0x0004D2FA
	// (set) Token: 0x06000BF1 RID: 3057 RVA: 0x0004F102 File Offset: 0x0004D302
	public int SampleCount { get; internal set; }

	// Token: 0x1700014A RID: 330
	// (get) Token: 0x06000BF2 RID: 3058 RVA: 0x0004F10B File Offset: 0x0004D30B
	// (set) Token: 0x06000BF3 RID: 3059 RVA: 0x0004F113 File Offset: 0x0004D313
	public int Frequency { get; internal set; }

	// Token: 0x06000BF4 RID: 3060 RVA: 0x0004F11C File Offset: 0x0004D31C
	public WAV(string filename)
		: this(WAV.GetBytes(filename))
	{
	}

	// Token: 0x06000BF5 RID: 3061 RVA: 0x0004F12C File Offset: 0x0004D32C
	public WAV(byte[] wav)
	{
		this.ChannelCount = (int)wav[22];
		this.Frequency = WAV.bytesToInt(wav, 24);
		int i = 12;
		while (wav[i] != 100 || wav[i + 1] != 97 || wav[i + 2] != 116 || wav[i + 3] != 97)
		{
			i += 4;
			int num = (int)wav[i] + (int)wav[i + 1] * 256 + (int)wav[i + 2] * 65536 + (int)wav[i + 3] * 16777216;
			i += 4 + num;
		}
		i += 8;
		this.SampleCount = (wav.Length - i) / 2;
		if (this.ChannelCount == 2)
		{
			this.SampleCount /= 2;
		}
		this.LeftChannel = new float[this.SampleCount];
		if (this.ChannelCount == 2)
		{
			this.RightChannel = new float[this.SampleCount];
		}
		else
		{
			this.RightChannel = null;
		}
		int num2 = 0;
		while (i < wav.Length)
		{
			this.LeftChannel[num2] = WAV.bytesToFloat(wav[i], wav[i + 1]);
			i += 2;
			if (this.ChannelCount == 2)
			{
				this.RightChannel[num2] = WAV.bytesToFloat(wav[i], wav[i + 1]);
				i += 2;
			}
			num2++;
		}
	}

	// Token: 0x06000BF6 RID: 3062 RVA: 0x0004F258 File Offset: 0x0004D458
	public override string ToString()
	{
		return string.Format("[WAV: LeftChannel={0}, RightChannel={1}, ChannelCount={2}, SampleCount={3}, Frequency={4}]", new object[] { this.LeftChannel, this.RightChannel, this.ChannelCount, this.SampleCount, this.Frequency });
	}
}

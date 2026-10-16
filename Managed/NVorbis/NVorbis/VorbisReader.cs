using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NVorbis.Ogg;

namespace NVorbis
{
	// Token: 0x02000026 RID: 38
	public class VorbisReader : IDisposable
	{
		// Token: 0x06000122 RID: 290 RVA: 0x0000846E File Offset: 0x0000666E
		private VorbisReader()
		{
			this.ClipSamples = true;
			this._decoders = new List<VorbisStreamDecoder>();
			this._serials = new List<int>();
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00008493 File Offset: 0x00006693
		public VorbisReader(string fileName)
			: this(File.Open(fileName, 3, 1, 1), true)
		{
		}

		// Token: 0x06000124 RID: 292 RVA: 0x000084A8 File Offset: 0x000066A8
		public VorbisReader(Stream stream, bool closeStreamOnDispose)
			: this()
		{
			BufferedReadStream bufferedReadStream = new BufferedReadStream(stream);
			bufferedReadStream.CloseBaseStream = closeStreamOnDispose;
			ContainerReader containerReader = new ContainerReader(bufferedReadStream, closeStreamOnDispose);
			if (!this.LoadContainer(containerReader))
			{
				bufferedReadStream.Close();
				throw new InvalidDataException("Could not determine container type!");
			}
			this._containerReader = containerReader;
			if (this._decoders.Count == 0)
			{
				throw new InvalidDataException("No Vorbis data found!");
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000850A File Offset: 0x0000670A
		public VorbisReader(IContainerReader containerReader)
			: this()
		{
			if (!this.LoadContainer(containerReader))
			{
				throw new InvalidDataException("Container did not initialize!");
			}
			this._containerReader = containerReader;
			if (this._decoders.Count == 0)
			{
				throw new InvalidDataException("No Vorbis data found!");
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00008548 File Offset: 0x00006748
		public VorbisReader(IPacketProvider packetProvider)
			: this()
		{
			NewStreamEventArgs e = new NewStreamEventArgs(packetProvider);
			this.NewStream(this, e);
			if (e.IgnoreStream)
			{
				throw new InvalidDataException("No Vorbis data found!");
			}
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0000857D File Offset: 0x0000677D
		private bool LoadContainer(IContainerReader containerReader)
		{
			containerReader.NewStream += new EventHandler<NewStreamEventArgs>(this.NewStream);
			if (!containerReader.Init())
			{
				containerReader.NewStream -= new EventHandler<NewStreamEventArgs>(this.NewStream);
				return false;
			}
			return true;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000085B0 File Offset: 0x000067B0
		private void NewStream(object sender, NewStreamEventArgs ea)
		{
			IPacketProvider packetProvider = ea.PacketProvider;
			VorbisStreamDecoder vorbisStreamDecoder = new VorbisStreamDecoder(packetProvider);
			if (vorbisStreamDecoder.TryInit())
			{
				this._decoders.Add(vorbisStreamDecoder);
				this._serials.Add(packetProvider.StreamSerial);
				return;
			}
			ea.IgnoreStream = true;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x000085F8 File Offset: 0x000067F8
		public void Dispose()
		{
			if (this._decoders != null)
			{
				foreach (VorbisStreamDecoder vorbisStreamDecoder in this._decoders)
				{
					vorbisStreamDecoder.Dispose();
				}
				this._decoders.Clear();
				this._decoders = null;
			}
			if (this._containerReader != null)
			{
				this._containerReader.NewStream -= new EventHandler<NewStreamEventArgs>(this.NewStream);
				this._containerReader.Dispose();
				this._containerReader = null;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00008698 File Offset: 0x00006898
		private VorbisStreamDecoder ActiveDecoder
		{
			get
			{
				if (this._decoders == null)
				{
					throw new ObjectDisposedException("VorbisReader");
				}
				return this._decoders[this._streamIdx];
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600012B RID: 299 RVA: 0x000086BE File Offset: 0x000068BE
		public int Channels
		{
			get
			{
				return this.ActiveDecoder._channels;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600012C RID: 300 RVA: 0x000086CB File Offset: 0x000068CB
		public int SampleRate
		{
			get
			{
				return this.ActiveDecoder._sampleRate;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600012D RID: 301 RVA: 0x000086D8 File Offset: 0x000068D8
		public int UpperBitrate
		{
			get
			{
				return this.ActiveDecoder._upperBitrate;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600012E RID: 302 RVA: 0x000086E5 File Offset: 0x000068E5
		public int NominalBitrate
		{
			get
			{
				return this.ActiveDecoder._nominalBitrate;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600012F RID: 303 RVA: 0x000086F2 File Offset: 0x000068F2
		public int LowerBitrate
		{
			get
			{
				return this.ActiveDecoder._lowerBitrate;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000130 RID: 304 RVA: 0x000086FF File Offset: 0x000068FF
		public string Vendor
		{
			get
			{
				return this.ActiveDecoder._vendor;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000131 RID: 305 RVA: 0x0000870C File Offset: 0x0000690C
		public string[] Comments
		{
			get
			{
				return this.ActiveDecoder._comments;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00008719 File Offset: 0x00006919
		public bool IsParameterChange
		{
			get
			{
				return this.ActiveDecoder.IsParameterChange;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00008726 File Offset: 0x00006926
		public long ContainerOverheadBits
		{
			get
			{
				return this.ActiveDecoder.ContainerBits;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00008733 File Offset: 0x00006933
		// (set) Token: 0x06000135 RID: 309 RVA: 0x0000873B File Offset: 0x0000693B
		public bool ClipSamples { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00008747 File Offset: 0x00006947
		public IVorbisStreamStatus[] Stats
		{
			get
			{
				return Enumerable.ToArray<IVorbisStreamStatus>(Enumerable.Cast<IVorbisStreamStatus>(Enumerable.Select<VorbisStreamDecoder, VorbisStreamDecoder>(this._decoders, (VorbisStreamDecoder d) => d)));
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000137 RID: 311 RVA: 0x0000877B File Offset: 0x0000697B
		public int StreamIndex
		{
			get
			{
				return this._streamIdx;
			}
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00008784 File Offset: 0x00006984
		public int ReadSamples(float[] buffer, int offset, int count)
		{
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || offset + count > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			count = this.ActiveDecoder.ReadSamples(buffer, offset, count);
			if (this.ClipSamples)
			{
				VorbisStreamDecoder vorbisStreamDecoder = this._decoders[this._streamIdx];
				int i = 0;
				while (i < count)
				{
					buffer[offset] = Utils.ClipValue(buffer[offset], ref vorbisStreamDecoder._clipped);
					i++;
					offset++;
				}
			}
			return count;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00008804 File Offset: 0x00006A04
		public void ClearParameterChange()
		{
			this.ActiveDecoder.IsParameterChange = false;
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00008812 File Offset: 0x00006A12
		public int StreamCount
		{
			get
			{
				return this._decoders.Count;
			}
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000881F File Offset: 0x00006A1F
		public bool FindNextStream()
		{
			return this._containerReader != null && this._containerReader.FindNextStream();
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00008838 File Offset: 0x00006A38
		public bool SwitchStreams(int index)
		{
			if (index < 0 || index >= this.StreamCount)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (this._decoders == null)
			{
				throw new ObjectDisposedException("VorbisReader");
			}
			if (this._streamIdx == index)
			{
				return false;
			}
			VorbisStreamDecoder vorbisStreamDecoder = this._decoders[this._streamIdx];
			this._streamIdx = index;
			VorbisStreamDecoder vorbisStreamDecoder2 = this._decoders[this._streamIdx];
			return vorbisStreamDecoder._channels != vorbisStreamDecoder2._channels || vorbisStreamDecoder._sampleRate != vorbisStreamDecoder2._sampleRate;
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600013D RID: 317 RVA: 0x000088C7 File Offset: 0x00006AC7
		// (set) Token: 0x0600013E RID: 318 RVA: 0x000088E2 File Offset: 0x00006AE2
		public TimeSpan DecodedTime
		{
			get
			{
				return TimeSpan.FromSeconds((double)this.ActiveDecoder.CurrentPosition / (double)this.SampleRate);
			}
			set
			{
				this.ActiveDecoder.SeekTo((long)(value.TotalSeconds * (double)this.SampleRate));
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600013F RID: 319 RVA: 0x000088FF File Offset: 0x00006AFF
		// (set) Token: 0x06000140 RID: 320 RVA: 0x0000890C File Offset: 0x00006B0C
		public long DecodedPosition
		{
			get
			{
				return this.ActiveDecoder.CurrentPosition;
			}
			set
			{
				this.ActiveDecoder.SeekTo(value);
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000141 RID: 321 RVA: 0x0000891C File Offset: 0x00006B1C
		public TimeSpan TotalTime
		{
			get
			{
				VorbisStreamDecoder activeDecoder = this.ActiveDecoder;
				if (activeDecoder.CanSeek)
				{
					return TimeSpan.FromSeconds((double)activeDecoder.GetLastGranulePos() / (double)activeDecoder._sampleRate);
				}
				return TimeSpan.MaxValue;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00008954 File Offset: 0x00006B54
		public long TotalSamples
		{
			get
			{
				VorbisStreamDecoder activeDecoder = this.ActiveDecoder;
				if (activeDecoder.CanSeek)
				{
					return activeDecoder.GetLastGranulePos();
				}
				return long.MaxValue;
			}
		}

		// Token: 0x040000BC RID: 188
		private int _streamIdx;

		// Token: 0x040000BD RID: 189
		private IContainerReader _containerReader;

		// Token: 0x040000BE RID: 190
		private List<VorbisStreamDecoder> _decoders;

		// Token: 0x040000BF RID: 191
		private List<int> _serials;
	}
}

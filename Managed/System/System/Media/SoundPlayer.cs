using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Threading;
using Mono.Audio;

namespace System.Media
{
	/// <summary>Controls playback of a sound from a .wav file.</summary>
	// Token: 0x0200017A RID: 378
	[ToolboxItem(false)]
	[Serializable]
	public class SoundPlayer : Component, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Media.SoundPlayer" /> class.</summary>
		// Token: 0x060009F5 RID: 2549 RVA: 0x0002BCD5 File Offset: 0x00029ED5
		public SoundPlayer()
		{
			this.sound_location = string.Empty;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Media.SoundPlayer" /> class, and attaches the .wav file within the specified <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="stream">A <see cref="T:System.IO.Stream" /> to a .wav file.</param>
		// Token: 0x060009F6 RID: 2550 RVA: 0x0002BCFE File Offset: 0x00029EFE
		public SoundPlayer(Stream stream)
			: this()
		{
			this.audiostream = stream;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Media.SoundPlayer" /> class, and attaches the specified .wav file.</summary>
		/// <param name="soundLocation">The location of a .wav file to load.</param>
		/// <exception cref="T:System.UriFormatException">The URL value specified by <paramref name="soundLocation" /> cannot be resolved.</exception>
		// Token: 0x060009F7 RID: 2551 RVA: 0x0002BD0D File Offset: 0x00029F0D
		public SoundPlayer(string soundLocation)
			: this()
		{
			if (soundLocation == null)
			{
				throw new ArgumentNullException("soundLocation");
			}
			this.sound_location = soundLocation;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Media.SoundPlayer" /> class.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to be used for deserialization.</param>
		/// <param name="context">The destination to be used for deserialization.</param>
		/// <exception cref="T:System.UriFormatException">The <see cref="P:System.Media.SoundPlayer.SoundLocation" /> specified in <paramref name="serializationInfo" /> cannot be resolved.</exception>
		// Token: 0x060009F8 RID: 2552 RVA: 0x0002BD2A File Offset: 0x00029F2A
		protected SoundPlayer(SerializationInfo serializationInfo, StreamingContext context)
			: this()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x0002BD38 File Offset: 0x00029F38
		private void LoadFromStream(Stream s)
		{
			this.mstream = new MemoryStream();
			byte[] array = new byte[4096];
			int num;
			while ((num = s.Read(array, 0, 4096)) > 0)
			{
				this.mstream.Write(array, 0, num);
			}
			this.mstream.Position = 0L;
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x0002BD8C File Offset: 0x00029F8C
		private void LoadFromUri(string location)
		{
			this.mstream = null;
			if (string.IsNullOrEmpty(location))
			{
				return;
			}
			Stream stream;
			if (File.Exists(location))
			{
				stream = new FileStream(location, FileMode.Open, FileAccess.Read, FileShare.Read);
			}
			else
			{
				stream = WebRequest.Create(location).GetResponse().GetResponseStream();
			}
			using (stream)
			{
				this.LoadFromStream(stream);
			}
		}

		/// <summary>Loads a sound synchronously.</summary>
		/// <exception cref="T:System.ServiceProcess.TimeoutException">The elapsed time during loading exceeds the time, in milliseconds, specified by <see cref="P:System.Media.SoundPlayer.LoadTimeout" />. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified by <see cref="P:System.Media.SoundPlayer.SoundLocation" /> cannot be found.</exception>
		// Token: 0x060009FB RID: 2555 RVA: 0x0002BDF8 File Offset: 0x00029FF8
		public void Load()
		{
			if (this.load_completed)
			{
				return;
			}
			if (this.audiostream != null)
			{
				this.LoadFromStream(this.audiostream);
			}
			else
			{
				this.LoadFromUri(this.sound_location);
			}
			this.adata = null;
			this.adev = null;
			this.load_completed = true;
			AsyncCompletedEventArgs e = new AsyncCompletedEventArgs(null, false, this);
			this.OnLoadCompleted(e);
			if (this.LoadCompleted != null)
			{
				this.LoadCompleted(this, e);
			}
			if (SoundPlayer.use_win32_player)
			{
				if (this.win32_player == null)
				{
					this.win32_player = new Win32SoundPlayer(this.mstream);
					return;
				}
				this.win32_player.Stream = this.mstream;
			}
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x0002BE9C File Offset: 0x0002A09C
		private void AsyncFinished(IAsyncResult ar)
		{
			(ar.AsyncState as ThreadStart).EndInvoke(ar);
		}

		/// <summary>Loads a .wav file from a stream or a Web resource using a new thread.</summary>
		/// <exception cref="T:System.ServiceProcess.TimeoutException">The elapsed time during loading exceeds the time, in milliseconds, specified by <see cref="P:System.Media.SoundPlayer.LoadTimeout" />. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified by <see cref="P:System.Media.SoundPlayer.SoundLocation" /> cannot be found.</exception>
		// Token: 0x060009FD RID: 2557 RVA: 0x0002BEB0 File Offset: 0x0002A0B0
		public void LoadAsync()
		{
			if (this.load_completed)
			{
				return;
			}
			ThreadStart threadStart = new ThreadStart(this.Load);
			threadStart.BeginInvoke(new AsyncCallback(this.AsyncFinished), threadStart);
		}

		/// <summary>Raises the <see cref="E:System.Media.SoundPlayer.LoadCompleted" /> event.</summary>
		/// <param name="e">An <see cref="T:System.ComponentModel.AsyncCompletedEventArgs" />  that contains the event data. </param>
		// Token: 0x060009FE RID: 2558 RVA: 0x00003917 File Offset: 0x00001B17
		protected virtual void OnLoadCompleted(AsyncCompletedEventArgs e)
		{
		}

		/// <summary>Raises the <see cref="E:System.Media.SoundPlayer.SoundLocationChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x060009FF RID: 2559 RVA: 0x00003917 File Offset: 0x00001B17
		protected virtual void OnSoundLocationChanged(EventArgs e)
		{
		}

		/// <summary>Raises the <see cref="E:System.Media.SoundPlayer.StreamChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06000A00 RID: 2560 RVA: 0x00003917 File Offset: 0x00001B17
		protected virtual void OnStreamChanged(EventArgs e)
		{
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x0002BEE7 File Offset: 0x0002A0E7
		private void Start()
		{
			if (!SoundPlayer.use_win32_player)
			{
				this.stopped = false;
				if (this.adata != null)
				{
					this.adata.IsStopped = false;
				}
			}
			if (!this.load_completed)
			{
				this.Load();
			}
		}

		/// <summary>Plays the .wav file using a new thread, and loads the .wav file first if it has not been loaded.</summary>
		/// <exception cref="T:System.ServiceProcess.TimeoutException">The elapsed time during loading exceeds the time, in milliseconds, specified by <see cref="P:System.Media.SoundPlayer.LoadTimeout" />. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified by <see cref="P:System.Media.SoundPlayer.SoundLocation" /> cannot be found.</exception>
		/// <exception cref="T:System.InvalidOperationException">The .wav header is corrupted; the file specified by <see cref="P:System.Media.SoundPlayer.SoundLocation" /> is not a PCM .wav file.</exception>
		// Token: 0x06000A02 RID: 2562 RVA: 0x0002BF1C File Offset: 0x0002A11C
		public void Play()
		{
			if (!SoundPlayer.use_win32_player)
			{
				ThreadStart threadStart = new ThreadStart(this.PlaySync);
				threadStart.BeginInvoke(new AsyncCallback(this.AsyncFinished), threadStart);
				return;
			}
			this.Start();
			if (this.mstream == null)
			{
				SystemSounds.Beep.Play();
				return;
			}
			this.win32_player.Play();
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x0002BF76 File Offset: 0x0002A176
		private void PlayLoop()
		{
			this.Start();
			if (this.mstream == null)
			{
				SystemSounds.Beep.Play();
				return;
			}
			while (!this.stopped)
			{
				this.PlaySync();
			}
		}

		/// <summary>Plays and loops the .wav file using a new thread, and loads the .wav file first if it has not been loaded.</summary>
		/// <exception cref="T:System.ServiceProcess.TimeoutException">The elapsed time during loading exceeds the time, in milliseconds, specified by <see cref="P:System.Media.SoundPlayer.LoadTimeout" />. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified by <see cref="P:System.Media.SoundPlayer.SoundLocation" /> cannot be found.</exception>
		/// <exception cref="T:System.InvalidOperationException">The .wav header is corrupted; the file specified by <see cref="P:System.Media.SoundPlayer.SoundLocation" /> is not a PCM .wav file.</exception>
		// Token: 0x06000A04 RID: 2564 RVA: 0x0002BFA0 File Offset: 0x0002A1A0
		public void PlayLooping()
		{
			if (!SoundPlayer.use_win32_player)
			{
				ThreadStart threadStart = new ThreadStart(this.PlayLoop);
				threadStart.BeginInvoke(new AsyncCallback(this.AsyncFinished), threadStart);
				return;
			}
			this.Start();
			if (this.mstream == null)
			{
				SystemSounds.Beep.Play();
				return;
			}
			this.win32_player.PlayLooping();
		}

		/// <summary>Plays the .wav file and loads the .wav file first if it has not been loaded.</summary>
		/// <exception cref="T:System.ServiceProcess.TimeoutException">The elapsed time during loading exceeds the time, in milliseconds, specified by <see cref="P:System.Media.SoundPlayer.LoadTimeout" />. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">The file specified by <see cref="P:System.Media.SoundPlayer.SoundLocation" /> cannot be found.</exception>
		/// <exception cref="T:System.InvalidOperationException">The .wav header is corrupted; the file specified by <see cref="P:System.Media.SoundPlayer.SoundLocation" /> is not a PCM .wav file.</exception>
		// Token: 0x06000A05 RID: 2565 RVA: 0x0002BFFC File Offset: 0x0002A1FC
		public void PlaySync()
		{
			this.Start();
			if (this.mstream == null)
			{
				SystemSounds.Beep.Play();
				return;
			}
			if (!SoundPlayer.use_win32_player)
			{
				try
				{
					if (this.adata == null)
					{
						this.adata = new WavData(this.mstream);
					}
					if (this.adev == null)
					{
						this.adev = AudioDevice.CreateDevice(null);
					}
					if (this.adata != null)
					{
						this.adata.Setup(this.adev);
						this.adata.Play(this.adev);
					}
					return;
				}
				catch
				{
					return;
				}
			}
			this.win32_player.PlaySync();
		}

		/// <summary>Stops playback of the sound if playback is occurring.</summary>
		// Token: 0x06000A06 RID: 2566 RVA: 0x0002C0A0 File Offset: 0x0002A2A0
		public void Stop()
		{
			if (!SoundPlayer.use_win32_player)
			{
				this.stopped = true;
				if (this.adata != null)
				{
					this.adata.IsStopped = true;
					return;
				}
			}
			else
			{
				this.win32_player.Stop();
			}
		}

		/// <summary>For a description of this member, see the <see cref="M:System.Runtime.Serialization.ISerializable.GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)" /> method.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" />  to populate with data.</param>
		/// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
		// Token: 0x06000A07 RID: 2567 RVA: 0x00003917 File Offset: 0x00001B17
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
		}

		/// <summary>Gets a value indicating whether loading of a .wav file has successfully completed.</summary>
		/// <returns>true if a .wav file is loaded; false if a .wav file has not yet been loaded.</returns>
		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x0002C0D0 File Offset: 0x0002A2D0
		public bool IsLoadCompleted
		{
			get
			{
				return this.load_completed;
			}
		}

		/// <summary>Gets or sets the time, in milliseconds, in which the .wav file must load.</summary>
		/// <returns>The number of milliseconds to wait. The default is 10000 (10 seconds).</returns>
		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000A09 RID: 2569 RVA: 0x0002C0D8 File Offset: 0x0002A2D8
		// (set) Token: 0x06000A0A RID: 2570 RVA: 0x0002C0E0 File Offset: 0x0002A2E0
		public int LoadTimeout
		{
			get
			{
				return this.load_timeout;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("timeout must be >= 0");
				}
				this.load_timeout = value;
			}
		}

		/// <summary>Gets or sets the file path or URL of the .wav file to load.</summary>
		/// <returns>The file path or URL from which to load a .wav file, or <see cref="F:System.String.Empty" /> if no file path is present. The default is <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x0002C0F8 File Offset: 0x0002A2F8
		// (set) Token: 0x06000A0C RID: 2572 RVA: 0x0002C100 File Offset: 0x0002A300
		public string SoundLocation
		{
			get
			{
				return this.sound_location;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.sound_location = value;
				this.load_completed = false;
				this.OnSoundLocationChanged(EventArgs.Empty);
				if (this.SoundLocationChanged != null)
				{
					this.SoundLocationChanged(this, EventArgs.Empty);
				}
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.IO.Stream" /> from which to load the .wav file.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> from which to load the .wav file, or null if no stream is available. The default is null.</returns>
		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x0002C14D File Offset: 0x0002A34D
		// (set) Token: 0x06000A0E RID: 2574 RVA: 0x0002C155 File Offset: 0x0002A355
		public Stream Stream
		{
			get
			{
				return this.audiostream;
			}
			set
			{
				if (this.audiostream != value)
				{
					this.audiostream = value;
					this.load_completed = false;
					this.OnStreamChanged(EventArgs.Empty);
					if (this.StreamChanged != null)
					{
						this.StreamChanged(this, EventArgs.Empty);
					}
				}
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Object" /> that contains data about the <see cref="T:System.Media.SoundPlayer" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains data about the <see cref="T:System.Media.SoundPlayer" />.</returns>
		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000A0F RID: 2575 RVA: 0x0002C192 File Offset: 0x0002A392
		// (set) Token: 0x06000A10 RID: 2576 RVA: 0x0002C19A File Offset: 0x0002A39A
		public object Tag
		{
			get
			{
				return this.tag;
			}
			set
			{
				this.tag = value;
			}
		}

		/// <summary>Occurs when a .wav file has been successfully or unsuccessfully loaded.</summary>
		// Token: 0x14000010 RID: 16
		// (add) Token: 0x06000A11 RID: 2577 RVA: 0x0002C1A4 File Offset: 0x0002A3A4
		// (remove) Token: 0x06000A12 RID: 2578 RVA: 0x0002C1DC File Offset: 0x0002A3DC
		public event AsyncCompletedEventHandler LoadCompleted;

		/// <summary>Occurs when a new audio source path for this <see cref="T:System.Media.SoundPlayer" /> has been set.</summary>
		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06000A13 RID: 2579 RVA: 0x0002C214 File Offset: 0x0002A414
		// (remove) Token: 0x06000A14 RID: 2580 RVA: 0x0002C24C File Offset: 0x0002A44C
		public event EventHandler SoundLocationChanged;

		/// <summary>Occurs when a new <see cref="T:System.IO.Stream" /> audio source for this <see cref="T:System.Media.SoundPlayer" /> has been set.</summary>
		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06000A15 RID: 2581 RVA: 0x0002C284 File Offset: 0x0002A484
		// (remove) Token: 0x06000A16 RID: 2582 RVA: 0x0002C2BC File Offset: 0x0002A4BC
		public event EventHandler StreamChanged;

		// Token: 0x040006D9 RID: 1753
		private string sound_location;

		// Token: 0x040006DA RID: 1754
		private Stream audiostream;

		// Token: 0x040006DB RID: 1755
		private object tag = string.Empty;

		// Token: 0x040006DC RID: 1756
		private MemoryStream mstream;

		// Token: 0x040006DD RID: 1757
		private bool load_completed;

		// Token: 0x040006DE RID: 1758
		private int load_timeout = 10000;

		// Token: 0x040006DF RID: 1759
		private AudioDevice adev;

		// Token: 0x040006E0 RID: 1760
		private AudioData adata;

		// Token: 0x040006E1 RID: 1761
		private bool stopped;

		// Token: 0x040006E2 RID: 1762
		private Win32SoundPlayer win32_player;

		// Token: 0x040006E3 RID: 1763
		private static readonly bool use_win32_player = Environment.OSVersion.Platform != PlatformID.Unix;
	}
}

using System;
using System.IO;
using Unity;

namespace System.Media
{
	/// <summary>Represents a system sound type.</summary>
	/// <filterpriority>2</filterpriority>
	/// <completionlist cref="T:System.Media.SystemSounds" />
	// Token: 0x0200017B RID: 379
	public class SystemSound
	{
		// Token: 0x06000A17 RID: 2583 RVA: 0x0002C2F1 File Offset: 0x0002A4F1
		internal SystemSound(string tag)
		{
			this.resource = typeof(SystemSound).Assembly.GetManifestResourceStream(tag + ".wav");
		}

		/// <summary>Plays the system sound type.</summary>
		// Token: 0x06000A18 RID: 2584 RVA: 0x0002C31E File Offset: 0x0002A51E
		public void Play()
		{
			new SoundPlayer(this.resource).Play();
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x00013BAE File Offset: 0x00011DAE
		internal SystemSound()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040006E7 RID: 1767
		private Stream resource;
	}
}

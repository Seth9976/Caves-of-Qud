using System;

namespace System.Media
{
	/// <summary>Retrieves sounds associated with a set of Windows operating system sound-event types. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200017C RID: 380
	public sealed class SystemSounds
	{
		// Token: 0x06000A1A RID: 2586 RVA: 0x0000219B File Offset: 0x0000039B
		private SystemSounds()
		{
		}

		/// <summary>Gets the sound associated with the Asterisk program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:System.Media.SystemSound" /> associated with the Asterisk program event in the current Windows sound scheme.</returns>
		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000A1B RID: 2587 RVA: 0x0002C330 File Offset: 0x0002A530
		public static SystemSound Asterisk
		{
			get
			{
				return new SystemSound("Asterisk");
			}
		}

		/// <summary>Gets the sound associated with the Beep program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:System.Media.SystemSound" /> associated with the Beep program event in the current Windows sound scheme.</returns>
		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000A1C RID: 2588 RVA: 0x0002C33C File Offset: 0x0002A53C
		public static SystemSound Beep
		{
			get
			{
				return new SystemSound("Beep");
			}
		}

		/// <summary>Gets the sound associated with the Exclamation program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:System.Media.SystemSound" /> associated with the Exclamation program event in the current Windows sound scheme.</returns>
		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000A1D RID: 2589 RVA: 0x0002C348 File Offset: 0x0002A548
		public static SystemSound Exclamation
		{
			get
			{
				return new SystemSound("Exclamation");
			}
		}

		/// <summary>Gets the sound associated with the Hand program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:System.Media.SystemSound" /> associated with the Hand program event in the current Windows sound scheme.</returns>
		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x0002C354 File Offset: 0x0002A554
		public static SystemSound Hand
		{
			get
			{
				return new SystemSound("Hand");
			}
		}

		/// <summary>Gets the sound associated with the Question program event in the current Windows sound scheme.</summary>
		/// <returns>A <see cref="T:System.Media.SystemSound" /> associated with the Question program event in the current Windows sound scheme.</returns>
		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000A1F RID: 2591 RVA: 0x0002C360 File Offset: 0x0002A560
		public static SystemSound Question
		{
			get
			{
				return new SystemSound("Question");
			}
		}
	}
}

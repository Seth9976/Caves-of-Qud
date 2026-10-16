using System;
using System.Collections.Generic;
using System.Diagnostics;
using XRL.Core;
using XRL.Rules;
using XRL.Wish;
using XRL.World;

namespace XRL
{
	// Token: 0x020004EC RID: 1260
	[HasWishCommand]
	[HasOptionFlagUpdate]
	[Serializable]
	public class AmbientSoundsSystem : IGameSystem
	{
		// Token: 0x0600365B RID: 13915 RVA: 0x00106104 File Offset: 0x00104304
		[OptionFlagUpdate]
		public static void OnOptionUpdate()
		{
			if (AmbientSoundsSystem.Enabled == Globals.EnableAmbient)
			{
				return;
			}
			if (AmbientSoundsSystem.Enabled)
			{
				AmbientSoundsSystem.Timer.Reset();
				AmbientSoundsSystem.PlayAmbientBeds(null, null, null, null);
			}
			else
			{
				AmbientSoundsSystem.Timer.Restart();
				AmbientSoundsSystem.PlayAmbientBeds(The.ActiveZone, null, null, null, null);
			}
			AmbientSoundsSystem.Enabled = Globals.EnableAmbient;
		}

		// Token: 0x0600365C RID: 13916 RVA: 0x0010616C File Offset: 0x0010436C
		public static void Update()
		{
			if ((float)AmbientSoundsSystem.Timer.ElapsedMilliseconds < AmbientSoundsSystem.NextSound)
			{
				return;
			}
			AmbientSoundsSystem.Timer.Restart();
			AmbientSoundsSystem.NextSound = (float)Stat.RandomCosmetic(1000, 15000);
			if (AmbientSoundsSystem.Sounds != null && AmbientSoundsSystem.Sounds.Length != 0)
			{
				AmbientSoundsSystem.EmptyCells.Clear();
				Zone activeZone = The.ActiveZone;
				if (activeZone != null)
				{
					activeZone.GetEmptyCells(null, AmbientSoundsSystem.EmptyCells);
				}
				if (AmbientSoundsSystem.EmptyCells.Count > 0)
				{
					AmbientSoundsSystem.EmptyCells.GetRandomElement<Cell>(null).PlayWorldSound(AmbientSoundsSystem.Sounds.GetRandomElement<string>(null), Globals.AmbientVolume * 0.33f, 0f, false, 0f, 1f, 1f, int.MaxValue);
					return;
				}
			}
			else
			{
				Zone activeZone2 = The.ActiveZone;
				GameObject gameObject;
				if (activeZone2 == null)
				{
					gameObject = null;
				}
				else
				{
					List<GameObject> objectsWithTagOrProperty = activeZone2.GetObjectsWithTagOrProperty("AmbientIdleSound", true);
					gameObject = ((objectsWithTagOrProperty != null) ? objectsWithTagOrProperty.GetRandomElement<GameObject>(null) : null);
				}
				GameObject gameObject2 = gameObject;
				if (gameObject2 != null && !gameObject2.IsPlayer())
				{
					string soundTag = gameObject2.GetSoundTag("AmbientIdleSound", null);
					if (soundTag != null)
					{
						gameObject2.PlayWorldSound(soundTag, Globals.AmbientVolume * 0.33f, 0f, false, 0f, 1f, 1f, int.MaxValue);
					}
				}
			}
		}

		// Token: 0x0600365D RID: 13917 RVA: 0x00106298 File Offset: 0x00104498
		[WishCommand(null, null, Command = "ambient")]
		public static bool TestAmbient(string beds)
		{
			string[] array = beds.Split(' ', StringSplitOptions.None);
			AmbientSoundsSystem.PlayAmbientBeds((array.Length >= 1) ? ("Sounds/Ambiences/" + array[0]) : null, (array.Length >= 2) ? ("Sounds/Ambiences/" + array[1]) : null, (array.Length >= 3) ? ("Sounds/Ambiences/" + array[2]) : null, null);
			return true;
		}

		// Token: 0x0600365E RID: 13918 RVA: 0x00106300 File Offset: 0x00104500
		[WishCommand(null, null, Command = "ambientcreature")]
		public static bool TestAmbientCreature(string bed)
		{
			AmbientSoundsSystem.PlayAmbientBeds(The.ActiveZone, null, "Sounds/Ambiences/amb_creature_" + bed, null, null);
			return true;
		}

		// Token: 0x0600365F RID: 13919 RVA: 0x00106330 File Offset: 0x00104530
		public static void PlayAmbientBeds(string First = null, string Second = null, string Third = null, float? Volume = null)
		{
			if (Volume == null)
			{
				int num = ((First != null) ? 1 : 0) + ((Second != null) ? 1 : 0) + ((Third != null) ? 1 : 0);
				float num2;
				if (num != 2)
				{
					if (num != 3)
					{
						num2 = 1f;
					}
					else
					{
						num2 = 0.6f;
					}
				}
				else
				{
					num2 = 0.8f;
				}
				Volume = new float?(num2);
			}
			SoundManager.PlayMusic(First, "ambient_bed", true, 12f, Volume.Value, null, true);
			SoundManager.PlayMusic(Second, "ambient_bed_2", true, 12f, Volume.Value, null, true);
			SoundManager.PlayMusic(Third, "ambient_bed_3", true, 12f, Volume.Value, null, true);
		}

		// Token: 0x06003660 RID: 13920 RVA: 0x001063D4 File Offset: 0x001045D4
		public static void PlayAmbientBeds(Zone Zone, string First = null, string Second = null, string Third = null, float? Volume = null)
		{
			if (Zone == null)
			{
				AmbientSoundsSystem.PlayAmbientBeds(null, null, null, null);
				return;
			}
			if (First == null)
			{
				int z = Zone.Z;
				string text;
				if (z > 20)
				{
					if (z <= 40)
					{
						if (z <= 30)
						{
							text = "Sounds/Ambiences/amb_bed_caves_deep";
						}
						else
						{
							text = "Sounds/Ambiences/amb_bed_caves_deeper";
						}
					}
					else
					{
						text = "Sounds/Ambiences/amb_bed_caves_deepest";
					}
				}
				else if (z <= 10)
				{
					text = Zone.AmbientBed;
				}
				else
				{
					text = "Sounds/Ambiences/amb_bed_caves";
				}
				First = text;
			}
			if (Second == null)
			{
				Second = Zone.GetZoneProperty("ambient_bed_2", null);
			}
			if (Third == null)
			{
				Third = Zone.GetZoneProperty("ambient_bed_3", null);
			}
			if (Volume == null)
			{
				int ambientVolume = Zone.AmbientVolume;
				if (ambientVolume != -1)
				{
					Volume = new float?((float)ambientVolume / 100f);
				}
			}
			AmbientSoundsSystem.PlayAmbientBeds(First, Second, Third, Volume);
		}

		// Token: 0x06003661 RID: 13921 RVA: 0x00106494 File Offset: 0x00104694
		public static void StopAmbientBeds(bool Crossfade = true)
		{
			SoundManager.PlayMusic(null, "ambient_bed", Crossfade, 1f, 1f, null, true);
			SoundManager.PlayMusic(null, "ambient_bed_2", Crossfade, 1f, 1f, null, true);
			SoundManager.PlayMusic(null, "ambient_bed_3", Crossfade, 1f, 1f, null, true);
		}

		// Token: 0x06003662 RID: 13922 RVA: 0x001064E9 File Offset: 0x001046E9
		public static void SetAmbientSounds(Zone Zone)
		{
			if (Zone == null || Zone.AmbientSounds.IsNullOrEmpty())
			{
				AmbientSoundsSystem.Sounds = null;
				return;
			}
			AmbientSoundsSystem.Sounds = Zone.AmbientSounds.Split(',', StringSplitOptions.None);
		}

		// Token: 0x06003663 RID: 13923 RVA: 0x00106515 File Offset: 0x00104715
		public override void Register(XRLGame Game, IEventRegistrar Registrar)
		{
			Registrar.Register(ZoneActivatedEvent.ID, 0, false);
		}

		// Token: 0x06003664 RID: 13924 RVA: 0x00106524 File Offset: 0x00104724
		public override bool HandleEvent(ZoneActivatedEvent E)
		{
			if (AmbientSoundsSystem.Enabled)
			{
				AmbientSoundsSystem.Timer.Restart();
				AmbientSoundsSystem.PlayAmbientBeds(E.Zone, null, null, null, null);
				AmbientSoundsSystem.SetAmbientSounds(E.Zone);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x04001B35 RID: 6965
		[NonSerialized]
		public static Stopwatch Timer = new Stopwatch();

		// Token: 0x04001B36 RID: 6966
		[NonSerialized]
		public static float NextSound = 2000f;

		// Token: 0x04001B37 RID: 6967
		[NonSerialized]
		public static bool Enabled = false;

		// Token: 0x04001B38 RID: 6968
		[NonSerialized]
		public static string[] Sounds = null;

		// Token: 0x04001B39 RID: 6969
		[NonSerialized]
		public static List<Cell> EmptyCells = new List<Cell>(2000);
	}
}

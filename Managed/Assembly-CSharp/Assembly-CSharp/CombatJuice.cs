using System;
using System.Collections.Generic;
using System.Diagnostics;
using ConsoleLib.Console;
using Genkit;
using UnityEngine;
using XRL.Core;
using XRL.UI;
using XRL.World;

// Token: 0x02000042 RID: 66
public static class CombatJuice
{
	// Token: 0x17000041 RID: 65
	// (get) Token: 0x06000231 RID: 561 RVA: 0x0000B3CE File Offset: 0x000095CE
	public static GameManager gameManager
	{
		get
		{
			return GameManager.Instance;
		}
	}

	// Token: 0x17000042 RID: 66
	// (get) Token: 0x06000232 RID: 562 RVA: 0x0000B3D5 File Offset: 0x000095D5
	public static bool soundsEnabled
	{
		get
		{
			return Options.UseCombatSounds;
		}
	}

	// Token: 0x17000043 RID: 67
	// (get) Token: 0x06000233 RID: 563 RVA: 0x0000B3DC File Offset: 0x000095DC
	public static bool enabled
	{
		get
		{
			return Options.UseOverlayCombatEffects;
		}
	}

	// Token: 0x17000044 RID: 68
	// (get) Token: 0x06000234 RID: 564 RVA: 0x0000B3E3 File Offset: 0x000095E3
	public static CombatJuiceManager juiceManager
	{
		get
		{
			return CombatJuiceManager.instance;
		}
	}

	// Token: 0x06000235 RID: 565 RVA: 0x0000B3EC File Offset: 0x000095EC
	public static void _setLowHPIndicator(float percent)
	{
		if (CombatJuice.lowHPBlend == null)
		{
			CombatJuice.lowHPBlend = GameManager.MainCamera.GetComponent<CC_Blend>();
		}
		if (CombatJuice.lowHPBlend != null)
		{
			if (percent <= 0f && CombatJuice.lowHPBlend.enabled)
			{
				CombatJuice.lowHPBlend.enabled = false;
				return;
			}
			if (percent > 0f)
			{
				if (!CombatJuice.lowHPBlend.enabled)
				{
					CombatJuice.lowHPBlend.enabled = true;
				}
				CombatJuice.lowHPBlend.amount = percent * 0.5f;
			}
		}
	}

	// Token: 0x06000236 RID: 566 RVA: 0x0000B474 File Offset: 0x00009674
	public static global::UnityEngine.GameObject getInstance(string name, string root)
	{
		global::UnityEngine.GameObject gameObject = null;
		if (!CombatJuice.roots.ContainsKey(root))
		{
			if (!CombatJuice.roots.ContainsKey("_JuiceRoot"))
			{
				global::UnityEngine.GameObject gameObject2 = new global::UnityEngine.GameObject();
				gameObject2.name = "_JuiceRoot";
				gameObject2.transform.position = new Vector3(0f, 0f, 0f);
				CombatJuice.roots.Add("_JuiceRoot", gameObject2);
			}
			global::UnityEngine.GameObject gameObject3 = new global::UnityEngine.GameObject();
			gameObject3.transform.parent = CombatJuice.roots["_JuiceRoot"].transform;
			gameObject3.name = root;
			gameObject3.transform.position = new Vector3(0f, 0f, 0f);
			CombatJuice.roots.Add(root, gameObject3);
		}
		if (!CombatJuice.prefabPool.ContainsKey(name))
		{
			CombatJuice.prefabPool.Add(name, new Queue<global::UnityEngine.GameObject>());
		}
		if (CombatJuice.prefabPool.ContainsKey(name) && CombatJuice.prefabPool[name].Count > 0)
		{
			gameObject = CombatJuice.prefabPool[name].Dequeue();
		}
		if (!CombatJuice.prefabAnimations.ContainsKey(name))
		{
			CombatJuice.prefabAnimations.Add(name, Resources.Load("Prefabs/" + name) as global::UnityEngine.GameObject);
		}
		if (gameObject == null)
		{
			if (!CombatJuice.prefabAnimations.ContainsKey(name))
			{
				MetricsManager.LogError("Can't find animation name " + name);
				return null;
			}
			gameObject = global::UnityEngine.Object.Instantiate<global::UnityEngine.GameObject>(CombatJuice.prefabAnimations[name]);
			gameObject.transform.SetParent(CombatJuice.roots[root].transform, true);
		}
		gameObject.SetActive(true);
		return gameObject;
	}

	// Token: 0x06000237 RID: 567 RVA: 0x0000B613 File Offset: 0x00009813
	public static void pool(string name, global::UnityEngine.GameObject prefab, bool disableFirst = true)
	{
		if (disableFirst)
		{
			prefab.SetActive(false);
		}
		CombatJuice.prefabPool[name].Enqueue(prefab);
	}

	// Token: 0x06000238 RID: 568 RVA: 0x0000B630 File Offset: 0x00009830
	public static void _cameraShake(float duration)
	{
		CameraShake.shakeDuration += duration;
	}

	// Token: 0x06000239 RID: 569 RVA: 0x0000B63E File Offset: 0x0000983E
	public static void cameraShake(float duration, bool Async = false)
	{
		CombatJuiceManager.enqueueEntry(new CombatJuiceEntryCameraShake(duration), Async);
	}

	// Token: 0x0600023A RID: 570 RVA: 0x0000B64C File Offset: 0x0000984C
	public static void _text(Vector3 start, Vector3 end, string text, Color color, float floatTime, float scale = 1f)
	{
		global::UnityEngine.GameObject instance = CombatJuice.getInstance("CombatJuice/CombatJuiceText", "CombatJuiceText");
		instance.transform.position = start;
		instance.transform.localScale = new Vector3(scale, scale, 1f);
		instance.GetComponent<SimpleTextMeshTweener>().init(start, end, color, new Color(color.r, color.g, color.b, 0f), floatTime, "CombatJuice/CombatJuiceText");
		TextMesh component = instance.GetComponent<TextMesh>();
		component.color = color;
		component.text = text;
	}

	// Token: 0x0600023B RID: 571 RVA: 0x0000B6D0 File Offset: 0x000098D0
	public static void _playPrefabAnimation(Vector3 location, string name, string objectId = null, string configurationString = null, object configurationObject = null)
	{
		global::UnityEngine.GameObject animation = CombatJuice.getInstance(name, "PrefabAnimation_" + name);
		animation.transform.position = location;
		CombatJuice.ICombatJuiceConfigurable component = animation.GetComponent<CombatJuice.ICombatJuiceConfigurable>();
		if (component != null)
		{
			component.configureWithObject(configurationObject);
			component.configure(configurationString);
		}
		animation.GetComponent<CombatJuice.ICombatJuiceAnimator>().Play(false, delegate
		{
			CombatJuice.pool(name, animation, true);
		}, name, objectId);
	}

	// Token: 0x0600023C RID: 572 RVA: 0x0000B75E File Offset: 0x0000995E
	public static void _playPrefabAnimation(int x, int y, string name)
	{
		CombatJuice.playPrefabAnimation(GameManager.Instance.getTileCenter(x, y, 100), name, null, null, null, false);
	}

	// Token: 0x0600023D RID: 573 RVA: 0x0000B77C File Offset: 0x0000997C
	public static void StopPrefabAnimation(string Name)
	{
		if (GameManager.IsOnUIContext())
		{
			global::UnityEngine.GameObject gameObject;
			if (CombatJuice.roots.TryGetValue("PrefabAnimation_" + Name, out gameObject))
			{
				Transform transform = gameObject.transform;
				int i = 0;
				int childCount = transform.childCount;
				while (i < childCount)
				{
					global::UnityEngine.GameObject gameObject2 = transform.GetChild(i).gameObject;
					if (gameObject2.activeSelf)
					{
						CombatJuice.ICombatJuiceAnimator component = gameObject2.GetComponent<CombatJuice.ICombatJuiceAnimator>();
						if (component != null)
						{
							component.Stop();
						}
					}
					i++;
				}
				return;
			}
		}
		else
		{
			GameManager.Instance.uiQueue.queueTask(delegate
			{
				CombatJuice.StopPrefabAnimation(Name);
			}, 0);
		}
	}

	// Token: 0x0600023E RID: 574 RVA: 0x0000B820 File Offset: 0x00009A20
	public static void playWorldSound(XRL.World.GameObject obj, string clip, float volume = 0.5f, float pitchVariance = 0f, float t = 0f, float delay = 0f)
	{
		if (!CombatJuice.soundsEnabled)
		{
			return;
		}
		if (obj != null && obj.IsValid() && obj.IsVisible())
		{
			CombatJuiceEntryWorldSound juiceWorldSound = obj.Physics.GetJuiceWorldSound(clip, volume, pitchVariance, delay);
			if (juiceWorldSound != null)
			{
				juiceWorldSound.t = t;
				CombatJuiceManager.enqueueEntry(juiceWorldSound, false);
			}
		}
	}

	// Token: 0x0600023F RID: 575 RVA: 0x0000B86C File Offset: 0x00009A6C
	public static void _playPrefabAnimation(XRL.World.GameObject gameObject, string animation)
	{
		try
		{
			if (gameObject != null && gameObject.IsVisible() && gameObject.IsValid())
			{
				CombatJuice.playPrefabAnimation(GameManager.Instance.getTileCenter(gameObject.Physics.CurrentCell.Location.X, gameObject.Physics.CurrentCell.Location.Y, 100), animation, null, null, null, false);
			}
		}
		catch (Exception ex)
		{
			MetricsManager.LogException("CombatJuice::playPrefabAnimation", ex, "game_exception");
		}
	}

	// Token: 0x06000240 RID: 576 RVA: 0x0000B8F4 File Offset: 0x00009AF4
	public static void floatingText(Cell C, string text, Color color, float duration = 1.5f, float floatLength = 24f, float scale = 1f, bool ignoreVisibility = true, XRL.World.GameObject gameObject = null)
	{
		if (!Options.UseCombatText)
		{
			return;
		}
		if (C != null && (ignoreVisibility || C.IsVisible()))
		{
			Vector3 vector = CombatJuice.gameManager.getTileCenter(C.X, C.Y, 100) + new Vector3(0f, 12f, 0f);
			Vector3 vector2 = vector + new Vector3(0f, floatLength, 0f);
			CombatJuiceManager.enqueueEntry(new CombatJuiceEntryText(vector, vector2, duration, text, color, gameObject, scale), false);
		}
	}

	// Token: 0x06000241 RID: 577 RVA: 0x0000B974 File Offset: 0x00009B74
	public static void floatingText(XRL.World.GameObject gameObject, string text, Color color, float duration = 1.5f, float floatLength = 24f, float scale = 1f, bool ignoreVisibility = false)
	{
		if (!Options.UseCombatText)
		{
			return;
		}
		if (XRL.World.GameObject.Validate(ref gameObject) && (ignoreVisibility || gameObject.IsVisible()))
		{
			CombatJuice.floatingText(gameObject.CurrentCell, text, color, duration, floatLength, scale, true, gameObject);
		}
	}

	// Token: 0x06000242 RID: 578 RVA: 0x0000B9A7 File Offset: 0x00009BA7
	public static CombatJuiceEntryMissileWeaponVFX missileWeaponVFX(MissileWeaponVFXConfiguration configuration, bool Async = false)
	{
		CombatJuiceEntryMissileWeaponVFX combatJuiceEntryMissileWeaponVFX = new CombatJuiceEntryMissileWeaponVFX();
		combatJuiceEntryMissileWeaponVFX.configure(configuration);
		CombatJuiceManager.enqueueEntry(combatJuiceEntryMissileWeaponVFX, Async);
		return combatJuiceEntryMissileWeaponVFX;
	}

	// Token: 0x06000243 RID: 579 RVA: 0x0000B9BC File Offset: 0x00009BBC
	public static CombatJuiceEntryPunch punch(XRL.World.GameObject Attacker, XRL.World.GameObject Defender, float Time = 0.2f, Easing.Functions Ease = Easing.Functions.SineEaseInOut, float FromXOffset = 0f, float FromYOffset = 0f, float ToXOffset = 0f, float ToYOffset = 0f, float DistanceFactor = 1f, string SoundTag = "PunchSound", string DefaultSound = null, bool AllowPlayerSound = false, bool Async = false)
	{
		try
		{
			if (XRL.World.GameObject.Validate(ref Attacker) && XRL.World.GameObject.Validate(ref Defender) && Attacker.IsInActiveZone() && Defender.IsInActiveZone())
			{
				if ((AllowPlayerSound || !Attacker.IsPlayer()) && (!SoundTag.IsNullOrEmpty() || !DefaultSound.IsNullOrEmpty()))
				{
					Attacker.PlayWorldSound(Attacker.GetSoundTag(SoundTag, null).Coalesce(DefaultSound), 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				}
				if (Attacker.IsVisible())
				{
					Zone currentZone = Attacker.CurrentZone;
					Zone currentZone2 = Defender.CurrentZone;
					Location2D location = Attacker.CurrentCell.Location;
					Location2D location2 = Defender.CurrentCell.Location;
					if (DistanceFactor != 1f)
					{
						ToXOffset += (float)(location.X - location2.X) * DistanceFactor;
						ToYOffset += (float)(location.Y - location2.Y) * DistanceFactor;
					}
					if (currentZone == currentZone2)
					{
						Location2D location2D = location;
						Location2D location2D2 = location2;
						float num = ToXOffset;
						float num2 = ToYOffset;
						return CombatJuice.punch(location2D, location2D2, Time, Ease, FromXOffset, FromYOffset, num, num2);
					}
					int x = location.X;
					int y = location.Y;
					int num3;
					int num4;
					if (currentZone.Z < currentZone2.Z)
					{
						num3 = Math.Min(Math.Max(x + 1, 0), currentZone.Width);
						num4 = Math.Min(Math.Max(y + 1, 0), currentZone.Height);
						if (num3 != x)
						{
							ToXOffset -= 0.5f;
						}
						if (num4 != y)
						{
							ToYOffset -= 0.5f;
						}
					}
					else if (currentZone.Z > currentZone2.Z)
					{
						num3 = Math.Min(Math.Max(x - 1, 0), currentZone.Width);
						num4 = Math.Min(Math.Max(y - 1, 0), currentZone.Height);
						if (num3 != x)
						{
							ToXOffset += 0.5f;
						}
						if (num4 != y)
						{
							ToYOffset += 0.5f;
						}
					}
					else
					{
						num3 = location2.X;
						num4 = location2.Y;
						if (num3 > x + 1 || num3 < x - 1)
						{
							num3 = x;
							if (num4 == y)
							{
								ToYOffset += ((num4 > 0) ? 0.5f : (-0.5f));
								num4 += ((num4 > 0) ? (-1) : 1);
							}
						}
						if (num4 > y + 1 || num4 < y - 1)
						{
							num4 = y;
							if (num3 == x)
							{
								ToXOffset += ((num3 > 0) ? 0.5f : (-0.5f));
								num3 += ((num3 > 0) ? (-1) : 1);
							}
						}
					}
					int num5 = x;
					int num6 = y;
					int num7 = num3;
					int num8 = num4;
					float num9 = ToXOffset;
					float num10 = ToYOffset;
					return CombatJuice.punch(num5, num6, num7, num8, Time, Ease, FromXOffset, FromYOffset, num9, num10, Async);
				}
			}
		}
		catch (Exception ex)
		{
			MetricsManager.LogException("CombatJuice::punch", ex, "game_exception");
		}
		return null;
	}

	// Token: 0x06000244 RID: 580 RVA: 0x0000BCA4 File Offset: 0x00009EA4
	public static CombatJuiceEntryPunch punch(Location2D AttackerCellLocation, Location2D DefenderCellLocation, float Time = 0.2f, Easing.Functions Ease = Easing.Functions.SineEaseInOut, float FromXOffset = 0f, float FromYOffset = 0f, float ToXOffset = 0f, float ToYOffset = 0f)
	{
		if (AttackerCellLocation == null || DefenderCellLocation == null || AttackerCellLocation == DefenderCellLocation)
		{
			return null;
		}
		return CombatJuice.punch(AttackerCellLocation.X, AttackerCellLocation.Y, DefenderCellLocation.X, DefenderCellLocation.Y, Time, Ease, FromXOffset, FromYOffset, ToXOffset, ToYOffset, false);
	}

	// Token: 0x06000245 RID: 581 RVA: 0x0000BD00 File Offset: 0x00009F00
	public static CombatJuiceEntryPunch punch(int FromX, int FromY, int ToX, int ToY, float Time = 0.2f, Easing.Functions Ease = Easing.Functions.SineEaseInOut, float FromXOffset = 0f, float FromYOffset = 0f, float ToXOffset = 0f, float ToYOffset = 0f, bool Async = false)
	{
		ex3DSprite2 tile = CombatJuice.gameManager.getTile(FromX, FromY);
		Vector3 vector = CombatJuice.gameManager.getTileCenter(FromX, FromY, 0) + new Vector3(FromXOffset, FromYOffset, 0f);
		Vector3 vector2 = CombatJuice.gameManager.getTileCenter(ToX, ToY, 0) + new Vector3(ToXOffset, ToYOffset, 0f);
		CombatJuiceEntryPunch combatJuiceEntryPunch = new CombatJuiceEntryPunch(tile, vector, vector2, Time, Ease);
		CombatJuiceManager.enqueueEntry(combatJuiceEntryPunch, Async);
		return combatJuiceEntryPunch;
	}

	// Token: 0x06000246 RID: 582 RVA: 0x0000BD70 File Offset: 0x00009F70
	public static void Hover(Location2D Location, float Duration = 10f, float Rise = 1f)
	{
		ex3DSprite2 tile = CombatJuice.gameManager.getTile(Location.X, Location.Y);
		Vector3 tileCenter = CombatJuice.gameManager.getTileCenter(Location.X, Location.Y, 0);
		CombatJuiceManager.enqueueEntry(new CombatJuiceEntryHover(tile, tileCenter, 12.5f, Duration, Rise), true);
	}

	// Token: 0x06000247 RID: 583 RVA: 0x0000BDC0 File Offset: 0x00009FC0
	public static CombatJuiceEntryJump Jump(XRL.World.GameObject Actor, Location2D Location, Location2D Target, float Duration = 10f, float Arc = 0.5f, float Scale = 1f, bool Focus = false, bool Enqueue = true)
	{
		Vector3 tileCenter = CombatJuice.gameManager.getTileCenter(Location.X, Location.Y, 0);
		Vector3 tileCenter2 = CombatJuice.gameManager.getTileCenter(Target.X, Target.Y, 0);
		CombatJuiceEntryJump combatJuiceEntryJump = new CombatJuiceEntryJump(Actor.RenderForUI(null, false), tileCenter, tileCenter2, Duration, Arc, Scale, Focus);
		if (Enqueue)
		{
			CombatJuiceManager.enqueueEntry(combatJuiceEntryJump, true);
		}
		return combatJuiceEntryJump;
	}

	// Token: 0x06000248 RID: 584 RVA: 0x0000BE20 File Offset: 0x0000A020
	public static CombatJuiceEntryMissileWeaponVFX Throw(XRL.World.GameObject Actor, XRL.World.GameObject Weapon, Location2D Location, Location2D Target, bool Async = false)
	{
		MissileWeaponVFXConfiguration missileWeaponVFXConfiguration = MissileWeaponVFXConfiguration.next();
		MissileWeaponVFXConfiguration.MissileVFXPathDefinition path = missileWeaponVFXConfiguration.GetPath(0);
		path.addStep(Location);
		path.addStep(Target);
		path.SetParameter("Tile", "Inherit");
		path.SetParameter("Foreground", "Inherit");
		path.SetParameter("Detail", "Inherit");
		path.SetParameter("Arc", "16");
		path.SetParameter("Speed", "350");
		path.SetParameter("Orientation", "0,360");
		path.SetParameter("Rotation", "240,480");
		path.projectileVFX = Weapon.GetPropertyOrTag("ThrowVFX", "MissileWeaponsEffects/3cprojectile");
		Dictionary<string, Dictionary<string, string>> xTags = Weapon.GetBlueprint(true).xTags;
		Dictionary<string, string> dictionary;
		if (xTags != null && xTags.TryGetValue("ThrowVFX", out dictionary))
		{
			path.SetProjectileVFX(dictionary);
		}
		path.SetProjectileRender(Weapon);
		ConfigureMissileVisualEffectEvent.Send(missileWeaponVFXConfiguration, path, Actor, null, Weapon);
		return CombatJuice.missileWeaponVFX(missileWeaponVFXConfiguration, Async);
	}

	// Token: 0x06000249 RID: 585 RVA: 0x0000BF0C File Offset: 0x0000A10C
	public static void BlockUntilAllFinished(int MaxMilliseconds = 10000, bool Interruptible = false)
	{
		XRLCore core = XRLCore.Core;
		Stopwatch frameTimer = XRLCore.FrameTimer;
		long num = frameTimer.ElapsedMilliseconds + (long)MaxMilliseconds;
		while (CombatJuiceManager.AnyActive() && frameTimer.ElapsedMilliseconds < num)
		{
			if (Interruptible && Keyboard.kbhit())
			{
				CombatJuice.finishAll();
				return;
			}
			core.RenderBase(false, false);
		}
	}

	// Token: 0x0600024A RID: 586 RVA: 0x0000BF5C File Offset: 0x0000A15C
	public static void BlockUntilFinished(CombatJuiceEntry Entry, IList<XRL.World.GameObject> Hide = null, int MaxMilliseconds = 10000, bool Interruptible = true)
	{
		if (Entry == null)
		{
			return;
		}
		if (!Hide.IsNullOrEmpty<XRL.World.GameObject>())
		{
			for (int i = Hide.Count - 1; i >= 0; i--)
			{
				XRL.World.GameObject gameObject = Hide[i];
				if (gameObject.Render == null || !gameObject.Render.Visible)
				{
					Hide[i] = null;
				}
				else
				{
					gameObject.Render.Visible = false;
				}
			}
		}
		XRLCore core = XRLCore.Core;
		Stopwatch frameTimer = XRLCore.FrameTimer;
		long num = frameTimer.ElapsedMilliseconds + (long)MaxMilliseconds;
		while (!Entry.finished && frameTimer.ElapsedMilliseconds < num)
		{
			if (Interruptible && Keyboard.kbhit())
			{
				CombatJuice.finishAll();
				break;
			}
			XRLCore.RenderHiddenPlayer = Hide == null;
			core.RenderBase(false, false);
		}
		if (!Hide.IsNullOrEmpty<XRL.World.GameObject>())
		{
			for (int j = Hide.Count - 1; j >= 0; j--)
			{
				if (Hide[j] != null)
				{
					Hide[j].Render.Visible = true;
				}
			}
		}
	}

	// Token: 0x0600024B RID: 587 RVA: 0x0000C048 File Offset: 0x0000A248
	public static void BlockUntilFinished(CombatJuiceEntry Entry, Action Render, int MaxMilliseconds = 10000, bool Interruptible = true)
	{
		Stopwatch frameTimer = XRLCore.FrameTimer;
		long num = frameTimer.ElapsedMilliseconds + (long)MaxMilliseconds;
		while (!Entry.finished && frameTimer.ElapsedMilliseconds < num)
		{
			if (Interruptible && Keyboard.kbhit())
			{
				CombatJuice.finishAll();
				return;
			}
			Render();
		}
	}

	// Token: 0x0600024C RID: 588 RVA: 0x0000C08E File Offset: 0x0000A28E
	public static void clearUpToTurn(long n)
	{
		CombatJuiceManager.clearUpToTurn(n);
	}

	// Token: 0x0600024D RID: 589 RVA: 0x0000C096 File Offset: 0x0000A296
	public static void finishAll()
	{
		GameManager.Instance.uiQueue.queueTask(new Action(CombatJuiceManager.finishAll), 0);
	}

	// Token: 0x0600024E RID: 590 RVA: 0x0000C0B8 File Offset: 0x0000A2B8
	public static void startTurn()
	{
		CombatJuice.juiceTurn += 1L;
		GameManager.Instance.uiQueue.queueSingletonTask("combatJuiceClearTurn", delegate
		{
			try
			{
				CombatJuice.clearUpToTurn(CombatJuice.juiceTurn - 2L);
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("CombatJuice::startTurn", ex, "game_exception");
			}
		}, false);
	}

	// Token: 0x0600024F RID: 591 RVA: 0x0000C107 File Offset: 0x0000A307
	public static CombatJuiceEntryPrefabAnimation playPrefabAnimation(Vector3 location, string animation, string objectID = null, string configurationString = null, object configurationObject = null, bool async = false)
	{
		CombatJuiceEntryPrefabAnimation combatJuiceEntryPrefabAnimation = new CombatJuiceEntryPrefabAnimation(location, animation, objectID, configurationString, configurationObject);
		combatJuiceEntryPrefabAnimation.async = async;
		CombatJuiceManager.enqueueEntry(combatJuiceEntryPrefabAnimation, async);
		return combatJuiceEntryPrefabAnimation;
	}

	// Token: 0x06000250 RID: 592 RVA: 0x0000C124 File Offset: 0x0000A324
	public static CombatJuiceEntryPrefabAnimation playPrefabAnimation(XRL.World.GameObject gameObject, string animation, string objectId = null, string configurationString = null, object configurationObject = null, bool async = false)
	{
		if (gameObject != null && gameObject.IsValid() && gameObject.IsVisible())
		{
			return CombatJuice.playPrefabAnimation(gameObject.Physics.CurrentCell.Location, animation, objectId, configurationString, null, async);
		}
		return null;
	}

	// Token: 0x06000251 RID: 593 RVA: 0x0000C156 File Offset: 0x0000A356
	public static CombatJuiceEntryPrefabAnimation playPrefabAnimation(Location2D cellLocation, string animation, string objectId = null, string configurationString = null, object configurationObject = null, bool async = false)
	{
		return CombatJuice.playPrefabAnimation(CombatJuice.gameManager.getTileCenter(cellLocation.X, cellLocation.Y, 100), animation, objectId, configurationString, configurationObject, async);
	}

	// Token: 0x0400012F RID: 303
	public static Dictionary<string, global::UnityEngine.GameObject> prefabAnimations = new Dictionary<string, global::UnityEngine.GameObject>();

	// Token: 0x04000130 RID: 304
	public static Dictionary<string, Queue<global::UnityEngine.GameObject>> prefabPool = new Dictionary<string, Queue<global::UnityEngine.GameObject>>();

	// Token: 0x04000131 RID: 305
	public static CC_Blend lowHPBlend = null;

	// Token: 0x04000132 RID: 306
	public static Dictionary<string, global::UnityEngine.GameObject> roots = new Dictionary<string, global::UnityEngine.GameObject>();

	// Token: 0x04000133 RID: 307
	public static long juiceTurn = 0L;

	// Token: 0x02001569 RID: 5481
	public interface ICombatJuiceConfigurable
	{
		// Token: 0x0600D523 RID: 54563 RVA: 0x004F335A File Offset: 0x004F155A
		void configureWithObject(object args)
		{
		}

		// Token: 0x0600D524 RID: 54564 RVA: 0x004F335C File Offset: 0x004F155C
		void configure(string configurationString)
		{
		}
	}

	// Token: 0x0200156A RID: 5482
	public interface ICombatJuiceAnimator
	{
		// Token: 0x0600D525 RID: 54565
		void Play(bool loop = false, Action after = null, string name = null, string objectId = null);

		// Token: 0x0600D526 RID: 54566
		void Stop();
	}
}

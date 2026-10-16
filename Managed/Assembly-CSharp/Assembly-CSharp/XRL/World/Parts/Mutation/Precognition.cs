using System;
using System.Collections;
using System.Collections.Generic;
using Qud.UI;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;
using XRL.Core;
using XRL.UI;
using XRL.World.Capabilities;
using XRL.World.Parts.Skill;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x020013F4 RID: 5108
	[Serializable]
	public class Precognition : BaseMutation
	{
		// Token: 0x0600C5E1 RID: 50657 RVA: 0x0048062F File Offset: 0x0047E82F
		public Precognition()
		{
			base.Type = "Mental";
		}

		// Token: 0x0600C5E2 RID: 50658 RVA: 0x0048064C File Offset: 0x0047E84C
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			bool flag = stats.mode.Contains("ability");
			ActivatedAbilityEntry activatedAbilityEntry = base.MyActivatedAbility(this.ActivatedAbilityID, null);
			int duration = this.GetDuration(Level);
			int num = duration;
			GameObject parentObject = activatedAbilityEntry.ParentObject;
			if (parentObject != null && parentObject.HasPart<Nonlinearity_Tomorrowful>())
			{
				stats.postfix += string.Format("Duration increased by {0} due to the Tomorrowful skill.", duration);
				num = duration * 2;
			}
			stats.Set("Duration", num, !flag || duration != num, num - duration);
			stats.CollectCooldownTurns(activatedAbilityEntry, this.GetCooldown(Level), 0);
		}

		// Token: 0x0600C5E3 RID: 50659 RVA: 0x004806E6 File Offset: 0x0047E8E6
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetDefensiveAbilityListEvent.ID || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600C5E4 RID: 50660 RVA: 0x00480704 File Offset: 0x0047E904
		public override bool HandleEvent(AIGetDefensiveAbilityListEvent E)
		{
			if (this.TurnsLeft <= 0 && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null))
			{
				if (E.Distance > 1 && E.Actor.hitpoints >= E.Actor.baseHitpoints)
				{
					int? num = E.Actor.Con(null, true);
					int num2 = 5;
					if (!((num.GetValueOrDefault() >= num2) & (num != null)))
					{
						goto IL_0094;
					}
				}
				if (base.CheckMyRealityDistortionAdvisability(null, null, null, null, null, null, 0) && 70.in100())
				{
					E.Add("CommandPrecognition", 1, null, false, false, null, null);
				}
			}
			IL_0094:
			return base.HandleEvent(E);
		}

		// Token: 0x0600C5E5 RID: 50661 RVA: 0x004807AC File Offset: 0x0047E9AC
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("time", 1);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600C5E6 RID: 50662 RVA: 0x004807CF File Offset: 0x0047E9CF
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600C5E7 RID: 50663 RVA: 0x004807D4 File Offset: 0x0047E9D4
		public static void OnPostRenderCallback(Camera Camera)
		{
			try
			{
				if (Camera == GameManager.cameraMainCamera)
				{
					if (!Precognition.TextureCaptured)
					{
						Precognition.Texture.ReadPixels(new Rect(0f, 0f, (float)Precognition.Texture.width, (float)Precognition.Texture.height), 0, 0, false);
						NativeArray<Color32> pixelData = Precognition.Texture.GetPixelData<Color32>(0);
						int i = 0;
						int length = pixelData.Length;
						while (i < length)
						{
							Color color = pixelData[i];
							float num = color.grayscale * color.grayscale;
							pixelData[i] = new Color(num, num, num);
							i++;
						}
						Precognition.Texture.Apply(false);
						Precognition.TextureCaptured = true;
						Camera.onPostRender = (Camera.CameraCallback)Delegate.Remove(Camera.onPostRender, new Camera.CameraCallback(Precognition.OnPostRenderCallback));
					}
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("Precognition:TextureCapture", ex, "game_exception");
				Camera.onPostRender = (Camera.CameraCallback)Delegate.Remove(Camera.onPostRender, new Camera.CameraCallback(Precognition.OnPostRenderCallback));
			}
		}

		// Token: 0x0600C5E8 RID: 50664 RVA: 0x00480900 File Offset: 0x0047EB00
		public static void CaptureGlimpse(Guid GlimpseID)
		{
			if (!SystemInfo.SupportsTextureFormat(TextureFormat.RGBA32))
			{
				return;
			}
			try
			{
				int width = Screen.width;
				int height = Screen.height;
				if (Precognition.Texture)
				{
					if (Precognition.Texture.width != width || Precognition.Texture.height != height)
					{
						Precognition.Texture.Reinitialize(width, height, TextureFormat.RGBA32, false);
					}
				}
				else
				{
					Precognition.Texture = new Texture2D(width, height, TextureFormat.RGBA32, false);
				}
				Precognition.CurrentID = GlimpseID;
				Precognition.TextureCaptured = false;
				Camera.onPostRender = (Camera.CameraCallback)Delegate.Combine(Camera.onPostRender, new Camera.CameraCallback(Precognition.OnPostRenderCallback));
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("Precognition:TextureInit", ex, "game_exception");
				Precognition.CurrentID = Guid.Empty;
			}
		}

		// Token: 0x0600C5E9 RID: 50665 RVA: 0x004809C4 File Offset: 0x0047EBC4
		public static IEnumerator GlimpseRoutine(Guid GlimpseID)
		{
			Color color = Color.white;
			Sprite sprite = null;
			if (GlimpseID == Precognition.CurrentID && Precognition.TextureCaptured)
			{
				try
				{
					sprite = Sprite.Create(Precognition.Texture, new Rect(0f, 0f, (float)Precognition.Texture.width, (float)Precognition.Texture.height), new Vector2(0.5f, 0.5f));
				}
				catch (Exception ex)
				{
					MetricsManager.LogException("Precognition:CreateSprite", ex, "game_exception");
					sprite = null;
				}
			}
			if (sprite == null)
			{
				sprite = UIManager.instance.DefaultSquare;
				color = Color.black;
			}
			GameObject obj = new GameObject("Glimpse");
			RectTransform rectTransform = obj.AddComponent<RectTransform>();
			Image image = obj.AddComponent<Image>();
			image.sprite = sprite;
			int threshold = Shader.PropertyToID("_Threshold");
			Material material = (image.material = Resources.Load<Material>("Materials/FX_ImageTransition"));
			rectTransform.SetParent(UIManager.instance.gameObject.transform);
			rectTransform.anchorMax = new Vector2(1f, 1f);
			rectTransform.anchorMin = new Vector2(0f, 0f);
			rectTransform.offsetMax = default(Vector2);
			rectTransform.offsetMin = default(Vector2);
			rectTransform.anchoredPosition = default(Vector2);
			material.color = color;
			material.SetFloat(threshold, 0f);
			obj.SetActive(true);
			float t = 0f;
			while ((Precognition.Reverting || t <= 1f) && t <= 40f)
			{
				yield return null;
				t += Time.deltaTime * 2f;
				material.SetFloat(threshold, t);
			}
			material.color = color.WithAlpha(0f);
			material.SetFloat(threshold, 0f);
			t = 0f;
			while (t <= 1f)
			{
				yield return null;
				t += Time.deltaTime * 2f;
				material.SetFloat(threshold, t);
			}
			obj.SetActive(false);
			obj.Destroy();
			if (sprite != UIManager.instance.DefaultSquare)
			{
				sprite.Destroy();
			}
			Precognition.ClearGlimpses();
			Precognition.Reverting = false;
			yield break;
		}

		// Token: 0x0600C5EA RID: 50666 RVA: 0x004809D3 File Offset: 0x0047EBD3
		public static bool RemoveGlimpse(Guid GlimpseID)
		{
			return true;
		}

		// Token: 0x0600C5EB RID: 50667 RVA: 0x004809D6 File Offset: 0x0047EBD6
		public static void ClearGlimpses()
		{
			Precognition.TextureCaptured = false;
		}

		// Token: 0x0600C5EC RID: 50668 RVA: 0x004809E0 File Offset: 0x0047EBE0
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeforeDie");
			Registrar.Register("EndTurn");
			Registrar.Register("CommandPrecognition");
			Registrar.Register("CommandPrecognitionRevert");
			Registrar.Register("GameRestored");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600C5ED RID: 50669 RVA: 0x00480A2C File Offset: 0x0047EC2C
		public override string GetDescription()
		{
			return "You peer into your near future.";
		}

		// Token: 0x0600C5EE RID: 50670 RVA: 0x00480A34 File Offset: 0x0047EC34
		public override string GetLevelText(int Level)
		{
			return "You may activate this power and then later revert to the point in time when you activated it.\n" + "Duration between use and reversion: {{rules|" + this.GetDuration(Level).ToString() + "}} rounds\n" + "Cooldown: " + this.GetCooldown(Level).ToString() + " rounds";
		}

		// Token: 0x0600C5EF RID: 50671 RVA: 0x00480A82 File Offset: 0x0047EC82
		public int GetCooldown()
		{
			return this.GetCooldown(base.Level);
		}

		// Token: 0x0600C5F0 RID: 50672 RVA: 0x00480A90 File Offset: 0x0047EC90
		public virtual int GetCooldown(int Level)
		{
			return 500;
		}

		// Token: 0x0600C5F1 RID: 50673 RVA: 0x00480A97 File Offset: 0x0047EC97
		public int GetDuration()
		{
			return this.GetDuration(base.Level);
		}

		// Token: 0x0600C5F2 RID: 50674 RVA: 0x00480AA5 File Offset: 0x0047ECA5
		public virtual int GetDuration(int Level)
		{
			return 4 * Level + 12;
		}

		// Token: 0x0600C5F3 RID: 50675 RVA: 0x00480AB0 File Offset: 0x0047ECB0
		public static Guid Save()
		{
			Guid glimpseID = Guid.NewGuid();
			SingletonWindowBase<LoadingStatusWindow>.instance.StayHidden = true;
			The.Core.SaveGame("Precognition");
			GameManager.Instance.uiQueue.queueTask(delegate
			{
				Precognition.CaptureGlimpse(glimpseID);
			}, 0);
			SingletonWindowBase<LoadingStatusWindow>.instance.StayHidden = false;
			return glimpseID;
		}

		// Token: 0x0600C5F4 RID: 50676 RVA: 0x00480B18 File Offset: 0x0047ED18
		public static void Load(Guid GlimpseID, GameObject obj = null)
		{
			Precognition.Reverting = true;
			GameManager.Instance.uiQueue.queueTask(delegate
			{
				GameManager.Instance.StartCoroutine(Precognition.GlimpseRoutine(GlimpseID));
			}, 0);
			Precognition.LoadCommand.Issue();
		}

		// Token: 0x0600C5F5 RID: 50677 RVA: 0x00480B5C File Offset: 0x0047ED5C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "EndTurn")
			{
				if (this.TurnsLeft > 0)
				{
					this.TurnsLeft--;
					if (this.TurnsLeft <= 0)
					{
						base.MyActivatedAbility(this.RevertActivatedAbilityID, null).Enabled = false;
						if (this.WasPlayer && this.ParentObject.IsPlayer() && this.ActivatedSegment < The.Game.Segments)
						{
							if (Popup.ShowYesNo("Your precognition is about to run out. Would you like to return to the start of your vision?", "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.Yes)
							{
								this.ParentObject.PlayWorldSound("Sounds/Abilities/sfx_ability_mutation_precognition_timeRevert", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
								AutoAct.Interrupt(null, null, null, false);
								Precognition.Load(this.GlimpseID, this.ParentObject);
								this.ActivatedSegment = The.Game.Segments + 100L;
							}
							else if (this.GlimpseID != Guid.Empty)
							{
								Precognition.RemoveGlimpse(this.GlimpseID);
							}
						}
					}
				}
			}
			else if (E.ID == "CommandPrecognition")
			{
				if (this.TurnsLeft > 0)
				{
					if (this.ParentObject.IsPlayer())
					{
						Popup.ShowFail("You are already within a precognitive vision.", true, true, true);
					}
					return false;
				}
				if (this.RealityDistortionBased && !this.ParentObject.FireEvent(Event.New("InitiateRealityDistortionLocal", "Object", this.ParentObject, "Mutation", this), E))
				{
					return false;
				}
				SoundManager.PreloadClipSet("Sounds/Abilities/sfx_ability_mutation_precognition_timeRevert");
				this.ParentObject.PlayWorldSound("Sounds/Abilities/sfx_ability_mutation_precognition_activate", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				base.CooldownMyActivatedAbility(this.ActivatedAbilityID, 500, null, null, false);
				if (this.ParentObject.IsPlayer())
				{
					this.GlimpseID = Precognition.Save();
					this.WasPlayer = true;
					IComponent<GameObject>.AddPlayerMessage("You peer into the future.", null, true);
				}
				else
				{
					this.WasPlayer = false;
					if (SensePsychic.SensePsychicFromPlayer(this.ParentObject) != null)
					{
						IComponent<GameObject>.AddPlayerMessage("You sense a subtle psychic disturbance.", null, true);
					}
				}
				this.HitpointsAtSave = this.ParentObject.hitpoints;
				this.TemperatureAtSave = this.ParentObject.Physics.Temperature;
				base.MyActivatedAbility(this.RevertActivatedAbilityID, null).Enabled = true;
				this.TurnsLeft = this.GetDuration(base.Level) + 1;
				if (this.ParentObject.HasRegisteredEvent("InitiatePrecognition"))
				{
					Event @event = Event.New("InitiatePrecognition", "Duration", this.TurnsLeft);
					this.ParentObject.FireEvent(@event, E);
					this.TurnsLeft = @event.GetIntParameter("Duration", 0);
				}
			}
			else if (E.ID == "CommandPrecognitionRevert")
			{
				if (this.TurnsLeft > 0)
				{
					if (this.WasPlayer && this.ParentObject.IsPlayer())
					{
						GameObject parentObject = this.ParentObject;
						if (parentObject != null)
						{
							parentObject.PlayWorldSound("Sounds/Abilities/sfx_ability_mutation_precognition_timeRevert", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
						}
						IComponent<GameObject>.AddPlayerMessage("Your focus returns to the present.", null, true);
						Precognition.Load(this.GlimpseID, this.ParentObject);
						base.MyActivatedAbility(this.RevertActivatedAbilityID, null).Enabled = false;
					}
					else if (this.ParentObject.IsPlayer())
					{
						Popup.Show("You cannot access someone else's precognitive vision.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
					}
				}
			}
			else if (E.ID == "BeforeDie")
			{
				return Precognition.OnBeforeDie(this.ParentObject, this.RevertActivatedAbilityID, this.GlimpseID, ref this.TurnsLeft, ref this.HitpointsAtSave, ref this.TemperatureAtSave, ref this.ActivatedSegment, this.WasPlayer, this.RealityDistortionBased, this);
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600C5F6 RID: 50678 RVA: 0x00480F40 File Offset: 0x0047F140
		public static bool OnBeforeDie(GameObject Object, Guid RevertAAID, Guid GlimpseID, ref int TurnsLeft, ref int HitpointsAtSave, ref int TemperatureAtSave, ref long ActivatedSegment, bool WasPlayer, bool RealityDistortionBased, IPart Mutation)
		{
			if (ActivatedSegment >= The.Game.Segments)
			{
				Object.hitpoints = HitpointsAtSave;
				if (Object.Physics != null)
				{
					Object.Physics.Temperature = TemperatureAtSave;
				}
				return false;
			}
			if (TurnsLeft <= 0)
			{
				return true;
			}
			if (Object.IsPlayer())
			{
				AutoAct.Interrupt(null, null, null, false);
				if (WasPlayer)
				{
					Achievement.FORESEE_DEATH.Unlock();
					if (Popup.ShowYesNo("You sense your imminent demise. Would you like to return to the start of your vision?", "Sounds/UI/ui_notification", true, DialogResult.Yes, null) == DialogResult.Yes)
					{
						if (Object != null)
						{
							Object.PlayWorldSound("Sounds/Abilities/sfx_ability_mutation_precognition_timeRevert", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
						}
						IComponent<GameObject>.AddPlayerMessage("Your focus returns to the present.", null, true);
						Precognition.Load(GlimpseID, Object);
						ActivatedSegment = The.Game.Segments + 100L;
						return false;
					}
				}
			}
			else if (!Object.IsOriginalPlayerBody() && (!RealityDistortionBased || IComponent<GameObject>.CheckRealityDistortionUsability(Object, null, Object, null, Mutation, null, 0)))
			{
				TurnsLeft = 0;
				if (RevertAAID != Guid.Empty)
				{
					Object.DisableActivatedAbility(RevertAAID);
				}
				if (Object.HasStat("Hitpoints"))
				{
					ActivatedSegment = The.Game.Segments + 1L;
					Object.hitpoints = HitpointsAtSave;
					if (Object.Physics != null)
					{
						Object.Physics.Temperature = TemperatureAtSave;
					}
					Object.DilationSplat();
					if (Object != null)
					{
						Object.PlayWorldSound("Sounds/Abilities/sfx_ability_mutation_precognition_timeRevert", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
					}
					Object.Physics.DidX("swim", "before your eyes", "!", null, null, Object, null, false, false, null, null, false, false, false, false, false, null);
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600C5F7 RID: 50679 RVA: 0x004810F1 File Offset: 0x0047F2F1
		public override bool ChangeLevel(int NewLevel)
		{
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600C5F8 RID: 50680 RVA: 0x004810FC File Offset: 0x0047F2FC
		public override bool Mutate(GameObject GO, int Level)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Precognition - Start vision", "CommandPrecognition", "Mental Mutations", null, "!", null, false, false, false, false, this.RealityDistortionBased, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			this.RevertActivatedAbilityID = base.AddMyActivatedAbility("Precognition - End vision", "CommandPrecognitionRevert", "Mental Mutations", null, "?", null, false, false, false, false, this.RealityDistortionBased, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			base.MyActivatedAbility(this.RevertActivatedAbilityID, null).Enabled = false;
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600C5F9 RID: 50681 RVA: 0x00481196 File Offset: 0x0047F396
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			base.RemoveMyActivatedAbility(ref this.RevertActivatedAbilityID, null);
			return base.Unmutate(GO);
		}

		// Token: 0x04004E11 RID: 19985
		public int TurnsLeft;

		// Token: 0x04004E12 RID: 19986
		public bool RealityDistortionBased = true;

		// Token: 0x04004E13 RID: 19987
		public Guid RevertActivatedAbilityID;

		// Token: 0x04004E14 RID: 19988
		public bool WasPlayer;

		// Token: 0x04004E15 RID: 19989
		public int HitpointsAtSave;

		// Token: 0x04004E16 RID: 19990
		public int TemperatureAtSave;

		// Token: 0x04004E17 RID: 19991
		[NonSerialized]
		private Guid GlimpseID;

		// Token: 0x04004E18 RID: 19992
		[NonSerialized]
		private long ActivatedSegment;

		// Token: 0x04004E19 RID: 19993
		[NonSerialized]
		private static Guid CurrentID;

		// Token: 0x04004E1A RID: 19994
		[NonSerialized]
		private static Texture2D Texture;

		// Token: 0x04004E1B RID: 19995
		[NonSerialized]
		private static bool TextureCaptured;

		// Token: 0x04004E1C RID: 19996
		[NonSerialized]
		public static bool Reverting;

		// Token: 0x02001D4E RID: 7502
		public class LoadCommand : IActionCommand, IComposite
		{
			// Token: 0x0600F3D7 RID: 62423 RVA: 0x00550BB0 File Offset: 0x0054EDB0
			public static void Issue()
			{
				XRLGame game = The.Game;
				if (game == null || !game.Running)
				{
					return;
				}
				ActionManager actionManager = game.ActionManager;
				if (!actionManager.HasAction(typeof(Precognition.LoadCommand)))
				{
					if (actionManager.IsPlayerTurn())
					{
						actionManager.SkipPlayerTurn = true;
					}
					actionManager.SkipSegment = true;
					actionManager.DequeueActionsDescendedFrom<ISystemSaveCommand>();
					actionManager.EnqueueAction(Precognition.LoadCommand.Instance, 0);
				}
			}

			// Token: 0x0600F3D8 RID: 62424 RVA: 0x00550C14 File Offset: 0x0054EE14
			public void Execute(XRLGame Game, ActionManager Manager)
			{
				Dictionary<string, object> @for = GetPrecognitionRestoreGameStateEvent.GetFor(Game.Player.Body);
				SingletonWindowBase<LoadingStatusWindow>.instance.StayHidden = true;
				XRLGame.LoadCurrentGame("Precognition", false, @for);
				The.Core.RenderBase(true, false);
				Precognition.Reverting = false;
				SingletonWindowBase<LoadingStatusWindow>.instance.StayHidden = false;
			}

			// Token: 0x0400726F RID: 29295
			private static Precognition.LoadCommand Instance = new Precognition.LoadCommand();
		}
	}
}

using System;
using Genkit;
using UnityEngine;
using XRL.UI;
using XRL.World.Conversations.Parts;

namespace XRL.World.Parts
{
	// Token: 0x020012AE RID: 4782
	[Serializable]
	public class ShevaStarshipControl : IPart
	{
		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x0600BCEA RID: 48362 RVA: 0x004525FC File Offset: 0x004507FC
		public bool IsStarted
		{
			get
			{
				return this.Timer > 0;
			}
		}

		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x0600BCEB RID: 48363 RVA: 0x00452607 File Offset: 0x00450807
		public bool IsFinished
		{
			get
			{
				return this.Timer == 0;
			}
		}

		// Token: 0x0600BCEC RID: 48364 RVA: 0x00452612 File Offset: 0x00450812
		public override bool WantEvent(int ID, int Cascade)
		{
			return ID == GetInventoryActionsEvent.ID || ID == GetZoneSuspendabilityEvent.ID || ID == InventoryActionEvent.ID || ID == CanSmartUseEvent.ID || ID == CommandSmartUseEvent.ID;
		}

		// Token: 0x0600BCED RID: 48365 RVA: 0x00452640 File Offset: 0x00450840
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			E.AddAction("Launch", "launch", "StarshipLaunch", null, 'c', false, 0, 0, false, false, false, false, true, null, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600BCEE RID: 48366 RVA: 0x00452677 File Offset: 0x00450877
		public override bool HandleEvent(GetZoneSuspendabilityEvent E)
		{
			if (this.Timer > 0)
			{
				E.Suspendability = Suspendability.Pinned;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BCEF RID: 48367 RVA: 0x00452692 File Offset: 0x00450892
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "StarshipLaunch")
			{
				this.AttemptLaunch(E.Actor);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BCF0 RID: 48368 RVA: 0x004526BA File Offset: 0x004508BA
		public override bool HandleEvent(CanSmartUseEvent E)
		{
			return !E.Actor.IsPlayer() && base.HandleEvent(E);
		}

		// Token: 0x0600BCF1 RID: 48369 RVA: 0x004526D2 File Offset: 0x004508D2
		public override bool HandleEvent(CommandSmartUseEvent E)
		{
			this.AttemptLaunch(E.Actor);
			return false;
		}

		// Token: 0x0600BCF2 RID: 48370 RVA: 0x004526E4 File Offset: 0x004508E4
		public bool AttemptLaunch(GameObject Actor)
		{
			if (this.IsStarted)
			{
				return Actor.ShowFailure("The spaceship's launch sequence has already begun.");
			}
			if (this.IsFinished)
			{
				return Actor.ShowFailure("The spaceship is already traversing the void.");
			}
			Zone currentZone = this.ParentObject.CurrentZone;
			if (((currentZone != null) ? currentZone.ResolveZoneWorld() : null) != "NorthSheva")
			{
				return Actor.ShowFailure("The spaceship can't launch from here.");
			}
			if (Actor.IsPlayer())
			{
				string text = "";
				string text2 = null;
				if (CapabilityManager.CurrentPlatformClassification() == CapabilityManager.PlatformClassification.PC)
				{
					text = "(type " + this.Prompt + " to confirm)";
					text2 = "";
				}
				if (!Popup.AskString("Launch spaceship and end game?" + text, text2, "Sounds/UI/ui_notification", null, this.Prompt, this.Prompt.Length, 0, false, true, new bool?(false)).EqualsNoCase(this.Prompt))
				{
					return false;
				}
			}
			this.StartLaunch();
			return true;
		}

		// Token: 0x0600BCF3 RID: 48371 RVA: 0x004527C2 File Offset: 0x004509C2
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600BCF4 RID: 48372 RVA: 0x004527C5 File Offset: 0x004509C5
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (this.Timer <= 0)
			{
				return;
			}
			this.Timer = Math.Max(this.Timer - Amount, 0);
			this.CheckTimer();
			Zone currentZone = this.ParentObject.CurrentZone;
			if (currentZone == null)
			{
				return;
			}
			currentZone.MarkActive();
		}

		// Token: 0x0600BCF5 RID: 48373 RVA: 0x00452800 File Offset: 0x00450A00
		public void CheckTimer()
		{
			InteriorZone interiorZone = this.ParentObject.CurrentZone as InteriorZone;
			if (interiorZone == null)
			{
				this.Timer = -1;
				return;
			}
			if (this.Timer > 10)
			{
				interiorZone.SetZoneProperty("ambient_bed_2", "sfx_endgame_spaceship_engine_started_lp");
			}
			else if (this.Timer == 10)
			{
				interiorZone.SetZoneProperty("ambient_bed_2", "sfx_endgame_spaceship_engine_preTakeOff_lp");
			}
			else if (this.Timer == 0)
			{
				interiorZone.RemoveZoneProperty("ambient_bed_2");
			}
			if (interiorZone.IsActive())
			{
				AmbientSoundsSystem.PlayAmbientBeds(interiorZone, null, null, null, null);
				if (this.Timer == 200)
				{
					this.EmitLaunchPopup("Exodus launch sequence initiated!");
					return;
				}
				if (this.Timer == 10)
				{
					this.EmitLaunchPopup("Exodus launch in 10...");
					return;
				}
				if (this.Timer >= 1 && this.Timer <= 9)
				{
					base.EmitMessage(string.Format("Exodus launch in {0}...", this.Timer), ' ', false, false, true, null, null);
					return;
				}
				if (this.Timer == 0)
				{
					this.LaunchActiveEnd();
					return;
				}
				if (this.Timer <= 100 || this.Timer % 50 != 0)
				{
					int timer = this.Timer;
					if (timer <= 10 || timer > 100 || this.Timer % 25 != 0)
					{
						return;
					}
				}
				this.EmitLaunchPopup(string.Format("Launching in {0}...", this.Timer));
				return;
			}
			else if (this.Timer == 0)
			{
				The.Game.SetInt64GameState("StarshipLaunched", The.Game.Turns);
				if (interiorZone.HasObject("Barathrum"))
				{
					The.Game.TryAddDelimitedGameState("Barathrum", ',', "Launched");
					The.Game.TryAddDelimitedGameState("EndExtra", ',', "Starshiib");
				}
				GameObject parentObject = interiorZone.ParentObject;
				int num = parentObject.DistanceTo(The.Player);
				if (num < 200)
				{
					Cell currentCell = The.Player.CurrentCell;
					Point2D point2D = currentCell.Pos2D + parentObject.CurrentCell.PathDifferenceTo(currentCell);
					int num2 = Math.Clamp(10 + num / 3, 15, 25);
					SoundManager.PlayWorldSound("sfx_endgame_spaceship_takeOff", num2, true, 1f, point2D, 0f, 0f, 1f);
					Popup.Show("The moors rattle and a marble brick crumbles as a starship takes off.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				parentObject.AddPart<InteriorBlockEntrance>(new InteriorBlockEntrance
				{
					Message = "There is no starship to enter. The docking bay is empty."
				}, true, false);
			}
		}

		// Token: 0x0600BCF6 RID: 48374 RVA: 0x00452A64 File Offset: 0x00450C64
		public override bool FinalRender(RenderEvent E, bool bAlt)
		{
			if (this.Timer > 0)
			{
				CameraShake.shakeDuration = Math.Max(CameraShake.shakeDuration, 3f);
				float num = ((this.Timer < 10) ? 1.5f : 0.75f);
				float num2 = Mathf.Lerp(0.05f, num, (float)(200 - this.Timer) / 200f);
				CameraShake.currentShakeAmount = Math.Max(CameraShake.currentShakeAmount, num2);
			}
			return base.FinalRender(E, bAlt);
		}

		// Token: 0x0600BCF7 RID: 48375 RVA: 0x00452ADC File Offset: 0x00450CDC
		public void EmitLaunchPopup(string Message)
		{
			base.EmitMessage(Message, 'R', false, true, true, null, null);
		}

		// Token: 0x0600BCF8 RID: 48376 RVA: 0x00452AEC File Offset: 0x00450CEC
		public void StartLaunch()
		{
			this.Timer = 200;
			this.CheckTimer();
		}

		// Token: 0x0600BCF9 RID: 48377 RVA: 0x00452B00 File Offset: 0x00450D00
		public void LaunchActiveEnd()
		{
			SoundManager.PlayUISound("sfx_endgame_spaceship_takeOff", 1f, false, false, SoundRequest.SoundEffectType.None);
			The.Game.SetStringGameState("EndType", "Launch");
			if (NephalProperties.IsFoiled("Ehalcodon"))
			{
				The.Game.SetStringGameState("EndGrade", "Super");
			}
			if (this.ParentObject.CurrentZone.HasObject("Barathrum"))
			{
				The.Game.TryAddDelimitedGameState("EndExtra", ',', "Starshiib");
				The.Game.TryAddDelimitedGameState("EndExtra", ',', "WithBarathrum");
			}
			EndGame.Start();
		}

		// Token: 0x04004BB5 RID: 19381
		public const int LAUNCH_DURATION = 200;

		// Token: 0x04004BB6 RID: 19382
		public string Prompt = "LAUNCH";

		// Token: 0x04004BB7 RID: 19383
		public int Timer = -1;
	}
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using ConsoleLib.Console;
using Genkit;
using Qud.API;
using Qud.UI;
using UnityEngine;
using XRL.Core;
using XRL.Messages;
using XRL.Rules;
using XRL.UI;
using XRL.Wish;
using XRL.World.Anatomy;
using XRL.World.Conversations.Parts;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x020012A7 RID: 4775
	[HasWishCommand]
	[Serializable]
	public class ThinWorld : IPart
	{
		// Token: 0x0600BC9A RID: 48282 RVA: 0x0044F344 File Offset: 0x0044D544
		public override bool SameAs(IPart p)
		{
			return true;
		}

		// Token: 0x0600BC9B RID: 48283 RVA: 0x0044F347 File Offset: 0x0044D547
		public static void SetStateKnowAboutSalum()
		{
			The.Game.SetIntGameState("KnowAboutSalum", 1);
		}

		// Token: 0x0600BC9C RID: 48284 RVA: 0x0044F35C File Offset: 0x0044D55C
		[WishCommand("crossintobrightsheol", null)]
		public static bool CrossIntoBrightsheol()
		{
			if (!string.Equals(WorldFactory.Factory.getWorld(The.Player.GetCurrentCell().ParentZone.ZoneWorld).Protocol, "THIN"))
			{
				Popup.Show("You cannot cross into Brightsheol from this place.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			}
			if (Popup.AskString("Crossing into Brightsheol will retire your character. Are you sure you want to do it? Type 'CROSS' to confirm.", "", "Sounds/UI/ui_notification", null, null, 5, 0, false, true, new bool?(false)).ToUpper() == "CROSS")
			{
				GameManager.Instance.PushGameView("GameSummary", true);
				ScreenBuffer scrapBuffer = ScreenBuffer.GetScrapBuffer1(false);
				scrapBuffer.RenderBase();
				scrapBuffer.Draw();
				scrapBuffer.Draw();
				FadeToBlack.FadeOut(8f, new Color(1f, 1f, 1f));
				Thread.Sleep(13000);
				The.Game.SetStringGameState("EndType", "Brightsheol");
				JournalAPI.AddAccomplishment("You crossed into Brightsheol.", null, null, null, "general", MuralCategory.Generic, MuralWeight.Medium, null, -1L, true);
				float num = 3f;
				float? num2 = new float?((float)1);
				float? num3 = new float?((float)1);
				Color? color = new Color?(new Color(0f, 0f, 0f, 1f));
				FadeToBlack.Fade(num, num2, num3, null, color);
				Thread.Sleep(3000);
				EndGame.UnlockAchievement();
				EndGame.RunCredits();
				FadeToBlack.FadeIn(3f);
				The.Game.DeathReason = "You crossed into Brightsheol.";
				The.Game.Running = false;
				return true;
			}
			return false;
		}

		// Token: 0x0600BC9D RID: 48285 RVA: 0x0044F4E0 File Offset: 0x0044D6E0
		public static void RunThinWorldIntroSequence(bool express = false)
		{
			Stopwatch stopwatch = new Stopwatch();
			ScreenBuffer scrapBuffer = ScreenBuffer.GetScrapBuffer1(false);
			List<string> list = new List<string>
			{
				"..................", "", "", "         .   .          .                                    ", "   .     |\\  |  o       |                                .   ", " __|__   | \\ |  .  .--. |--. .  . .--..--. .-.  .-..   __|__ ", "   |     |  \\|  |  |  | |  | |  | |   `--.(   )(   |     |   ", "   '     '   '-' `-'  `-'  `-`--`-'   `--' `-'`-`-`|     '   ", "                                                ._.'         ", "",
				"", " ê 00000000 CRK SHELL", " ê 00000010 GET SEED", " ê 00000020 MOV BSHEOL", " ê 00000030 PUT SEED", " ê 00000040 GRW"
			};
			stopwatch.Start();
			for (int i = 0; i < list.Count; i++)
			{
				scrapBuffer.ScrollUp();
				for (int j = 0; j < list[i].Length; j++)
				{
					scrapBuffer.Goto(j, 24);
					scrapBuffer.Write("&c" + list[i][j].ToString(), true, false, false, null, -1);
					if (stopwatch.ElapsedMilliseconds % 500L < 250L && j != list[i].Length - 1)
					{
						scrapBuffer.Write("&C_", true, false, false, null, -1);
					}
					GameManager.Instance.uiQueue.queueTask(delegate
					{
						SoundManager.PlaySound("sub_bass_mouseover1", 0f, 1f, 1f, SoundRequest.SoundEffectType.None, 0f);
					}, 0);
					Thread.Sleep(Math.Min(70, 1400 / list[i].Length));
					scrapBuffer.Draw();
				}
				Thread.Sleep(50);
			}
			GameManager.Instance.uiQueue.queueTask(delegate
			{
				SoundManager.PlaySound("sfx_sultanSarcophagus_consciousness_uploaded", 0f, 1f, 1f, SoundRequest.SoundEffectType.None, 0f);
			}, 0);
			stopwatch.Stop();
			ScreenBuffer scrapBuffer2 = ScreenBuffer.GetScrapBuffer2(false);
			The.Core.RenderBaseToBuffer(scrapBuffer2);
			Stopwatch stopwatch2 = new Stopwatch();
			stopwatch2.Start();
			int num = 0;
			global::UnityEngine.Debug.Log("starting sequence");
			int num2 = 17000;
			if (express)
			{
				num2 = 0;
			}
			StringBuilder stringBuilder = new StringBuilder();
			while (stopwatch2.ElapsedMilliseconds < (long)num2)
			{
				if (FadeToBlack.stage == FadeToBlack.FadeToBlackStage.FadedIn && stopwatch2.ElapsedMilliseconds > (long)(num2 - 2500))
				{
					global::UnityEngine.Debug.Log("starting fade");
					FadeToBlack.FadeOut(2.4f, false);
				}
				num++;
				scrapBuffer.ScrollUp();
				int num3 = Stat.Random(0, 79);
				scrapBuffer.Goto(num3, scrapBuffer.Height - 1);
				for (int k = num3; k < 80; k++)
				{
					scrapBuffer.Write("&c" + ((char)Stat.RandomCosmetic(1, 254)).ToString(), true, false, false, null, -1);
				}
				for (int l = Math.Max(0, num); l < scrapBuffer.Height - 1; l++)
				{
					stringBuilder.Length = 0;
					stringBuilder.Append("&c");
					stringBuilder.Append((char)Stat.RandomCosmetic(1, 254));
					scrapBuffer.Write(stringBuilder, -1);
				}
				if (num > 80)
				{
					for (int m = 0; m < scrapBuffer.Width; m++)
					{
						for (int n = 0; n < scrapBuffer.Height; n++)
						{
							if ((long)Stat.RandomCosmetic(0, 10000) <= (stopwatch2.ElapsedMilliseconds - 7000L) / 400L)
							{
								scrapBuffer[m, n].Copy(scrapBuffer2[m, n]);
								scrapBuffer[m, n].SetBackground('k');
								if (Stat.RandomCosmetic(1, 100) <= 80)
								{
									scrapBuffer[m, n].SetForeground('b');
								}
								else
								{
									scrapBuffer[m, n].SetForeground('B');
								}
							}
						}
					}
				}
				if (num < 100)
				{
					Thread.Sleep(Math.Max(0, 100 - num));
				}
				else
				{
					Thread.Sleep(10);
				}
				scrapBuffer.Draw();
			}
			while (FadeToBlack.stage == FadeToBlack.FadeToBlackStage.FadingOut)
			{
			}
			scrapBuffer.Clear();
			scrapBuffer.Draw();
			Thread.Sleep(5000);
			FadeToBlack.FadeIn(0f);
			Popup.Show("The darkness recedes, and a new light breaks on the shores of your mind.", null, "Sounds/UI/ui_notification", true, true, false, true, null);
			JournalAPI.AddAccomplishment("You crossed into the Thin World, rarefied, and stood before the gate to Brightsheol.", null, null, null, "general", MuralCategory.Generic, MuralWeight.Medium, null, -1L, true);
			SoundManager.PlayMusic("Attenuated Signals", "music", true, 12f, 1f, null, true);
			scrapBuffer2.Draw();
			FadeToBlack.FadeOut(0f, false);
			FadeToBlack.FadeIn(6f);
			scrapBuffer2.Draw();
			GameManager.Instance.PopGameView(false);
		}

		// Token: 0x0600BC9E RID: 48286 RVA: 0x0044F9BC File Offset: 0x0044DBBC
		public static void TransitToThinWorld(GameObject sender, bool express = false)
		{
			Thread.Sleep(10);
			XRLGame game = The.Game;
			GameObject oldPlayer = The.Player;
			List<GameObject> objects = oldPlayer.GetCurrentCell().ParentZone.GetObjects((GameObject o) => o.PartyLeader == oldPlayer);
			oldPlayer.RemoveEffect<AmbientRealityStabilized>();
			game.SetInt64GameState("thinWorldTime", Calendar.TotalTimeTicks);
			game.SetStringGameState("thinWorldPlayerBody", game.ZoneManager.CacheObject(oldPlayer, false, true));
			game.SetIntGameState("thinWorldFollowerCount", objects.Count);
			for (int i = 0; i < objects.Count; i++)
			{
				game.SetStringGameState("thinWorldFollower" + i.ToString(), game.ZoneManager.CacheObject(objects[i], false, true));
				objects[i].CurrentCell = null;
				objects[i].MakeInactive();
			}
			GameObject gameObject = oldPlayer.DeepCopy(false, true, null);
			sender.GetPart<Enclosing>().EnterEnclosure(gameObject, null);
			GameManager.Instance.PushGameView(Options.StageViewID, true);
			Cell currentCell = oldPlayer.CurrentCell;
			oldPlayer.CurrentCell.RemoveObject(oldPlayer, false, false, false, false, false, true, true, null, null, null, null, null, null, null);
			if (!express)
			{
				ScreenBuffer scrapBuffer = ScreenBuffer.GetScrapBuffer1(false);
				currentCell.ParentZone.Render(scrapBuffer);
				CombatJuice.cameraShake(0.25f, false);
				scrapBuffer.Draw();
				Thread.Sleep(10);
				GameManager.Instance.uiQueue.queueTask(delegate
				{
					SoundManager.PlaySound("Sounds/Misc/sfx_ancientDoor_close", 0f, 1f, 1f, SoundRequest.SoundEffectType.None, 0f);
				}, 0);
			}
			oldPlayer.MakeInactive();
			if (!express)
			{
				Popup.Show("The colossal lid slams shut. Darkness engulfs you.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				Thread.Sleep(3000);
				SoundManager.PlaySound("sfx_sultanSarcophagus_crush", 0f, 1f, 1f, SoundRequest.SoundEffectType.None, 0f);
				SoundManager.StopMusic("music", false, 12f);
				CombatJuice._cameraShake(0.5f);
				Thread.Sleep(500);
				Popup.ShowSpace("You died.\n\nEntombed in the burial chamber of Resheph, the Last Sultan.", null, "Sounds/UI/ui_notification_death", null, true, true, null);
				JournalAPI.AddAccomplishment("You died and were entombed in the burial chamber of Resheph, the Last Sultan.", null, null, null, "general", MuralCategory.Generic, MuralWeight.Medium, null, -1L, true);
			}
			else
			{
				SoundManager.StopMusic("music", false, 12f);
			}
			The.Core.BuildScore(false, "You were entombed in the burial chamber of Resheph, the Last Sultan.", true);
			GameManager.Instance.uiQueue.queueTask(delegate
			{
				SoundManager.PlaySound("Sounds/Damage/sfx_damage_stone", 0f, 1f, 1f, SoundRequest.SoundEffectType.None, 0f);
			}, 0);
			GameManager.Instance.PushGameView("GameSummary", true);
			new Stopwatch();
			ScreenBuffer scrapBuffer2 = ScreenBuffer.GetScrapBuffer1(false);
			for (int j = 0; j < scrapBuffer2.Height; j++)
			{
				for (int k = 0; k < scrapBuffer2.Width; k++)
				{
					if (Stat.RandomCosmetic(1, 100) <= 10)
					{
						scrapBuffer2[k, j].Char = (char)Stat.RandomCosmetic(0, 254);
					}
					Stat.RandomCosmetic(1, 100);
				}
			}
			for (int l = 0; l < 8; l++)
			{
				scrapBuffer2.Draw();
				Keyboard.getch();
				for (int m = 0; m < scrapBuffer2.Height; m++)
				{
					for (int n = 0; n < scrapBuffer2.Width; n++)
					{
						if (Stat.RandomCosmetic(1, 100) <= 10)
						{
							scrapBuffer2[n, m].Char = (char)Stat.RandomCosmetic(0, 254);
						}
						Stat.RandomCosmetic(1, 100);
					}
				}
				CombatJuice.cameraShake(0.25f, false);
				scrapBuffer2.Draw();
				GameManager.Instance.uiQueue.queueTask(delegate
				{
					SoundManager.PlaySound("Sounds/Damage/sfx_damage_stone", 0f, 1f, 1f, SoundRequest.SoundEffectType.None, 0f);
				}, 0);
			}
			The.ZoneManager.SetActiveZone("ThinWorld.6.6.10.6.6");
			GameObject gameObject2 = The.ZoneManager.ActiveZone.GetCell(40, 22).AddObject(oldPlayer.DeepCopy(true, true, null), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			gameObject2.RequirePart<HologramMaterial>(false);
			gameObject2.RequirePart<RebornOnDeathInThinWorld>(false);
			gameObject2.SetIntProperty("NoStatusColor", 1, false);
			game.Player.Body = gameObject2;
			gameObject2.MakeActive(false);
			gameObject2.Energy.BaseValue = 1000;
			ThinWorld.RunThinWorldIntroSequence(express);
		}

		// Token: 0x0600BC9F RID: 48287 RVA: 0x0044FE40 File Offset: 0x0044E040
		public static void SomethingGoesWrong(GameObject go)
		{
			Anatomy randomAnatomy = Anatomies.GetRandomAnatomy();
			int i = 0;
			int num = Stat.Random(1, 3);
			while (i < num)
			{
				int j = 0;
				while (j < 10)
				{
					BodyPart randomElement = (from p in go.Body.GetParts()
						where p.Type != "Body" && (!(p.Type == "Head") || !p.Primary) && !p.Abstract
						select p).GetRandomElement<BodyPart>(null);
					AnatomyPart randomElement2 = randomAnatomy.Parts.Where<AnatomyPart>(delegate(AnatomyPart p)
					{
						if (p.Type.Type != "Body")
						{
							bool? @abstract = p.Type.Abstract;
							bool flag = false;
							return (@abstract.GetValueOrDefault() == flag) & (@abstract != null);
						}
						return false;
					}).GetRandomElement<AnatomyPart>(null);
					if (randomElement != null && randomElement2 != null)
					{
						BodyPart parentPart = randomElement.GetParentPart();
						if (randomElement.Equipped != null)
						{
							EquipmentAPI.UnequipObject(randomElement.Equipped);
							if (randomElement.Equipped == null)
							{
								randomElement.Dismember(true);
							}
						}
						string text = new BallBag<string>(0)
						{
							{ "parentPart", 70 },
							{ "body", 15 },
							{ "random", 15 }
						}.PluckOne();
						if (text == "parentPart")
						{
							randomElement2.ApplyTo(parentPart);
							break;
						}
						if (text == "body")
						{
							randomElement2.ApplyTo(go.Body._Body);
							break;
						}
						if (text == "random")
						{
							randomElement2.ApplyTo((from p in go.Body.GetParts()
								where !p.Abstract
								select p).GetRandomElement<BodyPart>(null));
							break;
						}
						break;
					}
					else
					{
						j++;
					}
				}
				i++;
			}
		}

		// Token: 0x0600BCA0 RID: 48288 RVA: 0x0044FFE4 File Offset: 0x0044E1E4
		public static GameObject ReturnBody(GameObject Object)
		{
			Popup.Suppress = true;
			The.ZoneManager.SetActiveZone(The.Game.GetStringGameState("Recorporealization_ZoneID", ""));
			Popup.Suppress = false;
			GameObject gameObject = The.ZoneManager.ActiveZone.FindObject("Full-Scale Recompositer");
			Cell cell = null;
			if (gameObject != null)
			{
				cell = gameObject.CurrentCell;
			}
			if (cell == null)
			{
				cell = The.ZoneManager.ActiveZone.GetEmptyCells(null, null).GetRandomElement<Cell>(null);
			}
			if (cell == null)
			{
				cell = (from c in The.ZoneManager.ActiveZone.GetCells()
					where c.IsPassable(null, true)
					select c).GetRandomElement<Cell>(null);
			}
			if (cell == null)
			{
				cell = The.ZoneManager.ActiveZone.GetCells().GetRandomElement<Cell>(null);
			}
			GameObject body = The.Game.Player.Body;
			cell.AddObject(Object, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
			The.Game.Player.Body = Object;
			Object.SetActive();
			Object.Energy.BaseValue = 1000;
			Object.ApplyEffect(new Dazed(Stat.Random(180, 220)), null);
			The.Game.TimeTicks = The.Game.GetInt64GameState("thinWorldTime", The.Game.TimeTicks) + 3600L;
			if (body != Object)
			{
				body.MakeInactive();
				body.Obliterate(null, false, null);
			}
			MessageQueue.Suppress = true;
			Popup.Suppress = true;
			try
			{
				BodyPart body2 = The.Player.Body.GetBody();
				string text = "Floating Nearby";
				int num = 0;
				string text2 = null;
				string text3 = null;
				string text4 = null;
				string text5 = null;
				string text6 = null;
				bool? flag = new bool?(true);
				body2.AddPart(text, num, text2, text3, text4, text5, text6, null, null, null, null, null, null, null, null, flag, null, null, null, null, true);
				Stomach part = The.Player.GetPart<Stomach>();
				if (part != null)
				{
					part.Water = RuleSettings.WATER_PARCHED - 1;
				}
				The.Player.RemovePart<Tattoos>();
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("exception cleaning the player body on recoming", ex, "game_exception");
			}
			return gameObject;
		}

		// Token: 0x0600BCA1 RID: 48289 RVA: 0x00450254 File Offset: 0x0044E454
		public static void ReturnToQud()
		{
			XRLGame game = The.Game;
			GameObject gameObject = The.Game.Player.Body;
			gameObject = game.ZoneManager.peekCachedObject(game.GetStringGameState("thinWorldPlayerBody", ""));
			if (gameObject == null)
			{
				gameObject = The.Game.Player.Body;
			}
			if (game.ZoneManager.CachedObjects.ContainsKey(game.GetStringGameState("thinWorldPlayerBody", "")))
			{
				game.ZoneManager.CachedObjects[game.GetStringGameState("thinWorldPlayerBody", "")] = null;
				game.ZoneManager.CachedObjects.Remove(game.GetStringGameState("thinWorldPlayerBody", ""));
			}
			SoundManager.StopMusic("music", true, 5f);
			FadeToBlack.FadeOut(5f, false);
			Thread.Sleep(8000);
			GameObject gameObject2 = ThinWorld.ReturnBody(gameObject);
			The.Game.FinishQuestStep("Tomb of the Eaters", "Disable the Spindle's Magnetic Field", -1, true, null);
			try
			{
				int intGameState = game.GetIntGameState("thinWorldFollowerCount", 0);
				for (int i = 0; i < intGameState; i++)
				{
					string text = "thinWorldFollower" + i.ToString();
					GameObject gameObject3 = game.ZoneManager.peekCachedObject(game.GetStringGameState(text, ""));
					if (gameObject3 != null)
					{
						Cell connectedSpawnLocation = The.Player.GetCurrentCell().GetConnectedSpawnLocation(true);
						if (connectedSpawnLocation != null)
						{
							game.ZoneManager.CachedObjects.Remove(game.GetStringGameState("thinWorldFollower" + i.ToString(), ""));
							connectedSpawnLocation.AddObject(gameObject3, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
							if (gameObject3.PartyLeader != The.Player)
							{
								gameObject3.PartyLeader = The.Player;
							}
							gameObject3.MakeActive(false);
							gameObject3.ApplyEffect(new Dazed(Stat.Random(180, 220)), null);
						}
					}
					game.ZoneManager.UncacheObject(text);
				}
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("exception returning followers from brightsheol", ex, "game_exception");
			}
			try
			{
				GameObject gameObject4 = The.ZoneManager.ActiveZone.FindObject("Recoming Reliquary");
				if (gameObject4 != null)
				{
					foreach (GameObject gameObject5 in The.Player.GetInventoryAndEquipment())
					{
						if (gameObject5.Equipped == The.Player)
						{
							EquipmentAPI.UnequipObject(gameObject5);
						}
					}
					Inventory inventory = gameObject4.Inventory;
					foreach (GameObject gameObject6 in The.Player.Inventory.Objects.ToArray())
					{
						if (!inventory.Objects.Contains(gameObject6))
						{
							inventory.AddObject(gameObject6, null, false, false, true, null);
						}
					}
					The.Player.Inventory.Objects.Clear();
				}
				else
				{
					List<Cell> connectedSpawnLocations = The.Player.CurrentCell.GetConnectedSpawnLocations(6);
					if (connectedSpawnLocations != null && connectedSpawnLocations.Count > 0)
					{
						foreach (GameObject gameObject7 in The.Player.GetInventoryAndEquipment())
						{
							if (gameObject7.Equipped == The.Player)
							{
								EquipmentAPI.UnequipObject(gameObject7);
							}
						}
						foreach (GameObject gameObject8 in The.Player.Inventory.Objects.ToArray())
						{
							connectedSpawnLocations.GetRandomElement<Cell>(null).AddObject(gameObject8, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						}
						The.Player.Inventory.Objects.Clear();
					}
				}
			}
			catch (Exception ex2)
			{
				MetricsManager.LogException("ThinWorld return", ex2, "game_exception");
			}
			finally
			{
				MessageQueue.Suppress = false;
				Popup.Suppress = false;
				ScreenBuffer scrapBuffer = ScreenBuffer.GetScrapBuffer1(false);
				scrapBuffer.Clear();
				scrapBuffer.Draw();
				FadeToBlack.FadeIn(0f);
				Popup.Show("The matter of your new body starts to thicken and clot. You're inlaid with ribbons of bone, tissue, nerve, and flesh.", null, "Sounds/UI/ui_notification", true, true, false, true, null);
				if (gameObject2 == null)
				{
					Popup.Show("Something went wrong.", null, "Sounds/UI/ui_notification", true, true, false, true, null);
					try
					{
						ThinWorld.SomethingGoesWrong(The.Player);
					}
					catch
					{
					}
				}
				scrapBuffer.Draw();
				FadeToBlack.FadeOut(0f, false);
				scrapBuffer.RenderBase();
				scrapBuffer.Draw();
				FadeToBlack.FadeIn(8f);
				Thread.Sleep(8000);
				Popup.Show("The neoteric body is differently charged. Tying inside you is another of the secret knots that bind the world.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				Popup.Show("You gain an additional {{rules|Floating Nearby}} slot!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
				JournalAPI.AddAccomplishment("You were reconstituted in the material world with a stronger magnetic charge.", null, null, null, "general", MuralCategory.Generic, MuralWeight.Medium, null, -1L, true);
				IComponent<GameObject>.TheGame.SetBooleanGameState("Recame", true);
				Achievement.RECAME.Unlock();
				CheckpointingSystem.ManualCheckpoint(The.ActiveZone, "Gyl_Recame");
			}
		}

		// Token: 0x0600BCA2 RID: 48290 RVA: 0x004507D4 File Offset: 0x0044E9D4
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeRenderEvent.ID;
		}

		// Token: 0x0600BCA3 RID: 48291 RVA: 0x004507EC File Offset: 0x0044E9EC
		public override bool HandleEvent(BeforeRenderEvent E)
		{
			int num = 80;
			if (The.Player != null)
			{
				if (num > 0)
				{
					Cell currentCell = The.Player.CurrentCell;
					if (currentCell != null)
					{
						Zone parentZone = currentCell.ParentZone;
						if (parentZone != null)
						{
							parentZone.AddLight(currentCell.X, currentCell.Y, num, LightLevel.Light, false);
						}
					}
				}
				if (num < 3)
				{
					Cell currentCell2 = The.Player.CurrentCell;
					if (currentCell2 != null)
					{
						Zone parentZone2 = currentCell2.ParentZone;
						if (parentZone2 != null)
						{
							parentZone2.AddExplored(currentCell2.X, currentCell2.Y, 3);
						}
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600BCA4 RID: 48292 RVA: 0x00450870 File Offset: 0x0044EA70
		public double sampleSimplexNoise(string type, int x, int y, int z, int amplitude, float frequencyMultiplier = 1f)
		{
			if (this.seed == 0)
			{
				this.seed = Stat.Random(0, 2147483646);
			}
			this.fastNoise.SetSeed(this.seed);
			this.fastNoise.SetNoiseType(FastNoise.NoiseType.SimplexFractal);
			this.fastNoise.SetFrequency(0.25f * frequencyMultiplier);
			this.fastNoise.SetFractalType(FastNoise.FractalType.FBM);
			this.fastNoise.SetFractalOctaves(3);
			this.fastNoise.SetFractalLacunarity(0.7f);
			this.fastNoise.SetFractalGain(1f);
			return Math.Ceiling((double)this.fastNoise.GetNoise((float)x, (float)y, (float)z) * (double)amplitude);
		}

		// Token: 0x0600BCA5 RID: 48293 RVA: 0x0045091C File Offset: 0x0044EB1C
		public double sampleSimplexNoiseRange(string type, int x, int y, int z, float low, float high, float frequencyMultiplier = 1f)
		{
			if (this.seed == 0)
			{
				this.seed = Stat.Random(0, 2147483646);
			}
			this.fastNoise.SetSeed(this.seed);
			this.fastNoise.SetNoiseType(FastNoise.NoiseType.SimplexFractal);
			this.fastNoise.SetFrequency(0.25f * frequencyMultiplier);
			this.fastNoise.SetFractalType(FastNoise.FractalType.FBM);
			this.fastNoise.SetFractalOctaves(3);
			this.fastNoise.SetFractalLacunarity(0.7f);
			this.fastNoise.SetFractalGain(1f);
			return (double)((this.fastNoise.GetNoise((float)x, (float)y, (float)z) + 1f) / 2f * (high - low) + low);
		}

		// Token: 0x0600BCA6 RID: 48294 RVA: 0x004509D4 File Offset: 0x0044EBD4
		public override bool FinalRender(RenderEvent E)
		{
			if (!this.triggered && (float)XRLCore.FrameTimer.ElapsedMilliseconds - this.timer >= 200f)
			{
				if (Stat.RandomCosmetic(1, 12) <= 1)
				{
					this.triggered = true;
					this.triggertime = XRLCore.FrameTimer.ElapsedMilliseconds;
					this.yhitch.Clear();
					int num = Stat.RandomCosmetic(-2, 2);
					for (int i = 0; i < 25; i++)
					{
						this.yhitch.Add(num);
						num += Stat.RandomCosmetic(-2, 2);
					}
				}
				this.timer = (float)XRLCore.FrameTimer.ElapsedMilliseconds;
				this.xoffset = Stat.RandomCosmetic(-100000, 100000);
				this.yoffset = Stat.RandomCosmetic(-100000, 100000);
			}
			E.WantsToPaint = true;
			return true;
		}

		// Token: 0x0600BCA7 RID: 48295 RVA: 0x00450AA8 File Offset: 0x0044ECA8
		public override void OnPaint(ScreenBuffer buffer)
		{
			for (int i = 0; i < buffer.Height; i++)
			{
				for (int j = 0; j < buffer.Width; j++)
				{
					try
					{
						char foregroundCode = buffer.Buffer[j, i].ForegroundCode;
						char backgroundCode = buffer.Buffer[j, i].BackgroundCode;
						char detailCode = buffer.Buffer[j, i].DetailCode;
						if (foregroundCode != 'b' && foregroundCode != 'B' && foregroundCode != 'k')
						{
							if (foregroundCode >= 'a' && foregroundCode <= 'z')
							{
								buffer.Buffer[j, i].SetForeground('b');
							}
							if (foregroundCode >= 'A' && foregroundCode <= 'Z')
							{
								buffer.Buffer[j, i].SetForeground('B');
							}
						}
						if (backgroundCode != 'b' && backgroundCode != 'B' && backgroundCode != 'k')
						{
							if (backgroundCode >= 'a' && backgroundCode <= 'z')
							{
								buffer.Buffer[j, i].SetBackground('b');
							}
							if (backgroundCode >= 'A' && backgroundCode <= 'Z')
							{
								buffer.Buffer[j, i].SetBackground('B');
							}
						}
						if (detailCode != 'b' && detailCode != 'B' && detailCode != 'k')
						{
							if (detailCode >= 'a' && detailCode <= 'z')
							{
								buffer.Buffer[j, i].SetDetail('b');
							}
							if (detailCode >= 'A' && detailCode <= 'Z')
							{
								buffer.Buffer[j, i].SetDetail('B');
							}
						}
					}
					catch (Exception ex)
					{
						MetricsManager.LogException("Thin world::OnPaint 1", ex, "game_exception");
					}
				}
			}
			if (this.triggered)
			{
				long num = (The.Game.WallTime.ElapsedMilliseconds - this.triggertime) / 8L;
				for (int k = 0; k < buffer.Height; k++)
				{
					for (int l = 0; l < buffer.Width; l++)
					{
						try
						{
							double num2 = (double)num - this.sampleSimplexNoiseRange("render", l + this.xoffset, k + this.yoffset + this.yhitch[k], 0, 0f, 160f, 0.25f) + (double)l + (double)(k / 10);
							if (num2 > 0.0)
							{
								if (num2 <= 3.0)
								{
									buffer.Buffer[l, k].Tile = null;
									buffer.Buffer[l, k].Char = buffer.Buffer[l, k].BackupChar;
									if (Stat.RandomCosmetic(1, 100) <= 50)
									{
										buffer.Buffer[l, k].SetForeground('B');
									}
									if (Stat.RandomCosmetic(1, 100) <= 50)
									{
										buffer.Buffer[l, k].SetForeground('b');
									}
									if (Stat.Random(1, 20) <= 1)
									{
										this.leftovers.Add(Location2D.Get(l, k));
									}
								}
								if (num2 <= 6.0)
								{
									buffer.Buffer[l, k].SetForeground('B');
								}
								if (num2 <= 9.0 && Stat.RandomCosmetic(1, 100) <= 50)
								{
									buffer.Buffer[l, k].SetForeground('b');
								}
							}
						}
						catch (Exception ex2)
						{
							MetricsManager.LogException("Thin world::OnPaint 2", ex2, "game_exception");
						}
					}
				}
				if (num > 160L)
				{
					this.triggered = false;
				}
			}
			if (this.leftovers.Count > 0)
			{
				try
				{
					foreach (Location2D location2D in this.leftovers)
					{
						if (Stat.Random(1, 100) <= 40)
						{
							buffer[location2D.X, location2D.Y].Tile = null;
							buffer[location2D.X, location2D.Y].Char = buffer.Buffer[location2D.X, location2D.Y].BackupChar;
						}
						if (Stat.Random(1, 100) <= 40)
						{
							buffer[location2D.X, location2D.Y].SetForeground('B');
							if (Stat.RandomCosmetic(1, 100) <= 10)
							{
								buffer[location2D.X, location2D.Y].SetForeground('b');
							}
						}
					}
					this.leftovers.RemoveAt(0);
				}
				catch (Exception ex3)
				{
					MetricsManager.LogException("Thin world::OnPaint 3", ex3, "game_exception");
				}
			}
			base.OnPaint(buffer);
		}

		// Token: 0x0600BCA8 RID: 48296 RVA: 0x00450F84 File Offset: 0x0044F184
		public override bool FireEvent(Event E)
		{
			return base.FireEvent(E);
		}

		// Token: 0x04004B98 RID: 19352
		private FastNoise fastNoise = new FastNoise(1337);

		// Token: 0x04004B99 RID: 19353
		public int seed;

		// Token: 0x04004B9A RID: 19354
		private List<int> yhitch = new List<int>();

		// Token: 0x04004B9B RID: 19355
		private List<Location2D> leftovers = new List<Location2D>();

		// Token: 0x04004B9C RID: 19356
		private bool triggered;

		// Token: 0x04004B9D RID: 19357
		private long triggertime;

		// Token: 0x04004B9E RID: 19358
		private float timer;

		// Token: 0x04004B9F RID: 19359
		private int xoffset;

		// Token: 0x04004BA0 RID: 19360
		private int yoffset;
	}
}

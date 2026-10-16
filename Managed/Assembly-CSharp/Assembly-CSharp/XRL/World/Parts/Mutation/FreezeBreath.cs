using System;
using System.Collections.Generic;
using System.Threading;
using ConsoleLib.Console;
using XRL.Core;
using XRL.Rules;
using XRL.UI;
using XRL.World.Anatomy;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x02001436 RID: 5174
	[Serializable]
	public class FreezeBreath : BaseMutation
	{
		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x0600CA06 RID: 51718 RVA: 0x00498898 File Offset: 0x00496A98
		private static GameObject Projectile
		{
			get
			{
				if (!GameObject.Validate(ref FreezeBreath._Projectile))
				{
					FreezeBreath._Projectile = GameObject.CreateUnmodified("ProjectileFreezingRay");
				}
				return FreezeBreath._Projectile;
			}
		}

		// Token: 0x0600CA07 RID: 51719 RVA: 0x004988BA File Offset: 0x00496ABA
		public override bool GeneratesEquipment()
		{
			return true;
		}

		// Token: 0x0600CA09 RID: 51721 RVA: 0x00498911 File Offset: 0x00496B11
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == SingletonEvent<BeforeAbilityManagerOpenEvent>.ID;
		}

		// Token: 0x0600CA0A RID: 51722 RVA: 0x00498937 File Offset: 0x00496B37
		public override bool HandleEvent(BeforeAbilityManagerOpenEvent E)
		{
			base.DescribeMyActivatedAbility(this.FreezeBreathActivatedAbilityID, new Action<Templates.StatCollector>(this.CollectStats), null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600CA0B RID: 51723 RVA: 0x0049895C File Offset: 0x00496B5C
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (E.Distance <= this.Range && base.IsMyActivatedAbilityAIUsable(this.FreezeBreathActivatedAbilityID, null) && GameObject.Validate(E.Target) && E.Actor.HasLOSTo(E.Target, true, false, true, true, null))
			{
				E.Add("CommandFreezeBreath", 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CA0C RID: 51724 RVA: 0x004989C3 File Offset: 0x00496BC3
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				E.Add("ice", 3);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CA0D RID: 51725 RVA: 0x004989E6 File Offset: 0x00496BE6
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CommandFreezeBreath");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600CA0E RID: 51726 RVA: 0x004989FB File Offset: 0x00496BFB
		public override string GetDescription()
		{
			return "You emit jets of frost from your mouth.";
		}

		// Token: 0x0600CA0F RID: 51727 RVA: 0x00498A04 File Offset: 0x00496C04
		public override string GetLevelText(int Level)
		{
			return "Emits a " + this.Range.ToString() + "-square ray of frost in the direction of your choice\n" + "Cooldown: 30 rounds\n" + "Damage: " + this.ComputeDamage(Level) + "\n" + "Cannot wear face accessories";
		}

		// Token: 0x0600CA10 RID: 51728 RVA: 0x00498A58 File Offset: 0x00496C58
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			stats.Set("EmitText", this.GetDescription(), false, 0);
			stats.Set("Range", this.Range, false, 0);
			stats.Set("Damage", this.ComputeDamage(), !stats.mode.Contains("ability"), 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), 30, 0);
		}

		// Token: 0x0600CA11 RID: 51729 RVA: 0x00498AC8 File Offset: 0x00496CC8
		public string ComputeDamage(int UseLevel)
		{
			string text = UseLevel.ToString() + "d4";
			if (this.ParentObject != null)
			{
				int partCount = this.ParentObject.Body.GetPartCount(this.BodyPartType);
				if (partCount > 0)
				{
					text += partCount.Signed(false);
				}
			}
			else
			{
				text += "+1";
			}
			return text;
		}

		// Token: 0x0600CA12 RID: 51730 RVA: 0x00498B27 File Offset: 0x00496D27
		public string ComputeDamage()
		{
			return this.ComputeDamage(base.Level);
		}

		// Token: 0x0600CA13 RID: 51731 RVA: 0x00498B38 File Offset: 0x00496D38
		public void Freeze(Cell C, ScreenBuffer Buffer)
		{
			string text = this.ComputeDamage();
			if (C != null)
			{
				foreach (GameObject gameObject in C.GetObjectsInCell())
				{
					if (gameObject.PhaseMatches(this.ParentObject) && gameObject.TemperatureChange(-20 - 60 * base.Level, this.ParentObject, false, false, false, false, 0, null, null))
					{
						for (int i = 0; i < 5; i++)
						{
							gameObject.ParticleText("&C" + ((char)(219 + Stat.Random(0, 4))).ToString(), 2.9f, 1);
						}
						for (int j = 0; j < 5; j++)
						{
							gameObject.ParticleText("&c" + ((char)(219 + Stat.Random(0, 4))).ToString(), 2.9f, 1);
						}
						for (int k = 0; k < 5; k++)
						{
							gameObject.ParticleText("&Y" + ((char)(219 + Stat.Random(0, 4))).ToString(), 2.9f, 1);
						}
					}
				}
				foreach (GameObject gameObject2 in C.GetObjectsWithPart("Combat"))
				{
					if (gameObject2.PhaseMatches(this.ParentObject))
					{
						gameObject2.TakeDamage(text.RollCached(), "from %t freeze!", "Cold", null, null, null, this.ParentObject, null, null, null, false, false, false, false, false, false, false, false, false, 5, null);
					}
				}
			}
			Buffer.Goto(C.X, C.Y);
			string text2 = "&C";
			int num = Stat.Random(1, 3);
			if (num == 1)
			{
				text2 = "&C";
			}
			if (num == 2)
			{
				text2 = "&B";
			}
			if (num == 3)
			{
				text2 = "&Y";
			}
			int num2 = Stat.Random(1, 3);
			if (num2 == 1)
			{
				text2 += "^C";
			}
			if (num2 == 2)
			{
				text2 += "^B";
			}
			if (num2 == 3)
			{
				text2 += "^Y";
			}
			if (C.ParentZone == XRLCore.Core.Game.ZoneManager.ActiveZone)
			{
				Stat.Random(1, 3);
				Buffer.Write(text2 + ((char)(219 + Stat.Random(0, 4))).ToString(), true, false, false, null, -1);
				Popup._TextConsole.DrawBuffer(Buffer, null, false);
				Thread.Sleep(10);
			}
		}

		// Token: 0x0600CA14 RID: 51732 RVA: 0x00498E00 File Offset: 0x00497000
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CommandFreezeBreath")
			{
				if (base.OnWorldMap)
				{
					this.ParentObject.ShowFailure("You cannot do that on the world map.");
					return false;
				}
				ScreenBuffer scrapBuffer = ScreenBuffer.GetScrapBuffer1(false);
				XRLCore.Core.RenderMapToBuffer(scrapBuffer);
				List<Cell> list = base.PickLine(this.Range, AllowVis.Any, null, false, true, true, false, null, null, "Freezing Ultraray", true, true);
				if (list == null || list.Count <= 0)
				{
					return false;
				}
				if (list.Count == 1 && this.ParentObject.IsPlayer() && Popup.ShowYesNoCancel("Are you sure you want to target " + this.ParentObject.itself + "?", "Sounds/UI/ui_notification", true, DialogResult.Cancel) != DialogResult.Yes)
				{
					return false;
				}
				base.UseEnergy(1000, "Physical Mutation Freeze Breath");
				base.CooldownMyActivatedAbility(this.FreezeBreathActivatedAbilityID, 30, null, null, false);
				base.PlayWorldSound(this.Sound, 0.5f, 0f, true, null, 0f, 1f, 1f, int.MaxValue);
				int i = 0;
				int num = Math.Min(list.Count, this.Range);
				while (i < num)
				{
					if (list.Count == 1 || list[i] != this.ParentObject.CurrentCell)
					{
						this.Freeze(list[i], scrapBuffer);
					}
					if (i < num - 1 && list[i].IsSolidForProjectile(FreezeBreath.Projectile, this.ParentObject, null, this.ParentObject.Target, null, null, false))
					{
						break;
					}
					i++;
				}
			}
			IComponent<GameObject>.XDidY(this.ParentObject, "emit", "a vast freezing ray from " + this.ParentObject.its + " mouth", "!", null, null, this.ParentObject, null, false, false, null, null, false, false, false, false, false, null);
			return true;
		}

		// Token: 0x0600CA15 RID: 51733 RVA: 0x00498FD4 File Offset: 0x004971D4
		public override bool ChangeLevel(int NewLevel)
		{
			this.ParentObject.Physics.BrittleTemperature = -600 + -300 * base.Level;
			if (GameObject.Validate(ref this.VaporObject))
			{
				TemperatureOnHit temperatureOnHit = this.VaporObject.RequirePart<TemperatureOnHit>(false);
				if (temperatureOnHit != null)
				{
					temperatureOnHit.Amount = "-" + base.Level.ToString() + "d4";
				}
			}
			return base.ChangeLevel(NewLevel);
		}

		// Token: 0x0600CA16 RID: 51734 RVA: 0x0049904C File Offset: 0x0049724C
		private void AddAbility()
		{
			this.FreezeBreathActivatedAbilityID = base.AddMyActivatedAbility("Freezing Ultraray", "CommandFreezeBreath", "Physical Mutations", null, "*", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
		}

		// Token: 0x0600CA17 RID: 51735 RVA: 0x00499090 File Offset: 0x00497290
		public override bool Mutate(GameObject GO, int Level)
		{
			if (GO.Physics != null)
			{
				this.OldFreeze = GO.Physics.FreezeTemperature;
				this.OldBrittle = GO.Physics.BrittleTemperature;
			}
			if (this.CreateObject)
			{
				Body body = GO.Body;
				if (body != null)
				{
					BodyPart bodyPart = body.GetFirstPart(this.BodyPartType);
					if (bodyPart == null || (bodyPart.Equipped != null && !bodyPart.Equipped.CanBeUnequipped(null, null, false, true)))
					{
						BodyPart bodyPart2 = body.GetFirstPart("Head") ?? body.GetBody();
						BodyPart bodyPart3 = bodyPart2;
						string bodyPartType = this.BodyPartType;
						int num = 0;
						string text = null;
						string text2 = null;
						string text3 = null;
						string text4 = null;
						string text5 = "Mutation::FreezeBreath";
						int? num2 = new int?(bodyPart2.Category);
						string[] array = new string[] { "Back", "Arm", "Leg", "Foot", "Hands", "Feet", "Roots", "Thrown Weapon" };
						bodyPart = bodyPart3.AddPartAt(bodyPartType, num, text, text2, text3, text4, text5, num2, null, null, null, null, null, null, null, null, null, null, null, null, "Head", array, true);
					}
					if (bodyPart == null)
					{
						return false;
					}
					bodyPart.ForceUnequip(true, false, false);
					this.VaporObject = GameObjectFactory.Factory.CreateObject("Icy Vapor");
					this.VaporObject.GetPart<Armor>().WornOn = bodyPart.Type;
					GO.ForceEquipObject(this.VaporObject, bodyPart, true, new int?(0));
					this.AddAbility();
				}
			}
			else
			{
				this.AddAbility();
			}
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600CA18 RID: 51736 RVA: 0x00499274 File Offset: 0x00497474
		public override bool Unmutate(GameObject GO)
		{
			if (GO.Physics != null)
			{
				if (this.OldFreeze != -1)
				{
					GO.Physics.FreezeTemperature = this.OldFreeze;
				}
				if (this.OldBrittle != -1)
				{
					GO.Physics.BrittleTemperature = this.OldBrittle;
				}
				this.OldFreeze = -1;
				this.OldBrittle = -1;
				GO.Physics.Temperature = 25;
			}
			base.CleanUpMutationEquipment(GO, ref this.VaporObject);
			base.RemoveMyActivatedAbility(ref this.FreezeBreathActivatedAbilityID, null);
			return base.Unmutate(GO);
		}

		// Token: 0x04004EDA RID: 20186
		public const string MANAGER_ID = "Mutation::FreezeBreath";

		// Token: 0x04004EDB RID: 20187
		public const string ATTACH_PART = "Head";

		// Token: 0x04004EDC RID: 20188
		public string BodyPartType = "Face";

		// Token: 0x04004EDD RID: 20189
		public bool CreateObject = true;

		// Token: 0x04004EDE RID: 20190
		public int Range = 30;

		// Token: 0x04004EDF RID: 20191
		public GameObject VaporObject;

		// Token: 0x04004EE0 RID: 20192
		public int OldFreeze = -1;

		// Token: 0x04004EE1 RID: 20193
		public int OldBrittle = -1;

		// Token: 0x04004EE2 RID: 20194
		public string Sound = "hiss_high";

		// Token: 0x04004EE3 RID: 20195
		public Guid FreezeBreathActivatedAbilityID = Guid.Empty;

		// Token: 0x04004EE4 RID: 20196
		[NonSerialized]
		private static GameObject _Projectile;
	}
}

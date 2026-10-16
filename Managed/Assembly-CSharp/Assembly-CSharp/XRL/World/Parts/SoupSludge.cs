using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using Qud.API;
using XRL.Core;
using XRL.Language;
using XRL.Liquids;
using XRL.World.Anatomy;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	// Token: 0x02000E8E RID: 3726
	[Serializable]
	public class SoupSludge : IPart
	{
		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x06009602 RID: 38402 RVA: 0x00364C2D File Offset: 0x00362E2D
		public string ManagerID
		{
			get
			{
				return this.ParentObject.ID + "::SoupSludge";
			}
		}

		// Token: 0x06009603 RID: 38403 RVA: 0x00364C44 File Offset: 0x00362E44
		public SoupSludge()
		{
		}

		// Token: 0x06009604 RID: 38404 RVA: 0x00364C75 File Offset: 0x00362E75
		public SoupSludge(string LiquidID)
		{
			this.LiquidID = LiquidID;
		}

		// Token: 0x06009605 RID: 38405 RVA: 0x00364CAD File Offset: 0x00362EAD
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06009606 RID: 38406 RVA: 0x00364CB0 File Offset: 0x00362EB0
		public override void Initialize()
		{
			this.EvolveSludge();
		}

		// Token: 0x06009607 RID: 38407 RVA: 0x00364CB8 File Offset: 0x00362EB8
		public override void Remove()
		{
			this.ParentObject.RemoveBodyPartsByManager(this.ManagerID, true);
			base.Remove();
		}

		// Token: 0x06009608 RID: 38408 RVA: 0x00364CD3 File Offset: 0x00362ED3
		public override IPart DeepCopy(GameObject Parent)
		{
			SoupSludge soupSludge = base.DeepCopy(Parent) as SoupSludge;
			soupSludge.ComponentLiquids = new List<string>(this.ComponentLiquids);
			return soupSludge;
		}

		// Token: 0x06009609 RID: 38409 RVA: 0x00364CF2 File Offset: 0x00362EF2
		public static string GetPrefix(int Count)
		{
			if (Count <= 0)
			{
				return "";
			}
			if (Count <= SoupSludge.Prefixes.Length)
			{
				return SoupSludge.Prefixes[Count - 1];
			}
			return "poly";
		}

		// Token: 0x0600960A RID: 38410 RVA: 0x00364D18 File Offset: 0x00362F18
		public void CatalyzeLiquid(string ID)
		{
			if (this.ComponentLiquids.Contains(ID))
			{
				return;
			}
			Mutations mutations = this.ParentObject.RequirePart<Mutations>(false);
			LiquidSpitter liquidSpitter = mutations.GetMutation("LiquidSpitter") as LiquidSpitter;
			if (liquidSpitter == null)
			{
				liquidSpitter = new LiquidSpitter(ID);
				mutations.AddMutation(liquidSpitter, 1, true);
			}
			liquidSpitter.AddLiquid(ID, null);
			this.ComponentLiquids.Add(ID);
			this.EquipPseudopod(ID);
			this.ParentObject.GetStat("Speed").BaseValue += 10;
			this.ParentObject.GetStat("Hitpoints").BaseValue += 20;
			this.ParentObject.GetStat("Level").BaseValue += 3;
		}

		// Token: 0x0600960B RID: 38411 RVA: 0x00364DDC File Offset: 0x00362FDC
		public void EvolveSludge()
		{
			if (this.LiquidID == null)
			{
				return;
			}
			string text = this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false);
			this.CatalyzeLiquid(this.LiquidID);
			this.CatalyzeName();
			this.CatalyzeMessage(this.LiquidID, text);
			this.LiquidID = null;
		}

		// Token: 0x0600960C RID: 38412 RVA: 0x00364E44 File Offset: 0x00363044
		public void CatalyzeName()
		{
			if (this.ComponentLiquids.Count == 0)
			{
				return;
			}
			string text = SoupSludge.GetPrefix(this.ComponentLiquids.Count) + "sludge";
			if (this.ParentObject.HasProperName)
			{
				string text2 = SoupSludge.GetPrefix(this.ComponentLiquids.Count - 1) + "sludge";
				this.ParentObject.DisplayName = this.ParentObject.DisplayNameOnlyDirect.Replace(text2, text, StringComparison.OrdinalIgnoreCase, true);
				SocialRoles socialRoles;
				if (this.ParentObject.TryGetPart<SocialRoles>(out socialRoles))
				{
					for (int i = 0; i < socialRoles.RoleList.Count; i++)
					{
						socialRoles.RoleList[i] = socialRoles.RoleList[i].Replace(text2, text, StringComparison.OrdinalIgnoreCase, true);
					}
					socialRoles.UpdateRoles();
					return;
				}
			}
			else
			{
				this.ParentObject.DisplayName = text;
			}
		}

		// Token: 0x0600960D RID: 38413 RVA: 0x00364F20 File Offset: 0x00363120
		public void CatalyzeMessage(string ID, string OldName)
		{
			if (this.ComponentLiquids.Count >= 5)
			{
				JournalAPI.AddAccomplishment("You witnessed the rare formation of " + Grammar.A(SoupSludge.GetPrefix(this.ComponentLiquids.Count) + "sludge", false) + ".", "=name= was blessed to witness the rare formation of " + Grammar.A(SoupSludge.GetPrefix(this.ComponentLiquids.Count) + "sludge", false) + ".", string.Concat(new string[]
				{
					"Deep in ",
					JournalAPI.GetLandmarkNearestPlayer().Text,
					", =name= stumbled on a soup sludge performing a secret ritual. Because of ",
					Grammar.MakePossessive(The.Player.BaseDisplayNameStripped),
					" <spice.elements.",
					The.Player.GetMythicDomain(5, null),
					".quality.!random>, the sludge taught ",
					The.Player.BaseDisplayNameStripped,
					" the secret to becoming ",
					Grammar.A(SoupSludge.GetPrefix(this.ComponentLiquids.Count), false),
					"."
				}), null, "general", MuralCategory.HasInspiringExperience, MuralWeight.Medium, null, -1L, true);
			}
			if (this.ParentObject.HasProperName)
			{
				base.EmitMessage(string.Concat(new string[]
				{
					"The ",
					LiquidVolume.GetLiquid(ID).GetName(),
					" catalyzes ",
					OldName,
					" into ",
					Grammar.A(SoupSludge.GetPrefix(this.ComponentLiquids.Count) + "sludge", false),
					"."
				}), ' ', false, false, false, null, null);
				return;
			}
			base.EmitMessage(string.Concat(new string[]
			{
				"The ",
				LiquidVolume.GetLiquid(ID).GetName(),
				" catalyzes ",
				OldName,
				" into ",
				this.ParentObject.an(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, null, false, null, false),
				"."
			}), ' ', false, false, false, null, null);
		}

		// Token: 0x0600960E RID: 38414 RVA: 0x0036513C File Offset: 0x0036333C
		public BodyPart GetEmptyHand()
		{
			foreach (BodyPart bodyPart in this.ParentObject.Body.GetPart("Hand"))
			{
				if (!bodyPart.Extrinsic && bodyPart.Equipped == null)
				{
					return bodyPart;
				}
			}
			return null;
		}

		// Token: 0x0600960F RID: 38415 RVA: 0x003651B0 File Offset: 0x003633B0
		public BodyPart RequireEmptyHand()
		{
			BodyPart bodyPart = this.GetEmptyHand();
			if (bodyPart == null)
			{
				BodyPart body = this.ParentObject.Body.GetBody();
				bodyPart = body.AddPartAt("Pseudopod", 2, null, null, null, null, this.ManagerID, null, null, null, null, null, null, null, null, null, null, null, null, null, "Hand", "Missile Weapon", true);
				body.AddPartAt(bodyPart, "Pseudopod", 1, null, null, null, null, this.ManagerID, null, null, null, null, null, null, null, null, null, null, null, null, null, true);
			}
			return bodyPart;
		}

		// Token: 0x06009610 RID: 38416 RVA: 0x00365300 File Offset: 0x00363500
		public void EquipPseudopod(string ID)
		{
			GameObject gameObject = null;
			uint num = <PrivateImplementationDetails>.ComputeStringHash(ID);
			if (num <= 2060791899U)
			{
				if (num <= 829063524U)
				{
					if (num <= 518813454U)
					{
						if (num != 320139124U)
						{
							if (num != 501402871U)
							{
								if (num == 518813454U)
								{
									if (ID == "warmstatic")
									{
										gameObject = GameObject.Create("Entropic Pseudopod");
									}
								}
							}
							else if (ID == "wax")
							{
								gameObject = GameObject.Create("Waxen Pseudopod");
							}
						}
						else if (ID == "wine")
						{
							gameObject = GameObject.Create("Lush Pseudopod");
						}
					}
					else if (num != 570417050U)
					{
						if (num != 613496948U)
						{
							if (num == 829063524U)
							{
								if (ID == "honey")
								{
									gameObject = GameObject.Create("Honeyed Pseudopod");
								}
							}
						}
						else if (ID == "cider")
						{
							gameObject = GameObject.Create("Spiced Pseudopod");
						}
					}
					else if (ID == "ooze")
					{
						gameObject = GameObject.Create("Oozing Pseudopod2");
					}
				}
				else if (num <= 1237752336U)
				{
					if (num != 1025215916U)
					{
						if (num != 1126633753U)
						{
							if (num == 1237752336U)
							{
								if (ID == "water")
								{
									gameObject = GameObject.Create("Watery Pseudopod");
								}
							}
						}
						else if (ID == "putrid")
						{
							gameObject = GameObject.Create("Putrid Pseudopod");
						}
					}
					else if (ID == "goo")
					{
						gameObject = GameObject.Create("Gooey Pseudopod2");
					}
				}
				else if (num <= 1426490280U)
				{
					if (num != 1345354536U)
					{
						if (num == 1426490280U)
						{
							if (ID == "acid")
							{
								gameObject = GameObject.Create("Acidic Pseudopod");
								this.ParentObject.GetStat("AcidResistance").BaseValue += 100;
							}
						}
					}
					else if (ID == "convalessence")
					{
						gameObject = GameObject.Create("Luminous Pseudopod2");
						this.ParentObject.GetStat("ColdResistance").BaseValue += 100;
						this.ParentObject.RequirePart<LightSource>(false).Radius = 6;
					}
				}
				else if (num != 1544336367U)
				{
					if (num == 2060791899U)
					{
						if (ID == "lava")
						{
							gameObject = GameObject.Create("Magmatic Pseudopod");
							this.ParentObject.AddPart<LavaSludge>(new LavaSludge(), true, false);
							this.ParentObject.GetStat("HeatResistance").BaseValue += 100;
							this.ParentObject.Physics.Temperature = 1000;
						}
					}
				}
				else if (ID == "gel")
				{
					gameObject = GameObject.Create("Unctuous Pseudopod");
					this.ParentObject.AddPart<DisarmOnHit>(new DisarmOnHit(100), true, false);
				}
			}
			else if (num <= 2813705861U)
			{
				if (num <= 2245368527U)
				{
					if (num != 2109435095U)
					{
						if (num != 2205864411U)
						{
							if (num == 2245368527U)
							{
								if (ID == "brainbrine")
								{
									gameObject = GameObject.Create("Nervous Pseudopod");
								}
							}
						}
						else if (ID == "slime")
						{
							gameObject = GameObject.Create("Slimy Pseudopod");
							this.ParentObject.AddPart<DisarmOnHit>(new DisarmOnHit(100), true, false);
						}
					}
					else if (ID == "neutronflux")
					{
						gameObject = GameObject.Create("Neutronic Pseudopod");
					}
				}
				else if (num != 2340250123U)
				{
					if (num != 2699661231U)
					{
						if (num == 2813705861U)
						{
							if (ID == "cloning")
							{
								gameObject = GameObject.Create("Homogenized Pseudopod");
								this.ParentObject.AddPart<CloneOnHit>(new CloneOnHit(), true, false);
							}
						}
					}
					else if (ID == "ink")
					{
						gameObject = GameObject.Create("Inky Pseudopod");
						this.ParentObject.AddPart<DisarmOnHit>(new DisarmOnHit(100), true, false);
					}
				}
				else if (ID == "algae")
				{
					gameObject = GameObject.Create("Algal Pseudopod");
				}
			}
			else if (num <= 3275256123U)
			{
				if (num != 3209073518U)
				{
					if (num != 3255728001U)
					{
						if (num == 3275256123U)
						{
							if (ID == "oil")
							{
								gameObject = GameObject.Create("Oily Pseudopod");
								this.ParentObject.AddPart<DisarmOnHit>(new DisarmOnHit(100), true, false);
							}
						}
					}
					else if (ID == "salt")
					{
						gameObject = GameObject.Create("Salty Pseudopod");
					}
				}
				else if (ID == "sunslag")
				{
					gameObject = GameObject.Create("Radiant Pseudopod");
					this.ParentObject.GetStat("Speed").BaseValue += 20;
					this.ParentObject.RequirePart<LightSource>(false).Radius = 6;
				}
			}
			else if (num <= 3907046420U)
			{
				if (num != 3525086347U)
				{
					if (num == 3907046420U)
					{
						if (ID == "asphalt")
						{
							gameObject = GameObject.Create("Tarry Pseudopod");
						}
					}
				}
				else if (ID == "sap")
				{
					gameObject = GameObject.Create("Sugary Pseudopod");
				}
			}
			else if (num != 3991842055U)
			{
				if (num == 4223973875U)
				{
					if (ID == "sludge")
					{
						gameObject = GameObject.Create("Sludgy Pseudopod2");
					}
				}
			}
			else if (ID == "blood")
			{
				gameObject = GameObject.Create("Bloody Pseudopod");
			}
			if (gameObject != null && !this.RequireEmptyHand().Equip(gameObject, new int?(0), false, false, false, true))
			{
				gameObject.Destroy(null, false, false, null);
			}
		}

		// Token: 0x06009611 RID: 38417 RVA: 0x003659AC File Offset: 0x00363BAC
		public override bool Render(RenderEvent E)
		{
			if (this.ComponentLiquids.Count > 0)
			{
				long elapsedMilliseconds = XRLCore.FrameTimer.ElapsedMilliseconds;
				if (this.Hero == 1)
				{
					if (elapsedMilliseconds % 480L > 240L)
					{
						return true;
					}
					if (this.Detail == null)
					{
						char? c;
						this.Detail = ((ColorUtility.FindLastForeground(E.ColorString) != null) ? c.GetValueOrDefault().ToString() : null) ?? "M";
					}
					int num = (int)(elapsedMilliseconds % (long)(480 * this.ComponentLiquids.Count));
					E.ColorString = E.ColorString + "&" + LiquidVolume.GetLiquid(this.ComponentLiquids[num / 480]).GetColor();
					E.DetailColor = this.Detail;
				}
				else if (this.Hero == 0)
				{
					if (this.ParentObject.HasIntProperty("Hero") || this.ParentObject.HasPart<GivesRep>())
					{
						this.Hero = 1;
					}
					else
					{
						this.Hero = 2;
					}
				}
				else
				{
					int num2 = (int)(elapsedMilliseconds % (long)(240 * this.ComponentLiquids.Count));
					E.ColorString = E.ColorString + "&" + LiquidVolume.GetLiquid(this.ComponentLiquids[num2 / 240]).GetColor();
				}
			}
			return true;
		}

		// Token: 0x06009612 RID: 38418 RVA: 0x00365B18 File Offset: 0x00363D18
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009613 RID: 38419 RVA: 0x00365B1B File Offset: 0x00363D1B
		public override bool WantEvent(int ID, int Cascade)
		{
			return base.WantEvent(ID, Cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == EnteredCellEvent.ID || ID == SingletonEvent<BeginTakeActionEvent>.ID || ID == BeforeDeathRemovalEvent.ID;
		}

		// Token: 0x06009614 RID: 38420 RVA: 0x00365B4C File Offset: 0x00363D4C
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Object.HasProperName)
			{
				return true;
			}
			if (this.ComponentLiquids.Count > this.MaxAdjectives)
			{
				XRLGame game = The.Game;
				long num = ((game != null) ? game.Turns : 0L);
				if (num != this.DisplayTurn)
				{
					this.DisplayTurn = num;
					this.Components.Clear();
					Random random = new Random(this.DisplayTurn.GetHashCode());
					int num2 = Math.Min(this.MaxAdjectives, this.ComponentLiquids.Count);
					while (this.Components.Count < num2)
					{
						int num3 = random.Next(0, this.ComponentLiquids.Count);
						if (!this.Components.Contains(num3))
						{
							this.Components.Add(num3);
						}
					}
				}
				using (List<int>.Enumerator enumerator = this.Components.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						int num4 = enumerator.Current;
						BaseLiquid liquid = LiquidVolume.GetLiquid(this.ComponentLiquids[num4]);
						E.AddAdjective(liquid.GetAdjective(null), 0);
					}
					goto IL_015B;
				}
			}
			foreach (string text in this.ComponentLiquids)
			{
				E.AddAdjective(LiquidVolume.GetLiquid(text).GetAdjective(null), 0);
			}
			IL_015B:
			return base.HandleEvent(E);
		}

		// Token: 0x06009615 RID: 38421 RVA: 0x00365CD8 File Offset: 0x00363ED8
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (this.LiquidID != null)
			{
				return true;
			}
			GameObject openLiquidVolume = E.Cell.GetOpenLiquidVolume();
			if (openLiquidVolume == null)
			{
				return true;
			}
			LiquidVolume liquidVolume = openLiquidVolume.LiquidVolume;
			if (!this.ReactWith(liquidVolume.GetPrimaryLiquidID(), liquidVolume))
			{
				this.ReactWith(liquidVolume.GetSecondaryLiquidID(), liquidVolume);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009616 RID: 38422 RVA: 0x00365D2C File Offset: 0x00363F2C
		public override bool HandleEvent(BeforeDeathRemovalEvent E)
		{
			if (E.Killer != null && E.Killer.IsPlayer())
			{
				int count = this.ComponentLiquids.Count;
				if (count != 3)
				{
					if (count != 5)
					{
						if (count == 10)
						{
							Achievement.KILL_DECASLUDGE.Unlock();
						}
					}
					else
					{
						Achievement.KILL_PENTASLUDGE.Unlock();
					}
				}
				else
				{
					Achievement.KILL_TRISLUDGE.Unlock();
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009617 RID: 38423 RVA: 0x00365D92 File Offset: 0x00363F92
		public override bool HandleEvent(BeginTakeActionEvent E)
		{
			if (this.LiquidID != null)
			{
				this.EvolveSludge();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009618 RID: 38424 RVA: 0x00365DAC File Offset: 0x00363FAC
		public bool ReactWith(string ID, LiquidVolume LV)
		{
			if (ID == null || this.ComponentLiquids.Contains(ID))
			{
				return false;
			}
			if (ID == "cloning" || ID == "proteangunk")
			{
				return false;
			}
			base.DidX("start", "reacting with the " + LiquidVolume.GetLiquid(ID).GetName(LV), null, null, null, null, null, false, false, null, null, false, false, false, false, false, null);
			LV.UseDrams(500);
			this.LiquidID = ID;
			return true;
		}

		// Token: 0x04003E07 RID: 15879
		public static readonly string[] Prefixes = new string[]
		{
			"mono", "di", "tri", "tetra", "penta", "hexa", "hepta", "octa", "ennea", "deca",
			"hendeca", "dodeca", "triskaideca", "tetrakaideca", "pentakaideca", "hexakaideca", "heptakaideca", "octakaideca", "enneakaideca", "icosa"
		};

		// Token: 0x04003E08 RID: 15880
		public List<string> ComponentLiquids = new List<string>();

		// Token: 0x04003E09 RID: 15881
		public string LiquidID;

		// Token: 0x04003E0A RID: 15882
		public int MaxAdjectives = int.MaxValue;

		// Token: 0x04003E0B RID: 15883
		public const int CLR_DIV = 240;

		// Token: 0x04003E0C RID: 15884
		[NonSerialized]
		private byte Hero;

		// Token: 0x04003E0D RID: 15885
		[NonSerialized]
		private string Detail;

		// Token: 0x04003E0E RID: 15886
		[NonSerialized]
		private List<int> Components = new List<int>();

		// Token: 0x04003E0F RID: 15887
		[NonSerialized]
		private long DisplayTurn = -1L;
	}
}

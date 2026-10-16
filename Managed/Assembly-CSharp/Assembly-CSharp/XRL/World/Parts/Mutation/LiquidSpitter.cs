using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleLib.Console;
using XRL.World.Text;

namespace XRL.World.Parts.Mutation
{
	// Token: 0x0200144B RID: 5195
	[Serializable]
	public class LiquidSpitter : BaseMutation
	{
		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x0600CADF RID: 51935 RVA: 0x0049BE64 File Offset: 0x0049A064
		public string LiquidName
		{
			get
			{
				if (this._LiquidName == null)
				{
					GameObject gameObject = this.CreatePool(true);
					this._LiquidName = gameObject.LiquidVolume.GetLiquidName();
					gameObject.Obliterate(null, false, null);
				}
				return this._LiquidName;
			}
		}

		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x0600CAE0 RID: 51936 RVA: 0x0049BEA2 File Offset: 0x0049A0A2
		// (set) Token: 0x0600CAE1 RID: 51937 RVA: 0x0049BEAA File Offset: 0x0049A0AA
		public string InitialLiquid
		{
			get
			{
				return this.GetLiquidSpec();
			}
			set
			{
				this.AddLiquidSpec(value, true);
			}
		}

		// Token: 0x0600CAE2 RID: 51938 RVA: 0x0049BEB4 File Offset: 0x0049A0B4
		public LiquidSpitter()
		{
		}

		// Token: 0x0600CAE3 RID: 51939 RVA: 0x0049BED2 File Offset: 0x0049A0D2
		public LiquidSpitter(string Liquid)
			: this()
		{
			this.AddLiquid(Liquid, null);
		}

		// Token: 0x0600CAE4 RID: 51940 RVA: 0x0049BEE2 File Offset: 0x0049A0E2
		public override bool CanLevel()
		{
			return false;
		}

		// Token: 0x0600CAE5 RID: 51941 RVA: 0x0049BEE5 File Offset: 0x0049A0E5
		public int GetCooldown()
		{
			return 40;
		}

		// Token: 0x0600CAE6 RID: 51942 RVA: 0x0049BEE9 File Offset: 0x0049A0E9
		public override IPart DeepCopy(GameObject Parent)
		{
			LiquidSpitter liquidSpitter = base.DeepCopy(Parent) as LiquidSpitter;
			liquidSpitter.Pool = this.Pool;
			liquidSpitter.Liquids = new List<string>(this.Liquids);
			liquidSpitter.Proportions = new List<string>(this.Proportions);
			return liquidSpitter;
		}

		// Token: 0x0600CAE7 RID: 51943 RVA: 0x0049BF25 File Offset: 0x0049A125
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			base.Read(Basis, Reader);
			if (Reader.FileVersion < 406)
			{
				while (this.Proportions.Count < this.Liquids.Count)
				{
					this.Proportions.Add(null);
				}
			}
		}

		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x0600CAE8 RID: 51944 RVA: 0x0049BF64 File Offset: 0x0049A164
		public string GetAnimationColor
		{
			get
			{
				string text;
				if (this.Liquids.Count > 0 && LiquidSpitter.LiquidAnimationColors.TryGetValue(this.Liquids.GetRandomElementCosmetic<string>(), out text))
				{
					return text;
				}
				return "&y";
			}
		}

		// Token: 0x0600CAE9 RID: 51945 RVA: 0x0049BFA0 File Offset: 0x0049A1A0
		public override void CollectStats(Templates.StatCollector stats, int Level)
		{
			stats.Set("Liquid", ColorUtility.StripBackgroundFormatting(this.LiquidName), false, 0);
			stats.Set("Range", 8, false, 0);
			stats.Set("Area", "3x3", false, 0);
			stats.CollectCooldownTurns(base.MyActivatedAbility(this.ActivatedAbilityID, null), this.GetCooldown(), 0);
		}

		// Token: 0x0600CAEA RID: 51946 RVA: 0x0049BFFF File Offset: 0x0049A1FF
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AIGetOffensiveAbilityListEvent.ID || ID == PooledEvent<CommandEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID;
		}

		// Token: 0x0600CAEB RID: 51947 RVA: 0x0049C028 File Offset: 0x0049A228
		public override bool HandleEvent(AIGetOffensiveAbilityListEvent E)
		{
			if (E.Distance <= 8 && base.IsMyActivatedAbilityAIUsable(this.ActivatedAbilityID, null) && GameObject.Validate(E.Target) && E.Actor.HasLOSTo(E.Target, true, false, true, true, null))
			{
				E.Add("CommandSpitLiquid", 1, null, false, false, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CAEC RID: 51948 RVA: 0x0049C08C File Offset: 0x0049A28C
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command != "CommandSpitLiquid")
			{
				return base.HandleEvent(E);
			}
			if (this.Liquids.Count == 0)
			{
				MetricsManager.LogError(string.Format("No liquids to spit (Creature: {0})", this.ParentObject));
				return this.ParentObject.ShowFailure("You lack a liquid to spit!");
			}
			List<Cell> list = base.PickBurst(1, 8, false, AllowVis.OnlyVisible, "Spit");
			if (list.IsNullOrEmpty<Cell>())
			{
				return false;
			}
			if (list.Any<Cell>((Cell C) => C.DistanceTo(this.ParentObject) > 9))
			{
				return this.ParentObject.ShowFailure("That is out of range! (8 squares)");
			}
			this.ParentObject.PlayWorldSound("Sounds/Abilities/sfx_ability_creature_liquid_spit", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
			SlimeGlands.SlimeAnimation(this.GetAnimationColor, this.ParentObject.CurrentCell, list[0]);
			base.UseEnergy(1000, "Physical Mutation Spit Liquid");
			base.CooldownMyActivatedAbility(this.ActivatedAbilityID, this.GetCooldown(), null, null, false);
			base.DidX("spit", "a puddle of " + this.LiquidName, "!", null, null, this.ParentObject, null, false, false, null, null, false, false, false, false, false, null);
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				if (80.in100() || i == 0)
				{
					list[i].AddObject(this.CreatePool(false), false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
				}
				i++;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CAED RID: 51949 RVA: 0x0049C214 File Offset: 0x0049A414
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject))
			{
				if (this.Liquids.Contains("salt"))
				{
					E.Add("salt", 1);
				}
				else if (this.Liquids.Contains("convalessence"))
				{
					E.Add("ice", 1);
				}
				else if (this.Liquids.Contains("sunslag"))
				{
					E.Add("stars", 1);
				}
				else if (this.Liquids.Contains("brainbrine"))
				{
					E.Add("scholarship", 1);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600CAEE RID: 51950 RVA: 0x0049C2B4 File Offset: 0x0049A4B4
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600CAEF RID: 51951 RVA: 0x0049C2B7 File Offset: 0x0049A4B7
		public override string GetDescription()
		{
			return "You spit a puddle of " + ColorUtility.StripBackgroundFormatting(this.LiquidName) + ".";
		}

		// Token: 0x0600CAF0 RID: 51952 RVA: 0x0049C2D3 File Offset: 0x0049A4D3
		public override string GetLevelText(int Level)
		{
			return "Range: 8\nArea: 3x3\nCooldown: 10 rounds";
		}

		// Token: 0x0600CAF1 RID: 51953 RVA: 0x0049C2DC File Offset: 0x0049A4DC
		public override bool Mutate(GameObject GO, int Level)
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Spit Liquid", "CommandSpitLiquid", "Physical Mutations", null, "\u00ad", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			return base.Mutate(GO, Level);
		}

		// Token: 0x0600CAF2 RID: 51954 RVA: 0x0049C325 File Offset: 0x0049A525
		public override bool Unmutate(GameObject GO)
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			return base.Unmutate(GO);
		}

		// Token: 0x0600CAF3 RID: 51955 RVA: 0x0049C33C File Offset: 0x0049A53C
		public bool TryGetPoolBlueprint(out GameObjectBlueprint Blueprint)
		{
			if (this.Pool.IsNullOrEmpty())
			{
				Blueprint = null;
				return false;
			}
			if (!GameObjectFactory.Factory.Blueprints.TryGetValue(this.Pool, out Blueprint))
			{
				MetricsManager.LogError("LiquidSpitter: Pool blueprint '" + this.Pool + "' does not exist, falling back");
				Blueprint = null;
				return false;
			}
			if (!Blueprint.InheritsFrom("Water"))
			{
				MetricsManager.LogError("LiquidSpitter: Pool blueprint '" + this.Pool + "' does not inherit from 'Water', falling back");
				Blueprint = null;
				return false;
			}
			string text;
			if (!Blueprint.TryGetPartParameter<string>("LiquidVolume", "StartVolume", out text) || text.IsNullOrEmpty())
			{
				MetricsManager.LogError("LiquidSpitter: Pool blueprint " + this.Pool + " has an invalid starting volume, falling back");
				Blueprint = null;
				return false;
			}
			return true;
		}

		// Token: 0x0600CAF4 RID: 51956 RVA: 0x0049C3FC File Offset: 0x0049A5FC
		public GameObject CreatePool(bool Sample = false)
		{
			GameObjectBlueprint blueprint;
			if (!this.TryGetPoolBlueprint(out blueprint))
			{
				blueprint = GameObjectFactory.Factory.GetBlueprint(LiquidSpitter.BasePool);
			}
			GameObject gameObject = GameObject.Create(blueprint, 0, 0, null, null, null, Sample ? "Sample" : null, null);
			LiquidVolume liquidVolume = gameObject.LiquidVolume;
			liquidVolume.ComponentLiquids.Clear();
			string startVolume = liquidVolume.StartVolume;
			int i = 0;
			int count = this.Liquids.Count;
			int count2 = this.Proportions.Count;
			while (i < count)
			{
				string text = this.Liquids[i];
				int num = ((i < count2) ? this.Proportions[i].Coalesce(startVolume) : startVolume).RollCached();
				if (num > 0)
				{
					liquidVolume.ComponentLiquids[text] = num;
				}
				i++;
			}
			liquidVolume.Update();
			if (liquidVolume.Primary.IsNullOrEmpty())
			{
				MetricsManager.LogError(string.Concat(new string[]
				{
					"Spitting liquid without primary ",
					string.Format("(Blueprint: {0}, Liquids: {1}, Proportions: {2}, Creature: {3}, Volume: {4}, ", new object[]
					{
						blueprint.Name,
						string.Join(", ", this.Liquids),
						string.Join(", ", this.Proportions),
						this.ParentObject,
						liquidVolume.Volume
					}),
					"Components: ",
					string.Join<KeyValuePair<string, int>>(", ", liquidVolume.ComponentLiquids),
					")"
				}));
			}
			return gameObject;
		}

		// Token: 0x0600CAF5 RID: 51957 RVA: 0x0049C570 File Offset: 0x0049A770
		public void CheckDilute()
		{
			if (this.Liquids.Count == 1 && LiquidSpitter.Dilute.Contains(this.Liquids[0]))
			{
				this._LiquidName = null;
				this.AddLiquidInternal("water", null);
			}
		}

		// Token: 0x0600CAF6 RID: 51958 RVA: 0x0049C5AB File Offset: 0x0049A7AB
		private void AddLiquidInternal(string ID, string Proportion = null)
		{
			if (this.Liquids.Contains(ID))
			{
				return;
			}
			if (LiquidSpitter.Exclude.Contains(ID))
			{
				return;
			}
			this.Liquids.Add(ID);
			this.Proportions.Add(Proportion);
		}

		// Token: 0x0600CAF7 RID: 51959 RVA: 0x0049C5E2 File Offset: 0x0049A7E2
		public void AddLiquid(string ID, string Proportion = null)
		{
			this._LiquidName = null;
			this.AddLiquidInternal(ID, null);
			this.CheckDilute();
		}

		// Token: 0x0600CAF8 RID: 51960 RVA: 0x0049C5FC File Offset: 0x0049A7FC
		public string GetLiquidSpec()
		{
			int count = this.Liquids.Count;
			int count2 = this.Proportions.Count;
			if (count == 0)
			{
				return "";
			}
			if (count == 1 && (count2 == 0 || this.Proportions[0].IsNullOrEmpty()))
			{
				return this.Liquids[0];
			}
			string text;
			using (TextBuilder textBuilder = TextBuilder.Get(64))
			{
				for (int i = 0; i < count; i++)
				{
					textBuilder.Compound(this.Liquids[i], ',');
					if (i < count2 && !this.Proportions[i].IsNullOrEmpty())
					{
						textBuilder.Compound(this.Proportions[i], ':');
					}
				}
				text = textBuilder.ToString();
			}
			return text;
		}

		// Token: 0x0600CAF9 RID: 51961 RVA: 0x0049C6CC File Offset: 0x0049A8CC
		public void AddLiquidSpec(string Spec, bool Replace = false)
		{
			this._LiquidName = null;
			if (Replace)
			{
				this.Liquids.Clear();
				this.Proportions.Clear();
			}
			if (Spec.IsNullOrEmpty())
			{
				return;
			}
			foreach (ReadOnlySpan<char> readOnlySpan in Spec.DelimitedBy(','))
			{
				ReadOnlySpan<char> readOnlySpan2;
				ReadOnlySpan<char> readOnlySpan3;
				readOnlySpan.Split(':', out readOnlySpan2, out readOnlySpan3);
				if (readOnlySpan3.IsEmpty)
				{
					readOnlySpan.Split('-', out readOnlySpan2, out readOnlySpan3);
				}
				this.AddLiquidInternal(readOnlySpan2.ToString(), readOnlySpan3.IsEmpty ? null : readOnlySpan3.ToString());
			}
			this.CheckDilute();
		}

		// Token: 0x0600CAFA RID: 51962 RVA: 0x0049C77C File Offset: 0x0049A97C
		public void AddLiquids(IReadOnlyList<string> Liquids, IReadOnlyList<string> Proportions = null, bool Replace = false)
		{
			this._LiquidName = null;
			if (Replace)
			{
				this.Liquids.Clear();
				this.Proportions.Clear();
			}
			if (Liquids.IsNullOrEmpty<string>())
			{
				return;
			}
			int i = 0;
			int count = Liquids.Count;
			int num = ((Proportions != null) ? Proportions.Count : 0);
			while (i < count)
			{
				this.AddLiquidInternal(Liquids[i], (i < num) ? Proportions[i] : null);
				i++;
			}
			this.CheckDilute();
		}

		// Token: 0x04004EFA RID: 20218
		public static readonly string BasePool = "BasePool";

		// Token: 0x04004EFB RID: 20219
		public static readonly Dictionary<string, string> LiquidAnimationColors = new Dictionary<string, string>
		{
			{ "lava", "&W" },
			{ "acid", "&G" },
			{ "honey", "&w" },
			{ "slime", "&g" },
			{ "water", "&B" },
			{ "salt", "&Y" },
			{ "base", "&B" },
			{ "wine", "&m" },
			{ "asphalt", "&K" },
			{ "oil", "&K" },
			{ "blood", "&r" },
			{ "sludge", "&w" },
			{ "goo", "&G" },
			{ "putrid", "&g" },
			{ "gel", "&Y" },
			{ "ooze", "&K" },
			{ "cider", "&w" },
			{ "convalessence", "&C" },
			{ "neutronflux", "&y" },
			{ "cloning", "&Y" },
			{ "proteangunk", "&c" },
			{ "wax", "&Y" },
			{ "ink", "&K" },
			{ "sap", "&W" },
			{ "brainbrine", "&g" },
			{ "algae", "&g" },
			{ "sunslag", "&Y" },
			{ "warmstatic", "&y" }
		};

		// Token: 0x04004EFC RID: 20220
		public static readonly string[] Exclude = new string[] { "neutronflux" };

		// Token: 0x04004EFD RID: 20221
		public static readonly string[] Dilute = new string[] { "cloning", "brainbrine", "sunslag", "warmstatic" };

		// Token: 0x04004EFE RID: 20222
		public const string ABL_CMD = "CommandSpitLiquid";

		// Token: 0x04004EFF RID: 20223
		[FieldSaveVersion(406)]
		public new string Pool;

		// Token: 0x04004F00 RID: 20224
		public List<string> Liquids = new List<string>();

		// Token: 0x04004F01 RID: 20225
		[FieldSaveVersion(406)]
		public List<string> Proportions = new List<string>();

		// Token: 0x04004F02 RID: 20226
		private string _LiquidName;
	}
}

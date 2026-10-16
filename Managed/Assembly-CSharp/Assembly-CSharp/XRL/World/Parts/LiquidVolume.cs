using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AiUnity.NLog.Core;
using ConsoleLib.Console;
using Newtonsoft.Json;
using Occult.Engine.CodeGeneration;
using XRL.Collections;
using XRL.Language;
using XRL.Liquids;
using XRL.Rules;
using XRL.UI;
using XRL.World.Anatomy;
using XRL.World.Capabilities;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000DC2 RID: 3522
	[GeneratePoolingPartial]
	[GenerateSerializationPartial]
	[HasModSensitiveStaticCache]
	[Serializable]
	public class LiquidVolume : IPart
	{
		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x06008D52 RID: 36178 RVA: 0x0032438A File Offset: 0x0032258A
		[GeneratedCode("PoolPartialsGenerator", "1.0.0.0")]
		public override IPartPool Pool
		{
			get
			{
				return LiquidVolume.LiquidVolumePool;
			}
		}

		// Token: 0x06008D53 RID: 36179 RVA: 0x00324394 File Offset: 0x00322594
		[GeneratedCode("PoolPartialsGenerator", "1.0.0.0")]
		public override void Reset()
		{
			base.Reset();
			this.Flags = 32;
			this.FrameOffset = 0;
			this.MaxVolume = -1;
			this.Volume = 0;
			this.StartVolume = "";
			this.NamePreposition = null;
			this.UnknownNamePreposition = null;
			this.AutoCollectLiquidType = null;
			this.Primary = null;
			this.Secondary = null;
			this._SmearedName = null;
			this._SmearedColor = null;
			this._StainedName = null;
			this._StainedColor = null;
			this.ComponentLiquids.Clear();
			this.LastPaintMask = -1;
		}

		// Token: 0x06008D54 RID: 36180 RVA: 0x00324420 File Offset: 0x00322620
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.Flags);
			Writer.WriteOptimized(this.MaxVolume);
			Writer.WriteOptimized(this.Volume);
			Writer.WriteOptimized(this.StartVolume);
			Writer.WriteOptimized(this.NamePreposition);
			Writer.WriteOptimized(this.UnknownNamePreposition);
			Writer.WriteOptimized(this.AutoCollectLiquidType);
			Writer.WriteOptimized(this.Primary);
			Writer.WriteOptimized(this.Secondary);
			Writer.WriteOptimized(this._SmearedName);
			Writer.WriteOptimized(this._SmearedColor);
			Writer.WriteOptimized(this._StainedName);
			Writer.WriteOptimized(this._StainedColor);
			Writer.Write<string, int>(this.ComponentLiquids);
		}

		// Token: 0x06008D55 RID: 36181 RVA: 0x003244D8 File Offset: 0x003226D8
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			this.Flags = Reader.ReadOptimizedInt32();
			this.MaxVolume = Reader.ReadOptimizedInt32();
			this.Volume = Reader.ReadOptimizedInt32();
			this.StartVolume = Reader.ReadOptimizedString();
			this.NamePreposition = Reader.ReadOptimizedString();
			this.UnknownNamePreposition = Reader.ReadOptimizedString();
			this.AutoCollectLiquidType = Reader.ReadOptimizedString();
			this.Primary = Reader.ReadOptimizedString();
			this.Secondary = Reader.ReadOptimizedString();
			this._SmearedName = Reader.ReadOptimizedString();
			this._SmearedColor = Reader.ReadOptimizedString();
			this._StainedName = Reader.ReadOptimizedString();
			this._StainedColor = Reader.ReadOptimizedString();
			this.ComponentLiquids = Reader.ReadDictionary<string, int>();
		}

		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x06008D56 RID: 36182 RVA: 0x0032458D File Offset: 0x0032278D
		// (set) Token: 0x06008D57 RID: 36183 RVA: 0x0032459A File Offset: 0x0032279A
		[JsonIgnore]
		public bool Flowing
		{
			get
			{
				return (this.Flags & 1) == 1;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 1) : (this.Flags & -2));
			}
		}

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x06008D58 RID: 36184 RVA: 0x003245B8 File Offset: 0x003227B8
		// (set) Token: 0x06008D59 RID: 36185 RVA: 0x003245C5 File Offset: 0x003227C5
		[JsonIgnore]
		public bool Collector
		{
			get
			{
				return (this.Flags & 2) == 2;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 2) : (this.Flags & -3));
			}
		}

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x06008D5A RID: 36186 RVA: 0x003245E3 File Offset: 0x003227E3
		// (set) Token: 0x06008D5B RID: 36187 RVA: 0x003245F0 File Offset: 0x003227F0
		[JsonIgnore]
		public bool Sealed
		{
			get
			{
				return (this.Flags & 4) == 4;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 4) : (this.Flags & -5));
			}
		}

		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x06008D5C RID: 36188 RVA: 0x0032460E File Offset: 0x0032280E
		// (set) Token: 0x06008D5D RID: 36189 RVA: 0x0032461B File Offset: 0x0032281B
		[JsonIgnore]
		public bool ManualSeal
		{
			get
			{
				return (this.Flags & 8) == 8;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 8) : (this.Flags & -9));
			}
		}

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x06008D5E RID: 36190 RVA: 0x00324639 File Offset: 0x00322839
		// (set) Token: 0x06008D5F RID: 36191 RVA: 0x00324648 File Offset: 0x00322848
		[JsonIgnore]
		public bool LiquidVisibleWhenSealed
		{
			get
			{
				return (this.Flags & 16) == 16;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 16) : (this.Flags & -17));
			}
		}

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x06008D60 RID: 36192 RVA: 0x00324667 File Offset: 0x00322867
		// (set) Token: 0x06008D61 RID: 36193 RVA: 0x00324676 File Offset: 0x00322876
		[JsonIgnore]
		public bool ShowSeal
		{
			get
			{
				return (this.Flags & 32) == 32;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 32) : (this.Flags & -33));
			}
		}

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x06008D62 RID: 36194 RVA: 0x00324695 File Offset: 0x00322895
		// (set) Token: 0x06008D63 RID: 36195 RVA: 0x003246A4 File Offset: 0x003228A4
		[JsonIgnore]
		public bool HasDrain
		{
			get
			{
				return (this.Flags & 64) == 64;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 64) : (this.Flags & -65));
			}
		}

		// Token: 0x17000A2A RID: 2602
		// (get) Token: 0x06008D64 RID: 36196 RVA: 0x003246C3 File Offset: 0x003228C3
		[JsonIgnore]
		public override int Priority
		{
			get
			{
				return 90000;
			}
		}

		// Token: 0x06008D65 RID: 36197 RVA: 0x003246CA File Offset: 0x003228CA
		public static bool isValidLiquid(string id)
		{
			return LiquidVolume.Liquids != null && id != null && LiquidVolume.Liquids.ContainsKey(id);
		}

		// Token: 0x06008D66 RID: 36198 RVA: 0x003246E3 File Offset: 0x003228E3
		public static IEnumerable<BaseLiquid> getAllLiquids()
		{
			foreach (BaseLiquid baseLiquid in LiquidVolume.Liquids.Values)
			{
				yield return baseLiquid;
			}
			StringMap<BaseLiquid>.ValueEnumerator valueEnumerator = default(StringMap<BaseLiquid>.ValueEnumerator);
			yield break;
			yield break;
		}

		// Token: 0x06008D67 RID: 36199 RVA: 0x003246EC File Offset: 0x003228EC
		[Obsolete("Use GetLiquid(ReadOnlySpan<char>) - Will not be removed before Q1 2025")]
		public static BaseLiquid getLiquid(string id)
		{
			return LiquidVolume.GetLiquid(id);
		}

		/// <summary>
		///     Gets the corresponding liquid from the <see cref="P:XRL.World.Parts.LiquidVolume.Liquids" /> dictionary.
		/// </summary>
		/// <param name="ID">The liquid string ID</param>
		/// <returns>The BaseLiquid instance</returns>
		// Token: 0x06008D68 RID: 36200 RVA: 0x003246FC File Offset: 0x003228FC
		public static BaseLiquid GetLiquid(ReadOnlySpan<char> ID)
		{
			BaseLiquid baseLiquid;
			if (LiquidVolume.Liquids.TryGetValue(ID, out baseLiquid))
			{
				return baseLiquid;
			}
			return null;
		}

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x06008D69 RID: 36201 RVA: 0x0032471B File Offset: 0x0032291B
		public static StringMap<BaseLiquid> Liquids
		{
			get
			{
				if (LiquidVolume._Liquids == null)
				{
					Loading.LoadTask("Initialize liquids", new Action(LiquidVolume.Init), true);
				}
				return LiquidVolume._Liquids;
			}
		}

		// Token: 0x06008D6A RID: 36202 RVA: 0x00324740 File Offset: 0x00322940
		public LiquidVolume()
		{
		}

		// Token: 0x06008D6B RID: 36203 RVA: 0x00324774 File Offset: 0x00322974
		public LiquidVolume(string Liquid, int Volume)
			: this()
		{
			this.InitialLiquid = Liquid;
			this.Volume = Volume;
		}

		// Token: 0x06008D6C RID: 36204 RVA: 0x0032478A File Offset: 0x0032298A
		public LiquidVolume(string Liquid, int Volume, int MaxVolume)
			: this(Liquid, Volume)
		{
			this.MaxVolume = MaxVolume;
		}

		// Token: 0x06008D6D RID: 36205 RVA: 0x0032479C File Offset: 0x0032299C
		public LiquidVolume(Dictionary<string, int> Amounts)
		{
			foreach (KeyValuePair<string, int> keyValuePair in Amounts)
			{
				this.ComponentLiquids.Add(keyValuePair.Key, keyValuePair.Value);
				this.Volume += keyValuePair.Value;
			}
			this.NormalizeProportions();
		}

		// Token: 0x06008D6E RID: 36206 RVA: 0x00324848 File Offset: 0x00322A48
		public override void Attach()
		{
			this.ParentObject.LiquidVolume = this;
			this.FrameOffset = Stat.RandomCosmetic(0, 60);
		}

		// Token: 0x06008D6F RID: 36207 RVA: 0x00324864 File Offset: 0x00322A64
		public override void Remove()
		{
			GameObject parentObject = this.ParentObject;
			if (((parentObject != null) ? parentObject.LiquidVolume : null) == this)
			{
				this.ParentObject.LiquidVolume = null;
			}
		}

		// Token: 0x06008D70 RID: 36208 RVA: 0x00324888 File Offset: 0x00322A88
		public static GameObject create(List<string> components, int vol = 1000)
		{
			GameObject gameObject = GameObjectFactory.create("WaterPool");
			LiquidVolume liquidVolume = gameObject.LiquidVolume;
			liquidVolume.Empty(true, false);
			for (int i = 0; i < components.Count; i++)
			{
				string text = components[i];
				if (liquidVolume.ComponentLiquids.ContainsKey(text))
				{
					Dictionary<string, int> componentLiquids = liquidVolume.ComponentLiquids;
					string text2 = text;
					componentLiquids[text2] += 1000 / components.Count;
				}
				else
				{
					liquidVolume.ComponentLiquids.Add(text, 1000 / components.Count);
				}
			}
			liquidVolume.Volume = vol;
			liquidVolume.Update();
			return gameObject;
		}

		// Token: 0x06008D71 RID: 36209 RVA: 0x00324928 File Offset: 0x00322B28
		public bool EffectivelySealed()
		{
			return this.Sealed && !base.IsBroken();
		}

		// Token: 0x06008D72 RID: 36210 RVA: 0x0032493D File Offset: 0x00322B3D
		public int GetAdsorbableDrams(GameObject obj)
		{
			return obj.GetMaximumLiquidExposure() * this.GetLiquidAdsorbence() / 100;
		}

		/// <summary>
		/// If the specified object is in contact with this liquid volume,
		/// calculates how much of a specified liquid the object is exposed to,
		/// in millidrams.
		/// </summary>
		/// <param Name="obj">
		/// the object whose exposure we're checking
		/// </param>
		/// <param Name="Liquid">
		/// the ID of the liquid we're checking exposure to
		/// </param>
		/// <returns>
		/// amount of the liquid the object is exposed to, in millidrams (divide
		/// by 1000 to get drams)
		/// </returns>
		// Token: 0x06008D73 RID: 36211 RVA: 0x00324950 File Offset: 0x00322B50
		public int GetLiquidExposureMillidrams(GameObject obj, string Liquid)
		{
			int num;
			if (!this.ComponentLiquids.TryGetValue(Liquid, out num) || num <= 0)
			{
				return 0;
			}
			double num2 = Math.Min(obj.GetMaximumLiquidExposureAsDouble(), (double)this.Volume);
			if (num2 <= 0.0)
			{
				return 0;
			}
			return (int)Math.Round(num2 * (double)num);
		}

		// Token: 0x06008D74 RID: 36212 RVA: 0x003249A0 File Offset: 0x00322BA0
		public string GetPreparedCookingIngredientLiquidDomainPairs()
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			foreach (BaseLiquid baseLiquid in this.GetComponentLiquids())
			{
				string preparedCookingIngredient = baseLiquid.GetPreparedCookingIngredient();
				if (!preparedCookingIngredient.IsNullOrEmpty())
				{
					foreach (string text in preparedCookingIngredient.CachedCommaExpansion())
					{
						if (stringBuilder.Length != 0)
						{
							stringBuilder.Append(',');
						}
						stringBuilder.Append(baseLiquid.ID).Append(':').Append(text);
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06008D75 RID: 36213 RVA: 0x00324A74 File Offset: 0x00322C74
		public bool HasPreparedCookingIngredient()
		{
			if (this.Volume == 0)
			{
				return false;
			}
			using (List<BaseLiquid>.Enumerator enumerator = this.GetComponentLiquids().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.GetPreparedCookingIngredient().IsNullOrEmpty())
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06008D76 RID: 36214 RVA: 0x00324ADC File Offset: 0x00322CDC
		public string GetPreparedCookingIngredient()
		{
			if (this.Volume == 0)
			{
				return "";
			}
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			foreach (BaseLiquid baseLiquid in this.GetComponentLiquids())
			{
				string preparedCookingIngredient = baseLiquid.GetPreparedCookingIngredient();
				if (!preparedCookingIngredient.IsNullOrEmpty())
				{
					if (stringBuilder.Length != 0)
					{
						stringBuilder.Append(',');
					}
					stringBuilder.Append(preparedCookingIngredient);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06008D77 RID: 36215 RVA: 0x00324B6C File Offset: 0x00322D6C
		public List<BaseLiquid> GetComponentLiquids()
		{
			List<BaseLiquid> list = new List<BaseLiquid>(2);
			BaseLiquid primaryLiquid = this.GetPrimaryLiquid();
			if (primaryLiquid != null)
			{
				list.Add(primaryLiquid);
			}
			BaseLiquid secondaryLiquid = this.GetSecondaryLiquid();
			if (secondaryLiquid != null)
			{
				list.Add(secondaryLiquid);
			}
			return list;
		}

		// Token: 0x06008D78 RID: 36216 RVA: 0x00324BA4 File Offset: 0x00322DA4
		public bool HasLiquid(string ID)
		{
			Dictionary<string, int> componentLiquids = this.ComponentLiquids;
			return componentLiquids != null && componentLiquids.Any<KeyValuePair<string, int>>((KeyValuePair<string, int> k) => k.Key == ID && k.Value > 0);
		}

		// Token: 0x06008D79 RID: 36217 RVA: 0x00324BDB File Offset: 0x00322DDB
		public bool HasPrimaryOrSecondaryLiquid(string ID)
		{
			return this.GetPrimaryLiquidID() == ID || this.GetSecondaryLiquidID() == ID;
		}

		// Token: 0x06008D7A RID: 36218 RVA: 0x00324C00 File Offset: 0x00322E00
		public BaseLiquid GetPrimaryLiquid()
		{
			if (this.Volume <= 0)
			{
				return null;
			}
			if (this.ComponentLiquids == null)
			{
				return null;
			}
			if (this.ComponentLiquids.Count <= 0)
			{
				return null;
			}
			if (this.Primary == null)
			{
				this.RecalculatePrimary();
				if (this.Primary == null)
				{
					return null;
				}
			}
			BaseLiquid baseLiquid;
			if (!LiquidVolume.Liquids.TryGetValue(this.Primary, out baseLiquid))
			{
				return null;
			}
			return baseLiquid;
		}

		// Token: 0x06008D7B RID: 36219 RVA: 0x00324C62 File Offset: 0x00322E62
		public string GetPrimaryLiquidID()
		{
			BaseLiquid primaryLiquid = this.GetPrimaryLiquid();
			if (primaryLiquid == null)
			{
				return null;
			}
			return primaryLiquid.ID;
		}

		// Token: 0x06008D7C RID: 36220 RVA: 0x00324C78 File Offset: 0x00322E78
		public BaseLiquid RequirePrimaryLiquid()
		{
			if (this.Primary == null)
			{
				if (this.Volume <= 0)
				{
					throw new Exception("no liquid");
				}
				if (this.ComponentLiquids == null)
				{
					throw new Exception("no component liquid list");
				}
				if (this.ComponentLiquids.Count <= 0)
				{
					throw new Exception("empty component liquid");
				}
				this.RecalculatePrimary();
				if (this.Primary == null)
				{
					throw new Exception("primary liquid cannot be determined");
				}
			}
			BaseLiquid baseLiquid;
			if (!LiquidVolume.Liquids.TryGetValue(this.Primary, out baseLiquid))
			{
				throw new Exception("primary liquid \"" + this.Primary + "\" unknown");
			}
			return baseLiquid;
		}

		// Token: 0x06008D7D RID: 36221 RVA: 0x00324D18 File Offset: 0x00322F18
		public BaseLiquid GetSecondaryLiquid()
		{
			if (this.Volume <= 0)
			{
				return null;
			}
			if (this.ComponentLiquids == null)
			{
				return null;
			}
			if (this.ComponentLiquids.Count <= 1)
			{
				return null;
			}
			if (this.Secondary == null)
			{
				this.RecalculatePrimary();
				if (this.Secondary == null)
				{
					return null;
				}
			}
			BaseLiquid baseLiquid;
			if (!LiquidVolume.Liquids.TryGetValue(this.Secondary, out baseLiquid))
			{
				return null;
			}
			return baseLiquid;
		}

		// Token: 0x06008D7E RID: 36222 RVA: 0x00324D7A File Offset: 0x00322F7A
		public string GetSecondaryLiquidID()
		{
			BaseLiquid secondaryLiquid = this.GetSecondaryLiquid();
			if (secondaryLiquid == null)
			{
				return null;
			}
			return secondaryLiquid.ID;
		}

		// Token: 0x06008D7F RID: 36223 RVA: 0x00324D90 File Offset: 0x00322F90
		public BaseLiquid RequireSecondaryLiquid()
		{
			if (this.Secondary == null)
			{
				if (this.Volume <= 0)
				{
					throw new Exception("no liquid");
				}
				if (this.ComponentLiquids == null)
				{
					throw new Exception("no component liquid list");
				}
				if (this.ComponentLiquids.Count <= 0)
				{
					throw new Exception("empty component liquid");
				}
				this.RecalculatePrimary();
				if (this.Secondary == null)
				{
					throw new Exception("secondary liquid cannot be determined");
				}
			}
			BaseLiquid baseLiquid;
			if (!LiquidVolume.Liquids.TryGetValue(this.Secondary, out baseLiquid))
			{
				throw new Exception("secondary liquid \"" + this.Secondary + "\" unknown");
			}
			return baseLiquid;
		}

		// Token: 0x06008D80 RID: 36224 RVA: 0x00324E30 File Offset: 0x00323030
		public override IPart DeepCopy(GameObject Parent, Func<GameObject, GameObject> MapInv)
		{
			LiquidVolume liquidVolume = base.DeepCopy(Parent, MapInv) as LiquidVolume;
			liquidVolume.ComponentLiquids = new Dictionary<string, int>(this.ComponentLiquids.Count);
			foreach (KeyValuePair<string, int> keyValuePair in this.ComponentLiquids)
			{
				liquidVolume.ComponentLiquids.Add(keyValuePair.Key, keyValuePair.Value);
			}
			return liquidVolume;
		}

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x06008D81 RID: 36225 RVA: 0x00324EBC File Offset: 0x003230BC
		public string SmearedName
		{
			get
			{
				if (this._SmearedName == null)
				{
					this.FindSmear();
				}
				return this._SmearedName;
			}
		}

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x06008D82 RID: 36226 RVA: 0x00324ED2 File Offset: 0x003230D2
		public string SmearedColor
		{
			get
			{
				if (this._SmearedColor == null && this._SmearedName == null)
				{
					this.FindSmear();
				}
				return this._SmearedColor;
			}
		}

		// Token: 0x06008D83 RID: 36227 RVA: 0x00324EF0 File Offset: 0x003230F0
		private void FindSmear()
		{
			if (this.ComponentLiquids.Count == 1)
			{
				string text = null;
				if (this.Primary == null)
				{
					using (List<string>.Enumerator enumerator = this.GetTertiaries().GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string text2 = enumerator.Current;
							BaseLiquid liquid = LiquidVolume.GetLiquid(text2);
							text = liquid.GetSmearedName(this);
							if (!text.IsNullOrEmpty())
							{
								this._SmearedColor = liquid.GetColor();
								break;
							}
						}
						goto IL_008D;
					}
				}
				BaseLiquid baseLiquid = this.RequirePrimaryLiquid();
				text = baseLiquid.GetSmearedName(this);
				this._SmearedColor = baseLiquid.GetColor();
				IL_008D:
				this._SmearedName = text ?? "liquid-covered";
				return;
			}
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			if (this.Secondary != null)
			{
				BaseLiquid baseLiquid2 = this.RequireSecondaryLiquid();
				string smearedAdjective = baseLiquid2.GetSmearedAdjective(this);
				if (!smearedAdjective.IsNullOrEmpty())
				{
					stringBuilder.Compound(smearedAdjective);
					this._SmearedColor = baseLiquid2.GetColor();
				}
			}
			List<string> tertiaries = this.GetTertiaries();
			if (tertiaries != null)
			{
				foreach (string text3 in tertiaries)
				{
					BaseLiquid liquid2 = LiquidVolume.GetLiquid(text3);
					string smearedAdjective2 = liquid2.GetSmearedAdjective(this);
					if (!smearedAdjective2.IsNullOrEmpty())
					{
						stringBuilder.Compound(smearedAdjective2);
						if (this._SmearedColor == null)
						{
							this._SmearedColor = liquid2.GetColor();
						}
					}
				}
			}
			if (this.Primary != null)
			{
				BaseLiquid baseLiquid3 = this.RequirePrimaryLiquid();
				string smearedName = this.RequirePrimaryLiquid().GetSmearedName(this);
				if (!smearedName.IsNullOrEmpty())
				{
					stringBuilder.Compound(smearedName);
					this._SmearedColor = baseLiquid3.GetColor() ?? this._SmearedColor;
				}
			}
			this._SmearedName = ((stringBuilder.Length > 0) ? stringBuilder.ToString() : "liquid-covered");
		}

		// Token: 0x06008D84 RID: 36228 RVA: 0x003250D4 File Offset: 0x003232D4
		public void ProcessSmear(GetDisplayNameEvent E)
		{
			if (E.Visible && !E.Reference)
			{
				E.AddAdjective(this.SmearedName, -25);
				if (!this._SmearedColor.IsNullOrEmpty())
				{
					E.AddColor(this._SmearedColor, 22);
				}
			}
		}

		// Token: 0x17000A2E RID: 2606
		// (get) Token: 0x06008D85 RID: 36229 RVA: 0x0032510F File Offset: 0x0032330F
		public string StainedName
		{
			get
			{
				if (this._StainedName == null)
				{
					this.FindStain();
				}
				return this._StainedName;
			}
		}

		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x06008D86 RID: 36230 RVA: 0x00325125 File Offset: 0x00323325
		public string StainedColor
		{
			get
			{
				if (this._StainedColor == null && this._StainedName == null)
				{
					this.FindStain();
				}
				return this._StainedColor;
			}
		}

		// Token: 0x06008D87 RID: 36231 RVA: 0x00325144 File Offset: 0x00323344
		private void FindStain()
		{
			if (this.ComponentLiquids.Count == 1)
			{
				string text = null;
				if (this.Primary == null)
				{
					using (List<string>.Enumerator enumerator = this.GetTertiaries().GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string text2 = enumerator.Current;
							BaseLiquid liquid = LiquidVolume.GetLiquid(text2);
							text = liquid.GetStainedName(this);
							if (!text.IsNullOrEmpty())
							{
								this._StainedColor = liquid.GetColor();
								break;
							}
						}
						goto IL_008D;
					}
				}
				BaseLiquid baseLiquid = this.RequirePrimaryLiquid();
				text = baseLiquid.GetStainedName(this);
				this._StainedColor = baseLiquid.GetColor();
				IL_008D:
				if (text != null)
				{
					this._StainedName = text + "-stained";
				}
			}
			else
			{
				StringBuilder stringBuilder = Event.NewStringBuilder(null);
				if (this.Primary != null)
				{
					BaseLiquid baseLiquid2 = this.RequirePrimaryLiquid();
					BaseLiquid baseLiquid3 = ((this.Secondary != null) ? this.RequireSecondaryLiquid() : null);
					this._StainedColor = baseLiquid2.GetColor() ?? ((baseLiquid3 != null) ? baseLiquid3.GetColor() : null);
					string stainedName = baseLiquid2.GetStainedName(this);
					if (!stainedName.IsNullOrEmpty())
					{
						stringBuilder.Append(stainedName);
					}
					if (baseLiquid3 != null)
					{
						string stainedName2 = baseLiquid3.GetStainedName(this);
						if (!stainedName2.IsNullOrEmpty())
						{
							if (!stainedName.IsNullOrEmpty())
							{
								stringBuilder.Append("-and-");
							}
							stringBuilder.Append(stainedName2);
						}
					}
				}
				if (stringBuilder.Length <= 0)
				{
					foreach (string text3 in this.GetTertiaries())
					{
						BaseLiquid liquid2 = LiquidVolume.GetLiquid(text3);
						string stainedName3 = liquid2.GetStainedName(this);
						if (stainedName3 != null)
						{
							this._StainedColor = liquid2.GetColor();
							stringBuilder.Append(stainedName3);
							break;
						}
					}
				}
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append("-stained");
					this._StainedName = stringBuilder.ToString();
				}
			}
			if (this._StainedName == null)
			{
				this._StainedName = "stained";
			}
		}

		// Token: 0x06008D88 RID: 36232 RVA: 0x0032535C File Offset: 0x0032355C
		public void ProcessStain(GetDisplayNameEvent E)
		{
			if (E.Visible && !E.Reference)
			{
				E.AddAdjective(this.StainedName, -20);
				if (!this._StainedColor.IsNullOrEmpty())
				{
					E.AddColor(this._StainedColor, 20);
				}
			}
		}

		// Token: 0x06008D89 RID: 36233 RVA: 0x00325398 File Offset: 0x00323598
		public int Proportion(string Liquid)
		{
			int num;
			if (this.ComponentLiquids.TryGetValue(Liquid, out num))
			{
				return num;
			}
			return 0;
		}

		// Token: 0x06008D8A RID: 36234 RVA: 0x003253B8 File Offset: 0x003235B8
		public int Proportion(string Liquid1, string Liquid2)
		{
			return this.Proportion(Liquid1) + this.Proportion(Liquid2);
		}

		// Token: 0x06008D8B RID: 36235 RVA: 0x003253C9 File Offset: 0x003235C9
		public int Proportion(string Liquid1, string Liquid2, string Liquid3)
		{
			return this.Proportion(Liquid1) + this.Proportion(Liquid2) + this.Proportion(Liquid3);
		}

		// Token: 0x06008D8C RID: 36236 RVA: 0x003253E2 File Offset: 0x003235E2
		public int Proportion(string Liquid1, string Liquid2, string Liquid3, string Liquid4)
		{
			return this.Proportion(Liquid1) + this.Proportion(Liquid2) + this.Proportion(Liquid3) + this.Proportion(Liquid4);
		}

		// Token: 0x06008D8D RID: 36237 RVA: 0x00325404 File Offset: 0x00323604
		public int Proportion(string Liquid1, string Liquid2, string Liquid3, string Liquid4, string Liquid5)
		{
			return this.Proportion(Liquid1) + this.Proportion(Liquid2) + this.Proportion(Liquid3) + this.Proportion(Liquid4) + this.Proportion(Liquid5);
		}

		// Token: 0x06008D8E RID: 36238 RVA: 0x0032542F File Offset: 0x0032362F
		public int Amount(string Liquid)
		{
			return this.Volume * this.Proportion(Liquid) / 1000;
		}

		// Token: 0x06008D8F RID: 36239 RVA: 0x00325445 File Offset: 0x00323645
		public int Amount(string Liquid1, string Liquid2)
		{
			return this.Volume * this.Proportion(Liquid1, Liquid2) / 1000;
		}

		// Token: 0x06008D90 RID: 36240 RVA: 0x0032545C File Offset: 0x0032365C
		public int Amount(string Liquid1, string Liquid2, string Liquid3)
		{
			return this.Volume * this.Proportion(Liquid1, Liquid2, Liquid3) / 1000;
		}

		// Token: 0x06008D91 RID: 36241 RVA: 0x00325474 File Offset: 0x00323674
		public int Amount(string Liquid1, string Liquid2, string Liquid3, string Liquid4)
		{
			return this.Volume * this.Proportion(Liquid1, Liquid2, Liquid3, Liquid4) / 1000;
		}

		// Token: 0x06008D92 RID: 36242 RVA: 0x0032548E File Offset: 0x0032368E
		public int Amount(string Liquid1, string Liquid2, string Liquid3, string Liquid4, string Liquid5)
		{
			return this.Volume * this.Proportion(Liquid1, Liquid2, Liquid3, Liquid4, Liquid5) / 1000;
		}

		// Token: 0x06008D93 RID: 36243 RVA: 0x003254AA File Offset: 0x003236AA
		public int UpperAmount(string Liquid)
		{
			return (int)Math.Ceiling((double)(this.Volume * this.Proportion(Liquid)) / 1000.0);
		}

		// Token: 0x06008D94 RID: 36244 RVA: 0x003254CB File Offset: 0x003236CB
		public int UpperAmount(string Liquid1, string Liquid2)
		{
			return (int)Math.Ceiling((double)(this.Volume * this.Proportion(Liquid1, Liquid2)) / 1000.0);
		}

		// Token: 0x06008D95 RID: 36245 RVA: 0x003254ED File Offset: 0x003236ED
		public int UpperAmount(string Liquid1, string Liquid2, string Liquid3)
		{
			return (int)Math.Ceiling((double)(this.Volume * this.Proportion(Liquid1, Liquid2, Liquid3)) / 1000.0);
		}

		// Token: 0x06008D96 RID: 36246 RVA: 0x00325510 File Offset: 0x00323710
		public int UpperAmount(string Liquid1, string Liquid2, string Liquid3, string Liquid4)
		{
			return (int)Math.Ceiling((double)(this.Volume * this.Proportion(Liquid1, Liquid2, Liquid3, Liquid4)) / 1000.0);
		}

		// Token: 0x06008D97 RID: 36247 RVA: 0x00325535 File Offset: 0x00323735
		public int UpperAmount(string Liquid1, string Liquid2, string Liquid3, string Liquid4, string Liquid5)
		{
			return (int)Math.Ceiling((double)(this.Volume * this.Proportion(Liquid1, Liquid2, Liquid3, Liquid4, Liquid5)) / 1000.0);
		}

		// Token: 0x06008D98 RID: 36248 RVA: 0x0032555C File Offset: 0x0032375C
		public int MilliAmount(string Liquid)
		{
			return this.Volume * this.Proportion(Liquid);
		}

		// Token: 0x06008D99 RID: 36249 RVA: 0x0032556C File Offset: 0x0032376C
		public int MilliAmount(string Liquid1, string Liquid2)
		{
			return this.Volume * this.Proportion(Liquid1, Liquid2);
		}

		// Token: 0x06008D9A RID: 36250 RVA: 0x0032557D File Offset: 0x0032377D
		public int MilliAmount(string Liquid1, string Liquid2, string Liquid3)
		{
			return this.Volume * this.Proportion(Liquid1, Liquid2, Liquid3);
		}

		// Token: 0x06008D9B RID: 36251 RVA: 0x0032558F File Offset: 0x0032378F
		public int MilliAmount(string Liquid1, string Liquid2, string Liquid3, string Liquid4)
		{
			return this.Volume * this.Proportion(Liquid1, Liquid2, Liquid3, Liquid4);
		}

		// Token: 0x06008D9C RID: 36252 RVA: 0x003255A3 File Offset: 0x003237A3
		public int MilliAmount(string Liquid1, string Liquid2, string Liquid3, string Liquid4, string Liquid5)
		{
			return this.Volume * this.Proportion(Liquid1, Liquid2, Liquid3, Liquid4, Liquid5);
		}

		// Token: 0x06008D9D RID: 36253 RVA: 0x003255B9 File Offset: 0x003237B9
		public bool IsMixed()
		{
			return this.ComponentLiquids.Count > 1;
		}

		// Token: 0x06008D9E RID: 36254 RVA: 0x003255C9 File Offset: 0x003237C9
		public bool IsPure()
		{
			return this.ComponentLiquids.Count <= 1;
		}

		// Token: 0x06008D9F RID: 36255 RVA: 0x003255DC File Offset: 0x003237DC
		public bool RecalculatePrimary()
		{
			this._SmearedName = null;
			this._SmearedColor = null;
			this._StainedName = null;
			this._StainedColor = null;
			string primary = this.Primary;
			string secondary = this.Secondary;
			if (this.ComponentLiquids.Count == 0)
			{
				this.Primary = null;
				this.Secondary = null;
			}
			else if (this.ComponentLiquids.Count == 1)
			{
				if (this.Primary != null && this.ComponentLiquids.ContainsKey(this.Primary))
				{
					return false;
				}
				this.Primary = this.ComponentLiquids.First<KeyValuePair<string, int>>().Key;
				this.Secondary = null;
			}
			else
			{
				LiquidVolume.SortedComponents.Clear();
				foreach (KeyValuePair<string, int> keyValuePair in this.ComponentLiquids)
				{
					int num = LiquidVolume.SortedComponents.BinarySearch(keyValuePair, LiquidVolume.ComponentSorter.Instance);
					LiquidVolume.SortedComponents.Insert((num < 0) ? (~num) : num, keyValuePair);
				}
				this.Primary = LiquidVolume.SortedComponents[0].Key;
				this.Secondary = LiquidVolume.SortedComponents[1].Key;
				if (this.Primary != "blood" && this.ComponentLiquids.ContainsKey("blood"))
				{
					this.Secondary = "blood";
				}
				else if (this.Primary == "warmstatic" && this.Secondary == "water")
				{
					this.Secondary = ((this.ComponentLiquids.Count == 2) ? null : LiquidVolume.SortedComponents[2].Key);
				}
			}
			if (this.Primary != primary || this.Secondary != secondary)
			{
				if (this.IsOpenVolume())
				{
					this.BaseRender();
				}
				return true;
			}
			return false;
		}

		// Token: 0x06008DA0 RID: 36256 RVA: 0x003257D8 File Offset: 0x003239D8
		public static void Init()
		{
			LiquidVolume._Liquids = new StringMap<BaseLiquid>();
			foreach (Type type in ModManager.GetTypesWithAttribute(typeof(IsLiquid), true))
			{
				try
				{
					BaseLiquid baseLiquid = Activator.CreateInstance(type) as BaseLiquid;
					if (baseLiquid == null)
					{
						NLogger gameLog = Logger.gameLog;
						string text = "couldn't instantiate ";
						Type type2 = type;
						gameLog.Error(text + ((type2 != null) ? type2.ToString() : null) + " or it is not derived from BaseLiquid.", Array.Empty<object>());
					}
					else
					{
						LiquidVolume._Liquids[baseLiquid.ID] = baseLiquid;
					}
				}
				catch (Exception ex)
				{
					Logger.Exception("Initializing liquid type " + type.Name, ex);
				}
			}
		}

		// Token: 0x06008DA1 RID: 36257 RVA: 0x003258AC File Offset: 0x00323AAC
		public static List<string> GetLiquidColors(string liquid)
		{
			return LiquidVolume.Liquids[liquid].GetColors();
		}

		// Token: 0x17000A30 RID: 2608
		// (set) Token: 0x06008DA2 RID: 36258 RVA: 0x003258C0 File Offset: 0x00323AC0
		public string InitialLiquid
		{
			set
			{
				if (value == "")
				{
					return;
				}
				this.ComponentLiquids.Clear();
				string text = (value.Contains(";") ? value.Split(';', StringSplitOptions.None).GetRandomElement<string>(null) : value);
				if (text.Contains(","))
				{
					foreach (string text2 in text.Split(',', StringSplitOptions.None))
					{
						try
						{
							this.ProcessInitialLiquid(text2);
						}
						catch (Exception ex)
						{
							MetricsManager.LogError("InitialLiquid " + text, ex);
						}
					}
				}
				else
				{
					this.ProcessInitialLiquid(text);
				}
				this.NormalizeProportions();
				this.RecalculatePrimary();
				GameObject parentObject = this.ParentObject;
				if (((parentObject != null) ? parentObject.CurrentCell : null) != null)
				{
					this.CheckImage();
				}
			}
		}

		// Token: 0x06008DA3 RID: 36259 RVA: 0x00325990 File Offset: 0x00323B90
		private void ProcessInitialLiquid(string Spec)
		{
			try
			{
				if (Spec.Contains('-'))
				{
					string[] array = Spec.Split('-', StringSplitOptions.None);
					string text = array[0];
					int num = Convert.ToInt32(array[1]);
					this.ComponentLiquids.Add(text, num);
				}
				else
				{
					string id = LiquidVolume.Liquids[Spec].ID;
					this.ComponentLiquids.Add(id, 1000);
				}
			}
			catch
			{
				MetricsManager.LogError("invalid initial liquid specification " + (Spec ?? "NULL"));
			}
		}

		// Token: 0x06008DA4 RID: 36260 RVA: 0x00325A1C File Offset: 0x00323C1C
		public bool IsEmpty()
		{
			return this.ComponentLiquids.Count == 0 || this.Volume == 0;
		}

		// Token: 0x06008DA5 RID: 36261 RVA: 0x00325A38 File Offset: 0x00323C38
		public int GetNavigationWeight(GameObject Object, bool Smart, bool Slimewalking, bool FilthAffinity, ref bool Uncacheable)
		{
			int num = 0;
			foreach (string text in this.ComponentLiquids.Keys)
			{
				BaseLiquid baseLiquid = LiquidVolume.Liquids[text];
				int num2 = baseLiquid.GetNavigationWeight(this, Object, Smart, Slimewalking, FilthAffinity, ref Uncacheable);
				if (num2 < 2 && baseLiquid.InterruptAutowalk)
				{
					num2 = 2;
				}
				if (num2 > num)
				{
					num = num2;
				}
			}
			return num;
		}

		// Token: 0x06008DA6 RID: 36262 RVA: 0x00325AC0 File Offset: 0x00323CC0
		private void TrackAsLiquid(string Liquid)
		{
			if (Liquid.IndexOf(',') != -1)
			{
				this.ComponentLiquids.Clear();
				int num = 0;
				foreach (string text in Liquid.CachedCommaExpansion())
				{
					string[] array = text.Split('-', StringSplitOptions.None);
					if (array.Length != 2)
					{
						MetricsManager.LogWarning("Invalid liquid specification: " + text);
						return;
					}
					string text2 = array[0];
					string text3 = array[1];
					int num2;
					try
					{
						num2 = Convert.ToInt32(text3);
					}
					catch
					{
						return;
					}
					if (!LiquidVolume.Liquids.ContainsKey(text2))
					{
						MetricsManager.LogWarning("Unknown liquid type: " + text2);
						return;
					}
					string id = LiquidVolume.Liquids[text2].ID;
					this.ComponentLiquids.Add(id, num2);
					num += num2;
				}
				this.NormalizeProportions();
				return;
			}
			if (this.ComponentLiquids.Count > 1 || !this.ComponentLiquids.ContainsKey(Liquid))
			{
				this.ComponentLiquids.Clear();
				this.ComponentLiquids.Add(Liquid, 1000);
				return;
			}
			this.ComponentLiquids[Liquid] = 1000;
		}

		// Token: 0x06008DA7 RID: 36263 RVA: 0x00325C14 File Offset: 0x00323E14
		public bool IsPureLiquid(bool AllowEmpty = false)
		{
			if (this.Volume == 0 || this.ComponentLiquids == null)
			{
				return AllowEmpty;
			}
			return this.ComponentLiquids.Count == 1;
		}

		// Token: 0x06008DA8 RID: 36264 RVA: 0x00325C38 File Offset: 0x00323E38
		public bool IsPureLiquid(string LiquidType, bool AllowEmpty = false)
		{
			if (LiquidType.IsNullOrEmpty())
			{
				return false;
			}
			if (this.Volume == 0 || this.ComponentLiquids == null)
			{
				return AllowEmpty;
			}
			if (LiquidType.IndexOf(',') == -1)
			{
				if (this.ComponentLiquids.Count != 1)
				{
					return false;
				}
				if (LiquidType.EndsWith("-1000"))
				{
					LiquidType = LiquidType.Substring(0, LiquidType.LastIndexOf('-'));
				}
				return this.ComponentLiquids.ContainsKey(LiquidType);
			}
			else
			{
				if (this.ComponentLiquids.Count < 2)
				{
					return false;
				}
				string[] array = LiquidType.Split(',', StringSplitOptions.None);
				if (this.ComponentLiquids.Count != array.Length)
				{
					return false;
				}
				string[] array2 = array;
				int i = 0;
				while (i < array2.Length)
				{
					string text = array2[i];
					string[] array3 = text.Split('-', StringSplitOptions.None);
					if (array3.Length != 2)
					{
						MetricsManager.LogWarning("Invalid liquid specification: " + text);
						return false;
					}
					string text2 = array3[0];
					string text3 = array3[1];
					int num;
					try
					{
						num = Convert.ToInt32(text3);
					}
					catch
					{
						return false;
					}
					if (!LiquidVolume.isValidLiquid(text2))
					{
						MetricsManager.LogWarning("Unknown liquid type: " + text2);
						return false;
					}
					int num2;
					if (!this.ComponentLiquids.TryGetValue(text2, out num2))
					{
						return false;
					}
					if (num2 != num)
					{
						return false;
					}
					i++;
					continue;
				}
				return true;
			}
		}

		// Token: 0x06008DA9 RID: 36265 RVA: 0x00325D80 File Offset: 0x00323F80
		public bool ContainsLiquid(string LiquidID)
		{
			return this.ComponentLiquids.ContainsKey(LiquidID);
		}

		// Token: 0x06008DAA RID: 36266 RVA: 0x00325D90 File Offset: 0x00323F90
		public bool ContainsSignificantLiquid(string LiquidID)
		{
			if (this.Volume < 1)
			{
				return false;
			}
			if (!this.ComponentLiquids.ContainsKey(LiquidID))
			{
				return false;
			}
			if (this.ComponentLiquids.Count == 1)
			{
				return true;
			}
			int num = this.ComponentLiquids[LiquidID];
			return this.Volume * num / 1000 >= 1;
		}

		// Token: 0x06008DAB RID: 36267 RVA: 0x00325DE9 File Offset: 0x00323FE9
		public bool IsWaste()
		{
			return this.ContainsSignificantLiquid("ooze") || this.ContainsSignificantLiquid("sludge") || this.ContainsSignificantLiquid("goo");
		}

		// Token: 0x06008DAC RID: 36268 RVA: 0x00325E12 File Offset: 0x00324012
		public bool IsWater(bool AllowEmpty = false)
		{
			return this.ContainsSignificantLiquid("water");
		}

		// Token: 0x06008DAD RID: 36269 RVA: 0x00325E1F File Offset: 0x0032401F
		public bool IsFreshWater(bool AllowEmpty = false)
		{
			return this.IsPureLiquid("water", AllowEmpty);
		}

		// Token: 0x06008DAE RID: 36270 RVA: 0x00325E2D File Offset: 0x0032402D
		public bool IsFreshWaterFast()
		{
			return this.Volume > 0 && this.ComponentLiquids.Count == 1 && this.Primary == "water";
		}

		// Token: 0x06008DAF RID: 36271 RVA: 0x00325E58 File Offset: 0x00324058
		public bool MixWith(LiquidVolume Liquid, ref bool RequestInterfaceExit, GameObject PouredFrom = null, GameObject PouredBy = null, int? Amount = null, bool UseTempSplit = false)
		{
			int volume = this.Volume;
			int num = Amount ?? Liquid.Volume;
			if (this.ParentObject != null && this.ParentObject.HasRegisteredEvent("LiquidMixing"))
			{
				Event @event = Event.New("LiquidMixing", 0, 0, 0);
				@event.SetParameter("TargetVolume", this);
				@event.SetParameter("MixingVolume", Liquid);
				@event.SetParameter("Amount", num);
				@event.SetParameter("PouredFrom", PouredFrom);
				@event.SetParameter("PouredTo", this.ParentObject);
				@event.SetParameter("PouredBy", PouredBy);
				if (!this.ParentObject.FireEvent(@event))
				{
					return false;
				}
			}
			LiquidVolume.IncomingLiquidTypes.Clear();
			LiquidVolume.IncomingLiquidTypes.AddRange(Liquid.ComponentLiquids.Keys);
			LiquidVolume.CombinedLiquidTypes.Clear();
			foreach (string text in Liquid.ComponentLiquids.Keys)
			{
				if (!LiquidVolume.Liquids[text].MixingWith(Liquid, this, num, PouredFrom, this.ParentObject, PouredBy, ref RequestInterfaceExit))
				{
					return false;
				}
				if (!LiquidVolume.CombinedLiquidTypes.Contains(text))
				{
					LiquidVolume.CombinedLiquidTypes.Add(text);
				}
			}
			foreach (string text2 in this.ComponentLiquids.Keys)
			{
				if (!LiquidVolume.Liquids[text2].MixingWith(Liquid, this, num, PouredFrom, this.ParentObject, PouredBy, ref RequestInterfaceExit))
				{
					return false;
				}
				if (!LiquidVolume.CombinedLiquidTypes.Contains(text2))
				{
					LiquidVolume.CombinedLiquidTypes.Add(text2);
				}
			}
			if (Amount != null)
			{
				Liquid = (UseTempSplit ? Liquid.TempSplit(Amount.Value) : Liquid.Split(Amount.Value));
			}
			int num2 = int.MinValue;
			string text3 = null;
			foreach (string text4 in LiquidVolume.CombinedLiquidTypes)
			{
				double num3 = (double)(this.ComponentLiquids.ContainsKey(text4) ? this.ComponentLiquids[text4] : 0);
				int num4 = (Liquid.ComponentLiquids.ContainsKey(text4) ? Liquid.ComponentLiquids[text4] : 0);
				int num5 = (int)Math.Floor((num3 * (double)volume + (double)(num4 * num)) / (double)(volume + num));
				if (num5 > num2 || (num5 == num2 && (text3 == null || text3.CompareTo(text4) < 0)))
				{
					num2 = num5;
					text3 = text4;
				}
				if (num5 < 1 && num > 0 && num4 > 0)
				{
					num5 = 1;
				}
				if (num5 > 0)
				{
					this.ComponentLiquids[text4] = num5;
				}
				else
				{
					this.ComponentLiquids.Remove(text4);
				}
			}
			this.Volume += num;
			if (this.MaxVolume >= 0 && this.Volume > this.MaxVolume)
			{
				this.Volume = this.MaxVolume;
			}
			if (this.Volume > 0 && this.ComponentLiquids.Count == 0 && text3 != null)
			{
				this.ComponentLiquids.Add(text3, 1000);
			}
			if (num > 0 && this.ParentObject != null && !this.IsOpenVolume())
			{
				foreach (string text5 in LiquidVolume.IncomingLiquidTypes)
				{
					LiquidVolume.Liquids[text5].FillingContainer(this.ParentObject, this);
				}
			}
			foreach (string text6 in new List<string>(LiquidVolume.CombinedLiquidTypes))
			{
				LiquidVolume.Liquids[text6].MixedWith(Liquid, this, num, PouredFrom, this.ParentObject, PouredBy, ref RequestInterfaceExit);
			}
			this.Update();
			if (this.ParentObject != null)
			{
				LiquidMixedEvent.Send(this);
			}
			return true;
		}

		// Token: 0x06008DB0 RID: 36272 RVA: 0x003262AC File Offset: 0x003244AC
		public bool MixWith(LiquidVolume Liquid, GameObject PouredFrom = null, GameObject PouredBy = null, int? Amount = null, bool UseTempSplit = false)
		{
			bool flag = false;
			return this.MixWith(Liquid, ref flag, PouredFrom, PouredBy, Amount, UseTempSplit);
		}

		// Token: 0x06008DB1 RID: 36273 RVA: 0x003262CC File Offset: 0x003244CC
		public void NormalizeProportions()
		{
			if (this.ComponentLiquids.Count == 0)
			{
				return;
			}
			int num = 0;
			foreach (int num2 in this.ComponentLiquids.Values)
			{
				num += num2;
			}
			if (num == 1000)
			{
				return;
			}
			if (this.ComponentLiquids.Count > 1)
			{
				int num3 = 1000 - num;
				if (num3 > 1 || num3 < -1)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(this.ComponentLiquids.Count);
					foreach (KeyValuePair<string, int> keyValuePair in this.ComponentLiquids)
					{
						int num4 = num3 * keyValuePair.Value / num;
						if (num4 != 0)
						{
							dictionary[keyValuePair.Key] = num4;
						}
					}
					foreach (KeyValuePair<string, int> keyValuePair2 in dictionary)
					{
						Dictionary<string, int> componentLiquids = this.ComponentLiquids;
						string text = keyValuePair2.Key;
						componentLiquids[text] += keyValuePair2.Value;
						num3 -= keyValuePair2.Value;
					}
				}
				if (num3 == 1 || num3 == -1)
				{
					int num5 = int.MinValue;
					string text2 = null;
					foreach (KeyValuePair<string, int> keyValuePair3 in this.ComponentLiquids)
					{
						if (keyValuePair3.Value > num5)
						{
							text2 = keyValuePair3.Key;
							num5 = keyValuePair3.Value;
						}
					}
					if (text2 != null)
					{
						if (num3 > 0)
						{
							Dictionary<string, int> componentLiquids2 = this.ComponentLiquids;
							string text = text2;
							int num6 = componentLiquids2[text];
							componentLiquids2[text] = num6 + 1;
							num3--;
						}
						else
						{
							Dictionary<string, int> componentLiquids3 = this.ComponentLiquids;
							string text = text2;
							int num6 = componentLiquids3[text];
							componentLiquids3[text] = num6 - 1;
							num3++;
						}
					}
				}
				else if (num3 != 0)
				{
					List<KeyValuePair<string, int>> list = this.ComponentLiquids.ToList<KeyValuePair<string, int>>();
					list.Sort((KeyValuePair<string, int> a, KeyValuePair<string, int> b) => b.Value.CompareTo(a.Value));
					while (num3 != 0)
					{
						foreach (KeyValuePair<string, int> keyValuePair4 in list)
						{
							if (num3 > 0)
							{
								Dictionary<string, int> componentLiquids4 = this.ComponentLiquids;
								string text = keyValuePair4.Key;
								int num6 = componentLiquids4[text];
								componentLiquids4[text] = num6 + 1;
								num3--;
							}
							else
							{
								Dictionary<string, int> componentLiquids5 = this.ComponentLiquids;
								string text = keyValuePair4.Key;
								int num6 = componentLiquids5[text];
								componentLiquids5[text] = num6 - 1;
								num3++;
							}
							if (num3 == 0)
							{
								break;
							}
						}
					}
				}
			}
			else
			{
				string text3 = null;
				using (Dictionary<string, int>.KeyCollection.Enumerator enumerator4 = this.ComponentLiquids.Keys.GetEnumerator())
				{
					if (enumerator4.MoveNext())
					{
						text3 = enumerator4.Current;
					}
				}
				if (text3 != null)
				{
					this.ComponentLiquids[text3] = 1000;
				}
			}
			base.FlushWeightCaches();
		}

		// Token: 0x06008DB2 RID: 36274 RVA: 0x00326644 File Offset: 0x00324844
		public string GetLiquidDesignation(bool hyphenateSoloLiquids = false)
		{
			if (this.ComponentLiquids.Count == 0)
			{
				return null;
			}
			if (this.ComponentLiquids.Count == 1)
			{
				using (Dictionary<string, int>.KeyCollection.Enumerator enumerator = this.ComponentLiquids.Keys.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						string text = enumerator.Current;
						if (hyphenateSoloLiquids)
						{
							return text + "-1000";
						}
						return text;
					}
				}
			}
			string[] array = new string[this.ComponentLiquids.Count];
			int num = 0;
			foreach (KeyValuePair<string, int> keyValuePair in this.ComponentLiquids)
			{
				array[num++] = keyValuePair.Key + "-" + keyValuePair.Value.ToString();
			}
			return string.Join(",", array);
		}

		// Token: 0x06008DB3 RID: 36275 RVA: 0x00326758 File Offset: 0x00324958
		public string GetLiquidDebugDesignation()
		{
			string text = this.GetLiquidDesignation(false) ?? "nothing";
			return string.Concat(new string[]
			{
				this.Volume.ToString(),
				"/",
				this.MaxVolume.ToString(),
				"(",
				text,
				")"
			});
		}

		// Token: 0x06008DB4 RID: 36276 RVA: 0x003267BC File Offset: 0x003249BC
		public void SetComponent(string b, int value)
		{
			if (value == 0)
			{
				if (!this.ComponentLiquids.ContainsKey(b))
				{
					goto IL_0174;
				}
				int num = this.ComponentLiquids[b];
				this.ComponentLiquids.Remove(b);
				using (StringMap<BaseLiquid>.Enumerator enumerator = LiquidVolume.Liquids.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						KeyValuePair<string, BaseLiquid> keyValuePair = enumerator.Current;
						string text;
						BaseLiquid baseLiquid;
						keyValuePair.Deconstruct(out text, out baseLiquid);
						string text2 = text;
						if (text2 != b && this.ComponentLiquids.ContainsKey(text2))
						{
							Dictionary<string, int> componentLiquids = this.ComponentLiquids;
							text = text2;
							componentLiquids[text] += (int)((float)num * ((float)this.ComponentLiquids[text2] / 1000f));
						}
					}
					goto IL_0174;
				}
			}
			int num2;
			if (this.ComponentLiquids.ContainsKey(b))
			{
				num2 = 1000 - (value - this.ComponentLiquids[b]);
			}
			else
			{
				num2 = 1000 - value;
			}
			this.ComponentLiquids[b] = value;
			foreach (KeyValuePair<string, BaseLiquid> keyValuePair in LiquidVolume.Liquids)
			{
				KeyValuePair<string, BaseLiquid> keyValuePair;
				string text;
				BaseLiquid baseLiquid;
				keyValuePair.Deconstruct(out text, out baseLiquid);
				string text3 = text;
				if (text3 != b && this.ComponentLiquids.ContainsKey(text3))
				{
					this.ComponentLiquids[text3] = (int)((float)num2 * ((float)this.ComponentLiquids[text3] / 1000f));
				}
			}
			IL_0174:
			this.Update();
		}

		// Token: 0x06008DB5 RID: 36277 RVA: 0x00326960 File Offset: 0x00324B60
		public void Update()
		{
			if (this.Volume <= 0)
			{
				this.Empty(false, false);
				return;
			}
			this.NormalizeProportions();
			this.RecalculatePrimary();
			this.RecalculateProperties();
		}

		// Token: 0x06008DB6 RID: 36278 RVA: 0x00326987 File Offset: 0x00324B87
		public void RecalculateProperties()
		{
			if (this.ParentObject != null)
			{
				this.SyncPhysicalProperties();
				this.CheckImage();
				base.FlushWeightCaches();
			}
		}

		// Token: 0x06008DB7 RID: 36279 RVA: 0x003269A4 File Offset: 0x00324BA4
		public void SyncPhysicalProperties()
		{
			GameObject parentObject = this.ParentObject;
			if (((parentObject != null) ? parentObject.Physics : null) != null && this.IsOpenVolume())
			{
				int num;
				this.GetLiquidPhysicalProperties(out this.ParentObject.Physics.FlameTemperature, out this.ParentObject.Physics.VaporTemperature, out this.ParentObject.Physics.FreezeTemperature, out this.ParentObject.Physics.BrittleTemperature, out num);
				this.ParentObject.BaseElectricalConductivity = num;
			}
		}

		// Token: 0x06008DB8 RID: 36280 RVA: 0x00326A24 File Offset: 0x00324C24
		public LiquidVolume TempSplit(int SplitVolume)
		{
			LiquidVolume liquidVolume = LiquidVolume.tempVolume;
			liquidVolume.Volume = ((SplitVolume == 0) ? 1 : SplitVolume);
			liquidVolume.ComponentLiquids.Clear();
			foreach (KeyValuePair<string, int> keyValuePair in this.ComponentLiquids)
			{
				liquidVolume.ComponentLiquids.Add(keyValuePair.Key, keyValuePair.Value);
			}
			this.Volume -= SplitVolume;
			if (this.Volume <= 0)
			{
				this.Empty(false, false);
			}
			else
			{
				base.FlushWeightCaches();
			}
			return liquidVolume;
		}

		// Token: 0x06008DB9 RID: 36281 RVA: 0x00326AD0 File Offset: 0x00324CD0
		public LiquidVolume Split(int SplitVolume)
		{
			LiquidVolume liquidVolume = new LiquidVolume();
			liquidVolume.Volume = ((SplitVolume == 0) ? 1 : SplitVolume);
			foreach (KeyValuePair<string, int> keyValuePair in this.ComponentLiquids)
			{
				liquidVolume.ComponentLiquids.Add(keyValuePair.Key, keyValuePair.Value);
			}
			this.Volume -= SplitVolume;
			if (this.Volume <= 0)
			{
				this.Empty(false, false);
			}
			else
			{
				base.FlushWeightCaches();
			}
			return liquidVolume;
		}

		// Token: 0x06008DBA RID: 36282 RVA: 0x00326B70 File Offset: 0x00324D70
		public bool LiquidSameAs(LiquidVolume V)
		{
			if (V == null)
			{
				return false;
			}
			if (V.ComponentLiquids.Count != this.ComponentLiquids.Count)
			{
				return false;
			}
			foreach (KeyValuePair<string, int> keyValuePair in V.ComponentLiquids)
			{
				int num;
				if (!this.ComponentLiquids.TryGetValue(keyValuePair.Key, out num))
				{
					return false;
				}
				if (num != keyValuePair.Value)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06008DBB RID: 36283 RVA: 0x00326C08 File Offset: 0x00324E08
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x06008DBC RID: 36284 RVA: 0x00326C0C File Offset: 0x00324E0C
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == AllowLiquidCollectionEvent.ID || ID == AnyAutoCollectDramsEvent.ID || ID == AutoexploreObjectEvent.ID || ID == PooledEvent<CheckAnythingToCleanWithEvent>.ID || ID == PooledEvent<CheckAnythingToCleanWithNearbyEvent>.ID || ID == EffectAppliedEvent.ID || ID == EffectRemovedEvent.ID || ID == EnteredCellEvent.ID || ID == BeforeRenderEvent.ID || ID == CanSmartUseEvent.ID || ID == CommandSmartUseEvent.ID || ID == FellDownEvent.ID || ID == PooledEvent<FrozeEvent>.ID || ID == GetAutoCollectDramsEvent.ID || ID == PooledEvent<GetCleaningItemsEvent>.ID || ID == PooledEvent<GetCleaningItemsNearbyEvent>.ID || ID == SingletonEvent<GetDebugInternalsEvent>.ID || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetExtrinsicValueEvent.ID || ID == GetExtrinsicWeightEvent.ID || ID == GetFreeDramsEvent.ID || ID == PooledEvent<GetGameObjectSortEvent>.ID || ID == GetInventoryActionsAlwaysEvent.ID || ID == PooledEvent<GetMatterPhaseEvent>.ID || ID == GetNavigationWeightEvent.ID || ID == GetShortDescriptionEvent.ID || ID == GetSlottedInventoryActionsEvent.ID || ID == PooledEvent<GetSpringinessEvent>.ID || ID == GetStorableDramsEvent.ID || ID == GiveDramsEvent.ID || ID == GravitationEvent.ID || ID == PooledEvent<InterruptAutowalkEvent>.ID || ID == InventoryActionEvent.ID || ID == ObjectCreatedEvent.ID || ID == ObjectEnteredCellEvent.ID || ID == ObjectGoingProneEvent.ID || ID == ObjectStoppedFlyingEvent.ID || ID == OnDestroyObjectEvent.ID || ID == PooledEvent<PollForHealingLocationEvent>.ID || ID == SingletonEvent<RadiatesHeatEvent>.ID || ID == PooledEvent<StripContentsEvent>.ID || ID == PooledEvent<ThawedEvent>.ID || ID == UseDramsEvent.ID || ID == PooledEvent<VaporizedEvent>.ID;
		}

		// Token: 0x06008DBD RID: 36285 RVA: 0x00326DDC File Offset: 0x00324FDC
		public override bool HandleEvent(AutoexploreObjectEvent E)
		{
			if (E.Command == null && !this.IsMixed())
			{
				string primaryLiquidID = this.GetPrimaryLiquidID();
				if (primaryLiquidID != null && (Options.AutogetDroppedLiquid || this.ParentObject.GetIntProperty("DroppedByPlayer", 0) <= 0) && !this.ParentObject.IsOwned() && !this.EffectivelySealed() && !this.ParentObject.IsInStasis() && !this.ParentObject.IsTemporary && E.Actor.AnyAutoCollectDrams(primaryLiquidID, null, null))
				{
					E.Command = "CollectLiquid";
					E.AllowRetry = true;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008DBE RID: 36286 RVA: 0x00326E76 File Offset: 0x00325076
		public override bool HandleEvent(GetSpringinessEvent E)
		{
			if (this.IsOpenVolume() && !this.ParentObject.IsFrozen())
			{
				E.LinearIncrease += this.ParentObject.GetKineticResistance() * 95 / 100;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008DBF RID: 36287 RVA: 0x00326EB2 File Offset: 0x003250B2
		public override bool HandleEvent(RadiatesHeatEvent E)
		{
			return (!this.IsOpenVolume() || this.GetLiquidTemperature() <= 25) && base.HandleEvent(E);
		}

		// Token: 0x06008DC0 RID: 36288 RVA: 0x00326ECF File Offset: 0x003250CF
		public override bool HandleEvent(GetMatterPhaseEvent E)
		{
			if (this.IsOpenVolume())
			{
				E.MinMatterPhase(2);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008DC1 RID: 36289 RVA: 0x00326EE8 File Offset: 0x003250E8
		public override bool HandleEvent(GetNavigationWeightEvent E)
		{
			if (!E.Flying && this.IsOpenVolume() && E.PhaseMatches(this.ParentObject) && this.CanInteractWithAnything(E.Cell))
			{
				int num = this.GetNavigationWeight(E.Actor, E.Smart, E.Slimewalking, E.FilthAffinity, ref E.Uncacheable);
				if (num < 60 && this.IsSwimmingDepth())
				{
					if (E.Smart)
					{
						if (E.Swimming)
						{
							num += 2;
						}
						else
						{
							num += 5;
						}
						num = Math.Min(num, 60);
					}
					else
					{
						num = Math.Min(num + (E.Swimming ? 2 : 5), 60);
					}
				}
				else if (num < 30 && this.IsWadingDepth())
				{
					num = Math.Min(num + 1, 30);
				}
				if (E.Reefer && num < 25 && this.ContainsLiquid("algae") && this.IsWadingDepth())
				{
					num = 25;
				}
				E.MinWeight(num);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008DC2 RID: 36290 RVA: 0x00326FE7 File Offset: 0x003251E7
		public override bool HandleEvent(BeforeRenderEvent E)
		{
			if (this.Primary != null)
			{
				this.RequirePrimaryLiquid().BeforeRender(this);
			}
			if (this.Secondary != null)
			{
				this.RequireSecondaryLiquid().BeforeRenderSecondary(this);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008DC3 RID: 36291 RVA: 0x00327018 File Offset: 0x00325218
		public override bool HandleEvent(GetFreeDramsEvent E)
		{
			if (E.ApplyTo(this.ParentObject) && !this.EffectivelySealed())
			{
				if (this.IsPureLiquid(E.Liquid, false))
				{
					E.Drams += this.Volume;
				}
				else if (E.ImpureOkay && this.ComponentLiquids.ContainsKey(E.Liquid))
				{
					E.Drams += Math.Max(this.Volume * this.ComponentLiquids[E.Liquid] / 1000, 1);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008DC4 RID: 36292 RVA: 0x003270B4 File Offset: 0x003252B4
		public override bool HandleEvent(GetStorableDramsEvent E)
		{
			if (this.Volume < this.MaxVolume && E.ApplyTo(this.ParentObject) && (this.IsPureLiquid(E.Liquid, false) || (this.IsEmpty() && (this.AutoCollectLiquidType == null || this.AutoCollectLiquidType == E.Liquid))) && !this.EffectivelySealed() && this.ParentObject.AllowLiquidCollection(E.Liquid, E.Actor))
			{
				E.Drams += this.MaxVolume - this.Volume;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008DC5 RID: 36293 RVA: 0x00327154 File Offset: 0x00325354
		public override bool HandleEvent(GiveDramsEvent E)
		{
			if (this.Volume < this.MaxVolume && E.Drams > 0 && E.ApplyTo(this.ParentObject) && ((E.Pass == 1 && this.AutoCollectLiquidType == E.Liquid) || (E.Pass >= 2 && this.ParentObject.WantsLiquidCollection(E.Liquid, null)) || (E.Pass >= 3 && this.IsPureLiquid(E.Liquid, false) && !ProducesLiquidEvent.Check(this.ParentObject, E.Liquid)) || (E.Pass >= 4 && this.IsEmpty() && !ProducesLiquidEvent.Check(this.ParentObject, E.Liquid)) || E.Pass >= 5))
			{
				int drams = E.Drams;
				this.GiveDrams(E.Liquid, ref drams, E.Auto, E.StoredIn, E.Actor);
				if (E.Drams != drams)
				{
					E.Drams = drams;
					if (E.Drams <= 0)
					{
						return false;
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008DC6 RID: 36294 RVA: 0x0032726C File Offset: 0x0032546C
		public override bool HandleEvent(UseDramsEvent E)
		{
			if (this.Volume > 0 && E.Drams > 0 && E.ApplyTo(this.ParentObject) && ((E.Pass == 1 && ProducesLiquidEvent.Check(this.ParentObject, E.Liquid)) || (E.Pass == 2 && !this.ParentObject.WantsLiquidCollection(E.Liquid, null)) || E.Pass >= 3) && !this.EffectivelySealed())
			{
				if (this.IsPureLiquid(E.Liquid, false))
				{
					if (this.Volume >= E.Drams)
					{
						this.Volume -= E.Drams;
						E.Drams = 0;
					}
					else
					{
						E.Drams -= this.Volume;
						this.Volume = 0;
					}
					if (E.TrackContainers != null && !E.TrackContainers.Contains(this.ParentObject))
					{
						E.TrackContainers.Add(this.ParentObject);
					}
					if (this.Volume <= 0)
					{
						this.Empty(false, false);
					}
					else
					{
						base.FlushWeightCaches();
						this.CheckImage();
					}
					if (E.Drams <= 0)
					{
						return false;
					}
				}
				else if (E.ImpureOkay && this.ComponentLiquids.ContainsKey(E.Liquid))
				{
					int num = Math.Max(this.Volume * this.ComponentLiquids[E.Liquid] / 1000, 1);
					int num2;
					if (num >= E.Drams)
					{
						num2 = E.Drams;
						E.Drams = 0;
					}
					else
					{
						num2 = num;
						E.Drams -= num;
					}
					this.UseDrams(E.Liquid, num2);
					if (E.TrackContainers != null && !E.TrackContainers.Contains(this.ParentObject))
					{
						E.TrackContainers.Add(this.ParentObject);
					}
					if (E.Drams <= 0)
					{
						return false;
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008DC7 RID: 36295 RVA: 0x00327460 File Offset: 0x00325660
		public override bool HandleEvent(AnyAutoCollectDramsEvent E)
		{
			if (this.Volume < this.MaxVolume && E.ApplyTo(this.ParentObject))
			{
				string activeAutogetLiquid = this.GetActiveAutogetLiquid();
				if (activeAutogetLiquid != null && E.Liquid == activeAutogetLiquid && (this.IsPureLiquid(E.Liquid, false) || this.IsEmpty()) && !this.EffectivelySealed() && this.ParentObject.AllowLiquidCollection(E.Liquid, E.Actor))
				{
					return false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008DC8 RID: 36296 RVA: 0x003274E4 File Offset: 0x003256E4
		public override bool HandleEvent(GetAutoCollectDramsEvent E)
		{
			if (this.Volume < this.MaxVolume && E.ApplyTo(this.ParentObject))
			{
				string activeAutogetLiquid = this.GetActiveAutogetLiquid();
				if (activeAutogetLiquid != null && E.Liquid == activeAutogetLiquid && (this.IsPureLiquid(E.Liquid, false) || this.IsEmpty()) && !this.EffectivelySealed() && this.ParentObject.AllowLiquidCollection(E.Liquid, E.Actor))
				{
					E.Drams += this.MaxVolume - this.Volume;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008DC9 RID: 36297 RVA: 0x0032757E File Offset: 0x0032577E
		public override bool HandleEvent(AllowLiquidCollectionEvent E)
		{
			return (this.AutoCollectLiquidType == null || !(E.Liquid != this.AutoCollectLiquidType)) && base.HandleEvent(E);
		}

		// Token: 0x06008DCA RID: 36298 RVA: 0x003275A4 File Offset: 0x003257A4
		public override bool HandleEvent(EnteredCellEvent E)
		{
			if (E.Cell != null)
			{
				List<string> keylist = this.getKeylist();
				try
				{
					keylist.Clear();
					keylist.AddRange(this.ComponentLiquids.Keys);
					foreach (string text in keylist)
					{
						if (!LiquidVolume.Liquids[text].EnteredCell(this, E))
						{
							return false;
						}
					}
					if (this.IsOpenVolume())
					{
						int i = 0;
						int count = E.Cell.Objects.Count;
						while (i < count)
						{
							GameObject gameObject = E.Cell.Objects[i];
							if (gameObject != this.ParentObject)
							{
								LiquidVolume liquidVolume = gameObject.LiquidVolume;
								if (liquidVolume != null && liquidVolume.IsOpenVolume())
								{
									Temporary part = this.ParentObject.GetPart<Temporary>();
									ExistenceSupport part2 = this.ParentObject.GetPart<ExistenceSupport>();
									if (part != null)
									{
										part.Expire(true);
										if (GameObject.Validate(this.ParentObject))
										{
											this.Empty(false, false);
										}
									}
									else if (part2 != null)
									{
										part2.Unsupported(true);
										if (GameObject.Validate(this.ParentObject))
										{
											this.Empty(false, false);
										}
									}
									else
									{
										this.ParentObject.RemoveFromContext(null);
										LiquidVolume liquidVolume2 = liquidVolume;
										GameObject parentObject = this.ParentObject;
										liquidVolume2.MixWith(this, ref E.InterfaceExit, parentObject, null, null, false);
										this.ParentObject.Obliterate(null, false, null);
									}
									return false;
								}
							}
							i++;
						}
						if (E.Type != "Pour" && E.Type != "Flow" && E.Cell.Objects.Count > 1 && E.Cell.ParentZone.IsActive() && this.CanInteractWithAnything(E.Cell))
						{
							int j = 0;
							int num = E.Cell.Objects.Count;
							while (j < num)
							{
								GameObject gameObject2 = E.Cell.Objects[j];
								if (this.CanInteractWith(gameObject2, E.Cell, true))
								{
									foreach (string text2 in keylist)
									{
										LiquidVolume.Liquids[text2].ObjectInCell(this, gameObject2);
									}
									this.ProcessContact(gameObject2, true, gameObject2.HasEffect<Prone>(), false, null, true, -1);
								}
								if (num != E.Cell.Objects.Count)
								{
									num = E.Cell.Objects.Count;
									if (j < num && E.Cell.Objects[j] != gameObject2)
									{
										j--;
									}
								}
								j++;
							}
						}
					}
				}
				finally
				{
					LiquidVolume.keylistPool.Enqueue(keylist);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008DCB RID: 36299 RVA: 0x003278EC File Offset: 0x00325AEC
		public void Splash(Cell C = null)
		{
			if (C == null)
			{
				if (this.ParentObject != null)
				{
					C = this.ParentObject.CurrentCell;
				}
				if (C == null)
				{
					return;
				}
			}
			if (C.IsVisible())
			{
				if (this.Secondary != null && 10.in100())
				{
					C.LiquidSplash(this.RequireSecondaryLiquid());
				}
				else if (this.Primary != null)
				{
					C.LiquidSplash(this.RequirePrimaryLiquid());
				}
			}
			string text = null;
			if (this.Primary != null)
			{
				string text2 = this.RequirePrimaryLiquid().SplashSound(this);
				if (text2 != null)
				{
					text = text2;
				}
			}
			if (this.Secondary != null && text == null)
			{
				string text3 = this.RequireSecondaryLiquid().SplashSound(this);
				if (text3 != null)
				{
					text = text3;
				}
			}
			if (text == null)
			{
				List<string> tertiaries = this.GetTertiaries();
				if (tertiaries != null)
				{
					foreach (string text4 in tertiaries)
					{
						if (text4 != this.Primary && text4 != this.Secondary)
						{
							string text5 = LiquidVolume.Liquids[text4].SplashSound(this);
							if (text5 != null)
							{
								text = text5;
								break;
							}
						}
					}
				}
			}
			base.PlayWorldSound(text, 0.5f, 0f, false, C, 0f, 1f, 1f, int.MaxValue);
		}

		// Token: 0x06008DCC RID: 36300 RVA: 0x00327A34 File Offset: 0x00325C34
		private int GetCurrentCoverIfSame(GameObject obj)
		{
			LiquidCovered effect = obj.GetEffect<LiquidCovered>();
			if (effect != null && this.LiquidSameAs(effect.Liquid))
			{
				return effect.Liquid.Volume;
			}
			return 0;
		}

		// Token: 0x06008DCD RID: 36301 RVA: 0x00327A68 File Offset: 0x00325C68
		private void LiquidInContact(GameObject obj, int Amount, ref bool TemporaryKnown, ref bool Temporary, bool Poured, GameObject PouredBy, bool FromCell)
		{
			if (!TemporaryKnown)
			{
				TemporaryKnown = true;
				Temporary = this.ParentObject.IsTemporary;
			}
			if (Temporary)
			{
				int volume = this.Volume;
				this.Volume = Amount;
				try
				{
					this.SmearOn(obj, PouredBy, FromCell);
					return;
				}
				finally
				{
					this.Volume = volume;
				}
			}
			obj.ApplyEffect(new LiquidCovered(this, Amount, 1, Poured, PouredBy, FromCell), null);
		}

		// Token: 0x06008DCE RID: 36302 RVA: 0x00327AD8 File Offset: 0x00325CD8
		public int ProcessContact(GameObject obj, bool Initial = false, bool Prone = false, bool Poured = false, GameObject PouredBy = null, bool FromCell = false, int ContactVolume = -1)
		{
			if (this.ParentObject != null)
			{
				if (this.ParentObject.IsFrozen())
				{
					return 0;
				}
				if (this.ParentObject.IsInStasis())
				{
					return 0;
				}
			}
			Physics physics = obj.Physics;
			if (physics == null || !physics.IsReal)
			{
				return 0;
			}
			if (obj.IsScenery)
			{
				return 0;
			}
			if (FromCell)
			{
				LiquidCovered effect = obj.GetEffect<LiquidCovered>();
				if (effect != null && effect.FromCell)
				{
					if (this.LiquidSameAs(effect.Liquid))
					{
						return 0;
					}
					effect.FromCell = false;
				}
			}
			int volume = this.Volume;
			if (ContactVolume == -1)
			{
				ContactVolume = this.Volume;
			}
			bool flag = false;
			int maximumLiquidExposure = obj.GetMaximumLiquidExposure();
			if (maximumLiquidExposure <= 0)
			{
				return 0;
			}
			Body body = obj.Body;
			List<GameObject> list = null;
			if (body != null)
			{
				list = Event.NewGameObjectList();
				body.GetEquippedObjectsExceptNatural(list);
			}
			bool flag2 = false;
			bool flag3 = false;
			if (!Poured && this.IsSwimmableFor(obj))
			{
				flag = true;
				if (Initial && obj.IsPlayer())
				{
					IComponent<GameObject>.XDidYToZ(obj, "swim", "through", this.ParentObject, null, null, null, null, null, null, false, false, true, false, false, null, null, null, false, false, false, false, false, null);
				}
				if (obj.IsCreature && !obj.HasEffect<Swimming>() && obj.IsPotentiallyMobile())
				{
					obj.ApplyEffect(new Swimming(), null);
				}
				int num = volume / 2;
				int num2 = Math.Min(Math.Min(maximumLiquidExposure, num), ContactVolume);
				if (num2 > 0)
				{
					int num3 = num2 - this.GetCurrentCoverIfSame(obj);
					if (num3 > 0)
					{
						this.LiquidInContact(obj, num3, ref flag2, ref flag3, Poured, PouredBy, FromCell);
					}
					flag = true;
					ContactVolume -= num2;
				}
				if (list != null && list.Count > 0 && this.Volume >= num && ContactVolume > 0)
				{
					if (list.Count > 1)
					{
						list.ShuffleInPlace<GameObject>(null);
					}
					int i = 0;
					int count = list.Count;
					while (i < count)
					{
						num2 = Math.Min(Math.Min(this.GetAdsorbableDrams(list[i]), this.Volume / 2), ContactVolume);
						if (num2 > 0)
						{
							int num4 = num2 - this.GetCurrentCoverIfSame(list[i]);
							if (num4 > 0)
							{
								this.LiquidInContact(list[i], num4, ref flag2, ref flag3, Poured, PouredBy, FromCell);
							}
							flag = true;
							ContactVolume -= num2;
						}
						if (this.Volume < num || ContactVolume <= 0)
						{
							break;
						}
						i++;
					}
				}
				if (this.Volume >= num && ContactVolume > 0)
				{
					Inventory inventory = obj.Inventory;
					List<GameObject> list2 = null;
					if (inventory != null)
					{
						list2 = Event.NewGameObjectList();
						list2.AddRange(inventory.Objects);
					}
					if (list2 != null && list2.Count > 0)
					{
						if (list2.Count > 1)
						{
							list2.ShuffleInPlace<GameObject>(null);
						}
						int j = 0;
						int count2 = list2.Count;
						while (j < count2)
						{
							if (list2[j].Weight != 0)
							{
								num2 = Math.Min(Math.Min(this.GetAdsorbableDrams(list2[j]), this.Volume / 2), ContactVolume);
								if (num2 > 0)
								{
									int num5 = num2 - this.GetCurrentCoverIfSame(list2[j]);
									if (num5 > 0)
									{
										this.LiquidInContact(list2[j], num5, ref flag2, ref flag3, Poured, PouredBy, FromCell);
									}
									flag = true;
									ContactVolume -= num2;
								}
								if (this.Volume < num || ContactVolume <= 0)
								{
									break;
								}
							}
							j++;
						}
					}
				}
			}
			else
			{
				if (!Poured && this.Volume > 0)
				{
					flag = true;
				}
				if (!Poured && this.IsWadingDepth())
				{
					if (Initial && obj.IsPlayer())
					{
						IComponent<GameObject>.XDidYToZ(obj, "wade", "through", this.ParentObject, null, null, null, null, null, null, false, false, true, false, false, null, null, null, false, false, false, false, false, null);
					}
					if (obj.IsCreature && !obj.HasEffect<Wading>() && obj.IsPotentiallyMobile())
					{
						obj.ApplyEffect(new Wading(), null);
					}
				}
				int num6 = (Poured ? Math.Max(ContactVolume / 2, 1) : 0);
				int num7;
				if (Poured)
				{
					num7 = ContactVolume;
				}
				else
				{
					double num8;
					if (body != null)
					{
						if (Prone)
						{
							num8 = 0.4;
						}
						else if (body.GetBodyMobility() > 0 || body.GetTotalMobility() <= 0)
						{
							num8 = 0.3;
						}
						else
						{
							num8 = 0.1;
						}
					}
					else
					{
						num8 = 0.2;
					}
					num8 *= 1.0 + (double)ContactVolume / 300.0;
					if (num8 >= 1.0)
					{
						num7 = ContactVolume;
					}
					else
					{
						num7 = (int)((double)ContactVolume * num8);
					}
				}
				int num9 = (Poured ? 100 : (ContactVolume + num7));
				int num10;
				if (num9 >= 100)
				{
					num10 = Math.Min(ContactVolume, num7);
				}
				else
				{
					num10 = 0;
					for (int k = 0; k < ContactVolume; k++)
					{
						if (num9.in100())
						{
							num10++;
							if (num10 >= num7)
							{
								break;
							}
						}
					}
				}
				if (num10 > 0)
				{
					int adsorbableDrams = this.GetAdsorbableDrams(obj);
					num10 -= num6;
					if (body != null)
					{
						LiquidVolume.BodyParts.Clear();
						body.GetConcreteParts(LiquidVolume.BodyParts);
						LiquidVolume.BodyPartExposure.Clear();
						LiquidVolume.BodyPartCapacity.Clear();
						LiquidVolume.BodyPartExposure.Clear();
						int num11 = 0;
						int num12 = 0;
						if (!Poured)
						{
							LiquidVolume.MobilityBodyParts.Clear();
							body.GetConcreteMobilityProvidingParts(LiquidVolume.MobilityBodyParts);
							if (LiquidVolume.MobilityBodyParts.Count > 0)
							{
								if (LiquidVolume.MobilityBodyParts.Count > 1)
								{
									LiquidVolume.MobilityBodyParts.ShuffleInPlace<BodyPart>(null);
								}
								int l = 0;
								int count3 = LiquidVolume.MobilityBodyParts.Count;
								while (l < count3)
								{
									BodyPart bodyPart = LiquidVolume.MobilityBodyParts[l];
									if (bodyPart.Equipped != null)
									{
										int adsorbableDrams2 = this.GetAdsorbableDrams(bodyPart.Equipped);
										if (adsorbableDrams2 > 0)
										{
											LiquidVolume.BodyPartCapacity.Add(bodyPart, adsorbableDrams2);
										}
									}
									else
									{
										LiquidVolume.BodyPartCapacity.Add(bodyPart, Math.Max(maximumLiquidExposure / LiquidVolume.BodyParts.Count, 1));
									}
									l++;
								}
								int num13 = 0;
								for (int m = 0; m < num10; m++)
								{
									BodyPart randomElement = LiquidVolume.BodyPartCapacity.GetRandomElement<BodyPart>(ref num13, null);
									if (randomElement == null)
									{
										break;
									}
									int num14;
									if (LiquidVolume.BodyPartExposure.TryGetValue(randomElement, out num14))
									{
										if (num14 < LiquidVolume.BodyPartCapacity[randomElement])
										{
											Dictionary<BodyPart, int> bodyPartExposure = LiquidVolume.BodyPartExposure;
											BodyPart bodyPart2 = randomElement;
											int num15 = bodyPartExposure[bodyPart2];
											bodyPartExposure[bodyPart2] = num15 + 1;
											num12++;
										}
									}
									else
									{
										LiquidVolume.BodyPartExposure.Add(randomElement, 1);
										num12++;
									}
								}
								if (num10 > num12 && LiquidVolume.BodyParts.Count > LiquidVolume.MobilityBodyParts.Count)
								{
									num11 = (this.Volume - 100) / 2;
								}
							}
							else
							{
								num11 = 100;
							}
						}
						else
						{
							num11 = 100;
						}
						if (num11 > 0)
						{
							int num16 = 0;
							if (num11 >= 100)
							{
								num16 = num10;
							}
							else
							{
								for (int n = num12; n < num10; n++)
								{
									if (num11.in100())
									{
										num16++;
									}
								}
							}
							if (num16 > 0)
							{
								int num17 = 0;
								int count4 = LiquidVolume.BodyParts.Count;
								while (num17 < count4)
								{
									BodyPart bodyPart3 = LiquidVolume.BodyParts[num17];
									if (!LiquidVolume.BodyPartCapacity.ContainsKey(bodyPart3))
									{
										if (bodyPart3.Equipped != null)
										{
											int adsorbableDrams3 = this.GetAdsorbableDrams(bodyPart3.Equipped);
											if (adsorbableDrams3 > 0)
											{
												LiquidVolume.BodyPartCapacity.Add(bodyPart3, adsorbableDrams3);
											}
										}
										else
										{
											LiquidVolume.BodyPartCapacity.Add(bodyPart3, Math.Max(maximumLiquidExposure / LiquidVolume.BodyParts.Count, 1));
										}
									}
									num17++;
								}
								int num18 = 0;
								for (int num19 = 0; num19 < num16; num19++)
								{
									BodyPart randomElement2 = LiquidVolume.BodyPartCapacity.GetRandomElement<BodyPart>(ref num18, null);
									if (randomElement2 == null)
									{
										break;
									}
									int num20;
									if (LiquidVolume.BodyPartExposure.TryGetValue(randomElement2, out num20))
									{
										if (num20 < LiquidVolume.BodyPartCapacity[randomElement2])
										{
											Dictionary<BodyPart, int> bodyPartExposure2 = LiquidVolume.BodyPartExposure;
											BodyPart bodyPart2 = randomElement2;
											int num15 = bodyPartExposure2[bodyPart2];
											bodyPartExposure2[bodyPart2] = num15 + 1;
											num12++;
										}
									}
									else
									{
										LiquidVolume.BodyPartExposure.Add(randomElement2, 1);
										num12++;
									}
								}
							}
						}
						foreach (KeyValuePair<BodyPart, int> keyValuePair in LiquidVolume.BodyPartExposure)
						{
							if (keyValuePair.Key.Equipped != null && !keyValuePair.Key.Equipped.HasTag("NaturalGear"))
							{
								num10 -= keyValuePair.Value;
								int num21 = keyValuePair.Value - this.GetCurrentCoverIfSame(keyValuePair.Key.Equipped);
								if (num21 > 0)
								{
									this.LiquidInContact(keyValuePair.Key.Equipped, num21, ref flag2, ref flag3, Poured, PouredBy, FromCell);
								}
							}
						}
					}
					num10 += num6;
					if (num10 > 0)
					{
						int num22 = Math.Min(num10 - this.GetCurrentCoverIfSame(obj), adsorbableDrams);
						if (num22 > 0)
						{
							this.LiquidInContact(obj, num22, ref flag2, ref flag3, Poured, PouredBy, FromCell);
						}
					}
					flag = true;
				}
			}
			if (flag && obj.IsPlayer() && !AutoAct.IsActive(true))
			{
				this.Splash(obj.CurrentCell);
			}
			return volume - this.Volume;
		}

		// Token: 0x06008DCF RID: 36303 RVA: 0x003283D4 File Offset: 0x003265D4
		public override bool HandleEvent(ObjectEnteredCellEvent E)
		{
			GameObject @object = E.Object;
			if (!E.IgnoreGravity && this.CanInteractWith(@object, E.Cell, false))
			{
				List<string> keylist = this.getKeylist();
				try
				{
					keylist.Clear();
					foreach (KeyValuePair<string, int> keyValuePair in this.ComponentLiquids)
					{
						keylist.Add(keyValuePair.Key);
					}
					foreach (string text in keylist)
					{
						BaseLiquid baseLiquid = LiquidVolume.Liquids[text];
						baseLiquid.ObjectEnteredCell(this, E);
						baseLiquid.ObjectEnteredCell(this, @object);
					}
					if (this.IsOpenVolume())
					{
						this.ProcessContact(@object, true, false, false, null, true, -1);
					}
				}
				finally
				{
					LiquidVolume.keylistPool.Enqueue(keylist);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008DD0 RID: 36304 RVA: 0x003284EC File Offset: 0x003266EC
		public override bool HandleEvent(GravitationEvent E)
		{
			GameObject @object = E.Object;
			if (this.CanInteractWith(@object, E.Cell, false))
			{
				List<string> keylist = this.getKeylist();
				try
				{
					keylist.Clear();
					foreach (KeyValuePair<string, int> keyValuePair in this.ComponentLiquids)
					{
						keylist.Add(keyValuePair.Key);
					}
					foreach (string text in keylist)
					{
						BaseLiquid baseLiquid = LiquidVolume.Liquids[text];
						baseLiquid.ObjectEnteredCell(this, E);
						baseLiquid.ObjectEnteredCell(this, @object);
					}
					if (this.IsOpenVolume())
					{
						this.ProcessContact(@object, true, false, false, null, true, -1);
					}
				}
				finally
				{
					LiquidVolume.keylistPool.Enqueue(keylist);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008DD1 RID: 36305 RVA: 0x003285F8 File Offset: 0x003267F8
		public override bool HandleEvent(ObjectGoingProneEvent E)
		{
			GameObject @object = E.Object;
			if (this.CanInteractWith(@object, E.Cell, false))
			{
				List<string> keylist = this.getKeylist();
				try
				{
					keylist.Clear();
					keylist.AddRange(this.ComponentLiquids.Keys);
					foreach (string text in keylist)
					{
						LiquidVolume.Liquids[text].ObjectGoingProne(this, E.Object, E.UsePopups);
					}
					if (this.IsOpenVolume())
					{
						this.ProcessContact(@object, false, true, false, null, true, -1);
					}
				}
				finally
				{
					LiquidVolume.keylistPool.Enqueue(keylist);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008DD2 RID: 36306 RVA: 0x003286CC File Offset: 0x003268CC
		public override bool HandleEvent(ObjectStoppedFlyingEvent E)
		{
			GameObject @object = E.Object;
			if (this.CanInteractWith(@object, E.Cell, false))
			{
				List<string> keylist = this.getKeylist();
				try
				{
					keylist.Clear();
					keylist.AddRange(this.ComponentLiquids.Keys);
					foreach (string text in keylist)
					{
						LiquidVolume.Liquids[text].ObjectInCell(this, E.Object);
					}
					if (this.IsOpenVolume())
					{
						this.ProcessContact(@object, true, false, false, null, true, -1);
					}
				}
				finally
				{
					LiquidVolume.keylistPool.Enqueue(keylist);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008DD3 RID: 36307 RVA: 0x0032879C File Offset: 0x0032699C
		public void ProcessExposure(GameObject GO, GameObject By = null, bool FromCell = false)
		{
			if (!GameObject.Validate(ref GO))
			{
				return;
			}
			double num = Math.Min((double)this.Volume, GO.GetMaximumLiquidExposureAsDouble());
			if (num <= 0.0)
			{
				return;
			}
			int liquidThermalConductivity = this.GetLiquidThermalConductivity();
			if (liquidThermalConductivity <= 0)
			{
				return;
			}
			int liquidTemperature = this.GetLiquidTemperature();
			int temperature = GO.Physics.Temperature;
			int num2 = liquidTemperature - temperature;
			if (num2 == 0)
			{
				return;
			}
			if (this.ParentObject != null && !GO.PhaseMatches(this.ParentObject))
			{
				return;
			}
			int liquidCombustibility = this.GetLiquidCombustibility();
			bool flag = GO.IsAflame();
			int? num5;
			if (flag && liquidCombustibility >= 50)
			{
				GameObject gameObject = GO;
				int num3 = (int)(num.DiminishingReturns(8.0) * (double)liquidCombustibility / 50.0);
				bool flag2 = false;
				bool flag3 = false;
				bool flag4 = false;
				bool flag5 = false;
				int num4 = 0;
				num5 = null;
				int? num6 = num5;
				num5 = null;
				gameObject.TemperatureChange(num3, By, flag2, flag3, flag4, flag5, num4, num6, num5);
				return;
			}
			double num7 = (double)num2 * num.DiminishingReturns(8.0) / 4.0;
			if (num2 > 0)
			{
				if (num7 > (double)num2)
				{
					num7 = (double)((float)num2);
				}
			}
			else if (num7 < (double)num2)
			{
				num7 = (double)((float)num2);
			}
			if (flag && liquidCombustibility != 0)
			{
				if (num7 > 0.0)
				{
					num7 = num7 * (double)(100 + liquidCombustibility) / 100.0;
				}
				else
				{
					num7 = num7 * (double)(100 - liquidCombustibility) / 100.0;
				}
			}
			if (liquidThermalConductivity != 100)
			{
				num7 = num7 * (double)liquidThermalConductivity / 100.0;
			}
			if (num7 > 0.0)
			{
				GameObject gameObject2 = GO;
				int num8 = (int)num7;
				bool flag6 = false;
				bool flag7 = false;
				bool flag8 = false;
				bool flag9 = false;
				int num9 = 0;
				num5 = new int?(liquidTemperature);
				gameObject2.TemperatureChange(num8, By, flag6, flag7, flag8, flag9, num9, null, num5);
				return;
			}
			GameObject gameObject3 = GO;
			int num10 = (int)num7;
			bool flag10 = false;
			bool flag11 = false;
			bool flag12 = false;
			bool flag13 = false;
			int num11 = 0;
			int? num12 = new int?(liquidTemperature);
			num5 = null;
			gameObject3.TemperatureChange(num10, By, flag10, flag11, flag12, flag13, num11, num12, num5);
		}

		// Token: 0x06008DD4 RID: 36308 RVA: 0x00328958 File Offset: 0x00326B58
		public override bool HandleEvent(GetGameObjectSortEvent E)
		{
			if (E.Category1 == "Water Containers" && E.Category2 == "Water Containers")
			{
				GameObject @object = E.Object1;
				LiquidVolume liquidVolume = ((@object != null) ? @object.LiquidVolume : null);
				GameObject object2 = E.Object2;
				LiquidVolume liquidVolume2 = ((object2 != null) ? object2.LiquidVolume : null);
				int num = (liquidVolume2 != null).CompareTo(liquidVolume != null);
				if (num != 0)
				{
					E.Sort = num;
					return false;
				}
				if (liquidVolume != null && liquidVolume2 != null)
				{
					int num2 = liquidVolume.IsEmpty().CompareTo(liquidVolume2.IsEmpty());
					if (num2 != 0)
					{
						E.Sort = num2;
						return false;
					}
					int num3 = liquidVolume2.GetLiquidExtrinsicValuePerDram().CompareTo(liquidVolume.GetLiquidExtrinsicValuePerDram());
					if (num3 != 0)
					{
						E.Sort = num3;
						return false;
					}
					int num4 = liquidVolume.GetLiquidName().CompareTo(liquidVolume2.GetLiquidName());
					if (num4 != 0)
					{
						E.Sort = num4;
						return false;
					}
					int num5 = liquidVolume2.Volume.CompareTo(liquidVolume.Volume);
					if (num5 != 0)
					{
						E.Sort = num5;
						return false;
					}
				}
				GameObject object3 = E.Object1;
				string text = ((object3 != null) ? object3.GetDisplayName(int.MaxValue, null, null, false, false, false, false, true, false, true, false, false, false, false, false, false, null, false, false, false, false, null, false, null, false, true) : null);
				GameObject object4 = E.Object2;
				int num6 = string.Compare(text, (object4 != null) ? object4.GetDisplayName(int.MaxValue, null, null, false, false, false, false, true, false, true, false, false, false, false, false, false, null, false, false, false, false, null, false, null, false, true) : null, true);
				if (num6 != 0)
				{
					E.Sort = num6;
					return false;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008DD5 RID: 36309 RVA: 0x00328AF8 File Offset: 0x00326CF8
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.ForSort && this.ParentObject.GetInventoryCategory(false) == "Water Containers")
			{
				E.ReplaceEntirety("Water Containers");
				return false;
			}
			if (E.GetPrimaryBase() == "pool")
			{
				if (this.IsSwimmingDepth())
				{
					E.AddAdjective("deep", 0);
				}
				else if (!this.IsWadingDepth())
				{
					E.ReplacePrimaryBase("puddle", 0);
				}
			}
			if (!E.Reference && (E.AsIfKnown || this.GetEpistemicStatus() != 0))
			{
				if (this.IsEmpty())
				{
					if (!this.UsesNamePreposition())
					{
						if (this.EffectivelySealed())
						{
							if (this.LiquidVisibleWhenSealed)
							{
								if (this.ShowSeal)
								{
									if (E.NoColor)
									{
										E.AddTag("[empty, sealed]", -5);
									}
									else
									{
										E.AddTag("{{y|[{{K|empty, {{c|sealed}}}}]}}", -5);
									}
								}
								else if (E.NoColor)
								{
									E.AddTag("[empty]", -5);
								}
								else
								{
									E.AddTag("{{y|[{{K|empty}}]}}", -5);
								}
							}
							else if (this.ShowSeal)
							{
								if (E.NoColor)
								{
									E.AddTag("[sealed]", -5);
								}
								else
								{
									E.AddTag("{{y|[{{c|sealed}}]}}", -5);
								}
							}
						}
						else if (E.NoColor)
						{
							E.AddTag("[empty]", -5);
						}
						else
						{
							E.AddTag("{{y|[{{K|empty}}]}}", -5);
						}
					}
				}
				else
				{
					string liquidDescription = this.GetLiquidDescription(E.Cutoff > 1040, false, E.NoColor, false, E.ForSort, null);
					if (!liquidDescription.IsNullOrEmpty())
					{
						if (this.UsesNamePreposition())
						{
							E.AddClause(liquidDescription, 0);
						}
						else
						{
							E.AddTag(liquidDescription, -5);
						}
					}
				}
				if (this.AutoCollectLiquidType != null)
				{
					if (this.Volume == 0 || !this.IsPureLiquid(this.AutoCollectLiquidType, false))
					{
						if (LiquidVolume.Liquids.ContainsKey(this.AutoCollectLiquidType) && LiquidVolume.GetLiquid(this.AutoCollectLiquidType) != null)
						{
							if (E.NoColor)
							{
								E.AddTag("[auto-collecting " + LiquidVolume.Liquids[this.AutoCollectLiquidType].GetName(this).Strip() + "]", 20);
							}
							else
							{
								E.AddTag("{{y|[{{c|auto-collecting " + LiquidVolume.Liquids[this.AutoCollectLiquidType].GetName(this) + "}}]}}", 20);
							}
						}
					}
					else if (E.NoColor)
					{
						E.AddTag("[auto-collecting]", 20);
					}
					else
					{
						E.AddTag("{{y|[{{c|auto-collecting}}]}}", 20);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008DD6 RID: 36310 RVA: 0x00328D98 File Offset: 0x00326F98
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.ParentObject.HasTagOrProperty("Pool") && this.Volume > 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				if (this.IsSwimmingDepth())
				{
					stringBuilder.Append("Light scatters off the surface of the wide and yawning pool and gets lost in its depths.");
				}
				else if (this.IsWadingDepth())
				{
					stringBuilder.Append("Liquid pools into a cistern of air and ground.");
				}
				else
				{
					stringBuilder.Append("Liquid dots the ground in shallow pools.");
				}
				E.Base = stringBuilder;
			}
			if (this.Volume > 0 && (!this.EffectivelySealed() || this.LiquidVisibleWhenSealed))
			{
				List<string> list = null;
				foreach (KeyValuePair<string, int> keyValuePair in this.ComponentLiquids)
				{
					if (keyValuePair.Value > 0)
					{
						List<string> list2 = LiquidVolume.Liquids[keyValuePair.Key].GetPreparedCookingIngredient().CachedCommaExpansion();
						if (list == null)
						{
							list = list2.ToList<string>();
						}
						else
						{
							int i = 0;
							int count = list2.Count;
							while (i < count)
							{
								if (!list.Contains(list2[i]))
								{
									list.Add(list2[i]);
								}
								i++;
							}
						}
					}
				}
				if (list != null && (this.IsOpenVolume() || (this.ParentObject != null && this.ParentObject.HasTagOrProperty("WaterContainer"))))
				{
					List<string> list3 = null;
					foreach (string text in list)
					{
						string text2 = "ProceduralCookingIngredient_" + text;
						if (GameObjectFactory.Factory.Blueprints.ContainsKey(text2))
						{
							string tag = GameObjectFactory.Factory.Blueprints[text2].GetTag("Description", "");
							if (!tag.IsNullOrEmpty())
							{
								if (list3 == null)
								{
									list3 = new List<string>();
								}
								list3.Add(tag);
							}
						}
					}
					if (list3 != null)
					{
						E.Postfix.AppendRules("Adds " + Grammar.MakeOrList(list3, true) + " effects to cooked meals.");
					}
				}
			}
			if (this.Sealed)
			{
				E.Postfix.AppendRules("Sealed: The liquid contained inside can't be accessed.");
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008DD7 RID: 36311 RVA: 0x00328FE4 File Offset: 0x003271E4
		public override bool HandleEvent(GetDebugInternalsEvent E)
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			foreach (KeyValuePair<string, int> keyValuePair in this.ComponentLiquids)
			{
				stringBuilder.Compound(keyValuePair.Key, '\n').Append('-').Append(keyValuePair.Value);
			}
			E.AddEntry(this, "ComponentLiquids", stringBuilder.ToString());
			E.AddEntry(this, "Primary", this.Primary);
			E.AddEntry(this, "Secondary", this.Secondary);
			return base.HandleEvent(E);
		}

		// Token: 0x06008DD8 RID: 36312 RVA: 0x00329098 File Offset: 0x00327298
		public override bool HandleEvent(GetSlottedInventoryActionsEvent E)
		{
			if (this.GetEpistemicStatus() != 0 && !this.EffectivelySealed())
			{
				E.AddAction("FillSlotted", "fill " + this.ParentObject.BaseDisplayNameStripped, "FillFrom", null, 'f', false, 50 - this.Volume * 50 / this.MaxVolume, 0, false, false, false, false, true, this.ParentObject, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008DD9 RID: 36313 RVA: 0x00329108 File Offset: 0x00327308
		public override bool HandleEvent(GetInventoryActionsAlwaysEvent E)
		{
			int epistemicStatus = this.GetEpistemicStatus();
			if (epistemicStatus != 0)
			{
				if (this.ManualSeal)
				{
					if (this.Sealed)
					{
						E.AddAction("Unseal", "unseal", "Unseal", null, 'u', false, 0, 0, false, false, true, false, true, null, false);
					}
					else
					{
						E.AddAction("Seal", "seal", "Seal", null, 's', false, 0, 0, false, false, true, false, true, null, false);
					}
				}
				if (!this.EffectivelySealed())
				{
					if (!this.IsOpenVolume())
					{
						E.AddAction("Fill", "fill", "FillFrom", null, 'f', false, 0, 0, false, false, false, false, true, null, false);
						if (this.HasDrain)
						{
							E.AddAction("Drain", "drain", "Drain", null, 'r', false, -1, 0, false, false, false, false, true, null, false);
						}
					}
					if (this.Volume > 0)
					{
						int num = 0;
						if ((this.ParentObject.Equipped != null || this.ParentObject.InInventory != null) && !this.ParentObject.HasPart<MissileWeapon>())
						{
							num = 20;
						}
						E.AddAction("Drink", "drink", "Drink", null, 'k', false, 0, 0, false, false, false, false, true, null, false);
						E.AddAction("Pour", "pour", "Pour", null, 'p', false, num, 0, false, false, false, false, true, null, false);
						bool flag = false;
						bool flag2 = false;
						int num2 = 10;
						if (this.ParentObject.InInventory == E.Actor)
						{
							flag = true;
						}
						else
						{
							flag2 = true;
							if (this.ParentObject.Equipped == E.Actor)
							{
								flag = true;
								num2 = -1;
							}
						}
						if (flag && epistemicStatus == 2)
						{
							if (this.AutoCollectLiquidType != null)
							{
								E.AddAction("AutoCollect", "stop auto-collecting liquid", "AutoCollectLiquid", null, 'a', false, -1, 0, false, false, false, false, true, null, false);
							}
							else
							{
								E.AddAction("AutoCollect", "auto-collect liquid", "AutoCollectLiquid", null, 'a', false, -1, 0, false, false, false, false, true, null, false);
							}
						}
						if (flag2)
						{
							E.AddAction("Collect", "collect liquid", "CollectLiquid", null, 'c', false, num2, 0, false, false, false, false, true, null, false);
						}
						if (this.UsableForCleaning() && CheckAnythingToCleanEvent.Check(IComponent<GameObject>.ThePlayer, this.ParentObject))
						{
							E.AddAction("CleanAll", "clean all your items [1 dram]", "CleanWithLiquid", null, 'n', false, 20, 0, false, false, false, false, true, null, false);
						}
					}
					else if (this.AutoCollectLiquidType != null)
					{
						if (epistemicStatus == 2)
						{
							E.AddAction("AutoCollect", "stop auto-collecting liquid", "AutoCollectLiquid", null, 'a', false, -1, 0, false, false, false, false, true, null, false);
						}
					}
					else if ((this.ParentObject.InInventory == E.Actor || this.ParentObject.Equipped == E.Actor) && epistemicStatus == 2 && !GetPreferredLiquidEvent.GetFor(this.ParentObject, E.Actor).IsNullOrEmpty())
					{
						E.AddAction("AutoCollect", "auto-collect liquid", "AutoCollectLiquid", null, 'a', false, -1, 0, false, false, false, false, true, null, false);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008DDA RID: 36314 RVA: 0x003293FC File Offset: 0x003275FC
		public override bool HandleEvent(GetExtrinsicValueEvent E)
		{
			if (this.IsFreshWater(false))
			{
				E.Value += (double)this.Volume;
			}
			else
			{
				bool flag = this.IsPure();
				foreach (KeyValuePair<string, int> keyValuePair in this.ComponentLiquids)
				{
					float num = (float)(this.Volume * keyValuePair.Value) * LiquidVolume.Liquids[keyValuePair.Key].GetExtrinsicValuePerDram(flag) / 1000f;
					E.Value += (double)num;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008DDB RID: 36315 RVA: 0x003294B4 File Offset: 0x003276B4
		public override bool HandleEvent(GetExtrinsicWeightEvent E)
		{
			E.Weight += this.GetLiquidWeight();
			return base.HandleEvent(E);
		}

		// Token: 0x06008DDC RID: 36316 RVA: 0x003294D0 File Offset: 0x003276D0
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "Drink")
			{
				if (!this.EffectivelySealed())
				{
					bool flag;
					bool flag2;
					CanDrinkEvent.GetFor(E.Actor, this, out flag, out flag2);
					if (!flag)
					{
						if (flag2)
						{
							E.Actor.Fail("You are unable to drink that.");
						}
						else
						{
							E.Actor.Fail("You are unable to drink liquids.");
						}
						return false;
					}
					if (this.ParentObject.IsInStasis())
					{
						E.Actor.Fail("You cannot seem to interact with " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " in any way.");
						return false;
					}
					if (E.Actor.IsPlayer() && this.ConsiderLiquidDangerousToDrink() && Options.ConfirmDangerousLiquid && Popup.WarnYesNoCancel("Are you sure you want to drink that?", "Sounds/UI/ui_notification_warning", true, DialogResult.Cancel) != DialogResult.Yes)
					{
						return false;
					}
					if (this.ParentObject.IsTemporary)
					{
						base.PlayWorldSound("Sounds/Interact/sfx_interact_liquidContainer_drink", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
						if (E.Actor.IsPlayer())
						{
							IComponent<GameObject>.AddPlayerMessage("It's fizzy.", null, true);
						}
						this.Empty(false, false);
						return false;
					}
					if (this.ParentObject.FireEvent(Event.New("BeforeDrink", "Drinker", E.Actor), E))
					{
						int num = 0;
						if (E.Actor.IsPlayer() && this.ParentObject.Owner != null)
						{
							if (Popup.ShowYesNoCancel(string.Concat(new string[]
							{
								this.ParentObject.IndicativeDistal,
								this.ParentObject.Is,
								" not owned by you. Are you sure you want to drink from ",
								this.ParentObject.them,
								"?"
							}), "Sounds/UI/ui_notification", true, DialogResult.Cancel) != DialogResult.Yes)
							{
								return false;
							}
							this.ParentObject.Physics.BroadcastForHelp(E.Actor);
						}
						if (!E.Actor.FireEvent(Event.New("DrinkingFrom", "Container", this.ParentObject)))
						{
							return false;
						}
						bool flag3 = false;
						if (this.Volume > 0)
						{
							StringBuilder stringBuilder = Event.NewStringBuilder(null);
							int num2 = 0;
							int num3 = 0;
							Stomach part = E.Actor.GetPart<Stomach>();
							if (part != null)
							{
								num2 = part.Water;
								num3 = part.HungerLevel;
							}
							List<string> keylist = this.getKeylist();
							try
							{
								keylist.Clear();
								keylist.AddRange(this.ComponentLiquids.Keys);
								foreach (string text in keylist)
								{
									if (!LiquidVolume.Liquids[text].Drank(this, 1, E.Actor, stringBuilder, E))
									{
										if (stringBuilder.Length > 0)
										{
											Popup.ShowBlock(stringBuilder.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
										}
										return false;
									}
								}
							}
							finally
							{
								LiquidVolume.keylistPool.Enqueue(keylist);
							}
							if (this.IsFreshWater(false))
							{
								num = 1;
							}
							this.UseDram();
							if (part != null && (part.Water != num2 || part.HungerLevel != num3))
							{
								stringBuilder.Compound("You are now ");
								if (part.Water != num2)
								{
									stringBuilder.Append(part.WaterStatus());
									if (part.HungerLevel != num3)
									{
										stringBuilder.Append(" and ");
									}
								}
								if (part.HungerLevel != num3)
								{
									stringBuilder.Append(part.FoodStatus());
								}
								stringBuilder.Append('.');
							}
							base.PlayWorldSound("sfx_interact_liquidContainer_drink", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
							if (E.Actor.IsPlayer() && stringBuilder.Length > 0)
							{
								Popup.ShowBlock(stringBuilder.ToString(), null, "Sounds/UI/ui_notification", true, true, true, true, null);
							}
							if (this.Volume <= 0)
							{
								this.Empty(true, false);
							}
							flag3 = true;
						}
						else
						{
							E.Actor.Fail("It's empty, there's nothing left to drink!");
							this.Empty(true, false);
						}
						this.CheckImage();
						if (!flag3)
						{
							return false;
						}
						if (E.Actor != null)
						{
							if (E.Actor.HasRegisteredEvent("Drank"))
							{
								E.Actor.FireEvent(Event.New("Drank", "Object", this.ParentObject, "WasFreshWater", num));
							}
							E.Actor.UseEnergy(1000, "Item Drink", null, null, false);
							E.RequestInterfaceExit();
						}
					}
				}
			}
			else if (E.Command == "Pour")
			{
				GameObject gameObject = E.Actor;
				this.Pour(ref E.InterfaceExit, gameObject, null, false, false, -1, false);
			}
			else if (E.Command == "Fill")
			{
				this.PerformFill(E.Actor, ref E.InterfaceExit, E.OwnershipHandled);
			}
			else if (E.Command == "Drain")
			{
				if (!this.HasDrain)
				{
					E.Actor.Fail(this.ParentObject.Does("have", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " no drain.");
					return false;
				}
				if (this.EffectivelySealed())
				{
					E.Actor.Fail(this.ParentObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " sealed.");
					return false;
				}
				if (this.ParentObject.IsInStasis())
				{
					E.Actor.Fail("You cannot seem to interact with " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " in any way.");
					return false;
				}
				if (this.Volume <= 0)
				{
					E.Actor.Fail(this.ParentObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " empty.");
					return false;
				}
				if (!E.Actor.CheckFrozen(false, true, false, null))
				{
					return false;
				}
				if (E.Actor.IsPlayer())
				{
					if (this.ParentObject.Owner != null && !E.OwnershipHandled)
					{
						if (Popup.ShowYesNoCancel(string.Concat(new string[]
						{
							this.ParentObject.IndicativeDistal,
							this.ParentObject.Is,
							" not owned by you. Are you sure you want to drain ",
							this.ParentObject.them,
							"?"
						}), "Sounds/UI/ui_notification", true, DialogResult.Cancel) != DialogResult.Yes)
						{
							return false;
						}
						this.ParentObject.Physics.BroadcastForHelp(E.Actor);
					}
					else if (Popup.ShowYesNoCancel("Are you sure you want to drain " + this.ParentObject.t(2147483647, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + "?", "Sounds/UI/ui_notification", true, DialogResult.Cancel) != DialogResult.Yes)
					{
						return false;
					}
				}
				this.EmptyIntoCell(null, E.Actor);
			}
			else
			{
				if (E.Command == "FillFrom")
				{
					if (this.EffectivelySealed())
					{
						E.Actor.Fail(this.ParentObject.Does("are", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false) + " sealed.");
						return false;
					}
					if (this.ParentObject.IsInStasis())
					{
						E.Actor.Fail("You cannot seem to interact with " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " in any way.");
						return false;
					}
					if (!E.Actor.CheckFrozen(false, true, false, null))
					{
						return false;
					}
					if (E.Actor.IsPlayer() && this.ParentObject.Owner != null && !E.OwnershipHandled)
					{
						if (Popup.ShowYesNoCancel(string.Concat(new string[]
						{
							this.ParentObject.IndicativeDistal,
							this.ParentObject.Is,
							" not owned by you. Are you sure you want to fill ",
							this.ParentObject.them,
							"?"
						}), "Sounds/UI/ui_notification", true, DialogResult.Cancel) != DialogResult.Yes)
						{
							return false;
						}
						this.ParentObject.Physics.BroadcastForHelp(E.Actor);
					}
					List<GameObject> list = Event.NewGameObjectList();
					List<GameObject> list2 = Event.NewGameObjectList();
					List<GameObject> list3 = Event.NewGameObjectList();
					List<GameObject> list4 = Event.NewGameObjectList();
					E.Actor.GetInventoryAndEquipment(list4);
					int i = 0;
					int count = list4.Count;
					while (i < count)
					{
						GameObject gameObject2 = list4[i];
						if (gameObject2 != this.ParentObject)
						{
							LiquidVolume liquidVolume = gameObject2.LiquidVolume;
							if (liquidVolume != null && !liquidVolume.EffectivelySealed() && liquidVolume.Volume > 0)
							{
								if (liquidVolume.LiquidSameAs(this))
								{
									list.Add(gameObject2);
								}
								else
								{
									list2.Add(gameObject2);
								}
							}
						}
						i++;
					}
					list3.AddRange(list);
					list3.AddRange(list2);
					if (list3.Count == 0)
					{
						E.Actor.Fail("You have no containers to pour from.");
						return true;
					}
					GameObject gameObject3 = PickItem.ShowPicker(list3, null, PickItem.PickItemDialogStyle.SelectItemDialog, E.Actor, null, null, "[Select a container to fill from]", true, null, false, true, false);
					if (gameObject3 == null)
					{
						goto IL_1614;
					}
					try
					{
						this.ParentObject.SplitFromStack();
						if (gameObject3 == this.ParentObject)
						{
							E.Actor.Fail("You can't pour from a container into " + this.ParentObject.itself + ".");
							return false;
						}
						if (gameObject3.IsTemporary)
						{
							E.Actor.Fail("It's fizzy.");
							return false;
						}
						LiquidVolume liquidVolume2 = gameObject3.LiquidVolume;
						bool flag4 = false;
						if (this.Volume > 0 && !liquidVolume2.LiquidSameAs(this))
						{
							DialogResult dialogResult = Popup.ShowYesNoCancel("Do you want to empty " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " first?", "Sounds/UI/ui_notification", true, DialogResult.Cancel);
							if (dialogResult == DialogResult.Cancel)
							{
								return true;
							}
							if (dialogResult == DialogResult.Yes)
							{
								flag4 = true;
							}
						}
						int num4 = Math.Min(flag4 ? this.MaxVolume : (this.MaxVolume - this.Volume), liquidVolume2.Volume);
						int? num5 = Popup.AskNumber("How many drams? (max=" + num4.ToString() + ")", "Sounds/UI/ui_notification", "", num4, 0, num4);
						int num6 = 0;
						try
						{
							num6 = Convert.ToInt32(num5);
						}
						catch
						{
							return true;
						}
						if (num6 > liquidVolume2.Volume)
						{
							num6 = liquidVolume2.Volume;
						}
						if (num6 <= 0)
						{
							return true;
						}
						if (flag4)
						{
							this.EmptyIntoCell(null, E.Actor);
						}
						int num7 = this.MaxVolume - this.Volume;
						int num8;
						if (num7 < liquidVolume2.Volume)
						{
							num8 = num7;
						}
						else
						{
							num8 = liquidVolume2.Volume;
						}
						if (num8 > num6)
						{
							num8 = num6;
						}
						LiquidVolume liquidVolume3 = liquidVolume2;
						int? num9 = new int?(num8);
						GameObject gameObject = gameObject3;
						GameObject gameObject4 = E.Actor;
						this.MixWith(liquidVolume3, ref E.InterfaceExit, gameObject, gameObject4, num9, false);
						this.CheckImage();
						liquidVolume2.CheckImage();
						goto IL_1614;
					}
					finally
					{
						this.ParentObject.CheckStack();
					}
				}
				if (E.Command == "CollectLiquid")
				{
					if (this.Volume <= 0 || this.EffectivelySealed())
					{
						return false;
					}
					if (!E.Actor.CanMoveExtremities(null, true, false, true))
					{
						return false;
					}
					if (this.ParentObject.IsInStasis())
					{
						E.Actor.Fail("You cannot seem to interact with " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " in any way.");
						return false;
					}
					if (this.ParentObject.IsTemporary)
					{
						E.Actor.Fail("It's fizzy.");
						this.Empty(false, false);
						return false;
					}
					string liquidDesignation = this.GetLiquidDesignation(false);
					int num10 = (E.Auto ? E.Actor.GetAutoCollectDrams(liquidDesignation, this.ParentObject, null) : E.Actor.GetStorableDrams(liquidDesignation, this.ParentObject, null, null, true, null));
					if (num10 <= 0)
					{
						if (!E.Auto)
						{
							E.Actor.Fail("You have nowhere available to collect that.");
						}
					}
					else
					{
						if (E.Actor.IsPlayer() && this.ParentObject.Owner != null && !E.OwnershipHandled)
						{
							if (E.Auto || Popup.ShowYesNoCancel(string.Concat(new string[]
							{
								this.ParentObject.IndicativeDistal,
								this.ParentObject.Is,
								" not owned by you. Are you sure you want to collect from ",
								this.ParentObject.them,
								"?"
							}), "Sounds/UI/ui_notification", true, DialogResult.Cancel) != DialogResult.Yes)
							{
								goto IL_1614;
							}
							this.ParentObject.Physics.BroadcastForHelp(E.Actor);
						}
						int num11 = Math.Min(num10, this.Volume);
						if (!E.Actor.IsPlayer() || E.Auto || num11 <= 128 || Popup.ShowYesNoCancel(string.Concat(new string[]
						{
							"You are able to collect ",
							num11.Things("dram", null),
							" of ",
							this.GetLiquidName(),
							". Are you sure you want to?"
						}), "Sounds/UI/ui_notification", true, DialogResult.Cancel) == DialogResult.Yes)
						{
							List<GameObject> list5 = Event.NewGameObjectList();
							GameObject actor = E.Actor;
							int num12 = num11;
							string text2 = liquidDesignation;
							GameObject gameObject4 = this.ParentObject;
							actor.GiveDrams(num12, text2, E.Auto, gameObject4, null, null, list5, true, this);
							StringBuilder stringBuilder2 = Event.NewStringBuilder(null);
							stringBuilder2.Append(E.Actor.Does("collect", int.MaxValue, null, null, null, false, false, false, false, false, true, true, false, false, null, false, false, true, null, true, null, false)).Append(' ').Append(num11.Things("dram", null))
								.Append(" of ")
								.Append(this.GetLiquidName());
							if (this.IsOpenVolume())
							{
								stringBuilder2.Append(' ').Append(E.Actor.DescribeDirectionFrom(this.ParentObject, false));
							}
							else
							{
								stringBuilder2.Append(" from ").Append(this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false)).Append(' ')
									.Append(E.Actor.DescribeDirectionToward(this.ParentObject, false, false));
							}
							if (list5.Count > 0)
							{
								List<string> list6 = new List<string>();
								List<bool> list7 = new List<bool>();
								List<string> list8 = new List<string>();
								Dictionary<string, int> dictionary = new Dictionary<string, int>();
								foreach (GameObject gameObject5 in list5)
								{
									string displayNameOnly = gameObject5.DisplayNameOnly;
									if (gameObject5.HasProperName)
									{
										list8.Add(displayNameOnly);
									}
									else if (list6.Contains(displayNameOnly))
									{
										Dictionary<string, int> dictionary2 = dictionary;
										string text3 = displayNameOnly;
										int num13 = dictionary2[text3];
										dictionary2[text3] = num13 + 1;
									}
									else
									{
										list6.Add(displayNameOnly);
										list7.Add(gameObject5.IsPlural);
										dictionary.Add(displayNameOnly, 1);
									}
								}
								stringBuilder2.Append(" in ");
								if (list8.Count > 0)
								{
									stringBuilder2.Append(Grammar.MakeAndList(list8, true));
									if (list6.Count > 0)
									{
										stringBuilder2.Append(" and ");
									}
								}
								if (list6.Count > 0)
								{
									int j = 0;
									int count2 = list6.Count;
									while (j < count2)
									{
										if (!list7[j] && dictionary[list6[j]] > 1)
										{
											list6[j] = Grammar.Pluralize(list6[j]);
										}
										j++;
									}
									stringBuilder2.Append(E.Actor.its).Append(' ').Append(Grammar.MakeAndList(list6, true));
								}
							}
							stringBuilder2.Append('.');
							base.EmitMessage(E.Actor, stringBuilder2, ' ', !E.Auto, false, false, null, null);
							this.Volume -= num11;
							this.Update();
							E.Actor.UseEnergy(1000, "Item Liquid Collect", null, null, false);
						}
					}
				}
				else if (E.Command == "CleanWithLiquid")
				{
					bool flag5 = true;
					if (this.ParentObject.IsInStasis())
					{
						E.Actor.Fail("You cannot seem to interact with " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " in any way.");
						return false;
					}
					if (!E.Actor.CanMoveExtremities(null, true, false, true))
					{
						return false;
					}
					if (E.Actor.IsPlayer() && this.ParentObject.Owner != null && !E.OwnershipHandled)
					{
						if (Popup.ShowYesNoCancel(string.Concat(new string[]
						{
							this.ParentObject.IndicativeDistal,
							this.ParentObject.Is,
							" not owned by you. Are you sure you want to use ",
							this.GetLiquidName(),
							" from ",
							this.ParentObject.them,
							"?"
						}), "Sounds/UI/ui_notification", true, DialogResult.Cancel) != DialogResult.Yes)
						{
							flag5 = false;
						}
						else
						{
							this.ParentObject.Physics.BroadcastForHelp(E.Actor);
						}
					}
					if (flag5)
					{
						List<GameObject> list9 = null;
						List<string> list10 = null;
						if (this.UsableForCleaning())
						{
							CleanItemsEvent.PerformFor(E.Actor, E.Actor, this.ParentObject, out list9, out list10);
						}
						if (list9 != null && list9.Count > 0)
						{
							LiquidVolume.CleaningMessage(E.Actor, list9, list10, this.ParentObject, this, true);
							E.Actor.UseEnergy(1000, "Cleaning", null, null, false);
							E.RequestInterfaceExit();
						}
						else
						{
							E.Actor.Fail("You cannot do that for some reason.");
						}
					}
				}
				else
				{
					if (E.Command == "AutoCollectLiquid")
					{
						if (this.AutoCollectLiquidType != null)
						{
							this.AutoCollectLiquidType = null;
							goto IL_1614;
						}
						if (this.Sealed || this.ComponentLiquids.Count > 1)
						{
							E.Actor.Fail("Auto collection only works on unsealed containers with pure liquids.");
							goto IL_1614;
						}
						if (this.ComponentLiquids.Count == 0)
						{
							this.AutoCollectLiquidType = GetPreferredLiquidEvent.GetFor(this.ParentObject, E.Actor);
							if (this.AutoCollectLiquidType == null)
							{
								E.Actor.Fail("It isn't clear what kind of liquid would be appropriate for " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " to collect. Pour a pure liquid into it, and then enable auto-collect.");
								goto IL_1614;
							}
							goto IL_1614;
						}
						else
						{
							using (Dictionary<string, int>.KeyCollection.Enumerator enumerator3 = this.ComponentLiquids.Keys.GetEnumerator())
							{
								if (!enumerator3.MoveNext())
								{
									goto IL_1614;
								}
								string text4 = enumerator3.Current;
								this.AutoCollectLiquidType = text4;
								goto IL_1614;
							}
						}
					}
					if (E.Command == "Seal")
					{
						if (!this.Sealed)
						{
							if (!E.Actor.CheckFrozen(false, true, false, null))
							{
								return false;
							}
							if (this.ParentObject.IsInStasis())
							{
								E.Actor.Fail("You cannot seem to interact with " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " in any way.");
								return false;
							}
							this.Sealed = true;
							base.PlayWorldSound("Sounds/Interact/sfx_interact_liquidContainer_seal", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
							if (!this.EffectivelySealed())
							{
								IComponent<GameObject>.XDidYToZ(E.Actor, "seal", this.ParentObject, string.Concat(new string[]
								{
									"as best ",
									E.Actor.it,
									" can, but in ",
									this.ParentObject.its,
									" condition this does little"
								}), null, null, null, null, null, false, false, false, false, false, null, null, null, false, false, false, true, false, null);
							}
							else
							{
								IComponent<GameObject>.XDidYToZ(E.Actor, "seal", this.ParentObject, null, null, null, null, null, null, false, false, false, false, false, null, null, null, false, false, false, true, false, null);
							}
							E.Actor.UseEnergy(1000, "Item Seal", null, null, false);
						}
					}
					else if (E.Command == "Unseal" && this.Sealed)
					{
						if (!E.Actor.CheckFrozen(false, true, false, null))
						{
							return false;
						}
						if (this.ParentObject.IsInStasis())
						{
							E.Actor.Fail("You cannot seem to interact with " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " in any way.");
							return false;
						}
						base.PlayWorldSound("Sounds/Interact/sfx_interact_liquidContainer_unseal", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
						this.Sealed = false;
						IComponent<GameObject>.XDidYToZ(E.Actor, "unseal", this.ParentObject, null, null, null, null, null, null, false, false, false, false, false, null, null, null, false, false, false, true, false, null);
						E.Actor.UseEnergy(1000, "Item Unseal", null, null, false);
					}
				}
			}
			IL_1614:
			return base.HandleEvent(E);
		}

		// Token: 0x06008DDD RID: 36317 RVA: 0x0032AB90 File Offset: 0x00328D90
		private static string GetNameForCleaningMessage(GameObject Object)
		{
			return Object.GetDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, true, false, null, false, false, true, false, null, true, null, false, true) ?? "something";
		}

		// Token: 0x06008DDE RID: 36318 RVA: 0x0032ABD4 File Offset: 0x00328DD4
		public static void CleaningMessage(GameObject Actor, List<GameObject> Objects, List<string> Types = null, GameObject Source = null, LiquidVolume LiquidVolume = null, bool UseDram = false)
		{
			List<string> list = new List<string>(Objects.Count);
			if (Objects.Contains(Actor))
			{
				list.Add(Actor.itself);
			}
			foreach (GameObject gameObject in Objects)
			{
				if (gameObject != Actor && gameObject.HasProperName)
				{
					list.Add(LiquidVolume.GetNameForCleaningMessage(gameObject));
				}
			}
			foreach (GameObject gameObject2 in Objects)
			{
				if (gameObject2 != Actor && !gameObject2.HasProperName && gameObject2.Equipped == Actor)
				{
					list.Add(LiquidVolume.GetNameForCleaningMessage(gameObject2));
				}
			}
			foreach (GameObject gameObject3 in Objects)
			{
				if (gameObject3 != Actor && !gameObject3.HasProperName && gameObject3.Equipped != Actor)
				{
					list.Add(LiquidVolume.GetNameForCleaningMessage(gameObject3));
				}
			}
			foreach (GameObject gameObject4 in Objects)
			{
				gameObject4.CheckStack();
			}
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			stringBuilder.Append("the ");
			if (Types.IsNullOrEmpty<string>())
			{
				stringBuilder.Append("mess");
			}
			else
			{
				stringBuilder.Append(Grammar.MakeAndList(Types, true));
			}
			stringBuilder.Append(" from ").Append(Grammar.MakeAndList(list, true));
			if (LiquidVolume == null)
			{
				LiquidVolume = ((Source != null) ? Source.LiquidVolume : null);
			}
			if (LiquidVolume != null)
			{
				stringBuilder.Append(" with a dram of ");
				LiquidVolume.AppendLiquidName(stringBuilder, false);
				if (Source != null)
				{
					if (LiquidVolume.IsOpenVolume())
					{
						if (Source.CurrentCell != null)
						{
							stringBuilder.Append(' ').Append(Actor.DescribeDirectionFrom(Source, false));
						}
					}
					else
					{
						stringBuilder.Append(" from ").Append(Source.GetDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, true, false, false, true, false, false, true, null, false, false, true, false, null, true, null, false, true));
						if (Source.CurrentCell != null)
						{
							stringBuilder.Append(' ').Append(Actor.DescribeDirectionToward(Source, false, false));
						}
					}
				}
			}
			else if (Source != null)
			{
				stringBuilder.Append(" with ").Append(LiquidVolume.GetNameForCleaningMessage(Source));
			}
			IComponent<GameObject>.XDidY(Actor, "clean", stringBuilder.ToString(), null, null, null, null, null, false, false, null, null, false, false, false, false, false, null);
			if (UseDram && LiquidVolume != null)
			{
				LiquidVolume.FlowIntoCell(1, Actor.GetCurrentCell(), Actor, false, false, true);
			}
		}

		// Token: 0x06008DDF RID: 36319 RVA: 0x0032AEAC File Offset: 0x003290AC
		public override bool HandleEvent(CanSmartUseEvent E)
		{
			if (this.IsOpenVolume() || (!this.ParentObject.IsCreature && !this.EffectivelySealed()))
			{
				return !this.IsFreshWater(false) && E.MinPriority > 0;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008DE0 RID: 36320 RVA: 0x0032AEE8 File Offset: 0x003290E8
		public override bool HandleEvent(CommandSmartUseEvent E)
		{
			if (this.IsOpenVolume() || (!this.ParentObject.IsCreature && !this.EffectivelySealed()))
			{
				this.ParentObject.Twiddle(false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008DE1 RID: 36321 RVA: 0x0032AF1C File Offset: 0x0032911C
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			if (!this.StartVolume.IsNullOrEmpty())
			{
				this.Volume = this.StartVolume.RollCached();
			}
			if (this.Volume == 0)
			{
				this.Empty(false, true);
			}
			else
			{
				this.CheckImage();
			}
			this.SyncPhysicalProperties();
			return base.HandleEvent(E);
		}

		// Token: 0x06008DE2 RID: 36322 RVA: 0x0032AF6D File Offset: 0x0032916D
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			this.CheckImage();
			return base.HandleEvent(E);
		}

		// Token: 0x06008DE3 RID: 36323 RVA: 0x0032AF7D File Offset: 0x0032917D
		public override bool HandleEvent(EffectRemovedEvent E)
		{
			this.CheckImage();
			return base.HandleEvent(E);
		}

		// Token: 0x06008DE4 RID: 36324 RVA: 0x0032AF90 File Offset: 0x00329190
		public override bool HandleEvent(StripContentsEvent E)
		{
			if (!this.IsEmpty() && (!E.KeepNatural || this.ParentObject == null || this.GetLiquidDesignation(true) != this.ParentObject.GetBlueprint(true).GetPartParameter<string>("LiquidVolume", "InitialLiquid", null)))
			{
				this.Empty(false, false);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008DE5 RID: 36325 RVA: 0x0032AFF0 File Offset: 0x003291F0
		public override bool HandleEvent(OnDestroyObjectEvent E)
		{
			if (!this.IsOpenVolume() && !E.Obliterate)
			{
				this.MaxVolume = -1;
				GameObject holder = this.ParentObject.Holder;
				Cell cell = ((holder != null) ? holder.GetCellContext() : null);
				Cell cell2;
				if ((cell2 = this.ParentObject.GetCellContext()) == null)
				{
					GameObject objectContext = this.ParentObject.GetObjectContext();
					cell2 = ((objectContext != null) ? objectContext.GetCellContext() : null) ?? cell;
				}
				Cell cell3 = cell2;
				if (cell3 != null)
				{
					GameObject gameObject = holder;
					Cell cell4 = cell3;
					bool flag = cell == cell3;
					this.Pour(ref E.InterfaceExit, gameObject, cell4, true, flag, -1, false);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008DE6 RID: 36326 RVA: 0x0032B083 File Offset: 0x00329283
		public override bool HandleEvent(FellDownEvent E)
		{
			if (this.IsOpenVolume())
			{
				this.EmptyIntoCell(E.Cell, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008DE7 RID: 36327 RVA: 0x0032B0A4 File Offset: 0x003292A4
		public override bool HandleEvent(VaporizedEvent E)
		{
			if (this.Volume > 0)
			{
				List<string> keylist = this.getKeylist();
				try
				{
					keylist.Clear();
					keylist.AddRange(this.ComponentLiquids.Keys);
					foreach (string text in keylist)
					{
						if (!LiquidVolume.Liquids[text].Vaporized(this, E.By))
						{
							return false;
						}
					}
				}
				finally
				{
					LiquidVolume.keylistPool.Enqueue(keylist);
					this.CheckImage();
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008DE8 RID: 36328 RVA: 0x0032B158 File Offset: 0x00329358
		public override bool HandleEvent(FrozeEvent E)
		{
			if (this.Volume > 0)
			{
				List<string> keylist = this.getKeylist();
				try
				{
					keylist.Clear();
					keylist.AddRange(this.ComponentLiquids.Keys);
					foreach (string text in keylist)
					{
						if (LiquidVolume.Liquids.ContainsKey(text) && !LiquidVolume.Liquids[text].Froze(this, E.By))
						{
							return false;
						}
					}
				}
				finally
				{
					LiquidVolume.keylistPool.Enqueue(keylist);
					this.CheckImage();
				}
				Cell currentCell = this.ParentObject.CurrentCell;
				string text2;
				string liquidFreezeObject = this.GetLiquidFreezeObject(out text2);
				if (!string.IsNullOrEmpty(liquidFreezeObject) && this.ParentObject.IsValid())
				{
					int num = this.Volume * this.ParentObject.Count / 20;
					base.PlayWorldSound("Sounds/StatusEffects/sfx_statusEffect_frozen", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
					this.ParentObject.Physics.LastDamagedByType = "Frozen";
					this.ParentObject.Physics.LastDamageAccidental = true;
					this.ParentObject.Die(E.By, null, "You froze.", this.ParentObject.It + " @@froze.", true, null, null, false, false, text2.IsNullOrEmpty() ? "" : null, text2.IsNullOrEmpty() ? null : text2, null);
					if (currentCell != null)
					{
						GameObject gameObject = GameObject.Create(liquidFreezeObject, 0, 0, null, null, null, "Freeze", null);
						if (gameObject == null)
						{
							MetricsManager.LogError("Unknown freeze object " + liquidFreezeObject);
							return true;
						}
						Gas part = gameObject.GetPart<Gas>();
						if (part != null)
						{
							part.Density = num;
							if (GameObject.Validate(E.By))
							{
								part.Creator = E.By;
							}
						}
						currentCell.AddObject(gameObject, false, false, false, false, false, true, true, null, null, null, null, null, null, null, null);
						int i = 0;
						int count = currentCell.Objects.Count;
						while (i < count)
						{
							GameObject gameObject2 = currentCell.Objects[i];
							if (gameObject2 != null && gameObject2 != gameObject && gameObject2.IsCombatObject() && gameObject.ConsiderSolidFor(gameObject2, false))
							{
								Stuck stuck = new Stuck(30, 25, "Frozen Stuck Restraint", null, "stuck", "in", gameObject.ID, "Move", 0.25f, 1f, 1f, false, true);
								if (gameObject2.ApplyEffect(stuck, null))
								{
									gameObject2.RemoveAllEffects<LiquidCovered>(true);
									foreach (GameObject gameObject3 in gameObject2.GetInventoryAndEquipment())
									{
										gameObject3.RemoveAllEffects<LiquidCovered>(true);
									}
								}
							}
							i++;
						}
					}
					return false;
				}
				if (this.CanInteractWithAnything(currentCell) && this.ParentObject.CurrentCell != null)
				{
					foreach (GameObject gameObject4 in this.ParentObject.CurrentCell.GetObjectsWithPartReadonly("Combat"))
					{
						if (this.CanInteractWith(gameObject4, currentCell, true) && this.Volume >= gameObject4.GetMaximumLiquidExposure() / 8)
						{
							Stuck stuck2 = new Stuck(5, 15, "Frozen Stuck Restraint", null, "stuck", "in", this.ParentObject.ID, "Move", 0.25f, 1f, 1f, true, false);
							gameObject4.ApplyEffect(stuck2, null);
						}
					}
				}
			}
			IL_039A:
			return base.HandleEvent(E);
		}

		// Token: 0x06008DE9 RID: 36329 RVA: 0x0032B540 File Offset: 0x00329740
		public override bool HandleEvent(ThawedEvent E)
		{
			if (this.Volume > 0)
			{
				List<string> keylist = this.getKeylist();
				try
				{
					keylist.Clear();
					keylist.AddRange(this.ComponentLiquids.Keys);
					foreach (string text in keylist)
					{
						if (!LiquidVolume.Liquids[text].Thawed(this, E.By))
						{
							return false;
						}
					}
				}
				finally
				{
					LiquidVolume.keylistPool.Enqueue(keylist);
					this.CheckImage();
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008DEA RID: 36330 RVA: 0x0032B5F4 File Offset: 0x003297F4
		public override bool HandleEvent(PollForHealingLocationEvent E)
		{
			if (this.Volume > 0)
			{
				List<string> keylist = this.getKeylist();
				try
				{
					keylist.Clear();
					keylist.AddRange(this.ComponentLiquids.Keys);
					foreach (string text in keylist)
					{
						int healingLocationValue = LiquidVolume.Liquids[text].GetHealingLocationValue(this, E.Actor);
						if (healingLocationValue > E.Value)
						{
							E.Value = healingLocationValue;
							if (E.First)
							{
								return false;
							}
						}
					}
				}
				finally
				{
					LiquidVolume.keylistPool.Enqueue(keylist);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008DEB RID: 36331 RVA: 0x0032B6BC File Offset: 0x003298BC
		public override bool HandleEvent(InterruptAutowalkEvent E)
		{
			if (this.IsSwimmingDepth() && Options.ConfirmSwimming && !E.Actor.HasEffect<Swimming>() && this.CanInteractWith(E.Actor, E.Cell, false))
			{
				E.IndicateObject = this.ParentObject;
				return false;
			}
			if (Options.ConfirmDangerousLiquid)
			{
				foreach (string text in this.ComponentLiquids.Keys)
				{
					if (LiquidVolume.Liquids[text].InterruptAutowalk)
					{
						if (this.CanInteractWith(E.Actor, E.Cell, false) && !this.EffectivelySealed() && this.IsOpenVolume())
						{
							E.IndicateObject = this.ParentObject;
							return false;
						}
						break;
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008DEC RID: 36332 RVA: 0x0032B7A4 File Offset: 0x003299A4
		public override bool HandleEvent(CheckAnythingToCleanWithEvent E)
		{
			return !this.UsableForCleaning() && base.HandleEvent(E);
		}

		// Token: 0x06008DED RID: 36333 RVA: 0x0032B7B7 File Offset: 0x003299B7
		public override bool HandleEvent(CheckAnythingToCleanWithNearbyEvent E)
		{
			return !this.UsableForCleaning() && base.HandleEvent(E);
		}

		// Token: 0x06008DEE RID: 36334 RVA: 0x0032B7CA File Offset: 0x003299CA
		public override bool HandleEvent(GetCleaningItemsEvent E)
		{
			if (this.UsableForCleaning())
			{
				E.Objects.Add(this.ParentObject);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008DEF RID: 36335 RVA: 0x0032B7EC File Offset: 0x003299EC
		public override bool HandleEvent(GetCleaningItemsNearbyEvent E)
		{
			if (this.UsableForCleaning())
			{
				E.Objects.Add(this.ParentObject);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008DF0 RID: 36336 RVA: 0x0032B80E File Offset: 0x00329A0E
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x06008DF1 RID: 36337 RVA: 0x0032B811 File Offset: 0x00329A11
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.ProcessTurns(Amount);
		}

		// Token: 0x06008DF2 RID: 36338 RVA: 0x0032B81C File Offset: 0x00329A1C
		public void ProcessTurns(int Turns)
		{
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell != null && currentCell.ParentZone != null && currentCell.ParentZone.IsActive())
			{
				if (this.Primary == "wax" && this.IsOpenVolume() && this.ParentObject.Physics.Temperature < 25 && !currentCell.IsOccluding())
				{
					if (this.Volume >= 200)
					{
						currentCell.AddObject("Wax Block", null, null, null, null);
					}
					else
					{
						currentCell.AddObject("Wax Nodule", null, null, null, null);
					}
					this.ParentObject.Destroy(null, true, false, null);
				}
				List<string> list = null;
				try
				{
					if (this.ComponentLiquids.Count == 1 && this.Primary != null)
					{
						this.RequirePrimaryLiquid().ProcessTurns(this, this.ParentObject, Turns);
					}
					else if (this.ComponentLiquids.Count == 2 && this.Primary != null && this.Secondary != null)
					{
						this.RequirePrimaryLiquid().ProcessTurns(this, this.ParentObject, Turns);
						if (this.Secondary != null)
						{
							this.RequireSecondaryLiquid().ProcessTurns(this, this.ParentObject, Turns);
						}
					}
					else
					{
						list = this.getKeylist();
						list.Clear();
						list.AddRange(this.ComponentLiquids.Keys);
						foreach (string text in list)
						{
							LiquidVolume.Liquids[text].ProcessTurns(this, this.ParentObject, Turns);
						}
					}
					if (this.Volume > 0 && this.CanInteractWithAnything(currentCell))
					{
						int i = 0;
						int num = currentCell.Objects.Count;
						while (i < num)
						{
							GameObject gameObject = currentCell.Objects[i];
							if (this.CanInteractWith(gameObject, currentCell, true))
							{
								if (this.ComponentLiquids.Count == 1 && this.Primary != null)
								{
									this.RequirePrimaryLiquid().ObjectInCell(this, gameObject);
								}
								else if (this.ComponentLiquids.Count == 2 && this.Primary != null && this.Secondary != null)
								{
									this.RequirePrimaryLiquid().ObjectInCell(this, gameObject);
									if (this.Secondary != null)
									{
										this.RequireSecondaryLiquid().ObjectInCell(this, gameObject);
									}
								}
								else
								{
									if (list == null)
									{
										list = this.getKeylist();
									}
									foreach (string text2 in list)
									{
										if (this.ComponentLiquids.ContainsKey(text2))
										{
											LiquidVolume.Liquids[text2].ObjectInCell(this, gameObject);
										}
									}
								}
								if (this.IsOpenVolume())
								{
									this.ProcessContact(gameObject, false, gameObject.HasEffect<Prone>(), false, null, true, -1);
								}
							}
							if (num != currentCell.Objects.Count)
							{
								num = currentCell.Objects.Count;
								if (i < num && currentCell.Objects[i] != gameObject)
								{
									i--;
								}
							}
							i++;
						}
					}
					if (this.Volume > 0 && this.CanEvaporate())
					{
						int liquidEvaporativity = this.GetLiquidEvaporativity();
						if (liquidEvaporativity > 0)
						{
							int num2 = 0;
							for (int j = 0; j < this.Volume; j++)
							{
								if (liquidEvaporativity.in100())
								{
									num2++;
								}
							}
							if (num2 > 0)
							{
								this.UseDramsByEvaporativity(num2);
							}
						}
					}
					return;
				}
				finally
				{
					if (list != null)
					{
						LiquidVolume.keylistPool.Enqueue(list);
					}
				}
			}
			if (this.ParentObject != null)
			{
				if (this.ComponentLiquids.Count == 1 && this.Primary != null)
				{
					this.RequirePrimaryLiquid().ProcessTurns(this, this.ParentObject, Turns);
					return;
				}
				if (this.ComponentLiquids.Count == 2 && this.Primary != null && this.Secondary != null)
				{
					this.RequirePrimaryLiquid().ProcessTurns(this, this.ParentObject, Turns);
					if (this.Secondary != null)
					{
						this.RequireSecondaryLiquid().ProcessTurns(this, this.ParentObject, Turns);
						return;
					}
				}
				else
				{
					List<string> keylist = this.getKeylist();
					try
					{
						keylist.Clear();
						keylist.AddRange(this.ComponentLiquids.Keys);
						foreach (string text3 in keylist)
						{
							LiquidVolume.Liquids[text3].ProcessTurns(this, this.ParentObject, Turns);
						}
					}
					finally
					{
						LiquidVolume.keylistPool.Enqueue(keylist);
					}
				}
			}
		}

		// Token: 0x06008DF3 RID: 36339 RVA: 0x0032BCFC File Offset: 0x00329EFC
		public void RenderSmearWithNativeSupplied(RenderEvent E, GameObject obj, string native)
		{
			if (this.Primary != null || this.Secondary != null)
			{
				if (this.Primary != null && native != this.Primary)
				{
					this.RequirePrimaryLiquid().RenderSmearPrimary(this, E, obj);
				}
				if (this.Secondary != null && native != this.Secondary)
				{
					this.RequireSecondaryLiquid().RenderSmearSecondary(this, E, obj);
				}
			}
		}

		// Token: 0x06008DF4 RID: 36340 RVA: 0x0032BD64 File Offset: 0x00329F64
		public void RenderSmear(RenderEvent E, GameObject obj)
		{
			if (this.Primary != null || this.Secondary != null)
			{
				string text = ((obj != null) ? obj.GetPropertyOrTag("LiquidNative", null) : null);
				if (this.Primary != null && text != this.Primary)
				{
					this.RequirePrimaryLiquid().RenderSmearPrimary(this, E, obj);
				}
				if (this.Secondary != null && text != this.Secondary)
				{
					this.RequireSecondaryLiquid().RenderSmearSecondary(this, E, obj);
				}
			}
		}

		// Token: 0x06008DF5 RID: 36341 RVA: 0x0032BDDC File Offset: 0x00329FDC
		public void RenderStain(RenderEvent E, GameObject obj)
		{
			if (this.Primary != null)
			{
				string color = this.RequirePrimaryLiquid().GetColor();
				if (color != null)
				{
					E.ColorString = "&" + color;
				}
			}
			if (this.Secondary != null)
			{
				string color2 = this.RequireSecondaryLiquid().GetColor();
				if (color2 != null)
				{
					E.DetailColor = color2;
				}
			}
		}

		// Token: 0x06008DF6 RID: 36342 RVA: 0x0032BE30 File Offset: 0x0032A030
		public void Empty(bool WillCheckImage = false, bool NoDestroy = false)
		{
			if (!NoDestroy && this.IsOpenVolume())
			{
				GameObject parentObject = this.ParentObject;
				if (((parentObject != null) ? parentObject.CurrentCell : null) != null)
				{
					this.ParentObject.Destroy(null, true, true, null);
					return;
				}
			}
			if (this.Volume != 0)
			{
				this.Volume = 0;
				base.FlushWeightCaches();
			}
			this.ComponentLiquids.Clear();
			if (!WillCheckImage)
			{
				this.CheckImage();
			}
		}

		// Token: 0x06008DF7 RID: 36343 RVA: 0x0032BE97 File Offset: 0x0032A097
		public void EmptyIntoCell(Cell C = null, GameObject Pourer = null)
		{
			this.PourIntoCell(Pourer, C ?? this.ParentObject.GetCurrentCell(), this.Volume, true, false, false, false);
			this.Empty(false, false);
		}

		// Token: 0x06008DF8 RID: 36344 RVA: 0x0032BEC3 File Offset: 0x0032A0C3
		public int GetLiquidCount()
		{
			return this.ComponentLiquids.Count;
		}

		// Token: 0x06008DF9 RID: 36345 RVA: 0x0032BED0 File Offset: 0x0032A0D0
		public string GetLiquidName()
		{
			if (this.ComponentLiquids.Count == 1)
			{
				if (this.Primary != null)
				{
					return this.RequirePrimaryLiquid().GetName(this);
				}
				using (Dictionary<string, int>.KeyCollection.Enumerator enumerator = this.ComponentLiquids.Keys.GetEnumerator())
				{
					if (!enumerator.MoveNext())
					{
						goto IL_0179;
					}
					string text = enumerator.Current;
					return LiquidVolume.Liquids[text].GetName(this);
				}
			}
			if (this.ComponentLiquids.Count > 1)
			{
				StringBuilder stringBuilder = Event.NewStringBuilder(null);
				int num = 0;
				string text2 = null;
				foreach (KeyValuePair<string, int> keyValuePair in this.ComponentLiquids)
				{
					if (keyValuePair.Value > num || (keyValuePair.Value == num && keyValuePair.Key.CompareTo(text2) < 0))
					{
						text2 = keyValuePair.Key;
						num = keyValuePair.Value;
					}
				}
				foreach (string text3 in this.ComponentLiquids.Keys)
				{
					if (text3 != text2)
					{
						stringBuilder.Append(LiquidVolume.Liquids[text3].GetAdjective(this)).Append(' ');
					}
				}
				stringBuilder.Append(LiquidVolume.Liquids[text2].GetName(this));
				return stringBuilder.ToString();
			}
			IL_0179:
			return "";
		}

		// Token: 0x06008DFA RID: 36346 RVA: 0x0032C088 File Offset: 0x0032A288
		private int CompareProportions(string a, string b)
		{
			return -this.ComponentLiquids[a].CompareTo(this.ComponentLiquids[b]);
		}

		// Token: 0x06008DFB RID: 36347 RVA: 0x0032C0B8 File Offset: 0x0032A2B8
		public List<string> GetTertiaries()
		{
			int num = this.ComponentLiquids.Count;
			if (this.Primary != null)
			{
				num--;
			}
			if (this.Secondary != null)
			{
				num--;
			}
			if (num <= 0)
			{
				return null;
			}
			List<string> list = new List<string>(num);
			foreach (string text in this.ComponentLiquids.Keys)
			{
				if (text != this.Primary && text != this.Secondary)
				{
					list.Add(text);
				}
			}
			if (num > 1)
			{
				list.Sort(new Comparison<string>(this.CompareProportions));
			}
			return list;
		}

		// Token: 0x06008DFC RID: 36348 RVA: 0x0032C174 File Offset: 0x0032A374
		public void AppendLiquidName(StringBuilder SB, bool Stripped = false)
		{
			if (this.ComponentLiquids.Count == 0)
			{
				return;
			}
			if (this.ComponentLiquids.Count == 1)
			{
				if (this.Primary != null)
				{
					SB.Append(LiquidVolume.Liquids[this.Primary].GetName(this));
					goto IL_0161;
				}
				using (Dictionary<string, int>.KeyCollection.Enumerator enumerator = this.ComponentLiquids.Keys.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string text = enumerator.Current;
						SB.Append(LiquidVolume.Liquids[text].GetName(this));
					}
					goto IL_0161;
				}
			}
			bool flag = false;
			if (this.Secondary != null)
			{
				string adjective = this.RequireSecondaryLiquid().GetAdjective(this);
				if (!adjective.IsNullOrEmpty())
				{
					SB.Append(adjective);
					flag = true;
				}
			}
			List<string> tertiaries = this.GetTertiaries();
			if (tertiaries != null)
			{
				foreach (string text2 in tertiaries)
				{
					string adjective2 = LiquidVolume.Liquids[text2].GetAdjective(this);
					if (!adjective2.IsNullOrEmpty())
					{
						if (flag)
						{
							SB.Append(' ');
						}
						else
						{
							flag = true;
						}
						SB.Append(adjective2);
					}
				}
			}
			if (this.Primary != null)
			{
				string name = this.RequirePrimaryLiquid().GetName(this);
				if (!name.IsNullOrEmpty())
				{
					if (flag)
					{
						SB.Append(' ');
					}
					SB.Append(name);
				}
			}
			IL_0161:
			if (Stripped)
			{
				ColorUtility.StripFormatting(SB);
			}
		}

		// Token: 0x06008DFD RID: 36349 RVA: 0x0032C308 File Offset: 0x0032A508
		public string GetDescriptionPreposition()
		{
			if (this.ParentObject != null && !this.ParentObject.Understood())
			{
				return this.UnknownNamePreposition;
			}
			return this.NamePreposition;
		}

		// Token: 0x06008DFE RID: 36350 RVA: 0x0032C32C File Offset: 0x0032A52C
		public bool UsesNamePreposition()
		{
			return !this.GetDescriptionPreposition().IsNullOrEmpty();
		}

		// Token: 0x06008DFF RID: 36351 RVA: 0x0032C33C File Offset: 0x0032A53C
		public void AppendLiquidDescription(StringBuilder SB, bool IncludeAmount = true, bool IgnoreSeal = false, bool Stripped = false, bool Syntactic = false, bool ForSort = false, string UsePreposition = null)
		{
			if (UsePreposition == null)
			{
				UsePreposition = this.GetDescriptionPreposition();
			}
			bool flag = !IgnoreSeal && this.EffectivelySealed();
			if (!flag || this.LiquidVisibleWhenSealed || this.ShowSeal)
			{
				if (!Syntactic && UsePreposition.IsNullOrEmpty())
				{
					SB.Append(Stripped ? "[" : "{{y|[");
				}
				else if ((!flag || this.LiquidVisibleWhenSealed) && !UsePreposition.IsNullOrEmpty())
				{
					SB.Append(UsePreposition).Append(' ');
				}
				if (!flag || this.LiquidVisibleWhenSealed)
				{
					if (IncludeAmount)
					{
						if (ForSort)
						{
							if (Stripped)
							{
								SB.Append(this.Volume.ToString("D6")).Append((this.Volume == 1) ? " dram of " : " drams of ");
							}
							else
							{
								SB.Append("{{rules|").Append(this.Volume.ToString("D6")).Append("}}")
									.Append((this.Volume == 1) ? " dram of " : " drams of ");
							}
						}
						else if (Stripped)
						{
							SB.Append(this.Volume).Append((this.Volume == 1) ? " dram of " : " drams of ");
						}
						else
						{
							SB.Append("{{rules|").Append(this.Volume).Append("}}")
								.Append((this.Volume == 1) ? " dram of " : " drams of ");
						}
					}
					this.AppendLiquidName(SB, Stripped);
				}
				if (flag && this.ShowSeal)
				{
					if (this.LiquidVisibleWhenSealed)
					{
						SB.Append(", ");
					}
					SB.Append(Stripped ? "sealed" : "{{c|sealed}}");
				}
				if (!Syntactic && UsePreposition.IsNullOrEmpty())
				{
					SB.Append(Stripped ? "]" : "]}}");
				}
			}
		}

		// Token: 0x06008E00 RID: 36352 RVA: 0x0032C52C File Offset: 0x0032A72C
		public string GetLiquidDescription(bool IncludeAmount = true, bool IgnoreSeal = false, bool Stripped = false, bool Syntactic = false, bool ForSort = false, string UsePreposition = null)
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			this.AppendLiquidDescription(stringBuilder, IncludeAmount, IgnoreSeal, Stripped, Syntactic, ForSort, UsePreposition);
			return stringBuilder.ToString();
		}

		// Token: 0x06008E01 RID: 36353 RVA: 0x0032C556 File Offset: 0x0032A756
		public bool IsOpenVolume()
		{
			return this.MaxVolume == -1;
		}

		// Token: 0x06008E02 RID: 36354 RVA: 0x0032C561 File Offset: 0x0032A761
		public bool IsWadingDepth()
		{
			return this.IsOpenVolume() && this.Volume >= 200;
		}

		// Token: 0x06008E03 RID: 36355 RVA: 0x0032C57D File Offset: 0x0032A77D
		public bool IsSwimmingDepth()
		{
			return this.IsOpenVolume() && this.Volume >= 2000;
		}

		// Token: 0x06008E04 RID: 36356 RVA: 0x0032C599 File Offset: 0x0032A799
		public bool IsSwimmableFor(GameObject who)
		{
			return this.IsOpenVolume() && this.IsWadingDepth() && (this.IsSwimmingDepth() || Swimming.GetTargetMoveSpeedPenalty(who) <= 0);
		}

		// Token: 0x06008E05 RID: 36357 RVA: 0x0032C5C3 File Offset: 0x0032A7C3
		public void BaseRender()
		{
			if (this.ParentObject != null && this.IsOpenVolume())
			{
				if (this.Primary != null)
				{
					this.RequirePrimaryLiquid().BaseRenderPrimary(this);
				}
				if (this.Secondary != null)
				{
					this.RequireSecondaryLiquid().BaseRenderSecondary(this);
				}
			}
		}

		// Token: 0x06008E06 RID: 36358 RVA: 0x0032C5FD File Offset: 0x0032A7FD
		public override bool Render(RenderEvent E)
		{
			if (this.ParentObject != null && this.IsOpenVolume())
			{
				if (this.Primary != null)
				{
					this.RequirePrimaryLiquid().RenderPrimary(this, E);
				}
				if (this.Secondary != null)
				{
					this.RequireSecondaryLiquid().RenderSecondary(this, E);
				}
			}
			return true;
		}

		// Token: 0x06008E07 RID: 36359 RVA: 0x0032C63A File Offset: 0x0032A83A
		public string GetPrimaryLiquidColor()
		{
			if (this.Primary != null)
			{
				return this.RequirePrimaryLiquid().GetColor();
			}
			return null;
		}

		// Token: 0x06008E08 RID: 36360 RVA: 0x0032C654 File Offset: 0x0032A854
		public bool CheckImage()
		{
			if (this.ParentObject == null || this.ParentObject.Render == null)
			{
				return false;
			}
			bool flag = false;
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell != null)
			{
				this.LastPaintMask = -1;
				currentCell.CheckPaintLiquidsAround(this.ParentObject);
			}
			if (this.IsWadingDepth())
			{
				if (!this.ParentObject.Render.RenderIfDark || this.ParentObject.Render.RenderString != "~")
				{
					this.ParentObject.Render.RenderIfDark = true;
					this.ParentObject.Render.RenderString = "~";
					flag = true;
				}
			}
			else if (this.IsOpenVolume())
			{
				if (this.ParentObject.Render.RenderIfDark)
				{
					this.ParentObject.Render.RenderIfDark = false;
					flag = true;
				}
				string randomElement = LiquidVolume.PuddleRenderStrings.GetRandomElement<string>(null);
				if (this.ParentObject.Render.RenderString != randomElement)
				{
					this.ParentObject.Render.RenderString = LiquidVolume.PuddleRenderStrings.GetRandomElement<string>(null);
					flag = true;
				}
			}
			string propertyOrTag = this.ParentObject.GetPropertyOrTag("DetailColorByLiquid", null);
			string propertyOrTag2 = this.ParentObject.GetPropertyOrTag("ColorStringByLiquid", null);
			if (!propertyOrTag.IsNullOrEmpty() || !propertyOrTag2.IsNullOrEmpty())
			{
				if (this.Volume <= 0 || (!this.LiquidVisibleWhenSealed && this.EffectivelySealed()) || this.GetPrimaryLiquid() == null)
				{
					if (!propertyOrTag.IsNullOrEmpty() && this.ParentObject.Render.DetailColor != propertyOrTag)
					{
						this.ParentObject.Render.DetailColor = propertyOrTag;
						flag = true;
					}
					if (!propertyOrTag2.IsNullOrEmpty() && this.ParentObject.Render.ColorString != propertyOrTag2)
					{
						this.ParentObject.Render.ColorString = propertyOrTag2;
						flag = true;
					}
				}
				else
				{
					BaseLiquid primaryLiquid = this.GetPrimaryLiquid();
					string text = ((primaryLiquid != null) ? primaryLiquid.GetColor() : null);
					if (!propertyOrTag.IsNullOrEmpty())
					{
						string text2 = text ?? propertyOrTag;
						string propertyOrTag3 = this.ParentObject.GetPropertyOrTag("DetailColorByLiquidContrastShiftFrom", null);
						if (propertyOrTag3 != null)
						{
							string propertyOrTag4 = this.ParentObject.GetPropertyOrTag("DetailColorByLiquidContrastShiftTo", null);
							if (text2 == propertyOrTag3)
							{
								text2 = propertyOrTag4;
							}
						}
						if (this.ParentObject.Render.DetailColor != text2)
						{
							this.ParentObject.Render.DetailColor = text2;
							flag = true;
						}
					}
					if (!propertyOrTag2.IsNullOrEmpty())
					{
						string text3 = (text.IsNullOrEmpty() ? propertyOrTag2 : ("&" + text));
						string propertyOrTag5 = this.ParentObject.GetPropertyOrTag("ColorStringByLiquidContrastShiftFrom", null);
						if (propertyOrTag5 != null)
						{
							string propertyOrTag6 = this.ParentObject.GetPropertyOrTag("ColorStringByLiquidContrastShiftTo", null);
							if (text3 == propertyOrTag5)
							{
								text3 = propertyOrTag6;
							}
						}
						if (this.ParentObject.Render.ColorString != text3)
						{
							this.ParentObject.Render.ColorString = text3;
							flag = true;
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x06008E09 RID: 36361 RVA: 0x0032C955 File Offset: 0x0032AB55
		public bool UseDrams(int Num)
		{
			if (this.Volume > Num)
			{
				this.Volume -= Num;
				base.FlushWeightCaches();
				return true;
			}
			this.Empty(false, false);
			this.CheckImage();
			return false;
		}

		// Token: 0x06008E0A RID: 36362 RVA: 0x0032C986 File Offset: 0x0032AB86
		public bool UseDram()
		{
			return this.UseDrams(1);
		}

		// Token: 0x06008E0B RID: 36363 RVA: 0x0032C990 File Offset: 0x0032AB90
		public bool UseDrams(Dictionary<string, int> Amounts)
		{
			if (LiquidVolume.consumeVolume == null)
			{
				LiquidVolume.consumeVolume = new LiquidVolume();
			}
			LiquidVolume.consumeVolume.ComponentLiquids.Clear();
			bool flag = true;
			int num = 0;
			foreach (KeyValuePair<string, int> keyValuePair in Amounts)
			{
				int num2;
				if (this.ComponentLiquids.TryGetValue(keyValuePair.Key, out num2))
				{
					int num3 = Math.Max(this.Volume * num2 / 1000, 1);
					int num4 = Math.Min(keyValuePair.Value, num3);
					num += num4;
					LiquidVolume.consumeVolume.ComponentLiquids.Add(keyValuePair.Key, num4);
					if (num3 < keyValuePair.Value)
					{
						flag = false;
					}
				}
				else
				{
					flag = false;
				}
			}
			if (num > 0)
			{
				LiquidVolume.consumeVolume.MaxVolume = num;
				LiquidVolume.consumeVolume.Volume = -num;
				LiquidVolume.consumeVolume.NormalizeProportions();
				this.MixWith(LiquidVolume.consumeVolume, null, null, null, false);
				base.FlushNavigationCaches();
				this.CheckGroundLiquidMerge();
			}
			return flag;
		}

		// Token: 0x06008E0C RID: 36364 RVA: 0x0032CAB4 File Offset: 0x0032ACB4
		public bool UseDrams(string Liquid, int Amount)
		{
			bool flag = true;
			int num = 0;
			int num2;
			if (this.ComponentLiquids.TryGetValue(Liquid, out num2))
			{
				int num3 = Math.Max(this.Volume * num2 / 1000, 1);
				if (num3 < Amount)
				{
					num = num3;
					flag = false;
				}
				else
				{
					num = Amount;
				}
			}
			else
			{
				flag = false;
			}
			if (num > 0)
			{
				if (LiquidVolume.consumeVolume == null)
				{
					LiquidVolume.consumeVolume = new LiquidVolume();
				}
				LiquidVolume.consumeVolume.ComponentLiquids.Clear();
				if (!LiquidVolume.consumeVolume.ComponentLiquids.ContainsKey(Liquid) || LiquidVolume.consumeVolume.ComponentLiquids.Count > 1)
				{
					if (LiquidVolume.consumeVolume.ComponentLiquids.Count > 0)
					{
						LiquidVolume.consumeVolume.ComponentLiquids.Clear();
					}
					LiquidVolume.consumeVolume.ComponentLiquids.Add(Liquid, 1000);
				}
				LiquidVolume.consumeVolume.MaxVolume = num;
				LiquidVolume.consumeVolume.Volume = -num;
				this.MixWith(LiquidVolume.consumeVolume, null, null, null, false);
				base.FlushNavigationCaches();
				this.CheckGroundLiquidMerge();
			}
			return flag;
		}

		// Token: 0x06008E0D RID: 36365 RVA: 0x0032CBBC File Offset: 0x0032ADBC
		public bool UseDramsByEvaporativity(int Num)
		{
			if (this.ComponentLiquids.Count < 2)
			{
				using (Dictionary<string, int>.KeyCollection.Enumerator enumerator = this.ComponentLiquids.Keys.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (LiquidVolume.GetLiquid(enumerator.Current).Evaporativity > 0)
						{
							return this.UseDrams(Num);
						}
					}
				}
				return false;
			}
			LiquidVolume.EvaporativityProportions.Clear();
			LiquidVolume.EvaporativityAmounts.Clear();
			int num = 0;
			foreach (KeyValuePair<string, int> keyValuePair in this.ComponentLiquids)
			{
				int num2 = LiquidVolume.GetLiquid(keyValuePair.Key).Evaporativity * keyValuePair.Value;
				LiquidVolume.EvaporativityProportions.Add(keyValuePair.Key, num2);
				num += num2;
			}
			int num3 = 0;
			using (Dictionary<string, int>.Enumerator enumerator2 = LiquidVolume.EvaporativityProportions.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					KeyValuePair<string, int> keyValuePair2 = enumerator2.Current;
					int num4 = Num * keyValuePair2.Value / num;
					LiquidVolume.EvaporativityAmounts.Add(keyValuePair2.Key, num4);
					num3 += num4;
				}
				goto IL_01C1;
			}
			IL_013B:
			string text = null;
			int num5 = 0;
			foreach (KeyValuePair<string, int> keyValuePair3 in LiquidVolume.EvaporativityProportions)
			{
				if (keyValuePair3.Value > num5)
				{
					text = keyValuePair3.Key;
					num5 = keyValuePair3.Value;
				}
			}
			if (text == null)
			{
				goto IL_01C8;
			}
			Dictionary<string, int> evaporativityAmounts = LiquidVolume.EvaporativityAmounts;
			string text2 = text;
			int num6 = evaporativityAmounts[text2];
			evaporativityAmounts[text2] = num6 + 1;
			num3++;
			LiquidVolume.EvaporativityProportions.Remove(text);
			IL_01C1:
			if (num3 < Num)
			{
				goto IL_013B;
			}
			IL_01C8:
			this.UseDrams(LiquidVolume.EvaporativityAmounts);
			return true;
		}

		// Token: 0x06008E0E RID: 36366 RVA: 0x0032CDD4 File Offset: 0x0032AFD4
		public bool Stain(GameObject obj, int Num)
		{
			LiquidVolume.StainingProportions.Clear();
			LiquidVolume.StainingAmounts.Clear();
			int num = 0;
			foreach (KeyValuePair<string, int> keyValuePair in this.ComponentLiquids)
			{
				BaseLiquid liquid = LiquidVolume.GetLiquid(keyValuePair.Key);
				if (!liquid.StainOnlyWhenPure || this.ComponentLiquids.Count == 1)
				{
					int num2 = liquid.Staining * keyValuePair.Value;
					LiquidVolume.StainingProportions.Add(keyValuePair.Key, num2);
					num += num2;
				}
			}
			for (int i = 0; i < Num; i++)
			{
				string randomElement = LiquidVolume.StainingProportions.GetRandomElement<string>(ref num, null);
				if (randomElement == null)
				{
					return false;
				}
				if (LiquidVolume.StainingAmounts.ContainsKey(randomElement))
				{
					Dictionary<string, int> stainingAmounts = LiquidVolume.StainingAmounts;
					string text = randomElement;
					int num3 = stainingAmounts[text];
					stainingAmounts[text] = num3 + 1;
				}
				else
				{
					LiquidVolume.StainingAmounts.Add(randomElement, 1);
				}
			}
			this.UseDrams(LiquidVolume.StainingAmounts);
			return obj.ForceApplyEffect(new LiquidStained(new LiquidVolume(LiquidVolume.StainingAmounts)), null);
		}

		// Token: 0x06008E0F RID: 36367 RVA: 0x0032CF08 File Offset: 0x0032B108
		public double GetLiquidExtrinsicValuePerDram()
		{
			int count = this.ComponentLiquids.Count;
			if (count != 0)
			{
				if (count == 1)
				{
					if (this.Primary == null)
					{
						using (Dictionary<string, int>.KeyCollection.Enumerator enumerator = this.ComponentLiquids.Keys.GetEnumerator())
						{
							if (!enumerator.MoveNext())
							{
								goto IL_00EA;
							}
							string text = enumerator.Current;
							return (double)LiquidVolume.Liquids[text].GetExtrinsicValuePerDram(true);
						}
					}
					return (double)this.RequirePrimaryLiquid().GetExtrinsicValuePerDram(true);
				}
				double num = 0.0;
				foreach (KeyValuePair<string, int> keyValuePair in this.ComponentLiquids)
				{
					num += (double)(LiquidVolume.Liquids[keyValuePair.Key].GetExtrinsicValuePerDram(false) * (float)keyValuePair.Value);
				}
				return num / 1000.0;
			}
			IL_00EA:
			return 0.0;
		}

		// Token: 0x06008E10 RID: 36368 RVA: 0x0032D028 File Offset: 0x0032B228
		public int GetLiquidTemperature()
		{
			int count = this.ComponentLiquids.Count;
			if (count != 0)
			{
				if (count == 1)
				{
					if (this.Primary == null)
					{
						using (Dictionary<string, int>.KeyCollection.Enumerator enumerator = this.ComponentLiquids.Keys.GetEnumerator())
						{
							if (!enumerator.MoveNext())
							{
								return 25;
							}
							string text = enumerator.Current;
							return LiquidVolume.Liquids[text].Temperature;
						}
					}
					return this.RequirePrimaryLiquid().Temperature;
				}
				int num = 0;
				foreach (KeyValuePair<string, int> keyValuePair in this.ComponentLiquids)
				{
					num += LiquidVolume.Liquids[keyValuePair.Key].Temperature * keyValuePair.Value;
				}
				return num / 1000;
			}
			return 25;
		}

		// Token: 0x06008E11 RID: 36369 RVA: 0x0032D12C File Offset: 0x0032B32C
		public int GetLiquidFlameTemperature()
		{
			int count = this.ComponentLiquids.Count;
			if (count != 0)
			{
				if (count == 1)
				{
					if (this.Primary == null)
					{
						using (Dictionary<string, int>.KeyCollection.Enumerator enumerator = this.ComponentLiquids.Keys.GetEnumerator())
						{
							if (!enumerator.MoveNext())
							{
								return 0;
							}
							string text = enumerator.Current;
							return LiquidVolume.Liquids[text].FlameTemperature;
						}
					}
					return this.RequirePrimaryLiquid().FlameTemperature;
				}
				int num = 0;
				foreach (KeyValuePair<string, int> keyValuePair in this.ComponentLiquids)
				{
					num += LiquidVolume.Liquids[keyValuePair.Key].FlameTemperature * keyValuePair.Value;
				}
				return num / 1000;
			}
			return 0;
		}

		// Token: 0x06008E12 RID: 36370 RVA: 0x0032D22C File Offset: 0x0032B42C
		public int GetLiquidVaporTemperature()
		{
			int count = this.ComponentLiquids.Count;
			if (count != 0)
			{
				if (count == 1)
				{
					if (this.Primary == null)
					{
						using (Dictionary<string, int>.KeyCollection.Enumerator enumerator = this.ComponentLiquids.Keys.GetEnumerator())
						{
							if (!enumerator.MoveNext())
							{
								return 0;
							}
							string text = enumerator.Current;
							return LiquidVolume.Liquids[text].VaporTemperature;
						}
					}
					return this.RequirePrimaryLiquid().VaporTemperature;
				}
				int num = 0;
				foreach (KeyValuePair<string, int> keyValuePair in this.ComponentLiquids)
				{
					num += LiquidVolume.Liquids[keyValuePair.Key].VaporTemperature * keyValuePair.Value;
				}
				return num / 1000;
			}
			return 0;
		}

		// Token: 0x06008E13 RID: 36371 RVA: 0x0032D32C File Offset: 0x0032B52C
		public int GetLiquidFreezeTemperature()
		{
			int count = this.ComponentLiquids.Count;
			if (count != 0)
			{
				if (count == 1)
				{
					if (this.Primary == null)
					{
						using (Dictionary<string, int>.KeyCollection.Enumerator enumerator = this.ComponentLiquids.Keys.GetEnumerator())
						{
							if (!enumerator.MoveNext())
							{
								return 0;
							}
							string text = enumerator.Current;
							return LiquidVolume.Liquids[text].FreezeTemperature;
						}
					}
					return this.RequirePrimaryLiquid().FreezeTemperature;
				}
				int num = 0;
				foreach (KeyValuePair<string, int> keyValuePair in this.ComponentLiquids)
				{
					num += LiquidVolume.Liquids[keyValuePair.Key].FreezeTemperature * keyValuePair.Value;
				}
				return num / 1000;
			}
			return 0;
		}

		// Token: 0x06008E14 RID: 36372 RVA: 0x0032D42C File Offset: 0x0032B62C
		public int GetLiquidBrittleTemperature()
		{
			int count = this.ComponentLiquids.Count;
			if (count != 0)
			{
				if (count == 1)
				{
					if (this.Primary == null)
					{
						using (Dictionary<string, int>.KeyCollection.Enumerator enumerator = this.ComponentLiquids.Keys.GetEnumerator())
						{
							if (!enumerator.MoveNext())
							{
								return 0;
							}
							string text = enumerator.Current;
							return LiquidVolume.Liquids[text].BrittleTemperature;
						}
					}
					return this.RequirePrimaryLiquid().BrittleTemperature;
				}
				int num = 0;
				foreach (KeyValuePair<string, int> keyValuePair in this.ComponentLiquids)
				{
					num += LiquidVolume.Liquids[keyValuePair.Key].BrittleTemperature * keyValuePair.Value;
				}
				return num / 1000;
			}
			return 0;
		}

		// Token: 0x06008E15 RID: 36373 RVA: 0x0032D52C File Offset: 0x0032B72C
		public int GetLiquidElectricalConductivity()
		{
			int count = this.ComponentLiquids.Count;
			if (count != 0)
			{
				if (count == 1)
				{
					if (this.Primary == null)
					{
						using (Dictionary<string, int>.KeyCollection.Enumerator enumerator = this.ComponentLiquids.Keys.GetEnumerator())
						{
							if (!enumerator.MoveNext())
							{
								return 80;
							}
							string text = enumerator.Current;
							return LiquidVolume.Liquids[text].PureElectricalConductivity;
						}
					}
					return this.RequirePrimaryLiquid().PureElectricalConductivity;
				}
				int num = 0;
				foreach (KeyValuePair<string, int> keyValuePair in this.ComponentLiquids)
				{
					num += LiquidVolume.Liquids[keyValuePair.Key].MixedElectricalConductivity * keyValuePair.Value;
				}
				return num / 1000;
			}
			return 80;
		}

		// Token: 0x06008E16 RID: 36374 RVA: 0x0032D630 File Offset: 0x0032B830
		public string GetLiquidFreezeObject()
		{
			BaseLiquid primaryLiquid = this.GetPrimaryLiquid();
			if (primaryLiquid == null)
			{
				return null;
			}
			return primaryLiquid.GetFreezeObjectForVolume(this.Amount(this.Primary));
		}

		// Token: 0x06008E17 RID: 36375 RVA: 0x0032D64F File Offset: 0x0032B84F
		public string GetLiquidFreezeObject(out string FreezeVerb)
		{
			FreezeVerb = null;
			BaseLiquid primaryLiquid = this.GetPrimaryLiquid();
			if (primaryLiquid == null)
			{
				return null;
			}
			return primaryLiquid.GetFreezeObjectForVolume(this.Amount(this.Primary), out FreezeVerb);
		}

		// Token: 0x06008E18 RID: 36376 RVA: 0x0032D674 File Offset: 0x0032B874
		public void GetLiquidPhysicalProperties(out int FlameTemperature, out int VaporTemperature, out int FreezeTemperature, out int BrittleTemperature, out int ElectricalConductivity)
		{
			FlameTemperature = 99999;
			VaporTemperature = 350;
			FreezeTemperature = 0;
			BrittleTemperature = -100;
			ElectricalConductivity = 80;
			int count = this.ComponentLiquids.Count;
			if (count != 0)
			{
				if (count == 1)
				{
					if (this.Primary == null)
					{
						using (Dictionary<string, int>.KeyCollection.Enumerator enumerator = this.ComponentLiquids.Keys.GetEnumerator())
						{
							if (!enumerator.MoveNext())
							{
								return;
							}
							string text = enumerator.Current;
							BaseLiquid baseLiquid = LiquidVolume.Liquids[text];
							FlameTemperature = baseLiquid.FlameTemperature;
							VaporTemperature = baseLiquid.VaporTemperature;
							FreezeTemperature = baseLiquid.FreezeTemperature;
							BrittleTemperature = baseLiquid.BrittleTemperature;
							ElectricalConductivity = baseLiquid.PureElectricalConductivity;
							return;
						}
					}
					BaseLiquid baseLiquid2 = this.RequirePrimaryLiquid();
					FlameTemperature = baseLiquid2.FlameTemperature;
					VaporTemperature = baseLiquid2.VaporTemperature;
					FreezeTemperature = baseLiquid2.FreezeTemperature;
					BrittleTemperature = baseLiquid2.BrittleTemperature;
					ElectricalConductivity = baseLiquid2.PureElectricalConductivity;
					return;
				}
				FlameTemperature = 0;
				VaporTemperature = 0;
				FreezeTemperature = 0;
				BrittleTemperature = 0;
				ElectricalConductivity = 0;
				foreach (KeyValuePair<string, int> keyValuePair in this.ComponentLiquids)
				{
					BaseLiquid baseLiquid3 = LiquidVolume.Liquids[keyValuePair.Key];
					FlameTemperature += baseLiquid3.FlameTemperature * keyValuePair.Value;
					VaporTemperature += baseLiquid3.VaporTemperature * keyValuePair.Value;
					FreezeTemperature += baseLiquid3.FreezeTemperature * keyValuePair.Value;
					BrittleTemperature += baseLiquid3.BrittleTemperature * keyValuePair.Value;
					ElectricalConductivity += baseLiquid3.MixedElectricalConductivity * keyValuePair.Value;
				}
				FlameTemperature /= 1000;
				VaporTemperature /= 1000;
				FreezeTemperature /= 1000;
				BrittleTemperature /= 1000;
				ElectricalConductivity /= 1000;
			}
		}

		// Token: 0x06008E19 RID: 36377 RVA: 0x0032D87C File Offset: 0x0032BA7C
		public int GetLiquidThermalConductivity()
		{
			int count = this.ComponentLiquids.Count;
			if (count != 0)
			{
				if (count == 1)
				{
					if (this.Primary == null)
					{
						using (Dictionary<string, int>.KeyCollection.Enumerator enumerator = this.ComponentLiquids.Keys.GetEnumerator())
						{
							if (!enumerator.MoveNext())
							{
								return 50;
							}
							string text = enumerator.Current;
							return LiquidVolume.Liquids[text].ThermalConductivity;
						}
					}
					return this.RequirePrimaryLiquid().ThermalConductivity;
				}
				int num = 0;
				foreach (KeyValuePair<string, int> keyValuePair in this.ComponentLiquids)
				{
					num += LiquidVolume.Liquids[keyValuePair.Key].ThermalConductivity * keyValuePair.Value;
				}
				return num / 1000;
			}
			return 50;
		}

		// Token: 0x06008E1A RID: 36378 RVA: 0x0032D980 File Offset: 0x0032BB80
		public int GetLiquidCombustibility()
		{
			int count = this.ComponentLiquids.Count;
			if (count != 0)
			{
				if (count == 1)
				{
					if (this.Primary == null)
					{
						using (Dictionary<string, int>.KeyCollection.Enumerator enumerator = this.ComponentLiquids.Keys.GetEnumerator())
						{
							if (!enumerator.MoveNext())
							{
								return 0;
							}
							string text = enumerator.Current;
							return LiquidVolume.Liquids[text].Combustibility;
						}
					}
					return this.RequirePrimaryLiquid().Combustibility;
				}
				int num = 0;
				foreach (KeyValuePair<string, int> keyValuePair in this.ComponentLiquids)
				{
					num += LiquidVolume.Liquids[keyValuePair.Key].Combustibility * keyValuePair.Value;
				}
				return num / 1000;
			}
			return 0;
		}

		// Token: 0x06008E1B RID: 36379 RVA: 0x0032DA80 File Offset: 0x0032BC80
		public float GetLiquidCooling()
		{
			int liquidTemperature = this.GetLiquidTemperature();
			int liquidThermalConductivity = this.GetLiquidThermalConductivity();
			int liquidCombustibility = this.GetLiquidCombustibility();
			float num = 0.125f + (1000f - (float)liquidTemperature) / 1000f;
			if (liquidCombustibility > 0)
			{
				num = num * (float)(100 - liquidCombustibility) / 100f;
			}
			if (liquidThermalConductivity != 50)
			{
				num = num * (float)liquidThermalConductivity / 50f;
			}
			return num;
		}

		// Token: 0x06008E1C RID: 36380 RVA: 0x0032DADC File Offset: 0x0032BCDC
		public int GetLiquidAdsorbence()
		{
			int count = this.ComponentLiquids.Count;
			if (count != 0)
			{
				if (count == 1)
				{
					if (this.Primary == null)
					{
						using (Dictionary<string, int>.KeyCollection.Enumerator enumerator = this.ComponentLiquids.Keys.GetEnumerator())
						{
							if (!enumerator.MoveNext())
							{
								return 0;
							}
							string text = enumerator.Current;
							return LiquidVolume.Liquids[text].Adsorbence;
						}
					}
					return this.RequirePrimaryLiquid().Adsorbence;
				}
				int num = 0;
				foreach (KeyValuePair<string, int> keyValuePair in this.ComponentLiquids)
				{
					num += LiquidVolume.Liquids[keyValuePair.Key].Adsorbence * keyValuePair.Value;
				}
				if (num == 0)
				{
					return 0;
				}
				return Math.Max(num / 1000, 1);
			}
			return 0;
		}

		// Token: 0x06008E1D RID: 36381 RVA: 0x0032DBEC File Offset: 0x0032BDEC
		public int GetLiquidFluidity()
		{
			int count = this.ComponentLiquids.Count;
			if (count != 0)
			{
				if (count == 1)
				{
					if (this.Primary == null)
					{
						using (Dictionary<string, int>.KeyCollection.Enumerator enumerator = this.ComponentLiquids.Keys.GetEnumerator())
						{
							if (!enumerator.MoveNext())
							{
								return 0;
							}
							string text = enumerator.Current;
							return LiquidVolume.Liquids[text].Fluidity;
						}
					}
					return this.RequirePrimaryLiquid().Fluidity;
				}
				int num = 0;
				foreach (KeyValuePair<string, int> keyValuePair in this.ComponentLiquids)
				{
					num += LiquidVolume.Liquids[keyValuePair.Key].Fluidity * keyValuePair.Value;
				}
				if (num == 0)
				{
					return 0;
				}
				return Math.Max(num / 1000, 1);
			}
			return 0;
		}

		// Token: 0x06008E1E RID: 36382 RVA: 0x0032DCFC File Offset: 0x0032BEFC
		public int GetLiquidViscosity()
		{
			return 100 - this.GetLiquidFluidity();
		}

		// Token: 0x06008E1F RID: 36383 RVA: 0x0032DD08 File Offset: 0x0032BF08
		public int GetLiquidEvaporativity()
		{
			int count = this.ComponentLiquids.Count;
			if (count != 0)
			{
				if (count == 1)
				{
					if (this.Primary == null)
					{
						using (Dictionary<string, int>.KeyCollection.Enumerator enumerator = this.ComponentLiquids.Keys.GetEnumerator())
						{
							if (!enumerator.MoveNext())
							{
								return 0;
							}
							string text = enumerator.Current;
							return LiquidVolume.Liquids[text].Evaporativity;
						}
					}
					return this.RequirePrimaryLiquid().Evaporativity;
				}
				int num = 0;
				foreach (KeyValuePair<string, int> keyValuePair in this.ComponentLiquids)
				{
					num += LiquidVolume.Liquids[keyValuePair.Key].Evaporativity * keyValuePair.Value;
				}
				if (num == 0)
				{
					return 0;
				}
				return Math.Max(num / 1000, 1);
			}
			return 0;
		}

		// Token: 0x06008E20 RID: 36384 RVA: 0x0032DE18 File Offset: 0x0032C018
		public int GetLiquidStaining()
		{
			int count = this.ComponentLiquids.Count;
			if (count != 0)
			{
				if (count == 1)
				{
					if (this.Primary == null)
					{
						using (Dictionary<string, int>.KeyCollection.Enumerator enumerator = this.ComponentLiquids.Keys.GetEnumerator())
						{
							if (!enumerator.MoveNext())
							{
								return 0;
							}
							string text = enumerator.Current;
							return LiquidVolume.Liquids[text].Staining;
						}
					}
					return this.RequirePrimaryLiquid().Staining;
				}
				int num = 0;
				foreach (KeyValuePair<string, int> keyValuePair in this.ComponentLiquids)
				{
					BaseLiquid baseLiquid = LiquidVolume.Liquids[keyValuePair.Key];
					if (!baseLiquid.StainOnlyWhenPure)
					{
						num += baseLiquid.Staining * keyValuePair.Value;
					}
				}
				if (num == 0)
				{
					return 0;
				}
				return Math.Max(num / 1000, 1);
			}
			return 0;
		}

		// Token: 0x06008E21 RID: 36385 RVA: 0x0032DF34 File Offset: 0x0032C134
		public int GetLiquidCleansing()
		{
			int count = this.ComponentLiquids.Count;
			if (count != 0)
			{
				if (count == 1)
				{
					if (this.Primary == null)
					{
						using (Dictionary<string, int>.KeyCollection.Enumerator enumerator = this.ComponentLiquids.Keys.GetEnumerator())
						{
							if (!enumerator.MoveNext())
							{
								return 0;
							}
							string text = enumerator.Current;
							return LiquidVolume.Liquids[text].Cleansing;
						}
					}
					return this.RequirePrimaryLiquid().Cleansing;
				}
				int num = 0;
				foreach (KeyValuePair<string, int> keyValuePair in this.ComponentLiquids)
				{
					num += LiquidVolume.Liquids[keyValuePair.Key].Cleansing * keyValuePair.Value;
				}
				if (num == 0)
				{
					return 0;
				}
				return Math.Max(num / 1000, 1);
			}
			return 0;
		}

		// Token: 0x06008E22 RID: 36386 RVA: 0x0032E044 File Offset: 0x0032C244
		public bool IsLiquidUsableForCleaning()
		{
			int count = this.ComponentLiquids.Count;
			if (count != 0)
			{
				if (count == 1)
				{
					if (this.Primary == null)
					{
						using (Dictionary<string, int>.KeyCollection.Enumerator enumerator = this.ComponentLiquids.Keys.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								string text = enumerator.Current;
								if (LiquidVolume.Liquids[text].EnableCleaning)
								{
									return true;
								}
							}
							return false;
						}
					}
					if (this.RequirePrimaryLiquid().EnableCleaning)
					{
						return true;
					}
				}
				else
				{
					int num = 0;
					bool flag = false;
					foreach (KeyValuePair<string, int> keyValuePair in this.ComponentLiquids)
					{
						BaseLiquid baseLiquid = LiquidVolume.Liquids[keyValuePair.Key];
						num += baseLiquid.Cleansing * keyValuePair.Value;
						if (baseLiquid.EnableCleaning)
						{
							flag = true;
						}
					}
					if (flag && num > 0 && Math.Max(num / 1000, 1) >= 2)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06008E23 RID: 36387 RVA: 0x0032E178 File Offset: 0x0032C378
		public bool UsableForCleaning()
		{
			return this.ParentObject != null && this.Volume > 0 && this.IsLiquidUsableForCleaning() && !this.EffectivelySealed();
		}

		// Token: 0x06008E24 RID: 36388 RVA: 0x0032E1A4 File Offset: 0x0032C3A4
		public bool SafeContainer(GameObject obj)
		{
			if (this.ComponentLiquids.Count == 1 && this.Primary != null)
			{
				return this.RequirePrimaryLiquid().SafeContainer(obj);
			}
			foreach (string text in this.ComponentLiquids.Keys)
			{
				if (!LiquidVolume.Liquids[text].SafeContainer(obj))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06008E25 RID: 36389 RVA: 0x0032E234 File Offset: 0x0032C434
		public static bool IsGameObjectSafeContainerForLiquid(GameObject obj, string liquid)
		{
			try
			{
				if (liquid.Contains(','))
				{
					foreach (string text in liquid.Split(',', StringSplitOptions.None))
					{
						if (text.Contains('-'))
						{
							if (!LiquidVolume.GetLiquid(LiquidVolume.Liquids[text.Split('-', StringSplitOptions.None)[0]].ID).SafeContainer(obj))
							{
								return false;
							}
						}
						else if (!LiquidVolume.GetLiquid(LiquidVolume.Liquids[text].ID).SafeContainer(obj))
						{
							return false;
						}
					}
				}
				else if (liquid.Contains('-'))
				{
					if (!LiquidVolume.GetLiquid(LiquidVolume.Liquids[liquid.Split('-', StringSplitOptions.None)[0]].ID).SafeContainer(obj))
					{
						return false;
					}
				}
				else if (!LiquidVolume.GetLiquid(LiquidVolume.Liquids[liquid].ID).SafeContainer(obj))
				{
					return false;
				}
			}
			catch
			{
				MetricsManager.LogError("invalid liquid specification " + (liquid ?? "NULL"));
			}
			return true;
		}

		// Token: 0x06008E26 RID: 36390 RVA: 0x0032E364 File Offset: 0x0032C564
		public bool ConsiderLiquidDangerousToContact()
		{
			if (this.ComponentLiquids.Count == 1 && this.Primary != null)
			{
				return this.RequirePrimaryLiquid().ConsiderDangerousToContact;
			}
			foreach (string text in this.ComponentLiquids.Keys)
			{
				if (LiquidVolume.Liquids[text].ConsiderDangerousToContact)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06008E27 RID: 36391 RVA: 0x0032E3F0 File Offset: 0x0032C5F0
		public bool ConsiderLiquidDangerousToDrink()
		{
			if (this.ComponentLiquids.Count == 1 && this.Primary != null)
			{
				return this.RequirePrimaryLiquid().ConsiderDangerousToDrink;
			}
			foreach (string text in this.ComponentLiquids.Keys)
			{
				if (LiquidVolume.Liquids[text].ConsiderDangerousToDrink)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06008E28 RID: 36392 RVA: 0x0032E47C File Offset: 0x0032C67C
		public double GetLiquidWeightPerDram()
		{
			int count = this.ComponentLiquids.Count;
			if (count != 0)
			{
				if (count == 1)
				{
					if (this.Primary == null)
					{
						using (Dictionary<string, int>.KeyCollection.Enumerator enumerator = this.ComponentLiquids.Keys.GetEnumerator())
						{
							if (!enumerator.MoveNext())
							{
								goto IL_00E4;
							}
							string text = enumerator.Current;
							return LiquidVolume.Liquids[text].Weight;
						}
					}
					return this.RequirePrimaryLiquid().Weight;
				}
				double num = 0.0;
				foreach (KeyValuePair<string, int> keyValuePair in this.ComponentLiquids)
				{
					num += LiquidVolume.Liquids[keyValuePair.Key].Weight * (double)keyValuePair.Value;
				}
				return num / 1000.0;
			}
			IL_00E4:
			return 0.0;
		}

		// Token: 0x06008E29 RID: 36393 RVA: 0x0032E594 File Offset: 0x0032C794
		public string GetCirculatoryLossTerm()
		{
			return this.RequirePrimaryLiquid().CirculatoryLossTerm;
		}

		// Token: 0x06008E2A RID: 36394 RVA: 0x0032E5A1 File Offset: 0x0032C7A1
		public string GetColoredCirculatoryLossTerm()
		{
			return this.RequirePrimaryLiquid().ColoredCirculatoryLossTerm;
		}

		// Token: 0x06008E2B RID: 36395 RVA: 0x0032E5AE File Offset: 0x0032C7AE
		public string GetCirculatoryLossNoun()
		{
			return this.RequirePrimaryLiquid().CirculatoryLossNoun;
		}

		// Token: 0x06008E2C RID: 36396 RVA: 0x0032E5BB File Offset: 0x0032C7BB
		public string GetColoredCirculatoryLossNoun()
		{
			return this.RequirePrimaryLiquid().ColoredCirculatoryLossNoun;
		}

		// Token: 0x06008E2D RID: 36397 RVA: 0x0032E5C8 File Offset: 0x0032C7C8
		private bool PourIntoCellInternal(GameObject Pourer, Cell TargetCell, int PourAmount, bool CanPourOn, bool Pure, bool CanMergeWithGroundLiquid, ref bool RequestInterfaceExit, bool UsePerformancePath = false)
		{
			if (this.ParentObject != null && this.ParentObject.IsTemporary)
			{
				this.Volume -= PourAmount;
				return false;
			}
			if (TargetCell == null || TargetCell.OnWorldMap())
			{
				return false;
			}
			foreach (KeyValuePair<string, int> keyValuePair in this.ComponentLiquids)
			{
				if (!LiquidVolume.Liquids[keyValuePair.Key].PourIntoCell(this, Pourer, TargetCell, ref PourAmount, CanPourOn, ref RequestInterfaceExit))
				{
					return false;
				}
			}
			bool flag = TargetCell.IsSolidFor(Pourer, false);
			LiquidVolume liquidVolume3;
			LiquidVolume liquidVolume4;
			LiquidVolume liquidVolume5;
			for (;;)
			{
				IL_0095:
				if (CanPourOn)
				{
					GameObject combatTarget = TargetCell.GetCombatTarget(Pourer, false, false, false, 0, null, null, null, null, null, true, false, null);
					if (combatTarget != null)
					{
						LiquidVolume liquidVolume = combatTarget.LiquidVolume;
						if (liquidVolume == null || (!liquidVolume.IsOpenVolume() && !liquidVolume.Collector))
						{
							PourAmount -= this.ProcessContact(combatTarget, true, false, true, Pourer, false, PourAmount);
							if (PourAmount <= 0)
							{
								break;
							}
						}
					}
					int i = 0;
					int count = TargetCell.Objects.Count;
					while (i < count)
					{
						GameObject gameObject = TargetCell.Objects[i];
						if (gameObject != this.ParentObject && gameObject != Pourer && gameObject != combatTarget && (Pourer == null || Pourer.PhaseMatches(gameObject)) && ((Pourer != null && Pourer.IsFlying) || !gameObject.IsFlying))
						{
							LiquidVolume liquidVolume2 = gameObject.LiquidVolume;
							if (liquidVolume2 == null || (!liquidVolume2.IsOpenVolume() && !liquidVolume2.Collector))
							{
								PourAmount -= this.ProcessContact(gameObject, true, false, true, Pourer, false, PourAmount);
								if (PourAmount <= 0)
								{
									return true;
								}
							}
						}
						i++;
					}
				}
				for (int j = 0; j < TargetCell.Objects.Count; j++)
				{
					GameObject gameObject2 = TargetCell.Objects[j];
					if (gameObject2 != this.ParentObject && (!flag || gameObject2.CanInteractInCellWithSolid(Pourer)))
					{
						liquidVolume3 = gameObject2.LiquidVolume;
						if (liquidVolume3 != null && liquidVolume3.IsOpenVolume())
						{
							Temporary part = gameObject2.GetPart<Temporary>();
							ExistenceSupport part2 = gameObject2.GetPart<ExistenceSupport>();
							if (part != null)
							{
								part.Expire(true);
								if (!GameObject.Validate(gameObject2))
								{
									goto IL_0095;
								}
							}
							else
							{
								if (part2 == null)
								{
									goto IL_0216;
								}
								part2.Unsupported(true);
								if (!GameObject.Validate(gameObject2))
								{
									goto IL_0095;
								}
							}
						}
					}
				}
				for (int k = 0; k < TargetCell.Objects.Count; k++)
				{
					GameObject gameObject3 = TargetCell.Objects[k];
					if (gameObject3 != this.ParentObject && (!flag || gameObject3.CanInteractInCellWithSolid(Pourer)))
					{
						liquidVolume4 = gameObject3.LiquidVolume;
						if (liquidVolume4 != null && liquidVolume4 != this && !liquidVolume4.IsOpenVolume() && liquidVolume4.Collector && liquidVolume4.Volume < liquidVolume4.MaxVolume)
						{
							int num = liquidVolume4.MaxVolume - liquidVolume4.Volume;
							if (num >= PourAmount)
							{
								goto Block_34;
							}
							this.MixInto(liquidVolume4, num, ref RequestInterfaceExit, Pourer);
							PourAmount -= num;
						}
					}
				}
				for (int l = 0; l < TargetCell.Objects.Count; l++)
				{
					GameObject gameObject4 = TargetCell.Objects[l];
					if (gameObject4 != this.ParentObject && (!flag || gameObject4.CanInteractInCellWithSolid(Pourer)))
					{
						liquidVolume5 = gameObject4.LiquidVolume;
						if (liquidVolume5 != null && liquidVolume5.IsOpenVolume())
						{
							if (liquidVolume5 == this)
							{
								return true;
							}
							Temporary part3 = gameObject4.GetPart<Temporary>();
							ExistenceSupport part4 = gameObject4.GetPart<ExistenceSupport>();
							if (part3 != null)
							{
								part3.Expire(true);
								if (!GameObject.Validate(gameObject4))
								{
									goto IL_0095;
								}
								return true;
							}
							else
							{
								if (part4 == null)
								{
									goto IL_037F;
								}
								part4.Unsupported(true);
								if (!GameObject.Validate(gameObject4))
								{
									goto IL_0095;
								}
								return true;
							}
						}
					}
				}
				goto Block_45;
			}
			return true;
			IL_0216:
			this.MixInto(liquidVolume3, PourAmount, ref RequestInterfaceExit, Pourer);
			return true;
			Block_34:
			this.MixInto(liquidVolume4, PourAmount, ref RequestInterfaceExit, Pourer);
			return true;
			IL_037F:
			this.MixInto(liquidVolume5, PourAmount, ref RequestInterfaceExit, Pourer);
			return true;
			Block_45:
			string groundLiquid = TargetCell.GroundLiquid;
			if (CanMergeWithGroundLiquid && !groundLiquid.IsNullOrEmpty() && this.IsPureLiquid(groundLiquid, false))
			{
				this.Volume -= PourAmount;
				if (this.Volume <= 0)
				{
					this.Empty(false, false);
					this.CheckImage();
				}
				return true;
			}
			if (UsePerformancePath && TargetCell.HasOpenLiquidVolume())
			{
				TargetCell.GetOpenLiquidVolume().GetPart<LiquidVolume>().MixWith(this, null, null, new int?(PourAmount), true);
				this.Volume -= PourAmount;
				if (this.Volume <= 0)
				{
					this.Empty(false, false);
					this.CheckImage();
				}
				return true;
			}
			GameObject gameObject5 = GameObject.Create("Water");
			LiquidVolume liquidVolume6 = gameObject5.LiquidVolume;
			if (Pure || groundLiquid.IsNullOrEmpty())
			{
				liquidVolume6.Volume = 0;
				this.MixInto(liquidVolume6, PourAmount, ref RequestInterfaceExit, Pourer);
			}
			else
			{
				liquidVolume6.InitialLiquid = groundLiquid;
				liquidVolume6.Volume = PourAmount;
				this.MixInto(liquidVolume6, PourAmount, ref RequestInterfaceExit, Pourer);
				liquidVolume6.Volume = PourAmount;
			}
			if (Pourer != null)
			{
				Phase.carryOver(Pourer, gameObject5, 1);
			}
			TargetCell.AddObject(gameObject5, false, false, false, false, false, true, true, null, CanPourOn ? "Pour" : "Flow", null, Pourer, null, null, null, null);
			return true;
		}

		// Token: 0x06008E2E RID: 36398 RVA: 0x0032EAA8 File Offset: 0x0032CCA8
		public bool PourIntoCell(GameObject Pourer, Cell TargetCell, int PourAmount, ref bool RequestInterfaceExit, bool CanPourOn = true, bool Pure = false, bool CanMergeWithGroundLiquid = false, bool UsePerformancePath = false)
		{
			bool flag = this.PourIntoCellInternal(Pourer, TargetCell, PourAmount, CanPourOn, Pure, CanMergeWithGroundLiquid, ref RequestInterfaceExit, UsePerformancePath);
			if (this.Volume <= 0)
			{
				this.Empty(false, false);
			}
			this.CheckImage();
			return flag;
		}

		// Token: 0x06008E2F RID: 36399 RVA: 0x0032EAE0 File Offset: 0x0032CCE0
		public bool PourIntoCell(GameObject Pourer, Cell TargetCell, int PourAmount, bool CanPourOn = true, bool Pure = false, bool CanMergeWithGroundLiquid = false, bool UsePerformancePath = false)
		{
			bool flag = false;
			return this.PourIntoCell(Pourer, TargetCell, PourAmount, ref flag, CanPourOn, Pure, CanMergeWithGroundLiquid, UsePerformancePath);
		}

		// Token: 0x06008E30 RID: 36400 RVA: 0x0032EB02 File Offset: 0x0032CD02
		public bool PourIntoCell(Cell TargetCell, int PourAmount, bool CanPourOn = true, bool Pure = false, bool CanMergeWithGroundLiquid = false)
		{
			return this.PourIntoCell(null, TargetCell, PourAmount, CanPourOn, Pure, CanMergeWithGroundLiquid, false);
		}

		// Token: 0x06008E31 RID: 36401 RVA: 0x0032EB13 File Offset: 0x0032CD13
		public bool FlowIntoCell(int Amount = -1, Cell TargetCell = null, GameObject Pourer = null, bool CanPourOn = false, bool Pure = false, bool CanMergeWithGroundLiquid = true)
		{
			if (Amount == -1)
			{
				Amount = this.Volume;
			}
			if (TargetCell == null && this.ParentObject != null)
			{
				TargetCell = this.ParentObject.GetCurrentCell();
			}
			return this.PourIntoCell(Pourer, TargetCell, Amount, CanPourOn, Pure, CanMergeWithGroundLiquid, true);
		}

		// Token: 0x06008E32 RID: 36402 RVA: 0x0032EB4C File Offset: 0x0032CD4C
		private bool MixInto(LiquidVolume V, int Amount, ref bool RequestInterfaceExit, GameObject PouredBy = null)
		{
			bool flag = false;
			if (V != this)
			{
				if (Amount < this.Volume)
				{
					int? num = new int?(Amount);
					V.MixWith(this, ref RequestInterfaceExit, this.ParentObject, PouredBy, num, true);
				}
				else
				{
					GameObject parentObject = this.ParentObject;
					int? num = null;
					flag = V.MixWith(this, ref RequestInterfaceExit, parentObject, PouredBy, num, false);
					if (flag)
					{
						this.Empty(false, false);
					}
				}
			}
			return flag;
		}

		// Token: 0x06008E33 RID: 36403 RVA: 0x0032EBAC File Offset: 0x0032CDAC
		private bool MixInto(LiquidVolume V, int Amount, GameObject PouredBy = null)
		{
			bool flag = false;
			return this.MixInto(V, Amount, ref flag, PouredBy);
		}

		/// <summary>
		/// Mingles this liquid volume with another as appropriate to them being
		/// adjacent and free to intermix, as if you had a divider between two
		/// halves of a bottle then removed it.
		/// </summary>
		/// <param Name="other">
		/// The other liquid volume to mingle with.
		/// </param>
		// Token: 0x06008E34 RID: 36404 RVA: 0x0032EBC8 File Offset: 0x0032CDC8
		public void MingleAdjacent(LiquidVolume other)
		{
			int num;
			int num2;
			if (this.Volume == other.Volume && this.MaxVolume == other.MaxVolume)
			{
				num = this.Volume;
				num2 = other.Volume;
			}
			else if (this.Volume == this.MaxVolume && other.Volume == other.MaxVolume)
			{
				num = this.Volume;
				num2 = other.Volume;
			}
			else
			{
				int num3 = this.MaxVolume + other.MaxVolume;
				int num4 = this.Volume + other.Volume;
				if (num4 <= 1)
				{
					return;
				}
				if (this.IsOpenVolume() || other.IsOpenVolume())
				{
					num2 = (num = num4 / 2);
				}
				else
				{
					num = num4 * this.MaxVolume / num3;
					num2 = num4 * other.MaxVolume / num3;
				}
				if (num + num2 < num4)
				{
					if (string.Compare(this.ParentObject.ID, other.ParentObject.ID) > 0)
					{
						num++;
					}
					else
					{
						num2++;
					}
				}
			}
			if (this.Volume < num)
			{
				int? num5 = new int?(num - this.Volume);
				this.MixWith(other, other.ParentObject, null, num5, false);
				return;
			}
			if (other.Volume < num2)
			{
				int? num5 = new int?(num2 - other.Volume);
				other.MixWith(this, other.ParentObject, null, num5, false);
				return;
			}
			if (!this.ComponentLiquids.SameAs<string, int>(other.ComponentLiquids))
			{
				int num6 = Stat.Random(1, Math.Max(Math.Min(this.Volume, other.Volume) / 4, 1));
				int? num5 = new int?(num6);
				this.MixWith(other, other.ParentObject, null, num5, false);
				num5 = new int?(num6);
				other.MixWith(this, this.ParentObject, null, num5, false);
			}
		}

		// Token: 0x06008E35 RID: 36405 RVA: 0x0032ED6D File Offset: 0x0032CF6D
		private List<string> getKeylist()
		{
			if (LiquidVolume.keylistPool.Count > 0)
			{
				return LiquidVolume.keylistPool.Dequeue();
			}
			return new List<string>(256);
		}

		// Token: 0x06008E36 RID: 36406 RVA: 0x0032ED91 File Offset: 0x0032CF91
		public string GetActiveAutogetLiquid()
		{
			if (this.AutoCollectLiquidType != null)
			{
				return this.AutoCollectLiquidType;
			}
			if (Options.AutogetFreshWater)
			{
				return "water";
			}
			return null;
		}

		// Token: 0x06008E37 RID: 36407 RVA: 0x0032EDB0 File Offset: 0x0032CFB0
		public void Fill(string Liquid, int Drams)
		{
			this.Empty(true, true);
			if (Drams <= 0)
			{
				this.CheckImage();
				return;
			}
			this.TrackAsLiquid(Liquid);
			this.Volume = Drams;
			LiquidVolume.Liquids[Liquid].FillingContainer(this.ParentObject, this);
			this.RecalculatePrimary();
			this.RecalculateProperties();
		}

		// Token: 0x06008E38 RID: 36408 RVA: 0x0032EE04 File Offset: 0x0032D004
		public bool AddDrams(string Liquid, int Drams)
		{
			if (Drams > 0)
			{
				int num = (this.IsOpenVolume() ? int.MaxValue : this.MaxVolume) - this.Volume;
				if (num > 0)
				{
					bool flag = this.IsEmpty();
					if (flag || this.IsPureLiquid(Liquid, false))
					{
						if (flag)
						{
							this.TrackAsLiquid(Liquid);
						}
						if (Drams < num)
						{
							this.Volume += Drams;
						}
						else
						{
							this.Volume += num;
						}
						if (this.ParentObject != null && !this.IsOpenVolume())
						{
							string[] array = new string[this.ComponentLiquids.Keys.Count];
							this.ComponentLiquids.Keys.CopyTo(array, 0);
							foreach (string text in array)
							{
								LiquidVolume.Liquids[text].FillingContainer(this.ParentObject, this);
							}
						}
						this.RecalculatePrimary();
						this.RecalculateProperties();
					}
					else
					{
						this.MixWith(new LiquidVolume(Liquid, Math.Min(Drams, num)), null, null, null, false);
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x06008E39 RID: 36409 RVA: 0x0032EF1C File Offset: 0x0032D11C
		public bool GiveDrams(string Liquid, ref int Drams, bool Auto = false, List<GameObject> StoredIn = null, GameObject Actor = null)
		{
			if (Drams > 0 && (this.IsPureLiquid(Liquid, false) || this.IsEmpty()) && (!Auto || Liquid == this.GetActiveAutogetLiquid()) && !this.EffectivelySealed() && this.ParentObject.AllowLiquidCollection(Liquid, Actor))
			{
				int num = this.MaxVolume - this.Volume;
				if (num > 0)
				{
					if (this.IsEmpty())
					{
						this.TrackAsLiquid(Liquid);
					}
					if (StoredIn != null && this.ParentObject != null)
					{
						StoredIn.Add(this.ParentObject);
					}
					if (Drams < num)
					{
						this.Volume += Drams;
						Drams = 0;
					}
					else
					{
						Drams -= num;
						this.Volume += num;
					}
					if (this.ParentObject != null && !this.IsOpenVolume())
					{
						string[] array = new string[this.ComponentLiquids.Keys.Count];
						this.ComponentLiquids.Keys.CopyTo(array, 0);
						foreach (string text in array)
						{
							LiquidVolume.Liquids[text].FillingContainer(this.ParentObject, this);
						}
					}
					this.RecalculatePrimary();
					this.RecalculateProperties();
					return true;
				}
			}
			return false;
		}

		// Token: 0x06008E3A RID: 36410 RVA: 0x0032F057 File Offset: 0x0032D257
		public bool GiveDrams(string Liquid, int Drams, bool Auto = false, List<GameObject> StoredIn = null, GameObject Actor = null)
		{
			return this.GiveDrams(Liquid, ref Drams, Auto, StoredIn, Actor);
		}

		// Token: 0x06008E3B RID: 36411 RVA: 0x0032F068 File Offset: 0x0032D268
		public bool Pour(ref bool RequestInterfaceExit, GameObject Actor = null, Cell TargetCell = null, bool Forced = false, bool Douse = false, int PourAmount = -1, bool OwnershipHandled = false)
		{
			if (Forced || !this.EffectivelySealed())
			{
				if (this.ParentObject.IsInStasis())
				{
					if (Actor != null)
					{
						Actor.Fail("You cannot seem to interact with " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " in any way.");
					}
					return false;
				}
				if (!Forced && !OwnershipHandled && Actor != null && Actor.IsPlayer() && this.ParentObject.Owner != null)
				{
					if (Popup.ShowYesNoCancel(string.Concat(new string[]
					{
						this.ParentObject.IndicativeDistal,
						this.ParentObject.Is,
						" not owned by you. Are you sure you want to pour from ",
						this.ParentObject.them,
						"?"
					}), "Sounds/UI/ui_notification", true, DialogResult.Cancel) != DialogResult.Yes)
					{
						return false;
					}
					this.ParentObject.Physics.BroadcastForHelp(Actor);
				}
				Temporary part = this.ParentObject.GetPart<Temporary>();
				if (part != null)
				{
					if (this.IsOpenVolume())
					{
						part.Expire(false);
						if (GameObject.Validate(this.ParentObject))
						{
							this.Empty(false, false);
						}
					}
					else
					{
						this.Empty(false, false);
					}
					return false;
				}
				ExistenceSupport part2 = this.ParentObject.GetPart<ExistenceSupport>();
				if (part2 != null)
				{
					if (this.IsOpenVolume())
					{
						part2.Unsupported(false);
						if (GameObject.Validate(this.ParentObject))
						{
							this.Empty(false, false);
						}
					}
					else
					{
						this.Empty(false, false);
					}
					return false;
				}
				if (TargetCell == null)
				{
					if (!Forced && Actor != null && Actor.IsPlayer())
					{
						if (this.IsOpenVolume() || Actor.OnWorldMap())
						{
							return InventoryActionEvent.Check(this.ParentObject, Actor, this.ParentObject, "Fill", false, true, false, false, false, 0, 0, 0, null, null, null, null);
						}
						int num = Popup.PickOption("", "Where do you want to pour " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + "?", "", "Sounds/UI/ui_notification", new string[]
						{
							"Pour it into another container.",
							"Pour it nearby.",
							"Pour it on " + Actor.itself + "."
						}, new char[] { 'a', 'b', 'c' }, null, null, null, null, null, 0, 60, 0, -1, true, false, false, true, false, null, null);
						if (num < 0)
						{
							return false;
						}
						if (num == 0)
						{
							return InventoryActionEvent.Check(this.ParentObject, Actor, this.ParentObject, "Fill", false, true, false, false, false, 0, 0, 0, null, null, null, null);
						}
						if (num == 1)
						{
							Douse = false;
							string text = XRL.UI.PickDirection.ShowPicker("Pour");
							if (text == null)
							{
								return false;
							}
							TargetCell = Actor.GetCurrentCell().GetCellFromDirection(text, false);
						}
						if (num == 2)
						{
							Douse = true;
							TargetCell = Actor.GetCurrentCell();
						}
					}
					else
					{
						if (this.IsOpenVolume())
						{
							return false;
						}
						Douse = true;
						if (Actor != null)
						{
							TargetCell = Actor.GetCurrentCell();
						}
					}
				}
				if (PourAmount == -1)
				{
					if (Forced)
					{
						PourAmount = this.Volume;
					}
					else if (Actor != null && Actor.IsPlayer())
					{
						int? num2 = Popup.AskNumber("How many drams? (max=" + this.Volume.ToString() + ")", "Sounds/UI/ui_notification", "", this.Volume, 0, this.Volume);
						try
						{
							PourAmount = Convert.ToInt32(num2);
						}
						catch
						{
							return false;
						}
					}
				}
				if (PourAmount > this.Volume)
				{
					PourAmount = this.Volume;
				}
				if (PourAmount <= 0)
				{
					return false;
				}
				if (this.Volume > 0)
				{
					if (Actor != null && Douse)
					{
						foreach (KeyValuePair<string, int> keyValuePair in this.ComponentLiquids)
						{
							if (!LiquidVolume.Liquids[keyValuePair.Key].Douse(this, Actor, ref PourAmount, ref RequestInterfaceExit))
							{
								return false;
							}
						}
						base.PlayWorldSound("Sounds/Interact/sfx_interact_liquidContainer_pourout", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
						if (Actor.IsPlayer())
						{
							Popup.Show(PourAmount.Things("dram", null) + " of " + this.GetLiquidName() + " pours out all over you!", null, "Sounds/UI/ui_notification", true, true, true, true, null);
						}
						else if (Actor.IsVisible() && Actor.IsVisible())
						{
							IComponent<GameObject>.AddPlayerMessage(string.Concat(new string[]
							{
								PourAmount.Things("dram", null),
								" of ",
								this.GetLiquidName(),
								" pours out all over ",
								Actor.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false),
								"!"
							}), null, true);
						}
						PourAmount -= this.ProcessContact(Actor, true, false, true, Actor, false, PourAmount);
						if (PourAmount <= 0)
						{
							return true;
						}
					}
					if (TargetCell == null)
					{
						return true;
					}
					if (!Douse && Actor != null)
					{
						if (Actor != null && Actor.IsPlayer())
						{
							IComponent<GameObject>.PlayUISound("Sounds/Abilities/sfx_ability_generic_waterPour", 1f, false, SoundRequest.SoundEffectType.None);
						}
						else if (TargetCell != null)
						{
							TargetCell.PlayWorldSound("Sounds/Abilities/sfx_ability_generic_waterPour", 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
						}
						IComponent<GameObject>.XDidY(Actor, "pour", PourAmount.Things("dram", null) + " of " + this.GetLiquidName() + " out", null, null, null, null, null, false, false, null, null, false, false, false, true, false, null);
					}
					if (!this.PourIntoCell(Actor, TargetCell, PourAmount, ref RequestInterfaceExit, !Forced, false, false, false))
					{
						return false;
					}
					return true;
				}
				else
				{
					if (Actor != null)
					{
						Actor.Fail("There's nothing in " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " to pour.");
						return true;
					}
					return true;
				}
				bool flag;
				return flag;
			}
			return true;
		}

		// Token: 0x06008E3C RID: 36412 RVA: 0x0032F654 File Offset: 0x0032D854
		public bool Pour(GameObject Actor = null, Cell TargetCell = null, bool Forced = false, bool Douse = false, int PourAmount = -1, bool OwnershipHandled = false)
		{
			bool flag = false;
			return this.Pour(ref flag, Actor, TargetCell, Forced, Douse, PourAmount, OwnershipHandled);
		}

		// Token: 0x06008E3D RID: 36413 RVA: 0x0032F674 File Offset: 0x0032D874
		public double GetLiquidWeight()
		{
			return (double)this.Volume * this.GetLiquidWeightPerDram();
		}

		// Token: 0x06008E3E RID: 36414 RVA: 0x0032F684 File Offset: 0x0032D884
		public void SmearOn(GameObject Object, GameObject By, bool FromCell)
		{
			int count = this.ComponentLiquids.Count;
			if (count != 0)
			{
				if (count == 1)
				{
					if (this.Primary == null)
					{
						string text = null;
						using (Dictionary<string, int>.KeyCollection.Enumerator enumerator = this.ComponentLiquids.Keys.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								text = enumerator.Current;
							}
						}
						if (text != null)
						{
							LiquidVolume.GetLiquid(text).SmearOn(this, Object, By, FromCell);
						}
					}
					else
					{
						this.RequirePrimaryLiquid().SmearOn(this, Object, By, FromCell);
					}
				}
				else
				{
					List<string> keylist = this.getKeylist();
					try
					{
						keylist.Clear();
						foreach (KeyValuePair<string, int> keyValuePair in this.ComponentLiquids)
						{
							keylist.Add(keyValuePair.Key);
						}
						foreach (string text2 in keylist)
						{
							LiquidVolume.GetLiquid(text2).SmearOn(this, Object, By, FromCell);
						}
					}
					finally
					{
						LiquidVolume.keylistPool.Enqueue(keylist);
					}
				}
			}
			this.ProcessExposure(Object, By, FromCell);
		}

		// Token: 0x06008E3F RID: 36415 RVA: 0x0032F7EC File Offset: 0x0032D9EC
		public void SmearOnTick(GameObject Object, GameObject By, bool FromCell)
		{
			int count = this.ComponentLiquids.Count;
			if (count != 0)
			{
				if (count == 1)
				{
					if (this.Primary == null)
					{
						string text = null;
						using (Dictionary<string, int>.KeyCollection.Enumerator enumerator = this.ComponentLiquids.Keys.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								text = enumerator.Current;
							}
						}
						if (text != null)
						{
							LiquidVolume.GetLiquid(text).SmearOnTick(this, Object, By, FromCell);
						}
					}
					else
					{
						this.RequirePrimaryLiquid().SmearOnTick(this, Object, By, FromCell);
					}
				}
				else
				{
					List<string> keylist = this.getKeylist();
					try
					{
						keylist.Clear();
						foreach (KeyValuePair<string, int> keyValuePair in this.ComponentLiquids)
						{
							keylist.Add(keyValuePair.Key);
						}
						foreach (string text2 in keylist)
						{
							LiquidVolume.GetLiquid(text2).SmearOnTick(this, Object, By, FromCell);
						}
					}
					finally
					{
						LiquidVolume.keylistPool.Enqueue(keylist);
					}
				}
			}
			this.ProcessExposure(Object, By, FromCell);
		}

		// Token: 0x06008E40 RID: 36416 RVA: 0x0032F954 File Offset: 0x0032DB54
		public bool CanEvaporate()
		{
			return this.IsOpenVolume() && !this.IsWadingDepth() && this.ComponentLiquids.Count != 1;
		}

		// Token: 0x06008E41 RID: 36417 RVA: 0x0032F97C File Offset: 0x0032DB7C
		public bool CheckGroundLiquidMerge()
		{
			if (this.Volume <= 0)
			{
				return false;
			}
			if (!this.IsOpenVolume())
			{
				return false;
			}
			if (this.IsWadingDepth())
			{
				return false;
			}
			if (this.ParentObject == null)
			{
				return false;
			}
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell == null)
			{
				return false;
			}
			string groundLiquid = currentCell.GroundLiquid;
			if (groundLiquid.IsNullOrEmpty())
			{
				return false;
			}
			if (!this.IsPureLiquid(groundLiquid, false))
			{
				return false;
			}
			this.ParentObject.Obliterate(null, false, null);
			return true;
		}

		// Token: 0x06008E42 RID: 36418 RVA: 0x0032F9F0 File Offset: 0x0032DBF0
		public bool PerformFill(GameObject Actor, ref bool RequestInterfaceExit, bool ownershipHandled = false)
		{
			if (this.EffectivelySealed())
			{
				return false;
			}
			if (this.ParentObject.IsInStasis())
			{
				if (Actor != null)
				{
					Actor.Fail("You cannot seem to interact with " + this.ParentObject.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " in any way.");
				}
				return false;
			}
			if (Actor.IsPlayer() && this.ParentObject.Owner != null && !ownershipHandled)
			{
				if (Popup.ShowYesNoCancel(string.Concat(new string[]
				{
					this.ParentObject.IndicativeDistal,
					this.ParentObject.Is,
					" not owned by you. Are you sure you want to take from ",
					this.ParentObject.them,
					"?"
				}), "Sounds/UI/ui_notification", true, DialogResult.Cancel) != DialogResult.Yes)
				{
					return false;
				}
				this.ParentObject.Physics.BroadcastForHelp(Actor);
			}
			if (!Actor.CheckFrozen(false, true, false, null))
			{
				return false;
			}
			List<GameObject> list = Event.NewGameObjectList();
			List<GameObject> list2 = Event.NewGameObjectList();
			List<GameObject> list3 = Event.NewGameObjectList();
			List<GameObject> list4 = Event.NewGameObjectList();
			Actor.GetInventoryAndEquipment(list4);
			int i = 0;
			int count = list4.Count;
			while (i < count)
			{
				GameObject gameObject = list4[i];
				if (gameObject != this.ParentObject)
				{
					LiquidVolume liquidVolume = gameObject.LiquidVolume;
					if (liquidVolume != null && !liquidVolume.EffectivelySealed())
					{
						if (liquidVolume.LiquidSameAs(this))
						{
							list.Add(gameObject);
						}
						else
						{
							list2.Add(gameObject);
						}
					}
				}
				i++;
			}
			list3.AddRange(list);
			list3.AddRange(list2);
			if (list3.Count == 0)
			{
				Actor.Fail("You have no containers to fill.");
				return false;
			}
			GameObject gameObject2 = PickItem.ShowPicker(list3, null, PickItem.PickItemDialogStyle.SelectItemDialog, Actor, null, null, "[Select a container to fill]", true, null, false, true, false);
			if (gameObject2 != null)
			{
				if (gameObject2 == this.ParentObject)
				{
					Actor.Fail("You can't pour from a container into " + this.ParentObject.itself + ".");
					return false;
				}
				try
				{
					gameObject2 = gameObject2.RemoveOne();
					LiquidVolume liquidVolume2 = gameObject2.LiquidVolume;
					bool flag = false;
					if (liquidVolume2.Volume > 0 && !liquidVolume2.LiquidSameAs(this))
					{
						if (Actor.IsPlayer())
						{
							DialogResult dialogResult = Popup.ShowYesNoCancel("Do you want to empty " + gameObject2.t(int.MaxValue, null, null, false, false, false, false, false, true, true, false, null, false, true, false, null, true, null, false) + " first?", "Sounds/UI/ui_notification", true, DialogResult.Cancel);
							if (dialogResult == DialogResult.Cancel)
							{
								return false;
							}
							if (dialogResult == DialogResult.Yes)
							{
								flag = true;
							}
						}
						else
						{
							flag = true;
						}
					}
					int num = Math.Min(flag ? liquidVolume2.MaxVolume : (liquidVolume2.MaxVolume - liquidVolume2.Volume), this.Volume);
					int num2 = 0;
					if (Actor.IsPlayer())
					{
						int? num3 = Popup.AskNumber("How many drams? (max=" + num.ToString() + ")", "Sounds/UI/ui_notification", "", num, 0, num);
						try
						{
							num2 = Convert.ToInt32(num3);
							goto IL_02E2;
						}
						catch
						{
							return false;
						}
					}
					num2 = num;
					IL_02E2:
					if (num2 > this.Volume)
					{
						num2 = this.Volume;
					}
					if (num2 <= 0)
					{
						return false;
					}
					base.PlayWorldSound("Sounds/Interact/sfx_interact_liquidContainer_fill", 0.5f, 0f, false, null, 0f, 1f, 1f, int.MaxValue);
					if (flag)
					{
						liquidVolume2.EmptyIntoCell(null, Actor);
					}
					int num4 = liquidVolume2.MaxVolume - liquidVolume2.Volume;
					int num5;
					if (num4 < this.Volume)
					{
						num5 = num4;
					}
					else
					{
						num5 = this.Volume;
					}
					if (num5 > num2)
					{
						num5 = num2;
					}
					LiquidVolume liquidVolume3 = liquidVolume2;
					int? num6 = new int?(num5);
					liquidVolume3.MixWith(this, ref RequestInterfaceExit, this.ParentObject, Actor, num6, false);
					this.CheckImage();
					liquidVolume2.CheckImage();
				}
				finally
				{
					gameObject2.CheckStack();
				}
				return true;
			}
			return true;
		}

		// Token: 0x06008E43 RID: 36419 RVA: 0x0032FDDC File Offset: 0x0032DFDC
		public bool PerformFill(GameObject Actor, bool ownershipHandled = false)
		{
			bool flag = false;
			return this.PerformFill(Actor, ref flag, ownershipHandled);
		}

		// Token: 0x06008E44 RID: 36420 RVA: 0x0032FDF5 File Offset: 0x0032DFF5
		private bool CanInteractWithAnything(Cell inCell = null)
		{
			return (inCell == null || !this.IsOpenVolume() || !inCell.HasBridge()) && !this.EffectivelySealed() && (!this.ParentObject.HasPart<FungalVision>() || FungalVisionary.VisionLevel > 0);
		}

		// Token: 0x06008E45 RID: 36421 RVA: 0x0032FE30 File Offset: 0x0032E030
		private bool CanInteractWith(GameObject obj, Cell inCell = null, bool AnythingChecked = false)
		{
			return GameObject.Validate(ref obj) && obj != this.ParentObject && obj.IsReal && !obj.IsScenery && !obj.IsBridge && (AnythingChecked || this.CanInteractWithAnything(inCell)) && this.ParentObject.PhaseAndFlightMatches(obj) && obj.GetMatterPhase() < 3;
		}

		// Token: 0x06008E46 RID: 36422 RVA: 0x0032FE9C File Offset: 0x0032E09C
		public bool CanPaintWith(int Group)
		{
			BaseLiquid primaryLiquid = this.GetPrimaryLiquid();
			return primaryLiquid != null && primaryLiquid.GetPaintGroup(this) == Group;
		}

		// Token: 0x06008E47 RID: 36423 RVA: 0x0032FEC0 File Offset: 0x0032E0C0
		public void Paint(int Mask)
		{
			if (this.LastPaintMask == Mask)
			{
				return;
			}
			BaseLiquid primaryLiquid = this.GetPrimaryLiquid();
			if (primaryLiquid == null)
			{
				return;
			}
			if (Mask == 0 && !this.IsWadingDepth())
			{
				this.ParentObject.Render.Tile = primaryLiquid.GetPuddle(this);
			}
			else
			{
				this.ParentObject.Render.Tile = LiquidVolume.PaintBuilder.Clear().Append(primaryLiquid.GetPaintAtlas(this)).Append(primaryLiquid.GetPaint(this))
					.Append('-')
					.AppendMask(Mask, 8)
					.Append(primaryLiquid.GetPaintExtension(this))
					.ToString();
			}
			this.LastPaintMask = Mask;
		}

		// Token: 0x06008E48 RID: 36424 RVA: 0x0032FF60 File Offset: 0x0032E160
		public static string GetCleaningLiquidGeneralization()
		{
			string text = null;
			foreach (BaseLiquid baseLiquid in LiquidVolume.Liquids.Values)
			{
				if (baseLiquid.EnableCleaning)
				{
					if (text == null)
					{
						text = baseLiquid.GetName().Strip();
					}
					else
					{
						if (text.Contains(" or "))
						{
							text = null;
							break;
						}
						string text2 = baseLiquid.GetName().Strip();
						if (text.CompareTo(text2) > 0)
						{
							text = text2 + " or " + text;
						}
						else
						{
							text = text + " or " + text2;
						}
					}
				}
			}
			return text ?? "liquid";
		}

		// Token: 0x04003B3B RID: 15163
		[GeneratedCode("PoolPartialsGenerator", "1.0.0.0")]
		private static readonly IPartPool LiquidVolumePool = new IPartPool();

		// Token: 0x04003B3C RID: 15164
		public const int SWIM_THRESHOLD = 2000;

		// Token: 0x04003B3D RID: 15165
		public const int WADE_THRESHOLD = 200;

		// Token: 0x04003B3E RID: 15166
		public const int CLEANING_MIXTURE_THRESHOLD = 2;

		// Token: 0x04003B3F RID: 15167
		public const int LIQUID_FLOWING = 1;

		// Token: 0x04003B40 RID: 15168
		public const int LIQUID_COLLECTOR = 2;

		// Token: 0x04003B41 RID: 15169
		public const int LIQUID_SEALED = 4;

		// Token: 0x04003B42 RID: 15170
		public const int LIQUID_MANUAL_SEAL = 8;

		// Token: 0x04003B43 RID: 15171
		public const int LIQUID_VISIBLE_WHEN_SEALED = 16;

		// Token: 0x04003B44 RID: 15172
		public const int LIQUID_SHOW_SEAL = 32;

		// Token: 0x04003B45 RID: 15173
		public const int LIQUID_HAS_DRAIN = 64;

		// Token: 0x04003B46 RID: 15174
		public int Flags = 32;

		// Token: 0x04003B47 RID: 15175
		[NonSerialized]
		public int FrameOffset;

		// Token: 0x04003B48 RID: 15176
		public int MaxVolume = -1;

		// Token: 0x04003B49 RID: 15177
		public int Volume;

		// Token: 0x04003B4A RID: 15178
		public string StartVolume = "";

		// Token: 0x04003B4B RID: 15179
		public string NamePreposition;

		// Token: 0x04003B4C RID: 15180
		public string UnknownNamePreposition;

		// Token: 0x04003B4D RID: 15181
		public string AutoCollectLiquidType;

		// Token: 0x04003B4E RID: 15182
		public string Primary;

		// Token: 0x04003B4F RID: 15183
		public string Secondary;

		// Token: 0x04003B50 RID: 15184
		public string _SmearedName;

		// Token: 0x04003B51 RID: 15185
		public string _SmearedColor;

		// Token: 0x04003B52 RID: 15186
		public string _StainedName;

		// Token: 0x04003B53 RID: 15187
		public string _StainedColor;

		// Token: 0x04003B54 RID: 15188
		public Dictionary<string, int> ComponentLiquids = new Dictionary<string, int>();

		// Token: 0x04003B55 RID: 15189
		[ModSensitiveStaticCache(false)]
		[NonSerialized]
		private static StringMap<BaseLiquid> _Liquids;

		// Token: 0x04003B56 RID: 15190
		private static List<KeyValuePair<string, int>> SortedComponents = new List<KeyValuePair<string, int>>(4);

		// Token: 0x04003B57 RID: 15191
		[NonSerialized]
		private static List<string> CombinedLiquidTypes = new List<string>();

		// Token: 0x04003B58 RID: 15192
		[NonSerialized]
		private static List<string> IncomingLiquidTypes = new List<string>();

		// Token: 0x04003B59 RID: 15193
		private static LiquidVolume tempVolume = new LiquidVolume();

		// Token: 0x04003B5A RID: 15194
		[NonSerialized]
		private static List<BodyPart> BodyParts = new List<BodyPart>();

		// Token: 0x04003B5B RID: 15195
		[NonSerialized]
		private static List<BodyPart> MobilityBodyParts = new List<BodyPart>();

		// Token: 0x04003B5C RID: 15196
		[NonSerialized]
		private static Dictionary<BodyPart, int> BodyPartCapacity = new Dictionary<BodyPart, int>();

		// Token: 0x04003B5D RID: 15197
		[NonSerialized]
		private static Dictionary<BodyPart, int> BodyPartExposure = new Dictionary<BodyPart, int>();

		// Token: 0x04003B5E RID: 15198
		[NonSerialized]
		private static string[] PuddleRenderStrings = new string[] { ",", "`", "'", "\a" };

		// Token: 0x04003B5F RID: 15199
		[NonSerialized]
		private static LiquidVolume consumeVolume;

		// Token: 0x04003B60 RID: 15200
		[NonSerialized]
		private static Dictionary<string, int> EvaporativityProportions = new Dictionary<string, int>();

		// Token: 0x04003B61 RID: 15201
		[NonSerialized]
		private static Dictionary<string, int> EvaporativityAmounts = new Dictionary<string, int>();

		// Token: 0x04003B62 RID: 15202
		[NonSerialized]
		private static Dictionary<string, int> StainingProportions = new Dictionary<string, int>();

		// Token: 0x04003B63 RID: 15203
		[NonSerialized]
		private static Dictionary<string, int> StainingAmounts = new Dictionary<string, int>();

		// Token: 0x04003B64 RID: 15204
		[NonSerialized]
		private static Queue<List<string>> keylistPool = new Queue<List<string>>();

		// Token: 0x04003B65 RID: 15205
		[NonSerialized]
		private static StringBuilder PaintBuilder = new StringBuilder();

		// Token: 0x04003B66 RID: 15206
		[NonSerialized]
		public int LastPaintMask = -1;

		// Token: 0x02001C46 RID: 7238
		private class ComponentSorter : IComparer<KeyValuePair<string, int>>
		{
			// Token: 0x0600F0AC RID: 61612 RVA: 0x0054A9A4 File Offset: 0x00548BA4
			public int Compare(KeyValuePair<string, int> A, KeyValuePair<string, int> B)
			{
				return B.Value.CompareTo(A.Value);
			}

			// Token: 0x04006FB0 RID: 28592
			public static readonly LiquidVolume.ComponentSorter Instance = new LiquidVolume.ComponentSorter();
		}
	}
}

using System;
using System.Collections.Generic;
using System.Reflection;
using ConsoleLib.Console;
using XRL.Wish;
using XRL.World.Parts;

namespace XRL.World
{
	// Token: 0x02000948 RID: 2376
	[HasWishCommand]
	[Serializable]
	public class Effect : IComponent<GameObject>
	{
		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06005BF3 RID: 23539 RVA: 0x001BCE9E File Offset: 0x001BB09E
		public sealed override IEventBinder Binder
		{
			get
			{
				return Effect.EventBinder.Instance;
			}
		}

		// Token: 0x06005BF4 RID: 23540 RVA: 0x001BCEA5 File Offset: 0x001BB0A5
		public sealed override void RegisterEvent(int EventID, int Order = 0, bool Serialize = false)
		{
			this.Object.RegisterEvent(this, EventID, Order, Serialize);
		}

		// Token: 0x06005BF5 RID: 23541 RVA: 0x001BCEB6 File Offset: 0x001BB0B6
		public sealed override void UnregisterEvent(int EventID)
		{
			this.Object.UnregisterEvent(this, EventID);
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06005BF6 RID: 23542 RVA: 0x001BCEC8 File Offset: 0x001BB0C8
		public virtual string ApplySound
		{
			get
			{
				int effectType = this.GetEffectType();
				if (effectType.HasBit(33554432))
				{
					if (effectType.HasBit(32768))
					{
						return "sfx_statusEffect_spacetimeWeirdness";
					}
					if (effectType.HasBit(2))
					{
						return "sfx_statusEffect_mentalImpairment";
					}
				}
				return null;
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06005BF7 RID: 23543 RVA: 0x001BCF0C File Offset: 0x001BB10C
		public virtual string RemoveSound
		{
			get
			{
				if (this.Object == null || !this.Object.IsPlayer() || this.IsOfTypes(16777216))
				{
					return null;
				}
				if (this.IsOfTypes(33554432))
				{
					return "sfx_statusEffect_negativeStatusEffect_stop";
				}
				return "sfx_statusEffect_stop";
			}
		}

		// Token: 0x06005BF8 RID: 23544 RVA: 0x001BCF4A File Offset: 0x001BB14A
		public Effect()
		{
			this.ID = Guid.NewGuid();
			this.DisplayName = "";
		}

		// Token: 0x06005BF9 RID: 23545 RVA: 0x001BCF68 File Offset: 0x001BB168
		public override GameObject GetComponentBasis()
		{
			return this.Object;
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06005BFA RID: 23546 RVA: 0x001BCF70 File Offset: 0x001BB170
		public string ClassName
		{
			get
			{
				string text;
				if ((text = this._ClassName) == null)
				{
					text = (this._ClassName = base.GetType().Name);
				}
				return text;
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06005BFB RID: 23547 RVA: 0x001BCF9B File Offset: 0x001BB19B
		public string DisplayNameStripped
		{
			get
			{
				return this.DisplayName.Strip();
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06005BFC RID: 23548 RVA: 0x001BCFA8 File Offset: 0x001BB1A8
		public override bool IsValid
		{
			get
			{
				return this._Object != null && this._Object.IsValid();
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06005BFD RID: 23549 RVA: 0x001BCFBF File Offset: 0x001BB1BF
		// (set) Token: 0x06005BFE RID: 23550 RVA: 0x001BCFC7 File Offset: 0x001BB1C7
		public GameObject Object
		{
			get
			{
				return this._Object;
			}
			set
			{
				this._Object = value;
				if (this._StatShifter != null)
				{
					this._StatShifter.Owner = this._Object;
				}
			}
		}

		// Token: 0x06005BFF RID: 23551 RVA: 0x001BCFE9 File Offset: 0x001BB1E9
		public bool HasStatShifts()
		{
			return this._StatShifter != null && this._StatShifter.HasStatShifts();
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06005C00 RID: 23552 RVA: 0x001BD000 File Offset: 0x001BB200
		public StatShifter StatShifter
		{
			get
			{
				if (this._StatShifter == null)
				{
					this._StatShifter = new StatShifter(this.Object, this.GetDescription());
				}
				return this._StatShifter;
			}
		}

		// Token: 0x06005C01 RID: 23553 RVA: 0x001BD027 File Offset: 0x001BB227
		public virtual bool allowCopyOnNoEffectDeepCopy()
		{
			return false;
		}

		// Token: 0x06005C02 RID: 23554 RVA: 0x001BD02A File Offset: 0x001BB22A
		public virtual int GetEffectType()
		{
			return 1;
		}

		// Token: 0x06005C03 RID: 23555 RVA: 0x001BD02D File Offset: 0x001BB22D
		public bool IsOfType(int Mask)
		{
			return this.GetEffectType().HasBit(Mask);
		}

		// Token: 0x06005C04 RID: 23556 RVA: 0x001BD03B File Offset: 0x001BB23B
		public bool IsOfTypes(int Mask)
		{
			return this.GetEffectType().HasAllBits(Mask);
		}

		// Token: 0x06005C05 RID: 23557 RVA: 0x001BD049 File Offset: 0x001BB249
		public virtual bool IsTonic()
		{
			return false;
		}

		// Token: 0x06005C06 RID: 23558 RVA: 0x001BD04C File Offset: 0x001BB24C
		public virtual string GetDescription()
		{
			if (this.DisplayName.Contains("<"))
			{
				return null;
			}
			return this.DisplayName;
		}

		// Token: 0x06005C07 RID: 23559 RVA: 0x001BD068 File Offset: 0x001BB268
		public virtual string GetStateDescription()
		{
			return this.GetDescription() ?? this.DisplayName;
		}

		// Token: 0x06005C08 RID: 23560 RVA: 0x001BD07A File Offset: 0x001BB27A
		public virtual bool SuppressInLookDisplay()
		{
			return false;
		}

		// Token: 0x06005C09 RID: 23561 RVA: 0x001BD07D File Offset: 0x001BB27D
		public virtual bool SuppressInStageDisplay()
		{
			return false;
		}

		// Token: 0x06005C0A RID: 23562 RVA: 0x001BD080 File Offset: 0x001BB280
		public static bool CanEffectTypeBeAppliedTo(int Type, GameObject Object)
		{
			if (Type.HasBit(2) && Object.Brain == null)
			{
				return false;
			}
			if ((Type.HasBit(4) || Type.HasBit(8)) && !Object.HasPart<Stomach>())
			{
				return false;
			}
			if (Type.HasBit(16) && Object.GetIntProperty("Bleeds", 0) <= 0)
			{
				return false;
			}
			if (Type.HasBit(128) && !Object.HasPart<Body>())
			{
				return false;
			}
			switch (Object.GetMatterPhase())
			{
			case 1:
				if (!Effect.CanEffectTypeBeAppliedToSolid(Type, null))
				{
					return false;
				}
				break;
			case 2:
				if (!Effect.CanEffectTypeBeAppliedToLiquid(Type, null))
				{
					return false;
				}
				break;
			case 3:
				if (!Effect.CanEffectTypeBeAppliedToGas(Type, null))
				{
					return false;
				}
				break;
			case 4:
				if (!Effect.CanEffectTypeBeAppliedToPlasma(Type, null))
				{
					return false;
				}
				break;
			}
			return true;
		}

		// Token: 0x06005C0B RID: 23563 RVA: 0x001BD13C File Offset: 0x001BB33C
		public bool CanBeAppliedTo(GameObject Object)
		{
			return Effect.CanEffectTypeBeAppliedTo(this.GetEffectType(), Object);
		}

		// Token: 0x06005C0C RID: 23564 RVA: 0x001BD14A File Offset: 0x001BB34A
		public static bool CanEffectTypeBeAppliedToSolid(int Type, GameObject Object = null)
		{
			return true;
		}

		// Token: 0x06005C0D RID: 23565 RVA: 0x001BD14D File Offset: 0x001BB34D
		public static bool CanEffectTypeBeAppliedToLiquid(int Type, GameObject Object = null)
		{
			return !Type.HasBit(32) && !Type.HasBit(1024);
		}

		// Token: 0x06005C0E RID: 23566 RVA: 0x001BD16B File Offset: 0x001BB36B
		public static bool CanEffectTypeBeAppliedToGas(int Type, GameObject Object = null)
		{
			return !Type.HasBit(32) && !Type.HasBit(4) && !Type.HasBit(1024);
		}

		// Token: 0x06005C0F RID: 23567 RVA: 0x001BD194 File Offset: 0x001BB394
		public static bool CanEffectTypeBeAppliedToPlasma(int Type, GameObject Object = null)
		{
			return !Type.HasBit(32) && !Type.HasBit(4) && !Type.HasBit(512) && !Type.HasBit(1024) && !Type.HasBit(2048);
		}

		// Token: 0x06005C10 RID: 23568 RVA: 0x001BD1E6 File Offset: 0x001BB3E6
		public virtual bool SameAs(Effect FX)
		{
			return !(FX.ClassName != this.ClassName) && !(FX.DisplayName != this.DisplayName) && FX.Duration == this.Duration;
		}

		// Token: 0x06005C11 RID: 23569 RVA: 0x001BD223 File Offset: 0x001BB423
		public virtual string GetDetails()
		{
			return "[effect details]";
		}

		// Token: 0x06005C12 RID: 23570 RVA: 0x001BD22A File Offset: 0x001BB42A
		public virtual void OnPaint(ScreenBuffer Buffer)
		{
		}

		// Token: 0x06005C13 RID: 23571 RVA: 0x001BD22C File Offset: 0x001BB42C
		public virtual bool RenderTile(ConsoleChar E)
		{
			return true;
		}

		// Token: 0x06005C14 RID: 23572 RVA: 0x001BD230 File Offset: 0x001BB430
		public virtual Effect DeepCopy(GameObject Parent)
		{
			Effect effect = (Effect)Activator.CreateInstance(base.GetType());
			foreach (FieldInfo fieldInfo in base.GetType().GetFields())
			{
				if ((fieldInfo.Attributes & FieldAttributes.NotSerialized) == FieldAttributes.PrivateScope && !fieldInfo.IsLiteral)
				{
					fieldInfo.SetValue(effect, fieldInfo.GetValue(this));
				}
			}
			effect.Object = Parent;
			if (this._StatShifter != null)
			{
				effect._StatShifter = new StatShifter(Parent, this._StatShifter);
			}
			return effect;
		}

		// Token: 0x06005C15 RID: 23573 RVA: 0x001BD2B2 File Offset: 0x001BB4B2
		public virtual Effect DeepCopy(GameObject Parent, Func<GameObject, GameObject> MapInv)
		{
			return this.DeepCopy(Parent);
		}

		// Token: 0x06005C16 RID: 23574 RVA: 0x001BD2BC File Offset: 0x001BB4BC
		public static void Save(Effect Effect, SerializationWriter Writer)
		{
			SerializationWriter.Block block = Writer.StartBlock();
			Type type = null;
			try
			{
				type = Effect.GetType();
				Writer.WriteTokenized(type);
				Writer.Write(Effect.ID);
				Writer.WriteOptimized(Effect.DisplayName);
				Writer.WriteOptimized(Effect.Duration);
				StatShifter.Save(Effect._StatShifter, Writer);
				Effect.SaveData(Writer);
			}
			catch (Exception ex)
			{
				block.Reset();
				Type type2 = type;
				string text = "Skipping failed serializion of effect '";
				string text2 = ((type != null) ? type.FullName : null);
				string text3 = "': ";
				Exception ex2 = ex;
				MetricsManager.LogAssemblyError(type2, text + text2 + text3 + ((ex2 != null) ? ex2.ToString() : null));
			}
			finally
			{
				block.Dispose();
			}
		}

		// Token: 0x06005C17 RID: 23575 RVA: 0x001BD374 File Offset: 0x001BB574
		public static Effect Load(GameObject Basis, SerializationReader Reader)
		{
			long num;
			int num2;
			Reader.StartBlock(out num, out num2);
			if (num2 == 0)
			{
				return null;
			}
			Type type = null;
			Effect effect = null;
			try
			{
				type = Reader.ReadTokenizedType();
				effect = (Effect)Activator.CreateInstance(type);
				effect._Object = Basis;
				effect.ID = Reader.ReadGuid();
				effect.DisplayName = Reader.ReadOptimizedString();
				effect.Duration = Reader.ReadOptimizedInt32();
				effect._StatShifter = StatShifter.Load(Reader, effect._Object);
				effect.LoadData(Reader);
			}
			catch (Exception ex)
			{
				if (effect == null || !effect.ReadError(ex, Reader, num, num2))
				{
					Reader.SkipBlock(ex, type, num, num2);
				}
			}
			return effect;
		}

		// Token: 0x06005C18 RID: 23576 RVA: 0x001BD420 File Offset: 0x001BB620
		[Obsolete("Override Write(GameObject, SerializationWriter")]
		public virtual void SaveData(SerializationWriter Writer)
		{
			this.Write(this._Object, Writer);
		}

		// Token: 0x06005C19 RID: 23577 RVA: 0x001BD42F File Offset: 0x001BB62F
		[Obsolete("Override Read(GameObject, SerializationReader")]
		public virtual void LoadData(SerializationReader Reader)
		{
			this.Read(this._Object, Reader);
		}

		// Token: 0x06005C1A RID: 23578 RVA: 0x001BD43E File Offset: 0x001BB63E
		[Obsolete("Override FinalizeRead(SerializationReader).")]
		public virtual void FinalizeLoad()
		{
		}

		// Token: 0x06005C1B RID: 23579 RVA: 0x001BD440 File Offset: 0x001BB640
		public virtual void FinalizeRead(SerializationReader Reader)
		{
			this.FinalizeLoad();
		}

		// Token: 0x06005C1C RID: 23580 RVA: 0x001BD448 File Offset: 0x001BB648
		public virtual bool Apply(GameObject Object)
		{
			return true;
		}

		// Token: 0x06005C1D RID: 23581 RVA: 0x001BD44B File Offset: 0x001BB64B
		public virtual void Remove(GameObject Object)
		{
		}

		// Token: 0x06005C1E RID: 23582 RVA: 0x001BD44D File Offset: 0x001BB64D
		public virtual void Applied(GameObject Object)
		{
		}

		// Token: 0x06005C1F RID: 23583 RVA: 0x001BD44F File Offset: 0x001BB64F
		public virtual bool CanApplyToStack()
		{
			return false;
		}

		// Token: 0x06005C20 RID: 23584 RVA: 0x001BD452 File Offset: 0x001BB652
		public virtual void WasUnstackedFrom(GameObject obj)
		{
		}

		// Token: 0x06005C21 RID: 23585 RVA: 0x001BD454 File Offset: 0x001BB654
		[Obsolete("Use Register(GameObject, IEventRegistrar)")]
		public virtual void Register(GameObject Object)
		{
		}

		// Token: 0x06005C22 RID: 23586 RVA: 0x001BD456 File Offset: 0x001BB656
		[Obsolete("Use Register(GameObject, IEventRegistrar)")]
		public virtual void Unregister(GameObject Object)
		{
		}

		/// <summary>Register to events from the <see cref="T:XRL.World.GameObject" />.</summary>
		/// <param name="Object">The current game object.</param>
		/// <param name="Registrar">An event registrar with this <see cref="T:XRL.World.Effect" /> and <see cref="T:XRL.World.GameObject" /> provisioned as defaults.</param>
		// Token: 0x06005C23 RID: 23587 RVA: 0x001BD458 File Offset: 0x001BB658
		public virtual void Register(GameObject Object, IEventRegistrar Registrar)
		{
		}

		/// <summary>Register to events from the <see cref="T:XRL.World.GameObject" /> while it is active in the action queue.</summary>
		/// <remarks>It is safer to register for external events here, since they're guaranteed to be cleaned up once the object goes out of scope.</remarks>
		/// <param name="Object">The current game object.</param>
		/// <param name="Registrar">An event registrar with this <see cref="T:XRL.World.Effect" /> and <see cref="T:XRL.World.GameObject" /> provisioned as defaults.</param>
		// Token: 0x06005C24 RID: 23588 RVA: 0x001BD45A File Offset: 0x001BB65A
		public virtual void RegisterActive(GameObject Object, IEventRegistrar Registrar)
		{
		}

		// Token: 0x06005C25 RID: 23589 RVA: 0x001BD45C File Offset: 0x001BB65C
		public virtual void ApplyRegistrar(GameObject Object, bool Active = false)
		{
			if (Active)
			{
				this.RegisterActive(Object, EventRegistrar.Get(Object, this));
				return;
			}
			this.Register(Object);
			this.Register(Object, EventRegistrar.Get(Object, this));
			int[] array;
			if (ComponentReflection.EffectEvents.TryGetValue(base.GetType(), out array))
			{
				Cell currentCell = Object.CurrentCell;
				if (currentCell != null)
				{
					currentCell.FlushRenderCache();
				}
				int i = 0;
				int num = array.Length;
				while (i < num)
				{
					Object.RegisterEvent(this, array[i], 0, false);
					i++;
				}
			}
			if (this.WantEvent(SingletonEvent<EndTurnEvent>.ID, EndTurnEvent.CascadeLevel))
			{
				Object.RegisterEvent(this, SingletonEvent<EndTurnEvent>.ID, 0, false);
			}
		}

		// Token: 0x06005C26 RID: 23590 RVA: 0x001BD4F4 File Offset: 0x001BB6F4
		public virtual void ApplyUnregistrar(GameObject Object, bool Active = false)
		{
			EventUnregistrar eventUnregistrar = EventUnregistrar.Get(Object, this);
			if (!Active)
			{
				this.Unregister(Object);
				this.Register(Object, eventUnregistrar);
				int[] array;
				if (ComponentReflection.EffectEvents.TryGetValue(base.GetType(), out array))
				{
					Cell currentCell = Object.CurrentCell;
					if (currentCell != null)
					{
						currentCell.FlushRenderCache();
					}
					int i = 0;
					int num = array.Length;
					while (i < num)
					{
						Object.UnregisterEvent(this, array[i]);
						i++;
					}
				}
				if (this.WantEvent(SingletonEvent<EndTurnEvent>.ID, EndTurnEvent.CascadeLevel))
				{
					Object.UnregisterEvent(this, SingletonEvent<EndTurnEvent>.ID);
				}
			}
			this.RegisterActive(Object, eventUnregistrar);
		}

		// Token: 0x06005C27 RID: 23591 RVA: 0x001BD580 File Offset: 0x001BB780
		public virtual void Expired()
		{
		}

		// Token: 0x06005C28 RID: 23592 RVA: 0x001BD582 File Offset: 0x001BB782
		public virtual bool UseStandardDurationCountdown()
		{
			return false;
		}

		// Token: 0x06005C29 RID: 23593 RVA: 0x001BD585 File Offset: 0x001BB785
		public virtual bool UseThawEventToUpdateDuration()
		{
			return false;
		}

		// Token: 0x06005C2A RID: 23594 RVA: 0x001BD588 File Offset: 0x001BB788
		public override bool WantEvent(int ID, int Cascade)
		{
			if (!base.WantEvent(ID, Cascade) && ID != SingletonEvent<GetDebugInternalsEvent>.ID)
			{
				if (ID == SingletonEvent<BeforeBeginTakeActionEvent>.ID && this.UseStandardDurationCountdown())
				{
					GameObject @object = this.Object;
					if (((@object != null) ? @object.Brain : null) != null)
					{
						return true;
					}
				}
				if (ID == SingletonEvent<EndTurnEvent>.ID && this.UseStandardDurationCountdown())
				{
					GameObject object2 = this.Object;
					if (((object2 != null) ? object2.Brain : null) == null)
					{
						return true;
					}
				}
				return ID == ZoneThawedEvent.ID && this.UseThawEventToUpdateDuration();
			}
			return true;
		}

		// Token: 0x06005C2B RID: 23595 RVA: 0x001BD604 File Offset: 0x001BB804
		public override bool HandleEvent(ZoneThawedEvent E)
		{
			if (this.UseThawEventToUpdateDuration() && E.TicksFrozen > 0L)
			{
				if (E.TicksFrozen > (long)this.Duration)
				{
					this.Duration = 0;
				}
				else
				{
					this.Duration -= (int)E.TicksFrozen;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06005C2C RID: 23596 RVA: 0x001BD658 File Offset: 0x001BB858
		public override bool HandleEvent(BeforeBeginTakeActionEvent E)
		{
			if (this.UseStandardDurationCountdown())
			{
				GameObject @object = this.Object;
				if (((@object != null) ? @object.Brain : null) != null && this.Duration > 0 && this.Duration != 9999)
				{
					this.Duration--;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06005C2D RID: 23597 RVA: 0x001BD6AC File Offset: 0x001BB8AC
		public override bool HandleEvent(EndTurnEvent E)
		{
			if (this.UseStandardDurationCountdown())
			{
				GameObject @object = this.Object;
				if (((@object != null) ? @object.Brain : null) == null && this.Duration > 0 && this.Duration != 9999)
				{
					this.Duration--;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06005C2E RID: 23598 RVA: 0x001BD700 File Offset: 0x001BB900
		public override bool HandleEvent(GetDebugInternalsEvent E)
		{
			E.AddEntry(this, "Duration", this.Duration);
			return base.HandleEvent(E);
		}

		// Token: 0x04002D61 RID: 11617
		public static readonly int ICON_COLOR_PRIORITY = 1000;

		// Token: 0x04002D62 RID: 11618
		public const int TYPE_GENERAL = 1;

		// Token: 0x04002D63 RID: 11619
		public const int TYPE_MENTAL = 2;

		// Token: 0x04002D64 RID: 11620
		public const int TYPE_METABOLIC = 4;

		// Token: 0x04002D65 RID: 11621
		public const int TYPE_RESPIRATORY = 8;

		// Token: 0x04002D66 RID: 11622
		public const int TYPE_CIRCULATORY = 16;

		// Token: 0x04002D67 RID: 11623
		public const int TYPE_CONTACT = 32;

		// Token: 0x04002D68 RID: 11624
		public const int TYPE_FIELD = 64;

		// Token: 0x04002D69 RID: 11625
		public const int TYPE_ACTIVITY = 128;

		// Token: 0x04002D6A RID: 11626
		public const int TYPE_DIMENSIONAL = 256;

		// Token: 0x04002D6B RID: 11627
		public const int TYPE_CHEMICAL = 512;

		// Token: 0x04002D6C RID: 11628
		public const int TYPE_STRUCTURAL = 1024;

		// Token: 0x04002D6D RID: 11629
		public const int TYPE_SONIC = 2048;

		// Token: 0x04002D6E RID: 11630
		public const int TYPE_TEMPORAL = 4096;

		// Token: 0x04002D6F RID: 11631
		public const int TYPE_NEUROLOGICAL = 8192;

		// Token: 0x04002D70 RID: 11632
		public const int TYPE_DISEASE = 16384;

		// Token: 0x04002D71 RID: 11633
		public const int TYPE_PSIONIC = 32768;

		// Token: 0x04002D72 RID: 11634
		public const int TYPE_POISON = 65536;

		// Token: 0x04002D73 RID: 11635
		public const int TYPE_EQUIPMENT = 131072;

		// Token: 0x04002D74 RID: 11636
		public const int TYPE_MINOR = 16777216;

		// Token: 0x04002D75 RID: 11637
		public const int TYPE_NEGATIVE = 33554432;

		// Token: 0x04002D76 RID: 11638
		public const int TYPE_REMOVABLE = 67108864;

		// Token: 0x04002D77 RID: 11639
		public const int TYPE_VOLUNTARY = 134217728;

		// Token: 0x04002D78 RID: 11640
		public const int TYPES_MECHANISM = 16777215;

		// Token: 0x04002D79 RID: 11641
		public const int TYPES_CLASS = 251658240;

		// Token: 0x04002D7A RID: 11642
		public const int DURATION_INDEFINITE = 9999;

		// Token: 0x04002D7B RID: 11643
		public static List<string> NegativeEffects;

		// Token: 0x04002D7C RID: 11644
		[NonSerialized]
		public GameObject _Object;

		// Token: 0x04002D7D RID: 11645
		[NonSerialized]
		public StatShifter _StatShifter;

		// Token: 0x04002D7E RID: 11646
		[NonSerialized]
		public Guid ID;

		// Token: 0x04002D7F RID: 11647
		[NonSerialized]
		public string DisplayName;

		// Token: 0x04002D80 RID: 11648
		[NonSerialized]
		public int Duration;

		/// <remarks>Type.Name allocates garbage, cache it here.</remarks>
		// Token: 0x04002D81 RID: 11649
		[NonSerialized]
		private string _ClassName;

		// Token: 0x02001A43 RID: 6723
		public class EventBinder : IEventBinder
		{
			// Token: 0x0600E6B9 RID: 59065 RVA: 0x00532BBD File Offset: 0x00530DBD
			public override void WriteBind(SerializationWriter Writer, IEventHandler Handler, int ID)
			{
				Writer.WriteGameObject(((Effect)Handler)._Object, true);
				Writer.WriteTokenized(Handler.GetType());
			}

			// Token: 0x0600E6BA RID: 59066 RVA: 0x00532BE0 File Offset: 0x00530DE0
			public override IEventHandler ReadBind(SerializationReader Reader, int ID)
			{
				GameObject gameObject = Reader.ReadGameObject(null);
				Type type = Reader.ReadTokenizedType();
				if (((gameObject != null) ? gameObject._Effects : null) != null)
				{
					foreach (Effect effect in gameObject._Effects)
					{
						if (effect.GetType() == type)
						{
							return effect;
						}
					}
				}
				return null;
			}

			// Token: 0x0400687B RID: 26747
			public static readonly Effect.EventBinder Instance = new Effect.EventBinder();
		}
	}
}

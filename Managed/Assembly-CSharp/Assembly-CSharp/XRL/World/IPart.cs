using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ConsoleLib.Console;
using XRL.World.Anatomy;
using XRL.World.Parts;
using XRL.World.Parts.Mutation;

namespace XRL.World
{
	// Token: 0x02000967 RID: 2407
	[Serializable]
	public class IPart : IComponent<GameObject>
	{
		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x0600635B RID: 25435 RVA: 0x001E8432 File Offset: 0x001E6632
		public sealed override IEventBinder Binder
		{
			get
			{
				return IPart.EventBinder.Instance;
			}
		}

		// Token: 0x0600635C RID: 25436 RVA: 0x001E8439 File Offset: 0x001E6639
		public sealed override void RegisterEvent(int EventID, int Order = 0, bool Serialize = false)
		{
			this.ParentObject.RegisterEvent(this, EventID, Order, Serialize);
		}

		// Token: 0x0600635D RID: 25437 RVA: 0x001E844A File Offset: 0x001E664A
		public sealed override void UnregisterEvent(int EventID)
		{
			this.ParentObject.UnregisterEvent(this, EventID);
		}

		// Token: 0x0600635E RID: 25438 RVA: 0x001E8459 File Offset: 0x001E6659
		public override GameObject GetComponentBasis()
		{
			return this._ParentObject;
		}

		// Token: 0x0600635F RID: 25439 RVA: 0x001E8461 File Offset: 0x001E6661
		public virtual void UpdateImposter(QudScreenBufferExtra extra)
		{
		}

		// Token: 0x06006360 RID: 25440 RVA: 0x001E8463 File Offset: 0x001E6663
		public virtual void OnPaint(ScreenBuffer buffer)
		{
		}

		// Token: 0x06006361 RID: 25441 RVA: 0x001E8465 File Offset: 0x001E6665
		public virtual bool AllowStaticRegistration()
		{
			return false;
		}

		// Token: 0x06006362 RID: 25442 RVA: 0x001E8468 File Offset: 0x001E6668
		public virtual bool CanGenerateStacked()
		{
			return this.SameAs(this);
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06006363 RID: 25443 RVA: 0x001E8471 File Offset: 0x001E6671
		// (set) Token: 0x06006364 RID: 25444 RVA: 0x001E8479 File Offset: 0x001E6679
		public virtual GameObject ParentObject
		{
			get
			{
				return this._ParentObject;
			}
			set
			{
				if (this._StatShifter != null)
				{
					this._StatShifter.Owner = value;
				}
				this._ParentObject = value;
			}
		}

		/// <summary>The priority of this part within the parent object's internals, which affects serialization and event cascade order.</summary>
		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06006365 RID: 25445 RVA: 0x001E8496 File Offset: 0x001E6696
		public virtual int Priority
		{
			get
			{
				return 45000;
			}
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x06006366 RID: 25446 RVA: 0x001E84A0 File Offset: 0x001E66A0
		public string Name
		{
			get
			{
				string text;
				if ((text = this._Name) == null)
				{
					text = (this._Name = ModManager.ResolveTypeName(base.GetType()));
				}
				return text;
			}
		}

		/// <summary>
		/// Generally don't use this. Used by the UI For certain naughty things.
		/// </summary>
		/// <param name="newName" />
		// Token: 0x06006367 RID: 25447 RVA: 0x001E84CB File Offset: 0x001E66CB
		public void SetName(string newName)
		{
			this._Name = newName;
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x06006368 RID: 25448 RVA: 0x001E84D4 File Offset: 0x001E66D4
		public override bool IsValid
		{
			get
			{
				return this._ParentObject != null && this._ParentObject.IsValid();
			}
		}

		/// <summary>The static pool instances of this part should be returned to.</summary>
		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x06006369 RID: 25449 RVA: 0x001E84EB File Offset: 0x001E66EB
		public virtual IPartPool Pool
		{
			get
			{
				return null;
			}
		}

		/// <summary>Reset the fields of this part to a cleared state.</summary>
		/// <remarks>Primarily used for pooled parts.</remarks>
		// Token: 0x0600636A RID: 25450 RVA: 0x001E84EE File Offset: 0x001E66EE
		public virtual void Reset()
		{
			this._ParentObject = null;
			this._Name = null;
		}

		// Token: 0x0600636B RID: 25451 RVA: 0x001E84FE File Offset: 0x001E66FE
		[Obsolete("Use Register(GameObject, IEventRegistrar)")]
		public virtual void Register(GameObject Object)
		{
		}

		/// <summary>Register to events from the <see cref="T:XRL.World.GameObject" />.</summary>
		/// <param name="Object">The current game object.</param>
		/// <param name="Registrar">An event registrar with this <see cref="T:XRL.World.IPart" /> and <see cref="T:XRL.World.GameObject" /> provisioned as defaults.</param>
		// Token: 0x0600636C RID: 25452 RVA: 0x001E8500 File Offset: 0x001E6700
		public virtual void Register(GameObject Object, IEventRegistrar Registrar)
		{
		}

		/// <summary>Register to events from the <see cref="T:XRL.World.GameObject" /> while it is active in the action queue.</summary>
		/// <remarks>It is safer to register for external events here, since they're guaranteed to be cleaned up once the object goes out of scope.</remarks>
		/// <param name="Object">The current game object.</param>
		/// <param name="Registrar">An event registrar with this <see cref="T:XRL.World.IPart" /> and <see cref="T:XRL.World.GameObject" /> provisioned as defaults.</param>
		// Token: 0x0600636D RID: 25453 RVA: 0x001E8502 File Offset: 0x001E6702
		public virtual void RegisterActive(GameObject Object, IEventRegistrar Registrar)
		{
		}

		// Token: 0x0600636E RID: 25454 RVA: 0x001E8504 File Offset: 0x001E6704
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
			if (ComponentReflection.PartEvents.TryGetValue(base.GetType(), out array))
			{
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

		// Token: 0x0600636F RID: 25455 RVA: 0x001E8588 File Offset: 0x001E6788
		public virtual void ApplyUnregistrar(GameObject Object, bool Active = false)
		{
			EventUnregistrar eventUnregistrar = EventUnregistrar.Get(Object, this);
			if (!Active)
			{
				this.Register(Object, eventUnregistrar);
				int[] array;
				if (ComponentReflection.PartEvents.TryGetValue(base.GetType(), out array))
				{
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

		// Token: 0x06006370 RID: 25456 RVA: 0x001E85FC File Offset: 0x001E67FC
		public virtual void Attach()
		{
		}

		// Token: 0x06006371 RID: 25457 RVA: 0x001E85FE File Offset: 0x001E67FE
		public virtual void Initialize()
		{
		}

		// Token: 0x06006372 RID: 25458 RVA: 0x001E8600 File Offset: 0x001E6800
		public virtual void AddedAfterCreation()
		{
		}

		// Token: 0x06006373 RID: 25459 RVA: 0x001E8602 File Offset: 0x001E6802
		public virtual void Remove()
		{
		}

		// Token: 0x06006374 RID: 25460 RVA: 0x001E8604 File Offset: 0x001E6804
		public virtual void ObjectLoaded()
		{
		}

		// Token: 0x06006375 RID: 25461 RVA: 0x001E8606 File Offset: 0x001E6806
		public virtual string[] GetStaticEvents()
		{
			return null;
		}

		// Token: 0x06006376 RID: 25462 RVA: 0x001E860C File Offset: 0x001E680C
		public void RegisterStaticEvents(GameObject GO)
		{
			string[] staticEvents = this.GetStaticEvents();
			if (staticEvents == null)
			{
				return;
			}
			int i = 0;
			int num = staticEvents.Length;
			while (i < num)
			{
				GO.RegisterPartEvent(this, staticEvents[i]);
				i++;
			}
		}

		// Token: 0x06006377 RID: 25463 RVA: 0x001E8640 File Offset: 0x001E6840
		public bool IsStaticEvent(string ID)
		{
			string[] staticEvents = this.GetStaticEvents();
			if (staticEvents == null)
			{
				return false;
			}
			int i = 0;
			int num = staticEvents.Length;
			while (i < num)
			{
				if (staticEvents[i] == ID)
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x06006378 RID: 25464 RVA: 0x001E8677 File Offset: 0x001E6877
		[Obsolete("Use static LoadBlueprint(GameObjectBlueprint)")]
		public virtual void LoadBlueprint()
		{
		}

		// Token: 0x06006379 RID: 25465 RVA: 0x001E8679 File Offset: 0x001E6879
		[Obsolete("Use static LoadBlueprint(GameObjectBlueprint)")]
		public virtual void GameStarted()
		{
		}

		// Token: 0x0600637A RID: 25466 RVA: 0x001E867B File Offset: 0x001E687B
		public virtual bool RenderTile(ConsoleChar E)
		{
			return true;
		}

		// Token: 0x0600637B RID: 25467 RVA: 0x001E8680 File Offset: 0x001E6880
		public static void Save(IPart Part, SerializationWriter Writer)
		{
			SerializationWriter.Block block = Writer.StartBlock();
			Type type = null;
			try
			{
				type = Part.GetType();
				Writer.WriteTokenized(type);
				StatShifter.Save(Part._StatShifter, Writer);
				Part.SaveData(Writer);
			}
			catch (Exception ex)
			{
				block.Reset();
				Type type2 = type;
				string text = "Skipping failed serialization of part '";
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

		// Token: 0x0600637C RID: 25468 RVA: 0x001E8714 File Offset: 0x001E6914
		public static IPart Load(GameObject Basis, SerializationReader Reader)
		{
			long num;
			int num2;
			Reader.StartBlock(out num, out num2);
			if (num2 == 0)
			{
				return null;
			}
			Type type = null;
			IPart part = null;
			try
			{
				type = Reader.ReadTokenizedType();
				part = GamePartBlueprint.PartReflectionCache.Get(type).GetInstance() ?? ((IPart)Activator.CreateInstance(type));
				part._ParentObject = Basis;
				part._StatShifter = StatShifter.Load(Reader, part._ParentObject);
				part.LoadData(Reader);
			}
			catch (Exception ex)
			{
				if (part == null || !part.ReadError(ex, Reader, num, num2))
				{
					Reader.SkipBlock(ex, type, num, num2);
				}
			}
			return part;
		}

		// Token: 0x0600637D RID: 25469 RVA: 0x001E87A8 File Offset: 0x001E69A8
		[Obsolete("Override Write(GameObject, SerializationWriter")]
		public virtual void SaveData(SerializationWriter Writer)
		{
			this.Write(this._ParentObject, Writer);
		}

		// Token: 0x0600637E RID: 25470 RVA: 0x001E87B7 File Offset: 0x001E69B7
		[Obsolete("Override Read(GameObject, SerializationReader")]
		public virtual void LoadData(SerializationReader Reader)
		{
			this.Read(this._ParentObject, Reader);
		}

		// Token: 0x0600637F RID: 25471 RVA: 0x001E87C6 File Offset: 0x001E69C6
		[Obsolete("Override FinalizeRead(SerializationReader).")]
		public virtual void FinalizeLoad()
		{
		}

		// Token: 0x06006380 RID: 25472 RVA: 0x001E87C8 File Offset: 0x001E69C8
		public virtual void FinalizeRead(SerializationReader Reader)
		{
			this.FinalizeLoad();
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x06006381 RID: 25473 RVA: 0x001E87D0 File Offset: 0x001E69D0
		private Dictionary<FieldInfo, AACopyFinalEntry> FinalizeCopyAbilities
		{
			get
			{
				Dictionary<FieldInfo, AACopyFinalEntry> dictionary;
				if (IPart._FinalizeCopyAbilities.TryGetValue(this, out dictionary))
				{
					return dictionary;
				}
				dictionary = new Dictionary<FieldInfo, AACopyFinalEntry>();
				IPart._FinalizeCopyAbilities.Add(this, dictionary);
				return dictionary;
			}
		}

		// Token: 0x06006382 RID: 25474 RVA: 0x001E8804 File Offset: 0x001E6A04
		public virtual IPart DeepCopy(GameObject Parent)
		{
			IPart part = (IPart)Activator.CreateInstance(base.GetType());
			IEnumerable<KeyValuePair<string, string>> enumerable = new Dictionary<string, string>();
			Dictionary<string, string> dest = new Dictionary<string, string>();
			enumerable.ToList<KeyValuePair<string, string>>().ForEach(delegate(KeyValuePair<string, string> kv)
			{
				dest.Add(kv.Key, kv.Value);
			});
			foreach (FieldInfo fieldInfo in base.GetType().GetFields())
			{
				if ((fieldInfo.Attributes & FieldAttributes.NotSerialized) == FieldAttributes.PrivateScope && !fieldInfo.IsLiteral)
				{
					if (fieldInfo.FieldType.FullName.Contains("ActivatedAbilityEntry"))
					{
						if (fieldInfo.GetValue(this) != null)
						{
							part.FinalizeCopyAbilities.Add(fieldInfo, new AACopyFinalEntry(((ActivatedAbilityEntry)fieldInfo.GetValue(this)).ID, part));
						}
					}
					else
					{
						fieldInfo.SetValue(part, fieldInfo.GetValue(this));
					}
				}
			}
			part.ParentObject = Parent;
			if (this.HasStatShifts())
			{
				part._StatShifter = new StatShifter(Parent, this._StatShifter);
			}
			return part;
		}

		// Token: 0x06006383 RID: 25475 RVA: 0x001E8900 File Offset: 0x001E6B00
		public virtual IPart DeepCopy(GameObject Parent, Func<GameObject, GameObject> MapInv)
		{
			return this.DeepCopy(Parent);
		}

		// Token: 0x06006384 RID: 25476 RVA: 0x001E890C File Offset: 0x001E6B0C
		public virtual void FinalizeCopyEarly(GameObject Source, bool CopyEffects, bool CopyID, Func<GameObject, GameObject> MapInv)
		{
			if (this.FinalizeCopyAbilities != null)
			{
				ActivatedAbilities part = this.ParentObject.GetPart<ActivatedAbilities>();
				if (part != null)
				{
					foreach (FieldInfo fieldInfo in this.FinalizeCopyAbilities.Keys)
					{
						fieldInfo.SetValue(this.FinalizeCopyAbilities[fieldInfo].Part, part.AbilityByGuid[this.FinalizeCopyAbilities[fieldInfo].ID]);
					}
					IPart._FinalizeCopyAbilities.Remove(this);
				}
			}
		}

		// Token: 0x06006385 RID: 25477 RVA: 0x001E89B8 File Offset: 0x001E6BB8
		public virtual void FinalizeCopy(GameObject Source, bool CopyEffects, bool CopyID, Func<GameObject, GameObject> MapInv)
		{
		}

		// Token: 0x06006386 RID: 25478 RVA: 0x001E89BA File Offset: 0x001E6BBA
		public virtual void FinalizeCopyLate(GameObject Source, bool CopyEffects, bool CopyID, Func<GameObject, GameObject> MapInv)
		{
		}

		// Token: 0x06006387 RID: 25479 RVA: 0x001E89BC File Offset: 0x001E6BBC
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x06006388 RID: 25480 RVA: 0x001E89C4 File Offset: 0x001E6BC4
		public virtual bool SameAs(IPart p)
		{
			return this.Name == p.Name;
		}

		// Token: 0x06006389 RID: 25481 RVA: 0x001E89D8 File Offset: 0x001E6BD8
		public void ForceDropSelf()
		{
			if (this.ParentObject.CurrentCell != null)
			{
				return;
			}
			if (this.ParentObject.Equipped != null)
			{
				Body body = this.ParentObject.Equipped.Body;
				BodyPart bodyPart = ((body != null) ? body.FindEquippedItem(this.ParentObject) : null);
				if (bodyPart != null)
				{
					this.ParentObject.FireEvent(Event.New("CommandForceUnequipObject", "BodyPart", bodyPart));
				}
			}
			GameObject inInventory = this.ParentObject.InInventory;
			if (inInventory != null)
			{
				InventoryActionEvent.Check(inInventory, inInventory, this.ParentObject, "CommandDropObject", false, false, false, true, false, 0, 0, 0, null, null, null, null);
			}
		}

		// Token: 0x0600638A RID: 25482 RVA: 0x001E8A70 File Offset: 0x001E6C70
		public bool ShouldUsePsychometry(GameObject who)
		{
			Psychometry psychometry;
			return who.TryGetPart<Psychometry>(out psychometry) && psychometry.Advisable(this.ParentObject);
		}

		// Token: 0x0600638B RID: 25483 RVA: 0x001E8A95 File Offset: 0x001E6C95
		public bool ShouldUsePsychometry()
		{
			return this.ShouldUsePsychometry(IComponent<GameObject>.ThePlayer);
		}

		// Token: 0x0600638C RID: 25484 RVA: 0x001E8AA2 File Offset: 0x001E6CA2
		public bool UsePsychometry(GameObject Actor, GameObject Subject)
		{
			Psychometry psychometry = ((Actor != null) ? Actor.GetPart<Psychometry>() : null);
			return psychometry != null && psychometry.Activate(Subject);
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x0600638D RID: 25485 RVA: 0x001E8ABC File Offset: 0x001E6CBC
		public StatShifter StatShifter
		{
			get
			{
				if (this._StatShifter == null)
				{
					this._StatShifter = new StatShifter(this.ParentObject, null);
				}
				return this._StatShifter;
			}
		}

		// Token: 0x0600638E RID: 25486 RVA: 0x001E8ADE File Offset: 0x001E6CDE
		public bool HasStatShifts()
		{
			return this._StatShifter != null && this._StatShifter.HasStatShifts();
		}

		// Token: 0x0600638F RID: 25487 RVA: 0x001E8AF5 File Offset: 0x001E6CF5
		public void CombatJuiceWait(float t)
		{
			GameObject parentObject = this.ParentObject;
			if (parentObject == null)
			{
				return;
			}
			parentObject.CombatJuiceWait(t);
		}

		/// <summary>Part is skipped by event cascades.</summary>
		// Token: 0x04002EBF RID: 11967
		public const int PRIORITY_SKIP = -2147483648;

		// Token: 0x04002EC0 RID: 11968
		public const int PRIORITY_VERY_LOW = 15000;

		// Token: 0x04002EC1 RID: 11969
		public const int PRIORITY_LOW = 30000;

		// Token: 0x04002EC2 RID: 11970
		public const int PRIORITY_MEDIUM = 45000;

		// Token: 0x04002EC3 RID: 11971
		public const int PRIORITY_HIGH = 60000;

		// Token: 0x04002EC4 RID: 11972
		public const int PRIORITY_VERY_HIGH = 75000;

		// Token: 0x04002EC5 RID: 11973
		public const int PRIORITY_INTEGRAL = 90000;

		// Token: 0x04002EC6 RID: 11974
		public const int PRIORITY_ADJUST_VERY_SMALL = 1;

		// Token: 0x04002EC7 RID: 11975
		public const int PRIORITY_ADJUST_SMALL = 10;

		// Token: 0x04002EC8 RID: 11976
		public const int PRIORITY_ADJUST_MEDIUM = 100;

		// Token: 0x04002EC9 RID: 11977
		public const int PRIORITY_ADJUST_LARGE = 1000;

		// Token: 0x04002ECA RID: 11978
		public const int PRIORITY_ADJUST_VERY_LARGE = 10000;

		// Token: 0x04002ECB RID: 11979
		[NonSerialized]
		public GameObject _ParentObject;

		// Token: 0x04002ECC RID: 11980
		[NonSerialized]
		private string _Name;

		// Token: 0x04002ECD RID: 11981
		[NonSerialized]
		private static readonly Dictionary<IPart, Dictionary<FieldInfo, AACopyFinalEntry>> _FinalizeCopyAbilities = new Dictionary<IPart, Dictionary<FieldInfo, AACopyFinalEntry>>();

		// Token: 0x04002ECE RID: 11982
		[NonSerialized]
		private StatShifter _StatShifter;

		// Token: 0x02001A6C RID: 6764
		public class EventBinder : IEventBinder
		{
			// Token: 0x0600E787 RID: 59271 RVA: 0x0053636D File Offset: 0x0053456D
			public override void WriteBind(SerializationWriter Writer, IEventHandler Handler, int ID)
			{
				Writer.WriteGameObject(((IPart)Handler)._ParentObject, true);
				Writer.WriteTokenized(Handler.GetType());
			}

			// Token: 0x0600E788 RID: 59272 RVA: 0x00536390 File Offset: 0x00534590
			public override IEventHandler ReadBind(SerializationReader Reader, int ID)
			{
				GameObject gameObject = Reader.ReadGameObject(null);
				Type type = Reader.ReadTokenizedType();
				if (gameObject != null)
				{
					foreach (IPart part in gameObject.PartsList)
					{
						if (part.GetType() == type)
						{
							return part;
						}
					}
				}
				return null;
			}

			// Token: 0x0400694C RID: 26956
			public static readonly IPart.EventBinder Instance = new IPart.EventBinder();
		}
	}
}

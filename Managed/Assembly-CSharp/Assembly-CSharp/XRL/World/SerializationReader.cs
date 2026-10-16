using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Genkit;
using UnityEngine;
using XRL.Collections;
using XRL.Serialization;
using XRL.World.Parts;

namespace XRL.World
{
	/// <summary>
	/// A SerializationReader instance is used to read stored values and objects from a byte array.
	///
	/// Once an instance is created, use the various methods to read the required data.
	/// The data read MUST be exactly the same type and in the same order as it was written.
	/// </summary>
	// Token: 0x02000918 RID: 2328
	public sealed class SerializationReader : BinaryReader
	{
		// Token: 0x060058B9 RID: 22713 RVA: 0x001AD754 File Offset: 0x001AB954
		public static bool TryGetShared(out SerializationReader Reader)
		{
			GameManager.IsOnGameContext();
			if (FastSerialization.SharedCache.Reserved)
			{
				Reader = null;
				return false;
			}
			Reader = SerializationReader.Instance;
			return FastSerialization.SharedCache.Reserved = true;
		}

		// Token: 0x060058BA RID: 22714 RVA: 0x001AD78D File Offset: 0x001AB98D
		public static void ReleaseShared()
		{
			SerializationReader.Instance.Clear();
			FastSerialization.SharedCache.Reserved = false;
		}

		// Token: 0x060058BB RID: 22715 RVA: 0x001AD7A4 File Offset: 0x001AB9A4
		public static SerializationReader Get()
		{
			SerializationReader serializationReader;
			if (SerializationReader.TryGetShared(out serializationReader))
			{
				return serializationReader;
			}
			return new SerializationReader(new FastSerialization.Cache(0));
		}

		// Token: 0x060058BC RID: 22716 RVA: 0x001AD7C7 File Offset: 0x001AB9C7
		public static void Release(SerializationReader Reader)
		{
			if (Reader == SerializationReader.Instance)
			{
				SerializationReader.ReleaseShared();
				return;
			}
			Reader.Dispose();
		}

		/// <summary>
		/// Creates a SerializationReader based on the passed Stream.
		/// </summary>
		/// <param name="Stream">The stream containing the serialized data</param>
		// Token: 0x060058BD RID: 22717 RVA: 0x001AD7E0 File Offset: 0x001AB9E0
		public SerializationReader(FastSerialization.Cache Cache)
			: base(Cache.MemoryStream, FastSerialization.Encoding, true)
		{
			this.Stream = Cache.MemoryStream;
			this.Cache = Cache;
		}

		/// <summary>
		/// Returns the number of bytes or serialized remaining to be processed.
		/// Useful for checking that deserialization is complete.
		///
		/// Warning: Retrieving the Position in certain stream types can be expensive,
		/// e.g. a FileStream, so use sparingly unless known to be a MemoryStream.
		/// </summary>
		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x060058BE RID: 22718 RVA: 0x001AD881 File Offset: 0x001ABA81
		public long BytesRemaining
		{
			get
			{
				return this.Stream.Length - this.Stream.Position;
			}
		}

		// Token: 0x060058BF RID: 22719 RVA: 0x001AD89C File Offset: 0x001ABA9C
		public void Start(bool SerializePlayer = false)
		{
			this.SerializePlayer = SerializePlayer;
			this.ReadHeader();
			MemoryStream stream = this.Stream;
			int num = this.EventRegistryLength;
			this.EventRegistries = this.Cache.EventRegistries.GetArray(num);
			for (int i = 0; i < num; i++)
			{
				this.EventRegistries[i] = EventRegistry.Get();
			}
			num = this.GameObjectLength;
			this.TokenGameObjects = this.Cache.GameObjects.GetArray(num);
			for (int j = 0; j < num; j++)
			{
				this.TokenGameObjects[j] = GameObject.Get();
			}
			stream.Position = this.StringPosition;
			num = this.ReadOptimizedInt32();
			this.TokenStrings = this.Cache.Strings.GetArray(num);
			for (int k = 0; k < num; k++)
			{
				this.TokenStrings[k] = base.ReadString();
			}
			stream.Position = 68L;
			this.ReadModVersions();
			this.ReadExtensionHash();
			long position = stream.Position;
			stream.Position = this.TypePosition;
			num = this.ReadOptimizedInt32();
			this.TokenTypes = this.Cache.Types.GetArray(num);
			this.TokenTypeNames = this.Cache.Names.GetArray(num);
			for (int l = 0; l < num; l++)
			{
				string text = "";
				try
				{
					text = (this.TokenTypeNames[l] = base.ReadString());
					this.TokenTypes[l] = ModManager.ResolveType(text, false, true, true);
				}
				catch (Exception ex)
				{
					MetricsManager.LogException("SerializationReader.TokenTypes:" + text, ex, "game_exception");
				}
			}
			stream.Position = this.GameObjectReferencePosition;
			num = this.ReadOptimizedInt32();
			this.GameObjectReferences = this.Cache.GameObjectReferences.GetArray(num);
			for (int m = 0; m < num; m++)
			{
				this.GameObjectReferences[m] = new GameObjectReference();
				this.GameObjectReferences[m].Read(this);
			}
			stream.Position = this.ObjectPosition;
			num = this.ReadOptimizedInt32();
			this.TokenObjects = this.Cache.Objects.GetArray(num);
			for (int n = 0; n < num; n++)
			{
				try
				{
					this.TokenObjects[n] = this.ReadObject();
				}
				catch (Exception ex2)
				{
					MetricsManager.LogException("SerializationReader.TokenObjects", ex2, "game_exception");
				}
			}
			stream.Position = this.TokenizedPosition;
			num = this.ReadOptimizedInt32();
			this.Tokenized = this.Cache.Tokenized.GetArray(num);
			for (int num2 = 0; num2 < num; num2++)
			{
				this.Tokenized[num2] = (ITokenized)this.ReadComposite();
			}
			stream.Position = position;
		}

		// Token: 0x060058C0 RID: 22720 RVA: 0x001ADB5C File Offset: 0x001ABD5C
		public void Clear()
		{
			this.Stream.SetLength(0L);
			Dictionary<string, Version> modVersions = this.ModVersions;
			if (modVersions != null)
			{
				modVersions.Clear();
			}
			List<GameObject> invalidGameObjects = this.InvalidGameObjects;
			if (invalidGameObjects != null)
			{
				invalidGameObjects.Clear();
			}
			Dictionary<GameObject, Cell> locations = this.Locations;
			if (locations != null)
			{
				locations.Clear();
			}
			Array.Clear(this.TokenStrings, 0, this.TokenStrings.Length);
			Array.Clear(this.TokenObjects, 0, this.TokenObjects.Length);
			Array.Clear(this.Tokenized, 0, this.Tokenized.Length);
			Array.Clear(this.TokenTypes, 0, this.TokenTypes.Length);
			Array.Clear(this.TokenTypeNames, 0, this.TokenTypeNames.Length);
			Array.Clear(this.TokenGameObjects, 0, this.TokenGameObjects.Length);
			Array.Clear(this.GameObjectReferences, 0, this.GameObjectReferences.Length);
			Array.Clear(this.EventRegistries, 0, this.EventRegistries.Length);
			this.Errors = 0;
			SerializationReader.formatter = null;
		}

		// Token: 0x060058C1 RID: 22721 RVA: 0x001ADC56 File Offset: 0x001ABE56
		protected override void Dispose(bool disposing)
		{
			SerializationReader.formatter = null;
			base.Dispose(disposing);
		}

		// Token: 0x060058C2 RID: 22722 RVA: 0x001ADC68 File Offset: 0x001ABE68
		public void ReadHeader()
		{
			this.FileVersion = this.ReadInt32();
			this.GameObjectPosition = this.ReadInt64();
			this.GameObjectReferencePosition = this.ReadInt64();
			this.EventRegistryPosition = this.ReadInt64();
			this.TokenizedPosition = this.ReadInt64();
			this.ObjectPosition = this.ReadInt64();
			this.TypePosition = this.ReadInt64();
			this.StringPosition = this.ReadInt64();
			this.GameObjectLength = this.ReadInt32();
			this.EventRegistryLength = this.ReadInt32();
		}

		/// <summary>
		/// Returns an ArrayList or null from the stream.
		/// </summary>
		/// <returns>An ArrayList instance.</returns>
		// Token: 0x060058C3 RID: 22723 RVA: 0x001ADCED File Offset: 0x001ABEED
		public ArrayList ReadArrayList()
		{
			if (this.readTypeCode() == SerializedType.NullType)
			{
				return null;
			}
			return new ArrayList(this.ReadOptimizedObjectArray());
		}

		/// <summary>
		/// Returns a BitArray or null from the stream.
		/// </summary>
		/// <returns>A BitArray instance.</returns>
		// Token: 0x060058C4 RID: 22724 RVA: 0x001ADD09 File Offset: 0x001ABF09
		public BitArray ReadBitArray()
		{
			if (this.readTypeCode() == SerializedType.NullType)
			{
				return null;
			}
			return this.ReadOptimizedBitArray();
		}

		/// <summary>
		/// Returns a BitVector32 value from the stream.
		/// </summary>
		/// <returns>A BitVector32 value.</returns>
		// Token: 0x060058C5 RID: 22725 RVA: 0x001ADD20 File Offset: 0x001ABF20
		public BitVector32 ReadBitVector32()
		{
			return new BitVector32(this.ReadInt32());
		}

		/// <summary>
		/// Reads the specified number of bytes directly from the stream.
		/// </summary>
		/// <param name="count">The number of bytes to read</param>
		/// <returns>A byte[] containing the read bytes</returns>
		// Token: 0x060058C6 RID: 22726 RVA: 0x001ADD2D File Offset: 0x001ABF2D
		public byte[] ReadBytesDirect(int count)
		{
			return base.ReadBytes(count);
		}

		/// <summary>
		/// Returns a DateTime value from the stream.
		/// </summary>
		/// <returns>A DateTime value.</returns>
		// Token: 0x060058C7 RID: 22727 RVA: 0x001ADD36 File Offset: 0x001ABF36
		public DateTime ReadDateTime()
		{
			return DateTime.FromBinary(this.ReadInt64());
		}

		/// <summary>
		/// Returns a Guid value from the stream.
		/// </summary>
		/// <returns>A DateTime value.</returns>
		// Token: 0x060058C8 RID: 22728 RVA: 0x001ADD43 File Offset: 0x001ABF43
		public Guid ReadGuid()
		{
			return new Guid(this.ReadBytes(16));
		}

		/// <summary>
		/// Returns an object based on the SerializedType read next from the stream.
		/// </summary>
		/// <returns>An object instance.</returns>
		// Token: 0x060058C9 RID: 22729 RVA: 0x001ADD52 File Offset: 0x001ABF52
		public object ReadObject()
		{
			return this.processObject((SerializedType)this.ReadByte());
		}

		// Token: 0x060058CA RID: 22730 RVA: 0x001ADD60 File Offset: 0x001ABF60
		public T ReadInstanceFields<T>(BindingFlags Flags = BindingFlags.Instance | BindingFlags.Public, FieldAttributes Mask = FieldAttributes.Static | FieldAttributes.Literal | FieldAttributes.NotSerialized) where T : new()
		{
			T t = new T();
			this.ReadTypeFields(t, typeof(T), Flags, Mask);
			return t;
		}

		// Token: 0x060058CB RID: 22731 RVA: 0x001ADD8C File Offset: 0x001ABF8C
		public void ReadFields<T>(T Instance, BindingFlags Flags = BindingFlags.Instance | BindingFlags.Public, FieldAttributes Mask = FieldAttributes.Static | FieldAttributes.Literal | FieldAttributes.NotSerialized)
		{
			this.ReadTypeFields(Instance, typeof(T), Flags, Mask);
		}

		// Token: 0x060058CC RID: 22732 RVA: 0x001ADDA8 File Offset: 0x001ABFA8
		public void ReadTypeFields(object Instance, Type Type, BindingFlags Flags = BindingFlags.Instance | BindingFlags.Public, FieldAttributes Mask = FieldAttributes.Static | FieldAttributes.Literal | FieldAttributes.NotSerialized)
		{
			foreach (FieldInfo fieldInfo in (Flags == (BindingFlags.Instance | BindingFlags.Public)) ? Type.GetCachedFields() : Type.GetFields(Flags))
			{
				if ((fieldInfo.Attributes & Mask) == FieldAttributes.PrivateScope)
				{
					FieldSaveVersion customAttribute = fieldInfo.GetCustomAttribute<FieldSaveVersion>();
					if (customAttribute == null || customAttribute.minimumSaveVersion <= -1 || customAttribute.minimumSaveVersion <= this.FileVersion)
					{
						fieldInfo.SetValue(Instance, this.ReadObject());
					}
				}
			}
		}

		// Token: 0x060058CD RID: 22733 RVA: 0x001ADE18 File Offset: 0x001AC018
		public void ReadNamedFields(object Instance, Type Type, BindingFlags Flags = BindingFlags.Instance | BindingFlags.Public, FieldAttributes Mask = FieldAttributes.Static | FieldAttributes.Literal | FieldAttributes.NotSerialized)
		{
			FieldInfo[] array = ((Flags == (BindingFlags.Instance | BindingFlags.Public)) ? Type.GetCachedFields() : Type.GetFields(Flags));
			int num = this.ReadOptimizedInt32();
			int i = 0;
			int num2 = array.Length;
			while (i < num)
			{
				string text = this.ReadOptimizedString();
				object obj = this.ReadObject();
				for (int j = 0; j < num2; j++)
				{
					FieldInfo fieldInfo = array[j];
					if (fieldInfo.Name == text)
					{
						fieldInfo.SetValue(Instance, obj);
						break;
					}
				}
				i++;
			}
		}

		// Token: 0x060058CE RID: 22734 RVA: 0x001ADE94 File Offset: 0x001AC094
		public void ReadGameObjectList(List<GameObject> List, string Forensics = null)
		{
			List.Clear();
			int num = this.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				List.Add(this.ReadGameObject(Forensics));
			}
		}

		// Token: 0x060058CF RID: 22735 RVA: 0x001ADEC7 File Offset: 0x001AC0C7
		public GameObject GetPlayer()
		{
			return this.TokenGameObjects[0];
		}

		// Token: 0x060058D0 RID: 22736 RVA: 0x001ADED4 File Offset: 0x001AC0D4
		public GameObject ReadGameObject(string Forensics = null)
		{
			int num = this.ReadOptimizedInt32() - 2;
			if (num == -2)
			{
				return null;
			}
			if (num == -1)
			{
				return GameObject.FindByID(this.ReadOptimizedString());
			}
			if (num != 0)
			{
				return this.TokenGameObjects[num];
			}
			if (!this.SerializePlayer)
			{
				return The.Player;
			}
			return this.GetPlayer();
		}

		// Token: 0x060058D1 RID: 22737 RVA: 0x001ADF24 File Offset: 0x001AC124
		public unsafe bool UnspoolTo(ushort Value)
		{
			Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)2], 2);
			*span[0] = (byte)(Value & 255);
			*span[1] = (byte)(Value >> 8);
			long num = this.BytesRemaining;
			int num2 = 0;
			int length = span.Length;
			while (num > 0L)
			{
				if (this.ReadByte() == *span[num2])
				{
					num2++;
					if (num2 >= length)
					{
						return true;
					}
				}
				else
				{
					num2 = 0;
				}
				num -= 1L;
			}
			return false;
		}

		// Token: 0x060058D2 RID: 22738 RVA: 0x001ADF98 File Offset: 0x001AC198
		public unsafe bool UnspoolTo(int Value, bool Prior = false)
		{
			Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)4], 4);
			BitConverter.TryWriteBytes(span, Value);
			long num = this.BytesRemaining;
			int num2 = 0;
			int length = span.Length;
			while (num > 0L)
			{
				if (this.ReadByte() == *span[num2])
				{
					num2++;
					if (num2 >= length)
					{
						if (Prior)
						{
							this.Stream.Position -= 4L;
						}
						return true;
					}
				}
				else
				{
					num2 = 0;
				}
				num -= 1L;
			}
			return false;
		}

		// Token: 0x060058D3 RID: 22739 RVA: 0x001AE00A File Offset: 0x001AC20A
		public void ReadEndVal()
		{
			if (this.ReadUInt16() != 44203)
			{
				throw new SerializationException("Invalid game object end value.");
			}
		}

		// Token: 0x060058D4 RID: 22740 RVA: 0x001AE024 File Offset: 0x001AC224
		public bool ReadStartVal()
		{
			ushort num = this.ReadUInt16();
			if (num == 64206)
			{
				return true;
			}
			if (num == 52958)
			{
				return false;
			}
			throw new SerializationException("Invalid game object start value.");
		}

		// Token: 0x060058D5 RID: 22741 RVA: 0x001AE056 File Offset: 0x001AC256
		public void FlagObjectForRemoval(GameObject Object)
		{
			if (this.InvalidGameObjects == null)
			{
				this.InvalidGameObjects = new List<GameObject>();
			}
			if (Object == null || this.InvalidGameObjects.Contains(Object))
			{
				return;
			}
			this.InvalidGameObjects.Add(Object);
		}

		// Token: 0x060058D6 RID: 22742 RVA: 0x001AE08C File Offset: 0x001AC28C
		public void HandleObjectError(GameObject Object)
		{
			if (Object == null)
			{
				return;
			}
			if (Object.IsValid())
			{
				foreach (IPart part in Object.PartsList)
				{
					part.BasisError(Object, this);
				}
				if (Object._Effects.IsNullOrEmpty<Effect>())
				{
					return;
				}
				using (Container<Effect>.Enumerator enumerator2 = Object._Effects.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						Effect effect = enumerator2.Current;
						effect.BasisError(Object, this);
					}
					return;
				}
			}
			this.FlagObjectForRemoval(Object);
		}

		// Token: 0x060058D7 RID: 22743 RVA: 0x001AE140 File Offset: 0x001AC340
		public void ReadGameObjects()
		{
			int num = (this.SerializePlayer ? 0 : 1);
			int num2 = 0;
			try
			{
				this.Stream.Position = this.GameObjectPosition;
				GameObject gameObject = null;
				long length = this.Stream.Length;
				long num3 = -1L;
				while (num3 < length)
				{
					try
					{
						num2 = this.Errors;
						if (!this.ReadStartVal())
						{
							break;
						}
						num++;
						gameObject = this.TokenGameObjects[this.ReadOptimizedInt32()];
						gameObject.Load(this);
						this.ReadEndVal();
						num3 = -1L;
						if (this.Errors != num2)
						{
							this.HandleObjectError(gameObject);
						}
					}
					catch (Exception ex)
					{
						this.Errors++;
						FieldInfo fieldInfo = ex.Data["Field"] as FieldInfo;
						Type type = (ex.Data["Type"] as Type) ?? ((fieldInfo != null) ? fieldInfo.DeclaringType : null);
						Type type2 = ex.Data["LastType"] as Type;
						string text = ex.Data["TypeName"] as string;
						StringBuilder stringBuilder = Event.NewStringBuilder(null);
						stringBuilder.Append("Blueprint: ").Append(((gameObject != null) ? gameObject.Blueprint : null) ?? "Unknown");
						if (type != null)
						{
							stringBuilder.Compound("Type: ", ", ").Append(type.FullName);
						}
						else if (text != null)
						{
							stringBuilder.Compound("Type: ", ", ").Append(text);
						}
						else if (type2 != null)
						{
							stringBuilder.Compound("LastType: ", ", ").Append(type2.Name);
						}
						if (fieldInfo != null)
						{
							stringBuilder.Compound("Field: ", ", ").Append(fieldInfo.Name);
						}
						stringBuilder.Compound(ex.ToString(), "\n\n");
						this.HandleObjectError(gameObject);
						if (num3 >= 0L && this.Stream.CanSeek)
						{
							this.Stream.Position = num3;
							num3 = -1L;
						}
						if (!this.UnspoolTo(44203))
						{
							throw;
						}
						num3 = this.Stream.Position;
						stringBuilder.Insert(0, "Recovered from game object deserialization error.\n");
						ModInfo modInfo;
						StackFrame stackFrame;
						if (type != null)
						{
							MetricsManager.LogAssemblyError(type, stringBuilder.ToString());
						}
						else if (ModManager.TryGetStackMod(ex, out modInfo, out stackFrame))
						{
							modInfo.Error(stringBuilder.ToString());
						}
						else
						{
							MetricsManager.LogError(stringBuilder.ToString());
						}
					}
					gameObject = null;
				}
			}
			catch (Exception ex2)
			{
				MetricsManager.LogException("Unrecoverable GameObject read error", ex2, "game_exception");
				throw;
			}
			finally
			{
				if (this.GameObjectLength != num)
				{
					MetricsManager.LogError(string.Format("All requested game objects have not been read ({0}/{1}, spool errors: {2})", num, this.GameObjectLength, num2));
				}
			}
		}

		// Token: 0x060058D8 RID: 22744 RVA: 0x001AE468 File Offset: 0x001AC668
		public void FinalizeRead()
		{
			this.ReadGameObjects();
			this.ReadEventRegistries();
			int i = 0;
			int gameObjectLength = this.GameObjectLength;
			while (i < gameObjectLength)
			{
				this.TokenGameObjects[i].FinalizeRead(this);
				i++;
			}
			GameObject.ExternalLoadBindings.Clear();
			this.SaveCompatibility();
			this.RemoveInvalidObjects();
		}

		// Token: 0x060058D9 RID: 22745 RVA: 0x001AE4B8 File Offset: 0x001AC6B8
		public void ReadEventRegistries()
		{
			if (this.EventRegistryLength == 0)
			{
				return;
			}
			try
			{
				this.Stream.Position = this.EventRegistryPosition;
				int i = 0;
				int eventRegistryLength = this.EventRegistryLength;
				while (i < eventRegistryLength)
				{
					this.EventRegistries[i].Read(this);
					i++;
				}
			}
			catch (Exception ex)
			{
				this.Errors++;
				this.Stream.Position = this.GameObjectReferencePosition;
				MetricsManager.LogException("Exception when reading event registries", ex, "game_exception");
			}
		}

		// Token: 0x060058DA RID: 22746 RVA: 0x001AE548 File Offset: 0x001AC748
		public void SaveCompatibility()
		{
			if (this.FileVersion <= 400)
			{
				foreach (GameObject gameObject in this.TokenGameObjects)
				{
					string text = ((gameObject != null) ? gameObject.Blueprint : null);
					if (text == "CarbideFist" || text == "FulleriteFist" || text == "CrysteelFist")
					{
						gameObject.SetStringProperty("TemporaryDefaultBehavior", "CyberneticsFistReplacement", false);
					}
				}
				foreach (GameObject gameObject2 in this.TokenGameObjects)
				{
					if (gameObject2 != null)
					{
						Body body = gameObject2.Body;
						if (body != null)
						{
							body.RegenerateDefaultEquipment();
						}
					}
				}
			}
		}

		// Token: 0x060058DB RID: 22747 RVA: 0x001AE5F1 File Offset: 0x001AC7F1
		private static bool IsRemovable(GameObject Object)
		{
			if (Object.Physics == null)
			{
				return true;
			}
			if (!Object.Physics.Takeable)
			{
				if (!Object.IsNatural())
				{
					return true;
				}
				Object.Physics.Takeable = true;
			}
			return false;
		}

		// Token: 0x060058DC RID: 22748 RVA: 0x001AE624 File Offset: 0x001AC824
		public void RemoveInvalidObjects()
		{
			if (this.InvalidGameObjects.IsNullOrEmpty<GameObject>())
			{
				return;
			}
			Dictionary<string, GameObject> cachedObjects = The.ZoneManager.CachedObjects;
			foreach (GameObject gameObject in this.InvalidGameObjects)
			{
				gameObject.RemoveFromContext(null);
				if (gameObject.HasID)
				{
					cachedObjects.Remove(gameObject.ID);
				}
			}
		}

		/// <summary>
		/// Called ReadOptimizedString().
		/// This override to hide base BinaryReader.ReadString().
		/// </summary>
		/// <returns>A string value.</returns>
		// Token: 0x060058DD RID: 22749 RVA: 0x001AE6A8 File Offset: 0x001AC8A8
		public override string ReadString()
		{
			return this.ReadOptimizedString();
		}

		/// <summary>
		/// Returns a string value from the stream.
		/// </summary>
		/// <returns>A string value.</returns>
		// Token: 0x060058DE RID: 22750 RVA: 0x001AE6B0 File Offset: 0x001AC8B0
		public string ReadStringDirect()
		{
			return base.ReadString();
		}

		/// <summary>
		/// Returns a TimeSpan value from the stream.
		/// </summary>
		/// <returns>A TimeSpan value.</returns>
		// Token: 0x060058DF RID: 22751 RVA: 0x001AE6B8 File Offset: 0x001AC8B8
		public TimeSpan ReadTimeSpan()
		{
			return new TimeSpan(this.ReadInt64());
		}

		/// <summary>
		/// Returns a Type or null from the stream.
		///
		/// Throws an exception if the Type cannot be found.
		/// </summary>
		/// <returns>A Type instance.</returns>
		// Token: 0x060058E0 RID: 22752 RVA: 0x001AE6C5 File Offset: 0x001AC8C5
		public Type ReadType()
		{
			return this.ReadType(true);
		}

		/// <summary>
		/// Returns a Type or null from the stream.
		///
		/// Throws an exception if the Type cannot be found and throwOnError is true.
		/// </summary>
		/// <returns>A Type instance.</returns>
		// Token: 0x060058E1 RID: 22753 RVA: 0x001AE6CE File Offset: 0x001AC8CE
		public Type ReadType(bool throwOnError)
		{
			if (this.readTypeCode() == SerializedType.NullType)
			{
				return null;
			}
			return Type.GetType(this.ReadOptimizedString(), throwOnError);
		}

		/// <summary>
		/// Returns an ArrayList from the stream that was stored optimized.
		/// </summary>
		/// <returns>An ArrayList instance.</returns>
		// Token: 0x060058E2 RID: 22754 RVA: 0x001AE6EB File Offset: 0x001AC8EB
		public ArrayList ReadOptimizedArrayList()
		{
			return new ArrayList(this.ReadOptimizedObjectArray());
		}

		/// <summary>
		/// Returns a BitArray from the stream that was stored optimized.
		/// </summary>
		/// <returns>A BitArray instance.</returns>
		// Token: 0x060058E3 RID: 22755 RVA: 0x001AE6F8 File Offset: 0x001AC8F8
		public BitArray ReadOptimizedBitArray()
		{
			int num = this.ReadOptimizedInt32();
			if (num == 0)
			{
				return SerializationReader.FullyOptimizableTypedArray;
			}
			return new BitArray(base.ReadBytes((num + 7) / 8))
			{
				Length = num
			};
		}

		/// <summary>
		/// Returns a BitVector32 value from the stream that was stored optimized.
		/// </summary>
		/// <returns>A BitVector32 value.</returns>
		// Token: 0x060058E4 RID: 22756 RVA: 0x001AE72C File Offset: 0x001AC92C
		public BitVector32 ReadOptimizedBitVector32()
		{
			return new BitVector32(base.Read7BitEncodedInt());
		}

		/// <summary>
		/// Returns a DateTime value from the stream that was stored optimized.
		/// </summary>
		/// <returns>A DateTime value.</returns>
		// Token: 0x060058E5 RID: 22757 RVA: 0x001AE73C File Offset: 0x001AC93C
		public DateTime ReadOptimizedDateTime()
		{
			BitVector32 bitVector = new BitVector32((int)this.ReadByte() | ((int)this.ReadByte() << 8) | ((int)this.ReadByte() << 16));
			DateTime dateTime = new DateTime(bitVector[SerializationWriter.DateYearMask], bitVector[SerializationWriter.DateMonthMask], bitVector[SerializationWriter.DateDayMask]);
			if (bitVector[SerializationWriter.DateHasTimeOrKindMask] == 1)
			{
				byte b = this.ReadByte();
				DateTimeKind dateTimeKind = (DateTimeKind)(b & 3);
				b &= 252;
				if (dateTimeKind != DateTimeKind.Unspecified)
				{
					dateTime = DateTime.SpecifyKind(dateTime, dateTimeKind);
				}
				if (b == 0)
				{
					this.ReadByte();
				}
				else
				{
					dateTime = dateTime.Add(this.decodeTimeSpan(b));
				}
			}
			return dateTime;
		}

		/// <summary>
		/// Returns a Decimal value from the stream that was stored optimized.
		/// </summary>
		/// <returns>A Decimal value.</returns>
		// Token: 0x060058E6 RID: 22758 RVA: 0x001AE7E0 File Offset: 0x001AC9E0
		public decimal ReadOptimizedDecimal()
		{
			byte b = this.ReadByte();
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			byte b2 = 0;
			if ((b & 2) != 0)
			{
				b2 = this.ReadByte();
			}
			if ((b & 4) == 0)
			{
				if ((b & 32) != 0)
				{
					num = this.ReadOptimizedInt32();
				}
				else
				{
					num = this.ReadInt32();
				}
			}
			if ((b & 8) == 0)
			{
				if ((b & 64) != 0)
				{
					num2 = this.ReadOptimizedInt32();
				}
				else
				{
					num2 = this.ReadInt32();
				}
			}
			if ((b & 16) == 0)
			{
				if ((b & 128) != 0)
				{
					num3 = this.ReadOptimizedInt32();
				}
				else
				{
					num3 = this.ReadInt32();
				}
			}
			return new decimal(num, num2, num3, (b & 1) > 0, b2);
		}

		/// <summary>
		/// Returns an Int32 value from the stream that was stored optimized.
		/// </summary>
		/// <returns>An Int32 value.</returns>
		// Token: 0x060058E7 RID: 22759 RVA: 0x001AE870 File Offset: 0x001ACA70
		public int ReadOptimizedInt32()
		{
			int num = 0;
			int num2 = 0;
			byte b;
			do
			{
				b = this.ReadByte();
				num |= (int)(b & 127) << num2;
				num2 += 7;
			}
			while ((b & 128) != 0);
			return num;
		}

		/// <summary>
		/// Returns an Int16 value from the stream that was stored optimized.
		/// </summary>
		/// <returns>An Int16 value.</returns>
		// Token: 0x060058E8 RID: 22760 RVA: 0x001AE8A2 File Offset: 0x001ACAA2
		public short ReadOptimizedInt16()
		{
			return (short)this.ReadOptimizedInt32();
		}

		/// <summary>
		/// Returns an Int64 value from the stream that was stored optimized.
		/// </summary>
		/// <returns>An Int64 value.</returns>
		// Token: 0x060058E9 RID: 22761 RVA: 0x001AE8AC File Offset: 0x001ACAAC
		public long ReadOptimizedInt64()
		{
			long num = 0L;
			int num2 = 0;
			byte b;
			do
			{
				b = this.ReadByte();
				num |= (long)((long)((ulong)b & 127UL) << num2);
				num2 += 7;
			}
			while ((b & 128) != 0);
			return num;
		}

		/// <summary>
		/// Returns an object[] from the stream that was stored optimized.
		/// </summary>
		/// <returns>An object[] instance.</returns>
		// Token: 0x060058EA RID: 22762 RVA: 0x001AE8E1 File Offset: 0x001ACAE1
		public object[] ReadOptimizedObjectArray()
		{
			return this.ReadOptimizedObjectArray(null);
		}

		/// <summary>
		/// Returns an object[] from the stream that was stored optimized.
		/// The returned array will be typed according to the specified element type
		/// and the resulting array can be cast to the expected type.
		/// e.g.
		/// string[] myStrings = (string[]) reader.ReadOptimizedObjectArray(typeof(string));
		///
		/// An exception will be thrown if any of the deserialized values cannot be
		/// cast to the specified elementType.
		///
		/// </summary>
		/// <param name="elementType">The Type of the expected array elements. null will return a plain object[].</param>
		/// <returns>An object[] instance.</returns>
		// Token: 0x060058EB RID: 22763 RVA: 0x001AE8EC File Offset: 0x001ACAEC
		public object[] ReadOptimizedObjectArray(Type elementType)
		{
			int num = this.ReadOptimizedInt32();
			object[] array = (object[])((elementType == null) ? new object[num] : Array.CreateInstance(elementType, num));
			for (int i = 0; i < array.Length; i++)
			{
				SerializedType serializedType = (SerializedType)this.ReadByte();
				if (serializedType == SerializedType.NullSequenceType)
				{
					i += this.ReadOptimizedInt32();
				}
				else if (serializedType == SerializedType.DuplicateValueSequenceType)
				{
					object obj = (array[i] = this.ReadObject());
					int num2 = this.ReadOptimizedInt32();
					while (num2-- > 0)
					{
						array[++i] = obj;
					}
				}
				else if (serializedType == SerializedType.DBNullSequenceType)
				{
					int num3 = this.ReadOptimizedInt32();
					array[i] = DBNull.Value;
					while (num3-- > 0)
					{
						array[++i] = DBNull.Value;
					}
				}
				else if (serializedType != SerializedType.NullType)
				{
					array[i] = this.processObject(serializedType);
				}
			}
			return array;
		}

		/// <summary>
		/// Returns a pair of object[] arrays from the stream that were stored optimized.
		/// </summary>
		/// <returns>A pair of object[] arrays.</returns>
		// Token: 0x060058EC RID: 22764 RVA: 0x001AE9C8 File Offset: 0x001ACBC8
		public void ReadOptimizedObjectArrayPair(out object[] values1, out object[] values2)
		{
			values1 = this.ReadOptimizedObjectArray(null);
			values2 = new object[values1.Length];
			for (int i = 0; i < values2.Length; i++)
			{
				SerializedType serializedType = (SerializedType)this.ReadByte();
				if (serializedType == SerializedType.DuplicateValueSequenceType)
				{
					values2[i] = values1[i];
					int num = this.ReadOptimizedInt32();
					while (num-- > 0)
					{
						values2[++i] = values1[i];
					}
				}
				else if (serializedType == SerializedType.DuplicateValueType)
				{
					values2[i] = values1[i];
				}
				else if (serializedType == SerializedType.NullSequenceType)
				{
					i += this.ReadOptimizedInt32();
				}
				else if (serializedType == SerializedType.DBNullSequenceType)
				{
					int num2 = this.ReadOptimizedInt32();
					values2[i] = DBNull.Value;
					while (num2-- > 0)
					{
						values2[++i] = DBNull.Value;
					}
				}
				else if (serializedType != SerializedType.NullType)
				{
					values2[i] = this.processObject(serializedType);
				}
			}
		}

		/// <summary>
		/// Returns a string value from the stream that was stored optimized.
		/// </summary>
		/// <returns>A string value.</returns>
		// Token: 0x060058ED RID: 22765 RVA: 0x001AEAA0 File Offset: 0x001ACCA0
		public string ReadOptimizedString()
		{
			SerializedType serializedType = this.readTypeCode();
			if (serializedType < SerializedType.NullType)
			{
				return this.readTokenizedString((int)serializedType);
			}
			if (serializedType == SerializedType.NullType)
			{
				return null;
			}
			if (serializedType == SerializedType.YStringType)
			{
				return "Y";
			}
			if (serializedType == SerializedType.NStringType)
			{
				return "N";
			}
			if (serializedType == SerializedType.SingleCharStringType)
			{
				return char.ToString(this.ReadChar());
			}
			if (serializedType == SerializedType.SingleSpaceType)
			{
				return " ";
			}
			if (serializedType == SerializedType.EmptyStringType)
			{
				return string.Empty;
			}
			throw new InvalidOperationException("Unrecognized TypeCode, expected a type of string instead got: " + serializedType.ToString());
		}

		/// <summary>
		/// Returns a TimeSpan value from the stream that was stored optimized.
		/// </summary>
		/// <returns>A TimeSpan value.</returns>
		// Token: 0x060058EE RID: 22766 RVA: 0x001AEB36 File Offset: 0x001ACD36
		public TimeSpan ReadOptimizedTimeSpan()
		{
			return this.decodeTimeSpan(this.ReadByte());
		}

		/// <summary>
		/// Returns a Type from the stream.
		///
		/// Throws an exception if the Type cannot be found.
		/// </summary>
		/// <returns>A Type instance.</returns>
		// Token: 0x060058EF RID: 22767 RVA: 0x001AEB44 File Offset: 0x001ACD44
		public Type ReadOptimizedType()
		{
			return this.ReadOptimizedType(true);
		}

		/// <summary>
		/// Returns a Type from the stream.
		///
		/// Throws an exception if the Type cannot be found and throwOnError is true.
		/// </summary>
		/// <returns>A Type instance.</returns>
		// Token: 0x060058F0 RID: 22768 RVA: 0x001AEB4D File Offset: 0x001ACD4D
		public Type ReadOptimizedType(bool throwOnError)
		{
			return ModManager.ResolveType(this.ReadOptimizedString(), false, false, true);
		}

		// Token: 0x060058F1 RID: 22769 RVA: 0x001AEB5D File Offset: 0x001ACD5D
		public Type ReadDirectType(bool ThrowOnError = false)
		{
			return ModManager.ResolveType(base.ReadString(), false, ThrowOnError, true);
		}

		/// <summary>
		/// Returns a UInt16 value from the stream that was stored optimized.
		/// </summary>
		/// <returns>A UInt16 value.</returns>
		// Token: 0x060058F2 RID: 22770 RVA: 0x001AEB6D File Offset: 0x001ACD6D
		[CLSCompliant(false)]
		public ushort ReadOptimizedUInt16()
		{
			return (ushort)this.ReadOptimizedUInt32();
		}

		/// <summary>
		/// Returns a UInt32 value from the stream that was stored optimized.
		/// </summary>
		/// <returns>A UInt32 value.</returns>
		// Token: 0x060058F3 RID: 22771 RVA: 0x001AEB78 File Offset: 0x001ACD78
		[CLSCompliant(false)]
		public uint ReadOptimizedUInt32()
		{
			uint num = 0U;
			int num2 = 0;
			byte b;
			do
			{
				b = this.ReadByte();
				num |= (uint)((uint)(b & 127) << num2);
				num2 += 7;
			}
			while ((b & 128) != 0);
			return num;
		}

		/// <summary>
		/// Returns a UInt64 value from the stream that was stored optimized.
		/// </summary>
		/// <returns>A UInt64 value.</returns>
		// Token: 0x060058F4 RID: 22772 RVA: 0x001AEBAC File Offset: 0x001ACDAC
		[CLSCompliant(false)]
		public ulong ReadOptimizedUInt64()
		{
			ulong num = 0UL;
			int num2 = 0;
			byte b;
			do
			{
				b = this.ReadByte();
				num |= ((ulong)b & 127UL) << num2;
				num2 += 7;
			}
			while ((b & 128) != 0);
			return num;
		}

		/// <summary>
		/// Returns a typed array from the stream.
		/// </summary>
		/// <returns>A typed array.</returns>
		// Token: 0x060058F5 RID: 22773 RVA: 0x001AEBE1 File Offset: 0x001ACDE1
		public Array ReadTypedArray()
		{
			return (Array)this.processArrayTypes(this.readTypeCode(), null);
		}

		/// <summary>
		/// Returns a new, simple generic dictionary populated with keys and values from the stream.
		/// </summary>
		/// <typeparam name="K">The key Type.</typeparam>
		/// <typeparam name="V">The value Type.</typeparam>
		/// <returns>A new, simple, populated generic Dictionary.</returns>
		// Token: 0x060058F6 RID: 22774 RVA: 0x001AEBF8 File Offset: 0x001ACDF8
		public Dictionary<K, V> ReadDictionary<K, V>()
		{
			int num = this.ReadInt32();
			if (num == -1)
			{
				return null;
			}
			Dictionary<K, V> dictionary = new Dictionary<K, V>(num);
			for (int i = 0; i < num; i++)
			{
				dictionary.Add((K)((object)this.ReadObject()), (V)((object)this.ReadObject()));
			}
			return dictionary;
		}

		/// <summary>
		/// Populates a pre-existing generic dictionary with keys and values from the stream.
		/// This allows a generic dictionary to be created without using the default constructor.
		/// </summary>
		/// <typeparam name="K">The key Type.</typeparam>
		/// <typeparam name="V">The value Type.</typeparam>
		// Token: 0x060058F7 RID: 22775 RVA: 0x001AEC44 File Offset: 0x001ACE44
		public void ReadDictionary<K, V>(Dictionary<K, V> dictionary)
		{
			K[] array = (K[])this.processArrayTypes(this.readTypeCode(), typeof(K));
			V[] array2 = (V[])this.processArrayTypes(this.readTypeCode(), typeof(V));
			if (dictionary == null)
			{
				dictionary = new Dictionary<K, V>(array.Length);
			}
			for (int i = 0; i < array.Length; i++)
			{
				dictionary.Add(array[i], array2[i]);
			}
		}

		/// <summary>
		/// Returns a generic List populated with values from the stream.
		/// </summary>
		/// <typeparam name="T">The list Type.</typeparam>
		/// <returns>A new generic List.</returns>
		// Token: 0x060058F8 RID: 22776 RVA: 0x001AECB8 File Offset: 0x001ACEB8
		public List<T> ReadList<T>()
		{
			int num = this.ReadInt32();
			if (num == -1)
			{
				return null;
			}
			List<T> list = new List<T>(num);
			for (int i = 0; i < num; i++)
			{
				list.Add((T)((object)this.ReadObject()));
			}
			return list;
		}

		// Token: 0x060058F9 RID: 22777 RVA: 0x001AECF8 File Offset: 0x001ACEF8
		public void ReadList<T>(List<T> List)
		{
			int num = this.ReadInt32();
			List.EnsureCapacity<T>(num);
			for (int i = 0; i < num; i++)
			{
				List.Add((T)((object)this.ReadObject()));
			}
		}

		// Token: 0x060058FA RID: 22778 RVA: 0x001AED30 File Offset: 0x001ACF30
		public List<string> ReadStringList()
		{
			int num = this.ReadInt32();
			if (num == -1)
			{
				return null;
			}
			List<string> list = new List<string>(num);
			for (int i = 0; i < num; i++)
			{
				list.Add((string)this.ReadObject());
			}
			return list;
		}

		// Token: 0x060058FB RID: 22779 RVA: 0x001AED70 File Offset: 0x001ACF70
		public Location2D ReadLocation2D()
		{
			int num = this.ReadOptimizedInt32();
			if (num < 0)
			{
				return null;
			}
			int num2 = this.ReadOptimizedInt32();
			return Location2D.Get(num, num2);
		}

		// Token: 0x060058FC RID: 22780 RVA: 0x001AED98 File Offset: 0x001ACF98
		public List<Location2D> ReadLocation2DList()
		{
			int num = this.ReadInt32();
			if (num == -1)
			{
				return null;
			}
			List<Location2D> list = new List<Location2D>(num);
			for (int i = 0; i < num; i++)
			{
				list.Add(this.ReadLocation2D());
			}
			return list;
		}

		// Token: 0x060058FD RID: 22781 RVA: 0x001AEDD4 File Offset: 0x001ACFD4
		public Cell ReadCell()
		{
			string text = this.ReadOptimizedString();
			if (text == null)
			{
				return null;
			}
			int num = this.ReadOptimizedInt32();
			int num2 = this.ReadOptimizedInt32();
			return The.ZoneManager.GetZone(text).GetCell(num, num2);
		}

		// Token: 0x060058FE RID: 22782 RVA: 0x001AEE10 File Offset: 0x001AD010
		public IComposite ReadComposite()
		{
			SerializedType serializedType = this.readTypeCode();
			if (serializedType == SerializedType.NullType)
			{
				return null;
			}
			return this.DeserializeComposite(serializedType);
		}

		// Token: 0x060058FF RID: 22783 RVA: 0x001AEE38 File Offset: 0x001AD038
		private IComposite DeserializeComposite(SerializedType Code)
		{
			long num;
			int num2;
			this.StartBlock(out num, out num2);
			Type type = null;
			IComposite composite = null;
			try
			{
				type = this.ReadTokenizedType();
				composite = (IComposite)Activator.CreateInstance(type, true);
				if (Code == SerializedType.ICompositeFieldType)
				{
					this.ReadTypeFields(composite, type, BindingFlags.Instance | BindingFlags.Public, FieldAttributes.Static | FieldAttributes.Literal | FieldAttributes.NotSerialized);
				}
				composite.Read(this);
			}
			catch (Exception ex)
			{
				this.SkipBlock(ex, type, num, num2);
			}
			return composite;
		}

		// Token: 0x06005900 RID: 22784 RVA: 0x001AEEA8 File Offset: 0x001AD0A8
		public T ReadComposite<T>() where T : IComposite, new()
		{
			SerializedType serializedType = this.readTypeCode();
			if (serializedType == SerializedType.NullType)
			{
				return default(T);
			}
			long num;
			int num2;
			this.StartBlock(out num, out num2);
			T t = new T();
			try
			{
				if (serializedType == SerializedType.ICompositeFieldType)
				{
					this.ReadTypeFields(t, typeof(T), BindingFlags.Instance | BindingFlags.Public, FieldAttributes.Static | FieldAttributes.Literal | FieldAttributes.NotSerialized);
				}
				t.Read(this);
			}
			catch (Exception ex)
			{
				this.SkipBlock(ex, typeof(T), num, num2);
			}
			return t;
		}

		// Token: 0x06005901 RID: 22785 RVA: 0x001AEF40 File Offset: 0x001AD140
		public List<T> ReadCompositeList<T>() where T : IComposite
		{
			int num = this.ReadOptimizedInt32() - 1;
			if (num == -1)
			{
				return null;
			}
			List<T> list = new List<T>(num);
			for (int i = 0; i < num; i++)
			{
				SerializedType serializedType = this.readTypeCode();
				if (serializedType == SerializedType.NullType)
				{
					list.Add(default(T));
				}
				T t = (T)((object)this.DeserializeComposite(serializedType));
				if (t != null)
				{
					list.Add(t);
				}
			}
			return list;
		}

		/// <summary>
		/// Returns a Nullable struct from the stream.
		/// The value returned must be cast to the correct Nullable type.
		/// Synonym for ReadObject();
		/// </summary>
		/// <returns>A struct value or null</returns>
		// Token: 0x06005902 RID: 22786 RVA: 0x001AEFAE File Offset: 0x001AD1AE
		public ValueType ReadNullable()
		{
			return (ValueType)this.ReadObject();
		}

		/// <summary>
		/// Returns a Nullable Boolean from the stream.
		/// </summary>
		/// <returns>A Nullable Boolean.</returns>
		// Token: 0x06005903 RID: 22787 RVA: 0x001AEFBB File Offset: 0x001AD1BB
		public bool? ReadNullableBoolean()
		{
			return (bool?)this.ReadObject();
		}

		/// <summary>
		/// Returns a Nullable Byte from the stream.
		/// </summary>
		/// <returns>A Nullable Byte.</returns>
		// Token: 0x06005904 RID: 22788 RVA: 0x001AEFC8 File Offset: 0x001AD1C8
		public byte? ReadNullableByte()
		{
			return (byte?)this.ReadObject();
		}

		/// <summary>
		/// Returns a Nullable Char from the stream.
		/// </summary>
		/// <returns>A Nullable Char.</returns>
		// Token: 0x06005905 RID: 22789 RVA: 0x001AEFD5 File Offset: 0x001AD1D5
		public char? ReadNullableChar()
		{
			return (char?)this.ReadObject();
		}

		/// <summary>
		/// Returns a Nullable DateTime from the stream.
		/// </summary>
		/// <returns>A Nullable DateTime.</returns>
		// Token: 0x06005906 RID: 22790 RVA: 0x001AEFE2 File Offset: 0x001AD1E2
		public DateTime? ReadNullableDateTime()
		{
			return (DateTime?)this.ReadObject();
		}

		/// <summary>
		/// Returns a Nullable Decimal from the stream.
		/// </summary>
		/// <returns>A Nullable Decimal.</returns>
		// Token: 0x06005907 RID: 22791 RVA: 0x001AEFEF File Offset: 0x001AD1EF
		public decimal? ReadNullableDecimal()
		{
			return (decimal?)this.ReadObject();
		}

		/// <summary>
		/// Returns a Nullable Double from the stream.
		/// </summary>
		/// <returns>A Nullable Double.</returns>
		// Token: 0x06005908 RID: 22792 RVA: 0x001AEFFC File Offset: 0x001AD1FC
		public double? ReadNullableDouble()
		{
			return (double?)this.ReadObject();
		}

		/// <summary>
		/// Returns a Nullable Guid from the stream.
		/// </summary>
		/// <returns>A Nullable Guid.</returns>
		// Token: 0x06005909 RID: 22793 RVA: 0x001AF009 File Offset: 0x001AD209
		public Guid? ReadNullableGuid()
		{
			return (Guid?)this.ReadObject();
		}

		/// <summary>
		/// Returns a Nullable Int16 from the stream.
		/// </summary>
		/// <returns>A Nullable Int16.</returns>
		// Token: 0x0600590A RID: 22794 RVA: 0x001AF016 File Offset: 0x001AD216
		public short? ReadNullableInt16()
		{
			return (short?)this.ReadObject();
		}

		/// <summary>
		/// Returns a Nullable Int32 from the stream.
		/// </summary>
		/// <returns>A Nullable Int32.</returns>
		// Token: 0x0600590B RID: 22795 RVA: 0x001AF023 File Offset: 0x001AD223
		public int? ReadNullableInt32()
		{
			return (int?)this.ReadObject();
		}

		/// <summary>
		/// Returns a Nullable Int64 from the stream.
		/// </summary>
		/// <returns>A Nullable Int64.</returns>
		// Token: 0x0600590C RID: 22796 RVA: 0x001AF030 File Offset: 0x001AD230
		public long? ReadNullableInt64()
		{
			return (long?)this.ReadObject();
		}

		/// <summary>
		/// Returns a Nullable SByte from the stream.
		/// </summary>
		/// <returns>A Nullable SByte.</returns>
		// Token: 0x0600590D RID: 22797 RVA: 0x001AF03D File Offset: 0x001AD23D
		[CLSCompliant(false)]
		public sbyte? ReadNullableSByte()
		{
			return (sbyte?)this.ReadObject();
		}

		/// <summary>
		/// Returns a Nullable Single from the stream.
		/// </summary>
		/// <returns>A Nullable Single.</returns>
		// Token: 0x0600590E RID: 22798 RVA: 0x001AF04A File Offset: 0x001AD24A
		public float? ReadNullableSingle()
		{
			return (float?)this.ReadObject();
		}

		/// <summary>
		/// Returns a Nullable TimeSpan from the stream.
		/// </summary>
		/// <returns>A Nullable TimeSpan.</returns>
		// Token: 0x0600590F RID: 22799 RVA: 0x001AF057 File Offset: 0x001AD257
		public TimeSpan? ReadNullableTimeSpan()
		{
			return (TimeSpan?)this.ReadObject();
		}

		/// <summary>
		/// Returns a Nullable UInt16 from the stream.
		/// </summary>
		/// <returns>A Nullable UInt16.</returns>
		// Token: 0x06005910 RID: 22800 RVA: 0x001AF064 File Offset: 0x001AD264
		[CLSCompliant(false)]
		public ushort? ReadNullableUInt16()
		{
			return (ushort?)this.ReadObject();
		}

		/// <summary>
		/// Returns a Nullable UInt32 from the stream.
		/// </summary>
		/// <returns>A Nullable UInt32.</returns>
		// Token: 0x06005911 RID: 22801 RVA: 0x001AF071 File Offset: 0x001AD271
		[CLSCompliant(false)]
		public uint? ReadNullableUInt32()
		{
			return (uint?)this.ReadObject();
		}

		/// <summary>
		/// Returns a Nullable UInt64 from the stream.
		/// </summary>
		/// <returns>A Nullable UInt64.</returns>
		// Token: 0x06005912 RID: 22802 RVA: 0x001AF07E File Offset: 0x001AD27E
		[CLSCompliant(false)]
		public ulong? ReadNullableUInt64()
		{
			return (ulong?)this.ReadObject();
		}

		/// <summary>
		/// Returns a Byte[] from the stream.
		/// </summary>
		/// <returns>A Byte instance; or null.</returns>
		// Token: 0x06005913 RID: 22803 RVA: 0x001AF08C File Offset: 0x001AD28C
		public byte[] ReadByteArray()
		{
			SerializedType serializedType = this.readTypeCode();
			if (serializedType == SerializedType.NullType)
			{
				return null;
			}
			if (serializedType == SerializedType.EmptyTypedArrayType)
			{
				return new byte[0];
			}
			return this.readByteArray();
		}

		/// <summary>
		/// Returns a Char[] from the stream.
		/// </summary>
		/// <returns>A Char[] value; or null.</returns>
		// Token: 0x06005914 RID: 22804 RVA: 0x001AF0C0 File Offset: 0x001AD2C0
		public char[] ReadCharArray()
		{
			SerializedType serializedType = this.readTypeCode();
			if (serializedType == SerializedType.NullType)
			{
				return null;
			}
			if (serializedType == SerializedType.EmptyTypedArrayType)
			{
				return new char[0];
			}
			return this.readCharArray();
		}

		/// <summary>
		/// Returns a Double[] from the stream.
		/// </summary>
		/// <returns>A Double[] instance; or null.</returns>
		// Token: 0x06005915 RID: 22805 RVA: 0x001AF0F4 File Offset: 0x001AD2F4
		public double[] ReadDoubleArray()
		{
			SerializedType serializedType = this.readTypeCode();
			if (serializedType == SerializedType.NullType)
			{
				return null;
			}
			if (serializedType == SerializedType.EmptyTypedArrayType)
			{
				return new double[0];
			}
			return this.readDoubleArray();
		}

		/// <summary>
		/// Returns a Guid[] from the stream.
		/// </summary>
		/// <returns>A Guid[] instance; or null.</returns>
		// Token: 0x06005916 RID: 22806 RVA: 0x001AF128 File Offset: 0x001AD328
		public Guid[] ReadGuidArray()
		{
			SerializedType serializedType = this.readTypeCode();
			if (serializedType == SerializedType.NullType)
			{
				return null;
			}
			if (serializedType == SerializedType.EmptyTypedArrayType)
			{
				return new Guid[0];
			}
			return this.readGuidArray();
		}

		/// <summary>
		/// Returns an Int16[] from the stream.
		/// </summary>
		/// <returns>An Int16[] instance; or null.</returns>
		// Token: 0x06005917 RID: 22807 RVA: 0x001AF15C File Offset: 0x001AD35C
		public short[] ReadInt16Array()
		{
			SerializedType serializedType = this.readTypeCode();
			if (serializedType == SerializedType.NullType)
			{
				return null;
			}
			if (serializedType == SerializedType.EmptyTypedArrayType)
			{
				return new short[0];
			}
			BitArray bitArray = this.readTypedArrayOptimizeFlags(serializedType);
			short[] array = new short[this.ReadOptimizedInt32()];
			for (int i = 0; i < array.Length; i++)
			{
				if (bitArray == null || (bitArray != SerializationReader.FullyOptimizableTypedArray && !bitArray[i]))
				{
					array[i] = this.ReadInt16();
				}
				else
				{
					array[i] = this.ReadOptimizedInt16();
				}
			}
			return array;
		}

		/// <summary>
		/// Returns an object[] or null from the stream.
		/// </summary>
		/// <returns>A DateTime value.</returns>
		// Token: 0x06005918 RID: 22808 RVA: 0x001AF1D4 File Offset: 0x001AD3D4
		public object[] ReadObjectArray()
		{
			return this.ReadObjectArray(null);
		}

		/// <summary>
		/// Returns an object[] or null from the stream.
		/// The returned array will be typed according to the specified element type
		/// and the resulting array can be cast to the expected type.
		/// e.g.
		/// string[] myStrings = (string[]) reader.ReadObjectArray(typeof(string));
		///
		/// An exception will be thrown if any of the deserialized values cannot be
		/// cast to the specified elementType.
		///
		/// </summary>
		/// <param name="elementType">The Type of the expected array elements. null will return a plain object[].</param>
		/// <returns>An object[] instance.</returns>
		// Token: 0x06005919 RID: 22809 RVA: 0x001AF1E0 File Offset: 0x001AD3E0
		public object[] ReadObjectArray(Type elementType)
		{
			SerializedType serializedType = this.readTypeCode();
			if (serializedType == SerializedType.NullType)
			{
				return null;
			}
			if (serializedType == SerializedType.EmptyObjectArrayType)
			{
				if (!(elementType == null))
				{
					return (object[])Array.CreateInstance(elementType, 0);
				}
				return new object[0];
			}
			else
			{
				if (serializedType == SerializedType.EmptyTypedArrayType)
				{
					throw new Exception();
				}
				return this.ReadOptimizedObjectArray(elementType);
			}
		}

		/// <summary>
		/// Returns a Single[] from the stream.
		/// </summary>
		/// <returns>A Single[] instance; or null.</returns>
		// Token: 0x0600591A RID: 22810 RVA: 0x001AF238 File Offset: 0x001AD438
		public float[] ReadSingleArray()
		{
			SerializedType serializedType = this.readTypeCode();
			if (serializedType == SerializedType.NullType)
			{
				return null;
			}
			if (serializedType == SerializedType.EmptyTypedArrayType)
			{
				return new float[0];
			}
			return this.readSingleArray();
		}

		/// <summary>
		/// Returns an SByte[] from the stream.
		/// </summary>
		/// <returns>An SByte[] instance; or null.</returns>
		// Token: 0x0600591B RID: 22811 RVA: 0x001AF26C File Offset: 0x001AD46C
		[CLSCompliant(false)]
		public sbyte[] ReadSByteArray()
		{
			SerializedType serializedType = this.readTypeCode();
			if (serializedType == SerializedType.NullType)
			{
				return null;
			}
			if (serializedType == SerializedType.EmptyTypedArrayType)
			{
				return new sbyte[0];
			}
			return this.readSByteArray();
		}

		/// <summary>
		/// Returns a string[] or null from the stream.
		/// </summary>
		/// <returns>An string[] instance.</returns>
		// Token: 0x0600591C RID: 22812 RVA: 0x001AF29F File Offset: 0x001AD49F
		public string[] ReadStringArray()
		{
			return (string[])this.ReadObjectArray(typeof(string));
		}

		/// <summary>
		/// Returns a UInt16[] from the stream.
		/// </summary>
		/// <returns>A UInt16[] instance; or null.</returns>
		// Token: 0x0600591D RID: 22813 RVA: 0x001AF2B8 File Offset: 0x001AD4B8
		[CLSCompliant(false)]
		public ushort[] ReadUInt16Array()
		{
			SerializedType serializedType = this.readTypeCode();
			if (serializedType == SerializedType.NullType)
			{
				return null;
			}
			if (serializedType == SerializedType.EmptyTypedArrayType)
			{
				return new ushort[0];
			}
			BitArray bitArray = this.readTypedArrayOptimizeFlags(serializedType);
			ushort[] array = new ushort[this.ReadOptimizedUInt32()];
			for (int i = 0; i < array.Length; i++)
			{
				if (bitArray == null || (bitArray != SerializationReader.FullyOptimizableTypedArray && !bitArray[i]))
				{
					array[i] = this.ReadUInt16();
				}
				else
				{
					array[i] = this.ReadOptimizedUInt16();
				}
			}
			return array;
		}

		/// <summary>
		/// Returns a Boolean[] from the stream.
		/// </summary>
		/// <returns>A Boolean[] instance; or null.</returns>
		// Token: 0x0600591E RID: 22814 RVA: 0x001AF330 File Offset: 0x001AD530
		public bool[] ReadBooleanArray()
		{
			SerializedType serializedType = this.readTypeCode();
			if (serializedType == SerializedType.NullType)
			{
				return null;
			}
			if (serializedType == SerializedType.EmptyTypedArrayType)
			{
				return new bool[0];
			}
			return this.readBooleanArray();
		}

		/// <summary>
		/// Returns a DateTime[] from the stream.
		/// </summary>
		/// <returns>A DateTime[] instance; or null.</returns>
		// Token: 0x0600591F RID: 22815 RVA: 0x001AF364 File Offset: 0x001AD564
		public DateTime[] ReadDateTimeArray()
		{
			SerializedType serializedType = this.readTypeCode();
			if (serializedType == SerializedType.NullType)
			{
				return null;
			}
			if (serializedType == SerializedType.EmptyTypedArrayType)
			{
				return new DateTime[0];
			}
			BitArray bitArray = this.readTypedArrayOptimizeFlags(serializedType);
			DateTime[] array = new DateTime[this.ReadOptimizedInt32()];
			for (int i = 0; i < array.Length; i++)
			{
				if (bitArray == null || (bitArray != SerializationReader.FullyOptimizableTypedArray && !bitArray[i]))
				{
					array[i] = this.ReadDateTime();
				}
				else
				{
					array[i] = this.ReadOptimizedDateTime();
				}
			}
			return array;
		}

		/// <summary>
		/// Returns a Decimal[] from the stream.
		/// </summary>
		/// <returns>A Decimal[] instance; or null.</returns>
		// Token: 0x06005920 RID: 22816 RVA: 0x001AF3E4 File Offset: 0x001AD5E4
		public decimal[] ReadDecimalArray()
		{
			SerializedType serializedType = this.readTypeCode();
			if (serializedType == SerializedType.NullType)
			{
				return null;
			}
			if (serializedType == SerializedType.EmptyTypedArrayType)
			{
				return new decimal[0];
			}
			return this.readDecimalArray();
		}

		/// <summary>
		/// Returns an Int32[] from the stream.
		/// </summary>
		/// <returns>An Int32[] instance; or null.</returns>
		// Token: 0x06005921 RID: 22817 RVA: 0x001AF418 File Offset: 0x001AD618
		public int[] ReadInt32Array()
		{
			SerializedType serializedType = this.readTypeCode();
			if (serializedType == SerializedType.NullType)
			{
				return null;
			}
			if (serializedType == SerializedType.EmptyTypedArrayType)
			{
				return new int[0];
			}
			BitArray bitArray = this.readTypedArrayOptimizeFlags(serializedType);
			int[] array = new int[this.ReadOptimizedInt32()];
			for (int i = 0; i < array.Length; i++)
			{
				if (bitArray == null || (bitArray != SerializationReader.FullyOptimizableTypedArray && !bitArray[i]))
				{
					array[i] = this.ReadInt32();
				}
				else
				{
					array[i] = this.ReadOptimizedInt32();
				}
			}
			return array;
		}

		/// <summary>
		/// Returns an Int64[] from the stream.
		/// </summary>
		/// <returns>An Int64[] instance; or null.</returns>
		// Token: 0x06005922 RID: 22818 RVA: 0x001AF490 File Offset: 0x001AD690
		public long[] ReadInt64Array()
		{
			SerializedType serializedType = this.readTypeCode();
			if (serializedType == SerializedType.NullType)
			{
				return null;
			}
			if (serializedType == SerializedType.EmptyTypedArrayType)
			{
				return new long[0];
			}
			BitArray bitArray = this.readTypedArrayOptimizeFlags(serializedType);
			long[] array = new long[this.ReadOptimizedInt64()];
			for (int i = 0; i < array.Length; i++)
			{
				if (bitArray == null || (bitArray != SerializationReader.FullyOptimizableTypedArray && !bitArray[i]))
				{
					array[i] = this.ReadInt64();
				}
				else
				{
					array[i] = this.ReadOptimizedInt64();
				}
			}
			return array;
		}

		/// <summary>
		/// Returns a string[] from the stream that was stored optimized.
		/// </summary>
		/// <returns>An string[] instance.</returns>
		// Token: 0x06005923 RID: 22819 RVA: 0x001AF509 File Offset: 0x001AD709
		public string[] ReadOptimizedStringArray()
		{
			return (string[])this.ReadOptimizedObjectArray(typeof(string));
		}

		/// <summary>
		/// Returns a TimeSpan[] from the stream.
		/// </summary>
		/// <returns>A TimeSpan[] instance; or null.</returns>
		// Token: 0x06005924 RID: 22820 RVA: 0x001AF520 File Offset: 0x001AD720
		public TimeSpan[] ReadTimeSpanArray()
		{
			SerializedType serializedType = this.readTypeCode();
			if (serializedType == SerializedType.NullType)
			{
				return null;
			}
			if (serializedType == SerializedType.EmptyTypedArrayType)
			{
				return new TimeSpan[0];
			}
			BitArray bitArray = this.readTypedArrayOptimizeFlags(serializedType);
			TimeSpan[] array = new TimeSpan[this.ReadOptimizedInt32()];
			for (int i = 0; i < array.Length; i++)
			{
				if (bitArray == null || (bitArray != SerializationReader.FullyOptimizableTypedArray && !bitArray[i]))
				{
					array[i] = this.ReadTimeSpan();
				}
				else
				{
					array[i] = this.ReadOptimizedTimeSpan();
				}
			}
			return array;
		}

		/// <summary>
		/// Returns a UInt[] from the stream.
		/// </summary>
		/// <returns>A UInt[] instance; or null.</returns>
		// Token: 0x06005925 RID: 22821 RVA: 0x001AF5A0 File Offset: 0x001AD7A0
		[CLSCompliant(false)]
		public uint[] ReadUInt32Array()
		{
			SerializedType serializedType = this.readTypeCode();
			if (serializedType == SerializedType.NullType)
			{
				return null;
			}
			if (serializedType == SerializedType.EmptyTypedArrayType)
			{
				return new uint[0];
			}
			BitArray bitArray = this.readTypedArrayOptimizeFlags(serializedType);
			uint[] array = new uint[this.ReadOptimizedUInt32()];
			for (int i = 0; i < array.Length; i++)
			{
				if (bitArray == null || (bitArray != SerializationReader.FullyOptimizableTypedArray && !bitArray[i]))
				{
					array[i] = this.ReadUInt32();
				}
				else
				{
					array[i] = this.ReadOptimizedUInt32();
				}
			}
			return array;
		}

		/// <summary>
		/// Returns a UInt64[] from the stream.
		/// </summary>
		/// <returns>A UInt64[] instance; or null.</returns>
		// Token: 0x06005926 RID: 22822 RVA: 0x001AF618 File Offset: 0x001AD818
		[CLSCompliant(false)]
		public ulong[] ReadUInt64Array()
		{
			SerializedType serializedType = this.readTypeCode();
			if (serializedType == SerializedType.NullType)
			{
				return null;
			}
			if (serializedType == SerializedType.EmptyTypedArrayType)
			{
				return new ulong[0];
			}
			BitArray bitArray = this.readTypedArrayOptimizeFlags(serializedType);
			ulong[] array = new ulong[this.ReadOptimizedInt64()];
			for (int i = 0; i < array.Length; i++)
			{
				if (bitArray == null || (bitArray != SerializationReader.FullyOptimizableTypedArray && !bitArray[i]))
				{
					array[i] = this.ReadUInt64();
				}
				else
				{
					array[i] = this.ReadOptimizedUInt64();
				}
			}
			return array;
		}

		/// <summary>
		/// Returns a Boolean[] from the stream.
		/// </summary>
		/// <returns>A Boolean[] instance; or null.</returns>
		// Token: 0x06005927 RID: 22823 RVA: 0x001AF691 File Offset: 0x001AD891
		public bool[] ReadOptimizedBooleanArray()
		{
			return this.ReadBooleanArray();
		}

		/// <summary>
		/// Returns a DateTime[] from the stream.
		/// </summary>
		/// <returns>A DateTime[] instance; or null.</returns>
		// Token: 0x06005928 RID: 22824 RVA: 0x001AF699 File Offset: 0x001AD899
		public DateTime[] ReadOptimizedDateTimeArray()
		{
			return this.ReadDateTimeArray();
		}

		/// <summary>
		/// Returns a Decimal[] from the stream.
		/// </summary>
		/// <returns>A Decimal[] instance; or null.</returns>
		// Token: 0x06005929 RID: 22825 RVA: 0x001AF6A1 File Offset: 0x001AD8A1
		public decimal[] ReadOptimizedDecimalArray()
		{
			return this.ReadDecimalArray();
		}

		/// <summary>
		/// Returns a Int16[] from the stream.
		/// </summary>
		/// <returns>An Int16[] instance; or null.</returns>
		// Token: 0x0600592A RID: 22826 RVA: 0x001AF6A9 File Offset: 0x001AD8A9
		public short[] ReadOptimizedInt16Array()
		{
			return this.ReadInt16Array();
		}

		/// <summary>
		/// Returns a Int32[] from the stream.
		/// </summary>
		/// <returns>An Int32[] instance; or null.</returns>
		// Token: 0x0600592B RID: 22827 RVA: 0x001AF6B1 File Offset: 0x001AD8B1
		public int[] ReadOptimizedInt32Array()
		{
			return this.ReadInt32Array();
		}

		/// <summary>
		/// Returns a Int64[] from the stream.
		/// </summary>
		/// <returns>A Int64[] instance; or null.</returns>
		// Token: 0x0600592C RID: 22828 RVA: 0x001AF6B9 File Offset: 0x001AD8B9
		public long[] ReadOptimizedInt64Array()
		{
			return this.ReadInt64Array();
		}

		/// <summary>
		/// Returns a TimeSpan[] from the stream.
		/// </summary>
		/// <returns>A TimeSpan[] instance; or null.</returns>
		// Token: 0x0600592D RID: 22829 RVA: 0x001AF6C1 File Offset: 0x001AD8C1
		public TimeSpan[] ReadOptimizedTimeSpanArray()
		{
			return this.ReadTimeSpanArray();
		}

		/// <summary>
		/// Returns a UInt16[] from the stream.
		/// </summary>
		/// <returns>A UInt16[] instance; or null.</returns>
		// Token: 0x0600592E RID: 22830 RVA: 0x001AF6C9 File Offset: 0x001AD8C9
		[CLSCompliant(false)]
		public ushort[] ReadOptimizedUInt16Array()
		{
			return this.ReadUInt16Array();
		}

		/// <summary>
		/// Returns a UInt32[] from the stream.
		/// </summary>
		/// <returns>A UInt32[] instance; or null.</returns>
		// Token: 0x0600592F RID: 22831 RVA: 0x001AF6D1 File Offset: 0x001AD8D1
		[CLSCompliant(false)]
		public uint[] ReadOptimizedUInt32Array()
		{
			return this.ReadUInt32Array();
		}

		/// <summary>
		/// Returns a UInt64[] from the stream.
		/// </summary>
		/// <returns>A UInt64[] instance; or null.</returns>
		// Token: 0x06005930 RID: 22832 RVA: 0x001AF6D9 File Offset: 0x001AD8D9
		[CLSCompliant(false)]
		public ulong[] ReadOptimizedUInt64Array()
		{
			return this.ReadUInt64Array();
		}

		// Token: 0x06005931 RID: 22833 RVA: 0x001AF6E4 File Offset: 0x001AD8E4
		public Version ReadOptimizedVersion()
		{
			if (this.FileVersion < 400)
			{
				return new Version(this.ReadOptimizedInt32(), this.ReadOptimizedInt32(), this.ReadOptimizedInt32(), this.ReadOptimizedInt32());
			}
			return new Version((int)this.ReadOptimizedUInt32(), (int)this.ReadOptimizedUInt32(), (int)this.ReadOptimizedUInt32(), (int)this.ReadOptimizedUInt32());
		}

		/// <summary>
		/// Allows an existing object, implementing IOwnedDataSerializable, to 
		/// retrieve its owned data from the stream.
		/// </summary>
		/// <param name="target">Any IOwnedDataSerializable object.</param>
		/// <param name="context">An optional, arbitrary object to allow context to be provided.</param>
		// Token: 0x06005932 RID: 22834 RVA: 0x001AF739 File Offset: 0x001AD939
		public void ReadOwnedData(IOwnedDataSerializable target, object context)
		{
			target.DeserializeOwnedData(this, context);
		}

		/// <summary>
		/// Returns the object associated with the object token read next from the stream.
		/// </summary>
		/// <returns>An object.</returns>
		// Token: 0x06005933 RID: 22835 RVA: 0x001AF743 File Offset: 0x001AD943
		public object ReadTokenizedObject()
		{
			return this.TokenObjects[this.ReadOptimizedInt32()];
		}

		// Token: 0x06005934 RID: 22836 RVA: 0x001AF754 File Offset: 0x001AD954
		public ITokenized ReadTokenized()
		{
			int num = this.ReadOptimizedInt32() - 1;
			if (num < 0)
			{
				return null;
			}
			return this.Tokenized[num];
		}

		// Token: 0x06005935 RID: 22837 RVA: 0x001AF778 File Offset: 0x001AD978
		public Type ReadTokenizedType()
		{
			int num = this.ReadOptimizedInt32() - 1;
			if (num == -1)
			{
				return null;
			}
			Type type = this.TokenTypes[num];
			if (type == null)
			{
				TypeLoadException ex = new TypeLoadException("No type by name '" + this.TokenTypeNames[num] + "' is loaded");
				ex.Data["TypeName"] = this.TokenTypeNames[num];
				throw ex;
			}
			return type;
		}

		// Token: 0x06005936 RID: 22838 RVA: 0x001AF7D4 File Offset: 0x001AD9D4
		public EventRegistry ReadEventRegistry()
		{
			int num = this.ReadOptimizedInt32() - 1;
			if (num == -1)
			{
				return null;
			}
			return this.EventRegistries[num];
		}

		// Token: 0x06005937 RID: 22839 RVA: 0x001AF7F8 File Offset: 0x001AD9F8
		public GameObjectReference ReadGameObjectReference()
		{
			int num = this.ReadOptimizedInt32() - 1;
			if (num == -1)
			{
				return null;
			}
			return this.GameObjectReferences[num];
		}

		// Token: 0x06005938 RID: 22840 RVA: 0x001AF81C File Offset: 0x001ADA1C
		public void ReadModVersions()
		{
			int num = this.ReadOptimizedInt32();
			this.ModVersions = new Dictionary<string, Version>(num);
			for (int i = 0; i < num; i++)
			{
				this.ModVersions.Add(this.ReadOptimizedString(), this.ReadOptimizedVersion());
			}
		}

		// Token: 0x06005939 RID: 22841 RVA: 0x001AF85F File Offset: 0x001ADA5F
		internal void ReadExtensionHash()
		{
			if (this.FileVersion >= 408)
			{
				this.ExtensionHash = this.ReadOptimizedUInt64();
			}
		}

		// Token: 0x0600593A RID: 22842 RVA: 0x001AF87A File Offset: 0x001ADA7A
		public IEnumerable<string> GetSavedMods()
		{
			return this.ModVersions.Keys;
		}

		// Token: 0x0600593B RID: 22843 RVA: 0x001AF887 File Offset: 0x001ADA87
		public void StartBlock(out long Position, out int Length)
		{
			Length = this.ReadOptimizedInt32();
			Position = this.Stream.Position;
		}

		// Token: 0x0600593C RID: 22844 RVA: 0x001AF8A0 File Offset: 0x001ADAA0
		public void SkipBlock(Exception Exception, Type Type, long Position, int Length)
		{
			this.Errors++;
			long num = Position + (long)Length - this.Stream.Position;
			this.Stream.Position = Position + (long)Length;
			string text;
			if ((text = ((Type != null) ? Type.FullName : null)) == null)
			{
				text = (Exception.Data["TypeName"] as string) ?? "unknown type";
			}
			string text2 = text;
			MetricsManager.LogException(string.Format("Exception when deserializing '{0}' (Skipping {1}/{2} bytes)", text2, num, Length), Exception, "game_exception");
		}

		/// <summary>
		/// Returns a TimeSpan decoded from packed data.
		/// This routine is called from ReadOptimizedDateTime() and ReadOptimizedTimeSpan().
		/// <remarks>
		/// This routine uses a parameter to allow ReadOptimizedDateTime() to 'peek' at the
		/// next byte and extract the DateTimeKind from bits one and two (IsNegative and HasDays)
		/// which are never set for a Time portion of a DateTime.
		/// </remarks>
		/// </summary>
		/// <param name="initialByte">The first of two always-present bytes.</param>
		/// <returns>A decoded TimeSpan</returns>
		// Token: 0x0600593D RID: 22845 RVA: 0x001AF930 File Offset: 0x001ADB30
		private TimeSpan decodeTimeSpan(byte initialByte)
		{
			long num = 0L;
			BitVector32 bitVector = new BitVector32((int)initialByte | ((int)this.ReadByte() << 8));
			bool flag = bitVector[SerializationWriter.HasTimeSection] == 1;
			bool flag2 = bitVector[SerializationWriter.HasSecondsSection] == 1;
			bool flag3 = bitVector[SerializationWriter.HasMillisecondsSection] == 1;
			if (flag3)
			{
				bitVector = new BitVector32(bitVector.Data | ((int)this.ReadByte() << 16) | ((int)this.ReadByte() << 24));
			}
			else if (flag2 && flag)
			{
				bitVector = new BitVector32(bitVector.Data | ((int)this.ReadByte() << 16));
			}
			if (flag)
			{
				num += (long)bitVector[SerializationWriter.HoursSection] * 36000000000L;
				num += (long)bitVector[SerializationWriter.MinutesSection] * 600000000L;
			}
			if (flag2)
			{
				num += (long)bitVector[(!flag && !flag3) ? SerializationWriter.MinutesSection : SerializationWriter.SecondsSection] * 10000000L;
			}
			if (flag3)
			{
				num += (long)bitVector[SerializationWriter.MillisecondsSection] * 10000L;
			}
			if (bitVector[SerializationWriter.HasDaysSection] == 1)
			{
				num += (long)this.ReadOptimizedInt32() * 864000000000L;
			}
			if (bitVector[SerializationWriter.IsNegativeSection] == 1)
			{
				num = -num;
			}
			return new TimeSpan(num);
		}

		/// <summary>
		/// Creates a BitArray representing which elements of a typed array
		/// are serializable.
		/// </summary>
		/// <param name="serializedType">The type of typed array.</param>
		/// <returns>A BitArray denoting which elements are serializable.</returns>
		// Token: 0x0600593E RID: 22846 RVA: 0x001AFA78 File Offset: 0x001ADC78
		private BitArray readTypedArrayOptimizeFlags(SerializedType serializedType)
		{
			BitArray bitArray = null;
			if (serializedType == SerializedType.FullyOptimizedTypedArrayType)
			{
				bitArray = SerializationReader.FullyOptimizableTypedArray;
			}
			else if (serializedType == SerializedType.PartiallyOptimizedTypedArrayType)
			{
				bitArray = this.ReadOptimizedBitArray();
			}
			return bitArray;
		}

		/// <summary>
		/// Returns an object based on supplied SerializedType.
		/// </summary>
		/// <returns>An object instance.</returns>
		// Token: 0x0600593F RID: 22847 RVA: 0x001AFAA8 File Offset: 0x001ADCA8
		private object processObject(SerializedType typeCode)
		{
			if (typeCode == SerializedType.NullType)
			{
				return null;
			}
			if (typeCode == SerializedType.Int32Type)
			{
				return this.ReadInt32();
			}
			if (typeCode == SerializedType.EmptyStringType)
			{
				return string.Empty;
			}
			if (typeCode < SerializedType.NullType)
			{
				return this.readTokenizedString((int)typeCode);
			}
			if (typeCode == SerializedType.BooleanFalseType)
			{
				return false;
			}
			if (typeCode == SerializedType.ZeroInt32Type)
			{
				return 0;
			}
			if (typeCode == SerializedType.OptimizedInt32Type)
			{
				return this.ReadOptimizedInt32();
			}
			if (typeCode == SerializedType.OptimizedInt32NegativeType)
			{
				return -this.ReadOptimizedInt32() - 1;
			}
			if (typeCode == SerializedType.DecimalType)
			{
				return this.ReadOptimizedDecimal();
			}
			if (typeCode == SerializedType.ZeroDecimalType)
			{
				return 0m;
			}
			if (typeCode == SerializedType.YStringType)
			{
				return "Y";
			}
			if (typeCode == SerializedType.DateTimeType)
			{
				return this.ReadDateTime();
			}
			if (typeCode == SerializedType.OptimizedDateTimeType)
			{
				return this.ReadOptimizedDateTime();
			}
			if (typeCode == SerializedType.SingleCharStringType)
			{
				return char.ToString(this.ReadChar());
			}
			if (typeCode == SerializedType.SingleSpaceType)
			{
				return " ";
			}
			if (typeCode == SerializedType.OneInt32Type)
			{
				return 1;
			}
			if (typeCode == SerializedType.OptimizedInt16Type)
			{
				return this.ReadOptimizedInt16();
			}
			if (typeCode == SerializedType.OptimizedInt16NegativeType)
			{
				return (int)(-this.ReadOptimizedInt16() - 1);
			}
			if (typeCode == SerializedType.OneDecimalType)
			{
				return 1m;
			}
			if (typeCode == SerializedType.BooleanTrueType)
			{
				return true;
			}
			if (typeCode == SerializedType.NStringType)
			{
				return "N";
			}
			if (typeCode == SerializedType.DBNullType)
			{
				return DBNull.Value;
			}
			if (typeCode == SerializedType.ObjectArrayType)
			{
				return this.ReadOptimizedObjectArray();
			}
			if (typeCode == SerializedType.EmptyObjectArrayType)
			{
				return new object[0];
			}
			if (typeCode == SerializedType.MinusOneInt32Type)
			{
				return -1;
			}
			if (typeCode == SerializedType.MinusOneInt64Type)
			{
				return -1L;
			}
			if (typeCode == SerializedType.MinusOneInt16Type)
			{
				return -1;
			}
			if (typeCode == SerializedType.MinDateTimeType)
			{
				return DateTime.MinValue;
			}
			if (typeCode == SerializedType.GuidType)
			{
				return this.ReadGuid();
			}
			if (typeCode == SerializedType.EmptyGuidType)
			{
				return Guid.Empty;
			}
			if (typeCode == SerializedType.TimeSpanType)
			{
				return this.ReadTimeSpan();
			}
			if (typeCode == SerializedType.MaxDateTimeType)
			{
				return DateTime.MaxValue;
			}
			if (typeCode == SerializedType.ZeroTimeSpanType)
			{
				return TimeSpan.Zero;
			}
			if (typeCode == SerializedType.OptimizedTimeSpanType)
			{
				return this.ReadOptimizedTimeSpan();
			}
			if (typeCode == SerializedType.DoubleType)
			{
				return this.ReadDouble();
			}
			if (typeCode == SerializedType.ZeroDoubleType)
			{
				return 0.0;
			}
			if (typeCode == SerializedType.Int64Type)
			{
				return this.ReadInt64();
			}
			if (typeCode == SerializedType.ZeroInt64Type)
			{
				return 0L;
			}
			if (typeCode == SerializedType.OptimizedInt64Type)
			{
				return this.ReadOptimizedInt64();
			}
			if (typeCode == SerializedType.OptimizedInt64NegativeType)
			{
				return -this.ReadOptimizedInt64() - 1L;
			}
			if (typeCode == SerializedType.Int16Type)
			{
				return this.ReadInt16();
			}
			if (typeCode == SerializedType.ZeroInt16Type)
			{
				return 0;
			}
			if (typeCode == SerializedType.SingleType)
			{
				return this.ReadSingle();
			}
			if (typeCode == SerializedType.ZeroSingleType)
			{
				return 0f;
			}
			if (typeCode == SerializedType.ByteType)
			{
				return this.ReadByte();
			}
			if (typeCode == SerializedType.ZeroByteType)
			{
				return 0;
			}
			if (typeCode == SerializedType.GameObjectType)
			{
				return this.ReadGameObject(null);
			}
			if (typeCode == SerializedType.OtherType)
			{
				AppDomain.CurrentDomain.AssemblyResolve += SerializationReader.assemblyResolveHandler;
				object obj = null;
				try
				{
					obj = SerializationReader.createBinaryFormatter().Deserialize(this.Stream);
				}
				catch (Exception ex)
				{
					string text = "Exception deserializing an unknown type: ";
					Exception ex2 = ex;
					global::UnityEngine.Debug.LogError(text + ((ex2 != null) ? ex2.ToString() : null));
				}
				finally
				{
					AppDomain.CurrentDomain.AssemblyResolve -= SerializationReader.assemblyResolveHandler;
				}
				return obj;
			}
			if (typeCode == SerializedType.UInt16Type)
			{
				return this.ReadUInt16();
			}
			if (typeCode == SerializedType.ZeroUInt16Type)
			{
				return 0;
			}
			if (typeCode == SerializedType.UInt32Type)
			{
				return this.ReadUInt32();
			}
			if (typeCode == SerializedType.ZeroUInt32Type)
			{
				return 0U;
			}
			if (typeCode == SerializedType.OptimizedUInt32Type)
			{
				return this.ReadOptimizedUInt32();
			}
			if (typeCode == SerializedType.UInt64Type)
			{
				return this.ReadUInt64();
			}
			if (typeCode == SerializedType.ZeroUInt64Type)
			{
				return 0UL;
			}
			if (typeCode == SerializedType.OptimizedUInt64Type)
			{
				return this.ReadOptimizedUInt64();
			}
			if (typeCode == SerializedType.BitVector32Type)
			{
				return this.ReadBitVector32();
			}
			if (typeCode == SerializedType.CharType)
			{
				return this.ReadChar();
			}
			if (typeCode == SerializedType.ZeroCharType)
			{
				return '\0';
			}
			if (typeCode == SerializedType.SByteType)
			{
				return this.ReadSByte();
			}
			if (typeCode == SerializedType.ZeroSByteType)
			{
				return 0;
			}
			if (typeCode == SerializedType.OneByteType)
			{
				return 1;
			}
			if (typeCode == SerializedType.OneDoubleType)
			{
				return 1.0;
			}
			if (typeCode == SerializedType.OneCharType)
			{
				return '\u0001';
			}
			if (typeCode == SerializedType.OneInt16Type)
			{
				return 1;
			}
			if (typeCode == SerializedType.OneInt64Type)
			{
				return 1L;
			}
			if (typeCode == SerializedType.OneUInt16Type)
			{
				return 1;
			}
			if (typeCode == SerializedType.OptimizedUInt16Type)
			{
				return this.ReadOptimizedUInt16();
			}
			if (typeCode == SerializedType.OneUInt32Type)
			{
				return 1U;
			}
			if (typeCode == SerializedType.OneUInt64Type)
			{
				return 1UL;
			}
			if (typeCode == SerializedType.OneSByteType)
			{
				return 1;
			}
			if (typeCode == SerializedType.OneSingleType)
			{
				return 1f;
			}
			if (typeCode == SerializedType.BitArrayType)
			{
				return this.ReadOptimizedBitArray();
			}
			if (typeCode == SerializedType.TypeType)
			{
				return this.ReadTokenizedType();
			}
			if (typeCode == SerializedType.ArrayListType)
			{
				return this.ReadOptimizedArrayList();
			}
			if (typeCode == SerializedType.StringListType)
			{
				return this.ReadStringList();
			}
			if (typeCode == SerializedType.IDictionaryType)
			{
				Type type = this.ReadTokenizedType();
				int num = this.ReadInt32();
				this.CapacityArgs[0] = num;
				IDictionary dictionary = (IDictionary)Activator.CreateInstance(type, this.CapacityArgs);
				for (int i = 0; i < num; i++)
				{
					dictionary.Add(this.ReadObject(), this.ReadObject());
				}
				return dictionary;
			}
			if (typeCode == SerializedType.IListType)
			{
				Type type2 = this.ReadTokenizedType();
				int num2 = this.ReadInt32();
				this.CapacityArgs[0] = num2;
				IList list = (IList)Activator.CreateInstance(type2, this.CapacityArgs);
				for (int j = 0; j < num2; j++)
				{
					list.Add(this.ReadObject());
				}
				return list;
			}
			if (typeCode == SerializedType.ICompositeType || typeCode == SerializedType.ICompositeFieldType)
			{
				return this.DeserializeComposite(typeCode);
			}
			if (typeCode == SerializedType.SingleInstanceType)
			{
				try
				{
					return Activator.CreateInstance(this.ReadTokenizedType(), true);
				}
				catch
				{
					return null;
				}
			}
			if (typeCode == SerializedType.OwnedDataSerializableAndRecreatableType)
			{
				object obj2 = Activator.CreateInstance(this.ReadTokenizedType());
				this.ReadOwnedData((IOwnedDataSerializable)obj2, null);
				return obj2;
			}
			if (typeCode == SerializedType.OptimizedEnumType)
			{
				Type type3 = this.ReadTokenizedType();
				Type underlyingType = Enum.GetUnderlyingType(type3);
				if (underlyingType == typeof(int) || underlyingType == typeof(uint) || underlyingType == typeof(long) || underlyingType == typeof(ulong))
				{
					return Enum.ToObject(type3, this.ReadOptimizedUInt64());
				}
				return Enum.ToObject(type3, this.ReadUInt64());
			}
			else if (typeCode == SerializedType.EnumType)
			{
				Type type4 = this.ReadTokenizedType();
				Type underlyingType2 = Enum.GetUnderlyingType(type4);
				if (underlyingType2 == typeof(int))
				{
					return Enum.ToObject(type4, this.ReadInt32());
				}
				if (underlyingType2 == typeof(byte))
				{
					return Enum.ToObject(type4, this.ReadByte());
				}
				if (underlyingType2 == typeof(short))
				{
					return Enum.ToObject(type4, this.ReadInt16());
				}
				if (underlyingType2 == typeof(uint))
				{
					return Enum.ToObject(type4, this.ReadUInt32());
				}
				if (underlyingType2 == typeof(long))
				{
					return Enum.ToObject(type4, this.ReadInt64());
				}
				if (underlyingType2 == typeof(sbyte))
				{
					return Enum.ToObject(type4, this.ReadSByte());
				}
				if (underlyingType2 == typeof(ushort))
				{
					return Enum.ToObject(type4, this.ReadUInt16());
				}
				return Enum.ToObject(type4, this.ReadUInt64());
			}
			else
			{
				if (typeCode == SerializedType.SurrogateHandledType)
				{
					Type type5 = this.ReadTokenizedType();
					return SerializationWriter.findSurrogateForType(type5).Deserialize(this, type5);
				}
				object obj3 = this.processArrayTypes(typeCode, null);
				if (obj3 != null)
				{
					return obj3;
				}
				throw new InvalidOperationException("Unrecognized TypeCode: " + typeCode.ToString());
			}
		}

		// Token: 0x06005940 RID: 22848 RVA: 0x001B0358 File Offset: 0x001AE558
		private static IFormatter createBinaryFormatter()
		{
			if (SerializationReader.formatter == null)
			{
				Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
				SerializationReader.formatter = new BinaryFormatter();
				SerializationReader.formatter.AssemblyFormat = FormatterAssemblyStyle.Simple;
				SerializationReader.formatter.TypeFormat = FormatterTypeStyle.TypesWhenNeeded;
			}
			return SerializationReader.formatter;
		}

		/// <summary>
		/// Determine whether the passed-in type code refers to an array type
		/// and deserializes the array if it is.
		/// Returns null if not an array type.
		/// </summary>
		/// <param name="typeCode">The SerializedType to check.</param>
		/// <param name="defaultElementType">The Type of array element; null if to be read from stream.</param>
		/// <returns />
		// Token: 0x06005941 RID: 22849 RVA: 0x001B0398 File Offset: 0x001AE598
		private object processArrayTypes(SerializedType typeCode, Type defaultElementType)
		{
			if (typeCode == SerializedType.StringArrayType)
			{
				return this.ReadOptimizedStringArray();
			}
			if (typeCode == SerializedType.Int32ArrayType)
			{
				return this.ReadInt32Array();
			}
			if (typeCode == SerializedType.Int64ArrayType)
			{
				return this.ReadInt64Array();
			}
			if (typeCode == SerializedType.DecimalArrayType)
			{
				return this.readDecimalArray();
			}
			if (typeCode == SerializedType.TimeSpanArrayType)
			{
				return this.ReadTimeSpanArray();
			}
			if (typeCode == SerializedType.UInt32ArrayType)
			{
				return this.ReadUInt32Array();
			}
			if (typeCode == SerializedType.UInt64ArrayType)
			{
				return this.ReadUInt64Array();
			}
			if (typeCode == SerializedType.DateTimeArrayType)
			{
				return this.ReadDateTimeArray();
			}
			if (typeCode == SerializedType.BooleanArrayType)
			{
				return this.readBooleanArray();
			}
			if (typeCode == SerializedType.ByteArrayType)
			{
				return this.readByteArray();
			}
			if (typeCode == SerializedType.CharArrayType)
			{
				return this.readCharArray();
			}
			if (typeCode == SerializedType.DoubleArrayType)
			{
				return this.readDoubleArray();
			}
			if (typeCode == SerializedType.SingleArrayType)
			{
				return this.readSingleArray();
			}
			if (typeCode == SerializedType.GuidArrayType)
			{
				return this.readGuidArray();
			}
			if (typeCode == SerializedType.SByteArrayType)
			{
				return this.readSByteArray();
			}
			if (typeCode == SerializedType.Int16ArrayType)
			{
				return this.ReadInt16Array();
			}
			if (typeCode == SerializedType.UInt16ArrayType)
			{
				return this.ReadUInt16Array();
			}
			if (typeCode == SerializedType.EmptyTypedArrayType)
			{
				return Array.CreateInstance((defaultElementType != null) ? defaultElementType : this.ReadTokenizedType(), 0);
			}
			if (typeCode == SerializedType.OtherTypedArrayType)
			{
				return this.ReadOptimizedObjectArray(this.ReadTokenizedType());
			}
			if (typeCode == SerializedType.ObjectArrayType)
			{
				return this.ReadOptimizedObjectArray(defaultElementType);
			}
			if (typeCode == SerializedType.FullyOptimizedTypedArrayType || typeCode == SerializedType.PartiallyOptimizedTypedArrayType || typeCode == SerializedType.NonOptimizedTypedArrayType)
			{
				BitArray bitArray = this.readTypedArrayOptimizeFlags(typeCode);
				int num = this.ReadOptimizedInt32();
				if (defaultElementType == null)
				{
					defaultElementType = this.ReadTokenizedType();
				}
				Array array = Array.CreateInstance(defaultElementType, num);
				for (int i = 0; i < num; i++)
				{
					if (bitArray == null)
					{
						array.SetValue(this.ReadObject(), i);
					}
					else if (bitArray == SerializationReader.FullyOptimizableTypedArray || !bitArray[i])
					{
						IOwnedDataSerializable ownedDataSerializable = (IOwnedDataSerializable)Activator.CreateInstance(defaultElementType);
						this.ReadOwnedData(ownedDataSerializable, null);
						array.SetValue(ownedDataSerializable, i);
					}
				}
				return array;
			}
			return null;
		}

		/// <summary>
		/// Returns the string value associated with the string token read next from the stream.
		/// </summary>
		/// <returns>A DateTime value.</returns>
		// Token: 0x06005942 RID: 22850 RVA: 0x001B057C File Offset: 0x001AE77C
		private string readTokenizedString(int bucket)
		{
			int num = this.ReadOptimizedInt32();
			return this.TokenStrings[(num << 7) + bucket];
		}

		/// <summary>
		/// Returns the SerializedType read next from the stream.
		/// </summary>
		/// <returns>A SerializedType value.</returns>
		// Token: 0x06005943 RID: 22851 RVA: 0x001B059C File Offset: 0x001AE79C
		private SerializedType readTypeCode()
		{
			return (SerializedType)this.ReadByte();
		}

		/// <summary>
		/// Internal implementation returning a Bool[].
		/// </summary>
		/// <returns>A Bool[].</returns>
		// Token: 0x06005944 RID: 22852 RVA: 0x001B05A4 File Offset: 0x001AE7A4
		private bool[] readBooleanArray()
		{
			BitArray bitArray = this.ReadOptimizedBitArray();
			bool[] array = new bool[bitArray.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = bitArray[i];
			}
			return array;
		}

		/// <summary>
		/// Internal implementation returning a Byte[].
		/// </summary>
		/// <returns>A Byte[].</returns>
		// Token: 0x06005945 RID: 22853 RVA: 0x001B05DD File Offset: 0x001AE7DD
		private byte[] readByteArray()
		{
			return base.ReadBytes(this.ReadOptimizedInt32());
		}

		/// <summary>
		/// Internal implementation returning a Char[].
		/// </summary>
		/// <returns>A Char[].</returns>
		// Token: 0x06005946 RID: 22854 RVA: 0x001B05EB File Offset: 0x001AE7EB
		private char[] readCharArray()
		{
			return base.ReadChars(this.ReadOptimizedInt32());
		}

		/// <summary>
		/// Internal implementation returning a Decimal[].
		/// </summary>
		/// <returns>A Decimal[].</returns>
		// Token: 0x06005947 RID: 22855 RVA: 0x001B05FC File Offset: 0x001AE7FC
		private decimal[] readDecimalArray()
		{
			decimal[] array = new decimal[this.ReadOptimizedInt32()];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.ReadOptimizedDecimal();
			}
			return array;
		}

		/// <summary>
		/// Internal implementation returning a Double[].
		/// </summary>
		/// <returns>A Double[].</returns>
		// Token: 0x06005948 RID: 22856 RVA: 0x001B0634 File Offset: 0x001AE834
		private double[] readDoubleArray()
		{
			double[] array = new double[this.ReadOptimizedInt32()];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.ReadDouble();
			}
			return array;
		}

		/// <summary>
		/// Internal implementation returning a Guid[].
		/// </summary>
		/// <returns>A Guid[].</returns>
		// Token: 0x06005949 RID: 22857 RVA: 0x001B0668 File Offset: 0x001AE868
		private Guid[] readGuidArray()
		{
			Guid[] array = new Guid[this.ReadOptimizedInt32()];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.ReadGuid();
			}
			return array;
		}

		/// <summary>
		/// Internal implementation returning an SByte[].
		/// </summary>
		/// <returns>An SByte[].</returns>
		// Token: 0x0600594A RID: 22858 RVA: 0x001B06A0 File Offset: 0x001AE8A0
		private sbyte[] readSByteArray()
		{
			sbyte[] array = new sbyte[this.ReadOptimizedInt32()];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.ReadSByte();
			}
			return array;
		}

		/// <summary>
		/// Internal implementation returning a Single[].
		/// </summary>
		/// <returns>A Single[].</returns>
		// Token: 0x0600594B RID: 22859 RVA: 0x001B06D4 File Offset: 0x001AE8D4
		private float[] readSingleArray()
		{
			float[] array = new float[this.ReadOptimizedInt32()];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = this.ReadSingle();
			}
			return array;
		}

		// Token: 0x0600594C RID: 22860 RVA: 0x001B0705 File Offset: 0x001AE905
		[Conditional("DEBUG")]
		public void DumpStringTables(ArrayList list)
		{
			list.AddRange(this.TokenStrings);
		}

		// Token: 0x04002C24 RID: 11300
		private static readonly BitArray FullyOptimizableTypedArray = new BitArray(0);

		// Token: 0x04002C25 RID: 11301
		internal const ushort GameObjectStartValue = 64206;

		// Token: 0x04002C26 RID: 11302
		internal const ushort GameObjectEndValue = 44203;

		// Token: 0x04002C27 RID: 11303
		internal const ushort GameObjectFinishValue = 52958;

		// Token: 0x04002C28 RID: 11304
		private static SerializationReader Instance = new SerializationReader(FastSerialization.SharedCache);

		// Token: 0x04002C29 RID: 11305
		public MemoryStream Stream;

		// Token: 0x04002C2A RID: 11306
		public FastSerialization.Cache Cache;

		// Token: 0x04002C2B RID: 11307
		public int FileVersion;

		// Token: 0x04002C2C RID: 11308
		public Dictionary<string, Version> ModVersions;

		// Token: 0x04002C2D RID: 11309
		internal ulong ExtensionHash;

		// Token: 0x04002C2E RID: 11310
		public int Errors;

		// Token: 0x04002C2F RID: 11311
		private string[] TokenStrings = Array.Empty<string>();

		// Token: 0x04002C30 RID: 11312
		private object[] TokenObjects = Array.Empty<object>();

		// Token: 0x04002C31 RID: 11313
		private ITokenized[] Tokenized = Array.Empty<ITokenized>();

		// Token: 0x04002C32 RID: 11314
		private Type[] TokenTypes = Array.Empty<Type>();

		// Token: 0x04002C33 RID: 11315
		private string[] TokenTypeNames = Array.Empty<string>();

		// Token: 0x04002C34 RID: 11316
		private GameObject[] TokenGameObjects = Array.Empty<GameObject>();

		// Token: 0x04002C35 RID: 11317
		private GameObjectReference[] GameObjectReferences = Array.Empty<GameObjectReference>();

		// Token: 0x04002C36 RID: 11318
		private EventRegistry[] EventRegistries = Array.Empty<EventRegistry>();

		// Token: 0x04002C37 RID: 11319
		private List<GameObject> InvalidGameObjects;

		// Token: 0x04002C38 RID: 11320
		private long StringPosition;

		// Token: 0x04002C39 RID: 11321
		private long ObjectPosition;

		// Token: 0x04002C3A RID: 11322
		private long TypePosition;

		// Token: 0x04002C3B RID: 11323
		private long GameObjectPosition;

		// Token: 0x04002C3C RID: 11324
		private long GameObjectReferencePosition;

		// Token: 0x04002C3D RID: 11325
		private long EventRegistryPosition;

		// Token: 0x04002C3E RID: 11326
		private long TokenizedPosition;

		// Token: 0x04002C3F RID: 11327
		private int GameObjectLength;

		// Token: 0x04002C40 RID: 11328
		private int EventRegistryLength;

		// Token: 0x04002C41 RID: 11329
		private int TokenizedLength;

		// Token: 0x04002C42 RID: 11330
		private bool SerializePlayer;

		// Token: 0x04002C43 RID: 11331
		public static Guid ImmutableObject = new Guid("00000000-0000-0000-0000-000000000002");

		// Token: 0x04002C44 RID: 11332
		public static Guid PlayerGuid = new Guid("00000000-0000-0000-0000-000000000001");

		// Token: 0x04002C45 RID: 11333
		public Dictionary<GameObject, Cell> Locations = new Dictionary<GameObject, Cell>();

		// Token: 0x04002C46 RID: 11334
		private object[] CapacityArgs = new object[1];

		// Token: 0x04002C47 RID: 11335
		private static BinaryFormatter formatter = null;

		// Token: 0x04002C48 RID: 11336
		public static ResolveEventHandler modAssemblyResolveHandler = delegate(object sender, ResolveEventArgs args)
		{
			AssemblyName assemblyName = new AssemblyName(args.Name);
			string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(assemblyName.Name);
			foreach (ModInfo modInfo in ModManager.Mods)
			{
				string name = assemblyName.Name;
				Assembly assembly = modInfo.Assembly;
				string text;
				if (assembly == null)
				{
					text = null;
				}
				else
				{
					AssemblyName name2 = assembly.GetName();
					text = ((name2 != null) ? name2.Name : null);
				}
				if (name == text)
				{
					return modInfo.Assembly;
				}
				if (fileNameWithoutExtension == modInfo.ID && !modInfo.IsEnabled)
				{
					MetricsManager.LogModError(modInfo, "Required mod " + modInfo.DisplayTitleStripped + " has status: " + Enum.GetName(typeof(ModState), modInfo.State));
					return null;
				}
			}
			MetricsManager.LogError("Unable to resolve mod assembly: " + assemblyName.Name);
			return null;
		};

		// Token: 0x04002C49 RID: 11337
		public static ResolveEventHandler assemblyResolveHandler = (object a, ResolveEventArgs b) => Assembly.GetExecutingAssembly();
	}
}

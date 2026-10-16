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
using XRL.Collections;
using XRL.Serialization;
using XRL.World.Parts;

namespace XRL.World
{
	/// <summary>
	/// A SerializationWriter instance is used to store values and objects in a byte array.
	///
	/// Once an instance is created, use the various methods to store the required data.
	/// ToArray() will return a byte[] containing all of the data required for deserialization.
	/// This can be stored in the SerializationInfo parameter in an ISerializable.GetObjectData() method.
	/// <para />
	/// As an alternative to ToArray(), if you want to apply some post-processing to the serialized bytes, 
	/// such as compression, call AppendTokenTables first to ensure that the string and object token tables 
	/// are appended to the stream, and then cast BaseStream to MemoryStream. You can then access the
	/// MemoryStream's internal buffer as follows:
	/// <para />
	/// <example><code>
	/// writer.AppendTokenTables();
	/// MemoryStream stream = (MemoryStream) writer.BaseStream;
	///             	serializedData = MiniLZO.Compress(stream.GetBuffer(), (int) stream.Length);
	/// </code></example>
	/// </summary>
	// Token: 0x02000919 RID: 2329
	public sealed class SerializationWriter : BinaryWriter
	{
		/// <summary>
		/// Holds a list of optional IFastSerializationTypeSurrogate instances which
		/// SerializationWriter and SerializationReader will use to serialize objects
		/// not directly supported.
		/// It is important to use the same list on both client and server ends to ensure
		/// that the same surrogated-types are supported.
		/// </summary>
		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x0600594E RID: 22862 RVA: 0x001B0789 File Offset: 0x001AE989
		public static List<IFastSerializationTypeSurrogate> TypeSurrogates
		{
			get
			{
				if (SerializationWriter.typeSurrogates == null)
				{
					SerializationWriter.typeSurrogates = new List<IFastSerializationTypeSurrogate>();
					SerializationWriter.typeSurrogates.Add(new ColorSerializationSurrogate());
				}
				return SerializationWriter.typeSurrogates;
			}
		}

		// Token: 0x0600594F RID: 22863 RVA: 0x001B07B0 File Offset: 0x001AE9B0
		public static bool TryGetShared(out SerializationWriter Writer)
		{
			GameManager.IsOnGameContext();
			if (FastSerialization.SharedCache.Reserved)
			{
				Writer = null;
				return false;
			}
			Writer = SerializationWriter.Instance;
			return FastSerialization.SharedCache.Reserved = true;
		}

		// Token: 0x06005950 RID: 22864 RVA: 0x001B07E9 File Offset: 0x001AE9E9
		public static void ReleaseShared()
		{
			SerializationWriter.Instance.Clear();
			FastSerialization.SharedCache.Reserved = false;
		}

		// Token: 0x06005951 RID: 22865 RVA: 0x001B0800 File Offset: 0x001AEA00
		public static SerializationWriter Get()
		{
			SerializationWriter serializationWriter;
			if (SerializationWriter.TryGetShared(out serializationWriter))
			{
				return serializationWriter;
			}
			return new SerializationWriter(new FastSerialization.Cache(0));
		}

		// Token: 0x06005952 RID: 22866 RVA: 0x001B0823 File Offset: 0x001AEA23
		public static void Release(SerializationWriter Writer)
		{
			if (Writer == SerializationWriter.Instance)
			{
				SerializationWriter.ReleaseShared();
				return;
			}
			Writer.Dispose();
		}

		/// <summary>
		/// Creates a FastSerializer around the specified stream
		/// Note: The stream must be seekable in this version to allow the token table 
		/// offset to be written on completion 
		/// </summary>
		/// <param name="stream">The seekable stream in which to store data</param>
		/// <param name="FileVersion">The current serialization version, used for upgrading saves to newer versions.</param>
		/// <param name="SerializePlayer">
		/// Whether the player's game object should be serialized,
		/// typically true when saving the entire game and false when saving individual zones.
		/// </param>
		// Token: 0x06005953 RID: 22867 RVA: 0x001B083C File Offset: 0x001AEA3C
		public SerializationWriter(MemoryStream Stream)
			: base(Stream, FastSerialization.Encoding)
		{
			this.Stream = Stream;
			if (!Stream.CanSeek)
			{
				throw new InvalidOperationException("Stream must be seekable");
			}
		}

		// Token: 0x06005954 RID: 22868 RVA: 0x001B088C File Offset: 0x001AEA8C
		public SerializationWriter(FastSerialization.Cache Cache)
			: this(Cache.MemoryStream)
		{
			this.Initialize(Cache);
		}

		/// <summary>
		/// Gets or Sets a boolean flag to indicate whether to optimize for size (default)
		/// by storing data as packed bits or sections where possible.
		/// Setting this value to false will turn off this optimization and store
		/// data directly which increases the speed.
		/// Note: This only affects optimization of data passed to the WriteObject method
		/// and direct calls to the WriteOptimized methods will always pack data into
		/// the smallest space where possible.
		/// </summary>
		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x06005955 RID: 22869 RVA: 0x001B08A1 File Offset: 0x001AEAA1
		// (set) Token: 0x06005956 RID: 22870 RVA: 0x001B08A9 File Offset: 0x001AEAA9
		public bool OptimizeForSize
		{
			get
			{
				return this.optimizeForSize;
			}
			set
			{
				this.optimizeForSize = value;
			}
		}

		/// <summary>
		/// Gets or Sets a boolean flag to indicate whether to preserve the scale within
		/// a Decimal value when it would have no effect on the represented value.
		/// Note: a 2m value and a 2.00m value represent the same value but internally they 
		/// are stored differently - the former has a value of 2 and a scale of 0 and
		/// the latter has a value of 200 and a scale of 2. 
		/// The scaling factor also preserves any trailing zeroes in a Decimal number. 
		/// Trailing zeroes do not affect the value of a Decimal number in arithmetic or 
		/// comparison operations. However, trailing zeroes can be revealed by the ToString 
		/// method if an appropriate format string is applied.
		/// From a serialization point of view, the former will take 2 bytes whereas the 
		/// latter would take 4 bytes, therefore it is preferable to not save the scale where
		/// it doesn't affect the represented value.
		/// </summary>
		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x06005957 RID: 22871 RVA: 0x001B08B2 File Offset: 0x001AEAB2
		// (set) Token: 0x06005958 RID: 22872 RVA: 0x001B08BA File Offset: 0x001AEABA
		public bool PreserveDecimalScale
		{
			get
			{
				return this.preserveDecimalScale;
			}
			set
			{
				this.preserveDecimalScale = value;
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x06005959 RID: 22873 RVA: 0x001B08C3 File Offset: 0x001AEAC3
		// (set) Token: 0x0600595A RID: 22874 RVA: 0x001B08D0 File Offset: 0x001AEAD0
		public long Position
		{
			get
			{
				return this.OutStream.Position;
			}
			set
			{
				this.OutStream.Position = value;
			}
		}

		// Token: 0x0600595B RID: 22875 RVA: 0x001B08E0 File Offset: 0x001AEAE0
		public void Start(int FileVersion, bool SerializePlayer = false)
		{
			this.FileVersion = FileVersion;
			this.SerializePlayer = SerializePlayer;
			this.ReserveHeader();
			this.LocalAssemblies.Add(typeof(int).Assembly);
			this.LocalAssemblies.Add(typeof(GameObject).Assembly);
			foreach (Assembly assembly in ModManager.ModAssemblies)
			{
				this.LocalAssemblies.Add(assembly);
			}
			this.SetPlayer(The.Player);
			this.WriteModVersions();
			this.WriteExtensionHash();
		}

		// Token: 0x0600595C RID: 22876 RVA: 0x001B099C File Offset: 0x001AEB9C
		public void Initialize(FastSerialization.Cache Cache)
		{
			if (this.Buffer != null)
			{
				return;
			}
			this.Buffer = new byte[16];
			this.Blocks = new Stack<SerializationWriter.Block>();
			this.TokenObjects = Cache.Objects;
			this.TokenByObject = new Dictionary<object, int>();
			this.TokenStrings = new SerializationWriter.UniqueStringList();
			this.TokenTypes = Cache.Types;
			this.TokenByType = new Dictionary<Type, int>(768);
			this.TokenGameObjects = Cache.GameObjects;
			this.GameObjectReferences = Cache.GameObjectReferences;
			this.TokenByGameObject = new Dictionary<GameObject, int>(2560);
			this.EventRegistries = Cache.EventRegistries;
			this.Tokenized = Cache.Tokenized;
			SerializationWriter.formatter = null;
			this.LocalAssemblies = new HashSet<Assembly>(ModManager.ModAssemblies.Count + 2);
		}

		// Token: 0x0600595D RID: 22877 RVA: 0x001B0A68 File Offset: 0x001AEC68
		public void Clear()
		{
			this.Stream.SetLength(0L);
			this.TokenObjects.Clear();
			this.TokenByObject.Clear();
			this.TokenStrings.Clear();
			this.TokenTypes.Clear();
			this.TokenByType.Clear();
			this.TokenGameObjects.Clear();
			this.GameObjectReferences.Clear();
			this.TokenByGameObject.Clear();
			this.EventRegistries.Clear();
			this.Tokenized.Clear();
			this.LocalAssemblies.Clear();
			SerializationWriter.formatter = null;
		}

		// Token: 0x0600595E RID: 22878 RVA: 0x001B0B01 File Offset: 0x001AED01
		public void SetPlayer(GameObject Object)
		{
			if (this.TokenGameObjects.Count == 0)
			{
				this.TokenGameObjects.Add(Object);
			}
			else
			{
				this.TokenGameObjects[0] = Object;
			}
			if (Object != null)
			{
				this.TokenByGameObject[Object] = 0;
			}
		}

		/// <summary>
		/// Writes an ArrayList into the stream using the fewest number of bytes possible.
		/// Stored Size: 1 byte upwards depending on data content
		/// Notes:
		/// A null Arraylist takes 1 byte.
		/// An empty ArrayList takes 2 bytes.
		/// The contents are stored using WriteOptimized(ArrayList) which should be used
		/// if the ArrayList is guaranteed never to be null.
		/// </summary>
		/// <param name="value">The ArrayList to store.</param>
		// Token: 0x0600595F RID: 22879 RVA: 0x001B0B3B File Offset: 0x001AED3B
		public void Write(ArrayList value)
		{
			if (value == null)
			{
				this.writeTypeCode(SerializedType.NullType);
				return;
			}
			this.writeTypeCode(SerializedType.ArrayListType);
			this.WriteOptimized(value);
		}

		/// <summary>
		/// Writes a BitArray value into the stream using the fewest number of bytes possible.
		/// Stored Size: 1 byte upwards depending on data content
		/// Notes:
		/// A null BitArray takes 1 byte.
		/// An empty BitArray takes 2 bytes.
		/// </summary>
		/// <param name="value">The BitArray value to store.</param>
		// Token: 0x06005960 RID: 22880 RVA: 0x001B0B5E File Offset: 0x001AED5E
		public void Write(BitArray value)
		{
			if (value == null)
			{
				this.writeTypeCode(SerializedType.NullType);
				return;
			}
			this.writeTypeCode(SerializedType.BitArrayType);
			this.WriteOptimized(value);
		}

		/// <summary>
		/// Writes a BitVector32 into the stream.
		/// Stored Size: 4 bytes.
		/// </summary>
		/// <param name="value">The BitVector32 to store.</param>
		// Token: 0x06005961 RID: 22881 RVA: 0x001B0B81 File Offset: 0x001AED81
		public void Write(BitVector32 value)
		{
			base.Write(value.Data);
		}

		/// <summary>
		/// Writes a DateTime value into the stream.
		/// Stored Size: 8 bytes
		/// </summary>
		/// <param name="value">The DateTime value to store.</param>
		// Token: 0x06005962 RID: 22882 RVA: 0x001B0B90 File Offset: 0x001AED90
		public void Write(DateTime value)
		{
			this.Write(value.ToBinary());
		}

		/// <summary>
		/// Writes a Guid into the stream.
		/// Stored Size: 16 bytes.
		/// </summary>
		/// <param name="value" />
		// Token: 0x06005963 RID: 22883 RVA: 0x001B0BA0 File Offset: 0x001AEDA0
		public void Write(Guid value)
		{
			Span<byte> span = this.Buffer.AsSpan<byte>(0, 16);
			if (value.TryWriteBytes(span))
			{
				this.OutStream.Write(this.Buffer, 0, 16);
				return;
			}
			base.Write(value.ToByteArray());
		}

		/// <summary>
		/// Allows any object implementing IOwnedDataSerializable to serialize itself
		/// into this SerializationWriter.
		/// A context may also be used to give the object an indication of what data
		/// to store. As an example, using a BitVector32 gives a list of flags and
		/// the object can conditionally store data depending on those flags.
		/// </summary>
		/// <param name="target">The IOwnedDataSerializable object to ask for owned data</param>
		/// <param name="context">An arbtritrary object but BitVector32 recommended</param>
		// Token: 0x06005964 RID: 22884 RVA: 0x001B0BE8 File Offset: 0x001AEDE8
		public void Write(IOwnedDataSerializable target, object context)
		{
			target.SerializeOwnedData(this, context);
		}

		/// <summary>
		/// Stores an object into the stream using the fewest number of bytes possible.
		/// Stored Size: 1 byte upwards depending on type and/or content.
		///
		/// 1 byte: null, DBNull.Value, Boolean
		///
		/// 1 to 2 bytes: Int16, UInt16, Byte, SByte, Char, 
		///
		/// 1 to 4 bytes: Int32, UInt32, Single, BitVector32
		///
		/// 1 to 8 bytes: DateTime, TimeSpan, Double, Int64, UInt64
		///
		/// 1 or 16 bytes: Guid
		///
		/// 1 plus content: string, object[], byte[], char[], BitArray, Type, ArrayList
		///
		/// Any other object be stored using a .Net Binary formatter but this should 
		/// only be allowed as a last resort:
		/// Since this is effectively a different serialization session, there is a 
		/// possibility of the same shared object being serialized twice or, if the 
		/// object has a reference directly or indirectly back to the parent object, 
		/// there is a risk of looping which will throw an exception.
		///
		/// The type of object is checked with the most common types being checked first.
		/// Each 'section' can be reordered to provide optimum speed but the check for
		/// null should always be first and the default serialization always last.
		///
		/// Once the type is identified, a SerializedType byte is stored in the stream
		/// followed by the data for the object (certain types/values may not require
		/// storage of data as the SerializedType may imply the value).
		///
		/// For certain objects, if the value is within a certain range then optimized
		/// storage may be used. If the value doesn't meet the required optimization
		/// criteria then the value is stored directly.
		/// The checks for optimization may be disabled by setting the OptimizeForSize
		/// property to false in which case the value is stored directly. This could 
		/// result in a slightly larger stream but there will be a speed increate to
		/// compensate.
		/// </summary>
		/// <param name="value">The object to store.</param>
		// Token: 0x06005965 RID: 22885 RVA: 0x001B0BF4 File Offset: 0x001AEDF4
		public void WriteObject(object value)
		{
			try
			{
				if (value == null)
				{
					this.writeTypeCode(SerializedType.NullType);
				}
				else if (value is string)
				{
					this.WriteOptimized((string)value);
				}
				else if (value is int)
				{
					int num = (int)value;
					if (num == 0)
					{
						this.writeTypeCode(SerializedType.ZeroInt32Type);
					}
					else if (num == -1)
					{
						this.writeTypeCode(SerializedType.MinusOneInt32Type);
					}
					else if (num == 1)
					{
						this.writeTypeCode(SerializedType.OneInt32Type);
					}
					else
					{
						if (this.optimizeForSize)
						{
							if (num > 0)
							{
								if (num <= 2097151)
								{
									this.writeTypeCode(SerializedType.OptimizedInt32Type);
									this.write7bitEncodedSigned32BitValue(num);
									return;
								}
							}
							else
							{
								int num2 = -(num + 1);
								if (num2 <= 2097151)
								{
									this.writeTypeCode(SerializedType.OptimizedInt32NegativeType);
									this.write7bitEncodedSigned32BitValue(num2);
									return;
								}
							}
						}
						this.writeTypeCode(SerializedType.Int32Type);
						this.Write(num);
					}
				}
				else if (value == DBNull.Value)
				{
					this.writeTypeCode(SerializedType.DBNullType);
				}
				else if (value is bool)
				{
					this.writeTypeCode(((bool)value) ? SerializedType.BooleanTrueType : SerializedType.BooleanFalseType);
				}
				else if (value is decimal)
				{
					decimal num3 = (decimal)value;
					if (num3 == 0m)
					{
						this.writeTypeCode(SerializedType.ZeroDecimalType);
					}
					else if (num3 == 1m)
					{
						this.writeTypeCode(SerializedType.OneDecimalType);
					}
					else
					{
						this.writeTypeCode(SerializedType.DecimalType);
						this.WriteOptimized(num3);
					}
				}
				else if (value is DateTime)
				{
					DateTime dateTime = (DateTime)value;
					if (dateTime == DateTime.MinValue)
					{
						this.writeTypeCode(SerializedType.MinDateTimeType);
					}
					else if (dateTime == DateTime.MaxValue)
					{
						this.writeTypeCode(SerializedType.MaxDateTimeType);
					}
					else if (this.optimizeForSize && dateTime.Ticks % 10000L == 0L)
					{
						this.writeTypeCode(SerializedType.OptimizedDateTimeType);
						this.WriteOptimized(dateTime);
					}
					else
					{
						this.writeTypeCode(SerializedType.DateTimeType);
						this.Write(dateTime);
					}
				}
				else if (value is double)
				{
					double num4 = (double)value;
					if (num4 == 0.0)
					{
						this.writeTypeCode(SerializedType.ZeroDoubleType);
					}
					else if (num4 == 1.0)
					{
						this.writeTypeCode(SerializedType.OneDoubleType);
					}
					else
					{
						this.writeTypeCode(SerializedType.DoubleType);
						this.Write(num4);
					}
				}
				else if (value is float)
				{
					float num5 = (float)value;
					if (num5 == 0f)
					{
						this.writeTypeCode(SerializedType.ZeroSingleType);
					}
					else if (num5 == 1f)
					{
						this.writeTypeCode(SerializedType.OneSingleType);
					}
					else
					{
						this.writeTypeCode(SerializedType.SingleType);
						this.Write(num5);
					}
				}
				else if (value is short)
				{
					short num6 = (short)value;
					if (num6 == 0)
					{
						this.writeTypeCode(SerializedType.ZeroInt16Type);
					}
					else if (num6 == -1)
					{
						this.writeTypeCode(SerializedType.MinusOneInt16Type);
					}
					else if (num6 == 1)
					{
						this.writeTypeCode(SerializedType.OneInt16Type);
					}
					else
					{
						if (this.optimizeForSize)
						{
							if (num6 > 0)
							{
								if (num6 <= 127)
								{
									this.writeTypeCode(SerializedType.OptimizedInt16Type);
									this.write7bitEncodedSigned32BitValue((int)num6);
									return;
								}
							}
							else
							{
								int num7 = (int)(-(int)(num6 + 1));
								if (num7 <= 127)
								{
									this.writeTypeCode(SerializedType.OptimizedInt16NegativeType);
									this.write7bitEncodedSigned32BitValue(num7);
									return;
								}
							}
						}
						this.writeTypeCode(SerializedType.Int16Type);
						this.Write(num6);
					}
				}
				else if (value is Guid)
				{
					Guid guid = (Guid)value;
					if (guid == Guid.Empty)
					{
						this.writeTypeCode(SerializedType.EmptyGuidType);
					}
					else
					{
						this.writeTypeCode(SerializedType.GuidType);
						this.Write(guid);
					}
				}
				else if (value is long)
				{
					long num8 = (long)value;
					if (num8 == 0L)
					{
						this.writeTypeCode(SerializedType.ZeroInt64Type);
					}
					else if (num8 == -1L)
					{
						this.writeTypeCode(SerializedType.MinusOneInt64Type);
					}
					else if (num8 == 1L)
					{
						this.writeTypeCode(SerializedType.OneInt64Type);
					}
					else
					{
						if (this.optimizeForSize)
						{
							if (num8 > 0L)
							{
								if (num8 <= 562949953421311L)
								{
									this.writeTypeCode(SerializedType.OptimizedInt64Type);
									this.write7bitEncodedSigned64BitValue(num8);
									return;
								}
							}
							else
							{
								long num9 = -(num8 + 1L);
								if (num9 <= 562949953421311L)
								{
									this.writeTypeCode(SerializedType.OptimizedInt64NegativeType);
									this.write7bitEncodedSigned64BitValue(num9);
									return;
								}
							}
						}
						this.writeTypeCode(SerializedType.Int64Type);
						this.Write(num8);
					}
				}
				else if (value is byte)
				{
					byte b = (byte)value;
					if (b == 0)
					{
						this.writeTypeCode(SerializedType.ZeroByteType);
					}
					else if (b == 1)
					{
						this.writeTypeCode(SerializedType.OneByteType);
					}
					else
					{
						this.writeTypeCode(SerializedType.ByteType);
						this.Write(b);
					}
				}
				else if (value is char)
				{
					char c = (char)value;
					if (c == '\0')
					{
						this.writeTypeCode(SerializedType.ZeroCharType);
					}
					else if (c == '\u0001')
					{
						this.writeTypeCode(SerializedType.OneCharType);
					}
					else
					{
						this.writeTypeCode(SerializedType.CharType);
						this.Write(c);
					}
				}
				else if (value is sbyte)
				{
					sbyte b2 = (sbyte)value;
					if (b2 == 0)
					{
						this.writeTypeCode(SerializedType.ZeroSByteType);
					}
					else if (b2 == 1)
					{
						this.writeTypeCode(SerializedType.OneSByteType);
					}
					else
					{
						this.writeTypeCode(SerializedType.SByteType);
						this.Write(b2);
					}
				}
				else if (value is uint)
				{
					uint num10 = (uint)value;
					if (num10 == 0U)
					{
						this.writeTypeCode(SerializedType.ZeroUInt32Type);
					}
					else if (num10 == 1U)
					{
						this.writeTypeCode(SerializedType.OneUInt32Type);
					}
					else if (this.optimizeForSize && num10 <= 2097151U)
					{
						this.writeTypeCode(SerializedType.OptimizedUInt32Type);
						this.write7bitEncodedUnsigned32BitValue(num10);
					}
					else
					{
						this.writeTypeCode(SerializedType.UInt32Type);
						this.Write(num10);
					}
				}
				else if (value is ushort)
				{
					ushort num11 = (ushort)value;
					if (num11 == 0)
					{
						this.writeTypeCode(SerializedType.ZeroUInt16Type);
					}
					else if (num11 == 1)
					{
						this.writeTypeCode(SerializedType.OneUInt16Type);
					}
					else if (this.optimizeForSize && num11 <= 127)
					{
						this.writeTypeCode(SerializedType.OptimizedUInt16Type);
						this.write7bitEncodedUnsigned32BitValue((uint)num11);
					}
					else
					{
						this.writeTypeCode(SerializedType.UInt16Type);
						this.Write(num11);
					}
				}
				else if (value is ulong)
				{
					ulong num12 = (ulong)value;
					if (num12 == 0UL)
					{
						this.writeTypeCode(SerializedType.ZeroUInt64Type);
					}
					else if (num12 == 1UL)
					{
						this.writeTypeCode(SerializedType.OneUInt64Type);
					}
					else if (this.optimizeForSize && num12 <= 562949953421311UL)
					{
						this.writeTypeCode(SerializedType.OptimizedUInt64Type);
						this.WriteOptimized(num12);
					}
					else
					{
						this.writeTypeCode(SerializedType.UInt64Type);
						this.Write(num12);
					}
				}
				else if (value is TimeSpan)
				{
					TimeSpan timeSpan = (TimeSpan)value;
					if (timeSpan == TimeSpan.Zero)
					{
						this.writeTypeCode(SerializedType.ZeroTimeSpanType);
					}
					else if (this.optimizeForSize && timeSpan.Ticks % 10000L == 0L)
					{
						this.writeTypeCode(SerializedType.OptimizedTimeSpanType);
						this.WriteOptimized(timeSpan);
					}
					else
					{
						this.writeTypeCode(SerializedType.TimeSpanType);
						this.Write(timeSpan);
					}
				}
				else
				{
					GameObject gameObject = value as GameObject;
					if (gameObject != null)
					{
						this.writeTypeCode(SerializedType.GameObjectType);
						this.WriteGameObject(gameObject);
					}
					else
					{
						IComposite composite = value as IComposite;
						if (composite != null)
						{
							this.SerializeComposite(composite);
						}
						else
						{
							Array array = value as Array;
							if (array != null && array.Rank == 1)
							{
								this.writeTypedArray(array, true);
							}
							else
							{
								Type type = value as Type;
								if (type != null)
								{
									this.writeTypeCode(SerializedType.TypeType);
									this.WriteTokenized(type);
								}
								else if (value is BitArray)
								{
									this.writeTypeCode(SerializedType.BitArrayType);
									this.WriteOptimized((BitArray)value);
								}
								else if (value is BitVector32)
								{
									this.writeTypeCode(SerializedType.BitVector32Type);
									this.Write((BitVector32)value);
								}
								else if (value is SerializationWriter.SingletonTypeWrapper)
								{
									this.writeTypeCode(SerializedType.SingleInstanceType);
									this.WriteTokenized(((SerializationWriter.SingletonTypeWrapper)value).WrappedType);
								}
								else if (value is List<string>)
								{
									this.writeTypeCode(SerializedType.StringListType);
									this.Write(value as List<string>);
								}
								else if (value is ArrayList)
								{
									this.writeTypeCode(SerializedType.ArrayListType);
									this.WriteOptimized(value as ArrayList);
								}
								else if (value is IDictionary)
								{
									this.writeTypeCode(SerializedType.IDictionaryType);
									this.WriteTokenized(value.GetType());
									this.Write((IDictionary)value);
								}
								else if (value is IList && !(value is Array))
								{
									this.writeTypeCode(SerializedType.IListType);
									this.WriteTokenized(value.GetType());
									this.Write((IList)value);
								}
								else if (value is Enum)
								{
									Type type2 = value.GetType();
									Type underlyingType = Enum.GetUnderlyingType(type2);
									if (underlyingType == typeof(int) || underlyingType == typeof(uint))
									{
										uint num13 = (uint)((underlyingType == typeof(int)) ? ((int)value) : ((int)((uint)value)));
										if (num13 <= 2097151U)
										{
											this.writeTypeCode(SerializedType.OptimizedEnumType);
											this.WriteTokenized(type2);
											this.write7bitEncodedUnsigned32BitValue(num13);
										}
										else
										{
											this.writeTypeCode(SerializedType.EnumType);
											this.WriteTokenized(type2);
											this.Write(num13);
										}
									}
									else if (underlyingType == typeof(long) || underlyingType == typeof(ulong))
									{
										ulong num14 = (ulong)((underlyingType == typeof(long)) ? ((long)value) : ((long)((ulong)value)));
										if (num14 <= 562949953421311UL)
										{
											this.writeTypeCode(SerializedType.OptimizedEnumType);
											this.WriteTokenized(type2);
											this.write7bitEncodedUnsigned64BitValue(num14);
										}
										else
										{
											this.writeTypeCode(SerializedType.EnumType);
											this.WriteTokenized(type2);
											this.Write(num14);
										}
									}
									else
									{
										this.writeTypeCode(SerializedType.EnumType);
										this.WriteTokenized(type2);
										if (underlyingType == typeof(byte))
										{
											this.Write((byte)value);
										}
										else if (underlyingType == typeof(sbyte))
										{
											this.Write((sbyte)value);
										}
										else if (underlyingType == typeof(short))
										{
											this.Write((short)value);
										}
										else
										{
											this.Write((ushort)value);
										}
									}
								}
								else
								{
									Type type3 = value.GetType();
									IFastSerializationTypeSurrogate fastSerializationTypeSurrogate;
									if (SerializationWriter.isTypeRecreatable(type3))
									{
										this.writeTypeCode(SerializedType.OwnedDataSerializableAndRecreatableType);
										this.WriteTokenized(type3);
										this.Write((IOwnedDataSerializable)value, null);
									}
									else if ((fastSerializationTypeSurrogate = SerializationWriter.findSurrogateForType(type3)) != null)
									{
										this.writeTypeCode(SerializedType.SurrogateHandledType);
										this.WriteTokenized(type3);
										fastSerializationTypeSurrogate.Serialize(this, value);
									}
									else
									{
										try
										{
											this.writeTypeCode(SerializedType.OtherType);
											SerializationWriter.createBinaryFormatter().Serialize(this.OutStream, value);
											if (SerializationWriter.BinaryFormattedTypes.Add(type3) && type3.Assembly != Assembly.GetExecutingAssembly())
											{
												MetricsManager.LogAssemblyError(type3, string.Format("Using binary formatter for type '{0}', convert it to an IComposite or serialize it manually.", type3));
											}
										}
										catch (Exception ex)
										{
											throw new Exception(string.Format("exception serializing value '{0}' of type '{1}'", value, type3), ex);
										}
									}
								}
							}
						}
					}
				}
			}
			catch (Exception ex2)
			{
				throw new Exception(string.Concat(new string[]
				{
					"exception serializing object ",
					value.ToString(),
					" of ",
					value.GetType().Name,
					" : "
				}), ex2);
			}
		}

		/// <summary>
		/// Calls WriteOptimized(string).
		/// This override to hide base BinaryWriter.Write(string).
		/// </summary>
		/// <param name="value">The string to store.</param>
		// Token: 0x06005966 RID: 22886 RVA: 0x001B1830 File Offset: 0x001AFA30
		public override void Write(string value)
		{
			this.WriteOptimized(value);
		}

		/// <summary>
		/// Writes a TimeSpan value into the stream.
		/// Stored Size: 8 bytes
		/// </summary>
		/// <param name="value">The TimeSpan value to store.</param>
		// Token: 0x06005967 RID: 22887 RVA: 0x001B1839 File Offset: 0x001AFA39
		public void Write(TimeSpan value)
		{
			this.Write(value.Ticks);
		}

		/// <summary>
		/// Stores a Type object into the stream.
		/// Stored Size: Depends on the length of the Type's name and whether the fullyQualified parameter is set.
		/// A null Type takes 1 byte.
		/// </summary>
		/// <param name="value">The Type to store.</param>
		/// <param name="fullyQualified">true to store the AssemblyQualifiedName or false to store the FullName. </param>
		// Token: 0x06005968 RID: 22888 RVA: 0x001B1848 File Offset: 0x001AFA48
		public void Write(Type value, bool fullyQualified)
		{
			if (value == null)
			{
				this.writeTypeCode(SerializedType.NullType);
				return;
			}
			this.writeTypeCode(SerializedType.TypeType);
			this.WriteTokenized(value);
		}

		/// <summary>
		/// Writes an non-null ArrayList into the stream using the fewest number of bytes possible.
		/// Stored Size: 1 byte upwards depending on data content
		/// Notes:
		/// An empty ArrayList takes 1 byte.
		/// </summary>
		/// <param name="value">The ArrayList to store. Must not be null.</param>
		// Token: 0x06005969 RID: 22889 RVA: 0x001B1871 File Offset: 0x001AFA71
		public void WriteOptimized(ArrayList value)
		{
			this.writeObjectArray(value.ToArray());
		}

		/// <summary>
		/// Writes a BitArray into the stream using the fewest number of bytes possible.
		/// Stored Size: 1 byte upwards depending on data content
		/// Notes:
		/// An empty BitArray takes 1 byte.
		/// </summary>
		/// <param name="value">The BitArray value to store. Must not be null.</param>
		// Token: 0x0600596A RID: 22890 RVA: 0x001B1880 File Offset: 0x001AFA80
		public void WriteOptimized(BitArray value)
		{
			this.write7bitEncodedSigned32BitValue(value.Length);
			if (value.Length > 0)
			{
				byte[] array = new byte[(value.Length + 7) / 8];
				value.CopyTo(array, 0);
				base.Write(array, 0, array.Length);
			}
		}

		/// <summary>
		/// Writes a BitVector32 into the stream using the fewest number of bytes possible.
		/// Stored Size: 1 to 4 bytes. (.Net is 4 bytes)
		///  1 to  7 bits takes 1 byte
		///  8 to 14 bits takes 2 bytes
		/// 15 to 21 bits takes 3 bytes
		/// 22 to 28 bits takes 4 bytes
		/// -------------------------------------------------------------------
		/// 29 to 32 bits takes 5 bytes - use Write(BitVector32) method instead
		///
		/// Try to order the BitVector32 masks so that the highest bits are least-likely
		/// to be set.
		/// </summary>
		/// <param name="value">The BitVector32 to store. Must not use more than 28 bits.</param>
		// Token: 0x0600596B RID: 22891 RVA: 0x001B18C5 File Offset: 0x001AFAC5
		public void WriteOptimized(BitVector32 value)
		{
			this.write7bitEncodedSigned32BitValue(value.Data);
		}

		/// <summary>
		/// Writes a DateTime value into the stream using the fewest number of bytes possible.
		/// Stored Size: 3 bytes to 7 bytes (.Net is 8 bytes)
		/// Notes:
		/// A DateTime containing only a date takes 3 bytes
		/// (except a .NET 2.0 Date with a specified DateTimeKind which will take a minimum
		/// of 5 bytes - no further optimization for this situation felt necessary since it
		/// is unlikely that a DateTimeKind would be specified without hh:mm also)
		/// Date plus hh:mm takes 5 bytes.
		/// Date plus hh:mm:ss takes 6 bytes.
		/// Date plus hh:mm:ss.fff takes 7 bytes.
		/// </summary>
		/// <param name="value">The DateTime value to store. Must not contain sub-millisecond data.</param>
		// Token: 0x0600596C RID: 22892 RVA: 0x001B18D4 File Offset: 0x001AFAD4
		public void WriteOptimized(DateTime value)
		{
			BitVector32 bitVector = default(BitVector32);
			bitVector[SerializationWriter.DateYearMask] = value.Year;
			bitVector[SerializationWriter.DateMonthMask] = value.Month;
			bitVector[SerializationWriter.DateDayMask] = value.Day;
			bool flag = value != value.Date;
			int kind = (int)value.Kind;
			flag |= kind != 0;
			bitVector[SerializationWriter.DateHasTimeOrKindMask] = (flag ? 1 : 0);
			int data = bitVector.Data;
			this.Write((byte)data);
			this.Write((byte)(data >> 8));
			this.Write((byte)(data >> 16));
			if (flag)
			{
				this.encodeTimeSpan(value.TimeOfDay, true, kind);
			}
		}

		/// <summary>
		/// Writes a Decimal value into the stream using the fewest number of bytes possible.
		/// Stored Size: 1 byte to 14 bytes (.Net is 16 bytes)
		/// Restrictions: None
		/// </summary>
		/// <param name="value">The Decimal value to store</param>
		// Token: 0x0600596D RID: 22893 RVA: 0x001B198C File Offset: 0x001AFB8C
		public void WriteOptimized(decimal value)
		{
			int[] array = decimal.GetBits(value);
			byte b = (byte)(array[3] >> 16);
			byte b2 = 0;
			if (b != 0 && !this.preserveDecimalScale && this.optimizeForSize)
			{
				decimal num = decimal.Truncate(value);
				if (num == value)
				{
					array = decimal.GetBits(num);
					b = 0;
				}
			}
			if ((array[3] & -2147483648) != 0)
			{
				b2 |= 1;
			}
			if (b != 0)
			{
				b2 |= 2;
			}
			if (array[0] == 0)
			{
				b2 |= 4;
			}
			else if (array[0] <= 2097151 && array[0] >= 0)
			{
				b2 |= 32;
			}
			if (array[1] == 0)
			{
				b2 |= 8;
			}
			else if (array[1] <= 2097151 && array[1] >= 0)
			{
				b2 |= 64;
			}
			if (array[2] == 0)
			{
				b2 |= 16;
			}
			else if (array[2] <= 2097151 && array[2] >= 0)
			{
				b2 |= 128;
			}
			this.Write(b2);
			if (b != 0)
			{
				this.Write(b);
			}
			if ((b2 & 4) == 0)
			{
				if ((b2 & 32) != 0)
				{
					this.write7bitEncodedSigned32BitValue(array[0]);
				}
				else
				{
					this.Write(array[0]);
				}
			}
			if ((b2 & 8) == 0)
			{
				if ((b2 & 64) != 0)
				{
					this.write7bitEncodedSigned32BitValue(array[1]);
				}
				else
				{
					this.Write(array[1]);
				}
			}
			if ((b2 & 16) == 0)
			{
				if ((b2 & 128) != 0)
				{
					this.write7bitEncodedSigned32BitValue(array[2]);
					return;
				}
				this.Write(array[2]);
			}
		}

		/// <summary>
		/// Write an Int16 value using the fewest number of bytes possible.
		/// </summary>
		/// <remarks>
		/// 0x0000 - 0x007f (0 to 127) takes 1 byte
		/// 0x0080 - 0x03FF (128 to 16,383) takes 2 bytes
		/// ----------------------------------------------------------------
		/// 0x0400 - 0x7FFF (16,384 to 32,767) takes 3 bytes
		/// All negative numbers take 3 bytes
		///
		/// Only call this method if the value is known to be between 0 and 
		/// 16,383 otherwise use Write(Int16 value)
		/// </remarks>
		/// <param name="value">The Int16 to store. Must be between 0 and 16,383 inclusive.</param>
		// Token: 0x0600596E RID: 22894 RVA: 0x001B1AC9 File Offset: 0x001AFCC9
		public void WriteOptimized(short value)
		{
			this.write7bitEncodedSigned32BitValue((int)value);
		}

		/// <summary>
		/// Write an Int32 value using the fewest number of bytes possible.
		/// </summary>
		/// <remarks>
		/// 0x00000000 - 0x0000007f (0 to 127) takes 1 byte
		/// 0x00000080 - 0x000003FF (128 to 16,383) takes 2 bytes
		/// 0x00000400 - 0x001FFFFF (16,384 to 2,097,151) takes 3 bytes
		/// 0x00200000 - 0x0FFFFFFF (2,097,152 to 268,435,455) takes 4 bytes
		/// ----------------------------------------------------------------
		/// 0x10000000 - 0x07FFFFFF (268,435,456 and above) takes 5 bytes
		/// All negative numbers take 5 bytes
		///
		/// Only call this method if the value is known to be between 0 and 
		/// 268,435,455 otherwise use Write(Int32 value)
		/// </remarks>
		/// <param name="value">The Int32 to store. Must be between 0 and 268,435,455 inclusive.</param>
		// Token: 0x0600596F RID: 22895 RVA: 0x001B1AD2 File Offset: 0x001AFCD2
		public void WriteOptimized(int value)
		{
			this.write7bitEncodedSigned32BitValue(value);
		}

		/// <summary>
		/// Write an Int64 value using the fewest number of bytes possible.
		/// </summary>
		/// <remarks>
		/// 0x0000000000000000 - 0x000000000000007f (0 to 127) takes 1 byte
		/// 0x0000000000000080 - 0x00000000000003FF (128 to 16,383) takes 2 bytes
		/// 0x0000000000000400 - 0x00000000001FFFFF (16,384 to 2,097,151) takes 3 bytes
		/// 0x0000000000200000 - 0x000000000FFFFFFF (2,097,152 to 268,435,455) takes 4 bytes
		/// 0x0000000010000000 - 0x00000007FFFFFFFF (268,435,456 to 34,359,738,367) takes 5 bytes
		/// 0x0000000800000000 - 0x000003FFFFFFFFFF (34,359,738,368 to 4,398,046,511,103) takes 6 bytes
		/// 0x0000040000000000 - 0x0001FFFFFFFFFFFF (4,398,046,511,104 to 562,949,953,421,311) takes 7 bytes
		/// 0x0002000000000000 - 0x00FFFFFFFFFFFFFF (562,949,953,421,312 to 72,057,594,037,927,935) takes 8 bytes
		/// ------------------------------------------------------------------
		/// 0x0100000000000000 - 0x7FFFFFFFFFFFFFFF (72,057,594,037,927,936 to 9,223,372,036,854,775,807) takes 9 bytes
		/// 0x7FFFFFFFFFFFFFFF - 0xFFFFFFFFFFFFFFFF (9,223,372,036,854,775,807 and above) takes 10 bytes
		/// All negative numbers take 10 bytes
		///
		/// Only call this method if the value is known to be between 0 and
		/// 72,057,594,037,927,935 otherwise use Write(Int64 value)
		/// </remarks>
		/// <param name="value">The Int64 to store. Must be between 0 and 72,057,594,037,927,935 inclusive.</param>
		// Token: 0x06005970 RID: 22896 RVA: 0x001B1ADB File Offset: 0x001AFCDB
		public void WriteOptimized(long value)
		{
			this.write7bitEncodedSigned64BitValue(value);
		}

		/// <summary>
		/// Writes a string value into the stream using the fewest number of bytes possible.
		/// Stored Size: 1 byte upwards depending on string length
		/// Notes:
		/// Encodes null, Empty, 'Y', 'N', ' ' values as a single byte
		/// Any other single char string is stored as two bytes
		/// All other strings are stored in a string token list:
		///
		/// The TypeCode representing the current string token list is written first (1 byte), 
		/// followed by the string token itself (1-4 bytes)
		///
		/// When the current string list has reached 128 values then a new string list
		/// is generated and that is used for generating future string tokens. This continues
		/// until the maximum number (128) of string lists is in use, after which the string 
		/// lists are used in a round-robin fashion.
		/// By doing this, more lists are created with fewer items which allows a smaller 
		/// token size to be used for more strings.
		///
		/// The first 16,384 strings will use a 1 byte token.
		/// The next 2,097,152 strings will use a 2 byte token. (This should suffice for most uses!)
		/// The next 268,435,456 strings will use a 3 byte token. (My, that is a lot!!)
		/// The next 34,359,738,368 strings will use a 4 byte token. (only shown for completeness!!!)
		/// </summary>
		/// <param name="value">The string to store.</param>
		// Token: 0x06005971 RID: 22897 RVA: 0x001B1AE4 File Offset: 0x001AFCE4
		public void WriteOptimized(string value)
		{
			if (value == null)
			{
				this.writeTypeCode(SerializedType.NullType);
				return;
			}
			if (value.Length == 1)
			{
				char c = value[0];
				if (c == 'Y')
				{
					this.writeTypeCode(SerializedType.YStringType);
					return;
				}
				if (c == 'N')
				{
					this.writeTypeCode(SerializedType.NStringType);
					return;
				}
				if (c == ' ')
				{
					this.writeTypeCode(SerializedType.SingleSpaceType);
					return;
				}
				this.writeTypeCode(SerializedType.SingleCharStringType);
				this.Write(c);
				return;
			}
			else
			{
				if (value.Length == 0)
				{
					this.writeTypeCode(SerializedType.EmptyStringType);
					return;
				}
				int num = this.TokenStrings.Add(value);
				this.Write((byte)(num % 128));
				this.write7bitEncodedSigned32BitValue(num >> 7);
				return;
			}
		}

		/// <summary>
		/// Writes a TimeSpan value into the stream using the fewest number of bytes possible.
		/// Stored Size: 2 bytes to 8 bytes (.Net is 8 bytes)
		/// Notes:
		/// hh:mm (time) are always stored together and take 2 bytes.
		/// If seconds are present then 3 bytes unless (time) is not present in which case 2 bytes
		/// since the seconds are stored in the minutes position.
		/// If milliseconds are present then 4 bytes.
		/// In addition, if days are present they will add 1 to 4 bytes to the above.
		/// </summary>
		/// <param name="value">The TimeSpan value to store. Must not contain sub-millisecond data.</param>
		// Token: 0x06005972 RID: 22898 RVA: 0x001B1B8F File Offset: 0x001AFD8F
		public void WriteOptimized(TimeSpan value)
		{
			this.encodeTimeSpan(value, false, 0);
		}

		/// <summary>
		/// Stores a non-null Type object into the stream.
		/// Stored Size: Depends on the length of the Type's name.
		/// If the type is a System type (mscorlib) then it is stored without assembly name information,
		/// otherwise the Type's AssemblyQualifiedName is used.
		/// </summary>
		/// <param name="value">The Type to store. Must not be null.</param>
		// Token: 0x06005973 RID: 22899 RVA: 0x001B1B9C File Offset: 0x001AFD9C
		public void WriteOptimized(Type value)
		{
			Assembly assembly = value.Assembly;
			if (this.LocalAssemblies.Contains(assembly))
			{
				this.WriteOptimized(value.FullName);
				return;
			}
			this.WriteOptimized(value.AssemblyQualifiedName);
		}

		// Token: 0x06005974 RID: 22900 RVA: 0x001B1BD8 File Offset: 0x001AFDD8
		public void WriteDirect(Type value)
		{
			Assembly assembly = value.Assembly;
			if (this.LocalAssemblies.Contains(assembly))
			{
				base.Write(value.FullName);
				return;
			}
			base.Write(value.AssemblyQualifiedName);
		}

		/// <summary>
		/// Write a UInt16 value using the fewest number of bytes possible.
		/// </summary>
		/// <remarks>
		/// 0x0000 - 0x007f (0 to 127) takes 1 byte
		/// 0x0080 - 0x03FF (128 to 16,383) takes 2 bytes
		/// ----------------------------------------------------------------
		/// 0x0400 - 0xFFFF (16,384 to 65,536) takes 3 bytes
		///
		/// Only call this method if the value is known to  be between 0 and 
		/// 16,383 otherwise use Write(UInt16 value)
		/// </remarks>
		/// <param name="value">The UInt16 to store. Must be between 0 and 16,383 inclusive.</param>
		// Token: 0x06005975 RID: 22901 RVA: 0x001B1C13 File Offset: 0x001AFE13
		[CLSCompliant(false)]
		public void WriteOptimized(ushort value)
		{
			this.write7bitEncodedUnsigned32BitValue((uint)value);
		}

		// Token: 0x06005976 RID: 22902 RVA: 0x001B1C1C File Offset: 0x001AFE1C
		[CLSCompliant(false)]
		public void WriteOptimized(uint value)
		{
			this.write7bitEncodedUnsigned32BitValue(value);
		}

		/// <summary>
		/// Write a UInt64 value using the fewest number of bytes possible.
		/// </summary>
		/// <remarks>
		/// 0x0000000000000000 - 0x000000000000007f (0 to 127) takes 1 byte
		/// 0x0000000000000080 - 0x00000000000003FF (128 to 16,383) takes 2 bytes
		/// 0x0000000000000400 - 0x00000000001FFFFF (16,384 to 2,097,151) takes 3 bytes
		/// 0x0000000000200000 - 0x000000000FFFFFFF (2,097,152 to 268,435,455) takes 4 bytes
		/// 0x0000000010000000 - 0x00000007FFFFFFFF (268,435,456 to 34,359,738,367) takes 5 bytes
		/// 0x0000000800000000 - 0x000003FFFFFFFFFF (34,359,738,368 to 4,398,046,511,103) takes 6 bytes
		/// 0x0000040000000000 - 0x0001FFFFFFFFFFFF (4,398,046,511,104 to 562,949,953,421,311) takes 7 bytes
		/// 0x0002000000000000 - 0x00FFFFFFFFFFFFFF (562,949,953,421,312 to 72,057,594,037,927,935) takes 8 bytes
		/// ------------------------------------------------------------------
		/// 0x0100000000000000 - 0x7FFFFFFFFFFFFFFF (72,057,594,037,927,936 to 9,223,372,036,854,775,807) takes 9 bytes
		/// 0x7FFFFFFFFFFFFFFF - 0xFFFFFFFFFFFFFFFF (9,223,372,036,854,775,807 and above) takes 10 bytes
		///
		/// Only call this method if the value is known to be between 0 and
		/// 72,057,594,037,927,935 otherwise use Write(UInt64 value)
		/// </remarks>
		/// <param name="value">The UInt64 to store. Must be between 0 and 72,057,594,037,927,935 inclusive.</param>
		// Token: 0x06005977 RID: 22903 RVA: 0x001B1C25 File Offset: 0x001AFE25
		[CLSCompliant(false)]
		public void WriteOptimized(ulong value)
		{
			this.write7bitEncodedUnsigned64BitValue(value);
		}

		/// <summary>
		/// Writes a Boolean[] into the stream.
		/// Notes:
		/// A null or empty array will take 1 byte.
		/// Calls WriteOptimized(Boolean[]).
		/// </summary>
		/// <param name="values">The Boolean[] to store.</param>
		// Token: 0x06005978 RID: 22904 RVA: 0x001B1C2E File Offset: 0x001AFE2E
		public void Write(bool[] values)
		{
			this.WriteOptimized(values);
		}

		/// <summary>
		/// Writes a Byte[] into the stream.
		/// Notes:
		/// A null or empty array will take 1 byte.
		/// </summary>
		/// <param name="values">The Byte[] to store.</param>
		// Token: 0x06005979 RID: 22905 RVA: 0x001B1C37 File Offset: 0x001AFE37
		public override void Write(byte[] values)
		{
			if (values == null)
			{
				this.writeTypeCode(SerializedType.NullType);
				return;
			}
			if (values.Length == 0)
			{
				this.writeTypeCode(SerializedType.EmptyTypedArrayType);
				return;
			}
			this.writeTypeCode(SerializedType.NonOptimizedTypedArrayType);
			this.writeArray(values);
		}

		/// <summary>
		/// Writes a Char[] into the stream.
		/// Notes:
		/// A null or empty array will take 1 byte.
		/// </summary>
		/// <param name="values">The Char[] to store.</param>
		// Token: 0x0600597A RID: 22906 RVA: 0x001B1C6A File Offset: 0x001AFE6A
		public override void Write(char[] values)
		{
			if (values == null)
			{
				this.writeTypeCode(SerializedType.NullType);
				return;
			}
			if (values.Length == 0)
			{
				this.writeTypeCode(SerializedType.EmptyTypedArrayType);
				return;
			}
			this.writeTypeCode(SerializedType.NonOptimizedTypedArrayType);
			this.writeArray(values);
		}

		/// <summary>
		/// Writes a DateTime[] into the stream.
		/// Notes:
		/// A null or empty array will take 1 byte.
		/// </summary>
		/// <param name="values">The DateTime[] to store.</param>
		// Token: 0x0600597B RID: 22907 RVA: 0x001B1C9D File Offset: 0x001AFE9D
		public void Write(DateTime[] values)
		{
			if (values == null)
			{
				this.writeTypeCode(SerializedType.NullType);
				return;
			}
			if (values.Length == 0)
			{
				this.writeTypeCode(SerializedType.EmptyTypedArrayType);
				return;
			}
			this.writeArray(values, null);
		}

		/// <summary>
		/// Writes a Decimal[] into the stream.
		/// Notes:
		/// A null or empty array will take 1 byte.
		/// Calls WriteOptimized(Decimal[]).
		/// </summary>
		/// <param name="values">The Decimal[] to store.</param>
		// Token: 0x0600597C RID: 22908 RVA: 0x001B1CC6 File Offset: 0x001AFEC6
		public void Write(decimal[] values)
		{
			this.WriteOptimized(values);
		}

		/// <summary>
		/// Writes a Double[] into the stream.
		/// Notes:
		/// A null or empty array will take 1 byte.
		/// </summary>
		/// <param name="values">The Double[] to store.</param>
		// Token: 0x0600597D RID: 22909 RVA: 0x001B1CCF File Offset: 0x001AFECF
		public void Write(double[] values)
		{
			if (values == null)
			{
				this.writeTypeCode(SerializedType.NullType);
				return;
			}
			if (values.Length == 0)
			{
				this.writeTypeCode(SerializedType.EmptyTypedArrayType);
				return;
			}
			this.writeTypeCode(SerializedType.NonOptimizedTypedArrayType);
			this.writeArray(values);
		}

		/// <summary>
		/// Writes a Single[] into the stream.
		/// Notes:
		/// A null or empty array will take 1 byte.
		/// </summary>
		/// <param name="values">The Single[] to store.</param>
		// Token: 0x0600597E RID: 22910 RVA: 0x001B1D02 File Offset: 0x001AFF02
		public void Write(float[] values)
		{
			if (values == null)
			{
				this.writeTypeCode(SerializedType.NullType);
				return;
			}
			if (values.Length == 0)
			{
				this.writeTypeCode(SerializedType.EmptyTypedArrayType);
				return;
			}
			this.writeTypeCode(SerializedType.NonOptimizedTypedArrayType);
			this.writeArray(values);
		}

		/// <summary>
		/// Writes a Guid[] into the stream.
		/// Notes:
		/// A null or empty array will take 1 byte.
		/// </summary>
		/// <param name="values">The Guid[] to store.</param>
		// Token: 0x0600597F RID: 22911 RVA: 0x001B1D35 File Offset: 0x001AFF35
		public void Write(Guid[] values)
		{
			if (values == null)
			{
				this.writeTypeCode(SerializedType.NullType);
				return;
			}
			if (values.Length == 0)
			{
				this.writeTypeCode(SerializedType.EmptyTypedArrayType);
				return;
			}
			this.writeTypeCode(SerializedType.NonOptimizedTypedArrayType);
			this.writeArray(values);
		}

		/// <summary>
		/// Writes an Int32[] into the stream.
		/// Notes:
		/// A null or empty array will take 1 byte.
		/// </summary>
		/// <param name="values">The Int32[] to store.</param>
		// Token: 0x06005980 RID: 22912 RVA: 0x001B1D68 File Offset: 0x001AFF68
		public void Write(int[] values)
		{
			if (values == null)
			{
				this.writeTypeCode(SerializedType.NullType);
				return;
			}
			if (values.Length == 0)
			{
				this.writeTypeCode(SerializedType.EmptyTypedArrayType);
				return;
			}
			this.writeArray(values, null);
		}

		/// <summary>
		/// Writes an Int64[] into the stream.
		/// Notes:
		/// A null or empty array will take 1 byte.
		/// </summary>
		/// <param name="values">The Int64[] to store.</param>
		// Token: 0x06005981 RID: 22913 RVA: 0x001B1D91 File Offset: 0x001AFF91
		public void Write(long[] values)
		{
			if (values == null)
			{
				this.writeTypeCode(SerializedType.NullType);
				return;
			}
			if (values.Length == 0)
			{
				this.writeTypeCode(SerializedType.EmptyTypedArrayType);
				return;
			}
			this.writeArray(values, null);
		}

		/// <summary>
		/// Writes an object[] into the stream.
		/// Stored Size: 2 bytes upwards depending on data content
		/// Notes:
		/// A null object[] takes 1 byte.
		/// An empty object[] takes 2 bytes.
		/// The contents of the array will be stored optimized.
		/// </summary>
		/// <param name="values">The object[] to store.</param>
		// Token: 0x06005982 RID: 22914 RVA: 0x001B1DBA File Offset: 0x001AFFBA
		public void Write(object[] values)
		{
			if (values == null)
			{
				this.writeTypeCode(SerializedType.NullType);
				return;
			}
			if (values.Length == 0)
			{
				this.writeTypeCode(SerializedType.EmptyObjectArrayType);
				return;
			}
			this.writeTypeCode(SerializedType.ObjectArrayType);
			this.writeObjectArray(values);
		}

		/// <summary>
		/// Writes an SByte[] into the stream.
		/// Notes:
		/// A null or empty array will take 1 byte.
		/// </summary>
		/// <param name="values">The SByte[] to store.</param>
		// Token: 0x06005983 RID: 22915 RVA: 0x001B1DED File Offset: 0x001AFFED
		[CLSCompliant(false)]
		public void Write(sbyte[] values)
		{
			if (values == null)
			{
				this.writeTypeCode(SerializedType.NullType);
				return;
			}
			if (values.Length == 0)
			{
				this.writeTypeCode(SerializedType.EmptyTypedArrayType);
				return;
			}
			this.writeTypeCode(SerializedType.NonOptimizedTypedArrayType);
			this.writeArray(values);
		}

		/// <summary>
		/// Writes an Int16[]or a null into the stream.
		/// Notes:
		/// A null or empty array will take 1 byte.
		/// Calls WriteOptimized(decimal[]).
		/// </summary>
		/// <param name="values">The Int16[] to store.</param>
		// Token: 0x06005984 RID: 22916 RVA: 0x001B1E20 File Offset: 0x001B0020
		public void Write(short[] values)
		{
			if (values == null)
			{
				this.writeTypeCode(SerializedType.NullType);
				return;
			}
			if (values.Length == 0)
			{
				this.writeTypeCode(SerializedType.EmptyTypedArrayType);
				return;
			}
			this.writeTypeCode(SerializedType.NonOptimizedTypedArrayType);
			this.writeArray(values);
		}

		/// <summary>
		/// Writes a TimeSpan[] into the stream.
		/// Notes:
		/// A null or empty array will take 1 byte.
		/// </summary>
		/// <param name="values">The TimeSpan[] to store.</param>
		// Token: 0x06005985 RID: 22917 RVA: 0x001B1E53 File Offset: 0x001B0053
		public void Write(TimeSpan[] values)
		{
			if (values == null)
			{
				this.writeTypeCode(SerializedType.NullType);
				return;
			}
			if (values.Length == 0)
			{
				this.writeTypeCode(SerializedType.EmptyTypedArrayType);
				return;
			}
			this.writeArray(values, null);
		}

		/// <summary>
		/// Writes a UInt32[] into the stream.
		/// Notes:
		/// A null or empty array will take 1 byte.
		/// </summary>
		/// <param name="values">The UInt32[] to store.</param>
		// Token: 0x06005986 RID: 22918 RVA: 0x001B1E7C File Offset: 0x001B007C
		[CLSCompliant(false)]
		public void Write(uint[] values)
		{
			if (values == null)
			{
				this.writeTypeCode(SerializedType.NullType);
				return;
			}
			if (values.Length == 0)
			{
				this.writeTypeCode(SerializedType.EmptyTypedArrayType);
				return;
			}
			this.writeArray(values, null);
		}

		/// <summary>
		/// Writes a UInt64[] into the stream.
		/// Notes:
		/// A null or empty array will take 1 byte.
		/// </summary>
		/// <param name="values">The UInt64[] to store.</param>
		// Token: 0x06005987 RID: 22919 RVA: 0x001B1EA5 File Offset: 0x001B00A5
		[CLSCompliant(false)]
		public void Write(ulong[] values)
		{
			if (values == null)
			{
				this.writeTypeCode(SerializedType.NullType);
				return;
			}
			if (values.Length == 0)
			{
				this.writeTypeCode(SerializedType.EmptyTypedArrayType);
				return;
			}
			this.writeArray(values, null);
		}

		/// <summary>
		/// Writes a UInt16[] into the stream.
		/// Notes:
		/// A null or empty array will take 1 byte.
		/// </summary>
		/// <param name="values">The UInt16[] to store.</param>
		// Token: 0x06005988 RID: 22920 RVA: 0x001B1ECE File Offset: 0x001B00CE
		[CLSCompliant(false)]
		public void Write(ushort[] values)
		{
			if (values == null)
			{
				this.writeTypeCode(SerializedType.NullType);
				return;
			}
			if (values.Length == 0)
			{
				this.writeTypeCode(SerializedType.EmptyTypedArrayType);
				return;
			}
			this.writeTypeCode(SerializedType.NonOptimizedTypedArrayType);
			this.writeArray(values);
		}

		/// <summary>
		/// Writes an optimized Boolean[] into the stream using the fewest possible bytes.
		/// Notes:
		/// A null or empty array will take 1 byte.
		/// Stored as a BitArray.
		/// </summary>
		/// <param name="values">The Boolean[] to store.</param>
		// Token: 0x06005989 RID: 22921 RVA: 0x001B1F01 File Offset: 0x001B0101
		public void WriteOptimized(bool[] values)
		{
			if (values == null)
			{
				this.writeTypeCode(SerializedType.NullType);
				return;
			}
			if (values.Length == 0)
			{
				this.writeTypeCode(SerializedType.EmptyTypedArrayType);
				return;
			}
			this.writeTypeCode(SerializedType.FullyOptimizedTypedArrayType);
			this.writeArray(values);
		}

		/// <summary>
		/// Writes a DateTime[] into the stream using the fewest possible bytes.
		/// Notes:
		/// A null or empty array will take 1 byte.
		/// </summary>
		/// <param name="values">The DateTime[] to store.</param>
		// Token: 0x0600598A RID: 22922 RVA: 0x001B1F34 File Offset: 0x001B0134
		public void WriteOptimized(DateTime[] values)
		{
			if (values == null)
			{
				this.writeTypeCode(SerializedType.NullType);
				return;
			}
			if (values.Length == 0)
			{
				this.writeTypeCode(SerializedType.EmptyTypedArrayType);
				return;
			}
			BitArray bitArray = null;
			int num = 0;
			int num2 = 1 + (int)((float)values.Length * (this.optimizeForSize ? 0.8f : 0.6f));
			int num3 = 0;
			while (num3 < values.Length && num < num2)
			{
				if (values[num3].Ticks % 10000L != 0L)
				{
					num++;
				}
				else
				{
					if (bitArray == null)
					{
						bitArray = new BitArray(values.Length);
					}
					bitArray[num3] = true;
				}
				num3++;
			}
			if (num == 0)
			{
				bitArray = SerializationWriter.FullyOptimizableTypedArray;
			}
			else if (num >= num2)
			{
				bitArray = null;
			}
			this.writeArray(values, bitArray);
		}

		/// <summary>
		/// Writes a Decimal[] into the stream using the fewest possible bytes.
		/// Notes:
		/// A null or empty array will take 1 byte.
		/// </summary>
		/// <param name="values">The Decimal[] to store.</param>
		// Token: 0x0600598B RID: 22923 RVA: 0x001B1FDB File Offset: 0x001B01DB
		public void WriteOptimized(decimal[] values)
		{
			if (values == null)
			{
				this.writeTypeCode(SerializedType.NullType);
				return;
			}
			if (values.Length == 0)
			{
				this.writeTypeCode(SerializedType.EmptyTypedArrayType);
				return;
			}
			this.writeTypeCode(SerializedType.FullyOptimizedTypedArrayType);
			this.writeArray(values);
		}

		/// <summary>
		/// Writes a not-null object[] into the stream using the fewest number of bytes possible.
		/// Stored Size: 2 bytes upwards depending on data content
		/// Notes:
		/// An empty object[] takes 1 byte.
		/// The contents of the array will be stored optimized.
		/// </summary>
		/// <param name="values">The object[] to store. Must not be null.</param>
		// Token: 0x0600598C RID: 22924 RVA: 0x001B200E File Offset: 0x001B020E
		public void WriteOptimized(object[] values)
		{
			this.writeObjectArray(values);
		}

		/// <summary>
		/// Writes a pair of object[] arrays into the stream using the fewest number of bytes possible.
		/// The arrays must not be null and must have the same length
		/// The first array's values are written optimized
		/// The second array's values are compared against the first and, where identical, will be stored
		/// using a single byte.
		/// Useful for storing entity data where there is a before-change and after-change set of value pairs
		/// and, typically, only a few of the values will have changed.
		/// </summary>
		/// <param name="values1">The first object[] value which must not be null and must have the same length as values2</param>
		/// <param name="values2">The second object[] value which must not be null and must have the same length as values1</param>
		// Token: 0x0600598D RID: 22925 RVA: 0x001B2018 File Offset: 0x001B0218
		public void WriteOptimized(object[] values1, object[] values2)
		{
			this.writeObjectArray(values1);
			int num = values2.Length - 1;
			for (int i = 0; i < values2.Length; i++)
			{
				object obj = values2[i];
				if ((obj == null) ? (values1[i] == null) : obj.Equals(values1[i]))
				{
					int num2 = 0;
					while (i < num && ((values2[i + 1] == null) ? (values1[i + 1] == null) : values2[i + 1].Equals(values1[i + 1])))
					{
						num2++;
						i++;
					}
					if (num2 == 0)
					{
						this.writeTypeCode(SerializedType.DuplicateValueType);
					}
					else
					{
						this.writeTypeCode(SerializedType.DuplicateValueSequenceType);
						this.write7bitEncodedSigned32BitValue(num2);
					}
				}
				else if (obj == null)
				{
					int num3 = 0;
					while (i < num && values2[i + 1] == null)
					{
						num3++;
						i++;
					}
					if (num3 == 0)
					{
						this.writeTypeCode(SerializedType.NullType);
					}
					else
					{
						this.writeTypeCode(SerializedType.NullSequenceType);
						this.write7bitEncodedSigned32BitValue(num3);
					}
				}
				else if (obj == DBNull.Value)
				{
					int num4 = 0;
					while (i < num && values2[i + 1] == DBNull.Value)
					{
						num4++;
						i++;
					}
					if (num4 == 0)
					{
						this.writeTypeCode(SerializedType.DBNullType);
					}
					else
					{
						this.writeTypeCode(SerializedType.DBNullSequenceType);
						this.write7bitEncodedSigned32BitValue(num4);
					}
				}
				else
				{
					this.WriteObject(obj);
				}
			}
		}

		/// <summary>
		/// Writes an Int16[] into the stream using the fewest possible bytes.
		/// Notes:
		/// A null or empty array will take 1 byte.
		/// </summary>
		/// <param name="values">The Int16[] to store.</param>
		// Token: 0x0600598E RID: 22926 RVA: 0x001B2154 File Offset: 0x001B0354
		public void WriteOptimized(short[] values)
		{
			if (values == null)
			{
				this.writeTypeCode(SerializedType.NullType);
				return;
			}
			if (values.Length == 0)
			{
				this.writeTypeCode(SerializedType.EmptyTypedArrayType);
				return;
			}
			BitArray bitArray = null;
			int num = 0;
			int num2 = 1 + (int)((float)values.Length * (this.optimizeForSize ? 0.8f : 0.6f));
			int num3 = 0;
			while (num3 < values.Length && num < num2)
			{
				if (values[num3] < 0 || values[num3] > 127)
				{
					num++;
				}
				else
				{
					if (bitArray == null)
					{
						bitArray = new BitArray(values.Length);
					}
					bitArray[num3] = true;
				}
				num3++;
			}
			if (num == 0)
			{
				bitArray = SerializationWriter.FullyOptimizableTypedArray;
			}
			else if (num >= num2)
			{
				bitArray = null;
			}
			this.writeArray(values, bitArray);
		}

		/// <summary>
		/// Writes an Int32[] into the stream using the fewest possible bytes.
		/// Notes:
		/// A null or empty array will take 1 byte.
		/// </summary>
		/// <param name="values">The Int32[] to store.</param>
		// Token: 0x0600598F RID: 22927 RVA: 0x001B21F4 File Offset: 0x001B03F4
		public void WriteOptimized(int[] values)
		{
			if (values == null)
			{
				this.writeTypeCode(SerializedType.NullType);
				return;
			}
			if (values.Length == 0)
			{
				this.writeTypeCode(SerializedType.EmptyTypedArrayType);
				return;
			}
			BitArray bitArray = null;
			int num = 0;
			int num2 = 1 + (int)((float)values.Length * (this.optimizeForSize ? 0.8f : 0.6f));
			int num3 = 0;
			while (num3 < values.Length && num < num2)
			{
				if (values[num3] < 0 || values[num3] > 2097151)
				{
					num++;
				}
				else
				{
					if (bitArray == null)
					{
						bitArray = new BitArray(values.Length);
					}
					bitArray[num3] = true;
				}
				num3++;
			}
			if (num == 0)
			{
				bitArray = SerializationWriter.FullyOptimizableTypedArray;
			}
			else if (num >= num2)
			{
				bitArray = null;
			}
			this.writeArray(values, bitArray);
		}

		/// <summary>
		/// Writes an Int64[] into the stream using the fewest possible bytes.
		/// Notes:
		/// A null or empty array will take 1 byte.
		/// </summary>
		/// <param name="values">The Int64[] to store.</param>
		// Token: 0x06005990 RID: 22928 RVA: 0x001B2298 File Offset: 0x001B0498
		public void WriteOptimized(long[] values)
		{
			if (values == null)
			{
				this.writeTypeCode(SerializedType.NullType);
				return;
			}
			if (values.Length == 0)
			{
				this.writeTypeCode(SerializedType.EmptyTypedArrayType);
				return;
			}
			BitArray bitArray = null;
			int num = 0;
			int num2 = 1 + (int)((float)values.Length * (this.optimizeForSize ? 0.8f : 0.6f));
			int num3 = 0;
			while (num3 < values.Length && num < num2)
			{
				if (values[num3] < 0L || values[num3] > 562949953421311L)
				{
					num++;
				}
				else
				{
					if (bitArray == null)
					{
						bitArray = new BitArray(values.Length);
					}
					bitArray[num3] = true;
				}
				num3++;
			}
			if (num == 0)
			{
				bitArray = SerializationWriter.FullyOptimizableTypedArray;
			}
			else if (num >= num2)
			{
				bitArray = null;
			}
			this.writeArray(values, bitArray);
		}

		/// <summary>
		/// Writes a TimeSpan[] into the stream using the fewest possible bytes.
		/// Notes:
		/// A null or empty array will take 1 byte.
		/// </summary>
		/// <param name="values">The TimeSpan[] to store.</param>
		// Token: 0x06005991 RID: 22929 RVA: 0x001B2340 File Offset: 0x001B0540
		public void WriteOptimized(TimeSpan[] values)
		{
			if (values == null)
			{
				this.writeTypeCode(SerializedType.NullType);
				return;
			}
			if (values.Length == 0)
			{
				this.writeTypeCode(SerializedType.EmptyTypedArrayType);
				return;
			}
			BitArray bitArray = null;
			int num = 0;
			int num2 = 1 + (int)((float)values.Length * (this.optimizeForSize ? 0.8f : 0.6f));
			int num3 = 0;
			while (num3 < values.Length && num < num2)
			{
				if (values[num3].Ticks % 10000L != 0L)
				{
					num++;
				}
				else
				{
					if (bitArray == null)
					{
						bitArray = new BitArray(values.Length);
					}
					bitArray[num3] = true;
				}
				num3++;
			}
			if (num == 0)
			{
				bitArray = SerializationWriter.FullyOptimizableTypedArray;
			}
			else if (num >= num2)
			{
				bitArray = null;
			}
			this.writeArray(values, bitArray);
		}

		/// <summary>
		/// Writes a UInt16[] into the stream using the fewest possible bytes.
		/// Notes:
		/// A null or empty array will take 1 byte.
		/// </summary>
		/// <param name="values">The UInt16[] to store.</param>
		// Token: 0x06005992 RID: 22930 RVA: 0x001B23E8 File Offset: 0x001B05E8
		[CLSCompliant(false)]
		public void WriteOptimized(ushort[] values)
		{
			if (values == null)
			{
				this.writeTypeCode(SerializedType.NullType);
				return;
			}
			if (values.Length == 0)
			{
				this.writeTypeCode(SerializedType.EmptyTypedArrayType);
				return;
			}
			BitArray bitArray = null;
			int num = 0;
			int num2 = 1 + (int)((float)values.Length * (this.optimizeForSize ? 0.8f : 0.6f));
			int num3 = 0;
			while (num3 < values.Length && num < num2)
			{
				if (values[num3] > 127)
				{
					num++;
				}
				else
				{
					if (bitArray == null)
					{
						bitArray = new BitArray(values.Length);
					}
					bitArray[num3] = true;
				}
				num3++;
			}
			if (num == 0)
			{
				bitArray = SerializationWriter.FullyOptimizableTypedArray;
			}
			else if (num >= num2)
			{
				bitArray = null;
			}
			this.writeArray(values, bitArray);
		}

		/// <summary>
		/// Writes a UInt32[] into the stream using the fewest possible bytes.
		/// Notes:
		/// A null or empty array will take 1 byte.
		/// </summary>
		/// <param name="values">The UInt32[] to store.</param>
		// Token: 0x06005993 RID: 22931 RVA: 0x001B2484 File Offset: 0x001B0684
		[CLSCompliant(false)]
		public void WriteOptimized(uint[] values)
		{
			if (values == null)
			{
				this.writeTypeCode(SerializedType.NullType);
				return;
			}
			if (values.Length == 0)
			{
				this.writeTypeCode(SerializedType.EmptyTypedArrayType);
				return;
			}
			BitArray bitArray = null;
			int num = 0;
			int num2 = 1 + (int)((float)values.Length * (this.optimizeForSize ? 0.8f : 0.6f));
			int num3 = 0;
			while (num3 < values.Length && num < num2)
			{
				if (values[num3] > 2097151U)
				{
					num++;
				}
				else
				{
					if (bitArray == null)
					{
						bitArray = new BitArray(values.Length);
					}
					bitArray[num3] = true;
				}
				num3++;
			}
			if (num == 0)
			{
				bitArray = SerializationWriter.FullyOptimizableTypedArray;
			}
			else if (num >= num2)
			{
				bitArray = null;
			}
			this.writeArray(values, bitArray);
		}

		/// <summary>
		/// Writes a UInt64[] into the stream using the fewest possible bytes.
		/// Notes:
		/// A null or empty array will take 1 byte.
		/// </summary>
		/// <param name="values">The UInt64[] to store.</param>
		// Token: 0x06005994 RID: 22932 RVA: 0x001B2520 File Offset: 0x001B0720
		[CLSCompliant(false)]
		public void WriteOptimized(ulong[] values)
		{
			if (values == null)
			{
				this.writeTypeCode(SerializedType.NullType);
				return;
			}
			if (values.Length == 0)
			{
				this.writeTypeCode(SerializedType.EmptyTypedArrayType);
				return;
			}
			BitArray bitArray = null;
			int num = 0;
			int num2 = 1 + (int)((float)values.Length * (this.optimizeForSize ? 0.8f : 0.6f));
			int num3 = 0;
			while (num3 < values.Length && num < num2)
			{
				if (values[num3] > 562949953421311UL)
				{
					num++;
				}
				else
				{
					if (bitArray == null)
					{
						bitArray = new BitArray(values.Length);
					}
					bitArray[num3] = true;
				}
				num3++;
			}
			if (num == 0)
			{
				bitArray = SerializationWriter.FullyOptimizableTypedArray;
			}
			else if (num >= num2)
			{
				bitArray = null;
			}
			this.writeArray(values, bitArray);
		}

		// Token: 0x06005995 RID: 22933 RVA: 0x001B25C0 File Offset: 0x001B07C0
		public void WriteOptimized(Version Value)
		{
			this.write7bitEncodedUnsigned32BitValue((uint)Value.Major);
			this.write7bitEncodedUnsigned32BitValue((uint)Value.Minor);
			this.write7bitEncodedUnsigned32BitValue((uint)Value.Build);
			this.write7bitEncodedUnsigned32BitValue((uint)Value.Revision);
		}

		/// <summary>
		/// Writes a Nullable type into the stream.
		/// Synonym for WriteObject().
		/// </summary>
		/// <param name="value">The Nullable value to store.</param>
		// Token: 0x06005996 RID: 22934 RVA: 0x001B25F2 File Offset: 0x001B07F2
		public void WriteNullable(ValueType value)
		{
			this.WriteObject(value);
		}

		/// <summary>
		/// Writes a non-null generic Dictionary into the stream.
		/// </summary>
		/// <remarks>
		/// The key and value types themselves are not stored - they must be 
		/// supplied at deserialization time.
		/// <para />
		/// An array of keys is stored followed by an array of values.
		/// </remarks>
		/// <typeparam name="K">The key Type.</typeparam>
		/// <typeparam name="V">The value Type.</typeparam>
		/// <param name="value">The generic dictionary.</param>
		// Token: 0x06005997 RID: 22935 RVA: 0x001B25FC File Offset: 0x001B07FC
		public void Write<K, V>(Dictionary<K, V> value)
		{
			if (value == null)
			{
				this.Write(-1);
				return;
			}
			this.Write(value.Keys.Count);
			foreach (KeyValuePair<K, V> keyValuePair in value)
			{
				this.WriteObject(keyValuePair.Key);
				this.WriteObject(keyValuePair.Value);
			}
		}

		// Token: 0x06005998 RID: 22936 RVA: 0x001B2684 File Offset: 0x001B0884
		public void Write(IDictionary Value)
		{
			if (Value == null)
			{
				this.Write(-1);
				return;
			}
			this.Write(Value.Keys.Count);
			foreach (object obj in Value)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				this.WriteObject(dictionaryEntry.Key);
				this.WriteObject(dictionaryEntry.Value);
			}
		}

		/// <summary>
		/// Writes a non-null generic List into the stream.
		/// </summary>
		/// <remarks>
		/// The list type itself is not stored - it must be supplied
		/// at deserialization time.
		/// <para />
		/// The list contents are stored as an array.
		/// </remarks>
		/// <typeparam name="T">The list Type.</typeparam>
		/// <param name="value">The generic List.</param>
		// Token: 0x06005999 RID: 22937 RVA: 0x001B2708 File Offset: 0x001B0908
		public void Write<T>(List<T> value)
		{
			if (value == null)
			{
				this.Write(-1);
				return;
			}
			this.Write(value.Count);
			for (int i = 0; i < value.Count; i++)
			{
				this.WriteObject(value[i]);
			}
		}

		// Token: 0x0600599A RID: 22938 RVA: 0x001B2750 File Offset: 0x001B0950
		public void Write(List<string> value)
		{
			if (value == null)
			{
				this.Write(-1);
				return;
			}
			this.Write(value.Count);
			for (int i = 0; i < value.Count; i++)
			{
				this.Write(value[i]);
			}
		}

		// Token: 0x0600599B RID: 22939 RVA: 0x001B2794 File Offset: 0x001B0994
		public void Write(IList Value)
		{
			if (Value == null)
			{
				this.Write(-1);
				return;
			}
			this.Write(Value.Count);
			foreach (object obj in Value)
			{
				this.WriteObject(obj);
			}
		}

		// Token: 0x0600599C RID: 22940 RVA: 0x001B27FC File Offset: 0x001B09FC
		public void Write(Location2D Value)
		{
			if (Value == null)
			{
				this.Write(-1);
				return;
			}
			this.WriteOptimized(Value.X);
			this.WriteOptimized(Value.Y);
		}

		// Token: 0x0600599D RID: 22941 RVA: 0x001B2828 File Offset: 0x001B0A28
		public void Write(List<Location2D> Value)
		{
			if (Value == null)
			{
				this.Write(-1);
				return;
			}
			this.Write(Value.Count);
			foreach (Location2D location2D in Value)
			{
				this.Write(location2D);
			}
		}

		// Token: 0x0600599E RID: 22942 RVA: 0x001B2890 File Offset: 0x001B0A90
		public void Write(Cell Value)
		{
			string text;
			if (Value == null)
			{
				text = null;
			}
			else
			{
				Zone parentZone = Value.ParentZone;
				text = ((parentZone != null) ? parentZone.ZoneID : null);
			}
			string text2 = text;
			this.WriteOptimized(text2);
			if (text2 != null)
			{
				this.WriteOptimized(Value.X);
				this.WriteOptimized(Value.Y);
			}
		}

		/// <remark>
		/// Serialization of event registries are delayed until the finalization step,
		/// ensuring all external event bindings can reference their targets correctly.
		/// </remark>
		// Token: 0x0600599F RID: 22943 RVA: 0x001B28D8 File Offset: 0x001B0AD8
		public void Write(EventRegistry Registry)
		{
			if (Registry == null || !Registry.Serialize)
			{
				this.write7bitEncodedSigned32BitValue(0);
				return;
			}
			this.write7bitEncodedSigned32BitValue(this.EventRegistries.Count + 1);
			this.EventRegistries.Add(Registry);
		}

		/// <remark>
		/// Serialization of object references are delayed until the finalization step.
		/// </remark>
		// Token: 0x060059A0 RID: 22944 RVA: 0x001B290C File Offset: 0x001B0B0C
		public void Write(GameObjectReference Reference)
		{
			if (Reference == null)
			{
				this.write7bitEncodedSigned32BitValue(0);
				return;
			}
			this.write7bitEncodedSigned32BitValue(this.GameObjectReferences.Count + 1);
			this.GameObjectReferences.Add(Reference);
		}

		// Token: 0x060059A1 RID: 22945 RVA: 0x001B2938 File Offset: 0x001B0B38
		public void Write(IComposite Value)
		{
			if (Value == null)
			{
				this.writeTypeCode(SerializedType.NullType);
				return;
			}
			this.SerializeComposite(Value);
		}

		// Token: 0x060059A2 RID: 22946 RVA: 0x001B2950 File Offset: 0x001B0B50
		private void SerializeComposite(IComposite Value)
		{
			bool wantFieldReflection = Value.WantFieldReflection;
			this.writeTypeCode(wantFieldReflection ? SerializedType.ICompositeFieldType : SerializedType.ICompositeType);
			Type type = Value.GetType();
			using (this.StartBlock())
			{
				this.WriteTokenized(type);
				if (wantFieldReflection)
				{
					this.WriteTypeFields(Value, type, BindingFlags.Instance | BindingFlags.Public, FieldAttributes.Static | FieldAttributes.Literal | FieldAttributes.NotSerialized);
				}
				Value.Write(this);
			}
		}

		// Token: 0x060059A3 RID: 22947 RVA: 0x001B29C4 File Offset: 0x001B0BC4
		public void WriteComposite<T>(T Value) where T : IComposite, new()
		{
			if (Value == null)
			{
				this.writeTypeCode(SerializedType.NullType);
				return;
			}
			bool wantFieldReflection = Value.WantFieldReflection;
			this.writeTypeCode(wantFieldReflection ? SerializedType.ICompositeFieldType : SerializedType.ICompositeType);
			using (this.StartBlock())
			{
				if (wantFieldReflection)
				{
					this.WriteFields<T>(Value, BindingFlags.Instance | BindingFlags.Public, FieldAttributes.Static | FieldAttributes.Literal | FieldAttributes.NotSerialized);
				}
				Value.Write(this);
			}
		}

		// Token: 0x060059A4 RID: 22948 RVA: 0x001B2A4C File Offset: 0x001B0C4C
		public void WriteComposite<T>(List<T> Value) where T : IComposite
		{
			if (Value == null)
			{
				this.WriteOptimized(0);
				return;
			}
			int count = Value.Count;
			this.WriteOptimized(count + 1);
			for (int i = 0; i < count; i++)
			{
				this.Write(Value[i]);
			}
		}

		/// <summary>
		/// Writes a null or a typed array into the stream.
		/// </summary>
		/// <param name="values">The array to store.</param>
		// Token: 0x060059A5 RID: 22949 RVA: 0x001B2A92 File Offset: 0x001B0C92
		public void WriteTypedArray(Array values)
		{
			if (values == null)
			{
				this.writeTypeCode(SerializedType.NullType);
				return;
			}
			this.writeTypedArray(values, true);
		}

		/// <summary>
		/// Writes the contents of the string and object token tables into the stream.
		/// Also write the starting offset into the first 4 bytes of the stream.
		/// Notes:
		/// Called automatically by ToArray().
		/// Can be used to ensure that the complete graph is written before using an
		/// alternate technique of extracting a Byte[] such as using compression on
		/// the underlying stream.
		/// </summary>
		/// <returns>The length of the string and object tables.</returns>
		// Token: 0x060059A6 RID: 22950 RVA: 0x001B2AAC File Offset: 0x001B0CAC
		public int AppendTokenTables()
		{
			this.GameObjectReferencePosition = this.OutStream.Position;
			int num = this.GameObjectReferences.Count;
			this.write7bitEncodedSigned32BitValue(num);
			for (int i = 0; i < num; i++)
			{
				this.GameObjectReferences[i].Write(this);
			}
			this.ObjectPosition = this.OutStream.Position;
			num = this.TokenObjects.Count;
			this.write7bitEncodedSigned32BitValue(num);
			for (int j = 0; j < num; j++)
			{
				this.WriteObject(this.TokenObjects[j]);
			}
			this.TokenizedPosition = this.OutStream.Position;
			num = this.Tokenized.Count;
			this.write7bitEncodedSigned32BitValue(num);
			for (int k = 0; k < num; k++)
			{
				this.Write(this.Tokenized[k]);
				this.Tokenized[k].Token = 0;
			}
			this.TypePosition = this.OutStream.Position;
			num = this.TokenTypes.Count;
			this.write7bitEncodedSigned32BitValue(num);
			for (int l = 0; l < num; l++)
			{
				this.WriteDirect(this.TokenTypes[l]);
			}
			this.StringPosition = this.OutStream.Position;
			num = this.TokenStrings.Count;
			this.write7bitEncodedSigned32BitValue(num);
			for (int m = 0; m < num; m++)
			{
				base.Write(this.TokenStrings[m]);
			}
			SerializationWriter.formatter = null;
			return (int)(this.OutStream.Position - this.ObjectPosition);
		}

		// Token: 0x060059A7 RID: 22951 RVA: 0x001B2C38 File Offset: 0x001B0E38
		public void FinalizeWrite()
		{
			this.WriteGameObjects();
			this.WriteEventRegistries();
			this.AppendTokenTables();
			this.WriteHeader();
		}

		// Token: 0x060059A8 RID: 22952 RVA: 0x001B2C54 File Offset: 0x001B0E54
		public void ReserveHeader()
		{
			for (int i = 0; i < 68; i++)
			{
				this.OutStream.WriteByte(0);
			}
		}

		// Token: 0x060059A9 RID: 22953 RVA: 0x001B2C7C File Offset: 0x001B0E7C
		public void WriteHeader()
		{
			long position = this.OutStream.Position;
			this.OutStream.Position = 0L;
			this.Write(this.FileVersion);
			this.Write(this.GameObjectPosition);
			this.Write(this.GameObjectReferencePosition);
			this.Write(this.EventRegistryPosition);
			this.Write(this.TokenizedPosition);
			this.Write(this.ObjectPosition);
			this.Write(this.TypePosition);
			this.Write(this.StringPosition);
			this.Write(this.TokenGameObjects.Count);
			this.Write(this.EventRegistries.Count);
			this.OutStream.Position = position;
		}

		/// <summary>
		/// Returns a byte[] containing all of the serialized data.
		///
		/// The current implementation has the data in 3 sections:
		/// 1) A 4 byte Int32 giving the offset to the 3rd section.
		/// 2) The main serialized data.
		/// 3) The serialized string tokenization lists and object
		///    tokenization lists.
		///
		/// Only call this method once all of the data has been serialized.
		///
		/// This method appends all of the tokenized data (string and object)
		/// to the end of the stream and ensures that the first four bytes
		/// reflect the offset of the tokenized data so that it can be
		/// deserialized first.
		/// This is the reason for requiring a rewindable stream.
		///
		/// Future implementations may also allow the serialized data to be
		/// accessed via 2 byte[] arrays. This would remove the requirement
		/// for a rewindable stream opening the possibility of streaming the
		/// serialized data directly over the network allowing simultaneous
		/// of partially simultaneous deserialization.
		/// </summary>
		/// <returns>A byte[] containing all serialized data.</returns>
		// Token: 0x060059AA RID: 22954 RVA: 0x001B2D30 File Offset: 0x001B0F30
		public byte[] ToArray()
		{
			this.AppendTokenTables();
			return this.Stream.ToArray();
		}

		/// <summary>
		/// Writes a byte[] directly into the stream.
		/// The size of the array is not stored so only use this method when
		/// the number of bytes will be known at deserialization time.
		///
		/// A null value will throw an exception
		/// </summary>
		/// <param name="value">The byte[] to store. Must not be null.</param>
		// Token: 0x060059AB RID: 22955 RVA: 0x001B2D44 File Offset: 0x001B0F44
		public void WriteBytesDirect(byte[] value)
		{
			base.Write(value);
		}

		/// <summary>
		/// Writes a non-null string directly to the stream without tokenization.
		/// </summary>
		/// <param name="value">The string to store. Must not be null.</param>
		// Token: 0x060059AC RID: 22956 RVA: 0x001B2D4D File Offset: 0x001B0F4D
		public void WriteStringDirect(string value)
		{
			base.Write(value);
		}

		// Token: 0x060059AD RID: 22957 RVA: 0x001B2D56 File Offset: 0x001B0F56
		public void WriteFields<T>(T Value, BindingFlags Flags = BindingFlags.Instance | BindingFlags.Public, FieldAttributes Mask = FieldAttributes.Static | FieldAttributes.Literal | FieldAttributes.NotSerialized)
		{
			this.WriteTypeFields(Value, typeof(T), Flags, Mask);
		}

		// Token: 0x060059AE RID: 22958 RVA: 0x001B2D70 File Offset: 0x001B0F70
		public void WriteObjectFields(object Value, BindingFlags Flags = BindingFlags.Instance | BindingFlags.Public, FieldAttributes Mask = FieldAttributes.Static | FieldAttributes.Literal | FieldAttributes.NotSerialized)
		{
			this.WriteTypeFields(Value, Value.GetType(), Flags, Mask);
		}

		// Token: 0x060059AF RID: 22959 RVA: 0x001B2D84 File Offset: 0x001B0F84
		public void WriteTypeFields(object Value, Type Type, BindingFlags Flags = BindingFlags.Instance | BindingFlags.Public, FieldAttributes Mask = FieldAttributes.Static | FieldAttributes.Literal | FieldAttributes.NotSerialized)
		{
			foreach (FieldInfo fieldInfo in (Flags == (BindingFlags.Instance | BindingFlags.Public)) ? Type.GetCachedFields() : Type.GetFields(Flags))
			{
				if ((fieldInfo.Attributes & Mask) == FieldAttributes.PrivateScope)
				{
					this.WriteObject(fieldInfo.GetValue(Value));
				}
			}
		}

		// Token: 0x060059B0 RID: 22960 RVA: 0x001B2DD0 File Offset: 0x001B0FD0
		public void WriteNamedFields(object Value, Type Type, BindingFlags Flags = BindingFlags.Instance | BindingFlags.Public, FieldAttributes Mask = FieldAttributes.Static | FieldAttributes.Literal | FieldAttributes.NotSerialized)
		{
			FieldInfo[] array = ((Flags == (BindingFlags.Instance | BindingFlags.Public)) ? Type.GetCachedFields() : Type.GetFields(Flags));
			int num = array.Length;
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				if ((array[i].Attributes & Mask) == FieldAttributes.PrivateScope)
				{
					num2++;
				}
			}
			this.WriteOptimized(num2);
			int num3 = 0;
			while (num3 < num && num2 > 0)
			{
				FieldInfo fieldInfo = array[num3];
				if ((fieldInfo.Attributes & Mask) == FieldAttributes.PrivateScope)
				{
					this.WriteOptimized(fieldInfo.Name);
					this.WriteObject(fieldInfo.GetValue(Value));
					num2--;
				}
				num3++;
			}
		}

		// Token: 0x060059B1 RID: 22961 RVA: 0x001B2E64 File Offset: 0x001B1064
		public void WriteGameObjectList(List<GameObject> List)
		{
			this.Write(List.Count);
			for (int i = 0; i < List.Count; i++)
			{
				this.WriteGameObject(List[i]);
			}
		}

		// Token: 0x060059B2 RID: 22962 RVA: 0x001B2E9C File Offset: 0x001B109C
		public void WriteGameObject(GameObject Object)
		{
			if (Object == null)
			{
				this.write7bitEncodedSigned32BitValue(0);
				return;
			}
			int count;
			if (this.TokenByGameObject.TryGetValue(Object, out count))
			{
				this.write7bitEncodedSigned32BitValue(count + 2);
				return;
			}
			Object.Flags |= 32;
			count = this.TokenGameObjects.Count;
			this.TokenByGameObject[Object] = count;
			this.TokenGameObjects.Add(Object);
			this.write7bitEncodedSigned32BitValue(count + 2);
		}

		// Token: 0x060059B3 RID: 22963 RVA: 0x001B2F0C File Offset: 0x001B110C
		public void WriteGameObject(GameObject Object, bool Reference)
		{
			if (Object == null)
			{
				this.write7bitEncodedSigned32BitValue(0);
				return;
			}
			int count;
			if (this.TokenByGameObject.TryGetValue(Object, out count))
			{
				this.write7bitEncodedSigned32BitValue(count + 2);
				return;
			}
			if (!Reference)
			{
				Object.Flags |= 32;
				count = this.TokenGameObjects.Count;
				this.TokenByGameObject[Object] = count;
				this.TokenGameObjects.Add(Object);
				this.write7bitEncodedSigned32BitValue(count + 2);
				return;
			}
			string text;
			if (Object.Property.TryGetValue("id", out text))
			{
				this.write7bitEncodedSigned32BitValue(1);
				this.WriteOptimized(text);
				return;
			}
			this.write7bitEncodedSigned32BitValue(0);
		}

		// Token: 0x060059B4 RID: 22964 RVA: 0x001B2FAC File Offset: 0x001B11AC
		public void WriteGameObjects()
		{
			this.GameObjectPosition = this.OutStream.Position;
			for (int i = (this.SerializePlayer ? 0 : 1); i < this.TokenGameObjects.Count; i++)
			{
				this.Write(64206);
				this.write7bitEncodedSigned32BitValue(i);
				GameObject gameObject = this.TokenGameObjects[i];
				gameObject.Save(this);
				this.Write(44203);
				gameObject.Flags &= -33;
			}
			for (int j = 0; j < this.TokenGameObjects.Count; j++)
			{
				GameObject gameObject2 = this.TokenGameObjects[j];
				List<GameObject> list;
				if (gameObject2 == null)
				{
					list = null;
				}
				else
				{
					Inventory inventory = gameObject2.Inventory;
					list = ((inventory != null) ? inventory.Objects : null);
				}
				List<GameObject> list2 = list;
				if (!list2.IsNullOrEmpty<GameObject>())
				{
					foreach (GameObject gameObject3 in list2)
					{
						if (gameObject3.Flags.HasBit(32))
						{
							MetricsManager.LogError(string.Concat(new string[]
							{
								"!POOLEDOBJECT Inventory object '",
								gameObject3.DebugName,
								"' in '",
								this.TokenGameObjects[j].DebugName,
								"' was tokenized but never wrote its data."
							}));
						}
					}
				}
			}
			this.Write(52958);
		}

		// Token: 0x060059B5 RID: 22965 RVA: 0x001B3114 File Offset: 0x001B1314
		public void WriteEventRegistries()
		{
			this.EventRegistryPosition = this.OutStream.Position;
			int i = 0;
			int count = this.EventRegistries.Count;
			while (i < count)
			{
				this.EventRegistries[i].Write(this);
				i++;
			}
		}

		/// <summary>
		/// Writes a token (an Int32 taking 1 to 4 bytes) into the stream that represents the object instance.
		/// The same token will always be used for the same object instance.
		///
		/// The object will be serialized once and recreated at deserialization time.
		/// Calls to SerializationReader.ReadTokenizedObject() will retrieve the same object instance.
		///
		/// </summary>
		/// <param name="value">The object to tokenize. Must not be null and must not be a string.</param>
		// Token: 0x060059B6 RID: 22966 RVA: 0x001B315C File Offset: 0x001B135C
		public void WriteTokenizedObject(object value)
		{
			this.WriteTokenizedObject(value, false);
		}

		/// <summary>
		/// Writes a token (an Int32 taking 1 to 4 bytes) into the stream that represents the object instance.
		/// The same token will always be used for the same object instance.
		///
		/// When recreateFromType is set to true, the object's Type will be stored and the object recreated using 
		/// Activator.GetInstance with a parameterless contructor. This is useful for stateless, factory-type classes.
		///
		/// When recreateFromType is set to false, the object will be serialized once and recreated at deserialization time.
		///
		/// Calls to SerializationReader.ReadTokenizedObject() will retrieve the same object instance.
		/// </summary>
		/// <param name="value">The object to tokenize. Must not be null and must not be a string.</param>
		/// <param name="recreateFromType">true if the object can be recreated using a parameterless constructor; 
		/// false if the object should be serialized as-is</param>
		// Token: 0x060059B7 RID: 22967 RVA: 0x001B3168 File Offset: 0x001B1368
		public void WriteTokenizedObject(object value, bool recreateFromType)
		{
			if (recreateFromType)
			{
				if (this.WrapperCache == null)
				{
					this.WrapperCache = new SerializationWriter.SingletonTypeWrapper();
				}
				this.WrapperCache.WrappedType = value.GetType();
				value = this.WrapperCache;
			}
			object obj = this.TokenByObject[value];
			if (obj != null)
			{
				this.write7bitEncodedSigned32BitValue((int)obj);
				return;
			}
			if (recreateFromType)
			{
				value = new SerializationWriter.SingletonTypeWrapper(this.WrapperCache);
			}
			int count = this.TokenObjects.Count;
			this.TokenObjects.Add(value);
			this.TokenByObject[value] = count;
			this.write7bitEncodedSigned32BitValue(count);
		}

		// Token: 0x060059B8 RID: 22968 RVA: 0x001B3204 File Offset: 0x001B1404
		public void WriteTokenized(Type Type)
		{
			if (Type == null)
			{
				this.write7bitEncodedSigned32BitValue(0);
				return;
			}
			int num;
			if (!this.TokenByType.TryGetValue(Type, out num))
			{
				num = this.TokenTypes.Count + 1;
				this.TokenTypes.Add(Type);
				this.TokenByType[Type] = num;
			}
			this.write7bitEncodedSigned32BitValue(num);
		}

		// Token: 0x060059B9 RID: 22969 RVA: 0x001B325C File Offset: 0x001B145C
		public void WriteTokenized(ITokenized Value)
		{
			int num = 0;
			if (Value != null)
			{
				num = Value.Token;
				if (num <= 0)
				{
					num = (Value.Token = this.Tokenized.Count + 1);
					this.Tokenized.Add(Value);
				}
			}
			this.write7bitEncodedSigned32BitValue(num);
		}

		// Token: 0x060059BA RID: 22970 RVA: 0x001B32A4 File Offset: 0x001B14A4
		public void WriteModVersions()
		{
			Rack<ModInfo> activeMods = ModManager.ActiveMods;
			int num = ((activeMods != null) ? activeMods.Count : 0);
			this.WriteOptimized(num);
			for (int i = 0; i < num; i++)
			{
				ModInfo modInfo = ModManager.ActiveMods[i];
				this.WriteOptimized(modInfo.ID);
				ModManifest manifest = modInfo.Manifest;
				this.WriteOptimized((manifest != null) ? manifest.Version : Version.Zero);
			}
		}

		// Token: 0x060059BB RID: 22971 RVA: 0x001B330A File Offset: 0x001B150A
		internal void WriteExtensionHash()
		{
			this.WriteOptimized(ComponentReflection.ExtensionHash);
		}

		// Token: 0x060059BC RID: 22972 RVA: 0x001B3318 File Offset: 0x001B1518
		internal static IFastSerializationTypeSurrogate findSurrogateForType(Type type)
		{
			foreach (IFastSerializationTypeSurrogate fastSerializationTypeSurrogate in SerializationWriter.TypeSurrogates)
			{
				if (fastSerializationTypeSurrogate.SupportsType(type))
				{
					return fastSerializationTypeSurrogate;
				}
			}
			return null;
		}

		// Token: 0x060059BD RID: 22973 RVA: 0x001B3374 File Offset: 0x001B1574
		private static IFormatter createBinaryFormatter()
		{
			if (SerializationWriter.formatter == null)
			{
				Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
				SerializationWriter.formatter = new BinaryFormatter();
				SerializationWriter.formatter.AssemblyFormat = FormatterAssemblyStyle.Simple;
				SerializationWriter.formatter.TypeFormat = FormatterTypeStyle.TypesWhenNeeded;
			}
			return SerializationWriter.formatter;
		}

		/// <summary>
		/// Encodes a TimeSpan into the fewest number of bytes.
		/// Has been separated from the WriteOptimized(TimeSpan) method so that WriteOptimized(DateTime)
		/// can also use this for .NET 2.0 DateTimeKind information.
		/// By taking advantage of the fact that a DateTime's TimeOfDay portion will never use the IsNegative
		/// and HasDays flags, we can use these 2 bits to store the DateTimeKind and, since DateTimeKind is
		/// unlikely to be set without a Time, we need no additional bytes to support a .NET 2.0 DateTime.
		/// </summary>
		/// <param name="value">The TimeSpan to store.</param>
		/// <param name="partOfDateTime">True if the TimeSpan is the TimeOfDay from a DateTime; False if a real TimeSpan.</param>
		/// <param name="initialData">The intial data for the BitVector32 - contains DateTimeKind or 0</param>
		// Token: 0x060059BE RID: 22974 RVA: 0x001B33B4 File Offset: 0x001B15B4
		private void encodeTimeSpan(TimeSpan value, bool partOfDateTime, int initialData)
		{
			BitVector32 bitVector = new BitVector32(initialData);
			int num = Math.Abs(value.Hours);
			int num2 = Math.Abs(value.Minutes);
			int num3 = Math.Abs(value.Seconds);
			int num4 = Math.Abs(value.Milliseconds);
			bool flag = num != 0 || num2 != 0;
			int num5 = 0;
			int num6;
			if (partOfDateTime)
			{
				num6 = 0;
			}
			else
			{
				num6 = Math.Abs(value.Days);
				bitVector[SerializationWriter.IsNegativeSection] = ((value.Ticks < 0L) ? 1 : 0);
				bitVector[SerializationWriter.HasDaysSection] = ((num6 != 0) ? 1 : 0);
			}
			if (flag)
			{
				bitVector[SerializationWriter.HasTimeSection] = 1;
				bitVector[SerializationWriter.HoursSection] = num;
				bitVector[SerializationWriter.MinutesSection] = num2;
			}
			if (num3 != 0)
			{
				bitVector[SerializationWriter.HasSecondsSection] = 1;
				if (!flag && num4 == 0)
				{
					bitVector[SerializationWriter.MinutesSection] = num3;
				}
				else
				{
					bitVector[SerializationWriter.SecondsSection] = num3;
					num5++;
				}
			}
			if (num4 != 0)
			{
				bitVector[SerializationWriter.HasMillisecondsSection] = 1;
				bitVector[SerializationWriter.MillisecondsSection] = num4;
				num5 = 2;
			}
			int data = bitVector.Data;
			this.Write((byte)data);
			this.Write((byte)(data >> 8));
			if (num5 > 0)
			{
				this.Write((byte)(data >> 16));
			}
			if (num5 > 1)
			{
				this.Write((byte)(data >> 24));
			}
			if (num6 != 0)
			{
				this.write7bitEncodedSigned32BitValue(num6);
			}
		}

		/// <summary>
		/// Checks whether an optimization condition has been met and throw an exception if not.
		///
		/// This method has been made conditional on THROW_IF_NOT_OPTIMIZABLE being set at compile time.
		/// By default, this is set if DEBUG is set but could be set explicitly if exceptions are required and
		/// the evaluation overhead is acceptable. 
		/// If not set, then this method and all references to it are removed at compile time.
		///
		/// Leave at the default for optimum usage.
		/// </summary>
		/// <param name="condition">An expression evaluating to true if the optimization condition is met, false otherwise.</param>
		/// <param name="message">The message to include in the exception should the optimization condition not be met.</param>
		// Token: 0x060059BF RID: 22975 RVA: 0x001B3523 File Offset: 0x001B1723
		[Conditional("THROW_IF_NOT_OPTIMIZABLE")]
		private static void checkOptimizable(bool condition, string message)
		{
			if (!condition)
			{
				throw new OptimizationException(message);
			}
		}

		/// <summary>
		/// Stores a 32-bit signed value into the stream using 7-bit encoding.
		///
		/// The value is written 7 bits at a time (starting with the least-significant bits) until there are no more bits to write.
		/// The eighth bit of each byte stored is used to indicate whether there are more bytes following this one.
		///
		/// See Write(Int32) for details of the values that are optimizable.
		/// </summary>
		/// <param name="value">The Int32 value to encode.</param>
		// Token: 0x060059C0 RID: 22976 RVA: 0x001B3530 File Offset: 0x001B1730
		private void write7bitEncodedSigned32BitValue(int value)
		{
			uint num;
			for (num = (uint)value; num >= 128U; num >>= 7)
			{
				this.Write((byte)(num | 128U));
			}
			this.Write((byte)num);
		}

		/// <summary>
		/// Stores a 64-bit signed value into the stream using 7-bit encoding.
		///
		/// The value is written 7 bits at a time (starting with the least-significant bits) until there are no more bits to write.
		/// The eighth bit of each byte stored is used to indicate whether there are more bytes following this one.
		///
		/// See Write(Int64) for details of the values that are optimizable.
		/// </summary>
		/// <param name="value">The Int64 value to encode.</param>
		// Token: 0x060059C1 RID: 22977 RVA: 0x001B3564 File Offset: 0x001B1764
		private void write7bitEncodedSigned64BitValue(long value)
		{
			ulong num;
			for (num = (ulong)value; num >= 128UL; num >>= 7)
			{
				this.Write((byte)(num | 128UL));
			}
			this.Write((byte)num);
		}

		/// <summary>
		/// Stores a 32-bit unsigned value into the stream using 7-bit encoding.
		///
		/// The value is written 7 bits at a time (starting with the least-significant bits) until there are no more bits to write.
		/// The eighth bit of each byte stored is used to indicate whether there are more bytes following this one.
		///
		/// See Write(UInt32) for details of the values that are optimizable.
		/// </summary>
		/// <param name="value">The UInt32 value to encode.</param>
		// Token: 0x060059C2 RID: 22978 RVA: 0x001B3599 File Offset: 0x001B1799
		private void write7bitEncodedUnsigned32BitValue(uint value)
		{
			while (value >= 128U)
			{
				this.Write((byte)(value | 128U));
				value >>= 7;
			}
			this.Write((byte)value);
		}

		/// <summary>
		/// Stores a 64-bit unsigned value into the stream using 7-bit encoding.
		///
		/// The value is written 7 bits at a time (starting with the least-significant bits) until there are no more bits to write.
		/// The eighth bit of each byte stored is used to indicate whether there are more bytes following this one.
		///
		/// See Write(ULong) for details of the values that are optimizable.
		/// </summary>
		/// <param name="value">The ULong value to encode.</param>
		// Token: 0x060059C3 RID: 22979 RVA: 0x001B35C0 File Offset: 0x001B17C0
		private void write7bitEncodedUnsigned64BitValue(ulong value)
		{
			while (value >= 128UL)
			{
				this.Write((byte)(value | 128UL));
				value >>= 7;
			}
			this.Write((byte)value);
		}

		/// <summary>
		/// Internal implementation to store a non-null Boolean[].
		/// </summary>
		/// <remarks>
		/// Stored as a BitArray for optimization.
		/// </remarks>
		/// <param name="values">The Boolean[] to store.</param>
		// Token: 0x060059C4 RID: 22980 RVA: 0x001B35E9 File Offset: 0x001B17E9
		private void writeArray(bool[] values)
		{
			this.WriteOptimized(new BitArray(values));
		}

		/// <summary>
		/// Internal implementation to store a non-null Byte[].
		/// </summary>
		/// <param name="values">The Byte[] to store.</param>
		// Token: 0x060059C5 RID: 22981 RVA: 0x001B35F7 File Offset: 0x001B17F7
		private void writeArray(byte[] values)
		{
			this.write7bitEncodedSigned32BitValue(values.Length);
			if (values.Length != 0)
			{
				base.Write(values);
			}
		}

		/// <summary>
		/// Internal implementation to store a non-null Char[].
		/// </summary>
		/// <param name="values">The Char[] to store.</param>
		// Token: 0x060059C6 RID: 22982 RVA: 0x001B360D File Offset: 0x001B180D
		private void writeArray(char[] values)
		{
			this.write7bitEncodedSigned32BitValue(values.Length);
			if (values.Length != 0)
			{
				base.Write(values);
			}
		}

		/// <summary>
		/// Internal implementation to write a non, null DateTime[] using a BitArray to 
		/// determine which elements are optimizable.
		/// </summary>
		/// <param name="values">The DateTime[] to store.</param>
		/// <param name="optimizeFlags">A BitArray indicating which of the elements which are optimizable; 
		/// a reference to constant FullyOptimizableValueArray if all the elements are optimizable; or null
		/// if none of the elements are optimizable.</param>
		// Token: 0x060059C7 RID: 22983 RVA: 0x001B3624 File Offset: 0x001B1824
		private void writeArray(DateTime[] values, BitArray optimizeFlags)
		{
			this.writeTypedArrayTypeCode(optimizeFlags, values.Length);
			for (int i = 0; i < values.Length; i++)
			{
				if (optimizeFlags == null || (optimizeFlags != SerializationWriter.FullyOptimizableTypedArray && !optimizeFlags[i]))
				{
					this.Write(values[i]);
				}
				else
				{
					this.WriteOptimized(values[i]);
				}
			}
		}

		/// <summary>
		/// Internal implementation to store a non-null Decimal[].
		/// </summary>
		/// <remarks>
		/// All elements are stored optimized.
		/// </remarks>
		/// <param name="values">The Decimal[] to store.</param>
		// Token: 0x060059C8 RID: 22984 RVA: 0x001B367C File Offset: 0x001B187C
		private void writeArray(decimal[] values)
		{
			this.write7bitEncodedSigned32BitValue(values.Length);
			for (int i = 0; i < values.Length; i++)
			{
				this.WriteOptimized(values[i]);
			}
		}

		/// <summary>
		/// Internal implementation to store a non-null Double[].
		/// </summary>
		/// <param name="values">The Double[] to store.</param>
		// Token: 0x060059C9 RID: 22985 RVA: 0x001B36B0 File Offset: 0x001B18B0
		private void writeArray(double[] values)
		{
			this.write7bitEncodedSigned32BitValue(values.Length);
			foreach (double num in values)
			{
				this.Write(num);
			}
		}

		/// <summary>
		/// Internal implementation to store a non-null Single[].
		/// </summary>
		/// <param name="values">The Single[] to store.</param>
		// Token: 0x060059CA RID: 22986 RVA: 0x001B36E4 File Offset: 0x001B18E4
		private void writeArray(float[] values)
		{
			this.write7bitEncodedSigned32BitValue(values.Length);
			foreach (float num in values)
			{
				this.Write(num);
			}
		}

		/// <summary>
		/// Internal implementation to store a non-null Guid[].
		/// </summary>
		/// <param name="values">The Guid[] to store.</param>
		// Token: 0x060059CB RID: 22987 RVA: 0x001B3718 File Offset: 0x001B1918
		private void writeArray(Guid[] values)
		{
			this.write7bitEncodedSigned32BitValue(values.Length);
			foreach (Guid guid in values)
			{
				this.Write(guid);
			}
		}

		/// <summary>
		/// Internal implementation to write a non-null Int16[] using a BitArray to determine which elements are optimizable.
		/// </summary>
		/// <param name="values">The Int16[] to store.</param>
		/// <param name="optimizeFlags">A BitArray indicating which of the elements which are optimizable; 
		/// a reference to constant FullyOptimizableValueArray if all the elements are optimizable; or null
		/// if none of the elements are optimizable.</param>
		// Token: 0x060059CC RID: 22988 RVA: 0x001B3750 File Offset: 0x001B1950
		private void writeArray(short[] values, BitArray optimizeFlags)
		{
			this.writeTypedArrayTypeCode(optimizeFlags, values.Length);
			for (int i = 0; i < values.Length; i++)
			{
				if (optimizeFlags == null || (optimizeFlags != SerializationWriter.FullyOptimizableTypedArray && !optimizeFlags[i]))
				{
					this.Write(values[i]);
				}
				else
				{
					this.write7bitEncodedSigned32BitValue((int)values[i]);
				}
			}
		}

		/// <summary>
		/// Internal implementation to write a non-null Int32[] using a BitArray to determine which elements are optimizable.
		/// </summary>
		/// <param name="values">The Int32[] to store.</param>
		/// <param name="optimizeFlags">A BitArray indicating which of the elements which are optimizable; 
		/// a reference to constant FullyOptimizableValueArray if all the elements are optimizable; or null
		/// if none of the elements are optimizable.</param>
		// Token: 0x060059CD RID: 22989 RVA: 0x001B37A0 File Offset: 0x001B19A0
		private void writeArray(int[] values, BitArray optimizeFlags)
		{
			this.writeTypedArrayTypeCode(optimizeFlags, values.Length);
			for (int i = 0; i < values.Length; i++)
			{
				if (optimizeFlags == null || (optimizeFlags != SerializationWriter.FullyOptimizableTypedArray && !optimizeFlags[i]))
				{
					this.Write(values[i]);
				}
				else
				{
					this.write7bitEncodedSigned32BitValue(values[i]);
				}
			}
		}

		/// <summary>
		/// Internal implementation to writes a non-null Int64[] using a BitArray to determine which elements are optimizable.
		/// </summary>
		/// <param name="values">The Int64[] to store.</param>
		/// <param name="optimizeFlags">A BitArray indicating which of the elements which are optimizable; 
		/// a reference to constant FullyOptimizableValueArray if all the elements are optimizable; or null
		/// if none of the elements are optimizable.</param>
		// Token: 0x060059CE RID: 22990 RVA: 0x001B37F0 File Offset: 0x001B19F0
		private void writeArray(long[] values, BitArray optimizeFlags)
		{
			this.writeTypedArrayTypeCode(optimizeFlags, values.Length);
			for (int i = 0; i < values.Length; i++)
			{
				if (optimizeFlags == null || (optimizeFlags != SerializationWriter.FullyOptimizableTypedArray && !optimizeFlags[i]))
				{
					this.Write(values[i]);
				}
				else
				{
					this.write7bitEncodedSigned64BitValue(values[i]);
				}
			}
		}

		/// <summary>
		/// Internal implementation to store a non-null SByte[].
		/// </summary>
		/// <param name="values">The SByte[] to store.</param>
		// Token: 0x060059CF RID: 22991 RVA: 0x001B3840 File Offset: 0x001B1A40
		private void writeArray(sbyte[] values)
		{
			this.write7bitEncodedSigned32BitValue(values.Length);
			foreach (sbyte b in values)
			{
				this.Write(b);
			}
		}

		/// <summary>
		/// Internal implementation to store a non-null Int16[].
		/// </summary>
		/// <param name="values">The Int16[] to store.</param>
		// Token: 0x060059D0 RID: 22992 RVA: 0x001B3874 File Offset: 0x001B1A74
		private void writeArray(short[] values)
		{
			this.write7bitEncodedSigned32BitValue(values.Length);
			foreach (short num in values)
			{
				this.Write(num);
			}
		}

		/// <summary>
		/// Internal implementation to write a non-null TimeSpan[] using a BitArray to determine which elements are optimizable.
		/// </summary>
		/// <param name="values">The TimeSpan[] to store.</param>
		/// <param name="optimizeFlags">A BitArray indicating which of the elements which are optimizable; 
		/// a reference to constant FullyOptimizableValueArray if all the elements are optimizable; or null
		/// if none of the elements are optimizable.</param>
		// Token: 0x060059D1 RID: 22993 RVA: 0x001B38A8 File Offset: 0x001B1AA8
		private void writeArray(TimeSpan[] values, BitArray optimizeFlags)
		{
			this.writeTypedArrayTypeCode(optimizeFlags, values.Length);
			for (int i = 0; i < values.Length; i++)
			{
				if (optimizeFlags == null || (optimizeFlags != SerializationWriter.FullyOptimizableTypedArray && !optimizeFlags[i]))
				{
					this.Write(values[i]);
				}
				else
				{
					this.WriteOptimized(values[i]);
				}
			}
		}

		/// <summary>
		/// Internal implementation to write a non-null UInt16[] using a BitArray to determine which elements are optimizable.
		/// </summary>
		/// <param name="values">The UInt16[] to store.</param>
		/// <param name="optimizeFlags">A BitArray indicating which of the elements which are optimizable; 
		/// a reference to constant FullyOptimizableValueArray if all the elements are optimizable; or null
		/// if none of the elements are optimizable.</param>
		// Token: 0x060059D2 RID: 22994 RVA: 0x001B3900 File Offset: 0x001B1B00
		private void writeArray(ushort[] values, BitArray optimizeFlags)
		{
			this.writeTypedArrayTypeCode(optimizeFlags, values.Length);
			for (int i = 0; i < values.Length; i++)
			{
				if (optimizeFlags == null || (optimizeFlags != SerializationWriter.FullyOptimizableTypedArray && !optimizeFlags[i]))
				{
					this.Write(values[i]);
				}
				else
				{
					this.write7bitEncodedUnsigned32BitValue((uint)values[i]);
				}
			}
		}

		/// <summary>
		/// Internal implementation to write a non-null UInt32[] using a BitArray to determine which elements are optimizable.
		/// </summary>
		/// <param name="values">The UInt32[] to store.</param>
		/// <param name="optimizeFlags">A BitArray indicating which of the elements which are optimizable; 
		/// a reference to constant FullyOptimizableValueArray if all the elements are optimizable; or null
		/// if none of the elements are optimizable.</param>
		// Token: 0x060059D3 RID: 22995 RVA: 0x001B3950 File Offset: 0x001B1B50
		private void writeArray(uint[] values, BitArray optimizeFlags)
		{
			this.writeTypedArrayTypeCode(optimizeFlags, values.Length);
			for (int i = 0; i < values.Length; i++)
			{
				if (optimizeFlags == null || (optimizeFlags != SerializationWriter.FullyOptimizableTypedArray && !optimizeFlags[i]))
				{
					this.Write(values[i]);
				}
				else
				{
					this.write7bitEncodedUnsigned32BitValue(values[i]);
				}
			}
		}

		/// <summary>
		/// Internal implementation to store a non-null UInt16[].
		/// </summary>
		/// <param name="values">The UIn16[] to store.</param>
		// Token: 0x060059D4 RID: 22996 RVA: 0x001B39A0 File Offset: 0x001B1BA0
		private void writeArray(ushort[] values)
		{
			this.write7bitEncodedSigned32BitValue(values.Length);
			foreach (ushort num in values)
			{
				this.Write(num);
			}
		}

		/// <summary>
		/// Internal implementation to write a non-null UInt64[] using a BitArray to determine which elements are optimizable.
		/// </summary>
		/// <param name="values">The UInt64[] to store.</param>
		/// <param name="optimizeFlags">A BitArray indicating which of the elements which are optimizable; 
		/// a reference to constant FullyOptimizableValueArray if all the elements are optimizable; or null
		/// if none of the elements are optimizable.</param>
		// Token: 0x060059D5 RID: 22997 RVA: 0x001B39D4 File Offset: 0x001B1BD4
		private void writeArray(ulong[] values, BitArray optimizeFlags)
		{
			this.writeTypedArrayTypeCode(optimizeFlags, values.Length);
			for (int i = 0; i < values.Length; i++)
			{
				if (optimizeFlags == null || (optimizeFlags != SerializationWriter.FullyOptimizableTypedArray && !optimizeFlags[i]))
				{
					this.Write(values[i]);
				}
				else
				{
					this.write7bitEncodedUnsigned64BitValue(values[i]);
				}
			}
		}

		/// <summary>
		/// Writes the values in the non-null object[] into the stream.
		///
		/// Sequences of null values and sequences of DBNull.Values are stored with a flag and optimized count.
		/// Other values are stored using WriteObject().
		///
		/// This routine is called by the Write(object[]), WriteOptimized(object[]) and Write(object[], object[])) methods.
		/// </summary>
		/// <param name="values" />
		// Token: 0x060059D6 RID: 22998 RVA: 0x001B3A24 File Offset: 0x001B1C24
		private void writeObjectArray(object[] values)
		{
			this.write7bitEncodedSigned32BitValue(values.Length);
			int num = values.Length - 1;
			for (int i = 0; i < values.Length; i++)
			{
				object obj = values[i];
				if (i < num && ((obj == null) ? (values[i + 1] == null) : obj.Equals(values[i + 1])))
				{
					int num2 = 1;
					if (obj == null)
					{
						this.writeTypeCode(SerializedType.NullSequenceType);
						for (i++; i < num; i++)
						{
							if (values[i + 1] != null)
							{
								break;
							}
							num2++;
						}
					}
					else if (obj == DBNull.Value)
					{
						this.writeTypeCode(SerializedType.DBNullSequenceType);
						for (i++; i < num; i++)
						{
							if (values[i + 1] != DBNull.Value)
							{
								break;
							}
							num2++;
						}
					}
					else
					{
						this.writeTypeCode(SerializedType.DuplicateValueSequenceType);
						i++;
						while (i < num && obj.Equals(values[i + 1]))
						{
							num2++;
							i++;
						}
						this.WriteObject(obj);
					}
					this.write7bitEncodedSigned32BitValue(num2);
				}
				else
				{
					this.WriteObject(obj);
				}
			}
		}

		/// <summary>
		/// Stores the specified SerializedType code into the stream.
		///
		/// By using a centralized method, it is possible to collect statistics for the
		/// type of data being stored in DEBUG mode.
		///
		/// Use the DumpTypeUsage() method to show a list of used SerializedTypes and
		/// the number of times each has been used. This method and the collection code
		/// will be optimized out when compiling in Release mode.
		/// </summary>
		/// <param name="typeCode">The SerializedType to store.</param>
		// Token: 0x060059D7 RID: 22999 RVA: 0x001B3B1B File Offset: 0x001B1D1B
		private void writeTypeCode(SerializedType typeCode)
		{
			this.Write((byte)typeCode);
		}

		/// <summary>
		/// Internal implementation to write a non-null typed array into the stream.
		/// </summary>
		/// <remarks>
		/// Checks first to see if the element type is a primitive type and calls the 
		/// correct routine if so. Otherwise determines the best, optimized method
		/// to store the array contents.
		/// <para />
		/// An array of object elements never stores its type.
		/// </remarks>
		/// <param name="value">The non-null typed array to store.</param>
		/// <param name="storeType">True if the type should be stored; false otherwise</param>
		// Token: 0x060059D8 RID: 23000 RVA: 0x001B3B24 File Offset: 0x001B1D24
		private void writeTypedArray(Array value, bool storeType)
		{
			Type elementType = value.GetType().GetElementType();
			if (elementType == typeof(object))
			{
				storeType = false;
			}
			if (elementType == typeof(string))
			{
				this.writeTypeCode(SerializedType.StringArrayType);
				this.WriteOptimized((object[])value);
				return;
			}
			if (elementType == typeof(int))
			{
				this.writeTypeCode(SerializedType.Int32ArrayType);
				if (this.optimizeForSize)
				{
					this.WriteOptimized((int[])value);
					return;
				}
				this.Write((int[])value);
				return;
			}
			else if (elementType == typeof(short))
			{
				this.writeTypeCode(SerializedType.Int16ArrayType);
				if (this.optimizeForSize)
				{
					this.WriteOptimized((short[])value);
					return;
				}
				this.Write((short[])value);
				return;
			}
			else if (elementType == typeof(long))
			{
				this.writeTypeCode(SerializedType.Int64ArrayType);
				if (this.optimizeForSize)
				{
					this.WriteOptimized((long[])value);
					return;
				}
				this.Write((long[])value);
				return;
			}
			else if (elementType == typeof(uint))
			{
				this.writeTypeCode(SerializedType.UInt32ArrayType);
				if (this.optimizeForSize)
				{
					this.WriteOptimized((uint[])value);
					return;
				}
				this.Write((uint[])value);
				return;
			}
			else if (elementType == typeof(ushort))
			{
				this.writeTypeCode(SerializedType.UInt16ArrayType);
				if (this.optimizeForSize)
				{
					this.WriteOptimized((ushort[])value);
					return;
				}
				this.Write((ushort[])value);
				return;
			}
			else if (elementType == typeof(ulong))
			{
				this.writeTypeCode(SerializedType.UInt64ArrayType);
				if (this.optimizeForSize)
				{
					this.WriteOptimized((ulong[])value);
					return;
				}
				this.Write((ulong[])value);
				return;
			}
			else
			{
				if (elementType == typeof(float))
				{
					this.writeTypeCode(SerializedType.SingleArrayType);
					this.writeArray((float[])value);
					return;
				}
				if (elementType == typeof(double))
				{
					this.writeTypeCode(SerializedType.DoubleArrayType);
					this.writeArray((double[])value);
					return;
				}
				if (elementType == typeof(decimal))
				{
					this.writeTypeCode(SerializedType.DecimalArrayType);
					this.writeArray((decimal[])value);
					return;
				}
				if (elementType == typeof(DateTime))
				{
					this.writeTypeCode(SerializedType.DateTimeArrayType);
					if (this.optimizeForSize)
					{
						this.WriteOptimized((DateTime[])value);
						return;
					}
					this.Write((DateTime[])value);
					return;
				}
				else if (elementType == typeof(TimeSpan))
				{
					this.writeTypeCode(SerializedType.TimeSpanArrayType);
					if (this.optimizeForSize)
					{
						this.WriteOptimized((TimeSpan[])value);
						return;
					}
					this.Write((TimeSpan[])value);
					return;
				}
				else
				{
					if (elementType == typeof(Guid))
					{
						this.writeTypeCode(SerializedType.GuidArrayType);
						this.writeArray((Guid[])value);
						return;
					}
					if (elementType == typeof(sbyte))
					{
						this.writeTypeCode(SerializedType.SByteArrayType);
						this.writeArray((sbyte[])value);
						return;
					}
					if (elementType == typeof(bool))
					{
						this.writeTypeCode(SerializedType.BooleanArrayType);
						this.writeArray((bool[])value);
						return;
					}
					if (elementType == typeof(byte))
					{
						this.writeTypeCode(SerializedType.ByteArrayType);
						this.writeArray((byte[])value);
						return;
					}
					if (elementType == typeof(char))
					{
						this.writeTypeCode(SerializedType.CharArrayType);
						this.writeArray((char[])value);
						return;
					}
					if (value.Length == 0)
					{
						this.writeTypeCode((elementType == typeof(object)) ? SerializedType.EmptyObjectArrayType : SerializedType.EmptyTypedArrayType);
						if (storeType)
						{
							this.WriteTokenized(elementType);
							return;
						}
					}
					else
					{
						if (elementType == typeof(object))
						{
							this.writeTypeCode(SerializedType.ObjectArrayType);
							this.writeObjectArray((object[])value);
							return;
						}
						BitArray bitArray = (SerializationWriter.isTypeRecreatable(elementType) ? SerializationWriter.FullyOptimizableTypedArray : null);
						if (!elementType.IsValueType)
						{
							if (bitArray == null || !SerializationWriter.arrayElementsAreSameType((object[])value, elementType))
							{
								if (!storeType)
								{
									this.writeTypeCode(SerializedType.ObjectArrayType);
								}
								else
								{
									this.writeTypeCode(SerializedType.OtherTypedArrayType);
									this.WriteTokenized(elementType);
								}
								this.writeObjectArray((object[])value);
								return;
							}
							for (int i = 0; i < value.Length; i++)
							{
								if (value.GetValue(i) == null)
								{
									if (bitArray == SerializationWriter.FullyOptimizableTypedArray)
									{
										bitArray = new BitArray(value.Length);
									}
									bitArray[i] = true;
								}
							}
						}
						this.writeTypedArrayTypeCode(bitArray, value.Length);
						if (storeType)
						{
							this.WriteTokenized(elementType);
						}
						for (int j = 0; j < value.Length; j++)
						{
							if (bitArray == null)
							{
								this.WriteObject(value.GetValue(j));
							}
							else if (bitArray == SerializationWriter.FullyOptimizableTypedArray || !bitArray[j])
							{
								this.Write((IOwnedDataSerializable)value.GetValue(j), null);
							}
						}
					}
					return;
				}
			}
		}

		/// <summary>
		/// Checks whether instances of a Type can be created.
		/// </summary>
		/// <remarks>
		/// A Value Type only needs to implement IOwnedDataSerializable. 
		/// A Reference Type needs to implement IOwnedDataSerializableAndRecreatable and provide a default constructor.
		/// </remarks>
		/// <param name="type">The Type to check</param>
		/// <returns>true if the Type is recreatable; false otherwise.</returns>
		// Token: 0x060059D9 RID: 23001 RVA: 0x001B4015 File Offset: 0x001B2215
		private static bool isTypeRecreatable(Type type)
		{
			if (type.IsValueType)
			{
				return typeof(IOwnedDataSerializable).IsAssignableFrom(type);
			}
			return typeof(IOwnedDataSerializableAndRecreatable).IsAssignableFrom(type) && type.GetConstructor(Type.EmptyTypes) != null;
		}

		/// <summary>
		/// Checks whether each element in an array is of the same type.
		/// </summary>
		/// <param name="values">The array to check</param>
		/// <param name="elementType">The expected element type.</param>
		/// <returns />
		// Token: 0x060059DA RID: 23002 RVA: 0x001B4058 File Offset: 0x001B2258
		private static bool arrayElementsAreSameType(object[] values, Type elementType)
		{
			foreach (object obj in values)
			{
				if (obj != null && obj.GetType() != elementType)
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Writes the TypeCode for the Typed Array followed by the number of elements.
		/// </summary>
		/// <param name="optimizeFlags" />
		/// <param name="length" />
		// Token: 0x060059DB RID: 23003 RVA: 0x001B408D File Offset: 0x001B228D
		private void writeTypedArrayTypeCode(BitArray optimizeFlags, int length)
		{
			if (optimizeFlags == null)
			{
				this.writeTypeCode(SerializedType.NonOptimizedTypedArrayType);
			}
			else if (optimizeFlags == SerializationWriter.FullyOptimizableTypedArray)
			{
				this.writeTypeCode(SerializedType.FullyOptimizedTypedArrayType);
			}
			else
			{
				this.writeTypeCode(SerializedType.PartiallyOptimizedTypedArrayType);
				this.WriteOptimized(optimizeFlags);
			}
			this.write7bitEncodedSigned32BitValue(length);
		}

		// Token: 0x060059DC RID: 23004 RVA: 0x001B40D0 File Offset: 0x001B22D0
		[Conditional("DEBUG")]
		public void DumpTypeUsage()
		{
			StringBuilder stringBuilder = new StringBuilder("Type Usage Dump\r\n---------------\r\n");
			for (int i = 0; i < 256; i++)
			{
			}
			Console.WriteLine(stringBuilder);
		}

		// Token: 0x060059DD RID: 23005 RVA: 0x001B4100 File Offset: 0x001B2300
		public SerializationWriter.Block StartBlock()
		{
			SerializationWriter.Block block;
			if (!this.Blocks.TryPop(out block))
			{
				block = new SerializationWriter.Block();
			}
			block.Start(this);
			return block;
		}

		/// <summary>
		/// Default capacity for the underlying MemoryStream
		/// </summary>
		// Token: 0x04002C4A RID: 11338
		public static int DefaultCapacity = 1024;

		/// <summary>
		/// The Default setting for the OptimizeForSize property.
		/// </summary>
		// Token: 0x04002C4B RID: 11339
		public static bool DefaultOptimizeForSize = true;

		/// <summary>
		/// The Default setting for the PreserveDecimalScale property.
		/// </summary>
		// Token: 0x04002C4C RID: 11340
		public static bool DefaultPreserveDecimalScale = false;

		// Token: 0x04002C4D RID: 11341
		private static List<IFastSerializationTypeSurrogate> typeSurrogates = null;

		// Token: 0x04002C4E RID: 11342
		private static HashSet<Type> BinaryFormattedTypes = new HashSet<Type>();

		/// <summary>
		/// Section masks used for packing DateTime values
		/// </summary>
		// Token: 0x04002C4F RID: 11343
		internal static readonly BitVector32.Section DateYearMask = BitVector32.CreateSection(127);

		// Token: 0x04002C50 RID: 11344
		internal static readonly BitVector32.Section DateMonthMask = BitVector32.CreateSection(12, SerializationWriter.DateYearMask);

		// Token: 0x04002C51 RID: 11345
		internal static readonly BitVector32.Section DateDayMask = BitVector32.CreateSection(31, SerializationWriter.DateMonthMask);

		// Token: 0x04002C52 RID: 11346
		internal static readonly BitVector32.Section DateHasTimeOrKindMask = BitVector32.CreateSection(1, SerializationWriter.DateDayMask);

		/// <summary>
		/// Section masks used for packing TimeSpan values
		/// </summary>
		// Token: 0x04002C53 RID: 11347
		internal static readonly BitVector32.Section IsNegativeSection = BitVector32.CreateSection(1);

		// Token: 0x04002C54 RID: 11348
		internal static readonly BitVector32.Section HasDaysSection = BitVector32.CreateSection(1, SerializationWriter.IsNegativeSection);

		// Token: 0x04002C55 RID: 11349
		internal static readonly BitVector32.Section HasTimeSection = BitVector32.CreateSection(1, SerializationWriter.HasDaysSection);

		// Token: 0x04002C56 RID: 11350
		internal static readonly BitVector32.Section HasSecondsSection = BitVector32.CreateSection(1, SerializationWriter.HasTimeSection);

		// Token: 0x04002C57 RID: 11351
		internal static readonly BitVector32.Section HasMillisecondsSection = BitVector32.CreateSection(1, SerializationWriter.HasSecondsSection);

		// Token: 0x04002C58 RID: 11352
		internal static readonly BitVector32.Section HoursSection = BitVector32.CreateSection(23, SerializationWriter.HasMillisecondsSection);

		// Token: 0x04002C59 RID: 11353
		internal static readonly BitVector32.Section MinutesSection = BitVector32.CreateSection(59, SerializationWriter.HoursSection);

		// Token: 0x04002C5A RID: 11354
		internal static readonly BitVector32.Section SecondsSection = BitVector32.CreateSection(59, SerializationWriter.MinutesSection);

		// Token: 0x04002C5B RID: 11355
		internal static readonly BitVector32.Section MillisecondsSection = BitVector32.CreateSection(127, SerializationWriter.SecondsSection);

		/// <summary>
		/// Holds the highest Int16 that can be optimized into less than the normal 2 bytes
		/// </summary>
		// Token: 0x04002C5C RID: 11356
		public const short HighestOptimizable16BitValue = 127;

		/// <summary>
		/// Holds the highest Int32 that can be optimized into less than the normal 4 bytes
		/// </summary>
		// Token: 0x04002C5D RID: 11357
		public const int HighestOptimizable32BitValue = 2097151;

		/// <summary>
		/// Holds the highest Int64 that can be optimized into less than the normal 8 bytes
		/// </summary>
		// Token: 0x04002C5E RID: 11358
		public const long HighestOptimizable64BitValue = 562949953421311L;

		// Token: 0x04002C5F RID: 11359
		internal const short OptimizationFailure16BitValue = 16384;

		// Token: 0x04002C60 RID: 11360
		internal const int OptimizationFailure32BitValue = 268435456;

		// Token: 0x04002C61 RID: 11361
		internal const long OptimizationFailure64BitValue = 72057594037927936L;

		// Token: 0x04002C62 RID: 11362
		internal const ushort GameObjectStartValue = 64206;

		// Token: 0x04002C63 RID: 11363
		internal const ushort GameObjectEndValue = 44203;

		// Token: 0x04002C64 RID: 11364
		internal const ushort GameObjectFinishValue = 52958;

		// Token: 0x04002C65 RID: 11365
		private static readonly BitArray FullyOptimizableTypedArray = new BitArray(0);

		// Token: 0x04002C66 RID: 11366
		private static SerializationWriter Instance = new SerializationWriter(FastSerialization.SharedCache);

		// Token: 0x04002C67 RID: 11367
		public MemoryStream Stream;

		// Token: 0x04002C68 RID: 11368
		private byte[] Buffer;

		// Token: 0x04002C69 RID: 11369
		private Stack<SerializationWriter.Block> Blocks;

		// Token: 0x04002C6A RID: 11370
		private HashSet<Assembly> LocalAssemblies;

		// Token: 0x04002C6B RID: 11371
		private SerializationWriter.UniqueStringList TokenStrings;

		// Token: 0x04002C6C RID: 11372
		private Rack<object> TokenObjects;

		// Token: 0x04002C6D RID: 11373
		private Dictionary<object, int> TokenByObject;

		// Token: 0x04002C6E RID: 11374
		private Rack<ITokenized> Tokenized;

		// Token: 0x04002C6F RID: 11375
		private Rack<Type> TokenTypes;

		// Token: 0x04002C70 RID: 11376
		private Dictionary<Type, int> TokenByType;

		// Token: 0x04002C71 RID: 11377
		private Rack<GameObject> TokenGameObjects;

		// Token: 0x04002C72 RID: 11378
		private Dictionary<GameObject, int> TokenByGameObject;

		// Token: 0x04002C73 RID: 11379
		private Rack<EventRegistry> EventRegistries;

		// Token: 0x04002C74 RID: 11380
		private Rack<GameObjectReference> GameObjectReferences;

		// Token: 0x04002C75 RID: 11381
		private long StringPosition;

		// Token: 0x04002C76 RID: 11382
		private long ObjectPosition;

		// Token: 0x04002C77 RID: 11383
		private long TokenizedPosition;

		// Token: 0x04002C78 RID: 11384
		private long TypePosition;

		// Token: 0x04002C79 RID: 11385
		private long GameObjectPosition;

		// Token: 0x04002C7A RID: 11386
		private long GameObjectReferencePosition;

		// Token: 0x04002C7B RID: 11387
		private long EventRegistryPosition;

		// Token: 0x04002C7C RID: 11388
		public int FileVersion;

		// Token: 0x04002C7D RID: 11389
		public bool SerializePlayer = true;

		// Token: 0x04002C7E RID: 11390
		private bool optimizeForSize = SerializationWriter.DefaultOptimizeForSize;

		// Token: 0x04002C7F RID: 11391
		private bool preserveDecimalScale = SerializationWriter.DefaultPreserveDecimalScale;

		// Token: 0x04002C80 RID: 11392
		private SerializationWriter.SingletonTypeWrapper WrapperCache;

		// Token: 0x04002C81 RID: 11393
		private static BinaryFormatter formatter = null;

		/// <summary>
		/// Private class used to wrap an object that is to be tokenized, and recreated at deserialization by its type.
		/// </summary>
		// Token: 0x02001A2B RID: 6699
		private class SingletonTypeWrapper : IEquatable<SerializationWriter.SingletonTypeWrapper>
		{
			// Token: 0x0600E65F RID: 58975 RVA: 0x0053213F File Offset: 0x0053033F
			public SingletonTypeWrapper()
			{
			}

			// Token: 0x0600E660 RID: 58976 RVA: 0x00532147 File Offset: 0x00530347
			public SingletonTypeWrapper(SerializationWriter.SingletonTypeWrapper Wrapper)
			{
				this.WrappedType = Wrapper.WrappedType;
			}

			// Token: 0x0600E661 RID: 58977 RVA: 0x0053215B File Offset: 0x0053035B
			public bool Equals(SerializationWriter.SingletonTypeWrapper other)
			{
				return other != null && this.WrappedType == other.WrappedType;
			}

			// Token: 0x0600E662 RID: 58978 RVA: 0x00532170 File Offset: 0x00530370
			public override bool Equals(object obj)
			{
				SerializationWriter.SingletonTypeWrapper singletonTypeWrapper = obj as SerializationWriter.SingletonTypeWrapper;
				return singletonTypeWrapper != null && this.WrappedType == singletonTypeWrapper.WrappedType;
			}

			// Token: 0x0600E663 RID: 58979 RVA: 0x00532197 File Offset: 0x00530397
			public override int GetHashCode()
			{
				return this.WrappedType.GetHashCode();
			}

			// Token: 0x04006842 RID: 26690
			public Type WrappedType;
		}

		// Token: 0x02001A2C RID: 6700
		public class Block : IDisposable
		{
			// Token: 0x0600E664 RID: 58980 RVA: 0x005321A4 File Offset: 0x005303A4
			public void Start(SerializationWriter Writer)
			{
				this.Writer = Writer;
				this.ParentStream = Writer.OutStream;
				Writer.OutStream = this.Stream;
			}

			// Token: 0x0600E665 RID: 58981 RVA: 0x005321C8 File Offset: 0x005303C8
			public void End()
			{
				if (this.ParentStream == null)
				{
					return;
				}
				this.Writer.OutStream = this.ParentStream;
				int num = (int)this.Stream.Position;
				this.Writer.WriteOptimized(num);
				if (num > 0)
				{
					byte[] buffer = this.Stream.GetBuffer();
					this.ParentStream.Write(buffer, 0, num);
					this.Stream.Position = 0L;
				}
				this.ParentStream = null;
			}

			// Token: 0x0600E666 RID: 58982 RVA: 0x0053223A File Offset: 0x0053043A
			public void Reset()
			{
				this.Stream.Position = 0L;
			}

			// Token: 0x0600E667 RID: 58983 RVA: 0x00532249 File Offset: 0x00530449
			public void Dispose()
			{
				if (this.Writer == null)
				{
					return;
				}
				this.End();
				this.Writer.Blocks.Push(this);
				this.Writer = null;
			}

			// Token: 0x04006843 RID: 26691
			private SerializationWriter Writer;

			// Token: 0x04006844 RID: 26692
			private Stream ParentStream;

			// Token: 0x04006845 RID: 26693
			private MemoryStream Stream = new MemoryStream();
		}

		/// <summary>
		/// Provides a faster way to store string tokens both maintaining the order that they were added and
		/// providing a fast lookup.
		///
		/// Based on code developed by ewbi at http://ewbi.blogs.com/develops/2006/10/uniquestringlis.html
		/// </summary>
		// Token: 0x02001A2D RID: 6701
		private sealed class UniqueStringList
		{
			// Token: 0x0600E669 RID: 58985 RVA: 0x00532288 File Offset: 0x00530488
			public UniqueStringList()
			{
				int[] array = SerializationWriter.UniqueStringList.primeNumberList;
				int num = this.primeNumberListIndex;
				this.primeNumberListIndex = num + 1;
				this.bucketListCapacity = array[num];
				this.stringList = new string[this.bucketListCapacity];
				this.buckets = new int[this.bucketListCapacity];
				this.loadLimit = (int)((float)this.bucketListCapacity * 0.72f);
			}

			// Token: 0x17000EB7 RID: 3767
			public string this[int index]
			{
				get
				{
					return this.stringList[index];
				}
			}

			// Token: 0x17000EB8 RID: 3768
			// (get) Token: 0x0600E66B RID: 58987 RVA: 0x005322F8 File Offset: 0x005304F8
			public int Count
			{
				get
				{
					return this.stringListIndex;
				}
			}

			// Token: 0x0600E66C RID: 58988 RVA: 0x00532300 File Offset: 0x00530500
			public int Add(string value)
			{
				int bucketIndex = this.getBucketIndex(value);
				int num = this.buckets[bucketIndex];
				if (num == 0)
				{
					string[] array = this.stringList;
					int num2 = this.stringListIndex;
					this.stringListIndex = num2 + 1;
					array[num2] = value;
					this.buckets[bucketIndex] = this.stringListIndex;
					if (this.stringListIndex > this.loadLimit)
					{
						this.expand();
					}
					return this.stringListIndex - 1;
				}
				return num - 1;
			}

			// Token: 0x0600E66D RID: 58989 RVA: 0x00532368 File Offset: 0x00530568
			public void Clear()
			{
				Array.Clear(this.buckets, 0, this.buckets.Length);
				Array.Clear(this.stringList, 0, this.stringList.Length);
				this.stringListIndex = 0;
			}

			// Token: 0x0600E66E RID: 58990 RVA: 0x0053239C File Offset: 0x0053059C
			private void expand()
			{
				int[] array = SerializationWriter.UniqueStringList.primeNumberList;
				int num = this.primeNumberListIndex;
				this.primeNumberListIndex = num + 1;
				this.bucketListCapacity = array[num];
				this.buckets = new int[this.bucketListCapacity];
				string[] array2 = new string[this.bucketListCapacity];
				this.stringList.CopyTo(array2, 0);
				this.stringList = array2;
				this.reindex();
			}

			// Token: 0x0600E66F RID: 58991 RVA: 0x00532400 File Offset: 0x00530600
			private void reindex()
			{
				this.loadLimit = (int)((float)this.bucketListCapacity * 0.72f);
				for (int i = 0; i < this.stringListIndex; i++)
				{
					int bucketIndex = this.getBucketIndex(this.stringList[i]);
					this.buckets[bucketIndex] = i + 1;
				}
			}

			// Token: 0x0600E670 RID: 58992 RVA: 0x0053244C File Offset: 0x0053064C
			private int getBucketIndex(string value)
			{
				int num = (value.GetHashCode() & int.MaxValue) % this.bucketListCapacity;
				int num2 = ((num > 1) ? num : 1);
				int num3 = this.bucketListCapacity;
				while (0 < num3--)
				{
					int num4 = this.buckets[num];
					if (num4 == 0)
					{
						return num;
					}
					if (string.CompareOrdinal(value, this.stringList[num4 - 1]) == 0)
					{
						return num;
					}
					num = (num + num2) % this.bucketListCapacity;
				}
				throw new InvalidOperationException("Failed to locate a bucket.");
			}

			// Token: 0x04006846 RID: 26694
			private const float LoadFactor = 0.72f;

			// Token: 0x04006847 RID: 26695
			private static readonly int[] primeNumberList = new int[]
			{
				389, 1543, 6151, 24593, 98317, 196613, 393241, 786433, 1572869, 3145739,
				6291469, 12582917, 25165843, 50331653, 100663319, 201326611, 402653189, 805306457, 1610612741
			};

			// Token: 0x04006848 RID: 26696
			private string[] stringList;

			// Token: 0x04006849 RID: 26697
			private int[] buckets;

			// Token: 0x0400684A RID: 26698
			private int bucketListCapacity;

			// Token: 0x0400684B RID: 26699
			private int stringListIndex;

			// Token: 0x0400684C RID: 26700
			private int loadLimit;

			// Token: 0x0400684D RID: 26701
			private int primeNumberListIndex;
		}
	}
}

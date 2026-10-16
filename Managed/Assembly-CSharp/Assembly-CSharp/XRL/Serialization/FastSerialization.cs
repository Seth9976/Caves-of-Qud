using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using XRL.Collections;
using XRL.World;

namespace XRL.Serialization
{
	// Token: 0x0200056D RID: 1389
	public static class FastSerialization
	{
		// Token: 0x06003C4A RID: 15434 RVA: 0x00126DD5 File Offset: 0x00124FD5
		public static IEnumerable<FieldInfo> YieldFields(Type Type, BindingFlags Flags = BindingFlags.Instance | BindingFlags.Public, FieldAttributes Mask = FieldAttributes.Static | FieldAttributes.Literal | FieldAttributes.NotSerialized)
		{
			FieldInfo[] array = ((Flags == (BindingFlags.Instance | BindingFlags.Public)) ? Type.GetCachedFields() : Type.GetFields(Flags));
			foreach (FieldInfo fieldInfo in array)
			{
				if ((fieldInfo.Attributes & Mask) == FieldAttributes.PrivateScope)
				{
					yield return fieldInfo;
				}
			}
			FieldInfo[] array2 = null;
			yield break;
		}

		// Token: 0x06003C4B RID: 15435 RVA: 0x00126DF3 File Offset: 0x00124FF3
		public static ulong GetFieldSaveHash(Type Type, BindingFlags Flags = BindingFlags.Instance | BindingFlags.Public, FieldAttributes Mask = FieldAttributes.Static | FieldAttributes.Literal | FieldAttributes.NotSerialized)
		{
			return Type.GetStableHashCode64(Flags, Mask);
		}

		// Token: 0x06003C4C RID: 15436 RVA: 0x00126E00 File Offset: 0x00125000
		public static void CacheFieldSaveVersions()
		{
			if (FastSerialization.FieldSaveVersionInfo != null)
			{
				return;
			}
			FastSerialization.FieldSaveVersionInfo = new Dictionary<FieldInfo, FieldSaveVersion>(64);
			foreach (Type type in Assembly.GetExecutingAssembly().GetTypes())
			{
				if (typeof(IComposite).IsAssignableFrom(type) || type.IsSubclassOfGeneric(typeof(IComponent<>)))
				{
					foreach (FieldInfo fieldInfo in type.GetCachedFields())
					{
						if ((fieldInfo.Attributes & (FieldAttributes.Static | FieldAttributes.Literal | FieldAttributes.NotSerialized)) == FieldAttributes.PrivateScope && Attribute.IsDefined(fieldInfo, typeof(FieldSaveVersion)))
						{
							FieldSaveVersion fieldSaveVersion;
							if (fieldInfo.DeclaringType == null || fieldInfo.DeclaringType == type)
							{
								fieldSaveVersion = fieldInfo.GetCustomAttribute<FieldSaveVersion>();
								if (fieldSaveVersion.minimumSaveVersion < 395)
								{
									goto IL_0104;
								}
							}
							else
							{
								FieldInfo field = fieldInfo.DeclaringType.GetField(fieldInfo.Name, BindingFlags.Instance | BindingFlags.Public);
								if (!FastSerialization.FieldSaveVersionInfo.TryGetValue(field, out fieldSaveVersion))
								{
									fieldSaveVersion = field.GetCustomAttribute<FieldSaveVersion>();
									if (fieldSaveVersion.minimumSaveVersion < 395)
									{
										goto IL_0104;
									}
								}
							}
							FastSerialization.FieldSaveVersionInfo.TryAdd(fieldInfo, fieldSaveVersion);
						}
						IL_0104:;
					}
				}
			}
		}

		// Token: 0x04001D94 RID: 7572
		public static FastSerialization.Cache SharedCache = new FastSerialization.Cache(1024);

		// Token: 0x04001D95 RID: 7573
		public static readonly Encoding Encoding = new UTF8Encoding(false, true);

		// Token: 0x04001D96 RID: 7574
		public const BindingFlags FieldBindingFlags = BindingFlags.Instance | BindingFlags.Public;

		// Token: 0x04001D97 RID: 7575
		public const FieldAttributes FieldSerializationMask = FieldAttributes.Static | FieldAttributes.Literal | FieldAttributes.NotSerialized;

		// Token: 0x04001D98 RID: 7576
		public const int TokenRecordBytes = 68;

		// Token: 0x04001D99 RID: 7577
		public static Dictionary<FieldInfo, FieldSaveVersion> FieldSaveVersionInfo;

		// Token: 0x0200197B RID: 6523
		public class Cache
		{
			// Token: 0x0600E3BD RID: 58301 RVA: 0x00528294 File Offset: 0x00526494
			public Cache(int Capacity)
			{
				this.MemoryStream = new MemoryStream(Capacity * 8);
				this.EventRegistries = new Rack<EventRegistry>(Capacity / 32);
				this.GameObjects = new Rack<GameObject>(Capacity * 4);
				this.GameObjectReferences = new Rack<GameObjectReference>(Capacity / 32);
				this.Tokenized = new Rack<ITokenized>(Capacity);
				this.Types = new Rack<Type>(Capacity);
				this.Names = new Rack<string>(Capacity);
				this.Strings = new Rack<string>(Capacity * 8);
				this.Objects = new Rack<object>(Capacity * 4);
			}

			// Token: 0x040065EB RID: 26091
			public bool Reserved;

			// Token: 0x040065EC RID: 26092
			public MemoryStream MemoryStream;

			// Token: 0x040065ED RID: 26093
			public Rack<EventRegistry> EventRegistries;

			// Token: 0x040065EE RID: 26094
			public Rack<GameObject> GameObjects;

			// Token: 0x040065EF RID: 26095
			public Rack<GameObjectReference> GameObjectReferences;

			// Token: 0x040065F0 RID: 26096
			public Rack<ITokenized> Tokenized;

			// Token: 0x040065F1 RID: 26097
			public Rack<Type> Types;

			// Token: 0x040065F2 RID: 26098
			public Rack<string> Names;

			// Token: 0x040065F3 RID: 26099
			public Rack<string> Strings;

			// Token: 0x040065F4 RID: 26100
			public Rack<object> Objects;
		}
	}
}

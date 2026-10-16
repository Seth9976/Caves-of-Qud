using System;
using System.Collections.Generic;
using System.Reflection;

namespace XRL.World
{
	// Token: 0x02000976 RID: 2422
	[Serializable]
	public class ZoneBuilderBlueprint : IEquatable<ZoneBuilderBlueprint>
	{
		// Token: 0x06006424 RID: 25636 RVA: 0x001EE644 File Offset: 0x001EC844
		public static ZoneBuilderBlueprint Get(string Class)
		{
			return ZoneBuilderBlueprint.Get(Class, ZoneBuilderBlueprint.GetParameterBuffer());
		}

		// Token: 0x06006425 RID: 25637 RVA: 0x001EE654 File Offset: 0x001EC854
		public static ZoneBuilderBlueprint Get(string Class, params object[] Parameters)
		{
			int num = ZoneBuilderBlueprint.HashCodeOf(Class, Parameters);
			ZoneBuilderBlueprint zoneBuilderBlueprint;
			if (!ZoneBuilderBlueprint.Repository.TryGetValue(num, out zoneBuilderBlueprint))
			{
				zoneBuilderBlueprint = (ZoneBuilderBlueprint.Repository[num] = new ZoneBuilderBlueprint(Class));
				zoneBuilderBlueprint.AddParameters(Parameters);
			}
			else if (zoneBuilderBlueprint.Class != Class || zoneBuilderBlueprint.ParameterCount != Parameters.Length / 2)
			{
				ZoneBuilderBlueprint zoneBuilderBlueprint2 = new ZoneBuilderBlueprint(Class);
				zoneBuilderBlueprint2.AddParameters(Parameters);
				MetricsManager.LogWarning(string.Format("ZoneBuilderBlueprint hash collision: [{0}] x [{1}]", zoneBuilderBlueprint2, zoneBuilderBlueprint));
				zoneBuilderBlueprint = zoneBuilderBlueprint2;
			}
			return zoneBuilderBlueprint;
		}

		// Token: 0x06006426 RID: 25638 RVA: 0x001EE6D4 File Offset: 0x001EC8D4
		public static ZoneBuilderBlueprint Get(string Class, string Key1, object Value1)
		{
			Dictionary<string, object> parameterBuffer = ZoneBuilderBlueprint.GetParameterBuffer();
			parameterBuffer[Key1] = Value1;
			return ZoneBuilderBlueprint.Get(Class, parameterBuffer);
		}

		// Token: 0x06006427 RID: 25639 RVA: 0x001EE6F8 File Offset: 0x001EC8F8
		public static ZoneBuilderBlueprint Get(string Class, string Key1, object Value1, string Key2, object Value2)
		{
			Dictionary<string, object> parameterBuffer = ZoneBuilderBlueprint.GetParameterBuffer();
			parameterBuffer[Key1] = Value1;
			parameterBuffer[Key2] = Value2;
			return ZoneBuilderBlueprint.Get(Class, parameterBuffer);
		}

		// Token: 0x06006428 RID: 25640 RVA: 0x001EE724 File Offset: 0x001EC924
		public static ZoneBuilderBlueprint Get(string Class, string Key1, object Value1, string Key2, object Value2, string Key3, object Value3)
		{
			Dictionary<string, object> parameterBuffer = ZoneBuilderBlueprint.GetParameterBuffer();
			parameterBuffer[Key1] = Value1;
			parameterBuffer[Key2] = Value2;
			parameterBuffer[Key3] = Value3;
			return ZoneBuilderBlueprint.Get(Class, parameterBuffer);
		}

		// Token: 0x06006429 RID: 25641 RVA: 0x001EE75C File Offset: 0x001EC95C
		public static ZoneBuilderBlueprint Get(string Class, string Key1, object Value1, string Key2, object Value2, string Key3, object Value3, string Key4, object Value4)
		{
			Dictionary<string, object> parameterBuffer = ZoneBuilderBlueprint.GetParameterBuffer();
			parameterBuffer[Key1] = Value1;
			parameterBuffer[Key2] = Value2;
			parameterBuffer[Key3] = Value3;
			parameterBuffer[Key4] = Value4;
			return ZoneBuilderBlueprint.Get(Class, parameterBuffer);
		}

		// Token: 0x0600642A RID: 25642 RVA: 0x001EE79C File Offset: 0x001EC99C
		public static ZoneBuilderBlueprint Get(string Class, string Key1, object Value1, string Key2, object Value2, string Key3, object Value3, string Key4, object Value4, string Key5, object Value5)
		{
			Dictionary<string, object> parameterBuffer = ZoneBuilderBlueprint.GetParameterBuffer();
			parameterBuffer[Key1] = Value1;
			parameterBuffer[Key2] = Value2;
			parameterBuffer[Key3] = Value3;
			parameterBuffer[Key4] = Value4;
			parameterBuffer[Key5] = Value5;
			return ZoneBuilderBlueprint.Get(Class, parameterBuffer);
		}

		// Token: 0x0600642B RID: 25643 RVA: 0x001EE7E8 File Offset: 0x001EC9E8
		public static ZoneBuilderBlueprint Get(string Class, string Key1, object Value1, string Key2, object Value2, string Key3, object Value3, string Key4, object Value4, string Key5, object Value5, string Key6, object Value6)
		{
			Dictionary<string, object> parameterBuffer = ZoneBuilderBlueprint.GetParameterBuffer();
			parameterBuffer[Key1] = Value1;
			parameterBuffer[Key2] = Value2;
			parameterBuffer[Key3] = Value3;
			parameterBuffer[Key4] = Value4;
			parameterBuffer[Key5] = Value5;
			parameterBuffer[Key6] = Value6;
			return ZoneBuilderBlueprint.Get(Class, parameterBuffer);
		}

		// Token: 0x0600642C RID: 25644 RVA: 0x001EE83C File Offset: 0x001ECA3C
		public static ZoneBuilderBlueprint Get(string Class, Dictionary<string, object> Parameters)
		{
			int num = ZoneBuilderBlueprint.HashCodeOf(Class, Parameters);
			ZoneBuilderBlueprint zoneBuilderBlueprint;
			if (!ZoneBuilderBlueprint.Repository.TryGetValue(num, out zoneBuilderBlueprint))
			{
				zoneBuilderBlueprint = (ZoneBuilderBlueprint.Repository[num] = new ZoneBuilderBlueprint(Class));
				zoneBuilderBlueprint.Parameters = new Dictionary<string, object>(Parameters);
			}
			else if (zoneBuilderBlueprint.Class != Class || zoneBuilderBlueprint.ParameterCount != ((Parameters != null) ? Parameters.Count : 0))
			{
				ZoneBuilderBlueprint zoneBuilderBlueprint2 = new ZoneBuilderBlueprint(Class);
				zoneBuilderBlueprint.Parameters = new Dictionary<string, object>(Parameters);
				MetricsManager.LogWarning(string.Format("ZoneBuilderBlueprint hash collision: [{0}] x [{1}]", zoneBuilderBlueprint2, zoneBuilderBlueprint));
				zoneBuilderBlueprint = zoneBuilderBlueprint2;
			}
			return zoneBuilderBlueprint;
		}

		/// <summary>Legacy signature support</summary>
		// Token: 0x0600642D RID: 25645 RVA: 0x001EE8CC File Offset: 0x001ECACC
		public static ZoneBuilderBlueprint Get(ZoneBuilderBlueprint Blueprint)
		{
			int fullHashCode = Blueprint.GetFullHashCode();
			ZoneBuilderBlueprint zoneBuilderBlueprint;
			if (!ZoneBuilderBlueprint.Repository.TryGetValue(fullHashCode, out zoneBuilderBlueprint))
			{
				Dictionary<int, ZoneBuilderBlueprint> repository = ZoneBuilderBlueprint.Repository;
				int num = fullHashCode;
				zoneBuilderBlueprint = Blueprint;
				repository[num] = Blueprint;
			}
			else if (zoneBuilderBlueprint.Class != Blueprint.Class || zoneBuilderBlueprint.ParameterCount != Blueprint.ParameterCount)
			{
				MetricsManager.LogWarning(string.Format("ZoneBuilderBlueprint hash collision: [{0}] x [{1}]", Blueprint, zoneBuilderBlueprint));
				zoneBuilderBlueprint = Blueprint;
			}
			return zoneBuilderBlueprint;
		}

		// Token: 0x0600642E RID: 25646 RVA: 0x001EE934 File Offset: 0x001ECB34
		public ZoneBuilderBlueprint()
		{
		}

		// Token: 0x0600642F RID: 25647 RVA: 0x001EE943 File Offset: 0x001ECB43
		public ZoneBuilderBlueprint(string Class)
		{
			this.Class = Class;
		}

		// Token: 0x06006430 RID: 25648 RVA: 0x001EE959 File Offset: 0x001ECB59
		public ZoneBuilderBlueprint(string Class, string Name, object Value)
		{
			this.Class = Class;
			this.AddParameter(Name, Value);
		}

		// Token: 0x06006431 RID: 25649 RVA: 0x001EE977 File Offset: 0x001ECB77
		public ZoneBuilderBlueprint(string Class, string Name, object Value, string Name2, object Value2)
		{
			this.Class = Class;
			this.AddParameter(Name, Value);
			this.AddParameter(Name2, Value2);
		}

		// Token: 0x06006432 RID: 25650 RVA: 0x001EE99F File Offset: 0x001ECB9F
		public ZoneBuilderBlueprint(string Class, string Name, object Value, string Name2, object Value2, string Name3, object Value3)
		{
			this.Class = Class;
			this.AddParameter(Name, Value);
			this.AddParameter(Name2, Value2);
			this.AddParameter(Name3, Value3);
		}

		// Token: 0x06006433 RID: 25651 RVA: 0x001EE9D1 File Offset: 0x001ECBD1
		public ZoneBuilderBlueprint(string Class, string Name, object Value, string Name2, object Value2, string Name3, object Value3, string Name4, object Value4)
		{
			this.Class = Class;
			this.AddParameter(Name, Value);
			this.AddParameter(Name2, Value2);
			this.AddParameter(Name3, Value3);
			this.AddParameter(Name4, Value4);
		}

		// Token: 0x06006434 RID: 25652 RVA: 0x001EEA10 File Offset: 0x001ECC10
		public ZoneBuilderBlueprint(string Class, string Name, object Value, string Name2, object Value2, string Name3, object Value3, string Name4, object Value4, string Name5, object Value5)
		{
			this.Class = Class;
			this.AddParameter(Name, Value);
			this.AddParameter(Name2, Value2);
			this.AddParameter(Name3, Value3);
			this.AddParameter(Name4, Value4);
			this.AddParameter(Name5, Value5);
		}

		// Token: 0x06006435 RID: 25653 RVA: 0x001EEA64 File Offset: 0x001ECC64
		public ZoneBuilderBlueprint(string Class, string Name, object Value, string Name2, object Value2, string Name3, object Value3, string Name4, object Value4, string Name5, object Value5, string Name6, object Value6)
		{
			this.Class = Class;
			this.AddParameter(Name, Value);
			this.AddParameter(Name2, Value2);
			this.AddParameter(Name3, Value3);
			this.AddParameter(Name4, Value4);
			this.AddParameter(Name5, Value5);
			this.AddParameter(Name6, Value6);
		}

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x06006436 RID: 25654 RVA: 0x001EEABF File Offset: 0x001ECCBF
		public int ParameterCount
		{
			get
			{
				if (this.Parameters != null)
				{
					return this.Parameters.Count;
				}
				return 0;
			}
		}

		// Token: 0x06006437 RID: 25655 RVA: 0x001EEAD6 File Offset: 0x001ECCD6
		public void AddParameter(string nam, object val)
		{
			if (this.Parameters == null)
			{
				this.Parameters = new Dictionary<string, object>();
			}
			this.Parameters[nam] = val;
		}

		// Token: 0x06006438 RID: 25656 RVA: 0x001EEAF8 File Offset: 0x001ECCF8
		public void AddParameters(object[] Parameters)
		{
			if (Parameters.IsNullOrEmpty<object>())
			{
				return;
			}
			for (int i = 0; i < Parameters.Length; i += 2)
			{
				this.AddParameter((string)Parameters[i], Parameters[i + 1]);
			}
		}

		// Token: 0x06006439 RID: 25657 RVA: 0x001EEB2F File Offset: 0x001ECD2F
		public object GetParameter(string Key, object Default = null)
		{
			Dictionary<string, object> parameters = this.Parameters;
			if (parameters == null)
			{
				return null;
			}
			return parameters.GetValue(Key, Default);
		}

		// Token: 0x0600643A RID: 25658 RVA: 0x001EEB44 File Offset: 0x001ECD44
		public T GetParameter<T>(string Key, T Default = default(T))
		{
			if (this.Parameters != null)
			{
				return (T)((object)this.Parameters.GetValue(Key, Default));
			}
			return Default;
		}

		// Token: 0x0600643B RID: 25659 RVA: 0x001EEB67 File Offset: 0x001ECD67
		public int GetFullHashCode()
		{
			return ZoneBuilderBlueprint.HashCodeOf(this.Class, this.Parameters);
		}

		// Token: 0x0600643C RID: 25660 RVA: 0x001EEB7C File Offset: 0x001ECD7C
		protected static int HashCodeOf(string Class, Dictionary<string, object> Parameters = null)
		{
			int num = 17;
			if (!Class.IsNullOrEmpty())
			{
				num = num * 23 + Class.GetHashCode();
			}
			if (!Parameters.IsNullOrEmpty<KeyValuePair<string, object>>())
			{
				foreach (KeyValuePair<string, object> keyValuePair in Parameters)
				{
					num = num * 23 + keyValuePair.Key.GetHashCode();
					int num2 = num * 23;
					object value = keyValuePair.Value;
					num = num2 + ((value != null) ? value.GetHashCode() : 0);
				}
			}
			return num;
		}

		// Token: 0x0600643D RID: 25661 RVA: 0x001EEC10 File Offset: 0x001ECE10
		protected static int HashCodeOf(string Class, object[] Parameters = null)
		{
			int num = 17;
			if (!Class.IsNullOrEmpty())
			{
				num = num * 23 + Class.GetHashCode();
			}
			if (!Parameters.IsNullOrEmpty<object>())
			{
				foreach (object obj in Parameters)
				{
					num = num * 23 + ((obj != null) ? obj.GetHashCode() : 0);
				}
			}
			return num;
		}

		// Token: 0x0600643E RID: 25662 RVA: 0x001EEC64 File Offset: 0x001ECE64
		public bool Equals(ZoneBuilderBlueprint other)
		{
			if (other == null)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			if (other.Class != this.Class)
			{
				return false;
			}
			if (this.Parameters.IsNullOrEmpty<KeyValuePair<string, object>>())
			{
				return other.Parameters.IsNullOrEmpty<KeyValuePair<string, object>>();
			}
			if (other.Parameters.IsNullOrEmpty<KeyValuePair<string, object>>() || this.Parameters.Count != other.Parameters.Count)
			{
				return false;
			}
			foreach (KeyValuePair<string, object> keyValuePair in this.Parameters)
			{
				object obj;
				if (!other.Parameters.TryGetValue(keyValuePair.Key, out obj) || !obj.Equals(keyValuePair.Value))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600643F RID: 25663 RVA: 0x001EED3C File Offset: 0x001ECF3C
		public override string ToString()
		{
			return "ZoneBuilder<" + this.Class + ">";
		}

		/// <returns>Boolean indicating whether execution should continue (true) or stopped and retried from start (false).</returns>
		// Token: 0x06006440 RID: 25664 RVA: 0x001EED54 File Offset: 0x001ECF54
		public bool ApplyTo(Zone Zone)
		{
			if (this.Class.IsNullOrEmpty())
			{
				MetricsManager.LogError("Blueprint class is null or empty for: " + Zone.ZoneID);
				return true;
			}
			if (this.Class.StartsWith("ZoneTemplate:"))
			{
				try
				{
					ZoneTemplateManager.Templates[this.Class.GetDelimitedSubstring(':', 1, false)].Execute(Zone, null);
				}
				catch (Exception ex)
				{
					MetricsManager.LogException("Applying zone template: " + this.Class, ex, "game_exception");
				}
				return true;
			}
			object obj = this.Create();
			MethodInfo method = obj.GetType().GetMethod("BuildZone", ZoneBuilderBlueprint.BuildZoneTypes);
			if (method == null)
			{
				MetricsManager.LogError("No BuildZone method found for class: " + this.Class);
				return true;
			}
			try
			{
				if (!(bool)method.Invoke(obj, new object[] { Zone }))
				{
					return false;
				}
			}
			catch (Exception ex2)
			{
				MetricsManager.LogException(string.Concat(new string[] { "Executing builder '", this.Class, "' for Zone '", Zone.ZoneID, "'" }), ex2, "game_exception");
				return false;
			}
			return true;
		}

		// Token: 0x06006441 RID: 25665 RVA: 0x001EEE9C File Offset: 0x001ED09C
		public static ZoneBuilderBlueprint Load(SerializationReader reader)
		{
			ZoneBuilderBlueprint zoneBuilderBlueprint = new ZoneBuilderBlueprint();
			zoneBuilderBlueprint.Class = reader.ReadString();
			int num = reader.ReadOptimizedInt32();
			for (int i = 0; i < num; i++)
			{
				zoneBuilderBlueprint.AddParameter(reader.ReadString(), reader.ReadObject());
			}
			return zoneBuilderBlueprint;
		}

		// Token: 0x06006442 RID: 25666 RVA: 0x001EEEE4 File Offset: 0x001ED0E4
		public static ZoneBuilderBlueprint GetSerialized(SerializationReader Reader, int Token = -1)
		{
			string text = Reader.ReadString();
			int num = Reader.ReadOptimizedInt32();
			Dictionary<string, object> parameterBuffer = ZoneBuilderBlueprint.GetParameterBuffer();
			for (int i = 0; i < num; i++)
			{
				parameterBuffer[Reader.ReadString()] = Reader.ReadObject();
			}
			ZoneBuilderBlueprint zoneBuilderBlueprint = ZoneBuilderBlueprint.Get(text, parameterBuffer);
			zoneBuilderBlueprint.Token = Token;
			return zoneBuilderBlueprint;
		}

		// Token: 0x06006443 RID: 25667 RVA: 0x001EEF31 File Offset: 0x001ED131
		public static Dictionary<string, object> GetParameterBuffer()
		{
			ZoneBuilderBlueprint.ParameterBuffer.Clear();
			return ZoneBuilderBlueprint.ParameterBuffer;
		}

		// Token: 0x06006444 RID: 25668 RVA: 0x001EEF44 File Offset: 0x001ED144
		public void Save(SerializationWriter writer)
		{
			writer.Write(this.Class);
			if (this.Parameters == null)
			{
				writer.WriteOptimized(0);
				return;
			}
			writer.WriteOptimized(this.Parameters.Count);
			foreach (KeyValuePair<string, object> keyValuePair in this.Parameters)
			{
				writer.Write(keyValuePair.Key);
				writer.WriteObject(keyValuePair.Value);
			}
		}

		// Token: 0x06006445 RID: 25669 RVA: 0x001EEFD8 File Offset: 0x001ED1D8
		public void SaveTokenized(SerializationWriter Writer)
		{
			if (this.Token >= 0)
			{
				Writer.WriteOptimized(this.Token);
				return;
			}
			this.Token = ZoneBuilderBlueprint.Sequence++;
			Writer.WriteOptimized(this.Token);
			this.Save(Writer);
		}

		// Token: 0x06006446 RID: 25670 RVA: 0x001EF018 File Offset: 0x001ED218
		public static void ClearTokens()
		{
			ZoneBuilderBlueprint.Sequence = 0;
			foreach (ZoneBuilderBlueprint zoneBuilderBlueprint in ZoneBuilderBlueprint.Repository.Values)
			{
				zoneBuilderBlueprint.Token = -1;
			}
		}

		// Token: 0x06006447 RID: 25671 RVA: 0x001EF074 File Offset: 0x001ED274
		public static void ClearOrphaned()
		{
			ZoneBuilderBlueprint.Repository.RemoveAll<int, ZoneBuilderBlueprint>((KeyValuePair<int, ZoneBuilderBlueprint> x) => x.Value.Token == -1);
		}

		// Token: 0x06006448 RID: 25672 RVA: 0x001EF0A0 File Offset: 0x001ED2A0
		public object Create()
		{
			string text = "XRL.World.ZoneBuilders." + this.Class;
			Type type = ModManager.ResolveType(text, false, false, true);
			if (type == null)
			{
				MetricsManager.LogError("Unknown builder " + text + "!");
				return true;
			}
			object obj = Activator.CreateInstance(type);
			if (this.Parameters.IsNullOrEmpty<KeyValuePair<string, object>>())
			{
				return obj;
			}
			foreach (KeyValuePair<string, object> keyValuePair in this.Parameters)
			{
				try
				{
					FieldInfo field = type.GetField(keyValuePair.Key, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
					if (field != null && !field.IsInitOnly)
					{
						if (keyValuePair.Value is IConvertible)
						{
							field.SetValue(obj, Convert.ChangeType(keyValuePair.Value, field.FieldType));
						}
						else
						{
							field.SetValue(obj, keyValuePair.Value);
						}
					}
					else
					{
						PropertyInfo property = type.GetProperty(keyValuePair.Key, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
						if (property != null && property.CanWrite)
						{
							if (keyValuePair.Value is IConvertible)
							{
								property.SetValue(obj, Convert.ChangeType(keyValuePair.Value, property.PropertyType));
							}
							else
							{
								property.SetValue(obj, keyValuePair.Value);
							}
						}
					}
				}
				catch (Exception ex)
				{
					MetricsManager.LogAssemblyError(type.Assembly, string.Format("Error setting field '{0}.{1}' to '{2}': {3}", new object[] { type.FullName, keyValuePair.Key, keyValuePair.Value, ex }));
				}
			}
			return obj;
		}

		// Token: 0x04002F0A RID: 12042
		public static Dictionary<int, ZoneBuilderBlueprint> Repository = new Dictionary<int, ZoneBuilderBlueprint>();

		// Token: 0x04002F0B RID: 12043
		private static readonly Type[] BuildZoneTypes = new Type[] { typeof(Zone) };

		// Token: 0x04002F0C RID: 12044
		public string Class;

		// Token: 0x04002F0D RID: 12045
		public Dictionary<string, object> Parameters;

		// Token: 0x04002F0E RID: 12046
		[NonSerialized]
		private int Token = -1;

		// Token: 0x04002F0F RID: 12047
		[NonSerialized]
		private static Dictionary<string, object> ParameterBuffer = new Dictionary<string, object>();

		// Token: 0x04002F10 RID: 12048
		[NonSerialized]
		private static int Sequence = 0;
	}
}

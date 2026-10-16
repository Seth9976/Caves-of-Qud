using System;
using System.Collections.Generic;
using System.Reflection;
using XRL.World.ZoneParts;

namespace XRL.World
{
	// Token: 0x02000997 RID: 2455
	[Serializable]
	public class ZonePartBlueprint : IEquatable<ZonePartBlueprint>
	{
		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x060069EE RID: 27118 RVA: 0x00217D56 File Offset: 0x00215F56
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

		// Token: 0x060069EF RID: 27119 RVA: 0x00217D6D File Offset: 0x00215F6D
		public ZonePartBlueprint()
		{
		}

		// Token: 0x060069F0 RID: 27120 RVA: 0x00217D7C File Offset: 0x00215F7C
		public ZonePartBlueprint(string Name = null, Dictionary<string, object> Parameters = null)
			: this()
		{
			this.Name = Name;
			this.Parameters = Parameters;
		}

		// Token: 0x060069F1 RID: 27121 RVA: 0x00217D92 File Offset: 0x00215F92
		public static ZonePartBlueprint Get(string Name)
		{
			return ZonePartBlueprint.Get(Name, ZonePartBlueprint.GetParameterBuffer());
		}

		// Token: 0x060069F2 RID: 27122 RVA: 0x00217DA0 File Offset: 0x00215FA0
		public static ZonePartBlueprint Get(string Name, params object[] Parameters)
		{
			int num = ZonePartBlueprint.HashCodeOf(Name, Parameters);
			ZonePartBlueprint zonePartBlueprint;
			if (!ZonePartBlueprint.Repository.TryGetValue(num, out zonePartBlueprint))
			{
				zonePartBlueprint = (ZonePartBlueprint.Repository[num] = new ZonePartBlueprint(Name, null));
				zonePartBlueprint.SetParameters(Parameters);
			}
			else if (zonePartBlueprint.Name != Name || zonePartBlueprint.ParameterCount != Parameters.Length / 2)
			{
				ZonePartBlueprint zonePartBlueprint2 = new ZonePartBlueprint(Name, null);
				zonePartBlueprint2.SetParameters(Parameters);
				MetricsManager.LogWarning(string.Format("ZonePartBlueprint hash collision: [{0}] x [{1}]", zonePartBlueprint2, zonePartBlueprint));
				zonePartBlueprint = zonePartBlueprint2;
			}
			return zonePartBlueprint;
		}

		// Token: 0x060069F3 RID: 27123 RVA: 0x00217E20 File Offset: 0x00216020
		public static ZonePartBlueprint Get(string Name, string Key1, object Value1)
		{
			Dictionary<string, object> parameterBuffer = ZonePartBlueprint.GetParameterBuffer();
			parameterBuffer[Key1] = Value1;
			return ZonePartBlueprint.Get(Name, parameterBuffer);
		}

		// Token: 0x060069F4 RID: 27124 RVA: 0x00217E44 File Offset: 0x00216044
		public static ZonePartBlueprint Get(string Name, string Key1, object Value1, string Key2, object Value2)
		{
			Dictionary<string, object> parameterBuffer = ZonePartBlueprint.GetParameterBuffer();
			parameterBuffer[Key1] = Value1;
			parameterBuffer[Key2] = Value2;
			return ZonePartBlueprint.Get(Name, parameterBuffer);
		}

		// Token: 0x060069F5 RID: 27125 RVA: 0x00217E70 File Offset: 0x00216070
		public static ZonePartBlueprint Get(string Name, string Key1, object Value1, string Key2, object Value2, string Key3, object Value3)
		{
			Dictionary<string, object> parameterBuffer = ZonePartBlueprint.GetParameterBuffer();
			parameterBuffer[Key1] = Value1;
			parameterBuffer[Key2] = Value2;
			parameterBuffer[Key3] = Value3;
			return ZonePartBlueprint.Get(Name, parameterBuffer);
		}

		// Token: 0x060069F6 RID: 27126 RVA: 0x00217EA8 File Offset: 0x002160A8
		public static ZonePartBlueprint Get(string Name, string Key1, object Value1, string Key2, object Value2, string Key3, object Value3, string Key4, object Value4)
		{
			Dictionary<string, object> parameterBuffer = ZonePartBlueprint.GetParameterBuffer();
			parameterBuffer[Key1] = Value1;
			parameterBuffer[Key2] = Value2;
			parameterBuffer[Key3] = Value3;
			parameterBuffer[Key4] = Value4;
			return ZonePartBlueprint.Get(Name, parameterBuffer);
		}

		// Token: 0x060069F7 RID: 27127 RVA: 0x00217EE8 File Offset: 0x002160E8
		public static ZonePartBlueprint Get(string Name, string Key1, object Value1, string Key2, object Value2, string Key3, object Value3, string Key4, object Value4, string Key5, object Value5)
		{
			Dictionary<string, object> parameterBuffer = ZonePartBlueprint.GetParameterBuffer();
			parameterBuffer[Key1] = Value1;
			parameterBuffer[Key2] = Value2;
			parameterBuffer[Key3] = Value3;
			parameterBuffer[Key4] = Value4;
			parameterBuffer[Key5] = Value5;
			return ZonePartBlueprint.Get(Name, parameterBuffer);
		}

		// Token: 0x060069F8 RID: 27128 RVA: 0x00217F34 File Offset: 0x00216134
		public static ZonePartBlueprint Get(string Name, string Key1, object Value1, string Key2, object Value2, string Key3, object Value3, string Key4, object Value4, string Key5, object Value5, string Key6, object Value6)
		{
			Dictionary<string, object> parameterBuffer = ZonePartBlueprint.GetParameterBuffer();
			parameterBuffer[Key1] = Value1;
			parameterBuffer[Key2] = Value2;
			parameterBuffer[Key3] = Value3;
			parameterBuffer[Key4] = Value4;
			parameterBuffer[Key5] = Value5;
			parameterBuffer[Key6] = Value6;
			return ZonePartBlueprint.Get(Name, parameterBuffer);
		}

		// Token: 0x060069F9 RID: 27129 RVA: 0x00217F88 File Offset: 0x00216188
		public static ZonePartBlueprint Get(string Name, Dictionary<string, object> Parameters)
		{
			int num = ZonePartBlueprint.HashCodeOf(Name, Parameters);
			ZonePartBlueprint zonePartBlueprint;
			if (!ZonePartBlueprint.Repository.TryGetValue(num, out zonePartBlueprint))
			{
				zonePartBlueprint = (ZonePartBlueprint.Repository[num] = new ZonePartBlueprint(Name, null));
				zonePartBlueprint.Parameters = new Dictionary<string, object>(Parameters);
			}
			else if (zonePartBlueprint.Name != Name || zonePartBlueprint.ParameterCount != ((Parameters != null) ? Parameters.Count : 0))
			{
				ZonePartBlueprint zonePartBlueprint2 = new ZonePartBlueprint(Name, null);
				zonePartBlueprint.Parameters = new Dictionary<string, object>(Parameters);
				MetricsManager.LogWarning(string.Format("ZonePartBlueprint hash collision: [{0}] x [{1}]", zonePartBlueprint2, zonePartBlueprint));
				zonePartBlueprint = zonePartBlueprint2;
			}
			return zonePartBlueprint;
		}

		// Token: 0x060069FA RID: 27130 RVA: 0x00218018 File Offset: 0x00216218
		public void SetParameter(string Name, object Val)
		{
			if (this.Parameters == null)
			{
				this.Parameters = new Dictionary<string, object>();
			}
			this.Parameters[Name] = Val;
		}

		// Token: 0x060069FB RID: 27131 RVA: 0x0021803C File Offset: 0x0021623C
		public void SetParameters(object[] Parameters)
		{
			if (Parameters.IsNullOrEmpty<object>())
			{
				return;
			}
			for (int i = 0; i < Parameters.Length; i += 2)
			{
				this.SetParameter((string)Parameters[i], Parameters[i + 1]);
			}
		}

		// Token: 0x060069FC RID: 27132 RVA: 0x00218073 File Offset: 0x00216273
		public object GetParameter(string Key, object Default = null)
		{
			Dictionary<string, object> parameters = this.Parameters;
			if (parameters == null)
			{
				return null;
			}
			return parameters.GetValue(Key, Default);
		}

		// Token: 0x060069FD RID: 27133 RVA: 0x00218088 File Offset: 0x00216288
		public T GetParameter<T>(string Key, T Default = default(T))
		{
			Dictionary<string, object> parameters = this.Parameters;
			object obj = ((parameters != null) ? parameters.GetValue(Key, Default) : null);
			if (obj is T)
			{
				return (T)((object)obj);
			}
			return Default;
		}

		// Token: 0x060069FE RID: 27134 RVA: 0x002180C1 File Offset: 0x002162C1
		public int GetFullHashCode()
		{
			return ZonePartBlueprint.HashCodeOf(this.Name, this.Parameters);
		}

		// Token: 0x060069FF RID: 27135 RVA: 0x002180D4 File Offset: 0x002162D4
		protected static int HashCodeOf(string Name, Dictionary<string, object> Parameters = null)
		{
			int num = 17;
			if (!Name.IsNullOrEmpty())
			{
				num = num * 23 + Name.GetHashCode();
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

		// Token: 0x06006A00 RID: 27136 RVA: 0x00218168 File Offset: 0x00216368
		protected static int HashCodeOf(string Name, object[] Parameters = null)
		{
			int num = 17;
			if (!Name.IsNullOrEmpty())
			{
				num = num * 23 + Name.GetHashCode();
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

		// Token: 0x06006A01 RID: 27137 RVA: 0x002181BC File Offset: 0x002163BC
		public bool Equals(ZonePartBlueprint Other)
		{
			if (Other == null)
			{
				return false;
			}
			if (this == Other)
			{
				return true;
			}
			if (Other.Name != this.Name)
			{
				return false;
			}
			if (this.Parameters.IsNullOrEmpty<KeyValuePair<string, object>>())
			{
				return Other.Parameters.IsNullOrEmpty<KeyValuePair<string, object>>();
			}
			if (Other.Parameters.IsNullOrEmpty<KeyValuePair<string, object>>() || this.Parameters.Count != Other.Parameters.Count)
			{
				return false;
			}
			foreach (KeyValuePair<string, object> keyValuePair in this.Parameters)
			{
				object obj;
				if (!Other.Parameters.TryGetValue(keyValuePair.Key, out obj) || !obj.Equals(keyValuePair.Value))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06006A02 RID: 27138 RVA: 0x00218294 File Offset: 0x00216494
		public override string ToString()
		{
			return "ZonePart<" + this.Name + ">";
		}

		/// <returns>Boolean indicating whether execution should continue (true) or stopped and retried from start (false).</returns>
		// Token: 0x06006A03 RID: 27139 RVA: 0x002182AB File Offset: 0x002164AB
		public bool ApplyTo(Zone Zone)
		{
			if (this.Name.IsNullOrEmpty())
			{
				MetricsManager.LogError("Part class is null or empty for: " + Zone.ZoneID);
				return true;
			}
			Zone.AddPart(this.Create(), true);
			return true;
		}

		// Token: 0x06006A04 RID: 27140 RVA: 0x002182E0 File Offset: 0x002164E0
		public static ZonePartBlueprint Load(SerializationReader reader)
		{
			ZonePartBlueprint zonePartBlueprint = new ZonePartBlueprint();
			zonePartBlueprint.Name = reader.ReadString();
			int num = reader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				zonePartBlueprint.SetParameter(reader.ReadString(), reader.ReadObject());
			}
			return zonePartBlueprint;
		}

		// Token: 0x06006A05 RID: 27141 RVA: 0x00218328 File Offset: 0x00216528
		public static ZonePartBlueprint GetSerialized(SerializationReader Reader, int Token = -1)
		{
			string text = Reader.ReadString();
			int num = Reader.ReadInt32();
			Dictionary<string, object> parameterBuffer = ZonePartBlueprint.GetParameterBuffer();
			for (int i = 0; i < num; i++)
			{
				parameterBuffer[Reader.ReadString()] = Reader.ReadObject();
			}
			ZonePartBlueprint zonePartBlueprint = ZonePartBlueprint.Get(text, parameterBuffer);
			zonePartBlueprint.Token = Token;
			return zonePartBlueprint;
		}

		// Token: 0x06006A06 RID: 27142 RVA: 0x00218375 File Offset: 0x00216575
		public static Dictionary<string, object> GetParameterBuffer()
		{
			ZonePartBlueprint.ParameterBuffer.Clear();
			return ZonePartBlueprint.ParameterBuffer;
		}

		// Token: 0x06006A07 RID: 27143 RVA: 0x00218388 File Offset: 0x00216588
		public void Save(SerializationWriter writer)
		{
			writer.Write(this.Name);
			if (this.Parameters == null)
			{
				writer.Write(-1);
				return;
			}
			writer.Write(this.Parameters.Count);
			foreach (KeyValuePair<string, object> keyValuePair in this.Parameters)
			{
				writer.Write(keyValuePair.Key);
				writer.WriteObject(keyValuePair.Value);
			}
		}

		// Token: 0x06006A08 RID: 27144 RVA: 0x0021841C File Offset: 0x0021661C
		public void SaveTokenized(SerializationWriter Writer)
		{
			if (this.Token >= 0)
			{
				Writer.Write(this.Token);
				return;
			}
			this.Token = ZonePartBlueprint.Sequence++;
			Writer.Write(this.Token);
			this.Save(Writer);
		}

		// Token: 0x06006A09 RID: 27145 RVA: 0x0021845C File Offset: 0x0021665C
		public static void ClearTokens()
		{
			ZonePartBlueprint.Sequence = 0;
			foreach (ZonePartBlueprint zonePartBlueprint in ZonePartBlueprint.Repository.Values)
			{
				zonePartBlueprint.Token = -1;
			}
		}

		// Token: 0x06006A0A RID: 27146 RVA: 0x002184B8 File Offset: 0x002166B8
		public static void ClearOrphaned()
		{
			ZonePartBlueprint.Repository.RemoveAll<int, ZonePartBlueprint>((KeyValuePair<int, ZonePartBlueprint> x) => x.Value.Token == -1);
		}

		// Token: 0x06006A0B RID: 27147 RVA: 0x002184E4 File Offset: 0x002166E4
		public IZonePart Create()
		{
			string text = "XRL.World.ZoneParts." + this.Name;
			Type type = ModManager.ResolveType(text, false, false, true);
			if (type == null)
			{
				MetricsManager.LogError("Unknown zone part " + text + "!");
				return null;
			}
			IZonePart zonePart = Activator.CreateInstance(type) as IZonePart;
			if (this.Parameters.IsNullOrEmpty<KeyValuePair<string, object>>())
			{
				return zonePart;
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
							field.SetValue(zonePart, Convert.ChangeType(keyValuePair.Value, field.FieldType));
						}
						else
						{
							field.SetValue(zonePart, keyValuePair.Value);
						}
					}
					else
					{
						PropertyInfo property = type.GetProperty(keyValuePair.Key, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
						if (property != null && property.CanWrite)
						{
							if (keyValuePair.Value is IConvertible)
							{
								property.SetValue(zonePart, Convert.ChangeType(keyValuePair.Value, property.PropertyType));
							}
							else
							{
								property.SetValue(zonePart, keyValuePair.Value);
							}
						}
					}
				}
				catch (Exception ex)
				{
					MetricsManager.LogAssemblyError(type.Assembly, string.Format("Error setting field '{0}.{1}' to '{2}': {3}", new object[] { type.FullName, keyValuePair.Key, keyValuePair.Value, ex }));
				}
			}
			return zonePart;
		}

		// Token: 0x04003080 RID: 12416
		public static Dictionary<int, ZonePartBlueprint> Repository = new Dictionary<int, ZonePartBlueprint>();

		// Token: 0x04003081 RID: 12417
		public string Name;

		// Token: 0x04003082 RID: 12418
		public Dictionary<string, object> Parameters;

		// Token: 0x04003083 RID: 12419
		[NonSerialized]
		private int Token = -1;

		// Token: 0x04003084 RID: 12420
		[NonSerialized]
		private static Dictionary<string, object> ParameterBuffer = new Dictionary<string, object>();

		// Token: 0x04003085 RID: 12421
		[NonSerialized]
		private static int Sequence = 0;
	}
}

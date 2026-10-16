using System;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Layouts
{
	// Token: 0x02000113 RID: 275
	[Serializable]
	public struct InputDeviceDescription : IEquatable<InputDeviceDescription>
	{
		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06000FEB RID: 4075 RVA: 0x0004DE66 File Offset: 0x0004C066
		// (set) Token: 0x06000FEC RID: 4076 RVA: 0x0004DE6E File Offset: 0x0004C06E
		public string interfaceName
		{
			get
			{
				return this.m_InterfaceName;
			}
			set
			{
				this.m_InterfaceName = value;
			}
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06000FED RID: 4077 RVA: 0x0004DE77 File Offset: 0x0004C077
		// (set) Token: 0x06000FEE RID: 4078 RVA: 0x0004DE7F File Offset: 0x0004C07F
		public string deviceClass
		{
			get
			{
				return this.m_DeviceClass;
			}
			set
			{
				this.m_DeviceClass = value;
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06000FEF RID: 4079 RVA: 0x0004DE88 File Offset: 0x0004C088
		// (set) Token: 0x06000FF0 RID: 4080 RVA: 0x0004DE90 File Offset: 0x0004C090
		public string manufacturer
		{
			get
			{
				return this.m_Manufacturer;
			}
			set
			{
				this.m_Manufacturer = value;
			}
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06000FF1 RID: 4081 RVA: 0x0004DE99 File Offset: 0x0004C099
		// (set) Token: 0x06000FF2 RID: 4082 RVA: 0x0004DEA1 File Offset: 0x0004C0A1
		public string product
		{
			get
			{
				return this.m_Product;
			}
			set
			{
				this.m_Product = value;
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06000FF3 RID: 4083 RVA: 0x0004DEAA File Offset: 0x0004C0AA
		// (set) Token: 0x06000FF4 RID: 4084 RVA: 0x0004DEB2 File Offset: 0x0004C0B2
		public string serial
		{
			get
			{
				return this.m_Serial;
			}
			set
			{
				this.m_Serial = value;
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06000FF5 RID: 4085 RVA: 0x0004DEBB File Offset: 0x0004C0BB
		// (set) Token: 0x06000FF6 RID: 4086 RVA: 0x0004DEC3 File Offset: 0x0004C0C3
		public string version
		{
			get
			{
				return this.m_Version;
			}
			set
			{
				this.m_Version = value;
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06000FF7 RID: 4087 RVA: 0x0004DECC File Offset: 0x0004C0CC
		// (set) Token: 0x06000FF8 RID: 4088 RVA: 0x0004DED4 File Offset: 0x0004C0D4
		public string capabilities
		{
			get
			{
				return this.m_Capabilities;
			}
			set
			{
				this.m_Capabilities = value;
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06000FF9 RID: 4089 RVA: 0x0004DEE0 File Offset: 0x0004C0E0
		public bool empty
		{
			get
			{
				return string.IsNullOrEmpty(this.m_InterfaceName) && string.IsNullOrEmpty(this.m_DeviceClass) && string.IsNullOrEmpty(this.m_Manufacturer) && string.IsNullOrEmpty(this.m_Product) && string.IsNullOrEmpty(this.m_Serial) && string.IsNullOrEmpty(this.m_Version) && string.IsNullOrEmpty(this.m_Capabilities);
			}
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x0004DF48 File Offset: 0x0004C148
		public override string ToString()
		{
			bool flag = !string.IsNullOrEmpty(this.product);
			bool flag2 = !string.IsNullOrEmpty(this.manufacturer);
			bool flag3 = !string.IsNullOrEmpty(this.interfaceName);
			if (flag && flag2)
			{
				if (flag3)
				{
					return string.Concat(new string[] { this.manufacturer, " ", this.product, " (", this.interfaceName, ")" });
				}
				return this.manufacturer + " " + this.product;
			}
			else if (flag)
			{
				if (flag3)
				{
					return this.product + " (" + this.interfaceName + ")";
				}
				return this.product;
			}
			else if (!string.IsNullOrEmpty(this.deviceClass))
			{
				if (flag3)
				{
					return this.deviceClass + " (" + this.interfaceName + ")";
				}
				return this.deviceClass;
			}
			else if (!string.IsNullOrEmpty(this.capabilities))
			{
				string text = this.capabilities;
				if (this.capabilities.Length > 40)
				{
					text = text.Substring(0, 40) + "...";
				}
				if (flag3)
				{
					return text + " (" + this.interfaceName + ")";
				}
				return text;
			}
			else
			{
				if (flag3)
				{
					return this.interfaceName;
				}
				return "<Empty Device Description>";
			}
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x0004E0A0 File Offset: 0x0004C2A0
		public bool Equals(InputDeviceDescription other)
		{
			return this.m_InterfaceName.InvariantEqualsIgnoreCase(other.m_InterfaceName) && this.m_DeviceClass.InvariantEqualsIgnoreCase(other.m_DeviceClass) && this.m_Manufacturer.InvariantEqualsIgnoreCase(other.m_Manufacturer) && this.m_Product.InvariantEqualsIgnoreCase(other.m_Product) && this.m_Serial.InvariantEqualsIgnoreCase(other.m_Serial) && this.m_Version.InvariantEqualsIgnoreCase(other.m_Version) && this.m_Capabilities.InvariantEqualsIgnoreCase(other.m_Capabilities);
		}

		// Token: 0x06000FFC RID: 4092 RVA: 0x0004E134 File Offset: 0x0004C334
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is InputDeviceDescription)
			{
				InputDeviceDescription inputDeviceDescription = (InputDeviceDescription)obj;
				return this.Equals(inputDeviceDescription);
			}
			return false;
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x0004E160 File Offset: 0x0004C360
		public override int GetHashCode()
		{
			return (((((((((((((this.m_InterfaceName != null) ? this.m_InterfaceName.GetHashCode() : 0) * 397) ^ ((this.m_DeviceClass != null) ? this.m_DeviceClass.GetHashCode() : 0)) * 397) ^ ((this.m_Manufacturer != null) ? this.m_Manufacturer.GetHashCode() : 0)) * 397) ^ ((this.m_Product != null) ? this.m_Product.GetHashCode() : 0)) * 397) ^ ((this.m_Serial != null) ? this.m_Serial.GetHashCode() : 0)) * 397) ^ ((this.m_Version != null) ? this.m_Version.GetHashCode() : 0)) * 397) ^ ((this.m_Capabilities != null) ? this.m_Capabilities.GetHashCode() : 0);
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x0004E231 File Offset: 0x0004C431
		public static bool operator ==(InputDeviceDescription left, InputDeviceDescription right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x0004E23B File Offset: 0x0004C43B
		public static bool operator !=(InputDeviceDescription left, InputDeviceDescription right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x0004E248 File Offset: 0x0004C448
		public string ToJson()
		{
			return JsonUtility.ToJson(new InputDeviceDescription.DeviceDescriptionJson
			{
				@interface = this.interfaceName,
				type = this.deviceClass,
				product = this.product,
				manufacturer = this.manufacturer,
				serial = this.serial,
				version = this.version,
				capabilities = this.capabilities
			}, true);
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x0004E2C4 File Offset: 0x0004C4C4
		public static InputDeviceDescription FromJson(string json)
		{
			if (json == null)
			{
				throw new ArgumentNullException("json");
			}
			InputDeviceDescription.DeviceDescriptionJson deviceDescriptionJson = JsonUtility.FromJson<InputDeviceDescription.DeviceDescriptionJson>(json);
			return new InputDeviceDescription
			{
				interfaceName = deviceDescriptionJson.@interface,
				deviceClass = deviceDescriptionJson.type,
				product = deviceDescriptionJson.product,
				manufacturer = deviceDescriptionJson.manufacturer,
				serial = deviceDescriptionJson.serial,
				version = deviceDescriptionJson.version,
				capabilities = deviceDescriptionJson.capabilities
			};
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x0004E34C File Offset: 0x0004C54C
		internal static bool ComparePropertyToDeviceDescriptor(string propertyName, JsonParser.JsonString propertyValue, string deviceDescriptor)
		{
			JsonParser jsonParser = new JsonParser(deviceDescriptor);
			if (!jsonParser.NavigateToProperty(propertyName))
			{
				return propertyValue.text.isEmpty;
			}
			return jsonParser.CurrentPropertyHasValueEqualTo(propertyValue);
		}

		// Token: 0x040006A4 RID: 1700
		[SerializeField]
		private string m_InterfaceName;

		// Token: 0x040006A5 RID: 1701
		[SerializeField]
		private string m_DeviceClass;

		// Token: 0x040006A6 RID: 1702
		[SerializeField]
		private string m_Manufacturer;

		// Token: 0x040006A7 RID: 1703
		[SerializeField]
		private string m_Product;

		// Token: 0x040006A8 RID: 1704
		[SerializeField]
		private string m_Serial;

		// Token: 0x040006A9 RID: 1705
		[SerializeField]
		private string m_Version;

		// Token: 0x040006AA RID: 1706
		[SerializeField]
		private string m_Capabilities;

		// Token: 0x02000237 RID: 567
		private struct DeviceDescriptionJson
		{
			// Token: 0x04000C3F RID: 3135
			public string @interface;

			// Token: 0x04000C40 RID: 3136
			public string type;

			// Token: 0x04000C41 RID: 3137
			public string product;

			// Token: 0x04000C42 RID: 3138
			public string serial;

			// Token: 0x04000C43 RID: 3139
			public string version;

			// Token: 0x04000C44 RID: 3140
			public string manufacturer;

			// Token: 0x04000C45 RID: 3141
			public string capabilities;
		}
	}
}

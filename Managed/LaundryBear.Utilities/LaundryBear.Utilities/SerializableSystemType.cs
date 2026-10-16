using System;
using UnityEngine;

namespace LaundryBear
{
	// Token: 0x02000011 RID: 17
	[Serializable]
	public class SerializableSystemType
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002D10 File Offset: 0x00000F10
		public string Name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002D18 File Offset: 0x00000F18
		public string AssemblyQualifiedName
		{
			get
			{
				return this.m_AssemblyQualifiedName;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002D20 File Offset: 0x00000F20
		public string AssemblyName
		{
			get
			{
				return this.m_AssemblyName;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002D28 File Offset: 0x00000F28
		public Type SystemType
		{
			get
			{
				if (this.m_SystemType == null)
				{
					this.GetSystemType();
				}
				return this.m_SystemType;
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002D44 File Offset: 0x00000F44
		private void GetSystemType()
		{
			this.m_SystemType = Type.GetType(this.m_AssemblyQualifiedName);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002D57 File Offset: 0x00000F57
		public SerializableSystemType(Type _SystemType)
		{
			this.m_SystemType = _SystemType;
			this.m_Name = _SystemType.Name;
			this.m_AssemblyQualifiedName = _SystemType.AssemblyQualifiedName;
			this.m_AssemblyName = _SystemType.Assembly.FullName;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002D90 File Offset: 0x00000F90
		public override bool Equals(object obj)
		{
			SerializableSystemType serializableSystemType = obj as SerializableSystemType;
			return serializableSystemType != null && this.Equals(serializableSystemType);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002DB0 File Offset: 0x00000FB0
		public bool Equals(SerializableSystemType _Object)
		{
			return _Object.SystemType.Equals(this.SystemType);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002DC3 File Offset: 0x00000FC3
		public override int GetHashCode()
		{
			return this.SystemType.GetHashCode();
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00002DD0 File Offset: 0x00000FD0
		public static bool operator ==(SerializableSystemType a, SerializableSystemType b)
		{
			return a == b || (a != null && b != null && a.Equals(b));
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002DE7 File Offset: 0x00000FE7
		public static bool operator !=(SerializableSystemType a, SerializableSystemType b)
		{
			return !(a == b);
		}

		// Token: 0x04000020 RID: 32
		[SerializeField]
		private string m_Name;

		// Token: 0x04000021 RID: 33
		[SerializeField]
		private string m_AssemblyQualifiedName;

		// Token: 0x04000022 RID: 34
		[SerializeField]
		private string m_AssemblyName;

		// Token: 0x04000023 RID: 35
		private Type m_SystemType;
	}
}

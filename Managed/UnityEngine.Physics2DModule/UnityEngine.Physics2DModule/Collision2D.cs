using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200001A RID: 26
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class Collision2D
	{
		// Token: 0x06000251 RID: 593 RVA: 0x00007998 File Offset: 0x00005B98
		private ContactPoint2D[] GetContacts_Internal()
		{
			return (this.m_LegacyContacts == null) ? this.m_ReusedContacts : this.m_LegacyContacts;
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000252 RID: 594 RVA: 0x000079C0 File Offset: 0x00005BC0
		public Collider2D collider
		{
			get
			{
				return Object.FindObjectFromInstanceID(this.m_Collider) as Collider2D;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000253 RID: 595 RVA: 0x000079E4 File Offset: 0x00005BE4
		public Collider2D otherCollider
		{
			get
			{
				return Object.FindObjectFromInstanceID(this.m_OtherCollider) as Collider2D;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000254 RID: 596 RVA: 0x00007A08 File Offset: 0x00005C08
		public Rigidbody2D rigidbody
		{
			get
			{
				return Object.FindObjectFromInstanceID(this.m_Rigidbody) as Rigidbody2D;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000255 RID: 597 RVA: 0x00007A2C File Offset: 0x00005C2C
		public Rigidbody2D otherRigidbody
		{
			get
			{
				return Object.FindObjectFromInstanceID(this.m_OtherRigidbody) as Rigidbody2D;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000256 RID: 598 RVA: 0x00007A50 File Offset: 0x00005C50
		public Transform transform
		{
			get
			{
				return (this.rigidbody != null) ? this.rigidbody.transform : this.collider.transform;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000257 RID: 599 RVA: 0x00007A88 File Offset: 0x00005C88
		public GameObject gameObject
		{
			get
			{
				return (this.rigidbody != null) ? this.rigidbody.gameObject : this.collider.gameObject;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000258 RID: 600 RVA: 0x00007AC0 File Offset: 0x00005CC0
		public Vector2 relativeVelocity
		{
			get
			{
				return this.m_RelativeVelocity;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000259 RID: 601 RVA: 0x00007AD8 File Offset: 0x00005CD8
		public bool enabled
		{
			get
			{
				return this.m_Enabled == 1;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600025A RID: 602 RVA: 0x00007AF4 File Offset: 0x00005CF4
		public ContactPoint2D[] contacts
		{
			get
			{
				bool flag = this.m_LegacyContacts == null;
				if (flag)
				{
					this.m_LegacyContacts = new ContactPoint2D[this.m_ContactCount];
					Array.Copy(this.m_ReusedContacts, this.m_LegacyContacts, this.m_ContactCount);
				}
				return this.m_LegacyContacts;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600025B RID: 603 RVA: 0x00007B44 File Offset: 0x00005D44
		public int contactCount
		{
			get
			{
				return this.m_ContactCount;
			}
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00007B5C File Offset: 0x00005D5C
		public ContactPoint2D GetContact(int index)
		{
			bool flag = index < 0 || index >= this.m_ContactCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Cannot get contact at index {0}. There are {1} contact(s).", index, this.m_ContactCount));
			}
			return this.GetContacts_Internal()[index];
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00007BB4 File Offset: 0x00005DB4
		public int GetContacts(ContactPoint2D[] contacts)
		{
			bool flag = contacts == null;
			if (flag)
			{
				throw new NullReferenceException("Cannot get contacts as the provided array is NULL.");
			}
			int num = Mathf.Min(this.m_ContactCount, contacts.Length);
			Array.Copy(this.GetContacts_Internal(), contacts, num);
			return num;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00007BF8 File Offset: 0x00005DF8
		public int GetContacts(List<ContactPoint2D> contacts)
		{
			bool flag = contacts == null;
			if (flag)
			{
				throw new NullReferenceException("Cannot get contacts as the provided list is NULL.");
			}
			contacts.Clear();
			ContactPoint2D[] contacts_Internal = this.GetContacts_Internal();
			for (int i = 0; i < this.m_ContactCount; i++)
			{
				contacts.Add(contacts_Internal[i]);
			}
			return this.m_ContactCount;
		}

		// Token: 0x04000071 RID: 113
		internal int m_Collider;

		// Token: 0x04000072 RID: 114
		internal int m_OtherCollider;

		// Token: 0x04000073 RID: 115
		internal int m_Rigidbody;

		// Token: 0x04000074 RID: 116
		internal int m_OtherRigidbody;

		// Token: 0x04000075 RID: 117
		internal Vector2 m_RelativeVelocity;

		// Token: 0x04000076 RID: 118
		internal int m_Enabled;

		// Token: 0x04000077 RID: 119
		internal int m_ContactCount;

		// Token: 0x04000078 RID: 120
		internal ContactPoint2D[] m_ReusedContacts;

		// Token: 0x04000079 RID: 121
		internal ContactPoint2D[] m_LegacyContacts;
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace UnityEngine
{
	// Token: 0x02000003 RID: 3
	public class Collision
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002120 File Offset: 0x00000320
		public Vector3 impulse
		{
			get
			{
				return this.m_Pair.impulseSum;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000009 RID: 9 RVA: 0x0000212D File Offset: 0x0000032D
		public Vector3 relativeVelocity
		{
			get
			{
				return this.m_Flipped ? this.m_Header.m_RelativeVelocity : (-this.m_Header.m_RelativeVelocity);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002154 File Offset: 0x00000354
		public Rigidbody rigidbody
		{
			get
			{
				return this.body as Rigidbody;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002161 File Offset: 0x00000361
		public ArticulationBody articulationBody
		{
			get
			{
				return this.body as ArticulationBody;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000216E File Offset: 0x0000036E
		public Component body
		{
			get
			{
				return this.m_Flipped ? this.m_Header.body : this.m_Header.otherBody;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002190 File Offset: 0x00000390
		public Collider collider
		{
			get
			{
				return this.m_Flipped ? this.m_Pair.collider : this.m_Pair.otherCollider;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000021B4 File Offset: 0x000003B4
		public Transform transform
		{
			get
			{
				return (this.rigidbody != null) ? this.rigidbody.transform : this.collider.transform;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000021EC File Offset: 0x000003EC
		public GameObject gameObject
		{
			get
			{
				return (this.body != null) ? this.body.gameObject : this.collider.gameObject;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002224 File Offset: 0x00000424
		// (set) Token: 0x06000011 RID: 17 RVA: 0x0000223C File Offset: 0x0000043C
		internal bool Flipped
		{
			get
			{
				return this.m_Flipped;
			}
			set
			{
				this.m_Flipped = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002248 File Offset: 0x00000448
		public int contactCount
		{
			get
			{
				return (int)this.m_Pair.m_NbPoints;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002268 File Offset: 0x00000468
		public ContactPoint[] contacts
		{
			get
			{
				bool flag = this.m_LegacyContacts == null;
				if (flag)
				{
					this.m_LegacyContacts = new ContactPoint[this.m_Pair.m_NbPoints];
					this.m_Pair.ExtractContactsArray(this.m_LegacyContacts, this.m_Flipped);
				}
				return this.m_LegacyContacts;
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022BD File Offset: 0x000004BD
		public Collision()
		{
			this.m_Header = default(ContactPairHeader);
			this.m_Pair = default(ContactPair);
			this.m_Flipped = false;
			this.m_LegacyContacts = null;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000022F4 File Offset: 0x000004F4
		internal Collision(in ContactPairHeader header, in ContactPair pair, bool flipped)
		{
			this.m_LegacyContacts = new ContactPoint[pair.m_NbPoints];
			pair.ExtractContactsArray(this.m_LegacyContacts, flipped);
			this.m_Header = header;
			this.m_Pair = pair;
			this.m_Flipped = flipped;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000234E File Offset: 0x0000054E
		internal void Reuse(in ContactPairHeader header, in ContactPair pair)
		{
			this.m_Header = header;
			this.m_Pair = pair;
			this.m_LegacyContacts = null;
			this.m_Flipped = false;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002378 File Offset: 0x00000578
		public unsafe ContactPoint GetContact(int index)
		{
			bool flag = index < 0 || index >= this.contactCount;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Cannot get contact at index {0}. There are {1} contact(s).", index, this.contactCount));
			}
			bool flag2 = this.m_LegacyContacts != null;
			ContactPoint contactPoint;
			if (flag2)
			{
				contactPoint = this.m_LegacyContacts[index];
			}
			else
			{
				float num = (this.m_Flipped ? (-1f) : 1f);
				ContactPairPoint* contactPoint_Internal = this.m_Pair.GetContactPoint_Internal(index);
				contactPoint = new ContactPoint(contactPoint_Internal->m_Position, contactPoint_Internal->m_Normal * num, contactPoint_Internal->m_Impulse, contactPoint_Internal->m_Separation, this.m_Flipped ? this.m_Pair.otherColliderInstanceID : this.m_Pair.colliderInstanceID, this.m_Flipped ? this.m_Pair.colliderInstanceID : this.m_Pair.otherColliderInstanceID);
			}
			return contactPoint;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000246C File Offset: 0x0000066C
		public int GetContacts(ContactPoint[] contacts)
		{
			bool flag = contacts == null;
			if (flag)
			{
				throw new NullReferenceException("Cannot get contacts as the provided array is NULL.");
			}
			bool flag2 = this.m_LegacyContacts != null;
			int num2;
			if (flag2)
			{
				int num = Mathf.Min(this.m_LegacyContacts.Length, contacts.Length);
				Array.Copy(this.m_LegacyContacts, contacts, num);
				num2 = num;
			}
			else
			{
				num2 = this.m_Pair.ExtractContactsArray(contacts, this.m_Flipped);
			}
			return num2;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000024D4 File Offset: 0x000006D4
		public int GetContacts(List<ContactPoint> contacts)
		{
			bool flag = contacts == null;
			if (flag)
			{
				throw new NullReferenceException("Cannot get contacts as the provided list is NULL.");
			}
			contacts.Clear();
			bool flag2 = this.m_LegacyContacts != null;
			int num;
			if (flag2)
			{
				contacts.AddRange(this.m_LegacyContacts);
				num = this.m_LegacyContacts.Length;
			}
			else
			{
				int nbPoints = (int)this.m_Pair.m_NbPoints;
				bool flag3 = nbPoints == 0;
				if (flag3)
				{
					num = 0;
				}
				else
				{
					bool flag4 = contacts.Capacity < nbPoints;
					if (flag4)
					{
						contacts.Capacity = nbPoints;
					}
					num = this.m_Pair.ExtractContacts(contacts, this.m_Flipped);
				}
			}
			return num;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x0000256C File Offset: 0x0000076C
		[Obsolete("Do not use Collision.GetEnumerator(), enumerate using non-allocating array returned by Collision.GetContacts() or enumerate using Collision.GetContact(index) instead.", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual IEnumerator GetEnumerator()
		{
			return this.contacts.GetEnumerator();
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000258C File Offset: 0x0000078C
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Use Collision.relativeVelocity instead. (UnityUpgradable) -> relativeVelocity", false)]
		public Vector3 impactForceSum
		{
			get
			{
				return Vector3.zero;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000025A4 File Offset: 0x000007A4
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("Will always return zero.", true)]
		public Vector3 frictionForceSum
		{
			get
			{
				return Vector3.zero;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000025BC File Offset: 0x000007BC
		[Obsolete("Please use Collision.rigidbody, Collision.transform or Collision.collider instead", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public Component other
		{
			get
			{
				return (this.body != null) ? this.body : this.collider;
			}
		}

		// Token: 0x04000007 RID: 7
		private ContactPairHeader m_Header;

		// Token: 0x04000008 RID: 8
		private ContactPair m_Pair;

		// Token: 0x04000009 RID: 9
		private bool m_Flipped;

		// Token: 0x0400000A RID: 10
		private ContactPoint[] m_LegacyContacts = null;
	}
}

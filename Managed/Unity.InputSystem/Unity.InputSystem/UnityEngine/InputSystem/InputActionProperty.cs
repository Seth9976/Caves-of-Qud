using System;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000021 RID: 33
	[Serializable]
	public struct InputActionProperty : IEquatable<InputActionProperty>, IEquatable<InputAction>, IEquatable<InputActionReference>
	{
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00007443 File Offset: 0x00005643
		public InputAction action
		{
			get
			{
				if (!this.m_UseReference)
				{
					return this.m_Action;
				}
				if (!(this.m_Reference != null))
				{
					return null;
				}
				return this.m_Reference.action;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060001FE RID: 510 RVA: 0x0000746F File Offset: 0x0000566F
		public InputActionReference reference
		{
			get
			{
				if (!this.m_UseReference)
				{
					return null;
				}
				return this.m_Reference;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00007481 File Offset: 0x00005681
		internal InputAction serializedAction
		{
			get
			{
				return this.m_Action;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000200 RID: 512 RVA: 0x00007489 File Offset: 0x00005689
		internal InputActionReference serializedReference
		{
			get
			{
				return this.m_Reference;
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00007491 File Offset: 0x00005691
		public InputActionProperty(InputAction action)
		{
			this.m_UseReference = false;
			this.m_Action = action;
			this.m_Reference = null;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x000074A8 File Offset: 0x000056A8
		public InputActionProperty(InputActionReference reference)
		{
			this.m_UseReference = true;
			this.m_Action = null;
			this.m_Reference = reference;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x000074BF File Offset: 0x000056BF
		public bool Equals(InputActionProperty other)
		{
			return this.m_Reference == other.m_Reference && this.m_UseReference == other.m_UseReference && this.m_Action == other.m_Action;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x000074F2 File Offset: 0x000056F2
		public bool Equals(InputAction other)
		{
			return this.action == other;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x000074FD File Offset: 0x000056FD
		public bool Equals(InputActionReference other)
		{
			return this.m_Reference == other;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000750B File Offset: 0x0000570B
		public override bool Equals(object obj)
		{
			if (this.m_UseReference)
			{
				return this.Equals(obj as InputActionReference);
			}
			return this.Equals(obj as InputAction);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000752E File Offset: 0x0000572E
		public override int GetHashCode()
		{
			if (this.m_UseReference)
			{
				if (!(this.m_Reference != null))
				{
					return 0;
				}
				return this.m_Reference.GetHashCode();
			}
			else
			{
				if (this.m_Action == null)
				{
					return 0;
				}
				return this.m_Action.GetHashCode();
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00007569 File Offset: 0x00005769
		public static bool operator ==(InputActionProperty left, InputActionProperty right)
		{
			return left.Equals(right);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00007573 File Offset: 0x00005773
		public static bool operator !=(InputActionProperty left, InputActionProperty right)
		{
			return !left.Equals(right);
		}

		// Token: 0x040000C4 RID: 196
		[SerializeField]
		private bool m_UseReference;

		// Token: 0x040000C5 RID: 197
		[SerializeField]
		private InputAction m_Action;

		// Token: 0x040000C6 RID: 198
		[SerializeField]
		private InputActionReference m_Reference;
	}
}

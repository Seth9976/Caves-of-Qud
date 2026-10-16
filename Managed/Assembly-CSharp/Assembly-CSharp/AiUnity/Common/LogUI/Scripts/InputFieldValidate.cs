using System;
using UnityEngine;
using UnityEngine.UI;

namespace AiUnity.Common.LogUI.Scripts
{
	// Token: 0x020002CB RID: 715
	public class InputFieldValidate : MonoBehaviour
	{
		// Token: 0x060018C8 RID: 6344 RVA: 0x0007FF2B File Offset: 0x0007E12B
		public char ValidateInput(string text, int charIndex, char addedChar)
		{
			if (this.validateNumber && !char.IsNumber(addedChar))
			{
				addedChar = '\0';
			}
			return addedChar;
		}

		// Token: 0x060018C9 RID: 6345 RVA: 0x0007FF41 File Offset: 0x0007E141
		private void Start()
		{
			InputField component = base.gameObject.GetComponent<InputField>();
			component.onValidateInput = (InputField.OnValidateInput)Delegate.Combine(component.onValidateInput, new InputField.OnValidateInput(this.ValidateInput));
		}

		// Token: 0x04000EB0 RID: 3760
		public bool validateNumber = true;
	}
}

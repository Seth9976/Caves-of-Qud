using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RedShadow.CommonDialogs
{
	// Token: 0x020001FB RID: 507
	public class InputDialog : DialogBase
	{
		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x0600123F RID: 4671 RVA: 0x0006CC67 File Offset: 0x0006AE67
		// (set) Token: 0x06001240 RID: 4672 RVA: 0x0006CC6F File Offset: 0x0006AE6F
		public Buttons Result { get; private set; }

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06001241 RID: 4673 RVA: 0x0006CC78 File Offset: 0x0006AE78
		// (set) Token: 0x06001242 RID: 4674 RVA: 0x0006CC80 File Offset: 0x0006AE80
		public InputType InputType { get; private set; }

		// Token: 0x06001243 RID: 4675 RVA: 0x0006CC8C File Offset: 0x0006AE8C
		protected override void Awake()
		{
			base.Awake();
			this.Result = Buttons.Cancel;
			this._messageText = base.transform.Find("Window/MessagePanel/Text").GetComponent<Text>();
			this._inputField = base.transform.Find("Window/InputPanel/InputField").GetComponent<InputField>();
			this._slider = base.transform.Find("Window/InputPanel/Slider").GetComponent<Slider>();
			this._sliderInput = base.transform.Find("Window/InputPanel/Slider/InputField").GetComponent<InputField>();
			this._dropdown = base.transform.Find("Window/InputPanel/Dropdown").GetComponent<Dropdown>();
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x0006CD30 File Offset: 0x0006AF30
		public void getString(string text, string defaultValue, Action<string> callback, InputType type = InputType.String)
		{
			switch (type)
			{
			case InputType.String:
				break;
			case InputType.UserName:
				this._inputField.contentType = InputField.ContentType.Alphanumeric;
				break;
			case InputType.Password:
				this._inputField.contentType = InputField.ContentType.Password;
				break;
			case InputType.Email:
				this._inputField.contentType = InputField.ContentType.EmailAddress;
				break;
			default:
				type = InputType.String;
				break;
			}
			this.InputType = type;
			this._inputField.gameObject.SetActive(true);
			this._slider.gameObject.SetActive(false);
			this._dropdown.gameObject.SetActive(false);
			this.Result = Buttons.Cancel;
			this._stringCallback = callback;
			this._floatCallback = null;
			this._intCallback = null;
			this.setText(text);
			this.setValue(defaultValue);
			base.StartCoroutine(this.show_co(0.01f));
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x0006CDFA File Offset: 0x0006AFFA
		public void getUserName(string text, string defaultValue, Action<string> callback)
		{
			this.getString(text, defaultValue, callback, InputType.UserName);
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x0006CE06 File Offset: 0x0006B006
		public void getPassword(string text, string defaultValue, Action<string> callback)
		{
			this.getString(text, defaultValue, callback, InputType.Password);
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x0006CE12 File Offset: 0x0006B012
		public void getEmail(string text, string defaultValue, Action<string> callback)
		{
			this.getString(text, defaultValue, callback, InputType.Email);
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x0006CE20 File Offset: 0x0006B020
		public void getFloat(string text, float defaultValue, float minValue, float maxValue, Action<float> callback)
		{
			this.InputType = InputType.Float;
			this._inputField.gameObject.SetActive(false);
			this._slider.gameObject.SetActive(true);
			this._dropdown.gameObject.SetActive(false);
			this.Result = Buttons.Cancel;
			this._stringCallback = null;
			this._floatCallback = callback;
			this._intCallback = null;
			this.setText(text);
			this._sliderInput.contentType = InputField.ContentType.DecimalNumber;
			this._slider.minValue = minValue;
			this._slider.maxValue = maxValue;
			this._slider.wholeNumbers = false;
			this.setValue(defaultValue);
			base.StartCoroutine(this.show_co(0.01f));
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x0006CED8 File Offset: 0x0006B0D8
		public void getInt(string text, int defaultValue, int minValue, int maxValue, Action<int> callback)
		{
			this.InputType = InputType.Int;
			this._inputField.gameObject.SetActive(false);
			this._slider.gameObject.SetActive(true);
			this._dropdown.gameObject.SetActive(false);
			this.Result = Buttons.Cancel;
			this._stringCallback = null;
			this._floatCallback = null;
			this._intCallback = callback;
			this.setText(text);
			this._sliderInput.contentType = InputField.ContentType.IntegerNumber;
			this._slider.minValue = (float)minValue;
			this._slider.maxValue = (float)maxValue;
			this._slider.wholeNumbers = true;
			this.setValue(defaultValue);
			base.StartCoroutine(this.show_co(0.01f));
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x0006CF90 File Offset: 0x0006B190
		public void getChoice(string text, int defaultChoice, List<string> choices, Action<int> callback)
		{
			this.InputType = InputType.Choice;
			this._inputField.gameObject.SetActive(false);
			this._slider.gameObject.SetActive(false);
			this._dropdown.gameObject.SetActive(true);
			this.Result = Buttons.Cancel;
			this._stringCallback = null;
			this._floatCallback = null;
			this._intCallback = callback;
			this.setText(text);
			this._dropdown.AddOptions(choices);
			this.setValue(defaultChoice);
			base.StartCoroutine(this.show_co(0.01f));
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x0006D020 File Offset: 0x0006B220
		public void setText(string text)
		{
			this._messageText.text = text;
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x0006D02E File Offset: 0x0006B22E
		public void setValue(string value)
		{
			if (this.InputType == InputType.String || this.InputType == InputType.UserName || this.InputType == InputType.Password || this.InputType == InputType.Email)
			{
				this._inputField.text = value;
			}
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x0006D05F File Offset: 0x0006B25F
		public void setValue(float value)
		{
			if (this.InputType == InputType.Float)
			{
				this._slider.value = value;
			}
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x0006D076 File Offset: 0x0006B276
		public void setValue(int value)
		{
			if (this.InputType == InputType.Choice)
			{
				this._dropdown.value = value;
				return;
			}
			if (this.InputType == InputType.Int || this.InputType == InputType.Float)
			{
				this._slider.value = (float)value;
			}
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x0006D0B0 File Offset: 0x0006B2B0
		public string getStringValue()
		{
			if (this.InputType == InputType.String || this.InputType == InputType.UserName || this.InputType == InputType.Password || this.InputType == InputType.Email)
			{
				return this._inputField.text;
			}
			if (this.InputType == InputType.Float)
			{
				return this._slider.value.ToString();
			}
			if (this.InputType == InputType.Int)
			{
				return Mathf.RoundToInt(this._slider.value).ToString();
			}
			if (this.InputType == InputType.Choice)
			{
				return this._dropdown.captionText.text;
			}
			return null;
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x0006D146 File Offset: 0x0006B346
		public float getFloatValue()
		{
			if (this.InputType == InputType.Float || this.InputType == InputType.Int)
			{
				return this._slider.value;
			}
			return -1f;
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x0006D16B File Offset: 0x0006B36B
		public int getIntValue()
		{
			if (this.InputType == InputType.Choice)
			{
				return this._dropdown.value;
			}
			if (this.InputType == InputType.Int || this.InputType == InputType.Float)
			{
				return Mathf.RoundToInt(this._slider.value);
			}
			return -1;
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x0006D1A8 File Offset: 0x0006B3A8
		public void onSliderChanged()
		{
			if (this._isUpdating)
			{
				return;
			}
			this._isUpdating = true;
			this._sliderInput.text = this._slider.value.ToString();
			this._isUpdating = false;
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x0006D1EC File Offset: 0x0006B3EC
		public void onSliderInputChanged()
		{
			if (this._isUpdating)
			{
				return;
			}
			this._isUpdating = true;
			try
			{
				this._slider.value = float.Parse(this._sliderInput.text);
			}
			catch (FormatException)
			{
				this._slider.value = this._slider.minValue;
			}
			this._isUpdating = false;
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x0006D258 File Offset: 0x0006B458
		public void onOk()
		{
			this.Result = Buttons.Ok;
			this.hide();
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x0006D268 File Offset: 0x0006B468
		protected override void hide()
		{
			base.hide();
			switch (this.InputType)
			{
			case InputType.String:
			case InputType.UserName:
			case InputType.Password:
			case InputType.Email:
				this._stringCallback((this.Result != Buttons.Ok) ? null : this.getStringValue());
				return;
			case InputType.Float:
				this._floatCallback((this.Result != Buttons.Ok) ? (-1f) : this.getFloatValue());
				return;
			case InputType.Int:
				this._intCallback((this.Result != Buttons.Ok) ? (-1) : this.getIntValue());
				return;
			case InputType.Choice:
				this._intCallback((this.Result != Buttons.Ok) ? (-1) : this.getIntValue());
				return;
			default:
				return;
			}
		}

		// Token: 0x04000C3B RID: 3131
		private InputField _inputField;

		// Token: 0x04000C3C RID: 3132
		private Slider _slider;

		// Token: 0x04000C3D RID: 3133
		private InputField _sliderInput;

		// Token: 0x04000C3E RID: 3134
		private Dropdown _dropdown;

		// Token: 0x04000C3F RID: 3135
		private Text _messageText;

		// Token: 0x04000C40 RID: 3136
		private Action<string> _stringCallback;

		// Token: 0x04000C41 RID: 3137
		private Action<float> _floatCallback;

		// Token: 0x04000C42 RID: 3138
		private Action<int> _intCallback;

		// Token: 0x04000C43 RID: 3139
		private bool _isUpdating;
	}
}

﻿using System;
using System.Linq;

namespace Xamarin.Forms.Platform.Android
{
	public static class AccessibilityExtensions
	{
		public static string SetContentDescription(this global::Android.Views.View Control, Element Element, string _defaultContentDescription = null)
		{
			if (Element == null)
				return _defaultContentDescription;

			if (_defaultContentDescription == null)
				_defaultContentDescription = Control.ContentDescription;

			var elemValue = ConcatenateNameAndHint(Element);

			if (!string.IsNullOrWhiteSpace(elemValue))
				Control.ContentDescription = elemValue;
			else
				Control.ContentDescription = _defaultContentDescription;

			return _defaultContentDescription;
		}

		public static bool? SetFocusable(this global::Android.Views.View Control, Element Element, bool? _defaultFocusable = null)
		{
			if (Element == null)
				return _defaultFocusable;

			if (!_defaultFocusable.HasValue)
				_defaultFocusable = Control.Focusable;

			Control.Focusable = (bool)((bool?)Element.GetValue(Accessibility.IsInAccessibleTreeProperty) ?? _defaultFocusable);

			return _defaultFocusable;
		}

		public static string SetHint(this global::Android.Widget.TextView Control, Element Element, string _defaultHint)
		{
			if (Element == null)
				return _defaultHint;

			if (_defaultHint == null)
				_defaultHint = Control.Hint;

			var elemValue = ConcatenateNameAndHint(Element);

			if (!string.IsNullOrWhiteSpace(elemValue))
				Control.Hint = elemValue;
			else
				Control.Hint = _defaultHint;

			return _defaultHint;
		}

		public static void SetLabeledBy(this global::Android.Views.View Control, Element Element)
		{
			if (Element == null)
				return;

			var elemValue = (VisualElement)Element.GetValue(Accessibility.LabeledByProperty);

			if (elemValue != null)
			{
				var id = Control.Id;
				if (id == -1)
					id = Control.Id = FormsAppCompatActivity.GetUniqueId();

				var renderer = elemValue?.GetRenderer();
				renderer?.SetLabelFor(id);
			}
		}

		public static int? SetLabelFor(this global::Android.Views.View Control, int? id, int? _defaultLabelFor = null)
		{
			if (_defaultLabelFor == null)
				_defaultLabelFor = Control.LabelFor;

			Control.LabelFor = (int)(id ?? _defaultLabelFor);

			return _defaultLabelFor;
		}

		static string ConcatenateNameAndHint(Element Element)
		{
			string separator;

			var name = (string)Element.GetValue(Accessibility.NameProperty);
			var hint = (string)Element.GetValue(Accessibility.HintProperty);

			if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(hint))
				separator = "";
			else
				separator = ". ";

			return string.Join(separator, name, hint);
		}
	}
}

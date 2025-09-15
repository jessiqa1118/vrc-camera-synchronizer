using System;
using System.Collections.Generic;

namespace VRCCamera
{
    /// <summary>
    /// Reactive property that notifies when value changes
    /// </summary>
    /// <typeparam name="T">Value type (struct)</typeparam>
    public class ReactiveProperty<T> where T : struct
    {
        private T _value;

        /// <summary>
        /// Event triggered when value changes
        /// </summary>
        public event Action<T> OnValueChanged;

        /// <summary>
        /// Gets the current value
        /// </summary>
        public T Value => _value;

        /// <summary>
        /// Creates a new reactive property with initial value
        /// </summary>
        public ReactiveProperty(T initialValue)
        {
            _value = initialValue;
        }

        /// <summary>
        /// Sets the value and triggers change event if different
        /// </summary>
        public void SetValue(T newValue)
        {
            if (EqualityComparer<T>.Default.Equals(_value, newValue)) return;

            _value = newValue;
            OnValueChanged?.Invoke(_value);
        }

        /// <summary>
        /// Clears all event subscriptions
        /// </summary>
        public void ClearSubscriptions()
        {
            OnValueChanged = null;
        }
    }
}

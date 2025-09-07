using System;

namespace VRCCamera
{
    /// <summary>
    /// Reactive property that notifies when value changes
    /// </summary>
    /// <typeparam name="T">Value type that implements IEquatable</typeparam>
    public class ReactiveProperty<T> where T : struct, IEquatable<T>
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
            if (_value.Equals(newValue)) return;

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
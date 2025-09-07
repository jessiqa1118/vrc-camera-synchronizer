using System;

namespace JessiQa
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
        /// Gets whether the value has changed since last check
        /// </summary>
        public bool HasChanged { get; private set; }

        /// <summary>
        /// Creates a new reactive property with initial value
        /// </summary>
        public ReactiveProperty(T initialValue)
        {
            _value = initialValue;
            HasChanged = false;
        }

        /// <summary>
        /// Sets the value and triggers change event if different
        /// </summary>
        public void SetValue(T newValue)
        {
            if (_value.Equals(newValue)) return;

            _value = newValue;
            HasChanged = true;
            OnValueChanged?.Invoke(_value);
        }

        /// <summary>
        /// Force update without equality check
        /// </summary>
        public void ForceSetValue(T newValue)
        {
            _value = newValue;
            HasChanged = true;
            OnValueChanged?.Invoke(_value);
        }

        /// <summary>
        /// Resets the changed flag
        /// </summary>
        public void ResetChangedFlag()
        {
            HasChanged = false;
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
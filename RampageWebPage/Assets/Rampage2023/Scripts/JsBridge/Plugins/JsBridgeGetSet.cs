using System;
using System.Globalization;
using JetBrains.Annotations;
using UnityEngine;

namespace JsToUnityAndBack.Plugins
{
    public class JsBridgeGetSet : MonoBehaviour
    {
        public event Action<string, int> onReceiveIntValue;
        public event Action<string, float> onReceiveFloatValue;
        public event Action<string, string> onReceiveStringValue;
        public event Action<string, bool> onReceiveBooleanValue;

        private static (string key, string value)? TrySplitReceivedValue(string keyValue)
        {
            string[] splitValue = keyValue.Split(':');

            if (splitValue.Length == 2)
            {
                return (splitValue[0], splitValue[1]);
            }

            Debug.LogError("tried to split a invalid string.");
            return null;
        }

        [UsedImplicitly]
        public void JsReceiveIntValue(string keyValue)
        {
            (string parameterName, string parameterValue)? result = TrySplitReceivedValue(keyValue);
            if (result == null) return;

            bool parsed = int.TryParse(result.Value.parameterValue, NumberStyles.Any, CultureInfo.InvariantCulture,
                out int intValue);

            if (!parsed)
            {
                Debug.LogError("Tried to parse a value with a invalid syntax. Correct way: 'parameterName:floatValue'");
                return;
            }

            onReceiveIntValue?.Invoke(result.Value.parameterName, intValue);
        }
        
        [UsedImplicitly]
        public void JsReceiveFloatValue(string keyValue)
        {
            (string parameterName, string parameterValue)? result = TrySplitReceivedValue(keyValue);
            if (result == null) return;

            bool parsed = float.TryParse(result.Value.parameterValue, NumberStyles.Any, CultureInfo.InvariantCulture,
                out float floatValue);

            if (!parsed)
            {
                Debug.LogError("Tried to parse a value with a invalid syntax. Correct way: 'parameterName:floatValue'");
                return;
            }

            onReceiveFloatValue?.Invoke(result.Value.parameterName, floatValue);
        }

        [UsedImplicitly]
        public void JsReceiveStringValue(string keyValue)
        {
            (string parameterName, string parameterValue)? result = TrySplitReceivedValue(keyValue);
            if (result == null) return;

            onReceiveStringValue?.Invoke(result.Value.parameterName, result.Value.parameterValue);
        }

        [UsedImplicitly]
        public void JsReceiveBooleanValue(string keyValue)
        {
            (string parameterName, string parameterValue)? result = TrySplitReceivedValue(keyValue);
            if (result == null) return;

            bool.TryParse(result.Value.parameterValue, out bool booleanValue);
            bool parsed = float.TryParse(result.Value.parameterValue, out var floatValue);

            if (!parsed)
            {
                Debug.LogError("Tried to parse a value with a invalid syntax. Correct way: 'parameterName:floatValue'");
                return;
            }

            onReceiveBooleanValue?.Invoke(result.Value.parameterName, booleanValue);
        }
    }
}
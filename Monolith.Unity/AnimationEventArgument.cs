using UnityEngine;

namespace Monolith.Unity.Utilities
{

    public readonly struct AnimationEventArgument
    {

        public readonly AnimationEventArgumentType Type;
        public readonly float FloatValue;
        public readonly int IntegerValue;
        public readonly string StringValue;
        public readonly Object ObjectValue;

        public AnimationEventArgument(float value)
            : this(AnimationEventArgumentType.Float, value, default, default, default)
        {
        }

        public AnimationEventArgument(int value)
            : this(AnimationEventArgumentType.Integer, default, value, default, default)
        {
        }

        public AnimationEventArgument(string value)
            : this(AnimationEventArgumentType.String, default, default, value, default)
        {
        }

        public AnimationEventArgument(Object value)
            : this(AnimationEventArgumentType.Object, default, default, default, value)
        {
        }

        private AnimationEventArgument(AnimationEventArgumentType type, float floatValue, int integerValue, string stringValue, Object objectValue)
        {
            Type = type;
            FloatValue = floatValue;
            IntegerValue = integerValue;
            StringValue = stringValue;
            ObjectValue = objectValue;
        }

        public static implicit operator float(AnimationEventArgument arg) => arg.FloatValue;

        public static implicit operator int(AnimationEventArgument arg) => arg.IntegerValue;

        public static implicit operator string(AnimationEventArgument arg) => arg.StringValue;

        public static implicit operator Object(AnimationEventArgument arg) => arg.ObjectValue;

    }

}

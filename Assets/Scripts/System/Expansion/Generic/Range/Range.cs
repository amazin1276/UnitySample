using System;

namespace Expansion
{


    /// <summary>
    /// 値の範囲を定め、値を管理するクラス
    /// </summary>
    public partial struct Range : IEquatable<Range>
    {


        public Range(float _minimum, float _maxim)
        {
            min = _minimum;
            max = _maxim;
        }


        public float min;

        public float max;





        public static bool operator ==(float _value, Range _range)
        {
            bool isInRange = (
                _value >= _range.min &&
                _value <= _range.max
            );
            return isInRange;
        }
        public static bool operator !=(float _value, Range _range)
        {
            bool isOutOfRange = (
                _value < _range.min &&
                _value > _range.max
            );
            return isOutOfRange;
        }
        public override bool Equals(object obj) { return base.Equals(obj); }
        public override int GetHashCode() { return base.GetHashCode(); }


        public bool Equals(Range _range)
        {
            bool isCompatible = (
                _range.min == min &&
                _range.max == max
            );
            return isCompatible;
        }


        public float AdjustInputInRange(float _value) { return Math.Clamp(_value, min, max); }




        /// <summary>
        /// minとmaxが一致していた場合の例外処理
        /// </summary>
        [System.Serializable]
        public class NoRangeException : System.Exception
        {
            public NoRangeException() : base("例外'NoRangeException': 範囲変数のmaxとminがある値で一致しておりvalueの範囲がありません。") { }
            public NoRangeException(string message) : base(message) { }
            /// <summary>
            /// minとmaxが一致していた場合の例外処理
            /// </summary>
            /// <param name="_rangeValue">minかmaxの値</param>
            public NoRangeException(float _rangeValue) : base($"例外'NoRangeException': 範囲変数のmaxとminが{_rangeValue}で一致しておりvalueの範囲がありません。") { }
            public NoRangeException(string message, System.Exception inner) : base(message, inner) { }
            protected NoRangeException(
                System.Runtime.Serialization.SerializationInfo info,
                System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
        }
    }
}


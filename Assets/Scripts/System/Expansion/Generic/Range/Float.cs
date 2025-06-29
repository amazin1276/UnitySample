using System;
using static Expansion.Functions;


namespace Expansion
{


    public partial struct Range
    {


        /// <summary>
        /// 範囲が定められたfloat型の値を持つクラス。
        /// Manage関数を使わないと値を範囲内に収められない。
        /// このクラスで変数を宣言する場合には変数名の頭部分に"rng"とつけること。
        /// </summary>
        [System.Serializable]
        public struct Float : IEquatable<Range.Float>, IEquatable<float>, IComparable<Range.Float>, IComparable<Range.Int>, IComparable<float>, IComparable<int>
        {


            /// <summary>中身の値</summary>
            public float Value
            {
                get
                {
                    if (isKeepInRange) internalValue = AdjustInputInRange(internalValue);
                    return internalValue;
                }
                set
                {
                    if (isKeepInRange) value = AdjustInputInRange(value);
                    internalValue = value;
                }
            }
            private float internalValue;


            /// <summary>値の最大値</summary>
            [UnityEngine.SerializeField]
            public float Max
            {
                get
                {
                    return internalMax;
                }
                set
                {
                    if (isVariableRange) return;
                    internalMax = value;
                }
            }
            private float internalMax;


            [UnityEngine.SerializeField]
            /// <summary>値の最小値</summary>
            public float Min
            {
                get
                {
                    return internalMin;
                }
                set
                {
                    if (isVariableRange) return;
                    internalMin = value;
                }
            }
            private float internalMin;

            public readonly bool isVariableRange;

            public readonly bool isKeepInRange;

            private readonly float defaultMax;

            private readonly float defaultMin;


            public Float(
                float _max, float _min = 0, float _initValue = 0,
                bool _isVariableRange = true, bool _isKeepInRange = false
                ) : this()
            {
                bool isOverMax = _min > _max;
                if (isOverMax) _min = _max;

                bool isUnderMin = _max < _min;
                if (isUnderMin) _max = _min;

                isVariableRange = _isVariableRange;
                isKeepInRange = _isKeepInRange;
                Max = _max;
                Min = _min;
                defaultMax = Max;
                defaultMin = Min;
                Value = _initValue;

                bool isNoRange = isOverMax || isUnderMin;
                if (isNoRange) throw new Range.NoRangeException(_max);
            }


            public Float(
                Range _range, int _initValue, bool _isVariableRange, bool _isKeepInRange
                ) : this()
            {
                bool isOverMax = _range.min > _range.max;
                if (isOverMax) _range.min = _range.max;

                bool isUnderMin = _range.max < _range.min;
                if (isUnderMin) _range.max = _range.min;

                Value = _initValue;
                Max = _range.max;
                Min = _range.min;
                defaultMax = Max;
                defaultMin = Min;
                isVariableRange = _isVariableRange;
                isKeepInRange = _isKeepInRange;

                bool isNoRange = isOverMax || isUnderMin;
                if (isNoRange) throw new Range.NoRangeException(_range.max);
            }


            public Float(Range.Float _copy) : this()
            {
                Max = _copy.Max;
                Min = _copy.Min;
                defaultMax = _copy.Max;
                defaultMin = _copy.Min;
                Value = _copy.Value;
                isVariableRange = _copy.isVariableRange;
                isKeepInRange = _copy.isKeepInRange;

                bool isNoRange = Max == Min;
                if (isNoRange) throw new NoRangeException(Max);
            }


            public Float(Range.Int _copy) : this()
            {
                Max = _copy.Max;
                Min = _copy.Min;
                defaultMax = _copy.Max;
                defaultMin = _copy.Min;
                Value = _copy.Value;
                isVariableRange = _copy.isVariableRange;
                isKeepInRange = _copy.isKeepInRange;

                bool isNoRange = Max == Min;
                if (isNoRange) throw new NoRangeException(Max);
            }


            public static implicit operator float(Range.Float i)
            {
                return i.Value;
            }

            public static Range.Float operator ++(Range.Float rngF) { return new Range.Float(rngF.Max, rngF.Min, rngF.Value + 1, rngF.isVariableRange); }
            public static Range.Float operator --(Range.Float rngF) { return new Range.Float(rngF.Max, rngF.Min, rngF.Value - 1, rngF.isVariableRange); }

            public static Range.Float operator +(Range.Float ri, float i) { return new Range.Float(ri.Min, ri.Max, ri.Value + i, ri.isVariableRange); }
            public static Range.Float operator -(Range.Float ri, float i) { return new Range.Float(ri.Min, ri.Max, ri.Value + i, ri.isVariableRange); }

            public static float operator +(float i, Range.Float ri) { return i + ri.Value; }
            public static float operator -(float i, Range.Float ri) { return i - ri.Value; }

            public static Range.Float operator +(Range.Float ri, Range.Float tri) { return new Range.Float(ri.Min, ri.Max, ri.Value + tri.Value, ri.isVariableRange); }
            public static Range.Float operator -(Range.Float ri, Range.Float tri) { return new Range.Float(ri.Min, ri.Max, ri.Value - tri.Value, ri.isVariableRange); }

            public static Range.Float operator +(Range.Float ri, Range.Int tri) { return new Range.Float(ri.Min, ri.Max, ri.Value + tri.Value, ri.isVariableRange); }
            public static Range.Float operator -(Range.Float ri, Range.Int tri) { return new Range.Float(ri.Min, ri.Max, ri.Value - tri.Value, ri.isVariableRange); }


            public static bool operator >(float i, Range.Float ri) { return i > ri.Value; }
            public static bool operator <(float i, Range.Float ri) { return i < ri.Value; }

            public static bool operator >=(Range.Float ri, float i) { return ri.Value >= i; }
            public static bool operator <=(Range.Float ri, float i) { return ri.Value <= i; }

            public static bool operator ==(Range.Float rf, Range.Float trf) { return rf.Value == trf.Value; }
            public static bool operator !=(Range.Float rf, Range.Float trf) { return rf.Value != trf.Value; }


            public bool Equals(Range.Float _value)
            {
                bool isSameType = this.GetType() == _value.GetType();
                if (!isSameType) return false;

                return this.Equals(_value);
            }
            public override bool Equals(object _value)
            {
                return _value.Equals(_value);
            }
            public override int GetHashCode()
            {
                throw new System.NotImplementedException();
            }
            public bool Equals(float _value)
            {
                return _value == this.Value;
            }


            public int CompareTo(Range.Float _rngF)
            {
                if (_rngF.Equals(null))
                    return 1;

                else if (this > _rngF)
                    return 1;

                else if (this == _rngF)
                    return 0;

                else if (this < _rngF)
                    return -1;

                return 0;
            }
            public int CompareTo(Range.Int _rngI)
            {
                if (_rngI.Equals(null))
                    return 1;

                else if (this > _rngI)
                    return 1;

                else if (this == _rngI)
                    return 0;

                else if (this < _rngI)
                    return -1;

                return 0;
            }
            public int CompareTo(float _value)
            {
                if (_value.Equals(null))
                    return 1;

                else if (this > _value)
                    return 1;

                else if (this == _value)
                    return 0;

                else if (this < _value)
                    return -1;

                return 0;
            }
            public int CompareTo(int _value)
            {
                if (_value.Equals(null))
                    return 1;

                else if (this > _value)
                    return 1;

                else if (this == _value)
                    return 0;

                else if (this < _value)
                    return -1;

                return 0;
            }


            /// <summary>
            /// valueの値の最小値から最大値までの進行度を指定したスケールで返す
            /// </summary>
            /// <param name="_minScale">進行度の最小値</param>
            /// <param name="_maxScale">進行度の最大値</param>
            public float GetProgress(float _minScale, float _maxScale)
            {
                return ReturnProgress(
                    _value: Value,
                    _maxValue: Max,
                    _minValue: Min,
                    _maxScale: _maxScale,
                    _minScale: _minScale
                );
            }


            /// <summary>
            /// valueの値の最小値から最大値までの進行度を取得する
            /// </summary>
            /// <returns>0から100までの進行度</returns>
            public float GetProgress()
            {
                return ReturnProgress(
                    _value: Value,
                    _maxValue: Max,
                    _minValue: Min,
                    _maxScale: 100,
                    _minScale: 0
                );
            }


            /// <summary>
            /// valueを範囲内に収める
            /// </summary>
            public void Manage()
            {
                ManageRange();

                if (isVariableRange)
                {
                    Max = defaultMax;
                    Min = defaultMin;
                }

                // 最小値より値が小さい場合
                bool isOverMin = Min > Value;
                if (isOverMin)
                    Value = Min;

                // 最大値より値が大きい場合
                bool isOverMax = Max < Value;
                if (isOverMax)
                    Value = Max;
            }


            public void ManageRange()
            {
                if (!isVariableRange)
                {
                    Max = defaultMax;
                    Min = defaultMin;
                }
            }


            /// <summary>
            /// valueが指定した範囲内に入っているかどうか
            /// </summary>
            /// <returns></returns>
            public bool IsInRangeOf(float _min, float _max)
            {
                ManageRange();

                bool isInMin = Value > _min;
                bool isInMax = Value < _max;
                bool isInRange = isInMin && isInMax;

                return isInRange;
            }


            /// <summary>
            /// 値が最大値以上かどうか
            /// </summary>
            public bool IsReachMax()
            {
                ManageRange();

                return Value >= Max;
            }


            /// <param name="_additionalValue">追加する値</param>
            /// <returns>対象の値を足したとき最大値に届くかどうか</returns>
            public bool IsReachMax(float _additionalValue)
            {
                ManageRange();

                float updatedValue = Value + _additionalValue;
                bool isReach = updatedValue >= Max;

                return isReach;
            }


            /// <summary>
            /// 最小値以下かどうか
            /// </summary>
            public bool IsReachMin()
            {
                ManageRange();

                return Value <= Min;
            }


            public bool IsOverRangeOnAdd(float _additionalValue)
            {
                float addedValue = Value + _additionalValue;

                bool isLessThanMax = addedValue < Max;
                bool isMoreThanMin = addedValue > Min;

                bool isInRange = (
                    isLessThanMax &&
                    isMoreThanMin
                );
                return isInRange;
            }


            /// <summary>
            /// 入力された値を範囲内に収めて返す
            /// </summary>
            public float AdjustInputInRange(float _input) { return Math.Clamp(_input, Min, Max); }


            /// <summary>
            /// 範囲内からランダムに値を定める
            /// </summary>
            public void Randomize()
            {
                Value = UnityEngine.Random.Range(Min, Max);
            }


            public string ToString()
            {
                return Value.ToString();
            }


            /// <returns>RngIに変換したものを返す</returns>
            public Range.Int ToRangeInt()
            {
                ManageRange();

                return new Range.Int((int)Min, (int)Max, (int)Value, isVariableRange);
            }
        }
    }
}

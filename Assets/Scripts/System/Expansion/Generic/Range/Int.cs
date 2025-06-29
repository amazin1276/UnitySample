using System;
using static Expansion.Functions;


namespace Expansion
{


    public partial struct Range
    {


        /// <summary>
        /// 範囲が定められたint型の値を持つクラス
        /// Manage関数を使わないと値を範囲内に収められない
        /// このクラスで変数を宣言する場合には変数名の頭部分に"rng"とつけること。
        /// </summary>
        [System.Serializable]
        public struct Int : IEquatable<Range.Int>, IEquatable<int>, IComparable<Range.Int>, IComparable<Range.Float>, IComparable<int>, IComparable<float>
        {


            /// <summary>中身の値</summary>
            public int Value
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
            private int internalValue;


            /// <summary>値の最大値</summary>
            [UnityEngine.SerializeField]
            public int Max
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
            private int internalMax;


            [UnityEngine.SerializeField]
            /// <summary>値の最小値</summary>
            public int Min
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
            private int internalMin;


            /// <summary>min, maxが変更可能か</summary>
            public readonly bool isVariableRange;

            public readonly bool isKeepInRange;

            private readonly int defaultMax;

            private readonly int defaultMin;


            /// <param name="_min">最小値</param>
            /// <param name="_max">最大値</param>
            /// <param name="_initValue">初期値</param>
            /// <param name="_isVariableRange">最大値最小値が再設定可能かどうか</param>
            /// <param name="_isKeepInRange">値を範囲内に収めるかどうか</param>
            public Int(int _max, int _min = 0, int _initValue = 0, bool _isVariableRange = true, bool _isKeepInRange = true) : this()
            {
                bool isOverMax = _min > _max;
                if (isOverMax) _min = _max;

                bool isUnderMin = _max < _min;
                if (isUnderMin) _max = _min;

                Max = _max;
                Min = _min;
                isVariableRange = _isVariableRange;
                isKeepInRange = _isKeepInRange;
                defaultMax = Max;
                defaultMin = Min;
                Value = _initValue;

                bool isNoRange = isOverMax || isUnderMin;
                if (isNoRange) throw new Range.NoRangeException(_max);
            }


            /// <param name="_initValue">初期値</param>
            /// <param name="_isVariableRange">最大値最小値が再設定可能かどうか</param>
            /// <param name="_isKeepInRange">値を範囲内に収めるかどうか</param>
            public Int(Range _range, int _initValue = 0, bool _isVariableRange = true, bool _isKeepInRange = true) : this()
            {
                bool isOverMax = _range.min > _range.max;
                if (isOverMax) _range.min = _range.max;

                bool isUnderMin = _range.max < _range.min;
                if (isUnderMin) _range.max = _range.min;

                Max = _range.max.AsRounded();
                Min = _range.min.AsRounded();
                isVariableRange = _isVariableRange;
                isKeepInRange = _isKeepInRange;
                defaultMax = Max;
                defaultMin = Min;
                Value = _initValue;

                bool isNoRange = isOverMax || isUnderMin;
                if (isNoRange) throw new Range.NoRangeException(_range.max);
            }


            public Int(Range.Int _copy) : this()
            {
                Min = _copy.Min;
                Max = _copy.Max;
                isVariableRange = _copy.isVariableRange;
                isKeepInRange = _copy.isKeepInRange;
                defaultMax = _copy.Max;
                defaultMin = _copy.Min;
                Value = _copy.Value;

                bool isNoRange = Max == Min;
                if (isNoRange) throw new NoRangeException(Max);
            }


            public Int(Range.Float _copy) : this()
            {
                Min = (int)_copy.Min;
                Max = (int)_copy.Max;
                isVariableRange = _copy.isVariableRange;
                isKeepInRange = _copy.isKeepInRange;
                defaultMax = (int)_copy.Max;
                defaultMin = (int)_copy.Min;
                Value = (int)_copy.Value;

                bool isNoRange = Max == Min;
                if (isNoRange) throw new NoRangeException(Max);
            }


            public static implicit operator int(Range.Int i)
            {
                return i.Value;
            }


            public static Range.Int operator ++(Range.Int rngI) { return new Range.Int(rngI.Max, rngI.Min, rngI.Value + 1, rngI.isVariableRange); }
            public static Range.Int operator --(Range.Int rngI) { return new Range.Int(rngI.Max, rngI.Min, rngI.Value - 1, rngI.isVariableRange); }


            public static Range.Int operator +(Range.Int ri, int i) { return new Range.Int(ri.Min, ri.Max, ri.Value + i, ri.isVariableRange); }
            public static Range.Int operator -(Range.Int ri, int i) { return new Range.Int(ri.Min, ri.Max, ri.Value - i, ri.isVariableRange); }


            public static int operator +(int i, Range.Int ri) { return i + ri.Value; }
            public static int operator -(int i, Range.Int ri) { return i - ri.Value; }


            public static Range.Int operator +(Range.Int ri, Range.Int tri) { return new Range.Int(ri.Min, ri.Max, ri.Value + tri.Value, ri.isVariableRange); }
            public static Range.Int operator -(Range.Int ri, Range.Int tri) { return new Range.Int(ri.Min, ri.Max, ri.Value - tri.Value, ri.isVariableRange); }


            public static Range.Int operator +(Range.Int ri, Range.Float trf) { return new Range.Int(ri.Min, ri.Max, ri.Value + (int)trf.Value, ri.isVariableRange); }
            public static Range.Int operator -(Range.Int ri, Range.Float trf) { return new Range.Int(ri.Min, ri.Max, ri.Value - (int)trf.Value, ri.isVariableRange); }


            public static bool operator >(int i, Range.Int ri) { return i > ri.Value; }
            public static bool operator <(int i, Range.Int ri) { return i < ri.Value; }


            public static bool operator >=(Range.Int ri, int i) { return ri.Value >= i; }
            public static bool operator <=(Range.Int ri, int i) { return ri.Value <= i; }


            public static bool operator ==(Range.Int _left, Range.Int _right)
            {
                return _left.Value == _right.Value;
            }
            public static bool operator !=(Range.Int lhs, Range.Int rhs) { return !(lhs == rhs); }


            public bool Equals(Range.Int _value)
            {
                bool isSameType = this.GetType() == _value.GetType();
                if (!isSameType) return false;

                return this.Equals(_value);
            }
            public override bool Equals(object _value) { return _value.Equals(_value); }
            public bool Equals(int _value) { return _value == this.Value; }


            public override int GetHashCode()
            {
                int hashNum = Value + Min + Max;

                if (isVariableRange)
                    hashNum *= Value;
                else
                    hashNum *= -1 * Value;

                return hashNum.GetHashCode();
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


            /// <summary>
            /// valueの値の最小値から最大値までの進行度を指定したスケールで返す
            /// </summary>
            /// <param name="_minScale">進行度の最小値</param>
            /// <param name="_maxScale">進行度の最大値</param>
            public int GetProgress(int _minScale, int _maxScale)
            {
                ManageRange();

                return (int)ReturnProgress(
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
            /// <returns>0から1までの進行度</returns>
            public int GetProgress()
            {
                ManageRange();

                return (int)ReturnProgress(
                    _value: Value,
                    _maxValue: Max,
                    _minValue: Min,
                    _maxScale: 1,
                    _minScale: 0
                );
            }


            /// <summary>
            /// valueを範囲内に収める
            /// </summary>
            public void Manage()
            {
                ManageRange();
                Math.Clamp(Value, Min, Max);
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
            /// <param name="min"></param>
            /// <param name="max"></param>
            /// <param name="value"></param>
            /// <returns></returns>
            public bool IsInRangeOf(int _min, int _max)
            {
                ManageRange();

                bool a = 1 <= this;
                bool isInMin = Value > _min;
                bool isInMax = Value < _max;
                bool isInRange = isInMin && isInMax;

                return isInRange;
            }


            /// <summary>
            /// isVariableが真である時に最大値を設定する
            /// </summary>
            public void SetMax(int _max)
            {
                if (!isVariableRange)
                {
                    Warning(
                        "最大値を設定できませんでした",
                        "Expansion/Range/Int/SetMin()"
                    );
                    return;
                }
                Max = _max;
            }


            /// <summary>
            /// isVariableが真である時に最大値を設定する
            /// </summary>
            public void SetMin(int _min)
            {
                if (!isVariableRange)
                {
                    Warning(
                        "最小値を設定できませんでした",
                        "Expansion/Range/Int/SetMin()"
                    );
                    return;
                }
                Min = _min;
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
            public bool IsReachMax(int _additionalValue)
            {
                ManageRange();

                int updatedValue = Value + _additionalValue;
                bool isReach = updatedValue >= Max;

                return isReach;
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
            /// 最小値以下かどうか
            /// </summary>
            public bool IsReachMin()
            {
                ManageRange();

                return Value <= Min;
            }


            public int OutValueOnAdd(int _additionalValue)
            {
                if (!IsOverRangeOnAdd(_additionalValue)) return 0;

                int addedValue = Value + _additionalValue;

                bool isLessThanMax = addedValue < Max;
                if (isLessThanMax)
                    return addedValue - Max;
                else
                    return addedValue - Min;
            }


            /// <returns>最大値までの大小の差分</returns>
            public int ExcessValueToMax()
            {
                ManageRange();

                return Max - Value;
            }


            /// <returns>最小値までの大小の差分</returns>
            public int ExcessValueToMin()
            {
                ManageRange();

                return Min - Value;
            }


            /// <summary>
            /// 入力された値を範囲内に収めて返す
            /// </summary>
            public int AdjustInputInRange(float _input) { return Math.Clamp(_input, Min, Max).AsRounded(); }


            public void Randomize()
            {
                System.Random randomer = new System.Random();
                Value = randomer.Next(Min, Max);
            }


            public new string ToString()
            {
                ManageRange();

                return Value.ToString();
            }

            public Range.Float ToRangeFloat()
            {
                ManageRange();

                return new Range.Float(Min, Max, Value, isVariableRange);
            }
        }
    }
}

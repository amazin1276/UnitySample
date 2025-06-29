using Expansion;
using UnityEngine;
using static Expansion.Functions;



namespace Expansion.Components
{
    /// <summary>
    /// ゲーム内の時間を管理する基礎的なコンポーネント
    /// </summary>
    [Tooltip("ゲーム内の時間を管理する基礎的なコンポーネント")]
    public class TimeFlow : MonoBehaviour
    {
        [SerializeField]
        private string Preview;

        /// <summary>ゲーム時間内での1分が何秒か</summary>
        [Tooltip("ゲーム時間内での1分が何秒か"), Space()]
        public float secondPerMinute;

        /// <summary経過時間></summary>
        [Tooltip("経過時間")]
        public Range.Float time;

        /// <summary>ゲーム時間内の分単位</summary>
        [Tooltip("ゲーム時間内の分単位")]
        public Range.Int minutes;

        /// <summary>ゲーム時間内の分単位</summary>
        [Tooltip("ゲーム時間内の時単位")]
        public Range.Int hours;

        /// <summary>日の出の時間</summary>
        public System.TimeSpan sunrise;

        /// <summary>日の入りの時間</summary>
        public System.TimeSpan sunset;

        public System.TimeSpan dark;

        public System.TimeSpan bright;

        /// <summary>時間をTimeSpanで表したもの</summary>
        public System.TimeSpan formalized { get; private set; }


        [SerializeField, Space()]
        private SeasonFlow seasonFlow;

        [SerializeField]
        private Weather weather;

        /// <summary>時間の速さ(デフォルト値: 1)</summary>
        private float speed;

        void Start()
        {
            void SetRangeOfTime()
            {
                time = new Range.Float(secondPerMinute, 0);
                minutes = new Range.Int(60, 1, 1, false);
                hours = new Range.Int(24, 1, 1, false);
            }

            SetRangeOfTime();
            TryGetComponent<SeasonFlow>(out seasonFlow);
        }
        void Update()
        {
            speed = Time.timeScale;
            time.Value -= 0.025f;
            Preview = PreviewTimeFormat();

            ExpressTime();
            ManageMinute();
            ManageHour();
        }



        #region Public

        public void Reset()
        {
            time.Value = 0;
            minutes.Value = 0;
            hours.Value = 0;
        }


        /// <returns>ゲーム時間を文字列で</returns>
        public string PreviewTimeFormat()
        {
            int toSeconds = (int)time.GetProgress(0, 60);
            string previewTime = new System.TimeSpan(hours.Value, minutes.Value, toSeconds).ToString();

            return previewTime;
        }


        /// <returns>ゲーム時間が合計で何秒立ったか</returns>
        public float TotalSeconds()
        {
            float perHour = secondPerMinute * 60;
            float hoursToSeconds = perHour * hours.Value;

            float minutesToSeconds = secondPerMinute * minutes.Value;

            return hoursToSeconds + minutesToSeconds;
        }

        /// <returns>ゲーム時間での一時間の秒数</returns>
        public float SecondsPerHour() { return secondPerMinute * 60; }


        /// <returns>ゲーム時間での一日の秒数</returns>
        public float SecondsPerDay() { return SecondsPerHour() * 24; }


        /// <summary>
        /// 時間を一つの値で表したもの。
        /// ( 例: 16:30 = 16.5f )
        /// </summary>
        /// <returns></returns>
        public float ToFloatFormat()
        {
            float hour = hours.Value;
            float minute = ReturnProgress(minutes.Value, 0, 60);
            float format = hour + minute;

            return format;
        }


        public System.TimeSpan ToTimeSpanFormat()
        {
            int toSeconds = (int)time.GetProgress(0, 60);
            System.TimeSpan toFormat = new System.TimeSpan(hours.Value, minutes.Value, toSeconds);
            return toFormat;
        }


        /// <returns>ゲーム時間内の一日のうちどれくらい時間が進んだかを 0 - 100 で返す</returns>
        public int GetDayProgress()
        {
            float perHour = secondPerMinute * 60;
            float perDay = perHour * 24;

            float progress = ReturnProgress(
                _value: TotalSeconds(),

                _minValue: 0,
                _maxValue: perDay,

                _minScale: 0,
                _maxScale: 100
            );
            progress = System.MathF.Round(progress);

            return (int)progress;
        }


        public bool IsPM()
        {
            bool isPM = (
                hours.Value < 12 &&
                hours.Value >= 0
            );

            return isPM;
        }


        public bool IsAM()
        {
            bool isAM = (
                hours.Value >= 12 &&
                hours.Value < 24
            );

            return isAM;
        }


        public bool IsMorning()
        {
            bool isMorning = (
                hours.Value >= 4 &&
                hours.Value <= 10
            );
            return isMorning;
        }


        /// <summary>お昼の時間帯かどうか( 11:00 ~ 13:00 )</summary>
        public bool IsDayTime()
        {
            System.TimeSpan startTime = new System.TimeSpan(11, 00, 00);
            System.TimeSpan endTime = new System.TimeSpan(13, 00, 00);
            bool isDayTime = (
                formalized > startTime &&
                formalized < endTime
            );

            return isDayTime;
        }


        public bool IsEveningTime()
        {
            System.TimeSpan startTime = new System.TimeSpan(18, 00, 00);
            System.TimeSpan endTime = new System.TimeSpan(21, 00, 00);
            bool isEveningTime = (
                formalized > startTime &&
                formalized < endTime
            );

            return isEveningTime;
        }


        public bool IsDark()
        {
            bool isNight = (
                formalized > sunset ||
                formalized < sunrise
            );

            return isNight;
        }


        public bool IsBright()
        {
            return !IsDark();
        }

        #endregion



        #region Private

        private void ExpressTime()
        {
            if (time.IsReachMax()) return;

            time.Value += Time.deltaTime * speed;
        }


        private void ManageMinute()
        {
            if (!time.IsReachMax()) return;

            ++minutes;
            time.Value = 0;
        }


        private void ManageHour()
        {
            if (minutes.IsReachMax())
            {
                ++hours;
                minutes.Value = 0;
            }

            if (!hours.IsReachMax()) return;

            bool isAdvanceDay = seasonFlow.IsExist();
            if (isAdvanceDay)
                seasonFlow.AdvanceDay();
            if (weather.IsExist())
                weather.StartDay();

            hours.Value = 0;
        }


        private void UpdateTimeSpan()
        {

        }

        #endregion
    }
}
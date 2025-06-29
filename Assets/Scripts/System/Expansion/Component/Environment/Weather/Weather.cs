using Expansion;
using UnityEngine;
using static Expansion.Functions;

namespace Expansion.Components
{

    
    /// <summary>
    /// 簡易的な天気をシュミレートするコンポーネント
    /// </summary>
    [Tooltip("簡易的な天気をシュミレートするコンポーネント")]
    public class Weather : MonoBehaviour
    {

        #region プロパティ


        /// <summary>
        /// 天気の特性の設定
        /// </summary>
        [System.Serializable]
        public class Property
        {
            /// <summary>平均気温</summary>
            [Tooltip("平均気温")]
            public float aveTemperature;

            /// <summary>気温の変則性</summary>
            [Tooltip("平均気温の変則性(デフォルト値: 1)")]
            public int irregularAveTempRate = 1;

            /// <summary>気温の変動グラフ</summary>
            [Tooltip("気温の変動グラフ")]
            public AnimationCurve temperatureVariabilityGraph;

            /// <summary>気温の変動率</summary>
            [Tooltip("気温の変動率")]
            public float tempVariability;


            /// <summary>平均湿度</summary>
            [Tooltip("平均湿度"), Space()]
            public int aveHumidity;

            /// <summary>湿度の変則性</summary>
            [Tooltip("湿度の変則性")]
            public int irregularHumidityRate;


            /// <summary>曇りになる確率</summary>
            [Tooltip("曇りになる確率"), Space()]
            public int aveCloudProbability;


            /// <summary>雨が降る確率</summary>
            [Tooltip("雨が降る確率"), Space()]
            public float aveRainProbability;


            /// <summary>霧になる平均確率</summary>
            [Tooltip("霧になる平均確率"), Space()]
            public float aveMistProbability;


            /// <summary>雷になる平均確率</summary>
            [Tooltip("雷になる平均確率"), Space()]
            public float aveThunderProbability;


            /// <summary>シュミレートする間隔。ゲーム時間で何分ごとに</summary>
            [Tooltip("シュミレートする間隔。ゲーム時間で何分ごとに"), Space()]
            public int simulationInterval;

        }
        /// <summary>天気の特性の設定</summary>
        [SerializeField, Tooltip("天気の特性の設定"), Space()]
        public Property property = new Property();


        #endregion


        #region パラメーター
        /// <summary>温度</summary>
        [Tooltip("温度"), Space(30)]
        public float temp;

        /// <summary>平均気温</summary>
        [Tooltip("平均気温")]
        public float aveTemp;


        /// <summary>湿度</summary>
        [Tooltip("湿度")]
        public int humidity;

        /// <summary>平均湿度</summary>
        [Tooltip("平均湿度")]
        public int aveHumidity;

        /// <summary>雲量(0 ~ 10)</summary>
        [Tooltip("雲量")]
        public Range.Int rngCloudCover;

        /// <summary>降水量を 0 - 40 で表す</summary>
        [Tooltip("降水量を 0 - 40 で表す")]
        public Range.Float rngPrecipitation;

        /// <summary>雪のレベルを 0 - 40 で表す</summary>
        [Tooltip("雪のレベルを 0 - 40 で表す")]
        public Range.Float rngSnowAccumulation;

        /// <summary>雷のレベルを 0 - 2 で表す</summary>
        [Tooltip("雷のレベルを 0 - 2 で表す")]
        public Range.Float rngThunderLevel;

        /// <summary>霧のレベルを 0 - 2 で表す</summary>
        [Tooltip("霧のレベルを 0 - 2 で表す")]
        public Range.Float rngMistLevel;

        /// <summary>視程(M)</summary>
        [Tooltip("視程(M)")]
        public int visibleDistance;

        /// <summary>台風かどうか</summary>
        [Tooltip("台風かどうか")]
        public bool isTyphoon;

        /// <summary>空気がどれくらい乾いているかを表す範囲変数</summary>
        [HideInInspector]
        public Range.Int rngDrynessAir = new Range.Int(40, 0, 0, false, true);

        /// <summary>空気がどれくらい乾いているかを表す範囲変数</summary>
        [HideInInspector]
        public Range.Int rngMoistnessAir  = new Range.Int(100, 60, 0, false, true);

        private float time;

        private float day;

        private SeasonFlow.Seasons season;
        #endregion


        [SerializeField, Space()]
        private Wind2D wind2D;

        [SerializeField]
        private TimeFlow timeFlow;

        [SerializeField]
        private SeasonFlow seasonFlow;



        void Start()
        {
            void SetRange()
            {
                humidity = new Range.Int(100, 0, 30, false, true);
                rngCloudCover = new Range.Int(10, 0, 0, false, true);
                rngPrecipitation = new Range.Float(40, 0, 0, false, true);
                rngSnowAccumulation = new Range.Float(40, 0, 0, false, true);
                rngThunderLevel = new Range.Float(2, 0, 0, false, true);
                rngMistLevel = new Range.Float(2, 0, 0, false, true);
            }
            SetRange();

            float updateInterval = timeFlow.secondPerMinute * property.simulationInterval;
            InvokeRepeating(nameof(UpdateWeather), 1, updateInterval);
            StartDay();
        }

        void Update()
        {
            UpdateRanges();
            UpdateTemp();
            UpdateHumidity();
            UpdateTime();
        }


        #region Public

        public void StartDay()
        {
            int SetterI(int _irregularRate, int _ave)
            {
                int irregular = _irregularRate / 2;
                int randomized = Random.Range(_ave - irregular, _ave + irregular);

                return randomized;
            }
            float SetterF(float _irregularRate, float _ave)
            {
                float irregular = _irregularRate / 2;
                float randomized = Random.Range(_ave - irregular, _ave + irregular);
                float rounded = System.MathF.Round(randomized, 1);

                return rounded;
            }

            aveTemp = SetterF(property.irregularAveTempRate, property.aveTemperature);
            aveHumidity = SetterI(property.irregularHumidityRate, property.aveHumidity);
        }


        /// <summary>
        /// その一日の天気の特性を決める関数
        /// </summary>
        public void UpdateWeather()
        {
            isTyphoon = Probability(5);

            rngCloudCover.Value = GetDegreeOfCloudiness();
            rngPrecipitation.Value = GetPrecipitation();
            rngSnowAccumulation.Value = GetSnowAccumulation();
            rngThunderLevel.Value = GetThunderLevel();
            rngMistLevel.Value = GetMistLevel();
        }


        /// <returns>氷点下かどうか</returns>
        public bool IsBelowFreezing
        {
            get { return temp < 0; }
        }


        /// <returns>曇っているかどうか</returns>
        public bool IsCloud
        {
            get { return rngCloudCover >= 9; }
        }


        /// <returns>曇っているがまだ明るいくらいの状態かどうか</returns>
        public bool IsLighterCloud
        {
            get { return rngCloudCover == 9; }
        }


        /// <returns>空が物凄く厚い雲に覆われているかどうか</returns>
        public bool IsDarkerCloud
        {
            get { return rngCloudCover == 10; }
        }


        public bool IsClear
        {
            get { return !IsCloud; }
        }


        /// <returns>少しでも雨が降っているかどうか</returns>
        public bool IsRainy
        {
            get { return rngPrecipitation >= 0; }
        }


        /// <returns>小雨かどうか</returns>
        public bool IsLightRain
        {
            get { return rngPrecipitation < 2; }
        }


        /// <returns>雨が普通に降っているかどうか</returns>
        public bool IsMiddleRain
        {
            get { return (rngPrecipitation >= 2) && (rngPrecipitation < 10); }
        }


        /// <returns>雨が強く降っているかどうか</returns>
        public bool IsHeavyRain
        {
            get { return (rngPrecipitation >= 10) && (rngCloudCover < 20); }
        }


        /// <returns>雨が危険なほどに降っているかどうか</returns>
        public bool IsHellRain
        {
            get { return rngPrecipitation >= 20; }
        }


        /// <returns>雷がおこるかどうか</returns>
        public bool IsThunder
        {
            get { return rngPrecipitation >= 20; }
        }


        /// <returns>空気が乾燥しているか</returns>
        public bool IsDryAir
        {
            get { return aveHumidity < 40; }
        }


        /// <returns>空気が湿っているか</returns>
        public bool IsMoistAir
        {
            get { return aveHumidity > 60; }
        }

        #endregion



        #region Private

        /// <returns>雲量を返す 0 ~ 10</returns>
        private int GetDegreeOfCloudiness()
        {
            int degreeOfCloudiness = 0;


            bool isCloud = Probability(property.aveCloudProbability);
            if (isCloud)
                degreeOfCloudiness = RandomlyInt(9, 11);
            else
                degreeOfCloudiness = RandomlyInt(0, 9);

            if (isTyphoon) degreeOfCloudiness = 10;

            return degreeOfCloudiness;
        }



        /// <returns>降水量を返す 0 ~ 40</returns>
        private float GetPrecipitation()
        {
            bool isRain = (
                Probability(property.aveRainProbability) &&
                IsCloud &&
                !IsBelowFreezing
            );
            if (!isRain) return 0;

            float precipitation = 0;

            if (IsLighterCloud)
                precipitation = RandomlyFloat(0, 9.9f).AsRounded(1);

            else if (!isTyphoon && IsDarkerCloud)
                precipitation = RandomlyFloat(10, 19.9f).AsRounded(1);

            else if (isTyphoon && IsDarkerCloud)
                precipitation = RandomlyFloat(20, 40).AsRounded(1);

            else
                precipitation = 0;

            return precipitation;
        }


        /// <returns>積雪量</returns>
        public float GetSnowAccumulation()
        {
            bool isSnow = (
                Probability(property.aveRainProbability) &&
                IsCloud &&
                IsBelowFreezing
            );
            if (!isSnow) return 0;

            float Accumulation = 0;

            if (IsLighterCloud)
                Accumulation = RandomlyFloat(0, 9.9f).AsRounded(1);

            else if (!isTyphoon && IsDarkerCloud)
                Accumulation = RandomlyFloat(10, 19.9f).AsRounded(1);

            else if (isTyphoon && IsDarkerCloud)
                Accumulation = RandomlyFloat(20, 40).AsRounded(1);

            else
                Accumulation = 0;

            return Accumulation;
        }


        /// <returns>雷のレベル 0 ~ 2</returns>
        private int GetThunderLevel()
        {
            if (isTyphoon) return 2;

            bool isThunder = (
                Probability(property.aveThunderProbability) &&
                IsDarkerCloud
            );
            if (!isThunder) return 0;

            int level = 0;

            bool isHeavy = Probability(35);
            if (isHeavy)
                level = 2;
            else
                level = 1;

            return level;
        }


        /// <returns>霧のレベル 0 ~ 2</returns>
        private int GetMistLevel()
        {
            bool isMist = (
                Probability(property.aveMistProbability) &&
                timeFlow.IsMorning() &&
                !IsCloud
            );
            if (!isMist) return 0;

            int level = 0;

            bool isStrong = Probability(35);
            if (isStrong)
                level = 2;
            else
                level = 1;

            return level;
        }


        private void UpdateRanges()
        {
            rngCloudCover.Manage();
            rngPrecipitation.Manage();
            rngThunderLevel.Manage();
            rngMistLevel.Manage();

            rngDrynessAir.Value = aveHumidity;
            rngMoistnessAir.Value = aveHumidity;
        }


        private void UpdateTemp()
        {
            float additionalValue = property.temperatureVariabilityGraph.Evaluate(time) * property.tempVariability;
            temp = System.MathF.Round(
                aveTemp + additionalValue,
                1
            );
        }


        private void UpdateHumidity()
        {
            const float variability = 2;
            float additionalValue = property.temperatureVariabilityGraph.Evaluate(time) * variability;

            humidity = (aveHumidity + additionalValue).AsRounded();

            if (!IsRainy)
                return;
            else if (IsLightRain)
                humidity += 2;
            else if (IsMiddleRain)
                humidity += 4;
            else if (IsHeavyRain)
                humidity += 6;
            else if (IsHellRain)
                humidity += 8;
        }


        private void UpdateTime()
        {
            if (timeFlow.IsExist()) time = timeFlow.ToFloatFormat();
            if (seasonFlow.IsExist())
            {
                day = seasonFlow.day;
                season = seasonFlow.season;
            }
        }

        #endregion
    }
}
using System;
using System.Collections.Generic;





namespace Expansion.Mathematics
{
    /*
    各パラメーター解説

    Q = 熱量
    m = 質量(g)
    c = 比熱
    K = ケルビン温度 K
    S ＝セルシス温度 ℃
    T = 温度変化量
    */


    /// <summary>
    /// 物体の熱運動を簡易的にシュミレートするクラス
    /// </summary>
    public static class Thermodynamics
    {
        public const float ABSOLUTE_TEMPURATURE = 273.15f;


        public static float ToKelvinTemp(float _selsiusTemp)
        {
            float K = _selsiusTemp + ABSOLUTE_TEMPURATURE;
            return K;
        }


        public static float ToSelsiusTemp(float _KelvinTemp)
        {
            float S = _KelvinTemp - ABSOLUTE_TEMPURATURE;
            return S;
        }


        public static float ToSelsiusTemp(Object _object)
        {
            return ToSelsiusTemp(_object.K);
        }


        /// <param name="_c">比熱(g/K)</param>
        /// <param name="m">質量(g)</param>
        /// <param name="_T">温度変化値(K)</param>
        /// <returns>熱量</returns>
        public static float GetQuantityHeat(float _c, float _m, float _T)
        {
            float J = _c * (_m / 1000) * _T;
            return J;
        }


        public static float GetQuantityHeat(Object _object)
        {
            float T = ToSelsiusTemp(_object);
            return GetQuantityHeat(_object.c, _object.m, T);
        }


        /// <param name="m">質量(g)</param>
        /// <param name="_T">温度変化値(K)</param>
        /// <param name="_Q">熱量(J)</param>
        /// <returns>比熱</returns>
        public static float GetSpecificHeat(float _m, float _Q, float _T)
        {
            float c = _Q / (_m * _T);
            return c;
        }


        /// <returns>比熱</returns>
        public static float GetSpecificHeat(Object _object)
        {
            float Q = GetQuantityHeat(_object.c, _object.m, _object.K);
            return GetSpecificHeat(_object.m, Q, _object.K);
        }


        /// <param name="_c">比熱(g/K)</param>
        /// <param name="m">質量(g)</param>
        /// <returns>熱容量</returns>
        public static float GetHeatCapacity(float _c, float _m)
        {
            return _c * _m;
        }


        /// <returns>熱容量を返す</returns>
        public static float GetHeatCapacity(Object _object)
        {
            return GetHeatCapacity(_object.c, _object.m);
        }


        public static float GetHeatEquilibriumA(Object _objectA, Object _objectB)
        {
            float t = 0;

            Object GetWormer()
            {
                bool isA = GetQuantityHeat(_objectA) > GetQuantityHeat(_objectB);
                
                if (isA)
                    return _objectA;
                else
                    return _objectB;
            }

            return 0; // tag test
        }




        /// <summary>
        /// 熱力学で扱う用の物体
        /// </summary>
        public class Object
        {


            /// <summary>
            /// 熱力学で扱う用の物体
            /// </summary>
            /// <param name="_m">質量(g)</param>
            /// <param name="_c">比熱</param>
            /// <param name="_K">温度(K)</param>
            public Object(float _m, float _c, float _K)
            {
                m = _m;
                c = _c;
                K = _K;
            }


            public Object(float _m, Material _material, float _K)
            {
                m = _m;
                c = GetSpecificHeat(_material);
                K = _K;
            }


            public enum Material
            {
                METAL,
                ALUMINUM,
                COPPER,
                CLOTHE,
                PLASTIC,
                WOOD,
                PAPER,
                LEATHER,
                RUBBER,
                GLASS,
                WATER,
                STONE,
                CHINA,
                CERAMIC,
            }


            /// <summary>質量(g)</summary>
            public float m;

            /// <summary>比熱</summary>
            public readonly float c;

            /// <summary>持っている熱量(K)</summary>
            public float K;


            /// <returns>比熱</returns>
            public float GetSpecificHeat(Material _material)
            {
                if (((_material & Material.ALUMINUM) == Material.ALUMINUM))
                    return 0.9f;

                else if (((_material & Material.CERAMIC) == Material.CERAMIC))
                    return 0.8f;

                else if (((_material & Material.CHINA) == Material.CHINA))
                    return 1.3f;

                else if (((_material & Material.CLOTHE) == Material.CLOTHE))
                    return 1.3f;

                else if (((_material & Material.COPPER) == Material.COPPER))
                    return 1.1f;

                else if (((_material & Material.GLASS) == Material.GLASS))
                    return 0.6f;

                else if (((_material & Material.LEATHER) == Material.LEATHER))
                    return 1.3f;

                else if (((_material & Material.METAL) == Material.METAL))
                    return 0.4f;

                else if (((_material & Material.PAPER) == Material.PAPER))
                    return 1.2f;

                else if (((_material & Material.PLASTIC) == Material.PLASTIC))
                    return 1.3f;

                else if (((_material & Material.RUBBER) == Material.RUBBER))
                    return 2;

                else if (((_material & Material.STONE) == Material.STONE))
                    return 0.7f;

                return 1;
            }


            /// <returns>熱伝導率</returns>
            public float GetThermalConductivity(Material _material)
            {
                if (((_material & Material.ALUMINUM) == Material.ALUMINUM))
                    return 0.9f;

                else if (((_material & Material.CERAMIC) == Material.CERAMIC))
                    return 0.8f;

                else if (((_material & Material.CHINA) == Material.CHINA))
                    return 1.3f;

                else if (((_material & Material.CLOTHE) == Material.CLOTHE))
                    return 1.3f;

                else if (((_material & Material.COPPER) == Material.COPPER))
                    return 1.1f;

                else if (((_material & Material.GLASS) == Material.GLASS))
                    return 0.6f;

                else if (((_material & Material.LEATHER) == Material.LEATHER))
                    return 1.3f;

                else if (((_material & Material.METAL) == Material.METAL))
                    return 0.4f;

                else if (((_material & Material.PAPER) == Material.PAPER))
                    return 1.2f;

                else if (((_material & Material.PLASTIC) == Material.PLASTIC))
                    return 1.3f;

                else if (((_material & Material.RUBBER) == Material.RUBBER))
                    return 2;

                else if (((_material & Material.STONE) == Material.STONE))
                    return 0.7f;

                return 1;
            }
        }
    }
}
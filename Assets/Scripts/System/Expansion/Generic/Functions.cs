using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UniRx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


namespace Expansion
{
    /// <summary>
    /// Unityで開発する上で便利な関数をまとめたクラス
    /// </summary>
    public static class Functions
    {
        public delegate void func();


        /// <summary>
        /// インスタンスしたものをまとめて処理する
        /// </summary>
        ///  <returns>インスタンスした物のインデックス</returns>
        // public static GameObject HandleInstance(string tagName, func function, bool isIndexed = false, dynamic[] index0 = null, dynamic[] index1 = null, dynamic[] index2 = null)
        // {
        //     GameObject[] targets = GameObject.FindGameObjectsWithTag(tagName);
        //     int Length = targets.Length;
        //     if (isIndexed && Length != checkLength)
        //     {
        //         if (index0 != null) index0 = new dynamic[Length];
        //         if (index1 != null) index1 = new dynamic[Length];
        //         if (index2 != null) index2 = new dynamic[Length];
        //         checkLength = Length;
        //     }
        //     for (int i = 0; i < targets.Length; i++)
        //     {
        //         function();
        //         return targets[i];
        //     }
        //     return null;
        // }


        /// <summary>
        /// Input.GetKey()の省略形
        /// </summary>
        public static bool IsKey(KeyCode _keycode)
        {
            if (Input.GetKey(_keycode))
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Input.GetKey()の省略形
        /// </summary>
        public static bool IsKey(List<KeyCode> _keycodes)
        {
            for (int i = 0; i < _keycodes.Count; i++)
            {
                if (Input.GetKey(_keycodes[i]))
                    return true;
            }

            return false;
        }


        /// <summary>
        /// Input.GetKeyDown()の省略形
        /// </summary>
        public static bool IsKeyDown(KeyCode _keycode)
        {
            if (Input.GetKeyDown(_keycode))
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Input.GetKeyDown()の省略形
        /// </summary>
        public static bool IsKeyDown(List<KeyCode> _keycodes)
        {
            for (int i = 0; i < _keycodes.Count; i++)
            {
                if (Input.GetKeyDown(_keycodes[i]))
                    return true;
            }

            return false;
        }


        /// <summary>
        /// Input.GetKeyUp()の省略形
        /// </summary>
        public static bool IsKeyUp(KeyCode _keycode)
        {
            if (Input.GetKeyUp(_keycode))
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Input.GetKeyUp()の省略形
        /// </summary>
        public static bool IsKeyUp(List<KeyCode> _keycodes)
        {
            for (int i = 0; i < _keycodes.Count; i++)
            {
                if (Input.GetKeyUp(_keycodes[i]))
                    return true;
            }

            return false;
        }


        /// <summary>
        /// 特定のキーが押されている間特定の処理をする関数
        /// </summary>
        /// <param name="_keycode">対象のキー</param>
        /// <param name="function">処理</param>
        /// <returns>処理されたかどうか</returns>
        public static bool IsKeyAction(KeyCode _keycode, func _function)
        {
            if (Input.GetKey(_keycode))
            {
                _function();
                return true;
            }
            return false;
        }


        /// <summary>
        /// 特定のキーが押し下がった時の特定の処理をする関数
        /// </summary>
        /// <param name="_keycode">対象のキー</param>
        /// <param name="function">処理</param>
        /// <returns>処理されたかどうか</returns>
        public static bool IsKeyDownAction(KeyCode _keycode, func _function)
        {
            if (Input.GetKeyDown(_keycode))
            {
                _function();
                return true;
            }
            return false;
        }


        /// <summary>
        /// 特定のキーが押されて上がりきった時の特定の処理をする関数
        /// </summary>
        /// <param name="_keycode">対象のキー</param>
        /// <param name="function">処理</param>
        /// <returns>処理されたかどうか</returns>
        public static bool IsKeyUpAction(KeyCode _keycode, func _function)
        {
            if (Input.GetKeyUp(_keycode))
            {
                _function();
                return true;
            }
            return false;
        }


        /// <summary>
        /// 左Ctrlに加えて特定のキーが押されているか
        /// </summary>
        /// <param name="_isPress">Input.GetKey~()で指定すること</param>
        public static bool LeftCtrlPlus(bool _isPress) => Input.GetKey(KeyCode.LeftControl) && _isPress;


        /// <summary>
        /// 右Ctrlに加えて特定のキーが押されているか
        /// </summary>
        /// <param name="_isPress">Input.GetKey~()で指定すること</param>
        public static bool RightCtrlPlus(bool _isPress) => Input.GetKey(KeyCode.RightControl) && _isPress;



        /// <summary>
        /// 左Altに加えて特定のキーが押されているか
        /// </summary>
        /// <param name="_isPress">Input.GetKey~()で指定すること</param>
        public static bool LeftAltPlus(bool _isPress) => Input.GetKey(KeyCode.LeftAlt) && _isPress;



        /// <summary>
        /// 右Altに加えて特定のキーが押されているか
        /// </summary>
        /// <param name="_isPress">Input.GetKey~()で指定すること</param>
        public static bool RightAltPlus(bool _isPress) => Input.GetKey(KeyCode.RightAlt) && _isPress;



        /// <summary>
        /// デバッグの時に用いる特定のキー
        /// </summary>
        /// <returns>F1と左altが押されているかどうか</returns>
        public static void DebugKeyDown1(func _OnTrue) { if (LeftAltPlus(Input.GetKeyDown(KeyCode.F1))) Try(() => _OnTrue()); }


        /// <summary>
        /// デバッグの時に用いる特定のキー
        /// </summary>
        /// <returns>F2と左altが押されているかどうか</returns>
        public static void DebugKeyDown2(func _OnTrue) { if (LeftAltPlus(Input.GetKeyDown(KeyCode.F2))) Try(() => _OnTrue()); }



        /// <summary>
        /// デバッグの時に用いる特定のキー
        /// </summary>
        /// <returns>F3と左altが押されているかどうか</returns>
        public static void DebugKeyDown3(func _OnTrue) { if (LeftAltPlus(Input.GetKeyDown(KeyCode.F3))) Try(() => _OnTrue()); }


        /// <summary>
        /// デバッグの時に用いる特定のキー
        /// </summary>
        /// <returns>F4と左altが押されているかどうか</returns>
        public static void DebugKeyDown4(func _OnTrue) { if (LeftAltPlus(Input.GetKeyDown(KeyCode.F4))) Try(() => _OnTrue()); }


        /// <summary>
        /// デバッグの時に用いる特定のキー
        /// </summary>
        /// <returns>F5と左altが押されているかどうか</returns>
        public static void DebugKeyDown5(func _OnTrue) { if (LeftAltPlus(Input.GetKeyDown(KeyCode.F5))) Try(() => _OnTrue()); }


        /// <summary>
        /// デバッグの時に用いる特定のキー
        /// </summary>
        /// <returns>右Altキーを押した状態でF1キーを押している間にtrueが返される</returns>
        public static void DebugKey1(func _OnTrue) { if (LeftCtrlPlus(Input.GetKey(KeyCode.F1))) Try(() => _OnTrue()); }


        /// <summary>
        /// デバッグの時に用いる特定のキー
        /// </summary>
        /// <returns>右Altキーを押した状態でF2キーを押している間にtrueが返される</returns>
        public static void DebugKey2(func _OnTrue) { if (LeftCtrlPlus(Input.GetKey(KeyCode.F2))) Try(() => _OnTrue()); }



        /// <summary>
        /// デバッグの時に用いる特定のキー
        /// </summary>
        /// <returns>右Altキーを押した状態でF3キーを押している間にtrueが返される</returns>
        public static void DebugKey3(func _OnTrue) { if (LeftCtrlPlus(Input.GetKey(KeyCode.F3))) Try(() => _OnTrue()); }


        /// <summary>
        /// デバッグの時に用いる特定のキー
        /// </summary>
        /// <returns>右Altキーを押した状態でF4キーを押している間にtrueが返される</returns>
        public static void DebugKey4(func _OnTrue) { if (LeftCtrlPlus(Input.GetKey(KeyCode.F4))) Try(() => _OnTrue()); }


        /// <summary>
        /// デバッグの時に用いる特定のキー
        /// </summary>
        /// <returns>右Altキーを押した状態でF5キーを押している間にtrueが返される</returns>
        public static void DebugKey5(func _OnTrue) { if (LeftCtrlPlus(Input.GetKey(KeyCode.F5))) Try(() => _OnTrue()); }


        /// <summary>
        /// マウスを右クリックしている間にtrueを返す
        /// </summary>
        public static bool MouseRight() { if (Input.GetMouseButton(1)) return true; return false; }


        /// <returns>マウスを左クリックしている間にtrueを返す</returns>
        public static bool MouseLeft() { if (Input.GetMouseButton(0)) return true; return false; }


        /// <returns>マウスを右クリックを押し下げたらtrueを返す</returns>
        public static bool MouseRightDown() { if (Input.GetMouseButtonDown(1)) return true; return false; }


        /// <returns>マウスを左クリックを押し下げたらtrueを返す</returns>
        public static bool MouseLeftDown() { if (Input.GetMouseButtonDown(0)) return true; return false; }


        public static void WaitAction(func Function, double milli = 0, double sec = 0, double min = 0, double hour = 0, double day = 0)
        {
            const short perSecond = 100;
            const short perMinute = perSecond * 60;
            const float perHour = perMinute * 60;
            const float perDay = perHour * 24;

            double waitingTime = 0;

            waitingTime += milli;
            waitingTime += sec * perSecond;
            waitingTime += min * perMinute;
            waitingTime += hour * perHour;
            waitingTime += day * perDay;

            Observable.Timer(System.TimeSpan.FromMilliseconds(waitingTime)).
            Subscribe(_ => Function());
        }


        ///<summary>
        ///一フレームだけ反対のbooleanを返す
        ///</summary>
        public static void InstantaneousBoolean(bool _bool)
        {
            _bool = !_bool;
            WaitAction(
                sec: Time.deltaTime,
                Function: () =>
               {
                   _bool = !_bool;
               }
            );
        }


        public static void HandleInstanceAction(GameObject _return, string tagName, func Function, int index = 0)
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag(tagName);
            int length = objects.Length;
            for (int i = 0; i < objects.Length; i++)
            {
                index = i;
                _return = objects[i];
                Function();
            }
        }


        public static void HandleHour(int hour, float addHourPerSec)
        {
            if (hour == 24) hour = 0;
            WaitAction(
                Function: () =>
                {
                    ++hour;
                },
                sec: addHourPerSec
            );
        }


        ///<summary>
        ///_probabilityの確率で_returnを返す
        ///</summary>
        public static bool Probability(float _probability, bool _return = true)
        {
            float _random = UnityEngine.Random.Range(1, 100);
            if (_probability >= 100) _probability = 100;
            if (_random <= _probability) return _return;
            else return !_return;
        }


        ///<summary>
        ///_probabilityの確率で_function関数を実行する
        ///</summary>
        public static void ProbabilityAction(int _probability, func _function)
        {
            if (Probability(_probability))
            {
                _function();
            }
        }


        public static void EveryWaitAction(bool _isWaiting, int _waitingSec, func _function)
        {
            if (!_isWaiting)
            {
                _isWaiting = true;
                WaitAction(
                    sec: _waitingSec,
                    Function: () =>
                    {
                        _function();
                        _isWaiting = false;
                    }
                );
            }
        }


        ///<summary>
        ///大元の親を返す
        ///</summary>
        public static GameObject RootParent(GameObject _objSelf)
        {
            return _objSelf.transform.root.gameObject;
        }


        ///<summary>
        ///_objSelfの親を返す
        ///</summary>
        public static GameObject Parent(GameObject _objSelf)
        {
            return _objSelf.transform.parent.gameObject;
        }


        ///<summary>
        ///上から_possition目の_objSelfの子オブジェクトをとってくる
        ///</summary>
        public static GameObject Child(GameObject _objSelf, int _possition)
        {
            return _objSelf.transform.GetChild(_possition).gameObject;
        }


        ///<summary>
        ///nullかどうか確認し警告する
        ///</summary>
        public static void CheckNull(object _target, string _varName = null)
        {
            string _text = "nullが返されています";
            NotNullAction(_varName, () => _text = "'" + _varName + "' はnullが返されています");
            if (_target == null) Debug.LogWarning(_text);
        }


        ///<summary>
        ///nullだった場合trueを返す
        ///</summary>
        public static bool IsNull(this object _target)
        {
            if (_target == null) return true;
            return false;
        }


        /// <returns>オブジェクトの中身があるかどうかを返す</returns>
        public static bool IsExist(this object _target) { return !_target.IsNull(); }



        ///<summary>
        ///nullではなかった場合_function()を実行する
        ///</summary>
        public static void NotNullAction(object _target, func _function, string _varName = null)
        {
            if (_target != null) _function();
            else if (_varName != null) Debug.LogWarning("'" + _varName + "' はnullが返されています");
        }


        ///<summary>
        ///ゲームを一時停止する
        ///</summary>
        public static void PauseGame()
        {
            Time.timeScale = 0;
        }


        ///<summary>
        ///ゲームを再生させる
        ///</summary>
        public static void PlayGame()
        {
            Time.timeScale = 1;
        }


        ///<summary>
        ///ゲーム内の時間の早さを設定する(デフォルトは1)
        ///</summary>
        public static void SetTimeScale(float _speed)
        {
            Time.timeScale = _speed;
        }


        ///<summary>
        ///オブジェクトの名前をprint()する
        ///</summary>
        public static void SayName(GameObject _objTarget)
        {
            Debug.Log("Say name: " + "'" + _objTarget.name + "'");
        }


        ///<summary>
        ///フレームレートを設定する
        ///</summary>
        public static void SetFPS(int _fps)
        {
            Application.targetFrameRate = _fps;
        }


        public static bool IsPlus(this int _value) { return _value >= 0; }
        public static bool IsPlus(this float _value) { return _value >= 0; }
        public static bool IsPlus(this Range.Int _rng) { return _rng >= 0; }
        public static bool IsPlus(this Range.Float _rng) { return _rng >= 0; }


        public static bool IsMinus(this int _value) { return _value < 0; }
        public static bool IsMinus(this float _value) { return _value < 0; }
        public static bool IsMinus(this Range.Int _rng) { return _rng < 0; }
        public static bool IsMinus(this Range.Float _rng) { return _rng < 0; }


        ///<summary>
        ///変数が負の場合と正の場合で処理を分ける
        ///</summary>
        public static void DoByPlusOrMinus(float _target, func _plus, func _minValueus)
        {
            if (IsPlus(_target)) _plus();
            else _minValueus();
        }


        ///<summary>
        ///色と色の差を求め変化させたい時間から一秒ごとの減数値を返す
        ///</summary>
        public static float[] ChangeColorPer(Color _currentColor, Color _changeTo, int _sec)
        {
            float[] _return = null;
            float[] _diffRGB = null;
            float[] _currentRGB = {
            _currentColor.r,
            _currentColor.g,
            _currentColor.b
        };
            float[] _changeToRGB = {
            _changeTo.r,
            _changeTo.g,
            _changeTo.b
        };
            void ConputeDiffrent()
            {
                for (int i = 0; i < 2; i++)
                {
                    _diffRGB[i] = _currentRGB[i] - _changeToRGB[i];
                }
            }
            void ConputeIncreaseEverySec()
            {
                for (int i = 0; i < 2; i++)
                {
                    _return[i] = _diffRGB[i] / _sec;
                }
            }

            ConputeDiffrent();
            ConputeIncreaseEverySec();
            CheckNull(_return, "_return in ChangeColorPer function");

            return _return;
        }



        /// <summary>
        /// 対象のオーディオにオーディオクリップの中からランダムに選び再生する
        /// </summary>
        public static void AudioPlayRandomly(AudioSource _auds, AudioClip[] _audc)
        {
            int _index = UnityEngine.Random.Range(0, _audc.Length);
            _auds.PlayOneShot(_audc[_index]);
        }



        /// <summary>
        /// 対象の値の最小値から最大値からの値の進行度を設定したスケールに置き換えそのスケールでの進行度合いの値をfloat型で返す
        /// </summary>
        /// <param name="_value">対象の値</param>
        /// 
        /// <param name="_minValue">値の最小値</param>
        /// <param name="_maxValue">値の最大値</param>
        /// 
        /// <param name="_minScale">置き換えるスケールの最小値</param>
        /// <param name="_maxScale">置き換えるスケールの最大値</param>
        /// <returns></returns>
        public static float ReturnProgress(
            float _value,
            float _minValue, float _maxValue,
            float _minScale = 0, float _maxScale = 1)
        {
            if (_minValue > _maxValue) _minValue = _maxValue;
            if (_minScale > _maxScale) _minScale = _maxScale;
            if (_value > _maxValue) _value = _maxValue;
            float _valueRange = _maxValue - _minValue;
            float _scaleRange = _maxScale - _minScale;
            float _compile = 1 / (_valueRange / _scaleRange);
            float _return = (float)_compile * _value;
            if (_return > _maxScale) _return = _maxScale;
            if (_return < _minScale) _return = _minScale;

            if (_value == 0) return _minScale;
            else return _minScale + _return;
        }


        /// <summary>
        /// 対象の文字列をAESアルゴリズムで暗号化する関数
        /// </summary>
        /// <param name="_text">暗号化する文字列</param>
        /// <param name="_key">複合化するときの秘密鍵</param>
        /// <param name="_keySize">公開鍵のサイズ</param>
        /// <param name="_blockSize">暗号文の長さ</param>
        /// <returns></returns>
        public static byte[] EncryptAES(string _text, string _iv = "f@ghpjfg1hj87gn8",
            string _key = "1fa68se43sa2d4fe", int _keySize = 128, int _blockSize = 128)
        {
            byte[] _data = System.Text.Encoding.UTF8.GetBytes(_text);

            AesManaged aes = new AesManaged();
            aes.KeySize = _keySize;
            aes.BlockSize = _blockSize;
            aes.Mode = CipherMode.CBC;
            aes.IV = Encoding.UTF8.GetBytes(_iv);
            aes.Key = Encoding.UTF8.GetBytes(_key);
            aes.Padding = PaddingMode.PKCS7;

            byte[] _encrypted = aes.CreateEncryptor().TransformFinalBlock(_data, 0, _data.Length);
            return _encrypted;
        }


        /// <summary>
        /// AESで暗号化した文を複合化する関数
        /// </summary>
        /// <param name="_data">複合化する暗号文</param>
        /// <param name="_key">秘密鍵</param>
        /// <param name="_keySize">公開鍵のサイズ</param>
        /// <param name="_blockSize">暗号文の長さ</param>
        /// <returns></returns>
        public static string DecryptAES(byte[] _data, string _iv = "f@ghpjfg1hj87gn8",
            string _key = "1fa68se43sa2d4fe", int _keySize = 128, int _blockSize = 128)
        {
            AesManaged aes = new AesManaged();
            aes.KeySize = _keySize;
            aes.BlockSize = _blockSize;
            aes.Mode = CipherMode.CBC;
            aes.IV = Encoding.UTF8.GetBytes(_iv);
            aes.Key = Encoding.UTF8.GetBytes(_key);
            aes.Padding = PaddingMode.PKCS7;

            byte[] _decrypted = aes.CreateDecryptor().TransformFinalBlock(_data, 0, _data.Length);
            string _toString = Encoding.UTF8.GetString(_decrypted);

            return _toString;
        }


        public static void ListsToDictionary<Key, Value>(List<Key> _key, List<Value> _value,
            Dictionary<Key, Value> _dic)
        {
            _dic = new Dictionary<Key, Value>();
            for (int i = 0; i < _key.Count; i++)
            {
                _dic.Add(_key[i], _value[i]);
            }
        }


        public static void Warning(string _explanation, string _position)
        {
            Debug.LogWarning(
                WarningFormat(_explanation, _position)
            );
        }


        public static string WarningFormat(string _explanation, string _position)
        {
            return $"警告: {_explanation}\n\n場所: {_position}";
        }


        public static Vector3 UnaffectedRotation(GameObject _objSelf)
        {
            Vector3 parentRot = _objSelf.transform.parent.localEulerAngles;
            Vector3 currentRot = _objSelf.transform.localEulerAngles;

            Vector2 updateRot = currentRot - parentRot;
            return updateRot;
        }


        /// <summary>
        /// trueの場合とfalseの場合で行う処理を分ける
        /// </summary>
        /// <param name="_boolean">判定</param>
        /// <param name="_OnTrue">判定がtrueだった場合にする処理</param>
        /// <param name="_OnFalse">判定がfalseだった場合にする処理</param>
        public static void BooleanExecute(bool _boolean, func _OnTrue, func _OnFalse)
        {
            if (_boolean) _OnTrue();
            else _OnFalse();
        }


        /// <summary>
        /// 特定の処理をさせて、何かしらのバグで処理しきれなかった場合は警告をログに出す
        /// </summary>
        /// <param name="_try">処理</param>
        /// <param name="_errorMassage">処理しきれなかった場合のログに出すメッセージ</param>
        /// <returns>処理しきれたかどうか</returns>
        public static bool Try(func _try, string _errorMassage = null)
        {
            try
            {
                _try();
                return true;
            }
            catch (Exception e)
            {
                if (_errorMassage.IsNull()) _errorMassage = e.Message;

                Debug.LogError(WarningFormat(
                    _errorMassage,
                    $"{e.StackTrace}\n"
                ));
                return false;
            }
        }


        public static void Log(object _object) => Debug.Log(_object.ToString());


        public static string RandomlyString(int _length)
        {
            string text = null;
            string characters = "abcdefghijklmnopqrstuvwxyz";
            System.Random random = new System.Random();

            for (int i = 0; i < _length; i++)
            {
                text += characters[random.Next(characters.Length)];
            }

            return text;
        }


        public static int RandomlyInt(int _min, int _max) { return UnityEngine.Random.Range(_min, _max); }


        public static float RandomlyFloat(float _min, float _max) { return UnityEngine.Random.Range(_min, _max); }


        public static int AsRounded(this float v) { return (int)System.Math.Round(v, 0); }


        public static float AsRounded(this float v, int _decimal) { return (float)System.Math.Round(v, _decimal); }


        public static List<T> Add<T>(this List<T> _target, List<T> _additional)
        {
            for (int i = 0; i < _additional.Count; i++)
            {
                _target.Add(_additional[i]);
            }
            return _target;
        }


        /// <summary>
        /// _keyのすべてがtrueだったら値を返す
        /// </summary>
        /// <param name="_key">対象の鍵</param>
        /// <returns>_keyがすべてtrueかどうか</returns>
        public static bool KeyBoolean(List<bool> _key)
        {
            foreach (bool check in _key)
            {
                if (check)
                    return true;
            }
            return false;
        }


        /// <summary>
        /// _keyのすべてがtrueだったら値を返す
        /// </summary>
        /// <param name="_key">対象の鍵</param>
        /// <returns>_keyがすべてtrueかどうか</returns>
        public static bool KeyBoolean(bool[] _key)
        {
            foreach (bool check in _key)
            {
                if (check)
                    return true;
            }
            return false;
        }


        /// <summary>
        /// _keyのすべてがtrueだったら値を返す
        /// </summary>
        /// <param name="_key">対象の鍵</param>
        /// <returns>_return</returns>
        public static int KeyInteger(List<bool> _key, int _return)
        {
            foreach (bool check in _key)
            {
                if (!check)
                    return 0;
            }
            return _return;
        }


        /// <summary>
        /// _keyのすべてがtrueだったら値を返す
        /// </summary>
        /// <param name="_key">対象の鍵</param>
        /// <returns>_return</returns>
        public static int KeyInteger(bool[] _key, int _return)
        {
            foreach (bool check in _key)
            {
                if (!check)
                    return 0;
            }
            return _return;
        }


        /// <summary>
        /// _keyのすべてがtrueだったら値を返す
        /// </summary>
        /// <param name="_key">対象の鍵</param>
        /// <returns>_return</returns>
        public static float KeyFloat(List<bool> _key, float _return)
        {
            foreach (bool check in _key)
            {
                if (!check)
                    return 0;
            }
            return _return;
        }


        /// <summary>
        /// _keyのすべてがtrueだったら値を返す
        /// </summary>
        /// <param name="_key">対象の鍵</param>
        /// <returns>_return</returns>
        public static float KeyFloat(bool[] _key, float _return)
        {
            foreach (bool check in _key)
            {
                if (!check)
                    return 0;
            }
            return _return;
        }


        /// <summary>
        /// _unKeyのいずれかがtrueだったら値を返す
        /// </summary>
        /// <returns>_unKeyのいずれかがtrueかどうか</returns>
        public static bool UnKeyBoolean(List<bool> _unKey)
        {
            foreach (bool check in _unKey)
            {
                if (check)
                    return true;
            }
            return false;
        }


        /// <summary>
        /// _unKeyのいずれかがtrueだったら値を返す
        /// </summary>
        /// <returns>_unKeyのいずれかがtrueかどうか</returns>
        public static bool UnKeyBoolean(bool[] _unKey)
        {
            foreach (bool check in _unKey)
            {
                if (check)
                    return true;
            }
            return false;
        }


        /// <summary>
        /// _unKeyのいずれかがtrueだったら値を返す
        /// </summary>
        /// <returns>_unKeyのいずれかがtrueかどうか</returns>
        public static int UnKeyInteger(List<bool> _unKey, int _return)
        {
            foreach (bool check in _unKey)
            {
                if (check)
                    return _return;
            }
            return 0;
        }


        /// <summary>
        /// _unKeyのいずれかがtrueだったら値を返す
        /// </summary>
        /// <returns></returns>
        public static int UnKeyInteger(bool[] _unKey, int _return)
        {
            foreach (bool check in _unKey)
            {
                if (check)
                    return _return;
            }
            return 0;
        }


        /// <summary>
        /// _unKeyのいずれかがtrueだったら値を返す
        /// </summary>
        /// <returns></returns>
        public static float UnKeyFloat(List<bool> _unKey, float _return)
        {
            foreach (bool check in _unKey)
            {
                if (check)
                    return _return;
            }
            return 0;
        }


        /// <summary>
        /// _unKeyのいずれかがtrueだったら値を返す
        /// </summary>
        /// <returns></returns>
        public static float UnKeyFloat(bool[] _unKey, float _return)
        {
            foreach (bool check in _unKey)
            {
                if (check)
                    return _return;
            }
            return 0;
        }


        public static bool EnumEqualTo<EnumT>(this EnumT _leftEnum, EnumT _rightEnum)
        {
            return _leftEnum.Equals(_rightEnum);
        }
    }
}

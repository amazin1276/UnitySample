using UnityEngine;
using System.Collections.Generic;


namespace Expansion
{
    /// <summary>
    /// Unityで2Dゲームを開発する上で便利な関数をまとめたクラス
    /// </summary>
    public static class Functions2D
    {
        
        ///<summary>
        ///対象物を動かす
        ///</summary>
        public static void Move(GameObject target, Vector2 direction, float speed = 5)
        {
            target.transform.Translate(direction * speed * Time.deltaTime);
        }

        
        ///<summary>
        ///対象物を動かす
        ///</summary>
        public static void Move(GameObject target, Quaternion direction, float speed = 5)
        {
            Vector2 rot = new Vector2(direction.x, direction.y);
            target.transform.Translate(rot * speed);
        }

        
        ///<summary>
        ///対象物を動かす
        ///</summary>
        public static void MoveAction(GameObject target, Vector2 direction, Functions.func Function, float speed = 5)
        {
            Function();
            target.transform.Translate(direction * speed * Time.deltaTime);
        }

        
        ///<summary>
        ///対象物を動かす
        ///</summary>
        public static void MoveAction(GameObject target, Quaternion direction, Functions.func Function, float speed = 10)
        {
            Vector2 rot = new Vector2(direction.x, direction.y);
            target.transform.Translate(rot * speed);
        }

        
        ///<summary>
        ///targetから弾を飛ばし、弾がrangeを出たら弾を消し、撃った時の処理を扱える
        ///</summary>
        public static void Fire(GameObject target, GameObject Bullet, string BulletTag, bool isFire = true, float speed = 50, float range = 20)
        {
            if (isFire)
            {
                UnityEngine.MonoBehaviour.Instantiate(
                    Bullet, target.transform.position, target.transform.rotation);
            }
            GameObject[] bullets = GameObject.FindGameObjectsWithTag(BulletTag);
            foreach (GameObject item in bullets)
            {
                Move(item, Vector2.right, speed);
                if (item.transform.position.x > range) UnityEngine.MonoBehaviour.Destroy(item);
                if (item.transform.position.y > range) UnityEngine.MonoBehaviour.Destroy(item);
                if (item.transform.position.x < -range) UnityEngine.MonoBehaviour.Destroy(item);
                if (item.transform.position.y < -range) UnityEngine.MonoBehaviour.Destroy(item);
            }
        }

        
        ///<summary>
        ///targetから弾を飛ばし、弾がrangeを出たら弾を消し
        ///</summary>
        public static void FireAction(GameObject target, GameObject Bullet, string BulletTag,
            bool isFire = true, float speed = 50, float range = 20, Functions.func onFire = null)
        {
            if (isFire)
            {
                onFire();
                UnityEngine.MonoBehaviour.Instantiate(
                    Bullet, target.transform.position, target.transform.rotation);
            }
            GameObject[] bullets = GameObject.FindGameObjectsWithTag(BulletTag);
            Vector3 _range = new Vector2(range + target.transform.position.x, range + target.transform.position.y);
            foreach (GameObject item in bullets)
            {
                Move(item, Vector2.right, speed);
                if (item.transform.position.x > _range.x) UnityEngine.MonoBehaviour.Destroy(item);
                if (item.transform.position.y > _range.y) UnityEngine.MonoBehaviour.Destroy(item);
                if (item.transform.position.x < -_range.x) UnityEngine.MonoBehaviour.Destroy(item);
                if (item.transform.position.y < -_range.y) UnityEngine.MonoBehaviour.Destroy(item);
            }
        }


        public static void FacingHorizontalAction(bool isRight, Functions.func Right, bool isLeft, Functions.func Left)
        {
            if (isRight) Right();
            if (isLeft) Left();
        }


        public static void FacingVerticalAction(bool isUp, Functions.func Up, bool isDown, Functions.func Down)
        {
            if (isUp) Up();
            if (isDown) Down();
        }


        public static void Jump(Rigidbody2D targetRB, bool isJump, float power = 10)
        {
            if (isJump)
            {
                targetRB.AddForce(Vector2.up * power * Time.deltaTime, ForceMode2D.Impulse);
            }
        }


        public static void JumpAction(Rigidbody2D targetRB, bool isJump, float power = 10, Functions.func func = null)
        {
            if (isJump)
            {
                func();
                targetRB.AddForce(Vector2.up * power * Time.deltaTime, ForceMode2D.Impulse);
            }
        }

        
        ///　<summary>
        ///objMouseの座標をマウスと同期させる
        ///　</summary>
        public static void Mouse(GameObject objMouse, Sprite mouseSprite = null, float mousePositionZ = -3, float sensitivity = 1)
        {
            Vector3 posMouse = Input.mousePosition;
            Vector3 posWorld = Camera.main.ScreenToWorldPoint(posMouse);
            objMouse.transform.position = posWorld * sensitivity;

            Vector3 currentPos = objMouse.transform.position;
            objMouse.transform.position = new Vector3(currentPos.x, currentPos.y, mousePositionZ);

            SpriteRenderer spriteRenderer = objMouse.GetComponent<SpriteRenderer>();
            if (mouseSprite != null)
            {
                spriteRenderer.sprite = mouseSprite;
            }
        }

        
        ///　<summary>
        ///　指定のオブジェクトへの方向に回転させる
        ///　</summary>
        public static void LookAt2D(GameObject self, GameObject target, Vector2 direction)
        {
            self.transform.LookAt2D(target.transform, direction);
        }

        
        ///　<summary>
        ///カメラの初期設定
        ///　</summary>
        public static void SetInitialCamara(GameObject Camera, bool isFollowPlayer = false,
            bool isMainCamara = true, GameObject followingTarget = null)
        {
            if (isFollowPlayer) Camera.transform.position = followingTarget.transform.position;
            if (isMainCamara) Camera.tag = "MainCamera";
        }

        
        public static void SetLayer(GameObject target, int layerNumber)
        {
            Vector3 pos = target.transform.position;
            float posZ = -layerNumber / 10;
            pos.z = posZ;
            target.transform.position = pos;
        }

        
        public static void SetLocalLayer(GameObject target, int layerNumber)
        {
            Vector3 pos = target.transform.localPosition;
            float posZ = -layerNumber / 10;
            pos.z = posZ;
            target.transform.localPosition = pos;
        }

        
        public static float ReturnLayer(int layerNumber)
        {
            return -layerNumber / 10;
        }

        
        public static void SoundToReality(float _distance, AudioSource _audioSource = null,
            AudioHighPassFilter _audioHighPassFilter = null, AudioReverbFilter _audioReverbFilter = null,
            float _Sensitivity = 1, float _maxAudioVolume = 0.8f)
        {
            if (_audioSource != null)
            {
                //音量の調節
                float _volume = (_distance / 125) * _Sensitivity;
                _audioSource.volume = _maxAudioVolume - _volume;
                if (_audioSource.volume <= 0.01f) _audioSource.volume = 0.01f;
            }

            if (_audioHighPassFilter != null)
            {
                //ハイバス共鳴Qの調節
                float _adjRes = (_distance / 10) * _Sensitivity;
                _audioHighPassFilter.highpassResonanceQ = 1 + _adjRes;
                if (_audioHighPassFilter.highpassResonanceQ >= 10) _audioHighPassFilter.highpassResonanceQ = 10;

                //カットオフ周波数の調節
                float _adjCut = _distance * 4.4f * _Sensitivity;
                _audioHighPassFilter.cutoffFrequency = 10 + _adjCut;
                if (_audioHighPassFilter.cutoffFrequency >= 450) _audioHighPassFilter.cutoffFrequency = 450;
            }

            if (_audioReverbFilter != null)
            {
                //Reverb Filter　の調節
                float _roomHf = _distance * 50 * _Sensitivity;
                _audioReverbFilter.dryLevel = 0;
                _audioReverbFilter.room = 0;
                _audioReverbFilter.roomHF = -10000 + _roomHf;
                _audioReverbFilter.roomLF = -2500;
                _audioReverbFilter.decayTime = 0.1f;
                _audioReverbFilter.decayHFRatio = 0.1f;
                _audioReverbFilter.reflectionsLevel = -10000;
                _audioReverbFilter.reflectionsDelay = 0;
                _audioReverbFilter.reverbLevel = 2000;
                _audioReverbFilter.reverbDelay = 0.1f;
                _audioReverbFilter.hfReference = 5000;
                _audioReverbFilter.lfReference = 250;
                _audioReverbFilter.diffusion = 100;
                _audioReverbFilter.density = 50;
            }
        }

        
        public static Vector2 DirectionTo(GameObject _objSelf, GameObject _objTarget)
        {
            Vector2 _return = _objTarget.transform.position - _objSelf.transform.position;
            return _return;
        }

        
        public static Vector2 DirectionTo(Vector2 _posSelf, Vector2 _posTarget)
        {
            Vector2 _return = _posTarget - _posSelf;
            return _return;
        }

        
        public static GameObject GetClosestObject(string _tag, string _name, int _findRange, Vector2 _posSelf)
        {
            List<GameObject> _targets = new List<GameObject>();

            void _FindTargets()
            {
                GameObject[] _founds = GameObject.FindGameObjectsWithTag(_tag);
                foreach (GameObject item in _founds)
                {
                    bool _isTarget = item.name == _name;
                    _targets.Add(item);
                }
            }

            GameObject _FindClosestTarget()
            {
                bool _isTargetNull = _targets.Count == 0;
                if (_isTargetNull) return null;

                List<float> distances = new List<float>();

                foreach (GameObject item in _targets)
                {
                    Vector2 posItem = item.transform.position;
                    float distance = Vector2.Distance(_posSelf, posItem);

                    if (distance <= _findRange) distances.Add(distance);
                }

                if (distances.Count == 0) return null;

                distances.Sort();
                foreach (GameObject item in _targets)
                {
                    Vector2 posItem = item.transform.position;
                    float distance = Vector2.Distance(_posSelf, posItem);

                    if (distance == distances[0]) return item;
                }

                return null;
            }

            _FindTargets();
            return _FindClosestTarget();
        }

        
        public static GameObject GetClosestObject(string _tag, int _layer, int _findRange, Vector2 _posSelf)
        {
            List<GameObject> _targets = new List<GameObject>();

            void _FindTargets()
            {
                GameObject[] _founds = GameObject.FindGameObjectsWithTag(_tag);
                foreach (GameObject item in _founds)
                {
                    bool _isTarget = item.layer == _layer;
                    _targets.Add(item);
                }
            }

            GameObject _FindClosestTarget()
            {
                bool _isTargetNull = _targets.Count == 0;
                if (_isTargetNull) return null;

                List<float> distances = new List<float>();

                foreach (GameObject item in _targets)
                {
                    Vector2 posItem = item.transform.position;
                    float distance = Vector2.Distance(_posSelf, posItem);

                    if (distance <= _findRange) distances.Add(distance);
                }

                if (distances.Count == 0) return null;

                distances.Sort();
                foreach (GameObject item in _targets)
                {
                    Vector2 posItem = item.transform.position;
                    float distance = Vector2.Distance(_posSelf, posItem);

                    if (distance == distances[0]) return item;
                }

                return null;
            }

            _FindTargets();
            return _FindClosestTarget();
        }

        
        public static GameObject GetClosestObject(string _tag, int _findRange, Vector2 _posSelf)
        {
            //候補のゲームオブジェクトを取得
            List<GameObject> _targets;
            _targets = new List<GameObject>(GameObject.FindGameObjectsWithTag(_tag));

            GameObject _FindClosestTarget()
            {
                bool _isTargetNull = _targets.Count == 0;
                if (_isTargetNull) return null;

                List<float> distances = new List<float>();

                foreach (GameObject item in _targets)
                {
                    Vector2 posItem = item.transform.position;
                    float distance = Vector2.Distance(_posSelf, posItem);

                    if (distance <= _findRange) distances.Add(distance);
                }

                if (distances.Count == 0) return null;

                distances.Sort();
                foreach (GameObject item in _targets)
                {
                    Vector2 posItem = item.transform.position;
                    float distance = Vector2.Distance(_posSelf, posItem);

                    if (distance == distances[0]) return item;
                }

                return null;
            }

            return _FindClosestTarget();
        }

        
        public static bool GetClosestObject(string _tag, int _findRange, Vector2 _posSelf, out GameObject _objTarget)
        {
            //候補のゲームオブジェクトを取得
            List<GameObject> _targets;
            _targets = new List<GameObject>(GameObject.FindGameObjectsWithTag(_tag));

            GameObject _FindClosestTarget()
            {
                bool _isTargetNull = _targets.Count == 0;
                if (_isTargetNull) return null;

                List<float> distances = new List<float>();

                foreach (GameObject item in _targets)
                {
                    Vector2 posItem = item.transform.position;
                    float distance = Vector2.Distance(_posSelf, posItem);

                    if (distance <= _findRange) distances.Add(distance);
                }

                if (distances.Count == 0) return null;

                distances.Sort();
                foreach (GameObject item in _targets)
                {
                    Vector2 posItem = item.transform.position;
                    float distance = Vector2.Distance(_posSelf, posItem);

                    if (distance == distances[0]) return item;
                }

                return null;
            }

            _objTarget = _FindClosestTarget();
            bool isFound = _objTarget != null;
            return isFound;
        }
    }

    
    
    public static class TransformExtensions
    {
        /// <summary>
        /// 指定のオブジェクトの方向に回転する
        /// </summary>
        /// <param name="self">Self.</param>
        /// <param name="target">Target.</param>
        /// <param name="forward">正面方向</param>
        public  static void LookAt2D(this Transform self, Transform target, Vector2 forward)
        {
            LookAt2D(self, target.position, forward);
        }


        public static void LookAt2D(this Transform self, Vector3 target, Vector2 forward)
        {
            var forwardDiff = GetForwardDiffPoint(forward);
            Vector3 direction = target - self.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            self.rotation = Quaternion.AngleAxis(angle - forwardDiff, Vector3.forward);
        }
        

        /// <summary>
        /// 正面の方向の差分を算出する
        /// </summary>
        /// <returns>The forward diff point.</returns>
        /// <param name="forward">Forward.</param>
        static private float GetForwardDiffPoint(Vector2 forward)
        {
            if (Equals(forward, Vector2.up)) return 90;
            if (Equals(forward, Vector2.right)) return 0;
            return 0;
        }
    }
}
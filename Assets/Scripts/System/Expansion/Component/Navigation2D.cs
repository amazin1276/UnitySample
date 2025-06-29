using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



namespace Expansion.Components
{
    [DisallowMultipleComponent, RequireComponent(typeof(PolygonCollider2D))]
    public class Navigation2D : MonoBehaviour
    {
        public enum ModeSelect { None, Navigator, Obstacle }
        [SerializeField]
        public ModeSelect Mode;


        [System.Serializable]
        public class NavigatorVariables
        {
            [SerializeField] public GameObject NavigateObject;
            [SerializeField] public GameObject DestinationObject;
            [HideInInspector] public PolygonCollider2D HitCollider;

            [SerializeField, Space(10)] public float MoveSpeed;
            [SerializeField] public float Distance;
            [SerializeField] public Vector2 DestinationPosition;

            [SerializeField, Space(10)] public bool DebugMode;

            [SerializeField, Space(10)] public UnityEvent OnEvade;
        }
        [SerializeField, Space(10)]
        public NavigatorVariables Navigator = new NavigatorVariables();


        [System.Serializable]
        public class ObstacleVariable
        {
            [SerializeField] public PolygonCollider2D collider;

            [SerializeField, Space(10)] public float Size;

            [SerializeField, Space(10)] public UnityEvent OnEvade;
        }
        [SerializeField]
        public ObstacleVariable Obstacle = new ObstacleVariable();


        #region 変数
        private Vector2 posDestination; // 目的地
        private Vector2 posEvadeTo; // 回避する座標
        private Vector2 posSelf;
        private bool isEvade; // 回避するかどうか
        #endregion


        void Start()
        {
        }

        void Update()
        {
            this.gameObject.layer = 11;
            posSelf = this.gameObject.transform.position;
            Navigator.Distance = Vector2.Distance(posDestination, posSelf);

            NavigatorFunctions();
            ObstacleFunctions();
        }

        void NavigatorFunctions()
        {
            bool _isNavigatorMode = Mode == ModeSelect.Navigator;
            if (!_isNavigatorMode) return;

            SetDestination();
            FindObstacle();
            GetEvadePoint();
            Navigate();
        }

        void ObstacleFunctions()
        {
            bool _isObsticleMode = Mode == ModeSelect.Obstacle;
            if (!_isObsticleMode) return;
        }

        #region Navigation Methots
        private void SetDestination()
        {
            bool _isNull = Navigator.DestinationObject == null;
            if (_isNull) posDestination = Navigator.DestinationPosition;
            else posDestination = Navigator.DestinationObject.transform.position;
            Navigator.DestinationPosition = posDestination;
        }

        private void FindObstacle()
        {
            float _DrawRange = Navigator.Distance;
            Ray2D ray = new Ray2D(posSelf, posDestination - posSelf);
            RaycastHit2D[] hit = Physics2D.RaycastAll(ray.origin, ray.direction, _DrawRange);

            // ray を可視化
            void _ForDebug()
            {
                if (!Navigator.DebugMode) return;
                Color _rayColor = (Color)new Color32(252, 15, 192, 255); // Pink
                Debug.DrawRay(ray.origin, ray.direction * _DrawRange, _rayColor, Time.deltaTime, false);
            }
            // 障害物のコリジョンをとってくる
            PolygonCollider2D _GetObstacleCollition()
            {
                List<PolygonCollider2D> _cols = new List<PolygonCollider2D>();
                foreach (RaycastHit2D item in hit)
                {
                    bool _isObsticle = item.collider.gameObject.TryGetComponent<Navigation2D>(out Navigation2D _nav);
                    print(_nav);
                    bool _canAdd = (_isObsticle) && (_nav.Mode == ModeSelect.Obstacle);
                    if (_canAdd) _cols.Add(_nav.Obstacle.collider);
                }
                if (_cols != null)
                {
                    isEvade = true;
                    print(_cols[0]);
                }
                else
                {
                    isEvade = false;
                    return _cols[0];
                }
                return new PolygonCollider2D();
                // PolygonCollider2D _return;
                // bool _isGet = hit.collider.gameObject.TryGetComponent<Navigation2D>(out Navigation2D _nav);
                // print(hit.collider);
                // bool _isObsticleMode = false;

                // if (_isGet) _isObsticleMode = _nav.Mode == ModeSelect.Obstacle;

                // if (_isObsticleMode) // 障害物を見つけた時の処理
                // {
                //     isEvade = true;
                //     _return = _nav.Obstacle.collider;
                //     return _return;
                // }
                // else // 障害物を発見できなっかた時の処理
                // {
                //     isEvade = false;
                //     posEvadeTo = Vector2.zero;
                //     return null;
                // }
            }

            _ForDebug();
            Navigator.HitCollider = _GetObstacleCollition();
        }

        private void GetEvadePoint()
        {
            if (!isEvade) return;

            Vector2[] _posEvadePoint = Navigator.HitCollider.points;
            List<Vector2> _posEvadeTo = new List<Vector2>();

            // 回避できるポイントを探す
            void _FindEvadePoints()
            {
                for (int i = 0; i < _posEvadePoint.Length; i++)
                {
                    // プレイヤーにrayを飛ばす
                    Ray2D _rayToPlayer = new Ray2D(_posEvadePoint[i], posSelf - _posEvadePoint[i]);
                    RaycastHit2D _hitP = Physics2D.Raycast(_rayToPlayer.origin, _rayToPlayer.direction);
                    bool _isPlayer = _hitP.collider.tag == "Player";

                    // ターゲットにrayを飛ばす
                    Ray2D _rayToTarget = new Ray2D(_posEvadePoint[i], Navigator.DestinationPosition - _posEvadePoint[i]);
                    RaycastHit2D _hitT = Physics2D.Raycast(_rayToTarget.origin, _rayToTarget.direction);
                    bool _isTarget = _hitT.collider.tag == "TagDestination";

                    if (_isPlayer && _isTarget) _posEvadeTo.Add(_posEvadePoint[i]);
                }
            }
            _FindEvadePoints();

            bool _notNull = _posEvadeTo != null;

            if (_notNull) posEvadeTo = _posEvadeTo[0];
            else posEvadeTo = Vector2.zero;
        }

        private void Navigate()
        {
            Vector2 _moveTo;
            bool _isArrive = Navigator.Distance <= 0;
            void _SetDestination()
            {
                if (isEvade) _moveTo = posEvadeTo;
                else _moveTo = posDestination;
            }

            _SetDestination();
            if (!_isArrive)
            {
                Navigator.NavigateObject.transform.position =
                    Vector2.MoveTowards(
                        posSelf,
                        _moveTo,
                        Navigator.MoveSpeed * Time.deltaTime
                        );
            }
        }
        #endregion



        #region Obstacle Methots

        #endregion
    }
}
using UnityEngine;



namespace Expansion.Components
{
[DisallowMultipleComponent]
public class Move : MonoBehaviour
{
    private enum Type
    {
        None,
        Horizontal,
        Vertical,
        Both,
    }
    [SerializeField]
    private Type type;


    public class HorizontalOptions
    {

    }
    public HorizontalOptions horizontalOptions;


    public class VerticalOptions
    {

    }
    public VerticalOptions verticalOptions;


    public struct BothOptions
    {
        public float maxSpeed;

        public AnimationCurve acceleration;

        public Vector2 moveTo;
    }
    public BothOptions bothOptions;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
}
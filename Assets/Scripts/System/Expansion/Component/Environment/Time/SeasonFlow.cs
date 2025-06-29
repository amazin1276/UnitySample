using Expansion;
using UnityEngine;
using static Expansion.Functions;



namespace Expansion.Components
{
    public class SeasonFlow : MonoBehaviour
    {
        public Range.Int day;

        [System.Serializable]
        public enum Seasons
        {
            Spring,
            Summer,
            Fall,
            Winter
        }
        [SerializeField]
        public Seasons season;

        void Start()
        {
            day = new Range.Int(30, 1, 1);
            season = Seasons.Winter;
        }


        public void AdvanceDay()
        {
            day.Value += 1;
            if (day.IsReachMax())
                AdvanceSeason();
        }


        public void AdvanceSeason()
        {
            bool isSpring = ((season & Seasons.Spring) == Seasons.Spring);
            if (isSpring)
                season = Seasons.Summer;

            bool isSummer = ((season & Seasons.Summer) == Seasons.Summer);
            if (isSummer)
                season = Seasons.Fall;

            bool isFall = ((season & Seasons.Fall) == Seasons.Fall);
            if (isFall)
                season = Seasons.Winter;

            bool isWinter = ((season & Seasons.Winter) == Seasons.Winter);
            if (isWinter)
                season = Seasons.Spring;

            day.Value = 0;
        }
    }
}
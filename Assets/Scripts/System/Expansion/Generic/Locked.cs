using System;


namespace Expansion
{


    [System.Serializable]
    public class Locked<TItem>
    {
        public Locked(string _key, TItem _item)
        {
            key = _key;
            item = _item;
        }

        public readonly string key;
        private TItem item;


        public TItem Unlock(string _key)
        {
            bool isAppropriateKey = key == _key;
            if (isAppropriateKey)
                return item;
            else
            {
                string message = $"アンロックされた{item}に不正にアクセスしました。\nkey:{_key}";
                throw new ArgumentException(message);
            }
        }
    }
}
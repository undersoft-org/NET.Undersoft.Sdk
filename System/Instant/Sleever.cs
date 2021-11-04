namespace System.Instant
{
    using Series;
    using Uniques;

    public static class Sleever
    {
        #region Fields

        public static IDeck<Sleeve> Cache = new Catalog<Sleeve>();

        #endregion

        #region Methods

        public static Sleeve Compile<T>()
        {
            var key = typeof(T).UniqueKey32();
            if (!Cache.TryGet(key, out Sleeve sleeve))
            {
                sleeve = new Sleeve<T>();
                sleeve.Combine();
                Cache.Add(key, sleeve);
                return sleeve;
            }
            return sleeve;
        }

        public static Sleeve Compile(Type type)
        {
            var key = type.UniqueKey32();
            if(!Cache.TryGet(key, out Sleeve sleeve))
            {
                sleeve = new Sleeve(type);
                sleeve.Combine();
                Cache.Add(key, sleeve);
                return sleeve;
            }
            return sleeve;
        }

        public static Sleeve Get<T>()
        {
            return Compile<T>();
        }

        public static Sleeve Get(Type type)
        {            
            return Compile(type);
        }

        public static ISleeve ToSleeve(this object item)
        {
            return Combine(item);
        }

        public static ISleeve ToSleeve<T>(this T item)
        {
            Type t = typeof(T);            
            if(t.IsInterface)
                return Combine((object)item);

            return Combine(item);
        }

        public static ISleeve ToSleeve(this Type type)
        {
            return Combine(type.New());
        }
    
        public static ISleeve Combine(this object item)
        {
            var type = item.GetType();
            var key = type.UniqueKey32();
            if(!Cache.TryGet(key, out Sleeve sleeve))
            {
                sleeve = new Sleeve(type);
                Cache.Add(key, sleeve);
                return sleeve.Combine(item);
            }
            return sleeve.Combine(item);
        }

        public static ISleeve Combine<T>(this T item)
        {
            var key = typeof(T).UniqueKey32();
            if(!Cache.TryGet(key, out Sleeve sleeve))
            {
                sleeve = new Sleeve<T>();
                Cache.Add(key, sleeve);
                return sleeve.Combine(item);
            }
            return sleeve.Combine(item);
        }

        #endregion
    }
}

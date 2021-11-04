namespace System.Instant
{
    public static class Evaluator
    {
        #region Methods

        public static R ValueOf<T, R>(this T item, string name) where T : class
        {
            return (R)Sleever.Combine(item)[name];
        }
        public static R ValueOf<R>(this object item, string name)
        {
            return (R)Sleever.Combine(item)[name];
        }
        public static object ValueOf(this object item, string name)
        {
            return Sleever.Combine(item)[name];
        }

        public static R ValueOf<T, R>(this T item, int index) where T : class
        {
            return (R)Sleever.Combine(item)[index];
        }
        public static R ValueOf<R>(this object item, int index)
        {
            return (R)Sleever.Combine(item)[index];
        }
        public static object ValueOf(this object item, int index)
        {
            return Sleever.Combine(item)[index];
        }

        public static R ValueOf<T, R>(this T item, string name, R value) where T : class
        {
            return (R)(Sleever.Combine(item)[name] = value);
        }
        public static R ValueOf<R>(this object item, string name, R value)
        {
            return (R)(Sleever.Combine(item)[name] = value);
        }
        public static object ValueOf(this object item, string name, object value)
        {
            return (Sleever.Combine(item)[name] = value);
        }

        public static R ValueOf<T, R>(this T item, int index, R value) where T : class
        {
            return (R)(Sleever.Combine(item)[index] = value);
        }
        public static R ValueOf<R>(this object item, int index, R value)
        {
            return (R)(Sleever.Combine(item)[index] = value);
        }
        public static object ValueOf(this object item, int index, object value)
        {
            return (Sleever.Combine(item)[index] = value);
        }

        #endregion
    }

    public static class Variator
    {
        #region Methods

        public static T PatchTo<T>(this T item, T target) where T : class
        {
            return new Variety<T>(item).Patch(target);
        }
        public static E PatchTo<T, E>(this T item, E target) where T : class where E : class
        {
            return new Variety<T>(item).Patch(target);
        }

        public static T PutTo<T>(this T item, T target) where T : class
        {
            return new Variety<T>(item).Put(target);
        }
        public static E PutTo<T, E>(this T item, E target) where T : class where E : class
        {
            return new Variety<T>(item).Put(target);
        }
      
        #endregion
    }
}

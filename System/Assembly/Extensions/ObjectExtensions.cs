using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace BenefitSystems.Hornet
{
    public static class ObjectExtensions
    {
        #region Methods

        /// <summary>
        /// The Default.
        /// </summary>
        /// <param name="type">The type<see cref="Type"/>.</param>
        /// <returns>The <see cref="object"/>.</returns>
        public static Vector ToVector(this object obj)
        {
            return new Vector(obj);
        }

        public static Vector<T> ToVector<T>(this T obj)
        {
            return new Vector<T>(obj);
        }

        #endregion
    }

    public class Vector : KeyedCollection<string, ValueTuple<FieldInfo, object>>
    {
        private object obj;
        private FieldInfo[] fields;

        public Vector(object o)
        {
            obj = o;
            fields = o.GetType().GetRuntimeFields().ToArray();
            
            foreach (var field in fields)
            {
                Add(new ValueTuple<FieldInfo, object>(field, null));
            }
        }

        public new object this[string fieldName]
        {
            get { return base[fieldName].Item1.GetValueDirect(TypedReference.MakeTypedReference(obj, fields)); }
            set
            {
                var t = TypedReference.MakeTypedReference(obj, fields);
                ValueTuple<FieldInfo, object> preset = base[fieldName];
                object o = value;
                preset.Item2 = o;
                preset.Item1.SetValueDirect(t, o);
            }
        }

        public object[] this[string[] names]
        {
            get
            {
                var t = TypedReference.MakeTypedReference(obj, fields);
                object[] o = new object[names.Length];
                for (int i = 0; i < fields.Length; i++)
                {
                    o[i] = base[names[i]].Item1.GetValueDirect(t);
                }
                return o;
            }
            set
            {
                var t = TypedReference.MakeTypedReference(obj, fields);
                for (int i = 0; i < names.Length; i++)
                {
                    ValueTuple<FieldInfo, object> preset = base[names[i]];
                    object o = value[i];
                    preset.Item2 = o;
                    preset.Item1.SetValueDirect(t, o);
                }
            }
        }

        public object this[FieldInfo field]
        {
            get { return base[field.Name].Item1.GetValueDirect(TypedReference.MakeTypedReference(obj, fields)); }
            set
            {

                ValueTuple<FieldInfo, object> preset = base[field.Name];
                object o = value;
                preset.Item2 = o;
                preset.Item1.SetValueDirect(TypedReference.MakeTypedReference(obj, fields), o);
            }
        }

        public object[] this[FieldInfo[] fields]
        {
            get
            {
                var t = TypedReference.MakeTypedReference(obj, fields);
                object[] o = new object[fields.Length];
                for (int i = 0; i < fields.Length; i++)
                {
                    o[i] = base[fields[i].Name].Item1.GetValueDirect(t);
                }
                return o;
            }
            set
            {
                var t = TypedReference.MakeTypedReference(obj, fields);
                for (int i = 0; i < fields.Length; i++)
                {
                    ValueTuple<FieldInfo, object> preset = base[fields[i].Name];
                    object o = value[i];
                    preset.Item2 = o;
                    preset.Item1.SetValueDirect(t, o);
                }
            }
        }

        public object Get(string name)
        {
            return this[name];
        }

        public void Set(string name, object value)
        {
            if (Contains(name))
                this[name] = value;
        }

        public object Get(FieldInfo name)
        {
            return this[name];
        }

        public void Set(FieldInfo name, object value)
        {
            if (Contains(name.Name))
                this[name] = value;
        }

        public object[] Get(FieldInfo[] name)
        {
            return this[name];
        }

        public void Set(object value)
        {
            var o = value.ToVector();
            foreach (var name in o)
            {
                string n = name.Item1.Name;
                if (Contains(n))
                    this[n] = o[n];
            }
        }

        public void Set<S>(S value)
        {
            var o = value.ToVector();
            foreach (var name in o)
            {
                string n = name.Item1.Name;
                if (Contains(n))
                    this[n] = o[n];
            }
        }

        public void Set(FieldInfo[] names, object value)
        {
            var o = value.ToVector();
            foreach (var name in names)
            {
                if (Contains(name.Name))
                    this[name] = o[name];
            }

        }

        public object GetObject()
        {
            return obj;
        }

        public T GetObject<T>()
        {
            return (T)obj;
        }

        public void ClearPresets()
        {
            for (int i = 0; i < Count; i++)
            {
                if (this[i].Item2 != null)
                {
                    var o = this[i];
                    o.Item2 = null;
                }
            }
        }

        protected override string GetKeyForItem(ValueTuple<FieldInfo, object> item)
        {
            return item.Item1.Name;
        }

    }

    public class Vector<T> : KeyedCollection<string, ValueTuple<FieldInfo, object>>
    {
        private T obj;
        private FieldInfo[] fields;

        public Vector(T o)
        {
            obj = o;
            fields = typeof(T).GetRuntimeFields().ToArray();

            foreach (var field in fields)
            {
                Add(new ValueTuple<FieldInfo, object>(field, null));
            }
        }

        public new object this[string fieldName]
        {
            get { return base[fieldName].Item1.GetValueDirect(TypedReference.MakeTypedReference(obj, fields)); }
            set 
            {
                var t = TypedReference.MakeTypedReference(obj, fields);
                ValueTuple<FieldInfo, object> preset = base[fieldName];
                object o = value;
                preset.Item2 = o;
                preset.Item1.SetValueDirect(t, o);
            }
        }

        public object[] this[string[] names]
        {
            get
            {
                var t = TypedReference.MakeTypedReference(obj, fields);
                object[] o = new object[names.Length];
                for (int i = 0; i < fields.Length; i++)
                {
                    o[i] = base[names[i]].Item1.GetValueDirect(t);
                }
                return o;
            }
            set
            {
                var t = TypedReference.MakeTypedReference(obj, fields);
                for (int i = 0; i < names.Length; i++)
                {
                    ValueTuple<FieldInfo, object> preset = base[names[i]];
                    object o = value[i];
                    preset.Item2 = o;
                    preset.Item1.SetValueDirect(t, o);
                }
            }
        }

        public object this[FieldInfo field]
        {
            get { return base[field.Name].Item1.GetValueDirect(TypedReference.MakeTypedReference(obj, fields)); }
            set 
            {

                ValueTuple<FieldInfo, object> preset = base[field.Name];
                object o = value;
                preset.Item2 = o;
                preset.Item1.SetValueDirect(TypedReference.MakeTypedReference(obj, fields), o);
            }
        }

        public object[] this[FieldInfo[] fields]
        {
            get
            {
                var t = TypedReference.MakeTypedReference(obj, fields);
                object[] o = new object[fields.Length];
                for (int i = 0; i < fields.Length; i++)
                {
                    o[i] = base[fields[i].Name].Item1.GetValueDirect(t);
                }
                return o;
            }
            set
            {
                var t = TypedReference.MakeTypedReference(obj, fields);
                for (int i = 0; i < fields.Length; i++)
                {
                    ValueTuple<FieldInfo, object> preset = base[fields[i].Name];
                    object o = value[i];
                    preset.Item2 = o;
                    preset.Item1.SetValueDirect(t, o);
                }
            }
        }

        public object Get(string name)
        {
            return this[name];
        }

        public void Set(string name, T value)
        {
            if (Contains(name))
                this[name] = value;
        }

        public object Get(FieldInfo name)
        {
            return this[name];
        }

        public void Set(FieldInfo name, object value)
        {
            if (Contains(name.Name))
                this[name] = value;
        }

        public object[] Get(FieldInfo[] name)
        {
            return this[name];
        }

        public void Set(T value)
        {
            var o = value.ToVector();
            foreach (var name in o)
            {
                string n = name.Item1.Name;
                if (Contains(n))
                    this[n] = o[n];
            }          
        }

        public void Set<S>(S value)
        {
            var o = value.ToVector();
            foreach (var name in o)
            {
                string n = name.Item1.Name;
                if (Contains(n))
                    this[name.Item1] = o[name.Item1];
            }
        }

        public void Set(FieldInfo[] names, T value)
        {
            var o = value.ToVector();
            foreach (var name in names)
            {
                if (Contains(name.Name))
                    this[name] = o[name];
            }

        }

        public new void Clear()
        {
            for (int i = 0; i < Count; i++)
            {
                if (this[i].Item2 != null)
                {
                    var o = this[i];
                    o.Item2 = null;
                }
            }
        }

        public void Map<S>(S value)
        {
            var o = value.ToVector();
            var l = new List<string>();
            foreach (var t in this)
            {
                if (t.Item2 != null)
                {
                    string n = t.Item1.Name;
                    if (o.Contains(n))
                        o[n] = this[n];
                }
            }
        }

        public T GetObject()
        {
            return obj;
        }

        protected override string GetKeyForItem(ValueTuple<FieldInfo, object> item)
        {
            return item.Item1.Name;
        }
    }

}

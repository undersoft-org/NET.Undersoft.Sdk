namespace System.Instant
{
    using Collections.Generic;
    using Linq;
    using Uniques;

    #region Interfaces

    public interface IVariety : ISleeve
    {
        #region Properties

        ISleeve ClientSleeve { get; }

        ISleeve HostSleeve { get; }

        IEnumerable<Vary> Unchanged { get; }

        IEnumerable<Vary> Variations { get; }

        object Client { get; }

        object Host { get; }

        object Devisor { get; }

        #endregion

        #region Methods

        object Clone();

        void MapClient();

        void MapDevisor();

        void MapHost();

        E Patch<E>() where E : class;

        E Patch<E>(E item) where E : class;

        E Put<E>() where E : class;

        E Put<E>(E item) where E : class;

        #endregion
    }

    public interface IVariety<T> : IVariety
    {
        #region Properties

        new T Client { get; }

        new T Host { get; }

        new T Devisor { get; }

        #endregion

        #region Methods

        T Clone();

        T Patch();

        T Patch(T item);

        T Put();

        T Put(T item);

        #endregion
    }

    #endregion

    public struct Vary
    {
        #region Fields

        public int      HostIndex;
        public string   RubricName;
        public object   ClientValue;
        public IRubric  ClientRubric;

        #endregion
    }

    public class Variety<T> : Variety, IVariety<T> where T : class
    {
        public Variety() : base()
        {
            factory = Sleever.Get<T>();
            host = factory.Combine(typeof(T).New());
            client = factory.Combine(host);
        }
        public Variety(T item) : base()
        {
            factory = Sleever.Get<T>();
            host = factory.Combine(item);
            client = factory.Combine(host);
            MapDevisor();
        }

        public T Patch(T item)
        {
            var _host = host.Devisor;
            host.Devisor = item;
            Patch();
            host.Devisor = _host;
            return item;
        }
        public T Patch()
        {
            Variations.ForEach(v => host[v.HostIndex] = v.ClientValue).ToArray();
            return (T)(host.Devisor);
        }

        public T Put(T item)
        {
            var _host = host.Devisor;
            host.Devisor = item;
            Put();
            host.Devisor = _host;
            return item;
        }
        public T Put()
        {
            host.ValueArray = client.ValueArray;
            return (T)(host.Devisor);
        }

        public new T Clone()
        {
            client.Devisor = typeof(T).New();
            client.ValueArray = host.ValueArray;
            client.Devisor = host;
            return Host;
        }

        public new T Host => (T)(client.Devisor);

        public new T Client => (T)(client);

        public new T Devisor
        {
            get => (T)(host.Devisor);
            set => host.Devisor = value;
        }
    }

    public class Variety : IVariety
    {
        protected bool differentTypes = false;
        protected Sleeve factory;
        protected ISleeve host;
        protected ISleeve client;
        protected Type type => factory.BaseType;

        public ISleeve HostSleeve { get => host; set => host = value; }
        public ISleeve ClientSleeve { get => client; set => client = value; }
       
        public Variety()
        {         
        }
        public Variety(object item) : base()
        {
            factory = Sleever.Get(item.GetType());
            host = factory.Combine(item);
            client = factory.Combine(host);
            MapDevisor();
        }

        private void setDeltaMarks()
        {
            foreach (var rubric in Rubrics)
            {
                if (rubric.DeltaMark != null)
                    client[rubric.RubricId] = rubric.DeltaMark;
            }
        }

        public E Patch<E>(E item) where E : class
        {
            object localDevisor = Devisor;
            ISleeve target;
            if(item is ISleeve)
                Devisor = target = ((ISleeve)item);
            else
                Devisor = target = item.ToSleeve();

            if(typeof(E) != type)
                differentTypes = true;

            Variations.ForEach((vary) =>
            {
                var hi = vary.HostIndex;             
                var cv = vary.ClientValue;

                if(differentTypes)
                {
                    var ht = target.Rubrics[hi].RubricType;
                    var ct = vary.ClientRubric.RubricType;

                    if(ht == ct)
                        target[hi] = cv;
                }
                else
                    target[hi] = cv;
            });

            differentTypes = false;
            Devisor = localDevisor;
            return item;
        }
        public E Patch<E>() where E : class
        {
            return Patch(typeof(E).New<E>());
        }     

        public E Put<E>(E item) where E : class
        {
            object localDevisor = Devisor;
            ISleeve target;
            if(item is ISleeve)
                Devisor = target = ((ISleeve)item);
            else
                Devisor = target = item.ToSleeve();

            if(typeof(E) != type)
                differentTypes = true;

            Rubrics.ForEach((rubric) =>
            {
                if(!rubric.IsKey)
                {
                    if(target.Rubrics.TryGet(rubric.Name, out MemberRubric mr))
                    {
                        var cv = this[rubric.RubricId];
                        var hi = mr.RubricId;

                        if(differentTypes)
                        {
                            var ht = target.Rubrics[hi].RubricType;
                            var ct = rubric.RubricType;

                            if(ht == ct)
                                target[hi] = cv;
                        }
                        else
                            target[hi] = cv;
                    }
                }
            });

            differentTypes = false;
            Devisor = localDevisor;
            return item;
        }
        public E Put<E>() where E : class
        {
            return Put(typeof(E).New<E>());
        }

        public void MapClient()
        {
            var clientShell = factory.Combine(client);
            client.ValueArray = clientShell.ValueArray;
        }
        public void MapHost()
        {
            var clientShell = factory.Combine(client);
            clientShell.ValueArray = client.ValueArray;
        }
        public void MapDevisor()
        {
            client.ValueArray = host.ValueArray;
        }

        public void SafeMapClient()
        {
            var clientShell = factory.Combine(client);

            clientShell.Rubrics.Select(v =>
                client.Rubrics.ContainsKey(v.RubricName)
                    ? client[v.RubricName] = clientShell[v.RubricId]
                    : null).ToArray();
        }
        public void SafeMapHost()
        {
            var clientShell = factory.Combine(client);

            client.Rubrics.Select(v =>
                clientShell.Rubrics.ContainsKey(v.RubricName)
                    ? clientShell[v.RubricName] = client[v.RubricId]
                    : null).ToArray();

        }
        public void SafeMapDevisor()
        {
            Rubrics.Select(v =>
                client.Rubrics.ContainsKey(v.RubricName)
                    ? client[v.RubricName] = host[v.RubricId]
                    : null).ToArray();
        }      

        public object Clone()
        {
            client.Devisor = type.New();
            client.ValueArray = host.ValueArray;
            client.Devisor = host;
            return Host;
        }

        public IEnumerable<Vary> Variations => differentTypes ? customTypeVariations : sameTypeVariations;

        public IEnumerable<Vary> Unchanged => differentTypes ? customTypeUnchanged : sameTypeUnchanged;

        private IEnumerable<Vary> sameTypeVariations
        {
            get
            {
                foreach (var rubric in Rubrics.AsValues())
                {
                    if(!rubric.IsKey)
                    {
                        var index = rubric.RubricId;
                        var value = this[index];

                        if(!value.NullOrEquals(rubric.DeltaMark) &&
                            !value.Equals(host[index]))
                        {
                            yield return new Vary()
                            {
                               HostIndex = index,
                               RubricName = rubric.Name,
                               ClientValue = value,
                               ClientRubric = rubric
                            };
                        }
                    }
                }
            }
        }

        private IEnumerable<Vary> customTypeVariations
        {
            get
            {
                var devisor = (ISleeve)Devisor;
                foreach(var rubric in Rubrics.AsValues())
                {
                    if(!rubric.IsKey)
                    {
                        var name = rubric.Name;
                        if(devisor.Rubrics.TryGet(name, out MemberRubric mr))
                        {
                            var clientValue = this[rubric.RubricId];
                            var hostIndex  = mr.RubricId;

                            if(!clientValue.NullOrEquals(rubric.DeltaMark) &&
                                !clientValue.Equals(devisor[hostIndex]))
                            {
                                yield return new Vary()
                                {
                                    HostIndex = hostIndex,
                                    RubricName = name,
                                    ClientValue = clientValue,
                                    ClientRubric = rubric
                                };
                            }
                        }
                    }
                }
            }
        }

        private IEnumerable<Vary> sameTypeUnchanged
        {
            get
            {
                foreach (var rubric in Rubrics.AsValues())
                {
                    var index = rubric.RubricId;
                    var value = this[index];

                    if (value.NullOrEquals(rubric.DeltaMark) ||
                        value == host[index])
                    {
                        yield return new Vary()
                        {
                            HostIndex = index,
                            RubricName = rubric.Name,
                            ClientValue = value,
                            ClientRubric = rubric
                        };
                    }
                }
            }
        }

        private IEnumerable<Vary> customTypeUnchanged
        {
            get
            {
                var devisor = (ISleeve)Devisor;
                foreach(var rubric in Rubrics.AsValues())
                {
                    var name = rubric.Name;
                    if(devisor.Rubrics.TryGet(name, out MemberRubric mr))
                    {

                        var clientValue = this[rubric.RubricId];
                        var hostIndex  = mr.RubricId;

                        if(clientValue.NullOrEquals(rubric.DeltaMark) ||
                            clientValue.Equals(host[hostIndex]))
                        {
                            yield return new Vary()
                            {
                                HostIndex = hostIndex,
                                RubricName = name,
                                ClientValue = clientValue,
                                ClientRubric = rubric
                            };
                        }
                    }                    
                }
            }
        }

        public bool Equals(IUnique other)
        {
            return host.Equals(other);
        }
        public int CompareTo(IUnique? other)
        {
            return client.CompareTo(other);
        }
     
        public ulong UniqueKey
        {
            get => host.UniqueKey;
            set => host.UniqueKey = value;
        }
        public ulong UniqueSeed
        {
            get => host.UniqueSeed;
            set => host.UniqueSeed = value;
        }

        public byte[] GetBytes()
        {
            return host.GetBytes();
        }

        public byte[] GetUniqueBytes()
        {
            return host.GetUniqueBytes();
        }

        public object this[string propertyName]
        {
            get => client[propertyName];
            set => client[propertyName] = value;
        }

        public object this[int fieldId]
        {
            get => client[fieldId];
            set => client[fieldId] = value;
        }

        public object[] ValueArray
        {
            get => client.ValueArray;
            set => client.ValueArray = value;
        }

        public Ussn SerialCode
        {
            get => host.SerialCode;
            set => host.SerialCode = value;
        }

        public IRubrics Rubrics
        {
            get => host.Rubrics;
            set => host.Rubrics = value;
        }
        
        public object Devisor
        {
            get => host.Devisor;
            set => host.Devisor = value;
        }

        public object Host => client.Devisor;

        public object Client => client;
    }
}

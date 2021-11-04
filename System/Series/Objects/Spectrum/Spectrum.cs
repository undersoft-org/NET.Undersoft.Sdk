/*************************************************
   Copyright (c) 2021 Undersoft

   System.Series.Spectrum.cs
   
   Data structure based on van Emde Boas tree algorithm
   with constant maximum count of items in universum defined on the beginning. 
   Innovation is that all scopes have one global cluster registry (hash deck).
   Summary scopes(sigma scopes) are also in one global hash deck. 
   Another innovation is that tree branch ends with 4 leafs (values) instead of 2
   which are encoded in to global cluster registry. Achieved complexity of
   collection is Olog^log^(n/2). For dynamic resizing collection
   inside universum are used IDeck Collections assigned by interface 
   When Safe Thread parameter is set to true Board32 is assigned
   otherwise Deck32.        

   @project: Vegas.Sdk
   @stage: Development
   @author: PhD Radoslaw Rudek, Dariusz Hanc
   @date: (30.05.2021) 
   @licence MIT
 *************************************************/

namespace System.Series
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Series.Spectrum;

    public class Spectrum<V> : ISpectrum<V>
    {
        #region Fields

        private IList<vEBTreeLevel> levels;
        private IDeck<V> registry;
        private SpectrumBase root;
        private IDeck<SpectrumBase> scopes;
        private IDeck<SpectrumBase> sigmaScopes;
        private int size;

        #endregion

        #region Constructors

        public Spectrum() : this(int.MaxValue, false)
        {
        }
        public Spectrum(int size, bool safeThread)
        {
            Initialize(size);
        }

        #endregion

        #region Properties

        public int Count => registry.Count;

        public int IndexMax
        {
            get { return root.IndexMax; }
        }

        public int IndexMin
        {
            get { return root.IndexMin; }
        }

        public int Size { get; }

        #endregion

        #region Methods

        public bool Add(int key, V obj)
        {
            if(registry.Add(key, obj))
            {
                root.Add(0, 1, 0, key);
                return true;
            }
            return false;
        }

        public bool Contains(int key)
        {
            return registry.ContainsKey(key);
        }

        public V Get(int key)
        {
            return registry.Get(key);
        }

        public IEnumerator<CardBase<V>> GetEnumerator()
        {
            return new SpectrumSeries<V>(this);
        }

        public void Initialize(int range = 0, bool safeThred = false)
        {
            scopes = new Deck64<SpectrumBase>();
            sigmaScopes = new Deck64<SpectrumBase>();

            if((range == 0) || (range > int.MaxValue))
            {
                range = int.MaxValue;
            }
            if(!safeThred)
                registry = new Deck64<V>(range);
            else
                registry = new Board64<V>(range);

            size = range;

            CreateLevels(range);   //create levels

            root = new ScopeValue(range, scopes, sigmaScopes, levels, 0, 0, 0);
        }

        public int Next(int key)
        {
            return root.Next(0, 1, 0, key);
        }

        public int Previous(int key)
        {
            return root.Previous(0, 1, 0, key);
        }

        public bool Remove(int key)
        {
            if(registry.TryRemove(key))
            {
                root.Remove(0, 1, 0, key);
                return true;
            }
            return false;
        }

        public bool Set(int key, V value)
        {
            return Add(key, value);
        }

        public bool TestAdd(int key)
        {
            root.Add(0, 1, 0, key);
            return true;
        }

        public bool TestContains(int key)
        {
            return root.Contains(0, 1, 0, key);
        }

        public bool TestRemove(int key)
        {
            root.Remove(0, 1, 0, key);
            return true;
        }

        private void BuildSigmaScopes(int range, byte level, byte nodeTypeIndex, int nodeCounter, int clusterSize)
        {
            int parentSqrt = ScopeValue.ParentSqrt(range);

            if(levels == null)
            {
                levels = new List<vEBTreeLevel>();
            }
            if(levels.Count <= level)
            {
                levels.Add(new vEBTreeLevel());
            }
            if(levels[level].Nodes == null)
            {
                levels[level].Nodes = new List<vEBTreeNode>();
                levels[level].Nodes.Add(new vEBTreeNode());
            }
            else
            {
                levels[level].Nodes.Add(new vEBTreeNode());
            }

            levels[level].Nodes[nodeTypeIndex].NodeCounter = nodeCounter;
            levels[level].Nodes[nodeTypeIndex].NodeSize = parentSqrt;

            if(parentSqrt > 4)
            {
                // sigmaNode
                BuildSigmaScopes(parentSqrt, (byte)(level + 1), (byte)(2 * nodeTypeIndex), nodeCounter, parentSqrt);
                // cluster
                BuildSigmaScopes(parentSqrt, (byte)(level + 1), (byte)(2 * nodeTypeIndex + 1), nodeCounter * parentSqrt, parentSqrt);
            }
        }

        private void CreateLevels(int range)
        {
            if(levels == null)
            {
                int parentSqrt = ScopeValue.ParentSqrt(size);
                BuildSigmaScopes(range, 0, 0, 1, parentSqrt);
            }

            int baseOffset = 0;
            for(int i = 1; i < levels.Count; i++)
            {
                levels[i].BaseOffset = baseOffset;
                for(int j = 0; j < levels[i].Nodes.Count - 1; j++)
                {
                    levels[i].Nodes[j].IndexOffset = baseOffset;
                    baseOffset += levels[i].Nodes[j].NodeCounter * levels[i].Nodes[j].NodeSize;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new SpectrumSeries<V>(this);
        }

        #endregion
    }
}

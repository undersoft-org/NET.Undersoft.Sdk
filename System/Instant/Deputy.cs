/********************************************************************
   Copyright (c) 2021 Benefit Systems

   @based on: github.com/undersoft-org/NET.Undersoft.Vegas.Sdk.Devel
   @author: Dariusz Hanc
 ********************************************************************/

using System.Collections.ObjectModel;
using System.Linq;
using System.Uniques;
using System.Reflection;
using System.Reflection.Emit;
using System.Series;
using System.Threading.Tasks;

namespace System
{
    #region Delegates

    /// <summary>
    /// The InstantDelegate.
    /// </summary>
    /// <param name="target">The target<see cref="object"/>.</param>
    /// <param name="parameters">The parameters<see cref="object[]"/>.</param>
    /// <returns>The <see cref="object"/>.</returns>
    public delegate object DeputyRoutine(object target, params object[] parameters);

    #endregion

    #region Enums

    /// <summary>
    /// Defines the ChangesType.
    /// </summary>
    public enum ChangesType
    {
        /// <summary>
        /// Defines the Added.
        /// </summary>
        Added,

        /// <summary>
        /// Defines the Removed.
        /// </summary>
        Removed,

        /// <summary>
        /// Defines the Replaced.
        /// </summary>
        Replaced,

        /// <summary>
        /// Defines the Cleared.
        /// </summary>
        Cleared
    };

    #endregion

    public class Deputy<T> : Deputy
    {
        public Deputy() : base(typeof(T))
        {
        }
        public Deputy(params object[] constructorParams) : base(typeof(T), constructorParams)
        {
        }
        public Deputy(Type[] parameterTypes, params object[] constructorParams) : base(typeof(T), parameterTypes, constructorParams)
        {
        }
        public Deputy(string methodName) : base(typeof(T), methodName)
        {
        }
        public Deputy(string methodName, Type[] parameterTypes) : base(typeof(T), methodName, parameterTypes)
        {
        }
        public Deputy(string methodName, Type[] parameterTypes, params object[] constructorParams) : base(typeof(T), methodName, parameterTypes, constructorParams)
        {
        }
        public Deputy(string methodName, params object[] constructorParams) : base(typeof(T), methodName, constructorParams)
        {
        }
    }

    public class Deputy : IDeputy
    {
        private Ussn serialcode;
        private event DeputyRoutine routine;

        public Deputy(Type targetType) : this(targetType.GetMethods().FirstOrDefault()) { }
        public Deputy(Type targetType, Type[] parameterTypes) 
            : this(targetType.GetMethods().Where(m => m.GetParameters().All(p => parameterTypes.Contains(p.ParameterType))).FirstOrDefault()) { }
        public Deputy(Type targetType, Type[] parameterTypes, params object[] constructorParameters) 
            : this(targetType.GetMethods()
                .FirstOrDefault(m => m.GetParameters().
                    All(p => parameterTypes
                        .Contains(p.ParameterType))),
                constructorParameters) { }
        public Deputy(Type targetType, params object[] constructorParameters) 
            : this(targetType.GetMethods().FirstOrDefault(), constructorParameters) { }
        public Deputy(Delegate targetMethod)
        {
            TargetObject = targetMethod.Target;
            Type t = TargetObject.GetType();
            MethodInfo m = targetMethod.Method;

            Method = invoking(m);
            NumberOfArguments = m.GetParameters().Length;
            Info = m;
            Parameters = m.GetParameters();
            createUniqueKey();
        }
        public Deputy(object targetObject, string methodName) 
            : this(targetObject, methodName, null) {}
        public Deputy(object targetObject, string methodName, Type[] parameterTypes)
        {
            this.TargetObject = targetObject;
            Type t = TargetObject.GetType();

            MethodInfo m = parameterTypes != null ? t.GetMethod(methodName, parameterTypes) : t.GetMethod(methodName);

            Method = invoking(m);

            NumberOfArguments = m.GetParameters().Length;
            Info = m;
            Parameters = m.GetParameters();
            createUniqueKey();
        }
        public Deputy(Type targetType, string methodName) 
            : this(Summon.New(targetType), methodName, null) { }
        public Deputy(Type targetType, string methodName, Type[] parameterTypes) 
            : this(Summon.New(targetType), methodName, parameterTypes) { }
        public Deputy(Type targetType, string methodName, params object[] constructorParams) 
            : this(Summon.New(targetType, constructorParams), methodName) { }
        public Deputy(Type targetType, string methodName, Type[] parameterTypes, params object[] constructorParams) 
            : this(Summon.New(targetType, constructorParams), methodName, parameterTypes) { }
        public Deputy(string targetName, string methodName) 
            : this(Summon.New(targetName), methodName, null) { }
        public Deputy(string targetName, string methodName, Type[] parameterTypes) 
            : this(Summon.New(targetName), methodName, parameterTypes) { }
        public Deputy(string targetName, string methodName, Type[] parameterTypes, params object[] constructorParams) 
            : this(Summon.New(targetName, constructorParams), methodName, parameterTypes) { }
        public Deputy(object targetObject, MethodInfo methodInvokeInfo)
           : this(targetObject,
               methodInvokeInfo.Name,
               methodInvokeInfo.GetParameters()
                   .Select(p => p.ParameterType).ToArray())
        {
        }      
        public Deputy(MethodInfo methodInvokeInfo) 
            : this(methodInvokeInfo.DeclaringType.New(),
                methodInvokeInfo.Name,
                methodInvokeInfo.GetParameters()
                    .Select(p => p.ParameterType).ToArray()) {
        }
        public Deputy(MethodInfo methodInvokeInfo, params object[] constructorParams) 
            : this(methodInvokeInfo.DeclaringType.New(constructorParams), 
                methodInvokeInfo.Name, 
            methodInvokeInfo.GetParameters()
                .Select(p => p.ParameterType).ToArray()) {
        }

        public string Name { get; set;  }

        public string QualifiedName { get; set;  }

        public object this[int fieldId]
        {
            get => ParameterValues[fieldId];
            set => ParameterValues[fieldId] = value;
        }
        public object this[string propertyName]
        {
            get
            {
                for (int i = 0; i < Parameters.Length; i++)
                    if (Parameters[i].Name == propertyName)
                        return ParameterValues[i];
                return null;
            }
            set
            {
                for (int i = 0; i < Parameters.Length; i++)
                    if (Parameters[i].Name == propertyName)
                        ParameterValues[i] = value;
            }
        }

        public DeputyRoutine Routine
        {
            get
            {
                if (routine == null)
                    routine += (DeputyRoutine) Method;
                return routine;
            }
        }

        public Object TargetObject { get; set; }

        public Delegate Method { get; }

        public MethodInfo Info { get; set; }

        public ParameterInfo[] Parameters { get; set; }

        public object[] ParameterValues { get; set; }

        public int NumberOfArguments { get; set; }

        public IUnique Empty => Ussn.Empty;

        public ulong UniqueKey
        {
            get => serialcode.UniqueKey;
            set => serialcode.UniqueKey = value;
        }

        public ulong UniqueSeed
        {
            get => serialcode.UniqueSeed;
            set => serialcode.UniqueSeed = value;
        }

        public object[] ValueArray { get => ParameterValues; set => ParameterValues = value; }

        public Ussn SerialCode { get => serialcode; set => serialcode = value; }

        private Delegate    invoking(MethodInfo methodInfo)
        {
            DynamicMethod dynamicMethod = new DynamicMethod(string.Empty,
                          typeof(object), new Type[] { typeof(object),
                          typeof(object[]) },
                          methodInfo.DeclaringType.Module);
            
            ILGenerator il = dynamicMethod.GetILGenerator();
            ParameterInfo[] ps = methodInfo.GetParameters();
            
            Type[] paramTypes = new Type[ps.Length];
            for (int i = 0; i < paramTypes.Length; i++)
            {
                if (ps[i].ParameterType.IsByRef)
                    paramTypes[i] = ps[i].ParameterType.GetElementType();
                else
                    paramTypes[i] = ps[i].ParameterType;
            }
            LocalBuilder[] locals = new LocalBuilder[paramTypes.Length];

            for (int i = 0; i < paramTypes.Length; i++)
            {
                locals[i] = il.DeclareLocal(paramTypes[i], true);
            }

            for (int i = 0; i < paramTypes.Length; i++)
            {
                il.Emit(OpCodes.Ldarg_1);
                directint(il, i);
                il.Emit(OpCodes.Ldelem_Ref);
                casting(il, paramTypes[i]);
                il.Emit(OpCodes.Stloc, locals[i]);
            }

            if (!methodInfo.IsStatic)
            {
                il.Emit(OpCodes.Ldarg_0);
            }

            for (int i = 0; i < paramTypes.Length; i++)
            {
                if (ps[i].ParameterType.IsByRef)
                    il.Emit(OpCodes.Ldloca_S, locals[i]);
                else
                    il.Emit(OpCodes.Ldloc, locals[i]);
            }

            if (methodInfo.IsStatic)
                il.EmitCall(OpCodes.Call, methodInfo, null);
            else
                il.EmitCall(OpCodes.Callvirt, methodInfo, null);
            
            if (methodInfo.ReturnType == typeof(void))
                il.Emit(OpCodes.Ldnull);
            else
                boxing(il, methodInfo.ReturnType);

            for (int i = 0; i < paramTypes.Length; i++)
            {
                if (ps[i].ParameterType.IsByRef)
                {
                    il.Emit(OpCodes.Ldarg_1);
                    directint(il, i);
                    il.Emit(OpCodes.Ldloc, locals[i]);
                    if (locals[i].LocalType.IsValueType)
                        il.Emit(OpCodes.Box, locals[i].LocalType);
                    il.Emit(OpCodes.Stelem_Ref);
                }
            }

            il.Emit(OpCodes.Ret);

            Delegate invoker = (DeputyRoutine)dynamicMethod.CreateDelegate(typeof(DeputyRoutine));

            return invoker;
        }

        private static void casting(ILGenerator il, Type type)
        {
            if (type.IsValueType)
            {
                il.Emit(OpCodes.Unbox_Any, type);
            }
            else
            {
                il.Emit(OpCodes.Castclass, type);
            }
        }

        private static void boxing(ILGenerator il, Type type)
        {
            if (type.IsValueType)
            {
                il.Emit(OpCodes.Box, type);
            }
        }

        private static void directint(ILGenerator il, int value)
        {
            switch (value)
            {
                case -1:
                    il.Emit(OpCodes.Ldc_I4_M1);
                    return;
                case 0:
                    il.Emit(OpCodes.Ldc_I4_0);
                    return;
                case 1:
                    il.Emit(OpCodes.Ldc_I4_1);
                    return;
                case 2:
                    il.Emit(OpCodes.Ldc_I4_2);
                    return;
                case 3:
                    il.Emit(OpCodes.Ldc_I4_3);
                    return;
                case 4:
                    il.Emit(OpCodes.Ldc_I4_4);
                    return;
                case 5:
                    il.Emit(OpCodes.Ldc_I4_5);
                    return;
                case 6:
                    il.Emit(OpCodes.Ldc_I4_6);
                    return;
                case 7:
                    il.Emit(OpCodes.Ldc_I4_7);
                    return;
                case 8:
                    il.Emit(OpCodes.Ldc_I4_8);
                    return;
            }

            if (value > -129 && value < 128)
            {
                il.Emit(OpCodes.Ldc_I4_S, (SByte)value);
            }
            else
            {
                il.Emit(OpCodes.Ldc_I4, value);
            }
        }

        public object Execute(params object[] parameters)
        {
            try
            {
                return Method.DynamicInvoke(TargetObject, parameters);
            }
            catch (Exception e)
            {
                throw new TargetInvocationException(e);
            }
        }

        public T Execute<T>(params object[] parameters)
        {
            try
            {
                return (T)Method.DynamicInvoke(TargetObject, parameters);
            }
            catch (Exception e)
            {
                throw new TargetInvocationException(e);
            }
        }

        public async Task<object> ExecuteAsync(params object[] parameters)
        {
            try
            {
                return await Task.Run<object>(() => Execute(parameters)).ConfigureAwait(true);
            }
            catch (Exception e)
            {
                throw new TargetInvocationException(e);
            }
        }

        public async Task<T> ExecuteAsync<T>(params object[] parameters)
        {
            try
            {
                return await Task.Run<T>(() => Execute<T>(parameters)).ConfigureAwait(true);
            }
            catch (Exception e)
            {
                throw new TargetInvocationException(e);
            }
        }

        public object ConvertType(object source, Type destination)
        {
            object newobject = Convert.ChangeType(source, destination);
            return (newobject);
        }

        private void createUniqueKey()
        {
            long time = DateTime.Now.ToBinary();
            Name = getFullName();
            QualifiedName = getQualifiedName();
            ulong seed = Info.DeclaringType.UniqueKey(); //time.UniqueKey32();
            serialcode.UniqueKey = QualifiedName.UniqueKey64();
            serialcode.UniqueSeed = seed;
            serialcode.TimeBlock = time;
        }

        private string getFullName()
        {
            return $"{Info.DeclaringType.FullName}." +
                   $"{Info.Name}";
        }

        private string getQualifiedName()
        {
            return $"{Info.DeclaringType.FullName}." +
                   $"{Info.Name}" +
                   $"{new String(Parameters.SelectMany(p => "." + p.ParameterType.Name).ToArray())}";
        }

        public byte[] GetBytes()
        {
            return serialcode.GetBytes();
        }

        public byte[] GetUniqueBytes()
        {
            return serialcode.GetUniqueBytes();
        }

        public bool Equals(IUnique other)
        {
            return serialcode.Equals(other);
        }

        public int CompareTo(IUnique other)
        {
            return serialcode.CompareTo(other);
        }
    }

    public class Deputies : Catalog<Deputy>
    {
    }

    /// <summary>
    /// Defines the <see cref="ItemChangedEventArgs{T}" />.
    /// </summary>
    /// <typeparam name="T">.</typeparam>
    public class ItemChangedEventArgs<T> : EventArgs
    {
        #region Fields

        /// <summary>
        /// Defines the ChangedItem.
        /// </summary>
        public readonly T ChangedItem;
        /// <summary>
        /// Defines the ChangesType.
        /// </summary>
        public readonly ChangesType ChangesType;
        /// <summary>
        /// Defines the ReplacedWith.
        /// </summary>
        public readonly T ReplacedWith;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemChangedEventArgs{T}"/> class.
        /// </summary>
        /// <param name="changesType">The changesType<see cref="ChangesType"/>.</param>
        /// <param name="changedItem">The changedItem<see cref="T"/>.</param>
        /// <param name="replacement">The replacement<see cref="T"/>.</param>
        public ItemChangedEventArgs(ChangesType changesType, T changedItem,
            T replacement)
        {
            ChangesType = changesType;
            ChangedItem = changedItem;
            ReplacedWith = replacement;
        }

        #endregion
    }
}

using NodaTime;
using System;
using System.Reflection;

namespace Corazon.Saga
{
    public static class SagaFactory
    {
        //public static TSaga Create<TSaga>() where TSaga : Saga
        //{
        //    return Activator.CreateInstance<TSaga>();
        //}

        public static TSaga Create<TSaga>() where TSaga : Saga
        {
            var instance = Activator.CreateInstance<TSaga>();
            return instance;
        }

        public static TSaga Create<TSaga>(SagaId id) where TSaga : Saga
        {
            //return (TSaga)Activator.CreateInstance(typeof(TSaga), new object[] { id });
            //private static readonly ParameterExpression YCreator_Arg_Param = Expression.Parameter(typeof(int), "z");
            //private static readonly Func<int, X> YCreator_Arg = Expression.Lambda<Func<int, X>>(
            //   Expression.New(typeof(Y).GetConstructor(new[] { typeof(int), }), new[] { YCreator_Arg_Param, }),
            //   YCreator_Arg_Param
            //).Compile();
            //static X CreateY_CompiledExpression_Arg(int z)
            //{
            //    return YCreator_Arg(z);
            //}
            var instance = Activator.CreateInstance<TSaga>();
            SetPropertyValue(instance, "Id", id);
            return instance;
        }

        public static Saga Create(Type sagaType, SagaId id)
        {
            var instance = (Saga)Activator.CreateInstance(sagaType);
            SetPropertyValue(instance, "Id", id);
            return instance;
        }

        public static TSaga Create<TSaga>(
            SagaId id, 
            SagaContextId contextId,
            LocalDateTime startedOn, 
            LocalDateTime nextProcessingTimeDueOn,
            LocalDateTime? nextRetryTime,
            bool isExpired, 
            bool isCompleted) where TSaga : Saga
        {
            var instance = Create<TSaga>(id);
            instance.ContextId = contextId;
            instance.StartedOn = startedOn;
            instance.NextProcessingTimeDueOn = nextProcessingTimeDueOn;
            instance.NextRetryTime = nextRetryTime;
            instance.IsExpired = isExpired;
            instance.IsCompleted = isCompleted;
            return instance;
        }

        public static Saga Create(
            Type sagaType, 
            SagaId id,
            SagaContextId contextId,
            LocalDateTime startedOn, 
            LocalDateTime nextProcessingTimeDueOn,
            LocalDateTime? nextRetryTime,
            bool isExpired, 
            bool isCompleted)
        {
            var instance = Create(sagaType, id);
            instance.ContextId = contextId;
            instance.StartedOn = startedOn;
            instance.NextProcessingTimeDueOn = nextProcessingTimeDueOn;
            instance.NextRetryTime = nextRetryTime;
            instance.IsExpired = isExpired;
            instance.IsCompleted = isCompleted;
            return instance;
        }

        public static Saga Create(
            string typeName, 
            SagaId id,
            SagaContextId contextId,
            LocalDateTime startedOn,
            LocalDateTime nextProcessingTimeDueOn,
            LocalDateTime? nextRetryTime,
            bool isExpired, 
            bool isCompleted)
        {
#if NET45
            var instance = (Saga)Activator.CreateInstance(null, typeName).Unwrap();
#else
            var instance = (Saga)Activator.CreateInstance(null, typeName);
#endif
            SetPropertyValue(instance, "Id", id);
            instance.ContextId = contextId;
            instance.StartedOn = startedOn;
            instance.NextProcessingTimeDueOn = nextProcessingTimeDueOn;
            instance.NextRetryTime = nextRetryTime;
            instance.IsExpired = isExpired;
            instance.IsCompleted = isCompleted;
            return instance;
        }

        private static void SetPropertyValue(object source, string propertyName, object propertyValue)
        {
            var propertyInfo = source.GetType().GetProperty(propertyName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);

            if (propertyInfo != null)
            {
                propertyInfo.SetValue(source, propertyValue);
            }
        }

        private static void SetFieldValue(object source, string fieldName, object fieldValue)
        {
            var fieldInfo = source.GetType().GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy);

            if (fieldInfo != null)
            {
                fieldInfo.SetValue(source, fieldValue);
            }
        }
    }
}
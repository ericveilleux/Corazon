using System;

namespace Corazon.Saga
{
    // TODO: I removed the Id parameter because I though it was hard to manage and I figured that a single saga of each type would be enough.
    // However now I realize that it might be interesting to start sagas with particular contexts.  For example, in a multi-tenant world, each
    // saga could run on a single tenant, so the same one would work for multiple contexts.  Same thing when there is a saga that needs to run
    // for each service consumer.  I could also use this to start periodic sagas, by associating them to a tenant or to a top-level structure, 
    // when that structure is initialized by the application, then this event starts all of the periodic sagas for that context.
    // So, instead of working with a sagaid that I need to map logically between the context, I could use a contextid that is optional in these
    // methods and it would therefore support multi-instances.
    public interface ISagaActivator
    {
        /// <summary>
        /// Start the saga by performing the specified action.  Nothing is done if it already exists.
        /// </summary>
        /// <typeparam name="TSaga"></typeparam>
        /// <param name="action"></param>
        void Start<TSaga>(Action<TSaga> action) where TSaga : Saga;

        void Start<TSaga>(SagaContextId contextId, Action<TSaga> action) where TSaga : Saga;

        /// <summary>
        /// Perform the specified action on the saga.  It creates it if it does not exist.
        /// </summary>
        void Act<TSaga>(Action<TSaga> action) where TSaga : Saga;

        void Act<TSaga>(SagaContextId contextId, Action<TSaga> action) where TSaga : Saga;

        /// <summary>
        /// Perform the specified action on an existing saga.  Otherwise it does nothing.
        /// </summary>
        /// <typeparam name="TSaga"></typeparam>
        /// <param name="action"></param>
        void ActExisting<TSaga>(Action<TSaga> action) where TSaga : Saga;

        void ActExisting<TSaga>(SagaContextId contextId, Action<TSaga> action) where TSaga : Saga;

        /// <summary>
        /// Cancels the saga with the specified id. Nothing is done if it does not exist.
        /// </summary>
        void Cancel<TSaga>() where TSaga : Saga;

        void Cancel<TSaga>(SagaContextId contextId) where TSaga : Saga;
    }
}
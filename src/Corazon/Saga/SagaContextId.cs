using System;

namespace Corazon.Saga
{
    public class SagaContextId : Identity
    {
        public SagaContextId()
        {
        }

        public SagaContextId(Guid id)
            : base(id)
        {
        }

        public SagaContextId(Identity id)
            : base(id)
        {
        }
    }
}
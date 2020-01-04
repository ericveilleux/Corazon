using System;

namespace Corazon.Saga
{
    public class SagaId : Identity
    {
        public SagaId()
        {
        }

        public SagaId(Guid id)
            : base(id)
        {
        }

        public SagaId(Identity id)
            : base(id)
        {
        }
    }
}
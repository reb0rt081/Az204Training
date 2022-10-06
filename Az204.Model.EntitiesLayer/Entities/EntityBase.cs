namespace Az204.Model.EntitiesLayer.Entities
{
    public abstract class EntityBase
    {
        protected EntityBase()
        {
            //  Sigue la regla de quien crea el objeto asigna el Id.
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
    }
}

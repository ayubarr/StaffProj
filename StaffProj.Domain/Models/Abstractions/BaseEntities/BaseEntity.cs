namespace StaffProj.Domain.Models.Abstractions.BaseEntities
{
    /// <summary>
    /// Base entity class with a unique identifier.
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// identifier of the entity
        /// </summary>
        public Guid Id { get; set; }
    }
}

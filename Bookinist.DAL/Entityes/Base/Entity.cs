using Bookinist.Repositories;

namespace Bookinist.DAL.Entityes.Base
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}

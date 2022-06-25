
namespace ECS
{
    public class Component
    {
        public bool IsActive { get; set; } = true;

        public int EntityID;
  
        /// <summary>
        /// Override to handle the destruction of your component: Called by the Entity.Destroy()
        /// </summary>
        public virtual void Destroy() { }

        /// <summary>
        /// Override to handle the duplication of your component: Called by the Entity.Duplicate()
        /// </summary>
        public virtual Component Duplicate() { return new Component(); }
    
    }
}

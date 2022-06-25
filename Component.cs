

namespace ECS
{
    public class Component
    {
        public bool IsActive { get; set; } = true;

        public int EntityID;
  
        
        public virtual void Destroy() { }
        public virtual Component Duplicate() { return new Component(); }
    
    }
}

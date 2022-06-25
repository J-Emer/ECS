using System;
using System.Collections.Generic;
using System.Linq;


namespace ECS
{
    public class Entity
    {
        public int ID;
        public bool IsActive { get; set; } = true;
        public string Name;
        public string Tag;
        public List<Component> Components;

        public Entity(string name, string tag, List<Component> components)
        {
            this.Name = name;
            this.Tag = tag;
            this.Components = components;

            EntityWorld.Instance.Add(this);
        }
        /// <summary>
        /// Generates an empty Entity 
        /// Does NOT call the EntityWorld.Add()
        /// </summary>
        public Entity()
        {

        }
        public Entity Duplicate()
        {
            List<Component> _comps = new List<Component>();

            foreach (var item in this.Components)
            {
                _comps.Add(item.Duplicate());
            }


            Entity _copy = new Entity()
            {
                Name = this.Name,
                Tag = this.Tag,
                Components = _comps
            };


            EntityWorld.Instance.Add(_copy);

            return _copy;
        }
        public void Destroy()
        {
            for (int i = 0; i < Components.Count; i++)
            {
                Components[i].Destroy();
            }

            EntityWorld.Instance.Remove(this);
        }
        public bool HasComponent<T>() where T : Component
        {
            var _comp = Components.FirstOrDefault(x => x.GetType() == typeof(T));

            if(_comp != null)
            {
                return true;
            }

            return false;
        }
        public T GetComponent<T>() where T : Component
        {
            var _comp = Components.FirstOrDefault(x => x.GetType() == typeof(T));

            if (_comp != null)
            {
                return (T)_comp;
            }

            return null;
        }
        public override string ToString()
        {
            return $"Entity| ID: {this.ID} | Name: {this.Name} | Tag: {this.Tag} | IsActive: {this.IsActive} | Component Count: {this.Components.Count}";
        }
    }
}

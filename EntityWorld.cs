using System;
using System.Collections.Generic;
using System.Text;

namespace ECS
{
    public class EntityWorld
    {
        public static EntityWorld Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new EntityWorld();
                }
                return _instance;
            }
        }
        private static EntityWorld _instance;

        public List<Entity> Entities => _entities;
        private List<Entity> _entities = new List<Entity>();


        public event Action OnEntitiesChanged;
        public event Action OnEntitiesCleared;

        private Random _rand = new Random();
        private int _nextID = 0;


        public void Add(Entity _ent)
        {
            if(!_entities.Contains(_ent))
            {
                _entities.Add(_ent);
                _ent.ID = GetNextID();

                foreach (var item in _ent.Components)
                {
                    item.EntityID = _ent.ID;
                }

                OnEntitiesChanged?.Invoke();
            }
        }
        public void Add(List<Entity> _newEnts)
        {
            foreach (var ent in _newEnts)
            {
                ent.ID = GetNextID();

                for (int i = 0; i < ent.Components.Count; i++)
                {
                    ent.Components[i].EntityID = ent.ID;
                }

                _entities.Add(ent);
            }

            OnEntitiesChanged?.Invoke();
        }
        public void Remove(Entity _ent)
        {
            _entities.Remove(_ent);
            OnEntitiesChanged?.Invoke();
        }
        public void Clear()
        {
            _entities.Clear();
            OnEntitiesCleared?.Invoke();
        }
        private int GetNextID()
        {
            _nextID += _rand.Next(1, 10);
            return _nextID;
        }
        /// <summary>
        /// Forces the OnEntitiesChanged event to fire
        /// </summary>
        public void ForceRefresh()
        {
            OnEntitiesChanged?.Invoke();
        }
        public Entity GetEntityByID(int id)
        {
            return _entities.Find(x => x.ID == id);
        }
        public List<Entity> GetEntitiesByName(string _name)
        {
            return _entities.FindAll(x => x.Name == _name);
        }
        public List<Entity> GetEntitiesByTag(string _tag)
        {
            return _entities.FindAll(x => x.Tag == _tag);
        }
        public List<Entity> GetEntitiesWithComponents<T>() where T : Component
        {
            return _entities.FindAll(x => x.HasComponent<T>() == true);
        }
        public List<Entity> GetEntitiesWithComponents<T, T2>() where T : Component where T2 : Component
        {
            return _entities.FindAll(x => (x.HasComponent<T>() == true) && (x.HasComponent<T2>() == true));
        }
        public List<Entity> GetEntitiesWithComponents<T, T2, T3>() where T : Component where T2 : Component where T3 : Component
        {
            return _entities.FindAll(x => (x.HasComponent<T>() == true) && (x.HasComponent<T2>() == true) && (x.HasComponent<T3>() == true));
        }
        /// <summary>
        /// Returns a list of all the specified components
        /// </summary>
        /// <typeparam name="T">type of component</typeparam>
        /// <returns></returns>
        public List<T> GetAllComponents<T>() where T : Component
        {
            List<Entity> _entsWithComponents = GetEntitiesWithComponents<T>();

            List<T> _comps = new List<T>();

            _entities.ForEach(x => _comps.Add(x.GetComponent<T>()));

            return _comps;
        }




        public override string ToString()
        {
            StringBuilder _sb = new StringBuilder();

            foreach (var item in _entities)
            {
                _sb.Append(item.ToString());
            }

            return _sb.ToString();
        }
    }
}

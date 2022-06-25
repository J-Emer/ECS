using System.Collections.Generic;


using Microsoft.Xna.Framework;

namespace ECS
{
    public abstract class BaseSystem<T> : DrawableGameComponent where T : Component
    {
        /// <summary>
        /// All Entities that have a Component of Type<T>
        /// </summary>
        protected List<Entity> Entities = new List<Entity>();
        /// <summary>
        /// All of the Entities that have their IsActive = true
        /// </summary>
        protected List<Entity> ActiveEntities => Entities.FindAll(x => x.IsActive);

        public BaseSystem(Game _game) : base(_game)
        {
            _game.Components.Add(this);
            _game.Services.AddService(this.GetType(), this);

            EntityWorld.Instance.OnEntitiesChanged += Instance_OnEntitiesChanged;
            EntityWorld.Instance.OnEntitiesCleared += Instance_OnEntitiesCleared;
        }

        protected void Instance_OnEntitiesCleared()
        {
            Entities.Clear();
        }
        protected void Instance_OnEntitiesChanged()
        {
            BeforeEntitiesChanged();
            
            Entities = EntityWorld.Instance.GetEntitiesWithComponents<T>();
            
            AfterEntitiesChanged();
        }
        protected virtual void BeforeEntitiesChanged() { }
        protected virtual void AfterEntitiesChanged() { }

        public override string ToString()
        {
            return $"{this.GetType().Name} | Entity Count: {Entities.Count}";
        }
    }

    public abstract class BaseSystem<T, T2> : DrawableGameComponent where T : Component where T2 : Component
    {
        /// <summary>
        /// All Entities that have a Component of Type<T>
        /// </summary>
        protected List<Entity> Entities = new List<Entity>();
        /// <summary>
        /// All of the Entities that have their IsActive = true
        /// </summary>
        protected List<Entity> ActiveEntities => Entities.FindAll(x => x.IsActive);

        public BaseSystem(Game _game) : base(_game)
        {
            _game.Components.Add(this);
            _game.Services.AddService(this.GetType(), this);

            EntityWorld.Instance.OnEntitiesChanged += Instance_OnEntitiesChanged;
            EntityWorld.Instance.OnEntitiesCleared += Instance_OnEntitiesCleared;
        }

        protected void Instance_OnEntitiesCleared()
        {
            Entities.Clear();
        }

        protected void Instance_OnEntitiesChanged()
        {
            BeforeEntitiesChanged();
            Entities = EntityWorld.Instance.GetEntitiesWithComponents<T, T2>();
            AfterEntitiesChanged();
        }
        protected virtual void BeforeEntitiesChanged() { }
        protected virtual void AfterEntitiesChanged() { }
        public override string ToString()
        {
            return $"{this.GetType().Name} | Entity Count: {Entities.Count}";
        }
    }

    public abstract class BaseSystem<T, T2, T3> : DrawableGameComponent where T : Component where T2 : Component where T3 : Component
    {
        /// <summary>
        /// All Entities that have a Component of Type<T>
        /// </summary>
        protected List<Entity> Entities = new List<Entity>();
        /// <summary>
        /// All of the Entities that have their IsActive = true
        /// </summary>
        protected List<Entity> ActiveEntities => Entities.FindAll(x => x.IsActive);

        public BaseSystem(Game _game) : base(_game)
        {
            _game.Components.Add(this);
            _game.Services.AddService(this.GetType(), this);

            EntityWorld.Instance.OnEntitiesChanged += Instance_OnEntitiesChanged;
            EntityWorld.Instance.OnEntitiesCleared += Instance_OnEntitiesCleared;
        }

        protected void Instance_OnEntitiesCleared()
        {
            Entities.Clear();
        }

        protected void Instance_OnEntitiesChanged()
        {
            BeforeEntitiesChanged();
            Entities = EntityWorld.Instance.GetEntitiesWithComponents<T, T2, T3>();
            AfterEntitiesChanged();
        }
        protected virtual void BeforeEntitiesChanged() { }
        protected virtual void AfterEntitiesChanged() { }
        public override string ToString()
        {
            return $"{this.GetType().Name} | Entity Count: {Entities.Count}";
        }
    }
}

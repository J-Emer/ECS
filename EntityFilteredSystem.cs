using System.Collections.Generic;


using Microsoft.Xna.Framework;

namespace ECS
{
    public class EntityFilteredSystem : DrawableGameComponent
    {
        //goal: 
            //only take entities that have a specific property ie(tag, name)

        /// <summary>
        /// All Entities that have a Component of Type<T>
        /// </summary>
        protected List<Entity> Entities = new List<Entity>();

        /// <summary>
        /// All of the Entities that have their IsActive = true
        /// </summary>
        protected List<Entity> ActiveEntities => Entities.FindAll(x => x.IsActive);

        /// <summary>
        /// String value used to find all entities by name or tag
        /// </summary>
        protected string FilterString;

        public EntityFilteredSystem(Game _game, string _filterString) : base(_game)
        {
            _game.Components.Add(this);
            EntityWorld.Instance.OnEntitiesChanged += EntitiesChanged;
            EntityWorld.Instance.OnEntitiesCleared += EntitiesCleared;

        }
        private void EntitiesChanged()
        {
            BeforeEntitiesChanged();
            
            Filter();
            
            AfterEntitiesChanged();
        }
        private void EntitiesCleared()
        {
            Entities.Clear();
        }

        protected virtual void Filter(){}
        protected virtual void BeforeEntitiesChanged() { }
        protected virtual void AfterEntitiesChanged() { }
    }

    public class EntityFilterByTagSystem : EntityFilteredSystem
    {
        public EntityFilterByTagSystem(Game _game, string _filterString) : base(_game, _filterString){}

        protected override void Filter()
        {
            Entities = EntityWorld.Instance.GetEntitiesByTag(FilterString);
        }
    }
    public class EntityFilterByNameSystem : EntityFilteredSystem
    {
        public EntityFilterByNameSystem(Game _game, string _filterString) : base(_game, _filterString){}

        protected override void Filter()
        {
            Entities = EntityWorld.Instance.GetEntitiesByName(FilterString);
        }
    }
}

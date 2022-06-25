# ECS

This is a simple ECS that I use for my Monogame projects. Built aginst .net5.0, and Monogame OpenGl v3.8.0.1641

ECS doesn't come with Components or Systems out of the box. You will have to build those yourself. Instead ECS provides a simple framework for you to build your systems and components off of. 

## Setup
As of now there is no Nuget, so you will have to download and build yourself. Download the .zip, unpack and from the projects root run ```dotnet build``` this will build the ```.dll``` which you can reference in your project. 


## Creating Components
All components inherite from the base Component class. Here we are making 2 components. The ```Mover``` and ```Position``` components. 

``` csharp
    public class Position : Component
    {
        public Vector2 Pos;

        //Constructor is optional
        public Position(Vector2 _pos)
        {
            this.Pos = _pos;
        }

    }

    public class Mover : Component
    {
        public float MoveSpeed { get; set; } = 5f;
    }
```

## Creating Systems
All Systems inherite from the BaseSystem, which is a generic class with 3 overloads. BaseSystem itself inherites from the ```DrawableGameComponent``` which allows you to override the ```Update()``` or ```Draw()``` or both depending on your needs. Lets create a system that will use both our ```Position``` and ```Mover``` components. Here we are using inheriting from the ```BaseSystem<T, T2>``` where T and T2 are both components. Then we are overriding the the ```Update()```. In the update we are looping through all of the ```ActiveEntities``` this is all the Entities that this system has that have their ```IsActive = true```. You could loop over all ```Entities``` but it will not perform the isActive check. 

``` csharp
using ECS;
using Microsoft.Xna.Framework;

    public class MoverSystem : BaseSystem<Mover, Position>
    {
        public MoverSystem(Game _game) : base(_game)
        {
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var Entity in ActiveEntities)
            {
                Mover _moverComponent = Entity.GetComponent<Mover>();
                Position _posComponent = Entity.GetComponent<Position>();

                _posComponent.Pos += new Vector2(_moverComponent.MoveSpeed, _moverComponent.MoveSpeed);
            }
        }
    }
```

## Game1 Setup

All Systems and Entities depend on the ```EntityWorld``` which is a Singleton. After creating your EntityWorld, you can begin to add in the ```Systems``` that you created. Here we will add in the ```MoverSystem``` we created above. 

``` csharp
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            new EntityWorld();
            new MoverSystem(this);
        }
```

As mentioned above each ```BaseSystem``` inherites from the DrawableGameComponent class. This means calling your Systems ```Update()``` and ```Draw()``` will be handled for you by the Game1 itself. In addition to the each system also adds itself as a service to the Game.
If you need to get a System then use ```        Game1.Instance.Services.GetService<MoverSystem>();```


## Adding Entities

After initializing your EntityWorld and any Systems you may have. Its time to create some Entities. 

``` csharp
            Entity _testEntity = new Entity("Test Entity", "Default", new List<Component>
                                                                                        {
                                                                                            new Position(new Vector2(100, 100)),
                                                                                            new Mover()
                                                                                        });
```

We just create and Entity with a Name = "Test Entity, a Tag = "Default" and a list of components (Position, and Mover). Adding this Entity to the EntityWorld, and adding components to your Systems was handled automatically on creation.











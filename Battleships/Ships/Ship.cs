namespace Battleships.Ships
{
    public abstract class Ship
    {
        public int Size { get; protected set; }
        public List<Field> PositionFields { get; set; }
        public string Name { get; protected set; }

        public Ship()
        {            
        }

        public bool IsHit(Field field)
        {
            return PositionFields.Contains(field);
        }

        public bool IsDestroyed()
        {
            return PositionFields.All(f => f.IsHit == true);
        }        

    }
}

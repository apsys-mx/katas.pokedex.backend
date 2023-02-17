namespace katas.pokedex
{
    public class Pokemon
    {
        public virtual string Id { get; set; }
        public virtual int Code { get;set; }
        public virtual string Name { get; set; }

        public Pokemon()
        { }

        public Pokemon(int code, string name)
        {
            Code = code;
            Name = name;
        }

    }
}
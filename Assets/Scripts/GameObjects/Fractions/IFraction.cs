namespace Assets.Scripts.GameObjects.Fractions {
    
    public interface IFraction
    {
        FractionType FractionType { get; }
    }
    public interface IModifyFraction
    {
        FractionType FractionType { get; set; }
    }
}
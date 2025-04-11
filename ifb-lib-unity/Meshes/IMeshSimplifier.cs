namespace IFBLibUnity.Meshes
{
    public interface IMeshSimplifier
    {
        void Simplify(EMeshSimplifyStrength simplifyStrength);
        void SimplifyLow();
        void SimplifyMid();
        void SimplifyHigh();
        void ResetSimplification();
    }
}
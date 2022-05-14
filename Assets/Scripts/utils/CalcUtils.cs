public class CalcUtils
{
    public static float GetValorPositivo(float val)
    {
        return val > 0 ? val : val * -1;
    }
    public static int GetDirecao(float val)
    {
        return val >= 0 ? 1 : -1;
    }
    public static int GetDirecaoInvertida(float val)
    {
        return val >= 0 ? -1 : 1;
    }
}

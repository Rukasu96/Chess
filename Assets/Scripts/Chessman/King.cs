public class King : Chessman, IKingable
{
    private bool canUseCastling = true;

    public bool CanUseCastling()
    {
        return canUseCastling;
    }

    public void UpdateCastlingStatus()
    {
        canUseCastling = false;
    }
}

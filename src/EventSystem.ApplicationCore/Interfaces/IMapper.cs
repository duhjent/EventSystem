namespace EventSystem.ApplicationCore.Interfaces
{
    public interface IMapper<TIn, TOut>
    {
        TOut Map(TIn input);
        TIn MapReverse(TOut input);
    }
}

namespace Application.Interface;

public interface ITransformer<T1, T2>
{
    Task<T1> Transform(T2 data);

    Task<T2> Transform(T1 data);
}
using System.Linq.Expressions;

namespace Base.Contracts;

public interface IMapper<TIn, TOut>
{
    TOut? Map(TIn? entity);
    TIn? Map(TOut? entity);
   

}
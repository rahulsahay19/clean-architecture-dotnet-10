namespace Ordering.Application.Abstractions
{
    //Marker Interface not returning result
    public interface ICommand 
    {
    
    }
    //Marker Interface returning result
    public interface ICommand<TResult>
    {
    }
}

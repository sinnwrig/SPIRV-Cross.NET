namespace SPIRVCross.NET;

public class BaseChildObject : IParentObject
{ 
    public readonly IParentObject rootParent;
    public readonly IParentObject parent;

    public bool IsAlive => parent.IsAlive;

    public BaseChildObject(IParentObject parent)
    {
        this.parent = parent;

        if (parent is BaseChildObject childObject)
            this.rootParent = childObject.rootParent; 
        else
            this.rootParent = parent;
    }

    public void Validate(Exception? exception = null)
    {   
        if (!IsAlive)
            throw exception ?? new MissingParentException($"Parent of child object {GetType().Name} is missing or no longer alive.");
    }

    public void CompareParent(IParentObject other, Exception? exception = null)
    {
        if (!IsAlive || parent != rootParent)
            throw exception ?? new InvalidParentException($"Parent {other} does not match child object parent {parent.GetType().Name}.");
    }

    public void CompareRoot(IParentObject root, Exception? exception = null)
    {
        if (!IsAlive || root != rootParent)
            throw exception ?? new InvalidParentException($"Root parent {root} does not match child object parent {rootParent.GetType().Name}");
    }
}

public class ChildObject<T> : BaseChildObject where T : IParentObject
{
    public new readonly T parent;

    internal ChildObject(T parent) : base(parent)
    {
        this.parent = parent;
    }
}

public interface IParentObject 
{
    public bool IsAlive { get; }
}


public class MissingParentException : Exception
{
    public MissingParentException() { }
    public MissingParentException(string? message) : base(message) { }
    public MissingParentException(string? message, Exception? inner) : base(message, inner) { }
}

public class InvalidParentException : Exception
{
    public InvalidParentException() { }
    public InvalidParentException(string? message) : base(message) { }
    public InvalidParentException(string? message, Exception? inner) : base(message, inner) { }
}
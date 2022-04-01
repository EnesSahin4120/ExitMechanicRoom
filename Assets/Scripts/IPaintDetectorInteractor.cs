public interface IDetectablePaint
{
    public void Execute();
}

public interface IPaintDetectorInteractor<T> where T : IDetectablePaint
{
    public void Interact(T detectablePaint);
}

public interface IInteractableBin
{
    public void Execute();
}

public interface IBinInteractor<T> where T : IInteractableBin
{
    public void Interact(T interactableBin);
}
